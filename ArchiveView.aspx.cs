using exam.DAL;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.services;
using System.Net.Http;
using Newtonsoft.Json;
using System.Linq;
using System.Configuration;
using System.Data.SqlClient;
using ClosedXML.Excel;
using System.IO;
using System.Data.OleDb;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace exam
{
    public partial class ArchiveView : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();

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
        public int userid
        {
            get
            {
                if (ViewState["userid"] != null)
                    return (int)ViewState["userid"];
                else
                    return 0;
            }
            set
            {
                ViewState["userid"] = value;
            }
        }
        public DataTable dsReport
        {
            get
            {
                if (ViewState["dsReport"] != null)
                    return (DataTable)ViewState["dsReport"];
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
                else
                {
                    Session.Timeout = 240;
                }
                Session["MenuName"] = "AIView";
                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    utypeall = usertype;
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    if (!IsPostBack)
                    {
                        LoadDistrict(usertype);
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_gridauto() -- >  " + ex.Message);
            }
        }
        private void LoadDistrict(string user_type)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByuserid(userid);
                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict() -- >  " + ex.Message);
            }
        }

        private void LoadLocation()
        {
            try
            {
                var Location = _boothlist.GetAlllocationbyAcCode(ddlAssembly.SelectedValue);
                ddlLocation.Items.Clear();
                ddlLocation.DataSource = Location;
                ddlLocation.DataTextField = "Location";
                ddlLocation.DataValueField = "Location";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
                if (ddlLocation.SelectedIndex == 0)
                {
                    dsReport = null;
                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtDID.Text = "";
                if (ddlDistrict.SelectedValue == "0")
                {
                   
                    LoadPC("", userid);
                }
                else
                {
                    LoadPC(ddlDistrict.SelectedValue, userid);
                }
                dsReport = null;
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        private void LoadPC(string District, int userid)
        {
            try
            {
                var Assembly = _boothlist.GetAllacnameByuserid(ddlDistrict.SelectedValue, userid);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "accode";
                ddlAssembly.DataBind();
                ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadLocation();
                dsReport = null;
            }

            catch (Exception ex)
            {
                Common.Log("ddlAssembly_SelectedIndexChanged_list() -- >  " + ex.Message);
            }

        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (ddlLocation.SelectedIndex != 0)
                {
                    getdata();
                }


            }

            catch (Exception ex)
            {

            }
        }
        protected string GetStyleAttribute(int itemIndex)
        {
            return $"style='left: {itemIndex * 100}px;'";
        }
        public void getdata()
        {
            try
            {

                if (ddlLocation.SelectedIndex != 0)
                {
                    DataSet ds = _boothlist.getLocationWise(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, ddlLocation.SelectedValue);
                    if(ds.Tables[0].Rows.Count>0)
                    {
                        string url = "https://" + ds.Tables[0].Rows[0]["servername"].ToString() + "/live-record/" + ds.Tables[0].Rows[0]["deviceid"].ToString();
                        lblservername.Text = ds.Tables[0].Rows[0]["servername"].ToString();
                        lblcameraid.Text = ds.Tables[0].Rows[0]["deviceid"].ToString();
                        //string url = "https://punjab106.vmukti.com/live-record/abcd/";
                        //https://biharmedia2.vmukti.com/live-record/ANYK-804347-AAAAA
                        WebClient client = new WebClient();
                        string response = client.DownloadString(url);
                        string response1 = response;
                        DataSet dataSet = new DataSet();
                        StringReader reader = new StringReader(response);
                        dataSet.ReadXml(reader);
                        DataTable sourceTable = dataSet.Tables[0];
                        DataSet destDataSet = new DataSet();
                        DataTable destTable = new DataTable();
                        destTable.Columns.Add("href", typeof(string));
                        destTable.Columns.Add("a_Text", typeof(string));
                        destDataSet.Tables.Add(destTable);
                        for (int i = 0; i < sourceTable.Rows.Count; i++)
                        {
                            string stringValue = sourceTable.Rows[i]["a_Text"].ToString();
                            if (stringValue.IndexOf(FromDt.Text) >= 0)
                            {
                                DataRow destRow = destTable.NewRow();
                                destRow["href"] = sourceTable.Rows[i]["href"];
                                destRow["a_Text"] = sourceTable.Rows[i]["a_Text"];
                                destTable.Rows.Add(destRow);
                            }
                        }
                        dsReport = destTable;
                    }
                    else
                    {
                        dsReport = null;
                    }
                   
                }

            }

            catch (Exception ex)
            {

            }
        }
        public void getdata1()
        {
            try
            {
                 

                    lblservername.Text = "";
                    lblcameraid.Text = "";
                    DataSet ds1 = _boothlist.getservername(txtDID.Text);
                    if (ds1.Tables[0].Rows.Count > 0)
                    {
                        string url = "https://" + ds1.Tables[0].Rows[0]["servername"].ToString() + "/live-record/" + ds1.Tables[0].Rows[0]["deviceid"].ToString();
                        lblservername.Text = ds1.Tables[0].Rows[0]["servername"].ToString();
                        lblcameraid.Text = ds1.Tables[0].Rows[0]["deviceid"].ToString();
                        WebClient client = new WebClient();
                        string response = client.DownloadString(url);
                        DataSet dataSet = new DataSet();
                        StringReader reader = new StringReader(response);
                        dataSet.ReadXml(reader);
                        DataTable sourceTable = dataSet.Tables[0];
                        DataSet destDataSet = new DataSet();
                        DataTable destTable = new DataTable();
                        destTable.Columns.Add("href", typeof(string));
                        destTable.Columns.Add("a_Text", typeof(string));
                        destDataSet.Tables.Add(destTable);
                        for (int i = 0; i < sourceTable.Rows.Count; i++)
                        {
                            string stringValue = sourceTable.Rows[i]["a_Text"].ToString();
                            if (stringValue.IndexOf(FromDt.Text) >= 0)
                            {
                                DataRow destRow = destTable.NewRow();
                                destRow["href"] = sourceTable.Rows[i]["href"];
                                destRow["a_Text"] = sourceTable.Rows[i]["a_Text"];
                                destTable.Rows.Add(destRow);
                            }
                        }
                        dsReport = destTable;
                    }
                    else
                    {
                        dsReport = null;
                    } 
            }

            catch (Exception ex)
            {

            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if(txtDID.Text!="")
            {
                ddlDistrict.Items.Clear();
                LoadDistrict("");
                ddlAssembly.Items.Clear();
                ddlLocation.Items.Clear(); 
                ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
                getdata1();
            }
            else
            {
                getdata();
            } 
        }
    }


}