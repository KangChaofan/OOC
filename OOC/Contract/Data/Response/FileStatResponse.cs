using System.Runtime.Serialization;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileStatResponse
    {
        [DataMember]
        public string FileName { get; set; }

        [DataMember]
        public long Size { get; set; }

        public FileStatResponse(string fileName, long size)
        {
            FileName = fileName;
            Size = size;
        }
    }
}