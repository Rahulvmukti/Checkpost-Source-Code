using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Configuration;
using exam.DAL;
using System.IO;
using exam.services;
using System.Text;
using ClosedXML.Excel;
using System.Drawing;

namespace exam
{
    public partial class CameraStatus : System.Web.UI.Page
    {
        LoadTopSelections objloadtop = new LoadTopSelections();

        db_data _boothlist = new db_data();
        db_data_admin _dbadmin = new db_data_admin();   
        public string usertype
        {
            get
            {
                if (ViewState["usertype"] != null)
                    return (string)ViewState["usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["usertype"] = value;
            }
        }
        public string useridentifer
        {
            get
            {
                if (ViewState["useridentifer"] != null)
                    return (string)ViewState["useridentifer"];
                else
                    return "";
            }
            set
            {
                ViewState["useridentifer"] = value;
            }
        }
        public string _usertype
        {
            get
            {
                if (ViewState["_usertype"] != null)
                    return (string)ViewState["_usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["_usertype"] = value;
            }
        }
        public string utypeall
        {
            get
            {
                if (ViewState["utypeall"] != null)
                    return (string)ViewState["utypeall"];
                else
                    return "live";
            }
            set
            {
                ViewState["utypeall"] = value;
            }
        }
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
       
        public string selectedValue
        {
            get
            {
                if (ViewState["selectedValue"] != null)
                    return (string)ViewState["selectedValue"];
                else
                    return "";
            }
            set
            {
                ViewState["selectedValue"] = value;
            }
        }
        public DataSet BoothList
        {
            get
            {
                if (ViewState["BoothList"] != null)
                    return (DataSet)ViewState["BoothList"];
                else
                    return BoothList1;
            }
            set
            {
                ViewState["BoothList"] = value;
            }
        }
        public DataSet BoothList1
        {
            get
            {
                if (ViewState["BoothList"] != null)
                    return (DataSet)ViewState["BoothList1"];
                else
                    return BoothList1;
            }
            set
            {
                ViewState["BoothList1"] = value;
            }
        }
        public int pageitemcount = 10000;
        public static int totalDatacount = 0;
        public int PageNumber
        {
            get
            {

                if (ViewState["PageNumber"] != null)
                    return (int)ViewState["PageNumber"];
                else
                    return 1;
            }
            set
            {
                ViewState["PageNumber"] = value;
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
                Session["MenuName"] = "CameraStatus";
                if (!IsPostBack)
                {

                    testdist.Attributes.Add("onclick", "triggerPostBack();");
                    testtotalps.Attributes.Add("onclick", "triggerPostBack();");
                    testlastonehour.Attributes.Add("onclick", "triggerPostBack();");
                    testonline.Attributes.Add("onclick", "triggerPostBack();");
                    testco.Attributes.Add("onclick", "triggerPostBack();");
                    testoffline.Attributes.Add("onclick", "triggerPostBack();");
                    Session["IsDescending"] = true;
                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    useridentifer = ds.Tables[0].Rows[0]["identifier"].ToString();
                    utypeall = usertype;
                    string assembly = ds.Tables[0].Rows[0]["AssemblyAccesIds"].ToString();
                    string[] assemblyIds = assembly.Split(','); 
                    string assemblyIdsString = string.Join(",", assemblyIds); 
                    Session["AssemblyIdsString"] = assemblyIdsString; 
                    _usertype = usertype; 
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadBooth(usertype);
                    }
                 
                } 
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }
         
        private void LoadBooth(string usertype)
        {
            try
            {
                setusertype();
                int startRow = ((PageNumber - 1) * pageitemcount);
                DataSet ds = new DataSet();

                int UserID = 0;
                if (Session["userid"] != null)
                    UserID = Convert.ToInt32(Session["userid"]);
                DataSet dspc = _dbadmin.allcountlivenew(usertype, UserID);
                if (dspc.Tables.Count > 0)
                { 
                    BoothList = dspc;
                }
                    DateTime chklastseen = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["minute"]) * -1);

                // Check if the session variable exists
                if (Session["IsDescending"] == null)
                {
                    // Initialize the session variable with default value (false for ascending)
                    Session["IsDescending"] = false;
                }

                // Retrieve the current sorting order from the session variable
                bool isDescending = Convert.ToBoolean(Session["IsDescending"]);

                // Toggle the sorting order for the next click
                isDescending = !isDescending;

                // Save the updated sorting order back to the session variable
                Session["IsDescending"] = isDescending;

                // Now use the updated sorting order to build the orderby clause
                string orderby = selectedValue + (isDescending ? " DESC" : " ASC"); 

                DataSet dspc1 = _dbadmin.allcountlive_assemblyneworderby("", UserID, orderby);
                BoothList = dspc1;
                if (utypeall.StartsWith("eci"))
                {

                }
                else
                {
                    LiveCount(usertype, dspc.Tables[0].Rows.Count >= 0 ? Convert.ToInt32(dspc.Tables[0].Compute("SUM(TotalBooth)", string.Empty)) : 0
                        , dspc.Tables[0].Rows.Count >= 0 ? Convert.ToInt32(dspc.Tables[0].Compute("SUM(Live)", string.Empty)) : 0
                        , dspc.Tables[0].Rows.Count >= 0 ? Convert.ToInt32(dspc.Tables[0].Compute("SUM(stop)", string.Empty)) : 0
                        , dspc.Tables[0].Rows.Count >= 0 ? Convert.ToInt32(dspc.Tables[0].Compute("SUM(connectedonce)", string.Empty)) : 0
                        , dspc.Tables[0].Rows.Count >= 0 ? Convert.ToInt32(dspc.Tables[0].Compute("SUM(lastLive)", string.Empty)) : 0

                    );

                }

            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_list() -- >  " + ex.Message);
            }
        } 
         
