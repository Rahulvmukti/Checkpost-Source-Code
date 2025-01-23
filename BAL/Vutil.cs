using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;

namespace exam.BAL
{
    public class Vutil
    {
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

        public int VUtil_encodeMsgHeader(ref int[] buff, MQTTMessage msgHdr)
        {
            int offset;
            offset = 0;
            offset += VUtil_packInt(ref buff, offset, msgHdr.srcId);
            offset += VUtil_packInt(ref buff, offset, msgHdr.dstId);
            offset += VUtil_packInt(ref buff, offset, msgHdr.msgType);
            offset += VUtil_packInt(ref buff, offset, msgHdr.msgLen);

            return offset;
        }
        public int VUtil_packInt(ref int[] array, int offset, int val)
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
        public List<clientStats> VUtil_decodeStatusMsg(char[] buff, int offset, int len)
        {

            List<clientStats> clientStats = new List<clientStats>();


            string msg = "";
            int i = 0;

            msg = VUtil_getStringFromBuff(buff, offset, len);
            string[] clients = msg.Split('\n');
            for (i = 0; i < clients.Length; i++)
            {
                clientStats.Add(VUtil_parseStatLine(clients[i]));
            }
            return clientStats;
        }
        public clientStats VUtil_parseStatLine(string statStr)
        {
            try
            {
                //var clientStat = {name:"", isUp:0};
                clientStats cs = new clientStats();
                int start;
                int end;

                string name;
                string isUp;

                start = statStr.IndexOf("name=");
                start += 5;
                end = statStr.IndexOf(" value=");
                name = statStr.Substring(start, statStr.Length - end + 6);


                start = statStr.IndexOf("value=");
                start += 6;
                end = start + 1;
                isUp = statStr.Substring(start, 1);

                cs.name = name;
                cs.isUp = int.Parse(isUp);

                return cs;
            }
            catch (Exception ex)
            {
                return new clientStats();
            }
        }

        public string VUtil_getStringFromBuff(char[] buff, int offset, int len)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(buff, offset, len);

            //string str = "";

            //for (int i = offset; i < (offset + len); i++)
            //{
            //    str += Convert.ToString(buff[i]);// String.fromCharCode(buff[i]);
            //}

            return sb.ToString();
        }

        public string processStatusMsg(char[] payload, int offset, int len)
        {
            List<clientStats> clientStats = VUtil_decodeStatusMsg(payload, offset, len);
            string str = "";

            for (int i = 0; i < (clientStats.Count - 1); i++)
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
        public int VUtil_getStreamId(string stream)
        {
            int streamId = -2;

            streamId = 0;
            for (int i = 0; i < stream.Length; i++)
            {
                streamId += (int)stream[i];//CharUnicodeInfo.GetDigitValue(stream, i);// stream.charCodeAt(i);
            }

            return streamId;
        }
        public appSettings getDefaultCfg(appSettings appSettings)
        {

            appSettings.uuid = "fooBar";
            appSettings.grUuid = "";

            appSettings.streamCfg.enabled = 0;
            appSettings.streamCfg.enableAudio = 0;
            appSettings.streamCfg.publishUrl = "";
            appSettings.streamCfg.mqttUrl = "";
            appSettings.streamCfg.enableTelnet = 0;
            appSettings.streamCfg.telnetUrl = "";
            appSettings.streamCfg.isHd = 0;

            appSettings.timeCfg.time = "1234567890";
            appSettings.timeCfg.tz = "STD:+5:30";
            appSettings.timeCfg.timeZone = "Asia/Kolkata";
            appSettings.timeCfg.dstmode = 1;
            appSettings.timeCfg.autoupdate = 2;
            appSettings.timeCfg.autoupdatetzonvif = 3;
            appSettings.timeCfg.ntpserver = "ntp.server.com";
            appSettings.timeCfg.ntpinterval = 4;
            appSettings.timeCfg.ntpenable = 5;

            appSettings.videoCh011.bps = 1;
            appSettings.videoCh011.fps = 2;
            appSettings.videoCh011.gop = 3;
            appSettings.videoCh011.brmode = 4;
            appSettings.videoCh011.piclevel = 5;
            appSettings.videoCh011.fixqplevel = 6;
            appSettings.videoCh011.width = 7;
            appSettings.videoCh011.height = 8;
            appSettings.videoCh011.bmainstream = 9;
            appSettings.videoCh011.bfield = 10;

            appSettings.videoCh012.bps = 11;
            appSettings.videoCh012.fps = 12;
            appSettings.videoCh012.gop = 13;
            appSettings.videoCh012.brmode = 14;
            appSettings.videoCh012.piclevel = 15;
            appSettings.videoCh012.fixqplevel = 16;
            appSettings.videoCh012.width = 17;
            appSettings.videoCh012.height = 18;
            appSettings.videoCh012.bmainstream = 19;
            appSettings.videoCh012.bfield = 20;

            appSettings.videoCh013.bps = 21;
            appSettings.videoCh013.fps = 22;
            appSettings.videoCh013.gop = 23;
            appSettings.videoCh013.brmode = 24;
            appSettings.videoCh013.piclevel = 25;
            appSettings.videoCh013.fixqplevel = 26;
            appSettings.videoCh013.width = 27;
            appSettings.videoCh013.height = 28;
            appSettings.videoCh013.bmainstream = 29;
            appSettings.videoCh013.bfield = 30;

            appSettings.emailCfg.emailserver = "smtp.mail.com";
            appSettings.emailCfg.emailport = 1;
            appSettings.emailCfg.ssl = 2;
            appSettings.emailCfg.logintype = 3;

            appSettings.emailCfg.emailusername = "alerts@ambicam.com";
            appSettings.emailCfg.emailpassword = "";
            appSettings.emailCfg.from = "alerts@ambicam.com";

            appSettings.emailCfg.to = "info@vmukti.com";
            appSettings.emailCfg.subject = "mysubject";
            appSettings.emailCfg.text = "This is an ambicam email alert";
            appSettings.emailCfg.attatchment = "This is attachement";

            appSettings.displayCfg.hue = 50;
            appSettings.displayCfg.brightness = 50;
            appSettings.displayCfg.saturation = 50;
            appSettings.displayCfg.contrast = 50;
            appSettings.displayCfg.ircutmode = 1;

            appSettings.osdCfg.rgncnt = 1;
            appSettings.osdCfg.fontsize = 2;
            appSettings.osdCfg.x_0 = 3;
            appSettings.osdCfg.y_0 = 4;
            appSettings.osdCfg.w_0 = 5;
            appSettings.osdCfg.h_0 = 6;
            appSettings.osdCfg.cont_0 = "This is time";
            appSettings.osdCfg.show_0 = 7;
            appSettings.osdCfg.x_1 = 8;
            appSettings.osdCfg.y_1 = 9;
            appSettings.osdCfg.w_1 = 10;
            appSettings.osdCfg.h_1 = 11;
            appSettings.osdCfg.cont_1 = "VMukti VCAM";
            appSettings.osdCfg.show_1 = 12;

            appSettings.imageCfg.devno = 22;
            appSettings.imageCfg.chn = 23;
            appSettings.imageCfg.flip = 24;
            appSettings.imageCfg.mirror = 26;
            appSettings.imageCfg.wdr = 27;

            appSettings.recordCh011.startTimerRec = 88;
            appSettings.recordCh011.startManualRec = 89;
            appSettings.recordCh011.singlefiletime = 90;
            appSettings.recordCh011.enable = 91;
            appSettings.recordCh011.filepath = "/home/pravin/vmukti/rec1";
            appSettings.recordCh012.startTimerRec = 92;
            appSettings.recordCh012.startManualRec = 93;
            appSettings.recordCh012.singlefiletime = 94;
            appSettings.recordCh012.enable = 95;
            appSettings.recordCh012.filepath = "/home/pravin/vmukti/rec2";
            appSettings.recordSch.workday = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.weekend = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.sun = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.mon = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.tue = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.wed = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.thu = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.fri = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.sat = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.rectime = 96;

            appSettings.mdCfg.md_email_switch = 0;
            appSettings.mdCfg.md_snap_switch = 0;
            appSettings.mdCfg.md_emailsnap_switch = 0;
            appSettings.mdCfg.md_ftpsnap_switch = 0;
            appSettings.mdCfg.md_record_switch = 0;
            appSettings.mdCfg.md_ftprec_switch = 0;
            appSettings.mdCfg.md_ioalmdo_switch = 0;
            appSettings.mdCfg.etm = 0;
            appSettings.mdCfg.workday = 0;
            appSettings.mdCfg.weekend = 0;
            appSettings.mdCfg.md_interval = 5;
            appSettings.mdCfg.MdbEnable = 0;
            appSettings.mdCfg.MdSensitiValue = 10;
            appSettings.mdCfg.MDThresholdValue = 10;
            appSettings.mdCfg.MdInterval = 5;
            appSettings.mdCfg.MdRegion = new List<int>(new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
            appSettings.mdCfg.md_alarm = 0;
            appSettings.mdCfg.defend_alarm = 0;
            appSettings.mdCfg.tc_alarm = 0;

            appSettings.devInfo.hwVer = 0;
            appSettings.devInfo.swVer = 0;
            appSettings.devInfo.provisioningVer = 0;
            appSettings.devInfo.publisherVer = 0;
            appSettings.devInfo.serialNo = "";

            appSettings.nwInfo.networktype = "";
            appSettings.nwInfo.macaddress = "";
            appSettings.nwInfo.ip = "";
            appSettings.nwInfo.netmask = "";
            appSettings.nwInfo.gateway = "";
            appSettings.nwInfo.sdnsip = "";
            appSettings.nwInfo.fdnsip = "";
            return appSettings;
        }

        public appSettings getDefaultSettings()
        {
            appSettings appSettings = new appSettings();
            appSettings.streamCfg = new streamCfg();
            appSettings.devInfo = new devInfo();
            appSettings.displayCfg = new displayCfg();
            appSettings.emailCfg = new emailCfg();
            appSettings.imageCfg = new imageCfg();
            appSettings.mdCfg = new mdCfg();
            appSettings.nwInfo = new nwInfo();
            appSettings.ptzinfo = new ptzInfo();
            appSettings.osdCfg = new osdCfg();
            appSettings.recordCh011 = new recordCh011();
            appSettings.recordCh012 = new recordCh012();
            appSettings.recordSch = new recordSch();
            appSettings.timeCfg = new timeCfg();
            appSettings.videoCh011 = new videoCh011();
            appSettings.videoCh012 = new videoCh012();
            appSettings.videoCh013 = new videoCh013();
            return getDefaultCfg(appSettings);
        }

        public string VUtil_encodeMsg(appSettings appSettings)
        {
            string strMsg = "";
            strMsg += encodeUuid(appSettings.uuid);
            strMsg += encodeGrUuid(appSettings.grUuid);
            strMsg += encodeStreamCfg(appSettings.streamCfg);
            strMsg += encodeTimeCfg(appSettings.timeCfg);
            strMsg += encodeEmailCfg(appSettings.emailCfg);
            strMsg += encodePTZCfg(appSettings.ptzinfo);
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

        public string encodeEmailCfg(emailCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_EMAIL_CFG);
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
            msg += addEndTag(CommonConfig.TAG_EMAIL_CFG);

            return msg;
        }

        public string encodeVideoCfg(videoCh011 ch011, videoCh012 ch012, videoCh013 ch013)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_VIDEO_CFG);
            msg += addStartTag(CommonConfig.TAG_VIDEO_CH011);
            //msg += addTag("bps", "150");
            //msg += addTag("fps", "10");
            //msg += addTag("gop", "40");
            msg += addTag("bps",ch011.bps.ToString());
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

        public string encodeOsdCfg(osdCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_OSD_CFG);
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
            msg += addEndTag(CommonConfig.TAG_OSD_CFG);

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

        public string encodeUuid(string uuid)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_UUID);
            msg += addTag("uuid", uuid);
            msg += addEndTag(CommonConfig.TAG_UUID);

