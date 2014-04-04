using System.Runtime.Serialization;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class FileListResponse : GenericResponse
    {
        public FileListResponse(FileDescription[] files)
            : base(true)
        {
            Files = files;
        }

        public FileListResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }

        [DataMember]
        public FileDescription[] Files { get; set; }
    }
}