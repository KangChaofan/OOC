using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OOC.Instance.TaskService;

namespace OOC.Instance
{
    public delegate void TaskStateChanged(TaskRunnerManager sender, TaskState taskState);

    public class TaskRunnerManager
    {
        public TaskInfoResponse TaskInfo { get; set; }
        public TaskStateChanged TaskStateChangedHandler;

        private Thread watcher;

        public TaskRunnerManager(TaskInfoResponse taskInfo)
        {
            TaskInfo = taskInfo;
        }

        public void Run()
        {
            watcher = new Thread(new ThreadStart(delegate()
            {
                Thread.Sleep(5000);
                TaskStateChangedHandler(this, TaskState.Aborted);
            }));
            watcher.Start();
        }
    }
}
