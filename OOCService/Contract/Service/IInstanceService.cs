using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IInstanceService
    {
        [OperationContract]
        GenericResponse Heartbeat(InstanceHeartbeatStatus status);

        [OperationContract]
        InstanceStatusResponse QueryStatusByInstanceName(string instanceName);

    }
}
