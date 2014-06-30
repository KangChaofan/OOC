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
using OOC.OutputProcessor;
using System.Reflection;

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
        private Dictionary<string, string> modelProgress;

        private int progressReportInterval = 10;

        private int dataRecordBatchSize = 1000;
        private Dictionary<string, List<OutputDataProcessor>> outputDataProcessors;
        private Dictionary<string, Dictionary<string, string>> outputFiles;
        private Dictionary<string, Dictionary<string, string>> modelProperties;

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

        private List<OutputDataProcessor> getOutputDataProcessors(Model model, string assemblyPath)
        {
            Assembly assembly = AssemblySupport.LoadAssembly(null, assemblyPath);
            List<OutputDataProcessor> processors = new List<OutputDataProcessor>();
            foreach (Type type in assembly.GetTypes())
            {
                if (typeof(OutputDataProcessor).IsAssignableFrom(type))
                {
                    logger.Info("Found data processor: " + type.FullName);
                    processors.Add((OutputDataProcessor)Activator.CreateInstance(type));
                }
            }
            return processors;
        }

        private void flushDataRecord(BinaryWriter bw, string channel, List<string[]> buffer)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters["Channel"] = channel;
            parameters["Records"] = SerializationUtil.FromArrayList(buffer);
            PipeUtil.WriteCommand(bw, new PipeCommand("DataRecord", parameters));
            buffer.Clear();
        }
        public void Run()
        {
            DateTime lastReport;
            outputDataProcessors = new Dictionary<string, List<OutputDataProcessor>>();
            modelProperties = new Dictionary<string, Dictionary<string, string>>();
            outputFiles = new Dictionary<string, Dictionary<string, string>>();
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
                        string modelId, assemblyPath;
                        PipeCommand command = PipeUtil.ReadCommand(br);
                        logger.Info("Pipe: Received command: " + command.Command);
                        switch (command.Command)
                        {
                            case "AddModel":
                                modelId = command.Parameters["modelId"];
                                string workingDirectory = command.Parameters["workingDirectory"];
                                assemblyPath = command.Parameters["assemblyPath"];
                                string linkableComponent = command.Parameters["linkableComponent"];
                                Model model = new Model();
                                model.Create(modelId, workingDirectory, assemblyPath, linkableComponent);
                                composition.AddModel(model);
                                logger.Info("Created model " + modelId + ": " + linkableComponent);
                                break;
                            case "AddDataProcessor":
                                modelId = command.Parameters["modelId"];
                                assemblyPath = command.Parameters["assemblyPath"];
                                outputFiles[modelId] = SerializationUtil.Deserialize<Dictionary<string, string>>(command.Parameters["outputFiles"]);
                                logger.Info("Adding data processor for model " + modelId + ": " + assemblyPath + "...");
                                foreach (KeyValuePair<string, string> entry in outputFiles[modelId])
                                {
                                    logger.Info("Output file " + entry.Key + ": " + entry.Value);
                                }
                                List<OutputDataProcessor> processors = getOutputDataProcessors(composition.GetModel(modelId), assemblyPath);
                                if (!outputDataProcessors.ContainsKey(modelId))
                                    outputDataProcessors[modelId] = new List<OutputDataProcessor>();
                                outputDataProcessors[modelId].AddRange(processors);
                                foreach (OutputDataProcessor processor in processors)
                                {
                                    logger.Info("Model " + modelId + " data processor: " + processor.GetType().FullName);
                                }
                                break;
                            case "SetModelProperties":
                                modelId = command.Parameters["modelId"];
                                foreach (KeyValuePair<string, string> entry in command.Parameters)
                                {
                                    logger.Info("Model " + modelId + " Property: [Key=" + entry.Key + ", Value=" + entry.Value + "]");
                                }
                                modelProperties[modelId] = command.Parameters;
                                composition.GetModel(modelId).Init(modelProperties[modelId]);
                                break;
                            case "AddLink":
                                string linkId = command.Parameters["linkId"];
                                string sourceModelId = command.Parameters["sourceModelId"];
                                string targetModelId = command.Parameters["targetModelId"];
                                string sourceQuantity = command.Parameters["sourceQuantity"];
                                string targetQuantity = command.Parameters["targetQuantity"];
                                string sourceElementSet = command.Parameters["sourceElementSet"];
                                string targetElementSet = command.Parameters["targetElementSet"];
                                logger.Info("Link " + linkId + " Property: [sourceModelId=" + sourceModelId + ", targetModelId=" + targetModelId + ", sourceQuantity=" + sourceQuantity + ", targetQuantity=" + targetQuantity + ", sourceElementSet=" + sourceElementSet + ", targetElementSet=" + targetElementSet + "]");
                                composition.AddLink(linkId, sourceModelId, targetModelId, sourceQuantity, targetQuantity, sourceElementSet, targetElementSet);
                                break;
                            case "SetSimulationProperties":
                                DateTime triggerInvokeTime = DateTime.Parse(command.Parameters["triggerInvokeTime"]);
                                bool parallelized = Boolean.Parse(command.Parameters["parallelized"]);
                                progressReportInterval = Int32.Parse(command.Parameters["progressReportInterval"]);
                                dataRecordBatchSize = Int32.Parse(command.Parameters["dataRecordBatchSize"]);
                                composition.TriggerInvokeTime = triggerInvokeTime;
                                composition.Parallelized = parallelized;
                                break;
                            case "RunSimulation":
                                lastReport = DateTime.Now;
                                modelProgress = new Dictionary<string, string>();
                                composition.CompositionModelProgressChangedHandler += new CompositionModelProgressChangedDelegate(delegate(object sender, string cmGuid, string progress)
                                {
                                    lock (this)
                                    {
                                        modelProgress[cmGuid] = progress;
                                        if ((DateTime.Now - lastReport).TotalSeconds > progressReportInterval)
                                        {
                                            lastReport = DateTime.Now;
                                            PipeUtil.WriteCommand(bw, new PipeCommand("Progress", modelProgress));
                                        }
                                    }
                                });
                                RunSimulation(delegate(object sender, bool succeed)
                                {
                                    logger.Info("Simulation finished, succeed=" + succeed);
                                    PipeUtil.WriteCommand(bw, new PipeCommand("Progress", modelProgress));
                                    PipeUtil.WriteCommand(bw, new PipeCommand(succeed ? "Completed" : "Failed"));
                                });
                                break;
                            case "PostProcess":
                                int channel = 0;
                                foreach (KeyValuePair<string, List<OutputDataProcessor>> p in outputDataProcessors)
                                {
                                    logger.Info("Post-processing model " + p.Key + "...");
                                    foreach (OutputDataProcessor processor in p.Value)
                                    {
                                        logger.Info("Calling data processor " + processor.GetType().FullName + "...");
                                        List<Stream> openedFs = new List<Stream>();
                                        try
                                        {
                                            // set properties
                                            processor.SetProperties(modelProperties[p.Key]);
                                            // open data readers
                                            foreach (KeyValuePair<string, string> o in outputFiles[p.Key])
                                            {
                                                if (!File.Exists(o.Value)) continue;
                                                Stream fs = new FileStream(o.Value, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                                                processor.SetDataReader(o.Key, fs);
                                                openedFs.Add(fs);
                                                logger.Info("Opened " + o.Value + " as " + o.Key + ".");
                                            }
                                            // transfer data
                                            Dictionary<string, string> parameters = new Dictionary<string, string>();
                                            parameters["ModelId"] = p.Key;
                                            parameters["ClassName"] = processor.GetType().FullName;
                                            parameters["Name"] = processor.GetName();
                                            parameters["Channel"] = channel.ToString();
                                            PipeUtil.WriteCommand(bw, new PipeCommand("DataSet", parameters));
                                            List<string[]> buffer = new List<string[]>();
                                            while (processor.HasNextRecord())
                                            {
                                                buffer.Add(processor.GetNextRecord());
                                                if (buffer.Count >= dataRecordBatchSize)
                                                    flushDataRecord(bw, channel.ToString(), buffer);
                                            }
                                            if (buffer.Count > 0)
                                                flushDataRecord(bw, channel.ToString(), buffer);
                                            channel++;
                                        }
                                        finally
                                        {
                                            // clean up
                                            foreach (Stream fs in openedFs)
                                            {
                                                fs.Close();
                                                fs.Dispose();
                                            }
                                        }
                                    }
                                }
                                PipeUtil.WriteCommand(bw, new PipeCommand("PostProcessCompleted"));
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
