using System;
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
    public interface ITaskProcessedDataService
    {
        [OperationContract]
        string CreateDataSet(string taskGuid, string cmGuid, string className, string name);

        [OperationContract]
        void RemoveDataSetByGuid(string dataSetGuid);

        [OperationContract]
        void InsertIntoDataSet(string dataSetGuid, string[] record);

        [OperationContract]
        void InsertMultipleIntoDataSet(string dataSetGuid, List<string[]> records);

        [OperationContract]
        List<string[]> QueryDataSet(string dataSetGuid, int start, int limit);

    }
}
