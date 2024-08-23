using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using ICSharpCode.SharpZipLib.Tar;
using System.Web.Mvc;
using System.Web;

namespace WdS.ElioPlus.pages
{
    public partial class PrmFeaturesPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    InitData();
                    SetLinks();
                    FixPage();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixPage()
        {
            aGetStartedFree.Visible = aSIgnUp1.Visible = aFreeSignUp.Visible = aSignUp.Visible = vSession.User == null;
        }

        private void InitData()
        {
            string feature = (HttpContext.Current.Request.Url.Segments.Length > 2) ? HttpContext.Current.Request.Url.Segments[2] : "";

            //if (feature.Contains("integration"))
            //{
            //    //divFeatures.Visible = false;
            //    //divIntegrations.Visible = true;

            //    switch (feature)
            //    {
            //        case "crm-integrations":
            //            //InitCrmIntegrations();
            //            //aCrmIntegrations.Attributes["class"] = "active";
            //            aPrevious.Visible = false;
            //            aNext.HRef = ControlLoader.PrmSoftwareHubspotIntegration;
            //            break;

            //        case "hubspot-integration":
            //            //InitHubspotIntegration();
            //            //aHubspotIntegration.Attributes["class"] = "active";
            //            aPrevious.Visible = true;
            //            aPrevious.HRef = ControlLoader.PrmSoftwareCrmIntegrations;
            //            aNext.HRef = ControlLoader.PrmSoftwareSalesforceIntegration;
            //            break;

            //        case "salesforce-integration":
            //            //InitSalesforceIntegration();
            //            //aSalesforceIntegration.Attributes["class"] = "active";
            //            aPrevious.HRef = ControlLoader.PrmSoftwareHubspotIntegration;
            //            aNext.Visible = false;
            //            break;
            //    }
            //}
            //else
            //{
            //divFeatures.Visible = true;
            //divIntegrations.Visible = false;

            switch (feature)
            {
                case "partner-portal":
                    InitPartnerPortal();
                    aPartnerPortal.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.Visible = false;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerDirectory;
                    break;
                case "partner-portal-feature":
                    Response.Redirect("/prm-software/partner-portal", false);
                    break;
                case "partner-directory":
                    InitPartnerDirectory();
                    aPartnerDirectory.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerPortal;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerOnboarding;
                    break;
                case "partner-directory-feature":
                    Response.Redirect("/prm-software/partner-directory", false);
                    break;
                case "partner-onboarding":
                    InitPartnerOnboarding();
                    aPartnerOnboarding.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerDirectory;
                    aNext.HRef = ControlLoader.PrmSoftwareDealRegistration;
                    break;
                case "partner-onboarding-feature":
                    Response.Redirect("/prm-software/partner-onboarding", false);
                    break;
                case "deal-registration":
                    InitDealRegistration();
                    aDealRegistration.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerOnboarding;
                    aNext.HRef = ControlLoader.PrmSoftwareLeadDistribution;
                    break;
                case "deal-registration-feature":
                    Response.Redirect("/prm-software/deal-registration", false);
                    break;
                case "lead-distribution":
                    InitLeadDistribution();
                    aLeadDistribution.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareDealRegistration;
                    aNext.HRef = ControlLoader.PrmSoftwareContentManagement;
                    break;
                case "lead-distribution-feature":
                    Response.Redirect("/prm-software/lead-distribution", false);
                    break;
                case "content-management":
                    InitContentManagement();
                    aContentManagement.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareLeadDistribution;
                    aNext.HRef = ControlLoader.PrmSoftwareCollaboration;
                    break;
                case "content-management-feature":
                    Response.Redirect("/prm-software/content-management", false);
                    break;
                case "collaboration":
                    InitCollaboration();
                    aCollaboration.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareContentManagement;
                    aNext.HRef = ControlLoader.PrmSoftwareChannelAnalytics;
                    break;
                case "collaboration-feature":
                    Response.Redirect("/prm-software/collaboration", false);
                    break;
                case "channel-analytics":
                    InitChannelAnalytics();
                    aChannelAnalytics.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareCollaboration;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerLocator;
                    break;
                case "channel-analytics-feature":
                    Response.Redirect("/prm-software/channel-analytics", false);
                    break;
                case "partner-locator":
                    InitPartnerLocator();
                    aPartnerLocator.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareChannelAnalytics;
                    aNext.HRef = ControlLoader.PrmSoftwarePartner2Partner;
                    break;
                case "partner-locator-feature":
                    Response.Redirect("/prm-software/partner-locator", false);
                    break;
                case "partner-2-partner":
                    InitPartner2Partner();
                    aPartner2Partner.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerLocator;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerActivation;
                    break;
                case "partner-2-partner-feature":
                    Response.Redirect("/prm-software/partner-2-partner", false);
                    break;
                case "partner-activation":
                    InitPartnerActivation();
                    aPartnerActivation.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartner2Partner;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerTierManagement;
                    break;
                case "partner-activation-feature":
                    Response.Redirect("/prm-software/partner-activation", false);
                    break;
                case "tier-management":
                    InitPartnerTierManagement();
                    aPartnerTierManagement.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerActivation;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerTeamRoles;
                    break;
                case "tier-management-feature":
                    Response.Redirect("/prm-software/partner-tier-management", false);
                    break;
                case "team-roles":
                    InitPartnerTeamRoles();
                    aPartnerTeamRoles.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerTierManagement;
                    aNext.HRef = ControlLoader.PrmSoftwarePartnerManagement;
                    break;
                case "team-roles-feature":
                    Response.Redirect("/prm-software/partner-team-roles", false);
                    break;
                case "partner-management":
                    InitPartnerManagement();
                    aPartnerManagement.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerTeamRoles;
                    aNext.HRef = ControlLoader.PrmSoftwareCrmIntegrations;
                    break;
                case "partner-management-feature":
                    Response.Redirect("/prm-software/partner-management", false);
                    break;
                case "crm-integrations":
                    InitCrmIntegrations();
                    aCrmIntegrations.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwarePartnerManagement;
                    aNext.HRef = ControlLoader.PrmSoftwareHubspotIntegration;
                    break;
                case "crm-integrations-feature":
                    Response.Redirect("/prm-software/crm-integrations", false);
                    break;
                case "hubspot-integration":
                    InitHubspotIntegrations();
                    aHubspotIntegration.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareCrmIntegrations;
                    aNext.HRef = ControlLoader.PrmSoftwareSalesforceIntegration;
                    break;
                case "hubspot-integration-feature":
                    Response.Redirect("/prm-software/hubspot-integration", false);
                    break;
                case "salesforce-integration":
                    InitSalesforceIntegrations();
                    aSalesforceIntegration.Attributes["class"] = "text-base active text-textGray";
                    aPrevious.HRef = ControlLoader.PrmSoftwareHubspotIntegration;
                    aNext.Visible = false;
                    break;
                case "salesforce-integration-feature":
                    Response.Redirect("/prm-software/salesforce-integration", false);
                    break;

                default:
                    if (vSession.User != null)
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                    else
                        Response.Redirect(ControlLoader.PrmSoftware, false);
                    break;
            }
            //}
        }

