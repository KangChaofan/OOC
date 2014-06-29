using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using WebApplication1.ModelTypeServiceReference;

namespace WebApplication1.UserControl
{
    public partial class subNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ModelYunBLL.ModelType modelTypeBLL = new ModeModelType();
            ModelTypeServiceClient mts = new ModelTypeServiceClient(); 
            //ArrayList Toplist = modelTypeBLL.GetTopList();
            List<ModelType> mtl = new List<ModelType>();
            mtl.AddRange(mts.GetTopList());
            StringBuilder sb = new StringBuilder();
            sb.Append("<ul class=\"subNav\">");
            foreach (var oneType in mtl)
            {
                List<ModelType> sublist = new List<ModelType>();
                sublist.AddRange(mts.GetSubByTopID(oneType.id));
                string TopPage = DBHelper.Tools.GetBelongTo(Request.Url.AbsoluteUri);
                sb.Append("<li><a value=\"" + DBHelper.Tools.GetUrlHost(Request.Url.AbsoluteUri) + TopPage + "" + "?Type=" + oneType.id.ToString() + "\" href=\"#\" class=\"exp\"><span class=\"icos-list\"></span>" + oneType.typeName + IsNumShow(sublist.Count) + "</a>");
                sb.Append("<ul>");
                foreach (var subType in sublist)
                {
                    string SubPage = DBHelper.Tools.GetBelongTo(Request.Url.AbsoluteUri);
                    sb.Append("<li><a  value=\"" + DBHelper.Tools.GetUrlHost(Request.Url.AbsoluteUri) + SubPage + "" + "?Type=" + subType.id.ToString() + "\" href=\"#\">" + subType.typeName + "</a></li>");
                }
                sb.Append("</ul>");
                sb.Append("</li>");
            }
            sb.Append("</ul>");
            LitModelType.Text = sb.ToString();
        }
        /// <summary>
        /// 判断子类数量，若无子类则不显示右侧标记的数字
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public string IsNumShow(int i)
        {
            string cssStr = "";
            if (i >= 1)
            {
                cssStr = "<span class=\"dataNumBlue\">" + i.ToString() + "</span>";
            }
            else
            {
                cssStr = "";
            }
            return cssStr;
        }
    }
}