using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxReply : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && HttpContext.Current.Request.Url.AbsolutePath != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        FixPage();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        # region Methods

        private void UpdateStrings()
        {
            LblTitle.Text = "Reply";
            LblTo.Text = "To:";
            LblSubject.Text = "Subject:";
            BtnSend.Text = "Send";
            BtnCancel.Text = "Discard";
            LblMessage.Text = "Message:";
            LblOriginalMessage.Text = "Original Message:";
        }

        private void FixPage()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
            string[] pathArray = path.Split('/');

            List<string> pathElements = new List<string>();

            foreach (string item in pathArray)
            {
                if (item != string.Empty)
                {
                    pathElements.Add(item);
                }
            }

            int number;
            int messageId = (int.TryParse(pathElements[4], out number)) ? number : 0;

            if (messageId > 0)
            {
                ElioUsersMessages message = Sql.GetUsersMessageById(messageId, session);

                if (message != null)
                {
                    ElioUsers recipient = Sql.GetUserById((message.SenderUserId == vSession.User.Id) ? message.ReceiverUserId : message.SenderUserId, session);
                    if (recipient != null)
                        TbxRecipient.Text = recipient.CompanyName;


                    TbxSubject.Text = message.Subject;
                    TbxOriginalMessage.Text = message.Message;
                }
            }
        }

        private void ClearMessageData()
        {
            try
            {
                TbxSubject.Text = string.Empty;
                TbxReplyContent.Text = string.Empty;
                divError.Visible = false;
                divInfo.Visible = false;
                divSuccess.Visible = false;
                divWarning.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }        

        # endregion

        #region Buttons

        protected void BtnDiscard_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearMessageData();
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "messages/inbox"), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSendMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divError.Visible = false;
                divSuccess.Visible = false;
                divWarning.Visible = false;
                divInfo.Visible = false;
                                
                if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                {
                    LblInfo.Text = "Info! ";
                    LblInfoContent.Text = "You have to complete your registration to be able to send a message.";
                    divInfo.Visible = true;
                    divInfo.Focus();
                    return;
                }                                
                
                if (string.IsNullOrEmpty(TbxSubject.Text))
                {
                    LblError.Text = "Error! ";
                    LblErrorContent.Text = "You have to enter a subject for your message.";
                    divError.Visible = true;
                    divError.Focus();
                    return;
                }

                if (string.IsNullOrEmpty(TbxReplyContent.Text))
                {
                    LblError.Text = "Error! ";
                    LblErrorContent.Text = "Your message must have a content.";
                    divError.Visible = true;
                    divError.Focus();
                    return;
                }

                string recipientName = TbxRecipient.Text;
                
                ElioUsersMessages message = new ElioUsersMessages();

                ElioUserPacketStatus senderPacketStatusFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

                ElioUsers recipientDetails = Sql.GetCompanynameAndIdAndEmailAndOfficialEmailByName(recipientName, session);

                if (recipientDetails != null)
                {
                    if (senderPacketStatusFeatures != null)
                    {
                        if (senderPacketStatusFeatures.AvailableMessagesCount <= 0)
                        {
                            LblWarning.Text = "Warning! ";
                            LblWarningContent.Text = "You have no available messages left on this pricing plan.";
                            divWarning.Visible = true;
                            divWarning.Focus();
                            return;
                        }
                    }
                    else
                    {
                        LblWarning.Text = "Warning! ";
                        LblWarningContent.Text = "You have no available messages left. Please contact with Elioplus support team.";
                        divWarning.Visible = true;
                        divWarning.Focus();
                        return;
                    }

                    try
                    {
                        session.BeginTransaction();

                        message = GlobalDBMethods.InsertCompanyMessage(vSession.User.Id, vSession.User.Email, recipientDetails.Id, recipientDetails.Email, recipientDetails.OfficialEmail, TbxSubject.Text, TbxReplyContent.Text, session);

                        List<string> emails = new List<string>();

                        emails.Add(recipientDetails.Email);

                        if (!string.IsNullOrEmpty(recipientDetails.OfficialEmail))
                        {
                            emails.Add(recipientDetails.OfficialEmail);
                        }

                        //EmailNotificationsLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxSubject.Text, session);
                        EmailSenderLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxSubject.Text, TbxReplyContent.Text, false, vSession.Lang, session);

                        ClearMessageData();

                        LblSuccess.Text = "Congratulations! ";
                        LblSuccessContent.Text = "Your message was sent successfully.";
                        divSuccess.Visible = true;
                        divSuccess.Focus();

                        if (recipientDetails.Id != vSession.User.Id)
                        {
                            GlobalDBMethods.FixUserEmailAndPacketStatusFeatutes(message, senderPacketStatusFeatures, session);
                        }

                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();

                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                        LblError.Text = "Error! ";
                        LblErrorContent.Text = "Something happened and the message could not be sent, please try again or contact us.";
                        divError.Visible = true;
                        divError.Focus();
                        return;
                    }
                }
                else
                {
                    LblError.Text = "Error! ";
                    LblErrorContent.Text = "It seems that this company name does not exist in our database, please try again.";
                    divError.Visible = true;
                    divError.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        # endregion
    }
}