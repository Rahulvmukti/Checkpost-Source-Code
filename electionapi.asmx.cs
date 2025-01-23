using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace exam
{
    /// <summary>
    /// Summary description for electionapi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService] 
    [System.Web.Script.Services.ScriptService]
    public class electionapi : System.Web.Services.WebService
    {

        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());

        [WebMethod]

        public string SaveData(string jsonData)
        {
            try
            {
                // Parse the incoming JSON data
                JObject data = JObject.Parse(jsonData);
                string cameraId = data["CameraID"].ToString();
                string sendTime = data["RecordingDate"].ToString();
                string presentTime = data["PrersentTime"].ToString();
                string duration = data["Duration"].ToString();

                // Set up your SQL connection string
              //  string connstr = "Server=tcp:punjab.database.windows.net;Initial Catalog=DelhiECI;Persist Security Info=False;User ID=punjabfsv;Password=fsv@12345"; // Replace with your connection string

                using (SqlConnection connection = new SqlConnection(connstr))
                {
                    using (SqlCommand command = new SqlCommand("Sp_RecordingSize", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the stored procedure
                        command.Parameters.AddWithValue("@CameraID", cameraId);
                        command.Parameters.AddWithValue("@RecordingDate", sendTime);
                        command.Parameters.AddWithValue("@PrersentTime", presentTime);
                        command.Parameters.AddWithValue("@Duration", duration);
                        command.Parameters.AddWithValue("@flag", "SAVEDATA");
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                        return "Data saved successfully.";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}";
            }
        }
    }
}
