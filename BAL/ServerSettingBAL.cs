using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
namespace exam.BAL
{
    public class ServerSettingBAL
    {
        DAL.ServerSettingDAL districtdal = new DAL.ServerSettingDAL();
        public DataSet GetMediaserver()
        {
            DataSet dsDevices = districtdal.GetMediaserver();
            return dsDevices;
        }
        public DataSet GetProvisioningServer()
        {
            DataSet dsDevices = districtdal.GetProvisioningServer();
            return dsDevices;
        }
        public string addServer(string Mservername, string Maxdevices, int id)
        {
            try
            {
                districtdal.addMediaServer(Mservername, Maxdevices, id);
                return "Success";
            }
            catch (Exception ex)
            {
                Log.logmessage("addServer()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }

        public string getProvisioningServerbyID(int id)
        {
            try
            {
                DataSet ds= districtdal.GetProvisioningServerbyID(id);
                
                if(ds!=null && ds.Tables.Count>0 && ds.Tables[0].Rows.Count>0)
                {
                    return ds.Tables[0].Rows[0]["provisioningURL"].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                Log.logmessage("getProvisioningServerbyID()", ex.Message.ToString());
                return ""; //fail
            }
        }

        public string getServerbyID(int id)
        {
            try
            {
                DataSet ds = districtdal.GetServerbyID(id);

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0]["mediaserver"].ToString() + "$" + ds.Tables[0].Rows[0]["mediaserverurl"].ToString(); 
                }
                return "";
            }
            catch (Exception ex)
            {
                Log.logmessage("getProvisioningServerbyID()", ex.Message.ToString());
                return ""; //fail
            }
        }
        public string addProvisioningServer(string Mservername, string Maxdevices, int id)
        {
            try
            {
                districtdal.addProvisioningServer(Mservername, Maxdevices, id);
                return "Success";
            }
            catch (Exception ex)
            {
                Log.logmessage("addServer()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }
        public DataSet getDeviceIdsByProURL(string proURL)
        {
            try
            {
                DataSet ds= districtdal.getDeviceIdsByProURL(proURL);
                return ds;
            }
            catch (Exception ex)
            {
                Log.logmessage("getDeviceIdsByProURL()", ex.Message.ToString());
                return null; //fail
            }
        }

        public DataSet getDeviceIdsByServername(string servername)
        {
            try
            {
                DataSet ds = districtdal.getDeviceIdsByServername(servername);
                return ds;
            }
            catch (Exception ex)
            {
                Log.logmessage("addServer()", ex.Message.ToString());
                return null; //fail
            }
        }
        public bool deleteMediaServer(int id)
        {
            try
            {
                return districtdal.deleteMediaServer(id);

            }
            catch (Exception ex)
            {
                Log.logmessage("deleteMediaServer()", ex.Message.ToString());
                return false; //fail
            }
        }
        public bool deleteProvisioningServer(int id)
        {
            try
            {
                return districtdal.deleteProvisioningServer(id);

            }
            catch (Exception ex)
            {
                Log.logmessage("deleteProvisioningServer()", ex.Message.ToString());
                return false; //fail
            }
        }
    }
}