using System;
using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using System.Threading;
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
        private const int INSTANCE_CHECK_INTERVAL = 30000;
        private const int INSTANCE_TTL = 30000;
        private static Dictionary<string, InstanceStatus> instances = new Dictionary<string, InstanceStatus>();

        public InstanceService()
        {
            new Thread(new ThreadStart(delegate()
            {
                while (true)
                {
                    lock (this)
                    {
                        List<string> diedInstances = new List<string>();
                        foreach (KeyValuePair<string, InstanceStatus> entry in instances)
                        {
                            if ((DateTime.Now - entry.Value.LastHeartbeat).TotalMilliseconds > INSTANCE_TTL)
                            {
                                /* assume instance is died */
                                ITaskService taskService = new TaskService();
                                taskService.ReportInstanceFault(entry.Value.InstanceName);
                                diedInstances.Add(entry.Value.InstanceName);
                            }
                        }
                        foreach (string instanceName in diedInstances)
                        {
                            instances.Remove(instanceName);
                        }
                    }
                    Thread.Sleep(INSTANCE_CHECK_INTERVAL);
                }
            })).Start();
        }

        public void Heartbeat(InstanceHeartbeatStatus status)
        {
            lock (this)
            {
                instances[status.InstanceName] = new InstanceStatus(RemoteUtil.GetClientIPAddress(), status);
            }
        }

        public InstanceStatus QueryStatusByInstanceName(string instanceName)
        {
            if (!instances.ContainsKey(instanceName))
            {
                throw new FaultException("INSTANCE_NOT_EXISTS");
            }
            return instances[instanceName];
        }

        public double GetLoadFactor()
        {
            int maxSlot = 0;
            int usedSlot = 0;
            lock (this)
            {
                List<string> diedInstances = new List<string>();
                foreach (KeyValuePair<string, InstanceStatus> entry in instances)
                {
                    InstanceStatus status = entry.Value;
                    maxSlot += status.MaxRunningTask;
                    usedSlot += status.CurrentRunningTask;
                }
            }
            if (maxSlot == 0) return 1.0d;
            return (double)usedSlot / (double)maxSlot;
        }
    }
}