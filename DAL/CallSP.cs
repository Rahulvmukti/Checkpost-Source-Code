using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace exam.DAL
{
    public class CallSP
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());


        public DataSet GetFolderListWithCount(string username, string password)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connstr))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("vmukti_GetUserData_1", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = username;
                    sql_cmnd.Parameters.AddWithValue("@password", SqlDbType.NVarChar).Value = password;

                    SqlDataAdapter da = new SqlDataAdapter(sql_cmnd);
                    da.Fill(ds);
                    sqlCon.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet GetUserDataFromUsername(string username)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connstr))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("vmukti_GetUserDataFromUsername", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@username", SqlDbType.NVarChar).Value = username;

                    SqlDataAdapter da = new SqlDataAdapter(sql_cmnd);
                    da.Fill(ds);
                    sqlCon.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataSet CreateDynamicHrTables(DateTime Startdate,DateTime EndDate)
        {
            try
            {
                DataSet ds = new DataSet();
                SqlConnection sqlCon = null;
                using (sqlCon = new SqlConnection(connstr))
                {
                    sqlCon.Open();
                    SqlCommand sql_cmnd = new SqlCommand("vmukti_CreateDynamicHrsTable", sqlCon);
                    sql_cmnd.CommandType = CommandType.StoredProcedure;
                    sql_cmnd.Parameters.AddWithValue("@startDate", SqlDbType.Date).Value = Startdate.Date;
                    sql_cmnd.Parameters.AddWithValue("@endDate", SqlDbType.Date).Value = EndDate.Date;

                    SqlDataAdapter da = new SqlDataAdapter(sql_cmnd);
                    da.Fill(ds);
                    sqlCon.Close();
                }
                return ds;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}