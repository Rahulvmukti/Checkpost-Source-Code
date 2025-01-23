using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace exam.BAL
{
    public class MQTTMessage
    {
        public int srcId { get; set; } //844
        public int dstId { get; set; }//0
        public int msgType { get; set; }//6
        public int msgLen { get; set; }//0
    }
    public class clientStats
    {
        public string name { get; set; }
        public int isUp { get; set; }
    }
    public class mqttmessage_mdCfg
    {
        public int index { get; set; }
        public int value { get; set; }
    }
    public class hrSch
    {
        public int tp1StartHr { get; set; }
        public int tp1StartMin { get; set; }
        public int tp1EndHr { get; set; }
        public int tp1EndMin { get; set; }
        public int tp2StartHr { get; set; }
        public int tp2StartMin { get; set; }
        public int tp2EndHr { get; set; }
        public int tp2EndMin { get; set; }
        public int tp3StartHr { get; set; }
        public int tp3StartMin { get; set; }
        public int tp3EndHr { get; set; }
        public int tp3EndMin { get; set; }
    }
}