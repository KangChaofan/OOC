using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections;
using OOC.Contract.Data.Response;
using OOC.Contract.Data.Common;
using OOC.Contract.Service;
using OOC.Entity;
using OOC.ServiceAttribute;
using System.Configuration;

namespace OOC.Service
{
    [ExposedService("FileService")]
    public class FileService : IFileService
    {
        private readonly string fileRoot = ConfigurationManager.AppSettings["fileRepositoryRoot"];

        public FileEntityResponse Get(string fileName)
        {
            string realPath = Path.Combine(new string[] { fileRoot, fileName }).ToString();
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            return new FileEntityResponse(fileName, File.ReadAllBytes(realPath));
        }

        public void Put(string fileName, byte[] content)
        {
            string realPath = Path.Combine(new string[] { fileRoot, fileName }).ToString();
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            Directory.CreateDirectory(Path.GetDirectoryName(realPath));
            File.WriteAllBytes(realPath, content);
        }

        public List<FileDescription> List(string path)
        {
            string realPath = Path.Combine(new string[] { fileRoot, path }).ToString();
            if (!Directory.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("PATH_NOT_EXISTS");
            }
            string[] files = Directory.GetFiles(realPath);
            List<FileDescription> fileDescriptions = new List<FileDescription>();
            foreach (string file in files)
            {
                string fileName = file.Substring(file.LastIndexOf("\\") + 1);
                FileDescription desc = new FileDescription(fileName, new FileInfo(file));
                fileDescriptions.Add(desc);
            }
            return fileDescriptions;
        }

        public void Copy(string sourceFileName, string destFileName)
        {
            string srcRealPath = Path.Combine(new string[] { fileRoot, sourceFileName }).ToString();
            string dstRealPath = Path.Combine(new string[] { fileRoot, destFileName }).ToString();
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
            string realPath = Path.Combine(new string[] { fileRoot, fileName }).ToString();
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            FileInfo info = new FileInfo(realPath);
            return new FileDescription(fileName, info);
        }

        public void Delete(string path)
        {
            string realPath = Path.Combine(new string[] { fileRoot, path }).ToString();
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
            string realPath = Path.Combine(new string[] { fileRoot, path }).ToString();
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            Directory.CreateDirectory(realPath);
        }
    }
}
