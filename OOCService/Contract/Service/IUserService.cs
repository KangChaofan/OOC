using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Contract.Data.Response;
using OOC.Entity;

namespace OOC.Contract.Service
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string Hash(string password);

        [OperationContract]
        bool Auth(string username, string password);

        [OperationContract]
        void Create(User user);

        [OperationContract]
        User GetByUsername(string Username);

        [OperationContract]
        User GetById(int id);
    }

}
