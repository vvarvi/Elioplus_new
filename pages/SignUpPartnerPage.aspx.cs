using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Activities.Statements;
using System.Net;
using System.Web.Services;
using WdS.ElioPlus.Controls.TermsPrivacy;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Objects;
using System.Configuration;
using System.Web;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus.pages
{
    public partial class SignUpPartnerPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private Random random = new Random();

        CaptchaGenerator _captchaGenerator;

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

        public SignUpPartnerPage()
        {
            _captchaGenerator = new CaptchaGenerator();
        }

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

        public bool IsCollaborationOrThirdPartyPartnerAccount
        {
            get
            {
                return ViewState["IsCollaborationOrThirdPartyPartnerAccount"] == null ? false : (bool)ViewState["IsCollaborationOrThirdPartyPartnerAccount"];
            }
            set
            {
                ViewState["IsCollaborationOrThirdPartyPartnerAccount"] = value;
            }
        }

        public bool IsThirdPartyPartnerAccount
        {
            get
            {
                return ViewState["IsThirdPartyPartnerAccount"] == null ? false : (bool)ViewState["IsThirdPartyPartnerAccount"];
            }
            set
            {
                ViewState["IsThirdPartyPartnerAccount"] = value;
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

        protected static string ReCaptcha_Key = ConfigurationManager.AppSettings["ReCaptcha_Key"].ToString();
        protected static string ReCaptcha_Secret = ConfigurationManager.AppSettings["ReCaptcha_Secret"].ToString();

        [WebMethod]
        public static string VerifyCaptcha(string response)
        {
            string url = "https://www.google.com/recaptcha/api/siteverify?secret=" + ReCaptcha_Secret + "&response=" + response;
            return (new WebClient()).DownloadString(url);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

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
                    //Response.Redirect(ControlLoader.Default, false);
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
                else
                {
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
                            LblSignUpTitle.Text = "Enter your details below to sign up to " + vendor.CompanyName + " Partner Portal. It will take less than 2 minutes and it's free. ";
                        }
                        else
                            Response.Redirect(ControlLoader.SignUp, false);

                        #endregion

                        UpdateStrings();
                        SetLinks();
                        PageTitle();
                        //GenerateRandomCode();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                        vSession.LoggedInSubAccountRoleID = 0;
                        vSession.SubAccountEmailLogin = "";
                        vSession.IsAdminRole = false;

                        UcMessageControlAlert.Visible = false;
                        divEmail.Visible = true;
                        TbxUsername.ReadOnly = false;
                        BtnSave.Enabled = true;

                        #region Collaboration or Third Party or Sub Account Registration

                        if (Request.QueryString["verificationViewID"] != null)
                        {
                            if (Request.QueryString["type"] != null)
                            {
                                #region Collaboration User Registration

                                //is collaboration invited partner account registration
                                string userApplicationType = Request.QueryString["type"].ToString();

                                if (userApplicationType != string.Empty && (userApplicationType == (Convert.ToInt32(UserApplicationType.Collaboration)).ToString() || userApplicationType == (Convert.ToInt32(UserApplicationType.ThirdParty)).ToString()))
                                {
                                    IsSubAccount = false;
                                    IsCollaborationOrThirdPartyPartnerAccount = true;
                                }

                                #endregion
                            }
                            else
                            {
                                #region Sub Account User Registration

                                //is sub account registration
                                IsSubAccount = true;
                                IsCollaborationOrThirdPartyPartnerAccount = false;

                                #endregion
                            }

                            UserGuid = Request.QueryString["verificationViewID"].ToString();

                            divEmail.Visible = false;
                            TbxUsername.ReadOnly = true;

                            //string guid = Request.QueryString["verificationViewID"].ToString();

                            try
                            {
                                session.OpenConnection();

                                if (IsSubAccount)
                                {
                                    #region Sub Account Case

                                    ElioUsersSubAccounts subAccount = Sql.GetSubAccountByGuid(UserGuid, session);

                                    if (subAccount != null)
                                    {
                                        if (subAccount.IsConfirmed == 0)
                                        {
                                            TbxUsername.Text = subAccount.Email.Trim();
                                            TbxUsername.ReadOnly = true;
                                        }
                                        else
                                        {
                                            Response.Redirect(ControlLoader.Login, false);
                                        }
                                    }
                                    else
                                    {
                                        IsSubAccount = false;
                                        IsCollaborationOrThirdPartyPartnerAccount = false;
                                        divEmail.Visible = true;
                                        TbxUsername.ReadOnly = false;
                                        TbxUsername.Text = string.Empty;
                                    }

                                    #endregion
                                }
                                else if (IsCollaborationOrThirdPartyPartnerAccount)
                                {
                                    #region Collaboration Account Case

                                    ElioUsers collaborationUser = Sql.GetUserByGuId(UserGuid, session);

                                    if (collaborationUser != null)
                                    {
                                        if (collaborationUser.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                                        {
                                            TbxUsername.Text = collaborationUser.Email.Trim();
                                            TbxUsername.ReadOnly = true;
                                        }
                                        else
                                        {
                                            Response.Redirect(ControlLoader.Login, false);
                                        }
                                    }
                                    else
                                    {
                                        IsSubAccount = false;
                                        IsCollaborationOrThirdPartyPartnerAccount = false;
                                        divEmail.Visible = true;
                                        TbxUsername.ReadOnly = false;
                                        TbxUsername.Text = string.Empty;
                                    }

                                    #endregion
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

        # region Methods

        private void PageTitle()
        {
            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            pgTitle.InnerText = (!string.IsNullOrEmpty(VendorName)) ? "Sign up to the " + VendorName + " partner portal" : "Sign up to the vendor's partner portal";
            metaDescription.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? "Sign up and join the " + VendorName + " partner portal" : "Sign up and join the vendor's partner portal";
            metaKeywords.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal, sign up partner portal" : "Vendor partner portal, sign up partner portal";
        }

        private void SetLinks()
        {
            aLogin.HRef = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.LoginPartner.Replace("{CompanyName}", VendorName) : ControlLoader.Login;
        }

        private void UpdateStrings()
        {
            //ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "signup", "alternate", "1")).Text;
            //ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
            //LblSignUpTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "18")).Text;
            //LblSignUpInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "19")).Text;
            BtnSave.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "20")).Text;
            //LblAgreeTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "21")).Text;
            //LblAgreeTerms2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "49")).Text;
            //LblTermsAndServices.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "48")).Text;
            //LblPrivacy2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "50")).Text;
            //LblHaveAccount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "22")).Text;
            //LblHaveAccount2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "52")).Text;
            //LblLogin.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "23")).Text;
            //LblOr.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "24")).Text;
            //LblSocialInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "32")).Text;
            TbxUsername.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "27")).Text;
            TbxPassword.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "28")).Text;
            TbxRetypePassword.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "28")).Text;
            TbxEmail.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "29")).Text;
        }

        private bool DataHasError()
        {
            #region Reset Controls

            UcMessageControlAlert.Visible = false;

            #endregion

            #region Check Data

            int userApplicationType = (int)UserApplicationType.NotRegistered;
            bool isFullRegistered = false;
            bool isRegisteredEmail = false;

            if (!string.IsNullOrEmpty(TbxEmail.Text.Trim()))
            {
                if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
                {
                    string message = "Access denied";
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);
                    BtnSave.Enabled = false;

                    return true;
                }
            }

            if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount)
            {
                if (!string.IsNullOrEmpty(TbxEmail.Text.Trim()) && Validations.IsEmail(TbxEmail.Text.Trim()))
                {
                    isRegisteredEmail = Sql.IsRegisteredEmail(TbxEmail.Text.Trim(), out userApplicationType, out isFullRegistered, session);

                    if (isRegisteredEmail && userApplicationType == (int)UserApplicationType.ThirdParty)
                    {
                        IsThirdPartyPartnerAccount = true;
                    }
                }
            }

            if (string.IsNullOrEmpty(TbxUsername.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "25")).Text;
                //LblUsernameError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxUsername.Text.Trim().Length < 8)
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "26")).Text;
                    //LblUsernameError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsUsernameCharsValid(TbxUsername.Text.Trim()))
                    {
                        string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "30")).Text;
                        //LblUsernameError.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                        return true;
                    }

                    if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount)
                    {
                        if (Sql.ExistUsername(TbxUsername.Text.Trim(), session))
                        {
                            string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "31")).Text;
                            //LblUsernameError.Visible = true;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                            return true;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(TbxPassword.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "33")).Text;
                //LblPasswordError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim().Length < 8 || TbxPassword.Text.Trim().Length > 15)
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "34")).Text;
                    //LblPasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                if (!Validations.IsPasswordCharsValid(TbxPassword.Text.Trim()))
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "35")).Text;
                    //LblPasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (string.IsNullOrEmpty(TbxRetypePassword.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "36")).Text;
                //LblRetypePasswordError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim() != TbxRetypePassword.Text.Trim())
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "37")).Text;
                    //LblRetypePasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount)
            {
                if (string.IsNullOrEmpty(TbxEmail.Text.Trim()))
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "38")).Text;
                    //LblEmailError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsEmail(TbxEmail.Text.Trim()))
                    {
                        string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "39")).Text;
                        //LblEmailError.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                        return true;
                    }

                    //int userApplicationType = (int)UserApplicationType.NotRegistered;
                    //bool isFullRegistered = false;

                    //bool isRegisteredEmail = Sql.IsRegisteredEmail(TbxEmail.Text.Trim(), out userApplicationType, out isFullRegistered, session);

                    if (isRegisteredEmail)
                    {
                        if (userApplicationType == (int)UserApplicationType.Elioplus)
                        {
                            string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "40")).Text;
                            //LblEmailError.Visible = true;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                            return true;
                        }
                        else if (userApplicationType == (int)UserApplicationType.ThirdParty)
                        {
                            IsThirdPartyPartnerAccount = true;
                            return false;
                        }
                    }

                    //if (Sql.ExistEmail(TbxEmail.Text.Trim(), session))
                    //{
                    //    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "40")).Text;
                    //    LblEmailError.Visible = true;
                    //    return true;
                    //}
                }
            }

            #endregion

            return false;
        }

        # endregion

        # region Buttons

        protected void btnRenewImageCode_Click(object sender, EventArgs e)
        {
            //VisiblePanel.Value = "0";
            //GenerateRandomCode();
        }

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                #region Captcha

