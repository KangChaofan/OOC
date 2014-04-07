using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.ServiceModel;
using OOC.Instance.TaskService;
using OOC.Instance.FileService;
using OOC.Util;

namespace OOC.Instance
{
    public class WorkspaceManager
    {
        public string TaskGuid { get; set; }

        public string WorkingDirectory { get; set; }

        public string TempDirectory { get { return WorkingDirectory + @"\Temp"; } }

        public string CompositionDirectory { get { return WorkingDirectory + @"\Composition"; } }

        public string InputDirectory { get { return WorkingDirectory + @"\Input"; } }

        public string OutputDirectory { get { return WorkingDirectory + @"\Output"; } }

        public string LogDirectory { get { return WorkingDirectory; } }

        private FileServiceClient fileService = new FileServiceClient();
        private TaskServiceClient taskService = new TaskServiceClient();
        private Logger logger;
        private Dictionary<CompositionModel, string> mainLibraryMapping = new Dictionary<CompositionModel, string>();
        private Dictionary<string, string> remotePathMapping = new Dictionary<string, string>();

        public WorkspaceManager(string taskGuid, string workingDirectory)
        {
            TaskGuid = taskGuid;
            WorkingDirectory = workingDirectory;
            if (!Directory.Exists(WorkingDirectory)) Directory.CreateDirectory(WorkingDirectory);
            if (!Directory.Exists(TempDirectory)) Directory.CreateDirectory(TempDirectory);
            if (!Directory.Exists(CompositionDirectory)) Directory.CreateDirectory(CompositionDirectory);
            if (!Directory.Exists(InputDirectory)) Directory.CreateDirectory(InputDirectory);
            if (!Directory.Exists(OutputDirectory)) Directory.CreateDirectory(OutputDirectory);
            if (!Directory.Exists(LogDirectory)) Directory.CreateDirectory(LogDirectory);

            logger = new Logger(LogDirectory + @"\workspace.log");
            logger.Info("Workspace created.");
        }

        public string GetCompositionModelDirectory(CompositionModel compositionModel)
        {
            return CompositionDirectory + @"\" + compositionModel.guid;
        }

        public string GetCompositionModelMainLibrary(CompositionModel compositionModel)
        {
            if (!mainLibraryMapping.ContainsKey(compositionModel))
            {
                throw new Exception("Composition model not found.");
            }
            return mainLibraryMapping[compositionModel];
        }

        public string GetLocalPath(string remotePath)
        {
            if (!remotePathMapping.ContainsKey(remotePath))
            {
                throw new Exception("File path mapping not found for " + remotePath);
            }
            return remotePathMapping[remotePath];
        }

        private void collectOutputInDirectory(string currentDirectory)
        {
            foreach (string directoryPath in Directory.GetDirectories(currentDirectory))
            {
                if (!directoryPath.StartsWith(OutputDirectory)) continue;
                collectOutputInDirectory(directoryPath);
            }
            foreach (string filePath in Directory.GetFiles(currentDirectory))
            {
                if (!filePath.StartsWith(OutputDirectory)) continue;
                string relativePath = filePath.Substring(OutputDirectory.Length + 1);
                string fileServicePath = taskService.GenerateTaskFileName(TaskGuid, TaskFileType.Output, relativePath);
                logger.Info("Uploading " + filePath + " to " + fileServicePath + "...");
                fileService.Put(fileServicePath, File.ReadAllBytes(filePath));
                taskService.AddTaskFileMapping(TaskGuid, fileServicePath, relativePath, TaskFileType.Output, true);
            }
        }

        public void CollectOutput()
        {
            collectOutputInDirectory(OutputDirectory);
        }

        public void DeployComposition(CompositionData compositionData, TaskFileMapping[] inputFiles)
        {
            logger.Info("Deploying composition...");
            foreach (CompositionModelData modelData in compositionData.Models)
            {
                string modelDirectory = GetCompositionModelDirectory(modelData.CompositionModel);
                if (!Directory.Exists(modelDirectory)) Directory.CreateDirectory(modelDirectory);
                logger.Info("Deploying model " + modelData.CompositionModel.guid + " to " + modelDirectory + "...");
                foreach (ModelFileMapping fileMapping in modelData.ModelFiles)
                {
                    if (fileMapping.isDocument) continue;
                    string realPath = modelDirectory + @"\" + fileMapping.relativePath;
                    string baseDir = Path.GetDirectoryName(realPath);
                    if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                    logger.Info("Deploying " + realPath + "...");
                    FileEntityResponse fileEntity = fileService.Get(fileMapping.fileName);
                    File.WriteAllBytes(realPath, fileEntity.Content);
                    remotePathMapping[fileMapping.fileName] = realPath;

                    if (fileMapping.isMainLibrary)
                    {
                        mainLibraryMapping[modelData.CompositionModel] = realPath;
                    }
                }
            }

            logger.Info("Deploying task input files...");
            foreach (TaskFileMapping fileMapping in inputFiles)
            {
                string realPath = InputDirectory + @"\" + fileMapping.relativePath;
                string baseDir = Path.GetDirectoryName(realPath);
                if (!Directory.Exists(baseDir)) Directory.CreateDirectory(baseDir);

                logger.Info("Deploying " + realPath + "...");
                FileEntityResponse fileEntity = fileService.Get(fileMapping.fileName);
                File.WriteAllBytes(realPath, fileEntity.Content);
                remotePathMapping[fileMapping.fileName] = realPath;
            }
            logger.Info("Composition successfully deployed.");
        }
    }
}
