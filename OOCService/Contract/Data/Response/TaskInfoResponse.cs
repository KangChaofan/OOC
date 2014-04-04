using System.Runtime.Serialization;
using OOC.Contract.Data.Common;
using OOC.ORM;
using OOC.Util;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class TaskInfoResponse : GenericResponse
    {
        public TaskInfoResponse(Task task)
            : base(true)
        {
            Task = task;
            CompositionData = SerializeUtil.Deserialize<CompositionData>(task.compositionData);
        }

        public TaskInfoResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }

        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public CompositionData CompositionData { get; set; }
    }
}