using System.Runtime.Serialization;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Data.Request
{
    [DataContract]
    public class InstanceHeartbeatStatus
    {
        [DataMember]
<<<<<<< HEAD
        public string InstanceName { get; set; }

        [DataMember]
        public int CurrentRunningTask { get; set; }

        [DataMember]
=======
        public string InstanceName { get; set; }

        [DataMember]
        public int CurrentRunningTask { get; set; }

        [DataMember]
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
        public int MaxRunningTask { get; set; }

        [DataMember]
        public NodeSystemStatus SystemStatus { get; set; }
    }
}