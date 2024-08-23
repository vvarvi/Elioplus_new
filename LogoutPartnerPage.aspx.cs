using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Services.Customers.ioAPI;
using System.Configuration;
using WdS.ElioPlus.Lib.Services.LinkedinAPI;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus
{
    public partial class LogoutPartnerPage : System.Web.UI.Page
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

        private Random random = new Random();

        //CaptchaGenerator _captchaGenerator;

        private string CaptchaKey
        {
            get
            {
                object value = ViewState["CaptchaKey"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["CaptchaKey"] = value;
            }
        }

        private string VendorName
        {
            get
            {
                object value = ViewState["VendorName"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["VendorName"] = Regex.Replace(value, @"[^A-Za-z0-9]+", "_").Trim().ToLower();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    #region New Way

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

                    #endregion
                }

                if (vSession.User != null)
                {
                    #region Customers API

                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCustomersAPI"]))
                    {
                        if (ConfigurationManager.AppSettings["UseCustomersAPI"].ToString() == "true")
                        {
                            Customers.AddUsersToCustomersAPI(vSession, session);
                        }
                    }

                    #endregion

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
                else
                {
                    UcMessageControlAlert.Visible = UcMessageControlForgotAlert.Visible = false;
                    
                    if (!IsPostBack)
                    {
                        #region Get Company Profile Data from Url

                        ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                        if (vendor != null)
                        {
                            VendorName = vendor.CompanyName;
                            ImgElioplusLogo.ImageUrl = vendor.CompanyLogo;
                            aElioplusLogo.HRef = ControlLoader.Profile(vendor);
                            ImgElioplusLogo.ToolTip = vendor.CompanyName + " profile";
                            ImgElioplusLogo.AlternateText = vendor.CompanyName + " logo";
                            //ImgPowerByElio.AlternateText = vendor.CompanyName + " email";															   
                            //VendorName = vendor.CompanyName.Replace(" ", "-").ToLower();
                            LblTitle.Text = "Login to " + vendor.CompanyName + " Partner Portal";
                            //LblCreateAccount.Text = (vendor.CompanyName.Length <= 12) ? "Sign up to " + vendor.CompanyName + " Partner Portal" : "Sign up to " + vendor.CompanyName.Substring(0, 12) + "..." + " Partner Portal";
                        }
                        else
                            Response.Redirect(ControlLoader.Login, false);

                        #endregion

                        UpdateStrings();
                        PageTitle();
                        SetLinks();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                        vSession.LoggedInSubAccountRoleID = 0;
                        vSession.SubAccountEmailLogin = "";
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

        #region Methods

        private void SetLinks()
        {
            aTerms.HRef = ControlLoader.Terms;
            aPrivacy.HRef = ControlLoader.Privacy;
            aContact.HRef = ControlLoader.ContactUs;
            aCreateAccount.HRef = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.SignUpPartner.Replace("{CompanyName}", VendorName) : ControlLoader.SignUp;
        }

        private void PageTitle()
        {
            PgTitle.Text = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal logout page" : " Your vendor's partner portal login";    //GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView).Replace("{CompanyName}", VendorName);
            MetaDescription = (!string.IsNullOrEmpty(VendorName)) ? "Logout from " + VendorName + " partner portal. Login and Start working together and collaborate with " + VendorName + "" : "Logout from your Vendor's partner portal. Login and Start working together and collaborate with your vendor";
            MetaKeywords = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal logout page" : "Vendor partner portal logout page";
        }

        private void UpdateStrings()
        {
            try
            {
                //CbxRemember.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "4")).Text;
                //LblWarning.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "8")).Text;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool CheckData()
        {
            UcMessageControlAlert.Visible = UcMessageControlForgotAlert.Visible = false;

            bool isError = false;

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                string message = "Please enter your email";
                GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, message, MessageTypes.Error, true, true, true, true, false);
                return isError = true;
            }
            else
            {
                if (!Validations.IsEmail(TbxEmail.Text))
                {
                    string message = "This email is not valid";
                    GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, message, MessageTypes.Error, true, true, true, true, false);
                    return isError = true;
                }
            }

            return isError;
        }

        private void ClearMessageData()
        {
            TbxEmail.Text = string.Empty;
            UcMessageControlAlert.Visible = UcMessageControlForgotAlert.Visible = false;
        }

        #endregion

        #region Buttons

        protected void forgetPassword_ServerClick(object sender, EventArgs e)
        {
            try
            {
                loginForm.Visible = false;
                forgotForm.Visible = true;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void backBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                loginForm.Visible = true;
                forgotForm.Visible = false;

                UcMessageControlForgotAlert.Visible = UcMessageControlAlert.Visible = false;

                ClearMessageData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void submitBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageControlAlert.Visible = UcMessageControlForgotAlert.Visible = false;

                bool isError = CheckData();

                if (isError) return;

                ElioUsers user = Sql.GetUserByEmail(TbxEmail.Text, session);

                if (user != null)
                {
                    string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                    #region Send Email

                    EmailSenderLib.SendResetPasswordEmail(newPassword, TbxEmail.Text, vSession.Lang, session);

                    #endregion

                    if (newPassword != string.Empty)
                    {
                        #region Update new password

                        user.Password = newPassword;
                        user.PasswordEncrypted = MD5.Encrypt(user.Password);
                        user.LastUpdated = DateTime.Now;

                        user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion

                        #region Update Sub-Account Password too if has

                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByEmail(TbxEmail.Text, session);
                        if (subAccount != null)
                        {
                            subAccount.Password = newPassword;
                            subAccount.PasswordEncrypted = MD5.Encrypt(newPassword);
                            subAccount.LastUpdated = DateTime.Now;

                            GlobalDBMethods.UpDateSubUser(subAccount, session);
                        }

                        #endregion
                    }
                    else
                    {
                        isError = true;
                        string messageAlert = "We ase sorry but something wrong happened. Please try again later or contact.";
                        GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, messageAlert, MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                }
                else
                {
                    ElioUsersSubAccounts subUser = Sql.GetSubAccountByEmail(TbxEmail.Text, session);
                    if (subUser != null)
                    {
                        string newPassword = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);

                        #region Send Email

                        EmailSenderLib.SendResetPasswordEmail(newPassword, TbxEmail.Text, vSession.Lang, session);

                        #endregion

                        if (newPassword != string.Empty)
                        {
                            #region Update new password

                            subUser.Password = newPassword;
                            subUser.PasswordEncrypted = MD5.Encrypt(subUser.Password);
                            subUser.LastUpdated = DateTime.Now;

                            GlobalDBMethods.UpDateSubUser(subUser, session);

                            #endregion
                        }
                        else
                        {
                            isError = true;
                            string messageAlert = "We ase sorry but something wrong happened. Please try again later or contact.";
                            GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, messageAlert, MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                    }
                    else
                    {
                        isError = true;
                        string messageAlert = "This email does not belong to a user of Elioplus. Please try again or contact.";
                        GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, messageAlert, MessageTypes.Error, true, true, true, true, false);
                        return;
                    }
                }

                ClearMessageData();

                UcMessageControlAlert.Visible = UcMessageControlForgotAlert.Visible = false;

                string message = "Done! You will receive a new password in your email inbox!";
                GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, message, MessageTypes.Success, true, true, true, true, false);
            }
            catch (Exception ex)
            {
                string message = (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false") ? ex.Message : "We ase sorry but something wrong happened. Please try again later or contact us.";
                
                GlobalMethods.ShowMessageControl(UcMessageControlForgotAlert, message, MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString(), "User Email who request new password: " + TbxEmail.Text);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void signInBtn_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string responseError = string.Empty;

                ElioUsers user = Sql.GetUserByUsernameAndPassword(TbxUsername.Text, TbxPassword.Text, TbxUsername.Text, HttpContext.Current.Session.SessionID, out responseError, vSession.Lang, out string subAccountEmailLogin, out int loggedInRoleID, out bool isAdminRole, session);

                if (user != null)
                {
                    #region Success Login

                    vSession.SubAccountEmailLogin = subAccountEmailLogin;
                    vSession.LoggedInSubAccountRoleID = loggedInRoleID;
                    vSession.IsAdminRole = isAdminRole;

                    if (CbxRemember.Checked)
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

                    #endregion

                    #endregion

                    #region Redirect to Page

                    string path = HttpContext.Current.Request.Url.AbsolutePath;

                    string redirectUrl = ControlLoader.Dashboard(vSession.User, "home"); //ControlLoader.Default;
                    string url = ControlLoader.FullRegistrationPagePartner.Replace("{CompanyName}", VendorName);

                    if (path.EndsWith("partner-logout"))
                    {
                        if (vSession.User.AccountStatus != Convert.ToInt32(AccountStatus.Completed))
                            redirectUrl = path.Replace("/partner-logout", "/partner-full-registration");
                        else
                            redirectUrl = ControlLoader.Dashboard(vSession.User, "home");  //ControlLoader.Default;
                    }

                    Response.Redirect(redirectUrl, false);

                    #endregion

                    #region Write log file for Statistics

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        Logger.Info("A user of type {0} from {1} and ID: {2}, logged in", vSession.User.CompanyType, vSession.User.Country, vSession.User.Id);
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
                        Logger.Info("Bad login with username: {0} and password: {1}", TbxUsername.Text, TbxPassword.Text);

                        #region Bad Login

                        TbxUsername.Text = string.Empty;
                        TbxPassword.Text = string.Empty;

                        string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "22")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                        #endregion
                    }
                    else
                    {
                        #region Account Error

                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, responseError, MessageTypes.Error, true, true, true, true, false);

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}