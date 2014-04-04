using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Contract.Data.Response;
using OOC.Entity;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IBillService
    {
        [OperationContract]
        void Create(int userId, string taskGuid, string cmGuid, double amount);

        [OperationContract]
        Bill GetById(int id);

        [OperationContract]
        List<Bill> GetByUserId(int userId);

        [OperationContract]
        List<Bill> GetByTaskGuid(string taskGuid);

        [OperationContract]
        List<Bill> GetByCmGuid(string cmGuid);

        [OperationContract]
        void Refund(int id);
    }
}
