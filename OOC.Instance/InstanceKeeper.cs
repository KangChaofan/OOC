using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.ServiceModel;
using OOC.Instance.TaskService;
using OOC.Instance.InstanceService;
using OOC.Util;

namespace OOC.Instance
{
    public class InstanceKeeper
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
        public bool IsRunning { get; set; }

        private TaskServiceClient taskService = new TaskServiceClient();

        private Logger logger;

        public InstanceKeeper(string instanceName)
        {
            LogLocation = null;
            HeartbeatInterval = 30000;
            InstanceName = instanceName;
            RunningTask = 0;
        }

        private void startManager(TaskInfoResponse taskInfo)
        {
            RunningTask++;
            logger.Info("Starting task manager for task " + taskInfo.Task.guid + "...");
            TaskRunnerManager manager = new TaskRunnerManager(taskInfo);
            manager.TaskStateChangedHandler += new TaskStateChanged(delegate(TaskRunnerManager sender, TaskState state)
            {
                string guid = sender.TaskInfo.Task.guid;
                logger.Info("State of task " + guid + " changed to " + state);
                taskService.UpdateState(guid, state);

                if (state == TaskState.Completed || state == TaskState.Aborted)
                {
                    RunningTask--;
                }

            });
            manager.Run();
        }

        public void Run()
        {
            IsRunning = true;
            logger.Info("InstanceKeeper [" + InstanceName + "] is running.");
            using (InstanceServiceClient instanceService = new InstanceServiceClient())
            {
                while (IsRunning)
                {
                    try
                    {
                        logger.Info("Invoking heartbeat...");
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
                        logger.Info("Heartbeat sent.");
                    }
                    catch (FaultException ex)
                    {
                        logger.Error("Exception occured during heartbeat: " + ex.Reason);
                    }
                    try
                    {
                        if (RunningTask == 0)
                        {
                            TaskInfoResponse taskInfo = taskService.AssignPendingTask(InstanceName);
                            startManager(taskInfo);
                        }
                    }
                    catch
                    {
                        logger.Debug("No task available.");
                    }
                    Thread.Sleep(HeartbeatInterval);
                }
            }
        }
    }
}
