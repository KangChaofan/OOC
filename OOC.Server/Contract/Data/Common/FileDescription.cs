using System;
using System.IO;
using System.Runtime.Serialization;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class FileDescription
    {
        public FileDescription()
        {
        }

        public FileDescription(string fileName, FileInfo info)
            : this(fileName, info, false)
        {
        }

        public FileDescription(string fileName, FileInfo info, bool isDirecotry)
        {
            FileName = fileName;
            IsDirectory = isDirecotry;
            if (isDirecotry)
            {
                Size = -1;
            }
            else
            {
                Size = info.Length;
            }
            CreateTime = info.CreationTime;
            AccessTime = info.LastAccessTime;
            ModifyTime = info.LastWriteTime;
        }

        [DataMember]
        public bool IsDirectory { get; set; }

        [DataMember]
        public string FileName { get; set; }

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