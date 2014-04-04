using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Contract.Data.Response;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IFileService
    {
        [OperationContract]
        FileStatResponse Stat(string fileName);

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

        [OperationContract]
        GenericResponse CreateDirectory(string path);

    }

}
