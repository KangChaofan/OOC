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
using System.ServiceModel;
using OOC.Instance.TaskService;
using OOC.Util;

namespace OOC.Instance
{
    public delegate void TaskStateChanged(TaskRunnerManager sender, TaskState taskState);

    public delegate void TaskProgressChanged(TaskRunnerManager sender, Dictionary<string, string> modelProgress);

    public delegate void TaskStopped(TaskRunnerManager sender);

    public class TaskRunnerManager
    {
        private static string taskRunnerExecutable = ConfigurationManager.AppSettings["taskRunnerExecutable"];
        private static string taskWorkingDirectory = ConfigurationManager.AppSettings["taskWorkingDirectory"];
        private static string taskUsername = ConfigurationManager.AppSettings["taskUsername"];
        private static string taskPassword = ConfigurationManager.AppSettings["taskPassword"];
        private static int progressReportInterval = Int32.Parse(ConfigurationManager.AppSettings["progressReportInterval"]);

        public TaskAssignResponse TaskAssign { get; set; }
        public TaskStateChanged TaskStateChangedHandler;
        public TaskStopped TaskStoppedHandler;
        public TaskProgressChanged TaskProgressChangedHandler;
        public string WorkingDirectory { get { return WorkspaceManager.WorkingDirectory; } }
        public string PipeName { get; set; }
        public WorkspaceManager WorkspaceManager { get; set; }

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
                        try
                        {
                            runnerProcess.Kill();
                        }
                        catch { }
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
        private bool isReleased = false;

        public TaskRunnerManager(TaskAssignResponse taskAssign)
        {
            TaskAssign = taskAssign;
        }

        private void transmitComposition(BinaryWriter bw)
        {
            Dictionary<string, string> properties;

            foreach (CompositionModelData cmData in TaskAssign.CompositionData.Models)
            {
                string mainLibrary = WorkspaceManager.GetCompositionModelMainLibrary(cmData.CompositionModel);
                properties = new Dictionary<string, string>();
                properties["modelId"] = cmData.CompositionModel.guid;
                properties["linkableComponent"] = cmData.Model.className;
                properties["assemblyPath"] = mainLibrary;
                properties["workingDirectory"] = WorkspaceManager.GetCompositionModelDirectory(cmData.CompositionModel);
                PipeUtil.WriteCommand(bw, new PipeCommand("AddModel", properties));

                properties = new Dictionary<string, string>();
                properties["modelId"] = cmData.CompositionModel.guid;
                if (cmData.PropertyValues != null)
                {
                    Dictionary<string, string> propertyValues = cmData.PropertyValues.Kvs;
                    foreach (ModelProperty property in cmData.ModelProperties)
                    {
                        switch (property.type)
                        {
                            case 4:
                                properties[property.key] = WorkspaceManager.GetLocalPath(propertyValues[property.key]);
                                break;
                            case 5:
                                properties[property.key] = WorkspaceManager.HomeDirectory + @"\" + propertyValues[property.key];
                                break;
                            default:
                                properties[property.key] = propertyValues[property.key];
                                break;
                        }
                    }
                }
                PipeUtil.WriteCommand(bw, new PipeCommand("SetModelProperties", properties));
            }

            foreach (CompositionLinkData link in TaskAssign.CompositionData.Links)
            {
                properties = new Dictionary<string, string>();
                properties["linkId"] = link.CompositionLink.guid;
                properties["sourceModelId"] = link.CompositionLink.sourceCmGuid;
                properties["targetModelId"] = link.CompositionLink.targetCmGuid;
                properties["sourceQuantity"] = link.CompositionLink.sourceQuantity;
                properties["targetQuantity"] = link.CompositionLink.targetQuantity;
                properties["sourceElementSet"] = link.CompositionLink.sourceElementSet;
                properties["targetElementSet"] = link.CompositionLink.targetElementSet;
                PipeUtil.WriteCommand(bw, new PipeCommand("AddLink", properties));
            }

            properties = new Dictionary<string, string>();
            properties["triggerInvokeTime"] = TaskAssign.TriggerInvokeTime;
            properties["progressReportInterval"] = progressReportInterval.ToString();
            PipeUtil.WriteCommand(bw, new PipeCommand("SetSimulationProperties", properties));
        }

