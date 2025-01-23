using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using exam.DAL;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using exam.BAL;
using System.Configuration;
using System.Data;
using System.Threading;
using MqttLib;
using System.Collections;
using exam.services;

namespace exam
{
    public partial class ptzcam : System.Web.UI.Page
    { 
        db_data dbContext = new db_data();
        public string setusertype()
        { 
            return usertype;
        }
        public string ptzevent
        {
            get
            {
                if (ViewState["ptzevent"] != null)
                    return (string)ViewState["ptzevent"];
                else
                    return string.Empty;
            }
            set
            {
                ViewState["ptzevent"] = value;
            }
        }
        public string utypeall
        {
            get
            {
                if (ViewState["utypeall"] != null)
                    return (string)ViewState["utypeall"];
                else
                    return "live";
            }
            set
            {
                ViewState["utypeall"] = value;
            }
        }
        public string usertype
        {
            get
            {
                if (ViewState["usertype"] != null)
                    return (string)ViewState["usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["usertype"] = value;
            }
        }
        public string _usertype
        {
            get
            {
                if (ViewState["_usertype"] != null)
                    return (string)ViewState["_usertype"];
                else
                    return "live";
            }
            set
            {
                ViewState["_usertype"] = value;
            }
        }
        public int userid
        {
            get
            {
                if (ViewState["userid"] != null)
                    return (int)ViewState["userid"];
                else
                    return 0;
            }
            set
            {
                ViewState["userid"] = value;
            }
        }


        public string servername
        {
            get
            {
                if (ViewState["servername"] != null)
                    return (string)ViewState["servername"];
                else
                    return "";
            }
            set
            {
                ViewState["servername"] = value;
            }
        }

        public string DriverName
        {
            get
            {
                if (ViewState["DriverName"] != null)
                    return (string)ViewState["DriverName"];
                else
                    return "";
            }
            set
            {
                ViewState["DriverName"] = value;
            }
        }

        public string DriverContactNo
        {
            get
            {
                if (ViewState["DriverContactNo"] != null)
                    return (string)ViewState["DriverContactNo"];
                else
                    return "";
            }
            set
            {
                ViewState["DriverContactNo"] = value;
            }
        }

        IMqtt client;

        public string deviceid
        {
            get;
            set;
        }
        public string ptzstreamname
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
        public string host = ConfigurationManager.AppSettings["mqttserver3"].ToString(); //"tcp://pro.ambicam.com:1883";	// hostname or IP address

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
        public int stateid
        {
            get
            {
                if (ViewState["stateid"] != null)
                    return (int)ViewState["stateid"];
                else
                    return 0;
            }
            set
            {
                ViewState["stateid"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Cache.SetNoStore();

                if (!Page.User.Identity.IsAuthenticated || Session["userType"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
                Session["MenuName"] = "PTZView";
                // deviceid = ptzdid;

                if (!IsPostBack)
                {
                    DataSet ds = dbContext.GetUserData(Page.User.Identity.Name);
                    usertype = ds.Tables[0].Rows[0]["usercode"].ToString();
                    stateid = Convert.ToInt32(ds.Tables[0].Rows[0]["stateid"]); 
                    userid = Convert.ToInt32(ds.Tables[0].Rows[0]["id"]);
                    utypeall = usertype;
                    if (utypeall.Contains("_all"))
                    {
                        usertype = usertype.Replace("_all", "");
                    }
                    if (utypeall.StartsWith("eci"))
                    {
                        usertype = usertype.Replace("eci_", "");
                    }
                    _usertype = usertype;
                    if (!string.IsNullOrEmpty(usertype))
                    {
                        LoadDistrict(usertype);
                        /* if (utypeall.Contains("_all"))
                         {

                             if (ddlDistrict.SelectedIndex > 0)
                             {
                                 usertype = "dst_" + ddlDistrict.SelectedItem.Text;

                                 LoadBooth(usertype);
                             }
                             else
                             {
                                 LoadBooth(usertype);
                             }
                         }
                         else if (utypeall.StartsWith("sch"))
                         {
                             usertype = "sch_" + ddlDistrict.SelectedItem.Text + "_" + ddlbooth.SelectedItem.Text;
                             LoadBooth(usertype);
                         }

                         else
                         {

                             usertype = "dst_" + ddlDistrict.SelectedItem.Text;
                             LoadBooth(usertype);
                         }*/
                    }
                    // msgtype = CommonConfig.MSG_TYPE_GET_CONFIG;
                    // ConnectMQTT();
                }

            }
            catch (Exception ex)
            {
                Common.Log("Page_Load_home() -- >  " + ex.Message);
            }
        }
        private void LoadDistrict(string usertype)
        {
            try
            {
                var Alldist = dbContext.GetAllDistrictByuserid(userid);
                ddlDistrict.DataSource = Alldist;
                ddlDistrict.DataTextField = "district";
                ddlDistrict.DataValueField = "district";
                ddlDistrict.DataBind();

                ddlDistrict.Items.Insert(0, new ListItem("ALL District", ""));
                ddlPC.Items.Insert(0, new ListItem("ALL Assembly", ""));
                VehicleDropDownList.Items.Insert(0, new ListItem("Select Vehicle", "0"));
            }
            catch (Exception ex)
            {
                Common.Log("LoadDistrict_list() -- >  " + ex.Message);
            }
        }

        protected void ddlDistrict_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ddlPC.Items.Clear();
                VehicleDropDownList.Items.Clear();
                string selpc = "";

                db_data dbContext = new db_data();
                var Assembly = dbContext.GetAllacnameByuserid(ddlDistrict.SelectedValue, userid);

                ddlPC.Items.Clear();
                ddlPC.DataSource = Assembly;
                ddlPC.DataTextField = "acname";
                ddlPC.DataValueField = "accode";
                ddlPC.DataBind();

                ddlPC.Items.Insert(0, new ListItem("Select Assembly", ""));
                VehicleDropDownList.Items.Insert(0, new ListItem("Select Vehicle", "0"));
                strm_txtBox.Text = ""; 
            }
            catch (Exception ex)
            {
                Common.Log("ddlDistrict_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }


        protected void ddlbooth_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    db_data dbContext = new db_data();
                    var vehicle = dbContext.GetShiftWizeVehicleByAcCode(ddlPC.SelectedItem.Value);

                    VehicleDropDownList.Items.Clear();
                    VehicleDropDownList.DataSource = vehicle;
                    VehicleDropDownList.DataTextField = "location";
                    VehicleDropDownList.DataValueField = "location";
                    VehicleDropDownList.DataBind();
                    VehicleDropDownList.Items.Insert(0, new ListItem("Select location", "0"));

                }
                catch (Exception ex)
                {
                    Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                Common.Log("ddlbooth_SelectedIndexChanged_list() -- >  " + ex.Message);
            }
        }

        protected void VehicleDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadBooth(usertype);
        }
        private void LoadBooth(string usertype)
        {
            try
            {
                usertype = setusertype();
                DataSet ds = new DataSet();
                ds = dbContext.GetBoothList(usertype, true, stateid);
                //var dsStatus = dbContext.GetPTZViewData(VehicleDropDownList.SelectedItem.Text).Tables[0].AsEnumerable().FirstOrDefault();
                var dsStatus = (strm_txtBox.Text == "")
                    ? dbContext.GetPTZViewData(VehicleDropDownList.SelectedItem.Text).Tables[0].AsEnumerable().FirstOrDefault()
                    : dbContext.GetPTZViewDatawithcamera(strm_txtBox.Text).Tables[0].AsEnumerable().FirstOrDefault();

                deviceid = dsStatus.Field<string>("deviceid");
                //    var VehicleType = dsStatus.Field<string>("VehicleType");
                var Vehicle_No = dsStatus.Field<string>("location");
                ptzstreamname = dsStatus.Field<string>("deviceid");
                servername = dsStatus.Field<string>("servername");
                //      DriverContactNo = dsStatus.Field<int>("operatorNumber").ToString();
                DriverName = dsStatus.Field<string>("operatorName");


                if (deviceid != null)
                {
                    msgtype = CommonConfig.MSG_TYPE_GET_CONFIG;
                    ConnectMQTT();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myModal", "LoadNodePlayer('" + ptzstreamname + "','" + servername + "','" + DriverContactNo + "','" + DriverName + "','" + "" + "','" + Vehicle_No + "','" + ddlDistrict.SelectedItem.Text + "','" + ddlPC.SelectedItem.Text + "')", true);
            }
            catch (Exception ex)
            {
                Common.Log("LoadBooth_list() -- >  " + ex.Message);
            }
        }
          
        protected void btnup_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "up";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }
        protected void btndown_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "down";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }

        protected void btnleft_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "left";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }

