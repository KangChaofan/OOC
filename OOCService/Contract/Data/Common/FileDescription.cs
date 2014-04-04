using System.Runtime.Serialization;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class FileDescription
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public int Size { get; set; }
    }
}