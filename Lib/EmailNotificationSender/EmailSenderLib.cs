using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using WdS.ElioPlus.Objects;
using System.Configuration;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Xml;
using System.Threading;
using System.IO;
using WdS.ElioPlus.Lib.Utils;
using System.Net;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using Microsoft.Crm.Sdk.Messages;
using WdS.ElioPlus.Lib.Services.AnonymousTrackingAPI.Entities;

namespace WdS.ElioPlus.Lib.EmailNotificationSender
{
    public class EmailSenderLib
    {
        #region Global Methods

        public static MailMessage FillMailMessage(string emailName, string lang, DBSession session)
        {
            return FillMailMessage(emailName, lang, true, ConfigurationManager.AppSettings["EmailNotificationsTemplateFileName"].ToString(), session);
        }

        public static MailMessage FillMailMessage(string emailName, string lang, bool isHtmlFormated, string template, DBSession session)
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
                rootXmlPath = "D:/Project/ElioMarketplace/";
                rootTemplatePath = "ElioMarketplace\\";
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
                    mailMessage.Body = txtTemplate.Replace("{TITLE}",title).Replace("{WELCOME_MESSAGE}", subject).Replace("{CONTENT_BODY_MESSAGE}", body).Replace("{FOOTER_MSG}", footer);

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

        //public static void SendEmailWithReplyTo(MailMessage mailMessage, string replyToEmail)
        //{
        //    SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);

        //    try
        //    {
        //        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
        //        mailMessage.ReplyTo = new MailAddress(replyToEmail);
                
        //        smtpClient.EnableSsl = true;
        //        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
        //        smtpClient.Send(mailMessage);
        //    }
        //    catch (SmtpFailedRecipientException ex)
        //    {
        //        SmtpStatusCode statusCode = ex.StatusCode;

        //        if (statusCode == SmtpStatusCode.MailboxBusy || statusCode == SmtpStatusCode.MailboxUnavailable || statusCode == SmtpStatusCode.TransactionFailed)
        //        {
        //            // wait 5 seconds, try a second time
        //            Thread.Sleep(5000);

        //            smtpClient.Send(mailMessage);
        //        }
        //        else
        //        {

        //            throw;

        //        }
        //    }
        //    finally
        //    {
        //        smtpClient.Dispose();
        //    }
        //}

        #region Send Method Elio

        public static void SendEmail_Elio(MailMessage mailMessage, string email, int notificationEmailId, DBSession session)
        {
            bool sendEmailToUser = Sql.SendNotificationEmailToUser(email, notificationEmailId, session);
            if (sendEmailToUser)
            {
                SendEmail_Elio(mailMessage, email);
            }
        }

