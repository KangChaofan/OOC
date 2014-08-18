<<<<<<< HEAD
#region Copyright
/*
* Copyright (c) 2005,2006,2007, OpenMI Association
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the OpenMI Association nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "OpenMI Association" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "OpenMI Association" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Threading;
using Oatc.OpenMI.Sdk.Buffer;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using Oatc.OpenMI.Sdk.Wrapper;
using OOC.Util;


namespace OOC.OpenMIWrapper
{
    public delegate void AfterSimulationDelegate(object sender, bool succeed);
    public delegate void CompositionModelProgressChangedDelegate(object sender, string modelId, string progress);

    /// <summary>
    /// Summary description for CompositionManager.
    /// 
    /// TODO: it should be called Composition
    /// </summary>
    public class CompositionManager
    {
        public AfterSimulationDelegate AfterSimulationHandler;
        public CompositionModelProgressChangedDelegate CompositionModelProgressChangedHandler;
        public bool Parallelized { get; set; }
        public ParallelizeMode ParallelizeMode = ParallelizeMode.Aggressive;
        private DateTime startTime, endTime;

        #region Static members

        private static Event _simulationFinishedEvent;
        private static Event _simulationFailedEvent;

        static CompositionManager()
        {
            _simulationFinishedEvent = new Event(EventType.GlobalProgress);
            _simulationFinishedEvent.Description = "Simulation finished successfuly...";

            _simulationFailedEvent = new Event(EventType.GlobalProgress);
            _simulationFailedEvent.Description = "Simulation FAILED...";
        }


        /// <summary>
        /// Special event saying that simulation has finished.
        /// </summary>
        public static IEvent SimulationFinishedEvent
        {
            get { return (_simulationFinishedEvent); }
        }

        /// <summary>
        /// Special event saying that simulation has failed.
        /// </summary>
        public static IEvent SimulationFailedEvent
        {
            get { return (_simulationFailedEvent); }
        }


        /// <summary>
        /// Unique ID of trigger "model".
        /// </summary>
        /// <remarks>Standard models cannot have this ID.</remarks>
        public const string TriggerModelID = "Oatc.OpenMI.Gui.Trigger";

        #endregion

        #region Internal members

        Thread _runThread;
        bool _running;
        bool _runPrepareForComputationStarted;
        IListener _runListener;
        bool _runInSameThread;

        IList<Model> _models;
        ArrayList _connections;
        bool[] _listenedEventTypes;
        DateTime _triggerInvokeTime;
        Dictionary<ILinkableComponent, string> cmGuidMapping;

        #endregion

        /// <summary>
        /// Creates a new empty instance of <c>CompositionManager</c> class.
        /// </summary>
        /// <remarks>See <see cref="Initialize">Initialize</see> for more detail.</remarks>
        public CompositionManager()
        {
            Initialize();
        }


        #region Public properties

        /// <summary>
        /// Gets list of all models (ie. instances of <see cref="Model">UIModel</see> class) in composition.
        /// </summary>
        public IList<Model> Models
        {
            get { return _models; }
            set { _models = value; }
        }


        /// <summary>
        /// Gets list of all connections (ie. instances of <see cref="Connection">UIConnection</see> class) in composition.
        /// </summary>
        public ArrayList Connections
        {
            get { return (_connections); }
        }


        /// <summary>
        /// Gets array of <c>bool</c> describing which events should be listened during simulation run.
        /// </summary>
        /// <remarks>Array has <see cref="EventType.NUM_OF_EVENT_TYPES">EventType.NUM_OF_EVENT_TYPES</see>
        /// elements. See <see cref="EventType">EventType</see>, <see cref="Run">Run</see> for more detail.
        /// </remarks>
        public bool[] ListenedEventTypes
        {
            get { return (_listenedEventTypes); }
            /*	set
                {
                    Debug.Assert( value.Length == (int)EventType.NUM_OF_EVENT_TYPES );
                    _listenedEventTypes = value;
                    _shouldBeSaved = true;
                }*/
        }


        /// <summary>
        /// Time when trigger should be invoked.
        /// </summary>
        /// <remarks>See <see cref="EventType">EventType</see> and <see cref="Run">Run</see> for more detail.</remarks>
        public DateTime TriggerInvokeTime
        {
            get { return (_triggerInvokeTime); }
            set
            {
                if (_triggerInvokeTime != value)
                {
                    _triggerInvokeTime = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether simulation should be run in same thread. By default it's <c>false</c>.
        /// </summary>
        /// <remarks>
        /// This is only recommendation of composition author, you can override
        /// this setting while calling <see cref="Run">Run</see> method. For example
        /// if running from console, simulation is always executed in same thread.
        /// </remarks>
        public bool RunInSameThread
        {
            get { return (_runInSameThread); }
            set
            {
                if (_runInSameThread != value)
                {
                    _runInSameThread = value;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Initializes this composition.
        /// </summary>
        public void Initialize()
        {
            _models = new List<Model>();
            _connections = new ArrayList();
            _listenedEventTypes = new bool[(int)EventType.NUM_OF_EVENT_TYPES];
            for (int i = 0; i < (int)EventType.NUM_OF_EVENT_TYPES; i++)
                _listenedEventTypes[i] = false;

            _triggerInvokeTime = new DateTime(1900, 1, 1);
            _runPrepareForComputationStarted = false;
            _runThread = null;
            _running = false;
            _runListener = null;
            _runInSameThread = false;
            cmGuidMapping = new Dictionary<ILinkableComponent, string>();

            OnInitialized();
        }

        /// <summary>
        /// Executed after composition was initialized, allows to extend functionality in derived classes.
        /// </summary>
        public virtual void OnInitialized()
        {
        }

        /// <summary>
        /// Releases all models and intializes this composition.
        /// </summary>
        public void Release()
        {
            RemoveAllModels();
            Initialize();
        }

        public Model GetModel(string modelId)
        {
            foreach (Model model in _models)
            {
                if (model.ModelID == modelId)
                {
                    return model;
                }
            }
            return null;
        }

        public void AddModel(Model model)
        {
            cmGuidMapping[model.LinkableComponent] = model.ModelID;
            _models.Add(model);
        }

        /// <summary>
        /// Removes specified model from composition.
        /// </summary>
        /// <param name="model">Model to be removed.</param>
        /// <remarks>The <c>Dispose</c> method is called on the model.</remarks>
        public void RemoveModel(Model model)
        {
            // first remove all links from/to this model
            Connection[] copyOfLinks = (Connection[])_connections.ToArray(typeof(Connection));
            foreach (Connection uiLink in copyOfLinks)
                if (uiLink.AcceptingModel == model || uiLink.ProvidingModel == model)
                    RemoveConnection(uiLink);

            try
            {
                // We call Finish() after computation finished,
                // Dispose() when removing models
                model.LinkableComponent.Dispose();
            }
            catch
            {
                // we don't care about just disposed model, so do nothing...
            }

            _models.Remove(model); // remove model itself
        }


        /// <summary>
        /// Removes all model from composition.
        /// </summary>
        /// <remarks>See <see cref="RemoveModel">RemoveModel</see> for more detail.</remarks>
        public void RemoveAllModels()
        {
            while (Models.Count > 0)
            {
                RemoveModel(Models[0]);
            }
        }

        /// <summary>
        /// Creates new connection between two models in composition.
        /// </summary>
        /// <param name="providingModel">Source model</param>
        /// <param name="acceptingModel">Target model</param>
        /// <remarks>Connection between two models is just abstraction which can hold links between models.
        /// The direction of connection and its links is same. There can be only one connection between two models.</remarks>
        public Connection GetConnection(string providingModelId, string acceptingModelId)
        {
            if (providingModelId == acceptingModelId)
                throw (new Exception("Cannot connect model with itself."));

            Model providingModel = null;
            Model acceptingModel = null;

            // Check whether both models exist
            bool providingFound = false, acceptingFound = false;
            foreach (Model model in _models)
            {
                if (model.ModelID == providingModelId)
                {
                    providingModel = model;
                    providingFound = true;
                }
                if (model.ModelID == acceptingModelId)
                {
                    acceptingModel = model;
                    acceptingFound = true;
                }
            }
            if (!providingFound || !acceptingFound)
                throw (new Exception("Cannot find providing or accepting."));

            // check whether this link isn't already here (if yes, do nothing)
            foreach (Connection link in _connections)
                if (link.ProvidingModel == providingModel && link.AcceptingModel == acceptingModel)
                    return link;

            Connection connection = new Connection(providingModel, acceptingModel);
            _connections.Add(connection);
            return connection;
        }

        public void AddLink(string linkId, string sourceModelId, string targetModelId, string sourceQuantity, string targetQuantity, string sourceElementSet, string targetElementSet)
        {
            var dataOperationsToAdd = new ArrayList();
            Connection connection = GetConnection(sourceModelId, targetModelId);
            Model sourceModel = GetModel(sourceModelId);
            Model targetModel = GetModel(targetModelId);
            var outputExchangeItem = sourceModel.GetOutputExchangeItem(sourceElementSet, sourceQuantity);
            var inputExchangeItem = targetModel.GetInputExchangeItem(targetElementSet, targetQuantity);
            if (outputExchangeItem == null || inputExchangeItem == null)
                throw (new Exception(
                    "Cannot find exchange item"));
            Link link = new Link(
                sourceModel.LinkableComponent,
                outputExchangeItem.ElementSet,
                outputExchangeItem.Quantity,
                targetModel.LinkableComponent,
                inputExchangeItem.ElementSet,
                inputExchangeItem.Quantity,
                "No description available.",
                linkId,
                dataOperationsToAdd);
            connection.Links.Add(link);
            sourceModel.LinkableComponent.AddLink(link);
            targetModel.LinkableComponent.AddLink(link);
        }
        /// <summary>
        /// Removes connection between two models.
        /// </summary>
        /// <param name="connection">Connection to be removed.</param>
        public void RemoveConnection(Connection connection)
        {
            // remove ILinks from both connected components
            if (!_runPrepareForComputationStarted)
                foreach (ILink link in connection.Links)
                {
                    string linkID = link.ID;
                    ILinkableComponent
                        sourceComponent = link.SourceComponent,
                        targetComponent = link.TargetComponent;


                    sourceComponent.RemoveLink(linkID);
                    targetComponent.RemoveLink(linkID);
                }

            _connections.Remove(connection);
        }

        /// <summary>
        /// Calculates time horizon of the simulation,
        /// ie. time between earliest model start and latest model end.
        /// </summary>
        /// <returns>Returns simulation time horizon.</returns>
        public ITimeSpan GetSimulationTimehorizon()
        {
            TimeStamp start = new TimeStamp(double.MaxValue),
                end = new TimeStamp(double.MinValue);

            foreach (Model model in _models)
            {
                if (model.ModelID == CompositionManager.TriggerModelID)
                    continue;

                if (model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay == 0.0 || model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay > 100000)
                {
                    continue; // wrong time horizon is used by model
                }

                start.ModifiedJulianDay = Math.Min(start.ModifiedJulianDay, model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay);
                end.ModifiedJulianDay = Math.Max(end.ModifiedJulianDay, model.LinkableComponent.TimeHorizon.End.ModifiedJulianDay);
            }

            Debug.Assert(start.ModifiedJulianDay < end.ModifiedJulianDay);

            return (new Oatc.OpenMI.Sdk.Backbone.TimeSpan(start, end));
        }


        /// <summary>
        /// Runs simulation.
        /// </summary>
        /// <param name="runListener">Simulation listener.</param>
        /// <param name="runInSameThread">If <c>true</c>, simulation is run in same thread like caller,
        /// ie. method blocks until simulation don't finish. If <c>false</c>, simulation is
        /// run in separate thread and method returns immediately.</param>
        /// <remarks>
        /// Simulation is run the way that trigger invokes <see cref="ILinkableComponent.GetValues">ILinkableComponent.GetValues</see>
        /// method of the model it's connected to
        /// at the time specified by <see cref="TriggerInvokeTime">TriggerInvokeTime</see> property.
        /// If you need to use more than one listener you can use <see cref="ProxyListener">ProxyListener</see>
        /// class or <see cref="ProxyMultiThreadListener">ProxyMultiThreadListener</see> if <c>runInSameThread</c> is <c>false</c>.
        /// </remarks>
        public void Run(Logger logger, bool runInSameThread)
        {
            LoggerListener loggerListener = new LoggerListener(logger);
            ProgressListener progressListener = new ProgressListener();
            progressListener.ModelProgressChangedHandler += new ModelProgressChangedDelegate(delegate(ILinkableComponent linkableComponent, ITimeStamp simTime)
            {
                string guid = cmGuidMapping[linkableComponent];
                string progress = CalendarConverter.ModifiedJulian2Gregorian(simTime.ModifiedJulianDay).ToString();
                CompositionModelProgressChangedHandler(this, guid, progress);
            });

            ArrayList listeners = new ArrayList();
            listeners.Add(loggerListener);
            listeners.Add(progressListener);
            ProxyListener proxyListener = new ProxyListener();
            proxyListener.Initialize(listeners);

            startTime = DateTime.Now;

            if (_running)
                throw (new Exception("Simulation is already running."));

            _running = true;
            _runListener = proxyListener;

            try
            {
                TimeStamp runToTime = new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(_triggerInvokeTime));

                // Create informative message
                if (_runListener != null)
                {
                    StringBuilder description = new StringBuilder();
                    description.Append("Starting simulation at ");
                    description.Append(DateTime.Now.ToString());
                    description.Append(",");

                    description.Append(" composition consists from following models:\n");
                    foreach (Model model in _models)
                    {
                        description.Append(model.ModelID);
                        description.Append(", ");
                    }

                    // todo: add more info?

                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = description.ToString();
                    _runListener.OnEvent(theEvent);
                }

                _runPrepareForComputationStarted = true;

                // prepare for computation
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Preparing for computation....";
                    _runListener.OnEvent(theEvent);
                }

                // subscribing event listener to all models
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Subscribing proxy event listener....";
                    _runListener.OnEvent(theEvent);

                    for (int i = 0; i < _runListener.GetAcceptedEventTypeCount(); i++)
                        foreach (Model uimodel in _models)
                        {
                            theEvent = new Event(EventType.Informative);
                            theEvent.Description = "Calling Subscribe() method with EventType." + ((EventType)i).ToString() + " of model " + uimodel.ModelID;
                            _runListener.OnEvent(theEvent);

                            for (int j = 0; j < uimodel.LinkableComponent.GetPublishedEventTypeCount(); j++)
                                if (uimodel.LinkableComponent.GetPublishedEventType(j) == _runListener.GetAcceptedEventType(i))
                                {
                                    uimodel.LinkableComponent.Subscribe(_runListener, _runListener.GetAcceptedEventType(i));
                                    break;
                                }
                        }
                }

                if (!runInSameThread)
                {
                    // creating run thread
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Creating run thread....";
                        _runListener.OnEvent(theEvent);
                    }

                    _runThread = new Thread(RunThreadFunction) { Priority = ThreadPriority.Normal };

                    // starting thread...
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.GlobalProgress);
                        theEvent.Description = "Starting run thread....";
                        _runListener.OnEvent(theEvent);
                    }

                    _runThread.Start();
                }
                else
                {
                    // run simulation in same thread (for example when running from console)
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Running simulation in same thread....";
                        _runListener.OnEvent(theEvent);
                    }
                    RunThreadFunction();
                }
            }
            catch (System.Exception e)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Exception occured while initiating simulation run: " + e.ToString();
                    _runListener.OnEvent(theEvent);
                    _runListener.OnEvent(SimulationFailedEvent); // todo: add info about time to this event

                    endTime = DateTime.Now;
                    var ev = new Event
                    {
                        Description = "Elapsed time: " + (endTime - startTime),
                        Type = EventType.GlobalProgress
                    };

                    _runListener.OnEvent(ev);
                }
            }
            finally
            {
            }

        }


        /// <summary>
        /// Stops the simulation.
        /// </summary>
        /// <remarks>This method has effect only if simulation is run in separate thread
        /// (see <see cref="Run">Run</see> method).
        /// This method calls <see cref="Thread.Abort()">Abort</see> method on the simulation thread.</remarks>
        public void Stop()
        {
            if (_running && _runThread != null)
                _runThread.Abort();
            _runThread = null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// This method is called in <see cref="Run">Run</see> method.
        /// </summary>
        private void RunThreadFunction()
        {
            bool succeed = false;
            foreach (Model uimodel in _models)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Calling Prepare() method of model " + uimodel.ModelID;
                    _runListener.OnEvent(theEvent);
                }
                uimodel.LinkableComponent.Prepare();
            }

            //Trigger trigger = GetTrigger();

            //Debug.Assert(trigger != null);

            //WHAT THE HELL IS THIS FOR?
            Thread.Sleep(0);

            try
            {
                // run it !!!
                // trigger.Run(new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(TriggerInvokeTime)));
                ITime triggerTime = new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(TriggerInvokeTime));

                CompositionRunner runner = new CompositionRunner(_models, _runListener);
                if (Parallelized) runner.RunParallel(triggerTime, ParallelizeMode);
                else runner.RunSequence(triggerTime);

                // close models down
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Closing models down...";
                    _runListener.OnEvent(theEvent);
                }

                foreach (Model uimodel in _models)
                {
                    if (_runListener != null)
                    {
                        string ModelID = uimodel.ModelID;
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Calling Finish() on model " + ModelID;
                        _runListener.OnEvent(theEvent);
                    }
                    uimodel.LinkableComponent.Finish();
                }

                // thread finishes - send well known event
                if (_runListener != null)
                {
                    _simulationFinishedEvent.Description = "Simulation finished successfuly at " + DateTime.Now.ToString() + "...";
                    _runListener.OnEvent(SimulationFinishedEvent);
                }
                succeed = true;
            }
            catch (Exception e)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Exception occured during simulation: " + e.ToString();
                    _runListener.OnEvent(theEvent);

                    _simulationFailedEvent.Description = "Simulation FAILED at " + DateTime.Now.ToString() + "...";
                    _runListener.OnEvent(SimulationFailedEvent);
                }
            }
            finally
            {
                endTime = DateTime.Now;
                var ev = new Event
                {
                    Description = "Elapsed time: " + (endTime - startTime),
                    Type = EventType.GlobalProgress
                };

                _runListener.OnEvent(ev);

                _running = false;
                _runListener = null; // release listener

            }
            if (AfterSimulationHandler != null)
            {
                AfterSimulationHandler(this, succeed);
            }
        }


        #endregion
    }
}
=======
#region Copyright
/*
* Copyright (c) 2005,2006,2007, OpenMI Association
* All rights reserved.
*
* Redistribution and use in source and binary forms, with or without
* modification, are permitted provided that the following conditions are met:
*     * Redistributions of source code must retain the above copyright
*       notice, this list of conditions and the following disclaimer.
*     * Redistributions in binary form must reproduce the above copyright
*       notice, this list of conditions and the following disclaimer in the
*       documentation and/or other materials provided with the distribution.
*     * Neither the name of the OpenMI Association nor the
*       names of its contributors may be used to endorse or promote products
*       derived from this software without specific prior written permission.
*
* THIS SOFTWARE IS PROVIDED BY "OpenMI Association" ``AS IS'' AND ANY
* EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
* WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
* DISCLAIMED. IN NO EVENT SHALL "OpenMI Association" BE LIABLE FOR ANY
* DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
* (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
* LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
* ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
* (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
* SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;
using System.IO;
using System.Collections;
using System.Threading;
using Oatc.OpenMI.Sdk.Buffer;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using Oatc.OpenMI.Sdk.Wrapper;
using OOC.Util;


namespace OOC.OpenMIWrapper
{
    public delegate void AfterSimulationDelegate(object sender, bool succeed);
    public delegate void CompositionModelProgressChangedDelegate(object sender, string modelId, string progress);

    /// <summary>
    /// Summary description for CompositionManager.
    /// 
    /// TODO: it should be called Composition
    /// </summary>
    public class CompositionManager
    {
        public AfterSimulationDelegate AfterSimulationHandler;
        public CompositionModelProgressChangedDelegate CompositionModelProgressChangedHandler;
        public bool Parallelized { get; set; }
        public ParallelizeMode ParallelizeMode = ParallelizeMode.Aggressive;
        private DateTime startTime, endTime;

        #region Static members

        private static Event _simulationFinishedEvent;
        private static Event _simulationFailedEvent;

        static CompositionManager()
        {
            _simulationFinishedEvent = new Event(EventType.GlobalProgress);
            _simulationFinishedEvent.Description = "Simulation finished successfuly...";

            _simulationFailedEvent = new Event(EventType.GlobalProgress);
            _simulationFailedEvent.Description = "Simulation FAILED...";
        }


        /// <summary>
        /// Special event saying that simulation has finished.
        /// </summary>
        public static IEvent SimulationFinishedEvent
        {
            get { return (_simulationFinishedEvent); }
        }

        /// <summary>
        /// Special event saying that simulation has failed.
        /// </summary>
        public static IEvent SimulationFailedEvent
        {
            get { return (_simulationFailedEvent); }
        }


        /// <summary>
        /// Unique ID of trigger "model".
        /// </summary>
        /// <remarks>Standard models cannot have this ID.</remarks>
        public const string TriggerModelID = "Oatc.OpenMI.Gui.Trigger";

        #endregion

        #region Internal members

        Thread _runThread;
        bool _running;
        bool _runPrepareForComputationStarted;
        IListener _runListener;
        bool _runInSameThread;

        IList<Model> _models;
        ArrayList _connections;
        bool[] _listenedEventTypes;
        DateTime _triggerInvokeTime;
        Dictionary<ILinkableComponent, string> cmGuidMapping;

        #endregion

        /// <summary>
        /// Creates a new empty instance of <c>CompositionManager</c> class.
        /// </summary>
        /// <remarks>See <see cref="Initialize">Initialize</see> for more detail.</remarks>
        public CompositionManager()
        {
            Initialize();
        }


        #region Public properties

        /// <summary>
        /// Gets list of all models (ie. instances of <see cref="Model">UIModel</see> class) in composition.
        /// </summary>
        public IList<Model> Models
        {
            get { return _models; }
            set { _models = value; }
        }


        /// <summary>
        /// Gets list of all connections (ie. instances of <see cref="Connection">UIConnection</see> class) in composition.
        /// </summary>
        public ArrayList Connections
        {
            get { return (_connections); }
        }


        /// <summary>
        /// Gets array of <c>bool</c> describing which events should be listened during simulation run.
        /// </summary>
        /// <remarks>Array has <see cref="EventType.NUM_OF_EVENT_TYPES">EventType.NUM_OF_EVENT_TYPES</see>
        /// elements. See <see cref="EventType">EventType</see>, <see cref="Run">Run</see> for more detail.
        /// </remarks>
        public bool[] ListenedEventTypes
        {
            get { return (_listenedEventTypes); }
            /*	set
                {
                    Debug.Assert( value.Length == (int)EventType.NUM_OF_EVENT_TYPES );
                    _listenedEventTypes = value;
                    _shouldBeSaved = true;
                }*/
        }


        /// <summary>
        /// Time when trigger should be invoked.
        /// </summary>
        /// <remarks>See <see cref="EventType">EventType</see> and <see cref="Run">Run</see> for more detail.</remarks>
        public DateTime TriggerInvokeTime
        {
            get { return (_triggerInvokeTime); }
            set
            {
                if (_triggerInvokeTime != value)
                {
                    _triggerInvokeTime = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets whether simulation should be run in same thread. By default it's <c>false</c>.
        /// </summary>
        /// <remarks>
        /// This is only recommendation of composition author, you can override
        /// this setting while calling <see cref="Run">Run</see> method. For example
        /// if running from console, simulation is always executed in same thread.
        /// </remarks>
        public bool RunInSameThread
        {
            get { return (_runInSameThread); }
            set
            {
                if (_runInSameThread != value)
                {
                    _runInSameThread = value;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Initializes this composition.
        /// </summary>
        public void Initialize()
        {
            _models = new List<Model>();
            _connections = new ArrayList();
            _listenedEventTypes = new bool[(int)EventType.NUM_OF_EVENT_TYPES];
            for (int i = 0; i < (int)EventType.NUM_OF_EVENT_TYPES; i++)
                _listenedEventTypes[i] = false;

            _triggerInvokeTime = new DateTime(1900, 1, 1);
            _runPrepareForComputationStarted = false;
            _runThread = null;
            _running = false;
            _runListener = null;
            _runInSameThread = false;
            cmGuidMapping = new Dictionary<ILinkableComponent, string>();

            OnInitialized();
        }

        /// <summary>
        /// Executed after composition was initialized, allows to extend functionality in derived classes.
        /// </summary>
        public virtual void OnInitialized()
        {
        }

        /// <summary>
        /// Releases all models and intializes this composition.
        /// </summary>
        public void Release()
        {
            RemoveAllModels();
            Initialize();
        }

        public Model GetModel(string modelId)
        {
            foreach (Model model in _models)
            {
                if (model.ModelID == modelId)
                {
                    return model;
                }
            }
            return null;
        }

        public void AddModel(Model model)
        {
            cmGuidMapping[model.LinkableComponent] = model.ModelID;
            _models.Add(model);
        }

        /// <summary>
        /// Removes specified model from composition.
        /// </summary>
        /// <param name="model">Model to be removed.</param>
        /// <remarks>The <c>Dispose</c> method is called on the model.</remarks>
        public void RemoveModel(Model model)
        {
            // first remove all links from/to this model
            Connection[] copyOfLinks = (Connection[])_connections.ToArray(typeof(Connection));
            foreach (Connection uiLink in copyOfLinks)
                if (uiLink.AcceptingModel == model || uiLink.ProvidingModel == model)
                    RemoveConnection(uiLink);

            try
            {
                // We call Finish() after computation finished,
                // Dispose() when removing models
                model.LinkableComponent.Dispose();
            }
            catch
            {
                // we don't care about just disposed model, so do nothing...
            }

            _models.Remove(model); // remove model itself
        }


        /// <summary>
        /// Removes all model from composition.
        /// </summary>
        /// <remarks>See <see cref="RemoveModel">RemoveModel</see> for more detail.</remarks>
        public void RemoveAllModels()
        {
            while (Models.Count > 0)
            {
                RemoveModel(Models[0]);
            }
        }

        /// <summary>
        /// Creates new connection between two models in composition.
        /// </summary>
        /// <param name="providingModel">Source model</param>
        /// <param name="acceptingModel">Target model</param>
        /// <remarks>Connection between two models is just abstraction which can hold links between models.
        /// The direction of connection and its links is same. There can be only one connection between two models.</remarks>
        public Connection GetConnection(string providingModelId, string acceptingModelId)
        {
            if (providingModelId == acceptingModelId)
                throw (new Exception("Cannot connect model with itself."));

            Model providingModel = null;
            Model acceptingModel = null;

            // Check whether both models exist
            bool providingFound = false, acceptingFound = false;
            foreach (Model model in _models)
            {
                if (model.ModelID == providingModelId)
                {
                    providingModel = model;
                    providingFound = true;
                }
                if (model.ModelID == acceptingModelId)
                {
                    acceptingModel = model;
                    acceptingFound = true;
                }
            }
            if (!providingFound || !acceptingFound)
                throw (new Exception("Cannot find providing or accepting."));

            // check whether this link isn't already here (if yes, do nothing)
            foreach (Connection link in _connections)
                if (link.ProvidingModel == providingModel && link.AcceptingModel == acceptingModel)
                    return link;

            Connection connection = new Connection(providingModel, acceptingModel);
            _connections.Add(connection);
            return connection;
        }

        public void AddLink(string linkId, string sourceModelId, string targetModelId, string sourceQuantity, string targetQuantity, string sourceElementSet, string targetElementSet)
        {
            var dataOperationsToAdd = new ArrayList();
            Connection connection = GetConnection(sourceModelId, targetModelId);
            Model sourceModel = GetModel(sourceModelId);
            Model targetModel = GetModel(targetModelId);
            var outputExchangeItem = sourceModel.GetOutputExchangeItem(sourceElementSet, sourceQuantity);
            var inputExchangeItem = targetModel.GetInputExchangeItem(targetElementSet, targetQuantity);
            if (outputExchangeItem == null || inputExchangeItem == null)
                throw (new Exception(
                    "Cannot find exchange item"));
            Link link = new Link(
                sourceModel.LinkableComponent,
                outputExchangeItem.ElementSet,
                outputExchangeItem.Quantity,
                targetModel.LinkableComponent,
                inputExchangeItem.ElementSet,
                inputExchangeItem.Quantity,
                "No description available.",
                linkId,
                dataOperationsToAdd);
            connection.Links.Add(link);
            sourceModel.LinkableComponent.AddLink(link);
            targetModel.LinkableComponent.AddLink(link);
        }
        /// <summary>
        /// Removes connection between two models.
        /// </summary>
        /// <param name="connection">Connection to be removed.</param>
        public void RemoveConnection(Connection connection)
        {
            // remove ILinks from both connected components
            if (!_runPrepareForComputationStarted)
                foreach (ILink link in connection.Links)
                {
                    string linkID = link.ID;
                    ILinkableComponent
                        sourceComponent = link.SourceComponent,
                        targetComponent = link.TargetComponent;


                    sourceComponent.RemoveLink(linkID);
                    targetComponent.RemoveLink(linkID);
                }

            _connections.Remove(connection);
        }

        /// <summary>
        /// Calculates time horizon of the simulation,
        /// ie. time between earliest model start and latest model end.
        /// </summary>
        /// <returns>Returns simulation time horizon.</returns>
        public ITimeSpan GetSimulationTimehorizon()
        {
            TimeStamp start = new TimeStamp(double.MaxValue),
                end = new TimeStamp(double.MinValue);

            foreach (Model model in _models)
            {
                if (model.ModelID == CompositionManager.TriggerModelID)
                    continue;

                if (model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay == 0.0 || model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay > 100000)
                {
                    continue; // wrong time horizon is used by model
                }

                start.ModifiedJulianDay = Math.Min(start.ModifiedJulianDay, model.LinkableComponent.TimeHorizon.Start.ModifiedJulianDay);
                end.ModifiedJulianDay = Math.Max(end.ModifiedJulianDay, model.LinkableComponent.TimeHorizon.End.ModifiedJulianDay);
            }

            Debug.Assert(start.ModifiedJulianDay < end.ModifiedJulianDay);

            return (new Oatc.OpenMI.Sdk.Backbone.TimeSpan(start, end));
        }


        /// <summary>
        /// Runs simulation.
        /// </summary>
        /// <param name="runListener">Simulation listener.</param>
        /// <param name="runInSameThread">If <c>true</c>, simulation is run in same thread like caller,
        /// ie. method blocks until simulation don't finish. If <c>false</c>, simulation is
        /// run in separate thread and method returns immediately.</param>
        /// <remarks>
        /// Simulation is run the way that trigger invokes <see cref="ILinkableComponent.GetValues">ILinkableComponent.GetValues</see>
        /// method of the model it's connected to
        /// at the time specified by <see cref="TriggerInvokeTime">TriggerInvokeTime</see> property.
        /// If you need to use more than one listener you can use <see cref="ProxyListener">ProxyListener</see>
        /// class or <see cref="ProxyMultiThreadListener">ProxyMultiThreadListener</see> if <c>runInSameThread</c> is <c>false</c>.
        /// </remarks>
        public void Run(Logger logger, bool runInSameThread)
        {
            LoggerListener loggerListener = new LoggerListener(logger);
            ProgressListener progressListener = new ProgressListener();
            progressListener.ModelProgressChangedHandler += new ModelProgressChangedDelegate(delegate(ILinkableComponent linkableComponent, ITimeStamp simTime)
            {
                string guid = cmGuidMapping[linkableComponent];
                string progress = CalendarConverter.ModifiedJulian2Gregorian(simTime.ModifiedJulianDay).ToString();
                CompositionModelProgressChangedHandler(this, guid, progress);
            });

            ArrayList listeners = new ArrayList();
            listeners.Add(loggerListener);
            listeners.Add(progressListener);
            ProxyListener proxyListener = new ProxyListener();
            proxyListener.Initialize(listeners);

            startTime = DateTime.Now;

            if (_running)
                throw (new Exception("Simulation is already running."));

            _running = true;
            _runListener = proxyListener;

            try
            {
                TimeStamp runToTime = new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(_triggerInvokeTime));

                // Create informative message
                if (_runListener != null)
                {
                    StringBuilder description = new StringBuilder();
                    description.Append("Starting simulation at ");
                    description.Append(DateTime.Now.ToString());
                    description.Append(",");

                    description.Append(" composition consists from following models:\n");
                    foreach (Model model in _models)
                    {
                        description.Append(model.ModelID);
                        description.Append(", ");
                    }

                    // todo: add more info?

                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = description.ToString();
                    _runListener.OnEvent(theEvent);
                }

                _runPrepareForComputationStarted = true;

                // prepare for computation
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Preparing for computation....";
                    _runListener.OnEvent(theEvent);
                }

                // subscribing event listener to all models
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Subscribing proxy event listener....";
                    _runListener.OnEvent(theEvent);

                    for (int i = 0; i < _runListener.GetAcceptedEventTypeCount(); i++)
                        foreach (Model uimodel in _models)
                        {
                            theEvent = new Event(EventType.Informative);
                            theEvent.Description = "Calling Subscribe() method with EventType." + ((EventType)i).ToString() + " of model " + uimodel.ModelID;
                            _runListener.OnEvent(theEvent);

                            for (int j = 0; j < uimodel.LinkableComponent.GetPublishedEventTypeCount(); j++)
                                if (uimodel.LinkableComponent.GetPublishedEventType(j) == _runListener.GetAcceptedEventType(i))
                                {
                                    uimodel.LinkableComponent.Subscribe(_runListener, _runListener.GetAcceptedEventType(i));
                                    break;
                                }
                        }
                }

                if (!runInSameThread)
                {
                    // creating run thread
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Creating run thread....";
                        _runListener.OnEvent(theEvent);
                    }

                    _runThread = new Thread(RunThreadFunction) { Priority = ThreadPriority.Normal };

                    // starting thread...
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.GlobalProgress);
                        theEvent.Description = "Starting run thread....";
                        _runListener.OnEvent(theEvent);
                    }

                    _runThread.Start();
                }
                else
                {
                    // run simulation in same thread (for example when running from console)
                    if (_runListener != null)
                    {
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Running simulation in same thread....";
                        _runListener.OnEvent(theEvent);
                    }
                    RunThreadFunction();
                }
            }
            catch (System.Exception e)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Exception occured while initiating simulation run: " + e.ToString();
                    _runListener.OnEvent(theEvent);
                    _runListener.OnEvent(SimulationFailedEvent); // todo: add info about time to this event

                    endTime = DateTime.Now;
                    var ev = new Event
                    {
                        Description = "Elapsed time: " + (endTime - startTime),
                        Type = EventType.GlobalProgress
                    };

                    _runListener.OnEvent(ev);
                }
            }
            finally
            {
            }

        }


        /// <summary>
        /// Stops the simulation.
        /// </summary>
        /// <remarks>This method has effect only if simulation is run in separate thread
        /// (see <see cref="Run">Run</see> method).
        /// This method calls <see cref="Thread.Abort()">Abort</see> method on the simulation thread.</remarks>
        public void Stop()
        {
            if (_running && _runThread != null)
                _runThread.Abort();
            _runThread = null;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// This method is called in <see cref="Run">Run</see> method.
        /// </summary>
        private void RunThreadFunction()
        {
            bool succeed = false;
            foreach (Model uimodel in _models)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Calling Prepare() method of model " + uimodel.ModelID;
                    _runListener.OnEvent(theEvent);
                }
                uimodel.LinkableComponent.Prepare();
            }

            //Trigger trigger = GetTrigger();

            //Debug.Assert(trigger != null);

            //WHAT THE HELL IS THIS FOR?
            Thread.Sleep(0);

            try
            {
                // run it !!!
                // trigger.Run(new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(TriggerInvokeTime)));
                ITime triggerTime = new TimeStamp(CalendarConverter.Gregorian2ModifiedJulian(TriggerInvokeTime));

                CompositionRunner runner = new CompositionRunner(_models, _runListener);
                if (Parallelized) runner.RunParallel(triggerTime, ParallelizeMode);
                else runner.RunSequence(triggerTime);

                // close models down
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Closing models down...";
                    _runListener.OnEvent(theEvent);
                }

                foreach (Model uimodel in _models)
                {
                    if (_runListener != null)
                    {
                        string ModelID = uimodel.ModelID;
                        Event theEvent = new Event(EventType.Informative);
                        theEvent.Description = "Calling Finish() on model " + ModelID;
                        _runListener.OnEvent(theEvent);
                    }
                    uimodel.LinkableComponent.Finish();
                }

                // thread finishes - send well known event
                if (_runListener != null)
                {
                    _simulationFinishedEvent.Description = "Simulation finished successfuly at " + DateTime.Now.ToString() + "...";
                    _runListener.OnEvent(SimulationFinishedEvent);
                }
                succeed = true;
            }
            catch (Exception e)
            {
                if (_runListener != null)
                {
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Exception occured during simulation: " + e.ToString();
                    _runListener.OnEvent(theEvent);

                    _simulationFailedEvent.Description = "Simulation FAILED at " + DateTime.Now.ToString() + "...";
                    _runListener.OnEvent(SimulationFailedEvent);
                }
            }
            finally
            {
                endTime = DateTime.Now;
                var ev = new Event
                {
                    Description = "Elapsed time: " + (endTime - startTime),
                    Type = EventType.GlobalProgress
                };

                _runListener.OnEvent(ev);

                _running = false;
                _runListener = null; // release listener

            }
            if (AfterSimulationHandler != null)
            {
                AfterSimulationHandler(this, succeed);
            }
        }


        #endregion
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
