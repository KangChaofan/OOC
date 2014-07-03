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
        List<TaskProcessedDataSet> GetDataSetByTaskGuid(string taskGuid);

        [OperationContract]
        void InsertIntoDataSet(string dataSetGuid, string[] record);

        [OperationContract]
        void InsertMultipleIntoDataSet(string dataSetGuid, List<string[]> records);

        [OperationContract]
        List<string[]> QueryDataSetRecords(string dataSetGuid, int start, int limit);

        [OperationContract]
        List<TaskProcessedDataRecord> QueryDataSet(string dataSetGuid, int start, int limit);

    }
}
