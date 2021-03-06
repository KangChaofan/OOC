﻿using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using System.Collections;
using System.Text;
using System;

namespace OOC.Service
{
    [ExposedService("ModelTypeService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ModelTypeService : IModelTypeService
    {

        public List<ModelType> GetTypeList()
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<ModelType> result = from o in db.ModelType
                                               select o;
                return result.ToList();
            }
        }

        public System.Data.DataSet GetTypeListDS()
        {//功能与GetTypeList()重复，暂不使用
            ModelType modelTypeDAL = new ModelType();
            System.Data.DataSet ds = new System.Data.DataSet();
            return ds;
        }

        public List<ModelType> GetTopList()
        {
            bool IsTop = true;
            using (OOCEntities db = new OOCEntities())
            {
            
                    IQueryable<ModelType> result = from o in db.ModelType
                                                   where o.isTop == IsTop
                                                   select o;
                    string temp = result.First().typeName;
                    return result.ToList();           
            }
        }

        public List<ModelType> GetSubByTopID(Int32 TopID)
        {
            Int32 topId = TopID;
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<ModelType> result = from o in db.ModelType
                                               where o.topId == topId
                                               select o;
                return result.ToList();
            }
        }

        public bool IsTopType(int TypeID)
        {
            Int32 typeId = TypeID;

            using (OOCEntities db = new OOCEntities())
            {
                int result = (from o in db.ModelType
                              where o.id == typeId && o.topId == 0
                              select o).Count();


                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public ModelType GetTypeByID(int ID)
        {
            using (OOCEntities db = new OOCEntities())
            {
                IQueryable<ModelType> result = from o in db.ModelType
                                               where o.id == ID
                                               select o;
                return result.First();
            }
        }
    }
}
