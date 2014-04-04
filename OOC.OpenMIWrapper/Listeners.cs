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

namespace OOC.OpenMIWrapper
{
	/// <summary>
	/// Listener used to log simulation progress to text file.
	/// </summary>
	public class LogFileListener: IListener
	{			
		EventType[] _acceptedEventTypes;
		StreamWriter _writer;

		/// <summary>
		/// Creates new instance of <see cref="LogFileListener">LogFileListener</see> which doesn't listen any
		/// event type. 
		/// </summary>
		public LogFileListener()
		{
			_acceptedEventTypes = new EventType[0];
		}


		/// <summary>
		/// Creates new instance of <see cref="LogFileListener">LogFileListener</see>.
		/// </summary>
		/// <param name="listenedEvents">Listened event types.</param>
		/// <param name="filename">Path to text file for logging.</param>
		/// <remarks>See <see cref="Initialize">Initialize</see> for more detail.</remarks>
		public LogFileListener( bool[] listenedEvents, string filename )
		{
			Initialize( listenedEvents, filename );
		}


		/// <summary>
		/// Closes text file for logging, if any.
		/// </summary>
		~LogFileListener()
		{
			if( _writer!=null )
			{
                if(_writer.BaseStream.CanWrite)
                {
                    _writer.Close();
                }
                _writer = null;
			}
		}


		/// <summary>
		/// Initializes this listener to log events to text file.
		/// </summary>
		/// <param name="listenedEvents"><c>bool</c> array describing which event types should be listened.</param>
		/// <param name="filename">Path to text file for logging.</param>
		/// <remarks><c>listenedEvents</c> must have exactly
		/// <see cref="EventType.NUM_OF_EVENT_TYPES">EventType.NUM_OF_EVENT_TYPES</see> elements.
		/// See <see cref="EventType">EventType</see> for more detail.</remarks>
		public void Initialize( bool[] listenedEvents, string filename )
		{
			if( listenedEvents.Length != (int)EventType.NUM_OF_EVENT_TYPES )
				throw( new ArgumentException("Length of this array must be same as EventType.NUM_OF_EVENT_TYPES", "listenedEvents") );
			
			ArrayList acceptedEventTypes = new ArrayList();
			for( int i=0; i<listenedEvents.Length; i++ )
				if( listenedEvents[i] )
					acceptedEventTypes.Add( (EventType)i );
			_acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray( typeof(EventType) );

			// open writer only if there is something to listen			
			if( _acceptedEventTypes.Length > 0 )
			{
				_writer = new StreamWriter( filename, false, Encoding.Unicode );
				_writer.AutoFlush = true;
			}
			else
			{
				_writer = null;
			}
		}
		

		/// <summary>
		/// Get accepted event type.
		/// </summary>
		/// <param name="acceptedEventTypeIndex">Index of accepted event type.</param>
		/// <returns>Returns accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventType">IListener.GetAcceptedEventType</see>
		/// for more detail.</remarks>
		public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
		{
			return( _acceptedEventTypes[acceptedEventTypeIndex] );
		}


		/// <summary>
		/// Get accepted event type count.
		/// </summary>
		/// <returns>Returns number of accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventTypeCount">IListener.GetAcceptedEventTypeCount</see>
		/// for more detail.</remarks>
		public int GetAcceptedEventTypeCount()
		{
			return( _acceptedEventTypes.Length );
		}


		/// <summary>
		/// Logs one event to text file.
		/// </summary>
		/// <param name="e">Event to be logged.</param>
		/// <remarks>See <see cref="IListener.OnEvent">IListener.OnEvent</see>
		/// for more detail.</remarks>
		public void OnEvent(IEvent e)
		{
            if(e == null)
            {
                return;
            }

			if( _writer!=null )
			{
				_writer.WriteLine( Utils.EventToString(e) );
			
				if( e==CompositionManager.SimulationFinishedEvent
					|| e==CompositionManager.SimulationFailedEvent )
				{
					_writer.Close();
					_writer = null;
				}
			}
		}		
	}


	/// <summary>
	/// Listener used to write events to console.
	/// </summary>
	public class ConsoleListener: IListener
	{
		EventType[] _acceptedEventTypes;

		/// <summary>
		/// Creates new instance of <see cref="ConsoleListener">ConsoleListener</see> which doesn't listen any
		/// event type. 
		/// </summary>
		public ConsoleListener()
		{
			_acceptedEventTypes = new EventType[0];
		}

		/// <summary>
		/// Creates new instance of <see cref="ConsoleListener">ConsoleListener</see>.
		/// </summary>
		/// <param name="listenedEvents">Listened event types.</param>
		/// <remarks>See <see cref="Initialize">Initialize</see> for more detail.</remarks>		
		public ConsoleListener( bool[] listenedEvents )
		{
			Initialize( listenedEvents );
		}

