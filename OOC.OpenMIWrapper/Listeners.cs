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
using System.Runtime.InteropServices;
using System.Collections;
using System.Threading;
using System.Text;
using System.Diagnostics;
using System.IO;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using Timer = System.Threading.Timer;
using OOC.Util;

namespace OOC.OpenMIWrapper
{
    public delegate void ModelProgressChangedDelegate(ILinkableComponent linkableComponent, ITimeStamp simTime);

    public class LoggerListener : IListener
    {
        EventType[] _acceptedEventTypes;
        Logger _logger;

        public LoggerListener(Logger logger)
        {
            Initialize(logger);
        }

        public void Initialize(Logger logger)
        {
            ArrayList acceptedEventTypes = new ArrayList();
            for (int i = 0; i < 6; i++)
                acceptedEventTypes.Add((EventType)i);
            _acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray(typeof(EventType));
            _logger = logger;
        }


        public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
        {
            return (_acceptedEventTypes[acceptedEventTypeIndex]);
        }


        public int GetAcceptedEventTypeCount()
        {
            return (_acceptedEventTypes.Length);
        }

        public void OnEvent(IEvent e)
        {
            if (e == null)
            {
                return;
            }

            if (_logger != null)
            {
                lock (this)
                {
                    _logger.Info(Utils.EventToString(e));
                }
            }
        }
    }

    public class ProgressListener : IListener
    {
        public ModelProgressChangedDelegate ModelProgressChangedHandler;

        EventType[] _acceptedEventTypes;

        public ProgressListener()
        {
            ArrayList acceptedEventTypes = new ArrayList();
            acceptedEventTypes.Add(EventType.DataChanged);
            _acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray(typeof(EventType));
        }

        public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
        {
            return (_acceptedEventTypes[acceptedEventTypeIndex]);
        }

        public int GetAcceptedEventTypeCount()
        {
            return (_acceptedEventTypes.Length);
        }

        public void OnEvent(IEvent e)
        {
            if (e == null)
            {
                return;
            }
            if (e.Type != EventType.DataChanged)
            {
                return;
            }
            if (ModelProgressChangedHandler != null)
            {
                ModelProgressChangedHandler(e.Sender, e.SimulationTime);
            }
        }
    }
    public class ProxyListener : IListener
    {
        InternalListenerRecord[] _internalListeners;
        EventType[] _acceptedEventTypes;

        private struct InternalListenerRecord
        {
            public bool[] listenedEventTypes;
            public IListener listener;
        }

        public void Initialize(ArrayList listeners)
        {
            _internalListeners = new InternalListenerRecord[listeners.Count];

            bool[] listenedEventTypes = new bool[(int)EventType.NUM_OF_EVENT_TYPES];
            for (int i = 0; i < listenedEventTypes.Length; i++)
                listenedEventTypes[i] = false;

            // create internal table of listeners and set their listened events
            for (int i = 0; i < listeners.Count; i++)
            {
                IListener listener = (IListener)listeners[i];
                _internalListeners[i].listener = listener;
                _internalListeners[i].listenedEventTypes = new bool[(int)EventType.NUM_OF_EVENT_TYPES];

                for (int j = 0; j < (int)EventType.NUM_OF_EVENT_TYPES; j++)
                    _internalListeners[i].listenedEventTypes[j] = false;

                for (int j = 0; j < listener.GetAcceptedEventTypeCount(); j++)
                {
                    _internalListeners[i].listenedEventTypes[(int)listener.GetAcceptedEventType(j)] = true;
                    listenedEventTypes[(int)listener.GetAcceptedEventType(j)] = true;
                }
            }

            // set this listener's accepted event types
            ArrayList acceptedEventTypes = new ArrayList();
            for (int i = 0; i < listenedEventTypes.Length; i++)
                if (listenedEventTypes[i])
                    acceptedEventTypes.Add((EventType)i);
            _acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray(typeof(EventType));

        }

        public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
        {
            return (_acceptedEventTypes[acceptedEventTypeIndex]);
        }

        public int GetAcceptedEventTypeCount()
        {
            return (_acceptedEventTypes.Length);
        }

        public void OnEvent(IEvent e)
        {
            foreach (InternalListenerRecord record in _internalListeners)
                if (record.listenedEventTypes[(int)e.Type]
                    || e == CompositionManager.SimulationFinishedEvent
                    || e == CompositionManager.SimulationFailedEvent)
                    record.listener.OnEvent(e);
        }

    }

}
