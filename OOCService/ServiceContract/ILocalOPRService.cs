using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Response;

namespace OOC.Service
{
    [ServiceContract]
    public interface ILocalOPRService
    {
        [OperationContract]
        SimulationRuntimeResult RunOpr(string oprFilename, bool verboseOff);
    }

    [DataContract]
    public class SimulationRuntimeResult
    {
        int exitCode = -1;
        string error = null;

        [DataMember]
        public int ExitCode
        {
            get { return exitCode; }
            set { exitCode = value; }
        }

        [DataMember]
        public string Error
        {
            get { return error; }
            set { error = value; }
        }
    }
}
