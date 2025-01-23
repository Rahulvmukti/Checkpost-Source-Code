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
    public partial class LoginHistoryReport : System.Web.UI.Page
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

                Session["MenuName"] = "LoginHistoryReport";
                stateid = Convert.ToInt32(ConfigurationManager.AppSettings["stateid"]);

                if (!IsPostBack)
                {
                    FromDt.Text = DateTime.Now.ToString("dd/MM/yyyy");
                    GetUserLoginHistoryReport();
                }
                ScriptManager1.RegisterPostBackControl(this.btnExportPDF);
            }
            catch (Exception ex)
            {

            }
        }

        private void GetUserLoginHistoryReport()
        {
            DataSet ds = new DataSet();
            string dt = FromDt.Text.Trim().Split('/')[2] + "-" + FromDt.Text.Trim().Split('/')[1] + "-" + FromDt.Text.Trim().Split('/')[0];
            dsReport = _data.GetUserLoginHistoryReport(dt);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            GetUserLoginHistoryReport();
        }

        protected void btnExportPDF_Click(object sender, EventArgs e)
        {
            string content = "";
            var sb = new StringBuilder();
            divReport.RenderControl(new HtmlTextWriter(new StringWriter(sb)));
            content = sb.ToString();

            DateTime currentTime = TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=User_Login_History_Report_" + currentTime.ToString("ddMMyyyyhhmmss") + ".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);

            string headertext = "";
            if (FromDt.Text.Trim() != "")
            {
                headertext = "<h2><div style='text-align:center;'>User Login History Report on " + FromDt.Text.Trim() + "</div></h2><br/><br/>";
            }
            else
            {
                headertext = "<h2><div style='text-align:center;'>User Login History Report</div></h2><br/><br/>";
            }

            string legendtext = "";
            legendtext = "<br/><br/><div style='text-align:right;'>This is system genereted report. Printed on " + currentTime.ToString("dd/MM/yyyy hh:mm:ss tt") + "</div>";
            StringWriter stringWriter = new StringWriter();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(stringWriter);
            divReport.RenderControl(htmlTextWriter);
            StringReader stringReader = new StringReader(headertext + stringWriter.ToString() + legendtext);
            iTextSharp.text.Document Doc = new iTextSharp.text.Document(iTextSharp.text.PageSize.A4, 10, 10, 100, 0);
            HTMLWorker htmlparser = new HTMLWorker(Doc);
            PdfWriter.GetInstance(Doc, Response.OutputStream);
            Doc.Open();
            htmlparser.Parse(stringReader);
            Doc.Close();

            Response.Write(Doc);
            Response.End();
        }
    }
}