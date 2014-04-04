using System.Runtime.Serialization;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class TaskCreateResponse : GenericResponse
    {
        public TaskCreateResponse(string guid)
            : base(true)
        {
            Guid = guid;
        }

        public TaskCreateResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }

        [DataMember]
        public string Guid { get; set; }
    }
}