		/// <summary>
		/// Initializes this listener to write events to console.
		/// </summary>
		/// <param name="listenedEvents"><c>bool</c> array describing which event types should be listened.</param>
		/// <remarks><c>listenedEvents</c> must have exactly
		/// <see cref="EventType.NUM_OF_EVENT_TYPES">EventType.NUM_OF_EVENT_TYPES</see> elements.
		/// See <see cref="EventType">EventType</see> for more detail.</remarks>
		public void Initialize( bool[] listenedEvents )
		{
			if( listenedEvents.Length != (int)EventType.NUM_OF_EVENT_TYPES )
				throw( new ArgumentException("Length of this array must be same as EventType.NUM_OF_EVENT_TYPES", "listenedEvents") );
						
			ArrayList acceptedEventTypes = new ArrayList();
			for( int i=0; i<listenedEvents.Length; i++ )
				if( listenedEvents[i] )
					acceptedEventTypes.Add( (EventType)i );
			_acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray( typeof(EventType) );			
		}


		/// <summary>
		/// Get accepted event type.
		/// </summary>
		/// <param name="acceptedEventTypeIndex">Index of accepted event type.</param>
		/// <returns>Returns accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventType">IListener.GetAcceptedEventType</see>
		/// for more detail.</remarks>		
		public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
		{
			return( _acceptedEventTypes[acceptedEventTypeIndex] );
		}

		/// <summary>
		/// Get accepted event type count.
		/// </summary>
		/// <returns>Returns number of accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventTypeCount">IListener.GetAcceptedEventTypeCount</see>
		/// for more detail.</remarks>
		public int GetAcceptedEventTypeCount()
		{
			return( _acceptedEventTypes.Length );
		}

		/// <summary>
		/// Writes one event to console.
		/// </summary>
		/// <param name="e">Event to write.</param>
		/// <remarks>See <see cref="IListener.OnEvent">IListener.OnEvent</see>
		/// for more detail.</remarks>
		public void OnEvent(IEvent e)
		{
			Console.WriteLine( Utils.EventToString(e) );
		}
		
	}

	/// <summary>
	/// Listener used to call <see cref="Application.DoEvents">Application.DoEvents</see> method
	/// periodically.
	/// </summary>
	/// <remarks>This listener is typically used when simulation runs in same thread as GUI to be able
	/// to redraw window, etc...</remarks>
	public class DoEventsListener: IListener
	{
		DateTime _lastDoEventsTime;
		long _minDoEventsIntervalTicks;

		/// <summary>
		/// Creates new instance of <see cref="DoEventsListener">DoEventsListener</see> class.
		/// </summary>
		/// <param name="minDoEventsInterval">Minimum interval between <see cref="Application.DoEvents">Application.DoEvents</see> call in miliseconds.</param>
		/// <remarks>
		/// See <see cref="Initialize">Initialize</see> for more details.
		/// </remarks>
		public DoEventsListener( uint minDoEventsInterval )
		{
			Initialize( minDoEventsInterval );
		}

		/// <summary>
		/// Initializes this <see cref="DoEventsListener">DoEventsListener</see>.
		/// </summary>
		/// <param name="minDoEventsInterval">Minimum interval between <see cref="Application.DoEvents">Application.DoEvents</see> call in miliseconds.</param>
		public void Initialize( uint minDoEventsInterval  )
		{
			_lastDoEventsTime = DateTime.MinValue;
			_minDoEventsIntervalTicks = System.TimeSpan.TicksPerSecond * minDoEventsInterval / 1000;
		}

		/// <summary>
		/// Get accepted event type.
		/// </summary>
		/// <param name="acceptedEventTypeIndex">Index of accepted event type.</param>
		/// <returns>Returns accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventType">IListener.GetAcceptedEventType</see>
		/// for more detail.</remarks>		
		public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
		{
			return( (EventType)acceptedEventTypeIndex );			
		}

		/// <summary>
		/// Get accepted event type count.
		/// </summary>
		/// <returns>Returns number of accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventTypeCount">IListener.GetAcceptedEventTypeCount</see>
		/// for more detail.</remarks>
		public int GetAcceptedEventTypeCount()
		{
			return( (int)EventType.NUM_OF_EVENT_TYPES );	
		}

