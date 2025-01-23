using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace exam.did
{
    public partial class excel : System.Web.UI.Page
    {
        public string connstr = ConfigurationManager.ConnectionStrings["connectionstr_download"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["phase_stateid"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DataSet ds = LoaddataReport();
                Reportview.DataSource = ds;
                Reportview.DataBind();

            }
        }

        private DataSet LoaddataReport()
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DataSet ds = new DataSet();

                string query = "select district,pc as 'Block Code', schoolname as Block,psnum as PSNumber,location,streamname as Camera, T_5,T_6,T_7,T_8,T_9,T_10,T_11,T_12,T_13,T_14,T_15,T_16,T_17,T_18 from booth" + table_prefix + " b inner join streamlist" + table_prefix + " s on b.streamid = s.id inner join dt_" + ConfigurationManager.AppSettings["phase_date"].ToString() + " dt ON b.id=dt.boothid where b.isdisplay='True' and s.IsEnable=1 and b.boothstateID='" + ConfigurationManager.AppSettings["stateid"].ToString() + "' order by district,pc,schoolname";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                conn.Open();
                da.Fill(ds);

                return ds;
            }
            catch (Exception ex)
            {
                return new DataSet();
            }
            finally
            {
                conn.Close();
            }
        }

        protected void btnreportdst_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=" + ConfigurationManager.AppSettings["titlename"].ToString() + "ALLBooth" + DateTime.Now.ToString("ddMMyyyy") + ".xls");
                Response.ContentEncoding = System.Text.Encoding.Unicode;
                Response.BinaryWrite(System.Text.Encoding.Unicode.GetPreamble());
                Response.ContentType = "application/vnd.ms-excel";
                DataSet ds = new DataSet();
                ds = LoaddataReport();
                // create a string writer
                //ds.Tables[0].Columns[0].ColumnName = "District";
                //ds.Tables[0].Columns[1].ColumnName = "PC Name";
                //ds.Tables[0].Columns[2].ColumnName = "Assembly";
                //ds.Tables[0].Columns[3].ColumnName = "PS No.";
                //ds.Tables[0].Columns[4].ColumnName = "Location";
                //ds.Tables[0].Columns[5].ColumnName = "T_5";
                //ds.Tables[0].Columns[6].ColumnName = "T_6";
                //ds.Tables[0].Columns[7].ColumnName = "T_7";

                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                    {
                        DataGrid dg = new DataGrid();
                        dg.DataSource = ds.Tables[0];
                        dg.DataBind();
                        foreach (DataGridItem i in dg.Items)
                        {
                            foreach (TableCell tc in i.Cells)

                                tc.Attributes.Add("class", "text");
                        }
                        dg.RenderControl(htw);
                        string style = @"<style> .text { mso-number-format:\@; } </style> ";
                        Response.Write(style);
                        Response.Output.Write(sw.ToString());
                        Response.Flush();
                        Response.End();
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Confirms that an HtmlForm control is rendered for the specified ASP.NET
               server control at run time. */
        }

    }
}