        public static void SendEmail_Elio(MailMessage mailMessage, string email)
        {
            SmtpClient smtpClient = new SmtpClient("mail.elioplus.com", 25);

            try
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                    email = "vvarvi@gmail.com";

                mailMessage.To.Add(email);

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

        public static void SendEmail_Elio(MailMessage mailMessage)
        {
            SmtpClient smtpClient = new SmtpClient("mail.elioplus.com", 25);
            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());

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

        public static void SendEmails_Elio(MailMessage mailMessage, List<string> emails)
        {
            SmtpClient smtpClient = new SmtpClient("mail.elioplus.com", 25);

            try
            {
                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");

                if (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                {
                    mailMessage.To.Clear();
                    mailMessage.To.Add("vvarvi@gmail.com");
                }
                else
                {
                    foreach (string emailAddress in emails)
                    {
                        if (Validations.IsEmail(emailAddress))
                        {
                            MailAddress address = new MailAddress(emailAddress);

                            mailMessage.To.Add(address);
                        }
                    }
                }

                if (mailMessage.To.Count > 0)
                {
                    //smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                    smtpClient.Send(mailMessage);
                }
                else
                {
                    throw new Exception("Email address To has no emails");
                }
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

        #endregion

        #region Send Method Mailjet
                
        public static void SendEmail_API(MailMessage mailMessage, string email, string companyName)
        {
            try
            {
                bool success = Lib.Services.MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email, companyName, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
                
                if (!success)
                    throw new Exception("mail not sent");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //smtpClient.Dispose();
            }
        }

        public static void SendEmail(MailMessage mailMessage, string email, int notificationEmailId, DBSession session, bool enableApi = true)
        {
            try
            {
                bool sendEmailToUser = Sql.SendNotificationEmailToUser(email, notificationEmailId, session);
                if (sendEmailToUser)
                {
                    SendEmail(mailMessage, email, enableApi);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void SendEmail(MailMessage mailMessage, string email, bool enableApi = true)
        {
            if (enableApi)
            {
                try
                {
                    Lib.Services.MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email, email, mailMessage.Subject, "", mailMessage.Body).Wait(2000);
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

                    mailMessage.To.Add(email);

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

        public static void SendEmail(MailMessage mailMessage, bool enableApi = true)
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

        public static void SendEmails(MailMessage mailMessage, List<string> emails, int emailNotificationDesctriptions, bool isSubAccount, DBSession session, bool enableApi = true)
        {
            if (enableApi)
            {
                try
                {
                    if (emails.Count > 0)
                    {
                        foreach (string email in emails)
                        {
                            bool allowSend = false;
                            if (emailNotificationDesctriptions == (int)EmailNotificationDesctriptions.NotFullRegisteredUser)
                                allowSend = true;
                            else
                                allowSend = Sql.HasEmailSettingsAccess(email, emailNotificationDesctriptions, isSubAccount, session);

                            if (allowSend)
                            {
                                Lib.Services.MailJetAPI.MailjetSenderLib.SendEmailRunAsync(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus", email, "", mailMessage.Subject, "", mailMessage.Body).Wait(2000);
                            }
                        }
                    }
                    else
                        throw new Exception("No email address To was found!");
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
                    else
                    {
                        foreach (string emailAddress in emails)
                        {
                            if (Validations.IsEmail(emailAddress))
                            {
                                MailAddress address = new MailAddress(emailAddress);

                                mailMessage.To.Add(address);
                            }
                        }
                    }

                    if (mailMessage.To.Count > 0)
                    {
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email address To has no emails");
                    }
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

        #endregion

        #endregion

        #region LandingPages emails

        public static void SaveUsersEmailFromLandingPages(string email, string landingPage, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.InboxEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailNotificationsLandingTemplateFileName"].ToString(), session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "NewInboxElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{LANDING_PAGES}", landingPage).Replace("{CompanyEmail}", email);

                            SendEmail(mailMessage, true);
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email (" + email + ") from Landing Page (" + landingPage + ") could not be send");
                }
            }
        }

        #endregion

        #region Task Emails

        public static void SendNewTaskNotificationEmail(ElioOpportunitiesUsersTasks task, string lang, DBSession session)
        {
            string exception = "";

            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    ElioUsers user = Sql.GetUserById(task.UserId, session);
                    if (user != null)
                    {
                        ElioOpportunitiesUsers opportunityUser = Sql.GetOpportunityById(task.OpportunityId, session);
                        if (opportunityUser != null)
                        {
                            MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewTaskNotificationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailNotificationsTaskTemplateFileName"].ToString(), session);

                            mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                            string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                            
                            url += HttpContext.Current.Request.Url.Authority + "/task-reminder?TaskViewID=" + task.Id.ToString();

                            //string url = "https://www.elioplus.com/task-reminder?TaskViewID=" + task.Id.ToString();
                            
                            //if (ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
                            //{
                            //    url = "http://localhost:61107/task-reminder?TaskViewID=" + task.Id.ToString();
                            //}

                            if (mailMessage != null)
                            {
                                mailMessage.Body = mailMessage.Body
                                    .Replace("{CompanyName}", user.CompanyName)
                                    .Replace("{TaskDateIn}", task.LastUpdated.ToString())
                                    .Replace("{TaskRemindDate}", task.RemindDate.ToString())
                                    .Replace("{OpportunityName}", opportunityUser.OrganizationName)
                                    .Replace("{OpportunityEmail}", opportunityUser.Email)
                                    .Replace("{TaskSubject}", task.TaskSubject)
                                    .Replace("{TaskContent}", task.TaskContent)
                                    .Replace("{link_btn}", "<a href=\"" + url + "\"  style=\"background:url(button_bg.png) 0 0 repeat-x #39BD68;font-family:Arial,Helvetica,sans-serif;display:block;color:#fff!important;height:33px;text-align:center;text-decoration:none;font-weight:bold;font-size:16px;v-align:center;line-height: 33px;\">Send Task Email</a>").ToString();

                                SendEmail(mailMessage, true);
                            }
                        }
                        else
                        {
                            //no opportunity user found

                            exception = string.Format("The opportunity user  with {0} who is related to Task {1}, could not be found", task.OpportunityId.ToString(), task.Id.ToString());
                            throw new Exception(exception);
                        }
                    }
                    else
                    {
                        //no user found
                        exception = string.Format("The user with ID {0} who added Task, could not be found", task.UserId.ToString());
                        throw new Exception(exception);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception((ex.Message == "") ? "Email Notification For New Task could not be send" : "Email Notification For New Task could not be send -->" + exception);
                }
            }
        }

        public static void SendTaskReminderEmail(int taskId, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    ElioOpportunitiesUsersTasks task = Sql.GetTaskById(taskId, session);
                    if (task != null)
                    {
                        ElioUsers user = Sql.GetUserById(task.UserId, session);
                        if (user != null)
                        {
                            ElioOpportunitiesUsers opportunityUser = Sql.GetOpportunityById(task.OpportunityId, session);
                            if (opportunityUser != null)
                            {
                                MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.TaskReminderEmail.ToString(), lang, session);

                                if (mailMessage != null)
                                {
                                    mailMessage.Body = mailMessage.Body
                                   .Replace("{CompanyName}", user.CompanyName)
                                   .Replace("{TaskSubject}", task.TaskSubject)
                                   .Replace("{TaskDateIn}", task.LastUpdated.ToString())
                                   .Replace("{TaskCompanyName}", opportunityUser.OrganizationName)
                                   .Replace("{TaskRemindDate}", task.RemindDate.ToString())
                                   .Replace("{TaskContent}", task.TaskContent).ToString();

                                    SendEmail(mailMessage, user.Email, true);
                                }
                            }
                            else
                            {
                                //no opportunity user found
                                throw new Exception(string.Format("The opportunity user {0} who is related to Task {1}, could not be found", task.OpportunityId.ToString(), task.Id.ToString()));
                            }
                        }
                        else
                        {
                            //no user found
                            throw new Exception(string.Format("The user {0} who added Task, could not be found", task.UserId.ToString()));
                        }
                    }
                    else
                    {
                        //task not found
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        #endregion

        #region Stripe emails

        public static void SendStripeTrialActivationEmail(string email, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.StripeTrialActivationEmail.ToString(), lang, session);

                    if (mailMessage != null)
                    {
                        SendEmail(mailMessage, email, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Stripe Trial Activation could not be send");
                }
            }
        }

        #endregion

        #region Elioplus Emails

        public static void SendRFQApprovedRequestMessageNotificationEmail(ApiLeadCategory leadCategory, int leadId, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string products = "";

                    ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadById(leadId, session);
                    if (lead != null)
                    {

                        List<ElioSnitcherLeadsPageviews> pvws = Sql.GetSnitcherLeadPageViewsByElioWebsiteLeadsId(lead.Id, session);
                        if (pvws.Count > 0)
                        {
                            if (leadCategory == ApiLeadCategory.isRFQRequest)
                            {
                                MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.RFQApprovedRequestEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailRFQInvitationsTemplateFileName"].ToString(), session);

                                if (mailMessage != null)
                                {
                                    string prdQuery = " ";

                                    foreach (ElioSnitcherLeadsPageviews pageView in pvws)
                                    {
                                        if (!string.IsNullOrEmpty(pageView.Product))
                                        {
                                            products += pageView.Product + ",";
                                            prdQuery += "products like '%" + pageView.Product + "%' OR ";
                                        }
                                    }

                                    if (products.EndsWith(","))
                                        products = products.Substring(0, products.Length - 1);

                                    if (prdQuery.EndsWith(" OR "))
                                        prdQuery = prdQuery.Substring(0, prdQuery.Length - 4);

                                    //TO DO --> BUG FIX
                                    DataTable usersEmails = session.GetDataTable(@"SELECT u.email
                                                                              FROM Elio_snitcher_user_country_products cp
                                                                              inner join elio_users u
	                                                                            on u.id = cp.user_id
                                                                                and u.account_status = 1
                                                                                and u.billing_type > 1
                                                                                and u.id != 136710
                                                                              where 1 = 1 
                                                                              and (cp.country like '%" + lead.LeadCountry + "%') " +
                                                                              "and (" + prdQuery + ")");

                                    if (usersEmails.Rows.Count > 0)
                                    {
                                        foreach (DataRow row in usersEmails.Rows)
                                        {
                                            if (!string.IsNullOrEmpty(row["email"].ToString()))
                                            {
                                                mailMessage.To.Add(row["email"].ToString());
                                            }
                                        }

                                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", lead.LeadCompanyName).Replace("{Product}", products).Replace("{Description}", lead.Message);

                                        SendEmail(mailMessage, true);
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("SendRFQApprovedRequestMessageNotificationEmail receiver user/s not found for Lead.ID = {0}, in Country = {1} and for product = {2}.", lead.Id, lead.LeadCountry, prdQuery.Replace("products like ", "").Replace("%", "").Replace("'", "").Trim()));
                                    }
                                }
                                else
                                {
                                    throw new Exception(string.Format("SendRFQApprovedRequestMessageNotificationEmail MailMessage for emailName {0} not found.", EmailNotificationDesctriptions.RFQApprovedRequestEmail.ToString()));
                                }
                            }
                            else if (leadCategory == ApiLeadCategory.isRFQMessage)
                            {
                                if (lead.RfpMessageCompanyIdIsFor != "136710")
                                {
                                    foreach (ElioSnitcherLeadsPageviews pageView in pvws)
                                    {
                                        products += pageView.Product + ",";
                                    }

                                    if (products != "")
                                    {
                                        if (products.EndsWith(","))
                                            products = products.Substring(0, products.Length - 1);

                                        ElioUsers userEmail = Sql.GetCompanyEmailAndOfficialEmail(Convert.ToInt32(lead.RfpMessageCompanyIdIsFor), session);
                                        if (userEmail != null)
                                        {
                                            MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.RFQApprovedMessageEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailRFQInvitationsTemplateFileName"].ToString(), session);

                                            if (mailMessage != null)
                                            {
                                                mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", lead.LeadCompanyName).Replace("{Product}", products).Replace("{Description}", lead.Message);

                                                mailMessage.To.Add(userEmail.Email);

                                                SendEmail(mailMessage, true);
                                            }
                                            else
                                            {
                                                throw new Exception(string.Format("SendRFQApprovedRequestMessageNotificationEmail MailMessage for emailName {0} not found.", EmailNotificationDesctriptions.RFQApprovedMessageEmail.ToString()));
                                            }
                                        }
                                        else
                                        {
                                            throw new Exception(string.Format("SendRFQApprovedRequestMessageNotificationEmail receiver user ID {0} not found.", lead.RfpMessageCompanyIdIsFor));
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception(string.Format("SendRFQApprovedRequestMessageNotificationEmail products not found for Lead.ID {0}.", lead.Id));
                                    }
                                }
                            }
                        }
                        else
                        {
                            throw new Exception("SendRFQApprovedRequestMessageNotificationEmail Email Notification To Company For Lead category " + leadCategory + " could not be sent. No Leads Page views found for lead ID:" + leadId);
                        }
                    }
                    else
                    {
                        throw new Exception("SendRFQApprovedRequestMessageNotificationEmail Email Notification To Company For Lead category " + leadCategory + " could not be sent. No Lead found for lead ID:" + leadId);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                    //new Exception("SendRFQApprovedRequestMessageNotificationEmail Email Notification To Company For Lead category " + leadCategory + " could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToCompanyForNewReview(ElioUsers user, string companyEmail, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewReview.ToString(), lang, session);

                    if (mailMessage != null)
                    {
                        if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", user.CompanyName);
                        }
                        else
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", user.Username + " (username)");
                        }

                        SendEmail(mailMessage, companyEmail, (int)EmailNotificationDesctriptions.NewReview, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Company For New Review could not be sent");
                }
            }
        }

        public static void ContactElioplus(string companyName, string companyEmail, string subject, string message, string companyPhone, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string[] addr = companyEmail.Split('@');
                    
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ContactElioplusMessage.ToString(), lang, session);

                    mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage != null)
                    {
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{EmailSubject}", subject).Replace("{Phone}", companyPhone).Replace("{UserEmail}", companyEmail).Replace("{Message}", message).ToString();

                        SendEmail(mailMessage, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Email Notification For Contact To Elioplus could not be sent. MESSAGE ERROR FROM LIB(" + ex + ")");
                }
            }
        }

        public static void ContactElioplusHangFire(string companyName, string companyEmail, string subject, string message, string companyPhone, string lang, DBSession session)
        {
            //if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            //{
            try
            {
                string[] addr = companyEmail.Split('@');

                MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ContactElioplusMessage.ToString(), lang, session);

                mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");
                mailMessage.To.Add("vvarvi@gmail.com");

                //mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                if (mailMessage != null)
                {
                    mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{EmailSubject}", subject).Replace("{Phone}", companyPhone).Replace("{UserEmail}", companyEmail).Replace("{Message}", message).ToString();

                    SendEmail(mailMessage, true);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Email Notification For Contact To Elioplus could not be sent. MESSAGE ERROR FROM LIB(" + ex + ")");
            }
            //}
        }

        public static void ContactElioplusForDemoRequest(string firstName, string lastName, string companyName, string companyEmail, string receiverEmail, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string[] addr = companyEmail.Split('@');

                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ContactElioplusDemoRequestMessage.ToString(), lang, session);

                    mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");

                    if (receiverEmail == "")
                        mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");
                    else
                        mailMessage.To.Add(receiverEmail);

                    if (mailMessage != null)
                    {
                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{FirstName}",firstName).Replace("{LastName}", lastName).Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{CompanyEmail}", companyEmail).ToString();

                        SendEmail(mailMessage, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Email Notification For Contact To Elioplus could not be sent. MESSAGE ERROR FROM LIB(" + ex + ")");
                }
            }
        }

        public static void MAMContactElioplusRequest(string firstName, string lastName, string companyName, string companyEmail, string phone, string message, string receiverEmail, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string[] addr = companyEmail.Split('@');

                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.MarketplaceRequestMessageEmail.ToString(), lang, session);

                    mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");

                    if (mailMessage != null)
                    {
                        mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");

                        if (receiverEmail == "")
                            mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");
                        else
                        {
                            mailMessage.To.Add("info@elioplus.com");
                            mailMessage.To.Add("ilias.ndreu@gmail.com");
                        }

                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{FirstName}", firstName).Replace("{LastName}", lastName).Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{CompanyEmail}", companyEmail).Replace("{PhoneNumber}", phone).Replace("{Message}", message).ToString();

                        SendEmail(mailMessage, true);
                    }
                    else
                        throw new Exception("No template " + EmailNotificationDesctriptions.MarketplaceRequestMessageEmail + " found!");
                }
                catch (Exception ex)
                {
                    throw new Exception("Email Notification For M&amp;A Marketplace @ Elioplus could not be sent. MESSAGE ERROR FROM LIB(" + ex.Message + ")");
                }
            }
        }

        public static void ContactElioplusToApproveDemoRequest(string companyName, string companyEmail, string demoCompanyName, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string[] addr = companyEmail.Split('@');

                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ContactElioplusToApproveDemoRequestEmail.ToString(), lang, session);

                    mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");
                    
                    if (mailMessage != null)
                    {
                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{DemoCompany}", demoCompanyName);

                        SendEmail(mailMessage, true);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Email Notification For Contact To Elioplus could not be sent. MESSAGE ERROR FROM LIB(" + ex + ")");
                }
            }
        }

