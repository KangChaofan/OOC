using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections;
using OOC.Util;
using OOC.OpenMIWrapper;

namespace OOC.TaskRunner
{
    public class TaskRunner
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

        public string PipeName { get; set; }

        private NamedPipeClientStream pipeClient;
        private Logger logger;

        private CompositionManager composition;

        public TaskRunner(string pipeName)
        {
            PipeName = pipeName;
        }

        private void RunSimulation(AfterSimulationDelegate callback)
        {
            composition.AfterSimulationHandler += new AfterSimulationDelegate(delegate(object sender, bool succeed)
            {
                logger.Info("Closing composition...");
                composition.Release();

                callback(sender, succeed);
            });

            logger.Info("Starting composition run...");
            composition.Run(logger, true);
        }

        public void Run()
        {
            logger.Info("TaskRunner is initializing...");
            pipeClient = new NamedPipeClientStream(".", PipeName,
                           PipeDirection.InOut, PipeOptions.WriteThrough,
                           TokenImpersonationLevel.Impersonation);
            pipeClient.Connect();
            logger.Info("Pipe connected.");
            bool isReleased = false;
            try
            {
                using (BinaryReader br = new BinaryReader(pipeClient))
                using (BinaryWriter bw = new BinaryWriter(pipeClient))
                {
                    /* handshake */
                    PipeUtil.WriteCommand(bw, new PipeCommand("Hello"));
                    logger.Info("Pipe: Handshake signal sent.");
                    PipeCommand helloCommand = PipeUtil.ReadCommand(br);
                    if (helloCommand.Command != "Hello") throw new Exception("Handshake failed.");
                    logger.Info("Pipe: Handshake signal received.");
                    composition = new CompositionManager();
                    do
                    {
                        string modelId;
                        PipeCommand command = PipeUtil.ReadCommand(br);
                        logger.Info("Pipe: Received command: " + command.Command);
                        switch (command.Command)
                        {
                            case "AddModel":
                                modelId = command.Parameters["modelId"];
                                string workingDirectory = command.Parameters["workingDirectory"];
                                string assemblyPath = command.Parameters["assemblyPath"];
                                string linkableComponent = command.Parameters["linkableComponent"];
                                Model model = new Model();
                                model.Create(modelId, workingDirectory, assemblyPath, linkableComponent);
                                composition.AddModel(model);
                                logger.Info("Created model " + modelId + ": " + linkableComponent);
                                break;
                            case "SetModelProperties":
                                modelId = command.Parameters["modelId"];
                                foreach (KeyValuePair<string, string> entry in command.Parameters)
                                {
                                    logger.Info("Model " + modelId + " Property: [Key=" + entry.Key + ", Value=" + entry.Value + "]");
                                }
                                composition.GetModel(modelId).Init(command.Parameters);
                                break;
                            case "AddLink":
                                // TODO
                                break;
                            case "SetLinkProperties":
                                // TODO
                                break;
                            case "SetSimulationProperties":
                                // TODO
                                break;
                            case "RunSimulation":
                                RunSimulation(delegate(object sender, bool succeed)
                                {
                                    logger.Info("Simulation finished, succeed=" + succeed);
                                    PipeUtil.WriteCommand(bw, new PipeCommand(succeed ? "Completed" : "Failed"));
                                });
                                break;
                            case "Halt":
                                isReleased = true;
                                return;
                        }
                    } while (true);
                }
            }
            catch (Exception e)
            {
                if (!isReleased)
                {
                    logger.Crit("Exception occured during task lifetime: " + e.ToString());
                }
            }
            finally
            {
                pipeClient.Close();
                pipeClient.Dispose();
            }
        }
    }
}
