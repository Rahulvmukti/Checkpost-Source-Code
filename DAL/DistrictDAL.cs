using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace exam.DAL
{
    public class DistrictDAL
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        public string IsDistrictExist(string DT_NAME, string AC_NAME, string AC_NO)
        {
            DataSet ds = new DataSet();
            string returnMsg = "Success";
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT * FROM " + table_prefix + "district WHERE [district]='"+ DT_NAME  + "' and acname='"+ AC_NAME + "' and accode="+ AC_NO + " and stateid= '"+ System.Configuration.ConfigurationManager.AppSettings["stateid"].ToString() + "'";
                SqlCommand command = new SqlCommand(query, conn);
               
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    returnMsg = "Error: District entry already exists.";
                    return returnMsg;
                }
                ds.Clear();
                da.Dispose();
                conn.Close();
                return returnMsg;
            }
            catch (Exception ex)
            {
                Common.Log("IsDistrictExist()--> " + ex.Message);
            }
            finally
            {
                ds.Clear();
                conn.Close();
            }
            return returnMsg;
        }
        public DataSet GetDistrict(int stateid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from district where stateid='" + stateid + "' order by id desc";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDistrict()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }
        public bool deleteDistrict( int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try { 
            string query = string.Empty;
            string tblname = string.Empty;
            query = "delete from booth where district=(select [district] from district where id=" + id + ") and acname=(select acname from district where id=" + id + ") ";
                  
            SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
            SQLconn.Open();
            SQLcommand.ExecuteNonQuery();
			 query = "delete from district where id=" + id;
                SQLcommand = new SqlCommand(query, SQLconn);
                SQLcommand.ExecuteNonQuery();
                
            SQLconn.Close();
            return true;
            }
            catch (Exception ex)
            {
                Common.Log("deleteDistrict()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }
        public DataSet getUniqueDistricts()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select distinct district  from district where stateid=" + System.Configuration.ConfigurationManager.AppSettings["stateid"].ToString()+" order by district"; 
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("getUniqueDistricts()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }
        public bool addDistrict(string DT_NAME, string AC_NAME, string AC_NO, int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;

                if(id<=0)
                {//add new
                  query = "INSERT INTO " + table_prefix + "district ([district],[acname],[accode],[isenable],[stateid]) " +
                    "VALUES ('" + DT_NAME + "','" + AC_NAME + "','" + AC_NO + "',1," + System.Configuration.ConfigurationManager.AppSettings["stateid"].ToString() + ");";
                }
                else
                {
                    //edit
                    query = "update " + table_prefix + "district  set [acname]='" + AC_NAME + "',[accode]='" + AC_NO + "',[stateid]='" + ConfigurationManager.AppSettings["stateid"].ToString() + "'  where id= " + id;
                    

                }
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("addDistrict()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }
    }
}