using System;
using System.IO;
using System.Runtime.Serialization;
using OOC.Util;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class FileSystemDescription
    {
        public FileSystemDescription()
        {
        }

        public FileSystemDescription(string fileName, FileSystemInfo info)
        {
            Name = fileName;
            if (info is DirectoryInfo)
            {
                IsDirectory = true;
                Size = 0x1000;
            }
            else if (info is FileInfo)
            {
                IsDirectory = false;
                Size = info.CastTo<FileInfo>().Length;
            }
            CreateTime = info.CreationTime;
            AccessTime = info.LastAccessTime;
            ModifyTime = info.LastWriteTime;
        }

        [DataMember]
        public bool IsDirectory { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public long Size { get; set; }

        [DataMember]
        public DateTime CreateTime { get; set; }

        [DataMember]
        public DateTime AccessTime { get; set; }

        [DataMember]
        public DateTime ModifyTime { get; set; }
    }
}