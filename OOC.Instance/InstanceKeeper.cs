using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using OOC.Instance.TaskService;
using OOC.Instance.InstanceService;
using OOC.Util;

namespace OOC.Instance
{
    class InstanceKeeper
    {
        public string LogLocation
        {
            get
            {
                return logger.Path;
            }
            set
            {
                logger = new Logger(value);
            }
        }
        public int HeartbeatInterval { get; set; }
        public string InstanceName { get; set; }
        public int RunningTask { get; set; }

        private TaskServiceClient taskService = new TaskServiceClient();

        private Logger logger;

        public InstanceKeeper(string instanceName)
        {
            HeartbeatInterval = 30000;
            InstanceName = instanceName;
        }

        public void Run()
        {
            using (InstanceServiceClient instanceService = new InstanceServiceClient())
            {
                try
                {
                    instanceService.Heartbeat(new InstanceHeartbeatStatus()
                    {
                        InstanceName = InstanceName,
                        RunningTask = RunningTask,
                        SystemStatus = new NodeSystemStatus()
                        {
                            TotalRamSize = SysUtil.getTotalRamSize(),
                            AvailableRamSize = SysUtil.getAvailableRamSize(),
                            ProcessCount = SysUtil.getProcessCount(),
                            LoadAverage = SysUtil.getLoadAverage()
                        }
                    });
                }
                catch
                {

                }
                Thread.Sleep(HeartbeatInterval);
            }
        }
    }
}
