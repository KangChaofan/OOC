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
        public void Create(string name, string version, int authorUserId, string className, Int32 topId, Int32 typeId)
        {
            using (var db = new OOCEntities())
            {
                var model = new Model
                    {
                        name = name,
                        version = version,
                        authorUserId = authorUserId,
                        className = className,
                        topId = topId,
                        typeId = typeId
                    };
                db.Model.AddObject(model);
                db.SaveChanges();
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

        public List<ModelProperty> GetModelProperties(string guid)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<ModelProperty> result = from o in db.ModelProperty
                                                   where o.modelGuid == guid
                                                   select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_NOT_FOUND");
                }
                return result.ToList();
            }
        }

        public void AddModelProperty(string guid, ModelProperty modelProperty)
        {
            using (var db = new OOCEntities())
            {
                db.ModelProperty.AddObject(modelProperty);
                db.SaveChanges();
            }
        }

        public void RemoveModelProperty(string guid, string key)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<ModelProperty> result = from o in db.ModelProperty
                                                   where o.modelGuid == guid && o.key == key
                                                   select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_NOT_FOUND");
                }
                db.ModelProperty.DeleteObject(result.First());
                db.SaveChanges();
            }
        }

        public void UpdateModelProperty(string guid, string key, ModelProperty modelProperty)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<ModelProperty> result = from o in db.ModelProperty
                                                   where o.modelGuid == guid && o.key == key
                                                   select o;
                if (!result.Any())
                {
                    throw new FaultException("MODEL_MOT_FOUND");
                }
                db.ModelProperty.ApplyCurrentValues(modelProperty);
            }
        }

        public List<Model> ModelSimpleList()
        {
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           select o;
                return result.ToList();
            }

        }

        public ModelProperty GetRiverBasinByModelGuid(string modelGuid)
        {
            //string key = "RiverBasin";
            using (var db = new OOCEntities())
            {
                IQueryable<ModelProperty> result = from o in db.ModelProperty
                                                   where o.modelGuid == modelGuid //&& o.key == key
                                                   select o;
                return result.First();
            }
        }

        public List<Model> ModelSimpleListByTopID(int TypeID)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           where o.topId == TypeID
                                           select o;
                return result.ToList();
            }
        }

        public List<Model> ModelSimpleListByTypeID(int TypeID)
        {
            using (var db = new OOCEntities())
            {
                IQueryable<Model> result = from o in db.Model
                                           where o.typeId == TypeID
                                           select o;
                return result.ToList();
            }
        }
    }
}