using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebApplication1.ResultLogsServiceReference;
using System.Text.RegularExpressions;
using WebApplication1.TaskProcessedDataSetServiceReference;
using OOC.Util;


namespace WebApplication1
{
    public partial class statistics : System.Web.UI.Page
    {
                //public ResultLogs ModelResultLogs;
                protected void Page_Load(object sender, EventArgs e)
                {
                    if (!IsPostBack)
                    {
                        //显示初始化三个ref的值
                        //double ymax = 0;
                        //double xmax = 0;
                        //StringBuilder sb = new StringBuilder();
                        //ReadFileForChart("C:\\out\\initialization.dat", 3, 1, 3, ref ymax, ref xmax, ref sb);
                        //litForScript.Value = sb.ToString();

                        //显示初始化三个ref的值
                        double ymax = 0;
                        double xmax = 0;
                        StringBuilder sb = new StringBuilder();                      
                        string TaskID = Request["TaskID"];
                        //ModelYunBLL.ResultLogs BLLResultLogs = new ModelYunBLL.ResultLogs();
                        ResultLogsServiceClient rs = new ResultLogsServiceClient();
                        //ModelResultLogs = new ResultLogs();
                        //ModelResultLogs = rs.GetModelOne(TaskID);
                        TaskProcessedDataServiceClient taskProcessedDataService=new TaskProcessedDataServiceClient();
                        List<TaskProcessedDataSet> taskProcessedDataSetList=new List<TaskProcessedDataSet>();
                        taskProcessedDataSetList.AddRange(taskProcessedDataService.GetDataSetByTaskGuid(TaskID));
                        foreach(TaskProcessedDataSet oneDataSet in taskProcessedDataSetList){
                            List<TaskProcessedDataRecord> dataSetRecords=new List<TaskProcessedDataRecord>();
                            dataSetRecords.AddRange(taskProcessedDataService.QueryDataSet(oneDataSet.guid,1,0));                                                  
                            foreach(TaskProcessedDataRecord one in dataSetRecords){
                                string temp = "sin.push([" + SerializationUtil.ToArray(one.record)[0] + "," + one.seq + "]);";
                                sb.Append(temp);
                            }                                                     
                        }                     
                        //   StringBuilder strForScript   
                        //   string temp = "sin.push([" + ss[Col1year] + "," + ss[Col2SEQ] + "]);";
                        //   strForScript.Append(temp);
                        //      
                        litForScript.Value = sb.ToString();
                    }
                }

        /// <summary>
        /// 读取数据文件并传递出XY轴的最大值
        /// </summary>
        /// <param name="FilePath">文件绝对路径</param>
        /// <param name="StartRow">文件中数据开始的行</param>
        /// <param name="Col1">选取某列数据（从0开始）</param>
        /// <param name="Col2">选取某列数据（从0开始）</param>
        /// <param name="YMax">ref Y轴最大值</param>
        /// <param name="XMax">ref X轴最大值</param>
        /// <param name="strForScript">ref 数据script</param>
    }
}