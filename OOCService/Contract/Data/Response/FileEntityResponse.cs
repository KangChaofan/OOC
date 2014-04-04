using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileEntityResponse : GenericResponse
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Content { get; set; }

        public FileEntityResponse(string fileName, byte[] content)
            : base(true)
        {
            FileName = fileName;
            Content = content;
        }

        public FileEntityResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}