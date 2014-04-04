using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileEntityResponse
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public byte[] Content { get; set; }

        public FileEntityResponse(string fileName, byte[] content)
        {
            FileName = fileName;
            Content = content;
        }
    }
}