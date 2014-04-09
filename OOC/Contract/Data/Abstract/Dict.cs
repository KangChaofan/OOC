using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using OOC.Util;

namespace OOC.Contract.Data.Abstract
{
    [DataContract]
    public class Dict: SerializableData
    {
        [DataMember]
        public Dictionary<string, string> Kvs { get; set; }

        public new string Serialized
        {
            get
            {
                return SerializeUtil.Serialize(Kvs);
            }
            set
            {
                Kvs = SerializeUtil.Deserialize<Dictionary<string, string>>(value);
            }
        }

        public Dict(string xmlString)
        {
            Serialized = xmlString;
        }

        public Dict() { Kvs = new Dictionary<string, string>(); }
    }
}