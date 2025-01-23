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
    public class CameraBAL
    {
        DAL.CameraDAL cameradal = new DAL.CameraDAL();
        public DataSet GetMediaservers()
        {
            DataSet dsMediaServers = cameradal.GetMediaServers();
            return dsMediaServers;
        }
        public DataSet GetProvisingServers()
        {
            DataSet dsMediaServers = cameradal.GetProvisingServers();
            return dsMediaServers;
        }
        public string allotMediaserver(DataSet dsMediaServers)
        {
            string mediaserver = "";
            
            foreach(DataRow dr in dsMediaServers.Tables[0].Rows)
            {
                //string url = dr["mediaserver"].ToString();
                int used = Convert.ToInt32(dr["used"].ToString());
                int maxDevices = dr["maxDevices"] != DBNull.Value ? Convert.ToInt32(dr["maxDevices"].ToString()) : 200;
                if (used<maxDevices)
                {
                    mediaserver = dr["mediaserver"].ToString() + "$" + dr["mediaserverurl"].ToString(); 
                    dr["used"] = used + 1;
                    break;
                }
            }
            return mediaserver;

        }

        public string allotProvisingServer(DataSet dsprovisioningservers)
        {
            string mediaserver = "";

            foreach (DataRow dr in dsprovisioningservers.Tables[0].Rows)
            {
                //string url = dr["mediaserver"].ToString();
                int used = Convert.ToInt32(dr["used"].ToString());
                int maxDevices = dr["maxDevices"]!=DBNull.Value ?  Convert.ToInt32(dr["maxDevices"].ToString()) : 10000;

                if (used < maxDevices)
                {
                    mediaserver = dr["provisioningURL"].ToString();
                    dr["used"] = used + 1;
                    break;
                }
            }
            return mediaserver;

        }
        public string ValidateExcelData( string deviceid, string streamname)
        {
            try
            {
                string cameraid = string.Empty;
                if (deviceid.Contains(" "))
                {
                    return "Error: DeviceID contains space.";
                }
                else if (deviceid.Length != 17)
                {
                    return "Error: DeviceID Invalid.";
                }
                else
                {
                    string CameraMessage = cameradal.IsCameraExist(deviceid, streamname);
                    if (CameraMessage.Contains("Error"))
                    {
                        return CameraMessage;
                    }
                }
                return "Success";

            }
            catch (Exception ex)
            {
                Log.logmessage("ValidateExcelData()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }
        public bool deleteDevice(int id)
        {
            try
            {
                return cameradal.deleteDevice(id);

            }
            catch (Exception ex)
            {
                Log.logmessage("deleteDevice()", ex.Message.ToString());
                return false; //fail
            }
        }
        public DataSet GetDevices()
        {
            DataSet dsDevices = cameradal.GetDevices();
            return dsDevices;
        }
        public string IsMediaServerExists(string servername, string str_deviceid)
        {
            return cameradal.isMediaServerExists(servername, str_deviceid);

        }
        public string Updatecamera(string prourl, string deviceid, string streamname, string str_mediaserverurl)
        {
            MQTTCommand mqttcls = new MQTTCommand();
            string mqttresult = mqttcls.SetStreamConfig("test@ambicam.com", deviceid, str_mediaserverurl, streamname, prourl);
            if (mqttresult.Contains("ERROR:"))
            {
                return mqttresult;
            }
            return "";
        }
        public string addupdatecamera(string prourl, string deviceid, string streamname, string servername, string str_mediaserverurl, bool replace)
        {
            try
            {
                string cameraid = string.Empty;

                string result = ValidateExcelData( deviceid, streamname);
                if (result.Contains("Error") && replace == false)
                {
                    return result;
                }
                else
                {
                    //upload to DB
                    cameradal.addStreamList(prourl, deviceid, streamname, servername);
                    //upload to provisioning. 
                    MQTTCommand mqttcls = new MQTTCommand();
                    //rtmp://SERVER.vmukti.com:80/live-record/CAMNAME
                    if (str_mediaserverurl == "")
                    {
                        str_mediaserverurl = "rtmp://" + servername + "/live-record/" + deviceid;
                    }
                    string mqttresult = mqttcls.SetStreamConfig("test@ambicam.com", deviceid, str_mediaserverurl, streamname, prourl);
                    if (mqttresult.Contains("ERROR:"))
                    {
                        return mqttresult;
                    }
                    //Thread.Sleep(1000);
                    //mqttcls = new MQTTCommand();
                    //mqttresult = mqttcls.SetStreamConfig("test@ambicam.com", deviceid, "rtmp://" + servername + ".vmukti.com:80/live-record/" + streamname, streamname);
                    //if (mqttresult.Contains("ERROR:"))
                    //{
                    //    return mqttresult;
                    //}
                    return "Success";
                }
            }
            catch (Exception ex)
            {
                Log.logmessage("addcamera()", ex.Message.ToString());
                return "Fail"; //fail
            }
        }
    }
}