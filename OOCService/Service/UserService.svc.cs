using System.Linq;
using OOC.ORM;
using OOC.Response;
using OOC.Util;
using OOC.Candy;

namespace OOC.Service
{
    [ExposedService("UserService")]
    public class UserService : IUserService
    {
        private readonly oocEntities db = new oocEntities();

        public HashResponse Hash(string password)
        {
            return new HashResponse(HashUtil.MD5Hash(password));
        }

        public GenericResponse Auth(string username, string password)
        {
            IQueryable<User> result = from o in db.User
                                      where o.username == username && o.passhash == Hash(password).Hash
                                      select o;
            if (result.Any())
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
            IQueryable<User> result = from o in db.User
                                      where o.username == Username
                                      select o;
            if (result.Any())
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
            IQueryable<User> result = from o in db.User
                                      where o.id == id
                                      select o;
            if (result.Any())
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