        protected void btnright_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "right";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }

        protected void btnzoomin_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "near";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }

        protected void btnzoomout_Click(object sender, EventArgs e)
        {
            msgtype = CommonConfig.MSG_TYPE_SET_CONFIG;
            ptzevent = "far";
            ConnectMQTT();

            client.Connect(true);
            publishdone.WaitOne(5000);
            client.Disconnect();
        }
        private void ConnectMQTT()
        {
            try
            {
                publishdone = new ManualResetEvent(false);
                Random r = new Random();
                topic = "webPc-" + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10);
                gMyId = Vutil.VUtil_getStreamId(topic);
                client = MqttClientFactory.CreateClient(host, topic);
                client.Connected += new ConnectionDelegate(client_Connected);
                client.ConnectionLost += new ConnectionDelegate(_client_ConnectionLost);
                client.PublishArrived += new PublishArrivedDelegate(client_PublishArrived);

                if (msgtype == CommonConfig.MSG_TYPE_GET_CONFIG)
                {
                    client.Connect(true);
                    publishdone.WaitOne(5000);
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

                if (set_appsetting != null)
                {
                    if (ptzevent == "up")
                    {
                        int u1 = Convert.ToInt32(hf_up.Value);
                        if (u1 > 1000)
                        {
                            u1 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = 0;
                        set_appsetting.ptzinfo.rightPos = 0;
                        set_appsetting.ptzinfo.upPos = u1;
                        set_appsetting.ptzinfo.downPos = 0;
                        set_appsetting.ptzinfo.farPos = 0;
                        set_appsetting.ptzinfo.nearPos = 0;
                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }
                    else if (ptzevent == "down")
                    {
                        int u2 = Convert.ToInt32(hf_down.Value);
                        if (u2 > 1000)
                        {
                            u2 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = 0;
                        set_appsetting.ptzinfo.rightPos = 0;
                        set_appsetting.ptzinfo.upPos = 0;
                        set_appsetting.ptzinfo.downPos = u2;
                        set_appsetting.ptzinfo.farPos = 0;
                        set_appsetting.ptzinfo.nearPos = 0;
                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }
                    else if (ptzevent == "left")
                    {
                        int u3 = Convert.ToInt32(hf_left.Value);
                        if (u3 > 1000)
                        {
                            u3 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = u3;
                        set_appsetting.ptzinfo.rightPos = 0;
                        set_appsetting.ptzinfo.upPos = 0;
                        set_appsetting.ptzinfo.downPos = 0;
                        set_appsetting.ptzinfo.farPos = 0;
                        set_appsetting.ptzinfo.nearPos = 0;

                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }
                    else if (ptzevent == "right")
                    {
                        int u4 = Convert.ToInt32(hf_right.Value);
                        if (u4 > 1000)
                        {
                            u4 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = 0;
                        set_appsetting.ptzinfo.rightPos = u4;
                        set_appsetting.ptzinfo.upPos = 0;
                        set_appsetting.ptzinfo.downPos = 0;
                        set_appsetting.ptzinfo.farPos = 0;
                        set_appsetting.ptzinfo.nearPos = 0;

                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }
                    else if (ptzevent == "far")
                    {
                        int u5 = Convert.ToInt32(hf_far.Value);
                        if (u5 > 1000)
                        {
                            u5 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = 0;
                        set_appsetting.ptzinfo.rightPos = 0;
                        set_appsetting.ptzinfo.upPos = 0;
                        set_appsetting.ptzinfo.downPos = 0;
                        set_appsetting.ptzinfo.farPos = u5;
                        set_appsetting.ptzinfo.nearPos = 0;

                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }
                    else if (ptzevent == "near")
                    {
                        int u6 = Convert.ToInt32(hf_near.Value);
                        if (u6 > 1000)
                        {
                            u6 = 1000;
                        }
                        set_appsetting.ptzinfo.leftPos = 0;
                        set_appsetting.ptzinfo.rightPos = 0;
                        set_appsetting.ptzinfo.upPos = 0;
                        set_appsetting.ptzinfo.downPos = 0;
                        set_appsetting.ptzinfo.farPos = 0;
                        set_appsetting.ptzinfo.nearPos = u6;

                        set_appsetting.ptzinfo.currTiltPos = 0;
                        set_appsetting.ptzinfo.currPanPos = 0;
                        set_appsetting.ptzinfo.currZoomPos = 0;
                    }

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
                    // InvokeMessage(appSettings);
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

            string get_config_values = string.Empty;

            msgtype = CommonConfig.MSG_TYPE_EMail_publishURL_cameraName;

            appSettings set_appsetting = JSONDeSerialize(msgtoset);

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
            if (objstreamsetting == null)
            {
                return null;
            }
            return serializer.Serialize(objstreamsetting);
        }
        private appSettings JSONDeSerialize(string objstreamsetting)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            if (string.IsNullOrEmpty(objstreamsetting))
            {
                return null;
            }
            return serializer.Deserialize<appSettings>(objstreamsetting);
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
           if (strm_txtBox.Text!="")
            {
                LoadBooth(usertype);
            }

        }
    }
}