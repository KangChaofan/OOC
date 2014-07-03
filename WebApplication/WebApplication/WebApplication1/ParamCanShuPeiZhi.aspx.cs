using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Text;

namespace WebApplication1
{
    public partial class ParamCanShuPeiZhi : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ArrayList list = new ArrayList();
                //ModelType modelType = new ModelType();
                //list = modelType.GetTypeList();
                //StringBuilder typeSB = new StringBuilder();
                //foreach (ModelYunModel.ModelType oneModel in list)
                //{
                //    if (oneModel.TopID == 0)
                //    {
                //        typeSB.Append(" <option value=\"" + oneModel.ID + "\">" + "【" + oneModel.TypeName + "】" + "</option> \n\r ");
                //    }
                //    else
                //    {
                //        typeSB.Append(" <option value=\"" + oneModel.ID + "\">" + oneModel.TypeName + "</option> \n\r ");
                //    }

                //}
                //modelTypeListOption.Text = typeSB.ToString();
                //Response.Write(Session["ShuiLiuShiJianBuChang"]);
            }
        }
    }
}