using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Collections.Generic;
using System.Configuration;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using Org.BouncyCastle.Asn1.Ocsp;

namespace WdS.ElioPlus.Lib.LoadControls
{     
    public class ControlLoader
    {
        public ControlLoader()
        {
            
        }

        #region Menu Pages
                
        //public const string SiteMaster = "/ElioplusMaster.Master";
        //public const string Default = "https://elioplus.com";
        //public const string Default = "/home";
        public const string DefaultErrorPage = "/home";
        public const string HowItWorks = "/how-it-works";
        public const string HowItWorksCollaborationPage = "/how-it-works-collaboration-tool";
        public const string Search = "/search";
        public const string SearchResults = "/search-results";
        public const string FeaturesChannelPartners = "/channel-partners-features";
        public const string FeaturesVendors = "/vendors-features";

        public const string SearchForVendors = "/saas-partner-programs";     //"/search/vendors";
        public const string SystemIntegratorsPartnerPrograms = "/systems-integrators-partner-programs";
        public const string WhiteLabelPartnerPrograms = "/white-label-partner-programs";
        public const string ManagedServiceProvidersPartnerPrograms = "/msps-partner-programs";

        public const string SearchForChannelPartners = "/search/channel-partners";
        public const string SearchAllCategories = "/search-all-categories";
        public const string SearchForChannelPartnerships = "/partnerships";
        //public const string SearchCategories = "/search-categories";
        public const string CaseStudiesPage = "/case-studies";
        public const string SearchVerticals = "/search-details";
        //public const string SearchByType = "/search-vendors";
        //public const string SearchChannelPartners = "/search-channel-partners";
        public const string ContactUs = "/contact-us";
        public const string SignUp = "/free-sign-up";
        public const string SignUpPrm = "/prm-free-sign-up";
        public const string SignUpPartner = "/{CompanyName}/partner-free-sign-up";
        public const string ForgotPassword = "/reset-password";
        public const string ForgotPasswordPartner = "/{CompanyName}/partner-reset-password";
        public const string Pricing = "/pricing";
        public const string PricingPRMSoft = "/prm-software-pricing";
        public const string PricingPartnerRecruitment = "/partner-recruitment-pricing";
        public const string PricingIntentSignals = "/intent-signals-pricing";
        //public const string Payment = "/Payment.aspx";
        public const string Terms = "/terms";
        public const string Privacy = "/privacy";
        public const string MyData = "/my-data";
        public const string Faq = "/faq";
        public const string About = "/about-us";
        public const string ResellersLanding = "/reseller-free-sign-up";
        public const string ResellersPage = "/resellers";
        public const string VendorsLanding = "/vendor-free-sign-up";
        public const string Login = "/login";
        public const string LoginPartner = "/{CompanyName}/partner-login";
        public const string LogoutPartner = "/{CompanyName}/partner-logout";
        public const string FullRegistrationPage = "/full-registration";
        public const string FullRegistrationPrmPage = "/prm-full-registration";
        public const string FullRegistrationPagePartner = "/{CompanyName}/partner-full-registration";
        public const string MultiAccountsFullRegistrationPage = "/multi-accounts-full-registration";
        public const string ThankYouPage = "/successful-registration";
        public const string SuccessfulPaymentPage = "/successful-payment";
        public const string CanceledPaymentPage = "/canceled-payment";
        public const string ErrorPage = "/error-page";
        public const string Page404 = "/404";
        public const string PageDash404 = "/dashboard/404";
        public const string PageDash405 = "/dashboard/405";
        public const string PageComm404 = "/community/404";
        public const string AdminElioFinancialIncomeFlowPage = "/admin-elio-financial-income-flow-page";
        public const string AdminElioFinancialExpensesFlowPage = "/admin-elio-financial-expenses-flow-page";
        public const string AdminPage = "/admin-page";
        public const string AdminUserManagement = "/admin-user-management";
        public const string AdminDemoRequestsManagement = "/admin-demo-requests-management";
        public const string AdminStatisticsPage = "/admin-statistics-page";
        public const string AdminAddThirdPartyUsersPage = "/add-third-party-users-page";
        public const string AdminAddIntentSignalsDataPage = "/add-intent-signals-data-page";
        public const string AdminRegistrationProductsPage = "/registration-products-page";
        public const string Calculator = "/roi-calculator";
        public const string DashboardCollaborationCreateNewInvitations = "/collaboration-create-new-partners";
        public const string VendorLanding = "/business-development-tool";
        public const string DashboardCollaborationChatRoom = "/collaboration-chat-room";
        public const string DashboardCollaborationLibrary = "/collaboration-library";
        public const string PrmSoftware = "/prm-software";
        public const string PrmSoftwareFR = "/fr/prm-software";
        public const string PrmSoftwareES = "/es/prm-software";
        public const string PrmSoftwareDE = "/de/prm-software";
        public const string PrmSoftwarePT = "/pt/prm-software";
        public const string PrmSoftwareAR = "/ar/prm-software";
        public const string PrmSoftwareFeatures = "/prm-software-features";        
        public const string PartnerToPartnerDeals = "/partner-to-partner-deals";
        public const string ChannelPartnerRecruitment = "/channel-partner-recruitment";
        public const string ChannelPartnerRecruitmentAutoMate = "/channel-partner-recruitment-automation";
        public const string ChannelPartnerRecruitmentDatabase = "/channel-partner-recruitment-database";
        public const string ChannelPartners = "/channel-partners";
        public const string ProfilesSubIndustries = "/profiles/vendors/";
        public const string ResourcesPage = "/resources-page";
        public const string ResourcesPartnerManagementSystemPage = "/partner-relationship-management-system";
        public const string ResourcesManageChannelPartnersPage = "/manage-channel-partners";
        public const string ResourcesPartnerManagementPage = "/partner-management";
        public const string ResourcesPartnerRecruitmentPtoccessPage = "/channel-partner-recruitment-process";
        public const string ResourcesPartneringExamplesPage = "/partnering-examples";
        public const string PartnerPortal = "/partner-portals";
        public const string IntentSignals = "/intent-signals";
        public const string ReferalSoftware = "/referral-software";
        public const string ReferalSoftwareGetAccess = "/referral-software-get-access";
        public const string RequestQuote = "/get-a-quote";
        public const string MessageQuote = "/send-a-quote";
        public const string ClaimProfile = "/claim-profile";
        public const string MAMarketplace = "/mergers-acquisition-marketplace";

