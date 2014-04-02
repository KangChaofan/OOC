using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.IO;
using System.Collections;
using OOC.Response;
using OOC.ORM;
using OOC.Util;

namespace OOC.Service
{
    public class UserService : IUserService
    {
        private oocEntities db = new oocEntities();

        public HashResponse Hash(string password)
        {
            return new HashResponse(HashUtil.MD5Hash(password));
        }

        public GenericResponse Auth(string username, string password)
        {
            var result = from o in db.User
                         where o.username == username && o.passhash == Hash(password).Hash
                         select o;
            if (result.Count() > 0)
            {
                return new GenericResponse(true);
            }
            else
            {
                return new GenericResponse(false, 1, "USER_NOT_FOUND");
            }
        }

        public GenericResponse Create(User user)
        {
            try
            {
                db.User.AddObject(user);
                db.SaveChanges();
                return new GenericResponse(true);
            }
            catch
            {
                return new GenericResponse(false, 1, "INSERT_FAILED");
            }
        }

        public UserInfoResponse GetByUsername(string Username)
        {
            var result = from o in db.User
                         where o.username == Username
                         select o;
            if (result.Count() > 0)
            {
                return new UserInfoResponse(result.First());
            }
            else
            {
                return new UserInfoResponse(false, 1, "USER_NOT_FOUND");
            }
        }


        public UserInfoResponse GetById(int id)
        {
            var result = from o in db.User
                         where o.id == id
                         select o;
            if (result.Count() > 0)
            {
                return new UserInfoResponse(result.First());
            }
            else
            {
                return new UserInfoResponse(false, 1, "USER_NOT_FOUND");
            }
        }
    }
}
