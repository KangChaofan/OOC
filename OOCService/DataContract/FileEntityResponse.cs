using System.Runtime.Serialization;

namespace OOC.Response
{
    [DataContract]
    public class FileEntityResponse : GenericResponse
    {
        string fileName;
        byte[] content;

        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [DataMember]
        public byte[] Content
        {
            get { return content; }
            set { content = value; }
        }

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