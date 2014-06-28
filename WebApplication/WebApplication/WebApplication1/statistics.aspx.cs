using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebApplication1.ResultLogsServiceReference;
using System.Text.RegularExpressions;

namespace WebApplication1
{
    public partial class statistics : System.Web.UI.Page
    {
                public ResultLogs ModelResultLogs;
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

                 
                        string ResultLogsID = Request["ResultLogsID"];
                        //ModelYunBLL.ResultLogs BLLResultLogs = new ModelYunBLL.ResultLogs();
                        ResultLogsServiceClient rs = new ResultLogsServiceClient();
                        ModelResultLogs = new ResultLogs();
                        ModelResultLogs = rs.GetModelOne(ResultLogsID);
                        ReadFileForChart(ModelResultLogs.FileFolder + @"\Result\initialization.dat", 3, 1, 3, ref ymax, ref xmax, ref sb);
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