#if !bypassCaptcha
                //                if (CaptchaKey != null)
                //                {
#if !skipCaptcha
                //                    if (ImageCode.Text == "")
                //                    {
                //                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "21")).Text, MessageTypes.Error, true, true, true, true, false);

                //                        this.ImageCode.Text = "";
                //                        return;
                //                    }
                //                    else if (_captchaGenerator.DencryptImageKey(CaptchaKey.ToString()) != ImageCode.Text)
                //                    {
                //                        /* Wrong Captcha */
                //                        GenerateRandomCode();
                //                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "20")).Text, MessageTypes.Error, true, true, true, true, false);

                //                        this.ImageCode.Text = "";
                //                        return;
                //                    }
#endif
                //                }
#endif

                #endregion

                session.OpenConnection();

                if (DataHasError()) return;

                if (!IsSubAccount && (!IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount))
                {
                    #region Insert New User

                    ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                    if (vendor != null)
                    {
                        vSession.User = GlobalDBMethods.InsertNewUserWithCredentials(TbxUsername.Text.Trim(), TbxPassword.Text.Trim(), TbxEmail.Text.Trim(), (int)UserRegisterType.PrmRegisterType, session);

                        if (vSession.User != null)
                        {
                            bool successInsert = GlobalDBMethods.AddNewCollaborationUserSignUpToPRM(vendor, vSession.User, Request, session);

                            if (!successInsert)
                            {
                                //divWarningMessage.Visible = true;
                                string message = "Sorry, sign up could not be completed successfully";
                                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                                return;
                            }
                        }
                    }
                    else
                    {
                        //divWarningMessage.Visible = true;
                        string message = "Sorry, sign up could not continue. Please contact with us.";
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    #endregion

                    string FullRegistrationPartnerUrl = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.FullRegistrationPagePartner.Replace("{CompanyName}", VendorName) : ControlLoader.FullRegistrationPrmPage;

                    Response.Redirect(FullRegistrationPartnerUrl, false);

                    #region Send Notification Email

                    EmailSenderLib.SendNotificationEmailForNewVendorPartnerSimpleRegisteredUser(vSession.User, VendorName, vSession.Lang, session);

                    #endregion
                }
                else
                {
                    if (IsSubAccount)
                    {
                        #region Sub Account

                        if (!string.IsNullOrEmpty(UserGuid))
                        {
                            ElioUsersSubAccounts subAccount = Sql.GetSubAccountByGuid(UserGuid, session);

                            if (subAccount != null)
                            {
                                #region Update User Sub Account

                                subAccount.Password = TbxPassword.Text.Trim();
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

                                #endregion

                                #region User In Session

                                vSession.User = Sql.GetUserById(subAccount.UserId, session);
                                vSession.LoggedInSubAccountRoleID = subAccount.TeamRoleId;
                                vSession.SubAccountEmailLogin = subAccount.Email;

                                #endregion
                            }
                        }

                        #endregion

                        Response.Redirect(vSession.Page = (HttpContext.Current.Request.Url.AbsolutePath.Contains("/community")) ? ControlLoader.CommunityFullRegistration : ControlLoader.Dashboard(vSession.User, "team"), false);
                    }
                    else if (IsCollaborationOrThirdPartyPartnerAccount || IsThirdPartyPartnerAccount)
                    {
                        #region Collaboration or Third Party Partner Account

                        if (!string.IsNullOrEmpty(UserGuid) || IsThirdPartyPartnerAccount)
                        {
                            ElioUsers partnerAccount = new ElioUsers();

                            if (!string.IsNullOrEmpty(UserGuid))
                                partnerAccount = Sql.GetUserByGuId(UserGuid, session);
                            else if (IsThirdPartyPartnerAccount)
                                partnerAccount = Sql.GetUserByEmail(TbxEmail.Text.Trim(), session);

                            if (partnerAccount != null)
                            {
                                #region Update User Account

                                if (partnerAccount.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                                {
                                    partnerAccount.Password = TbxPassword.Text.Trim();
                                    partnerAccount.PasswordEncrypted = MD5.Encrypt(partnerAccount.Password);
                                    partnerAccount.LastUpdated = DateTime.Now;
                                    partnerAccount.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                                    partnerAccount.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                                    partnerAccount.IsPublic = Convert.ToInt32(AccountStatus.Public);
                                    partnerAccount.FirstName = string.Empty;
                                    partnerAccount.LastName = string.Empty;
                                    partnerAccount.PersonalImage = string.Empty;
                                    partnerAccount.Position = string.Empty;
                                    partnerAccount.LinkedInUrl = string.Empty;
                                    partnerAccount.LinkedinId = string.Empty;
                                }
                                else
                                {
                                    partnerAccount.Username = TbxUsername.Text.Trim();
                                    partnerAccount.UsernameEncrypted = MD5.Encrypt(partnerAccount.Username);
                                    partnerAccount.Password = TbxPassword.Text.Trim();
                                    partnerAccount.PasswordEncrypted = MD5.Encrypt(partnerAccount.Password);
                                    partnerAccount.LastUpdated = DateTime.Now;
                                    partnerAccount.IsPublic = Convert.ToInt32(AccountStatus.Public);
                                    partnerAccount.UserRegisterType = Convert.ToInt32(UserRegisterType.PrmRegisterType);
                                    partnerAccount.UserApplicationType = Convert.ToInt32(UserApplicationType.Collaboration);
                                }

                                DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                                loader.Update(partnerAccount);

                                #endregion

                                #region User In Session

                                vSession.User = Sql.GetUserById(partnerAccount.Id, session);

                                #endregion

                                #region Reset Properties

                                IsThirdPartyPartnerAccount = false;
                                UserGuid = string.Empty;

                                #endregion
                            }
                        }

                        #endregion

                        #region New Way

                        if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                        {
                            string FullRegistrationPartnerUrl = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.FullRegistrationPagePartner.Replace("{CompanyName}", VendorName) : ControlLoader.FullRegistrationPrmPage;

                            Response.Redirect(FullRegistrationPartnerUrl, false);

                            #region Send Notification Email

                            EmailSenderLib.SendNotificationEmailForNewVendorPartnerSimpleRegisteredUser(vSession.User, VendorName, vSession.Lang, session);

                            #endregion
                        }
                        else
                        {
                            ElioUsers vendor = GlobalDBMethods.GetCompanyFromAbsoluteUrl(HttpContext.Current.Request.Url.AbsolutePath, session);
                            if (vendor != null)
                            {
                                bool hasAlreadyGetInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vendor.Id, vSession.User.Email, session);
                                if (!hasAlreadyGetInvitation)
                                {
                                    ElioCollaborationUsersInvitations defaultInvitation = SqlCollaboration.GetUserInvitationById(1, session);

                                    if (defaultInvitation != null)
                                    {
                                        #region Add User Invitation

                                        ElioCollaborationUsersInvitations invitation = new ElioCollaborationUsersInvitations();

                                        invitation.UserId = vSession.User.Id;
                                        invitation.InvSubject = defaultInvitation.InvSubject;
                                        invitation.InvContent = defaultInvitation.InvContent;
                                        invitation.DateCreated = DateTime.Now;
                                        invitation.LastUpdated = DateTime.Now;
                                        invitation.IsPublic = 1;

                                        DataLoader<ElioCollaborationUsersInvitations> loader = new DataLoader<ElioCollaborationUsersInvitations>(session);
                                        loader.Insert(invitation);

                                        #endregion

                                        int collaborationId = SqlCollaboration.GetCollaborationIdFromChannelPartnerInvitation(vSession.User.Id, vSession.User.Email, session);

                                        if (collaborationId < 0)
                                        {
                                            #region Insert Collaboration Vendor/Reseller

                                            ElioCollaborationVendorsResellers newCollaboration = new ElioCollaborationVendorsResellers();

                                            int masterUserId = vendor.Id;
                                            int partnerUserId = vSession.User.Id;

                                            newCollaboration.MasterUserId = masterUserId;
                                            newCollaboration.PartnerUserId = partnerUserId;
                                            newCollaboration.IsActive = 1;
                                            newCollaboration.InvitationStatus = CollaborateInvitationStatus.Pending.ToString();
                                            newCollaboration.Sysdate = DateTime.Now;
                                            newCollaboration.LastUpdated = DateTime.Now;

                                            DataLoader<ElioCollaborationVendorsResellers> loader2 = new DataLoader<ElioCollaborationVendorsResellers>(session);
                                            loader2.Insert(newCollaboration);

                                            #endregion

                                            #region Insert V/R Invitation

                                            ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                            vendorResselerInvitation.UserId = vSession.User.Id;
                                            vendorResselerInvitation.VendorResellerId = newCollaboration.Id;
                                            vendorResselerInvitation.UserInvitationId = invitation.Id;
                                            vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                            vendorResselerInvitation.RecipientEmail = vSession.User.Email;
                                            vendorResselerInvitation.SendDate = DateTime.Now;
                                            vendorResselerInvitation.LastUpdated = DateTime.Now;

                                            DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                            loader3.Insert(vendorResselerInvitation);

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Insert V/R Invitation If Not Already Exist

                                            bool existInvitation = SqlCollaboration.HasInvitationRequestThisEmailBySpecificUser(vendor.Id, vSession.User.Email, session);

                                            if (!existInvitation)
                                            {
                                                ElioCollaborationVendorResellerInvitations vendorResselerInvitation = new ElioCollaborationVendorResellerInvitations();

                                                vendorResselerInvitation.UserId = vendor.Id;
                                                vendorResselerInvitation.VendorResellerId = collaborationId;
                                                vendorResselerInvitation.UserInvitationId = invitation.Id;
                                                vendorResselerInvitation.InvitationStepDescription = CollaborateInvitationStepDescription.Pending.ToString();
                                                vendorResselerInvitation.RecipientEmail = vSession.User.Email;
                                                vendorResselerInvitation.SendDate = DateTime.Now;
                                                vendorResselerInvitation.LastUpdated = DateTime.Now;

                                                DataLoader<ElioCollaborationVendorResellerInvitations> loader3 = new DataLoader<ElioCollaborationVendorResellerInvitations>(session);
                                                loader3.Insert(vendorResselerInvitation);
                                            }

                                            #endregion
                                        }

                                        try
                                        {
                                            #region Send Invitation Email To Vendor

                                            if (vendor != null)
                                            {
                                                EmailSenderLib.CollaborationInvitationEmailFromChannelPartner(vSession.User.UserApplicationType, vendor.Email.Trim(), vendor.CompanyName, vSession.User.CompanyLogo, vSession.User.CompanyName, Request, "en", session);
                                            }

                                            #endregion
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                }
                            }

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                        }

                        #endregion

                        #region Old Way

                        //Response.Redirect(ControlLoader.FullRegistrationPage, false);

                        #region Send Notification Email

                        //EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredUser(vSession.User, vSession.Lang, session);

                        #endregion

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

        # endregion
    }
}