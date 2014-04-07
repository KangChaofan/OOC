using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using OOC.Entity;
using OOC.Util;
using OOC.Contract.Data.Abstract;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class CompositionModelData
    {
        [DataMember]
        public CompositionModel CompositionModel { get; set; }

        [DataMember]
        public Model Model { get; set; }

        [DataMember]
        public List<ModelProperty> ModelProperties { get; set; }

        [DataMember]
        public CompositionModelProperties PropertyValues { get; set; }

        [DataMember]
        public List<ModelFileMapping> ModelFiles { get; set; }

        public CompositionModelData() { }

        public CompositionModelData(CompositionModel compositionModel)
        {
            CompositionModel = compositionModel;
            Model = compositionModel.Model;
            ModelProperties = Model.ModelProperty.ToList();
            PropertyValues = new CompositionModelProperties() { Serialized = compositionModel.properties };
            ModelFiles = compositionModel.Model.ModelFileMapping.ToList();
        }
    }

    [DataContract]
    public class CompositionData : SerializableData
    {
        [DataMember]
        public Composition Composition { get; set; }

        [DataMember]
        public List<CompositionModelData> Models { get; set; }

        [DataMember]
        public List<CompositionLink> Links { get; set; }

        public CompositionData() { }

        public CompositionData(Composition composition)
        {
            Composition = composition;
            Models = new List<CompositionModelData>();
            Links = new List<CompositionLink>();
            foreach (CompositionModel model in composition.CompositionModel)
            {
                Models.Add(new CompositionModelData(model));
            }
            foreach (CompositionLink link in composition.CompositionLink)
            {
                Links.Add(link);
            }
        }

    }
}