using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus.Controls.Common
{
    public partial class SignUpControl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private string CookieName
        {
            get
            {
                return "lgn";
            }
        }

        public bool IsSubAccount
        {
            get
            {
                return ViewState["IsSubAccount"] == null ? false : (bool)ViewState["IsSubAccount"];
            }
            set
            {
                ViewState["IsSubAccount"] = value;
            }
        }

        public string UserGuid
        {
            get
            {
                return ViewState["UserGuid"] == null ? string.Empty : ViewState["UserGuid"].ToString();
            }
            set
            {
                ViewState["UserGuid"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UcMessageAlert.Visible = false;

                if (Request.QueryString["verificationViewID"] != null)
                {
                    if (vSession.User != null)
                    {
                        Session.Clear();

                        if (Request.Browser.Cookies)
                        {
                            HttpCookie loginCookie = Request.Cookies[CookieName];
                            if (loginCookie != null)
                            {
                                loginCookie.Expires = DateTime.Now.AddYears(-30);
                                Response.Cookies.Add(loginCookie);
                            }
                        }
                    }
                }

                if (vSession.User == null)
                {
                    UpdateStrings();

                    if (Request.QueryString["verificationViewID"] != null)
                    {
                        //is sub account registration
                        IsSubAccount = true;
                        UserGuid = Request.QueryString["verificationViewID"].ToString();

                        divEmail.Visible = false;
                        divEmailError.Visible = false;
                        RtbxUsername.ReadOnly = true;

                        string guid = Request.QueryString["verificationViewID"].ToString();

                        try
                        {
                            session.OpenConnection();

                            ElioUsersSubAccounts subAccount = Sql.GetSubAccountByGuid(guid, session);

                            if (subAccount != null)
                            {
                                if (subAccount.IsConfirmed == 0)
                                {
                                    RtbxUsername.Text = subAccount.Email;
                                    RtbxUsername.ReadOnly = true;
                                }
                                else
                                {
                                    Response.Redirect(ControlLoader.Login, false);
                                }
                            }
                            else
                            {
                                IsSubAccount = false;
                                divEmail.Visible = true;
                                divEmailError.Visible = true;
                                LblEmailError.Text = string.Empty;
                                RtbxUsername.ReadOnly = false;
                                RtbxUsername.Text = string.Empty;
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
                }
                else
                {
                    RedirectToHome();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Buttons

        protected void LnkBtnCreatePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RtbxPassword.Text = WdS.ElioPlus.Lib.Utils.GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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
        
        protected void LnkBtnForgotPassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(ControlLoader.ForgotPassword, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (DataHasError()) return;

                if (!IsSubAccount)
                {
                    #region Insert New User

                    vSession.User = GlobalDBMethods.InsertNewUserWithCredentials(RtbxUsername.Text, RtbxPassword.Text, RtbxEmail.Text, 1, session);

                    #endregion

                    //RedirectToFullRegistrationPage();

                    if (HttpContext.Current.Request.Url.AbsolutePath.Contains("/community"))
                    {
                        Response.Redirect(ControlLoader.CommunityFullRegistration, false);

                        #region Send Notification Email

                        EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredCommunityUser(vSession.User, vSession.Lang, session);

                        #endregion
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.FullRegistrationPage, false);

                        #region Send Notification Email

                        EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredUser(vSession.User, vSession.Lang, session);

                        #endregion
                    }
                }
                else
                {
                    #region Sub Account

                    if (!string.IsNullOrEmpty(UserGuid))
                    {
                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByGuid(UserGuid, session);

                        if (subAccount != null)
                        {
                            subAccount.Password = RtbxPassword.Text;
                            subAccount.PasswordEncrypted = MD5.Encrypt(subAccount.Password);
                            subAccount.LastUpdated = DateTime.Now;
                            subAccount.IsConfirmed = 1;
                            subAccount.IsActive = 1;
                            subAccount.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            subAccount.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                            subAccount.FirstName = string.Empty;
                            subAccount.LastName = string.Empty;
                            subAccount.PersonalImage = string.Empty;
                            subAccount.Position = string.Empty;
                            subAccount.LinkedinUrl = string.Empty;
                            subAccount.LinkedinId = string.Empty;

                            DataLoader<ElioUsersSubAccounts> loader = new DataLoader<ElioUsersSubAccounts>(session);
                            loader.Update(subAccount);

                            vSession.User = Sql.GetUserById(subAccount.UserId, session);
                        }
                    }

                    #endregion

                    Response.Redirect(vSession.Page = (HttpContext.Current.Request.Url.AbsolutePath.Contains("/community")) ? ControlLoader.CommunityFullRegistration : ControlLoader.Dashboard(vSession.User, "team"), false);
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

        protected void RbtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RedirectToHome();
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

        protected void LnkBtnRegister_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityLogin : ControlLoader.Login, false);
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

        protected void LinkedinRetrieve_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int userId = 0;
                int result = 0;

                if (!string.IsNullOrEmpty(LinkId.Value) && !string.IsNullOrEmpty(LinkEmail.Value))
                {
                    result = GlobalDBMethods.CheckUserByLinkedinAccount(LinkId.Value,
                                                                            LinkFirstName.Value,
                                                                            LinkLastName.Value,
                                                                            LinkHeadline.Value,
                                                                            LinkPicture.Value,
                                                                            LinkEmail.Value,
                                                                            LinkSummary.Value,
                                                                            LinkProfileUrl.Value,
                                                                            session,
                                                                            ref userId);
                }
                switch (result)
                {
                    case -1:    //BLOCKED

                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "message", "1")).Text, MessageTypes.Error, false, true, false);

                        EmailSenderLib.SendErrorNotificationEmail(MessageTypes.Error.ToString(), Request.Url.ToString(), "User Account Blocked", "Error message : A user tried to login through Linkedin at: " + DateTime.Now + " but he's account is blocked.", vSession.Lang, session);

                        break;

                    case 0:     //ERROR DATA

                        string alertMsg = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "21")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alertMsg, MessageTypes.Error, false, true, false);

                        EmailSenderLib.SendErrorNotificationEmail(MessageTypes.Error.ToString(), Request.Url.ToString(), "Error message : " + alertMsg, "A user tried to login through Linkedin at: " + DateTime.Now + " but he did not achieved it.", vSession.Lang, session);

                        break;

                    case 1:     //USER EXIST --> LOGIN  
                    case 2:     //USER NOT EXIST --> REGISTER

                        if (userId != 0)
                        {
                            vSession.User = Sql.GetUserById(userId, session);
                        }
                        else
                        {
                            //ERROR
                            alertMsg = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "21")).Text;

                            GlobalMethods.ShowMessageControl(UcMessageAlert, alertMsg, MessageTypes.Error, false, true, false);

                            EmailSenderLib.SendErrorNotificationEmail(MessageTypes.Error.ToString(), Request.Url.ToString(), "Error message : " + alertMsg, "A user tried to login through Linkedin at: " + DateTime.Now + " but he did not achieved it because database returned userId = 0.", vSession.Lang, session);

                            return;
                        }

                        Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityPosts : ControlLoader.FullRegistrationPage, false);

                        break;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "21")).Text, MessageTypes.Error, false, true, false);

                EmailSenderLib.SendErrorNotificationEmail(MessageTypes.Error.ToString(), Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), vSession.Lang, session);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region Methods

        private void UpdateStrings()
        {
            LblHeader.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "3")).Text;

            RtbxUsername.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "1")).Text;
            RtbxPassword.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "2")).Text;
            RtbxRetypePassword.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "2")).Text;
            RtbxEmail.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "4")).Text;
            LnkBtnCreatePassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "button", "2")).Text;
            LblAccountText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "5")).Text;
            LnkBtnRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "6")).Text;

            //RadToolTipUsername.Text =Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "label", "1")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "tooltip", "1")).Text + " " + Validations.UsernameValidSymbols;
            //RadToolTipPassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "label", "2")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "tooltip", "1")).Text + " " + Validations.PasswordValidSymbols;
            //RadToolTipRetypePassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "tooltip", "2")).Text;
            //RadToolTipEmail.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "tooltip", "3")).Text;

            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;
            Label lblCancelText = (Label)ControlFinder.FindControlRecursive(RbtnCancel, "LblCancelText");
            lblCancelText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "7")).Text;
        }

        private void RedirectToHome()
        {
            Response.Redirect(vSession.Page, false);
        }

        private void RedirectToFullRegistrationPage()
        {
            //Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityFullRegistration : ControlLoader.FullRegistrationPage, false);
            Response.Redirect(vSession.Page = ControlLoader.FullRegistrationPage, false);
        }

        private bool DataHasError()
        {
            #region Reset Controls

            LblUsernameError.Text = string.Empty;
            LblPasswordError.Text = string.Empty;
            LblRetypePasswordError.Text = string.Empty;
            LblEmailError.Text = string.Empty;

            #endregion

            #region Check Data

            if (string.IsNullOrEmpty(RtbxUsername.Text))
            {
                LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "1")).Text;
                return true;
            }
            else
            {
                if (RtbxUsername.Text.Length < 8)
                {
                    LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "2")).Text;
                    return true;
                }
                else
                {
                    if (RtbxUsername.Enabled)   // if Enabled = false meens it is sub account --> Username is the email --> may have special characters --> allow special characters
                    {
                        if (!Validations.IsUsernameCharsValid(RtbxUsername.Text))
                        {
                            LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "3")).Text;
                            return true;
                        }
                    }

                    if (Sql.ExistUsername(RtbxUsername.Text, session))
                    {
                        LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "4")).Text;
                        return true;
                    }
                }
            }

            if (string.IsNullOrEmpty(RtbxPassword.Text))
            {
                LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "5")).Text;
                return true;
            }
            else
            {
                if (RtbxPassword.Text.Length < 8)
                {
                    LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "6")).Text;
                    return true;
                }
                if (!Validations.IsPasswordCharsValid(RtbxPassword.Text))
                {
                    LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "7")).Text;
                    return true;
                }
            }

            if (string.IsNullOrEmpty(RtbxRetypePassword.Text))
            {
                LblRetypePasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "8")).Text;
                return true;
            }
            else
            {
                if (RtbxPassword.Text != RtbxRetypePassword.Text)
                {
                    LblRetypePasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "9")).Text;
                    return true;
                }
            }

            if (string.IsNullOrEmpty(RtbxEmail.Text))
            {
                LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "10")).Text;
                return true;
            }
            else
            {
                if (!Validations.IsEmail(RtbxEmail.Text))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "11")).Text;
                    return true;
                }

                if (Sql.ExistEmail(RtbxEmail.Text, session))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "12")).Text;
                    return true;
                }
            }

            #endregion

            return false;
        }

        private void ClearFields()
        {
            LblUsernameError.Text = string.Empty;
            LblPasswordError.Text = string.Empty;
            LblRetypePasswordError.Text = string.Empty;
            LblEmailError.Text = string.Empty;

            RtbxUsername.Text = string.Empty;
            RtbxPassword.Text = string.Empty;
            RtbxRetypePassword.Text = string.Empty;
            RtbxEmail.Text = string.Empty;
        }

        #endregion
    }
}