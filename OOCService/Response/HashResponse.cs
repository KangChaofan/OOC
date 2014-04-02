using System.Runtime.Serialization;

namespace OOC.Response
{
    [DataContract]
    public class HashResponse : GenericResponse
    {
        string hash;

        [DataMember]
        public string Hash
        {
            get { return hash; }
            set { hash = value; }
        }

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