using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Entity;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface ITaskService
    {
        [OperationContract]
        string Create(string compositionGuid, int userId);

        [OperationContract]
        void UpdateState(string guid, TaskState state);

        [OperationContract]
        TaskInfoResponse AssignPendingTask(string instanceName);

        [OperationContract]
        TaskInfoResponse QueryTaskByGuid(string guid);

    }
}
