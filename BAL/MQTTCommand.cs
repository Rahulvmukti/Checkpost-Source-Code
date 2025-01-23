using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MqttLib;
using System.Configuration;

namespace exam.BAL
{
    public class MQTTCommand
    {

        public string SetStreamConfigAdmin(int onoffflag, string email, string cameradeviceid, string cameraurl)
        {
            try
            {
                Random r = new Random();
                string topic = "webPc-" + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10);

                string host = ConfigurationManager.AppSettings["mqttserver"].ToString();
                IMqtt client = MqttClientFactory.CreateClient(host, topic);
                int gMyId = VUtil_getStreamId(topic);
                client.Connect(true);
                int flag = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (client.IsConnected)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    #region SetStreaConfig
                    streamCfg streamCfg = new streamCfg();
                    streamCfg.enableAudio = 1;
                    if (onoffflag == 1)
                        streamCfg.enabled = 1;
                    else
                        streamCfg.enabled = 0;
                    streamCfg.enableTelnet = 0;
                    streamCfg.isHd = 0;
                    streamCfg.mqttUrl = "tcp://pro.ambicam.com:1884";
                    streamCfg.publishUrl = cameraurl;
                    streamCfg.telnetUrl = "telnet.ambicam.com:8888";

                    string msgStr = encodeUuid(cameradeviceid);
                    msgStr += encodeGrUuid(email);
                    msgStr += encodeStreamCfg(streamCfg);
                    int[] arraybuffer = new int[msgStr.Length + 16];

                    MQTTMessage mqttmessage = new MQTTMessage();

                    mqttmessage.srcId = gMyId;
                    mqttmessage.msgType = 40;
                    mqttmessage.msgLen = msgStr.Length;
                    mqttmessage.dstId = 0;
                    int offset = VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);


                    for (int i = 0; i < msgStr.Length; i++)
                    {
                        arraybuffer[offset++] = (int)msgStr[i];
                    }
                    byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                    MqttPayload payload = new MqttPayload(bytes, 0);
                    client.Publish("vmukti/VCam1_1/tx/" + topic, payload, QoS.BestEfforts, false);
                    #endregion

                    #region Send OSDConfig
                    //osdCfg osdCfg = new osdCfg();
                    //osdCfg.cont_0 = "YYYY-MM-DD hh:mm:ss";
                    //osdCfg.cont_1 = cameraname;
                    //osdCfg.fontsize = 1;
                    //osdCfg.h_0 = 32;
                    //osdCfg.h_1 = 32;
                    //osdCfg.rgncnt = 2;
                    //osdCfg.show_0 = 1;
                    //osdCfg.show_1 = 1;
                    //osdCfg.w_0 = 304;
                    //osdCfg.w_1 = 112;
                    //osdCfg.x_0 = 928;
                    //osdCfg.x_1 = 64;
                    //osdCfg.y_0 = 32;
                    //osdCfg.y_1 = 32;

                    //string msgStr1 = encodeUuid(cameradeviceid);
                    //msgStr1 += encodeGrUuid(email);
                    //msgStr1 += encodeOsdCfg(osdCfg);
                    //int[] arraybuffer1 = new int[msgStr1.Length + 16];

                    //MQTTMessage mqttmessage1 = new MQTTMessage();

                    //mqttmessage1.srcId = gMyId;
                    //mqttmessage1.msgType = 45;
                    //mqttmessage1.msgLen = msgStr1.Length;
                    //mqttmessage1.dstId = 0;
                    //int offset1 = VUtil_encodeMsgHeader(ref arraybuffer1, mqttmessage1);

                    //for (int i = 0; i < msgStr1.Length; i++)
                    //{
                    //    arraybuffer1[offset1++] = (int)msgStr1[i];
                    //}

                    //byte[] bytes1 = arraybuffer1.Select(x => (byte)x).ToArray();
                    //MqttPayload payload1 = new MqttPayload(bytes1, 0);
                    //client.Publish("vmukti/VCam1_1/tx/" + topic, payload1, QoS.BestEfforts, false);
                    #endregion

                    #region Set email Config
                    //emailCfg emailCfg = new emailCfg();
                    //emailCfg.emailserver = "smtp.gmail.com";
                    //emailCfg.emailport = 465;
                    //emailCfg.ssl = 1;
                    //emailCfg.logintype = 1;
                    //emailCfg.emailusername = "alerts@ambicam.com";
                    //emailCfg.emailpassword = "v|c{azo777";
                    //emailCfg.from = "alerts@ambicam.com";
                    //emailCfg.to = email;
                    //emailCfg.subject = "Ambicam Motion Alert";
                    //emailCfg.text = "This is an ambicam email alert";
                    //emailCfg.attatchment = "";
                    //string msgStr2 = encodeUuid(cameradeviceid);
                    //msgStr2 += encodeGrUuid(email);
                    //msgStr2 += encodeEmailCfg(emailCfg);
                    //int[] arraybuffer2 = new int[msgStr2.Length + 16];

