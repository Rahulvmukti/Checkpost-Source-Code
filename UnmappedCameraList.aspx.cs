using exam.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace exam
{
    public partial class UnmappedCameraList : System.Web.UI.Page
    {
        db_data _boothlist = new db_data();

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

                Session["MenuName"] = "UnmapListView";
                if (!IsPostBack)
                {
                    PageNumber = 1;
                    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
                    GridView1.DataSource = ds.Tables[0].DefaultView;
                    GridView1.DataBind();
                    DataSet ds1 = _boothlist.GetUserData(Page.User.Identity.Name);
                    usertype = ds1.Tables[0].Rows[0]["usercode"].ToString();
                    Session["UserName"]= ds1.Tables[0].Rows[0]["username"].ToString();
                    stateid = Convert.ToInt32(ds1.Tables[0].Rows[0]["stateid"]);
                    getOfflineonlineCount();
                }
            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_list() -- >  " + ex.Message);
            }
        }

        private void getOfflineonlineCount()
        {
            DataSet ds = new DataSet();
            int UserID = Convert.ToInt32(Session["userid"]);
            ds = _boothlist.GetDashboardList("", "", UserID);

            lblTotal.Text =Convert.ToString(ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("Total")));
            lblOnline.Text = Convert.ToString(ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("online")));
            lblOffline.Text = Convert.ToString(ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("offline")));
        }
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                Page_Load(null,null);
            }
            catch (Exception ex)
            {

            }
        }



        protected void btnsearch_Click(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;
                DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Common.Log("btnsearch_Click() -- >  " + ex.Message);
            }
        }

        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                PageNumber = 1;
                DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {
                Common.Log("btnsearch_Click() -- >  " + ex.Message);
            }
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
        //protected void next_Click(object sender, EventArgs e)
        //{
        //    PageNumber = PageNumber + 1;

        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();

        //}

        //protected void prev_Click(object sender, EventArgs e)
        //{
        //    PageNumber = PageNumber - 1;

        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();

        //}
        //protected void Page_Changed(object sender, EventArgs e)
        //{
        //    int pageIndex = Convert.ToInt32(((sender as LinkButton).CommandArgument));
        //    this.PageNumber = pageIndex;
        //    DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
        //    GridView1.DataSource = ds.Tables[0].DefaultView;
        //    GridView1.DataBind();
        //}

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                GridView1.PageIndex = e.NewPageIndex;
                DataSet ds = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
                GridView1.DataSource = ds.Tables[0].DefaultView;
                GridView1.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void LoadDistrict(string user_type)
        {
            try
            {
                var Alldist = _boothlist.GetAllDistrictByStateId(stateid);

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
        private void LoadPC(string District, string usertype)
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
                Common.Log("LoadPC() -- >  " + ex.Message);
            }
        }
        private void Loadoperator(string District, string usertype)
        {
            try
            {
                var Opertor = _boothlist.GetOperatorName(stateid, ddlDistrict.SelectedItem.Text);
                ddlOpertor.Items.Clear();
                ddlOpertor.DataSource = Opertor;
                ddlOpertor.DataTextField = "operatorName";
                ddlOpertor.DataValueField = "id";
                ddlOpertor.DataBind();
                ddlOpertor.Items.Insert(0, new ListItem("ALL operator", ""));
            }
            catch (Exception ex)
            {
                Common.Log("Loadoperator() -- >  " + ex.Message);
            }
        }

        private void LoadLocation(string District, string usertype)
        {
            try
            {
                var Location = _boothlist.GetassemblywiseLocation(ddlDistrict.SelectedItem.Text,ddlAssembly.SelectedItem.Text);
                ddlLocation.Items.Clear();
                ddlLocation.DataSource = Location;
                ddlLocation.DataTextField = "location";
                ddlLocation.DataValueField = "id";
                ddlLocation.DataBind();
                ddlLocation.Items.Insert(0, new ListItem("ALL Location", ""));
            }
            catch (Exception ex)
            {
                Common.Log("LoadLocation() -- >  " + ex.Message);
            }
        }
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Submit")
            {
                
                lbl_error.Text = "";
                LoadDistrict(usertype);
                GridViewRow Row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                Button btnSubmit = (Button)Row.FindControl("btnMapping");
                Label lblcameraid = (Label)Row.FindControl("lblid");
                Label lblstremid = (Label)Row.FindControl("lblSID");
                txtCameraID1.Text = lblcameraid.Text;
                lblSID.Text = lblstremid.Text;
            }
        }

        protected void btnMapCamera_Click(object sender, EventArgs e)
        {
            if(ddlDistrict.SelectedIndex!=0 && ddlAssembly.SelectedIndex!=0 && ddlLocation.SelectedIndex!=0)
            {
                DataSet ds = _boothlist.AddUnmapToMapCamera(Convert.ToInt32(lblboothID.Text) , Convert.ToInt32(lblSID.Text), Session["UserName"].ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                   // lbl_error.Text = ds.Tables[0].Rows[0][0].ToString();
                    DataSet ds1 = _boothlist.GetUnmappedCameraList(txtCameraID.Text.Trim(), ddlStatus.SelectedValue);
                    GridView1.DataSource = ds1.Tables[0].DefaultView;
                    GridView1.DataBind();
                    ddlDistrict.SelectedIndex = 0;
                    ddlAssembly.SelectedIndex = 0;
                    ddlOpertor.SelectedIndex = 0;
                    ddlLocation.SelectedIndex = 0;
                    ddlAssembly.Items.Clear();
                    ddlOpertor.Items.Clear();
                    ddlLocation.Items.Clear();
                    DataSet dataSet = getDataSet();
                    string htmlString = getHtml(dataSet);
                    SendAutomatedEmail(htmlString);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Success!','" + ds.Tables[0].Rows[0][0].ToString() + "','success');", true);
                }
                else
                {
                    lbl_error.Text = "";
                }
            }
            else
            {
                //lbl_error.Text = "Please Select District / Assembly /Location";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alert", "swal('Fail!','Please Select District / Assembly /Location','error');", true);
            }
           
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (ddlDistrict.SelectedValue == "0")
                {
                    LoadPC("", usertype);
                    Loadoperator("", usertype);
                }
                else
                {
                    LoadPC(ddlDistrict.SelectedItem.Text, usertype);
                    Loadoperator(ddlDistrict.SelectedItem.Text, usertype);
                }
                lbl_error.Text = "";
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void ddlAssembly_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_error.Text = "";
            LoadLocation(ddlDistrict.SelectedItem.Text, usertype);
        }

        protected void ddlOpertor_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_error.Text = "";
        }

        protected void ddlLocation_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbl_error.Text = "";
            lblboothID.Text = ddlLocation.SelectedValue.ToString();
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
            ddlDistrict.SelectedIndex = -1;
            ddlAssembly.SelectedIndex = -1;
            ddlAssembly.Items.Clear();
            ddlOpertor.Items.Clear();
            ddlLocation.Items.Clear();
        }
        public DataSet getDataSet()
        {
            string cnString = ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(cnString);

            // string CommandText12 = "select * from(select top 2 * from boothhistory where boothid in (" + lblboothID.Text +","+ lblBoothID1.Text + ") and isdelete = 1 order by id desc)tbl union  select* from boothhistory where boothid in(" + lblboothID.Text + "," + lblBoothID1.Text + ") and isdelete = 0";
            string CommandText2 = "GetLatestBoothHistoryByID";
            SqlCommand sqlCommand = new SqlCommand(CommandText2, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@BoothId", lblboothID.Text);
         //   sqlCommand.Parameters.AddWithValue("@BoothId1", lblBoothID1.Text);
            sqlCommand.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter sqlDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;

            DataSet dataSet = new DataSet();

            try
            {

                sqlDataAdapter.Fill(dataSet, "header");
                sqlConnection.Close();
            }
            catch (Exception _Exception)
            {
                sqlConnection.Close();

                return null;
            }

            return dataSet;

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
                    messageBody = messageBody + htmlTdStart + Row["updatedFrom"] + htmlTdEnd;
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
                message.Subject = "Map Camera Activity In Booth";
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
    }
}