		/// <summary>
		/// Makes <c>Application.DoEvents</c> call if time elapsed since the last call exceeds minimum interval.
		/// </summary>
		/// <param name="e">Event, isn't used anyway.</param>
		/// <remarks>See <see cref="Initialize">Initialize</see>, <see cref="IListener.OnEvent">IListener.OnEvent</see>
		/// for more detail.</remarks>		
		public void OnEvent(IEvent e)
		{
			DateTime actTime = DateTime.Now;

			if( (actTime-_lastDoEventsTime).Ticks >= _minDoEventsIntervalTicks )
			{				
				_lastDoEventsTime = actTime;
			}			
		}
		
	}


	/// <summary>
	/// Simulation listener used to forward events to other listeners. 
	/// </summary>
	/// <remarks>
	/// <see cref="CompositionManager.Run">CompositionManager.Run</see> allows
	/// only one listener to monitor the simulation, if you need more than one listener,
	/// you can use this class. This class should be used only if simulation runs in same thread as UI,
	/// in other case use <see cref="ProxyMultiThreadListener">ProxyMultiThreadListener</see>.	
	/// </remarks>	
	public class ProxyListener: IListener
	{		
		InternalListenerRecord[] _internalListeners;
		EventType[] _acceptedEventTypes;				
		
		private struct InternalListenerRecord 
		{			
			public bool[] listenedEventTypes;
			public IListener listener;
		}

		/// <summary>
		/// Initializes this <see cref="ProxyListener">ProxyListener</see> to send events to specific listeners.
		/// </summary>
		/// <param name="listeners">Listeners to recieve events.</param>
		/// <remarks>
		/// All registered listeners may not change content of the event
		/// in theit implementation of <see cref="IListener.OnEvent">IListener.OnEvent</see> method.
		/// See <see cref="OnEvent">OnEvent</see>, <see cref="ProxyListener">ProxyListener</see>
		/// for more detail.
		/// </remarks>	
		public void Initialize( ArrayList listeners )
		{
			_internalListeners = new InternalListenerRecord[ listeners.Count ];

			bool[] listenedEventTypes = new bool[ (int)EventType.NUM_OF_EVENT_TYPES ];
			for( int i=0; i<listenedEventTypes.Length; i++ )
				listenedEventTypes[i] = false;
			
			// create internal table of listeners and set their listened events
			for( int i=0; i<listeners.Count; i++ )
			{
				IListener listener = (IListener)listeners[i];
				_internalListeners[i].listener = listener;
				_internalListeners[i].listenedEventTypes = new bool[ (int)EventType.NUM_OF_EVENT_TYPES ];

				for( int j=0; j<(int)EventType.NUM_OF_EVENT_TYPES; j++ )
					_internalListeners[i].listenedEventTypes[j] = false;

				for( int j=0; j<listener.GetAcceptedEventTypeCount(); j++ )
				{
					_internalListeners[i].listenedEventTypes[ (int)listener.GetAcceptedEventType(j) ] = true;
					listenedEventTypes[ (int)listener.GetAcceptedEventType(j) ] = true;
				}
			}

			// set this listener's accepted event types
			ArrayList acceptedEventTypes = new ArrayList();
			for( int i=0; i<listenedEventTypes.Length; i++ )
				if( listenedEventTypes[i] )
					acceptedEventTypes.Add( (EventType)i );
			_acceptedEventTypes = (EventType[])acceptedEventTypes.ToArray( typeof(EventType) );	

		}

		/// <summary>
		/// Get accepted event type.
		/// </summary>
		/// <param name="acceptedEventTypeIndex">Index of accepted event type.</param>
		/// <returns>Returns accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventType">IListener.GetAcceptedEventType</see>
		/// for more detail.</remarks>		
		public EventType GetAcceptedEventType(int acceptedEventTypeIndex)
		{			
			return( _acceptedEventTypes[acceptedEventTypeIndex] );
		}

		/// <summary>
		/// Get accepted event type count.
		/// </summary>
		/// <returns>Returns number of accepted <see cref="EventType">EventType</see>.</returns>
		/// <remarks>See <see cref="IListener.GetAcceptedEventTypeCount">IListener.GetAcceptedEventTypeCount</see>
		/// for more detail.</remarks>
		public int GetAcceptedEventTypeCount()
		{
			return( _acceptedEventTypes.Length );
		}

		/// <summary>
		/// Sends this event to all registered listeners.
		/// </summary>
		/// <param name="e">Event to send.</param>
		/// <remarks>See <see cref="IListener.OnEvent">IListener.OnEvent</see>
		/// for more detail.</remarks>		
		public void OnEvent( IEvent e )
		{
			foreach( InternalListenerRecord record in _internalListeners )
				if( record.listenedEventTypes[(int)e.Type] 
					|| e==CompositionManager.SimulationFinishedEvent
					|| e==CompositionManager.SimulationFailedEvent )
					record.listener.OnEvent( e );
		}		
	
	}
	
}
