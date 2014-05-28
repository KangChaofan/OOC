using System;
using System.Collections.Generic;
using System.Collections;
using System.Threading;
using Oatc.OpenMI.Sdk.Buffer;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using Oatc.OpenMI.Sdk.Wrapper;

namespace OOC.OpenMIWrapper
{
    public class CompositionRunner
    {
        private IList<Model> _models;
        private IListener _runListener;

        private List<Model> _runningModels;
        private List<Model> _queueModels;
        private Semaphore _s;

        public CompositionRunner(IList<Model> models, IListener runListener)
        {
            _models = new List<Model>(models);
            _runListener = runListener;
        }

        private bool isModelEngineComponent(ILinkableComponent component)
        {
            return typeof(LinkableRunEngine).IsAssignableFrom(component.GetType());
        }

        private List<Model> getZeroDegreeNodes(List<Model> uimodels)
        {
            List<Model> tier = new List<Model>();
            foreach (Model uimodel in uimodels)
            {
                int inDegree = 0;
                LinkableRunEngine runEngine = (LinkableRunEngine)uimodel.LinkableComponent;
                foreach (ILink link in runEngine.GetAcceptingLinks())
                {
                    if (!isModelEngineComponent(link.SourceComponent)) continue;
                    foreach (Model m in uimodels)
                        if (m.LinkableComponent == link.SourceComponent)
                            inDegree++;
                }
                if (inDegree == 0)
                {
                    tier.Add(uimodel);
                }
            }
            return tier;
        }

        private List<List<Model>> getTopologicalOrder(IList<Model> models)
        {
            List<Model> uimodels = new List<Model>();
            List<List<Model>> orders = new List<List<Model>>();
            foreach (Model uimodel in models)
                if (isModelEngineComponent(uimodel.LinkableComponent))
                    uimodels.Add(uimodel);
            while (uimodels.Count > 0)
            {
                List<Model> tier = getZeroDegreeNodes(uimodels);
                if (tier.Count > 0)
                {
                    orders.Add(tier);
                    foreach (Model uimodel in tier) uimodels.Remove(uimodel);
                }
                else
                {
                    foreach (Model uimodel in uimodels)
                        orders.Add(new List<Model>(new Model[] { uimodel }));
                    uimodels.Clear();
                }
            }
            return orders;
        }

        private void runStandardParallel(ITime triggerTime)
        {
            List<Thread> threads = new List<Thread>();
            List<Model> uimodels = new List<Model>(_models);
            List<List<Model>> orders;
            while ((orders = getTopologicalOrder(uimodels)).Count > 0)
            {
                List<Model> tier = orders[0];
                string modelIds = "";
                foreach (Model uimodel in tier) modelIds += uimodel.ModelID + ", ";
                Event theEvent = new Event(EventType.Informative);
                theEvent.Description = "Tier models: " + modelIds;
                _runListener.OnEvent(theEvent);
                foreach (Model uimodel in tier)
                {
                    if (!isModelEngineComponent(uimodel.LinkableComponent)) continue;
                    LinkableRunEngine runEngine = (LinkableRunEngine)uimodel.LinkableComponent;
                    Thread th = new Thread(new ThreadStart(delegate()
                    {
                        runEngine.RunToTime(triggerTime, -1);
                    }));
                    th.Start();
                    threads.Add(th);
                    uimodels.Remove(uimodel);
                }
                foreach (Thread th in threads)
                {
                    th.Join();
                }
                threads.Clear();
            }
        }

        private void scheduleAggressive(ITime triggerTime)
        {
            List<List<Model>> orders;
            lock (this)
            {
                orders = getTopologicalOrder(_queueModels);
            }
            if (orders.Count > 0)
            {
                List<Model> tier = orders[0];
                foreach (Model model in tier)
                {
                    if (!isModelEngineComponent(model.LinkableComponent)) continue;
                    lock (this)
                    {
                        if (_runningModels.Contains(model)) continue;
                        _runningModels.Add(model);
                    }
                    Event theEvent = new Event(EventType.Informative);
                    theEvent.Description = "Triggering model: " + model.ModelID;
                    _runListener.OnEvent(theEvent);
                    LinkableRunEngine runEngine = (LinkableRunEngine)model.LinkableComponent;
                    new Thread(new ThreadStart(delegate()
                    {
                        runEngine.RunToTime(triggerTime, -1);
                        lock (this)
                        {
                            _runningModels.Remove(model);
                            _queueModels.Remove(model);
                        }
                        _s.Release();
                    })).Start();
                }
            }
        }

        private void runAggressiveParallel(ITime triggerTime)
        {
            _s = new Semaphore(0, _models.Count);
            _queueModels = new List<Model>(_models);
            _runningModels = new List<Model>();
            while (_queueModels.Count > 0 && _s.WaitOne())
            {
                scheduleAggressive(triggerTime);
            }
        }

        public void RunParallel(ITime triggerTime, ParallelizeMode mode)
        {
            Event theEvent = new Event(EventType.Informative);
            theEvent.Description = "Parallel execution feature enabled, with " + mode + " mode.";
            _runListener.OnEvent(theEvent);
            switch (mode)
            {
                case ParallelizeMode.Standard:
                    runStandardParallel(triggerTime);
                    break;
                case ParallelizeMode.Aggressive:
                    runAggressiveParallel(triggerTime);
                    break;
            }
        }

        public void RunSequence(ITime triggerTime)
        {
            Event theEvent = new Event(EventType.Informative);
            theEvent.Description = "Parallel execution feature disabled.";
            _runListener.OnEvent(theEvent);
            /* try find edge model and trigger them first, is possible */
            int edgeModel = 0;
            foreach (Model uimodel in _models)
            {
                if (!isModelEngineComponent(uimodel.LinkableComponent)) continue;
                int outDegree = 0;
                LinkableRunEngine runEngine = (LinkableRunEngine)uimodel.LinkableComponent;
                foreach (ILink link in runEngine.GetProvidingLinks())
                {
                    if (isModelEngineComponent(link.TargetComponent))
                        outDegree++;
                }
                theEvent = new Event(EventType.Informative);
                theEvent.Description = "Out degree of " + uimodel.ModelID + " is " + outDegree + ".";
                _runListener.OnEvent(theEvent);
                if (outDegree == 0)
                {
                    runEngine.RunToTime(triggerTime, -1);
                    edgeModel++;
                }
            }
            /* no edge model was found, perhaps the composition is combined with circles */
            if (edgeModel == 0)
            {
                theEvent = new Event(EventType.Informative);
                theEvent.Description = "No edge model found, try invoke all of them.";
                _runListener.OnEvent(theEvent);
                foreach (Model uimodel in _models)
                {
                    if (!isModelEngineComponent(uimodel.LinkableComponent)) continue;
                    LinkableRunEngine runEngine = (LinkableRunEngine)uimodel.LinkableComponent;
                    runEngine.RunToTime(triggerTime, -1);
                }
            }
        }

    }
}
