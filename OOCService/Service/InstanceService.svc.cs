using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.ORM;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

namespace OOC.Service
{
    [ExposedService("InstanceService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class InstanceService : IInstanceService
    {
        private static Dictionary<string, InstanceStatus> instances = new Dictionary<string, InstanceStatus>();

        public GenericResponse Heartbeat(InstanceHeartbeatStatus status)
        {
            instances[status.InstanceName] = new InstanceStatus(RemoteUtil.GetClientIPAddress(), status);
            return new GenericResponse(true);
        }

        public InstanceStatusResponse QueryStatusByInstanceName(string instanceName)
        {
            if (!instances.ContainsKey(instanceName))
            {
                return new InstanceStatusResponse(false, 1, "INSTANCE_NOT_FOUND");
            }
            return new InstanceStatusResponse(instances[instanceName]);
        }
    }
}