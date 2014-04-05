using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;

namespace OOC.TaskRunner
{
    public class TaskRunner
    {
        public string PipeName { get; set; }

        private NamedPipeClientStream pipeClient;

        public TaskRunner(string pipeName)
        {
            PipeName = pipeName;
        }

        public void Run()
        {
            pipeClient = new NamedPipeClientStream(".", PipeName,
                           PipeDirection.InOut, PipeOptions.None,
                           TokenImpersonationLevel.Impersonation);
            pipeClient.Connect();
            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }
}
