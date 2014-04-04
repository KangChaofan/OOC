using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOC.Instance.TaskService;

namespace OOC.Instance
{
    class InstanceMain
    {
        static void Main(string[] args)
        {
            TaskServiceClient cli = new TaskServiceClient();
            TaskInfoResponse resp = cli.QueryTaskByGuid("111");
            Console.ReadLine();
        }
    }
}