        private bool runnerLifetime()
        {
            bool succeed = false;
            using (BinaryReader br = new BinaryReader(pipeServer))
            using (BinaryWriter bw = new BinaryWriter(pipeServer))
            {
                /* handshake */
                PipeCommand helloCommand = PipeUtil.ReadCommand(br);
                if (helloCommand.Command != "Hello") throw new Exception("Handshake failed.");
                logger.Info("Pipe: Handshake signal received.");
                PipeUtil.WriteCommand(bw, new PipeCommand("Hello"));
                logger.Info("Pipe: Handshake signal sent.");

                /* transmit composition */
                transmitComposition(bw);
                PipeUtil.WriteCommand(bw, new PipeCommand("RunSimulation"));

                /* waiting for simulation finish */
                do
                {
                    PipeCommand command = PipeUtil.ReadCommand(br);
                    logger.Info("Pipe: Received command: " + command.Command);
                    switch (command.Command)
                    {
                        case "Progress":
                            TaskProgressChangedHandler(this, command.Parameters);
                            break;
                        case "Completed":
                        case "Failed":
                            PipeUtil.WriteCommand(bw, new PipeCommand("Halt"));
                            succeed = (command.Command == "Completed");
                            isReleased = true;
                            break;
                    }
                } while (!isReleased);
            }
            runnerProcess.WaitForExit();
            return succeed;
        }

        private void startPipeKeeper()
        {
            pipeKeeper = new Thread(new ThreadStart(delegate()
            {
                logger.Info("Pipe keeper thread started.");
                try
                {
                    if (runnerLifetime() == true)
                    {
                        TaskState = TaskState.Finishing;
                        WorkspaceManager.CollectOutput();
                        TaskState = TaskState.Completed;
                    }
                    else
                    {
                        WorkspaceManager.CollectOutput();
                        TaskState = TaskState.Aborted;
                    }
                }
                catch (Exception e)
                {
                    if (!isReleased)
                    {
                        logger.Crit("Exception occured during task lifetime: " + e.ToString());
                        TaskState = TaskState.Aborted;
                    }
                }
                finally
                {
                    pipeServer.Close();
                    pipeServer.Dispose();
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

        private void initWorkspace()
        {
            WorkspaceManager.DeployComposition(TaskAssign.CompositionData, TaskAssign.InputFiles);
        }

        private void prepareRunner()
        {
            PipeSecurity pipeSecurity = new PipeSecurity();
            pipeSecurity.SetAccessRule(new PipeAccessRule("Everyone",
                PipeAccessRights.ReadWrite, AccessControlType.Allow));
            pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.WriteThrough, 4096, 4096, pipeSecurity, HandleInheritability.None);
            logger.Info("Pipe created: " + PipeName + ".");

            ProcessStartInfo psi = new ProcessStartInfo(taskRunnerExecutable, "--pipeName \"" + PipeName + "\" --log \"" + WorkspaceManager.LogDirectory + @"\runner.log" + "\"");
            psi.WorkingDirectory = WorkspaceManager.HomeDirectory;
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
            string taskGuid = TaskAssign.Task.guid;
            IsDied = false;
            TaskState = TaskState.Assigned;
            WorkspaceManager = new WorkspaceManager(taskGuid, taskWorkingDirectory + taskGuid);
            PipeName = "OOCTaskPipe-" + taskGuid;
            logger = new Logger(WorkspaceManager.LogDirectory + @"\taskManager.log");
            logger.Info("TaskManager initializing...");
            try
            {
                initWorkspace();
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
