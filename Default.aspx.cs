using System; 
using System.Web;
using System.Web.UI; 
using System.Data;
using exam.DAL;
using System.Web.Security;
using System.Configuration;

namespace exam
{
    public partial class Default : System.Web.UI.Page
    {
        db_data _user = new db_data();
        CallSP _callSP = new CallSP();
        public string defaultpage = ConfigurationManager.AppSettings["defaultpage"].ToString(); 
        public int logincount = Convert.ToInt32(ConfigurationManager.AppSettings["logincount"].ToString());
        public int sessiontimeout = Convert.ToInt32(ConfigurationManager.AppSettings["sessiontimeout"].ToString());
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                 
                if (Page.User.Identity.IsAuthenticated && Session["userType"] != null)
                {
                    DataSet ds = _user.GetUserData(Page.User.Identity.Name);
                    Response.Redirect(defaultpage);
                } 
            }
            catch (Exception ex)
            {
            }
        }

        protected void btn_Login_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet ds = _callSP.GetFolderListWithCount(txtUsername.Text, txtPassword.Text);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string SessionId = "";
                    int userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
                    string usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    int loginallowedcount = Convert.ToInt32(ds.Tables[0].Rows[0]["loginallowedcount"].ToString());
                    logincount = loginallowedcount > 0 ? loginallowedcount : logincount;
                    try
                    {
                        DataSet dsSessionId = _user.SaveLoginUserHistory(Convert.ToInt32(ds.Tables[0].Rows[0]["id"]), System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                        SessionId = dsSessionId.Tables[0].Rows[0]["SessionId"].ToString();
                        Session["SessionId"] = SessionId;
                    }
                    catch { }
                    DataSet dsCount = _user.getuserbysessionid(userid, SessionId);
                    int ActiveSessionCount = Convert.ToInt32(dsCount.Tables[0].Rows[0]["SessionCount"].ToString());

                    if (ActiveSessionCount > logincount && Convert.ToBoolean(ds.Tables[0].Rows[0]["logincountenable"]))
                    {
                        //lblMsg.Visible = true;
                        //lblMsg.Text = ResponseReturnMessages.Contact_Administrator;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr", "$(function () { toastr.error('" + ResponseReturnMessages.LoginAttemptExceed + "'); });", true);
                    }
                    else
                    {
                        Session["userid"] = ds.Tables[0].Rows[0]["id"].ToString();
                        Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                        Session["AssemblyAccesIds"] = ds.Tables[0].Rows[0]["AssemblyAccesIds"].ToString();
                        Session["userType"] = ds.Tables[0].Rows[0]["usercode"].ToString();
                        string usrty = ds.Tables[0].Rows[0]["usercode"].ToString();
                        if (usrty.StartsWith("dst_"))
                        {
                            Session["userdist"] = usrty.Split('_')[1];
                        }
                        else if (usrty.StartsWith("sch_"))
                        {
                            Session["userdist"] = usrty.Split('_')[1];
                            Session["userpc"] = usrty.Split('_')[2];
                            Session["userbooth"] = usrty.Split('_')[3];
                        }

                        int state_id_user = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"].ToString());
                        if (state_id_user == 1)
                        {
                            Session["userAssemblyAccess"] = ds.Tables[1];
                            setuser_cookie(txtUsername.Text);
                            int useridentifier = Convert.ToInt32(ds.Tables[0].Rows[0]["identifier"].ToString());
                            Response.Redirect(defaultpage, true);

                        }
                        else
                        {
                            //lblMsg.Visible = true;
                            //lblMsg.Text = ResponseReturnMessages.InvalidCredential;
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr", "$(function () { toastr.error('" + ResponseReturnMessages.InvalidCredential + "'); });", true);

                        }

                    }
                }
                else
                {
                    //lblMsg.Visible = true;
                    //lblMsg.Text = "Invalid Username or Password";

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr", "$(function () { toastr.error('" + ResponseReturnMessages.InvalidCredential + "'); });", true);

                }

            }
            catch (Exception ex)
            {
                Common.Log("btn_Login_Click() -- >  " + ex.Message);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "toastr", "$(function () { toastr.error('" + ResponseReturnMessages.Contact_Administrator + "'); });", true);
            }
        }
        private void setuser_cookie(string uname)
        {
            FormsAuthenticationTicket _ticket = new FormsAuthenticationTicket(1, uname, DateTime.Now, DateTime.Now.AddMonths(1), false, uname, FormsAuthentication.FormsCookiePath);
            string encTicket = FormsAuthentication.Encrypt(_ticket);
            HttpCookie _cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            _cookie.Expires = DateTime.Now.AddHours(sessiontimeout);
            Response.Cookies.Add(_cookie);
            _user.updatelogincount(uname);
        }
    }
}