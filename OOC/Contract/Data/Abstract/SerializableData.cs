using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using OOC.Util;

namespace OOC.Contract.Data.Abstract
{
    [DataContract]
    public class SerializableData
    {
        public string Serialized
        {
            get
            {
                return SerializationUtil.Serialize(this);
            }
        }
    }
}