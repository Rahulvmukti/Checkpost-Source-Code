using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using exam.DAL;

namespace exam.Module
{
    public partial class header : System.Web.UI.UserControl
    {
        db_data _boothlist = new db_data();
        public string usertype;
        public int useridentifier;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                useridentifier = Convert.ToInt32(ds.Tables[0].Rows[0]["identifier"].ToString());
            }
            catch (Exception ex)
            {

            }
        }
    }
}