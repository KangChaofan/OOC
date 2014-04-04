using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileStatResponse : GenericResponse
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public long Size { get; set; }

        public FileStatResponse(string fileName, long size)
            : base(true)
        {
            FileName = fileName;
            Size = size;
        }

        public FileStatResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}