using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OOC.Response
{
    [DataContract]
    public class FileEntityResponse : GenericResponse
    {
        string fileName;
        byte[] content;

        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [DataMember]
        public byte[] Content
        {
            get { return content; }
            set { content = value; }
        }

        public FileEntityResponse(string fileName, byte[] content)
            : base(true)
        {
            FileName = fileName;
            Content = content;
        }

        public FileEntityResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }

    [DataContract]
    public class FileDescription
    {
        string fileName;
        int size;

        [DataMember]
        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        [DataMember]
        public int Size
        {
            get { return size; }
            set { size = value; }
        }

    }

    [DataContract]
    public class FileListResponse : GenericResponse
    {
        FileDescription[] files;

        [DataMember]
        public FileDescription[] Files
        {
            get { return files; }
            set { files = value; }
        }

        public FileListResponse(FileDescription[] files)
            : base(true)
        {
            Files = files;
        }

        public FileListResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}