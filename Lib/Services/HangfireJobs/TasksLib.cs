using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Web;
using System.Xml;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Services.HangfireJobs.Enumerators;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.HangfireJobs
{
    public class TasksLib
    {
        public static MailMessage FillMailMessage(string emailName)
        {
            return FillMailMessage(emailName, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString());
        }

        public static MailMessage FillMailMessage(string emailName, bool isHtmlFormated, string template)
        {
            string title = string.Empty;
            string subject = string.Empty;
            string body = string.Empty;
            string footer = string.Empty;

            XmlDocument xmlEmailsDoc = new XmlDocument();

            string rootXmlPath = ConfigurationManager.AppSettings["RootPath"].ToString();           //@"C:/inetpub/wwwroot/elioplus/";
            if (!rootXmlPath.EndsWith("/"))
                rootXmlPath += "/";

            //string rootXmlPath = @"C:/inetpub/wwwroot/elioplus.com/httpdocs/";
            //rootXmlPath = ConfigurationManager.AppSettings["RootPath"].ToString();
            string rootTemplatePath = string.Empty;

            if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                rootXmlPath = @"D:/Projects/WdS.ElioPlus/";
                rootTemplatePath = "WdS.ElioPlus\\";
            }

            xmlEmailsDoc.Load(rootXmlPath + "Lib/Services/HangfireJobs/EmailTemplates/EmailContentXml/SchedulerEmails_en.xml");
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
                    mailMessage.Body = txtTemplate.Replace("{TITLE}", title).Replace("{WELCOME_MESSAGE}", "").Replace("{CONTENT_BODY_MESSAGE}", body).Replace("{FOOTER_MSG}", footer);

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

        public void Send(MailMessage mailMessage, bool enableApi = true)
        {
            if (enableApi)
            {
                try
                {
                    if (mailMessage.To.Count > 0)
                    {
                        foreach (MailAddress email in mailMessage.To)
                        {
                            MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email.Address, email.DisplayName, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
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

        public static bool SendEmail(MailMessage mailMessage, bool enableApi = true)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                if (enableApi)
                {
                    try
                    {
                        bool success = false;

                        if (mailMessage.To.Count > 0)
                        {
                            foreach (MailAddress email in mailMessage.To)
                            {
                                MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email.Address, email.DisplayName, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
                                success = true;
                                break;
                            }
                        }
                        else
                            throw new Exception("emailTo address is empty!");

                        return success;
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

                        return true;
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

                        return false;
                    }
                    finally
                    {
                        smtpClient.Dispose();
                    }
                }
            }
            else
                return false;
        }

        public static void SendEmails(MailMessage mailMessage, bool enableApi = true)
        {
            if (enableApi)
            {
                try
                {
                    if (mailMessage.To.Count > 0)
                    {
                        foreach (MailAddress email in mailMessage.To)
                        {
                            MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email.Address, email.DisplayName, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
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

        public static MailMessage GetTemplate(TasksNotificationDescriptions emailDescription)
        {
            try
            {
                MailMessage mailMessage = FillMailMessage(emailDescription.ToString());

                if (mailMessage != null)
                    return mailMessage;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
                //throw Exception("Email Notification To Company For New Inbox Message could not be sent. ERROR MESSAGE -> ", ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void SendSchedulerEmails(List<ElioSchedulerNotificationEmails> emails, int runAfterDays, DataLoader<ElioSchedulerNotificationEmails> loader)
        {
            foreach (ElioSchedulerNotificationEmails email in emails)
            {
                if (email.IsActive)
                {
                    //email = Sql.GetElioSchedulerNotificationEmailById(email.Id, session);

                    MailMessage mailMessage = new MailMessage();

                    mailMessage.IsBodyHtml = true;

                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(email.ReceiverEmailAddress);

                    mailMessage.Subject = email.EmailSubject;
                    mailMessage.Body = email.EmailTemplate;

                    if (mailMessage != null && mailMessage.To.Count > 0)
                    {
                        bool successSent = false;

                        try
                        {
                            successSent = SendEmail(mailMessage, true);
                        }
                        catch (Exception ex)
                        {
                            email.ResponseError = ex.Message.ToString();
                            Logger.DetailedError("Lib.Services.HangfireJobs.TasksLib --> " + email.TaskName, ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        email.Count++;
                        email.DateSent = DateTime.Now;
                        email.IsSent = (successSent) ? 1 : 0;

                        if (!successSent)
                        {
                            email.RemindDate = Convert.ToDateTime(DateTime.Now).AddDays(1);
                        }
                        else
                        {
                            email.RemindDate = Convert.ToDateTime(DateTime.Now).AddDays(runAfterDays);
                        }

                        if (runAfterDays > 0 && email.Count < email.SentLimitCount)
                            email.NextDateSent = Convert.ToDateTime(email.RemindDate).AddDays(runAfterDays);
                        else
                            email.NextDateSent = null;

                        loader.Update(email);
                    }
                }
            }
        }
    }
}