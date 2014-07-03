using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.ServiceModel;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

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
            return new FileEntityResponse(fileName, IOUtil.ReadAllBytes(realPath));
        }

        public void Put(string fileName, byte[] content)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            string dirName = Path.GetDirectoryName(realPath);
            if (dirName != null) Directory.CreateDirectory(dirName);
            IOUtil.WriteAllBytes(realPath, content);
        }

        public List<FileSystemDescription> List(string path)
        {
            string realPath = Path.Combine(new[] {fileRoot, path});
            if (!Directory.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("PATH_NOT_EXISTS");
            }

            string[] files = Directory.GetFiles(realPath);

            //add files in the path.
            List<FileSystemDescription> fileDescriptions =
                (from file in files
                 let fileName = Path.Combine(path, Path.GetFileName(file))
                 select new FileSystemDescription(fileName, new FileInfo(file))).ToList();

            //add directories in the path.
            string[] directories = Directory.GetDirectories(realPath);
            fileDescriptions.AddRange(
                directories.Select(Path.GetFileNameWithoutExtension)
                           .Select(
                               dirName =>
                               new FileSystemDescription(Path.Combine(path, dirName), new DirectoryInfo(dirName))));

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

        public FileSystemDescription Stat(string fileName)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("PERMISSION_DENIED");
            }
            if (File.Exists(realPath))
            {
                return new FileSystemDescription(fileName, new FileInfo(realPath));
            }
            if (Directory.Exists(realPath))
            {
                return new FileSystemDescription(fileName, new DirectoryInfo(realPath));
            }
            throw new FaultException("FILE_NOT_EXISTS");
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

        public FileChunkResponse Read(string fileName, long offset, long length)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            byte[] chunk = IOUtil.ReadChunk(realPath, offset, length);
            return new FileChunkResponse(fileName, offset, chunk.Length, chunk);
        }

        public void Append(string fileName, byte[] chunk)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!realPath.StartsWith(fileRoot))
            {
                throw new FaultException("ACCESS_DENIED");
            }
            string dirName = Path.GetDirectoryName(realPath);
            if (dirName != null) Directory.CreateDirectory(dirName);
            IOUtil.AppendAllBytes(realPath, chunk);
        }

        public string Head(string fileName, int lines)
        {
            string realPath = Path.Combine(new[] {fileRoot, fileName});
            if (!File.Exists(realPath) || !realPath.StartsWith(fileRoot))
            {
                throw new FaultException("FILE_NOT_EXISTS");
            }
            return IOUtil.ReadLines(realPath, lines);
        }
    }
}