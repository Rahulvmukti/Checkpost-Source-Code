using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;

namespace exam
{
    /// <summary>
    /// Summary description for downloadhandler
    /// </summary>
    public class downloadhandler : IHttpHandler, IRequiresSessionState
    {
        
        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            string boothUploadErrorRepotName = ConfigurationManager.AppSettings["BoothUploadError"].ToString();
            DataTable dt = new DataTable();
            dt = Session["ErrorDt"] == null ? new DataTable(): (DataTable)Session["ErrorDt"];
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt, "Error");
                context.Response.Clear();
                context.Response.Buffer = true;
                context.Response.Charset = "";
                context.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                context.Response.AddHeader("content-disposition", "attachment;filename=" + boothUploadErrorRepotName + DateTime.Now.ToString() + ".xlsx");
                using (MemoryStream MyMemoryStream = new MemoryStream())
                {
                    wb.SaveAs(MyMemoryStream);
                    MyMemoryStream.WriteTo(context.Response.OutputStream);
                    //Response.Flush();
                    context.Response.End();
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        private HttpSessionState Session
        {
            get
            {
                return HttpContext.Current.Session;
            }
        }

    }
}