        public const string PrmSoftwarePartnerPortal = "/prm-software/partner-portal";
        public const string PrmSoftwarePartnerDirectory = "/prm-software/partner-directory";
        public const string PrmSoftwarePartnerOnboarding = "/prm-software/partner-onboarding";
        public const string PrmSoftwareDealRegistration = "/prm-software/deal-registration";
        public const string PrmSoftwareLeadDistribution = "/prm-software/lead-distribution";
        public const string PrmSoftwareCollaboration = "/prm-software/collaboration";
        public const string PrmSoftwareChannelAnalytics = "/prm-software/channel-analytics";
        public const string PrmSoftwarePartnerLocator = "/prm-software/partner-locator";
        public const string PrmSoftwarePartner2Partner = "/prm-software/partner-2-partner";
        public const string PrmSoftwarePartnerActivation = "/prm-software/partner-activation";
        public const string PrmSoftwarePartnerTierManagement = "/prm-software/tier-management";
        public const string PrmSoftwarePartnerTeamRoles = "/prm-software/team-roles";
        public const string PrmSoftwarePartnerManagement = "/prm-software/partner-management";
        public const string PrmSoftwareContentManagement = "/prm-software/content-management";

        public const string PrmSoftwareIntegrations = "/prm-software-integrations";
        public const string PrmSoftwareSalesforceIntegration = "/prm-software/salesforce-integration";
        public const string PrmSoftwareHubspotIntegration = "/prm-software/hubspot-integration";
        public const string PrmSoftwareCrmIntegrations = "/prm-software/crm-integrations";

        public const string DashboardTierManagement = "/tier-management";
        public const string DashboardPermissionsRolesManagement = "/permissions-roles-management";

        public const string ResourcesAlternativesAllboundPage = "/alternatives/allbound";
        public const string ResourcesAlternativesImpactCompanyPage = "/alternatives/impact-company";
        public const string ResourcesAlternativesImpartnerPage = "/alternatives/impartner";
        public const string ResourcesAlternativesMindmatrixPage = "/alternatives/mindmatrix";
        public const string ResourcesAlternativesModelNPage = "/alternatives/model-n";
        public const string ResourcesAlternativesPartnerizePage = "/alternatives/partnerize";
        public const string ResourcesAlternativesSalesforceCommunitiesPage = "/alternatives/salesforce-communities";
        public const string ResourcesAlternativesZiftSolutionsPage = "/alternatives/zift-solutions";
        public const string ResourcesAlternativesPartnerStackPage = "/alternatives/partnerstack";
        public const string ResourcesAlternativesMyPrmPage = "/alternatives/myprm";
        public const string ResourcesAlternativesChanneltivity = "/alternatives/channeltivity";

