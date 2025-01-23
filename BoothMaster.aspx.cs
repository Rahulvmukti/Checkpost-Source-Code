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
    public partial class BoothMaster : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();
        db_data_admin _dbadmin = new db_data_admin();
        
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
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                Session["MenuName"] = "BoothUpload";
                ScriptManager1.RegisterPostBackControl(this.btnupload);
                ScriptManager1.RegisterPostBackControl(this.ddlDistrict);
                ScriptManager1.RegisterPostBackControl(this.ddlAssembly);
                DataSet ds = _boothlist.GetUserData(Page.User.Identity.Name);
                usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]);
                Session["username"] = ds.Tables[0].Rows[0]["username"].ToString();
                Session["ID"] = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                utypeall = usertype;
                if (utypeall.Contains("_all"))
                {
                    usertype = usertype.Replace("_all", "");
                }
                if (utypeall.StartsWith("eci"))
                {
                    usertype = usertype.Replace("eci_", "");
                }

                if (!IsPostBack)
                {
                    LoadDistrict(usertype);
                    LoadPC(ddlDistrict.SelectedValue, usertype);
                    LoadBooth();
                    LoadMasterDistrict(usertype);
                    LoadMasterPC();

                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load() -- >  " + ex.Message);
            }
        }
        private void LoadBooth()
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _boothlist.GetMapBoothListNew_Master(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue != "" ? ddlAssembly.SelectedItem.Text : "", strm_txtBox.Text.Trim(), ddlCameraType.SelectedValue);

                if (ds.Tables[0].Rows.Count == 0)
                {
                    int res = 1;
                    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());

                    GridView1.DataSource = ds;
                    GridView1.DataBind();
                    int columncount = GridView1.Rows[0].Cells.Count;
                    GridView1.Rows[0].Cells.Clear();
                    GridView1.Rows[0].Cells.Add(new TableCell());
                    GridView1.Rows[0].Cells[0].ColumnSpan = columncount;
                    GridView1.Rows[0].Cells[0].Text = "<div align='center'><label class='text-center text-danger'>No Records Found</label></div>";
                }
                else
                {
                    GridView1.DataSource = ds.Tables[0];
                    GridView1.DataBind();
                }
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth() -- >  " + ex.Message);
            }
        }

        private void LoadDistrict(string user_type)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByStateId(1);

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
        private void LoadMasterDistrict(string user_type)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByStateId(1); 

                drpAddDistrict.DataSource = Alldist;
                drpAddDistrict.DataTextField = "district";
                drpAddDistrict.DataValueField = "district";
                drpAddDistrict.DataBind();

                drpAddDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                drpAddAcname.Items.Insert(0, new ListItem("ALL Assembly", ""));

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
                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, ddlDistrict.SelectedValue);
                ddlAssembly.Items.Clear();
                ddlAssembly.DataSource = Assembly;
                ddlAssembly.DataTextField = "acname";
                ddlAssembly.DataValueField = "accode";
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

                var Assembly = _boothlist.GetAllAssemblyByDistrict(stateid, drpAddDistrict.SelectedItem.Text);

                drpAddAcname.Items.Clear();
                drpAddAcname.DataSource = Assembly;
                drpAddAcname.DataTextField = "acname";
                drpAddAcname.DataValueField = "accode";
                drpAddAcname.DataBind();

                drpAddAcname.Items.Insert(0, new ListItem("ALL Assembly", ""));
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

                if (ddlDistrict.SelectedValue == "0")
                {
                    LoadPC("", usertype);


                }

                else
                {
                    LoadPC(ddlDistrict.SelectedValue, usertype);
                    ddlDistrict.Style.Add("background", "#007bff");
                    ddlDistrict.Style.Add("color", "#FFFFFF");
                }
                if (ddlDistrict.SelectedValue == "")
                {
                    ddlDistrict.Style.Add("background", "#fff");
                    ddlDistrict.Style.Add("color", "#333");
                    ddlAssembly.Style.Add("background", "#007bff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");
                }
                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpAddDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (drpAddDistrict.SelectedValue == "0")
                    LoadMasterPC();
                else
                    LoadMasterPC();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void drpAddAcname_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblaccode.Text = drpAddAcname.SelectedValue;
        }

        protected void drpaddLocationType_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void ddlCameraType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlCameraType.SelectedValue == "")
                {
                    ddlCameraType.Style.Add("background", "#fff");
                    ddlCameraType.Style.Add("color", "#333");
                }
                else
                {
                    ddlCameraType.Style.Add("background", "#007bff");
                    ddlCameraType.Style.Add("color", "#FFFFFF");
                }
                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlAssembly.SelectedValue == "")
                {
                    ddlAssembly.Style.Add("background", "#fff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");
                }
                else
                {
                    ddlAssembly.Style.Add("background", "#007bff");
                    ddlAssembly.Style.Add("color", "#FFFFFF");

                }

                LoadBooth();
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

                LoadBooth();
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadBooth();
            }
            catch
            {
            }
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                
                int streamid = 0;
                DataSet ds = new DataSet();
                DataSet ds1 = _dbadmin.GetStreamDetailsByDID(txtAddCameraId.Text.Trim().ToUpper());
                if (ds1.Tables[0].Rows.Count <= 0)
                {
                    //streamid = SaveCameraDetailsByDeviceID(txtAddCameraId.Text.Trim().ToUpper());
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + txtAddCameraId.Text.Trim().ToUpper() + " does not exist in System! Please enter valid CameraID or Contact to System Administrator.','error');$j('#PopupAddCamera').show();", true);
                }
                else
                {
                    streamid = Convert.ToInt32(ds1.Tables[0].Rows[0]["id"].ToString());
                }
                if (streamid <= 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + txtAddCameraId.Text.Trim().ToUpper() + " does not exist in System! Please enter valid CameraID or Contact to System Administrator.','error');$j('#PopupAddCamera').show();", true);
                }
                else
                {
                    ds = _boothlist.SaveBooth(Convert.ToInt32(lblboothid.Value), streamid, txtAddOperatorName.Text, txtAddMobileNumber.Text, txtoperatordesignation.Text, drpAddDistrict.SelectedItem.Text, drpAddAcname.SelectedValue, drpAddAcname.SelectedItem.Text, txtAddPSNum.Text, txtAddLocation.Text, drpaddLocationType.SelectedValue, Session["username"].ToString());
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString()))
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','success');$j('#PopupAddCamera').hide();", true);
                        LoadBooth();
                        //lblboothid.Value = "0";
                    }
                    else
                    {
                        if(ds.Tables[0].Rows[0]["Error"].ToString()== "DupDID" )
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "Confirm", "Confirm('" + ds.Tables[0].Rows[0]["msg"].ToString() + "'," + ds.Tables[0].Rows[0]["ID"].ToString() + "," + ds.Tables[0].Rows[0]["ID1"].ToString() + ")", true);
                        }
                        else if(ds.Tables[0].Rows[0]["Error"].ToString() == "AddExist")
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','error');$j('#PopupAddCamera').show();", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','" + ds.Tables[0].Rows[0]["msg"].ToString() + "','error');$j('#PopupAddCamera').show();", true);
                        }
                        
                    }
                }
               
                if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Status"].ToString()) == true)
                {
                    if (ds.Tables[0].Columns.Count == 3)
                    {
                        DataSet dataSet = _boothlist.getDataSet("INSERT", Convert.ToInt32(ds.Tables[0].Rows[0]["BoothID"]));
                        string htmlString = getHtml(dataSet);
                        SendAutomatedEmail(htmlString);
                    }
                    else
                    {
                        DataSet dataSet = _boothlist.getDataSet("", Convert.ToInt32(lblboothid.Value));
                        string htmlString = getHtml(dataSet);
                        SendAutomatedEmail(htmlString);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "Submit")
            {

                
                GridViewRow Row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                lblTitle.Text = "EDIT BOOTH";
                //HiddenField lblIdtest = (HiddenField)Row.FindControl("lblid");
                //lblboothid.Value = lblIdtest.Value;
                Label lblIdtest = (Label)Row.FindControl("lblid");
                lblboothid.Value = lblIdtest.Text;
                Label labelStreamtest = (Label)Row.FindControl("lblStreamId");
                lblStreamId.Text = labelStreamtest.Text;
                Label lblaccodetest = (Label)Row.FindControl("lblaccode");
                lblaccode.Text = lblaccodetest.Text;
                DataSet ds = _boothlist.GetBoothListMaster(Convert.ToInt32(lblboothid.Value));

                drpAddDistrict.SelectedValue = ds.Tables[0].Rows[0]["district"].ToString();
                LoadMasterPC();
                drpAddAcname.SelectedValue = ds.Tables[0].Rows[0]["accode"].ToString();
                txtAddPSNum.Text = ds.Tables[0].Rows[0]["PSNum"].ToString();
                txtAddLocation.Text = ds.Tables[0].Rows[0]["location"].ToString();
                txtAddCameraId.Text = ds.Tables[0].Rows[0]["streamname"].ToString();
                drpaddLocationType.SelectedValue = ds.Tables[0].Rows[0]["cameralocationtype"].ToString();
                txtAddOperatorName.Text = ds.Tables[0].Rows[0]["operatorName"].ToString();
                txtAddMobileNumber.Text = ds.Tables[0].Rows[0]["operatorNumber"].ToString();
                txtoperatordesignation.Text = ds.Tables[0].Rows[0]["Designation"].ToString();

                string chlIsActive1 = ds.Tables[0].Rows[0]["isdisplay"].ToString();
                if (chlIsActive1 == "True")
                {
                    chlIsActive.Checked = true;
                }
                else
                {
                    chlIsActive.Checked = false;
                }

            }

            if (e.CommandName == "Delete")
            {
                try
                {
                    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    Label boothid = (Label)row.FindControl("lblid");
                    Label lblstreamname = (Label)row.FindControl("lblStreamId");
                    bool result = _boothlist.DeleteBoothListMaster(Convert.ToInt32(boothid.Text), Session["username"].ToString());
                    if (result)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Record Deleted.')", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please try after sometime!!')", true);
                        return;

                    }
                    LoadBooth();
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                DataSet ds = _boothlist.GetMapBoothListNew_Master(ddlDistrict.SelectedValue, ddlAssembly.SelectedValue != "" ? ddlAssembly.SelectedItem.Text : "", strm_txtBox.Text, ddlCameraType.SelectedValue);
                GridView1.DataSource = ds;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }
        private int SaveCameraDetailsByDeviceID(string deviceid)
        {
            using (var client = new HttpClient())
            {
               client.BaseAddress = new Uri("http://media5.ambicam.com:5000");
               client.DefaultRequestHeaders.Accept.Clear();
               HttpResponseMessage response = client.GetAsync("api/camera/query/?uuid=" + deviceid.ToString().ToUpper()).Result;
               if (response.IsSuccessStatusCode)
               {
                 string res = response.Content.ReadAsStringAsync().Result;
                 CameraAPI oresponse = JsonConvert.DeserializeObject<CameraAPI>(res);
                 
                 if (oresponse?.rtmp != null && oresponse?.tcpurl != null)
                 {
                  string servername = "";
                  string tcpurl = "";
                  servername = oresponse.rtmp.Split('/')[2]?.Split(':').FirstOrDefault() ?? "";
                  tcpurl = oresponse.tcpurl.Split('/')[2] ?? "";
                  return _boothlist.SaveStreamList(deviceid, servername, tcpurl,Session["username"].ToString(), "BoothMaster.aspx");
                 }
                 else
                 {
                   return 0;
                 }
               }
            }
            return 0;
        }

        [System.Web.Services.WebMethod]
        public static string DeleteClick(int id)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            string query = "DeleteBoothByID";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
            cmd.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            int i = cmd.ExecuteNonQuery();
            con.Close();
            DataSet dataSet = getdeleteDataSet(id);
            string htmlString = getHtml(dataSet);
            SendAutomatedEmail(htmlString);
            return i > 0 ? "Deleted" : "Failed";

        }

        [System.Web.Services.WebMethod]
        public static string BulkDelete(string district, string acname)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            string query = "Assemblywise_bulkBooth_Deleete";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@District", district);
            cmd.Parameters.AddWithValue("@AcName", acname);
            cmd.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
            cmd.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i > 0 ? "Deleted" : "Failed";
        }
        [System.Web.Services.WebMethod]
        public static string swapCamera(string id1, string id2)
        {
            string conString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            string query = "swapCameraFromBooth";
            SqlCommand cmd = new SqlCommand(query);
            SqlConnection con = new SqlConnection(conString);
            cmd.Connection = con;
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@id1",Convert.ToInt32(id1));
            cmd.Parameters.AddWithValue("@id2", Convert.ToInt32(id2));
            cmd.Parameters.AddWithValue("@UserName", System.Web.HttpContext.Current.Session["username"].ToString());
            cmd.Parameters.AddWithValue("@IPAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i > 0 ? "Swap" : "Failed";

        }
        protected void btnupload_Click(object sender, EventArgs e)
        {
            string filename = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("/", "").Replace("AM", "").Replace("PM", "") + ".xlsx";
            if (FileUploadbooth.PostedFile.ContentLength == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please Choose excel file!!','error');$('#loaderdiv').hide();", true);
                return;
            }
            else
            {
                string extn = Path.GetExtension(FileUploadbooth.PostedFile.FileName).ToLower();
                if (extn != ".xls" && extn != ".xlsx")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please select only .xlsx file','error');$('#loaderdiv').hide();", true);
                    return;
                }
                //8/3/2021 4:30:41 PM
                FileUploadbooth.SaveAs(Server.MapPath(filename));
                MainFunc(filename, ddlDistrict.SelectedValue, ddlAssembly.SelectedItem.Text);
                SendUploadAutomatedEmail();
            }
        }
        public DataSet MainFunc(string filepath, string district, string acname)
        {
            DataSet ds = new DataSet();
            DataSet dsDID = new DataSet();
            string connstr = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                List<string> columnsnames = new List<string>();
                DataSet ds_Excel = GetExcelData(filepath, out columnsnames);
                DataTable dtDID = new DataTable();
                dtDID.Columns.Add("CameraID");

                foreach (DataRow row in ds_Excel.Tables[0].Rows)
                {
                    dtDID.Rows.Add(row["CameraDID"].ToString());
                }

                string queryDID = "CheckCameraDIDExists";
                SqlCommand SQLcommandDID = new SqlCommand(queryDID, SQLconn);
                SQLcommandDID.CommandType = CommandType.StoredProcedure;
                SQLcommandDID.Parameters.AddWithValue("@tbl", dtDID);
                SqlDataAdapter adpDID = new SqlDataAdapter(SQLcommandDID);
                SQLconn.Open();
                adpDID.Fill(dsDID);
                SQLconn.Close();
                //if (dsDID.Tables[0].Rows.Count > 0)
                //{
                //    foreach (DataRow row in dsDID.Tables[0].Rows)
                //    {
                //     SaveCameraDetailsByDeviceID(row["CameraID"].ToString());
                //    }
                //}
                if(dsDID.Tables[0].Rows.Count>0)
                {
                    DataTable dTdid = new DataTable();
                    dTdid.Columns.Add("uuid");
                    foreach (DataRow row in ds_Excel.Tables[0].Rows)
                    {
                        dTdid.Rows.Add(row["CameraDID"].ToString());
                    }
                    SaveStreamListWithAPI(dTdid);
                }
                
                DataTable firstTable = ds_Excel.Tables[0];
                string query = "UploadAssemblyWiseBooth";
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.CommandType = CommandType.StoredProcedure;
                SQLcommand.Parameters.AddWithValue("@District", district);
                SQLcommand.Parameters.AddWithValue("@Assembly", acname);
                SQLcommand.Parameters.AddWithValue("@UserName", Session["username"].ToString());
                SQLcommand.Parameters.AddWithValue("@tbl", firstTable);
                SQLcommand.Parameters.AddWithValue("@IpAddress", System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString());
                SqlDataAdapter adp = new SqlDataAdapter(SQLcommand);
                SQLconn.Open();
                adp.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    Session["ErrorDt"] = ds.Tables[0];
                    //DataTable dtExcel = new DataTable();
                    //dtExcel.Columns.Add("CameraDID");
                    //dtExcel.Columns.Add("PSNo");
                    //dtExcel.Columns.Add("Location");
                    //dtExcel.Columns.Add("Operator_Name");
                    //dtExcel.Columns.Add("Operator_Mobile_No");
                    //dtExcel.Columns.Add("Operator_Designation");
                    //dtExcel.Columns.Add("IsOutside_booth");
                    //dtExcel.Columns.Add("IsPink_booth");
                    //dtExcel.Columns.Add("isARO");
                    //dtExcel.Columns.Add("IsAICamera");
                    //dtExcel.Columns.Add("ErrorMessage");
                    //string boothUploadErrorRepotName = ConfigurationManager.AppSettings["BoothUploadError"].ToString();
                    //foreach (DataRow row in ds.Tables[0].Rows)
                    //{
                    //    DataRow dr = dtExcel.NewRow();
                    //    dr[0] = row["CameraDID"];
                    //    dr[1] = row["PSNo"];
                    //    dr[2] = row["Location"];
                    //    dr[3] = row["Operator_Name"];
                    //    dr[4] = row["Operator_Mobile_No"];
                    //    dr[5] = row["Operator_Designation"];
                    //    dr[6] = row["IsOutside_booth"];
                    //    dr[7] = row["IsPink_booth"];
                    //    dr[8] = row["isARO"];
                    //    dr[9] = row["IsAICamera"];
                    //    dr[10] = row["ErrorMessage"];
                    //    dtExcel.Rows.Add(dr);
                    //}
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Booth Uploaded with error!! Please check downloaded File','error').then(function() {$('#loaderdiv').hide();setTimeout(function () {window.location.href = 'BoothMaster.aspx';}, 2000);});", true);
                    Response.AppendHeader("Refresh", "2;url=downloadhandler.ashx");
                    //Response.Redirect("downloadhandler.ashx");
                    //using (XLWorkbook wb = new XLWorkbook())
                    //{
                    //    wb.Worksheets.Add(dtExcel, "Error");
                    //    Response.Clear();
                    //    Response.Buffer = true;
                    //    Response.Charset = "";
                    //    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //    Response.AddHeader("content-disposition", "attachment;filename=" + boothUploadErrorRepotName + DateTime.Now.ToString() + ".xlsx");
                    //    using (MemoryStream MyMemoryStream = new MemoryStream())
                    //    {
                    //        wb.SaveAs(MyMemoryStream);
                    //        MyMemoryStream.WriteTo(Response.OutputStream);
                    //        //Response.Flush();
                    //        Response.End();
                    //    }
                    //}
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','Booth Uploaded successfully','success');$('#loaderdiv').hide();", true);
                }

                LoadBooth();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "alert", "swal('Fail!','Booth Uploaded with error!! Please check downloaded File','error');$('#loaderdiv').hide();", true);
            }
            finally
            {
                SQLconn.Close();
            }
            return ds;
        }

        public void SaveStreamListWithAPI(DataTable dt)
        {
            try
            {
                DataSet streamdata = new DataSet();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://media5.ambicam.com:5000");
                    client.DefaultRequestHeaders.Accept.Clear();
                    string myJson = JsonConvert.SerializeObject(dt);
                    HttpResponseMessage response = client.PostAsync("/api/camera/json/", new StringContent(myJson,System.Text.Encoding.UTF8, "application/json")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string res = response.Content.ReadAsStringAsync().Result;
                        streamdata = JsonConvert.DeserializeObject<DataSet>(res);
                    }
                        foreach (DataRow row in streamdata.Tables[0].Rows)
                        {
                        if(row["rtmp"].ToString()!="" || row["tcpurl"].ToString() != "")
                        {
                            row["rtmp"] = row["rtmp"].ToString().Split('/')[2]?.Split(':').FirstOrDefault() ?? "";
                            row["tcpurl"] = row["tcpurl"].ToString().Split('/')[2] ?? "";
                        }
                        else
                        {
                            row["rtmp"] = "NA";
                            row["tcpurl"] = "NA";
                        }
                           
                        }
                   
                    if (streamdata.Tables[0].Columns.Count==4)
                    {
                        streamdata.Tables[0].Columns.Remove("message");
                    }
                    streamdata.Tables[0].Columns[0].ColumnName = "CameraDID";
                    streamdata.Tables[0].Columns[1].ColumnName = "SERVERName";
                    streamdata.Tables[0].Columns[2].ColumnName = "ProUrl";
                    streamdata.Tables[0].Columns.Add("ISAI");
                    streamdata.Tables[0].AcceptChanges();
                    DataTable firstTable = streamdata.Tables[0]; 
                    DataSet ds= _boothlist.SaveBulkStreamList(firstTable, Session["UserName"].ToString(),"BoothMaster.aspx");
                }
            }
            catch(Exception ex)
            {

            }
        }

        private void clearFileUpload()
        {
            FileUploadbooth.PostedFile.InputStream.Dispose();
        }
        public DataSet GetExcelData(string filepath, out List<string> columnsnames)
        {
            DataSet ds = new DataSet();
            try
            {
                DataTable dt = new DataTable();
                using (XLWorkbook workBook = new XLWorkbook(Server.MapPath(filepath)))
                {
                    columnsnames = new List<string>();
                    //Read the first Sheet from Excel file.
                    IXLWorksheet workSheet = workBook.Worksheet(1);
                    bool firstRow = true;
                    foreach (IXLRow row in workSheet.Rows())
                    {
                        if (firstRow)
                        {
                            foreach (IXLCell cell in row.Cells())
                            {
                                dt.Columns.Add(cell.Value.ToString());
                                columnsnames.Add(cell.Value.ToString());
                            }
                            firstRow = false;
                        }
                        else
                        {
                            if (row.FirstCellUsed() != null && row.LastCellUsed() != null)
                            {
                                dt.Rows.Add();
                                int i = 0;
                                foreach (IXLCell cell in row.Cells(row.FirstCellUsed().Address.ColumnNumber, row.LastCellUsed().Address.ColumnNumber))
                                {
                                    dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                                    i++;
                                }
                            }
                        }

                    }
                    ds.Tables.Add(dt);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return ds;
        }

        protected void btnaddbooth_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "ADD BOOTH";
            lblboothid.Value = "0";
            txtAddCameraId.Text = "";
            txtAddPSNum.Text = "";
            txtAddLocation.Text = "";
            txtAddOperatorName.Text = "";
            txtAddMobileNumber.Text = "";
            txtoperatordesignation.Text = "";
            chlIsActive.Checked = true;
            drpAddDistrict.SelectedIndex = 0;
            drpAddAcname.SelectedIndex = 0;
            lblmsg.Text = "";
        }
        public void SendUploadAutomatedEmail()

        {
            try
            {
                string userName = ConfigurationManager.AppSettings["SMTPUSERNAME"].ToString();
                string password = ConfigurationManager.AppSettings["SMTPPASSWORD"].ToString();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                string ToEmail = "";
                string[] multi = ToEmail.Split(',');
                foreach (string multiemailid in multi)
                {
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["MailTo"].ToString()));
                }
                message.CC.Add(new MailAddress(ConfigurationManager.AppSettings["MailCC"].ToString()));
                message.Subject = "Booth Upload With Excel OF"  + ConfigurationManager.AppSettings["ElectionName"].ToString(); ;
                if (FileUploadbooth.HasFile)
                {
                    string FileName = Path.GetFileName(FileUploadbooth.PostedFile.FileName);
                    message.Attachments.Add(new Attachment(FileUploadbooth.PostedFile.InputStream, FileName));
                }
                message.IsBodyHtml = false; 
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                clearFileUpload();
            }
            catch (Exception e)
            {

            }

        }

        public static string getHtml(DataSet dataSet)
        {
            try
            {
                string messageBody = "<font><b>Change 1: </b></font><br><br>";

                if (dataSet.Tables[0].Rows.Count == 0)
                    return messageBody;
                string htmlTableStart = "<table style=\"border-collapse:collapse; text-align:center;\" >";
                string htmlTableEnd = "</table>";
                string htmlHeaderRowStart = "<tr style =\"background-color:#6FA1D2; color:#ffffff;\">";
                string htmlHeaderRowEnd = "</tr>";
                string htmlTrStart = "<tr style =\"color:#555555;\">";
                string htmlTrEnd = "</tr>";
                //string htmlTrupdatedfromStart = "<tr style =\"color:#555555;\">";
                //string htmlTrupdatedfromEnd = "</tr>";
                string htmlTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                string htmlTdEnd = "</td>";
                //string htmlupdatedfromTdStart = "<td style=\" border-color:#5c87b2; border-style:solid; border-width:thin; padding: 5px;\">";
                //string htmludtaedfromTdEnd = "</td>";

                messageBody += htmlTableStart;
                messageBody += htmlHeaderRowStart;
                messageBody += htmlTdStart + "" + htmlTdEnd;
                messageBody += htmlTdStart + "Camera Id " + htmlTdEnd;
                messageBody += htmlTdStart + "District " + htmlTdEnd;
                messageBody += htmlTdStart + "Assemblycode " + htmlTdEnd;
                messageBody += htmlTdStart + "AssemblyName " + htmlTdEnd;
                messageBody += htmlTdStart + "PSNo" + htmlTdEnd;
                messageBody += htmlTdStart + "Location" + htmlTdEnd;
                messageBody += htmlTdStart + "Action Date" + htmlTdEnd;
                messageBody += htmlTdStart + "Action By" + htmlTdEnd;
                messageBody += htmlTdStart + "Action From" + htmlTdEnd;
                messageBody += htmlTdStart + "Camera Location Type" + htmlTdEnd;
                messageBody += htmlTdStart + "Action Intiated" + htmlTdEnd;
                messageBody += htmlTdStart + "IP Address" + htmlTdEnd;
                messageBody += htmlHeaderRowEnd;
                messageBody += htmlHeaderRowStart;
                //  messageBody += htmlupdatedfromTdStart + "updatedFrom " + htmludtaedfromTdEnd;
                messageBody += htmlHeaderRowEnd;

                foreach (DataRow Row in dataSet.Tables[0].Rows)
                {
                    messageBody = messageBody + htmlTrStart;
                    messageBody = messageBody + htmlTdStart + Row["Status"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["deviceid"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["district"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["accode"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["acname"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["PSNum"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["location"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["updatedDate"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["updatedBy"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["AddedFromPage"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["cameralocationtype"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["Action"] + htmlTdEnd;
                    messageBody = messageBody + htmlTdStart + Row["IPAddress"] + htmlTdEnd;
                    messageBody = messageBody + htmlTrEnd;
                    //messageBody = messageBody + htmlTrupdatedfromStart;
                    //messageBody = messageBody + htmlupdatedfromTdStart + Row["updatedFrom"] + htmludtaedfromTdEnd;
                    //messageBody = messageBody + htmlTrupdatedfromEnd;
                }
                messageBody = messageBody + htmlTableEnd;


                messageBody = messageBody + htmlTableEnd;
                return messageBody;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static void SendAutomatedEmail(string htmlString)

        {
            try
            {
                string userName = ConfigurationManager.AppSettings["SMTPUSERNAME"].ToString();
                string password = ConfigurationManager.AppSettings["SMTPPASSWORD"].ToString();
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"].ToString());
                string ToEmail = "";
                string[] multi = ToEmail.Split(',');
                foreach (string multiemailid in multi)
                {
                    message.To.Add(new MailAddress(ConfigurationManager.AppSettings["MailTo"].ToString()));
                }
                message.CC.Add(new MailAddress(ConfigurationManager.AppSettings["MailCC"].ToString()));
                message.Subject = "Camera Activity In Booth Of" + ConfigurationManager.AppSettings["ElectionName"].ToString();
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                // message.Body = htmlString1;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = new NetworkCredential(userName, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception e)
            {

            }

        }
        public static DataSet getdeleteDataSet(int id)
        {
            string cnString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(cnString);

            string CommandText2 = "GetLatestBoothHistoryByID";
            SqlCommand sqlCommand = new SqlCommand(CommandText2, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BoothId", id);
            sqlCommand.Parameters.AddWithValue("@Action", "DELETE");
            sqlCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet dataSet = new DataSet();
            try
            {
                sqlDataAdapter.Fill(dataSet, "header");
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                sqlConnection.Close();
                return null;
            }
            return dataSet;
        }
    }
    public class CameraAPI
    {
        public string deviceid { get; set; }
        public string rtmp { get; set; }
        public string tcpurl { get; set; }
        public string message { get; set; }
    }
}