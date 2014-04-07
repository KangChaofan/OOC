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

        private void startManager(TaskAssignResponse taskAssign)
        {
            RunningTask++;
            logger.Info("Starting task manager for task " + taskAssign.Task.guid + "...");
            TaskRunnerManager manager = new TaskRunnerManager(taskAssign);
            manager.TaskStateChangedHandler += new TaskStateChanged(delegate(TaskRunnerManager sender, TaskState state)
            {
                string guid = sender.TaskAssign.Task.guid;
                logger.Info("State of task " + guid + " changed to " + state);
                taskService.UpdateState(guid, state);
            });
            manager.TaskStoppedHandler += new TaskStopped(delegate(TaskRunnerManager sender)
            {
                RunningTask--;
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
                            TaskAssignResponse taskAssign = taskService.AssignPendingTask(InstanceName);
                            new Thread(new ThreadStart(delegate()
                            {
                                try
                                {
                                    startManager(taskAssign);
                                }
                                catch (Exception e)
                                {
                                    logger.Crit("Failed to start task manager: " + e.ToString());
                                }
                            })).Start();
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
