using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.pages
{
    public partial class Elioplus : System.Web.UI.MasterPage
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



        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                                
                if (!IsPostBack)
                {
                    UpdateStrings();
                    SetLinks();
                    PageTitle();
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
                                        //iUserFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
                                        //iUserNotFull.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;

                                        //liMainMenuUser.Visible = liMainMenuStatus.Visible = liMainMenuNotifications.Visible = liMainMenuMessages.Visible = (user == null) ? false : true;

                                        vSession.User = user;
                                    }
                                    else
                                    {
                                        ////LblUserBlocked.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "17")).Text;
                                    }
                                }
                                else
                                {
                                    Session.Clear();
                                    loginCookie.Expires = DateTime.Now.AddYears(-30);
                                    Response.Cookies.Add(loginCookie);
                                    Response.Redirect(vSession.Page = ControlLoader.Default(), false);
                                }
                            }
                        }
                    }

                    #endregion
                }

                if (!IsPostBack)
                {
                    if (vSession.User != null)
                    {
                        //iUserFull.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) && (vSession.User.IsPublic == 1) ? true : false;
                        //iUserNotFull.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) || (vSession.User.IsPublic == 0) ? true : false;
                        //LblMainMenuAlert.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "18")).Text : (vSession.User.IsPublic == 0) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "sitemaster", "label", "19")).Text : "Your account is OK!";
                        //aMainMenuAlert.HRef = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : (vSession.User.IsPublic == 0) ? ControlLoader.ContactUs : ControlLoader.Dashboard(vSession.User, "home");

                        if (vSession.User.AccountStatus != Convert.ToInt32(AccountStatus.Blocked))
                        {
                            aMainMenuLogin.Visible = aMainMenuFreeSignUp.Visible = aMainMenuSignUp.Visible = false;
                            liMainMenuUser.Visible = true;
                            LblUser.Text = LblUser2.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? (!string.IsNullOrEmpty(vSession.User.LastName) && !string.IsNullOrEmpty(vSession.User.FirstName)) ? vSession.User.LastName + " " + vSession.User.FirstName : vSession.User.CompanyName : vSession.User.Username;
                            LblCompanyName.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? vSession.User.CompanyName : vSession.User.Username;
                            LblCompanyEmail.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? vSession.User.Email : "";
                            ImgCompanyLogo.Src = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? vSession.User.CompanyLogo : "/assets_out/images/global/company.png";
                            ImgUserPhoto2.Src = ImgUserPhoto.Src = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? (!string.IsNullOrEmpty(vSession.User.PersonalImage)) ? vSession.User.PersonalImage : vSession.User.CompanyLogo : "/images/no_logo_company.png";
                            LblUserEmail.Text = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? (!string.IsNullOrEmpty(vSession.User.OfficialEmail)) ? vSession.User.OfficialEmail : vSession.User.Email : vSession.User.Email;

                            liMainMenuProfile.Visible = liMainMenuUserEditProfile.Visible = liMainMenuUserBilling.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed));

                            if (liMainMenuProfile.Visible && liMainMenuUserEditProfile.Visible)
                            {
                                LblUserProfile.Text = "View Profile";
                                LblUserEditProfile.Text = "Edit Profile";
                            }

                            LblUserDashboard.Text = "Dashboard";
                            liMainMenuRegister.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted);
                            LblUserRegister.Text = "Complete Registration";

                            liMainMenuAdmin.Visible = Sql.IsUserAdministrator(vSession.User.Id, session);
                            LblMainMenuAdmin.Text = "Administrator";
                            LblUserLogout.Text = "Log out";

                            //int newMessages = Sql.GetCompanyMessages(vSession.User.Id, 1, 0, session);
                            //iMainMenuMessages.Visible = newMessages > 0 ? true : false;
                            //iMainMenuNoMessages.Visible = newMessages > 0 ? false : true;
                            //spanUserMessages.InnerText = newMessages > 0 ? newMessages.ToString() : "0";
                            //spanUserNoMessages.InnerText = "0";
                            //LblMainMenuMessages.Text = newMessages > 0 ? "You have " + newMessages + " new messages!" : "No messages";
                            //aMainMenuMessages.HRef = ControlLoader.Dashboard(vSession.User, "messages/inbox");

                            //int isNew = 1;
                            //int isDeleted = 0;
                            //int companyLeads = Sql.GetCompanyRecentLeadsForCurrentMonthByIsNewIsDeletedStatus(vSession.User, isNew, isDeleted, session);
                            //iMainMenuUserLeads.Visible = companyLeads > 0 ? true : false;
                            //iMainMenuUserNoLeads.Visible = companyLeads > 0 ? false : true;
                            //spanUserLeads.InnerText = companyLeads > 0 ? companyLeads.ToString() : "0";
                            //spanUserNoLeads.InnerText = "0";
                            //LblMainMenuUserLeads.Text = companyLeads > 0 ? "You have " + companyLeads + " new leads" : "No leads";
                            //aMainMenuUserLeads.HRef = ControlLoader.Dashboard(vSession.User, "leads");

                            //bool hasActiveSubscription = true;
                            //if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId) && vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType)
                            //{
                            //    hasActiveSubscription = false;
                            //}
                            //else
                            //{
                            //    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId) && vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                            //    {
                            //        try
                            //        {
                            //            hasActiveSubscription = true;        //Sql.HasActivePacketSubscription(vSession.User.Id, vSession.User.CustomerStripeId, session);
                            //        }
                            //        catch (Exception ex)
                            //        {
                            //            Logger.DetailedError(session, ex, Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            //        }
                            //    }
                            //}

                            //if (!hasActiveSubscription)
                            //{
                            //    iMainMenuNotifications.Visible = true;
                            //    spanUserNotifications.InnerText = "1";
                            //    iMainMenuNoNotifications.Visible = false;
                            //    LblMainMenuNotifications.Text = "You have canceled your premium/service plan";
                            //    aMainMenuNotifications.HRef = ControlLoader.Dashboard(vSession.User, "billing");
                            //}
                            //else
                            //{
                            //    iMainMenuNoNotifications.Visible = true;
                            //    spanUserNoNotifications.InnerText = "0";
                            //    iMainMenuNotifications.Visible = false;
                            //    LblMainMenuNotifications.Text = "No notifications!";
                            //}
                        }
                    }
                    else
                    {
                        aMainMenuLogin.Visible = aMainMenuFreeSignUp.Visible = aMainMenuSignUp.Visible = true;
                        liMainMenuUser.Visible = false;                        
                        liMainMenuProfile.Visible = liMainMenuUserEditProfile.Visible = liMainMenuUserBilling.Visible = false;
                        liMainMenuRegister.Visible = false;
                        liMainMenuAdmin.Visible = false;
                        liMainMenuAdmin.Visible = false;
                    }
                }
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

            if (vSession.User != null)
            {
                VendorName = SqlCollaboration.GetVendorNameByResellerUserId(vSession.User.Id, session);
                if (VendorName != "")
                {
                    aMainMenuUserLogout.HRef = ControlLoader.LogoutPartner.Replace("{CompanyName}", VendorName);
                }
                else
                {
                    aMainMenuUserLogout.HRef = ControlLoader.Login;
                }

                aMainMenuUserDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
                aMainMenuUserRegister.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
                aUserEditProfile.HRef = ControlLoader.Dashboard(vSession.User, "edit-company-profile");
                aUserBilling.HRef = (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType) ? ControlLoader.Dashboard(vSession.User, "billing") : ControlLoader.Pricing;
                liMainMenuUserDashboard.HRef = ControlLoader.Profile(vSession.User);
            }

            aMainMenuAdmin.HRef = ControlLoader.AdminPage;

            aPartnerPortal.HRef = ControlLoader.PrmSoftwarePartnerPortal;
            aPartnerDirectory.HRef = ControlLoader.PrmSoftwarePartnerDirectory;
            aPartnerOnboarding.HRef = ControlLoader.PrmSoftwarePartnerOnboarding;
            aDealRegistration.HRef = ControlLoader.PrmSoftwareDealRegistration;
            aLeadDistribution.HRef = ControlLoader.PrmSoftwareLeadDistribution;
            aCollaboration.HRef = ControlLoader.PrmSoftwareCollaboration;
            aChannelAnalytics.HRef = ControlLoader.PrmSoftwareChannelAnalytics;
            aPartnerLocator.HRef = ControlLoader.PrmSoftwarePartnerLocator;
            aPartner2Partner.HRef = ControlLoader.PrmSoftwarePartner2Partner;
            aPartnerActivation.HRef = ControlLoader.PrmSoftwarePartnerActivation;
            aPartnerTierManagement.HRef = ControlLoader.PrmSoftwarePartnerTierManagement;
            aPartnerTeamRoles.HRef = ControlLoader.PrmSoftwarePartnerTeamRoles;
            aPartnerManagement.HRef = ControlLoader.PrmSoftwarePartnerManagement;
            aContentManagement.HRef = ControlLoader.PrmSoftwareContentManagement;

            aLogo.HRef = ControlLoader.Default();
            //aMainMenuHowItWorks.HRef = aFooterHowItWorks.HRef = ControlLoader.HowItWorks;
            aMainMenuPartnerRecruitmentAuto.HRef = ControlLoader.ChannelPartnerRecruitmentAutoMate;
            aMainMenuPartnerRecruitmentData.HRef = ControlLoader.ChannelPartnerRecruitmentDatabase;
            aMainMenuPRM.HRef = ControlLoader.PrmSoftware;
            aFooterPR.HRef = ControlLoader.ChannelPartnerRecruitment;
            aMainMenuVendorManagement.HRef = ControlLoader.IntentSignals;        //ControlLoader.ChannelPartners;
            aMainMenuMAMarketplace.HRef = aFooterMAMarketplace.HRef = ControlLoader.MAMarketplace;
            aImpartner.HRef = aImpartnerF.HRef = ControlLoader.ResourcesAlternativesImpartnerPage;
            aPartnerStack.HRef = aPartnerStackF.HRef = ControlLoader.ResourcesAlternativesPartnerStackPage;
            aAllbound.HRef = aAllboundF.HRef = ControlLoader.ResourcesAlternativesAllboundPage;
            aZiftSolutions.HRef = aZiftSolutionsF.HRef = ControlLoader.ResourcesAlternativesZiftSolutionsPage;
            aSalesforcePRM.HRef = aSalesforcePRMF.HRef = ControlLoader.ResourcesAlternativesSalesforceCommunitiesPage;
            aMagentrix.HRef = aMagentrixF.HRef = ControlLoader.ResourcesAlternativesMindmatrixPage;
            aMyprm.HRef = aMyprmF.HRef = ControlLoader.ResourcesAlternativesMyPrmPage;
            aMainMenuSearch.HRef = ControlLoader.Search;
            //aMainMenuSearchAll.HRef = ControlLoader.SearchAllCategories;
            aMainMenuCaseStudies.HRef = ControlLoader.CaseStudiesPage;

            aMainMenuPricingPRM.HRef = ControlLoader.PricingPRMSoft;
            aMainMenuPricingPR.HRef = ControlLoader.PricingPartnerRecruitment;
            aMainMenuPricingIS.HRef = ControlLoader.PricingIntentSignals;

            aFooterResources.HRef = aResources.HRef = ControlLoader.ResourcesPage;
            aBlog.HRef = aFooterBlog.HRef = "https://medium.com/@elioplus";
            aBlog.Target = aFooterBlog.Target = "_blank";

            aFooterFacebook.HRef = "http://www.facebook.com/elioplus";
            aFooterFacebook.Target = "_blank";
            aFooterTwitter.HRef = "http://www.twitter.com/elioplus";
            aFooterTwitter.Target = "_blank";
            aFooterLinkedin.HRef = "https://www.linkedin.com/company/elio";
            aFooterLinkedin.Target = "_blank";
            aFooterInfoEmail.HRef = "mailto:info@elioplus.com";
            //aFooterCopyright.HRef = ControlLoader.About;
            aMainMenuLogin.HRef = ControlLoader.Login;
            aMainMenuSignUp.HRef = aMainMenuFreeSignUp.HRef = ControlLoader.SignUp;
            aFooterTerms.HRef = ControlLoader.Terms;
            aFooterPrivacy.HRef = ControlLoader.Privacy;
            aFooterPricing.HRef = ControlLoader.Pricing;
            //aFooterFeatures.HRef=ControlLoader.
            aFooterMyData.HRef = ControlLoader.MyData;
            aFaq.HRef = aFooterFaq.HRef = ControlLoader.Faq;
            aMainMenuSearch.HRef = aFooterSearch.HRef = ControlLoader.Search;
            aSearchVendors.HRef = aMainMenuSearchVendors.HRef = ControlLoader.SearchForVendors;
            aSearchChannelPartners.HRef = aMainMenuSearchChannelPartners.HRef = ControlLoader.SearchForChannelPartners;
            aSearchPartnerhsips.HRef = aMainMenuSearchChannelPartnerships.HRef = ControlLoader.SearchForChannelPartnerships;
            aFooterCaseStudies.HRef = ControlLoader.CaseStudiesPage;
            aAbout.HRef = ControlLoader.About;
            aContact.HRef = aFooterContact.HRef = ControlLoader.ContactUs;
            aFooterIntentSignals.HRef = ControlLoader.IntentSignals;
            aMainMenuReferralSoftware.HRef = aFooterReferralSoftware.HRef = ControlLoader.ReferalSoftware;
            aFeaturesChannelPartners.HRef = ControlLoader.FeaturesChannelPartners;
            aFeaturesVendors.HRef = ControlLoader.FeaturesVendors;
        }

        private void UpdateStrings()
        {
            //ImgLogo.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "tooltip", "1")).Text;
            //ImgLogo.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "alternate", "1")).Text;
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "1")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "2")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "3")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "4")).Text;
            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "5")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "6")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "7")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "8")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "9")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "10")).Text;
            Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "11")).Text;
            Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "12")).Text;
            Label13.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "13")).Text;
            Label14.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "14")).Text;
            Label15.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "15")).Text;
            Label16.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "16")).Text;
            Label17.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "17")).Text;
            Label18.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "18")).Text;
            Label19.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "19")).Text;
            Label20.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "20")).Text;
            Label21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "21")).Text;
            Label22.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "22")).Text;
            Label23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "23")).Text;
            Label24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "24")).Text;
            Label25.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "25")).Text;
            Label26.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "26")).Text;
            Label27.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "27")).Text;
            Label28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "28")).Text;
            Label29.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "29")).Text;
            Label30.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "30")).Text;
            Label31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "31")).Text;
            Label32.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "32")).Text;
            Label33.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "33")).Text;
            Label34.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "34")).Text;
            Label35.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "35")).Text;
            Label36.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "36")).Text;
            Label37.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("header", "sitemaster", "label", "37")).Text;
        }

        #endregion

        #region Buttons

        #endregion
    }
}