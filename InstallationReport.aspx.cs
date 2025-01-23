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
    public partial class InstallationReport : System.Web.UI.Page
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
        public DataSet dsReport1 { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }

                Session["MenuName"] = "ExcelReport";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);
                if (!IsPostBack)
                {
                    
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
            DataSet dsColName = new DataSet();
            string fromdate = "";// FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = "";// FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];

            //ddlFromTime.Enabled = true;
            //ddlToTime.Enabled = true;
            dsReport1 = ds1;
            string fromtime = "";// ddlFromTime.SelectedValue;
            string totime = "";// ddlToTime.SelectedValue;
            fromtime = fromtime == "" ? "" : fromtime;
            totime = totime == "" ? "" : totime;
            DataSet ds = _data.GetExcelReport(fromdate + " " + fromtime, todate + " " + totime, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, "", Convert.ToInt32(ddlislive.SelectedValue), ddlStatus.SelectedValue);
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
                //ddlcamara.Items.Insert(0, new ListItem("Select Vehicle", ""));

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
              //  ddlcamara.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("All Assembly", ""));
              //  ddlcamara.Items.Insert(0, new ListItem("All Vehicle", ""));
                
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
                //var vehicle = _data.GetAllVehicleByAcCode(ddlAssembly.SelectedItem.Value);

                //ddlcamara.Items.Clear();
                //ddlcamara.DataSource = vehicle;
                //ddlcamara.DataTextField = "Vehicle_no";
                //ddlcamara.DataValueField = "Vehicle_no";
                //ddlcamara.DataBind();
                //ddlcamara.Items.Insert(0, new ListItem("All Vehicle", ""));

                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  AssemblyDropDownOnSelectedIndexChanged()" + ex.Message);
            }
        }

        protected void ddlcamara_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("VehicleStartStopReport -- >  ddlcamara_SelectedIndexChanged()" + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetStopCameraTotalList();
        }

         
        protected void btnSetting_Click(object sender, EventArgs e)
        {
            dsReport1 = ds1;
        }
      
        protected void ddlislive_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GetStopCameraTotalList();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GetStopCameraTotalList();
        }
    }
}