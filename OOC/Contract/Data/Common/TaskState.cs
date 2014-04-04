using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OOC.Contract.Data.Common
{
    public enum TaskState
    {
        /* task is created but not ready to run */
        Created = 0,
        /* task is waiting for assignment */
        Ready = 1,
        /* task is assigned and being initialized */
        Assigned = 2,
        /* task is running on instance */
        Running = 3,
        /* task is aborted or killed due to exception */
        Aborted = 4,
        /* task is finished successfully */
        Completed = 5
    }
}