using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;

namespace exam
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.User.Identity.IsAuthenticated || Session["username"] == null)
            {
                Response.Redirect("Default.aspx");
            }
            try
            {
                db_data _data = new db_data();
                int userid = Convert.ToInt32(Session["userid"].ToString());
                string sessionid = Session["Sessionid"].ToString();
                _data.UpdateUserSessionActivityByUserId(userid, sessionid);
            }
            catch
            {
            }
        }
    }
}