using System;
using System.Linq;
using System.Web.Routing;
using System.Web;
using System.Collections.Generic;
using Hangfire;
using Hangfire.SqlServer;
using System.Diagnostics;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using System.Threading.Tasks;
using WdS.ElioPlus.Lib.Services.Hangfire;
using WdS.ElioPlus.Lib;
using System.Web.Optimization;
using Hangfire.Mongo.Migration.Strategies.Backup;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo;
using System.Configuration;

namespace WdS.ElioPlus
{
    public class Global : System.Web.HttpApplication
    {
        public IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            //.UseSqlServerStorage("data source=52.166.73.224;initial catalog=ElioPlus_DB_Hangfire;persist security info=False;user id=elioplu$u$er@dm!n!str@t0r;pwd=t0rn@d0v@g1985;packet size=8192;Connection Lifetime=900;Max Pool Size=200;Connection Timeout=240;Pooling=true; Integrated Security=True;", new SqlServerStorageOptions
            .UseSqlServerStorage(@"Server=52.166.73.224; Database=ElioPlus_DB_Hangfire; persist security info=False;user id=elioplu$u$er@dm!n!str@t0r;pwd=t0rn@d0v@g1985;packet size=8192;Connection Lifetime=1200;Max Pool Size=200;Connection Timeout=240;Pooling=true;", new SqlServerStorageOptions
            {
                CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                QueuePollInterval = TimeSpan.Zero,
                UseRecommendedIsolationLevel = true,
                UsePageLocksOnDequeue = true,
                DisableGlobalLocks = true
            });

            yield return new BackgroundJobServer();
        }

