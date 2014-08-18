<<<<<<< HEAD
﻿using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

namespace OOC.Service
{
    [ExposedService("TaskService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TaskService : ITaskService
    {
        private static readonly object assigningLock = new object();

        public void SyncCompositionInputFiles(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                Task task = QueryTaskByGuid(guid);
                CompositionService compositionService = new CompositionService();
                List<string> fileNames = compositionService.GetInputFileNames(task.compositionGuid);
                foreach (string fileName in fileNames)
                {
                    IQueryable<TaskFileMapping> result = from o in db.TaskFileMapping
                                                         where o.taskGuid == guid && o.fileName == fileName
                                                         select o;
                    if (!result.Any())
                    {
                        AddTaskFileMapping(guid, fileName, GuidUtil.newGuid() + ".in", TaskFileType.Input, false);
                    }
                }
            }
        }

        public string Create(string compositionGuid, int userId, string triggerInvokeTime)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Composition> result = from o in db.Composition
                                                 where o.guid == compositionGuid
                                                 select o;
                if (!result.Any())
                {
                    throw new FaultException("COMPOSITION_NOT_EXISTS");
                }
                Composition composition = result.First();
                CompositionData compositionData = new CompositionData(composition);
                Task task = new Task()
                {
                    guid = GuidUtil.newGuid(),
                    compositionGuid = compositionGuid,
                    compositionData = compositionData.Serialized,
                    state = (sbyte)TaskState.Created,
                    userId = userId,
                    triggerInvokeTime = triggerInvokeTime,
                    modelProgress = new ModelProgress().Serialized
                };
                db.Task.AddObject(task);
                db.SaveChanges();
                SyncCompositionInputFiles(task.guid);
                return task.guid;
            }
        }

        public void UpdateState(string guid, TaskState state)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                task.state = (sbyte)state;
                if (state == TaskState.Running)
                {
                    task.timeStarted = System.DateTime.Now;
                }
                if (state == TaskState.Completed)
                {
                    task.timeFinished = System.DateTime.Now;
                }
                db.SaveChanges();
            }
        }

        public TaskAssignResponse AssignPendingTask(string instanceName)
        {
            lock (assigningLock)
            {
                using (OOCEntities db = new OOCEntities())
                {
                    IQueryable<Task> result = from o in db.Task
                                              where o.state == (sbyte)TaskState.Ready
                                              select o;
                    if (!result.Any())
                    {
                        throw new FaultException("NO_TASK_AVAILABLE");
                    }
                    Task task = result.First();
                    task.state = (sbyte)TaskState.Assigned;
                    task.instanceName = instanceName;
                    db.SaveChanges();
                    return new TaskAssignResponse(task, QueryTaskFileMapping(task.guid, TaskFileType.Input), task.triggerInvokeTime);
                }
            }
        }

        public TaskDataResponse QueryTaskDataByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return new TaskDataResponse(result.First());
            }
        }

        public List<Task> QueryTaskByUserId(int userId)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.userId == userId
                                          select o;
                return result.ToList();
            }
        }

        public void AddTaskFileMapping(string guid, string fileName, string relativePath, TaskFileType type, bool isDownloadable)
        {
            using (OOCEntities db = new OOCEntities())
            {
                TaskFileMapping taskFileMapping = new TaskFileMapping()
                {
                    taskGuid = guid,
                    fileName = fileName,
                    relativePath = relativePath,
                    type = (sbyte)type,
                    isDownloadable = isDownloadable
                };
                try
                {
                    db.TaskFileMapping.AddObject(taskFileMapping);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public List<TaskFileMapping> QueryTaskFileMapping(string guid, TaskFileType type)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<TaskFileMapping> result = from o in db.TaskFileMapping
                                                     where o.taskGuid == guid && o.type == (sbyte)type
                                                     select o;
                return result.ToList();
            }
        }

        public string GenerateTaskFileName(string guid, TaskFileType type, string relativePath)
        {
            return @"Tasks\" + guid + @"\" + type + @"\" + relativePath;
        }


        public void UpdateModelProgress(string guid, ModelProgress modelProgress)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                task.modelProgress = modelProgress.Serialized;
                db.SaveChanges();
            }
        }


        public void SyncComposition(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                CompositionData compositionData = new CompositionData(task.Composition);
                task.compositionData = compositionData.Serialized;
                db.SaveChanges();
                SyncCompositionInputFiles(guid);
            }
        }

        public Task QueryTaskByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return result.First();
            }
        }

        public ModelProgress QueryModelProgressByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return new ModelProgress() { Serialized = result.First().modelProgress };
            }
        }

        public void ReportInstanceFault(string instanceName)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.instanceName == instanceName && o.state >= (sbyte)TaskState.Assigned && o.state <= (sbyte)TaskState.Finishing
                                          select o;
                foreach (Task task in result)
                {
                    task.state = (sbyte)TaskState.Aborted;
                }
                db.SaveChanges();
            }
        }

        public List<Task> GetTaskByCompositionAndUser(string compositionGuid, int userId) {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<Task> result = from o in db.Task
                                          where o.compositionGuid == compositionGuid && o.userId==userId
                                          select o;
                return result.ToList();
            }
        
        }
    }
}
=======
﻿using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

