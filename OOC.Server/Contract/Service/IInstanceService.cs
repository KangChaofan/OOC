<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IInstanceService
    {
        [OperationContract]
        void Heartbeat(InstanceHeartbeatStatus status);

        [OperationContract]
        InstanceStatus QueryStatusByInstanceName(string instanceName);

        [OperationContract]
        double GetLoadFactor();

    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using OOC.Contract.Data.Common;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IInstanceService
    {
        [OperationContract]
        void Heartbeat(InstanceHeartbeatStatus status);

        [OperationContract]
        InstanceStatus QueryStatusByInstanceName(string instanceName);

        [OperationContract]
        double GetLoadFactor();

    }
}
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
