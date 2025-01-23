using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using exam.BAL;

namespace exam.services
{
    public class Emails
    {
        public static bool SendEmail(EmailConfig model)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol |= (SecurityProtocolType)48 | (SecurityProtocolType)192 | (SecurityProtocolType)768 | (SecurityProtocolType)3072;
                //using (MailMessage mail = new MailMessage(ConfigurationManager.AppSettings["SMTPFrom"].ToString(), model.To))
                //{
                //    mail.Subject = model.Subject;
                //    mail.Body = model.Body;
                //    //if (model.oAttachements != null && model.oAttachements.Length > 0)
                //    //{
                //    //    foreach (EmailAttachement item in model.oAttachements)
                //    //    {
                //    //        if (item.MediaType != null || item.MediaType != "")
                //    //        {
                //    //            mail.Attachments.Add(new Attachment(item.FileContentStream, item.FileName, item.MediaType));
                //    //        }
                //    //        else
                //    //        {
                //    //            mail.Attachments.Add(new Attachment(item.FileContentStream, item.FileName));
                //    //        }
                //    //    }
                //    //}
                //    mail.IsBodyHtml = false;
                //    SmtpClient smtp = new SmtpClient();
                //    smtp.Host = ConfigurationManager.AppSettings["SMTPServer"].ToString();
                //    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPSSLEnabled"].ToString());
                //    NetworkCredential networkCredential = new NetworkCredential(ConfigurationManager.AppSettings["SMTPFrom"].ToString(), ConfigurationManager.AppSettings["SMTPPassword"].ToString());
                //    smtp.UseDefaultCredentials = false;
                //    smtp.Credentials = networkCredential;
                //    smtp.Port = Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString());
                //    smtp.Timeout = 30000;
                //    smtp.Send(mail);

                    using (MailMessage mailMessage = new MailMessage())
                    {
                        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SMTPFrom"].ToString());
                        mailMessage.Subject = model.Subject;
                        mailMessage.Body = model.Body;
                        mailMessage.To.Add(new MailAddress(model.To));
                        if (model.oAttachements != null && model.oAttachements.Length > 0)
                        {
                            foreach (EmailAttachement item in model.oAttachements)
                            {
                                if (item.MediaType != null && item.MediaType != "")
                                {
                                    mailMessage.Attachments.Add(new Attachment(item.FileContentStream, item.FileName, item.MediaType));
                                }
                                else
                                {
                                    mailMessage.Attachments.Add(new Attachment(item.FileContentStream, item.FileName));
                                }
                            }
                        }
                        SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SMTPServer"].ToString(), Convert.ToInt32(ConfigurationManager.AppSettings["SMTPPort"].ToString()));
                        smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["SMTPSSLEnabled"].ToString());
                        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPFrom"].ToString(), ConfigurationManager.AppSettings["SMTPPassword"].ToString());
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = NetworkCred;
                        smtp.Timeout = 10000;// Convert.ToInt32(ConfigurationManager.AppSettings["SMTPTimeout"].ToString());
                        smtp.Send(mailMessage);
                    }

                    return true;
                //}
            }
            catch (Exception ex)
            {
                return false;
            }

        }
    }
}