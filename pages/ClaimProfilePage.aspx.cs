using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Linq;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Controls.AlertControls;
using System.Runtime.CompilerServices;
using System.Net.Mail;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI;

namespace WdS.ElioPlus.pages
{
    public partial class ClaimProfilePage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public int UserID
        {
            get
            {
                if (ViewState["UserID"] == null)
                {
                    int id = 0;

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        string segs = pathSegs[pathSegs.Length - 2].TrimEnd('/').TrimEnd('-');

                        if (pathSegs.Length > 2)
                        {
                            if (Lib.Utils.Validations.IsNumber(segs))
                            {
                                id = Convert.ToInt32(segs.TrimEnd());

                                ViewState["UserID"] = id;
                            }
                            else
                                ViewState["UserID"] = 0;
                        }
                        else
                            ViewState["UserID"] = 0;
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return (int)ViewState["UserID"];
                }
                else
                    return (int)ViewState["UserID"];
            }
            set
            {
                ViewState["UserID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    FixPage();

                if (UserID > 0)
                {
                    //ElioUsers user = Sql.GetUserById(UserID, session);
                    //if (user != null)
                    //{
                    //    string message = user.UserApplicationType == (int)UserApplicationType.ThirdParty ? "This request will be sent to all similar businesses on our network to help you compare prices. The <b>{ThirdPartyCompanyName}</b> profile is currently not managed or endorsed by <b>{ThirdPartyCompanyName}</b>.".Replace("{ThirdPartyCompanyName}", user.CompanyName) : "This request will be sent to all similar businesses on our network to help you compare prices.";

                    //    GlobalMethods.ShowMessageAlertControl(UcMessageAlertTop, message, MessageTypes.Info, true, true, false, false, false);
                    //}
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

        #region Methods

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            //aClose.HRef = ControlLoader.SearchForChannelPartners;
        }

        private void UpdateStrings()
        {
            try
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
                HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

                if (pgTitle != null)
                    pgTitle.InnerText = "Claim this profile";

                metaDescription.Attributes["content"] = "Claim this profile";
                metaKeywords.Attributes["content"] = "claim profile, Elioplus quotations";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool CheckData(bool isClaimMode)
        {
            bool isError = false;

            divClaimWarningMsg.Visible = false;
            divClaimSuccessMsg.Visible = false;

            if (TbxClaimMessageEmail.Visible)
            {
                if (string.IsNullOrEmpty(TbxClaimMessageEmail.Text))
                {
                    LblClaimWarningMsgContent.Text = "Please enter an email address!";
                    divClaimWarningMsg.Visible = true;
                    return isError = true;
                }
                else
                {
                    if (!Validations.IsEmail(TbxClaimMessageEmail.Text))
                    {
                        LblClaimWarningMsgContent.Text = "Please enter a valid email address!";
                        divClaimWarningMsg.Visible = true;
                        return isError = true;
                    }
                }
            }

            return isError;
        }

        private void ClearMessageData(bool isClaimMode)
        {
            TbxClaimMessageEmail.Text = string.Empty;

            LblClaimSuccessMsg.Text = string.Empty;
            LblClaimWarningMsg.Text = string.Empty;
            divClaimWarningMsg.Visible = false;
            divClaimSuccessMsg.Visible = false;
        }

        #endregion

        #region Buttons

        protected void BtnSendClaim_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divClaimSuccessMsg.Visible = false;
                divClaimWarningMsg.Visible = false;

                if (UserID > 0)
                { 
                    ElioUsers user = Sql.GetUserById(UserID, session);
                    if (user != null)
                    {                    
                        bool isError = CheckData(true);

                        if (isError) return;

                        MailAddress companyMail = new MailAddress(user.Email.Trim().ToLower());
                        MailAddress inputMail = new MailAddress(TbxClaimMessageEmail.Text.Trim().ToLower());
                        string companyHost = companyMail.Host;
                        string inputHost = inputMail.Host;

                        if (companyHost == inputHost)
                        {
                            //update new email and set user as elioplus from third party
                            if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers) && user.UserApplicationType == (int)UserApplicationType.ThirdParty)
                            {
                                if (user.Email != TbxClaimMessageEmail.Text)
                                    user.Email = TbxClaimMessageEmail.Text;

                                if (!string.IsNullOrEmpty(user.OfficialEmail))
                                    user.OfficialEmail = "";

                                user.UserApplicationType = (int)UserApplicationType.Elioplus;
                                user.LastUpdated = DateTime.Now;

                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                                loader.Update(user);
                            }

                            try
                            {
                                EmailSenderLib.ClaimProfileResetPasswordEmail(user.Password, user.Email, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }
                        else
                        {
                            isError = true;
                            divClaimSuccessMsg.Visible = false;
                            divClaimWarningMsg.Visible = true;
                            LblClaimWarningMsg.Text = "Warning! ";
                            LblClaimWarningMsgContent.Text = "We 're sorry but this email does not match to this profile's email. Please contact us.";

                            return;
                        }

                        if (!isError)
                        {
                            ClearMessageData(true);

                            divClaimSuccessMsg.Visible = true;
                            LblClaimSuccessMsgContent.Text = "Thank you! You have received an email with your password and account instructions. You can log in to it.";
                        }
                        else
                        {
                            ClearMessageData(true);

                            divClaimWarningMsg.Visible = true;
                            LblClaimWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Search, false);
                    }
                }
                else
                {
                    divClaimWarningMsg.Visible = true;
                    LblClaimWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";
                }
            }
            catch (Exception ex)
            {
                ClearMessageData(true);

                divClaimWarningMsg.Visible = true;
                LblClaimWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later to contact us!";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelClaimMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearMessageData(true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion        
    }
}