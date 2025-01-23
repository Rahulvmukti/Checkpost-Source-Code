using exam.DAL;
using exam.services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace exam
{
    public partial class Listview : System.Web.UI.Page
    {
        LoadTopSelections objloadtop = new LoadTopSelections();
        db_data _boothlist = new db_data();
        db_list_lq c1 = new db_list_lq();

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
        public string searchtext
        {
            get
            {
                if (ViewState["searchtext"] != null)
                    return (string)ViewState["searchtext"];
                else
                    return "";
            }
            set
            {
                ViewState["searchtext"] = value;
            }
        }
        public string searchtext2
        {
            get
            {
                if (ViewState["searchtext2"] != null)
                    return (string)ViewState["searchtext2"];
                else
                    return "";
            }
            set
            {
                ViewState["searchtext2"] = value;
            }
        }

        public int pageitemcount = Convert.ToInt32(ConfigurationManager.AppSettings["pageitemcount"]);
        public static int totalDatacount = 0;
        public void setusertype()
        {
            if (utypeall.Contains("_all"))
            {
                if (ddlDistrict.SelectedValue == "0")
                {
                    if (ddlDistrict.SelectedValue != "0")
                    {

                        if (ddlAssembly.SelectedValue != "0")
                        {
                            usertype = "pc1_ALL District_" + ddlAssembly.SelectedItem.Text;
                        }
                        else
                        {
                            usertype = utypeall.Replace("_all", "");
                        }

                    }
                }
                else
                {
                    if (ddlAssembly.SelectedValue == "0")
                    {
                        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                    }

                }
            }
            else
            {
                if (ddlAssembly.SelectedValue == "0")
                {
                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                }
            }
        }
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

                Session["MenuName"] = "ListView";
                if (!IsPostBack)
                {
                    DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    utypeall = usertype;
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    if (utypeall.StartsWith("eci"))
                    {
                        usertype = usertype.Replace("eci_", "");
                    }
                    _usertype = usertype;
                    if (Request.QueryString.Count > 0 && Request.QueryString["d"] != null)
                    {
                        string str = Common.Decode(Request.QueryString["d"].ToString());
                        _usertype = str.StartsWith("000") ? "online" : usertype;
                        _usertype = str.StartsWith("002") ? "offline" : _usertype;
                        _usertype = str.StartsWith("007") ? "Total" : _usertype;
                        usertype = "sch_" + str.Split('$')[1].ToString();
                    }
                    if (Request.QueryString.Count > 0 && Request.QueryString["s"] != null)
                    {
                        string str = Common.Decode(Request.QueryString["s"].ToString());
                        usertype = str;
                        searchtext = Request.QueryString["s1"] != null ? Common.base64Decode(Request.QueryString["s1"].ToString()) : string.Empty;
                        searchtext2 = Request.QueryString["s2"] != null ? Common.base64Decode(Request.QueryString["s2"].ToString()) : string.Empty;

                    }
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                        if (utypeall.Contains("_all"))
                        {

                            if (ddlDistrict.SelectedIndex > 0)
                            {
                                usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                                LoadBooth(usertype);
                            }
                            else
                            {

                                LoadBooth(usertype);
                            }
                        }
                        else if (utypeall.StartsWith("pc"))
                        {
                            ddlDistrict.Visible = false;
                            usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                            LoadBooth(usertype);
                        }
                        else if (utypeall.StartsWith("sch"))
                        {
                            usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                            LoadBooth(usertype);
                        }

                        else
                        {
                            usertype = "dst_" + ddlDistrict.SelectedItem.Text;

                            LoadBooth(usertype);
                        }

                    }
                }
            }

            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

        private string LoadDistrict_2(string usertype)
        {
            string selectedDistrictText = ""; // Variable to store the selected district text

            try
            {
                DataSet ds_dist = new DataSet();
                objloadtop.LoadDistrict(usertype, utypeall, stateid, out ds_dist);

                ddlDistrict.DataSource = ds_dist;
                ddlDistrict.DataTextField = "District";
                ddlDistrict.DataValueField = "SelValue";
                ddlDistrict.DataBind();
                ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                string seldist = "";
                var a = usertype.Split('_');
                if (a.Length >= 2)
                    seldist = a[1];

                if (seldist != "")
                {
                    ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(seldist));
                }

                selectedDistrictText = ddlDistrict.Items.FindByValue(seldist)?.Text;
                ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(selectedDistrictText));

            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }

            return selectedDistrictText; // Return the selected district text
        }
        private void LoadBooth(string usertype)
        {
            try
            {
                DataSet alldata1 = new DataSet();
                setusertype();
                int startRow = ((PageNumber - 1) * pageitemcount) + 1;
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                List<dbData> data = new List<dbData>();
                // Assuming c1.GetBoothList returns a DataSet
                DataSet alldata = new DataSet();
                if (Request.QueryString.Count > 0 && Request.QueryString["d"] != null)
                {
                    if (_usertype == "live")
                    {
                        ds1 = c1.GetBoothList_1(usertype, true, stateid, startRow, startRow + pageitemcount - 1, searchtext, searchtext2);
                        ds1.Tables["table1"].TableName = "table";
                        alldata1 = _boothlist.FilterDataByAccess(ds1, true, true);
                        totalDatacount = alldata1.Tables[0].Rows.Count;
                    }



                    if (_usertype == "offline")
                    {
                        ddlDistrict.Enabled = false;
                        ddlStatus.Enabled = false;
                        string selectedDistrict = LoadDistrict_2(usertype);
                        string selectedacname = ddlAssembly.SelectedItem.Text;
                        selectedacname = selectedacname == "ALL Assembly" ? "" : selectedacname;
                        string status = "STOPPED";
                        ddlStatus.SelectedValue = "STOPPED";
                        DataSet dsNew = _boothlist.GetMapBoothListView(userid, ddlDistrict.SelectedValue, selectedacname, ddllocationType.SelectedValue, status, -1, -1, "", strm_txtBox.Text, PageNumber, pageitemcount);
                        totalDatacount = Convert.ToInt32(dsNew.Tables[1].Rows[0]["Total"]);
                        int totalPages = (totalDatacount + pageitemcount - 1) / pageitemcount;

                        if (dsNew.Tables[0].Rows.Count != 0)
                        {
                            GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                        //GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                        //GridView1.DataBind();
                        BindPager(totalDatacount, this.PageNumber, pageitemcount);
                    }
                    else if (_usertype == "Total")
                    {

                        ddlDistrict.Enabled = false;
                        string selectedDistrict = LoadDistrict_2(usertype);
                        string selectedacname = "";
                        string status;

                        if (ddlStatus.Text == "RUNNING")
                            status = "RUNNING";
                        else if (ddlStatus.Text == "STOPPED")
                            status = "STOPPED";
                        else
                            status = "";

                        DataSet dsNew = _boothlist.GetMapBoothListView(userid, ddlDistrict.SelectedValue, ddlAssembly.SelectedValue, ddllocationType.SelectedValue, status, -1, -1, "", strm_txtBox.Text, PageNumber, pageitemcount);
                        totalDatacount = Convert.ToInt32(dsNew.Tables[1].Rows[0]["Total"]);
                        int totalPages = (totalDatacount + pageitemcount - 1) / pageitemcount;

                        if (dsNew.Tables[0].Rows.Count != 0)
                        {
                            GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                            GridView1.DataBind();
                        }
                        else
                        {
                            GridView1.DataSource = null;
                            GridView1.DataBind();
                        }
                        //GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                        //GridView1.DataBind();
                        BindPager(totalDatacount, this.PageNumber, pageitemcount);
                    }
                    //else
                    //{
                    //    ds1 = c1.GetBoothList_1(usertype, false, stateid, startRow, startRow + pageitemcount - 1, searchtext, searchtext2);
                    //    ds1.Tables["table1"].TableName = "table";
                    //    alldata1 = _boothlist.FilterDataByAccess(ds1, true, true);
                    //    totalDatacount = alldata1.Tables[0].Rows.Count;
                    //}
                }
                else
                {
                    string status = "";
                    string acname = "";
                    if (ddlStatus.SelectedValue != "BOTH" && ddlStatus.SelectedValue != "")
                    {
                        status = ddlStatus.SelectedValue;
                    }
                    if (ddlAssembly.SelectedIndex > 0)
                    {
                        acname = ddlAssembly.SelectedItem.Text;
                    }
                    DataSet dsNew = _boothlist.GetMapBoothListView(userid, ddlDistrict.SelectedValue, acname, ddllocationType.SelectedValue, status, -1, -1, "", strm_txtBox.Text, PageNumber, pageitemcount);
                    totalDatacount = Convert.ToInt32(dsNew.Tables[1].Rows[0]["Total"]);
                    int totalPages = (totalDatacount + pageitemcount - 1) / pageitemcount;

                    if (dsNew.Tables[0].Rows.Count != 0)
                    {
                        GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                    //GridView1.DataSource = dsNew.Tables[0];//.Select("rn1 >= " + (startRow + 1).ToString() + " AND rn1 <= " + (startRow + pageitemcount).ToString()).CopyToDataTable();
                    //GridView1.DataBind();
                    BindPager(totalDatacount, this.PageNumber, pageitemcount);


                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_list() -- >  " + ex.Message);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Common.Log("GridView1_RowDataBound_list() -- >  " + ex.Message);
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                loaddata();

            }
            catch (Exception ex)
            {

            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                loaddata();
            }
            catch (Exception ex)
            {
            }
        }



        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "ChangeState")
                {
                    _boothlist.UpdateStatus(e.CommandArgument.ToString());
                    LoadBooth(usertype);
                }
            }
            catch (Exception ex)
            {
            }
        }
        private void loaddata()
        {
            LoadBooth(usertype);
        }
        private void LoadDistrict(string usertype)
        {
            try
            {
                DataSet ds_dist = new DataSet();
                if (utypeall.StartsWith("sch_") || utypeall.StartsWith("dst_"))
                {
                    ddlDistrict.Items.Clear();
                    ddlDistrict.Items.Insert(0, new ListItem(usertype.Split('_')[1], usertype.Split('_')[1]));
                }
                else
                {
                    objloadtop.LoadDistrict(usertype, utypeall, stateid, out ds_dist);

                    ddlDistrict.DataSource = ds_dist;
                    ddlDistrict.DataTextField = "District";
                    ddlDistrict.DataValueField = "SelValue";
                    ddlDistrict.DataBind();
                    ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                    ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
                }
                string seldist = "";
                var a = usertype.Split('_');
                if (a.Length >= 2)
                    seldist = a[1];

                if (seldist != "")
                {
                    ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(seldist));
                }

                ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
        private void LoadAssembly(string District)
        {
            try
            {

                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

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
                PageNumber = 1;
                LoadAssembly(ddlDistrict.SelectedItem.Text);
                LoadBooth(usertype);
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
                PageNumber = 1;
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        protected void ddlbooth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;

                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        public void BindPager(int totalRecordCount, int currentPageIndex, int pageSize)
        {
            int totalLinkInPage = 10;
            int totalPageCount = (int)Math.Ceiling((decimal)totalRecordCount / pageSize);
            List<ListItem> pageLinkContainer = new List<ListItem>();
            if (totalPageCount > 1)
            {
                int startPageLink = Math.Max(currentPageIndex - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
                int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

                if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
                {
                    lastPageLink = Math.Min(currentPageIndex + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
                    startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
                }



                pageLinkContainer.Add(new ListItem("First", "1", currentPageIndex != 1));
                for (int i = startPageLink; i <= lastPageLink; i++)
                {
                    pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPageIndex != i));
                }

                pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPageIndex != totalPageCount));
                prev.Visible = true;
                next.Visible = true;
            }
            else
            {
                prev.Visible = false;
                next.Visible = false;
            }
            prev.Enabled = currentPageIndex != 1;
            next.Enabled = currentPageIndex != totalPageCount;
            rptPages.DataSource = pageLinkContainer;
            rptPages.DataBind();

        }

        protected void Page_Changed(object sender, EventArgs e)
        {
            int pageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
            this.PageNumber = pageIndex;
            loaddata();
        }
        protected void rptPages_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    LinkButton btnPage = (LinkButton)e.Item.FindControl("btn_page_camera");
                    if (int.Parse(btnPage.Text) == PageNumber)
                    {
                        btnPage.Font.Underline = true;
                        btnPage.CssClass = "btn btn-primary btncustom";
                    }
                    else
                    {
                        btnPage.Font.Underline = false;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void next_Click(object sender, EventArgs e)
        {
            PageNumber = PageNumber + 1;

            loaddata();

        }

        protected void prev_Click(object sender, EventArgs e)
        {
            PageNumber = PageNumber - 1;

            loaddata();

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {

                PageNumber = 1;
                LoadBooth(usertype);
            }
            catch
            {

            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;

                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlStatus_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void Timer1_Tick1(object sender, EventArgs e)
        {
            try
            {
                loaddata();

            }
            catch (Exception ex)
            {

            }
        }
        protected void ddllocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddllocationType_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Dashboard.aspx");
        }
    }
}