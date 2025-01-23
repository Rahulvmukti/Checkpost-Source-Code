using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam.BAL
{
    public class CommonConfig
    {
        public static string TAG_UUID = "Uuid";
        public static string TAG_GR_UUID = "GrUuid";
        public static string TAG_STREAM_CFG = "StreamCfg";
        public static string TAG_TIME_CFG = "TimeCfg";
        public static string TAG_EMAIL_CFG = "EmailCfg";
        public static string TAG_VIDEO_CFG = "VideoCfg";
        public static string TAG_DISPLAY_CFG = "DisplayCfg";
        public static string TAG_OSD_CFG = "OsdCfg";
        public static string TAG_RECORD_CFG = "RecordCfg";
        public static string TAG_IMAGE_CFG = "ImageCfg";
        public static string TAG_MD_CFG = "MdCfg";
        public static string TAG_DEV_INFO = "DevInfo";
        public static string TAG_NET_INFO = "NetworkInfo";
        public static string TAG_PTZ_INFO = "PtzCfg";
        public static string TAG_VIDEO_CH011 = "VideoCh011";
        public static string TAG_VIDEO_CH012 = "VideoCh012";
        public static string TAG_VIDEO_CH013 = "VideoCh013";

        public static string TAG_RECORD_CH011 = "RecordCh011";
        public static string TAG_RECORD_CH012 = "RecordCh012";
        public static string TAG_RECORD_SCH = "RecordSch";

        public static int ADDR_ID_BROADCAST = -1;
        public static int ADDR_ID_CONF_SERVER = 0;

        public const int MSG_TYPE_KEEP_ALIVE = 0;
        public const int MSG_TYPE_GET_CONFIG = 1;
        public const int MSG_TYPE_SET_CONFIG = 2;
        public const int MSG_TYPE_AUDIO = 3;
        public const int MSG_TYPE_FILE = 4;
        public const int MSG_TYPE_DISCONNECT = 5;
        public const int MSG_TYPE_GET_STATUS = 6;
        public const int MSG_TYPE_GET_CLI_STATS = 7;
        public const int MSG_TYPE_GET_GRP_STATS = 8;
        public const int MSG_TYPE_ALARM = 9;
        public const int MSG_TYPE_FACTORY_DFLT = 10;
        public const int MSG_TYPE_REBOOT = 11;
        public const int MSG_TYPE_REDIRECT = 12;
        public const int MSG_TYPE_EMail_publishURL_cameraName = 104;
    }
}