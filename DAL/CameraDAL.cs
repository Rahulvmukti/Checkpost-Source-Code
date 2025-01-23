using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace exam.DAL
{
    public class CameraDAL
    {
        public string connstr = Common.DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ToString());
        public string table_prefix = ConfigurationManager.AppSettings["tb_prefix"].ToString();
        public DataSet GetDevices()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from streamlist where stateid= "+ ConfigurationManager.AppSettings["stateid"].ToString();
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDevices()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet UpdateDevice(string deviceId, string str_prourl, string str_mediaservername)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "update  streamlist";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetDevices()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }
        public DataSet GetProvisingServers()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from (select *, (select count(*) from streamlist where prourl =provisioningURL)used from provisioningserverlist ) a where used<maxDevices";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetMediaServers()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }

        public DataSet GetMediaServers()
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                //string query = "select * from (SELECT mediaserverurl,maxDevices, Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'') mediaserver, (select count(*) from streamlist where servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'')) used from mediaserverlist) a where used<maxDevices";
               // string query = "select * from (SELECT mediaserverurl,maxDevices, Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),RIGHT(mediaserverurl,CHARINDEX('/',REVERSE(mediaserverurl))),'') mediaserver, (select count(*) from streamlist where servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100), SUBSTRING(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100), CHARINDEX('/', SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100)), 100),''))used from mediaserverlist) a where used<maxDevices";
                string query = "select * from (SELECT mediaserverurl,maxDevices, Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),RIGHT(mediaserverurl,CHARINDEX(':',REVERSE(mediaserverurl))),'') mediaserver, (select count(*) from streamlist where servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100), SUBSTRING(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100), CHARINDEX('/', SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100)), 100),''))used from mediaserverlist) a where used<maxDevices";
               
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Common.Log("GetMediaServers()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }
            return ds;
        }
        public string IsCameraExist(string device_id, string streamname)
        {
            DataSet ds = new DataSet();
            string returnMsg = "Success";
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "SELECT * FROM " + table_prefix + "streamlist WHERE deviceid=@deviceid";
                SqlCommand command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@deviceid", device_id);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    returnMsg = "Error: DeviceId already exists.";
                    return returnMsg;
                }
                ds.Clear();

                query = "SELECT * FROM " + table_prefix + "streamlist WHERE streamname=@streamname";
                command = new SqlCommand(query, conn);
                command.Parameters.AddWithValue("@streamname", streamname);
                da = new SqlDataAdapter(command);
                da.Fill(ds);
                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    returnMsg = "Error: StreamName already exists.";
                    return returnMsg;
                }
                ds.Clear();
                da.Dispose();
                conn.Close();
                return returnMsg;
            }
            catch (Exception ex)
            {
                Common.Log("IsCameraExist()--> " + ex.Message);
            }
            finally
            {
                ds.Clear();
                conn.Close();
            }
            return returnMsg;
        }

        public string isMediaServerExists(string servername, string str_deviceid)
        {
            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(connstr);
            try
            {
                string query = "select * from (SELECT mediaserverurl,maxDevices,Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),RIGHT(mediaserverurl,CHARINDEX(':',REVERSE(mediaserverurl))),'') mediaserver, (select count(*) from streamlist where  deviceid!='" + str_deviceid + "' and servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'')) used from mediaserverlist) a where mediaserver='" + servername + "'";
               // string query = "select * from (SELECT mediaserverurl,maxDevices,Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),RIGHT(mediaserverurl,CHARINDEX('/',REVERSE(mediaserverurl))),'') mediaserver, (select count(*) from streamlist where  deviceid!='" + str_deviceid + "' and servername =Replace(SUBSTRING(mediaserverurl, CHARINDEX('://', mediaserverurl) + 3, 100),SUBSTRING(mediaserverurl, CHARINDEX('.', mediaserverurl), 100),'')) used from mediaserverlist) a where mediaserver='" + servername + "'";
                SqlCommand command = new SqlCommand(query, conn);
                SqlDataAdapter da = new SqlDataAdapter(command);
                da.Fill(ds);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (Convert.ToInt32(ds.Tables[0].Rows[0]["maxDevices"]) > Convert.ToInt32(ds.Tables[0].Rows[0]["used"]))
                          { return ""; }
                else
                    return "'" + servername + "'" + " already allotted to " + ds.Tables[0].Rows[0]["maxDevices"] + " devices.";
                }
                else
                    return "'" + servername + "' not exists on mediaserverlist table";
            }
            catch (Exception ex)
            {
                Common.Log("isMediaServerExists()--> " + ex.Message);
            }
            finally
            {
                //ds.Clear();
                conn.Close();
            }

            return "";
        }

        public bool deleteDevice(int id)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;
                query = "delete from streamlist where id=" + id;

                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
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
        public bool addStreamList(string prourl, string deviceid, string streamname, string servername)
        {
            SqlConnection SQLconn = new SqlConnection(connstr);
            try
            {
                string query = string.Empty;
                string tblname = string.Empty;

                string CameraMessage = IsCameraExist(deviceid, streamname);
                if (CameraMessage.Contains("Error"))
                {
                    //update existing
                    query = "update " + table_prefix + "streamlist set [prourl]='"+ prourl + "',[streamname]='"+ streamname + "',[servername]='"+ servername + "'" +
                        ",[schoolid]='99999',[status]='STOPPED',[stateID]="+ ConfigurationManager.AppSettings["stateid"].ToString() +",[IsEnable]=1,[isLive]=0,[hasalarm]=0,[lastseen]=NULL,[stop]=0,[isdownload]=0,[isvod]=0," +
                        "[selected]=0,[statusFlag]=0,[streamnamebkp]=NULL,[jionum]=NULL where [deviceid]='"+ deviceid +"'";
                }
                else
                {//add new
                    query = "INSERT INTO " + table_prefix + "streamlist ([prourl],[deviceid],[streamname],[servername],[schoolid],[status],[stateID],[IsEnable],[isLive],[hasalarm],[lastseen],[stop],[isdownload],[isvod],[selected],[statusFlag],[streamnamebkp],[jionum]) " +
                    "VALUES ('" + prourl + "','" + deviceid + "','" + streamname + "','" + servername + "',99999,'STOPPED'," + ConfigurationManager.AppSettings["stateid"].ToString() + ",1,0,0,'1970-01-01 00:00:00.000',0,0,0,0,0,NULL,NULL);";
                }
                SqlCommand SQLcommand = new SqlCommand(query, SQLconn);
                SQLconn.Open();
                SQLcommand.ExecuteNonQuery();
                SQLconn.Close();
                return true;
            }
            catch (Exception ex)
            {
                Common.Log("addStreamList()--> " + ex.Message);
                return false;
            }
            finally
            {
                SQLconn.Close();
            }
        }
    }
}