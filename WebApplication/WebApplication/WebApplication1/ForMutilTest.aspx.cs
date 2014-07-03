using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

namespace WebApplication1
{
    public partial class ForMutilTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string flag="";
            string ModelID = "";
            flag = CopyFolder(HttpContext.Current.Server.MapPath(@"DATA\"+ModelID), HttpContext.Current.Server.MapPath(@"BackUp\"+ModelID+ "#Time#" + DateTime.Now.ToString("yyyyMMddHHmmss-ffff")));

            if (flag == "success")
            {
                Response.Write("OK");
            }
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