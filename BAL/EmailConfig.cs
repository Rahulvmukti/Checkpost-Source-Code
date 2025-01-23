using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace exam.BAL
{
    public class EmailConfig
    {
        public string To { get; set; }
        public string From { get; set; }
        public string CC { get; set; }
        public string BCC { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public EmailAttachement[] oAttachements { get; set; }
    }

    public class EmailAttachement
    {
        public string FileName { get; set; }
        //public string FileContentString { get; set; }
        public Stream FileContentStream { get; set; }
        //public byte[] FileContentByte { get; set; }
        public string MediaType { get; set; }
    }

}