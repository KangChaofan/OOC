using System.Runtime.Serialization;
using System.Collections.Generic;
using OOC.Contract.Data.Common;
using OOC.Entity;
using OOC.Util;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class TaskAssignResponse
    {
        public TaskAssignResponse(Task task, List<TaskFileMapping> inputFiles, string triggerInvokeTime)
        {
            Task = task;
            InputFiles = inputFiles;
            TriggerInvokeTime = triggerInvokeTime;
            CompositionData = SerializationUtil.Deserialize<CompositionData>(task.compositionData);
        }

        [DataMember]
        public Task Task { get; set; }

        [DataMember]
        public List<TaskFileMapping> InputFiles { get; set; }

        [DataMember]
        public CompositionData CompositionData { get; set; }

        [DataMember]
        public string TriggerInvokeTime { get; set; }
    }
}