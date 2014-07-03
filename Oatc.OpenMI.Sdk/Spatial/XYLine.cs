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

namespace Oatc.OpenMI.Sdk.Spatial
{
  /// <summary>
	/// The XYline class is used for representing line segments. XYPolylines 
	/// and XYPolygons are composed of XYLines.
	/// </summary>
	public class XYLine
	{    
    /// <summary>
		/// Constructor.
		/// </summary>
    /// <returns>None</returns>
		public XYLine()
		{
			P1 = new XYPoint();
			P2 = new XYPoint();
		}

    /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="x1">x-coordinate for line start point</param>
		/// <param name="y1">y-coordinate for line start point</param>
		/// <param name="x2">x-coordinate for line end point</param>
		/// <param name="y2">y-coordinate for line end point</param>
    /// <returns>None</returns>
    public XYLine(double x1, double y1, double x2, double y2)
		{
			P1 = new XYPoint(x1, y1);
			P2 = new XYPoint(x2, y2);
		}

    /// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="point1">Line start point</param>
		/// <param name="point2">Line end point</param>
    /// <returns>None</returns>
    public XYLine(XYPoint point1, XYPoint point2)
		{
			P1 = new XYPoint();
			P2 = new XYPoint();

			P1.X = point1.X;
			P1.Y = point1.Y;;
			P2.X = point2.X;
			P2.Y = point2.Y;
		}
		
    /// <summary>
		/// Constructor. Copies input line.
		/// </summary>
		/// <param name="line">Line to copy</param>
		public XYLine(XYLine line)
		{
			P1 = new XYPoint();
			P2 = new XYPoint();

			P1.X = line.P1.X;
			P1.Y = line.P1.Y;
			P2.X = line.P2.X;
			P2.Y = line.P2.Y;
		}

      /// <summary>
      /// Read only property describing the one end-point.
      /// </summary>
      public XYPoint P1;

      /// <summary>
      /// Read only property describing the one end-point.
      /// </summary>
      public XYPoint P2;

    /// <summary>
		/// Calculates the length of line.
		/// </summary>
		/// <returns>Line length</returns>
		public double	GetLength()
		{
            if (length == 0)
            {
                length = Math.Sqrt((P1.X - P2.X)*(P1.X - P2.X) + (P1.Y - P2.Y)*(P1.Y - P2.Y));
            }
			
            return length;
		}

      private double length;

    /// <summary>
		/// Calculates the mid point of the line.
		/// </summary>
		/// <returns>Returns the line mid point as a XYPoint</returns>
		public XYPoint GetMidpoint()
		{
			return new XYPoint(( P1.X + P2.X ) / 2, ( P1.Y + P2.Y ) / 2);
		}

    /// <summary>
    /// Compares the object type and the coordinates of the object and the 
    /// object passed as parameter.
    /// </summary>
    /// <returns>True if object type is XYLine and the coordinates are 
    /// equal to to the coordinates of the current object. False otherwise.</returns>
    public override bool Equals(Object obj) 
    {
      if (obj == null || GetType() != obj.GetType()) 
      {
        return false;
      }
      return P1.Equals(((XYLine) obj).P1) && this.P2.Equals(((XYLine) obj).P2);
    }

    /// <summary>
    /// Get Hash Code.
    /// </summary>
    /// <returns>Hash Code for the current instance.</returns>
    public override int GetHashCode()
    {
      return base.GetHashCode();
    }
	}
}