        protected void Application_Start(object sender, EventArgs e)
        {
            try
            {
                RegisterRoutes(RouteTable.Routes);

                BundleConfig.RegisterBundles(BundleTable.Bundles);

                HangfireBootstrapper.Instance.Start();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            #region Elio Site pages area

            routes.MapPageRoute(
                "homeRoute",
                "home",
                "~/Default.aspx"
            );

            routes.MapPageRoute(
                "loginPageRoute",
                "login",
                "~/pages/LoginPage.aspx"
            );

            routes.MapPageRoute(
                "loginPartnerPageRoute",
                "{CompanyName}/partner-login",
                "~/pages/LoginPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "logoutPartnerPageRoute",
                "{CompanyName}/partner-logout",
                "~/LogoutPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "signupPageRoute",
                "free-sign-up",
                "~/pages/SignUpPage.aspx"
            );

            routes.MapPageRoute(
                "signupPrmPageRoute",
                "prm-free-sign-up",
                "~/pages/SignUpPrmPage.aspx"
            );

            routes.MapPageRoute(
                "signupPartnerPageRoute",
                "{CompanyName}/partner-free-sign-up",
                "~/pages/SignUpPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "fullregistrationRoute",
                "full-registration",
                "~/FullRegistration.aspx"
            );

            routes.MapPageRoute(
                "fullregistrationPrmRoute",
                "prm-full-registration",
                "~/FullRegistrationPrm.aspx"
            );

            routes.MapPageRoute(
                "fullregistrationPartnerRoute",
                "{CompanyName}/partner-full-registration",
                "~/FullRegistrationPartner.aspx"
            );

            //routes.MapPageRoute(
            //    "howRoute",
            //    "how-it-works",
            //    "~/HowItWorks.aspx"
            //);

            routes.MapPageRoute(
                "howCollaborationRoute",
                "how-it-works-collaboration-tool",
                "~/HowItWorksCollaborationPage.aspx"
            );

            routes.MapPageRoute(
                "pricingPageRoute",
                "pricing",
                "~/pages/PricingPage.aspx"
            );

            routes.MapPageRoute(
                "pricingPrmSoftwarePageRoute",
                "prm-software-pricing",
                "~/pages/PricingPrmSoftwarePage.aspx"
            );

            routes.MapPageRoute(
                "pricingPartnerRecruitmentPageRoute",
                "partner-recruitment-pricing",
                "~/pages/PricingPartnerRecruitmentPage.aspx"
            );

            routes.MapPageRoute(
                "pricingIntentSignalsPageRoute",
                "intent-signals-pricing",
                "~/pages/PricingIntentSignalsPage.aspx"
            );

            routes.MapPageRoute(
                "searchPageRoute",
                "search",
                "~/pages/SearchPage.aspx"
            );

            routes.MapPageRoute(
                "searchResultsPageRoute",
                "search-results",
                "~/pages/SearchResultsPage.aspx"
            );

            routes.MapPageRoute(
                "searchForWhiteLabelPageRoute",
                "white-label-partner-programs",
                "~/pages/SearchWhiteLabelPage.aspx "
            );

            routes.MapPageRoute(
                "searchForMspPageRoute",
                "msps-partner-programs",
                "~/pages/SearchMSPPage.aspx "
            );

            routes.MapPageRoute(
                "searchForSystemIntegratorPageRoute",
                "systems-integrators-partner-programs",
                "~/pages/SearchSystemIntegratorPage.aspx "
            );

            routes.MapPageRoute(
                "featuresForChannelPartnersPageRoute",
                "channel-partners-features",
                "~/pages/FeaturesForChannelPartnersPage.aspx "
            );

            routes.MapPageRoute(
                "featuresForVendorsPageRoute",
                "vendors-features",
                "~/pages/FeaturesForVendorsPage.aspx "
            );

            routes.MapPageRoute(
                "searchForVendorsPageRoute",
                "saas-partner-programs",
                "~/pages/SearchForVendorsPage.aspx "
            );

            routes.MapPageRoute(
                "searchForChannelPartnershipsPageRoute",
                "partnerships",
                "~/pages/SearchForChannelPartnershipsPage.aspx "
            );

            routes.MapPageRoute(
                "searchForChannelPartnersPageRoute",
                "search/channel-partners",
                "~/pages/SearchForChannelPartnersPage.aspx "
            );

            routes.MapPageRoute(
                "searchForChannelPartnersByRegionCountryStateCityPageRoute",
                "{region}/{country}/{state}/{city}/channel-partners",
                "~/pages/SearchForChannelPartnersByRegionCountryStateCityPage.aspx"
            );

            routes.MapPageRoute(
                "searchForChannelPartnersByRegionCountryCityPageRoute",
                "{region}/{country}/{city}/channel-partners",
                "~/pages/SearchForChannelPartnersByRegionCountryCityPage.aspx"
            );

            routes.MapPageRoute(
                "searchForChannelPartnersByRegionCountryPageRoute",
                "{region}/{country}/channel-partners",
                "~/pages/SearchForChannelPartnersByRegionCountryPage.aspx"
            );

            routes.MapPageRoute(
                "searchForChannelPartnersByRegionPageRoute",
                "{region}/channel-partners",
                "~/pages/SearchForChannelPartnersByRegionPage.aspx"
            );

            routes.MapPageRoute(
                "profilesSubIndustriesForVendorsPageRoute",
                "partner-programs/{type}/{vertical}",
                "~/pages/ProfilesSubIndustriesForVendorsPage.aspx"
            );

            routes.MapPageRoute(
                "WhiteLabelSubIndustriesForVendorsPageRoute",
                "white-label/{type}/{vertical}",
                "~/pages/ProfilesSubIndustriesForVendorsPage.aspx"
            );

            routes.MapPageRoute(
                "MSPSSubIndustriesForVendorsPageRoute",
                "msps/{type}/{vertical}",
                "~/pages/ProfilesSubIndustriesForVendorsPage.aspx"
            );

            routes.MapPageRoute(
                "SystemIntegratorsSubIndustriesForVendorsPageRoute",
                "system-integrators/{type}/{vertical}",
                "~/pages/ProfilesSubIndustriesForVendorsPage.aspx"
            );

            routes.MapPageRoute(
                "profilesSubIndustriesForChannelPartnersPageRoute",
                "profile/{type}/{vertical}",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
                "profilesSubIndustriesForChannelPartnersTransPageRoute",
                "profile/{tr}/channel-partners/{vertical}",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
                "regionSubIndustriesForChannelPartnersPageRoute",
                "{region}/channel-partners/{vertical}",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
                "regionCountrySubIndustriesForChannelPartnersPageRoute",
                "{region}/{country}/channel-partners/{vertical}",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
                "regionCountryCitySubIndustriesForChannelPartnersPageRoute",
                "{region}/{country}/{city}/channel-partners/{vertical}",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
               "regionCountryCitySubIndustriesForChannelPartnersTransPageRoute",
               "{region}/{tr}/{country}/{city}/channel-partners/{vertical}",
               "~/pages/ProfilesSubIndustriesForChannelPartnersPage.aspx"
           );

            routes.MapPageRoute(
                "regionCountryCitySubIndustriesForChannelPartnersPageAllRoute",
                "{region}/{country}/{city}/all-channel-partners",
                "~/pages/ProfilesSubIndustriesForChannelPartnersPageAll.aspx"
            );

            routes.MapPageRoute(
               "regionCountryCitySubIndustriesForChannelPartnersPageAllTransRoute",
               "{region}/{tr}/{country}/{city}/all-channel-partners",
               "~/pages/ProfilesSubIndustriesForChannelPartnersPageAll.aspx"
           );

            //routes.MapPageRoute(
            //    "profilesPartnerTypesRoute",
            //    "{partnerProgram}/vendors/{vertical}",
            //    "~/ProfilesPartnerTypes.aspx"
            //);

            //routes.MapPageRoute(
            //    "SubIndustriesProfilesResultsRoute",
            //    "profile/{type}/{vertical}/{company}",
            //    "~/ProfilesSubIndustriesResults.aspx"
            //);

            routes.MapPageRoute(
                "aboutPageRoute",
                "about-us",
                "~/pages/AboutUsPage.aspx"
            );

            routes.MapPageRoute(
                "contactPageRoute",
                "contact-us",
                "~/pages/ContactUsPage.aspx"
            );

            routes.MapPageRoute(
                "forgotPageRoute",
                "reset-password",
                "~/pages/ResetPasswordPage.aspx"
            );

            routes.MapPageRoute(
                "forgotPartnerPageRoute",
                "{CompanyName}/partner-reset-password",
                "~/pages/ResetPasswordPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "faqPageRoute",
                "faq",
                "~/pages/FaqPage.aspx"
            );

            routes.MapPageRoute(
                "dashHelpCenterPageRoute",
                "dashboard/{id}/{company}/help-center",
                "~/DashboardHelpCenterPage.aspx"
            );

            routes.MapPageRoute(
                "termsConditionsPageRoute",
                "terms",
                "~/pages/TermsConditionsPage.aspx"
            );

            routes.MapPageRoute(
                "privacyPolicyPageRoute",
                "privacy",
                "~/pages/PrivacyPolicyPage.aspx"
            );

            routes.MapPageRoute(
                "myDataPageRoute",
                "my-data",
                "~/pages/MyDataPage.aspx"
            );

            //routes.MapPageRoute(
            //    "vendorsLandingRoute",
            //    "business-development-tool",
            //    "~/VendorsLanding.aspx"
            //);

            routes.MapPageRoute(
                "ErrorPageRoute",
                "Error-page",
                "~/ErrorPage.aspx"
            );

            routes.MapPageRoute(
                "404",
                "404",
                "~/404.aspx"
            );

            routes.MapPageRoute(
                "dashboard/404",
                "dashboard/404",
                "~/404.aspx"
            );

            routes.MapPageRoute(
                "dashboard405NotAuthenticatedPageRoute",
                "dashboard/405",
                "~/405_NotAuthenticatedPage.aspx"
            );

            //routes.MapPageRoute(
            //    "community/404",
            //    "community/404",
            //    "~/404.aspx"
            //);

            routes.MapPageRoute(
                "synergies",
                "synergies",
                "~/LandingPage_Synergies.aspx"
            );

            routes.MapPageRoute(
                "cloud",
                "cloud",
                "~/LandingPage_Cloud.aspx"
            );

            routes.MapPageRoute(
                "accelerate",
                "accelerate",
                "~/LandingPage_Accelerate.aspx"
            );

            //routes.MapPageRoute(
            //    "vendorlandingRoute",
            //    "vendor-free-sign-up",
            //    "~/VendorsLandingPage.aspx"
            //);

            routes.MapPageRoute(
                "thankyouRoute",
                "successful-registration",
                "~/ThankYouPage.aspx"
            );

            routes.MapPageRoute(
                "successfulpaymentRoute",
                "successful-payment",
                "~/SuccessfulPaymentPage.aspx"
            );

            routes.MapPageRoute(
                "cancelpaymentRoute",
                "canceled-payment",
                "~/CancelPaymentPage.aspx"
            );

            //routes.MapPageRoute(
            //    "resellerlandingRoute",
            //    "reseller-free-sign-up",
            //    "~/ResellersLandingPage.aspx"
            //);

            routes.MapPageRoute(
               "benefitsRoute",
               "benefits",
               "~/Benefits.aspx"
            );

            //routes.MapPageRoute(
            //    "profileVendorsPageRoute",
            //    "profiles/vendors/{id}/{company}",
            //    "~/pages/ProfileVendorsPage.aspx"
            //);

            routes.MapPageRoute(
                "profileLandingPageRoute",
                "profiles/{type}/{id}/{company}",
                "~/pages/ProfileLandingPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudiesPageRoute",
                "case-studies",
                "~/pages/CaseStudiesPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudyBIPageRoute",
                "case-study-bi",
                "~/pages/CaseStudyBiPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudyDataSecurityPageRoute",
                "case-study-data-security",
                "~/pages/CaseStudyDataSecurityPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudySaasPageRoute",
                "case-study-saas",
                "~/pages/CaseStudySaasPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudyPosPageRoute",
                "case-study-pos",
                "~/pages/CaseStudyPosPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudyEmailClientPageRoute",
                "case-study-email-client",
                "~/pages/CaseStudyEmailClientPage.aspx"
            );

            routes.MapPageRoute(
                "CaseStudyMobileAppPageRoute",
                "case-study-mobile-app",
                "~/pages/CaseStudyMobileAppPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesPageRoute",
                "resources-page",
                "~/pages/ResourcesPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesPartnerManagementSystemRoute",
                "partner-relationship-management-system",
                "~/pages/ResourcesPartnerManagementSystemPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesManageChannelPartnersRoute",
                "manage-channel-partners",
                "~/pages/ResourcesManageChannelPartners.aspx"
            );

            routes.MapPageRoute(
                "ResourcesPartnerManagementRoute",
                "partner-management",
                "~/pages/ResourcesPartnerManagement.aspx"
            );

            routes.MapPageRoute(
                "ResourcesPartnerRecruitmentProccessRoute",
                "channel-partner-recruitment-process",
                "~/pages/ResourcesPartnerRecruitmentProccess.aspx"
            );

            routes.MapPageRoute(
                "ResourcesPartneringExamplesRoute",
                "partnering-examples",
                "~/pages/ResourcesPartneringExamples.aspx"
            );

            routes.MapPageRoute(
                "ResourcesNetworksComptiaPageRoute",
                "networks/comptia",
                "~/pages/ResourcesNetworksComptiaPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesSpiceworksPageRoute",
                "networks/spiceworks",
                "~/pages/ResourcesAlternativesSpiceworksPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesAllboundRoute",
                "alternatives/allbound",
                "~/pages/ResourcesAlternativesAllboundPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesImpactCompanyRoute",
                "alternatives/impact-company",
                "~/pages/ResourcesAlternativesImpactCompany.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesImpartnerRoute",
                "alternatives/impartner",
                "~/pages/ResourcesAlternativesImpartnerPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesMindmatrixRoute",
                "alternatives/mindmatrix",
                "~/pages/ResourcesAlternativesMindmatrixPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesmodelNRoute",
                "alternatives/model-n",
                "~/pages/ResourcesAlternativesModelN.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesPartnerizeRoute",
                "alternatives/partnerize",
                "~/pages/ResourcesAlternativesPartnerize.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesSalesforceCommunitiesRoute",
                "alternatives/salesforce-communities",
                "~/pages/ResourcesAlternativesSalesforceCommunitiesPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesZiftSolutionsRoute",
                "alternatives/zift-solutions",
                "~/pages/ResourcesAlternativesZiftSolutionsPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesPartnerStackRoute",
                "alternatives/partnerstack",
                "~/pages/ResourcesAlternativesPartnerStackPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesMyPrmRoute",
                "alternatives/myprm",
                "~/pages/ResourcesAlternativesMyPrmPage.aspx"
            );

            routes.MapPageRoute(
                "ResourcesAlternativesChanneltivityRoute",
                "alternatives/channeltivity",
                "~/pages/ResourcesAlternativesChanneltivity.aspx"
            );

            routes.MapPageRoute(
                "PrmSoftwareTransPageRoute",
                "{lang}/prm-software",
                "~/pages/PrmSoftwarePage.aspx"
            );

            routes.MapPageRoute(
                "PrmSoftwarePageRoute",
                "prm-software",
                "~/pages/PrmSoftwarePage.aspx"
            );

            routes.MapPageRoute(
                "PrmFeaturesPageRoute",
                "prm-software/{feature}",
                "~/pages/PrmFeaturesPage.aspx"
            );

            routes.MapPageRoute(
                "recruitmentChannelPartnerPageRoute",
                "channel-partner-recruitment",
                "~/pages/RecruitmentChannelPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "recruitmentChannelPartnerAutomatePageRoute",
                "channel-partner-recruitment-automation",
                "~/pages/RecruitmentChannelPartnerAutomatePage.aspx"
            );

            routes.MapPageRoute(
                "recruitmentChannelPartnerDatabasePageRoute",
                "channel-partner-recruitment-database",
                "~/pages/RecruitmentChannelPartnerDatabasePage.aspx"
            );

            routes.MapPageRoute(
                "MAMLandingRoute",
                "mergers-acquisition-marketplace",
                "~/pages/MAMLandingPage.aspx"
            );

            routes.MapPageRoute(
                "partnerPortalLocatorPageRoute",
                "partner-portals",
                "~/pages/PartnerPortalLocatorPage.aspx"
            );

            routes.MapPageRoute(
                "IntentSignalRoute",
                "intent-signals",
                "~/pages/IntentSignalPage.aspx"
            );

            routes.MapPageRoute(
                "IntentSignalByCountryProductPageRoute",
                "intent-signals/{countryOrProduct}",
                "~/pages/IntentSignalByCountryProductPage.aspx"
            );

            routes.MapPageRoute(
                "ReferralSoftwarePageRoute",
                "referral-software",
                "~/pages/ReferralSoftwarePage.aspx"
            );

            routes.MapPageRoute(
                "ReferralSoftwareGetAccessPageRoute",
                "referral-software-get-access",
                "~/pages/ReferralSoftwareGetAccessPage.aspx"
            );

            routes.MapPageRoute(
                "RequestQuotePageSingleRoute",
                "get-a-quote",
                "~/pages/RequestQuotePage.aspx"
            );

            routes.MapPageRoute(
                "RequestQuotePageRoute",
                "{id}/get-a-quote",
                "~/pages/RequestQuotePage.aspx"
            );

            routes.MapPageRoute(
                "RequestQuotePageTransRoute",
                "{tr}/{id}/get-a-quote",
                "~/pages/RequestQuotePage.aspx"
            );

            routes.MapPageRoute(
                "MessageQuotePageRoute",
                "{id}/send-a-quote",
                "~/pages/MessageQuotePage.aspx"
            );

            routes.MapPageRoute(
                "ClaimProfilePageRoute",
                "{id}/claim-profile",
                "~/pages/ClaimProfilePage.aspx"
            );

            #endregion

            #region Elio Dashboard pages area

            routes.MapPageRoute(
                "company_profile",
                "dashboard/{id}/{company}",
                "~/Dashboard.aspx"
            );

            routes.MapPageRoute(
                "dashHomeRoute",
                "dashboard/{id}/{company}/home",
                "~/DashboardHomePage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnerOnBoardingPageRoute",
                "dashboard/{id}/{company}/partner-onboarding",
                "~/DashboardPartnerOnboardingPageNew.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingAddNewPageRoute",
                "dashboard/{id}/{company}/partner-training-add-new",
                "~/DashboardTrainingAddNewPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingOverviewPageRoute",
                "dashboard/{id}/{company}/partner-training-overview",
                "~/DashboardTrainingOverviewPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingCoursesOverviewPageRoute",
                "dashboard/{id}/{company}/partner-training-courses-overview",
                "~/DashboardTrainingCoursesOverviewPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingAnalyticsPageRoute",
                "dashboard/{id}/{company}/partner-training-analytics",
                "~/DashboardTrainingAnalyticsPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingCourseAnalyticsPageRoute",
                "dashboard/{id}/{company}/{courseId}/course-training-analytics",
                "~/DashboardTrainingCourseAnalyticsPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingLibraryPageRoute",
                "dashboard/{id}/{company}/partner-training-library",
                "~/DashboardTrainingLibraryPage.aspx"
            );

            routes.MapPageRoute(
                "dashTrainingCourseViewPageRoute",
                "dashboard/{id}/{company}/{courseid}/partner-training-course",
                "~/DashboardTrainingCourseViewPage.aspx"
            );

            routes.MapPageRoute(
                "dashInboxContentRoute",
                "dashboard/{id}/{company}/messages/inbox",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashInboxDeletedRoute",
                "dashboard/{id}/{company}/messages/deleted",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashInboxSentRoute",
                "dashboard/{id}/{company}/messages/sent",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashInboxComposeRoute",
                "dashboard/{id}/{company}/messages/compose",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashInboxViewRoute",
                "dashboard/{id}/{company}/messages/{message}/view",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashInboxReplyRoute",
                "dashboard/{id}/{company}/messages/{message}/reply",
                "~/DashboardMessages.aspx"
            );

            routes.MapPageRoute(
                "dashLeadsRoute",
                "dashboard/{id}/{company}/leads",
                "~/DashboardLeads.aspx"
            );

            routes.MapPageRoute(
                "dashEmptyRoute",
                "dashboard/{id}/{company}/empty-page",
                "~/DashboardEmptyPage.aspx"
            );

            routes.MapPageRoute(
                "dashMyMatchesPageRoute",
                "dashboard/{id}/{company}/find-new-partners",
                "~/DashboardMyMatchesPage.aspx"
            );

            routes.MapPageRoute(
                "dashMyMatchesPageNewRoute",
                "dashboard/{id}/{company}/find-new-partners-admin",
                "~/DashboardMyMatchesPageNew.aspx"
            );

            routes.MapPageRoute(
                "dashMyMatchesCriteriaSelectionPageRoute",
                "dashboard/{id}/{company}/find-new-partners-criteria",
                "~/DashboardMyMatchesCriteriaSelectionPage.aspx"
            );

            routes.MapPageRoute(
                "dashRecommendedChannelPartnersPageRoute",
                "dashboard/{id}/{company}/find-new-recommended-channel-partners",
                "~/DashboardFindRecommendedChannelPartnersPage.aspx"
            );

            routes.MapPageRoute(
                "dashDownloadsRoute",
                "dashboard/{id}/{company}/download-csv",
                "~/Downloads.aspx"
            );

            routes.MapPageRoute(
                "dashDownloadsInvoicesRoute",
                "dashboard/{id}/{company}/download-invoices",
                "~/Downloads.aspx"
            );

            routes.MapPageRoute(
                "dashPersonProfileRoute",
                "connection-profile/{id}/{company}",
                "~/PersonProfile.aspx"
            );

            routes.MapPageRoute(
                "dashAlgorithmRoute",
                "dashboard/{id}/{company}/algorithm",
                "~/DashboardAlgorithm.aspx"
            );

            routes.MapPageRoute(
                "dashBillingPageRoute",
                "dashboard/{id}/{company}/billing",
                "~/DashboardBillingPage.aspx"
            );

            routes.MapPageRoute(
                "dashBillingSelfServicePageRoute",
                "dashboard/{id}/{company}/billing-99",
                "~/DashboardBillingSelfServicePage.aspx"
            );

            routes.MapPageRoute(
                "dashConnectionsRoute",
                "dashboard/{id}/{company}/new-connections",
                "~/DashboardConnections.aspx"
            );

            routes.MapPageRoute(
                "dashNewClientsRoute",
                "dashboard/{id}/{company}/new-clients",
                "~/DashboardNewClients.aspx"
            );

            routes.MapPageRoute(
                "dashNewClientsSignUpRoute",
                "dashboard/{id}/{company}/new-clients-sign-up",
                "~/DashboardNewClientsSignUp.aspx"
            );

            routes.MapPageRoute(
                "multiaccountsfullregistrationRoute",
                "multi-accounts-full-registration",
                "~/MultiAccountsFullRegistration.aspx"
            );

            routes.MapPageRoute(
                "dashOpportunitiesViewRoute",
                "dashboard/{id}/{company}/opportunities",
                "~/DashboardOpportunitiesPage.aspx"
            );

            routes.MapPageRoute(
                "dashOpportunitiesTasksPageRoute",
                "dashboard/{id}/{company}/opportunities-tasks",
                "~/DashboardOpportunitiesTasksPage.aspx"
            );

            routes.MapPageRoute(
                "dashOpportunitiesAddEditPageRoute",
                "dashboard/{id}/{company}/opportunities-add-edit",
                "~/DashboardOpportunitiesAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashOpportunitiesViewNotesPageRoute",
                "dashboard/{id}/{company}/opportunities-view-notes",
                "~/DashboardOpportunitiesViewNotesPage.aspx"
            );

            routes.MapPageRoute(
                "dashOpportunitiesViewTasksPageRoute",
                "dashboard/{id}/{company}/opportunities-view-tasks",
                "~/DashboardOpportunitiesViewTasksPage.aspx"
            );

            routes.MapPageRoute(
               "taskReminderSenderPageRoute",
               "task-reminder",
               "~/TaskReminderSenderPage.aspx"
           );

            routes.MapPageRoute(
                "dashDealRegistrationPageRoute",
                "dashboard/{id}/{company}/deal-registration",
                "~/DashboardDealRegistrationPage.aspx"
            );

            routes.MapPageRoute(
                "dashDealRegistrationAddEditPageRoute",
                "dashboard/{id}/{company}/deal-registration-add-edit",
                "~/DashboardDealRegistrationAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashDealRegistrationViewPageRoute",
                "dashboard/{id}/{company}/deal-registration-view",
                "~/DashboardDealRegistrationViewPage.aspx"
            );

            routes.MapPageRoute(
               "dashRandstadDealsPageRoute",
               "dashboard/{id}/{company}/deals",
               "~/DashboardRandstadDealsPage.aspx"
           );

            routes.MapPageRoute(
                "dashRandstadDealsAddEditPageRoute",
                "dashboard/{id}/{company}/deals-add-edit",
                "~/DashboardRandstadDealsAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashRandstadDealsViewPageRoute",
                "dashboard/{id}/{company}/deals-view",
                "~/DashboardRandstadDealsViewPage.aspx"
            );

            routes.MapPageRoute(
               "dashGavsDealsPageRoute",
               "dashboard/{id}/{company}/deals-365",
               "~/DashboardGavsDealsPage.aspx"
           );

            routes.MapPageRoute(
                "dashGavsDealsAddEditPageRoute",
                "dashboard/{id}/{company}/deals-add-edit-365",
                "~/DashboardGavsDealsAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashGavsDealsViewPageRoute",
                "dashboard/{id}/{company}/deals-view-365",
                "~/DashboardGavsDealsViewPage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnerToPartnerPageRoute",
                "dashboard/{id}/{company}/partner-to-partner",
                "~/DashboardPartnerToPartnerPage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnerToPartnerAddEditPageRoute",
                "dashboard/{id}/{company}/partner-to-partner-add-edit",
                "~/DashboardPartnerToPartnerAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashLeadDistributionPageRoute",
                "dashboard/{id}/{company}/lead-distribution",
                "~/DashboardLeadDistributionPage.aspx"
            );

            routes.MapPageRoute(
                "dashLeadDistributionAddEditPageRoute",
                "dashboard/{id}/{company}/lead-distribution-add-edit",
                "~/DashboardLeadDistributionAddEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashLeadDistributionViewPageRoute",
                "dashboard/{id}/{company}/lead-distribution-view",
                "~/DashboardLeadDistributionViewPage.aspx"
            );

            routes.MapPageRoute(
                "dashDealPartnerDirectoryRoute",
                "dashboard/{id}/{company}/partner-directory",
                "~/DashboardDealPartnerDirectory.aspx"
            );

            routes.MapPageRoute(
                "dashDealPartnerDirectoryInviteNewRoute",
                "dashboard/{id}/{company}/partner-directory-invitations",
                "~/DashboardDealPartnerDirectoryInviteNew.aspx"
            );

            routes.MapPageRoute(
                "dashPDManagePartnersPageRoute",
                "dashboard/{id}/{company}/manage-partners",
                "~/DashboardPDManagePartnersPage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnersCommissionsBillingPageRoute",
                "dashboard/{id}/{company}/partner-commissions-billing",
                "~/DashboardPartnersCommissionsBillingPage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnersCommissionsPaymentsPageRoute",
                "dashboard/{id}/{company}/partner-commissions-payments",
                "~/DashboardPartnersCommissionsPaymentsPage.aspx"
            );

            routes.MapPageRoute(
                "dashPartnersCommissionsFeesPageRoute",
                "dashboard/{id}/{company}/partner-commissions-fees",
                "~/DashboardPartnersCommissionsFeesPage.aspx"
            );

            routes.MapPageRoute(
                "dashPDPartnersRequestsPageRoute",
                "dashboard/{id}/{company}/partners-requests",
                "~/DashboardPDPartnersRequestsPage.aspx"
            );

            routes.MapPageRoute(
                "dashPDPartnersSendInvitationsPageRoute",
                "dashboard/{id}/{company}/partners-invitations",
                "~/DashboardPDPartnersSendInvitationsPage.aspx"
            );

            routes.MapPageRoute(
                "dashAnalyticsGeneralPageRoute",
                "dashboard/{id}/{company}/general-analytics",
                "~/DashboardAnalyticsGeneralPage.aspx"
            );

            routes.MapPageRoute(
                "dashAnalyticsSalesLeaderBoardPageRoute",
                "dashboard/{id}/{company}/sales-leaderboard-analytics",
                "~/DashboardAnalyticsSalesLeaderBoardPage.aspx"
            );

            routes.MapPageRoute(
                "dashAnalyticsActivePartnersPageRoute",
                "dashboard/{id}/{company}/active-partners-analytics",
                "~/DashboardAnalyticsActivePartnersPage.aspx"
            );

            routes.MapPageRoute(
                "dashTierManagementPageRoute",
                "dashboard/{id}/{company}/tier-management",
                "~/DashboardTierManagementPage.aspx"
            );

            routes.MapPageRoute(
                "dashManagementTierPageRoute",
                "dashboard/{id}/{company}/management-tier",
                "~/DashboardManagementTierPage.aspx"
            );

            routes.MapPageRoute(
                "dashPermissionsRolesManagementPageRoute",
                "dashboard/{id}/{company}/permissions-roles-management",
                "~/DashboardPermissionsRolesManagementPage.aspx"
            );

            routes.MapPageRoute(
                "dashIntegrationsPageRoute",
                "dashboard/{id}/{company}/integrations",
                "~/DashboardIntegrationsPage.aspx"
            );

            routes.MapPageRoute(
                "dashTeamPageRoute",
                "dashboard/{id}/{company}/team",
                "~/DashboardTeamPage.aspx"
            );

            routes.MapPageRoute(
                "dashTeamEditPageRoute",
                "dashboard/{id}/{company}/team-edit",
                "~/DashboardTeamEditPage.aspx"
            );

            routes.MapPageRoute(
                "dashCollaborationInvitationsRoute",
                "dashboard/{id}/{company}/collaboration-invitations",
                "~/DashboardCollaborationInvitations.aspx"
            );

            routes.MapPageRoute(
               "dashCollaborationCreateNewInvitationsRoute",
               "dashboard/{id}/{company}/collaboration-create-new-partners",
               "~/DashboardCollaborationCreateNewInvitations.aspx"
           );

            routes.MapPageRoute(
                "dashCollaborationNewPartnersRoute",
                "dashboard/{id}/{company}/collaboration-new-partners",
                "~/DashboardCollaborationNewPartners.aspx"
            );

            routes.MapPageRoute(
                "dashCollaborationPartnersRoute",
                "dashboard/{id}/{company}/collaboration-partners",
                "~/DashboardCollaborationPartners.aspx"
            );

            routes.MapPageRoute(
                "dashCollaborationMainContentRoute",
                "dashboard/{id}/{company}/collaboration-mailbox",
                "~/DashboardCollaborationMainContent.aspx"     //"~/DashboardCollaborationMailBox.aspx"
            );

            routes.MapPageRoute(
               "dashCollaborationChatRoomPageRoute",
               "dashboard/{id}/{company}/collaboration-chat-room",
               "~/DashboardCollaborationChatRoomPageNew.aspx"     //"~/DashboardCollaborationMailBox.aspx"
           );

            routes.MapPageRoute(
                "dashCollaborationChooseVendorsPageRoute",
                "dashboard/{id}/{company}/collaboration-choose-partners",
                "~/DashboardCollaborationChoosePartnersToViewPage.aspx"
            );

            routes.MapPageRoute(
                "dashCollaborationLibraryPageRoute",
                "dashboard/{id}/{company}/collaboration-library/{key}",
                "~/DashboardCollaborationLibraryPageNew.aspx"
            );

            routes.MapPageRoute(
                "dashEditRoute",
                "dashboard/{id}/{company}/edit-company-profile",
                "~/DashboardEditProfile.aspx"
            );

            routes.MapPageRoute(
                "dashFavouritesRoute",
                "dashboard/{id}/{company}/favourites",
                "~/DashboardFavourites.aspx"
            );

            routes.MapPageRoute(
                "dashInfoLogsRoute",
                "dashboard/{id}/{company}/info-logs",
                "~/DashboardInfoLogs.aspx"
            );

            routes.MapPageRoute(
                "dashClearbitLogsRoute",
                "dashboard/{id}/{company}/clearbit-logs",
                "~/DashboardClearbitLogs.aspx"
            );

            routes.MapPageRoute(
                "dashLogsRoute",
                "dashboard/{id}/{company}/logs",
                "~/DashboardLogs.aspx"
            );

            routes.MapPageRoute(
                "dashAnonymousLogsRoute",
                "dashboard/{id}/{company}/anonymous-logs",
                "~/DashboardAnonymousLogs.aspx"
            );

            routes.MapPageRoute(
                "dashStatisticsRoute",
                "dashboard/{id}/{company}/statistics",
                "~/DashboardStatistics.aspx"
            );

            routes.MapPageRoute(
                "dashIntentSignalsPageRoute",
                "dashboard/{id}/{company}/intent-signals",
                "~/DashboardIntentSignalsPage.aspx"
            );

            routes.MapPageRoute(
                "AdminElioFinancialIncomeFlowPageRoute",
                "admin-elio-financial-income-flow-page",
                "~/AdminElioFinancialIncomeFlowPage.aspx"
            );

            routes.MapPageRoute(
                "AdminElioFinancialExpensesFlowPageRoute",
                "admin-elio-financial-expenses-flow-page",
                "~/AdminElioFinancialExpensesFlowPage.aspx"
            );

            routes.MapPageRoute(
                "AdminRoute",
                "admin-page",
                "~/AdminPage.aspx"
            );

            routes.MapPageRoute(
                "AdminManagementRoute",
                "admin-user-management",
                "~/Management/AdminUserManagement.aspx"
            );

            routes.MapPageRoute(
                "AdminDemoRequestsManagementRoute",
                "dashboard/{id}/{company}/admin-demo-requests-management",
                "~/AdminDemoRequestsManagement.aspx"
            );

            routes.MapPageRoute(
               "AdminRFPsRequestsManagementRoute",
               "dashboard/{id}/{company}/admin-rfps-requests-management",
               "~/AdminRFPsRequestsManagement.aspx"
           );

            routes.MapPageRoute(
               "AdminRFPsMessagesManagementRoute",
               "dashboard/{id}/{company}/admin-rfps-messages-management",
               "~/AdminRFPsMessagesManagement.aspx"
           );

            routes.MapPageRoute(
                "AdminStatisticsRoute",
                "admin-statistics-page",
                "~/AdminStatisticsPage.aspx"
            );

            routes.MapPageRoute(
                "AdminAddThirdPartyUsersRoute",
                "add-third-party-users-page",
                "~/AdminAddThirdPartyUsersPage.aspx"
                );

            routes.MapPageRoute(
                "AdminRegistrationProductsRoute",
                "registration-products-page",
                "~/AdminRegistrationProductsPage.aspx"
                );

            routes.MapPageRoute(
                "AdminAddIntentSignalsDataRoute",
                "add-intent-signals-data-page",
                "~/AdminAddIntentSignalsDataPage.aspx"
                );

            routes.MapPageRoute(
                "critSelectRoute",
                "full-registration/{id}/criteria-selection",
                "~/CriteriaSelection.aspx"
            );

            //routes.MapPageRoute(
            //    "PrmSoftwareFeaturesRoute",
            //    "prm-software-features",
            //    "~/PrmSoftwareFeatures.aspx"
            //);

            //routes.MapPageRoute(
            //    "PrmWithNoSoftwareFeaturesRoute",
            //    "prm/{feature}",
            //    "~/PrmWithNoSoftwareFeatures.aspx"
            //);            

            routes.MapPageRoute(
                "channelPartnersLandingRoute",
                "channel-partners",
                "~/ChannelPartnersLandingPage.aspx"
            );

            routes.MapPageRoute(
                "partnerToPartnerDealsLandingRoute",
                "partner-to-partner-deals",
                "~/PartnerToPartnerDealsLandingPage.aspx"
            );

            routes.MapPageRoute(
                "channelPartnerLocatorRoute",
                "{company}/partner-locator",
                "~/ChannelPartnerLocator.aspx"
            );

            #endregion
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session.Timeout = 240;
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            //////Sessions.Remove(Session.SessionID);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            try
            {
                HangfireBootstrapper.Instance.Stop();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}