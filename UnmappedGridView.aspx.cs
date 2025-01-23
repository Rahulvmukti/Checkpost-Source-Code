using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;
using System.Data;
using System.Configuration;
using exam.services;

namespace exam
{
    public partial class UnmappedGridView : System.Web.UI.Page
    {
        LoadTopSelections objloadtop = new LoadTopSelections();

        db_data _boothgrid = new db_data();
        db_list_lq c1 = new db_list_lq();
        //public int pageitemcount = 6;
        //public static int totalDatacount = 0;
        public string pcname = ConfigurationManager.AppSettings["pcname"].ToString();
        public string assemblyname = ConfigurationManager.AppSettings["assemblyname"].ToString();
        // registrationDAL _regDal = new registrationDAL();
        public int pageitemcount
        {
            get
            {
                if (ViewState["pageitemcount"] != null)
                    return (int)ViewState["pageitemcount"];
                else
                    return 20;
            }
            set
            {
                ViewState["pageitemcount"] = value;
            }
        }
        public int gridcolumns
        {
            get
            {
                if (ViewState["gridcolumns"] != null)
                    return (int)ViewState["gridcolumns"];
                else
                    return 5;
            }
            set
            {
                ViewState["gridcolumns"] = value;
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
        public int gridtypeval
        {
            get
            {
                if (ViewState["gridtypeval"] != null)
                    return (int)ViewState["gridtypeval"];
                else
                    return 30;
            }
            set
            {
                ViewState["gridtypeval"] = value;
            }
        }
        public int totalDatacount
        {
            get
            {
                if (ViewState["totalDatacount"] != null)
                    return (int)ViewState["totalDatacount"];
                else
                    return 6;
            }
            set
            {
                ViewState["totalDatacount"] = value;
            }
        }
        public void setusertype()
        {
            //if (utypeall.Contains("_all"))
            //{
            //    if (ddlDistrict.SelectedValue == "0")
            //    {
            //        //  DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
            //        if (ddlDistrict.SelectedValue != "0")
            //        {

            //            if (ddlAssembly.SelectedValue != "0")
            //            {
            //                usertype = "pc1_ALL District_" + ddlAssembly.SelectedItem.Text;
            //            }
            //            else
            //            {
            //                usertype = utypeall.Replace("_all", "");
            //            }

            //        }
            //    }
            //    else
            //    {
            //        if (ddlAssembly.SelectedValue == "0")
            //        {
            //            usertype = "dst_" + ddlDistrict.SelectedItem.Text;
            //        }
            //        else
            //        {
            //            if (ddlbooth.SelectedValue == "0")
            //            {
            //                usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //            }
            //            //else
            //            //{
            //            //    // usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //            //    if (ddlpsbooth.SelectedValue == "0")
            //            //    {
            //            //        usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //            //    }
            //            //    else
            //            //    {
            //            //        usertype = "psbooth_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text + "_" + ddlpsbooth.SelectedItem.Text;
            //            //    }
            //            //}
            //        }
            //    }
            //}
            //else if (utypeall.StartsWith("pc"))
            //{

            //    // usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //    if (ddlbooth.SelectedValue == "0")
            //    {
            //        usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //    }
            //    //else
            //    //{
            //    //    // usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //    //    if (ddlpsbooth.SelectedValue == "0")
            //    //    {
            //    //        usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //    //    }
            //    //    else
            //    //    {
            //    //        usertype = "psbooth_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text + "_" + ddlpsbooth.SelectedItem.Text;
            //    //    }
            //    //}

            //}
            //else if (utypeall.StartsWith("sch_"))
            //{
            //    if (ddlbooth.SelectedValue == "0")
            //    {
            //        usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //    }
            //    //else
            //    //{
            //    //    //usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //    //    if (ddlpsbooth.SelectedValue == "0")
            //    //    {
            //    //        usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //    //    }
            //    //    else
            //    //    {
            //    //        usertype = "psbooth_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text + "_" + ddlpsbooth.SelectedItem.Text;
            //    //    }
            //    //}
            //}
            //else if (utypeall.StartsWith("zn"))
            //{
            //    if (ddlDistrict.SelectedValue == "0")
            //    {
            //        // DataSet ds = _booth.GetUserData(Page.User.Identity.Name);
            //        usertype = utypeall;
            //    }
            //    else
            //    {
            //        if (ddlAssembly.SelectedValue == "0")
            //        {
            //            usertype = "dst_" + ddlDistrict.SelectedItem.Text;
            //        }
            //        else
            //        {
            //            usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //        }
            //    }
            //}
            //else
            //{
            //    if (ddlAssembly.SelectedValue == "0")
            //    {
            //        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
            //    }
            //    else
            //    {
            //        if (ddlbooth.SelectedValue == "0")
            //        {
            //            usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
            //        }
            //        //else
            //        //{
            //        //    //usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //        //    if (ddlpsbooth.SelectedValue == "0")
            //        //    {
            //        //        usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
            //        //    }
            //        //    else
            //        //    {
            //        //        usertype = "psbooth_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text + "_" + ddlpsbooth.SelectedItem.Text;
            //        //    }
            //        //}
            //    }
            //    //else
            //    //{
            //    //    usertype = "dd_" + ddlDistrict.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text + "_" + ddllocation.SelectedItem.Text + "_" + ddlDate.SelectedItem.Text;
            //    //}
            //}
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
                Session["MenuName"] = "UnmappedGridView";
                if (!IsPostBack)
                {

                    DataSet ds = _boothgrid.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                    utypeall = usertype;
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                    }



                    //if (utypeall.Contains("_all"))
                    //{
                    //    if (ddlDistrict.SelectedValue == "0")
                    //    {
                    //        //DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                    //        usertype = utypeall.Replace("_all", "");
                    //    }
                    //    else
                    //    {
                    //        if (ddlAssembly.SelectedValue == "0")
                    //        {
                    //            usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                    //        }
                    //        else
                    //        {
                    //            if (ddlbooth.SelectedValue == "0")
                    //            {
                    //                usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                    //            }
                    //            else
                    //            {
                    //                usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;

                    //            }
                    //        }
                    //    }
                    //}
                    //else if (utypeall.StartsWith("pc"))
                    //{
                    //    usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;

                    //}
                    //else if (utypeall.StartsWith("sch"))
                    //{
                    //    usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
                    //}
                    //else
                    //{
                    //    if (ddlAssembly.SelectedValue == "0")
                    //    {
                    //        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                    //    }
                    //    else
                    //    {
                    //        if (ddlbooth.SelectedValue == "0")
                    //        {
                    //            usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                    //        }
                    //        else
                    //        {
                    //            usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;

                    //        }
                    //    }
                    //}
                    LoadBooth(usertype);
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_gridauto() -- >  " + ex.Message);
            }
        }

        private void LoadBooth(string usertype)
        {
            try
            {
                setusertype();
                pageitemcount = gridtypeval;

                int startRow = ((PageNumber - 1) * pageitemcount);
                DataSet ds = new DataSet();
                IQueryable<dbData> alldata = Enumerable.Empty<dbData>().AsQueryable();
                bool isgrid = false;
                int? ident = c1.getUserIdentifier(Convert.ToInt32(Session["userid"]));
                if (ident == 4)
                    isgrid = true;

                var dsNew = _boothgrid.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);

                var data = dsNew.Tables[0].AsEnumerable();

                data = data.OrderBy(x => x.Field<string>("status"));
                int totalCount = data.Count();
                int totalPages = (totalCount + pageitemcount - 1) / pageitemcount;

                var dstotal = _boothgrid.GetUnmappedCameraList(txtCameraID.Text.Trim(), "RUNNING");
                lblTotalOnlineCount.Text = dstotal.Tables[0].AsEnumerable().Count().ToString();
                if (PageNumber > totalPages)
                {
                    PageNumber = 1;
                }
                startRow = ((PageNumber - 1) * pageitemcount);
                if (data.Count() > 0)
                {
                    listview1.DataSource = data.Skip(startRow).Take(pageitemcount).ToList().CopyToDataTable();
                    listview1.DataBind();
                }
                else
                {
                    listview1.DataSource = dsNew;
                    listview1.DataBind();
                }
                totalDatacount = data.Count();
                BindPager(totalDatacount, this.PageNumber, pageitemcount);

            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_grid() -- >  " + ex.Message);
            }
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            double getPageCount = (double)((decimal)totalDatacount / (decimal)pageitemcount);
            int pageCount = (int)Math.Ceiling(getPageCount);
            //PageNumber = PageNumber >= pageCount - 1 ? PageNumber = 1 : PageNumber + 1;
            PageNumber = PageNumber >= pageCount ? 1 : PageNumber + 1;

            loaddata();
        }
        private void loaddata()
        {
            LoadBooth(usertype);
            //Timer1.Enabled = true;
        }
        private void LoadDistrict(string usertype)
        {
            try
            {
                //DataSet ds_dist = new DataSet();
                //if (utypeall.StartsWith("sch_") || utypeall.StartsWith("dst_"))
                //{
                //    ddlDistrict.Items.Clear();
                //    ddlDistrict.Items.Insert(0, new ListItem(usertype.Split('_')[1], usertype.Split('_')[1]));
                //}
                //else
                //{
                //    objloadtop.LoadDistrict(usertype, utypeall, stateid, out ds_dist);

                //    ddlDistrict.DataSource = ds_dist;
                //    ddlDistrict.DataTextField = "District";
                //    ddlDistrict.DataValueField = "SelValue";
                //    ddlDistrict.DataBind();
                //}
                ////  ddlDistrict.SelectedIndex = Session["ddldistgrid"] != null ? Convert.ToInt32(Session["ddldistgrid"]) : 0;
                //ddlDistrict_SelectedIndexChanged(ddlDistrict, null);
                //var Alldist = _boothgrid.GetAllDistrictByStateId(stateid);

                //ddlDistrict.DataSource = Alldist;
                //ddlDistrict.DataTextField = "district";
                //ddlDistrict.DataValueField = "district";
                //ddlDistrict.DataBind();

                //ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                //ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }
        private void LoadAssembly()
        {
            try
            {
                ////if (!utypeall.StartsWith("pc_"))
                ////{
                //DataSet ds_PC = new DataSet();
                //if (utypeall.StartsWith("sch_"))
                //{
                //    ddlAssembly.Items.Clear();
                //    ddlAssembly.Items.Insert(0, new ListItem(utypeall.Split('_')[2], utypeall.Split('_')[2]));
                //}
                //else
                //{
                //    objloadtop.LoadPC(District, usertype, selectpc, utypeall, stateid, out ds_PC);

                //    ddlAssembly.DataSource = ds_PC;
                //    ddlAssembly.DataTextField = "accode";
                //    ddlAssembly.DataValueField = "SelValue";
                //    ddlAssembly.DataBind();
                //}
                //ddlAssembly.SelectedIndex = 0;
                //ddlAssembly_SelectedIndexChanged(ddlAssembly, null);
                //var Assembly = _boothgrid.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedItem.Text);

                //ddlAssembly.Items.Clear();
                //ddlAssembly.DataSource = Assembly;
                //ddlAssembly.DataTextField = "acname";
                //ddlAssembly.DataValueField = "acname";
                //ddlAssembly.DataBind();

                //ddlAssembly.Items.Insert(0, new ListItem("ALL Assembly", ""));
                //LoadSchool(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text, usertype);
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
                //ddlAssembly.Items.Clear();
                //ddlbooth.Items.Clear();
                //string selpc = "";
                //if (utypeall.Contains("_all"))
                //{
                //    if (ddlDistrict.SelectedValue == "0")
                //    {
                //        //DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                //        usertype = utypeall.Replace("_all", "");
                //        //ddlAssembly.Items.Insert(0, new ListItem("ALL " + pcname, "0"));
                //        LoadPC("", "ALL District-ALL PC", "");
                //    }
                //    else
                //    {
                //        usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                //        selpc = "SELECTPC";
                //        LoadPC(ddlDistrict.SelectedItem.Text, usertype, selpc);
                //    }
                //}
                //else if (utypeall.StartsWith("pc_"))
                //{
                //    LoadPC(ddlDistrict.SelectedItem.Text, usertype, utypeall.Split('_')[2]);
                //}
                //else
                //{
                //    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                //    LoadPC(ddlDistrict.SelectedItem.Text, usertype, selpc);
                //}

                //ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(ddlDistrict.SelectedItem.Text));
                //Session["ddldistgrid"] = ddlDistrict.SelectedIndex;
                ////   usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
                LoadAssembly();
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        //private void LoadDistrict(string usertype)
        //{
        //    try
        //    {
        //        DataSet ds_dist;
        //        if (utypeall.StartsWith("eci"))
        //        {
        //            ds_dist = _boothgrid.GetDistrictListECI(usertype, stateid);
        //        }
        //        else
        //        {
        //            ds_dist = _boothgrid.GetDistrictList(usertype, stateid);
        //        }
        //        ddlDistrict.DataSource = ds_dist;
        //        ddlDistrict.DataTextField = "District";
        //        ddlDistrict.DataValueField = "District";
        //        ddlDistrict.DataBind();
        //        int dist_count = ds_dist.Tables[0].Rows.Count;
        //        if (utypeall.Contains("_all"))
        //        {
        //            if (dist_count > 1)
        //            {
        //                ddlDistrict.Items.Insert(0, new ListItem("ALL District", "0"));
        //                ddlAssembly.Items.Insert(0, new ListItem("ALL " + pcname, "0"));
        //                ddlDistrict.SelectedIndex = Session["ddldistgrid"] != null ? Convert.ToInt32(Session["ddldistgrid"]) : 1;

        //                if (ddlDistrict.SelectedIndex > 0)
        //                {
        //                    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
        //                    LoadPC(ddlDistrict.SelectedItem.Text, usertype);
        //                }
        //                else
        //                {
        //                    if (ddlDistrict.SelectedItem.Text == "ALL District")
        //                    {
        //                        ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));

        //                    }
        //                    //ddlbooth.Items.Insert(0, new ListItem("ALL AC Name", "0"));
        //                }
        //            }
        //            else
        //            {
        //                LoadPC(ddlDistrict.Items[0].ToString(), usertype);
        //            }
        //        }
        //        else
        //        {
        //            if (dist_count > 0)
        //            {
        //                ddlDistrict.SelectedIndex = Session["ddldistgrid"] != null ? Convert.ToInt32(Session["ddldistgrid"]) : 0;
        //                ddlAssembly.Items.Insert(0, new ListItem("ALL " + pcname, "0"));
        //                ddlAssembly.SelectedIndex = 0;
        //                LoadPC(ddlDistrict.SelectedItem.Text, usertype);
        //            }
        //            else
        //            {
        //                ddlAssembly.Items.Clear();
        //                listview1.DataSource = null;
        //                listview1.DataBind();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Log("LoadDistrict_list() -- >  " + ex.Message);
        //    }
        //}

        //private void LoadPC(string District, string usertype)
        //{
        //    try
        //    {

        //        DataSet ds_PC;

        //        ds_PC = _boothgrid.GetPCFromDistrict(District, usertype, stateid);

        //        ddlAssembly.DataSource = ds_PC;
        //        ddlAssembly.DataTextField = "accode";
        //        ddlAssembly.DataValueField = "accode";
        //        ddlAssembly.DataBind();
        //        int sch_count = ds_PC.Tables[0].Rows.Count;
        //        if (sch_count > 1 || sch_count == 0)
        //        {
        //            ddlAssembly.Items.Insert(0, new ListItem("ALL " + pcname, "0"));
        //            ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));
        //        }
        //        ddlAssembly.SelectedIndex = 0;

        //        LoadSchool(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text, usertype);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Log("LoadSchool_list() -- >  " + ex.Message);
        //    }
        //}

        //private void LoadSchool(string District, string PCname, string usertype)
        //{
        //    try
        //    {

        //        DataSet ds_booth;
        //        if (utypeall.StartsWith("eci"))
        //        {
        //            ds_booth = _boothgrid.GetBoothFromDistrictECI(District, usertype, stateid);
        //        }
        //        else
        //        {
        //            ds_booth = _boothgrid.GetBoothFromDistrictPC(District, PCname, usertype);
        //        }
        //        ddlbooth.DataSource = ds_booth;
        //        ddlbooth.DataTextField = "acname";
        //        ddlbooth.DataValueField = "acname";
        //        ddlbooth.DataBind();
        //        int sch_count = ds_booth.Tables[0].Rows.Count;
        //        if (sch_count > 1 || sch_count == 0)
        //        {
        //            ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));

        //        }
        //        ddlbooth.SelectedIndex = 0;


        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Log("LoadSchool_list() -- >  " + ex.Message);
        //    }
        //}
        //protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        PageNumber = 1;
        //        ddlbooth.Items.Clear();
        //        if (utypeall.Contains("_all"))
        //        {
        //            if (ddlDistrict.SelectedValue == "0")
        //            {
        //                //DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
        //                usertype = utypeall.Replace("_all", "");
        //            }
        //            else
        //            {
        //                usertype = "dst_" + ddlDistrict.SelectedItem.Text;
        //            }
        //        }
        //        else
        //        {
        //            usertype = "dst_" + ddlDistrict.SelectedItem.Text;
        //        }
        //        LoadPC(ddlDistrict.SelectedItem.Text, usertype);
        //        ddlDistrict.SelectedIndex = ddlDistrict.Items.IndexOf(ddlDistrict.Items.FindByText(ddlDistrict.SelectedItem.Text));
        //        Session["ddldistgrid"] = ddlDistrict.SelectedIndex;
        //        //   usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
        //        LoadBooth(usertype);
        //    }
        //    catch (Exception ex)
        //    {
        //        Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
        //    }
        //}
        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;
                //if (ddlAssembly.SelectedValue == "0")
                //{
                //    usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                //    ddlbooth.Items.Clear();
                //    ddlbooth.Items.Insert(0, new ListItem("ALL " + assemblyname, "0"));
                //}
                //else
                //{
                //    usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;
                //    LoadSchool(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text, usertype);
                //}

                //LoadBooth(usertype);

                //LoadSchool(ddlDistrict.SelectedItem.Text, ddlAssembly.SelectedItem.Text, usertype);
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
                //if (ddlbooth.SelectedValue == "0")
                //{
                //    usertype = "pc_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text;

                //}
                //else
                //{
                //    usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlAssembly.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;

                //}

                //LoadBooth(usertype);
                LoadBooth(usertype);
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }
        public void BindPager(int totalRecordCount, int currentPageIndex, int pageSize)
        {
            //pageSize = 1;
            int totalLinkInPage = 10;
            int totalPageCount = (int)Math.Ceiling((decimal)totalRecordCount / pageSize);
            List<ListItem> pageLinkContainer = new List<ListItem>();
            //if (totalPageCount > 1)
            //{
            int startPageLink = Math.Max(currentPageIndex - (int)Math.Floor((decimal)totalLinkInPage / 2), 1);
            int lastPageLink = Math.Min(startPageLink + totalLinkInPage - 1, totalPageCount);

            if ((startPageLink + totalLinkInPage - 1) > totalPageCount)
            {
                lastPageLink = Math.Min(currentPageIndex + (int)Math.Floor((decimal)totalLinkInPage / 2), totalPageCount);
                startPageLink = Math.Max(lastPageLink - totalLinkInPage + 1, 1);
            }



            ////////pageLinkContainer.Add(new ListItem("First", "1", currentPageIndex != 1));
            // pageLinkContainer.Add(new ListItem("FIRST", "1", currentPageIndex > 0));
            for (int i = startPageLink; i <= lastPageLink; i++)
            {
                //pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPageIndex + 1 != i));
                pageLinkContainer.Add(new ListItem(i.ToString(), i.ToString(), currentPageIndex != i));
            }

            ////////pageLinkContainer.Add(new ListItem("Last", totalPageCount.ToString(), currentPageIndex != totalPageCount));
            //  pageLinkContainer.Add(new ListItem("LAST", totalPageCount.ToString(), currentPageIndex < totalPageCount - 1));
            //prev.Visible = true;
            //next.Visible = true;
            //}
            //else
            //{
            //    //prev.Visible = false;
            //    //next.Visible = false;
            //}
            prev.Enabled = currentPageIndex > 1;
            next.Enabled = currentPageIndex <= totalPageCount;
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
                    // System.Web.UI.HtmlControls.HtmlAnchor lipager = (System.Web.UI.HtmlControls.HtmlAnchor)e.Item.FindControl("pager");
                    if (int.Parse(btnPage.Text) == PageNumber)
                    {
                        btnPage.Font.Underline = true;
                        btnPage.CssClass = "btn btn-primary btncustom";
                        //btnPage.Attributes.Add("class", "btn-primary");
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
        protected void ddlgrid_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dimention = ddlgrid.SelectedValue.Split('x');
            gridcolumns = Convert.ToInt32(dimention[0]);
            gridtypeval = Convert.ToInt32(dimention[0]) * Convert.ToInt32(dimention[1]);

            loaddata();
        }

        protected void ddlTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Timer1.Interval = Convert.ToInt32(ddlTimer.SelectedValue);
            loaddata();
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            loaddata();
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            loaddata();
        }
    }
}