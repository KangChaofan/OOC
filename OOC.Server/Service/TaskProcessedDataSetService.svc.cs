﻿using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;
using OOC.Util;

namespace OOC.Service
{
    [ExposedService("TaskProcessedDataSetService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class TaskProcessedDataSetService : ITaskProcessedDataSetService
    {
        public string CreateDataSet(string taskGuid, string cmGuid, string className, string name) {
            
            using(OOCEntities db=new OOCEntities()){
                TaskProcessedDataSet dataSet = new TaskProcessedDataSet();
                dataSet.guid = GuidUtil.newGuid();
                dataSet.taskGuid = taskGuid;
                dataSet.cmGuid = cmGuid;
                dataSet.className = className;
                dataSet.name = name;
                db.TaskProcessedDataSet.AddObject(dataSet);
                db.SaveChanges();
                return dataSet.guid;
        }
        }
        public void RemoveDataSetByGuid(string dataSetGuid) {
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<TaskProcessedDataSet> result = from o in db.TaskProcessedDataSet
                                          where o.guid == dataSetGuid
                                          select o;
               
                if (!result.Any()) {
                    throw new FaultException("TaskProcessedDataSet_NOT_Exits");
                }
                db.TaskProcessedDataSet.DeleteObject(result.First());
                db.SaveChanges();
            }
        
        }

        public void InsertIntoDataSet(string dataSetGuid, string[] record) {
            using (OOCEntities db = new OOCEntities()) {
                TaskProcessedDataRecord dataRecord= new TaskProcessedDataRecord();
                dataRecord.dataSetGuid = dataSetGuid;
                dataRecord.record = SerializationUtil.FromArray(record);
                db.TaskProcessedDataRecord.AddObject(dataRecord);
                db.SaveChanges();
               
            }

        
        }
        public void InsertMultipleIntoDataSet(string dataSetGuid, List<string[]> records) {

            using (OOCEntities db = new OOCEntities()) {
                foreach (string[] record in records) {
                    TaskProcessedDataRecord dataRecord = new TaskProcessedDataRecord();
                    dataRecord.dataSetGuid = dataSetGuid;
                    dataRecord.record = SerializationUtil.FromArray(record);
                    db.TaskProcessedDataRecord.AddObject(dataRecord);
                    db.SaveChanges();
                }

            }

        }

        public List<string[]> QueryDataSet(int start, int limit) {
           
            using (OOCEntities db = new OOCEntities()) {
                
                    TaskProcessedDataRecord dataRecord = new TaskProcessedDataRecord();
                    IQueryable<TaskProcessedDataRecord>result= from o in db.TaskProcessedDataRecord
                                                                orderby o.seq
                                                               select o;
               if(limit<start){
               throw new FaultException("Limit<start");
               }
                if(limit>result.Count()){
                    throw new FaultException("Limit_Error");
                
                }
                if(start<result.Count()){
                    throw new FaultException("Start_Error");
                }
                if(limit==0){
                limit=result.Count();
                }
                List<string[]> records= new List<string[]>();
                foreach(TaskProcessedDataRecord dataSetRecord in result.Skip(start-1).Take(limit-start+1).ToList()){
                    string[] record =SerializationUtil.ToArray(dataSetRecord.record);
                    records.Add(record);

                        }
                    return records;
                        
                        
                }

            }
         public List<string[]> QueryDataSetByGuid(string dataSetGuid,int start, int limit){
         
         using (OOCEntities db = new OOCEntities()) {
                
                    TaskProcessedDataRecord dataRecord = new TaskProcessedDataRecord();
                    IQueryable<TaskProcessedDataRecord>result= from o in db.TaskProcessedDataRecord
                                                                where o.dataSetGuid==dataSetGuid
                                                                orderby o.seq
                                                                select o;
                    if (limit < start) {
                        throw new FaultException("Limit<start");
                    }
                    if (limit > result.Count()) {
                        throw new FaultException("Limit_Error");

                    }
                    if (start < result.Count()) {
                        throw new FaultException("Start_Error");
                    }
                    if (limit == 0) {
                        limit = result.Count();
                    }
                    List<string[]> records= new List<string[]>();
                    foreach (TaskProcessedDataRecord dataSetRecord in result.Skip(start - 1).Take(limit - start + 1).ToList()) {
                    string[] record =SerializationUtil.ToArray(dataSetRecord.record);
                    records.Add(record);

                        }
                    return records;
                        
                        
                }

         
         }
        
        }


    }
