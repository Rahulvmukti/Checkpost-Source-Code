using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Text;

namespace exam
{
    public class Common
    {
      //  public string connectionstring = DecodeConnectionstring(ConfigurationManager.ConnectionStrings["connectionstr"].ConnectionString);
        public static void Log(string msg)
        {
            try
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\log.txt"))
                {
                    StreamWriter sw;
                    sw = File.AppendText(AppDomain.CurrentDomain.BaseDirectory.ToString() + "\\log.txt");
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.WriteLine(DateTime.Now.ToString() + " ---> " + msg.ToString());
                    sw.WriteLine("----------------------------------------------------------------------");
                    sw.Close();
                }
            }
            catch (Exception ex)
            {

            }
        }
        public static string DecodeConnectionstring(string str)
        {
            return str;

            //System.Text.UTF8Encoding utfdecode = new UTF8Encoding();
            //int j = 0;

            //Byte[] encodedBytes = utfdecode.GetBytes(str);
            //Byte[] ans = new Byte[encodedBytes.Length];
            //foreach (Byte b in encodedBytes)
            //{
            //    int i = Convert.ToInt32(b);
            //    if ((i >= 65 && i <= 90) || (i >= 97 && i <= 122))
            //    {
            //        i -= 4;
            //        if ((i < 65 && i >= 61) || (i < 97 && i >= 93))
            //            i = i + 26;
            //    }
            //    else if (i >= 48 && i <= 57)
            //    {
            //        i -= 7;
            //        if (i < 48)
            //            i = i + 10;
            //    }
            //    else
            //    {
            //        if (i == 61)
            //            i = 44;
            //        else if (i == 44)
            //            i = 61;
            //    }
            //    ans[j++] = Convert.ToByte(i);
            //}
            //string temp = base64Decode(utfdecode.GetString(ans, 0, ans.Length));
            //return temp;
        }
        public static string base64Decode(string sData)
        {

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();

            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            byte[] todecode_byte = Convert.FromBase64String(sData);

            int charCount = utf8Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);

            char[] decoded_char = new char[charCount];

            utf8Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);

            string result = new String(decoded_char);
            return result;

        }
        public static string Encode(string str)
        {
            int j = 0;
            System.Text.UTF8Encoding utf = new UTF8Encoding();
            Byte[] encodedBytes = utf.GetBytes(str);
            Byte[] ans = new Byte[encodedBytes.Length];
            foreach (Byte b in encodedBytes)
            {
                int i = Convert.ToInt32(b);
                if ((i >= 65 && i <= 90) || (i >= 97 && i <= 122))
                {
                    i += 4;
                    if ((i > 90 && i <= 97) || (i > 122 && i <= 129))
                        i = i - 26;
                }
                else if (i >= 48 && i <= 57)
                {
                    i += 7;
                    if (i > 57)
                        i = i - 10;
                }
                else
                {
                    if (i == 61)
                        i = 44;
                    else if (i == 44)
                        i = 61;
                }
                ans[j++] = Convert.ToByte(i);
            }
            return utf.GetString(ans);
        }
        public static string base64Encode(string sData)
        {
            byte[] encData_byte = new byte[sData.Length];
            encData_byte = System.Text.Encoding.UTF8.GetBytes(sData);
            string encodedData = Convert.ToBase64String(encData_byte);
            return encodedData;
        }

        public static string Decode(string str)
        {
            System.Text.UTF8Encoding utfdecode = new UTF8Encoding();
            int j = 0;
            Byte[] encodedBytes = utfdecode.GetBytes(str);
            Byte[] ans = new Byte[encodedBytes.Length];
            foreach (Byte b in encodedBytes)
            {
                int i = Convert.ToInt32(b);
                if ((i >= 65 && i <= 90) || (i >= 97 && i <= 122))
                {
                    i -= 4;
                    if ((i < 65 && i >= 61) || (i < 97 && i >= 93))
                        i = i + 26;

                }
                else if (i >= 48 && i <= 57)
                {
                    i -= 7;
                    if (i < 48)
                        i = i + 10;
                }
                else
                {
                    if (i == 61)
                        i = 44;
                    else if (i == 44)
                        i = 61;
                }
                ans[j++] = Convert.ToByte(i);
            }
            return utfdecode.GetString(ans);
        }

        public bool isMobile()
        {
            //try
            //{
            //    string userAgent = HttpContext.Current.Request.UserAgent;
            //    if (userAgent.Contains("ipad") || (userAgent.Contains("iPhone") || (userAgent.Contains("Android"))))
            //    {
            //        return true;
            //    }
            //    else
            //    {
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return false;
            //}
            return false;
        }
    }
}