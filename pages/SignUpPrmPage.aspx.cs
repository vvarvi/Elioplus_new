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
using System.Data.Linq;
using WdS.ElioPlus.SalesforceDC;

namespace WdS.ElioPlus.pages
{
    public partial class SignUpPrmPage : System.Web.UI.Page
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

        public SignUpPrmPage()
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
                    //Response.Redirect(ControlLoader.Default, false);
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
                else
                {
                    if (!IsPostBack)
                    {
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
            string metaDescription = "";
            string metaKeywords = "";

            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            //HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            //HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            pgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);
            //metaDescription.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? "Login to the " + VendorName + " partner portal. Start working together and collaborate with " + VendorName + "" : "Login to your Vendor's partner portal. Start working together and collaborate with your vendor";
            //metaKeywords.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal" : "Vendor partner portal";
        }

        private void SetLinks()
        {
            try
            {
                aElioplusLogo.HRef = ControlLoader.Default();

                aLogin.HRef = ControlLoader.Login;

                //aFooterHome.HRef = ControlLoader.Default();
                //aFooterPartnerRecruitment.HRef = ControlLoader.ChannelPartnerRecruitment;
                //aFooterVendorManagement.HRef = ControlLoader.ChannelPartners;
                //aFooterPRM.HRef = ControlLoader.PrmSoftware;
                //aFooterPricing.HRef = ControlLoader.Pricing;
                //aFooterBlog.HRef = "http://blog.elioplus.com/";
                //aFooterBlog.Target = "_blank";
                //aFooterFacebook.HRef = "http://www.facebook.com/elioplus";
                //aFooterFacebook.Target = "_blank";
                //aFooterTwitter.HRef = "http://www.twitter.com/elioplus";
                //aFooterTwitter.Target = "_blank";
                //aFooterLinkedin.HRef = "https://www.linkedin.com/company/elio";
                //aFooterLinkedin.Target = "_blank";
                //aFooterInfoEmail.HRef = "mailto:info@elioplus.com";
                //aFooterCopyright.HRef = ControlLoader.About;
                //aFooterTerms.HRef = ControlLoader.Terms;
                //aFooterPrivacy.HRef = ControlLoader.Privacy;
                //aFooterFaq.HRef = ControlLoader.Faq;
                //aFooterSearch.HRef = ControlLoader.Search;
                //aFooterSearchVendors.HRef = ControlLoader.SearchForVendors;        //ControlLoader.SearchByType("vendors");
                //aFooterSearchChannelPartners.HRef = ControlLoader.SearchForChannelPartners;     //ControlLoader.SearchByType("channel-partners");
                //aFooterCaseStudies.HRef = ControlLoader.CaseStudiesPage;
                //aFooterAbout.HRef = ControlLoader.About;
                //aFooterContact.HRef = ControlLoader.ContactUs;
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
                ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "signup", "alternate", "1")).Text;
                ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
                LblSignUpTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "18")).Text;
                //LblSignUpInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "19")).Text;
                //BtnSave.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "20")).Text;
                //LblLogin.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "23")).Text;
                //LblOr.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "24")).Text;

                //LblSocialInfo.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "32")).Text;
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

            UcMessageControlAlert.Visible = false;

            #endregion

            #region Check Data

            int userApplicationType = (int)UserApplicationType.NotRegistered;
            bool isFullRegistered = false;
            bool isRegisteredEmail = false;

            if (!string.IsNullOrEmpty(TbxEmail.Text))
            {
                if (Sql.IsBlackListedDomain(TbxEmail.Text, session) || Sql.IsBlackListedEmail(TbxEmail.Text, session))
                {
                    string message = "Access denied";
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);
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
                    isRegisteredEmail = Sql.IsRegisteredEmail(TbxEmail.Text.Trim(), out userApplicationType, out isFullRegistered, session);

                    if (isRegisteredEmail && userApplicationType == (int)UserApplicationType.ThirdParty)
                        IsThirdPartyPartnerAccount = true;
                }
            }

            if (string.IsNullOrEmpty(TbxUsername.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "25")).Text;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxUsername.Text.Trim().Length < 8)
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "26")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsUsernameCharsValid(TbxUsername.Text.Trim()))
                    {
                        string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "30")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                        return true;
                    }

                    if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount)
                    {
                        if (Sql.ExistUsername(TbxUsername.Text.Trim(), session))
                        {
                            string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "31")).Text;
                            GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                            return true;
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(TbxPassword.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "33")).Text;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim().Length < 8)
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "34")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                if (!Validations.IsPasswordCharsValid(TbxPassword.Text.Trim()))
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "35")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (string.IsNullOrEmpty(TbxRetypePassword.Text.Trim()))
            {
                string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "36")).Text;
                GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                return true;
            }
            else
            {
                if (TbxPassword.Text.Trim() != TbxRetypePassword.Text.Trim())
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "37")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
            }

            if (!IsSubAccount && !IsCollaborationOrThirdPartyPartnerAccount)
            {
                if (string.IsNullOrEmpty(TbxEmail.Text.Trim()))
                {
                    string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "38")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageControlAlert, message, MessageTypes.Error, true, true, true, true, false);

                    return true;
                }
                else
                {
                    if (!Validations.IsEmail(TbxEmail.Text.Trim()))
                    {
                        string message = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "signup", "label", "39")).Text;
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

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (DataHasError()) return;

                if (!IsSubAccount && (!IsCollaborationOrThirdPartyPartnerAccount && !IsThirdPartyPartnerAccount))
                {
                    #region Insert New User

                    vSession.User = GlobalDBMethods.InsertNewUserWithCredentials(TbxUsername.Text.Trim(), TbxPassword.Text.Trim(), TbxEmail.Text.Trim(), (int)UserRegisterType.PrmRegisterType, session);

                    #endregion

                    #region Get Data from Clearbit

                    //vSession.User = Lib.Services.EnrichmentAPI.ClearBit.GetCombinedPersonCompanyByEmail_v2(vSession.User);

                    #endregion

                    if (HttpContext.Current.Request.Url.AbsolutePath.Contains("/community"))
                    {
                        Response.Redirect(ControlLoader.CommunityFullRegistration, false);

                        #region Send Community Notification Email

                        EmailSenderLib.SendNotificationEmailForNewSimpleRegisteredCommunityUser(vSession.User, vSession.Lang, session);

                        #endregion
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.FullRegistrationPrmPage, false);

                        #region Send Notification Email

                        EmailSenderLib.SendNotificationEmailForNewPRMSimpleRegisteredUser(vSession.User, vSession.Lang, session);

                        #endregion
                    }
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

        # endregion
    }
}