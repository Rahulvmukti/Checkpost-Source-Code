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
    public partial class assemblydashboard : System.Web.UI.Page
    {
        public string pcname
        {
            get;
            set;
        }
        public string type
        {
            get;
            set;
        }
        db_data_admin _dbadmin = new db_data_admin();
        db_data _boothlist = new db_data();
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
                            if (Request.QueryString["type"] != null)
                            {
                                type = Request.QueryString["type"];// Common.Decode(Request.QueryString["type"]);
                            }
                            if (type.ToLower() == "outside")
                            {
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID, "Outside");
                            }
                            else if (type.ToLower() == "inside")
                            {
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID, "Inside");
                            }
                            else if (type.ToLower() == "pink")
                            {
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID, "", 1);
                            }
                            else
                            {
                                dspc = _dbadmin.allcountlive_assemblynew(pcname, UserID);
                            }
                            BoothList = dspc;
                            grdDashboardDetail.DataSource = dspc;
                            grdDashboardDetail.DataBind();
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
        protected void grdDashboardDetail_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                string cmdarg = e.CommandArgument.ToString();
                string data = cmdarg.Split('|')[0];
                type = cmdarg.Split('|')[1];
                if (e.CommandName == "Online")
                {
                    DataSet ds = _boothlist.GetDashboardDetailListByAssembly(data, "RUNNING", type);
                    //divDashboardDetails
                    grdDashboardDetails.DataSource = ds;
                    grdDashboardDetails.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$j('form').find('.ui-dialog').remove();$j('#" + divDashboardDetails.ClientID + "').dialog({ title: 'Assembly: " + e.CommandArgument.ToString() + " | Status: " + e.CommandName.ToString() + "', width: 1000, height: 400, position: 'center' });$j('#" + divDashboardDetails.ClientID + "').parent().appendTo($('form'));$(window).scrollTop(0)", true);
                }
                else if (e.CommandName == "Offline")
                {
                    DataSet ds = _boothlist.GetDashboardDetailListByAssembly(data, "STOPPED", type);
                    //divDashboardDetails
                    grdDashboardDetails.DataSource = ds;
                    grdDashboardDetails.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$j('form').find('.ui-dialog').remove();$j('#" + divDashboardDetails.ClientID + "').dialog({ title: 'Assembly: " + e.CommandArgument.ToString() + " | Status: " + e.CommandName.ToString() + "', width: 1000, height: 400, position: 'center' });$j('#" + divDashboardDetails.ClientID + "').parent().appendTo($('form'));$(window).scrollTop(0)", true);
                }
                else if (e.CommandName == "Total")
                {
                    DataSet ds = _boothlist.GetDashboardDetailListByAssembly(data, "", type);
                    //divDashboardDetails
                    grdDashboardDetails.DataSource = ds;
                    grdDashboardDetails.DataBind();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "$j('form').find('.ui-dialog').remove();$j('#" + divDashboardDetails.ClientID + "').dialog({ title: 'Assembly: " + e.CommandArgument.ToString() + " | Status: " + e.CommandName.ToString() + "', width: 1000, height: 400, position: 'center' });$j('#" + divDashboardDetails.ClientID + "').parent().appendTo($('form'));$(window).scrollTop(0)", true);
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