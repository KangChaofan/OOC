using System.Linq;
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

        public string Create(string compositionGuid, int userId)
        {
            using (oocEntities db = new oocEntities())
            {
                IQueryable<Composition> result = from o in db.Composition
                                                 where o.guid == compositionGuid
                                                 select o;
                if (!result.Any())
                {
                    throw new FaultException("COMPOSITION_NOT_EXISTS");
                }
                Composition composition = result.First();
                Task task = new Task()
                {
                    guid = GuidUtil.newGuid(),
                    compositionGuid = compositionGuid,
                    compositionData = new CompositionData(composition).Serialized,
                    state = (sbyte)TaskState.PENDING,
                    userId = userId,
                    modelProgress = new ModelProgress().Serialized
                };
                try
                {
                    db.Task.AddObject(task);
                    db.SaveChanges();
                }
                catch
                {
                    throw new FaultException("TRANSACTION_FAILED");
                }
                return task.guid;
            }
        }

        public void UpdateState(string guid, TaskState state)
        {
            using (oocEntities db = new oocEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                result.First().state = (sbyte)state;
                db.SaveChanges();
            }
        }

        public TaskInfoResponse AssignPendingTask(string instanceName)
        {
            lock (assigningLock)
            {
                using (oocEntities db = new oocEntities())
                {
                    IQueryable<Task> result = from o in db.Task
                                              where o.state == (sbyte)TaskState.WAITING
                                              select o;
                    if (!result.Any())
                    {
                        throw new FaultException("TASK_NOT_EXISTS");
                    }
                    Task task = result.First();
                    task.state = (sbyte)TaskState.RUNNING;
                    task.instanceName = instanceName;
                    db.SaveChanges();
                    return new TaskInfoResponse(result.First());
                }
            }
        }

        public TaskInfoResponse QueryTaskByGuid(string guid)
        {
            using (oocEntities db = new oocEntities())
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.guid == guid
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("TASK_NOT_EXISTS");
                }
                return new TaskInfoResponse(result.First());
            }
        }
    }
}