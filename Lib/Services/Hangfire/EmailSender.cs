using ServiceStack;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Services.Hangfire
{
    public class EmailSender:IEmailSender
    {
        public async System.Threading.Tasks.Task SendEmailAsync(string companyName, string email, string phone, string subject, string message, string lang)
        {
            try
            {
                string[] addr = email.Split('@');

                MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ContactElioplusMessage.ToString(), lang);

                mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");
                mailMessage.To.Add("vvarvi@gmail.com");

                if (mailMessage != null)
                {
                    mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{EmailSubject}", subject).Replace("{Phone}", phone).Replace("{UserEmail}", email).Replace("{Message}", message).ToString();

                    SendEmail(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Email Notification For Community and case {0}, could not be send", ex.Message.ToString()));
            }
        }

        public static MailMessage FillMailMessage(string emailName, string lang)
        {
            return FillMailMessage(emailName, lang, true, ConfigurationManager.AppSettings["EmailNotificationsTemplateFileName"].ToString());
        }

        public static MailMessage FillMailMessage(string emailName, string lang, bool isHtmlFormated, string template)
        {
            string title = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string footer = string.Empty;

            XmlDocument xmlEmailsDoc = new XmlDocument();

            //string rootXmlPath = "C:/inetpub/wwwroot/";
            string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
            //rootXmlPath = ConfigurationManager.AppSettings["RootPath"].ToString();
            string rootTemplatePath = string.Empty;

            if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                rootXmlPath = "C:/Projects/Yuniverse/trunk/WdSElioPlus/WdS.ElioPlus/";
                rootTemplatePath = "Yuniverse\\trunk\\WdSElioPlus\\WdS.ElioPlus\\";
            }

            xmlEmailsDoc.Load(rootXmlPath + "Lib/EmailNotificationSender/EmailXml/Emails_" + lang + ".xml");
            XmlNode xmlEmail = xmlEmailsDoc.SelectSingleNode(@"descendant::email[@name=""" + emailName + @"""]");

            if (xmlEmail != null)
            {
                XmlNode xmlTitle = xmlEmail.SelectSingleNode("child::title");
                if (xmlTitle != null)
                {
                    if (isHtmlFormated)
                        title = xmlTitle.InnerXml.Trim().Replace("\n", "");
                    else
                        title = xmlTitle.InnerText.Trim().Replace("\n", "");
                }
                XmlNode xmlSubject = xmlEmail.SelectSingleNode("child::subject");
                if (xmlSubject != null)
                {
                    if (isHtmlFormated)
                        subject = xmlSubject.InnerXml.Trim().Replace("\n", "");
                    else
                        subject = xmlSubject.InnerText.Trim().Replace("\n", "");
                }
                XmlNode xmlBody = xmlEmail.SelectSingleNode("child::body");
                if (xmlBody != null)
                {
                    if (isHtmlFormated)
                        body = xmlBody.InnerXml;
                    else
                        body = xmlBody.InnerText;
                }
                XmlNode xmlFooter = xmlEmail.SelectSingleNode("child::footer");
                if (xmlFooter != null)
                {
                    if (isHtmlFormated)
                        footer = xmlFooter.InnerXml;
                    else
                        footer = xmlFooter.InnerText;
                }

                string txtTemplate = File.ReadAllText(FileHelper.AddRootToPath(rootTemplatePath + "EmailNotificationHtmlPages\\TxtFiles\\" + template + ".txt"));

                if (txtTemplate != string.Empty)
                {
                    MailMessage mailMessage = new MailMessage();

                    mailMessage.IsBodyHtml = true;

                    body = body.Replace("\r\n", "");
                    footer = footer.Replace("\r\n", "");
                    mailMessage.Subject = subject;
                    mailMessage.Body = txtTemplate.Replace("{TITLE}", title).Replace("{WELCOME_MESSAGE}", subject).Replace("{CONTENT_BODY_MESSAGE}", body).Replace("{FOOTER_MSG}", footer);

                    return mailMessage;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static void SendEmail12(MailMessage mailMessage, bool enableApi = true)
        {
            if (enableApi)
            {
                try
                {
                    if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                    {
                        mailMessage.To.Clear();
                        mailMessage.To.Add("vvarvi@gmail.com");
                    }

                    if (mailMessage.To.Count > 0)
                    {
                        foreach (MailAddress email in mailMessage.To)
                        {
                            Lib.Services.MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email.Address, email.DisplayName, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
                        }
                    }
                    else
                        throw new Exception("emailTo address is empty!");
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                SmtpClient smtpClient = new SmtpClient("in.mailjet.com", 587);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["MailJetApiPublicKey"].ToString(), ConfigurationManager.AppSettings["MailJetApiSecretKey"].ToString());

                try
                {
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                    {
                        mailMessage.To.Clear();
                        mailMessage.To.Add("vvarvi@gmail.com");
                    }

                    smtpClient.Send(mailMessage);
                }
                catch (SmtpFailedRecipientException ex)
                {
                    SmtpStatusCode statusCode = ex.StatusCode;

                    if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
                    {
                        // wait 5 seconds, try a second time
                        Thread.Sleep(5000);

                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw;
                    }
                }
                finally
                {
                    smtpClient.Dispose();
                }
            }
        }

        public static void SendEmail(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient("mail.elioplus.com", 25);

            try
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                //if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                //{
                mailMessage.To.Clear();
                mailMessage.To.Add("vvarvi@gmail.com");
                //}

                //smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                smtpClient.Send(mailMessage);
            }
            catch (SmtpFailedRecipientException ex)
            {
                SmtpStatusCode statusCode = ex.StatusCode;

                if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
                {
                    // wait 5 seconds, try a second time
                    Thread.Sleep(5000);

                    smtpClient.Send(mailMessage);
                }
                else
                {
                    throw;
                }
            }
            finally
            {
                smtpClient.Dispose();
            }
        }

    }
}