using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;
using OOC.Contract.Data.Common;
using OOC.Contract.Data.Request;
using OOC.Contract.Data.Response;
using System;
using System.Data.Objects;
using System.Data.Linq;



namespace OOC.Service
{
    [ExposedService("OutputParameterService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class OutputParameterService : IOutputParameterService
    {
        private static readonly object billingLock = new object();

        //string guid, string elementSet？,string quantity?,string parameterValue,string creation
        //根据task的计算结果创建outputParameter表项
        public void Create(string taskGuid, string compositionGuid, string compositionModelGuid, string elementSet, string quantity, string parameterValue, DateTime creation) {
            using (OOCEntities db = new OOCEntities()) {
                    OutputParameter outputParameter = new OutputParameter(){
                     guid = GuidUtil.newGuid(),
            taskGuid = taskGuid,
            compositionGuid = compositionGuid,
            compositionModelGuid = compositionModelGuid,
            elementSet = elementSet,
            quantity = quantity,
            parameterValue = parameterValue,
            creation = creation
                };
                    db.OutputParameter.AddObject(outputParameter);
                db.SaveChanges();
              
            }
        }
        //根据guid查outputParameter表项
        public List<OutputParameter> GetByGuid(string guid) {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                      where o.guid == guid
                                                      select o;
                return result.ToList();
            }
            
        }
        //根据taskGuid查outputParameter表项
        public List<OutputParameter> GetByTaskGuid(string taskGuid) 
        {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                     where o.taskGuid == taskGuid
                                                     select o;
                return result.ToList();
            }
        }
        //根据compositionGuid查outputParameter表项
        public List<OutputParameter> GetByCompGuid(string compGuid) {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                     where o.compositionGuid == compGuid
                                                     select o;
                return result.ToList();
            }
        }
        //根据Time查outputParameter表项
        public List<OutputParameter> GetByTime(DateTime startTime, DateTime overTime) {
            
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                     where o.creation >= startTime && o.creation <= overTime
                                                     select o;
                return result.ToList();
            }
        }
        //根据compositionModelGuid查outputParameter表项
        public List<OutputParameter> GetByCmGuid(string cmGuid) {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                     where o.compositionModelGuid == cmGuid
                                                     select o;
                return result.ToList();
            }
        }
        //根据taskGuid,compositionGuid,compositionModelGuid,Time查outputParameter表项
        public List<OutputParameter> GetByGuidsAndTime(string taskGuid, string compGuid, string cmGuid,DateTime startTime, DateTime overTime) {
            int pageSize = 2;//每页显示的条数方案一：可以通过外部参数传递进行设置，由外围控制分页的范围，但是要记得及时的保存；方案二：可以定时的改变变量值进行查询，然后固定保存用以查询
            int pageIndex = 0;//页数，从0页开始
           using (OOCEntities db = new OOCEntities()) {//使用LINQ在后续中关于频繁的数据访问可能会涉及到效率的问题，所以在实现基本功能后要考虑进行优化
                /*IQueryable<OutputParameter> result = from o in db.OutputParameter
                                                     where o.taskGuid == taskGuid && o.compositionGuid == compGuid && o.compositionModelGuid == cmGuid && o.creation >= startTime && o.creation <= overTime
                                                     orderby o.creation ascending
                                                     select o*/
               /*IQueryable<OutputParameter> result = (from o in db.OutputParameter orderby o.creation ascending
                                                     where o.taskGuid == taskGuid && o.compositionGuid == compGuid && o.compositionModelGuid == cmGuid && o.creation >= startTime && o.creation <= overTime
                                                     //where ((from o1 in db.OutputParameter
                                                       //      orderby o1.creation ascending 
                                                         // select o1.guid).Skip(m).Take(n)).Contains(o.guid)//从查询结果中跳过前每个记录，只要剩下的前n个记录
                                                   
                                                    select o).Skip(pageIndex*pageSize).Take(pageSize);*/
                 var result = db.OutputParameter_show(taskGuid, compGuid, cmGuid, startTime, overTime, pageIndex, pageSize);//利用linq to entity 技术添加数据库中的存储过程，调用数据库中的存储过程进行查询并返回
            return result.ToList();
            }
        }
    }
}