using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Contract.Data.Response;
using OOC.Contract.Data.Common;

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
        void Delete(string path);

        [OperationContract]
        void Put(string fileName, byte[] content);

        [OperationContract]
        void Copy(string sourceFileName, string destFileName);

        [OperationContract]
        List<FileDescription> List(string path);

        [OperationContract]
        void CreateDirectory(string path);

    }

}
