using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;
using WebApplication1.ResultLogsServiceReference;


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