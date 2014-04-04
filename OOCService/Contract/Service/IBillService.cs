using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Response;

namespace OOC.Contract.Service
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
