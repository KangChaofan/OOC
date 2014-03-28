using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.Response;
using OOC.ORM;

namespace OOC.Service
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        HashResponse Hash(string password);

        [OperationContract]
        GenericResponse Auth(string username, string password);

        [OperationContract]
        GenericResponse Create(User user);

        [OperationContract]
        UserInfoResponse GetByUsername(string Username);
    }

}
