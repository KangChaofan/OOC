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
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Oatc.OpenMI.Sdk.Backbone;
using OpenMI.Standard;

namespace OOC.OpenMIWrapper
{
    /// <summary>
    /// Summary description for Model.
    /// </summary>
    public class Model
    {
        private ILinkableComponent _linkableComponent;

        private string _modelID;

        private string _workingDirectory;

        /// <summary>
        /// Creates a new instance of <see cref="Model">UIModel</see> class.
        /// </summary>
        public Model()
        {
        }

        /// <summary>
        /// Gets ID of this model.
        /// </summary>
        /// <remarks>ID is equivalent to <see cref="ILinkableComponent.ModelID">ILinkableComponent.ModelID</see>.
        /// It must be unique in the composition.
        /// </remarks>
        public string ModelID
        {
            get { return (_modelID); }
        }

        /// <summary>
        /// Linkable component corresponding to this model.
        /// </summary>
        public ILinkableComponent LinkableComponent
        {
            get
            {
                return _linkableComponent;
            }
            set
            {
                _linkableComponent = value;
                _modelID = _linkableComponent.ModelID;
            }
        }

        public void Init(Dictionary<string, string> properties)
        {
            ArrayList linkableComponentArguments = new ArrayList();
            foreach (KeyValuePair<string, string> entry in properties)
            {
                linkableComponentArguments.Add(new Argument(entry.Key, entry.Value, true, "No description"));
            }
            string oldDirectory = Directory.GetCurrentDirectory();
            try
            {
                Directory.SetCurrentDirectory(_workingDirectory);
                _linkableComponent.Initialize((IArgument[])linkableComponentArguments.ToArray(typeof(IArgument)));
            }
            finally
            {
                Directory.SetCurrentDirectory(oldDirectory);
            }
        }

        public void Create(string modelId, string workingDirectory, string assemblyPath, string linkableComponent)
        {
            _workingDirectory = workingDirectory;
            string oldDirectory = Directory.GetCurrentDirectory();
            try
            {
                Directory.SetCurrentDirectory(_workingDirectory);
                AssemblySupport.LoadAssembly(_workingDirectory, assemblyPath);

                object obj = AssemblySupport.GetNewInstance(linkableComponent);
                if (!(obj is ILinkableComponent))
                {
                    throw new Exception("\n\nThe class type " + linkableComponent + " in\n" +
                        assemblyPath +
                        "\nis not an OpenMI.Standard.ILinkableComponent (OpenMI.Standard.dll version 1.4)." +
                        "\nYou may have specified a wrong class name, " +
                        "\nor the class implements the ILinkableComponent interface of a previous version of the OpenMI Standard.\n");
                }
                _linkableComponent = (ILinkableComponent)obj;
            }
            finally
            {
                Directory.SetCurrentDirectory(oldDirectory);
            }

            _modelID = modelId;

        }

        private IDictionary<StringPair, IInputExchangeItem> inputExchangeItems;

        private IDictionary<StringPair, IOutputExchangeItem> outputExchangeItems;

        public IInputExchangeItem GetInputExchangeItem(string elementSetId, string quantityId)
        {
            if (inputExchangeItems == null)
            {
                inputExchangeItems = new Dictionary<StringPair, IInputExchangeItem>();

                var count = _linkableComponent.InputExchangeItemCount;
                for (var i = 0; i < count; i++)
                {
                    var inputExchangeItem = _linkableComponent.GetInputExchangeItem(i);
                    inputExchangeItems[new StringPair(inputExchangeItem.ElementSet.ID, inputExchangeItem.Quantity.ID)] = inputExchangeItem;
                }
            }

            return inputExchangeItems[new StringPair(elementSetId, quantityId)];
        }

        public IOutputExchangeItem GetOutputExchangeItem(string elementSetId, string quantityId)
        {
            if (outputExchangeItems == null)
            {
                outputExchangeItems = new Dictionary<StringPair, IOutputExchangeItem>();

                var count = _linkableComponent.OutputExchangeItemCount;
                for (var i = 0; i < count; i++)
                {
                    var outputExchangeItem = _linkableComponent.GetOutputExchangeItem(i);
                    outputExchangeItems[new StringPair(outputExchangeItem.ElementSet.ID, outputExchangeItem.Quantity.ID)] = outputExchangeItem;
                }
            }

            return outputExchangeItems[new StringPair(elementSetId, quantityId)];
        }

        private struct StringPair
        {
            public StringPair(string first, string second)
            {
                First = first;
                Second = second;
            }

            public string First;

            public string Second;
        }
    }
}
