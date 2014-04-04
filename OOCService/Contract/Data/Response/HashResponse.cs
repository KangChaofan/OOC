using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class HashResponse : GenericResponse
    {
        [DataMember]
        public string Hash { get; set; }

        public HashResponse(string hash)
            : base(true)
        {
            Hash = hash;
        }

        public HashResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}