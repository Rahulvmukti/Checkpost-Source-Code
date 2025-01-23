using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace exam.BAL
{
    public class Log
    {


        public Log()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void logmessage(string funname, string msg)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\StreamLog.txt"))
                {
                    StreamWriter sw;
                    sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\StreamLog.txt");
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.WriteLine(DateTime.Now.ToString() + " ---> " + funname + " --> " + msg.ToString());
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static void logmessageAPI(string funname, string msg)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\logapi.txt"))
                {
                    StreamWriter sw;
                    sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\logapi.txt");
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.WriteLine(DateTime.Now.ToString() + " ---> " + funname + " --> " + msg.ToString());
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}