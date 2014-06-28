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
    public interface IOutputParameterService
    {
        [OperationContract]
        void Create(string taskGuid, string compositionGuid, string compositionModelGuid, string elementSet, string quantity, string parameterValue, System.DateTime creation);

        [OperationContract]
        List<OutputParameter> GetByGuid(string guid);

        [OperationContract]
        List<OutputParameter> GetByTaskGuid(string taskGuid);

        [OperationContract]
        List<OutputParameter> GetByCompGuid(string compGuid);

        [OperationContract]
        List<OutputParameter> GetByTime(DateTime startTime,DateTime overTime);

        [OperationContract]
        List<OutputParameter> GetByCmGuid(string cmGuid);

        [OperationContract]
        List<OutputParameter> GetByGuidsAndTime(string taskGuid, string compGuid, string cmGuid, DateTime startTime, DateTime overTime); 
    }
}
