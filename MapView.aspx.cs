using exam.DAL;
using exam.services;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Data;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace exam
{
    public partial class MapView : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                Session["MenuName"] = "LiveTracking"; 
                if (!IsPostBack)
                {
                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    utypeall = usertype; 
                    _usertype = usertype;
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                        LoadBooth("dst_" + ddlDistrict.SelectedItem.Text); 
                    }
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

       
        private void LoadDistrict(string usertype)
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
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
             
        private void LoadSchool(string District, int userid)
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
            }
            catch (Exception ex)
            {
                Common.Log("LoadSchool_list() -- >  " + ex.Message);
            }
        }


        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (utypeall.Contains("_all"))
                {
                    if (ddlDistrict.SelectedValue == "0")
                    {
                     
                        usertype = utypeall.Replace("_all", "");
                    }
                    else
                    {
                        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                    }
                }
                else
                {
                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                }
                LoadSchool(ddlDistrict.SelectedItem.Text, userid);
                ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(ddlDistrict.SelectedItem.Text));
                Session["ddldistgrid"] = ddlDistrict.SelectedIndex;
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_map() -- >  " + ex.Message);
            }
        }
        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssembly.SelectedValue == "0")
                {
                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                }
                else
                {
                    usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                }
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlAssembly_SelectedIndexChanged_map() -- >  " + ex.Message);
            }
        }


        private void LoadBooth(string usertype)
        {
            try
            { 
                string json = Common.Encode(usertype);
                 ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", " jQuery(function ($) {LoadLocation('" + json + "',0);});", true);
                 
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_map() -- >  " + ex.Message);
            }
        }
        [WebMethod]
        public static string LoadMap(string d)
        {
            try
            {
                db_data _boothlist = new db_data();
                DataSet ds = new DataSet(); 
                int Userid = Convert.ToInt32(System.Web.HttpContext.Current.Session["userid"]);
                ds = _boothlist.GetMapBoothList(Common.Decode(d), false, Userid);

                string json = JsonConvert.SerializeObject(ds, Formatting.Indented);
                return json;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    } 

}