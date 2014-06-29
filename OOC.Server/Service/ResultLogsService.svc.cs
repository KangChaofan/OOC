using System.Linq;
using System.ServiceModel;
using System.Collections.Generic;
using OOC.Entity;
using OOC.Contract.Data.Response;
using OOC.Contract.Service;
using OOC.ServiceAttribute;

namespace OOC.Service
{
    [ExposedService("ResultLogsService")]
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ResultLogsService : IResultLogsService
    {
        public void AddOne(ResultLogs ModelResultLog) {
            /*strSql.Append("insert into ResultLogs (ID,ModelID,UserID,CalTime,FileFolder)");
            strSql.Append(" values(@ID,@ModelID,@UserID,@CalTime,@FileFolder);");
            SqlParameter[] parameters ={
                                        new SqlParameter("@ID", SqlDbType.NVarChar,50),
                                        new SqlParameter("@ModelID",SqlDbType.NVarChar,50),
                                        new SqlParameter("@UserID",SqlDbType.NVarChar,50),
                                        new SqlParameter("@CalTime",SqlDbType.DateTime),
                                        new SqlParameter("@FileFolder",SqlDbType.NVarChar,200),
                                    };
            parameters[0].Value = ModelResultLog.ID;
            parameters[1].Value = ModelResultLog.ModelID;
            parameters[2].Value = ModelResultLog.UserID;
            parameters[3].Value = ModelResultLog.CalTime;
            parameters[4].Value = ModelResultLog.FileFolder;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);*/
            using (OOCEntities db = new OOCEntities()) {
                db.ResultLogs.AddObject(ModelResultLog);
                db.SaveChanges();
            }
        
        }

        public ResultLogs GetModelOne(string ID) {
            /*sb.Append("Select ID,ModelID,UserID,CalTime,FileFolder FROM ResultLogs WHERE ID=@ID;");
            SqlParameter[] parameters ={
                                        new SqlParameter("@ID", SqlDbType.NVarChar,50)              
                                    };
            parameters[0].Value = ID;

            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query(sb.ToString(), parameters);
            DataRow dr = ds.Tables[0].Rows[0];
            ModelYunModel.ResultLogs oneModel = new ModelYunModel.ResultLogs();
            oneModel.ID = dr[0].ToString();
            oneModel.ModelID = dr[1].ToString();
            oneModel.UserID = dr[2].ToString();
            oneModel.CalTime = Convert.ToDateTime(dr[3]);
            oneModel.FileFolder = dr[4].ToString();
            return oneModel;*/
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<ResultLogs> result = from o in db.ResultLogs
                                                where o.id == ID
                                                select o;
                return result.First();
            }

        
        }


        public List<ResultLogs> GetLogsList() {
            /*sb.Append("Select top 8 ID,ModelID,UserID,CalTime FROM ResultLogs order by CalTime desc;");
            DataSet ds = new DataSet();
            ds = DbHelperSQL.Query(sb.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows) {
                ModelYunModel.ResultLogs modelType = new ModelYunModel.ResultLogs();

                modelType.ID = dr[0].ToString();
                modelType.ModelID = dr[1].ToString();
                modelType.UserID = dr[2].ToString();
                modelType.CalTime = Convert.ToDateTime(dr[3]);
                list.Add(modelType);
            }
            return list;*/
            using (OOCEntities db = new OOCEntities()) {
                IQueryable<ResultLogs> result = from o in db.ResultLogs
                                                orderby o.calTime descending
                                                select o;
                return result.ToList();
            }
        
        }

    }
}