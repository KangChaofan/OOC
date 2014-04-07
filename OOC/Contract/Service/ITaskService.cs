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
        TaskAssignResponse AssignPendingTask(string instanceName);

        [OperationContract]
        TaskInfoResponse QueryTaskByGuid(string guid);

        [OperationContract]
        void AddTaskFileMapping(string guid, string fileName, string relativePath, TaskFileType type, bool isDownloadable);

        [OperationContract]
        string GenerateTaskFileName(string guid, TaskFileType type, string relativePath);

        [OperationContract]
        List<TaskFileMapping> QueryTaskFileMapping(string guid, TaskFileType type);
    }
}
