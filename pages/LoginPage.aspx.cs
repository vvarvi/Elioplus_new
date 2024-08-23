using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Services.Customers.ioAPI;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Configuration;

namespace WdS.ElioPlus.pages
{
    public partial class LoginPage : System.Web.UI.Page
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

        private string VendorName
        {
            get
            {
                object value = ViewState["VendorName"];
                return value != null ? value.ToString() : "";
            }
            set
            {
                ViewState["VendorName"] = value;
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

        public LoginPage()
        {
            //_captchaGenerator = new CaptchaGenerator();
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
                    UcMessageControlAlert.Visible = false;

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
                    UcMessageControlAlert.Visible = false;
                    
                    if (!IsPostBack)
                    {
                        #region Get CompanyName Profile

                        string path = HttpContext.Current.Request.Url.AbsolutePath;
                        string[] originalPathElements = path.Split('/');

                        if (originalPathElements.Length > 0)
                        {
                            if (originalPathElements[originalPathElements.Length - 1] == "partner-login")
                                Response.Redirect(ControlLoader.LoginPartner, false);
                        }

                        #endregion
                        
                        SetLinks();
                        UpdateStrings();

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

        # region Methods

        private void GenerateRandomCode()
        {
            string s = "";
            for (int i = 0; i < 6; i++)
                s = String.Concat(s, this.random.Next(10).ToString());
        }

        private void SetLinks()
        {
            try
            {
                aElioplusLogo.HRef = ControlLoader.Default2();
                aResetPassword.HRef = ControlLoader.ForgotPassword;
                aCreateAccount.HRef = ControlLoader.SignUp;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void UpdateStrings()
        {
            try
            {
                //ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
                //ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "alternate", "1")).Text;
                //LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "1")).Text;
                //LblForgotPassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "2")).Text;
                //BtnLogin.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "3")).Text;
                //CbxRemember.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "4")).Text;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool CheckLoginData()
        {
            bool isError = false;

            UcMessageControlAlert.Visible = false;
            
            if (string.IsNullOrEmpty(TbxUsername.Text))
            {
                string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "9")).Text;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxPassword.Text))
            {
                string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "10")).Text;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                return isError = true;
            }

            return isError;
        }

        #endregion

        #region Buttons

        protected void BtnSubmit_OnClick(object sender, EventArgs args)
        {
            try
            {
                #region Captcha

                //#if !bypassCaptcha

                //if (CaptchaKey != null)
                //{
                //#if !skipCaptcha

                //    if (ImageCode.Text == "")
                //    {
                //        divWarningMessage.Visible = true;
                //        LblWarningMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "21")).Text;
                //        this.ImageCode.Text = "";
                //        return;
                //    }
                //    else if (_captchaGenerator.DencryptImageKey(CaptchaKey.ToString()) != ImageCode.Text)
                //    {
                //        /* Wrong Captcha */
                //        GenerateRandomCode();
                //        divWarningMessage.Visible = true;
                //        LblWarningMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "20")).Text;
                //        this.ImageCode.Text = "";
                //        return;
                //    }

                //#endif
                //}

                //#endif

                #endregion

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

                    #region Redirect to Page

                    Response.Redirect(vSession.Page = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? ControlLoader.Dashboard(vSession.User, "home") : vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);

                    #endregion

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

                        string credentialsError = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "22")).Text;
                        
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, credentialsError, MessageTypes.Error, true, true, true, true, false);
                        
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
            finally
            {
                session.CloseConnection();
            }
        }

        protected void btnRenewImageCode_Click(object sender, EventArgs e)
        {
            GenerateRandomCode();
        }

        # endregion
    }
}