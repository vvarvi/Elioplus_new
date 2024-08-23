using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using Telerik.Web.UI;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls
{
    public partial class InboxCompose : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //RacbxRecipient.Filter = (RadAutoCompleteFilter)Enum.Parse(typeof(RadAutoCompleteFilter), RadAutoCompleteFilter.StartsWith.ToString(), true);
                //RacbxRecipient.TextSettings.SelectionMode = (RadAutoCompleteSelectionMode)Enum.Parse(typeof(RadAutoCompleteSelectionMode), RadAutoCompleteSelectionMode.Multiple.ToString(), true);
                //RacbxRecipient.Delimiter = ";";
                //RacbxRecipient.DropDownPosition = (RadAutoCompleteDropDownPosition)Enum.Parse(typeof(RadAutoCompleteDropDownPosition), RadAutoCompleteDropDownPosition.Automatic.ToString(), true);
                //RacbxRecipient.AllowCustomEntry = true;
                //RacbxRecipient.TokensSettings.AllowTokenEditing = false;
                //RacbxRecipient.MinFilterLength = 1;
                //RacbxRecipient.MaxResultCount = 10;

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # region Methods

        private void UpdateStrings()
        {
            LblTitle.Text = "Compose";
            LblTo.Text = "To:";
            LblSubject.Text = "Subject:";
            BtnSend.Text = "Send";
            BtnCancel.Text = "Discard";
            LblMessage.Text = "Message:";
        }

        private void ClearMessageData()
        {
            TbxSubject.Text = string.Empty;
            TbxMessage.Text = string.Empty;
            divError.Visible = false;
            divInfo.Visible = false;
            divSuccess.Visible = false;
            divWarning.Visible = false;
        } 

        # endregion

        #region Buttons

        protected void BtnDiscard_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ClearMessageData();
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "messages/inbox"), false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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
                if (vSession.User != null)
                {
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

                    if (string.IsNullOrEmpty(RacbxRecipient.Text))
                    {
                        LblError.Text = "Error! ";
                        LblErrorContent.Text = "You have to select a company as a recipient.";
                        divError.Visible = true;
                        divError.Focus();
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

                    if (string.IsNullOrEmpty(TbxMessage.Text))
                    {
                        LblError.Text = "Error! ";
                        LblErrorContent.Text = "Your message must have a content.";
                        divError.Visible = true;
                        divError.Focus();
                        return;
                    }

                    session.OpenConnection();

                    ElioUserPacketStatus senderPacketStatusFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

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

                    ElioUsersMessages message = new ElioUsersMessages();

                    ElioUsers recipientDetails = Sql.GetCompanynameAndIdAndEmailAndOfficialEmailByName(RacbxRecipient.Text, session);

                    if (recipientDetails != null)
                    {
                        try
                        {
                            session.BeginTransaction();

                            message = GlobalDBMethods.InsertCompanyMessage(vSession.User.Id, vSession.User.Email, recipientDetails.Id, recipientDetails.Email, recipientDetails.OfficialEmail, TbxSubject.Text, TbxMessage.Text, session);

                            List<string> emails = new List<string>();

                            emails.Add(recipientDetails.Email);

                            if (!string.IsNullOrEmpty(recipientDetails.OfficialEmail))
                            {
                                emails.Add(recipientDetails.OfficialEmail);
                            }                         

                            //EmailNotificationsLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxSubject.Text, session);
                            EmailSenderLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxSubject.Text, TbxMessage.Text, false, vSession.Lang, session);

                            ClearMessageData();

                            LblSuccess.Text = "Congratulations! ";
                            LblSuccessContent.Text = "Your message was sent successfully.";
                            divSuccess.Visible = true;
                            divSuccess.Focus();

                            if (RacbxRecipient.Text != vSession.User.CompanyName)
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
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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