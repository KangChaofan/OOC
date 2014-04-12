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
        public int RunningTask { get; set; }

        [DataMember]
        public NodeSystemStatus SystemStatus { get; set; }
    }
}