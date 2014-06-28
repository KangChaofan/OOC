using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Response;
using OOC.Entity;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IModelTypeService
    {
        [OperationContract]
        List<ModelType> GetTypeList();

        [OperationContract]
        System.Data.DataSet GetTypeListDS();

        [OperationContract]
        List<ModelType> GetTopList();

        [OperationContract]
        List<ModelType> GetSubByTopID(Int32 TopID);

        [OperationContract]
        bool IsTopType(Int32 TypeID);

        [OperationContract]
        ModelType GetTypeByID(Int32 ID);
    }
}
