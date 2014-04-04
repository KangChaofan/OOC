using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using OOC.Util;

namespace OOC.Contract.Data.Abstract
{
    [DataContract]
    public class Dict
    {
        [DataMember]
        public Dictionary<string, string> Kvs { get; set; }

        public string Serialized
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

        public Dict(string jsonString)
        {
            Serialized = jsonString;
        }

        public Dict() { }
    }
}