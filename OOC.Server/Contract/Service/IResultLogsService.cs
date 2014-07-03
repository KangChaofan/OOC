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
    public interface IResultLogsService
    {
        [OperationContract]
        void AddOne(ResultLogs ModelResultLog);

        [OperationContract]
        ResultLogs GetModelOne(string ID);

        [OperationContract]
        List<ResultLogs> GetLogsList();

    }
}