        private void InitCrmIntegrations()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Elioplus PRM one click integration with most CRMs in the market enables seamless data flow for deal registration and lead distribution activities between your company and your channel partners.";
            metaKeywords.Attributes["content"] = "elioplus prm to crm integration, prm and crm integration";

            div18.Visible = div018.Visible = true;
        }

        private void InitHubspotIntegrations()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Elioplus PRM one click integration with Hubspot CRM & marketing automation enables seamless data flow for deal registration and lead distribution.";
            metaKeywords.Attributes["content"] = "elioplus prm hubspot integration, prm Hubspot integration";

            div19.Visible = div019.Visible = true;
        }

        private void InitSalesforceIntegrations()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Elioplus PRM one click integration with Salesforce CRM enables seamless data flow for deal registration and lead distribution activities between your company and your partners.";
            metaKeywords.Attributes["content"] = "elioplus prm salesforce integration, prm salesforce integration";

            div20.Visible = div020.Visible = true;
        }

        private void InitContentManagement()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Distribute files to your channel partners, groups or tiers and create unlimited categories for content management through Elioplus PRM.";
            metaKeywords.Attributes["content"] = "content management, PRM content management, PRM library, PRM files, PRM materials";
            //ltrHeader.Text = "Content Management";
            div17.Visible = div017.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;
            //slide4.Visible = true;

            //ltrNo1.Text = "1 / 4";
            //ltrNo2.Text = "2 / 4";
            //ltrNo3.Text = "3 / 4";
            //ltrNo4.Text = "4 / 4";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management2.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management_add_new_category.png";
            //img4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management_select_partner.png";

            //img1.Alt = "Content Management";
            //img2.Alt = "Content Management 2";
            //img3.Alt = "Add new category";
            //img4.Alt = "Select partner";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;
            //col4.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management2.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management_add_new_category.png";
            //gal4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Content_Management_select_partner.png";

            //gal1.Alt = "1. partner management team";
            //gal2.Alt = "2. partner management activity";
            //gal3.Alt = "3. partner management analytics";
            //gal4.Alt = "4. partner management analytics";
        }

        private void InitPartnerManagement()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Scale your partner management efforts by having a 360 degree view for each channel partner concerning their activity, revenue and performance statistics.";
            metaKeywords.Attributes["content"] = "partner management, channel partner management";
            //ltrHeader.Text = "Partner management";
            div13.Visible = div013.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;

            //ltrNo1.Text = "1 / 3";
            //ltrNo2.Text = "2 / 3";
            //ltrNo3.Text = "3 / 3";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Team.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Activity.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Analytics.png";

            //img1.Alt = "partner management team";
            //img2.Alt = "partner management activity";
            //img3.Alt = "partner management analytics";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Team.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Activity.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Partner_Management_Analytics.png";

            //gal1.Alt = "1. partner management team";
            //gal2.Alt = "2. partner management activity";
            //gal3.Alt = "3. partner management analytics";
        }

        private void InitPartnerTeamRoles()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Invite unlimited team members to your partner portal and give them permissions based on their role.";
            metaKeywords.Attributes["content"] = "prm permissions, prm team roles, prm team roles permissions";
            //ltrHeader.Text = "Team Roles and Permissions";
            div12.Visible = div012.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;

            //ltrNo1.Text = "1 / 3";
            //ltrNo2.Text = "2 / 3";
            //ltrNo3.Text = "3 / 3";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Create_roles.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Roles_permissions.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Team.png";

            //img1.Alt = "Create roles";
            //img2.Alt = "Roles permissions";
            //img3.Alt = "Team";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Create_roles.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Roles_permissions.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Team.png";

            //gal1.Alt = "1. Create team roles";
            //gal2.Alt = "2. Give roles permissions";
            //gal3.Alt = "3. Team";
        }

        private void InitPartnerTierManagement()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Create unlimited tiers to manage your partners’ commissions and access to specific partner portal features.";
            metaKeywords.Attributes["content"] = "prm tier management, tier management, how to create partner tiers";
            //ltrHeader.Text = "Tier Management";
            div11.Visible = div011.Visible = true;

            //slide1.Visible = true;

            //ltrNo1.Text = "1 / 1";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Tier_management_categories.png";

            //img1.Alt = "Tier management";

            //col1.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Tier_Management.png";

            //gal1.Alt = "1. tier management";
        }

        private void InitPartnerActivation()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Activate your partners via triggered notifications so they don’t miss new material & documentation, leads and deal registration notifications.";
            metaKeywords.Attributes["content"] = "partner activation, triggered notifications, prm email notifications, prm triggered notifications";
            //ltrHeader.Text = "Keep your partners active through trigger notifications";
            div10.Visible = div010.Visible = true;
        }

        private void InitPartner2Partner()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Your channel partners can collaborate each other in order to win new deals and increase revenue. They can also collaborate with other " +
                                "resellers of Elioplus network.";
            metaKeywords.Attributes["content"] = "Channel sales, channel partners collaboration, channel revenue, channel partners revenue, channel opportunities, channel partners opportunities";
            //ltrHeader.Text = "Partner 2 Partner";
            div9.Visible = div09.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;
            //slide4.Visible = true;
            //slide5.Visible = true;

            //ltrNo1.Text = "1 / 5";
            //ltrNo2.Text = "2 / 5";
            //ltrNo3.Text = "3 / 5";
            //ltrNo4.Text = "4 / 5";
            //ltrNo5.Text = "5 / 5";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_2.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_3.png";
            //img4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_4.png";
            //img5.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_5.png";

            //img1.Alt = "deal request through P2P";
            //img2.Alt = "receive a notification";
            //img3.Alt = "express their interest";
            //img4.Alt = "Partner 2 Partner tab";
            //img5.Alt = "Messages tab receive";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;
            //col4.Visible = true;
            //col5.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_2.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_3.png";
            //gal4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_4.png";
            //gal5.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner2Partner_5.png";

            //gal1.Alt = "1. A channel partner opens a deal request through P2P form and sends it to those channel partners that fit the appropriate criteria";
            //gal2.Alt = "2. Channel partners that fit the criteria receive a notification";
            //gal3.Alt = "3. Channel  partners express their interest";
            //gal4.Alt = "4. The Deal can be seen as Open in the Partner 2 Partner tab";
            //gal5.Alt = "5. The channel partner can check the responses into the Messages tab and receive a notification email at the same time";
        }

        private void InitPartnerLocator()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Add your unique partner locator link on your website and provide leads to your partners from the visitors of your webpage. " +
                                "All your resellers accessible in one page.";
            metaKeywords.Attributes["content"] = "Partner locator, partner portal, partner location, PRM, Partner Relationship Management";
            //ltrHeader.Text = "Partner Locator";
            div8.Visible = div08.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;

            //ltrNo1.Text = "1 / 2";
            //ltrNo2.Text = "2 / 2";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_partner_locator.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Partner_locator_2.png";

            //img1.Alt = "locator URL link";
            //img2.Alt = "locator page";

            //col1.Visible = true;
            //col2.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Partner_locator_1.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Partner_locator_2.png";

            //gal1.Alt = "1. Partner locator unique URL link";
            //gal2.Alt = "2. Partner locator page";
        }

        private void InitChannelAnalytics()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Channel analytics for your indirect sales performance. How much revenue have you gained from each reseller and what to expect in the next months. " +
                                "Deals won rate, leads won rate and more.";
            metaKeywords.Attributes["content"] = "Channel analytics, PRM channel analytics, channel sales analytics, channel sales statistics, partners performance";
            //ltrHeader.Text = "Channel Analytics";
            div7.Visible = div07.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;

            //ltrNo1.Text = "1 / 2";
            //ltrNo2.Text = "2 / 2";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_analytics.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_deals_and_leads_stats.png";

            //img1.Alt = "Analytics";
            //img2.Alt = "Deals and leads statistics";

            //col1.Visible = true;
            //col2.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_analytics.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_deals_and_leads_stats.png";

            //gal1.Alt = "1. Analytics";
            //gal2.Alt = "2. Deals and leads statistics";
        }

        private void InitCollaboration()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Collaborate real time with your channel partners. Distribute material and exchange messages. Create unlimited groups and have your own library.";
            metaKeywords.Attributes["content"] = "Partner locator, partner portal, partner location, PRM, Partner Relationship Management";
            //ltrHeader.Text = "Collaboration";
            div6.Visible = div06.Visible = true;

            //slide1.Visible = true;

            //ltrNo1.Text = "1 / 1";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_collaboration.png";

            //img1.Alt = "Collaboration";

            //col1.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_collaboration.png";

            //gal1.Alt = "1. collaboration dashboard - communicate with your channel partners and distribute material";
        }

        private void InitLeadDistribution()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Distribute leads to your channel partners through our lead distribution feature and increase your channel sales. Lead distribution on Elioplus PRM " +
                                "and Elioplus Channel Management software.";
            metaKeywords.Attributes["content"] = "Lead distribution, channel sales, lead management, leads management, partners leads, channel marketing";
            //ltrHeader.Text = "Lead Distribution";
            div5.Visible = div05.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;

            //ltrNo1.Text = "1 / 3";
            //ltrNo2.Text = "2 / 3";
            //ltrNo3.Text = "3 / 3";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Leads_Form.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Leads.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Closed_Leads.png";

            //img1.Alt = "Lead distribution form";
            //img2.Alt = "New leads";
            //img3.Alt = "Closed leads";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Leads_Form.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Leads.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Lead_Distibution_Closed_Leads.png";

            //gal1.Alt = "1. Lead distribution form";
            //gal2.Alt = "2. New leads";
            //gal3.Alt = "3. Closed leads";
        }

        private void InitDealRegistration()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Receive deals from your channel partners with all the information needed of their potential clients. Take a look at the deals result and reward " +
                                "the most well performed resellers.";
            metaKeywords.Attributes["content"] = "Deal registration, channel sales, deals management, sales opportunities, partners deals, channel marketing";
            //ltrHeader.Text = "Deal Registration";
            div4.Visible = div04.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = true;
            //slide4.Visible = true;

            //ltrNo1.Text = "1 / 4";
            //ltrNo2.Text = "2 / 4";
            //ltrNo3.Text = "3 / 4";
            //ltrNo4.Text = "4 / 4";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Deal_duration.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_New_deals.png";
            //img3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Deal_form.png";
            //img4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Manage_Comments.png";

            //img1.Alt = "Deal duration";
            //img2.Alt = "New deals";
            //img3.Alt = "Deal form";
            //img4.Alt = "Manage Comments";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = true;
            //col4.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Deal_duration.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_New_deals.png";
            //gal3.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Deal_form.png";
            //gal4.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Deal_registration_Manage_Comments.png";

            //gal1.Alt = "1. Vendor defines the month duration that every deal will be active";
            //gal2.Alt = "2. Channel partners submit the deal registration form and distribute the deal to the vendor of their choice";
            //gal3.Alt = "3. Deal registration form";
            //gal4.Alt = "4. Exchange comments (texts and attached files) in order to win the deal";
        }

        private void InitPartnerOnboarding()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Partner Onboarding to help your channel partners increase their sales. Upload any type of file and categorize them to product updates, marketing material, " +
                                "documentation and more";
            metaKeywords.Attributes["content"] = "Partner onboarding, onboard partners, PRM partner onboarding, partner onboarding solution, partner training";
            //ltrHeader.Text = "Partner Onboarding";
            div3.Visible = div03.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;

            //ltrNo1.Text = "1 / 2";
            //ltrNo2.Text = "2 / 2";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner_Onboarding_categories.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner_Onboarding_upload_files.png";

            //img1.Alt = "Partner Onboarding vendors";
            //img2.Alt = "Partner Onboarding vendors";

            //col1.Visible = true;
            //col2.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner_Onboarding_categories.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Partner_Onboarding_upload_files.png";

            //gal1.Alt = "Partner Onboarding Categories";
            //gal2.Alt = "Partner Onboarding Upload Files";
        }

        private void InitPartnerDirectory()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Invite your channel partners into your PRM account and start collaborating each other, distribute leads, receive deals, train and incentivize them.";
            metaKeywords.Attributes["content"] = "Partner directory, PRM partner directory, PRM invitations, PRM solution, PRM software";
            //ltrHeader.Text = "Partner Directory";
            div2.Visible = div02.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;

            //ltrNo1.Text = "1 / 2";
            //ltrNo2.Text = "2 / 2";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Send_Invitations.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Manage_Partners.png";

            //img1.Alt = "invitations partners";
            //img2.Alt = "channel partners list";

            //col1.Visible = true;
            //col2.Visible = true;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Send_Invitations.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_Manage_Partners.png";

            //gal1.Alt = "1. Send invitations to your channel partners to join your PRM account";
            //gal2.Alt = "2. Manage partners";
        }

        private void InitPartnerPortal()
        {
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            metaDescription.Attributes["content"] = "Distribute your unique partner portal login and sign up page to let your channel partners use your PRM account. Add your unique partner portal URL " +
                                "on your website to increase your channel network.";
            metaKeywords.Attributes["content"] = "Partner portal, partner portals, partner login, partner login page, white label partner portal, white label PRM, white label Partner Management";
            //ltrHeader.Text = "Partner Portal";
            div1.Visible = div01.Visible = true;

            //slide1.Visible = true;
            //slide2.Visible = true;
            //slide3.Visible = false;
            //slide4.Visible = false;
            //slide5.Visible = false;
            //slide6.Visible = false;

            //ltrNo1.Text = "1 / 2";
            //ltrNo2.Text = "2 / 2";
            //ltrNo3.Text = "";
            //ltrNo4.Text = "";
            //ltrNo5.Text = "";
            //ltrNo6.Text = "";

            //img1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_partner_portal_sign_up_page-1.png";
            //img2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_partner_portal_login_page-2.png";

            //img1.Alt = "Elioplus PRM_partner portal sign up page - 1";
            //img2.Alt = "Elioplus PRM_partner portal login page - 2";

            //col1.Visible = true;
            //col2.Visible = true;
            //col3.Visible = false;
            //col4.Visible = false;
            //col5.Visible = false;
            //col6.Visible = false;

            //gal1.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_partner_portal_sign_up_page-1.png";
            //gal2.Src = "/assets/images/features/partnerManagementSoftware/Elioplus_PRM_partner_portal_login_page-2.png";

            //gal1.Alt = "1. Elioplus PRM_partner portal sign up page - 1";
            //gal2.Alt = "2. Elioplus PRM_partner portal login page - 2";
        }

        private void SetLinks()
        {
            aGetStartedFree.HRef = aSIgnUp1.HRef = aSIgnUp1.HRef = aFreeSignUp.HRef = aSignUp.HRef = ControlLoader.SignUpPrm;
            //aBackPrm.HRef = aPrmLogo.HRef = ControlLoader.PrmSoftware;
            //aRequestDemo.HRef = "https://calendly.com/elioplus";
            //aPartnerPortal.HRef = ControlLoader.PrmSoftwarePartnerPortal;
            //aPartnerDirectory.HRef = ControlLoader.PrmSoftwarePartnerDirectory;
            //aPartnerOnboarding.HRef = ControlLoader.PrmSoftwarePartnerOnboarding;
            //aDealRegistration.HRef = ControlLoader.PrmSoftwareDealRegistration;
            //aLeadDistribution.HRef = ControlLoader.PrmSoftwareLeadDistribution;
            //aCollaboration.HRef = ControlLoader.PrmSoftwareCollaboration;
            //aChannelAnalytics.HRef = ControlLoader.PrmSoftwareChannelAnalytics;
            //aPartnerLocator.HRef = ControlLoader.PrmSoftwarePartnerLocator;
            //aPartner2Partner.HRef = ControlLoader.PrmSoftwarePartner2Partner;
            //aPartnerActivation.HRef = ControlLoader.PrmSoftwarePartnerActivation;
            //aPartnerTierManagement.HRef = ControlLoader.PrmSoftwarePartnerTierManagement;
            //aPartnerTeamRoles.HRef = ControlLoader.PrmSoftwarePartnerTeamRoles;
            //aPartnerManagement.HRef = ControlLoader.PrmSoftwarePartnerManagement;
            //aContentManagement.HRef = ControlLoader.PrmSoftwareContentManagement;

            //ltrPartnerPortal.Text = "Partner Portal";
            //ltrPartnerDirectory.Text = "Partner Directory";
            //ltrPartnerOnboarding.Text = "Partner Onboarding";
            //ltrDealRegistration.Text = "Deal Registration";
            //ltrLeadDistribution.Text = "Lead Distribution";
            //ltrCollaboration.Text = "Collaboration";
            //ltrChannelAnalytics.Text = "Channel Analytics";
            //ltrPartnerLocator.Text = "Partner Locator";
            //ltrPartner2Partner.Text = "Partner 2 Partner";
            //ltrPartnerActivation.Text = "Partner Activation";
            //ltrPartnerTierManagement.Text = "Tier Management";
            //ltrPartnerTeamRoles.Text = "Team Roles";
            //ltrPartnerManagement.Text = "Partner Management";
            //ltrContentManagement.Text = "Content Management";
        }

        #endregion
    }
}