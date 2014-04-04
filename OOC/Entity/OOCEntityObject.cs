using System;
using System.ComponentModel;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;

namespace OOC.Entity
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class OOCEntityObject : EntityObject
    {
        [Browsable(false)]
        [IgnoreDataMember]
        public new EntityKey EntityKey { get; set; }
    }
}