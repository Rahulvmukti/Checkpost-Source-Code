using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam.BAL
{
    public class appSettings
    {
        public string uuid { get; set; }
        public string grUuid { get; set; }
        public streamCfg streamCfg { get; set; }
        public timeCfg timeCfg { get; set; }
        public emailCfg emailCfg { get; set; }
        public videoCh011 videoCh011 { get; set; }
        public videoCh012 videoCh012 { get; set; }
        public videoCh013 videoCh013 { get; set; }
        public displayCfg displayCfg { get; set; }
        public osdCfg osdCfg { get; set; }
        public recordCh011 recordCh011 { get; set; }
        public recordCh012 recordCh012 { get; set; }
        public recordSch recordSch { get; set; }
        public int rectime { get; set; }
        public imageCfg imageCfg { get; set; }
        public mdCfg mdCfg { get; set; }
        public devInfo devInfo { get; set; }
        public nwInfo nwInfo { get; set; }
        public ptzInfo ptzinfo { get; set; }
    }
    public class streamCfg
    {
        public int enabled { get; set; }
        public int enableAudio { get; set; }
        public int enableTelnet { get; set; }
        public int isHd { get; set; }
        public string publishUrl { get; set; }
        public string mqttUrl { get; set; }
        public string telnetUrl { get; set; }
    }
    public class timeCfg
    {
        public int dstmode { get; set; }
        public int autoupdate { get; set; }
        public int autoupdatetzonvif { get; set; }
        public int ntpinterval { get; set; }
        public int ntpenable { get; set; }
        public string time { get; set; }
        public string timeZone { get; set; }
        public string tz { get; set; }
        public string ntpserver { get; set; }
    }
    public class emailCfg
    {
        public string emailserver { get; set; }
        public string emailusername { get; set; }
        public string emailpassword { get; set; }
        public string from { get; set; }
        public string to { get; set; }
        public string subject { get; set; }
        public string text { get; set; }
        public string attatchment { get; set; }
        public int emailport { get; set; }
        public int ssl { get; set; }
        public int logintype { get; set; }
    }
    public class videoCh011
    {
        public int bps { get; set; }
        public int fps { get; set; }
        public int gop { get; set; }
        public int brmode { get; set; }
        public int piclevel { get; set; }
        public int fixqplevel { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bmainstream { get; set; }
        public int bfield { get; set; }
    }
    public class videoCh012
    {
        public int bps { get; set; }
        public int fps { get; set; }
        public int gop { get; set; }
        public int brmode { get; set; }
        public int piclevel { get; set; }
        public int fixqplevel { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bmainstream { get; set; }
        public int bfield { get; set; }
    }
    public class videoCh013
    {
        public int bps { get; set; }
        public int fps { get; set; }
        public int gop { get; set; }
        public int brmode { get; set; }
        public int piclevel { get; set; }
        public int fixqplevel { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bmainstream { get; set; }
        public int bfield { get; set; }
    }
    public class displayCfg
    {
        public int hue { get; set; }
        public int brightness { get; set; }
        public int saturation { get; set; }
        public int contrast { get; set; }
        public int ircutmode { get; set; }
    }
    public class osdCfg
    {
        public int rgncnt { get; set; }
        public int fontsize { get; set; }
        public int x_0 { get; set; }
        public int y_0 { get; set; }
        public int w_0 { get; set; }
        public int h_0 { get; set; }
        public string cont_0 { get; set; }
        public int show_0 { get; set; }
        public int x_1 { get; set; }
        public int y_1 { get; set; }
        public int w_1 { get; set; }
        public int h_1 { get; set; }
        public string cont_1 { get; set; }
        public int show_1 { get; set; }
    }
    public class recordCh011
    {
        public int startTimerRec { get; set; }
        public int startManualRec { get; set; }
        public int singlefiletime { get; set; }
        public int enable { get; set; }
        public string filepath { get; set; }
    }
    public class recordCh012
    {
        public int startTimerRec { get; set; }
        public int startManualRec { get; set; }
        public int singlefiletime { get; set; }
        public int enable { get; set; }
        public string filepath { get; set; }
    }
    public class recordSch
    {
        public int etm { get; set; }
        public int enWorkday { get; set; }
        public int enWeekend { get; set; }
        public int enSun { get; set; }
        public int enMon { get; set; }
        public int enTue { get; set; }
        public int enWed { get; set; }
        public int enThu { get; set; }
        public int enFri { get; set; }
        public int enSat { get; set; }

        public List<int> workday { get; set; }
        public List<int> weekend { get; set; }
        public List<int> sun { get; set; }
        public List<int> mon { get; set; }
        public List<int> tue { get; set; }
        public List<int> wed { get; set; }
        public List<int> thu { get; set; }
        public List<int> fri { get; set; }
        public List<int> sat { get; set; }
    }
    public class imageCfg
    {
        public int devno { get; set; }
        public int chn { get; set; }
        public int flip { get; set; }
        public int mirror { get; set; }
        public int wdr { get; set; }
    }
    public class mdCfg
    {
        public int md_email_switch { get; set; }
        public int md_snap_switch { get; set; }
        public int md_emailsnap_switch { get; set; }
        public int md_ftpsnap_switch { get; set; }
        public int md_record_switch { get; set; }
        public int md_ftprec_switch { get; set; }
        public int md_ioalmdo_switch { get; set; }
        public int etm { get; set; }
        public int workday { get; set; }
        public int weekend { get; set; }
        public int md_interval { get; set; }
        public int MdbEnable { get; set; }
        public int MdSensitiValue { get; set; }
        public int MDThresholdValue { get; set; }
        public int MdInterval { get; set; }
        public List<int> MdRegion { get; set; }
        public int md_alarm { get; set; }
        public int defend_alarm { get; set; }
        public int tc_alarm { get; set; }
    }
    public class devInfo
    {
        public int hwVer { get; set; }
        public int swVer { get; set; }
        public int provisioningVer { get; set; }
        public int publisherVer { get; set; }
        public string serialNo { get; set; }
    }
    public class nwInfo
    {
        public string networktype { get; set; }
        public string macaddress { get; set; }
        public string ip { get; set; }
        public string netmask { get; set; }
        public string gateway { get; set; }
        public string sdnsip { get; set; }
        public string fdnsip { get; set; }
    }
    public class ptzInfo
    {
        public int leftPos { get; set; }
        public int rightPos { get; set; }
        public int upPos { get; set; }
        public int downPos { get; set; }
        public int farPos { get; set; }
        public int nearPos { get; set; }
        public int currPanPos { get; set; }
        public int currTiltPos { get; set; }
        public int currZoomPos { get; set; }
    }
}