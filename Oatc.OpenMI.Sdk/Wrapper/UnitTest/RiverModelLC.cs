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
using System.Collections;
using OpenMI.Standard;
using Oatc.OpenMI.Sdk.Backbone;
using Oatc.OpenMI.Sdk.Wrapper;

namespace Oatc.OpenMI.Sdk.Wrapper.UnitTest
{
	/// <summary>
	/// Summary description for RiverModelLC.
	/// </summary>
	public class RiverModelLC : Oatc.OpenMI.Sdk.Wrapper.LinkableEngine, IManageState
	{
			
		public RiverModelEngine _riverModelEngine;
		public int _maxBufferSize;

		public RiverModelLC()
		{
			_riverModelEngine = new RiverModelEngine();
			_maxBufferSize = 0;
		}

		public override IValueSet GetValues(ITime time, string linkID)
		{ 
			int timesCount = ((SmartOutputLink)this.SmartOutputLinks[0]).SmartBuffer.TimesCount;
			if (timesCount > _maxBufferSize)
			{
				_maxBufferSize = timesCount;
			}

			return base.GetValues(time, linkID);

		}

		protected override void SetEngineApiAccess()
		{
			_engineApiAccess = _riverModelEngine;
		}

		// used for testing only
		public bool  PrepareForCompotationWasInvoked
		{
			get
			{
				return (this._prepareForCompotationWasInvoked);
			}
		}

        // used for testing only
        public ArrayList  SmartInputLinks
        {
            get
            {
                return (this._smartInputLinks);
            }
        }

        // used for testing only
        public ArrayList  SmartOutputLinks
        {
            get
            {
                return (this._smartOutputLinks);
            }
        }

		// used for testing only
		public ArrayList  ValidationErrorMessages
		{
			get
			{
				return (this._validationErrorMessages);
			}
		}

		// used for testing only
		public ArrayList ValidationWarningMessages 
		{
			get
			{
				return (this._validationWarningMessages );
			}
		}
	}

	
}
