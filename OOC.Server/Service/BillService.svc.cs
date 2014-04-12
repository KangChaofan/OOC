using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;

namespace OOC.Service
{
    [ExposedService("BillService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class BillService : IBillService
    {
        private static readonly object billingLock = new object();

        public void Create(int userId, string taskGuid, string cmGuid, double amount)
        {
            lock (billingLock)
            {
                using (OOCEntities db = new OOCEntities())
                {
                    IQueryable<User> result = from o in db.User
                                              where o.id == userId
                                              select o;
                    if (!result.Any())
                    {
                        throw new FaultException("USER_NOT_FOUND");
                    }
                    User user = result.First();
                    if (user.balance - amount < 0)
                    {
                        throw new FaultException("INSUFFICIENT_BALANCE");
                    }
                    var bill = new Bill
                        {
                            userId = userId,
                            taskGuid = taskGuid,
                            cmGuid = cmGuid,
                            amount = amount
                        };
                    user.balance -= amount;
                    db.Bill.AddObject(bill);
                    db.SaveChanges();
                }
            }
        }

        public Bill GetById(int id)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<Bill> result = from o in db.Bill
                                          where o.id == id
                                          select o;
                if (!result.Any())
                {
                    throw new FaultException("BILL_NOT_FOUND");
                }
                return result.First();
            }
        }

        public List<Bill> GetByUserId(int userId)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IOrderedQueryable<Bill> result = from o in db.Bill
                                                 where o.userId == userId
                                                 orderby o.creation
                                                 select o;
                return result.ToList();
            }
        }

        public List<Bill> GetByTaskGuid(string taskGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IOrderedQueryable<Bill> result = from o in db.Bill
                                                 where o.taskGuid == taskGuid
                                                 orderby o.creation
                                                 select o;
                return result.ToList();
            }
        }

        public List<Bill> GetByCmGuid(string cmGuid)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IOrderedQueryable<Bill> result = from o in db.Bill
                                                 where o.cmGuid == cmGuid
                                                 orderby o.creation
                                                 select o;
                return result.ToList();
            }
        }

        public void Refund(int id)
        {
            lock (billingLock)
            {
                using (OOCEntities db = new OOCEntities())
                {
                    IQueryable<Bill> result = from o in db.Bill
                                              where o.id == id
                                              select o;
                    if (!result.Any())
                    {
                        throw new FaultException("BILL_NOT_FOUND");
                    }
                    Bill bill = result.First();
                    if (bill.isRefunded)
                    {
                        throw new FaultException("BILL_ALREADY_REFUNDED");
                    }
                    bill.isRefunded = true;
                    bill.User.balance += bill.amount;
                    db.SaveChanges();
                }
            }
        }
    }
}