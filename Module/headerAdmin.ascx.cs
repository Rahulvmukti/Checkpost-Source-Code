using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;
using System.Data;

namespace exam.Module
{
    public partial class headerAdmin : System.Web.UI.UserControl
    {
        db_data_admin _boothlist = new db_data_admin();
        public int useridentifier;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                useridentifier = Convert.ToInt32(ds.Tables[0].Rows[0]["identifier"].ToString());
            }
            catch (Exception ex)
            {
            }
        }
    }
}