using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace exam.DAL
{
    public class ServerSettingDAL
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();

        public DataSet GetMediaserver()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from MediaServerList order by id desc";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetMediaserver()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet GetProvisioningServer()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from provisioningserverlist order by id desc";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetProvisioningServer()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet GetProvisioningServerbyID(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from provisioningserverlist where id="+id+" order by id desc";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetProvisioningServerbyID()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet GetServerbyID(int id)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from (SELECT id,mediaserverurl,maxDevices, Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'') mediaserver, (select count(*) from streamlist where servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'')) used from mediaserverlist) a where id=" + id;
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetServerbyID()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet getDeviceIdsByProURL(string proURL)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from streamlist where prourl='"+ proURL +"'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("getDeviceIdsByProURL()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet getDeviceIdsByServername(string servername)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from streamlist where servername='" + servername + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("getDeviceIdsByServername()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public bool deleteMediaServer(int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                query = "delete from MediaServerList where id=" + id;
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("deleteMediaServer()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public bool deleteProvisioningServer(int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                query = "delete from provisioningserverlist where id=" + id;
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("deleteProvisioningServer()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public bool addMediaServer(string Mservername, string Maxdevices, int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;

                if (id <= 0)
                {//add new
                    query = "INSERT INTO " + table_prefix + "MediaServerList ([MediaServerURL],[maxDevices]) " +
                    "VALUES ('" + Mservername + "','" + Maxdevices + "');";
                }
                else
                {
                    //edit
                    query = "update " + table_prefix + "MediaServerList  set [MediaServerURL]='" + Mservername + "',[maxDevices]='" + Maxdevices + "' where id= " + id;
                }
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("addMediaServer()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }

        public bool addProvisioningServer(string Mservername, string Maxdevices, int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;

                if (id <= 0)
                {//add new
                    query = "INSERT INTO " + table_prefix + "provisioningserverlist ([provisioningURL],[maxDevices]) " +
                    "VALUES ('" + Mservername + "','" + Maxdevices + "');";
                }
                else
                {
                    //edit
                    query = "update " + table_prefix + "provisioningserverlist  set [provisioningURL]='" + Mservername + "',[maxDevices]='" + Maxdevices + "' where id= " + id;
                }
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("addProvisioningServer()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }
    }
}