using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Configuration;
using System.Security;
using System.Security.AccessControl;
using System.IO;
using System.IO.Pipes;
using OOC.Instance.TaskService;
using OOC.Util;

namespace OOC.Instance
{
    public delegate void TaskStateChanged(TaskRunnerManager sender, TaskState taskState);

    public delegate void TaskStopped(TaskRunnerManager sender);

    public class TaskRunnerManager
    {
        private static string taskRunnerExecutable = ConfigurationManager.AppSettings["taskRunnerExecutable"];
        private static string taskWorkingDirectory = ConfigurationManager.AppSettings["taskWorkingDirectory"];
        private static string taskUsername = ConfigurationManager.AppSettings["taskUsername"];
        private static string taskPassword = ConfigurationManager.AppSettings["taskPassword"];

        public TaskInfoResponse TaskInfo { get; set; }
        public TaskStateChanged TaskStateChangedHandler;
        public TaskStopped TaskStoppedHandler;
        public string WorkingDirectory { get; set; }
        public string PipeName { get; set; }

        public TaskState TaskState
        {
            get { return taskState; }
            set
            {
                if (taskState != value)
                    TaskStateChangedHandler(this, value);
                taskState = value;
            }
        }

        public bool IsDied
        {
            get { return isDied; }
            set
            {
                if (isDied == false && value == true)
                {
                    if (runnerProcess != null)
                        runnerProcess.Kill();
                    TaskStoppedHandler(this);
                }
                isDied = value;
            }
        }

        private TaskState taskState;
        private bool isDied;
        private Logger logger;
        private NamedPipeServerStream pipeServer;
        private Thread pipeKeeper;
        private Process runnerProcess;

        public TaskRunnerManager(TaskInfoResponse taskInfo)
        {
            TaskInfo = taskInfo;
        }

        private void startPipeKeeper()
        {
            pipeKeeper = new Thread(new ThreadStart(delegate()
            {
                logger.Info("Pipe keeper thread started.");
                try
                {
                    // Read user input and send that to the client process.
                    using (StreamWriter sw = new StreamWriter(pipeServer))
                    {
                        sw.AutoFlush = true;
                        while (true)
                        {
                            Thread.Sleep(100);
                        }
                    }
                }
                catch (Exception e)
                {
                    logger.Info("Exception occured during pipe communication: " + e.ToString());
                    TaskState = TaskState.Aborted;
                }
                IsDied = true;
            }));
            pipeKeeper.Start();
        }

        private SecureString getPassword()
        {
            SecureString password = new SecureString();
            foreach (char c in taskPassword)
            {
                password.AppendChar(c);
            }
            return password;
        }

        private void createWorkspace()
        {
        }

        private void prepareRunner()
        {
            PipeSecurity pipeSecurity = new PipeSecurity();
            pipeSecurity.SetAccessRule(new PipeAccessRule("Everyone",
                PipeAccessRights.ReadWrite, AccessControlType.Allow));
            pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.None, 4096, 4096, pipeSecurity, HandleInheritability.None);
            logger.Info("Pipe created: " + PipeName + ".");

            ProcessStartInfo psi = new ProcessStartInfo(taskRunnerExecutable, "--pipeName " + PipeName);
            psi.WorkingDirectory = WorkingDirectory;
            psi.UserName = taskUsername;
            psi.Password = getPassword();
            psi.UseShellExecute = false;

            logger.Info("Starting " + psi.FileName + " with arguments " + psi.Arguments + "...");
            runnerProcess = Process.Start(psi);

            logger.Info("Waiting for pipe connection...");
            pipeServer.WaitForConnection();
            logger.Info("Pipe connected.");
        }

        public void Run()
        {
            IsDied = false;
            TaskState = TaskState.Assigned;
            WorkingDirectory = taskWorkingDirectory + TaskInfo.Task.guid;
            if (!Directory.Exists(WorkingDirectory)) Directory.CreateDirectory(WorkingDirectory);
            PipeName = "OOCTaskPipe-" + TaskInfo.Task.guid;
            logger = new Logger(WorkingDirectory + @"\taskManager.log");
            logger.Info("TaskManager initializing...");
            try
            {
                createWorkspace();
                prepareRunner();
                startPipeKeeper();
                TaskState = TaskState.Running;
            }
            catch (Exception e)
            {
                logger.Info("Failed to initialize task: " + e.ToString());
                TaskState = TaskState.Aborted;
                IsDied = true;
            }
        }
    }
}
