using System.Runtime.Serialization;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Data.Request
{
    [DataContract]
    public class InstanceHeartbeatStatus
    {
        [DataMember]
        public string InstanceName { get; set; }

        [DataMember]
        public int CurrentRunningTask { get; set; }

        [DataMember]
        public int MaxRunningTask { get; set; }

        [DataMember]
        public NodeSystemStatus SystemStatus { get; set; }
    }
}