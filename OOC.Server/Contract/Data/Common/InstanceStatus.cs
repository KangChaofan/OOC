using System.Runtime.Serialization;
using OOC.Contract.Data.Request;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class InstanceStatus : InstanceHeartbeatStatus
    {
        [DataMember]
        public string InstanceEndPoint { get; set; }

        public InstanceStatus() { }

        public InstanceStatus(string instanceEndPoint, InstanceHeartbeatStatus status)
        {
            InstanceEndPoint = instanceEndPoint;
            InstanceName = status.InstanceName;
            RunningTask = status.RunningTask;
            SystemStatus = status.SystemStatus;
        }
    }
}