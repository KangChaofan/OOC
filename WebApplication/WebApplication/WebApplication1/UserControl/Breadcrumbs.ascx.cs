using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelTypeServiceReference;

namespace WebApplication1.UserControl
{
    /// <summary>
    /// 面包屑，显示路径
    /// </summary>
    public partial class Breadcrumbs : System.Web.UI.UserControl
    {
        //用于显示模型类型名称
        public List<string> Names;

        protected void Page_Load(object sender, EventArgs e)
        {
            //--
            if(!IsPostBack)
            {
                Names = new List<string>(3);
                GetTitleName(Request.Url.AbsoluteUri);
                if (Request["Type"] == null)
                {
                    Names.Add("");
                    Names.Add("");

                }
                else
                {
                    int typeID = Convert.ToInt32(Request["Type"]);
                    //ModelYunBLL.ModelType typeBLL = new ModelYunBLL.ModelType();
                    ModelTypeServiceClient mts = new ModelTypeServiceClient();
                    //ModelYunModel.ModelType modelType = new ModelYunModel.ModelType();
                    ModelType modelType = new ModelType();
                    modelType = mts.GetTypeByID(typeID);

                    //判断是否是父类
                    if (mts.IsTopType(typeID))
                    {
                        Names.Add(modelType.typeName);
                        Names.Add("");
                    }
                    //非父类
                    else
                    {
                        string topTypeName = mts.GetTypeByID((int)modelType.topId).typeName;
                        //获取父类名称
                        Names.Add(topTypeName);
                        Names.Add(modelType.typeName);
                    }

                }
            }
            
            //--
        }

        //初步判断属于哪个页面
        public string GetTitleName(string URL)
        {
            int startInt = URL.LastIndexOf('/') + 1;
            int endInt = URL.Length - startInt;
            string temp = URL.Substring(startInt, endInt);

            if (URL.IndexOf('?') > 0)
            {
                temp = temp.Substring(0, temp.IndexOf('?'));
                switch (temp)
                {
                    case "introduce_Content.aspx":
                        Names.Add("模型介绍");                        
                        break;
                    case "Single_Model_Content.aspx":
                        Names.Add("单模型计算");
                        break;
                    case "ResultShow_Content.aspx":
                        Names.Add("计算结果显示");
                        break;
                    case "ui.aspx":
                        Names.Add("模型介绍");
                        break;
                    case "statistics.aspx":
                        Names.Add("计算结果显示");
                        break;
                }
                return temp;
            }
            else
            {
                switch (temp)
                {
                    case "Single_Model.aspx":
                        Names.Add("单模型计算");
                        break;
                    case "introduce.aspx":
                        Names.Add("模型介绍");
                        break;
                    case "introduce_Content.aspx":
                        Names.Add("模型介绍");
                        break;
                    case "Single_Model_Content.aspx":
                        Names.Add("单模型计算");
                        break;
                    case "ResultShow_Content.aspx":
                        Names.Add("计算结果显示");
                        break;
                    case "ResultShow.aspx":
                        Names.Add("计算结果显示");
                        break;
                }
                return temp;
            }

        }
    }
}