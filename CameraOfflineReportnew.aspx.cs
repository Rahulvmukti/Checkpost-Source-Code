using exam.DAL;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class CameraOfflineReportnew : System.Web.UI.Page
    {
        db_data _data = new db_data();
        DataSet ds1 = new DataSet();
        public int stateid
        {
            get
            {
                if (ViewState["stateid"] != null)
                    return (int)ViewState["stateid"];
                else
                    return 0;
            }
            set
            {
                ViewState["stateid"] = value;
            }
        }
        public DataSet dsReport1
        {
            get
            {
                if (ViewState["dsReport1"] != null)
                    return (DataSet)ViewState["dsReport1"];
                else
                    return null;
            }
            set
            {
                ViewState["dsReport1"] = value;
            }
        }
        public DataSet dsReport
        {
            get
            {
                if (ViewState["dsReport"] != null)
                    return (DataSet)ViewState["dsReport"];
                else
                    return null;
            }
            set
            {
                ViewState["dsReport"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                Session["MenuName"] = "ConsolidatedCameraOffline";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);

                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlFromTime.SelectedValue = "00:00:00";
                    ddlToTime.SelectedValue = "23:59:59";
                    LoadDistrict();
                    GetStopCameraList();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void GetStopCameraList()
        {

            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string fromtime = ddlFromTime.SelectedValue;
            string totime = ddlToTime.SelectedValue;
            fromtime = fromtime == "" ? "00:00:00" : fromtime; 
            string todaydate = DateTime.Now.ToString("yyyy-MM-dd");

            if (todate.ToString() == todaydate.ToString())
            {
                totime = DateTime.Now.ToString("HH:00:00"); 
                lblmsg.Text = "Report Till  " + totime;
            }
            else
            {
              
                lblmsg.Text = "";
            }
            string usertype=Session["usertypeDataSet"].ToString();

            if (usertype == "District_Level")
            {
                string distname = Session["DistrictDataSet"].ToString();
                ddlDistrict.SelectedValue = distname;
                ddlDistrict.Enabled = false;
            }
            DataSet ds = _data.GetCameraOfflineList11(fromdate + " " + fromtime, todate + " " + totime, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, "", "");
            dsReport1 = ds; 

        }

        private void LoadDistrict()
        {
            try
            {
                var Alldist = _data.GetAllDistrictByStateId(stateid);

                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("All District", ""));
                ddlAssembly.Items.Insert(0, new ListItem("All Assembly", "")); 
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  LoadDistrict()" + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var Assembly = _data.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "accode";
                ddlAssembly.DataBind();
                ddlAssembly.Items.Insert(0, new ListItem("All Assembly", ""));

                GetStopCameraList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  ddlDistrictOnSelectedIndexChanged()" + ex.Message);
            }
        }
         

        protected void btnsearch_Click(object sender, EventArgs e)
        { 
            GetStopCameraList();
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GetStopCameraList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }
    }
}