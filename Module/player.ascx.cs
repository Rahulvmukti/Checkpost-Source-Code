using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;

namespace exam.Module
{
    public partial class player : System.Web.UI.UserControl
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
        public string flag
        {
            get;
            set;
        }
        public string sid
        {
            get;
            set;
        }
        public string streamnamebkp
        {
            get;
            set;
        }

        public string accode
        {
            get;
            set;
        }
        public string psnum
        {
            get;
            set;
        }
        public string kbps
        {
            get;
            set;
        }
        public string mobno
        {
            get;
            set;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Convert.ToInt32(flag) == 2)
                //{
                //    db_list_lq db = new db_list_lq();
                //    streamlist info = db.Getstreaminfo(Convert.ToInt32(sid));
                //    if (info != null)
                //    {
                //        playerURL = info.servername;
                //        if (playerid.Contains("_"))

                //            playerid = playerid.Split('_')[0] + "_" + info.streamname;
                //    }
                //}
                if (Convert.ToInt32(flag) == 4)
                {
                    playerid = playerid.Split('_')[0] + "__" + playerid.Split('_')[1];
                }
                //if (Convert.ToInt32(flag) == 2)
                //{
                //    playerid = playerid.Split('_')[0] + "_" + streamnamebkp == "" ? playerid.Split('_')[1] : streamnamebkp;
                //}
            }
            catch (Exception ex)
            {
            }
        }

    }
}