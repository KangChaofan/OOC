using System.Runtime.Serialization;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class NodeSystemStatus
    {
        [DataMember]
        public long TotalRamSize { get; set; }

        [DataMember]
        public long AvailableRamSize { get; set; }

        [DataMember]
        public int ProcessCount { get; set; }

        [DataMember]
        public decimal LoadAverage { get; set; }
    }
}