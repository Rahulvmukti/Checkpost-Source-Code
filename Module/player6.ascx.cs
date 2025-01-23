using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace exam.Module
{
    public partial class player6 : System.Web.UI.UserControl
    {
        public string playerid
        {
            get;
            set;
        }
        public string playerURL
        {
            get;
            set;
        }
        public string playerstateid
        {
            get;
            set;
        }
        public string currentstateid;
        protected void Page_Load(object sender, EventArgs e)
        {
            currentstateid = ConfigurationManager.AppSettings["stateid"];
        }
    }
}