        public const string ResourcesNetworksComptiaPage = "/networks/comptia";
        public const string ResourcesNetworksSpiceworksPage = "/networks/spiceworks";

        public static string Default()
        {
            //string callbackurl = "https://elioplus.com";

            //if (HttpContext.Current.Request.Url.Host == "localhost")
            //    callbackurl = HttpContext.Current.Request.Url.Authority;

            //return callbackurl;

            ////return (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false") ? "http://" + HttpContext.Current.Request.Url.Authority : "https://" + HttpContext.Current.Request.Url.Authority;

            if (HttpContext.Current.Request.Url.Authority.StartsWith("elioplus") || HttpContext.Current.Request.Url.Host.StartsWith("www"))
                return "https://" + HttpContext.Current.Request.Url.Authority;
            else
                return "http://" + HttpContext.Current.Request.Url.Authority;
        }

        public static string Default2()
        {
            return "http://" + HttpContext.Current.Request.Url.Authority;
        }

        public static string Deals(int userId)
        {
            if (GlobalMethods.isRandstadCustomer(userId))
                return "deals";
            else
                return "deal-registration";
        }

        public static string DealsView(int userId)
        {
            if (GlobalMethods.isRandstadCustomer(userId))
                return "deals-view";            
            else
                return "deal-registration-view";
        }

        public static string DownloadInvoices(int userId, int orderId)
        {
            return "download-invoices?case=StripeInvoices&userID=" + userId.ToString() + "&orderID=" + orderId + "";
        }

        public static string PersonProfile(ElioUsers user)
        {
            string searhUrl = "";
            if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" || true)
            {
                string companyName = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                if (companyName.EndsWith("-"))
                    companyName = companyName.Substring(0, companyName.Length - 1);

                searhUrl = "/connection-profile/" + user.Id + "/" + companyName;
            }
            else
            {
                string searchCategoryUrl = "";
                string searchCategoryValue = "";
                GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                if (searchCategoryUrl == "" && searchCategoryValue == "")
                    Profile(user, "true");

                if (user.CompanyType == Types.Vendors.ToString())
                    return searhUrl = "/" + searchCategoryUrl + "-profiles/vendors/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
                else
                    return searhUrl = "/" + searchCategoryUrl + "-profiles/channel-partners/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;

                //searhUrl = "/" + searchCategoryUrl + "-profiles/" + user.CompanyType.ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
            }

            return searhUrl;
        }

        public static string PersonProfileForConnectionsPage(int userId, string userCompanyName, string userCompanyType, string isProductionMode)
        {
            return PersonProfileForConnectionsPage(userId, userCompanyName, userCompanyType);
        }

