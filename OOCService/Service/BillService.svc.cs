using System.Linq;
using OOC.ORM;
using OOC.Response;
using OOC.Candy;

namespace OOC.Service
{
    [ExposedService("BillService")]
    public class BillService : IBillService
    {
        private static readonly object billingLock = new object();

        private readonly oocEntities db = new oocEntities();

        public GenericResponse Create(int userId, string taskGuid, string cmGuid, double amount)
        {
            lock (billingLock)
            {
                IQueryable<User> result = from o in db.User
                                          where o.id == userId
                                          select o;
                if (!result.Any())
                {
                    return new GenericResponse(false, 1, "USER_NOT_FOUND");
                }
                User user = result.First();
                if (user.balance - amount < 0)
                {
                    return new GenericResponse(false, 2, "INSUFFICIENT_BALANCE");
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
                return new GenericResponse(true);
            }
        }

        public BillResponse GetById(int id)
        {
            IQueryable<Bill> result = from o in db.Bill
                                      where o.id == id
                                      select o;
            if (!result.Any())
            {
                return new BillResponse(false, 1, "BILL_NOT_FOUND");
            }
            return new BillResponse(result.First());
        }

        public BillListResponse GetByUserId(int userId)
        {
            IOrderedQueryable<Bill> result = from o in db.Bill
                                             where o.userId == userId
                                             orderby o.creation
                                             select o;
            return new BillListResponse(result.ToList());
        }

        public BillListResponse GetByTaskGuid(string taskGuid)
        {
            IOrderedQueryable<Bill> result = from o in db.Bill
                                             where o.taskGuid == taskGuid
                                             orderby o.creation
                                             select o;
            return new BillListResponse(result.ToList());
        }

        public BillListResponse GetByCmGuid(string cmGuid)
        {
            IOrderedQueryable<Bill> result = from o in db.Bill
                                             where o.cmGuid == cmGuid
                                             orderby o.creation
                                             select o;
            return new BillListResponse(result.ToList());
        }

        public GenericResponse Refund(int id)
        {
            lock (billingLock)
            {
                IQueryable<Bill> result = from o in db.Bill
                                          where o.id == id
                                          select o;
                if (!result.Any())
                {
                    return new GenericResponse(false, 1, "BILL_NOT_FOUND");
                }
                Bill bill = result.First();
                if (bill.isRefunded)
                {
                    return new GenericResponse(false, 2, "BILL_ALREADY_REFUNDED");
                }
                bill.isRefunded = true;
                bill.User.balance += bill.amount;
                db.SaveChanges();
                return new GenericResponse(true);
            }
        }
    }
}