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
using System.Runtime.InteropServices;
using System.Text;
using System.IO;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.DevelopmentSupport;
using Microsoft.Win32;


namespace OOC.OpenMIWrapper
{
	/// <summary>
	/// Class contains support methods.
	/// </summary>
	public class Utils
	{

		/// <summary>
		/// Determines whether two dimensions are equal.
		/// </summary>
		/// <param name="dimension1">Dimension one</param>
		/// <param name="dimension2">Dimension two</param>
		/// <returns>Returns <c>true</c> if powers of all dimension bases are same, otherwise returns <c>false</c>.</returns>
		public static bool CompareDimensions(IDimension dimension1, IDimension dimension2)
		{
			for (int i = 0; i < (int)DimensionBase.NUM_BASE_DIMENSIONS; i++)			
				if (dimension1.GetPower((DimensionBase) i) != dimension2.GetPower((DimensionBase) i))				
					return( false );
				
			return( true );
		}


		/// <summary>
		/// Gets <c>FileInfo</c> of file specified by it's (eventually relative) path.
		/// </summary>
		/// <param name="relativeDir">Directory <c>filename</c> is relative to, or <c>null</c> if <c>filename</c> is absolute path or relative path to current directory.</param>
		/// <param name="filename">Relative or absolute path to file.</param>
		/// <returns>Returns <c>FileInfo</c> of file specified.</returns>
		public static FileInfo GetFileInfo( string relativeDir, string filename )
		{
			string oldDirectory=null;
			if( relativeDir!=null )
				oldDirectory = Directory.GetCurrentDirectory();

			try 
			{
				if( relativeDir!=null )
					Directory.SetCurrentDirectory( relativeDir );
				return( new FileInfo(filename) );
			}
			finally
			{
				if( oldDirectory!=null )
					Directory.SetCurrentDirectory( oldDirectory );
			}
		}


		/// <summary>
		/// Converts event to <c>string</c> representation.
		/// </summary>
		/// <param name="Event">Event to be converted to <c>string</c></param>
		/// <returns>Returns resulting <c>string</c>.</returns>
		public static string EventToString( IEvent Event )
		{
			StringBuilder builder = new StringBuilder( 200 );
			builder.Append( "[Type=" );
			builder.Append( Event.Type.ToString() );
			
			if( Event.Description!=null )
			{
				builder.Append( "][Message=" );
				builder.Append( Event.Description );
			}
			
			if( Event.Sender != null )
			{
				builder.Append( "][ModelID=" );
				builder.Append( ((ILinkableComponent) Event.Sender).ModelID );
			}
			
			if( Event.SimulationTime != null )
			{
				builder.Append( "][SimTime=" );
				builder.Append( CalendarConverter.ModifiedJulian2Gregorian(Event.SimulationTime.ModifiedJulianDay).ToString() );
			}

			builder.Append( ']' );	

			return( builder.ToString() );
		}
		
	}
}
