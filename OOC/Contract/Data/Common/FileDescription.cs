using System;
using System.IO;
using System.Runtime.Serialization;

namespace OOC.Contract.Data.Common
{
    [DataContract]
    public class FileDescription
    {
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

        public FileDescription() { }

        public FileDescription(string fileName, FileInfo info)
        {
            FileName = fileName;
            Size = info.Length;
            CreateTime = info.CreationTime;
            AccessTime = info.LastAccessTime;
            ModifyTime = info.LastWriteTime;
        }
    }
}