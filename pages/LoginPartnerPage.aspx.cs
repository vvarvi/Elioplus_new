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
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.Services.LinkedinAPI;

namespace WdS.ElioPlus.pages
{
    public partial class LoginPartnerPage : System.Web.UI.Page
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
                    UcMessageControlAlert.Visible = false;
                    
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
                            LblTitle.Text = "Sign in to " + vendor.CompanyName + " Partner Portal";
                            LblCreateAccount.Text = (vendor.CompanyName.Length <= 12) ? "Sign up to " + vendor.CompanyName + " Partner Portal" : "Sign up to " + vendor.CompanyName.Substring(0, 12) + "..." + " Partner Portal";
                            //loginPartnerForm.Action = VendorName + "/partner-login";
                        }
                        else
                            Response.Redirect(ControlLoader.Login, false);

                        #endregion

                        SetLinks();
                        UpdateStrings();
                        PageTitle();

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

        private void PageTitle()
        {
            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            pgTitle.InnerText = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal login" : " Your vendor's partner portal login";    //GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView).Replace("{CompanyName}", VendorName);
            metaDescription.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? "Login to the " + VendorName + " partner portal. Start working together and collaborate with " + VendorName + "" : "Login to your Vendor's partner portal. Start working together and collaborate with your vendor";
            metaKeywords.Attributes["content"] = (!string.IsNullOrEmpty(VendorName)) ? VendorName + " partner portal" : "Vendor partner portal";
        }

        private void SetLinks()
        {
            //aFooterHome.HRef = ControlLoader.Default;

            //aFooterContact.HRef = ControlLoader.ContactUs;
            aResetPassword.HRef = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.ForgotPasswordPartner.Replace("{CompanyName}", VendorName) : ControlLoader.ForgotPassword;
            aCreateAccount.HRef = (!string.IsNullOrEmpty(VendorName)) ? ControlLoader.SignUpPartner.Replace("{CompanyName}", VendorName) : ControlLoader.SignUp;

            //aFooterHome.HRef = ControlLoader.Default;
            //aFooterPartnerRecruitment.HRef = ControlLoader.ChannelPartnerRecruitment;
            //aFooterVendorManagement.HRef = ControlLoader.ChannelPartners;
            //aFooterPRM.HRef = ControlLoader.PrmSoftware;
            //aFooterPricing.HRef = ControlLoader.Pricing;
            //aFooterBlog.HRef = "https://medium.com/@elioplus.com";
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

        private void UpdateStrings()
        {
            //LblManagementSoftwareBy.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "100")).Text;
            //ImgElioplusLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "tooltip", "1")).Text;
            //ImgElioplusLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "login", "alternate", "1")).Text;
            //LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "1")).Text;
            //LblForgotPassword.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "2")).Text;
            //BtnLoginPartner.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "3")).Text;
            //CbxRemember.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "4")).Text;

            //LblNoAccount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "5")).Text;
            //LblCreateAccount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "6")).Text;
            //LblOr.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "7")).Text;                
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

        protected void BtnLoginPartner_OnClick(object sender, EventArgs args)
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

                    #region Redirect to Page

                    string path = HttpContext.Current.Request.Url.AbsolutePath;

                    string redirectUrl = ControlLoader.Dashboard(vSession.User, "home"); //ControlLoader.Default;
                    string url = ControlLoader.FullRegistrationPagePartner.Replace("{CompanyName}", VendorName);

                    if (path.EndsWith("partner-login"))
                    {
                        if (vSession.User.AccountStatus != Convert.ToInt32(AccountStatus.Completed))
                            redirectUrl = path.Replace("/partner-login", "/partner-full-registration");
                        else
                            redirectUrl = ControlLoader.Dashboard(vSession.User, "home");  //ControlLoader.Default;
                    }

                    Response.Redirect(redirectUrl, false);

                    #endregion

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
                        //LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "login", "label", "12")).Text;

                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, credentialsError, MessageTypes.Error, true, true, true, true, false);
                        //LblUsernameError.Visible = true;
                        //LblPasswordError.Visible = true;

                        #endregion
                    }
                    else
                    {
                        #region Account Error

                        GlobalMethods.ShowMessageControl(UcMessageControlAlert, responseError, MessageTypes.Error, true, true, true, true, false);
                        //divWarningMessage.Visible = true;
                        //LblWarningMessage.Text = responseError;

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

        #endregion
    }
}