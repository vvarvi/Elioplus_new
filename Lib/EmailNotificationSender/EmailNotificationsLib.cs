using System;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Objects;
using System.Net.Mail;
using System.Configuration;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using System.Xml;
using System.Web;

namespace WdS.ElioPlus.Lib.EmailNotificationSender
{
    public class EmailNotificationsLib
    {
        public static bool GetEmail(string emailName, ref string subject, ref string body)
        {
            return GetEmail(emailName, ref subject, ref body, false);
        }

        public static bool GetEmail(string emailName, ref string subject, ref string body, bool isHtmlFormated)
        {
            bool emailFound = false;
            string lang = "en";

            XmlDocument xmlEmailsDoc = new XmlDocument();
            xmlEmailsDoc.Load(HttpContext.Current.Server.MapPath("Lib/EmailNotificationSender/EmailXml/Emails_" + lang + ".xml"));
            XmlNode xmlEmail = xmlEmailsDoc.SelectSingleNode(@"descendant::email[@name=""" + emailName + @"""]");

            if (xmlEmail != null)
            {
                emailFound = true;
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
                        body += xmlFooter.InnerXml;
                    else
                        body += xmlFooter.InnerText;
                }
            }

            return emailFound;
        }

        #region LandingPages emails

        public static void SaveUsersEmailFromLandingPages(string email, string landingPage, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.ReplyTo = new MailAddress(email);

                    GlobalMethods.GetEmailsFromConfig(mailMessage, "NewInboxElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.InboxEmail.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{LandingPage}", landingPage).Replace("{CompanyEmail}", email);
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            //EmailProviderSettings.SendEmail(mailMessage, email);

                            mailMessage.IsBodyHtml = true;
                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email (" + email + ") from Landing Page (" + landingPage + ") could not be found");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email (" + email + ") from Landing Page (" + landingPage + ") could not be sent");
                }
            }
        }

        //public static void SaveUsersEmailFromLandingPages(string email, string landingPage, DBSession session)
        //{
        //    if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
        //    {
        //        try
        //        {
        //            MailMessage mailMessage = new MailMessage();
        //            mailMessage.From = new MailAddress(email, landingPage);
                    
        //            GlobalMethods.GetEmailsFromConfig(mailMessage, "NewInboxElioplusEmails");

        //            if (mailMessage.To.Count > 0)
        //            {
        //                ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.InboxEmail.ToString(), session);
        //                if (notifemail != null)
        //                {
        //                    #region Template Email

        //                    mailMessage.Body = notifemail.HtmlEmailBody.Replace("{LandingPage}", landingPage).Replace("{CompanyEmail}", email);
        //                    mailMessage.Subject = notifemail.Subject;

        //                    #endregion                           

        //                    mailMessage.IsBodyHtml = true;

        //                    SmtpClient smtpClient = new SmtpClient("smtpout.asia.secureserver.net", 80);
        //                    smtpClient.EnableSsl = false;
        //                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["Email"].ToString(), ConfigurationManager.AppSettings["Password"].ToString());
        //                    smtpClient.Send(mailMessage);
        //                }
        //                else
        //                {
        //                    throw new Exception("Email (" + email + ") from Landing Page (" + landingPage + ") could not be found");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw new Exception("Email (" + email + ") from Landing Page (" + landingPage + ") could not be sent");
        //        }
        //    }
        //}

        #endregion

        #region Elioplus emails

        public static void SendNotificationEmailToCompanyForNewReview(ElioUsers user, string companyEmail, string lang, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(companyEmail);

                    string review = GlobalMethods.FindReviewDescriptionByUserType(user.CompanyType, lang);

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewReview.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email

                        if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                        {
                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{CompanyName}", user.CompanyName).Replace("{Subject}", notifemail.Subject).Replace("{review}", review).ToString();
                        }
                        else
                        {
                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{CompanyName}", user.Username + " (username)").Replace("company"," ").Replace("{Subject}", notifemail.Subject).Replace("{review}", "program").ToString();
                        }

                        mailMessage.Subject = notifemail.Subject;
                        
                        #endregion

                        #region Email without Template

                        //mailMessage.Subject = "Elioplus new review Notification";

                        //mailMessage.Body = user.CompanyName + " company wrote a review about your " + review + ". <br/><br/>";
                        //mailMessage.Body += "Login to your account and view it now here: <a href=\"http://elioplus.com\"><span>http://elioplus.com/Search.aspx</span></a><br/><br/>";
                        //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                        //mailMessage.Body += "The Elioplus team<br/><br/>";

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about New Review could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void ContactElioplus(string companyName, string companyEmail, string subject, string message, string companyPhone, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    string[] addr = companyEmail.Split('@');
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.Sender = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), companyName + " - <" + addr[0] + ">");
                    mailMessage.ReplyTo = new MailAddress(companyEmail);
                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.ContactElioplusMessage.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{EmailSubject}", subject).Replace("{Phone}", companyPhone).Replace("{Message}", message).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            #region Email without Template

                            //mailMessage.Subject = subject;

                            //mailMessage.Body = companyName + " company send you an email at " + DateTime.Now + " . <br/><br/>";
                            //mailMessage.Body += "<b>Company Phone: </b>" + companyPhone + " <br/><br/>";
                            //mailMessage.Body += message;

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about Contact with Elioplus could not be found");
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

        //public static void ContactElioplus(string companyName, string companyEmail, string subject, string message, string companyPhone, DBSession session)
        //{
        //    if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
        //    {
        //        try
        //        {
        //            MailMessage mailMessage = new MailMessage();
        //            mailMessage.From = new MailAddress(companyEmail,"Contact Us - Email");

        //            GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");
                                        
        //            if (mailMessage.To.Count > 0)
        //            {
        //                ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.ContactElioplusMessage.ToString(), session);
        //                if (notifemail != null)
        //                {
        //                    #region Template Email

        //                    mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{CompanyName}", companyName).Replace("{date}", DateTime.Now.ToString()).Replace("{EmailSubject}", subject).Replace("{Phone}", companyPhone).Replace("{Message}", message).ToString();
        //                    mailMessage.Subject = notifemail.Subject;

        //                    #endregion

        //                    #region Email without Template

        //                    //mailMessage.Subject = subject;

        //                    //mailMessage.Body = companyName + " company send you an email at " + DateTime.Now + " . <br/><br/>";
        //                    //mailMessage.Body += "<b>Company Phone: </b>" + companyPhone + " <br/><br/>";
        //                    //mailMessage.Body += message;

        //                    #endregion

        //                    mailMessage.IsBodyHtml = true;

        //                    SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
        //                    smtpClient.EnableSsl = true;
        //                    smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
        //                    smtpClient.Send(mailMessage);
        //                }
        //                else
        //                {
        //                    throw new Exception("Email Notification about Contact with Elioplus could not be found");
        //                }
        //            }
        //            else
        //            {
        //                throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            throw new Exception("Email Notification could not be sent");
        //        }
        //    }
        //}

        public static void SendNotificationEmailToCompanyForNewInboxMessage(ElioUsers user, List<string> companyEmails, string subject, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");

                    foreach (string email in companyEmails)
                    {
                        mailMessage.To.Add(email);
                    }

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewInboxMessage.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email

                        mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{CompanyName}", user.CompanyName).Replace("{EmailSubject}", subject);
                        mailMessage.Subject = notifemail.Subject;

                        #endregion

                        #region Email without Template

                        //mailMessage.Subject = "Elioplus Inbox New Message Notification";

                        //mailMessage.Body = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.CompanyName : "Elioplus ";
                        //mailMessage.Body += " company send you an email to your inbox. <br/><br/>";
                        //mailMessage.Body += "<b>Subject: </b>" + subject + "<br/><br/>";
                        //mailMessage.Body += "View it now here: <a href=\"http://elioplus.com\"><span>http://elioplus.com</span></a><br/><br/>";
                        //mailMessage.Body += "Under the Messages tab go to your Inbox, see your new message and get in touch with the respective company directly <br/><br/>";
                        //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                        //mailMessage.Body += "The Elioplus team<br/><br/>";

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about New Inbox Message could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToFriends(ElioUsers user, List<string> emails, string message, string subject, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");

                    foreach (string email in emails)
                    {
                        if (email != string.Empty)
                        {
                            mailMessage.To.Add(email);
                        }
                    }

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.EmailToFriends.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email

                        mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{CompanyName}", user.CompanyName).Replace("{EmailSubject}", subject).ToString();
                        mailMessage.Subject = notifemail.Subject;

                        #endregion

                        #region Email without Template

                        //mailMessage.Subject = "Elioplus New message Notification";

                        //mailMessage.Body = user.CompanyName + " company send you and email. <br/><br/>";
                        //mailMessage.Body += "<b> Message:</b> " + message + "<br/><br/>";
                        //mailMessage.Body += "Please login to <a href=\"http://elioplus.com\"><span>http://elioplus.com</span></a> to learn more details about. <br/><br/>";
                        //mailMessage.Body += "If you don't have account sign up <a href=\"http://elioplus.com/SignUp.aspx\"><span></span>now</a>. <br/><br/>";
                        //mailMessage.Body += "It's free and takes less than 30 seconds. <br/><br/>";
                        //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                        //mailMessage.Body += "The Elioplus team<br/><br/>";

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about Friends could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToCompanyForResentLeads(int companyId, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    ElioUsers userEmails = Sql.GetCompanyEmailAndOfficialEmail(companyId, session);
                    if (userEmails != null)
                    {
                        MailMessage mailMessage = new MailMessage();
                        mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                        mailMessage.To.Add(userEmails.Email);

                        if (!string.IsNullOrEmpty(userEmails.OfficialEmail))
                        {
                            mailMessage.To.Add(userEmails.OfficialEmail);
                        }

                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.ResentLeads.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            #region Email without Email

                            //mailMessage.Subject = "You have a new partner lead on Elioplus";

                            //mailMessage.Body = "Hi, <br/><br/>";
                            //mailMessage.Body += "You have a new prospect lead on your dashboard. <br/><br/>";
                            //mailMessage.Body += "To view it, go to our website, login and go to your profile. <br/><br/>";
                            //mailMessage.Body += "Under the Recent Leads tab you'll see the details and get in touch with the respective company directly <br/><br/>";

                            //mailMessage.Body += "View it now here: <a href=\"http://elioplus.com\"><span>http://elioplus.com</span></a><br/><br/>";
                            //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                            //mailMessage.Body += "The Elioplus team<br/><br/>";

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about Resend Leads could not be found");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailToNotFullRegisteredUser(string companyName, string email, string officialEmail, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(email);

                    if (!string.IsNullOrEmpty(officialEmail) && officialEmail != "-")
                    {
                        mailMessage.To.Add(officialEmail);
                    }

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NotFullRegisteredUser.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email

                        mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).ToString();
                        mailMessage.Subject = notifemail.Subject;

                        #endregion

                        #region Email without Teamplate

                        //mailMessage.Subject = "Now it's time to complete your profile";

                        //mailMessage.Body = "Hi there, <br/><br/>";
                        //mailMessage.Body += "We noticed that you haven't completed your public profile yet. <br/><br/>";
                        //mailMessage.Body += "Why you should do it now? <br/><br/>";
                        //mailMessage.Body += "We are attracting more and more users every day on our platform. If you complete your company's public profile not only you'll create more awareness  and exposure but also you'll be able to receive new leads on your dashboard. <br/><br/>";
                        //mailMessage.Body += "We are creating an interactive and transparent platform to help you connect with business partners and we'll keep adding features in your dashboard to help you achieve it. <br/><br/>";
                        //mailMessage.Body += "We'll keep all our features free and unlimited during our Beta so you'll better hurry up. <br/><br/>";
                        //mailMessage.Body += "Login and choose the option Register from the drop down on the top of the right corner, it will take you a couple of minutes to complete the registration. <br/><br/>";
                        //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                        //mailMessage.Body += "The Elioplus team<br/><br/>";
                        //mailMessage.Body += "W: http://elioplus.com<br/><br/>";
                        //mailMessage.Body += "E: <a href=\"mailto:info@elioplus.com?subject=Support\"><span class=\"email\">info@elioplus.com</span></a><br/><br/>";
                        //mailMessage.Body += "T: https://twitter.com/elioplus<br/><br/>";
                        //mailMessage.Body += "A:  https://angel.co/elioplus<br/><br/>";

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about Not Full Registered Users could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification about Not Full Registered Users could not be sent");
                }
            }
        }

        public static void SendActivationEmailToFullRegisteredUser(ElioUsers user, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(user.Email);

                    if (!string.IsNullOrEmpty(user.OfficialEmail))
                    {
                        mailMessage.To.Add(user.OfficialEmail);

                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.ActivationAccount.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            #region Email without Template

                            //mailMessage.Subject = "Your account on Elioplus is active";

                            //mailMessage.Body = "Hi, <br/><br/>";
                            //mailMessage.Body += "Thanks for joining our platform! <br/><br/>";
                            //mailMessage.Body += "We send you some quick notes to help you get started and make better use of our platform. <br/><br/>";
                            //mailMessage.Body += "If you haven't already created your public profile login and complete the form using the Register option from the drop down on the top right corner. This will give you the opportunity to expose your offering and also receive new leads.<br/><br/>";
                            //mailMessage.Body += "From your dashboard you'll be able to view statistics about your profile views, the incoming messages from other users and also your new leads and get in touch with them directly.<br/><br/>";
                            //mailMessage.Body += "We love feedback so if you have any suggestion or inquiry just reply to this message or use the contact form on our website.<br/><br/>";
                            //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                            //mailMessage.Body += "The Elioplus team<br/><br/>";
                            //mailMessage.Body += "W: http://elioplus.com<br/><br/>";
                            //mailMessage.Body += "E: <a href=\"mailto:info@elioplus.com?subject=Support\"><span class=\"email\">info@elioplus.com</span></a><br/><br/>";
                            //mailMessage.Body += "T: https://twitter.com/elioplus<br/><br/>";
                            //mailMessage.Body += "A:  https://angel.co/elioplus<br/><br/>";

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about Full Registered User could not be found");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendErrorNotificationEmail(string type, string url, string msg, string stackTrace, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["SupportEmail"].ToString(), "Support Elioplus");

                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.ErrorNotificationEmail.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{date}", DateTime.Now.ToString()).Replace("{type}", type).Replace("{url}", url).Replace("{msg}", msg).Replace("{stackTrace}", stackTrace).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            #region Email without Template

                            //mailMessage.Subject = "Your account on Elioplus is active";

                            //mailMessage.Body = "Hi, <br/><br/>";
                            //mailMessage.Body += "Thanks for joining our platform! <br/><br/>";
                            //mailMessage.Body += "We send you some quick notes to help you get started and make better use of our platform. <br/><br/>";
                            //mailMessage.Body += "If you haven't already created your public profile login and complete the form using the Register option from the drop down on the top right corner. This will give you the opportunity to expose your offering and also receive new leads.<br/><br/>";
                            //mailMessage.Body += "From your dashboard you'll be able to view statistics about your profile views, the incoming messages from other users and also your new leads and get in touch with them directly.<br/><br/>";
                            //mailMessage.Body += "We love feedback so if you have any suggestion or inquiry just reply to this message or use the contact form on our website.<br/><br/>";
                            //mailMessage.Body += "Best regards,<br/><br/><br/><br/>";
                            //mailMessage.Body += "The Elioplus team<br/><br/>";
                            //mailMessage.Body += "W: http://elioplus.com<br/><br/>";
                            //mailMessage.Body += "E: <a href=\"mailto:info@elioplus.com?subject=Support\"><span class=\"email\">info@elioplus.com</span></a><br/><br/>";
                            //mailMessage.Body += "T: https://twitter.com/elioplus<br/><br/>";
                            //mailMessage.Body += "A:  https://angel.co/elioplus<br/><br/>";

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about Error could not be found");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification about Error could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewFullRegisteredUser(ElioUsers user, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    
                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewFullRegisteredUser.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Teamplate Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{date}", DateTime.Now.ToString()).Replace("{CompanyName}", user.CompanyName).Replace("{CompanyType}", user.CompanyType).Replace("{Country}", user.Country).Replace("{Address}", user.Address).Replace("{Phone}", user.Phone).Replace("{WebSite}", user.WebSite).Replace("{Email}", user.Email).Replace("{Description}", user.Description).Replace("{Overview}", user.Overview).ToString();

                            if (!string.IsNullOrEmpty(user.OfficialEmail))
                            {
                                mailMessage.Body = mailMessage.Body.Replace("{OfficialEmail}", user.OfficialEmail).ToString();
                            }

                            mailMessage.Subject = notifemail.Subject + " (" + user.CompanyType + ")";

                            #endregion

                            #region Email without Template

                            //mailMessage.Subject = "New Company Full Registration";

                            //mailMessage.Body = "A new company was registered at " + DateTime.Now + " <br/><br/>";
                            //mailMessage.Body += "Company details <br/><br/>";
                            //mailMessage.Body += "<b>Company Name:</b> " + user.CompanyName + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Type:</b> " + user.CompanyType + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Country:</b> " + user.Country + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Address:</b> " + user.Address + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Phone:</b> " + user.Phone + " <br/><br/>";
                            //mailMessage.Body += "<b>Company WebSite:</b> " + user.WebSite + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Email:</b> " + user.Email + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Description:</b> " + user.Description + " <br/><br/>";
                            //mailMessage.Body += "<b>Company Overview:</b> " + user.Overview + " <br/><br/>";

                            //if (!string.IsNullOrEmpty(user.OfficialEmail))
                            //{
                            //    mailMessage.Body += "<b>Company Official Email:</b> " + user.OfficialEmail;
                            //}

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about New Full Registered User could not be found");
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

        public static void SendNotificationEmailForNewSimpleRegisteredUser(ElioUsers user, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    
                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewSimpleRegisteredUser.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{date}", DateTime.Now.ToString()).Replace("{Email}", user.Email).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about New Simple Registered User could not be found");
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

        public static void SendResetPasswordEmail(string newPassword, string email, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(email);

                    //string subject = string.Empty;
                    //string body = string.Empty;
                    //bool emailFound = GetEmail(EmailNotificationDesctriptions.Resetpassword.ToString(), ref subject, ref body, true);
                    //if (emailFound)
                    //{
                    //mailMessage.Body = body;
                    //mailMessage.Subject = subject;

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.Resetpassword.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email
                        //mailMessage.Body = mailMessage.Body.Replace("{Subject}", mailMessage.Subject).Replace("{date}", DateTime.Now.ToString()).Replace("{newPassword}", newPassword).ToString();

                        mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{date}", DateTime.Now.ToString()).Replace("{newPassword}", newPassword).ToString();
                        mailMessage.Subject = notifemail.Subject;

                        #endregion

                        #region Email without Template

                        //mailMessage.Subject = "Reset your password";

                        //mailMessage.Body = "You request a new password at " + DateTime.Now + " to log in to your elioplus account. <br/><br/>";
                        //mailMessage.Body += "Your new password is <b>" + newPassword + "</b>. <br/><br/>";
                        //mailMessage.Body += "We recommend to change it when you log in for your safety. <br/><br/>";
                        //mailMessage.Body += "Click <a href=\"http://www.elioplus.com\" target=\"_blank\"><span>here</span></a> to log in. <br/><br/>";
                        //mailMessage.Body += "For any question please contact us at <a href=\"mailto:info@elioplus.com?subject=Support\"><span class=\"email\">info@elioplus.com</span></a><br/><br/>";
                        //mailMessage.Body += "Thank you by Elio Plus support team. ";

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about Reset Password could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent to email: " + email);
                }
            }
            else
            {
                throw new Exception("You are in Development Mode so the email could not be send");
            }
        }

        public static void InvitationEmail(string email, string companyName, string url, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    mailMessage.To.Add(email);

                    ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.InvitationEmail.ToString(), session);
                    if (notifemail != null)
                    {
                        #region Template Email

                        mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).Replace("{CompanyName}", companyName).Replace("{link}", url).ToString();
                        mailMessage.Subject = notifemail.Subject.Replace("{CompanyName}", companyName);

                        #endregion

                        mailMessage.IsBodyHtml = true;

                        SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                        smtpClient.Send(mailMessage);
                    }
                    else
                    {
                        throw new Exception("Email Notification about Reset Password could not be found");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent to email: " + email);
                }
            }
        }

        #endregion

        #region Community emails

        public static void SendCommunityNotificationEmail(int userId, int emailNotificationCase, DBSession session)
        {
            
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");

                    List<string> emails = new List<string>();

                    switch (emailNotificationCase)
                    {
                        case 1:
                            emails.Add(Sql.GetCommentedOrUpvotedPostUserEmail(Convert.ToInt32(CommunityEmailNotifications.UpvoteUserPost), userId, session).Email);

                            break;

                        case 2:
                            emails.Add(Sql.GetCommentedOrUpvotedPostUserEmail(Convert.ToInt32(CommunityEmailNotifications.CommentUserPost), userId, session).Email);
                            emails = Sql.GetFollowersEmailsForNewPostCreated(Convert.ToInt32(CommunityEmailNotifications.CreateNewPost), userId, session);

                            break;

                        case 3:
                            emails = Sql.GetFollowersEmails(Convert.ToInt32(CommunityEmailNotifications.FollowUserPost), userId, session);

                            break;

                        case 4:
                            emails = Sql.GetFollowersEmailsForNewPostCreated(Convert.ToInt32(CommunityEmailNotifications.CreateNewPost), userId, session);

                            break;
                    } 

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.CommunityCreateNewPost.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody.Replace("{Subject}", notifemail.Subject).ToString();
                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about upvoted post could not be found");
                        }
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification could not be sent");
                }
            }
        }

        public static void SendNotificationEmailForNewSimpleRegisteredCommunityUser(ElioUsers user, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    
                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewSimpleRegisteredCommunityUser.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Template Email

                            mailMessage.Body = notifemail.HtmlEmailBody

                                    .Replace("{Subject}", notifemail.Subject)
                                    .Replace("{date}", DateTime.Now.ToString())
                                    .Replace("{Email}", user.Email).ToString();

                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about New Simple Registered Community User could not be found");
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

        public static void SendNotificationEmailForNewFullRegisteredCommunityUser(ElioUsers user, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true")
            {
                try
                {
                    MailMessage mailMessage = new MailMessage();
                    mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["Email"].ToString(), "Elioplus");
                    
                    GlobalMethods.GetEmailsFromConfig(mailMessage, "ElioplusEmails");

                    if (mailMessage.To.Count > 0)
                    {
                        ElioUserEmailNotifications notifemail = Sql.GetEmailNotificationByDescription(EmailNotificationDesctriptions.NewFullRegisteredCommunityUser.ToString(), session);
                        if (notifemail != null)
                        {
                            #region Teamplate Email

                            mailMessage.Body = notifemail.HtmlEmailBody

                                                .Replace("{Subject}", notifemail.Subject)
                                                .Replace("{date}", DateTime.Now.ToString())
                                                .Replace("{LastName}", user.LastName)
                                                .Replace("{FirstName}", user.FirstName)
                                                .Replace("{Position}", user.Position)
                                                .Replace("{Linkedin}", user.LinkedInUrl)
                                                .Replace("{Twitter}", user.TwitterUrl)
                                                .Replace("{Summary}", user.CommunitySummaryText);

                            mailMessage.Subject = notifemail.Subject;

                            #endregion

                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient("email-smtp.us-west-2.amazonaws.com", 587);
                            smtpClient.EnableSsl = true;
                            smtpClient.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["SMTPUSER"].ToString(), ConfigurationManager.AppSettings["SMTPPASS"].ToString());
                            smtpClient.Send(mailMessage);
                        }
                        else
                        {
                            throw new Exception("Email Notification about New Full Registered Community User could not be found");
                        }
                    }
                    else
                    {
                        throw new Exception("MailMessage.To.Add(Address) was empty. Check web config");
                    }
                }
                catch (Exception)
                {
                    throw new Exception("Email Notification for new community registered user could not be sent");
                }
            }
        }
        
        #endregion
    }
}