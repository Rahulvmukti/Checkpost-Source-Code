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
    public partial class CameraStatusReport : System.Web.UI.Page
    {
        db_data _data = new db_data(); 
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

                Session["MenuName"] = "ConsolidatedCameraStatus";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);
                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ddlFromTime.SelectedValue = "00:00:00";
                    ddlToTime.SelectedValue = "23:59:59";
                    LoadDistrict();
                    GetStopCameraTotalList();
                } 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetStopCameraTotalList()
        { 
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string fromtime = ddlFromTime.SelectedValue;
            string totime = ddlToTime.SelectedValue;
            fromtime = fromtime == "" ? "00:00:00" : fromtime;
             //   totime = totime == "" ? "23:59:59.999" : totime;
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
            string usertype = Session["usertypeDataSet"].ToString();
            if (usertype == "District_Level")
            {
                string distname = Session["DistrictDataSet"].ToString();
                ddlDistrict.SelectedValue = distname;
                ddlDistrict.Enabled = false;
            } 
            DataSet ds = _data.GetCameraStatusReport(fromdate + " " + fromtime, todate + " " + totime, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue,"");
            dsReport = ds; 
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

                ddlDistrict.Items.Insert(0, new ListItem("Select District", ""));
                ddlAssembly.Items.Insert(0, new ListItem("Select Assembly", ""));  
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
                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  ddlDistrictOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }
         
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetStopCameraTotalList();
        } 
    }
}