using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using OOC.Response;
using OOC.ORM;

namespace OOC.Service
{
    public class BillService : IBillService
    {
        private static object billingLock = new object();

        private oocEntities db = new oocEntities();

        public GenericResponse Create(int userId, string taskGuid, string cmGuid, double amount)
        {
            lock (billingLock)
            {
                var result = from o in db.User
                             where o.id == userId
                             select o;
                if (result.Count() == 0)
                {
                    return new GenericResponse(false, 1, "USER_NOT_FOUND");
                }
                User user = result.First();
                if (user.balance - amount < 0)
                {
                    return new GenericResponse(false, 2, "INSUFFICIENT_BALANCE");
                }
                Bill bill = new Bill()
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
            var result = from o in db.Bill
                         where o.id == id
                         select o;
            if (result.Count() == 0)
            {
                return new BillResponse(false, 1, "BILL_NOT_FOUND");
            }
            return new BillResponse(result.First());
        }

        public BillListResponse GetByUserId(int userId)
        {
            var result = from o in db.Bill
                         where o.userId == userId
                         select o;
            return new BillListResponse(result.ToList());
        }

        public BillListResponse GetByTaskGuid(string taskGuid)
        {
            var result = from o in db.Bill
                         where o.taskGuid == taskGuid
                         select o;
            return new BillListResponse(result.ToList());
        }

        public BillListResponse GetByCmGuid(string cmGuid)
        {
            var result = from o in db.Bill
                         where o.cmGuid == cmGuid
                         select o;
            return new BillListResponse(result.ToList());
        }

        public GenericResponse Refund(int id)
        {
            lock (billingLock)
            {
                var result = from o in db.Bill
                             where o.id == id
                             select o;
                if (result.Count() == 0)
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