        public static string PersonProfileForConnectionsPage(int userId, string userCompanyName, string userCompanyType)
        {
            string searhUrl = "";
            if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" || true)
            {
                string companyName = Regex.Replace(userCompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                if (companyName.EndsWith("-"))
                    companyName = companyName.Substring(0, companyName.Length - 1);

                searhUrl = "/connection-profile/" + userId + "/" + companyName;

                //string companyName = Regex.Replace(userCompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                //if (companyName.EndsWith("-"))
                //    companyName = companyName.Substring(0, companyName.Length - 1);

                //if (userCompanyType == Types.Vendors.ToString())
                //    searhUrl = "/profiles/vendors/" + userId + "/" + companyName;
                //else
                //    searhUrl = "/profiles/channel-partners/" + userId + "/" + companyName;
            }
            else
            {
                string searchCategoryUrl = "";
                string searchCategoryValue = "";
                GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                if (searchCategoryUrl == "" && searchCategoryValue == "")
                    PersonProfileForConnectionsPage(userId, userCompanyName, userCompanyType, "true");

                if (userCompanyType == Types.Vendors.ToString())
                    return searhUrl = "/" + searchCategoryUrl + "-profiles/vendors/" + searchCategoryValue + "/" + userCompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;
                else
                    return searhUrl = "/" + searchCategoryUrl + "-profiles/channel-partners/" + searchCategoryValue + "/" + userCompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;

                //searhUrl = "/" + searchCategoryUrl + "-profiles/" + user.CompanyType.ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
            }

            return searhUrl;
        }

        public static string SearchAllIndustries(string searchKeyValue)
        {
            string searchUrl = "/all-industries/" + searchKeyValue;

            return searchUrl;
        }

        public static string SearchAllByCategoriesValue(string category, string searchKeyValue)
        {
            string searchUrl = "/all-" + category + "/" + searchKeyValue;

            return searchUrl;
        }

        public static string SearchAllCompaniesLinksByCategoriesValue(string category, string searchKeyValue)
        {
            string searchUrl = "/all-" + category + "-companies/" + searchKeyValue;

            return searchUrl;
        }

        public static string SearchAllByCategoriesValue2Levels(string category, string groupCategory, string searchKeyValue)
        {
            string searchUrl = "/all-" + category + "/" + groupCategory + "/" + searchKeyValue;

            return searchUrl;
        }

        public static string SearchCategory(string categoryValue)
        {
            return "search-" + categoryValue.ToLower();
        }

        public static string SearchByType(string searchKeyValue)
        {
            string searchUrl = "/search/" + searchKeyValue;

            return searchUrl;
        }

        public static string PartnerProgramSubIndustryProfiles(string type, string program, string vertical)
        {
            if (type == "channel-partners")
                return "/" + program.Replace(" " ,"-").ToLower() + "/" + type.ToLower() + "/" + vertical.ToLower();
            else
                return "/" + program.Replace(" ", "-").ToLower() + "/" + type.ToLower() + "/" + vertical.ToLower();
        }

        public static string SubIndustryProfiles(string type, string vertical)
        {
            if (type == "channel-partners")
                return "/profile/" + type.ToLower() + "/" + vertical.ToLower();
            else
                return "/partner-programs/" + type.ToLower() + "/" + vertical.ToLower();
        }

        public static string SubIndustryPartnerProgramProfiles(string program, string type, string vertical)
        {            
            return "/" + program + "/" + type.ToLower() + "/" + vertical.ToLower();
        }

        public static string CountryProfiles(string type, string country)
        {
            return "/geo-search/" + type.ToLower() + "-" + country.ToLower();
        }

        //public static string SubIndustryCountryProfiles(string type, string country, string vertical)
        //{
        //    return "/geo-profile/" + type.ToLower() + "-" + country + "/" + vertical.ToLower();
        //}

        public static string SubIndustryProfilesResults(ElioUsers user, out string categoryValue)
        {
            categoryValue = "";
            string searhUrl = "";

            string searchCategoryUrl = "";
            string searchCategoryValue = "";
            GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

            if (searchCategoryUrl == "" && searchCategoryValue == "")
                Profile(user, "true");

            categoryValue = searchCategoryValue;
            searhUrl = "/" + searchCategoryUrl + "/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace("&", "").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower();

            if (searhUrl.EndsWith("-"))
                searhUrl += user.Id;
            else
                searhUrl += "-" + user.Id;

            return searhUrl;
        }

        public static string Profile(ElioUsers user, string isProductionMode)
        {
            return Profile(user);
        }

        public static string Profile(ElioUsers user)
        {
            string searhUrl = "";

            if (user != null)
            {
                if (user.AccountStatus == (int)AccountStatus.Completed)
                {
                    if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" || true)
                    {
                        string companyName = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                        if (companyName.EndsWith("-"))
                            companyName = companyName.Substring(0, companyName.Length - 1);

                        if (user.CompanyType == Types.Vendors.ToString())
                            searhUrl = "/profiles/vendors/" + user.Id + "/" + companyName;
                        else
                            searhUrl = "/profiles/channel-partners/" + user.Id + "/" + companyName;
                    }
                    else
                    {
                        string searchCategoryUrl = "";
                        string searchCategoryValue = "";
                        GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                        if (searchCategoryUrl == "" && searchCategoryValue == "")
                            Profile(user, "true");

                        if (user.CompanyType == Types.Vendors.ToString())
                            searhUrl = "/" + searchCategoryUrl + "-profiles/vendors/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
                        else
                            searhUrl = "/" + searchCategoryUrl + "-profiles/channel-partners/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;

                        //searhUrl = "/" + searchCategoryUrl + "-profiles/" + user.CompanyType.ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
                    }
                }
            }
            else
            {
                Logger.DetailedError("ControlLoader class --> Profile(ElioUsers user)", "user object is empty", "no company profile is available");
            }

            return searhUrl;
        }

        public static string ProfileNew(int userId, string companyName, string companyType, string isProductionMode)
        {
            return ProfileNew(userId, companyName, companyType);
        }

        public static string ProfileNew(int userId, string companyName, string companyType)
        {
            string searhUrl = "";
            if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" || true)
            {
                companyName = Regex.Replace(companyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                if (companyName.EndsWith("-"))
                    companyName = companyName.Substring(0, companyName.Length - 1);

                if (companyType == Types.Vendors.ToString())
                    searhUrl = "/profiles/vendors/" + userId + "/" + companyName;
                else
                    searhUrl = "/profiles/channel-partners/" + userId + "/" + companyName;
            }
            else
            {
                string searchCategoryUrl = "";
                string searchCategoryValue = "";
                GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                if (searchCategoryUrl == "" && searchCategoryValue == "")
                    ProfileNew(userId, companyName, companyType, "true");

                if (companyType == Types.Vendors.ToString())
                    searhUrl = "/" + searchCategoryUrl + "-profiles/vendors/" + searchCategoryValue + "/" + companyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;
                else
                    searhUrl = "/" + searchCategoryUrl + "-profiles/channel-partners/" + searchCategoryValue + "/" + companyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;

                //searhUrl = "/" + searchCategoryUrl + "-profiles/" + user.CompanyType.ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
            }

            return searhUrl;
        }

        public static string ProfileForConnectionspage(int userId, string userCompanyName, string userCompanyType, string isProductionMode)
        {
            return ProfileForConnectionspage(userId, userCompanyName, userCompanyType);
        }

        public static string ProfileForConnectionspage(int userId, string userCompanyName, string userCompanyType)
        {
            string searhUrl = "";
            if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" || true)
            {
                string companyName = Regex.Replace(userCompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                if (companyName.EndsWith("-"))
                    companyName = companyName.Substring(0, companyName.Length - 1);

                if (userCompanyType == Types.Vendors.ToString())
                    searhUrl = "/profiles/vendors/" + userId + "/" + companyName;
                else
                    searhUrl = "/profiles/channel-partners/" + userId + "/" + companyName;
            }
            else
            {
                string searchCategoryUrl = "";
                string searchCategoryValue = "";
                GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                if (searchCategoryUrl == "" && searchCategoryValue == "")
                    ProfileForConnectionspage(userId, userCompanyName, userCompanyType, "true");

                if (userCompanyType == Types.Vendors.ToString())
                    searhUrl = "/" + searchCategoryUrl + "-profiles/vendors/" + searchCategoryValue + "/" + userCompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;
                else
                    searhUrl = "/" + searchCategoryUrl + "-profiles/channel-partners/" + searchCategoryValue + "/" + userCompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + userId;

                //searhUrl = "/" + searchCategoryUrl + "-profiles/" + user.CompanyType.ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower() + "-" + user.Id;
            }

            return searhUrl;
        }

        public static string ProfileWithOutCategoryValue(ElioUsers user, out string categoryValue)
        {
            categoryValue = "";
            string searhUrl = "";
            if (ConfigurationManager.AppSettings["IsProductionMode"] == "true" && false)
            {
                string companyName = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                if (companyName.EndsWith("-"))
                    companyName = companyName.Substring(0, companyName.Length - 1);

                searhUrl = "/profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + user.Id + "/" + companyName;
            }
            else
            {
                string searchCategoryUrl = "";
                string searchCategoryValue = "";
                GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

                if (searchCategoryUrl == "" && searchCategoryValue == "")
                    Profile(user, "true");

                categoryValue = searchCategoryValue;
                searhUrl = "/" + searchCategoryUrl + "-profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace("&", "").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower();

                if (searhUrl.EndsWith("-"))
                    searhUrl += user.Id;
                else
                    searhUrl += "-" + user.Id;
            }

            return searhUrl;
        }

        //public static string ProfileByCategory(ElioUsers user, string searchArea, ElioSession vSession)
        //{
        //    List<string> categoryValues = new List<string>();
        //    foreach (string key in vSession.SearchCategoriesArea.Values)
        //    {
        //        if (vSession.SearchCategoriesArea.TryGetValue(key, out categoryValues))
        //        {
        //            break;
        //        }
        //    }

        //    return "/" + searchArea + "-profiles/" + user.CompanyType.ToLower() + "/" + categoryValue + "/" + user.CompanyName.Trim().ToLower();
        //}

        public static string ProfileByCategory11(ElioUsers user, string categoryUrl, string caregoryValue)
        {
            return "/" + categoryUrl + "-profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + caregoryValue + "/" + user.CompanyName.Trim().ToLower();
        }

        public static string ProfileAdvanced(ElioUsers user)
        {
            string searchCategoryUrl = "";
            string searchCategoryValue = "";
            GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

            return "/" + searchCategoryUrl + "-profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + searchCategoryValue + "/" + user.CompanyName.Replace(@"[^A-Za-z0-9]+", "-").Replace(",", ".").Replace(" ", "-").Replace(".", "").Replace(" ", "").Replace("''", "").Replace("@", "").Replace("\"", "").Replace("/", "-").Replace(@"\", "-").Replace("--", "-").Replace("---", "-").ToLower();
        }

        public static string Profile2Levels(ElioUsers user)
        {
            string searchCategoryUrl = "";
            string searchVarticalValue = "";
            string searchCategoryValue = "";
            GlobalMethods.GetSearchCategoriesValues(out searchCategoryUrl, out searchCategoryValue);

            return "/" + searchCategoryUrl + "-profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + searchVarticalValue + "/" + searchCategoryValue + "/" + user.CompanyName.Trim().ToLower();
        }

        public static string ProfileMarkets(ElioUsers user, string market)
        {
            return "/markets-profiles/" + Regex.Replace(user.CompanyType, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + market + "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
        }

        public static string Dashboard(ElioUsers user, string page)
        {
            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                return "/dashboard/" + user.Id + "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page;
            }
            else
            {
                if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                {
                    return "/dashboard/" + user.Id + "/" + Regex.Replace((user.FirstName + "-" + user.LastName), @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page;
                }
                else
                {
                    return "/dashboard/" + user.Id + "/" + Regex.Replace(user.Username, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page;
                }
            }
        }

        public static string Dashboard(ElioUsers user, string key, string page)
        {
            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                return "/dashboard/" + user.Id + "/" + Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page + "/";
            }
            else
            {
                if (!string.IsNullOrEmpty(user.FirstName) && !string.IsNullOrEmpty(user.LastName))
                {
                    return "/dashboard/" + user.Id + "/" + Regex.Replace((user.FirstName + "-" + user.LastName), @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page + "/";
                }
                else
                {
                    return "/dashboard/" + user.Id + "/" + Regex.Replace(user.Username, @"[^A-Za-z0-9]+", "-").Trim().ToLower() + "/" + page + "/";
                }
            }
        }

        public static string Criteria(ElioUsers user)
        {
            return "/full-registration/" + user.Id + "/criteria-selection";
        }

        public static string Dashboard2(ElioUsers user)
        {
            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                user.CompanyName = Regex.Replace(user.CompanyName, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
            }

            return (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? "/profiles" + "/" + user.CompanyName : ((!string.IsNullOrEmpty(user.LinkedinId))) ? "/profiles" + "/" + user.FirstName + "-" + user.LastName : "/profiles" + "/" + user.Username;
        }
              
        #endregion

        #region Controls

        public const string InboxSent = "/Controls/InboxSent.ascx";
        public const string InboxDeleted = "/Controls/InboxDeleted.ascx";
        public const string InboxReply = "/Controls/InboxReply.ascx";
        public const string InboxView = "/Controls/InboxView.ascx";
        public const string InboxContent = "/Controls/InboxContent.ascx";
        public const string InboxCompose = "/Controls/InboxCompose.ascx";
        public const string CriteriaSelection = "/Controls/CriteriaSelection.ascx";
        public const string SignUpControl = "/Controls/Common/SignUpControl.ascx";
        public const string SignUpPrmControl = "/Controls/Common/SignUpPrmControl.ascx";
        public const string LoginControl = "/Controls/Common/LoginControl.ascx";        

        public const string Messages = "/Controls/Dashboard/Messages.ascx";
        public const string InboxNew = "/Controls/Dashboard/InboxNew.ascx";
        public const string InboxOld = "/Controls/Dashboard/InboxOld.ascx";        
        public const string ComposeMessage = "/Controls/Dashboard/ComposeMessage.ascx";
        //public const string BillingInfo = "/Controls/Dashboard/BillingInfo.ascx";
        public const string PacketStatusFeatures = "/Controls/Dashboard/Common/PacketStatusFeatures.ascx";
        public const string CompanyDataEditMode = "/Controls/Dashboard/CompanyDataEditMode.ascx";
        public const string CompanyDataViewMode = "/Controls/Dashboard/CompanyDataViewMode.ascx";
        public const string SubIndustries = "/Controls/Dashboard/SubIndustries.ascx";
        public const string CompanyCategoriesData = "/Controls/Dashboard/CompanyCategoriesData.ascx";
        public const string ViewsControl = "/Controls/Dashboard/Charts/ViewsControl.ascx";
        public const string ProfileDataViewMode = "/Controls/Dashboard/ProfileDataViewMode.ascx";
        public const string ProfileDataEditMode = "/Controls/Dashboard/ProfileDataEditMode.ascx";
        public const string Statistics = "/Controls/Dashboard/Charts/Statistics.ascx";
        public const string MarketingTools = "/Controls/Dashboard/MarketingTools.ascx";
        public const string ViewLogFiles = "/Controls/Dashboard/Admin/ViewLogFiles.ascx";
        public const string ViewClearbitLogFiles = "/Controls/Dashboard/Admin/ViewClearbitLogFiles.ascx";
        public const string ViewInfoLogFiles = "~/Controls/Dashboard/Admin/ViewInfoLogFiles.ascx";
        public const string ViewAnonymousLogFiles = "~/Controls/Dashboard/Admin/ViewAnonymousLogFiles.ascx";
        public const string ElioplusNutshell = "/Controls/ElioplusNutshell.ascx";
        public const string OurPartners = "/Controls/OurPartners.ascx";
        public const string VendorsBenefits = "/Controls/Benefits/VendorsBenefits.ascx";
        public const string ResellersBenefits = "/Controls/Benefits/ResellersBenefits.ascx";
        public const string DevelopersBenefits = "/Controls/Benefits/DevelopersBenefits.ascx";
        
        //public const string SearchVendors = "/Controls/SearchControls/SearchVendors.ascx";
        //public const string SearchResellers = "/Controls/SearchControls/SearchResellers.ascx";
        //public const string SearchDevelopers = "/Controls/SearchControls/SearchDevelopers.ascx";

        public const string SimpleRegistration = "/Controls/SimpleRegistration.ascx";
        public const string FullRegistration = "/Controls/FullRegistration.ascx";
        //public const string FullRegistrationPartner = "/Controls/FullRegistrationPartner.ascx";
        public const string FullRegistrationPartnerPrm = "/Controls/FullRegistrationPartnerPrm.ascx";

        public const string FullRegistrationPrm = "/Controls/FullRegistrationPrm.ascx";

        public const string MultiAccountsFullRegistration = "/Controls/MultiAccountsFullRegistration.ascx";
        public const string MultiFullRegistration = "/Controls/MultiFullRegistration.ascx";
        //public const string MultiAccountsCriteriaSelection = "/Controls/MultiAccountsCriteriaSelection.ascx";

        public const string BuyPacket = "/Controls/BuyPacket.ascx";
        public const string FeatureCompanies = "/Controls/FeatureCompanies.ascx";

        public const string Contact = "/Controls/Contact.ascx";
        public const string SendActivationEmail = "/Controls/SendActivationEmail.ascx";

        public const string SuccessResult = "/Controls/PaymentResults/SuccessResult.ascx";
        public const string FailureResult = "/Controls/PaymentResults/FailureResult.ascx";
        public const string CancelResult = "/Controls/PaymentResults/CancelResult.ascx";
        public const string BankAccountResult = "/Controls/PaymentResults/BankAccountResult.ascx";

        #endregion

        #region Community Menu Pages

        public const string CommunityPosts = "/community";
        public const string CommunityLatest = "/community/latest-posts";
        public const string CommunityPopular = "/community/most-voted-posts";
        public const string CommunityMostDiscussed = "/community/most-discussed-posts";
        public const string CommunityMustRead = "/community/must-read-posts";        
        public const string CommunityAddNewPost = "/community/add-new-post";
        public const string CommunityLogin = "/community/login";
        public const string CommunityForgotPassword = "/community/reset-password";
        public const string CommunitySimpleRegistration = "/community/free-sign-up";
        public const string CommunityFullRegistration = "/community/full-registration";
        public static string CommunityComments(int postId, string postTopic)
        {
            return "/community/posts/" + postId + "/" + Regex.Replace(postTopic, @"[^A-Za-z0-9]+", "-").Trim().ToLower();
        }
        public static string CommunityUserProfile(ElioUsers user)
        {
            string name = Regex.Replace(user.FirstName + " " + user.LastName, @"[^A-Za-z0-9]+","-").Trim().ToLower();

            return (user.CommunityStatus == Convert.ToInt32(AccountStatus.Completed)) ? "/community/members/" + user.Id + "/" + name : "/community/members/" + user.Id + "/" + user.Username;
        }

        #endregion

        #region Community Controls

        public const string UcRdgPosts = "/Controls/Community/Grids/uc_RdgPosts.ascx";
        public const string UcRdgUserComments = "/Controls/Community/Grids/uc_RdgUserComments.ascx";
        public const string UcRdgUpVoted = "/Controls/Community/Grids/uc_RdgUpVoted.ascx";
        public const string UcRdgFollowed = "/Controls/Community/Grids/uc_RdgFollowed.ascx";
        public const string UcRdgFollowing = "/Controls/Community/Grids/uc_RdgFollowing.ascx";

        #endregion

        #region Collaboration Controls

        public const string CreateNewInvitationMessage = "/Controls/Collaboration/CreateNewInvitationMessage.ascx";
        public const string InvitationMessageForm = "/Controls/Collaboration/InvitationMessageForm.ascx";

        #endregion

        #region Dashboard Controls

        public const string UcDealsDefaultControl = "/Controls/Dashboard/Deals/UcDealsDefaultControl.ascx";
        public const string UcDealsCustomControl = "/Controls/Dashboard/Deals/UcDealsCustomControl.ascx";

        public const string UcDealsAddEditDefaultControl = "/Controls/Dashboard/Deals/UcDealsAddEditDefaultControl.ascx";
        public const string UcDealsAddEditCustomControl = "/Controls/Dashboard/Deals/UcDealsAddEditCustomControl.ascx";

        public const string UcDealsViewDefaultControl = "/Controls/Dashboard/Deals/UcDealsViewDefaultControl.ascx";
        public const string UcDealsViewCustomControl = "/Controls/Dashboard/Deals/UcDealsViewCustomControl.ascx";

        #endregion

        #region Modal Controls

        public const string AddToTeamForm = "/Controls/Modals/AddToTeamForm.ascx";

        #endregion

        public static void LoadElioControls(Control root, PlaceHolder placeHolder, string controlsToLoad)
        {
            if (!string.IsNullOrEmpty(controlsToLoad))
            {
                placeHolder.Controls.Clear();
                string[] controls = controlsToLoad.Split('|');
                foreach (string c in controls)
                {
                    ControlLoader.LoadElioControl(root, placeHolder, c);
                }
            }
        }

        public static void LoadElioControl(Control root, PlaceHolder placeHolder, string controlPath)
        {
            Control control = root.TemplateControl.LoadControl(controlPath);
            control.ID = controlPath.Replace(".ascx", "");

            placeHolder.Controls.Add(control);
        }

        public static string FindControlId(string control)
        {
            string id = control.Replace(".ascx", "").Replace("~/", "");
            return id;
        }

        //public static string LoadSearchControlById(int searchControlId)
        //{
        //    string loadedSearchControl = string.Empty;

        //    switch (searchControlId)
        //    {
        //        case 2:
        //            loadedSearchControl = ControlLoader.SearchResellers;
        //            break;

        //        case 3:
        //            loadedSearchControl = ControlLoader.SearchDevelopers;
        //            break;

        //        default:
        //            loadedSearchControl = ControlLoader.SearchVendors;

        //            break;
        //    }

        //    return loadedSearchControl;
        //}

        public static void OpenPopUpWindow(Control root, string controlPath, string title)
        {
            RadWindow rwndMessageAlert = (RadWindow)ControlFinder.FindControlBackWards(root, "RwndMessageAlert");
            rwndMessageAlert.Attributes["control"] = "";
            rwndMessageAlert.Attributes["control"] = controlPath;

            PlaceHolder phMessageAlert = (PlaceHolder)ControlFinder.FindControlBackWards(root, "PhMessageAlert");
            phMessageAlert.Controls.Clear();

            rwndMessageAlert.Title = title;
            LoadElioControl(root, phMessageAlert, controlPath);

            RadAjaxManager.GetCurrent(root.Page).ResponseScripts.Add("showRwndMessageAlert();");
        }     
    }
}