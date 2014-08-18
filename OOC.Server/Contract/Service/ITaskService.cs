<<<<<<< HEAD
﻿using System;
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
        string Create(string compositionGuid, int userId, string triggerInvokeTime);

        [OperationContract]
        void SyncComposition(string guid);

        [OperationContract]
        void UpdateState(string guid, TaskState state);

        [OperationContract]
        void ReportInstanceFault(string instanceName);

        [OperationContract]
        TaskAssignResponse AssignPendingTask(string instanceName);

        [OperationContract]
        Task QueryTaskByGuid(string guid);

        [OperationContract]
        ModelProgress QueryModelProgressByGuid(string guid);

        [OperationContract]
        TaskDataResponse QueryTaskDataByGuid(string guid);

        [OperationContract]
        List<Task> QueryTaskByUserId(int userId);

        [OperationContract]
        void AddTaskFileMapping(string guid, string fileName, string relativePath, TaskFileType type, bool isDownloadable);

        [OperationContract]
        string GenerateTaskFileName(string guid, TaskFileType type, string relativePath);

        [OperationContract]
        List<TaskFileMapping> QueryTaskFileMapping(string guid, TaskFileType type);

        [OperationContract]
        void UpdateModelProgress(string guid, ModelProgress modelProgress);

        [OperationContract]
        List<Task> GetTaskByCompositionAndUser(string compositionId,int userId);



    }
}
=======
﻿using System;
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
        string Create(string compositionGuid, int userId, string triggerInvokeTime);

        [OperationContract]
        void SyncComposition(string guid);

        [OperationContract]
        void UpdateState(string guid, TaskState state);

        [OperationContract]
        void ReportInstanceFault(string instanceName);

        [OperationContract]
        TaskAssignResponse AssignPendingTask(string instanceName);

        [OperationContract]
        Task QueryTaskByGuid(string guid);

        [OperationContract]
        ModelProgress QueryModelProgressByGuid(string guid);

        [OperationContract]
        TaskDataResponse QueryTaskDataByGuid(string guid);

        [OperationContract]
        List<Task> QueryTaskByUserId(int userId);

        [OperationContract]
        void AddTaskFileMapping(string guid, string fileName, string relativePath, TaskFileType type, bool isDownloadable);

        [OperationContract]
        string GenerateTaskFileName(string guid, TaskFileType type, string relativePath);

        [OperationContract]
        List<TaskFileMapping> QueryTaskFileMapping(string guid, TaskFileType type);

        [OperationContract]
        void UpdateModelProgress(string guid, ModelProgress modelProgress);
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
