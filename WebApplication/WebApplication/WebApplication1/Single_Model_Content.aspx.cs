using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebApplication1.ModelServiceReference;
using WebApplication1.ModelTypeServiceReference;

namespace WebApplication1
{
    public partial class Single_Model_Content : System.Web.UI.Page
    {
        public string TopModelName = "";
        public string ThisModelName = "";
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
            //全局变量，用于记录需要备份的所属文件夹名称（模型ID）
            Application["CalModel"] = "";
        }

        /// <summary>
        /// 根据类型完整表查询出模型名称
        /// </summary>
        /// <param name="ModelID"></param>
        /// <param name="ds"></param>
        /// <returns></returns>
        public string GetModelNameByID(string ModelID, System.Data.DataSet ds)//没用
        {
            string ss = ds.Tables[0].Select("ID=" + ModelID)[0][1].ToString();
            return ss;
        }
        public void GetAllModelListForHTML(Literal lit)
        {
            //ModelYunBLL.ModelInfo _modelInfoBll = new ModelYunBLL.ModelInfo();
            ModelServiceClient ms = new ModelServiceClient();
            List<Model> list = new List<Model>(); 
            list.AddRange(ms.ModelSimpleList());
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
           // ModelYunBLL.ModelType _ModelTypeBLL = new ModelYunBLL.ModelType();
            ModelTypeServiceClient mts = new ModelTypeServiceClient();
            //System.Data.DataSet ModelTypeDS = mts.GetTypeListDS();获取类型列表，这里无用
            //获取模型名称

            foreach (Model one in list)
            {
               // string _ModelTypeName = GetModelNameByID(one.TypeID, ModelTypeDS);
                ModelType mt = mts.GetTypeByID((Int32)one.typeId);
                //ModelProperty mp = ms.GetRiverBasinByModelGuid(one.guid);
                sb.Append(" <tr>");
                sb.Append(" <td>" + one.name + "</td>");
                sb.Append(" <td>" + one.eName + "</td>");
                sb.Append(" <td id=\"ModelTypeName\" name=\"" + one.typeId + "\">" + mt.typeName + "</td>");
                sb.Append(" <td>" + one.@abstract + "</td>");
                sb.Append(" <td>" + one.riverBasin+ "</td>");//直接从model中取
                //sb.Append(" <td><input type=\"button\" class=\"buttonS bLightBlue\" id=\""+one.ID+"\" name=\"" + one.ModelName + "\" value=\"配置计算\" /> </td>");
                sb.Append(" <td><input type=\"button\" class=\"buttonS bLightBlue\" id=\"" + one.guid + "\" name=\"" + one.name + "\" value=\"配置计算\" alt=\"" + one.typeId + "\" /> </td>");
                sb.Append("</tr>");
            }
            lit.Text = sb.ToString();
        }
        public void GetModelListForHTMLByTypeID(Literal lit, int _TypeID)
        {
            //ModelYunBLL.ModelInfo _modelInfoBll = new ModelYunBLL.ModelInfo();
            ModelServiceClient ms = new ModelServiceClient();
            //ModelYunBLL.ModelType _modelType = new ModelYunBLL.ModelType();
            ModelTypeServiceClient mts = new ModelTypeServiceClient();
            System.Collections.ArrayList list = new System.Collections.ArrayList();

            if (mts.IsTopType(_TypeID))
            {
                list.AddRange( ms.ModelSimpleListByTopID(_TypeID));
            }
            else
            {
                list.AddRange(ms.ModelSimpleListByTypeID(_TypeID));
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            //ModelYunBLL.ModelType _ModelTypeBLL = new ModelYunBLL.ModelType();
            //System.Data.DataSet ModelTypeDS = _ModelTypeBLL.GetTypeListDS();
            foreach (Model one in list)
            {
                //string _ModelTypeName = GetModelNameByID(one.TypeID, ModelTypeDS);
                ModelType mt = mts.GetTypeByID((Int32)one.typeId);
<<<<<<< HEAD
                //ModelProperty mp = ms.GetRiverBasinByModelGuid(one.guid);
=======
                ModelProperty mp = ms.GetRiverBasinByModelGuid(one.guid);
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
                sb.Append(" <tr>");
                sb.Append(" <td>" + one.name + "</td>");
                sb.Append(" <td>" + one.eName + "</td>");
                sb.Append(" <td id=\"ModelTypeName\" name=\"" + one.typeId + "\">" + mt.typeName + "</td>");
                sb.Append(" <td>" + one.@abstract + "</td>");
<<<<<<< HEAD
                //sb.Append(" <td>" + mp.@default+ "</td>");
                sb.Append(" <td>" + one.riverBasin + "</td>");
=======
                sb.Append(" <td>" + mp.@default+ "</td>");
>>>>>>> 0daec768afcc757c83c424118f28374d34e3dc64
                sb.Append(" <td><input type=\"button\" class=\"buttonS bLightBlue\" id=\"" + one.guid+ "\" name=\"" + one.name + "\" value=\"配置计算\" alt=\"" + one.typeId+ "\" /> </td>");
                sb.Append("</tr>");
            }
            lit.Text = sb.ToString();
        }
    }
}