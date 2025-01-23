using exam.DAL;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class ExcelReport : System.Web.UI.Page
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
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    ToDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    LoadZone();
                    LoadDistrict(ddlzone.SelectedItem.Value);
                    LoadMasterPC();
                  
                    GetStopCameraTotalList();
                }
                // ScriptManager1.RegisterPostBackControl(this.btnExportPDF);
                //ScriptManager1.RegisterPostBackControl(this.btnsearch);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void GetStopCameraTotalList()
        {
            DataSet dsColName = new DataSet();
            string fromdate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string todate = FromDt.Text.Trim().Split(new char[] { '/', '-' })[2] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[1] + "-" + FromDt.Text.Trim().Split(new char[] { '/', '-' })[0];
            string date = FromDt.Text;
            string toDate = ToDt.Text;
            string inputFormat = "dd/MM/yyyy"; // Adjust this format to match the format in your textbox

            // Try to parse the date input using the specified format
            if (DateTime.TryParseExact(date, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
            {
                // Format the date in "yyyy-MM-dd"
                string formatteodDate = parsedDate.ToString("yyyy-MM-dd");

                if (DateTime.TryParseExact(toDate, inputFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedToDate))
                {
                    // Format the toDate in "yyyy-MM-dd"
                    string formattedToDate = parsedToDate.ToString("yyyy-MM-dd");
                    Console.WriteLine($"Formatted To Date: {formattedToDate}");

                    //ddlFromTime.Enabled = true;
                    //ddlToTime.Enabled = true;
                    dsReport1 = ds1;
                    //string fromtime = ddlFromTime.SelectedValue;
                    //  string totime = ddlToTime.SelectedValue;
                    //  fromtime = fromtime == "" ? "" : fromtime;
                    //  totime = totime == "" ? "" : totime; 
                    DataSet ds = _data.GetExcelReport1(ddlzone.SelectedItem.Value, formatteodDate, formattedToDate, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, ddlcamara.SelectedValue, Convert.ToInt32(ddlislive.SelectedValue), ddlStatus.SelectedItem.Value);
                    dsReport = ds;
                }
            }
        }

        private void LoadDistrict(string zone)
        {
            try
            {
                var Alldist = _data.GetAllistictbyzone(zone);

                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));


            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        private void LoadPC(string District, string usertype)
        {
            try
            {
                var Assembly = _data.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedValue);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }

        private void LoadMasterPC()
        {
            try
            {

                var Assembly = _data.GetAllacnamebyzoneandistrict(ddlzone.SelectedItem.Text, ddlDistrict.SelectedItem.Text);

                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "acname";
                ddlAssembly.DataBind();

                ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
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
            if (ddlislive.SelectedValue == "")
            {

                ddlislive.Style.Add("background", "#fff");
                ddlislive.Style.Add("color", "#333");
            }
            else
            {
                ddlislive.Style.Add("background", "#007bff");
                ddlislive.Style.Add("color", "#FFFFFF");
            }
            GetStopCameraTotalList();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlStatus.SelectedValue == "")
            {
                ddlStatus.Style.Add("background", "#fff");
                ddlStatus.Style.Add("color", "#333");
            }
            else
            {
                ddlStatus.Style.Add("background", "#007bff");
                ddlislive.Style.Add("color", "#FFFFFF");
            }
            GetStopCameraTotalList();
        }
        private void LoadZone()
        {
            try
            {
                var Alldist = _data.GetZone();

                ddlzone.DataSource = Alldist;
                ddlzone.DataTextField = "Zone";
                ddlzone.DataValueField = "Zone";
                ddlzone.DataBind();

                ddlzone.Items.Insert(0, new ListItem("ALL Zone", ""));
                ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }
        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlDistrict.SelectedValue == "0")
                {
                    LoadMasterPC();


                }

                else
                {
                    LoadMasterPC();
                    ddlDistrict.Style.Add("background", "#007bff");
                    ddlDistrict.Style.Add("color", "#FFFFFF");
                }
                if (ddlDistrict.SelectedValue == "")
                {
                    ddlDistrict.Style.Add("background", "#fff");
                    ddlDistrict.Style.Add("color", "#333");

                }
                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssembly.SelectedValue == "")
                {
                    ddlAssembly.Style.Add("background", "#fff");
                    ddlAssembly.Style.Add("color", "#333");
                }
                else
                {
                    ddlAssembly.Style.Add("background", "#007bff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");

                }

                GetStopCameraTotalList();
            }


            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void ddlzone_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlzone.SelectedValue == "0")
                {
                    LoadDistrict(ddlzone.SelectedItem.Text);


                }

                else
                {
                    LoadDistrict(ddlzone.SelectedItem.Text);
                    ddlzone.Style.Add("background", "#007bff");
                    ddlzone.Style.Add("color", "#FFFFFF");
                }
                if (ddlzone.SelectedValue == "")
                {
                    ddlzone.Style.Add("background", "#fff");
                    ddlzone.Style.Add("color", "#333");

                }
                GetStopCameraTotalList();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
    }
}