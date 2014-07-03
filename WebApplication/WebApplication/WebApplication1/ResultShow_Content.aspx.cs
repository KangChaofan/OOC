using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;
using WebApplication1.ResultLogsServiceReference;
using WebApplication1.CompositionServiceReference;
using WebApplication1.TaskServiceReference;
using OOC.Entity;


namespace WebApplication1
{
    public partial class ResultShow_Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetResultLogsList8ForHTML(LitResultLogs);
            }
        }

        public void GetResultLogsList8ForHTML(Literal lit)
        {
            //ModelYunBLL.ResultLogs _ResultLogsListBLL = new ModelYunBLL.ResultLogs();
            ResultLogsServiceClient rs = new ResultLogsServiceClient();
            //ModelYunBLL.ModelType _ModelTypeBLL = new ModelYunBLL.ModelType();
            ModelTypeServiceClient mts = new ModelTypeServiceClient();
            ModelServiceClient ms = new ModelServiceClient();
            CompositionServiceClient compositionService = new CompositionServiceClient();
            //System.Data.DataSet ModelTypeDS = mts.GetTypeListDS();
            TaskServiceClient ts=new TaskServiceClient();
            List<ResultLogsServiceReference.ResultLogs> list = new List<ResultLogsServiceReference.ResultLogs>();
            //根据类型ID,UserID
            string _typeID = Request["Type"];
            string _userid = Request.Cookies["DigitalBasinUserID"].Value;
            List<ModelServiceReference.Model> ml = new List<ModelServiceReference.Model>();
            ml.AddRange(ms.ModelSimpleListByTypeID(Convert.ToInt32(_typeID)));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (ModelServiceReference.Model oneModel in ml) {
                List<OOC.Entity.Composition> compositionList = new List<OOC.Entity.Composition>();
                compositionList.AddRange(compositionService.QueryCompositionByModel(oneModel.guid));
                foreach (OOC.Entity.Composition oneComposition in compositionList) {
                    List<TaskServiceReference.Task> taskList = new List<TaskServiceReference.Task>();
                    taskList.AddRange(ts.GetTaskByCompositionAndUser(oneComposition.guid, Convert.ToInt32(_userid)));
                    foreach (var _oneTask in taskList) 
                    {
                        sb.Append(" <tr>");
                        sb.Append(" <td>" + _oneTask.guid + "</td>");
                        sb.Append(" <td>" + _oneTask.timeStarted.ToString() + "</td>");
                        sb.Append(" <td>" + _oneTask.timeFinished.ToString() + "</td>");
                        sb.Append(" <td><input type=\"button\" title=\"" + _typeID + "\" class=\"buttonS bGreen\" id=\"" + _oneTask.guid + "\" value=\"查看\" name=\"" + oneModel.guid + "\" /> </td>");
                        sb.Append("  <td><input type=\"button\" class=\"buttonS bRed\" id=\"EditReCal\" value=\"编辑重算\" />   </td>");
                        sb.Append("</tr>");
                    }
                
                }            
            }
            lit.Text = sb.ToString();
        }


        /// <summary>
        /// 根据类型完整表查询出模型名称
        /// </summary>
        /// <param name="ModelID"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string GetModelNameByID(string ModelID, System.Data.DataSet ds)//没用
        {
            string ss = ds.Tables[0].Select("ID=" + ModelID)[0][1].ToString();
            return ss;
        }
    }
}