        public static void SendNotificationEmailToCompanyForNewInboxMessage(ElioUsers user, List<string> companyEmails, string subject, string message, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewInboxMessage.ToString(), lang, session);
                    
                    if (mailMessage != null)
                    {
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyEmail}", user.Email).Replace("{mailMessage}", message);
                        mailMessage.Subject = subject;

                        SendEmails(mailMessage, companyEmails, (int)EmailNotificationDesctriptions.NewInboxMessage, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Company For New Inbox Message could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToFriends(ElioUsers user, List<string> emails, string message, string subject, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.EmailToFriends.ToString(), lang, session);
                    
                    if (mailMessage != null)
                    {
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", user.CompanyName).Replace("{EmailSubject}", subject).ToString();

                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.EmailToFriends, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Friends could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToCompanyForResentLeads(int companyId, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    ElioUsers userEmails = Sql.GetCompanyEmailAndOfficialEmail(companyId, session);
                    if (userEmails != null)
                    {
                        List<string> emails = new List<string>();
                        emails.Add(userEmails.Email);                        
                       
                        if (!string.IsNullOrEmpty(userEmails.OfficialEmail))
                        {
                            emails.Add(userEmails.OfficialEmail);
                        }

                        MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ResentLeads.ToString(), lang, session);

                        if (mailMessage != null)
                        {
                            SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.ResentLeads, isSubAccount, session, true);
                        }
                      
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Company For Resent Leads could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToNotFullRegisteredUser(ElioUsers user, List<string> emails, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NotFullRegisteredUser.ToString(), lang, session);

                    if (mailMessage != null)
                    {
                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.NotFullRegisteredUser, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Email Notification To Not Full Registered User with ID: {0}, could not be sent", user.Id.ToString()));
                }
            }
        }

