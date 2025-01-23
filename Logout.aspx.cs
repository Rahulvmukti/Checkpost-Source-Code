using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using exam.DAL;

namespace exam
{
    public partial class Logout : System.Web.UI.Page
    {
        db_data _user = new db_data();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                try
                {
                    int userid = Convert.ToInt32(Session["userid"].ToString());
                    string sessionid = Session["SessionId"].ToString();
                    _user.deleteuserbysessionid(userid, sessionid);
                }
                catch
                {
                }
                _user.updateloginstatus(false, Page.User.Identity.Name.ToString());
                FormsAuthentication.SignOut();
                Session.Clear();
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                Response.Cache.SetNoStore();
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Response.Redirect("Default.aspx", false);
            }
        }
    }
}