namespace OOC.Service
{
    [ExposedService("TaskService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TaskService : ITaskService
    {
        private static readonly object assigningLock = new object();

        public void SyncCompositionInputFiles(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                Task task = QueryTaskByGuid(guid);
                CompositionService compositionService = new CompositionService();
                List<string> fileNames = compositionService.GetInputFileNames(task.compositionGuid);
                foreach (string fileName in fileNames)
                {
                    IQueryable<TaskFileMapping> result = from o in db.TaskFileMapping
                                                         where o.taskGuid == guid && o.fileName == fileName
                                                         select o;
                    if (!result.Any())
                    {
                        AddTaskFileMapping(guid, fileName, GuidUtil.newGuid() + ".in", TaskFileType.Input, false);
                    }
                }
            }
        }

        public string Create(string compositionGuid, int userId, string triggerInvokeTime)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Composition> result = from o in db.Composition
                                                 where o.guid == compositionGuid
                                                 select o;
                if (!result.Any())
                {
                    throw new FaultException("COMPOSITION_NOT_EXISTS");
                }
                Composition composition = result.First();
                CompositionData compositionData = new CompositionData(composition);
                Task task = new Task()
                {
                    guid = GuidUtil.newGuid(),
                    compositionGuid = compositionGuid,
                    compositionData = compositionData.Serialized,
                    state = (sbyte)TaskState.Created,
                    userId = userId,
                    triggerInvokeTime = triggerInvokeTime,
                    modelProgress = new ModelProgress().Serialized
                };
                db.Task.AddObject(task);
                db.SaveChanges();
                SyncCompositionInputFiles(task.guid);
                return task.guid;
            }
        }

        public void UpdateState(string guid, TaskState state)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                task.state = (sbyte)state;
                if (state == TaskState.Running)
                {
                    task.timeStarted = System.DateTime.Now;
                }
                if (state == TaskState.Completed)
                {
                    task.timeFinished = System.DateTime.Now;
                }
                db.SaveChanges();
            }
        }

        public TaskAssignResponse AssignPendingTask(string instanceName)
        {
            lock (assigningLock)
            {
                using (OOCEntities db = new OOCEntities())
                {
                    IQueryable<Task> result = from o in db.Task
                                              where o.state == (sbyte)TaskState.Ready
                                              select o;
                    if (!result.Any())
                    {
                        throw new FaultException("NO_TASK_AVAILABLE");
                    }
                    Task task = result.First();
                    task.state = (sbyte)TaskState.Assigned;
                    task.instanceName = instanceName;
                    db.SaveChanges();
                    return new TaskAssignResponse(task, QueryTaskFileMapping(task.guid, TaskFileType.Input), task.triggerInvokeTime);
                }
            }
        }

        public TaskDataResponse QueryTaskDataByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return new TaskDataResponse(result.First());
            }
        }

        public List<Task> QueryTaskByUserId(int userId)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.userId == userId
                                          select o;
                return result.ToList();
            }
        }

        public void AddTaskFileMapping(string guid, string fileName, string relativePath, TaskFileType type, bool isDownloadable)
        {
            using (OOCEntities db = new OOCEntities())
            {
                TaskFileMapping taskFileMapping = new TaskFileMapping()
                {
                    taskGuid = guid,
                    fileName = fileName,
                    relativePath = relativePath,
                    type = (sbyte)type,
                    isDownloadable = isDownloadable
                };
                try
                {
                    db.TaskFileMapping.AddObject(taskFileMapping);
                    db.SaveChanges();
                }
                catch { }
            }
        }

        public List<TaskFileMapping> QueryTaskFileMapping(string guid, TaskFileType type)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<TaskFileMapping> result = from o in db.TaskFileMapping
                                                     where o.taskGuid == guid && o.type == (sbyte)type
                                                     select o;
                return result.ToList();
            }
        }

        public string GenerateTaskFileName(string guid, TaskFileType type, string relativePath)
        {
            return @"Tasks\" + guid + @"\" + type + @"\" + relativePath;
        }


        public void UpdateModelProgress(string guid, ModelProgress modelProgress)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                task.modelProgress = modelProgress.Serialized;
                db.SaveChanges();
            }
        }


        public void SyncComposition(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                Task task = result.First();
                CompositionData compositionData = new CompositionData(task.Composition);
                task.compositionData = compositionData.Serialized;
                db.SaveChanges();
                SyncCompositionInputFiles(guid);
            }
        }

        public Task QueryTaskByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return result.First();
            }
        }

        public ModelProgress QueryModelProgressByGuid(string guid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return new ModelProgress() { Serialized = result.First().modelProgress };
            }
        }

        public void ReportInstanceFault(string instanceName)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.instanceName == instanceName && o.state >= (sbyte)TaskState.Assigned && o.state <= (sbyte)TaskState.Finishing
                                          select o;
                foreach (Task task in result)
                {
                    task.state = (sbyte)TaskState.Aborted;
                }
                db.SaveChanges();
            }
        }
    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
