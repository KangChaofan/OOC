using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using Oatc.OpenMI.Examples.ModelComponents.SimpleRiver.Wrapper;
using System.Runtime.InteropServices;
using System.Threading;
using System.Xml;
using System.IO;


using System.Collections;

namespace WebApplication1
{
    public partial class runModel : System.Web.UI.Page
    {
        //-------------
       public  Thread t;
       public System.Threading.Timer time;  //线程计时器.
      // private DotNetAccess netaccess;
       string _FileFolder = "";
       //ModelYunModel.ResultLogs ModelResultLogs = new ModelYunModel.ResultLogs();

        //----------
       protected void Page_Load(object sender, EventArgs e)
       {
           //netaccess = new DotNetAccess();
           //netaccess.Initialize("sd");
           //netaccess.GetTimeStepLength();
           //netaccess.PerformTimeStep();

           //t = new Thread(new ThreadStart(RunOne));
           //t.Start();
           //try
           //{
           //    //TimerCallback是一个委托类型,第三个参数是开始计时,每四参数是间隔长(以ms为单位).
           //    time = new System.Threading.Timer(new TimerCallback(method), null, 0, 1000);
           //}
           //catch (Exception ee)
           //{
           //    // System.Web.HttpContext.Current.Response.Write(ee.Message + "<\br>");
           //}


           //ModelYunBLL.ResultLogs BllResultLogs = new ModelYunBLL.ResultLogs();
           //ModelResultLogs = new ModelYunModel.ResultLogs();
           //ModelYunBLL.ModelInfo BllModelInfo = new ModelYunBLL.ModelInfo();
           //ModelYunModel.ModelInfo ModelModelInfo = new ModelYunModel.ModelInfo();

           //ModelResultLogs.ID = Guid.NewGuid().ToString();
           ////ModelTypeID
           ////获取模型信息
           //string ModelID = Request["ModelID"];
           //ModelModelInfo = BllModelInfo.GetSimpleModelOne(ModelID);
           //ModelResultLogs.ModelID = ModelModelInfo.TypeID;
           //ModelResultLogs.UserID = "hky";//Request.Cookies["DigitalBasinUserName"].Value;
           //ModelResultLogs.CalTime = DateTime.Now;
           ////备份文件夹路径
           // _FileFolder = HttpContext.Current.Server.MapPath(@"BackUp\"
           //    + ModelID
           //    + "#Time#"
           //    + DateTime.Now.ToString("yyyyMMddHHmmss-ffff")
           //    );
           //ModelResultLogs.FileFolder = _FileFolder;
           ////执行备份Model相关的所有配置文件
           //CopyFolder(HttpContext.Current.Server.MapPath(@"DATA\" + ModelID), _FileFolder);

           //RunOne(_FileFolder);
           //BllResultLogs.AddOne(ModelResultLogs);
           //Response.Write(ModelResultLogs.ID);
       }
        public void method(object o) //注意参数.
        {
            t.Abort(); //终止线程.
            if (!t.IsAlive)
            {
                //终止成功.
                //终止计时器
                //time.Change(Timeout.Infinite, Timeout.Infinite);           
            }
        }
        public void RunOne(string _FileFolder)
        {
            try
            {
                //dllaccess.Initialize();                
                ////Int32 _timeStep = 50;
                ////dllaccess.get_time_step(ref _timeStep, ref _FileFolder);
                ////Response.Write("2<\br>");
                //dllaccess.PerformTimeStep();
                ////Response.Write("3<\br>");
                //CopyFolder(@"C:\out\", _FileFolder + @"\Result");

            }
            catch (Exception e)
            {
                //System.Web.HttpContext.Current.Response.Write(e.Message + "<\br>");
            }
            //if (!(dllaccess.PerformTimeStep()))
            //{
            //    CreateAndThrowException();
            //}
        }
        public void RunTow()
        {

        }
        private void CreateAndThrowException()
        {
            int numberOfMessages = 0;
            numberOfMessages = SimpleRiverEngineDllAccess.GetNumberOfMessages();
            string message = "Error Message from SimpleRiver Fortran Core ";

            for (int i = 0; i < numberOfMessages; i++)
            {
                int n = i;
                StringBuilder messageFromCore = new StringBuilder("                                                        ");
                //SimpleRiverEngineDllAccess.GetMessage(ref n, messageFromCore, (uint)messageFromCore.Length);
                message += "; ";
                message += messageFromCore.ToString().Trim();
            }
            throw new Exception(message);
        }
        /// </summary> 
        /// <param name="sPath">源文件夹路径</param>         
        /// <param name="dPath">目的文件夹路径</param> 
        /// <returns>完成状态：success-完成；其他-报错</returns>         
        public string CopyFolder(string sPath, string dPath)
        {
            string flag = "success";
            try
            {
                // 创建目的文件夹 
                if (!Directory.Exists(dPath))
                {
                    Directory.CreateDirectory(dPath);
                }
                // 拷贝文件 
                DirectoryInfo sDir = new DirectoryInfo(sPath);
                FileInfo[] fileArray = sDir.GetFiles();
                foreach (FileInfo file in fileArray)
                {
                    file.CopyTo(dPath + "\\" + file.Name, true);
                }
                // 循环子文件夹 
                DirectoryInfo dDir = new DirectoryInfo(dPath);
                DirectoryInfo[] subDirArray = sDir.GetDirectories();
                foreach (DirectoryInfo subDir in subDirArray)
                {
                    CopyFolder(subDir.FullName, dPath + "//" + subDir.Name);
                }
            }
            catch (Exception ex)
            {
                flag = ex.ToString();
            }
            return flag;
        }
    }
}