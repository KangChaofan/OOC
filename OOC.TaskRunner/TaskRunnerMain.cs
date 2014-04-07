using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOC.TaskRunner
{
    class TaskRunnerMain
    {
        static void Main(string[] args)
        {
            string pipeName = null;
            string logLocation = null;
            for (int i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "-p":
                    case "--pipeName":
                        pipeName = args[++i];
                        break;
                    case "-l":
                    case "--log":
                        logLocation = args[++i];
                        break;
                }
            }
            if (pipeName == null) return;
            TaskRunner taskRunner = new TaskRunner(pipeName);
            taskRunner.LogLocation = logLocation;
            taskRunner.Run();
        }
    }
}