        public static void SendActivationEmailToFullRegisteredUser(ElioUsers user, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ActivationAccount.ToString(), lang, session);

                    List<string> emails = new List<string>();

                    emails.Add(user.Email);

                    if (!string.IsNullOrEmpty(user.OfficialEmail))
                    {
                        emails.Add(user.OfficialEmail);
                    }

                    if (mailMessage != null)
                    {
                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.ActivationAccount, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Full Registered User could not be sent");
                }
            }
        }

        public static void SendErrorNotificationEmail(string type, string url, string msg, string stackTrace, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ErrorNotificationEmail.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {                        
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{type}", type).Replace("{url}", url).Replace("{msg}", msg).Replace("{stackTrace}", stackTrace).ToString();

                            SendEmail(mailMessage, true);
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Error could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewFullRegisteredUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewFullRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyType}", user.CompanyType);
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{CompanyName}", user.CompanyName).Replace("{CompanyType}", user.CompanyType).Replace("{Country}", user.Country).Replace("{Address}", user.Address).Replace("{WebSite}", user.WebSite).Replace("{Email}", user.Email).Replace("{Description}", user.Description).Replace("{Overview}", user.Overview).ToString();

                            mailMessage.Body = (!string.IsNullOrEmpty(user.OfficialEmail)) ? mailMessage.Body.Replace("{OfficialEmail}", user.OfficialEmail).ToString() : mailMessage.Body.Replace("{OfficialEmail}", "Has no Official Email");

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Full Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewVendorPartnerFullRegisteredUser(ElioUsers user, string vendorName, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewVendorPartnerFullRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyType}", user.CompanyType);
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{VendorName}", vendorName).Replace("{CompanyName}", user.CompanyName).Replace("{CompanyType}", user.CompanyType).Replace("{Country}", user.Country).Replace("{Address}", user.Address).Replace("{WebSite}", user.WebSite).Replace("{Email}", user.Email).Replace("{Description}", user.Description).Replace("{Overview}", user.Overview).ToString();

                            mailMessage.Body = (!string.IsNullOrEmpty(user.OfficialEmail)) ? mailMessage.Body.Replace("{OfficialEmail}", user.OfficialEmail).ToString() : mailMessage.Body.Replace("{OfficialEmail}", "Has no Official Email");

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Full Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewPRMFullRegisteredUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewPRMFullRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyType}", user.CompanyType);
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{CompanyName}", user.CompanyName).Replace("{CompanyType}", user.CompanyType).Replace("{Country}", user.Country).Replace("{Address}", user.Address).Replace("{WebSite}", user.WebSite).Replace("{Email}", user.Email).Replace("{Description}", user.Description).Replace("{Overview}", user.Overview).ToString();

                            mailMessage.Body = (!string.IsNullOrEmpty(user.OfficialEmail)) ? mailMessage.Body.Replace("{OfficialEmail}", user.OfficialEmail).ToString() : mailMessage.Body.Replace("{OfficialEmail}", "Has no Official Email");

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Full Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewMultiAccountRegisteredUser(ElioUsers user, string supervisorname, int supervisorId, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewMultiAccountRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{supervisor}", supervisorname).Replace("{id}", supervisorId.ToString()).Replace("{CompanyName}", user.CompanyName).Replace("{CompanyType}", Types.Vendors.ToString()).Replace("{Country}", user.Country).Replace("{Address}", user.Address).Replace("{Phone}", user.Phone).Replace("{WebSite}", user.WebSite).Replace("{Email}", user.Email).Replace("{Description}", user.Description).Replace("{Overview}", user.Overview).ToString();

                            mailMessage.Body = (!string.IsNullOrEmpty(user.OfficialEmail)) ? mailMessage.Body.Replace("{OfficialEmail}", user.OfficialEmail).ToString() : mailMessage.Body.Replace("{OfficialEmail}", "Has no Official Email");

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Full Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewSimpleRegisteredUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewSimpleRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{Email}", user.Email).ToString();

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Simple Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewVendorPartnerSimpleRegisteredUser(ElioUsers user, string vendorName, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewVendorPartnerSimpleRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{VendorName}", vendorName).Replace("{Email}", user.Email).ToString();

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Simple Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewPRMSimpleRegisteredUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewPRMSimpleRegisteredUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{Email}", user.Email).ToString();

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Simple Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewSimpleRegisteredThirdPartyToElioUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewSimpleRegisteredThirdPartyToElioUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{Email}", user.Email).Replace("UserId", user.Id.ToString()).ToString();

                            SendEmail(mailMessage, true);
                        }
                        else
                        {
                            throw new Exception("Email Notification For New Simple Registered User could not be send");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendResetPasswordEmail(string newPassword, string email, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.Resetpassword.ToString(), lang, true, ConfigurationManager.AppSettings["EmailResetPasswordTemplateFileName"].ToString(), session);

                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{newPassword}", newPassword).ToString();
                            mailMessage.To.Add(email);

                            SendEmail(mailMessage, true);
                        }
                    }
                    else
                        throw new Exception("No email address was found");
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Reset Password could not be send");
                }
            }
            else
            {
                throw new Exception("You are in Development Mode so the email could not be send");
            }
        }

        public static void ClaimProfileResetPasswordEmail(string newPassword, string email, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.ClaimProfileResetPasswordEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailResetPasswordTemplateFileName"].ToString(), session);

                    bool allowSend = true;   //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.ClaimProfileResetPasswordEmail, false, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body.Replace("{date}", DateTime.Now.ToString()).Replace("{newPassword}", newPassword).ToString();

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Reset Password could not be send");
                }
            }
            else
            {
                throw new Exception("You are in Development Mode so the email could not be send");
            }
        }

        public static void InvitationEmail(string email, string companyName, string url, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.InvitationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailTeamInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = true;  //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.InvitationEmail, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{link}", url).ToString();

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendEmailToCollaboratePartnerForAcceptInvitation(string email, string companyName, string companyType, HttpRequest request, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = null;

                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.AcceptInvitationEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (companyType == Types.Vendors.ToString())
                        {
                            mailMessage = FillMailMessage(EmailNotificationDesctriptions.AcceptInvitationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);

                            if (mailMessage != null)
                            {
                                mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);

                                string partnerPortalLoginUrl = FileHelper.AddToPhysicalRootPath(request) + "/" + Regex.Replace(companyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-login";

                                mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", partnerPortalLoginUrl);
                            }
                        }
                        else
                        {
                            mailMessage = FillMailMessage(EmailNotificationDesctriptions.AcceptRequestEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);

                            if (mailMessage != null)
                            {
                                mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);

                                string url = FileHelper.AddToPhysicalRootPath(request) + ControlLoader.Login;

                                mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", url);
                            }
                        }

                        if (mailMessage != null)
                            SendEmail(mailMessage, email, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendEmailToCollaboratePartnerForRejectInvitation(string email, string companyName, string companyType, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = null;

                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.RejectInvitationEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (companyType == Types.Vendors.ToString())
                            mailMessage = FillMailMessage(EmailNotificationDesctriptions.RejectInvitationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);
                        else
                            mailMessage = FillMailMessage(EmailNotificationDesctriptions.RejectRequestEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);

                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName);
                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void CollaborationInvitationEmail(int userApplicationType, string email, string companyName, string companyLogo, string resellerCompanyName, string url, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = null;

                    if (userApplicationType != Convert.ToInt32(UserApplicationType.Collaboration))
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);
                    }
                    else
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForUserPartners.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);
                    }

                    bool allowSend = true;   //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            if (companyLogo.StartsWith("~/"))
                                companyLogo = companyLogo.Replace("~/", "");

                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            //mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{link}", "<a href=\"" + url + "\"> here </a>").ToString();
                            mailMessage.Body = mailMessage.Body.Replace("{COMPANY_LOGO}", companyLogo).Replace("{CompanyName}", companyName).Replace("{ResellerCompanyName}", resellerCompanyName).Replace("{url}", url).Replace("{Date}", DateTime.Now.ToString());
                            
                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void CollaborationInvitationEmailFromChannelPartner(int userApplicationType, string email, string companyName, string companyLogo, string resellerCompanyName, HttpRequest request, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = null;

                    if (userApplicationType != Convert.ToInt32(UserApplicationType.Collaboration))
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);
                    }
                    else
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailFromChannelPartners.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);
                    }

                    bool allowSend = true;   //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            if (companyLogo.StartsWith("~/"))
                                companyLogo = companyLogo.Replace("~/", "");

                            if (userApplicationType != Convert.ToInt32(UserApplicationType.Collaboration))
                            {
                                mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                                //mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{link}", "<a href=\"" + url + "\"> here </a>").ToString();
                                mailMessage.Body = mailMessage.Body.Replace("{COMPANY_LOGO}", companyLogo).Replace("{CompanyName}", companyName);
                            }
                            else
                            {
                                //mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                                mailMessage.Body = mailMessage.Body.Replace("{COMPANY_LOGO}", companyLogo).Replace("{CompanyName}", resellerCompanyName);
                            }

                            string url = FileHelper.AddToPhysicalRootPath(request) + ControlLoader.Login;

                            mailMessage.Body = mailMessage.Body.Replace("{url}", url);

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void CollaborationInvitationEmailWithUserText(int userApplicationType, string email, string companyName, string url, string subject, string message, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = null;

                    if (userApplicationType == (int)UserApplicationType.Elioplus)
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections.ToString(), lang, true, ConfigurationManager.AppSettings["EmailInvitationsTemplateFileName"].ToString(), session);
                    }
                    else
                    {
                        mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForUserPartners.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);
                    }

                    bool allowSend = true;   //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.CollaborationInvitationEmailForExistingConnections, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            if (subject == string.Empty)
                                mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            else
                                mailMessage.Subject = subject;

                            if (message == string.Empty)
                                //mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{link}", "<a href=\"" + url + "\"> here </a>").ToString();
                                mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", url).ToString();
                            else
                            {
                                mailMessage.Body = message;
                                //mailMessage.Body += @"All you have to do is click {link} and accept the invitation from your Collaboration Tool Dashboard and join the team:
                                //                  Login to your Dashboard to see details!".Replace("{link}", "<a href=\"" + url + "\"> here </a>").ToString();
                            }

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void CollaborationInvitationEmailForNotFullRegisteredUsersWithUserText(int userApplicationType, string email, string companyName, string subject, string message, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForNotFullRegisteredUsers.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = true;  //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.CollaborationInvitationEmailForNotFullRegisteredUsers, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            if (subject == string.Empty)
                                mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            else
                                mailMessage.Subject = subject;

                            if (message == string.Empty)
                                mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName);
                            else
                                mailMessage.Body = message;
                            //email = "vvarvi@gmail.com";
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{Date}", DateTime.Now.ToString());

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void CollaborationInvitationEmail(MailMessage mailMessage, string email, string companyName, string url, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    if (mailMessage != null)
                    {
                        //email = "vvarvi@gmail.com";
                        SendEmail(mailMessage, email, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendCollaborationInvitationEmailForNotFullRegisteredUsers(string companyName, string email, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.CollaborationInvitationEmailForNotFullRegisteredUsers.ToString(), lang, session);

                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.CollaborationInvitationEmailForNotFullRegisteredUsers, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", "https://elioplus.com/login").Replace("{Date}", DateTime.Now.ToString());

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification To Not Full Registered Users could not be sent");
                }
            }
        }
        
        public static void SendNewUploadedFileEmail(string companyName, string fileType, string libraryCategory, List<string> emails, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewUploadedFileEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    if (mailMessage != null)
                    {
                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{FileType}", fileType).Replace("{LibraryCategory}", libraryCategory).Replace("{url}", "https://elioplus.com/login");

                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.NewUploadedFileEmail, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Uploaded File Email could not be send");
                }
            }
        }

        public static void SendNewUploadedFileEmail(string companyName, List<string> emails, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewUploadedFileEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    if (mailMessage != null)
                    {
                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", "https://elioplus.com/login");

                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.NewUploadedFileEmail, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Uploaded File Email could not be send");
                }
            }
        }

        public static void SendNewUploadedCollaborationLibraryFileEmail(string companyName, List<string> emails, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewUploadedCollaborationLibraryFileEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    if (mailMessage != null)
                    {
                        mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", "https://elioplus.com/login");

                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.NewUploadedCollaborationLibraryFileEmail, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Uploaded Library File Email could not be send");
                }
            }
        }

        public static void SendNewDealRegistrationEmail(string email, string companyName, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewDealRegistrationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);
                    
                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.NewDealRegistrationEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", "https://elioplus.com/login");

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendNewDealRegistrationCommentEmail(string email, string dealCompanyName, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewDealRegistrationCommentEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = true;  //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.NewDealRegistrationCommentEmail, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{DealCompanyName}", dealCompanyName);
                            mailMessage.Body = mailMessage.Body.Replace("{DealCompanyName}", dealCompanyName).Replace("{url}", "https://elioplus.com/login");

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendNewDealRegistrationAcceptRejectEmail(string resellerEmail, string vendorCompanyName, string resellerCompanyName, string dealStatus, string dealCompanyName, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewDealRegistrationChangeStatusEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = Sql.HasEmailSettingsAccess(resellerEmail, (int)EmailNotificationDesctriptions.NewDealRegistrationChangeStatusEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{Status}", dealStatus).Replace("{CompanyName}", vendorCompanyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", vendorCompanyName).Replace("{ResellerCompanyName}", resellerCompanyName).Replace("{Status}", dealStatus).Replace("{DealCompanyName}", dealCompanyName).Replace("{url}", "https://elioplus.com/login");

                            SendEmail(mailMessage, resellerEmail, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendNewDealRegistrationWonLostEmail(string vendorEmail, string resellerCompanyName, string dealResultStatus, string dealCompanyName, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewDealRegistrationWonLostEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = Sql.HasEmailSettingsAccess(vendorEmail, (int)EmailNotificationDesctriptions.NewDealRegistrationWonLostEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", resellerCompanyName).Replace("{Status}", dealResultStatus);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", resellerCompanyName).Replace("{Status}", dealResultStatus).Replace("{DealCompanyName}", dealCompanyName).Replace("{url}", "https://elioplus.com/login");

                            if (dealResultStatus == DealResultStatus.Lost.ToString())
                                mailMessage.Subject = mailMessage.Subject.Replace("Great news, ", "");

                            SendEmail(mailMessage, vendorEmail, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendNewLeadDistributionEmail(string email, string companyName, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewLeadDistributionEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.NewLeadDistributionEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", "https://elioplus.com/login");

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendNewLeadDistributionWonLostEmail(string email, string companyName, string leadResultStatus, string leadCompanyName, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewLeadDistributionWonLostEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.NewLeadDistributionWonLostEmail, isSubAccount, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName).Replace("{Status}", leadResultStatus);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{Status}", leadResultStatus).Replace("{LeadCompanyName}", leadCompanyName).Replace("{url}", "https://elioplus.com/login");

                            if(leadResultStatus == DealResultStatus.Lost.ToString())
                                mailMessage.Subject = mailMessage.Subject.Replace("Great news, ", "");

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        public static void SendPartner2PartnerDealRegistrationEmail(string companyName, List<string> emails, string opportunityName, string product, string location, string dealValue, string description, bool isSubAccount, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewPartner2PartnerDealRegistrationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    if (mailMessage != null)
                    {
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName)
                                                    .Replace("{OpportunityName}", opportunityName)
                                                    .Replace("{Product}", product)
                                                    .Replace("{Location}", location)
                                                    .Replace("{DealValue}", dealValue)
                                                    .Replace("{Description}", description)
                                                    .Replace("{url}", "https://elioplus.com/login");

                        SendEmails(mailMessage, emails, (int)EmailNotificationDesctriptions.NewPartner2PartnerDealRegistrationEmail, isSubAccount, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Uploaded File Email could not be send");
                }
            }
        }

        public static void SendPartner2PartnerChangeStatusDealRegistrationEmail(string companyName, string email, string opportunityName, string status, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewPartner2PartnerChangeStatusDealRegistrationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    if (mailMessage != null)
                    {
                        mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName)
                                                    .Replace("{OpportunityName}", opportunityName)                                                    
                                                    .Replace("{url}", "https://elioplus.com/login");

                        if (status == DealStatus.Closed.ToString())
                            mailMessage.Body = mailMessage.Body.Replace("{Status}", status).Replace("{ProStatus}", DealStatus.Open.ToString());
                        else if (status == DealStatus.Open.ToString())
                            mailMessage.Body.Replace("{Status}", status + " again").Replace("{ProStatus}", DealStatus.Closed.ToString()).Replace("more information.", "more information and express your interest again");

                        SendEmail(mailMessage, email, (int)EmailNotificationDesctriptions.NewPartner2PartnerChangeStatusDealRegistrationEmail, session, true);
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Uploaded File Email could not be send");
                }
            }
        }

        public static void SendDeletePartnerNotificationEmail(string email, string companyName, HttpRequest request, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.DeletePartnerNotificationEmail.ToString(), lang, true, ConfigurationManager.AppSettings["EmailCollaborationInvitationsTemplateFileName"].ToString(), session);

                    bool allowSend = true;  //Sql.HasEmailSettingsAccess(email, (int)EmailNotificationDesctriptions.InvitationEmail, session);
                    if (allowSend)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Subject = mailMessage.Subject.Replace("{CompanyName}", companyName);
                            mailMessage.Body = mailMessage.Body.Replace("{CompanyName}", companyName).Replace("{url}", FileHelper.AddToPhysicalRootPath(request) + ControlLoader.Login).ToString();

                            SendEmail(mailMessage, email, true);
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For Invitation could not be send");
                }
            }
        }

        #endregion

        #region Community emails

        public static void SendCommunityNotificationEmail(ElioUsers user, int postId, EmailNotificationDesctriptions notificationEmailDescription, CommunityEmailNotifications notificationEmailCase, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(notificationEmailDescription.ToString(), lang, session);

                    if (mailMessage != null)
                    {
                        ElioUsers userEmail = new ElioUsers();
                        List<ElioUsers> usersEmail = new List<ElioUsers>();

                        switch (notificationEmailCase)
                        {
                            case CommunityEmailNotifications.UpvoteUserPost:
                            case CommunityEmailNotifications.CommentUserPost:

                                userEmail = Sql.GetCommentedOrUpvotedPostUserEmail(postId, Convert.ToInt32(notificationEmailCase), session);

                                break;

                            case CommunityEmailNotifications.FollowUserPost:

                                userEmail = Sql.GetFollowingUserEmail(user.Id, Convert.ToInt32(notificationEmailCase), session);

                                break;

                            case CommunityEmailNotifications.CreateNewPost:

                                usersEmail = Sql.GetFollowersEmailsForNewPostCreated(Convert.ToInt32(CommunityEmailNotifications.CreateNewPost), user.Id, session).ToList();

                                break;
                        }

                        if (userEmail != null)
                        {
                            if (!string.IsNullOrEmpty(userEmail.Email))
                            {
                                mailMessage.To.Add(userEmail.Email);

                                mailMessage.Body = mailMessage.Body.Replace("{Community name of the user}", (user.CommunityStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.LastName + " " + user.FirstName : (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.CompanyName : user.Username);
                                SendEmail(mailMessage, true);
                            }
                            else if (usersEmail.Count > 0)
                            {
                                foreach (ElioUsers usrEmail in usersEmail)
                                {
                                    mailMessage.To.Add(usrEmail.Email);

                                    mailMessage.Body = mailMessage.Body.Replace("{Community name of the user}", (user.CommunityStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.LastName + " " + user.FirstName : (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.CompanyName : user.Username);
                                    SendEmail(mailMessage, true);
                                }
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception(string.Format("Email Notification For Community and case {0}, could not be send", notificationEmailCase));
                }
            }
        }

        public static void SendNotificationEmailForNewSimpleRegisteredCommunityUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewSimpleRegisteredCommunityUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body
                                    .Replace("{date}", DateTime.Now.ToString())
                                    .Replace("{Email}", user.Email).ToString();

                            SendEmail(mailMessage, true);
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Simple Registered Community User could not be send");
                }
            }
        }

        public static void SendNotificationEmailForNewFullRegisteredCommunityUser(ElioUsers user, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = FillMailMessage(EmailNotificationDesctriptions.NewFullRegisteredCommunityUser.ToString(), lang, session);

                    mailMessage = GlobalMethods.GetReceiversEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        if (mailMessage != null)
                        {
                            mailMessage.Body = mailMessage.Body
                                                .Replace("{date}", DateTime.Now.ToString())
                                                .Replace("{LastName}", user.LastName)
                                                .Replace("{FirstName}", user.FirstName)
                                                .Replace("{Position}", user.Position)
                                                .Replace("{Linkedin}", user.LinkedInUrl)
                                                .Replace("{Twitter}", user.TwitterUrl)
                                                .Replace("{Summary}", user.CommunitySummaryText);

                            SendEmail(mailMessage, true);
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification For New Full Registered Community User could not be send");
                }
            }
        }

        #endregion
    }
}