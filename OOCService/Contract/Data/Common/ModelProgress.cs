using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using OOC.ORM;
using OOC.Util;
using OOC.Contract.Data.Abstract;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class ModelProgress : SerializableData
    {
        [DataMember]
        public Dictionary<string, string> Progress { get; set; }

        public ModelProgress()
        {
            Progress = new Dictionary<string, string>();
        }

    }
}