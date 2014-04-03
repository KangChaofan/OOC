using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Response;

namespace OOC.Service
{
    [ServiceContract]
    public interface IBillService
    {
        [OperationContract]
        GenericResponse Create(int userId, string taskGuid, string cmGuid, double amount);

        [OperationContract]
        BillResponse GetById(int id);

        [OperationContract]
        BillListResponse GetByUserId(int userId);

        [OperationContract]
        BillListResponse GetByTaskGuid(string taskGuid);

        [OperationContract]
        BillListResponse GetByCmGuid(string cmGuid);

        [OperationContract]
        GenericResponse Refund(int id);
    }
}
