using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class ForSession : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string ContentName = Request["ContentName"];
            string _value = Request["Value"];
            if (IsHaveSession(ContentName) && _value == null)
            {
                Response.Write(Session[ContentName]);
            }
            else
            {
                try
                {
                    string _Value = Request["Value"];
                    Session.Remove(ContentName);
                    SetSession(ContentName, _Value);
                    Response.Write("SetOK");
                }
                catch
                {
                    Response.Write("Read");
                }
            }
        }

        //设置session
        public void SetSession(string _SessionName,string _Value)
        {
            Session[_SessionName] = _Value;
        }

        //session是否存在
        public bool IsHaveSession(string _SessionName)
        {
            if (Session[_SessionName] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //清除所有session
        public void SessionClear()
        {
            Session.Clear();
        }
        //清除某个session
        public void SessionClear(string SessionName)
        {
            Session[SessionName] = "";
        }
    }
}