        private void LiveCount(string usertype, int totalcount, int cur_livecount, int cur_stopcount, int cur_ConnectedOnce, int cur_lastlivecount)
        {

            totalbooth.InnerHtml = totalcount.ToString();
            runningbooth.InnerHtml = cur_livecount.ToString();
            runningboothlast.InnerHtml = cur_lastlivecount.ToString();
            stopbooth.InnerHtml = cur_stopcount.ToString();
            Connectedonce.InnerHtml = cur_ConnectedOnce.ToString();
        }

        private CheckBox previouslySelectedRadioButton = null;
        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox selectedRadioButton = (CheckBox)sender; 
            if (selectedRadioButton == previouslySelectedRadioButton)
            { 
                selectedRadioButton.Checked = false;
                selectedRadioButton.CssClass = "unselectedRadioButton";
                selectedValue = null; // Or set it to the appropriate default value
                previouslySelectedRadioButton = null; // Reset the previouslySelectedRadioButton variable
            }
            else
            {
              
                selectedValue = selectedRadioButton.Text;
                previouslySelectedRadioButton = selectedRadioButton; // Update the previouslySelectedRadioButton variable
            }
             
            foreach (Control control in selectedRadioButton.Parent.Controls)
            {
                if (control is RadioButton radioButton && radioButton != selectedRadioButton)
                {
                    radioButton.CssClass = "unselectedRadioButton";
                }
            }

            LoadBooth(usertype); 
        }
         
        private void loaddata(string orderby)
        {
            int UserID = 0;
            if (Session["userid"] != null)
                UserID = Convert.ToInt32(Session["userid"]);

            DataSet dspc1 = _dbadmin.allcountlive_assemblyneworderby("", UserID, orderby);
            BoothList = dspc1; 
        }

        public void setusertype()
        {

        }
        
        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            bool isDescending = Convert.ToBoolean(Session["IsDescending"]); 
            Session["IsDescending"] = isDescending; 
            string orderby = selectedValue + (isDescending ? " DESC" : " ASC");
            loaddata(orderby); 
        }
    }
}