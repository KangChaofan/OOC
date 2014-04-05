using System.Linq;
using System.ServiceModel;
using OOC.Entity;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.Util;
using OOC.ServiceAttribute;

namespace OOC.Service
{
    [ExposedService("UserService")]
    public class UserService : IUserService
    {
        public string Hash(string password)
        {
            return HashUtil.MD5Hash(password);
        }

        public bool Auth(string username, string password)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<User> result = from o in db.User
                                          where o.username == username && o.passhash == Hash(password)
                                          select o;
                return result.Any();
            }
        }

        public void Create(User user)
        {
            using (OOCEntities db = new OOCEntities())
            {
                try
                {
                    db.User.AddObject(user);
                    db.SaveChanges();
                }
                catch
                {
                    throw new FaultException("TRANSACTION_FAILED");
                }
            }
        }

        public User GetByUsername(string Username)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<User> result = from o in db.User
                                          where o.username == Username
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("USER_NOT_EXISTS");
                }
                return result.First();
            }
        }


        public User GetById(int id)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<User> result = from o in db.User
                                          where o.id == id
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("USER_NOT_EXISTS");
                }
                return result.First();
            }
        }
    }
}