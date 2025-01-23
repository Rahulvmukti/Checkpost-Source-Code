using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace exam.did
{
    public partial class Default : System.Web.UI.Page
    {
        public string connstr = ConfigurationManager.ConnectionStrings["connectionstr"].ToString();
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoaddataReport();
            }
        }

        private void LoaddataReport()
        {
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                DataSet ds = new DataSet();

                string query = "select * from " + table_prefix + "booth b inner join " + table_prefix + "streamlist s on b.streamid = s.id where b.isdisplay='True' and s.IsEnable=1 and b.boothstateID='" + ConfigurationManager.AppSettings["stateid"].ToString() + "' Order By s.id";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                conn.Open();
                da.Fill(ds);
                Reportview.DataSource = ds;
                Reportview.DataBind();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                conn.Close();
            }
        }
       
    }
}