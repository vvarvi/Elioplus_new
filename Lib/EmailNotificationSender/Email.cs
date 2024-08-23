using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Configuration;

namespace WdS.ElioPlus.Lib.EmailNotificationSender
{
    public class Email
    {
        string mFrom;
        string mTo;
        string mSubject;
        string mBody;
        string mServer;
        bool mIsBodyHtml;

        public Email(string emailFrom, string emailTo, string emailSubject, string emailBody, string mailServer, bool isBodyHtml)
        {
            mFrom = emailFrom;
            mTo = emailTo;
            mSubject = emailSubject;
            mBody = emailBody;
            mServer = mailServer;
            mIsBodyHtml = isBodyHtml;

        }

        public string SendEmailResults()
        {
            string results = string.Empty;

            try
            {
                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(mFrom);
                mailMessage.To.Add(mTo);
                mailMessage.Body = mBody;
                mailMessage.Subject = mSubject;
                mailMessage.IsBodyHtml = mIsBodyHtml;

                SmtpClient smtpClient = new SmtpClient("smtpout.asia.secureserver.net", 80);
                smtpClient.EnableSsl = false;
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
                smtpClient.Send(mailMessage);                
            }
            catch (Exception ex)
            {
                results = ex.Message.ToString();
            }

            return results;
        }
    }
}