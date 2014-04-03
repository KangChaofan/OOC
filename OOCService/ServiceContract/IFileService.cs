using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Response;

namespace OOC.Service
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        FileEntityResponse Get(string fileName);

        [OperationContract]
        GenericResponse Delete(string fileName);

        [OperationContract]
        GenericResponse Put(string fileName, byte[] content);

        [OperationContract]
        GenericResponse Copy(string sourceFileName, string destFileName);

        [OperationContract]
        FileListResponse List(string path);
    }

}
