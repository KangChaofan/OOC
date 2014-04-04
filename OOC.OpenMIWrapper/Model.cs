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
		private string _omiFilename;
	
		private ILinkableComponent _linkableComponent;

		private string _modelID;
		
		/// <summary>
		/// Creates a new instance of <see cref="Model">UIModel</see> class.
		/// </summary>
		public Model()
		{
		}

		/// <summary>
		/// Gets or sets path to OMI file representing this model.
		/// </summary>
		/// <remarks>Setting of this property has only sense in case this model is trigger, see
		/// <see cref="NewTrigger">NewTrigger</see> method.</remarks>
		public string OmiFilename
		{
			get { return(_omiFilename); }
			set { _omiFilename = value; }
		}

		/// <summary>
		/// Gets ID of this model.
		/// </summary>
		/// <remarks>ID is equivalent to <see cref="ILinkableComponent.ModelID">ILinkableComponent.ModelID</see>.
		/// It must be unique in the composition.
		/// </remarks>
		public string ModelID
		{
			get { return(_modelID); }
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


		/// <summary>
		/// Sets this model according to OMI file.
		/// </summary>
		/// <param name="filename">Relative or absolute path to OMI file describing the model.</param>
		/// <param name="relativeDirectory">Directory <c>filename</c> is relative to, or <c>null</c> if <c>filename</c> is absolute or relative to current directory.</param>
		/// <remarks>See <see cref="Utils.GetFileInfo">Utils.GetFileInfo</see> for more info about how
		/// specified file is searched.</remarks>	
		public void ReadOMIFile( string relativeDirectory, string filename )
		{
		    // Open OMI file as xmlDocument
		    FileInfo omiFileInfo = Utils.GetFileInfo(relativeDirectory, filename);
		    if (!omiFileInfo.Exists)
		        throw (new Exception("Omi file not found (CurrentDirectory='" + Directory.GetCurrentDirectory() + "', File='" +
		                             filename + "')"));

		    XmlDocument xmlDocument = new XmlDocument();
		    xmlDocument.Load(omiFileInfo.FullName);

		    // get 1st LinkableComponent element
		    XmlElement xmlLinkableComponent = null;
		    foreach (XmlNode node in xmlDocument.ChildNodes)
		        if (node.Name == "LinkableComponent")
		        {
		            xmlLinkableComponent = (XmlElement) node;
		            break;
		        }

		    // load assembly if present (from relative location of the OMI file)
		    if (xmlLinkableComponent == null)
		    {
		        throw new Exception("No linkable components found in composition file " + omiFileInfo);
		    }
		    else
		    {
		        string assemblyFilename = xmlLinkableComponent.GetAttribute("Assembly");
		        if (assemblyFilename != null)
		            AssemblySupport.LoadAssembly(omiFileInfo.DirectoryName, assemblyFilename);
		    }

		    // read arguments
		    ArrayList linkableComponentArguments = new ArrayList();

		    foreach (XmlElement xmlArguments in xmlLinkableComponent.ChildNodes)
		        if (xmlArguments.Name == "Arguments")
		            foreach (var xmlArgument in xmlArguments.ChildNodes)
		            {
		                if (xmlArgument is XmlElement)
		                {
		                    var element = xmlArgument as XmlElement;
		                    linkableComponentArguments.Add(new Argument(element.GetAttribute("Key"),
		                                                                element.GetAttribute("Value"), true,
		                                                                "No description"));
		                }
		            }

		    // get new instance of ILinkableComponent type
			// for this moment set current directory to omi file's directory
			string oldDirectory = Directory.GetCurrentDirectory(); 
			try 
			{
				Directory.SetCurrentDirectory( omiFileInfo.DirectoryName );

				string classTypeName = xmlLinkableComponent.GetAttribute("Type");
				object obj = AssemblySupport.GetNewInstance( classTypeName );
				if ( ! ( obj is ILinkableComponent ) )
				{
					throw new Exception("\n\nThe class type " + classTypeName + " in\n" +
						filename +
						"\nis not an OpenMI.Standard.ILinkableComponent (OpenMI.Standard.dll version 1.4)." +
						"\nYou may have specified a wrong class name, " +
						"\nor the class implements the ILinkableComponent interface of a previous version of the OpenMI Standard.\n");
				}
				_linkableComponent = (ILinkableComponent)obj;
				_linkableComponent.Initialize( (IArgument[])linkableComponentArguments.ToArray(typeof(IArgument)) );
			}
			finally
			{
				Directory.SetCurrentDirectory( oldDirectory );
			}

			_omiFilename = omiFileInfo.FullName;
			
			_modelID = _linkableComponent.ModelID;

		}

        private IDictionary<StringPair, IInputExchangeItem> inputExchangeItems;

        private IDictionary<StringPair, IOutputExchangeItem> outputExchangeItems;

	    public IInputExchangeItem GetInputExchangeItem(string elementSetId, string quantityId)
	    {
            if(inputExchangeItems == null)
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
            if(outputExchangeItems == null)
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
