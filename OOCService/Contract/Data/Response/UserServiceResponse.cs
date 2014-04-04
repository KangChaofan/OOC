using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using OOC.ORM;

namespace OOC.Contract.Data.Response
{
    [DataContract]
    public class UserInfoResponse : GenericResponse
    {
        [DataMember]
        public User User { get; set; }

        public UserInfoResponse(User user)
            : base(true)
        {
            User = user;
        }

        public UserInfoResponse(bool success, int errorCode, string errorInfo)
            : base(success, errorCode, errorInfo)
        {
        }
    }
}