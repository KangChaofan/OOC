using System;
using System.Collections.Generic;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;

namespace WebApplication1
{
    public partial class introduce_Content : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request["Type"] == null)
                {
                    GetAllModelListForHTML(forModelList);
                }
                else
                {
                    int TypeID = Convert.ToInt32(Request["Type"]);
                    GetModelListForHTMLByTypeID(forModelList, TypeID);
                }
            }
            catch
            {

            }
        }
        public void GetAllModelListForHTML(Literal lit)
        {
            ModelServiceClient ms = new ModelServiceClient();
            List<Model> list = new List<Model>(ms.ModelSimpleList());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (Model one in list)
            {
                sb.Append(" <tr>");
                sb.Append(" <td>" + one.name + "</td>");
                sb.Append(" <td>" + one.eName + "</td>");
                sb.Append(" <td>" + one.@abstract + "</td>");
                sb.Append(" <td>" + one.riverBasin + "</td>");
                sb.Append(" <td><input id=\"" + one.guid + "\" type=\"button\" class=\"buttonS bGreen\" name=\"" + one.typeId + "\" value=\"查看\"></td>");
                sb.Append("</tr>");
            }
            lit.Text = sb.ToString();
        }



        public void GetModelListForHTMLByTypeID(Literal lit, int _TypeID)
        {
            ModelServiceClient ms = new ModelServiceClient();
            ModelTypeServiceClient mts = new ModelTypeServiceClient();
            List<Model> list = new List<Model>();

            if (mts.IsTopType(_TypeID))
            {
                list = new List<Model>(ms.ModelSimpleListByTopID(_TypeID));
            }
            else
            {
                list = new List<Model>(ms.ModelSimpleListByTypeID(_TypeID));
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (Model one in list)
            {
                sb.Append(" <tr>");
                sb.Append(" <td>" + one.name + "</td>");
                sb.Append(" <td>" + one.eName + "</td>");
                sb.Append(" <td>" + one.@abstract + "</td>");
                sb.Append(" <td>" + one.riverBasin + "</td>");
                sb.Append(" <td><input id=\"" + one.guid + "\" type=\"button\" class=\"buttonS bGreen\" name=\"" + one.typeId + "\" value=\"查看\"></td>");
                sb.Append("</tr>");
            }
            lit.Text = sb.ToString();
        }

    }
}