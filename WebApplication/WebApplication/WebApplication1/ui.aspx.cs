using System;
using System.Collections.Generic;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebApplication1.ModelServiceReference;

namespace WebApplication1
{
    public partial class ui : System.Web.UI.Page
    {
        public Model modelOne;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request["ModelID"] != null)
                {
                    modelOne = new Model();
                    ModelServiceClient ms = new ModelServiceClient();
                    modelOne = ms.GetByGuid(Request["ModelID"]);
                }
            }
        }
    }
}