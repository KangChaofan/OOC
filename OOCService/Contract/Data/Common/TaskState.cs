using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.Contract.Data.Common
{
    public enum TaskState
    {
        /* task is created but not ready to run */
        PENDING = 0,
        /* task is waiting for assignment */
        WAITING = 1,
        /* task is assigned and being initialized */
        ASSIGNED = 2,
        /* task is running on instance */
        RUNNING = 3,
        /* task is aborted or killed due to exception */
        KILLED = 4,
        /* task is finished successfully */
        FINISHED = 5
    }
}