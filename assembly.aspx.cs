using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using exam.DAL;
using System.Configuration;

namespace exam
{
    public partial class assembly : System.Web.UI.Page
    {
        public string pcname
        {
            get;
            set;
        }
        db_data_admin _dbadmin = new db_data_admin();
        public string strcode = "000";
        public DataSet BoothList
        {
            get
            {
                if (ViewState["BoothList"] != null)
                    return (DataSet)ViewState["BoothList"];
                else
                    return BoothList;
            }
            set
            {
                ViewState["BoothList"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Page.User.Identity.IsAuthenticated && Session["userType"] != null)
                {

                    if (Request.QueryString["pcname"] != null)
                    {
                        pcname = Common.Decode(Request.QueryString["pcname"]);
                        //if (pcname.Contains("-"))
                        //{
                        //    pcname = pcname.Split('-')[0];
                        //}
                        strcode = pcname.Split('$')[0];
                        pcname = pcname.Split('$')[1];
                    }
                    if (!string.IsNullOrEmpty(pcname))
                    {
                        if (!IsPostBack)
                        {
                            int UserID = 0;
                            if (Session["userid"] != null)
                                UserID = Convert.ToInt32(Session["userid"]);
                            string allKeyword = ConfigurationManager.AppSettings["AllSelectKeword"].ToString() + " ";
                            string districtname = ConfigurationManager.AppSettings["district"].ToString();
                            DataSet dspc = null;
                            if(Session["MenuName"].ToString().Equals("CameraStatusOutdoor"))
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID,"Outside");
                            else if (Session["MenuName"].ToString().Equals("CameraStatusIndoor"))
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID, "Inside");
                            else if (Session["MenuName"].ToString().Equals("CameraStatusPink"))
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID, "", 1);
                            else
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID);
                            BoothList = dspc;
                            //listview1.DataSource = dspc;
                            //listview1.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        //protected void Timer1_Tick(object sender, EventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}