            return msg;
        }
        public string encodeGrUuid(string grUuid)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_GR_UUID);
            msg += addTag("grUuid", grUuid);
            msg += addEndTag(CommonConfig.TAG_GR_UUID);

            return msg;
        }

        public string encodeStreamCfg(streamCfg cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_STREAM_CFG);
            msg += addTag("enabled", cfg.enabled.ToString());
            msg += addTag("enableAudio", cfg.enableAudio.ToString());
            msg += addTag("publishUrl", cfg.publishUrl);
            msg += addTag("mqttUrl", cfg.mqttUrl);
            msg += addTag("enableTelnet", cfg.enableTelnet.ToString());
            msg += addTag("telnetUrl", cfg.telnetUrl.ToString());
            msg += addTag("isHd", cfg.isHd.ToString());
            msg += addEndTag(CommonConfig.TAG_STREAM_CFG);

            return msg;
        }

        public string addStartTag(string tag)
        {
            string msg;
            msg = "<" + tag + ">\n";
            return msg;
        }
        public string addTag(string tag, string value)
        {
            string str;
            str = "<var " + tag + "=\"" + value + "\"/>\n";
            return str;
        }
        public string addEndTag(string tag)
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

        public appSettings VUtil_decodeGetConfigMsg(char[] buff, int offset, int len)
        {
            string msg = "";
            int lineCnt;

            appSettings appSettings = getDefaultSettings();

            appSettings.recordSch.workday = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.weekend = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });

            appSettings.recordSch.sun = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.mon = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.tue = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.wed = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.thu = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.fri = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });
            appSettings.recordSch.sat = new List<int>(new int[] { -1, -1, -1, -1, -1, -1 });

            appSettings.mdCfg.MdRegion = new List<int>(new int[] { 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255, 255 });

            msg = VUtil_getStringFromBuff(buff, offset, len);

            string[] lines = msg.Split('\n');
            string line = "";
            offset = 0;
            lineCnt = lines.Length;
            while (offset < lineCnt)
            {
                line = lines[offset];

                //console.log(">>>>>>>>>>>>> Line : " + line);

                if (line.IndexOf(CommonConfig.TAG_GR_UUID) > 0)
                {
                    offset++;
                    offset = decodeGrUuid(lines.ToList(), offset, appSettings);
                }
                else if (line.IndexOf(CommonConfig.TAG_UUID) > 0)
                {
                    offset++;
                    offset = decodeUuid(lines, offset, appSettings);
                }
                else if (line.IndexOf(CommonConfig.TAG_STREAM_CFG) > 0)
                {
                    offset++;
                    offset = decodeStreamCfg(lines, offset, appSettings.streamCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_TIME_CFG) > 0)
                {
                    offset++;
                    offset = decodeTimeCfg(lines, offset, appSettings.timeCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_EMAIL_CFG) > 0)
                {
                    offset++;
                    offset = decodeEmailCfg(lines, offset, appSettings.emailCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_VIDEO_CFG) > 0)
                {
                    offset++;
                    offset = decodeVideoCfg(lines, offset, appSettings);
                }
                else if (line.IndexOf(CommonConfig.TAG_DISPLAY_CFG) > 0)
                {
                    offset++;
                    offset = decodeDisplayCfg(lines, offset, appSettings.displayCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_OSD_CFG) > 0)
                {
                    offset++;
                    offset = decodeOsdCfg(lines, offset, appSettings.osdCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_RECORD_CFG) > 0)
                {
                    offset++;
                    offset = decodeRecordCfg(lines, offset, appSettings);
                }
                else if (line.IndexOf(CommonConfig.TAG_IMAGE_CFG) > 0)
                {
                    offset++;
                    offset = decodeImageCfg(lines, offset, appSettings.imageCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_MD_CFG) > 0)
                {
                    offset++;
                    offset = decodeMdCfg(lines, offset, appSettings.mdCfg);
                }
                else if (line.IndexOf(CommonConfig.TAG_DEV_INFO) > 0)
                {
                    offset++;
                    offset = decodeDevInfo(lines, offset, appSettings.devInfo);
                }
                else if (line.IndexOf(CommonConfig.TAG_NET_INFO) > 0)
                {
                    offset++;
                    offset = decodeNetInfo(lines, offset, appSettings.nwInfo);
                }
                else if (line.IndexOf(CommonConfig.TAG_PTZ_INFO) > 0)
                {
                    offset++;
                    offset = decodePtzCfg(lines, offset, appSettings.ptzinfo);
                }
                else
                {
                    offset++;
                }
            }
            return appSettings;
        }
        private int decodePtzCfg(string[] lines, int offset, ptzInfo cfg)
        {
            int exitFlag;
            string line;
            string endTag;


            endTag = "</" + CommonConfig.TAG_PTZ_INFO + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("leftPos=") > 0)
                {
                    cfg.leftPos = parseIntTag(line, "<var leftPos=\"");
                }
                else if (line.IndexOf("rightPos=") > 0)
                {
                    cfg.rightPos = parseIntTag(line, "<var rightPos=\"");
                }
                else if (line.IndexOf("upPos=") > 0)
                {
                    cfg.upPos = parseIntTag(line, "<var upPos=\"");
                }
                else if (line.IndexOf("downPos=") > 0)
                {
                    cfg.downPos = parseIntTag(line, "<var downPos=\"");
                }
                else if (line.IndexOf("farPos=") > 0)
                {
                    cfg.farPos = parseIntTag(line, "<var farPos=\"");
                }
                else if (line.IndexOf("nearPos=") > 0)
                {
                    cfg.nearPos = parseIntTag(line, "<var nearPos=\"");
                }
                else if (line.IndexOf("currPanPos=") > 0)
                {
                    cfg.currPanPos = parseIntTag(line, "<var currPanPos=\"");
                }
                else if (line.IndexOf("currTiltPos=") > 0)
                {
                    cfg.currTiltPos = parseIntTag(line, "<var currTiltPos=\"");
                }
                else if (line.IndexOf("currZoomPos=") > 0)
                {
                    cfg.currZoomPos = parseIntTag(line, "<var currZoomPos=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodePtzCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        private int decodeNetInfo(string[] lines, int offset, nwInfo cfg)
        {
            int exitFlag;
            string line;
            string endTag;


            endTag = "</" + CommonConfig.TAG_NET_INFO + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("networktype=") > 0)
                {
                    cfg.networktype = parseStringTag(line, "<var networktype=\"");
                }
                else if (line.IndexOf("macaddress=") > 0)
                {
                    cfg.macaddress = parseStringTag(line, "<var macaddress=\"");
                }
                else if (line.IndexOf("netmask=") > 0)
                {
                    cfg.netmask = parseStringTag(line, "<var netmask=\"");
                }
                else if (line.IndexOf("gateway=") > 0)
                {
                    cfg.gateway = parseStringTag(line, "<var gateway=\"");
                }
                else if (line.IndexOf("sdnsip=") > 0)
                {
                    cfg.sdnsip = parseStringTag(line, "<var sdnsip=\"");
                }
                else if (line.IndexOf("fdnsip=") > 0)
                {
                    cfg.fdnsip = parseStringTag(line, "<var fdnsip=\"");
                }
                else if (line.IndexOf("ip=") > 0)
                {
                    cfg.ip = parseStringTag(line, "<var ip=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeNetInfo Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeDevInfo(string[] lines, int offset, devInfo cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_DEV_INFO + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("hwVer=") > 0)
                {
                    cfg.hwVer = parseIntTag(line, "<var hwVer=\"");
                }
                else if (line.IndexOf("swVer=") > 0)
                {
                    cfg.swVer = parseIntTag(line, "<var swVer=\"");
                }
                else if (line.IndexOf("provisioningVer=") > 0)
                {
                    cfg.provisioningVer = parseIntTag(line, "<var provisioningVer=\"");
                }
                else if (line.IndexOf("publisherVer=") > 0)
                {
                    cfg.publisherVer = parseIntTag(line, "<var publisherVer=\"");
                }
                else if (line.IndexOf("serialNo=") > 0)
                {
                    cfg.serialNo = parseStringTag(line, "<var serialNo=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeDevInfo Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeMdCfg(string[] lines, int offset, mdCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_MD_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("md_email_switch=") > 0)
                {
                    cfg.md_email_switch = parseIntTag(line, "<var md_email_switch=\"");
                }
                else if (line.IndexOf("md_snap_switch=") > 0)
                {
                    cfg.md_snap_switch = parseIntTag(line, "<var md_snap_switch=\"");
                }
                else if (line.IndexOf("md_emailsnap_switch=") > 0)
                {
                    cfg.md_emailsnap_switch = parseIntTag(line, "<var md_emailsnap_switch=\"");
                }
                else if (line.IndexOf("md_ftpsnap_switch=") > 0)
                {
                    cfg.md_ftpsnap_switch = parseIntTag(line, "<var md_ftpsnap_switch=\"");
                }
                else if (line.IndexOf("md_record_switch=") > 0)
                {
                    cfg.md_record_switch = parseIntTag(line, "<var md_record_switch=\"");
                }
                else if (line.IndexOf("md_ftprec_switch=") > 0)
                {
                    cfg.md_ftprec_switch = parseIntTag(line, "<var md_ftprec_switch=\"");
                }
                else if (line.IndexOf("md_ioalmdo_switch=") > 0)
                {
                    cfg.md_ioalmdo_switch = parseIntTag(line, "<var md_ioalmdo_switch=\"");
                }
                else if (line.IndexOf("etm=") > 0)
                {
                    cfg.etm = parseIntTag(line, "<var etm=\"");
                }
                else if (line.IndexOf("workday=") > 0)
                {
                    cfg.workday = parseIntTag(line, "<var workday=\"");
                }
                else if (line.IndexOf("weekend=") > 0)
                {
                    cfg.weekend = parseIntTag(line, "<var weekend=\"");
                }
                else if (line.IndexOf("md_interval=") > 0)
                {
                    cfg.md_interval = parseIntTag(line, "<var md_interval=\"");
                }
                else if (line.IndexOf("MdbEnable=") > 0)
                {
                    cfg.MdbEnable = parseIntTag(line, "<var MdbEnable=\"");
                }
                else if (line.IndexOf("MdSensitiValue=") > 0)
                {
                    cfg.MdSensitiValue = parseIntTag(line, "<var MdSensitiValue=\"");
                }
                else if (line.IndexOf("MDThresholdValue=") > 0)
                {
                    cfg.MDThresholdValue = parseIntTag(line, "<var MDThresholdValue=\"");
                }
                else if (line.IndexOf("MdInterval=") > 0)
                {
                    cfg.MdInterval = parseIntTag(line, "<var MdInterval=\"");
                }
                else if (line.IndexOf("MdRegion[") > 0)
                {
                    mqttmessage_mdCfg mdCfg = VUtil_parseMdLine(line);
                    cfg.MdRegion[mdCfg.index] = mdCfg.value;
                }
                else if (line.IndexOf("md_alarm=") > 0)
                {
                    cfg.md_alarm = parseIntTag(line, "<var md_alarm=\"");
                }
                else if (line.IndexOf("defend_alarm=") > 0)
                {
                    cfg.defend_alarm = parseIntTag(line, "<var defend_alarm=\"");
                }
                else if (line.IndexOf("tc_alarm=") > 0)
                {
                    cfg.tc_alarm = parseIntTag(line, "<var tc_alarm=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //  console.log("decodeMdCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeImageCfg(string[] lines, int offset, imageCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;


            endTag = "</" + CommonConfig.TAG_IMAGE_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("devno=") > 0)
                {
                    cfg.devno = parseIntTag(line, "<var devno=\"");
                }
                else if (line.IndexOf("chn=") > 0)
                {
                    cfg.chn = parseIntTag(line, "<var chn=\"");
                }
                else if (line.IndexOf("flip=") > 0)
                {
                    cfg.flip = parseIntTag(line, "<var flip=\"");
                }
                else if (line.IndexOf("mirror=") > 0)
                {
                    cfg.mirror = parseIntTag(line, "<var mirror=\"");
                }
                else if (line.IndexOf("wdr=") > 0)
                {
                    cfg.wdr = parseIntTag(line, "<var wdr=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    // console.log("decodeImageCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeRecordCfg(string[] lines, int offset, appSettings cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_RECORD_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf(CommonConfig.TAG_RECORD_CH011) > 0)
                {
                    offset++;
                    offset = decodeRecordChCfg(lines, offset, cfg.recordCh011, 11);
                }
                else if (line.IndexOf(CommonConfig.TAG_RECORD_CH012) > 0)
                {
                    offset++;
                    offset = decodeRecordChCfg(lines, offset, cfg.recordCh012, 12);
                }
                else if (line.IndexOf(CommonConfig.TAG_RECORD_SCH) > 0)
                {
                    offset++;
                    offset = decodeRecordSchCfg(lines, offset, cfg.recordSch);
                }
                else if (line.IndexOf("rectime=") > 0)
                {
                    cfg.rectime = parseIntTag(line, "<var rectime=\"");
                    offset++;
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                    offset++;
                }
                else
                {
                    //console.log("decodeRecordCfg Unknown Line : " + line);
                    offset++;
                }
            }

            return offset;
        }

        private int decodeRecordChCfg(string[] lines, int offset, recordCh011 cfg, int ch)
        {

            int exitFlag;
            string line;
            string endTag;
            if (11 == ch)
            {
                endTag = "</" + CommonConfig.TAG_RECORD_CH011 + ">";
            }
            else if (12 == ch)
            {
                endTag = "</" + CommonConfig.TAG_RECORD_CH012 + ">";
            }
            else
            {
                //console.log("Failed to decode Record Ch Cfg : " + ch);
                return 0;
            }


            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("startTimerRec=") > 0)
                {
                    cfg.startTimerRec = parseIntTag(line, "<var startTimerRec=\"");
                }
                else if (line.IndexOf("startManualRec=") > 0)
                {
                    cfg.startManualRec = parseIntTag(line, "<var startManualRec=\"");
                }
                else if (line.IndexOf("singlefiletime=") > 0)
                {
                    cfg.singlefiletime = parseIntTag(line, "<var singlefiletime=\"");
                }
                else if (line.IndexOf("enable=") > 0)
                {
                    cfg.enable = parseIntTag(line, "<var enable=\"");
                }
                else if (line.IndexOf("filepath=") > 0)
                {
                    cfg.filepath = parseStringTag(line, "filepath=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    // console.log("decodeRecordChCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        private int decodeRecordChCfg(string[] lines, int offset, recordCh012 cfg, int ch)
        {
            int exitFlag;
            string line;
            string endTag;
            if (11 == ch)
            {
                endTag = "</" + CommonConfig.TAG_RECORD_CH011 + ">";
            }
            else if (12 == ch)
            {
                endTag = "</" + CommonConfig.TAG_RECORD_CH012 + ">";
            }
            else
            {
                //console.log("Failed to decode Record Ch Cfg : " + ch);
                return 0;
            }


            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("startTimerRec=") > 0)
                {
                    cfg.startTimerRec = parseIntTag(line, "<var startTimerRec=\"");
                }
                else if (line.IndexOf("startManualRec=") > 0)
                {
                    cfg.startManualRec = parseIntTag(line, "<var startManualRec=\"");
                }
                else if (line.IndexOf("singlefiletime=") > 0)
                {
                    cfg.singlefiletime = parseIntTag(line, "<var singlefiletime=\"");
                }
                else if (line.IndexOf("enable=") > 0)
                {
                    cfg.enable = parseIntTag(line, "<var enable=\"");
                }
                else if (line.IndexOf("filepath=") > 0)
                {
                    cfg.filepath = parseStringTag(line, "filepath=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    // console.log("decodeRecordChCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeRecordSchCfg(string[] lines, int offset, recordSch cfg)
        {

            int exitFlag;
            string line;
            string endTag;
            string week0 = "";
            string week1 = "";
            string week2 = "";
            string week3 = "";
            string week4 = "";
            string week5 = "";
            string week6 = "";

            string workday = "";
            string weekend = "";


            endTag = "</" + CommonConfig.TAG_RECORD_SCH + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("etm=") > 0)
                {
                    cfg.etm = parseIntTag(line, "<var etm=\"");
                }
                else if (line.IndexOf("workday=") > 0)
                {
                    workday = parseStringTag(line, "<var workday=\"");
                }
                else if (line.IndexOf("weekend=") > 0)
                {
                    weekend = parseStringTag(line, "<var weekend=\"");
                }
                else if (line.IndexOf("sun=") > 0)
                {
                    week0 = parseStringTag(line, "<var sun=\"");
                }
                else if (line.IndexOf("mon=") > 0)
                {
                    week1 = parseStringTag(line, "<var mon=\"");
                }
                else if (line.IndexOf("tue=") > 0)
                {
                    week2 = parseStringTag(line, "<var tue=\"");
                }
                else if (line.IndexOf("wed=") > 0)
                {
                    week3 = parseStringTag(line, "<var wed=\"");
                }
                else if (line.IndexOf("thu=") > 0)
                {
                    week4 = parseStringTag(line, "<var thu=\"");
                }
                else if (line.IndexOf("fri=") > 0)
                {
                    week5 = parseStringTag(line, "<var fri=\"");
                }
                else if (line.IndexOf("sat=") > 0)
                {
                    week6 = parseStringTag(line, "<var sat=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    // console.log("decodeRecordSchCfg Unknown Line : " + line);
                }

                offset++;
            }
            int[] cfg_workday = cfg.workday.ToArray();
            int[] cfg_weekend = cfg.weekend.ToArray();
            int[] cfg_sun = cfg.sun.ToArray();
            int[] cfg_mon = cfg.mon.ToArray();
            int[] cfg_tue = cfg.tue.ToArray();
            int[] cfg_wed = cfg.wed.ToArray();
            int[] cfg_thu = cfg.thu.ToArray();
            int[] cfg_fri = cfg.fri.ToArray();
            int[] cfg_sat = cfg.sat.ToArray();

            cfg.enWorkday = parseWeekStr(workday, ref cfg_workday);
            cfg.enWeekend = parseWeekStr(weekend, ref cfg_weekend);
            cfg.enSun = parseWeekStr(week0, ref cfg_sun);
            cfg.enMon = parseWeekStr(week1, ref cfg_mon);
            cfg.enTue = parseWeekStr(week2, ref cfg_tue);
            cfg.enWed = parseWeekStr(week3, ref cfg_wed);
            cfg.enThu = parseWeekStr(week4, ref cfg_thu);
            cfg.enFri = parseWeekStr(week5, ref cfg_fri);
            cfg.enSat = parseWeekStr(week6, ref cfg_sat);
            cfg.workday = cfg_workday.ToList();
            cfg.weekend = cfg_weekend.ToList();
            cfg.sun = cfg_sun.ToList();
            cfg.mon = cfg_mon.ToList();
            cfg.tue = cfg_tue.ToList();
            cfg.wed = cfg_wed.ToList();
            cfg.thu = cfg_thu.ToList();
            cfg.fri = cfg_fri.ToList();
            cfg.sat = cfg_sat.ToList();
            return offset;
        }




        private int decodeOsdCfg(string[] lines, int offset, osdCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_OSD_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("rgncnt=") > 0)
                {
                    cfg.rgncnt = parseIntTag(line, "<var rgncnt=\"");
                }
                else if (line.IndexOf("fontsize=") > 0)
                {
                    cfg.fontsize = parseIntTag(line, "<var fontsize=\"");
                }
                else if (line.IndexOf("show_0=") > 0)
                {
                    cfg.show_0 = parseIntTag(line, "<var show_0=\"");
                }
                else if (line.IndexOf("x_0=") > 0)
                {
                    cfg.x_0 = parseIntTag(line, "<var x_0=\"");
                }
                else if (line.IndexOf("y_0=") > 0)
                {
                    cfg.y_0 = parseIntTag(line, "<var y_0=\"");
                }
                else if (line.IndexOf("w_0=") > 0)
                {
                    cfg.w_0 = parseIntTag(line, "<var w_0=\"");
                }
                else if (line.IndexOf("h_0=") > 0)
                {
                    cfg.h_0 = parseIntTag(line, "<var h_0=\"");
                }
                else if (line.IndexOf("cont_0=") > 0)
                {
                    cfg.cont_0 = parseStringTag(line, "cont_0=\"");
                }
                else if (line.IndexOf("show_1=") > 0)
                {
                    cfg.show_1 = parseIntTag(line, "<var show_1=\"");
                }
                else if (line.IndexOf("x_1=") > 0)
                {
                    cfg.x_1 = parseIntTag(line, "<var x_1=\"");
                }
                else if (line.IndexOf("y_1=") > 0)
                {
                    cfg.y_1 = parseIntTag(line, "<var y_1=\"");
                }
                else if (line.IndexOf("w_1=") > 0)
                {
                    cfg.w_1 = parseIntTag(line, "<var w_1=\"");
                }
                else if (line.IndexOf("h_1=") > 0)
                {
                    cfg.h_1 = parseIntTag(line, "<var h_1=\"");
                }
                else if (line.IndexOf("cont_1=") > 0)
                {
                    cfg.cont_1 = parseStringTag(line, "cont_1=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    // console.log("decodeOsdCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        public string encodePTZCfg(ptzInfo cfg)
        {
            string msg = "";

            msg += addStartTag(CommonConfig.TAG_PTZ_INFO);
            msg += addTag("leftPos", cfg.leftPos.ToString());
            msg += addTag("rightPos", cfg.rightPos.ToString());
            msg += addTag("upPos", cfg.upPos.ToString());
            msg += addTag("downPos", cfg.downPos.ToString());
            msg += addTag("farPos", cfg.farPos.ToString());
            msg += addTag("nearPos", cfg.nearPos.ToString());
            msg += addTag("currPanPos", cfg.currPanPos.ToString());
            msg += addTag("currTiltPos", cfg.currTiltPos.ToString());
            msg += addTag("currZoomPos", cfg.currZoomPos.ToString());
            msg += addEndTag(CommonConfig.TAG_PTZ_INFO);

            return msg;
        }
        private int decodeDisplayCfg(string[] lines, int offset, displayCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_DISPLAY_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("hue=") > 0)
                {
                    cfg.hue = parseIntTag(line, "<var hue=\"");
                }
                else if (line.IndexOf("brightness=") > 0)
                {
                    cfg.brightness = parseIntTag(line, "<var brightness=\"");
                }
                else if (line.IndexOf("saturation=") > 0)
                {
                    cfg.saturation = parseIntTag(line, "<var saturation=\"");
                }
                else if (line.IndexOf("contrast=") > 0)
                {
                    cfg.contrast = parseIntTag(line, "<var contrast=\"");
                }
                else if (line.IndexOf("ircutmode=") > 0)
                {
                    cfg.ircutmode = parseIntTag(line, "<var ircutmode=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeDisplayCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeVideoCfg(string[] lines, int offset, appSettings cfg)
        {
            int exitFlag;
            string line;
            string endTag;


            endTag = "</" + CommonConfig.TAG_VIDEO_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf(CommonConfig.TAG_VIDEO_CH011) > 0)
                {
                    offset++;
                    offset = decodeVideoChCfg(lines, offset, cfg.videoCh011, 11);
                }
                else if (line.IndexOf(CommonConfig.TAG_VIDEO_CH012) > 0)
                {
                    offset++;
                    offset = decodeVideoChCfg(lines, offset, cfg.videoCh012, 12);
                }
                else if (line.IndexOf(CommonConfig.TAG_VIDEO_CH013) > 0)
                {
                    offset++;
                    offset = decodeVideoChCfg(lines, offset, cfg.videoCh013, 13);
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                    offset++;
                }
                else
                {
                    //console.log("decodeVideoCfg Unknown Line : " + line);
                    offset++;
                }
            }

            return offset;
        }

        private int decodeVideoChCfg(string[] lines, int offset, videoCh011 cfg, int ch)
        {

            int exitFlag;
            string line;
            string endTag;
            if (11 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH011 + ">";
            }
            else if (12 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH012 + ">";
            }
            else if (13 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH013 + ">";
            }
            else
            {
                //console.log("Failed to decode Video Ch Cfg : " + ch);
                return 0;
            }

            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("bps=") > 0)
                {
                    cfg.bps = parseIntTag(line, "<var bps=\"");
                }
                else if (line.IndexOf("fps=") > 0)
                {
                    cfg.fps = parseIntTag(line, "<var fps=\"");
                }
                else if (line.IndexOf("gop=") > 0)
                {
                    cfg.gop = parseIntTag(line, "<var gop=\"");
                }
                else if (line.IndexOf("brmode=") > 0)
                {
                    cfg.brmode = parseIntTag(line, "<var brmode=\"");
                }
                else if (line.IndexOf("piclevel=") > 0)
                {
                    cfg.piclevel = parseIntTag(line, "<var piclevel=\"");
                }
                else if (line.IndexOf("fixqplevel=") > 0)
                {
                    cfg.fixqplevel = parseIntTag(line, "<var fixqplevel=\"");
                }
                else if (line.IndexOf("width=") > 0)
                {
                    cfg.width = parseIntTag(line, "<var width=\"");
                }
                else if (line.IndexOf("height=") > 0)
                {
                    cfg.height = parseIntTag(line, "<var height=\"");
                }
                else if (line.IndexOf("bmainstream=") > 0)
                {
                    cfg.bmainstream = parseIntTag(line, "<var bmainstream=\"");
                }
                else if (line.IndexOf("bfield=") > 0)
                {
                    cfg.bfield = parseIntTag(line, "<var bfield=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeVideoChCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        private int decodeVideoChCfg(string[] lines, int offset, videoCh012 cfg, int ch)
        {
            int exitFlag;
            string line;
            string endTag;
            if (11 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH011 + ">";
            }
            else if (12 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH012 + ">";
            }
            else if (13 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH013 + ">";
            }
            else
            {
                //console.log("Failed to decode Video Ch Cfg : " + ch);
                return 0;
            }

            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("bps=") > 0)
                {
                    cfg.bps = parseIntTag(line, "<var bps=\"");
                }
                else if (line.IndexOf("fps=") > 0)
                {
                    cfg.fps = parseIntTag(line, "<var fps=\"");
                }
                else if (line.IndexOf("gop=") > 0)
                {
                    cfg.gop = parseIntTag(line, "<var gop=\"");
                }
                else if (line.IndexOf("brmode=") > 0)
                {
                    cfg.brmode = parseIntTag(line, "<var brmode=\"");
                }
                else if (line.IndexOf("piclevel=") > 0)
                {
                    cfg.piclevel = parseIntTag(line, "<var piclevel=\"");
                }
                else if (line.IndexOf("fixqplevel=") > 0)
                {
                    cfg.fixqplevel = parseIntTag(line, "<var fixqplevel=\"");
                }
                else if (line.IndexOf("width=") > 0)
                {
                    cfg.width = parseIntTag(line, "<var width=\"");
                }
                else if (line.IndexOf("height=") > 0)
                {
                    cfg.height = parseIntTag(line, "<var height=\"");
                }
                else if (line.IndexOf("bmainstream=") > 0)
                {
                    cfg.bmainstream = parseIntTag(line, "<var bmainstream=\"");
                }
                else if (line.IndexOf("bfield=") > 0)
                {
                    cfg.bfield = parseIntTag(line, "<var bfield=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeVideoChCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        private int decodeVideoChCfg(string[] lines, int offset, videoCh013 cfg, int ch)
        {
            int exitFlag;
            string line;
            string endTag;
            if (11 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH011 + ">";
            }
            else if (12 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH012 + ">";
            }
            else if (13 == ch)
            {
                endTag = "</" + CommonConfig.TAG_VIDEO_CH013 + ">";
            }
            else
            {
                //console.log("Failed to decode Video Ch Cfg : " + ch);
                return 0;
            }

            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("bps=") > 0)
                {
                    cfg.bps = parseIntTag(line, "<var bps=\"");
                }
                else if (line.IndexOf("fps=") > 0)
                {
                    cfg.fps = parseIntTag(line, "<var fps=\"");
                }
                else if (line.IndexOf("gop=") > 0)
                {
                    cfg.gop = parseIntTag(line, "<var gop=\"");
                }
                else if (line.IndexOf("brmode=") > 0)
                {
                    cfg.brmode = parseIntTag(line, "<var brmode=\"");
                }
                else if (line.IndexOf("piclevel=") > 0)
                {
                    cfg.piclevel = parseIntTag(line, "<var piclevel=\"");
                }
                else if (line.IndexOf("fixqplevel=") > 0)
                {
                    cfg.fixqplevel = parseIntTag(line, "<var fixqplevel=\"");
                }
                else if (line.IndexOf("width=") > 0)
                {
                    cfg.width = parseIntTag(line, "<var width=\"");
                }
                else if (line.IndexOf("height=") > 0)
                {
                    cfg.height = parseIntTag(line, "<var height=\"");
                }
                else if (line.IndexOf("bmainstream=") > 0)
                {
                    cfg.bmainstream = parseIntTag(line, "<var bmainstream=\"");
                }
                else if (line.IndexOf("bfield=") > 0)
                {
                    cfg.bfield = parseIntTag(line, "<var bfield=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeVideoChCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeEmailCfg(string[] lines, int offset, emailCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;

            endTag = "</" + CommonConfig.TAG_EMAIL_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("emailserver=") > 0)
                {
                    cfg.emailserver = parseStringTag(line, "<var emailserver=\"");
                }
                else if (line.IndexOf("emailport=") > 0)
                {
                    cfg.emailport = parseIntTag(line, "<var emailport=\"");
                }
                else if (line.IndexOf("ssl=") > 0)
                {
                    cfg.ssl = parseIntTag(line, "<var ssl=\"");
                }
                else if (line.IndexOf("logintype=") > 0)
                {
                    cfg.logintype = parseIntTag(line, "<var logintype=\"");
                }
                else if (line.IndexOf("emailusername=") > 0)
                {
                    cfg.emailusername = parseStringTag(line, "<var emailusername=\"");
                }
                else if (line.IndexOf("emailpassword=") > 0)
                {
                    cfg.emailpassword = parseStringTag(line, "<var emailpassword=\"");
                }
                else if (line.IndexOf("from=") > 0)
                {
                    cfg.from = parseStringTag(line, "<var from=\"");
                }
                else if (line.IndexOf("to=") > 0)
                {
                    cfg.to = parseStringTag(line, "<var to=\"");
                }
                else if (line.IndexOf("subject=") > 0)
                {
                    cfg.subject = parseStringTag(line, "<var subject=\"");
                }
                else if (line.IndexOf("text=") > 0)
                {
                    cfg.text = parseStringTag(line, "<var text=\"");
                }
                else if (line.IndexOf("attatchment=") > 0)
                {
                    cfg.attatchment = parseStringTag(line, "var attatchment=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeEmailCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeTimeCfg(string[] lines, int offset, timeCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;

            endTag = "</" + CommonConfig.TAG_TIME_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("time=") > 0)
                {
                    cfg.time = parseStringTag(line, "<var time=\"");
                }
                else if (line.IndexOf("timeZone=") > 0)
                {
                    cfg.timeZone = parseStringTag(line, "<var timeZone=\"");
                }
                else if (line.IndexOf("tz=") > 0)
                {
                    cfg.tz = parseStringTag(line, "<var tz=\"");
                }
                else if (line.IndexOf("dstmode=") > 0)
                {
                    cfg.dstmode = parseIntTag(line, "<var dstmode=\"");
                }
                else if (line.IndexOf("autoupdate=") > 0)
                {
                    cfg.autoupdate = parseIntTag(line, "<var autoupdate=\"");
                }
                else if (line.IndexOf("autoupdatetzonvif=") > 0)
                {
                    cfg.autoupdatetzonvif = parseIntTag(line, "<var autoupdatetzonvif=\"");
                }
                else if (line.IndexOf("ntpserver=") > 0)
                {
                    cfg.ntpserver = parseStringTag(line, "<var ntpserver=\"");
                }
                else if (line.IndexOf("ntpinterval=") > 0)
                {
                    cfg.ntpinterval = parseIntTag(line, "<var ntpinterval=\"");
                }
                else if (line.IndexOf("ntpenable=") > 0)
                {
                    cfg.ntpenable = parseIntTag(line, "<var ntpenable=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //  console.log("decodeTimeCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        public int decodeStreamCfg(string[] lines, int offset, streamCfg cfg)
        {
            int exitFlag;
            string line;
            string endTag;
            endTag = "</" + CommonConfig.TAG_STREAM_CFG + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("enabled=") > 0)
                {
                    cfg.enabled = parseIntTag(line, "<var enabled=\"");
                }
                else if (line.IndexOf("enableAudio=") > 0)
                {
                    cfg.enableAudio = parseIntTag(line, "<var enableAudio=\"");
                }
                else if (line.IndexOf("publishUrl=") > 0)
                {
                    cfg.publishUrl = parseStringTag(line, "<var publishUrl=\"");
                }
                else if (line.IndexOf("mqttUrl=") > 0)
                {
                    cfg.mqttUrl = parseStringTag(line, "<var mqttUrl=\"");
                }
                else if (line.IndexOf("enableTelnet=") > 0)
                {
                    cfg.enableTelnet = parseIntTag(line, "<var enableTelnet=\"");
                }
                else if (line.IndexOf("telnetUrl=") > 0)
                {
                    cfg.telnetUrl = parseStringTag(line, "<var telnetUrl=\"");
                }
                else if (line.IndexOf("isHd=") > 0)
                {
                    cfg.isHd = int.Parse(parseStringTag(line, "<var isHd=\""));
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeStreamCfg Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }

        private int decodeUuid(string[] lines, int offset, appSettings appSettings)
        {
            int exitFlag;
            string line;
            string endTag;

            endTag = "</" + CommonConfig.TAG_UUID + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("uuid=") > 0)
                {
                    appSettings.uuid = parseStringTag(line, "<var uuid=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeUuid Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }


        public int decodeGrUuid(List<string> lines, int offset, appSettings appSetting)
        {
            int exitFlag;
            string line;
            string endTag;


            endTag = "</" + CommonConfig.TAG_GR_UUID + ">";
            exitFlag = 0;
            while (0 == exitFlag)
            {
                line = lines[offset];


                if (line.IndexOf("grUuid=") > 0)
                {
                    appSetting.grUuid = parseStringTag(line, "<var grUuid=\"");
                }
                else if (line.IndexOf(endTag) == 0)
                {
                    exitFlag = 1;
                }
                else
                {
                    //console.log("decodeGrUuid Unknown Line : " + line);
                }

                offset++;
            }

            return offset;
        }
        public string parseStringTag(string line, string tag)
        {
            int start = 0;
            int end = 0;
            string value;

            start = line.IndexOf(tag);
            start += tag.Length;
            end = line.IndexOf("\"/>");
            value = line.Substring(start, end - start);

            return value;
        }

        public int parseIntTag(string line, string tag)
        {
            int start = 0;
            int end = 0;
            string value;

            start = line.IndexOf(tag);
            start += tag.Length;
            end = line.IndexOf("\"/>");
            value = line.Substring(start, end - start);

            return int.Parse(value);
        }

        public int parseWeekStr(string str, ref int[] sch)
        {
            int i;
            string[] res = str.Split(',');
            int enabled;

            for (i = 0; i < 6; i++)
            {
                sch[i] = -1;
            }

            for (i = 0; i < res.Length; i++)
            {
                string[] sub = res[i].Split('~');
                sch[2 * i + 0] = int.Parse(sub[0]);
                sch[2 * i + 1] = int.Parse(sub[1]);
            }



            enabled = 0;
            hrSch hrSch = VUtil_schToHrSch(sch);
            if (hrSch.tp1StartHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp1StartMin > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp1EndHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp1EndMin > 0)
            {
                enabled = 1;
            }

            if (hrSch.tp2StartHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp2StartMin > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp2EndHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp2EndMin > 0)
            {
                enabled = 1;
            }

            if (hrSch.tp3StartHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp3StartMin > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp3EndHr > 0)
            {
                enabled = 1;
            }
            if (hrSch.tp3EndMin > 0)
            {
                enabled = 1;
            }

            return enabled;
        }

        public hrSch VUtil_schToHrSch(int[] sch)
        {
            int min;

            hrSch hrSch = new hrSch();

            hrSch.tp1StartHr = Convert.ToInt32((sch[0]) / 4);
            min = Convert.ToInt32(((sch[0]) % 4) * 15);
            hrSch.tp1StartMin = Convert.ToInt32(min > 0 ? min : 0);
            hrSch.tp1EndHr = Convert.ToInt32((sch[1] + 1) / 4);
            hrSch.tp1EndMin = Convert.ToInt32(((sch[1] + 1) % 4) * 15);

            hrSch.tp2StartHr = Convert.ToInt32((sch[2]) / 4);
            min = Convert.ToInt32(((sch[2]) % 4) * 15);
            hrSch.tp2StartMin = Convert.ToInt32(min > 0 ? min : 0);
            hrSch.tp2EndHr = Convert.ToInt32((sch[3] + 1) / 4);
            hrSch.tp2EndMin = Convert.ToInt32(((sch[3] + 1) % 4) * 15);

            hrSch.tp3StartHr = Convert.ToInt32((sch[4]) / 4);
            min = Convert.ToInt32(((sch[4]) % 4) * 15);
            hrSch.tp3StartMin = Convert.ToInt32(min > 0 ? min : 0);
            hrSch.tp3EndHr = Convert.ToInt32((sch[5] + 1) / 4);
            hrSch.tp3EndMin = Convert.ToInt32(((sch[5] + 1) % 4) * 15);

            return hrSch;
        }

        public mqttmessage_mdCfg VUtil_parseMdLine(string mdStr)
        {
            mqttmessage_mdCfg mdCfg = new mqttmessage_mdCfg();
            int start;
            int end;

            string index;
            string value;

            start = mdStr.IndexOf("MdRegion[");
            start += 9;
            end = mdStr.IndexOf("]=");
            index = mdStr.Substring(start, end - start);

            start = mdStr.IndexOf("]=\"");
            start += 3;
            end = mdStr.IndexOf("\"/>");
            value = mdStr.Substring(start, end - start);

            mdCfg.index = int.Parse(index);
            mdCfg.value = int.Parse(value);

            return mdCfg;
        }

        public int[] VUtil_hrSchToSch(hrSch hrSch, int[] sch)
        {
            int min;

            sch[0] = Convert.ToInt32(4 * (hrSch.tp1StartHr));
            min = Convert.ToInt32((hrSch.tp1StartMin) / 15);
            sch[0] += Convert.ToInt32((min > 0) ? min : 0);
            sch[1] = Convert.ToInt32(4 * (hrSch.tp1EndHr) - 1);
            min = Convert.ToInt32((hrSch.tp1EndMin) / 15);
            sch[1] += Convert.ToInt32((min > 0) ? min : 0);

            sch[2] = Convert.ToInt32(4 * (hrSch.tp2StartHr));
            min = Convert.ToInt32((hrSch.tp2StartMin) / 15);
            sch[2] += Convert.ToInt32((min > 0) ? min : 0);
            sch[3] = Convert.ToInt32(4 * (hrSch.tp2EndHr) - 1);
            min = Convert.ToInt32((hrSch.tp2EndMin) / 15);
            sch[3] += Convert.ToInt32((min > 0) ? min : 0);

            sch[4] = Convert.ToInt32(4 * (hrSch.tp3StartHr));
            min = Convert.ToInt32((hrSch.tp3StartMin) / 15);
            sch[4] += Convert.ToInt32((min > 0) ? min : 0);
            sch[5] = Convert.ToInt32(4 * (hrSch.tp3EndHr) - 1);
            min = Convert.ToInt32((hrSch.tp3EndMin) / 15);
            sch[5] += Convert.ToInt32((min > 0) ? min : 0);
            return sch;
        }

        public string VUtil_getMsgString(int msgType)
        {
            List<string> msgStr = new List<string>(new string[] { "Keep Alive", "Get Config", "Set Config", "Audio", "File", "Disconnect", "Get Status" });
            return msgStr[msgType];
        }

        public int VUtil_validateConfig(appSettings cfg)
        {
            if (0 != VUtil_isUuidValid(cfg.uuid))
            {
                //console.log("UUID is not valid : " + cfg.uuid);
                return -1;
            }
            if (0 == cfg.grUuid.CompareTo(""))
            {
                //console.log("grUuid is not valid : " + cfg.grUuid);
                return -1;
            }
            if (0 != isStreamCfgValid((cfg.streamCfg)))
            {
                //console.log("streamCfg is not valid");
                return -1;
            }
            if (0 != isTimeCfgValid((cfg.timeCfg)))
            {
                //console.log("timeCfg is not valid");
                return -1;
            }
            if (0 != isEmailCfgValid((cfg.emailCfg)))
            {
                //console.log("emailCfg is not valid");
                return -1;
            }
            if (0 != isVideoCfgValid((cfg)))
            {
                //console.log("videoCfg is not valid");
                return -1;
            }
            if (0 != isDisplayCfgValid((cfg.displayCfg)))
            {
                //console.log("displayCfg is not valid");
                return -1;
            }
            if (0 != isOsdCfgValid((cfg.osdCfg)))
            {
                //console.log("osdCfg is not valid");
                return -1;
            }
            if (0 != isRecordCfgValid((cfg)))
            {
                //console.log("recordCfg is not valid");
                return -1;
            }
            if (0 != isImageCfgValid((cfg.imageCfg)))
            {
                //console.log("imageCfg is not valid");
                return -1;
            }
            if (0 != isMdCfgValid((cfg.mdCfg)))
            {
                //console.log("mdCfg is not valid");
                return -1;
            }
            if (0 != isDevInfoValid((cfg.devInfo)))
            {
                //console.log("devInfo is not valid");
                return -1;
            }
            if (0 != isNwInfoValid((cfg.nwInfo)))
            {
                //console.log("nwInfo is not valid");
                return -1;
            }
            return 0;
        }

        public int VUtil_isUuidValid(string uuid)
        {
            int dashCnt = 0;
            int i;
            int strLen;
            dashCnt = 0;
            strLen = uuid.Length;
            for (i = 0; i < strLen; i++)
            {
                if ('-' == uuid[i])
                {
                    dashCnt++;
                }
            }
            if ((0 == uuid.CompareTo("")) || (0 == uuid.CompareTo("undefined")) || (17 != strLen) || (2 != dashCnt))
            {
                return -1;
            }
            return 0;
        }

        public int isStreamCfgValid(streamCfg cfg1)
        {
            if ((0 != cfg1.enabled) && (1 != cfg1.enabled))
            {
                //console.log("enabled invalid : " + cfg1.enabled);
                return -1;
            }
            if ((0 != cfg1.enableAudio) && (1 != cfg1.enableAudio))
            {
                //console.log("enableAudio invalid : " + cfg1.enableAudio);
                return -1;
            }
            if (0 == cfg1.publishUrl.CompareTo(""))
            {
                //console.log("publishUrl invalid : " + cfg1.publishUrl);
                return -1;
            }
            if ((0 == cfg1.mqttUrl.CompareTo("")) || (0 == cfg1.mqttUrl.CompareTo("null")) || (0 == cfg1.mqttUrl.CompareTo("undefined")))
            {
                //console.log("mqttUrl invalid : " + cfg1.mqttUrl);
                return -1;
            }

            if ((0 != cfg1.enableTelnet) && (1 != cfg1.enableTelnet))
            {
                //console.log("enableTelnet invalid : " + cfg1.enableTelnet);
                return -1;
            }
            if ((0 == cfg1.telnetUrl.CompareTo("")) || (0 == cfg1.telnetUrl.CompareTo("null")) || (0 == cfg1.telnetUrl.CompareTo("undefined")))
            {
                //console.log("telnetUrl invalid : " + cfg1.telnetUrl);
                return -1;
            }
            if (isTelnetCfgValid(cfg1.telnetUrl) < 0)
            {
                //console.log("telnetCfg invalid : " + cfg1.telnetUrl);
                return -1;
            }

            if ((0 != cfg1.isHd) && (1 != cfg1.isHd))
            {
                //console.log("isHd invalid : " + cfg1.isHd);
                return -1;
            }

            return 0;
        }
        public int isTelnetCfgValid(string telnetUrl)
        {
            int i;
            int strLen;
            int val;
            int port;
            bool isDigit;
            bool isAlphaCap;
            bool isAlphaSmall;

            string[] subStr = telnetUrl.Split(':');
            if (null == subStr)
            {
                //console.log("Invalid URL : null");
                return -1;
            }

            if (2 != subStr.Length)
            {
                //console.log("Invalid URL : specify ADDR:PORT");
                return -1;
            }

            if (subStr[0].Length <= 0)
            {
                //console.log("Invalid URL : Addr is invalid");
                return -1;
            }

            if (subStr[1].Length <= 0)
            {
                //console.log("Invalid URL : port is invalid");
                return -1;
            }

            port = int.Parse(subStr[1]);
            strLen = telnetUrl.Length;

            //console.log(">>>>>>> IP : " + subStr[0] + ", Port : " + parseInt(subStr[1]));

            if ((strLen <= 0) || (strLen >= 100))
            {
                //console.log("Invalid URL len : " + strLen);
                return -1;
            }


            if (((subStr[0].IndexOf(".vmukti.com")) <= 0) && ((subStr[0].IndexOf(".ambicam.com")) <= 0))
            {
                // console.log("Invalid URL : " + subStr[0] + ", should have .vmukti.com OR .ambicam.com");
                return -1;
            }
            for (i = 0; i < strLen; i++)
            {
                val = (int)telnetUrl[i];
                if ((val >= 48) && (val <= 57))
                {
                    isDigit = true;
                }
                else
                {
                    isDigit = false;
                }

                if ((val >= 65) && (val <= 90))
                {
                    isAlphaCap = true;
                }
                else
                {
                    isAlphaCap = false;
                }

                if ((val >= 97) && (val <= 122))
                {
                    isAlphaSmall = true;
                }
                else
                {
                    isAlphaSmall = false;
                }

                if ((!isDigit) && (!isAlphaCap) && (!isAlphaSmall))
                {
                    if ((46 != val) && (58 != val))
                    {
                        //console.log("Invalid URL : " + val + "  Neitther ./: NOR alphanum");
                        return -1;
                    }
                }
            }

            if ((port <= 0) || (port >= 65535))
            {
                //console.log("Port is invalid : " + port);
                return -1;
            }

            return 0;
        }
        public int isTimeCfgValid(timeCfg cfg1)
        {
            //@NOTE : time will always mismatch.
            //	if (0 == cfg1.time.CompareTo("")) {
            //		//		console.log("time invalid : " + cfg1.time);
            //		return -1;
            //	}

            if (0 == cfg1.timeZone.CompareTo(""))
            {
                //		console.log("timeZone invalid : " + cfg1.timeZone);
                return -1;
            }
            //	if (0 == cfg1.tz.CompareTo("")) {
            //		//		console.log("tz invalid : " + cfg1.tz);
            //		return -1;
            //	}

            if ((0 != cfg1.dstmode) && (1 != cfg1.dstmode))
            {
                //		console.log("dstmode invalid : " + cfg1.dstmode);
                return -1;
            }
            if ((0 != cfg1.autoupdate) && (1 != cfg1.autoupdate))
            {
                //		console.log("autoupdate invalid : " + cfg1.autoupdate);
                return -1;
            }
            if ((0 != cfg1.autoupdatetzonvif) && (1 != cfg1.autoupdatetzonvif))
            {
                //		console.log("autoupdatetzonvif invalid : " + cfg1.autoupdatetzonvif);
                return -1;
            }

            if (0 == cfg1.ntpserver.CompareTo(""))
            {
                //		console.log("ntpserver invalid : " + cfg1.ntpserver);
                return -1;
            }

            if ((1 != cfg1.ntpinterval) && (2 != cfg1.ntpinterval) && (12 != cfg1.ntpinterval) && (24 != cfg1.ntpinterval))
            {
                //		console.log("ntpinterval invalid : " + cfg1.ntpinterval);
                return -1;
            }
            if ((0 != cfg1.ntpenable) && (1 != cfg1.ntpenable))
            {
                //		console.log("ntpenable invalid : " + cfg1.ntpenable);
                return -1;
            }

            return 0;
        }
        public int isEmailCfgValid(emailCfg cfg1)
        {

            if (0 == cfg1.emailserver.CompareTo(""))
            {
                //		console.log("emailserver invalid : " + cfg1.emailserver);
                return -1;
            }

            if ((cfg1.emailport <= 0) || (cfg1.emailport >= 65535))
            {
                //		console.log("emailport invalid : " + cfg1.emailport);
                return -1;
            }
            if ((0 != cfg1.ssl) && (1 != cfg1.ssl))
            {
                //		console.log("ssl invalid : " + cfg1.ssl);
                return -1;
            }
            if ((1 != cfg1.logintype) && (3 != cfg1.logintype))
            {
                //		console.log("logintype invalid : " + cfg1.logintype);
                return -1;
            }

            if (0 == cfg1.emailusername.CompareTo(""))
            {
                //		console.log("emailusername invalid : " + cfg1.emailusername);
                return -1;
            }
            if (0 == cfg1.emailpassword.CompareTo(""))
            {
                //		console.log("emailpassword invalid : " + cfg1.emailpassword);
                return -1;
            }
            if (0 == cfg1.from.CompareTo(""))
            {
                //		console.log("from invalid : " + cfg1.from);
                return -1;
            }
            if (0 == cfg1.to.CompareTo(""))
            {
                //		console.log("to invalid : " + cfg1.to);
                return -1;
            }
            if (0 == cfg1.subject.CompareTo(""))
            {
                //		console.log("subject invalid : " + cfg1.subject);
                return -1;
            }
            //	if (0 == cfg1.text.CompareTo("")) {
            //		//		console.log("text invalid : " + cfg1.text);
            //		return -1;
            //	}
            //	if (0 == cfg1.attatchment.CompareTo("")) {
            //		//		console.log("attatchment invalid : " + cfg1.attatchment);
            //		return -1;
            //	}


            return 0;
        }

        public int isVideoCfgValid(appSettings cfg1)
        {

            if ((cfg1.videoCh011.bps < 10) || (cfg1.videoCh011.bps > 10000))
            {
                //		console.log("videoCh011.bps invalid : " + cfg1.videoCh011.bps);
                return -1;
            }
            if ((cfg1.videoCh011.fps < 1) || (cfg1.videoCh011.fps > 25))
            {
                //		console.log("videoCh011.fps invalid : " + cfg1.videoCh011.fps);
                return -1;
            }
            //@TODO : Pravin, this is issue
            //	if ((cfg1.videoCh011.gop) < (cfg1.videoCh011.fps)) {
            //		//		console.log("videoCh011.gop invalid : " + cfg1.videoCh011.gop + " < fps : " + cfg1.videoCh011.fps);
            //		return -1;
            //	}
            if ((1 != cfg1.videoCh011.brmode) && (2 != cfg1.videoCh011.brmode) && (4 != cfg1.videoCh011.brmode))
            {
                //		console.log("videoCh011.brmode invalid : " + cfg1.videoCh011.brmode);
                return -1;
            }
            if ((cfg1.videoCh011.piclevel < 1) || (cfg1.videoCh011.piclevel > 5))
            {
                //		console.log("videoCh011.piclevel invalid : " + cfg1.videoCh011.piclevel);
                return -1;
            }
            if ((cfg1.videoCh011.fixqplevel < 1) || (cfg1.videoCh011.fixqplevel > 5))
            {
                //		console.log("videoCh011.fixqplevel invalid : " + cfg1.videoCh011.fixqplevel);
                return -1;
            }
            if ((320 != cfg1.videoCh011.width) && (640 != cfg1.videoCh011.width) && (704 != cfg1.videoCh011.width) && (1280 != cfg1.videoCh011.width))
            {
                //		console.log("videoCh011.width invalid : " + (cfg1.videoCh011.width));
                return -1;
            }
            if ((180 != cfg1.videoCh011.height) && (360 != cfg1.videoCh011.height) && (576 != cfg1.videoCh011.height) && (720 != cfg1.videoCh011.height))
            {
                //		console.log("videoCh011.height invalid : " + cfg1.videoCh011.height);
                return -1;
            }
            if (1 != (cfg1.videoCh011.bmainstream))
            {
                //		console.log("videoCh011.bmainstream invalid : " + cfg1.videoCh011.bmainstream);
                return -1;
            }
            if (0 != (cfg1.videoCh011.bfield))
            {
                //		console.log("videoCh011.bfield invalid : " + cfg1.videoCh011.bfield);
                return -1;
            }


            if ((cfg1.videoCh012.bps < 10) || (cfg1.videoCh012.bps > 10000))
            {
                //		console.log("videoCh012.bps invalid : " + cfg1.videoCh012.bps);
                return -1;
            }
            if ((cfg1.videoCh012.fps < 1) || (cfg1.videoCh012.fps > 25))
            {
                //		console.log("videoCh012.fps invalid : " + cfg1.videoCh012.fps);
                return -1;
            }
            //@TODO : Pravin, this is issue
            //	if ((cfg1.videoCh012.gop) < (cfg1.videoCh012.fps)) {
            //		//		console.log("videoCh012.gop invalid : " + cfg1.videoCh012.gop);
            //		return -1;
            //	}
            if ((1 != cfg1.videoCh012.brmode) && (2 != cfg1.videoCh012.brmode) && (4 != cfg1.videoCh012.brmode))
            {
                //		console.log("videoCh012.brmode invalid : " + cfg1.videoCh012.brmode);
                return -1;
            }
            if ((cfg1.videoCh012.piclevel < 1) || (cfg1.videoCh012.piclevel > 5))
            {
                //		console.log("videoCh012.piclevel invalid : " + cfg1.videoCh012.piclevel);
                return -1;
            }
            if ((cfg1.videoCh012.fixqplevel < 1) || (cfg1.videoCh012.fixqplevel > 5))
            {
                //		console.log("videoCh012.fixqplevel invalid : " + cfg1.videoCh012.fixqplevel);
                return -1;
            }
            if ((320 != cfg1.videoCh012.width) && (640 != cfg1.videoCh012.width) && (704 != cfg1.videoCh012.width) && (1280 != cfg1.videoCh012.width))
            {
                //		console.log("videoCh012.width invalid : " + (cfg1.videoCh012.width));
                return -1;
            }
            if ((180 != cfg1.videoCh012.height) && (360 != cfg1.videoCh012.height) && (576 != cfg1.videoCh012.height) && (720 != cfg1.videoCh012.height))
            {
                //		console.log("videoCh012.height invalid : " + cfg1.videoCh012.height);
                return -1;
            }
            if (1 != (cfg1.videoCh012.bmainstream))
            {
                //		console.log("videoCh012.bmainstream invalid : " + cfg1.videoCh012.bmainstream);
                return -1;
            }
            if (0 != (cfg1.videoCh012.bfield))
            {
                //		console.log("videoCh012.bfield invalid : " + cfg1.videoCh012.bfield);
                return -1;
            }


            if ((cfg1.videoCh013.bps < 10) || (cfg1.videoCh013.bps > 10000))
            {
                //		console.log("videoCh013.bps invalid : " + cfg1.videoCh013.bps);
                return -1;
            }
            if ((cfg1.videoCh013.fps < 1) || (cfg1.videoCh013.fps > 25))
            {
                //		console.log("videoCh013.fps invalid : " + cfg1.videoCh013.fps);
                return -1;
            }
            //@TODO : Pravin, this is issue
            //	if ((cfg1.videoCh013.gop) < (cfg1.videoCh013.fps)) {
            //		//		console.log("videoCh013.gop invalid : " + cfg1.videoCh013.gop);
            //		return -1;
            //	}
            if ((1 != cfg1.videoCh013.brmode) && (2 != cfg1.videoCh013.brmode) && (4 != cfg1.videoCh013.brmode))
            {
                //		console.log("videoCh013.brmode invalid : " + cfg1.videoCh013.brmode);
                return -1;
            }
            if ((cfg1.videoCh013.piclevel < 1) || (cfg1.videoCh013.piclevel > 5))
            {
                //		console.log("videoCh013.piclevel invalid : " + cfg1.videoCh013.piclevel);
                return -1;
            }
            if ((cfg1.videoCh013.fixqplevel < 1) || (cfg1.videoCh013.fixqplevel > 5))
            {
                //		console.log("videoCh013.fixqplevel invalid : " + cfg1.videoCh013.fixqplevel);
                return -1;
            }
            if ((320 != cfg1.videoCh013.width) && (640 != cfg1.videoCh013.width) && (704 != cfg1.videoCh013.width) && (1280 != cfg1.videoCh013.width))
            {
                //		console.log("videoCh013.width invalid : " + (cfg1.videoCh013.width));
                return -1;
            }
            if ((180 != cfg1.videoCh013.height) && (360 != cfg1.videoCh013.height) && (576 != cfg1.videoCh013.height) && (720 != cfg1.videoCh013.height))
            {
                //		console.log("videoCh013.height invalid : " + cfg1.videoCh013.height);
                return -1;
            }
            if (1 != (cfg1.videoCh013.bmainstream))
            {
                //		console.log("videoCh013.bmainstream invalid : " + cfg1.videoCh013.bmainstream);
                return -1;
            }
            if (0 != (cfg1.videoCh013.bfield))
            {
                //		console.log("videoCh013.bfield invalid : " + cfg1.videoCh013.bfield);
                return -1;
            }
            return 0;
        }

        public int isDisplayCfgValid(displayCfg cfg1)
        {
            if ((cfg1.hue < 0) || (cfg1.hue > 100))
            {
                //		console.log("hue invalid : " + cfg1.hue);
                return -1;
            }
            if ((cfg1.brightness < 0) || (cfg1.brightness > 100))
            {
                //		console.log("brightness invalid : " + cfg1.brightness);
                return -1;
            }
            if ((cfg1.saturation < 0) || (cfg1.saturation > 100))
            {
                //		console.log("saturation invalid : " + cfg1.saturation);
                return -1;
            }
            if ((cfg1.contrast < 0) || (cfg1.contrast > 100))
            {
                //		console.log("contrast invalid : " + cfg1.contrast);
                return -1;
            }
            if ((cfg1.ircutmode < 0) || (cfg1.ircutmode > 3))
            {
                //		console.log("ircutmode invalid : " + cfg1.ircutmode);
                return -1;
            }

            return 0;
        }

        public int isOsdCfgValid(osdCfg cfg1)
        {

            if ((cfg1.rgncnt < 1) || (cfg1.rgncnt > 2))
            {
                //		console.log("rgncnt invalid : " + cfg1.rgncnt);
                return -1;
            }
            if ((cfg1.fontsize < 0) || (cfg1.fontsize > 2))
            {
                //		console.log("fontsize invalid : " + cfg1.fontsize);
                return -1;
            }

            //	if ((cfg1.x_0 < 0) || (cfg1.x_0 > 2)) {
            //		//		console.log("x_0 invalid : " + cfg1.x_0);
            //		return -1;
            //	}
            //	if ((cfg1.y_0 < 0) || (cfg1.y_0 > 2)) {
            //		//		console.log("y_0 invalid : " + cfg1.y_0);
            //		return -1;
            //	}
            //	if ((cfg1.w_0 < 0) || (cfg1.w_0 > 2)) {
            //		//		console.log("w_0 invalid : " + cfg1.w_0);
            //		return -1;
            //	}
            //	if ((cfg1.h_0 < 0) || (cfg1.h_0 > 2)) {
            //		//		console.log("h_0 invalid : " + cfg1.h_0);
            //		return -1;
            //	}
            if (0 == cfg1.cont_0.CompareTo(""))
            {
                //		console.log("cont_0 invalid : " + cfg1.cont_0);
                return -1;
            }
            if ((cfg1.show_0 < 0) || (cfg1.show_0 > 1))
            {
                //		console.log("show_0 invalid : " + cfg1.show_0);
                return -1;
            }



            //	if ((cfg1.x_1 < 0) || (cfg1.x_1 > 2)) {
            //		//		console.log("x_1 invalid : " + cfg1.x_1);
            //		return -1;
            //	}
            //	if ((cfg1.y_1 < 0) || (cfg1.y_1 > 2)) {
            //		//		console.log("y_1 invalid : " + cfg1.y_1);
            //		return -1;
            //	}
            //	if ((cfg1.w_1 < 0) || (cfg1.w_1 > 2)) {
            //		//		console.log("w_1 invalid : " + cfg1.w_1);
            //		return -1;
            //	}
            //	if ((cfg1.h_1 < 0) || (cfg1.h_1 > 2)) {
            //		//		console.log("h_1 invalid : " + cfg1.h_1);
            //		return -1;
            //	}
            if (0 == cfg1.cont_1.CompareTo(""))
            {
                //		console.log("cont_1 invalid : " + cfg1.cont_1);
                return -1;
            }
            if ((cfg1.show_1 < 0) || (cfg1.show_1 > 1))
            {
                //		console.log("show_1 invalid : " + cfg1.show_1);
                return -1;
            }
            return 0;
        }

        public int isRecordCfgValid(appSettings cfg1)
        {

            if ((cfg1.recordCh011.startTimerRec < 0) || (cfg1.recordCh011.startTimerRec > 1))
            {
                //		console.log("recordCh011.startTimerRec invalid : " + cfg1.recordCh011.startTimerRec);
                return -1;
            }
            if ((cfg1.recordCh011.startManualRec < 0) || (cfg1.recordCh011.startManualRec > 1))
            {
                //		console.log("recordCh011.startManualRec invalid : " + cfg1.recordCh011.startManualRec);
                return -1;
            }
            if ((cfg1.recordCh011.singlefiletime < 30) || (cfg1.recordCh011.singlefiletime > 600))
            {
                //		console.log("recordCh011.singlefiletime invalid : " + cfg1.recordCh011.singlefiletime);
                return -1;
            }
            if ((cfg1.recordCh011.enable < 0) || (cfg1.recordCh011.enable > 1))
            {
                //		console.log("recordCh011.enable invalid : " + cfg1.recordCh011.enable);
                return -1;
            }
            if (0 == cfg1.recordCh011.filepath.CompareTo(""))
            {
                //		console.log("recordCh011.filepath invalid : " + cfg1.recordCh011.filepath);
                return -1;
            }


            if ((cfg1.recordCh011.startTimerRec < 0) || (cfg1.recordCh011.startTimerRec > 1))
            {
                //		console.log("recordCh011.startTimerRec invalid : " + cfg1.recordCh011.startTimerRec);
                return -1;
            }
            if ((cfg1.recordCh011.startManualRec < 0) || (cfg1.recordCh011.startManualRec > 1))
            {
                //		console.log("recordCh011.startManualRec invalid : " + cfg1.recordCh011.startManualRec);
                return -1;
            }
            if ((cfg1.recordCh011.singlefiletime < 30) || (cfg1.recordCh011.singlefiletime > 600))
            {
                //		console.log("recordCh011.singlefiletime invalid : " + cfg1.recordCh011.singlefiletime);
                return -1;
            }
            if ((cfg1.recordCh011.enable < 0) || (cfg1.recordCh011.enable > 1))
            {
                //		console.log("recordCh011.enable invalid : " + cfg1.recordCh011.enable);
                return -1;
            }
            if (0 == cfg1.recordCh011.filepath.CompareTo(""))
            {
                //		console.log("recordCh011.filepath invalid : " + cfg1.recordCh011.filepath);
                return -1;
            }


            if ((cfg1.recordSch.etm < 0) || (cfg1.recordSch.etm > 2))
            {
                //		console.log("recordSch.etm invalid : " + cfg1.recordSch.etm);
                return -1;
            }
            if (isSchValid(cfg1.recordSch.workday) < 0)
            {
                //		console.log("recordSch.workday invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.weekend) < 0)
            {
                //		console.log("recordSch.weekend invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.sun) < 0)
            {
                //		console.log("recordSch.sun invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.mon) < 0)
            {
                //		console.log("recordSch.mon invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.tue) < 0)
            {
                //		console.log("recordSch.tue invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.wed) < 0)
            {
                //		console.log("recordSch.wed invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.thu) < 0)
            {
                //		console.log("recordSch.thu invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.fri) < 0)
            {
                //		console.log("recordSch.fri invalid");
                return -1;
            }
            if (isSchValid(cfg1.recordSch.sat) < 0)
            {
                //		console.log("recordSch.sat invalid");
                return -1;
            }


            //	if ((cfg1.rectime < 15) || (cfg1.rectime > 180)) {
            //		//		console.log("rectime invalid : " + cfg1.rectime);
            //		return -1;
            //	}

            return 0;
        }

        private int isSchValid(List<int> old)
        {
            int i;

            List<List<int>> list = new List<List<int>>();
            list.Add(old);
            for (i = 0; i < 3; i++)
            {
                if ((list[i][0] < 0) || (list[i][0] > 99))
                {
                    //		console.log("Sch invalid[0] : " + old[i][0]);
                    return -1;
                }

                if ((list[i][1] < -1) || (list[i][1] > 98))
                {
                    //		console.log("Sch invalid[1] : " + old[i][1]);
                    return -1;
                }
            }
            return 0;
        }


        public int isImageCfgValid(imageCfg cfg1)
        {
            //@TODO : Pravin, this validation may be incorrect
            if (0 != (cfg1.devno))
            {
                //		console.log("devno invalid : " + cfg1.devno);
                return -1;
            }
            //@TODO : Pravin, this validation may be incorrect
            if (0 != (cfg1.chn))
            {
                //		console.log("chn invalid : " + cfg1.chn);
                return -1;
            }
            if ((cfg1.flip < 0) || (cfg1.flip > 1))
            {
                //		console.log("flip invalid : " + cfg1.flip);
                return -1;
            }
            if ((cfg1.mirror < 0) || (cfg1.mirror > 1))
            {
                //		console.log("mirror invalid : " + cfg1.mirror);
                return -1;
            }
            if ((cfg1.wdr < 0) || (cfg1.wdr > 4))
            {
                //		console.log("wdr invalid : " + cfg1.wdr);
                return -1;
            }
            return 0;
        }

        public int isMdCfgValid(mdCfg cfg1)
        {
            if ((cfg1.md_email_switch < 0) || (cfg1.md_email_switch > 1))
            {
                //		console.log("md_email_switch invalid : " + cfg1.md_email_switch);
                return -1;
            }
            if ((cfg1.md_snap_switch < 0) || (cfg1.md_snap_switch > 1))
            {
                //		console.log("md_snap_switch invalid : " + cfg1.md_snap_switch);
                return -1;
            }
            if ((cfg1.md_emailsnap_switch < 0) || (cfg1.md_emailsnap_switch > 1))
            {
                //		console.log("md_emailsnap_switch invalid : " + cfg1.md_emailsnap_switch);
                return -1;
            }
            if ((cfg1.md_ftpsnap_switch < 0) || (cfg1.md_ftpsnap_switch > 1))
            {
                //		console.log("md_ftpsnap_switch invalid : " + cfg1.md_ftpsnap_switch);
                return -1;
            }

            if ((cfg1.md_record_switch < 0) || (cfg1.md_record_switch > 1))
            {
                //		console.log("md_record_switch invalid : " + cfg1.md_record_switch);
                return -1;
            }
            if ((cfg1.md_ftprec_switch < 0) || (cfg1.md_ftprec_switch > 1))
            {
                //		console.log("md_ftprec_switch invalid : " + cfg1.md_ftprec_switch);
                return -1;
            }

            if ((cfg1.md_ioalmdo_switch < 0) || (cfg1.md_ioalmdo_switch > 1))
            {
                //		console.log("md_ioalmdo_switch invalid : " + cfg1.md_ioalmdo_switch);
                return -1;
            }

            //@TODO : Pravin, this range may be invalid
            if ((cfg1.etm < 0) || (cfg1.etm > 2))
            {
                //		console.log("etm invalid : " + cfg1.etm);
                return -1;
            }
            //@TODO : Pravin, this range may be invalid
            if ((cfg1.workday < 0) || (cfg1.workday > 100))
            {
                //		console.log("workday invalid : " + cfg1.workday);
                return -1;
            }
            //@TODO : Pravin, this range may be invalid
            if ((cfg1.weekend < 0) || (cfg1.weekend > 100))
            {
                //		console.log("weekend invalid : " + cfg1.weekend);
                return -1;
            }
            if ((cfg1.md_interval < 10) || (cfg1.md_interval > 3600))
            {
                //		console.log("md_interval invalid : " + cfg1.md_interval);
                return -1;
            }

            if ((cfg1.MdbEnable < 0) || (cfg1.MdbEnable > 1))
            {
                //		console.log("MdbEnable invalid : " + cfg1.MdbEnable);
                return -1;
            }
            if ((cfg1.MdSensitiValue < 0) || (cfg1.MdSensitiValue > 3))
            {
                //		console.log("MdSensitiValue invalid : " + cfg1.MdSensitiValue);
                return -1;
            }
            //@TODO : Pravin, this range may be invalid
            if ((cfg1.MDThresholdValue < 0) || (cfg1.MDThresholdValue > 100))
            {
                //		console.log("MDThresholdValue invalid : " + cfg1.MDThresholdValue);
                return -1;
            }
            if ((cfg1.MdInterval < 10) || (cfg1.MdInterval > 3600))
            {
                //		console.log("MdInterval invalid : " + cfg1.MdInterval);
                return -1;
            }
            int i;
            for (i = 0; i < 32; i++)
            {
                if ((cfg1.MdRegion[i] < 0) || (cfg1.MdRegion[i] > 255))
                {
                    //		console.log("MdRegion[" + i + "] invalid : " + cfg1.MdRegion[i]);
                    return -1;
                }
            }

            if ((cfg1.md_alarm < 0) || (cfg1.md_alarm > 1))
            {
                //		console.log("md_alarm invalid : " + cfg1.md_alarm);
                return -1;
            }
            //@TODO : Pravin, this range may be invalid
            if ((cfg1.defend_alarm < 0) || (cfg1.defend_alarm > 100))
            {
                //		console.log("defend_alarm invalid : " + cfg1.defend_alarm);
                return -1;
            }
            //@TODO : Pravin, this range may be invalid
            if ((cfg1.tc_alarm < 0) || (cfg1.tc_alarm > 100))
            {
                //		console.log("tc_alarm invalid : " + cfg1.tc_alarm);
                return -1;
            }
            return 0;
        }

        public int isDevInfoValid(devInfo cfg1)
        {
            if ((cfg1.hwVer < 0) || (cfg1.hwVer > 100))
            {
                //		console.log("hwVer invalid : " + cfg1.hwVer);
                return -1;
            }
            if ((cfg1.swVer < 0) || (cfg1.swVer > 100))
            {
                //		console.log("swVer invalid : " + cfg1.swVer);
                return -1;
            }
            if ((cfg1.provisioningVer < 0) || (cfg1.provisioningVer > 100))
            {
                //		console.log("provisioningVer invalid : " + cfg1.provisioningVer);
                return -1;
            }
            if ((cfg1.publisherVer < 0) || (cfg1.publisherVer > 100))
            {
                //		console.log("publisherVer invalid : " + cfg1.publisherVer);
                return -1;
            }
            if (0 == cfg1.serialNo.CompareTo(""))
            {
                //		console.log("serialNo invalid : " + cfg1.serialNo);
                return -1;
            }
            return 0;
        }

        public int isNwInfoValid(nwInfo cfg1)
        {
            //	if (0 == cfg1.networktype.CompareTo("")) {
            //		//		console.log("networktype invalid : " + cfg1.networktype);
            //		return -1;
            //	}
            //	if (0 == cfg1.macaddress.CompareTo("00:00:00:00:00:00")) {
            //		//		console.log("macaddress invalid : " + cfg1.macaddress);
            //		return -1;
            //	}
            //	if (0 == cfg1.ip.CompareTo("0.0.0.0")) {
            //		//		console.log("ip invalid : " + cfg1.ip);
            //		return -1;
            //	}
            //	if (0 == cfg1.netmask.CompareTo("0.0.0.0")) {
            //		//		console.log("netmask invalid : " + cfg1.netmask);
            //		return -1;
            //	}
            //	if (0 == cfg1.gateway.CompareTo("0.0.0.0")) {
            //		//		console.log("gateway invalid : " + cfg1.gateway);
            //		return -1;
            //	}
            //	if (0 == cfg1.sdnsip.CompareTo("0.0.0.0")) {
            //		//		console.log("sdnsip invalid : " + cfg1.sdnsip);
            //		return -1;
            //	}
            //	if (0 == cfg1.fdnsip.CompareTo("0.0.0.0")) {
            //		//		console.log("fdnsip invalid : " + cfg1.fdnsip);
            //		return -1;
            //	}
            //
            return 0;
        }

    }
}