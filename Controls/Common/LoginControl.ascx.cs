using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus.Controls.Common
{
    public partial class LoginControl : System.Web.UI.UserControl
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UcMessageAlert.Visible = false;

                if (vSession.User == null)
                {
                    UpdateStrings();
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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

                RtbxPasswordLogin.Text = WdS.ElioPlus.Lib.Utils.GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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
                RedirectToSimpleRegistrationPage();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSubmit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                bool isError = CheckLoginData();

                if (isError) return;

                string responseError = string.Empty;

                ElioUsers user = Sql.GetUserByUsernameAndPassword(RtbxUsernameLogin.Text, RtbxPasswordLogin.Text, RtbxUsernameLogin.Text, HttpContext.Current.Session.SessionID, out responseError, vSession.Lang, out string subAccountEmailLogin, out int loggedInRoleID, out bool isAdminRole, session);
                if (user != null)
                {
                    #region Success Login

                    vSession.SubAccountEmailLogin = subAccountEmailLogin;
                    vSession.LoggedInSubAccountRoleID = loggedInRoleID;
                    vSession.IsAdminRole = isAdminRole;

                    if (CbxRememberMe.Checked)
                    {
                        #region Save User Login Credential In Coockies

                        if ((Request.Browser.Cookies))
                        {
                            HttpCookie loginCookie = Request.Cookies[CookieName];
                            if (loginCookie == null)
                            {
                                loginCookie = new HttpCookie(CookieName);
                                loginCookie.Expires = DateTime.Now.AddDays(30);

                                loginCookie.Value = Encrypter.EncryptStringAes(user.Username, Encrypter.SharedSecret) + Encrypter.EncryptStringAes(user.Password, Encrypter.SharedSecret);

                                if (loginCookie.Value.Length % 16 == 0)
                                {
                                    Response.Cookies.Add(loginCookie);
                                }
                                else
                                {
                                    loginCookie.Value = null;
                                }
                            }
                            else
                            {
                                if (loginCookie.Value.Length % 16 == 0)
                                {
                                    loginCookie.Value = user.Username + user.Password;
                                }
                                else
                                {
                                    loginCookie.Value = null;
                                }
                            }
                        }

                        #endregion
                    }

                    if (!Sql.IsUserAdministrator(user.Id, session))
                    {
                        #region Keep Login Statistics

                        Sql.InsertUserLoginStatistics(user.Id, session);

                        #endregion
                    }

                    #region Update User / Session

                    vSession.User = user;
                    //////Global.AddInSessionsList(vSession.User.CurrentSessionId);

                    #endregion

                    //if (Request.RawUrl.Contains("/community"))
                    //{
                    //    Response.Redirect(vSession.Page = (vSession.User.CommunityStatus == Convert.ToInt32(AccountStatus.Completed)) ? ControlLoader.CommunityPosts : ControlLoader.CommunityFullRegistration, false);
                    //}
                    //else
                    //{
                    //    Response.Redirect(vSession.Page = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? ControlLoader.Default : ControlLoader.FullRegistrationPage, false);
                    //}

                    Response.Redirect(vSession.Page = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? ControlLoader.Default() : vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);

                    #endregion

                    #region Write log file for Statistics

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        Logger.Info("A user of type {0} fr om {1} and ID: {2}, logged in", vSession.User.CompanyType, vSession.User.Country, vSession.User.Id);
                    }
                    else
                    {
                        Logger.Info("A user with ID: {0}, logged in", vSession.User.Id);
                    }

                    #endregion
                }
                else
                {
                    if (responseError == string.Empty)
                    {
                        Logger.Info("Bad login with username: {0} and password: {1}", RtbxUsernameLogin.Text, RtbxPasswordLogin.Text);

                        #region Bad Login

                        RtbxUsernameLogin.Text = string.Empty;
                        RtbxPasswordLogin.Text = string.Empty;

                        LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "43")).Text;
                        LblPasswordLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "44")).Text;

                        #endregion
                    }
                    else
                    {
                        #region Account Error

                        GlobalMethods.ShowMessageControl(UcMessageAlert, responseError, MessageTypes.Error, false, true, false);
                        //GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "42")).Text, MessageTypes.Error, false, true, false);

                        #endregion
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

        protected void BtnCancelLogin_OnClick(object sender, EventArgs args)
        {
            try
            {
                RedirectToHome();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnForgotPassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityForgotPassword : ControlLoader.ForgotPassword, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

                        Logger.Info("User {0}, tried to log in through Linkedin un-successfully because he is blocked", vSession.User.Id);

                        break;

                    case 0:     //ERROR DATA

                        string alertMsg = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "21")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alertMsg, MessageTypes.Error, false, true, false);

                        EmailSenderLib.SendErrorNotificationEmail(MessageTypes.Error.ToString(), Request.Url.ToString(), "Error message : " + alertMsg, "A user tried to login through Linkedin at: " + DateTime.Now + " but he did not achieved it.", vSession.Lang, session);

                        Logger.Info("User {0}, tried to log in through Linkedin un-successfully", vSession.User.Id);

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

                        Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityPosts : vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);

                        if (result == 1)
                            Logger.Info("User {0}, successfully logged in through Linkedin", vSession.User.Id);
                        else if (result == 2)
                            Logger.Info("User {0}, successfully registered through Linkedin", vSession.User.Id);

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
            LblTopicsHeader.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "1")).Text;
            RtbxUsernameLogin.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "1")).Text;
            RtbxPasswordLogin.EmptyMessage = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "emptymessage", "2")).Text;
            LblRememberMe.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "2")).Text;
            LnkBtnRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "communitysignin", "label", "3")).Text;
            LnkBtnForgotPassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "45")).Text;

            Label lblBtnSubmitText = (Label)ControlFinder.FindControlRecursive(BtnSubmit, "LblBtnSubmitText");
            lblBtnSubmitText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "16")).Text;

            Label lblBtnCancelLoginText = (Label)ControlFinder.FindControlRecursive(BtnCancelLogin, "LblBtnCancelLoginText");
            lblBtnCancelLoginText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "13")).Text;
        }

        private void RedirectToHome()
        {
            Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityPosts : ControlLoader.Default(), false);
        }

        private void RedirectToFullRegistrationPage()
        {
            Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityFullRegistration : vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);
        }

        private void RedirectToSimpleRegistrationPage()
        {
            Response.Redirect(vSession.Page = (Request.RawUrl.Contains("/community")) ? ControlLoader.CommunitySimpleRegistration : ControlLoader.SignUp, false);
        }

        private bool CheckLoginData()
        {
            bool isError = false;

            UcMessageAlert.Visible = false;
            LblUsernameLoginError.Text = string.Empty;
            LblPasswordLoginError.Text = string.Empty;

            if (string.IsNullOrEmpty(RtbxUsernameLogin.Text))
            {
                LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "40")).Text;
                return isError = true;
            }

            if (string.IsNullOrEmpty(RtbxPasswordLogin.Text))
            {
                LblPasswordLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "41")).Text;
                return isError = true;
            }

            return isError;
        }

        private bool DataHasError()
        {
            #region Reset Controls

            UcMessageAlert.Visible = false;
            LblUsernameLoginError.Text = string.Empty;
            LblPasswordLoginError.Text = string.Empty;

            #endregion

            #region Check Data

            if (string.IsNullOrEmpty(RtbxUsernameLogin.Text))
            {
                LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "1")).Text;
                return true;
            }
            else
            {
                if (RtbxUsernameLogin.Text.Length < 8)
                {
                    LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "2")).Text;
                    return true;
                }
                else
                {
                    if (!Validations.IsUsernameCharsValid(RtbxUsernameLogin.Text))
                    {
                        LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "3")).Text;
                        return true;
                    }

                    if (Sql.ExistUsername(RtbxUsernameLogin.Text, session))
                    {
                        LblUsernameLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "4")).Text;
                        return true;
                    }
                }
            }

            if (string.IsNullOrEmpty(RtbxPasswordLogin.Text))
            {
                LblPasswordLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "5")).Text;
                return true;
            }
            else
            {
                if (RtbxPasswordLogin.Text.Length < 8)
                {
                    LblPasswordLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "6")).Text;
                    return true;
                }
                if (!Validations.IsPasswordCharsValid(RtbxPasswordLogin.Text))
                {
                    LblPasswordLoginError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "7")).Text;
                    return true;
                }
            }

            #endregion

            return false;
        }

        #endregion        
    }
}