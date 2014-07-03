using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;
using WebApplication1.ResultLogsServiceReference;
using WebApplication1.UserServiceReference;

namespace WebApplication1
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            string Method = Request["Method"];
            //保存文件时用到的模型ID，用于分类目录
            string ModelIDParam = "";
            try
            {
                ModelIDParam = Request["ModelID"];
            }
            catch { }

            if (Method == "saveOMI")
            {
                saveFile(this.Context, "UploadOMI");
            }
            else if (Method == "saveDLL")
            {
                saveFile(this.Context, "UploadDLL");
            }
            else if (Method == "ModelInfo")
            {
                //ModelYunModel.ModelInfo _oneModel = new ModelYunModel.ModelInfo();
                Model _oneModel = new Model();
                _oneModel.name = Request["ModelName"];
                _oneModel.typeId = Convert.ToInt32(Request["SelectModelType"]);
               // _oneModel.ModelDllName = Request["ModelDllName"];
               // _oneModel.ResultStartRow = Convert.ToInt32(Request["ResultStartRow"]);
               // _oneModel.OMIFileName = Request["uploadifyNote"];
               // ModelYunBLL.ModelInfo _modelBLL = new ModelYunBLL.ModelInfo();
                ModelServiceClient ms = new ModelServiceClient();
                ms.Create(_oneModel.name,_oneModel.version,Convert.ToInt32(_oneModel.authorUserId),_oneModel.className,(Int32)_oneModel.topId,(Int32)_oneModel.typeId);
                Response.Write("OK");
            }
            switch (Method)
            {
                case "Login":
                    string username = Request["UserName"];
                    string password = Request["Password"];
                    int userID = 0;
                    //ModelYunBLL.Users UsersBll = new ModelYunBLL.Users();
                    UserServiceClient UsersBll = new UserServiceClient();
                    bool IsAccountOK = UsersBll.Auth(username, password);
                    userID = Convert.ToInt32(UsersBll.GetByUsername(username).id);
                    if (IsAccountOK == true)
                    {
                        Response.Write("OK");
                        Response.Cookies["DigitalBasinUserName"].Value = username;
                        Response.Cookies["DigitalBasinUserName"].Expires = DateTime.MaxValue;
                        Response.Cookies["DigitalBasinUserID"].Value = userID.ToString();
                        Response.Cookies["DigitalBasinUserID"].Expires = DateTime.MaxValue;

                    }
                    else
                    {
                        Response.Write("Not OK");
                    }                    
                    break;
                case "saveOMI":
                    saveFile(this.Context, "UploadOMI");
                    break;
                case "saveDLL":
                    saveFile(this.Context, "UploadDLL");
                    break;

                case "ModelInfo":
                   // ModelYunModel.ModelInfo _oneModel = new ModelYunModel.ModelInfo();
                    Model _oneModel = new Model();
                    _oneModel.name = Request["ModelName"];
                    _oneModel.typeId=Convert.ToInt32(Request["SelectModelType"]);
                    //_oneModel.ModelDllName = Request["ModelDllName"];
                    //_oneModel.ResultStartRow = Convert.ToInt32(Request["ResultStartRow"]);
                    //_oneModel.OMIFileName = Request["uploadifyNote"];
                    //ModelYunBLL.ModelInfo _modelBLL = new ModelYunBLL.ModelInfo();
                    ModelServiceClient _modelBLL = new ModelServiceClient();
                    _modelBLL.Create(_oneModel.name, _oneModel.version, Convert.ToInt32(_oneModel.authorUserId), _oneModel.className, (Int32)_oneModel.topId, (Int32)_oneModel.typeId);
                    Response.Write("OK");
                    break;

                //参数配置-------------------------------------Start-------------|
                case "canshupeizhi":
                    saveFile(this.Context, "DATA\\" + ModelIDParam + "\\canshupeizhi");
                    break;
                    //参数配置
                case "CanshupeizhiSubmit":
                    //水流时间步长（秒）
                    string buchang = Request["BuChang"];
                    Response.Write("OK");
                    break;
                //--------参数配置--------End    

                //边界控制-------------------------------------Start-------------|
                case "bianjiekongzhi_DiXing":
                    saveFile(this.Context, "DATA\\" + ModelIDParam + "\\bianjiekongzhi\\bianjiekongzhi_DiXing");
                    break;
                //边界控制-------------------------------------Start-------------|
                case "bianjiekongzhi_RuKou":
                    saveFile(this.Context, "DATA\\" + ModelIDParam + "\\bianjiekongzhi\\bianjiekongzhi_RuKou");
                    break;
                case "BianjiekongzhiSubmit":
                    Response.Write("OK");
                    break;
                //--------边界控制--------End    

                //初始条件-------------------------------------Start-------------|
                case "chushitiaojian":
                    saveFile(this.Context, "DATA\\" + ModelIDParam + "\\chushitiaojian");
                    break;
                case "ChushitiaojianSubmit":
                    Response.Write("OK");
                    break;
                //--------初始条件--------End    

                //过程控制-------------------------------------Start-------------|
                case "guochengkongzhi":
                    saveFile(this.Context, "DATA\\" + ModelIDParam + "\\guochengkongzhi");               
                    break;
                case "GuochengkongzhiSubmit":
                    Response.Write("OK");
                    break;
                //--------过程控制--------End    
            }
        }



 

        //上传文件所用服务器端程序（并覆盖同名文件）
        public string saveFile(HttpContext context,string fileDir)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Charset = "utf-8";

            HttpPostedFile file = context.Request.Files["Filedata"];
            string uploadPath = HttpContext.Current.Server.MapPath(@context.Request["folder"]) + "\\" + fileDir + "\\";
            if (file != null)
            {
                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                file.SaveAs(uploadPath + file.FileName);
                //下面这句代码缺少的话，上传成功后上传队列的显示不会自动消失
                context.Response.Write(file.FileName);
            }
            else
            {
                context.Response.Write("0");
            }

            return file.FileName;
        }

        /// <summary>
        /// 新增模型
        /// </summary>
        /// <param name="modelInfo"></param>
        public void AddModel(Model  model)
        {
            //ModelYunBLL.ModelInfo modelBLL = new ModelYunBLL.ModelInfo();
            ModelServiceClient ms=new ModelServiceClient();
            //modelBLL.AddModel(modelInfo);
            ms.Create(model.name, model.version, Convert.ToInt32(model.authorUserId), model.className, (Int32)model.topId, (Int32)model.typeId);
        }
    }
}