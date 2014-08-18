using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;
using WebApplication1.ResultLogsServiceReference;
<<<<<<< HEAD
using WebApplication1.CompositionServiceReference;
using WebApplication1.TaskServiceReference;

=======
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64


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
<<<<<<< HEAD
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
                List<CompositionServiceReference.Composition> compositionList = new List<CompositionServiceReference.Composition>();
                compositionList.AddRange(compositionService.QueryCompositionByModel(oneModel.guid));
                foreach (CompositionServiceReference.Composition oneComposition in compositionList) {
                    List<TaskServiceReference.Task> taskList = new List<TaskServiceReference.Task>();
                    taskList.AddRange(ts.GetTaskByCompositionAndUser(oneComposition.guid, Convert.ToInt32(_userid)));//！！！！！！！！！！！！！！！应该对任务的状态信息进行二次的筛选，因为不是所有的任务都是已经计算完毕的
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
=======
            //System.Data.DataSet ModelTypeDS = mts.GetTypeListDS();

            List<ResultLogs> list = new List<ResultLogs>();
            list.AddRange(rs.GetLogsList());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (ResultLogs one in list)
            {
                //获取模型名称
                //string _ModelName = GetModelNameByID(one.ModelID, ModelTypeDS);
                Model model = ms.GetByGuid(one.ModelID);
                sb.Append(" <tr>");
                sb.Append(" <td>" + one.ID + "</td>");
                sb.Append(" <td>" + model.name + "</td>");
                sb.Append(" <td>" + one.CalTime.ToString("yyyy-MM-dd hh:MM:ss") + "</td>");
                sb.Append(" <td><input type=\"button\" class=\"buttonS bGreen\" id=\"" + one.ID + "\" value=\"查看\" name=\"" +one.ModelID+ "\" /> </td>");
                sb.Append("  <td><input type=\"button\" class=\"buttonS bRed\" id=\"EditReCal\" value=\"编辑重算\" />   </td>");
                sb.Append("</tr>");
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
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