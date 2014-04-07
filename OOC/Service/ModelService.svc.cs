using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using OOC.Contract.Service;
using OOC.Entity;

namespace OOC.Service
{
    public class ModelService : IModelService
    {
        private static readonly object ModelLock = new object();

        public void Create(string name, string version, int authorUserId, string className)
        {
            lock (ModelLock)
            {
                using (var db = new OOCEntities())
                {
                    var model = new Model
                        {
                            name = name,
                            version = version,
                            authorUserId = authorUserId,
                            className = className,
                        };
                    db.Model.AddObject(model);
                    db.SaveChanges();
                }
            }
        }

        public void Create(string name, string version, int authorUserId, string className, DateTime creation,
                           DateTime modification,
                           bool isPublic, bool isApproved)
        {
            lock (ModelLock)
            {
                using (var db = new OOCEntities())
                {
                    var model = new Model
                        {
                            name = name,
                            version = version,
                            authorUserId = authorUserId,
                            className = className,
                            creation = creation,
                            modification = modification,
                            isPublic = isPublic,
                            isApproved = isApproved,
                        };
                    db.Model.AddObject(model);
                    db.SaveChanges();
                }
            }
        }

        public Model GetByGuid(string guid)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           where o.guid == guid
                                           select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_NOT_FOUND");
                }
                return result.First();
            }
        }

        public List<Model> ListByName(string name)
        {
            using (var db = new OOCEntities())
            {
                IOrderedQueryable<Model> result = from o in db.Model
                                                  where o.name == name
                                                  orderby o.creation descending
                                                  select o;
                return result.ToList();
            }
        }

        public List<Model> ListByAuthorUserId(int authorUserId)
        {
            using (var db = new OOCEntities())
            {
                IOrderedQueryable<Model> result = from o in db.Model
                                                  where o.authorUserId == authorUserId
                                                  orderby o.creation descending
                                                  select o;
                return result.ToList();
            }
        }

        public List<Model> ListByClassName(string className)
        {
            using (var db = new OOCEntities())
            {
                IOrderedQueryable<Model> result = from o in db.Model
                                                  where o.className == className
                                                  orderby o.creation descending
                                                  select o;
                return result.ToList();
            }
        }

        public List<Model> ListByModelTags(List<ModelTag> modelTags)
        {
            //TODO complete me.
            throw new NotImplementedException();
        }

        public void UpdateModification(string guid, DateTime modification)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           where o.guid == guid
                                           select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_NOT_FOUND");
                }
                Model model = result.First();
                model.modification = modification;
                db.SaveChanges();
            }
        }

        public bool Audit(string guid)
        {
            bool auditResult;
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           where o.guid == guid
                                           select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_NOT_FOUND");
                }
                Model model = result.First();
                model.isApproved = true; //TODO change this corresponding to pratical logics.
                db.SaveChanges();
                auditResult = model.isApproved;
            }
            return auditResult;
        }
    }
}