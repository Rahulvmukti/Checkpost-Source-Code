using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MqttLib;
using System.Configuration;
using System.Threading;
using exam.BAL;
using System.Web.UI.HtmlControls;
using System.Web.Services;


namespace exam
{
    public partial class camerasetting : System.Web.UI.Page
    {
        IMqtt client;

        public string deviceid
        {
            get;
            set;
        }

        public string prourl
        {
            get;
            set;
        }
       
        public string msgtoset
        {
            get
            {
                if (ViewState["msgtoset"] != null)
                    return (string)ViewState["msgtoset"];
                else
                    return null;
            }
            set
            {
                ViewState["msgtoset"] = value;
            }
        }
        public int msgtype
        {
            get
            {
                if (ViewState["msgtype"] != null)
                    return (int)ViewState["msgtype"];
                else
                    return CommonConfig.MSG_TYPE_GET_CONFIG;
            }
            set
            {
                ViewState["msgtype"] = value;
            }
        }
        public string host = ConfigurationManager.AppSettings["mqttserver"].ToString(); //"tcp://pro.ambicam.com:1883";	// hostname or IP address
        
        public string dbName = "vmukti";
        public string tbName = "VCam1_1";
        public string topic = "hr-0002";
        public int gMyId;

        Vutil Vutil = new Vutil();
        public string strwifi
        {
            get;
            set;
        }
        public string cid = string.Empty;
        ManualResetEvent publishdone = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.User.Identity.IsAuthenticated)
            {
                if (Request.QueryString["did"] != null && Request.QueryString["prourl"] != null)
                {
                    deviceid = Request.QueryString["did"];
                    prourl = "tcp://" + Request.QueryString["prourl"];
                }
                if (!string.IsNullOrEmpty(deviceid))
                {
                    if (!IsPostBack)
                    {
                        msgtype = CommonConfig.MSG_TYPE_GET_CONFIG;
                        ConnectMQTT();
                    }
                }
            }
        }

      
        private void ConnectMQTT()
        {
            try
            {
                publishdone = new ManualResetEvent(false);
                Random r = new Random();
                topic = "webPc-" + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10);
                gMyId = Vutil.VUtil_getStreamId(topic);
                client = MqttClientFactory.CreateClient(prourl, topic);
                client.Connected += new ConnectionDelegate(client_Connected);
                client.ConnectionLost += new ConnectionDelegate(_client_ConnectionLost);
                client.PublishArrived += new PublishArrivedDelegate(client_PublishArrived);

                if (msgtype == CommonConfig.MSG_TYPE_GET_CONFIG)
                {
                    client.Connect(true);
                    publishdone.WaitOne();
                    client.Disconnect();
                }
            }
            catch (Exception ex)
            {

            }
        }
        private void client_Connected(object sender, EventArgs e)
        {
            RegisterSubscriptions();
            if (msgtype == CommonConfig.MSG_TYPE_GET_CONFIG)
            {
                string struuid = deviceid;
                string request = "<uuid name=" + struuid + " >";
                int requestLen = request.Length + 1;
                int[] arraybuffer = new int[requestLen + 16];
                MQTTMessage mqttmessage = new MQTTMessage();

                mqttmessage.srcId = gMyId;
                mqttmessage.msgType = CommonConfig.MSG_TYPE_GET_CONFIG;
                mqttmessage.msgLen = requestLen;
                mqttmessage.dstId = 0;
                int offset = Vutil.VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);
                for (int i = 0; i < request.Length; i++)
                {
                    arraybuffer[offset++] = (int)request[i];
                }
                arraybuffer[offset++] = 0;
                byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                MqttPayload payload = new MqttPayload(bytes, 0);
                PublishMqtt(dbName + "/" + tbName + "/" + "tx/" + topic, payload);

            }
            else if (msgtype == CommonConfig.MSG_TYPE_SET_CONFIG)
            {
                appSettings set_appsetting = JSONDeSerialize(msgtoset);

                set_appsetting.displayCfg.hue = Convert.ToInt32(hf_imghue.Value);
                set_appsetting.displayCfg.brightness = Convert.ToInt32(hf_imgbright.Value);
                set_appsetting.displayCfg.contrast = Convert.ToInt32(hf_imgcontrast.Value);
                set_appsetting.displayCfg.saturation = Convert.ToInt32(hf_imgsaturation.Value);

                set_appsetting.displayCfg.ircutmode = ircut.SelectedIndex + 1;
                set_appsetting.imageCfg.flip = flip_on_off.Checked ? 1 : 0;
                set_appsetting.imageCfg.mirror = mirror_on_off.Checked ? 1 : 0;
                set_appsetting.imageCfg.wdr = wdr.SelectedIndex;


                //if (quality_level.Value == "High")
                //{
                //    set_appsetting.videoCh012.bps = 100;
                //    set_appsetting.videoCh012.fps = 10;
                //    set_appsetting.videoCh012.gop = 40;
                //    set_appsetting.videoCh012.brmode = 2;
                //    set_appsetting.videoCh012.width = 640;
                //    set_appsetting.videoCh012.height = 360;
                //    set_appsetting.videoCh012.bmainstream = 1;
                //    set_appsetting.videoCh012.bfield = 0;
                //    set_appsetting.videoCh012.piclevel = 2;
                //    set_appsetting.videoCh012.fixqplevel = 2;

                //    set_appsetting.videoCh011.bps = 500;
                //    set_appsetting.videoCh011.fps = 10;
                //    set_appsetting.videoCh011.gop = 40;
                //    set_appsetting.videoCh011.brmode = 2;
                //    set_appsetting.videoCh011.width = 1920;
                //    set_appsetting.videoCh011.height = 1080;
                //    set_appsetting.videoCh011.bmainstream = 1;
                //    set_appsetting.videoCh011.bfield = 0;
                //    set_appsetting.videoCh011.piclevel = 2;
                //    set_appsetting.videoCh011.fixqplevel = 2;
                //}
                //else if (quality_level.Value == "Low")
                //{
                //    set_appsetting.videoCh012.bps = 32;
                //    set_appsetting.videoCh012.fps = 5;
                //    set_appsetting.videoCh012.gop = 20;
                //    set_appsetting.videoCh012.brmode = 2;
                //    set_appsetting.videoCh012.width = 320;
                //    set_appsetting.videoCh012.height = 180;
                //    set_appsetting.videoCh012.bmainstream = 1;
                //    set_appsetting.videoCh012.bfield = 0;
                //    set_appsetting.videoCh012.piclevel = 4;
                //    set_appsetting.videoCh012.fixqplevel = 4;

                //    set_appsetting.videoCh011.bps = 150;
                //    set_appsetting.videoCh011.fps = 10;
                //    set_appsetting.videoCh011.gop = 40;
                //    set_appsetting.videoCh011.brmode = 2;
                //    set_appsetting.videoCh011.width = 1280;
                //    set_appsetting.videoCh011.height = 720;
                //    set_appsetting.videoCh011.bmainstream = 1;
                //    set_appsetting.videoCh011.bfield = 0;
                //    set_appsetting.videoCh011.piclevel = 2;
                //    set_appsetting.videoCh011.fixqplevel = 2;
                //    set_appsetting.streamCfg.isHd = 1;
                //}
                //else if (quality_level.Value == "2G")
                //{
                //    set_appsetting.videoCh012.bps = 32;
                //    set_appsetting.videoCh012.fps = 5;
                //    set_appsetting.videoCh012.gop = 20;
                //    set_appsetting.videoCh012.brmode = 2;
                //    set_appsetting.videoCh012.width = 320;
                //    set_appsetting.videoCh012.height = 180;
                //    set_appsetting.videoCh012.bmainstream = 1;
                //    set_appsetting.videoCh012.bfield = 0;
                //    set_appsetting.videoCh012.piclevel = 5;
                //    set_appsetting.videoCh012.fixqplevel = 5;

                //    set_appsetting.videoCh011.bps = 150;
                //    set_appsetting.videoCh011.fps = 10;
                //    set_appsetting.videoCh011.gop = 40;
                //    set_appsetting.videoCh011.brmode = 2;
                //    set_appsetting.videoCh011.width = 1280;
                //    set_appsetting.videoCh011.height = 720;
                //    set_appsetting.videoCh011.bmainstream = 1;
                //    set_appsetting.videoCh011.bfield = 0;
                //    set_appsetting.videoCh011.piclevel = 2;
                //    set_appsetting.videoCh011.fixqplevel = 2;
                //    set_appsetting.streamCfg.isHd = 1;
                //}
                //else
                //{
                //    set_appsetting.videoCh012.bps = 60;
                //    set_appsetting.videoCh012.fps = 5;
                //    set_appsetting.videoCh012.gop = 20;
                //    set_appsetting.videoCh012.brmode = 2;
                //    set_appsetting.videoCh012.width = 640;
                //    set_appsetting.videoCh012.height = 360;
                //    set_appsetting.videoCh012.bmainstream = 1;
                //    set_appsetting.videoCh012.bfield = 0;
                //    set_appsetting.videoCh012.piclevel = 3;
                //    set_appsetting.videoCh012.fixqplevel = 3;

                //    set_appsetting.videoCh011.bps = 150;
                //    set_appsetting.videoCh011.fps = 10;
                //    set_appsetting.videoCh011.gop = 40;
                //    set_appsetting.videoCh011.brmode = 2;
                //    set_appsetting.videoCh011.width = 1280;
                //    set_appsetting.videoCh011.height = 720;
                //    set_appsetting.videoCh011.bmainstream = 1;
                //    set_appsetting.videoCh011.bfield = 0;
                //    set_appsetting.videoCh011.piclevel = 2;
                //    set_appsetting.videoCh011.fixqplevel = 2;
                //}

                //New Block


                if (quality_level.Value == "Normal")
                {
                    set_appsetting.videoCh012.bps = 200;
                    set_appsetting.videoCh012.fps = 15;
                    set_appsetting.videoCh012.gop = 40;
                    set_appsetting.videoCh012.brmode = 2;
                    set_appsetting.videoCh012.width = 640;
                    set_appsetting.videoCh012.height = 360;
                    set_appsetting.videoCh012.bmainstream = 1;
                    set_appsetting.videoCh012.bfield = 0;
                    set_appsetting.videoCh012.piclevel = 2;
                    set_appsetting.videoCh012.fixqplevel = 2;

                    set_appsetting.videoCh011.bps = 200;
                    set_appsetting.videoCh011.fps = 15;
                    set_appsetting.videoCh011.gop = 40;
                    set_appsetting.videoCh011.brmode = 2;
                    set_appsetting.videoCh011.width = 1920;
                    set_appsetting.videoCh011.height = 1080;
                    set_appsetting.videoCh011.bmainstream = 1;
                    set_appsetting.videoCh011.bfield = 0;
                    set_appsetting.videoCh011.piclevel = 2;
                    set_appsetting.videoCh011.fixqplevel = 2;
                    set_appsetting.streamCfg.isHd = 0;
                }
                else if (quality_level.Value == "FHD")
                {
                    set_appsetting.videoCh012.bps = 512;
                    set_appsetting.videoCh012.fps = 20;
                    set_appsetting.videoCh012.gop = 40;
                    set_appsetting.videoCh012.brmode = 2;
                    set_appsetting.videoCh012.width = 640;
                    set_appsetting.videoCh012.height = 360;
                    set_appsetting.videoCh012.bmainstream = 1;
                    set_appsetting.videoCh012.bfield = 0;
                    set_appsetting.videoCh012.piclevel = 4;
                    set_appsetting.videoCh012.fixqplevel = 4;

                    set_appsetting.videoCh011.bps = 512;
                    set_appsetting.videoCh011.fps = 20;
                    set_appsetting.videoCh011.gop = 40;
                    set_appsetting.videoCh011.brmode = 2;
                    set_appsetting.videoCh011.width = 1920;
                    set_appsetting.videoCh011.height = 1080;
                    set_appsetting.videoCh011.bmainstream = 1;
                    set_appsetting.videoCh011.bfield = 0;
                    set_appsetting.videoCh011.piclevel = 2;
                    set_appsetting.videoCh011.fixqplevel = 2;
                    set_appsetting.streamCfg.isHd = 1;
                }
                else if (quality_level.Value == "HD")
                {
                    set_appsetting.videoCh012.bps = 350;
                    set_appsetting.videoCh012.fps = 20;
                    set_appsetting.videoCh012.gop = 40;
                    set_appsetting.videoCh012.brmode = 2;
                    set_appsetting.videoCh012.width = 640;
                    set_appsetting.videoCh012.height = 360;
                    set_appsetting.videoCh012.bmainstream = 1;
                    set_appsetting.videoCh012.bfield = 0;
                    set_appsetting.videoCh012.piclevel = 5;
                    set_appsetting.videoCh012.fixqplevel = 5;

                    set_appsetting.videoCh011.bps = 350;
                    set_appsetting.videoCh011.fps = 20;
                    set_appsetting.videoCh011.gop = 40;
                    set_appsetting.videoCh011.brmode = 2;
                    set_appsetting.videoCh011.width = 1920;
                    set_appsetting.videoCh011.height = 1080;
                    set_appsetting.videoCh011.bmainstream = 1;
                    set_appsetting.videoCh011.bfield = 0;
                    set_appsetting.videoCh011.piclevel = 2;
                    set_appsetting.videoCh011.fixqplevel = 2;
                    set_appsetting.streamCfg.isHd = 1;
                }
                else if (quality_level.Value == "3G")
                {
                    set_appsetting.videoCh012.bps = 100;
                    set_appsetting.videoCh012.fps = 10;
                    set_appsetting.videoCh012.gop = 40;
                    set_appsetting.videoCh012.brmode = 2;
                    set_appsetting.videoCh012.width = 640;
                    set_appsetting.videoCh012.height = 360;
                    set_appsetting.videoCh012.bmainstream = 1;
                    set_appsetting.videoCh012.bfield = 0;
                    set_appsetting.videoCh012.piclevel = 5;
                    set_appsetting.videoCh012.fixqplevel = 5;

                    set_appsetting.videoCh011.bps = 100;
                    set_appsetting.videoCh011.fps = 10;
                    set_appsetting.videoCh011.gop = 40;
                    set_appsetting.videoCh011.brmode = 2;
                    set_appsetting.videoCh011.width = 640;
                    set_appsetting.videoCh011.height = 360;
                    set_appsetting.videoCh011.bmainstream = 1;
                    set_appsetting.videoCh011.bfield = 0;
                    set_appsetting.videoCh011.piclevel = 2;
                    set_appsetting.videoCh011.fixqplevel = 2;
                    set_appsetting.streamCfg.isHd = 0;
                }
                else
                {
                    set_appsetting.videoCh012.bps = 50;
                    set_appsetting.videoCh012.fps = 5;
                    set_appsetting.videoCh012.gop = 40;
                    set_appsetting.videoCh012.brmode = 2;
                    set_appsetting.videoCh012.width = 640;
                    set_appsetting.videoCh012.height = 360;
                    set_appsetting.videoCh012.bmainstream = 1;
                    set_appsetting.videoCh012.bfield = 0;
                    set_appsetting.videoCh012.piclevel = 3;
                    set_appsetting.videoCh012.fixqplevel = 3;

                    set_appsetting.videoCh011.bps = 50;
                    set_appsetting.videoCh011.fps = 5;
                    set_appsetting.videoCh011.gop = 40;
                    set_appsetting.videoCh011.brmode = 2;
                    set_appsetting.videoCh011.width = 640;
                    set_appsetting.videoCh011.height = 360;
                    set_appsetting.videoCh011.bmainstream = 1;
                    set_appsetting.videoCh011.bfield = 0;
                    set_appsetting.videoCh011.piclevel = 2;
                    set_appsetting.videoCh011.fixqplevel = 2;
                    set_appsetting.streamCfg.isHd = 0;
                }
                //End New Block







                string msgStr = Vutil.VUtil_encodeMsg(set_appsetting);
                int[] arraybuffer = new int[msgStr.Length + 16];
                MQTTMessage mqttmessage = new MQTTMessage();

                mqttmessage.srcId = gMyId;
                mqttmessage.msgType = CommonConfig.MSG_TYPE_SET_CONFIG;
                mqttmessage.msgLen = msgStr.Length;
                mqttmessage.dstId = 0;
                int offset = Vutil.VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);

                for (int i = 0; i < msgStr.Length; i++)
                {
                    arraybuffer[offset++] = (int)msgStr[i];
                }

                byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                MqttPayload payload = new MqttPayload(bytes, 0);
                PublishMqtt(dbName + "/" + tbName + "/" + "tx/" + topic, payload);

                Thread.Sleep(2000);
                publishdone.Set();
            }
            else if (msgtype == CommonConfig.MSG_TYPE_REBOOT)
            {
                string struuid = "PPCN-198852-HUBJK";
                string request = "<uuid name=" + struuid + " >";
                int requestLen = request.Length + 1;
                int[] arraybuffer = new int[requestLen + 16];
                MQTTMessage mqttmessage = new MQTTMessage();

                mqttmessage.srcId = gMyId;
                mqttmessage.msgType = CommonConfig.MSG_TYPE_REBOOT;
                mqttmessage.msgLen = requestLen;
                mqttmessage.dstId = 0;
                int offset = Vutil.VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);
                for (int i = 0; i < request.Length; i++)
                {
                    arraybuffer[offset++] = (int)request[i];
                }
                arraybuffer[offset++] = 0;
                byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                MqttPayload payload = new MqttPayload(bytes, 0);
                PublishMqtt(dbName + "/" + tbName + "/" + "tx/" + topic, payload);
                Thread.Sleep(2000);
                publishdone.Set();
            }

        }

        private void PublishMqtt(string topic, MqttPayload payload)
        {
            try
            {
                client.Publish(topic, payload, QoS.BestEfforts, false);
            }
            catch (Exception ex)
            {
            }
        }

        private void RegisterSubscriptions()
        {
            client.Subscribe(dbName + "/" + tbName + "/" + "rx/" + topic, QoS.BestEfforts);
        }

        private void _client_ConnectionLost(object sender, EventArgs e)
        {

        }
        bool client_PublishArrived(object sender, PublishArrivedArgs e)
        {
            HandleMessage(e);

            return true;
        }

        private void HandleMessage(PublishArrivedArgs e)
        {
            byte[] outbuffer = e.Payload.TrimmedBuffer;

            int[] bytesAsInts = outbuffer.Select(x => (int)x).ToArray();
            char[] bytesAschar = System.Text.Encoding.UTF8.GetString(outbuffer).ToCharArray();
            MQTTMessage resultmessage = Vutil.VUtil_decodeMsgHeader(bytesAsInts);
            string output_result = string.Empty;
            switch (resultmessage.msgType)
            {
                case CommonConfig.MSG_TYPE_GET_CONFIG:
                    appSettings appSettings = processGetConfigMsg(bytesAschar, 16, resultmessage.msgLen);
                    msgtoset = JSONSerialize(appSettings);
                    InvokeMessage(appSettings);
                    publishdone.Set();
                    break;

                case CommonConfig.MSG_TYPE_SET_CONFIG:
                    processSetConfigMsg(bytesAschar, 16, resultmessage.msgLen);
                    break;

                default:
                    break;
            }
        }
        private void InvokeMessage(appSettings appSettings)
        {
            strwifi = appSettings.nwInfo.networktype;
            string get_config_values = string.Empty;

            flip_on_off.Checked = (0 == appSettings.imageCfg.flip) ? false : true;
            mirror_on_off.Checked = (0 == appSettings.imageCfg.mirror) ? false : true;

            wdr.SelectedIndex = appSettings.imageCfg.wdr;

            hf_imghue.Value = appSettings.displayCfg.hue.ToString();
            hf_imgbright.Value = appSettings.displayCfg.brightness.ToString();
            hf_imgcontrast.Value = appSettings.displayCfg.contrast.ToString();
            hf_imgsaturation.Value = appSettings.displayCfg.saturation.ToString();

            ircut.SelectedIndex = appSettings.displayCfg.ircutmode - 1;


            if (appSettings.videoCh012.bps == 512)
            {
                quality_level.SelectedIndex = 1;
            }
            else if (appSettings.videoCh012.bps == 350)
            {
                quality_level.SelectedIndex = 2;
            }
            else if (appSettings.videoCh012.bps == 200)
            {
                quality_level.SelectedIndex = 0;
            }
            else if (appSettings.videoCh012.bps == 100)
            {
                quality_level.SelectedIndex = 3;
            }
            else
            {
                quality_level.SelectedIndex = 4;
            }
            //if (quality_level.SelectedIndex == 0)
            //{
            //    lblaudio.InnerHtml = "8 kbps";
            //}
            //else
            //{
            //    lblaudio.InnerHtml = "4 kbps";
            //}
        }

        private string processGetStreamConfigMSg(char[] payload, int offset, int len)
        {
            //appSettings appSettings = Vutil.VUtil_decodeGetConfigMsg(payload, offset, len);
            //return JSONSerialize(appSettings.streamCfg);
            return null;
        }
        public string processStatusMsg(char[] payload, int offset, int len)
        {
            List<clientStats> clientStats = Vutil.VUtil_decodeStatusMsg(payload, offset, len);
            string str = "";

            for (var i = 0; i < (clientStats.Count - 1); i++)
            {
                if (1 == clientStats[i].isUp)
                {
                    str += clientStats[i].name;
                }
                else
                {
                    str += clientStats[i].name;
                }
                str += Environment.NewLine;
            }
            return str;
            //document.getElementById('statusTxt2').innerHTML = str;
        }

        private void processAlarmMsg(string topic)
        {

        }

        private void processGroupStatusMsg(char[] payload, int offset, int len)
        {

        }

        private void processDisconnectMsg(char[] payload, int offset, int len)
        {

        }

        private void processFileMsg(char[] payload, int offset, int len)
        {

        }

        private void processAudioMsg(char[] payload, int offset, int len)
        {

        }

        private void processSetConfigMsg(char[] payload, int offset, int len)
        {

        }

        private appSettings processGetConfigMsg(char[] payload, int offset, int len)
        {
            appSettings appSettings = Vutil.VUtil_decodeGetConfigMsg(payload, offset, len);
            strwifi = appSettings.nwInfo.networktype;
            // set values

            // updatepanelsetting.Update();
            return (appSettings);
        }

        private void processKeepAliveMsg(char[] payload, int offset, int len)
        {

        }
        private string JSONSerialize(appSettings objstreamsetting)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Serialize(objstreamsetting);
        }
        private appSettings JSONDeSerialize(string objstreamsetting)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return serializer.Deserialize<appSettings>(objstreamsetting);
        }


        protected void btnsaveconfig_Click(object sender, EventArgs e)
        {
            try
            {
                msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
                ConnectMQTT(); 
                client.Connect(true);
                publishdone.WaitOne();
                client.Disconnect();
               // Response.Redirect("ListView.aspx");
               // ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "window.parent.closeModal();", true);
                //ClientScript.RegisterStartupScript(this.GetType(), "CloseWindowScript", "window.close();", true);
                string script = "<script type=\"text/javascript\">window.close();</script>";
                ClientScript.RegisterStartupScript(this.GetType(), "CloseWindowScript", script);
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnclose_Click(object sender, EventArgs e)
        {
          Response.Redirect("ListView1.aspx");
           // Page.ClientScript.RegisterOnSubmitStatement(typeof(Page), "closePage", "window.onunload = CloseWindow();");
        }
    }
}