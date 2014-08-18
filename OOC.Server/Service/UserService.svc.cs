<<<<<<< HEAD
﻿using System.Linq;
using System.ServiceModel;
using System.Collections;
using System.Collections.Generic;
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
                string passhash = Hash(password);
                IQueryable<User> result = from o in db.User
                                          where o.username == username && o.passhash == passhash
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

        public void UpdatePassword(int id, string password)
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
                User user = result.First();
                user.passhash = Hash(password);
                db.SaveChanges();
            }
        }

        public void UpdateMobile(int id, string mobile)
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
                User user = result.First();
                user.mobile = mobile;
                db.SaveChanges();
            }
        }

        public List<string> QueryAcl(int id)
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
                User user = result.First();
                return SerializationUtil.Deserialize<List<string>>(user.acl);
            }
        }

        public bool HasAclEntry(int id, string entry)
        {
            List<string> entries = QueryAcl(id);
            return entries.Contains(entry);
        }

        public void AddAclEntry(int id, string entry)
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
                User user = result.First();
                List<string> entries = SerializationUtil.Deserialize<List<string>>(user.acl);
                if (entries == null) entries = new List<string>();
                if (entries.Contains(entry))
                {
                    throw new FaultException("ENTRY_ALREADY_EXISTED");
                }
                entries.Add(entry);
                user.acl = SerializationUtil.Serialize(entries);
                db.SaveChanges();
            }
        }

        public void RemoveAclEntry(int id, string entry)
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
                User user = result.First();
                List<string> entries = SerializationUtil.Deserialize<List<string>>(user.acl);
                if (entries == null) entries = new List<string>();
                if (!entries.Contains(entry))
                {
                    throw new FaultException("ENTRY_NOT_EXISTS");
                }
                entries.Remove(entry);
                user.acl = SerializationUtil.Serialize(entries);
                db.SaveChanges();
            }
        }
    }
=======
﻿using System.Linq;
using System.ServiceModel;
using System.Collections;
using System.Collections.Generic;
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
                string passhash = Hash(password);
                IQueryable<User> result = from o in db.User
                                          where o.username == username && o.passhash == passhash
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

        public void UpdatePassword(int id, string password)
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
                User user = result.First();
                user.passhash = Hash(password);
                db.SaveChanges();
            }
        }

        public void UpdateMobile(int id, string mobile)
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
                User user = result.First();
                user.mobile = mobile;
                db.SaveChanges();
            }
        }

        public List<string> QueryAcl(int id)
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
                User user = result.First();
                return SerializationUtil.Deserialize<List<string>>(user.acl);
            }
        }

        public bool HasAclEntry(int id, string entry)
        {
            List<string> entries = QueryAcl(id);
            return entries.Contains(entry);
        }

        public void AddAclEntry(int id, string entry)
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
                User user = result.First();
                List<string> entries = SerializationUtil.Deserialize<List<string>>(user.acl);
                if (entries == null) entries = new List<string>();
                if (entries.Contains(entry))
                {
                    throw new FaultException("ENTRY_ALREADY_EXISTED");
                }
                entries.Add(entry);
                user.acl = SerializationUtil.Serialize(entries);
                db.SaveChanges();
            }
        }

        public void RemoveAclEntry(int id, string entry)
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
                User user = result.First();
                List<string> entries = SerializationUtil.Deserialize<List<string>>(user.acl);
                if (entries == null) entries = new List<string>();
                if (!entries.Contains(entry))
                {
                    throw new FaultException("ENTRY_NOT_EXISTS");
                }
                entries.Remove(entry);
                user.acl = SerializationUtil.Serialize(entries);
                db.SaveChanges();
            }
        }
    }
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
}