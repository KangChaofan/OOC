using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;

namespace OOC.Service
{
    [ExposedService("FileService")]
    public class FileService : IFileService
    {
        private readonly string fileRoot = ConfigurationManager.AppSettings["fileRepositoryRoot"];

        public FileEntityResponse Get(string fileName)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            return new FileEntityResponse(fileName, File.ReadAllBytes(realPath));
        }

        public void Put(string fileName, byte[] content)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            Directory.CreateDirectory(Path.GetDirectoryName(realPath));
            File.WriteAllBytes(realPath, content);
        }

        public List<FileDescription> List(string path)
        {
            string realPath = Path.Combine(new[] {fileRoot, path});
            if (!Directory.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("PATH_NOT_EXISTS");
            }
            var fileDescriptions = new List<FileDescription>();

            string[] files = Directory.GetFiles(realPath);
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                var desc = new FileDescription(fileName, new FileInfo(file), false);
                fileDescriptions.Add(desc);
            }

            string[] directories = Directory.GetDirectories(realPath);
            fileDescriptions.AddRange(
                directories.Select(Path.GetFileNameWithoutExtension)
                           .Select(dirName => new FileDescription(dirName, new FileInfo(dirName), true)));

            return fileDescriptions;
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            string srcRealPath = Path.Combine(new[] {fileRoot, sourceFileName});
            string dstRealPath = Path.Combine(new[] {fileRoot, destFileName});
            if (!File.Exists(srcRealPath) || !srcRealPath.StartsWith(fileRoot))
            {
                throw new FaultException("SRC_FILE_NOT_EXISTS");
            }
            if (File.Exists(dstRealPath) || !dstRealPath.StartsWith(fileRoot))
            {
                throw new FaultException("DST_FILE_ALREADY_EXISTED");
            }
            File.Copy(srcRealPath, dstRealPath);
        }

        public FileDescription Stat(string fileName)
        {
            //TODO complete me!
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            var info = new FileInfo(realPath);
            return new FileDescription(fileName, info);
        }

        public void Delete(string path)
        {
            string realPath = Path.Combine(new[] {fileRoot, path});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            if (Directory.Exists(realPath))
            {
                Directory.Delete(realPath, true);
                return;
            }
            if (File.Exists(realPath))
            {
                File.Delete(realPath);
                return;
            }
            throw new FaultException("PATH_NOT_EXISTS");
        }

        public void CreateDirectory(string path)
        {
            string realPath = Path.Combine(new[] {fileRoot, path});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            Directory.CreateDirectory(realPath);
        }
    }
}