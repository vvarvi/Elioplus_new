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
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Objects;
using System.Configuration;
using System.Web;

namespace WdS.ElioPlus.pages
{
    public partial class SignUpPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        private Random random = new Random();

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

                if (vSession.User != null)
                {
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        SetLinks();
                        //GenerateRandomCode();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                        vSession.LoggedInSubAccountRoleID = 0;
                        vSession.SubAccountEmailLogin = "";
                        vSession.IsAdminRole = false;
                        BtnSave.Enabled = true;

                        divEmail.Visible = true;
                        TbxUsername.ReadOnly = false;

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

        #region Methods

        private void SetLinks()
        {
            try
            {
                aElioplusLogo.HRef = ControlLoader.Default();

                aLogin.HRef = ControlLoader.Login;
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
                //ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "signup", "alternate", "1")).Text;
                //ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
                //LblSignUpTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "18")).Text;
                //LblSignUpInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "19")).Text;
                //BtnSave.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "20")).Text;
                //LblAgreeTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "21")).Text;
                //LblAgreeTerms2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "49")).Text;
                //LblTermsAndServices.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "48")).Text;
                //LblPrivacy2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "50")).Text;
                ////LblHaveAccount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "22")).Text;
                //LblLogin.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "54")).Text;
                ////LblOr.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "24")).Text;
                ////LblSocialInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "32")).Text;


                TbxUsername.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "27")).Text;
                TbxPassword.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "28")).Text;
                TbxRetypePassword.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "28")).Text;
                TbxEmail.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "29")).Text;
                //LtrPrivacyTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //RwndPrivacy.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //BtnClosePrivacy.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "12")).Text;
                //RwndTerms.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "9")).Text;
                //BtnCloseTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "10")).Text;
                //LtrTermsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "literal", "1")).Text;
                //LblWarning.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "8")).Text;

                //LblFooterPartnerRecruitment.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "16")).Text;
                //LblFooterPRM.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "17")).Text;
                //LblFooterVendorManagement.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "18")).Text;

                //LblFooterHome.Text = "Home";
                //LblFooterPricing.Text = "Pricing";
                //LblFooterSearch.Text = "Search";
                //LblFooterSearchVendors.Text = "Search vendors";
                //LblFooterSearchChannelPartners.Text = "Search channel partners";
                //LblFooterCaseStudies.Text = "Case studies";
                //LblFooterContact.Text = "Contact";
                //LblFooterBlog.Text = "Blog";
                //LblFooterTerms.Text = "Terms & Conditions";
                //LblFooterPrivacy.Text = "Privacy Statement";
                //LblFooterFaq.Text = "Faq";
                //LblFooterCompany.Text = "Company";
                //LblFooterProducts.Text = "Products";
                //LblFooterBrowseCompanies.Text = "Browse Companies";
                //LblFooterContactInfo.Text = "Contact Information";
                //LblFooterAbout.Text = "About Us";
                //LtrInfoEmailText.Text = "Contact us at: ";
                //LtrInfoEmail.Text = "info@elioplus.com";
                //LtrAddressGR.Text = "Address: 33 Saronikou St , 163 45, Ilioupoli, Athens, Greece";
                ////LtrTelGR.Text = "Tel: +30 2177367850";
                //LtrAddressUS.Text = "Address: 3511 Silverside Road, Suite 105, Wilmington, DE 19810";
                //LtrCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("footer", "sitemaster", "literal", "9")).Text;   //"Copyright Elioplus @ 2015. Designed by ";
                //LtrElioplusTeam.Text = "Elioplus Team";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private bool DataHasError()
        {
            #region Reset Controls

            //LblUsernameError.Text = string.Empty;
            //LblPasswordError.Text = string.Empty;
            //LblRetypePasswordError.Text = string.Empty;
            //LblEmailError.Text = string.Empty;

            //LblUsernameError.Visible = false;
            //LblPasswordError.Visible = false;
            //LblRetypePasswordError.Visible = false;
            //LblEmailError.Visible = false;

            #endregion

            #region Check Data

            int userApplicationType = (int)UserApplicationType.NotRegistered;
            bool isFullRegistered = false;
            bool isRegisteredEmail = false;

            if (!string.IsNullOrEmpty(TbxEmail.Text.Trim()))
            {
                bool isBlackListed = Sql.IsBlackListedDomain(TbxEmail.Text, session);
                if (isBlackListed)
                {
                    string emailError = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "53")).Text;
                    //LblEmailError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, emailError, MessageTypes.Error, true, true, true, true, false);
                    BtnSave.Enabled = false;
                    return true;
                }

                isBlackListed = Sql.IsBlackListedEmail(TbxEmail.Text, session);
                if (isBlackListed)
                {
                    string emailError = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "53")).Text;
                    //LblEmailError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, emailError, MessageTypes.Error, true, true, true, true, false);
                    BtnSave.Enabled = false;
                    return true;
                }

                if (GlobalMethods.IsForbiddenDomain(TbxEmail.Text.Trim()))
                {
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, "Please use a valid business email address", MessageTypes.Error, true, true, true, true, false);
                    BtnSave.Enabled = false;
                    return true;
                }
            }

            if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount)
            {
                if (!string.IsNullOrEmpty(TbxEmail.Text.Trim()) && Validations.IsEmail(TbxEmail.Text.Trim()))
                {
                    #region Old Way by webconfig

                    //string[] blackListedEmailList = (ConfigurationManager.AppSettings["BlackListedEmails"] != "") ? ConfigurationManager.AppSettings["BlackListedEmails"].ToString().Split(',').ToArray() : null;
                    //if (blackListedEmailList != null && blackListedEmailList.Length > 0)
                    //{
                    //    foreach (string email in blackListedEmailList)
                    //    {
                    //        if (email.Trim() == TbxEmail.Text.Trim())
                    //        {
                    //            LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "53")).Text;
                    //            LblEmailError.Visible = true;
                    //            return true;
                    //        }
                    //    }
                    //}

                    #endregion

                    isRegisteredEmail = Sql.IsRegisteredEmail(TbxEmail.Text.Trim(), out userApplicationType, out isFullRegistered, session);

                    if (isRegisteredEmail && userApplicationType == (int)UserApplicationType.ThirdParty)
                        IsThirdPartyPartnerAccount = true;
                }
            }

            if (string.IsNullOrEmpty(TbxUsername.Text.Trim()))
            {
                string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "25")).Text;
                //LblUsernameError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxUsername.Text.Trim().Length < 8)
                {
                    string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "26")).Text;
                    //blUsernameError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsUsernameCharsValid(TbxUsername.Text.Trim()))
                    {
                        string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "30")).Text;
                        //LblUsernameError.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                        return true;
                    }

                    if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount)
                    {
                        if (Sql.ExistUsername(TbxUsername.Text.Trim(), session))
                        {
                            string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "31")).Text;
                            //LblUsernameError.Visible = true;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                            return true;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(TbxPassword.Text.Trim()))
            {
                string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "33")).Text;
                //LblPasswordError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim().Length < 8 || TbxPassword.Text.Trim().Length > 15)
                {
                    string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "34")).Text;
                    //blPasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                if (!Validations.IsPasswordCharsValid(TbxPassword.Text.Trim()))
                {
                    string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "35")).Text;
                    //LblPasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (string.IsNullOrEmpty(TbxRetypePassword.Text.Trim()))
            {
                string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "36")).Text;
                //LblRetypePasswordError.Visible = true;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim() != TbxRetypePassword.Text.Trim())
                {
                    string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "37")).Text;
                    //LblRetypePasswordError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount)
            {
                if (string.IsNullOrEmpty(TbxEmail.Text.Trim()))
                {
                    string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "38")).Text;
                    //LblEmailError.Visible = true;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsEmail(TbxEmail.Text.Trim()))
                    {
                        string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "39")).Text;
                        //LblEmailError.Visible = true;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

                        return true;
                    }

                    //int userApplicationType = (int)UserApplicationType.NotRegistered;
                    //bool isFullRegistered = false;

                    //bool isRegisteredEmail = Sql.IsRegisteredEmail(TbxEmail.Text.Trim(), out userApplicationType, out isFullRegistered, session);

                    if (isRegisteredEmail)
                    {
                        if (userApplicationType == (int)UserApplicationType.Elioplus)
                        {
                            string error = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "40")).Text;
                            //LblEmailError.Visible = true;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, error, MessageTypes.Error, true, true, true, true, false);

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

        private void GenerateRandomCode()
        {
            //string s = "";
            //for (int i = 0; i < 6; i++)
            //    s = String.Concat(s, this.random.Next(10).ToString());

            //string encryptedCode = _captchaGenerator.EncryptImageKey(s);
            //System.Diagnostics.Debug.WriteLine(encryptedCode);
            //CaptchaKey = encryptedCode;
            //captcha.ImageUrl = ResolveUrl("~/JpegImage.aspx?s=" + Server.UrlEncode(encryptedCode));
        }

        # endregion

        # region Buttons

        protected void btnRenewImageCode_Click(object sender, EventArgs e)
        {
            GenerateRandomCode();
        }

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                #region Captcha

                //#if !bypassCaptcha
                //                if (CaptchaKey != null)
                //                {
                //#if !skipCaptcha
                //                    if (ImageCode.Text == "")
                //                    {
                //                        divWarningMessage.Visible = true;
                //                        LblWarningMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "21")).Text;
                //                        this.ImageCode.Text = "";
                //                        return;
                //                    }
                //                    else if (_captchaGenerator.DencryptImageKey(CaptchaKey.ToString()) != ImageCode.Text)
                //                    {
                //                        /* Wrong Captcha */
                //                        GenerateRandomCode();
                //                        divWarningMessage.Visible = true;
                //                        LblWarningMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "20")).Text;
                //                        this.ImageCode.Text = "";
                //                        return;
                //                    }
                //#endif
                //                }
                //#endif

                #endregion

                session.OpenConnection();

                if (DataHasError()) return;

                if (!IsSubAccount && (!IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount))
                {
                    #region Insert New User

                    vSession.User = GlobalDBMethods.InsertNewUserWithCredentials(TbxUsername.Text.Trim(), TbxPassword.Text.Trim(), TbxEmail.Text.Trim(), 1, session);

                    #endregion

                    #region Get Data from Clearbit

                    //vSession.User = Lib.Services.EnrichmentAPI.ClearBit.GetCombinedPersonCompanyByEmail_v2(vSession.User);

                    #endregion

                    Response.Redirect(ControlLoader.FullRegistrationPage, false);

                    #region Send Notification Email

                    EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredUser(vSession.User, vSession.Lang, session);

                    #endregion
                }
                else if (IsSubAccount)
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

                    Response.Redirect((HttpContext.Current.Request.Url.AbsolutePath.Contains("/community")) ? ControlLoader.CommunityFullRegistration : ControlLoader.Dashboard(vSession.User, "team"), false);
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

                    Response.Redirect(ControlLoader.FullRegistrationPage, false);

                    #region Send Notification Email

                    EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredUser(vSession.User, vSession.Lang, session);

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

        protected void LnkBtnCreatePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                TbxPassword.Text = WdS.ElioPlus.Lib.Utils.GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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