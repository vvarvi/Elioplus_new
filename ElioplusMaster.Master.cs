using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web.UI;

namespace WdS.ElioPlus
{
    public partial class ElioplusMaster : System.Web.UI.MasterPage
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
                ViewState["VendorName"] = Regex.Replace(value, @"[^A-Za-z0-9]+", "_").Trim().ToLower();
            }
        }

        #region curtomers.io API Properties

        //protected int userId = -1;
        //protected string userCompName = "";
        //protected string userCompType = "";
        //protected string userMail = "";
        //protected string userCountry = "";
        //protected string userType = "";
        //protected string userAccountStatus = "";
        //protected long timeSt = 0000000000;
        //protected string userVerticals = "";
        //protected int isPublic = 1;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
                session.OpenConnection();

                //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                //scriptManager.RegisterPostBackControl(aMainMenuUserLogout);
                
                if (!IsPostBack)
                {                    
                    UpdateStrings(); 
                    SetLinks();
                }

                if (vSession.User == null)
                {
                    #region Login by cookies

                    if (Request.Browser.Cookies)
                    {
                        HttpCookie loginCookie = Request.Cookies[CookieName];

                        if (loginCookie != null)
                        {
                            if (loginCookie.Value.Length % 16 == 0)
                            {
                                List<string> usernameencr = Encrypter.SplitStringBySize(loginCookie.Value, loginCookie.Value.Length / 2);

                                string responseError = string.Empty;

                                ElioUsers user = Sql.GetUserByUsernameAndPassword(Encrypter.DecryptStringAes(usernameencr[0], Encrypter.SharedSecret), Encrypter.DecryptStringAes(usernameencr[1], Encrypter.SharedSecret), Encrypter.DecryptStringAes(usernameencr[0], Encrypter.SharedSecret), HttpContext.Current.Session.SessionID, out responseError, vSession.Lang, out string subAccountEmailLogin, out int loggedInRoleID, out bool isAdminRole, session);
                                if (user != null)
                                {
                                    vSession.SubAccountEmailLogin = subAccountEmailLogin;
                                    vSession.LoggedInSubAccountRoleID = loggedInRoleID;
                                    vSession.IsAdminRole = isAdminRole;

                                    if (user.AccountStatus != Convert.ToInt32(AccountStatus.Blocked))
                                    {
                                        LblUser.Text = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? user.CompanyName : user.Username;
                                        iUserFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
                                        iUserNotFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;

                                        liMainMenuLogin.Visible = liMainMenuSignUp.Visible = (user == null) ? true : false;
                                        liMainMenuUser.Visible = liMainMenuStatus.Visible = liMainMenuNotifications.Visible = liMainMenuMessages.Visible = (user == null) ? false : true;

                                        vSession.User = user;

                                        //GlobalDBMethods.UpDateUser(vSession.User, session);
                                    }
                                    else
                                    {
                                        //LblUserBlocked.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "17")).Text;
                                    }
                                }
                                else
                                {
                                    Session.Clear();
                                    loginCookie.Expires = DateTime.Now.AddYears(-30);
                                    Response.Cookies.Add(loginCookie);
                                    Response.Redirect((Request.RawUrl.Contains("/community")) ? ControlLoader.CommunityPosts : ControlLoader.Default(), false);
                                }
                            }
                        }
                    }

                    #endregion
                }

                liMainMenuUserImage.Visible = liMainMenuUser.Visible = liMainMenuStatus.Visible = liMainMenuNotifications.Visible = liMainMenuMessages.Visible = vSession.User != null ? true : false;
                liMainMenuLogin.Visible = liMainMenuSignUp.Visible = vSession.User != null ? false : true;
                //aFooterCommunity.Visible = false;

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                        //SetUserDataForCustomersAPI();

                    iUserFull.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) && (vSession.User.IsPublic == 1) ? true : false;
                    iUserNotFull.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) || (vSession.User.IsPublic == 0) ? true : false;
                    LblMainMenuAlert.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "18")).Text : (vSession.User.IsPublic == 0) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "19")).Text : "Your account is OK!";
                    aMainMenuAlert.HRef = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : (vSession.User.IsPublic == 0) ? ControlLoader.ContactUs : ControlLoader.Dashboard(vSession.User, "home");

                    LblUser.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? vSession.User.CompanyName : vSession.User.Username;
                    liMainMenuProfile.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;

                    if (liMainMenuProfile.Visible)
                    {
                        LblUserProfile.Text = "Profile";
                        aMainMenuUserProfile.HRef = ControlLoader.Profile(vSession.User);
                    }

                    LblUserDashboard.Text = "Dashboard";
                    aMainMenuUserDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
                    liMainMenuRegister.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;
                    LblUserRegister.Text = "Register";
                    aMainMenuUserRegister.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
                    liMainMenuAdmin.Visible = Sql.IsUserAdministrator(vSession.User.Id, session);
                    LblMainMenuAdmin.Text = "Administrator";
                    aMainMenuAdmin.HRef = ControlLoader.AdminPage;
                    LblUserLogout.Text = "Log out";

                    int newMessages = Sql.GetCompanyMessages(vSession.User.Id, 1, 0, session);
                    iMainMenuMessages.Visible = newMessages > 0 ? true : false;
                    iMainMenuNoMessages.Visible = newMessages > 0 ? false : true;
                    spanUserMessages.InnerText = newMessages > 0 ? newMessages.ToString() : "0";
                    spanUserNoMessages.InnerText = "0";
                    LblMainMenuMessages.Text = newMessages > 0 ? "You have " + newMessages + " new messages!" : "No messages";
                    aMainMenuMessages.HRef = ControlLoader.Dashboard(vSession.User, "messages/inbox");

                    int isNew = 1;
                    int isDeleted = 0;
                    int companyLeads = Sql.GetCompanyRecentLeadsForCurrentMonthByIsNewIsDeletedStatus(vSession.User, isNew, isDeleted, session);
                    iMainMenuUserLeads.Visible = companyLeads > 0 ? true : false;
                    iMainMenuUserNoLeads.Visible = companyLeads > 0 ? false : true;
                    spanUserLeads.InnerText = companyLeads > 0 ? companyLeads.ToString() : "0";
                    spanUserNoLeads.InnerText = "0";
                    LblMainMenuUserLeads.Text = companyLeads > 0 ? "You have " + companyLeads + " new leads" : "No leads";
                    aMainMenuUserLeads.HRef = ControlLoader.Dashboard(vSession.User, "leads");

                    bool hasActiveSubscription = true;
                    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId) && vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType)
                    {
                        hasActiveSubscription = false;
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId) && vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                        {
                            try
                            {
                                hasActiveSubscription = true;        //Sql.HasActivePacketSubscription(vSession.User.Id, vSession.User.CustomerStripeId, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(session, ex, Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                    }

                    //hasActiveSubscription = Sql.HasActivePacketSubscription(vSession.User.Id, session);       //Sql.GetUserLastOrderByStatus(vSession.User.Id, Convert.ToInt32(OrderStatus.Canceled), session);
                    if (!hasActiveSubscription)
                    {
                        iMainMenuNotifications.Visible = true;
                        spanUserNotifications.InnerText = "1";
                        iMainMenuNoNotifications.Visible = false;
                        LblMainMenuNotifications.Text = "You have canceled your premium/service plan";
                        aMainMenuNotifications.HRef = ControlLoader.Dashboard(vSession.User, "billing");
                    }
                    else
                    {
                        iMainMenuNoNotifications.Visible = true;
                        spanUserNoNotifications.InnerText = "0";
                        iMainMenuNotifications.Visible = false;
                        LblMainMenuNotifications.Text = "No notifications!";
                    }
                }

                PageTitle();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(session, ex, Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        protected void SetUserDataForCustomersAPI()
        {
            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {
                #region curtomers.io API

                //userId = vSession.User.Id;
                //userCompName = vSession.User.CompanyName;
                //userCompType = vSession.User.CompanyType;
                //userMail = vSession.User.Email;
                //userCountry = vSession.User.Country;
                //isPublic = vSession.User.IsPublic;

                //switch (vSession.User.BillingType)
                //{
                //    case 1:
                //        userType = "Free Packet Type User";
                //        break;
                //    case 2:
                //        userType = "Premium Packet Type User";
                //        break;
                //    case 3:
                //        userType = "StartUp Packet Type User";
                //        break;
                //    case 4:
                //        userType = "Growth Packet Type User";
                //        break;
                //    case 5:
                //        userType = "Enterprise Packet Type User";
                //        break;
                //    default:
                //        userType = "Free Packet Type User";
                //        break;
                //}

                //userAccountStatus = (vSession.User.AccountStatus == (int)AccountStatus.Completed) ? "Full Registered" : "Simple Registered";
                //timeSt = (vSession.User.SysDate.ToUniversalTime().Ticks - 621355968000000000) / 10000000;

                //List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(vSession.User.Id, session);
                //foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
                //{
                //    userVerticals += subcategory.DescriptionSubcategory + ",";
                //}

                //userVerticals = (profileSubcategories.Count > 0) ? userVerticals.Substring(0, userVerticals.Length - 1) : "-";

                #endregion
            }

            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["UseCustomersAPI"]))
            {
                if (ConfigurationManager.AppSettings["UseCustomersAPI"].ToString() == "true")
                {
                    List<ElioUsers> users = Sql.GetPublicFullRegisteredUsersJoinCustomersAPI(AccountPublicStatus.IsPublic, AccountStatus.Completed, session);
                    if (users.Count > 0 && vSession.LoggedInUsersCount <= 100)
                    {
                        Response.Redirect(ControlLoader.Login, false);
                    }
                }
            }
        }

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            if (PgTitle.InnerText == "")
                PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);
        }
		
        private void SetLinks()
        {
            try
            {
                if (vSession.User != null)
                {
                    VendorName = SqlCollaboration.GetVendorNameByResellerUserId(vSession.User.Id, session);
                    if (VendorName != "")
                        aMainMenuUserLogout.HRef = ControlLoader.LogoutPartner.Replace("{CompanyName}", VendorName);
                    else
                        aMainMenuUserLogout.HRef = ControlLoader.Login;

                    //if (vSession.User.AccountStatus == (int)AccountStatus.Completed && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic)
                    //{
                    //    aUserBackLink.Visible = true;
                    //    aUserBackLink.HRef = ControlLoader.Profile(vSession.User);
                    //    aUserBackLink.Target = "_blank";
                    //}
                    //else
                    //    aUserBackLink.Visible = false;
                }

                aLogo.HRef = aMainMenuHome.HRef = aFooterHome.HRef = ControlLoader.Default();
                //aMainMenuHowItWorks.HRef = aFooterHowItWorks.HRef = ControlLoader.HowItWorks;
                aMainMenuPartnerRecruitment.HRef = aFooterPartnerRecruitment.HRef = ControlLoader.ChannelPartnerRecruitment;
                aMainMenuPRM.HRef = aFooterPRM.HRef = ControlLoader.PrmSoftware;
                aMainMenuVendorManagement.HRef = aFooterVendorManagement.HRef = ControlLoader.IntentSignals;        //ControlLoader.ChannelPartners;
                aMainMenuMAMarketplace.HRef = aFooterMAMarketplace.HRef = ControlLoader.MAMarketplace;
                aImpartner.HRef = ControlLoader.ResourcesAlternativesImpartnerPage;
                aPartnerStack.HRef = ControlLoader.ResourcesAlternativesPartnerStackPage;
                aAllbound.HRef = ControlLoader.ResourcesAlternativesAllboundPage;
                aZiftSolutions.HRef = ControlLoader.ResourcesAlternativesZiftSolutionsPage;
                aSalesforcePRM.HRef = ControlLoader.ResourcesAlternativesSalesforceCommunitiesPage;
                aMagentrix.HRef = ControlLoader.ResourcesAlternativesMindmatrixPage;
                aMyprm.HRef = ControlLoader.ResourcesAlternativesMyPrmPage;
                aMainMenuSearch.HRef = ControlLoader.Search;
                aMainMenuSearchAll.HRef = ControlLoader.SearchAllCategories;
                aMainMenuCaseStudies.HRef = ControlLoader.CaseStudiesPage;
                aMainMenuPricing.HRef = aFooterPricing.HRef = ControlLoader.Pricing;
                aFooterResources.HRef = ControlLoader.ResourcesPage;
                aMainMenuBlog.HRef = aFooterBlog.HRef = "https://medium.com/@elioplus";
                aMainMenuBlog.Target = "_blank";
                aFooterBlog.Target = "_blank";
                
                aFooterFacebook.HRef = "http://www.facebook.com/elioplus";
                aFooterFacebook.Target = "_blank";
                aFooterTwitter.HRef = "http://www.twitter.com/elioplus";
                aFooterTwitter.Target = "_blank";
                aFooterLinkedin.HRef = "https://www.linkedin.com/company/elio";
                aFooterLinkedin.Target = "_blank";
                //aFooterGoogle.HRef = "https://plus.google.com/u/0/b/108177376326631142433/108177376326631142433/posts";
                //aFooterGoogle.Target = "_blank";
                aFooterInfoEmail.HRef = "mailto:info@elioplus.com";
                aFooterCopyright.HRef = ControlLoader.About;
                aMainMenuLogin.HRef = ControlLoader.Login;
                aMainMenuSignUp.HRef = ControlLoader.SignUp;
                aFooterTerms.HRef = ControlLoader.Terms;
                aFooterPrivacy.HRef = ControlLoader.Privacy;
                aFooterMyData.HRef = ControlLoader.MyData;
                aFooterFaq.HRef = ControlLoader.Faq;
                //aFooterCommunity.HRef = ControlLoader.CommunityPosts;
                //aMainMenuCommunity.HRef = ControlLoader.CommunityPosts;
                aFooterSearch.HRef = ControlLoader.Search;
                aFooterSearchVendors.HRef = ControlLoader.SearchForVendors;        //ControlLoader.SearchByType("vendors");
                aFooterSearchChannelPartners.HRef = ControlLoader.SearchForChannelPartners;     //ControlLoader.SearchByType("channel-partners");
                aFooterSearchChannelPartnerships.HRef = ControlLoader.SearchForChannelPartnerships;
                aFooterCaseStudies.HRef = ControlLoader.CaseStudiesPage;
                aFooterAbout.HRef = ControlLoader.About;
                aFooterContact.HRef = ControlLoader.ContactUs;
                aIntentSignals.HRef = ControlLoader.IntentSignals;
                aReferralSoftware.HRef = ControlLoader.ReferalSoftware;
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
                ImgLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "tooltip", "1")).Text;
                ImgLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "alternate", "1")).Text;
                LblHome.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "1")).Text;
                LblProducts.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "15")).Text;
                LblPartnerRecruitment.Text = LblFooterPartnerRecruitment.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "16")).Text;
                LblPRM.Text = LblFooterPRM.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "17")).Text;
                LblVendorManagement.Text = LblFooterVendorManagement.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "18")).Text;
                LblSearch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "3")).Text;
                LblSearchAll.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "14")).Text;
                LblCaseStudies.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "13")).Text;
                LblPricing.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "4")).Text;
                LblBlog.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "5")).Text;
                //LblAbout.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "6")).Text;
                //LblContact.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "7")).Text;              
                //LtrTermsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "literal", "1")).Text;
                //BtnCloseTerms.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "10")).Text;
                LblLogin.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "literal", "11")).Text;
                LblSignUp.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "literal", "12")).Text;
                //RwndTerms.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "label", "9")).Text;
                //BtnClosePrivacy.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "12")).Text;
                //RwndPrivacy.Title = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                //LtrPrivacyTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "privacy", "label", "10")).Text;
                LblMore.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "8")).Text;
                LblUserDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "10")).Text;
                LblUserRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "11")).Text;
                LblUserLogout.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "12")).Text;
                LblMAMarketplace.Text = LblFooterMAMarketplace.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "19")).Text;

                LblFooterHome.Text = "Home";                
                LblFooterPricing.Text = "Pricing";
                LblResources.Text = "Resources";
                LblFooterSearch.Text = "Search";
                LblFooterSearchVendors.Text = "Search vendors";
                LblFooterSearchChannelPartners.Text = "Search channel partners";
                LblFooterSearchChannelPartnerships.Text = "Browse by partnership";
                LblFooterCaseStudies.Text = "Case studies";
                LblFooterContact.Text = "Contact";
                LblFooterBlog.Text = "Blog";
                LblFooterTerms.Text = "Terms & Conditions";
                LblFooterPrivacy.Text = "Privacy Statement";
                LblFooterMyData.Text = "Do Not Sell My Data";
                LblFooterFaq.Text = "Faq";
                LblFooterCompany.Text = "Company";
                LblFooterProducts.Text = "Products";
                LblFooterBrowseCompanies.Text = "Browse Companies";
                LblFooterContactInfo.Text = "Contact Information";
                LblFooterAbout.Text = "About Us";
                LtrInfoEmailText.Text = "Contact us at: ";
                LtrInfoEmail.Text = "info@elioplus.com";
                LtrAddressGR.Text = "Address: 33 Saronikou St , 163 45, Ilioupoli, Athens, Greece";
                //LtrTelGR.Text = "Tel: +30 2177367850";
                LtrAddressUS.Text = "Address: 108 West 13th Street, Suite 105 Wilmington, Delaware 19801";                
                LtrCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("footer", "sitemaster", "literal", "9")).Text;   //"Copyright Elioplus @ 2015. Designed by ";
                LtrElioplusTeam.Text = "Elioplus Team";
                LblIntentSignals.Text = "Intent Signals";
                LblReferralSoftware.Text = "Referral Software";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void Logout_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                string logoutUrl = ControlLoader.Default();

                if (vSession.User != null)
                {
                    //if (vSession.User.UserRegisterType == 2)
                    //{

                    VendorName = SqlCollaboration.GetVendorNameByResellerUserId(vSession.User.Id, session);
                    if (VendorName != "")
                        logoutUrl = ControlLoader.LogoutPartner.Replace("{CompanyName}", VendorName);
                    else
                        logoutUrl = ControlLoader.Login;

                    //}
                    Logger.Info("User {0}, logged out", vSession.User.Id);
                }

                //////Global.RemoveFromSessionsList(vSession.User.CurrentSessionId);
                //Global.ClearSessionsList();
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

                Response.Redirect(logoutUrl, false);
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

        protected void BtnFooterTerms_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ControlLoader.Terms, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnFooterPrivacy_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ControlLoader.Privacy, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnMyData_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect(ControlLoader.MyData, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        //protected void ImgBtnGr_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        vSession.Lang = "el";

        //        Response.Redirect(vSession.Page, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void ImgBtnEn_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {
        //        vSession.Lang = "en";

        //        Response.Redirect(vSession.Page, false);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void ImgBtnFr_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void ImgBtnIt_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        //protected void ImgBtnDe_OnClick(object sender, EventArgs args)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
        //    }
        //}

        #endregion
    }
}