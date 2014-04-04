using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.ORM;
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

        private readonly oocEntities db = new oocEntities();

        public TaskCreateResponse Create(string compositionGuid, int userId)
        {
            Composition composition = (from o in db.Composition
                                       where o.guid == compositionGuid
                                       select o).First();
            Task task = new Task()
            {
                guid = GuidUtil.newGuid(),
                compositionGuid = compositionGuid,
                compositionData = new CompositionData(composition).Serialized,
                state = (sbyte)TaskState.PENDING,
                userId = userId,
                modelProgress = new ModelProgress().Serialized
            };
            db.Task.AddObject(task);
            db.SaveChanges();
            return new TaskCreateResponse(task.guid);
        }

        public GenericResponse UpdateState(string guid, TaskState state)
        {
            IQueryable<Task> result = from o in db.Task
                                      where o.guid == guid
                                      select o;
            if (result.Any())
            {
                result.First().state = (sbyte)state;
                db.SaveChanges();
                return new GenericResponse(true);
            }
            else
            {
                return new GenericResponse(false, 1, "TASK_NOT_FOUND");
            }
        }

        public TaskInfoResponse AssignPendingTask(string instanceName)
        {
            lock (assigningLock)
            {
                IQueryable<Task> result = from o in db.Task
                                          where o.state == (sbyte)TaskState.WAITING
                                          select o;
                if (result.Any())
                {
                    Task task = result.First();
                    task.state = (sbyte)TaskState.RUNNING;
                    task.instanceName = instanceName;
                    db.SaveChanges();
                    return new TaskInfoResponse(result.First());
                }
                else
                {
                    return new TaskInfoResponse(false, 1, "TASK_NOT_FOUND");
                }
            }
        }

        public TaskInfoResponse QueryTaskByGuid(string guid)
        {
            IQueryable<Task> result = from o in db.Task
                                      where o.guid == guid
                                      select o;
            if (result.Any())
            {
                return new TaskInfoResponse(result.First());
            }
            else
            {
                return new TaskInfoResponse(false, 1, new ModelProgress().Serialized);
            }
        }
    }
}