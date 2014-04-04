using System.Runtime.Serialization;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class InstanceStatusResponse : GenericResponse
    {
        [DataMember]
        public InstanceStatus InstanceStatus { get; set; }

        public InstanceStatusResponse(InstanceStatus instanceStatus)
            : base(true)
        {
            InstanceStatus = instanceStatus;
        }

        public InstanceStatusResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}