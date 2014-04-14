using System.Runtime.Serialization;
using OOC.Contract.Data.Common;
using OOC.Entity;
using OOC.Util;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class TaskDataResponse
    {
        public TaskDataResponse(Task task)
        {
            Task = task;
            CompositionData = SerializationUtil.Deserialize<CompositionData>(task.compositionData);
        }
        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public CompositionData CompositionData { get; set; }
    }
}