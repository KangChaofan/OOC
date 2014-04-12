using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
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

        public void Heartbeat(InstanceHeartbeatStatus status)
        {
            instances[status.InstanceName] = new InstanceStatus(RemoteUtil.GetClientIPAddress(), status);
        }

        public InstanceStatus QueryStatusByInstanceName(string instanceName)
        {
            if (!instances.ContainsKey(instanceName))
            {
                throw new FaultException("INSTANCE_NOT_EXISTS");
            }
            return instances[instanceName];
        }
    }
}