using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebApplication1.ResultLogsServiceReference;
using System.Text.RegularExpressions;
<<<<<<< HEAD
using WebApplication1.TaskProcessedDataSetServiceReference;
using OOC.Util;
=======
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64

namespace WebApplication1
{
    public partial class LatestResultShow : System.Web.UI.Page
    {
<<<<<<< HEAD
        //public ResultLogs ModelResultLogs;
        protected void Page_Load(object sender, EventArgs e) {
            /*
                           //显示初始化三个ref的值
                           double ymax = 0;
                           double xmax = 0;
                           StringBuilder sb = new StringBuilder();

          
                           string ResultLogsID = Request["ResultLogsID"];
                           //ModelYunBLL.ResultLogs BLLResultLogs = new ModelYunBLL.ResultLogs();
                           ResultLogsServiceClient rs = new ResultLogsServiceClient();
                           ModelResultLogs = new ResultLogs();
                           ModelResultLogs = rs.GetModelOne(ResultLogsID);

                           ReadFileForChart(ModelResultLogs.FileFolder + @"\Result\initialization.dat", 3, 1, 3, ref ymax, ref xmax, ref sb);
                           litForScript.Value = sb.ToString();
                */
            if (!IsPostBack) {
                //显示初始化三个ref的值
                //double ymax = 0;
                //double xmax = 0;
                //StringBuilder sb = new StringBuilder();
                //ReadFileForChart("C:\\out\\initialization.dat", 3, 1, 3, ref ymax, ref xmax, ref sb);
                //litForScript.Value = sb.ToString();

=======
        public ResultLogs ModelResultLogs;
        protected void Page_Load(object sender, EventArgs e)
        {
 
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
                //显示初始化三个ref的值
                double ymax = 0;
                double xmax = 0;
                StringBuilder sb = new StringBuilder();

<<<<<<< HEAD

                string TaskID = Request["TaskID"];
                //ModelYunBLL.ResultLogs BLLResultLogs = new ModelYunBLL.ResultLogs();
                ResultLogsServiceClient rs = new ResultLogsServiceClient();
                //ModelResultLogs = new ResultLogs();
                //ModelResultLogs = rs.GetModelOne(TaskID);
                TaskProcessedDataServiceClient taskProcessedDataService = new TaskProcessedDataServiceClient();
                List<TaskProcessedDataSet> taskProcessedDataSetList = new List<TaskProcessedDataSet>();
                taskProcessedDataSetList.AddRange(taskProcessedDataService.GetDataSetByTaskGuid(TaskID));
                foreach (TaskProcessedDataSet oneDataSet in taskProcessedDataSetList) {
                    List<TaskProcessedDataRecord> dataSetRecords = new List<TaskProcessedDataRecord>();
                    dataSetRecords.AddRange(taskProcessedDataService.QueryDataSet(oneDataSet.guid, 1, 0));

                    foreach (TaskProcessedDataRecord one in dataSetRecords) {
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
=======
          
                string ResultLogsID = Request["ResultLogsID"];
                //ModelYunBLL.ResultLogs BLLResultLogs = new ModelYunBLL.ResultLogs();
                ResultLogsServiceClient rs = new ResultLogsServiceClient();
                ModelResultLogs = new ResultLogs();
                ModelResultLogs = rs.GetModelOne(ResultLogsID);

                ReadFileForChart(ModelResultLogs.FileFolder + @"\Result\initialization.dat", 3, 1, 3, ref ymax, ref xmax, ref sb);
                litForScript.Value = sb.ToString();
     
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
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
        public void ReadFileForChart(string FilePath, int StartRow, int Col1, int Col2, ref double YMaxData, ref double XMaxDate, ref StringBuilder strForScript)
        {
            System.IO.StreamReader my = new System.IO.StreamReader(FilePath, System.Text.Encoding.Default);
            string line;
            int countRow = 0;

            while ((line = my.ReadLine()) != null)
            {
                string lineTemp = line;
                string[] ss = Regex.Split(lineTemp, "\u0020+");
                if (countRow >= StartRow)
                {
                    if (ss.Length < 4)
                    {
                        break;
                    }
                    else
                    {
                        string temp = "sin.push([" + ss[Col1] + "," + ss[Col2] + "]);";
                        strForScript.Append(temp);
                    }
                    
                }
                countRow++;
            }
            my.Close();
        }
    }
}