                    //MQTTMessage mqttmessage2 = new MQTTMessage();

                    //mqttmessage2.srcId = gMyId;
                    //mqttmessage2.msgType = 42;
                    //mqttmessage2.msgLen = msgStr2.Length;
                    //mqttmessage2.dstId = 0;
                    //int offset2 = VUtil_encodeMsgHeader(ref arraybuffer2, mqttmessage2);

                    //for (int i = 0; i < msgStr2.Length; i++)
                    //{
                    //    arraybuffer2[offset2++] = (int)msgStr2[i];
                    //}
                    //byte[] bytes2 = arraybuffer2.Select(x => (byte)x).ToArray();
                    //MqttPayload payload2 = new MqttPayload(bytes2, 0);
                    //client.Publish("vmukti/VCam1_1/tx/" + topic, payload2, QoS.BestEfforts, false); 
                    #endregion

                    return "SUCCESS:" + cameradeviceid;
                }
                else
                {
                    //notconnected
                    return "ERROR:500";
                }

            }
            catch (Exception ex)
            {
                Log.logmessage("SetStreamConfig()", ex.Message.ToString());
                return "ERROR:501";
            }
        }

        public string SetStreamConfig(string email, string cameradeviceid, string cameraurl, string cameraname, string mqttUrl)
        {
            try
            {
                Random r = new Random();
                string topic = "webPc-" + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10) + (int)(r.NextDouble() * 10);

                string host = ConfigurationManager.AppSettings["mqttserver"].ToString();


                IMqtt client = MqttClientFactory.CreateClient(host, topic);
                int gMyId = VUtil_getStreamId(topic);
                client.Connect(true);
                int flag = 0;
                for (int i = 0; i < 1000; i++)
                {
                    if (client.IsConnected)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 1)
                {
                    #region SetStreaConfig
                    //streamCfg streamCfg = new streamCfg();
                    //streamCfg.enableAudio = 1;
                    //streamCfg.enabled = 1;
                    //streamCfg.enableTelnet = 0;
                    //streamCfg.isHd = 0;
                    //streamCfg.mqttUrl = "tcp://pro.ambicam.com:1883";
                    //streamCfg.publishUrl = cameraurl;
                    //streamCfg.telnetUrl = "telnet.ambicam.com:8888";

                    //string msgStr = encodeUuid(cameradeviceid);
                    //msgStr += encodeGrUuid(email);
                    //msgStr += encodeStreamCfg(streamCfg);
                    //int[] arraybuffer = new int[msgStr.Length + 16];

                    //MQTTMessage mqttmessage = new MQTTMessage();

                    //mqttmessage.srcId = gMyId;
                    //mqttmessage.msgType = 40;
                    //mqttmessage.msgLen = msgStr.Length;
                    //mqttmessage.dstId = 0;
                    //int offset = VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);


                    //for (int i = 0; i < msgStr.Length; i++)
                    //{
                    //    arraybuffer[offset++] = (int)msgStr[i];
                    //}
                    //byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                    //MqttPayload payload = new MqttPayload(bytes, 0);
                    //client.Publish("vmukti/VCam1_1/tx/" + topic, payload, QoS.BestEfforts, false);
                    #endregion

                    #region Send OSDConfig
                    //osdCfg osdCfg = new osdCfg();
                    //osdCfg.cont_0 = "YYYY-MM-DD hh:mm:ss";
                    //osdCfg.cont_1 = cameraname;
                    //osdCfg.fontsize = 1;
                    //osdCfg.h_0 = 32;
                    //osdCfg.h_1 = 32;
                    //osdCfg.rgncnt = 2;
                    //osdCfg.show_0 = 1;
                    //osdCfg.show_1 = 1;
                    //osdCfg.w_0 = 304;
                    //osdCfg.w_1 = 112;
                    //osdCfg.x_0 = 928;
                    //osdCfg.x_1 = 64;
                    //osdCfg.y_0 = 32;
                    //osdCfg.y_1 = 32;

                    //string msgStr1 = encodeUuid(cameradeviceid);
                    //msgStr1 += encodeGrUuid(email);
                    //msgStr1 += encodeOsdCfg(osdCfg);
                    //int[] arraybuffer1 = new int[msgStr1.Length + 16];

                    //MQTTMessage mqttmessage1 = new MQTTMessage();

                    //mqttmessage1.srcId = gMyId;
                    //mqttmessage1.msgType = 45;
                    //mqttmessage1.msgLen = msgStr1.Length;
                    //mqttmessage1.dstId = 0;
                    //int offset1 = VUtil_encodeMsgHeader(ref arraybuffer1, mqttmessage1);

                    //for (int i = 0; i < msgStr1.Length; i++)
                    //{
                    //    arraybuffer1[offset1++] = (int)msgStr1[i];
                    //}

                    //byte[] bytes1 = arraybuffer1.Select(x => (byte)x).ToArray();
                    //MqttPayload payload1 = new MqttPayload(bytes1, 0);
                    //client.Publish("vmukti/VCam1_1/tx/" + topic, payload1, QoS.BestEfforts, false);
                    #endregion

                    #region Set email Config
                    //emailCfg emailCfg = new emailCfg();
                    //emailCfg.emailserver = "smtp.gmail.com";
                    //emailCfg.emailport = 465;
                    //emailCfg.ssl = 1;
                    //emailCfg.logintype = 1;
                    //emailCfg.emailusername = "alerts@ambicam.com";
                    //emailCfg.emailpassword = "v|c{azo777";
                    //emailCfg.from = "alerts@ambicam.com";
                    //emailCfg.to = email;
                    //emailCfg.subject = "Ambicam Motion Alert";
                    //emailCfg.text = "This is an ambicam email alert";
                    //emailCfg.attatchment = "";
                    //string msgStr2 = encodeUuid(cameradeviceid);
                    //msgStr2 += encodeGrUuid(email);
                    //msgStr2 += encodeEmailCfg(emailCfg);
                    //int[] arraybuffer2 = new int[msgStr2.Length + 16];

                    //MQTTMessage mqttmessage2 = new MQTTMessage();

                    //mqttmessage2.srcId = gMyId;
                    //mqttmessage2.msgType = 42;
                    //mqttmessage2.msgLen = msgStr2.Length;
                    //mqttmessage2.dstId = 0;
                    //int offset2 = VUtil_encodeMsgHeader(ref arraybuffer2, mqttmessage2);

                    //for (int i = 0; i < msgStr2.Length; i++)
                    //{
                    //    arraybuffer2[offset2++] = (int)msgStr2[i];
                    //}
                    //byte[] bytes2 = arraybuffer2.Select(x => (byte)x).ToArray();
                    //MqttPayload payload2 = new MqttPayload(bytes2, 0);
                    //client.Publish("vmukti/VCam1_1/tx/" + topic, payload2, QoS.BestEfforts, false); 
                    #endregion

                    appSettings appSettings = getSettingDefault(cameraurl, email, cameradeviceid, cameraname, mqttUrl);

                    string msgStr = VUtil_encodeMsg(appSettings);

                    int[] arraybuffer = new int[msgStr.Length + 16];

                    MQTTMessage mqttmessage = new MQTTMessage();

                    mqttmessage.srcId = gMyId;
                    mqttmessage.msgType = CommonConfig.MSG_TYPE_SET_CONFIG;
                    mqttmessage.msgLen = msgStr.Length;
                    mqttmessage.dstId = 0;

                    int offset = VUtil_encodeMsgHeader(ref arraybuffer, mqttmessage);


                    for (int i = 0; i < msgStr.Length; i++)
                    {
                        arraybuffer[offset++] = (int)msgStr[i];
                    }
                    byte[] bytes = arraybuffer.Select(x => (byte)x).ToArray();
                    MqttPayload payload = new MqttPayload(bytes, 0);
                    client.Publish("vmukti/VCam1_1/tx/" + topic, payload, QoS.BestEfforts, false);

                    return "SUCCESS:" + cameradeviceid;
                }
                else
                {
                    //notconnected
                    return "ERROR:500";
                }

            }
            catch (Exception ex)
            {
                Log.logmessage("SetStreamConfig()", ex.Message.ToString());
                return "ERROR:501";
            }
        }


        public MQTTMessage VUtil_decodeMsgHeader(int[] buff)
        {
            MQTTMessage mqttmessage = new MQTTMessage();


            /* Parse srcId */
            mqttmessage.srcId = VUtil_parseInt(buff, 0, 4);

            /* Parse dstId */
            mqttmessage.dstId = VUtil_parseInt(buff, 4, 8);

            /* Parse msgType */
            mqttmessage.msgType = VUtil_parseInt(buff, 8, 12);

            /* Parse msgLen */
            mqttmessage.msgLen = VUtil_parseInt(buff, 12, 16);

            return mqttmessage;
        }
        public int VUtil_parseInt(int[] array, int start, int end)
        {
            int i = 0;
            int j = 0;
            int retVal = 0;

            for (i = start; i < end; i++)
            {
                retVal |= (array[i] & 0x0FF) << (8 * j);
                j++;
            }

            return retVal;
        }
        public string VUtil_encodeMsg(appSettings appSettings)
        {
            string strMsg = "";
            strMsg += encodeUuid(appSettings.uuid);
            strMsg += encodeGrUuid(appSettings.grUuid);
            strMsg += encodeStreamCfg(appSettings.streamCfg);
            strMsg += encodeTimeCfg(appSettings.timeCfg);
            strMsg += encodeEmailCfg(appSettings.emailCfg);
            strMsg += encodeVideoCfg(appSettings.videoCh011, appSettings.videoCh012, appSettings.videoCh013);
            strMsg += encodeDisplayCfg(appSettings.displayCfg);
            strMsg += encodeOsdCfg(appSettings.osdCfg);
            strMsg += encodeRecordCfg(appSettings.recordCh011, appSettings.recordCh012, appSettings.recordSch, appSettings.rectime);
            strMsg += encodeImageCfg(appSettings.imageCfg);
            strMsg += encodeMdCfg(appSettings.mdCfg);
            strMsg += encodeDevInfo(appSettings.devInfo);
            strMsg += encodeNwInfo(appSettings.nwInfo);
            strMsg += "\0";
            return strMsg;
        }
        private int VUtil_encodeMsgHeader(ref int[] buff, MQTTMessage msgHdr)
        {
            int offset;
            offset = 0;
            offset += VUtil_packInt(ref buff, offset, msgHdr.srcId);
            offset += VUtil_packInt(ref buff, offset, msgHdr.dstId);
            offset += VUtil_packInt(ref buff, offset, msgHdr.msgType);
            offset += VUtil_packInt(ref buff, offset, msgHdr.msgLen);

            return offset;
        }
        private int VUtil_packInt(ref int[] array, int offset, int val)
        {

            //arraybuffer[offset] = (val >> 0) & 0x0FF;
            array[offset++] = (val >> 0) & 0x0FF;
            //arraybuffer[offset] = (val >> 8) & 0x0FF;
            array[offset++] = (val >> 8) & 0x0FF;
            // arraybuffer[offset] = (val >> 16) & 0x0FF;
            array[offset++] = (val >> 16) & 0x0FF;
            //   arraybuffer[offset] = (val >> 24) & 0x0FF;
            array[offset++] = (val >> 24) & 0x0FF;
            return 4;
        }
        private int VUtil_getStreamId(string stream)
        {
            int streamId = -2;

            streamId = 0;
            for (int i = 0; i < stream.Length; i++)
            {
                streamId += (int)stream[i];//CharUnicodeInfo.GetDigitValue(stream, i);// stream.charCodeAt(i);
            }

            return streamId;
        }
        private string encodeUuid(string uuid)
        {
            string msg = "";

            msg += addStartTag("Uuid");
            msg += addTag("uuid", uuid);
            msg += addEndTag("Uuid");

            return msg;
        }
        private string encodeGrUuid(string grUuid)
        {
            string msg = "";

            msg += addStartTag("GrUuid");
            msg += addTag("grUuid", grUuid);
            msg += addEndTag("GrUuid");

            return msg;
        }

        private string encodeStreamCfg(streamCfg cfg)
        {
            string msg = "";

            msg += addStartTag("StreamCfg");
            msg += addTag("enabled", cfg.enabled.ToString());
            msg += addTag("enableAudio", cfg.enableAudio.ToString());
            msg += addTag("publishUrl", cfg.publishUrl);
            msg += addTag("mqttUrl", cfg.mqttUrl);
            msg += addTag("enableTelnet", cfg.enableTelnet.ToString());
            msg += addTag("telnetUrl", cfg.telnetUrl.ToString());
            msg += addTag("isHd", cfg.isHd.ToString());
            msg += addEndTag("StreamCfg");

            return msg;
        }
        public string encodeOsdCfg(osdCfg cfg)
        {
            string msg = "";

            msg += addStartTag("OsdCfg");
            msg += addTag("rgncnt", cfg.rgncnt.ToString());
            msg += addTag("fontsize", cfg.fontsize.ToString());

            msg += addTag("x_0", cfg.x_0.ToString());
            msg += addTag("y_0", cfg.y_0.ToString());
            msg += addTag("w_0", cfg.w_0.ToString());
            msg += addTag("h_0", cfg.h_0.ToString());
            msg += addTag("cont_0", cfg.cont_0);
            msg += addTag("show_0", cfg.show_0.ToString());

            msg += addTag("x_1", cfg.x_1.ToString());
            msg += addTag("y_1", cfg.y_1.ToString());
            msg += addTag("w_1", cfg.w_1.ToString());
            msg += addTag("h_1", cfg.h_1.ToString());
            msg += addTag("cont_1", cfg.cont_1.ToString());
            msg += addTag("show_1", cfg.show_1.ToString());
            msg += addEndTag("OsdCfg");

            return msg;
        }
        public string encodeEmailCfg(emailCfg cfg)
        {
            string msg = "";

            msg += addStartTag("EmailCfg");
            msg += addTag("emailserver", cfg.emailserver);
            msg += addTag("emailport", cfg.emailport.ToString());
            msg += addTag("ssl", cfg.ssl.ToString());
            msg += addTag("logintype", cfg.logintype.ToString());
            msg += addTag("emailusername", cfg.emailusername);
            msg += addTag("emailpassword", cfg.emailpassword);
            msg += addTag("from", cfg.from);
            msg += addTag("to", cfg.to);
            msg += addTag("subject", cfg.subject);
            msg += addTag("text", cfg.text);
            msg += addTag("attatchment", cfg.attatchment);
            msg += addEndTag("EmailCfg");

            return msg;
        }
        public string encodeTimeCfg(timeCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_TIME_CFG);
            msg += addTag("time", cfg.time);
            msg += addTag("timeZone", cfg.timeZone);
            msg += addTag("tz", cfg.tz);
            msg += addTag("dstmode", cfg.dstmode.ToString());
            msg += addTag("autoupdate", cfg.autoupdate.ToString());
            msg += addTag("autoupdatetzonvif", cfg.autoupdatetzonvif.ToString());
            msg += addTag("ntpserver", cfg.ntpserver.ToString());
            msg += addTag("ntpinterval", cfg.ntpinterval.ToString());
            msg += addTag("ntpenable", cfg.ntpenable.ToString());
            msg += addEndTag(CommonConfig.TAG_TIME_CFG);

            return msg;
        }
        public string encodeVideoCfg(videoCh011 ch011, videoCh012 ch012, videoCh013 ch013)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_VIDEO_CFG);
            msg += addStartTag(CommonConfig.TAG_VIDEO_CH011);
            msg += addTag("bps", ch011.bps.ToString());
            msg += addTag("fps", ch011.fps.ToString());
            msg += addTag("gop", ch011.gop.ToString());
            msg += addTag("brmode", ch011.brmode.ToString());
            msg += addTag("piclevel", ch011.piclevel.ToString());
            msg += addTag("fixqplevel", ch011.fixqplevel.ToString());
            msg += addTag("width", ch011.width.ToString());
            msg += addTag("height", ch011.height.ToString());
            msg += addTag("bmainstream", ch011.bmainstream.ToString());
            msg += addTag("bfield", ch011.bfield.ToString());
            msg += addEndTag(CommonConfig.TAG_VIDEO_CH011);
            msg += addStartTag(CommonConfig.TAG_VIDEO_CH012);
            msg += addTag("bps", ch012.bps.ToString());
            msg += addTag("fps", ch012.fps.ToString());
            msg += addTag("gop", ch012.gop.ToString());
            msg += addTag("brmode", ch012.brmode.ToString());
            msg += addTag("piclevel", ch012.piclevel.ToString());
            msg += addTag("fixqplevel", ch012.fixqplevel.ToString());
            msg += addTag("width", ch012.width.ToString());
            msg += addTag("height", ch012.height.ToString());
            msg += addTag("bmainstream", ch012.bmainstream.ToString());
            msg += addTag("bfield", ch012.bfield.ToString());
            msg += addEndTag(CommonConfig.TAG_VIDEO_CH012);
            msg += addStartTag(CommonConfig.TAG_VIDEO_CH013);
            msg += addTag("bps", ch013.bps.ToString());
            msg += addTag("fps", ch013.fps.ToString());
            msg += addTag("gop", ch013.gop.ToString());
            msg += addTag("brmode", ch013.brmode.ToString());
            msg += addTag("piclevel", ch013.piclevel.ToString());
            msg += addTag("fixqplevel", ch013.fixqplevel.ToString());
            msg += addTag("width", ch013.width.ToString());
            msg += addTag("height", ch013.height.ToString());
            msg += addTag("bmainstream", ch013.bmainstream.ToString());
            msg += addTag("bfield", ch013.bfield.ToString());
            msg += addEndTag(CommonConfig.TAG_VIDEO_CH013);
            msg += addEndTag(CommonConfig.TAG_VIDEO_CFG);

            return msg;
        }

        public string encodeDisplayCfg(displayCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_DISPLAY_CFG);
            msg += addTag("hue", cfg.hue.ToString());
            msg += addTag("brightness", cfg.brightness.ToString());
            msg += addTag("saturation", cfg.saturation.ToString());
            msg += addTag("contrast", cfg.contrast.ToString());
            msg += addTag("ircutmode", cfg.ircutmode.ToString());
            msg += addEndTag(CommonConfig.TAG_DISPLAY_CFG.ToString());

            return msg;
        }
        public string encodeRecordCfg(recordCh011 ch1, recordCh012 ch2, recordSch recordSch, int rectime)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_RECORD_CFG);
            msg += addStartTag(CommonConfig.TAG_RECORD_CH011);
            msg += addTag("startTimerRec", ch1.startTimerRec.ToString());
            msg += addTag("startManualRec", ch1.startManualRec.ToString());
            msg += addTag("singlefiletime", ch1.singlefiletime.ToString());
            msg += addTag("enable", ch1.enable.ToString());
            msg += addTag("filepath", ch1.filepath);
            msg += addEndTag(CommonConfig.TAG_RECORD_CH011);
            msg += addStartTag(CommonConfig.TAG_RECORD_CH012);
            msg += addTag("startTimerRec", ch2.startTimerRec.ToString());
            msg += addTag("startManualRec", ch2.startManualRec.ToString());
            msg += addTag("singlefiletime", ch2.singlefiletime.ToString());
            msg += addTag("enable", ch2.enable.ToString());
            msg += addTag("filepath", ch2.filepath);
            msg += addEndTag(CommonConfig.TAG_RECORD_CH012);
            msg += addStartTag(CommonConfig.TAG_RECORD_SCH);
            msg += addTag("etm", recordSch.etm.ToString());
            msg += addSchArray("workday", recordSch.workday);
            msg += addSchArray("weekend", recordSch.weekend);
            msg += addSchArray("sun", recordSch.sun);
            msg += addSchArray("mon", recordSch.mon);
            msg += addSchArray("tue", recordSch.tue);
            msg += addSchArray("wed", recordSch.wed);
            msg += addSchArray("thu", recordSch.thu);
            msg += addSchArray("fri", recordSch.fri);
            msg += addSchArray("sat", recordSch.sat);
            msg += addEndTag(CommonConfig.TAG_RECORD_SCH);
            msg += addTag("rectime", rectime.ToString());
            msg += addEndTag(CommonConfig.TAG_RECORD_CFG);

            return msg;
        }

        public string encodeImageCfg(imageCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_IMAGE_CFG);
            msg += addTag("devno", cfg.devno.ToString());
            msg += addTag("chn", cfg.chn.ToString());
            msg += addTag("flip", cfg.flip.ToString());
            msg += addTag("mirror", cfg.mirror.ToString());
            msg += addTag("wdr", cfg.wdr.ToString());
            msg += addEndTag(CommonConfig.TAG_IMAGE_CFG);

            return msg;
        }

        public string encodeMdCfg(mdCfg cfg)
        {
            int index;
            string strBuff = "";
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_MD_CFG);
            msg += addTag("md_email_switch", cfg.md_email_switch.ToString());

            msg += addTag("md_snap_switch", cfg.md_snap_switch.ToString());
            msg += addTag("md_emailsnap_switch", cfg.md_emailsnap_switch.ToString());
            msg += addTag("md_ftpsnap_switch", cfg.md_ftpsnap_switch.ToString());

            msg += addTag("md_record_switch", cfg.md_record_switch.ToString());
            msg += addTag("md_ftprec_switch", cfg.md_ftprec_switch.ToString());

            msg += addTag("md_ioalmdo_switch", cfg.md_ioalmdo_switch.ToString());

            msg += addTag("etm", cfg.etm.ToString());
            msg += addTag("workday", cfg.workday.ToString());
            msg += addTag("weekend", cfg.weekend.ToString());
            msg += addTag("md_interval", cfg.md_interval.ToString());

            msg += addTag("MdbEnable", cfg.MdbEnable.ToString());
            msg += addTag("MdSensitiValue", cfg.MdSensitiValue.ToString());
            msg += addTag("MDThresholdValue", cfg.MDThresholdValue.ToString());
            msg += addTag("MdInterval", cfg.MdInterval.ToString());

            for (index = 0; index < 32; index++)
            {
                strBuff = "MdRegion[" + index + "]";
                msg += addTag(strBuff, cfg.MdRegion[index].ToString());
            }

            msg += addTag("md_alarm", cfg.md_alarm.ToString());
            msg += addTag("defend_alarm", cfg.defend_alarm.ToString());
            msg += addTag("tc_alarm", cfg.tc_alarm.ToString());
            msg += addEndTag(CommonConfig.TAG_MD_CFG);

            return msg;
        }

        public string encodeDevInfo(devInfo cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_DEV_INFO);
            msg += addTag("hwVer", cfg.hwVer.ToString());
            msg += addTag("swVer", cfg.swVer.ToString());
            msg += addTag("provisioningVer", cfg.provisioningVer.ToString());
            msg += addTag("publisherVer", cfg.publisherVer.ToString());
            msg += addTag("serialNo", cfg.serialNo);
            msg += addEndTag(CommonConfig.TAG_DEV_INFO);

            return msg;
        }

        public string encodeNwInfo(nwInfo cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_NET_INFO);
            msg += addTag("networktype", cfg.networktype);
            msg += addTag("macaddress", cfg.macaddress);
            msg += addTag("ip", cfg.ip);
            msg += addTag("netmask", cfg.netmask);
            msg += addTag("gateway", cfg.gateway);
            msg += addTag("sdnsip", cfg.sdnsip);
            msg += addTag("fdnsip", cfg.fdnsip);
            msg += addEndTag(CommonConfig.TAG_NET_INFO);

            return msg;
        }
        private string addStartTag(string tag)
        {
            string msg;
            msg = "<" + tag + ">\n";
            return msg;
        }
        private string addTag(string tag, string value)
        {
            string str;
            str = "<var " + tag + "=\"" + value + "\"/>\n";
            return str;
        }
        private string addEndTag(string tag)
        {
            string msg = "";
            msg = "</" + tag + ">\n";
            return msg;
        }
        public string addSchArray(string tag, List<int> sch)
        {
            string str;

            str = "<var " + tag + "=\"" +
                    sch[0] + "~" + sch[1] + "," +
                    sch[2] + "~" + sch[3] + "," +
                    sch[4] + "~" + sch[5] + "" +
                    "\"/>\n";

            return str;
        }
        private appSettings getSettingDefault(string cameraurl, string email, string deviceID, string cameraname, string mqttUrl)
        {
            appSettings appSettings = new appSettings();
            appSettings.streamCfg = new streamCfg();
            appSettings.devInfo = new devInfo();
            appSettings.displayCfg = new displayCfg();
            appSettings.emailCfg = new emailCfg();
            appSettings.imageCfg = new imageCfg();
            appSettings.mdCfg = new mdCfg();
            appSettings.nwInfo = new nwInfo();
            appSettings.osdCfg = new osdCfg();
            appSettings.recordCh011 = new recordCh011();
            appSettings.recordCh012 = new recordCh012();
            appSettings.recordSch = new recordSch();
            appSettings.timeCfg = new timeCfg();
            appSettings.videoCh011 = new videoCh011();
            appSettings.videoCh012 = new videoCh012();
            appSettings.videoCh013 = new videoCh013();
            appSettings.uuid = deviceID;
            appSettings.grUuid = email;

            appSettings.streamCfg.enableAudio = 1;
            appSettings.streamCfg.enabled = 1;
            appSettings.streamCfg.enableTelnet = 0;
            appSettings.streamCfg.isHd = 0;
            if (mqttUrl != "")
                appSettings.streamCfg.mqttUrl = mqttUrl;//ConfigurationManager.AppSettings["mqttserver"].ToString();//"tcp://pro.ambicam.com:1883";
            if (cameraurl != "")
                appSettings.streamCfg.publishUrl = cameraurl;
            appSettings.streamCfg.telnetUrl = "telnet.ambicam.com:8888";

            appSettings.timeCfg.time = "00000000";
            appSettings.timeCfg.tz = "STD:+5:30";
            appSettings.timeCfg.timeZone = "Asia/Calcutta";
            appSettings.timeCfg.dstmode = 0;
            appSettings.timeCfg.autoupdate = 1;
            appSettings.timeCfg.autoupdatetzonvif = 0;
            appSettings.timeCfg.ntpserver = "time.ambicam.com";
            appSettings.timeCfg.ntpinterval = 1;
            appSettings.timeCfg.ntpenable = 1;

            appSettings.videoCh011.bps = 150;
            appSettings.videoCh011.fps = 10;
            appSettings.videoCh011.gop = 40;
            appSettings.videoCh011.brmode = 2;
            appSettings.videoCh011.piclevel = 1;
            appSettings.videoCh011.fixqplevel = 1;
            appSettings.videoCh011.width = 1280;
            appSettings.videoCh011.height = 720;
            appSettings.videoCh011.bmainstream = 1;
            appSettings.videoCh011.bfield = 0;

            appSettings.videoCh012.bps = 256;
            appSettings.videoCh012.fps = 15;
            appSettings.videoCh012.gop = 60;
            appSettings.videoCh012.brmode = 2;
            appSettings.videoCh012.piclevel = 1;
            appSettings.videoCh012.fixqplevel = 1;
            appSettings.videoCh012.width = 640;
            appSettings.videoCh012.height = 360;
            appSettings.videoCh012.bmainstream = 1;
            appSettings.videoCh012.bfield = 0;

            appSettings.videoCh013.bps = 64;
            appSettings.videoCh013.fps = 5;
            appSettings.videoCh013.gop = 15;
            appSettings.videoCh013.brmode = 2;
            appSettings.videoCh013.piclevel = 4;
            appSettings.videoCh013.fixqplevel = 4;
            appSettings.videoCh013.width = 320;
            appSettings.videoCh013.height = 180;
            appSettings.videoCh013.bmainstream = 1;
            appSettings.videoCh013.bfield = 0;

            appSettings.emailCfg.emailserver = "smtp.gmail.com";
            appSettings.emailCfg.emailport = 465;
            appSettings.emailCfg.ssl = 1;
            appSettings.emailCfg.logintype = 1;
            appSettings.emailCfg.emailusername = "alerts@ambicam.com";
            appSettings.emailCfg.emailpassword = "v|c{azo777";
            appSettings.emailCfg.from = "alerts@ambicam.com";
            appSettings.emailCfg.to = email;
            appSettings.emailCfg.subject = "Ambicam Motion Alert";
            appSettings.emailCfg.text = "This is an ambicam email alert";
            appSettings.emailCfg.attatchment = "";


            appSettings.displayCfg.hue = 50;
            appSettings.displayCfg.brightness = 50;
            appSettings.displayCfg.saturation = 50;
            appSettings.displayCfg.contrast = 50;
            appSettings.displayCfg.ircutmode = 1;

            appSettings.osdCfg.cont_0 = "YYYY-MM-DD hh:mm:ss";
            appSettings.osdCfg.cont_1 = cameraname;
            appSettings.osdCfg.fontsize = 1;
            appSettings.osdCfg.h_0 = 32;
            appSettings.osdCfg.h_1 = 32;
            appSettings.osdCfg.rgncnt = 2;
            appSettings.osdCfg.show_0 = 1;
            appSettings.osdCfg.show_1 = 1;
            appSettings.osdCfg.w_0 = 304;
            appSettings.osdCfg.w_1 = 112;
            appSettings.osdCfg.x_0 = 928;
            appSettings.osdCfg.x_1 = 64;
            appSettings.osdCfg.y_0 = 32;
            appSettings.osdCfg.y_1 = 32;


            appSettings.imageCfg.devno = 0;
            appSettings.imageCfg.chn = 0;
            appSettings.imageCfg.flip = 0;
            appSettings.imageCfg.mirror = 0;
            appSettings.imageCfg.wdr = 1;

            appSettings.recordCh011.startTimerRec = 0;
            appSettings.recordCh011.startManualRec = 1;
            appSettings.recordCh011.singlefiletime = 300;
            appSettings.recordCh011.enable = 0;
            appSettings.recordCh011.filepath = "/bin/vs/sd/rec";

            appSettings.recordCh012.startTimerRec = 0;
            appSettings.recordCh012.startManualRec = 0;
            appSettings.recordCh012.singlefiletime = 60;
            appSettings.recordCh012.enable = 0;
            appSettings.recordCh012.filepath = "/bin/vs/sd/rec";

            appSettings.recordSch.workday = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.weekend = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.sun = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.mon = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.tue = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.wed = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.thu = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.fri = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.recordSch.sat = new List<int>(new int[] { 0, -1, 0, -1, 0, -1 });
            appSettings.rectime = 30;
            appSettings.recordSch.etm = 2;

            appSettings.mdCfg.md_email_switch = 0;
            appSettings.mdCfg.md_snap_switch = 1;
            appSettings.mdCfg.md_emailsnap_switch = 0;
            appSettings.mdCfg.md_ftpsnap_switch = 1;
            appSettings.mdCfg.md_record_switch = 0;
            appSettings.mdCfg.md_ftprec_switch = 0;
            appSettings.mdCfg.md_ioalmdo_switch = 0;
            appSettings.mdCfg.etm = 2;
            appSettings.mdCfg.workday = 0;
            appSettings.mdCfg.weekend = 0;
            appSettings.mdCfg.md_interval = 30;
            appSettings.mdCfg.MdbEnable = 0;
            appSettings.mdCfg.MdSensitiValue = 1;
            appSettings.mdCfg.MDThresholdValue = 10;
            appSettings.mdCfg.MdInterval = 30;
            appSettings.mdCfg.MdRegion = new List<int>(new int[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 0, 0 });
            appSettings.mdCfg.md_alarm = 0;
            appSettings.mdCfg.defend_alarm = 0;
            appSettings.mdCfg.tc_alarm = 0;

            appSettings.devInfo.hwVer = 1;
            appSettings.devInfo.swVer = 1;
            appSettings.devInfo.provisioningVer = 1;
            appSettings.devInfo.publisherVer = 1;
            appSettings.devInfo.serialNo = "VVVIPC1504173580HSDS-j0TsuixTi4l";

            appSettings.nwInfo.networktype = "NONE";
            appSettings.nwInfo.macaddress = "00:00:00:00:00:00";
            appSettings.nwInfo.ip = "0.0.0.0";
            appSettings.nwInfo.netmask = "0.0.0.0";
            appSettings.nwInfo.gateway = "0.0.0.0";
            appSettings.nwInfo.sdnsip = "0.0.0.0";
            appSettings.nwInfo.fdnsip = "0.0.0.0";
            return appSettings;
        }

    }
}