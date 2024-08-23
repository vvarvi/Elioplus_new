using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class MasterDashboard : System.Web.UI.MasterPage
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

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

        public int CustomVendorID
        {
            get
            {
                return (ConfigurationManager.AppSettings["Dynamics365_VendorID"].ToString() != null) ? Convert.ToInt32(ConfigurationManager.AppSettings["Dynamics365_VendorID"].ToString()) : 0;
            }
            set
            {
                ViewState["CustomVendorID"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (!IsPostBack)
                    {
                        if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                        {
                            Response.Redirect(vSession.Page = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage, false);
                            return;
                        }
                        else
                        {
                            UpdateStrings();
                            SetLinks();
                            PageTitle();
                            SetMenuSelectedItemActive();

                            FixPage();
                        }
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
                //GC.Collect();
            }
        }

        #region Methods

        public void ShowNotifications()
        {
            //if (Sql.IsUserAdministrator(vSession.User.Id, session))
            //{
            //    bool hasPendingDemoRequests = Sql.HasDemoRequestByStatus(0, session);
            //    if (hasPendingDemoRequests)
            //    {
            //        DemoRequestsNotif.Visible = true;
            //    }
            //    else
            //        DemoRequestsNotif.Visible = false;

            //}

            //int pendingRequestsCount = 0;
            //bool hasCollaborationPendingRequest = SqlCollaboration.HasInvitationRequestByStatus(vSession.User.Id, vSession.User.AccountStatus, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), out pendingRequestsCount, session);

            //CollaborationNotif.Visible = hasCollaborationPendingRequest;
            
            //int simpleMsgCount = 0;
            //int groupMsgCount = 0;
            //int isNew = 1;
            //int isViewed = 0;
            //int isDeleted = 0;
            //int isPublic = 1;

            //int totalNewMessagesCount = SqlCollaboration.GetUserTotalSimpleNewUnreadMailBoxMessagesNotification(vSession.User.Id, vSession.User.CompanyType, isNew, isViewed, isDeleted, isPublic, out simpleMsgCount, out groupMsgCount, session);
            //if (totalNewMessagesCount > 0)
            //{
            //    CollaborationNotif.Visible = true;
            //}

            //int newReceivedFiles = SqlCollaboration.GetUserNewFilesReceived(vSession.User.Id, session);
            //if (newReceivedFiles > 0)
            //{
            //    CollaborationNotif.Visible = true;
            //}

            //int count = 0;
            //bool hasNewDeals = false;
            //bool hasNewLeadDistribution = false;
            //bool hasInvitationRequest = false;
            ////bool hasNewP2PDeals = false;
            //int requestsCount = 0;

            //if (vSession.User.CompanyType == Types.Vendors.ToString())
            //{
            //    hasInvitationRequest = SqlCollaboration.HasInvitationRequestUserByStatus(vSession.User.Id, vSession.User.CompanyType, CollaborateInvitationStatus.Confirmed.ToString(), isNew, out requestsCount, session);
            //    if (hasInvitationRequest)
            //    {
            //        DealPartnerDirectoryNotif.Visible = true;
            //    }
            //    else
            //    {
            //        DealPartnerDirectoryNotif.Visible = false;
            //    }

            //    hasNewDeals = Sql.HasNewDealRegistration(vSession.User, (int)DealActivityStatus.NotConfirmed, (int)DealStatus.Open, DealResultStatus.Pending.ToString(), isNew, out count, session);
            //    if (hasNewDeals)
            //        DealRegistrationNotif.Visible = true;
            //    else
            //        DealRegistrationNotif.Visible = false;

            //    count = 0;
            //    isViewed = 0;
            //    hasNewLeadDistribution = Sql.HasNewLeadDistributionByStatus(vSession.User, (int)DealStatus.Closed, isViewed, out count, session);
            //    if (hasNewLeadDistribution)
            //        LeadDistributionNotif.Visible = true;
            //    else
            //        LeadDistributionNotif.Visible = false;
            //}
            //else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            //{
            //    hasInvitationRequest = SqlCollaboration.HasInvitationRequestUserByStatus(vSession.User.Id, vSession.User.CompanyType, CollaborateInvitationStatus.Pending.ToString(), isNew, out requestsCount, session);
            //    if (hasInvitationRequest)
            //    {
            //        DealPartnerDirectoryNotif.Visible = true;
            //    }
            //    else
            //    {
            //        DealPartnerDirectoryNotif.Visible = false;
            //    }

            //    hasNewDeals = Sql.HasNewDealRegistration(vSession.User, (int)DealActivityStatus.Approved, (int)DealStatus.Open, DealResultStatus.Pending.ToString(), isNew, out count, session);
            //    if (hasNewDeals)
            //        DealRegistrationNotif.Visible = true;
            //    else
            //        DealRegistrationNotif.Visible = false;

            //    //hasNewP2PDeals = Sql.HasNewP2PDealRegistration(vSession.User.Id, (int)DealActivityStatus.Approved, (int)DealStatus.Closed, out count, session);
            //    //if (hasNewP2PDeals)
            //    //    PartnerToPartnerNotif.Visible = true;
            //    //else
            //    //    PartnerToPartnerNotif.Visible = false;

            //    count = 0;
            //    isNew = 1;
            //    hasNewLeadDistribution = Sql.HasNewLeadDistribution(vSession.User, (int)DealStatus.Open, DealResultStatus.Pending.ToString(), isNew, out count, session);
            //    if (hasNewLeadDistribution)
            //        LeadDistributionNotif.Visible = true;
            //    else
            //        LeadDistributionNotif.Visible = false;
            //}
        }

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.User, out metaDescription, out metaKeywords, session);
        }

        private void SetLinks()
        {
            aElioplusLogo.HRef = ControlLoader.Default();
            aHome.HRef = aMenuDashboardChaPa.HRef = aHomeMob.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aMenuPartnerOnboarding.HRef = aMenuPartnerOnboardingChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-onboarding");

            if (Sql.IsUserAdministrator(vSession.User.Id, session) || (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType && (vSession.User.BillingType == (int)BillingTypePacket.StartupPRMDatabaseType || vSession.User.BillingType == (int)BillingTypePacket.GrowthPRMDatabaseType)))
            {
                aUserCommunityProfile.HRef = ControlLoader.CommunityUserProfile(vSession.User);
                aMenuFindPartners.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners-admin");
            }
            else
                aMenuFindPartners.HRef = (vSession.User.CompanyType == Types.Vendors.ToString()) ? ControlLoader.Dashboard(vSession.User, "find-new-partners") : ControlLoader.Dashboard(vSession.User, "find-new-recommended-channel-partners");

            aSubMenuOpportunities.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
            aSubMenuOpportunitiesTasks.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-tasks");

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                if (vSession.User.Id == GlobalMethods.GetRandstadCustomerID())
                {
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals");
                }
                else if (vSession.User.Id == CustomVendorID)
                {
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals-365");
                }
                else
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration");
            }
            else
            {
                if (SqlCollaboration.IsPartnerOfCustomVendor(GlobalMethods.GetRandstadCustomerID(), vSession.User.Id, session))
                {
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals");
                }
                else if (SqlCollaboration.IsPartnerOfCustomVendor(CustomVendorID, vSession.User.Id, session))
                {
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals-365");
                }
                else
                    aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration");
            }

            aMenuPartnerToPartner.HRef = aMenuPartnerToPartnerChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-to-partner");
            aIntentSignals.HRef = ControlLoader.Dashboard(vSession.User, "intent-signals");
            aMenuLeadDistribution.HRef = aMenuLeadDistributionChaPa.HRef = ControlLoader.Dashboard(vSession.User, "lead-distribution");
            aMenuTierManagement.HRef = ControlLoader.Dashboard(vSession.User, "tier-management");
            aMenuPermissionsRolesManagement.HRef = ControlLoader.Dashboard(vSession.User, "permissions-roles-management");
            aSubMenuDealPartnerManage.HRef = aSubMenuDealPartnerManageChaPa.HRef = ControlLoader.Dashboard(vSession.User, "manage-partners");
            aSubMenuDealPartnerRequests.HRef = aSubMenuDealPartnerRequestsChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partners-requests");
            aSubMenuDealPartnerSendInvitations.HRef = aSubMenuDealPartnerSendInvitationsChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partners-invitations");
            aSubMenuDealAnalyticsGeneral.HRef = ControlLoader.Dashboard(vSession.User, "general-analytics");
            aSubMenuDealAnalyticsSalesLeaderBoard.HRef = ControlLoader.Dashboard(vSession.User, "sales-leaderboard-analytics");
            aSubMenuDealAnalyticsActivePartners.HRef = ControlLoader.Dashboard(vSession.User, "active-partners-analytics");
            aMenuTeam.HRef = ControlLoader.Dashboard(vSession.User, "team");
            aMenuTeamAndIntegrations.HRef = ControlLoader.Dashboard(vSession.User, "integrations");
            aMenuCollaboration.HRef = aMenuCollaborationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-chat-room");
            aSubMenuCollaborationMyFiles.HRef = aSubMenuCollaborationMyFilesChaPa.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-library/" + Guid.NewGuid().ToString());
            aSubMenuCollaborationChoosePartners.HRef = aSubMenuCollaborationChoosePartnersChaPa.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-choose-partners");
            aMenuMessages.HRef = aMenuMessagesChaPa.HRef = ControlLoader.Dashboard(vSession.User, "messages/inbox");
            aUserEditProfile.HRef = ControlLoader.Dashboard(vSession.User, "edit-company-profile");
            aUserBilling.HRef = ControlLoader.Dashboard(vSession.User, "billing");
            aAdminElioFinancialIncomeFlow.HRef = ControlLoader.AdminElioFinancialIncomeFlowPage;
            aAdminElioFinancialExpensesFlow.HRef = ControlLoader.AdminElioFinancialExpensesFlowPage;
            aAddThirdPartyUsers.HRef = ControlLoader.AdminAddThirdPartyUsersPage;
            aAddIntentSignalsData.HRef = ControlLoader.AdminAddIntentSignalsDataPage;
            aUserGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            aUserAdminPage.HRef = ControlLoader.AdminPage;
            aManagement.HRef = ControlLoader.AdminUserManagement;
            aUserAdminStatisticsPage.HRef = ControlLoader.AdminStatisticsPage;
            aUserDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aHelpCenter.HRef = ControlLoader.Dashboard(vSession.User, "help-center");
            aSubAccountNotification.HRef = ControlLoader.Dashboard(vSession.User, "team");

            aMenuPartnerCommissions.HRef = aMenuPartnerCommissionsChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-payments");
            //aMenuPartnerTraining.HRef = 
            //aMenuPartnerTrainingChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-add-new-1");
            aSubMenuPartnerTrainingAdd.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-add-new");
            aSubMenuPartnerTrainingOverview.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-overview");
            aSubMenuPartnerTrainingAnalytics.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-analytics");
            aSubMenuPartnerTrainingCoursesOverview.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-courses-overview");

            if ((Sql.IsUserAdministrator(vSession.User.Id, session) || SqlCollaboration.IsAdminConfirmedCollaborationPartner(vSession.User.Id, session)) || (vSession.User.Id == 2308 || SqlCollaboration.IsPartnerOfCustomVendor(2308, vSession.User.Id, session)))
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    liMenuPartnerCommissions.Visible = true;
                    liMenuPartnerCommissionsChaPa.Visible = false;
                }
                else
                {
                    liMenuPartnerCommissionsChaPa.Visible = true;
                    liMenuPartnerCommissions.Visible = false;
                }
            }
            else
            {
                liMenuPartnerCommissions.Visible = liMenuPartnerCommissionsChaPa.Visible = false;
            }

            if ((Sql.IsUserAdministrator(vSession.User.Id, session) || SqlCollaboration.IsAdminConfirmedCollaborationPartner(vSession.User.Id, session)))
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    liMenuPartnerTraining.Visible = true;
                    liSubMenuPartnerTrainingOverview.Visible = true;
                    //liSubMenuPartnerTrainingCoursesOverview.Visible = true;
                    liSubMenuPartnerTrainingAnalytics.Visible = true;
                    liMenuPartnerTrainingChaPa.Visible = false;
                }
                else
                {
                    liMenuPartnerTrainingChaPa.Visible = true;
                    liSubMenuPartnerTrainingOverviewChaPa.Visible = true;
                    //liSubMenuPartnerTrainingCoursesOverviewChaPa.Visible = true;
                    liMenuPartnerTraining.Visible = false;

                    //aSubMenuPartnerTrainingCourseViewChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-course");
                    aSubMenuPartnerTrainingOverviewChaPa.HRef = ControlLoader.Dashboard(vSession.User, "partner-training-library");
                }
            }
            else
            {
                liMenuPartnerTraining.Visible = liMenuPartnerTrainingChaPa.Visible = false;
            }
        }

        private void UpdateStrings()
        {
            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                LblMenuFindPartners.Text = "My Matches";
            }
            else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                LblMenuFindPartners.Text = "Recommended Partners";
            }
            else
            {
                LblMenuFindPartners.Text = "My Matches";
            }

            LblMenuDashboardChaPa.Text = "Home";
            LblMenuPartnerOnboarding.Text = LblMenuPartnerOnboardingChaPa.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Partner Onboarding" : "Vendor Resources";
            LblMenuOpportunities.Text = LblSubMenuOpportunities.Text = "Opportunities";
            LblSubMenuOpportunitiesTasks.Text = "Tasks";
            LblMenuDealRegistration.Text = "Deal Registration";
            LblMenuPartnerToPartner.Text = "Partner 2 Partner";
            //LblMenuIntentSignals.Text = "Intent Signals";
            LblMenuLeadDistribution.Text = "Lead Distribution";
            LblMenuTierManagement.Text = "Tier Management";
            LblMenuPermissionsRolesManagement.Text = "Roles Management";
            LblMenuDealPartnerDirectory.Text = "Partner Directory";
            LblSubMenuDealPartnerManage.Text = "Manage Partners";
            LblSubMenuDealPartnerRequests.Text = "Partner Requests";
            LblSubMenuDealPartnerSendInvitations.Text = "Send Invitations";
            LblMenuDealAnalytics.Text = "Analytics";
            LblSubMenuDealAnalyticsGeneral.Text = "General Analytics";
            LblSubMenuDealAnalyticsSalesLeaderBoard.Text = "Sales Leaderboard";
            LblSubMenuDealAnalyticsActivePartners.Text = "Active Partners";
            LblMenuTeam.Text = "Add Team Members";
            LblMenuCollaboration.Text = "Collaborate";
            LblMenuMessages.Text = "Messages";            
            LblUserProfile.Text = "My profile";
            LblUserDashboard.Text = "Dashboard";
            LblUserEditProfile.Text = "Edit profile";
            LblUserBilling.Text = "Billing";
            LblAdminElioFinancialIncomeFlow.Text = "Financial income flow";
            LblAdminElioFinancialExpensesFlow.Text = "Financial expenses flow";
            LblUserCommunityProfile.Text = "Community profile";
            LblUserLogout.Text = "Logout";
            LblUserAdminPage.Text = "Admin page";
            LblManagement.Text = "Management";
            LblUserAdminStatisticsPage.Text = "Statistics Page";
            LblAddThirdPartyUsers.Text = "Add third party users";
            LblAddIntentSignalsData.Text = "Add intent signals data";
            LblMenuIntegrations.Text = "Integrations";
        }

        private void SetMenuSelectedItemActive()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                liIntentSignals.Visible = false;

                if (path.Contains("/home"))
                {
                    aHome.Attributes["class"] += " active";
                    aPartnerManagement.Attributes["class"] = aPartnerRecruitment.Attributes["class"] = "nav-link py-4 px-6";
                    kt_header_tab_0.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";

                    kt_header_tab_2.Attributes["class"] = kt_header_tab_1.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";
                }
                else if (path.Contains("/tier-management") || path.Contains("/permissions-roles-management") || path.Contains("/integrations") || path.Contains("/team") || path.Contains("/partner-locator"))
                {
                    aHome.Attributes["class"] = aPartnerManagement.Attributes["class"] = aPartnerRecruitment.Attributes["class"] = "nav-link py-4 px-6";
                    kt_header_tab_0.Attributes["class"] = kt_header_tab_1.Attributes["class"] = kt_header_tab_2.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";
                }
                else if (path.Contains("/find-new-partners") || path.Contains("/messages") || path.Contains("/opportunities") || path.Contains("/tasks"))
                {
                    aPartnerRecruitment.Attributes["class"] += " active";
                    aPartnerManagement.Attributes["class"] = aHome.Attributes["class"] = "nav-link py-4 px-6";
                    kt_header_tab_1.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";

                    kt_header_tab_2.Attributes["class"] = kt_header_tab_0.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";
                }
                else
                {
                    aPartnerManagement.Attributes["class"] += " active";
                    aPartnerRecruitment.Attributes["class"] = aHome.Attributes["class"] = "nav-link py-4 px-6";

                    kt_header_tab_2.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";
                    kt_header_tab_1.Attributes["class"] = kt_header_tab_0.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";
                }
            }
            else
            {
                FixMainTopMenu();
            }

            if (path.Contains("/home"))
            {
                aHome.Attributes["class"] = "nav-link py-4 px-6 active";
                aIntentSignals.Attributes["class"] = "nav-link py-4 px-6";
            }
            else if (path.Contains("/intent-signals"))
            {
                aHome.Attributes["class"] = "nav-link py-4 px-6";
                aIntentSignals.Attributes["class"] = "nav-link py-4 px-6 active";
                kt_header_tab_0.Visible = false;
            }
            else if (path.Contains("/find-new-partners"))
            {
                liMenuPartners.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open-dropdown";
            }
            else if (path.Contains("/opportunities"))
            {
                liMenuOpportunities.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                if (path.Contains("/opportunities") || path.Contains("/opportunities-add-edit") || path.Contains("/opportunities-view-notes"))
                {
                    liSubMenuOpportunitiesTasks.Attributes["class"] = "menu-item menu-item-submenu";
                    liSubMenuOpportunities.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                }
                if (path.Contains("/opportunities-tasks") || path.Contains("/opportunities-view-tasks") || path.Contains("/tasks"))
                {
                    liSubMenuOpportunities.Attributes["class"] = "menu-item menu-item-submenu";
                    liSubMenuOpportunitiesTasks.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                }

                //spanOpportunitiesArrow.Attributes["class"] = "arrow open";
            }
            else if (path.Contains("/messages"))
            {
                liMenuMessages.Attributes["class"] = liMenuMessagesChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/deal-registration"))
            {
                liMenuDealRegistration.Attributes["class"] = liMenuDealRegistrationChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/partner-to-partner"))
            {
                liMenuPartnerToPartner.Attributes["class"] = liMenuPartnerToPartnerChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/lead-distribution"))
            {
                liMenuLeadDistribution.Attributes["class"] = liMenuLeadDistributionChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            //else if (path.Contains("/tier-management"))
            //{
            //    liMenuTierManagement.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            //}
            else if (path.Contains("/permissions-roles-management"))
            {
                //liMenuPermissionsRolesManagement.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/partner-onboarding"))
            {
                liMenuPartnerOnboarding.Attributes["class"] = liMenuPartnerOnboardingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open-dropdown";
            }
            else if (path.Contains("/new-connections"))
            {
                //liMenuConnections.Attributes["class"] += " active";
            }
            else if (path.Contains("/new-clients"))
            {
                //liMenuNewClients.Attributes["class"] += " active";
            }
            else if (path.Contains("/manage-partners"))
            {
                liMenuDealPartnerDirectory.Attributes["class"] = liMenuDealPartnerDirectoryChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealPartnerManage.Attributes["class"] = liSubMenuDealPartnerManageChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuDealPartnerRequests.Attributes["class"] = liSubMenuDealPartnerRequestsChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealPartnerSendInvitations.Attributes["class"] = liSubMenuDealPartnerSendInvitationsChaPa.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/partners-requests"))
            {
                liMenuDealPartnerDirectory.Attributes["class"] = liMenuDealPartnerDirectoryChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealPartnerManage.Attributes["class"] = liSubMenuDealPartnerManageChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealPartnerRequests.Attributes["class"] = liSubMenuDealPartnerRequestsChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuDealPartnerSendInvitations.Attributes["class"] = liSubMenuDealPartnerSendInvitationsChaPa.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/partners-invitations"))
            {
                liMenuDealPartnerDirectory.Attributes["class"] = liMenuDealPartnerDirectoryChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealPartnerManage.Attributes["class"] = liSubMenuDealPartnerManageChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealPartnerRequests.Attributes["class"] = liSubMenuDealPartnerRequestsChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealPartnerSendInvitations.Attributes["class"] = liSubMenuDealPartnerSendInvitationsChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
            }
            else if (path.Contains("/general-analytics"))
            {
                liMenuDealAnalytics.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealAnalyticsGeneral.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuDealAnalyticsSalesLeaderBoard.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealAnalyticsActivePartners.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/sales-leaderboard-analytics"))
            {
                liMenuDealAnalytics.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealAnalyticsGeneral.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealAnalyticsSalesLeaderBoard.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuDealAnalyticsActivePartners.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/active-partners-analytics"))
            {
                liMenuDealAnalytics.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuDealAnalyticsGeneral.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealAnalyticsSalesLeaderBoard.Attributes["class"] = "menu-item menu-item-submenu";
                liSubMenuDealAnalyticsActivePartners.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
            }
            else if (path.Contains("/team"))
            {
                //kt_header_tab_1.Attributes["calss"] = "nav-link py-4 px-6";
                //aPartnerManagement.Attributes["calss"] += " active";
                //liMenuTeam.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/integrations"))
            {
                //kt_header_tab_1.Attributes["calss"] = "nav-link py-4 px-6";
                //aPartnerManagement.Attributes["calss"] += " active";
                //liMenuTeamAndIntegrations.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
            }
            else if (path.Contains("/collaboration"))
            {
                if (path.Contains("/collaboration-chat-room"))
                    liMenuCollaboration.Attributes["class"] = liMenuCollaborationChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
                else
                {
                    if (path.Contains("/collaboration-library"))
                    {
                        liMenuCollaborationLibrary.Attributes["class"] = liMenuCollaborationLibraryChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                        liSubMenuCollaborationMyFiles.Attributes["class"] = liSubMenuCollaborationMyFilesChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                        liSubMenuCollaborationChoosePartners.Attributes["class"] = liSubMenuCollaborationChoosePartnersChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                    }
                    else if (path.Contains("/collaboration-choose-partners"))
                    {
                        liMenuCollaborationLibrary.Attributes["class"] = liMenuCollaborationLibraryChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                        liSubMenuCollaborationChoosePartners.Attributes["class"] = liSubMenuCollaborationChoosePartnersChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                        liSubMenuCollaborationMyFiles.Attributes["class"] = liSubMenuCollaborationMyFilesChaPa.Attributes["class"] = "menu-item menu-item-submenu";
                    }
                }
            }
            else if (path.Contains("/leads"))
            {
                //liMenuLeads.Attributes["class"] += " active";
            }
            else if (path.Contains("/favourites"))
            {
                //liMenuFavorites.Attributes["class"] += " active";
            }
            else if (path.Contains("/billing"))
            {
                //liMenuBilling.Attributes["class"] += " active";
            }
            else if (path.Contains("/algorithm"))
            {
                //liMenuAlgor.Attributes["class"] += " active";
            }
            else if (path.Contains("/statistics"))
            {
                //liMenuStats.Attributes["class"] += " active";
            }
            else if (path.Contains("/admin-demo-requests-management"))
            {
                //liMenuDemoRequests.Attributes["class"] += " active";
            }
            else if (path.Contains("/logs"))
            {
                //liMenuLogs.Attributes["class"] += " active";
            }
            else if (path.Contains("/clearbit-logs"))
            {
                //liMenuClearbitLogs.Attributes["class"] += " active";
            }
            else if (path.Contains("/info-logs"))
            {
                //liMenuInfoLogs.Attributes["class"] += " active";
            }
            else if (path.Contains("/partner-commissions-payments") || path.Contains("/partner-commissions-billing"))
            {
                liMenuPartnerCommissions.Attributes["class"] = liMenuPartnerCommissionsChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";
			}
            else if (path.Contains("/partner-training-add-new"))
            {
                //liMenuPartnerTraining.Attributes["class"] = liMenuPartnerTrainingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liMenuPartnerTraining.Attributes["class"] = liMenuPartnerTrainingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuPartnerTrainingAdd.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuPartnerTrainingOverview.Attributes["class"] = liSubMenuPartnerTrainingCoursesOverview.Attributes["class"] = liSubMenuPartnerTrainingAnalytics.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/partner-training-overview"))
            {
                liMenuPartnerTraining.Attributes["class"] = liMenuPartnerTrainingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuPartnerTrainingOverview.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuPartnerTrainingAdd.Attributes["class"] = liSubMenuPartnerTrainingCoursesOverview.Attributes["class"] = liSubMenuPartnerTrainingAnalytics.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/partner-training-analytics"))
            {
                liMenuPartnerTraining.Attributes["class"] = liMenuPartnerTrainingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuPartnerTrainingAnalytics.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuPartnerTrainingAdd.Attributes["class"] = liSubMenuPartnerTrainingCoursesOverview.Attributes["class"] = liSubMenuPartnerTrainingOverview.Attributes["class"] = "menu-item menu-item-submenu";
            }
            else if (path.Contains("/partner-training-courses-overview"))
            {
                liMenuPartnerTraining.Attributes["class"] = liMenuPartnerTrainingChaPa.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu menu-item-rel menu-item-open menu-item-here";

                liSubMenuPartnerTrainingCoursesOverview.Attributes["class"] = "menu-item menu-item-open menu-item-here menu-item-submenu";
                liSubMenuPartnerTrainingAdd.Attributes["class"] = liSubMenuPartnerTrainingAnalytics.Attributes["class"] = liSubMenuPartnerTrainingOverview.Attributes["class"] = "menu-item menu-item-submenu";
            }
        }

        private void FixPage()
        {
            ShowNotifications();
            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                #region Account Completed

                aLocator.Visible = aPartnerPortalLogin.Visible = true;
                divLocator.Visible = divPartnerPortalLogin.Visible = true;

                int newMessages = Sql.GetCompanyMessages(vSession.User.Id, 1, 0, session);

                int isNew = 1;
                int isDeleted = 0;
                int companyLeads = Sql.GetCompanyRecentLeadsForCurrentMonthByIsNewIsDeletedStatus(vSession.User, isNew, isDeleted, session);

                //bool hasActiveSubscription = Sql.HasActivePacketSubscription(vSession.User.Id, session);        //Sql.GetUserLastOrderByStatus(vSession.User.Id, Convert.ToInt32(OrderStatus.Canceled), session);
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

                if (hasActiveSubscription)
                {
                    //to do
                }

                ImgUserPhoto.ImageUrl = (!string.IsNullOrEmpty(vSession.User.CompanyLogo)) ? vSession.User.CompanyLogo : (!string.IsNullOrEmpty(vSession.User.PersonalImage)) ? vSession.User.PersonalImage : "/assets/layouts/layout/img/avatar.png";
                ImgUserPhoto.AlternateText = vSession.User.CompanyName + "logo";
                LblUserCompanyName.Text = (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName : vSession.User.CompanyName;

                aUserProfile.Visible = (vSession.User.IsPublic == 1) ? true : false;
                aUserGoFull.Visible = false;
                aUserProfile.HRef = ControlLoader.Profile(vSession.User);

                //to do
                aMenuCollaboration.Visible = aMenuCollaborationChaPa.Visible = aSubMenuCollaborationMyFiles.Visible = aSubMenuCollaborationMyFilesChaPa.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed;       //((vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && SqlCollaboration.ExistUserInCollaboration(vSession.User.Id, session))); //(ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true") ? ((vSession.User.BillingType == Convert.ToInt32(BillingType.Premium) || Sql.IsUserAdministrator(vSession.User.Id, session)) && vSession.User.CompanyType != Types.Developers.ToString()) ? true : false : false;     //Sql.IsUserAdministrator(vSession.User.Id, session) && vSession.User.CompanyType != Types.Developers.ToString(); 

                LoadEmailSettings();

                #endregion
            }
            else
            {
                #region Account Not Completed

                ImgUserPhoto.ImageUrl = "/assets/layouts/layout/img/avatar.png";
                LblUserCompanyName.Text = vSession.User.Username;
                ImgUserPhoto.AlternateText = vSession.User.Username + "logo";

                aUserProfile.Visible = false;
                aUserGoFull.Visible = true;
                aUserProfile.HRef = "";
                //aMenuBilling.Visible = false;
                LblUserGoFull.Text = "Complete registration";

                aLocator.Visible = aPartnerPortalLogin.Visible = false;
                divLocator.Visible = divPartnerPortalLogin.Visible = false;

                #endregion
            }

            aMenuTierManagement.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
            aUserBilling.Visible = vSession.User.CompanyType == Types.Vendors.ToString() || (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic);
            aMenuPermissionsRolesManagement.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            aUserCommunityProfile.Visible = Sql.IsUserAdministrator(vSession.User.Id, session);

            aMenuTeam.Visible = aMenuTeamAndIntegrations.Visible =
                aSubMenuOpportunitiesTasks.Visible =
                aSubMenuOpportunities.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;

            aMenuOpportunities.Visible = vSession.User.CompanyType == Types.Vendors.ToString() && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);

            aUserAdminPage.Visible =
            aUserAdminStatisticsPage.Visible =
            aAddThirdPartyUsers.Visible =
            aAddIntentSignalsData.Visible =
            aAdminElioFinancialIncomeFlow.Visible =
            aAdminElioFinancialExpensesFlow.Visible = Sql.IsUserAdministrator(vSession.User.Id, session) ? true : false;

            liMenuPartnerOnboarding.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed;       //Sql.IsUserAdministrator(vSession.User.Id, session);

            liMenuDealRegistration.Visible = liMenuDealPartnerDirectory.Visible = liSubMenuDealPartnerManage.Visible = liSubMenuDealPartnerRequests.Visible = liSubMenuDealPartnerSendInvitations.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed;   //&& ((vSession.User.CompanyType == Types.Vendors.ToString() && SqlCollaboration.HasPartnerReseller(vSession.User.Id, session)) || (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && SqlCollaboration.IsConfirmedReseller(vSession.User.Id, session)));    //(Sql.IsUserAdministrator(vSession.User.Id, session) || (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && SqlCollaboration.IsAdministratorReseller(vSession.User.Id, session)));       //(ConfigurationManager.AppSettings["IsLocalMode"] != null && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true");

            liMenuLeadDistribution.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed;
            liMenuPartnerToPartner.Visible = liMenuPartnerToPartnerChaPa.Visible = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString());
            liIntentSignals.Visible = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic);
            liMenuDealAnalytics.Visible = vSession.User.CompanyType == Types.Vendors.ToString();    // && Sql.IsUserAdministrator(vSession.User.Id, session);
            liMenuPartners.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            if (vSession.User.AccountStatus == (int)AccountStatus.Completed && vSession.User.CompanyType == Types.Vendors.ToString())
                aSubAccountNotification.Visible = Sql.HasSubAccountsPending(vSession.User.Id, 1, session);

            if (vSession.User.AccountStatus == (int)AccountStatus.Completed && vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            {
                if (vSession.User.IsPublic == (int)AccountPublicStatus.IsNotPublic)
                {
                    liMenuMessages.Visible = liMenuMessagesChaPa.Visible = false;
                    divLocator.Visible = divPartnerPortalLogin.Visible = divTierManagement.Visible = divPermissionsRolesManagement.Visible = false;
                }
            }

            aMAMarketplace.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed && vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic;
            
            #region Roles Management Permissions for Menu Pages

            aMenuTeam.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTeamPage", Actions.View, session);
            liMenuLeadDistribution.Visible = liMenuLeadDistributionChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardLeadDistributionPage", Actions.View, session);
            liMenuDealRegistration.Visible = liMenuDealRegistrationChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardDealRegistrationPage", Actions.View, session);
            liSubMenuDealPartnerManage.Visible = liSubMenuDealPartnerManageChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPDManagePartnersPage", Actions.View, session);
            liMenuPartnerOnboarding.Visible = liMenuPartnerOnboardingChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPartnerOnboardingPage", Actions.View, session);
            liMenuCollaborationLibrary.Visible = liMenuCollaborationLibraryChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardCollaborationLibraryPage", Actions.View, session);
            liMenuCollaboration.Visible = liMenuCollaborationChaPa.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardCollaborationChatRoomPage", Actions.View, session);
            liSubMenuDealAnalyticsGeneral.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardAnalyticsGeneralPage", Actions.View, session);
            liSubMenuDealAnalyticsSalesLeaderBoard.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardAnalyticsSalesLeaderboardPage", Actions.View, session);
            aMenuTierManagement.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.View, session);

            #endregion

            if (!aMenuTeam.Visible)
                aSubAccountNotification.Visible = false;

            #region IS3 - CUSTOM AREA SETTINGS

            bool hide = false;
            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            {
                bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(41078, vSession.User.Id, session);
                if (isCustomPartner)
                    hide = true;

                if (hide)
                {
                    liPartnerReqruitment.Visible = liPartnerReqruitmentMob.Visible = false;
                    kt_header_tab_1.Visible = false;
                    liMenuDealPartnerDirectoryChaPa.Visible = false;
                    liMenuPartnerToPartnerChaPa.Visible = false;
                    liMenuLeadDistributionChaPa.Visible = false;
                    ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
                }
            }
            else
            {
                if (vSession.User.Id == 41078)
                {
                    ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
                }
            }

            #endregion

            #region RANDSTAD - CUSTOM AREA SETTINGS

            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {
                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                {
                    ElioUsers customVendor = SqlCollaboration.GetCustomVendorLogoByPartner(GlobalMethods.GetRandstadCustomerID(), vSession.User.Id, session);
                    if (customVendor != null)
                    {
                        ImgElioplusLogo.ImageUrl = customVendor.CompanyLogo;
                        aElioplusLogo.HRef = ControlLoader.Profile(customVendor);

                        liMenuLeadDistribution.Visible = liMenuLeadDistributionChaPa.Visible =
                            liMenuMessages.Visible = liMenuMessagesChaPa.Visible =
                            liMenuPartnerToPartner.Visible = liMenuPartnerToPartnerChaPa.Visible = false;

                        aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals");
                    }
                    else
                        ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
                }
                else
                {
                    if (vSession.User.Id == GlobalMethods.GetRandstadCustomerID())
                    {
                        ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
                        aElioplusLogo.HRef = ControlLoader.Profile(vSession.User);

                        liMenuLeadDistribution.Visible = liMenuLeadDistributionChaPa.Visible = false;
                        aMenuDealRegistration.HRef = aMenuDealRegistrationChaPa.HRef = ControlLoader.Dashboard(vSession.User, "deals");
                    }
                }
            }

            #endregion

            #region CYBUS - CUSTOM AREA SETTINGS

            //if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            //{
            //    if (vSession.User.CompanyType == Types.Vendors.ToString())
            //    {
            //        if (vSession.User.Id == 55346)
            //        {
            //            ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
            //            aElioplusLogo.HRef = ControlLoader.Profile(vSession.User);

            //            liMenuDealRegistration.Visible = liMenuDealRegistrationChaPa.Visible = false;
            //            liMenuCollaboration.Visible = liMenuCollaborationChaPa.Visible = false;
            //            divTierManagement.Visible = false;
            //            divPermissionsRolesManagement.Attributes["class"] = "col-12";
            //        }
            //    }
            //    else
            //    {
            //        bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(55346, vSession.User.Id, session);
            //        if (isCustomPartner)
            //        {
            //            ElioUsers customVendor = SqlCollaboration.GetCustomVendorLogoByPartner(55346, vSession.User.Id, session);
            //            if (customVendor != null)
            //            {
            //                ImgElioplusLogo.ImageUrl = customVendor.CompanyLogo;
            //                aElioplusLogo.HRef = ControlLoader.Profile(customVendor);
            //            }
            //            else
            //            {
            //                ImgElioplusLogo.ImageUrl = vSession.User.CompanyLogo;
            //            }

            //            liMenuDealPartnerDirectory.Visible = liMenuDealPartnerDirectoryChaPa.Visible = false;
            //            liMenuMessages.Visible = liMenuMessagesChaPa.Visible = false;
            //            liMenuCollaboration.Visible = liMenuCollaborationChaPa.Visible = false;
            //            liMenuPartnerToPartner.Visible = liMenuPartnerToPartnerChaPa.Visible = false;
            //            LblMenuPartnerOnboarding.Text = LblMenuPartnerOnboardingChaPa.Text = "Vendor Resources";
            //            LblMenuCollaborationLibrary.Text = LblMenuCollaborationLibraryChaPa.Text = "Library";

            //            divTierManagement.Visible = false;
            //            divPermissionsRolesManagement.Attributes["class"] = "col-12";

            //            divIntegrations.Visible = false;
            //            divTeam.Attributes["class"] = "col-12";

            //            divLocator.Visible = divPartnerPortalLogin.Visible = false;
            //        }
            //    }
            //}

            #endregion

            #region CYBERAWARE SECURITY - CUSTOM AREA SETTINGS

            hide = false;
            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            {
                bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(105906, vSession.User.Id, session);
                if (isCustomPartner)
                    hide = true;

                if (hide)
                {
                    liMenuDealPartnerDirectory.Visible = liMenuDealPartnerDirectoryChaPa.Visible = false;
                    liMenuMessages.Visible = liMenuMessagesChaPa.Visible = false;
                    liMenuPartnerToPartner.Visible = liMenuPartnerToPartnerChaPa.Visible = false;
                }
            }
            
            #endregion
        }

        private void FixMainTopMenu()
        {
            kt_header_tab_0.Visible = true;
            liHome.Visible = true;
            liIntentSignals.Visible = true;

            kt_header_tab_1.Visible = kt_header_tab_2.Visible = false;
            liPartnerReqruitment.Visible = liPartnerManagement.Visible = false;

            aPartnerManagement.Attributes["class"] = aPartnerRecruitment.Attributes["class"] = "nav-link py-4 px-6";
            kt_header_tab_0.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";

            kt_header_tab_2.Attributes["class"] = kt_header_tab_1.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";
        }

        private void LoadEmailSettings()
        {
            HtmlInputCheckBox cbx1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox1");
            HtmlInputCheckBox cbx2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox2");
            HtmlInputCheckBox cbx3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox3");
            HtmlInputCheckBox cbx4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox4");
            HtmlInputCheckBox cbx5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox5");
            HtmlInputCheckBox cbx6 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox6");
            HtmlInputCheckBox cbx7 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox7");
            HtmlInputCheckBox cbx8 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox8");
            HtmlInputCheckBox cbx9 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox9");
            HtmlInputCheckBox cbx10 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox10");
            HtmlInputCheckBox cbx11 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox11");
            HtmlInputCheckBox cbx12 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox12");
            HtmlInputCheckBox cbx13 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox13");
            HtmlInputCheckBox cbx14 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox14");
            HtmlInputCheckBox cbx15 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox15");
            HtmlInputCheckBox cbx16 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox16");
            HtmlInputCheckBox cbx17 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox17");
            HtmlInputCheckBox cbx19 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox19");
            HtmlInputCheckBox cbx20 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox20");
            HtmlInputCheckBox cbx21 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox21");

            cbx1.Checked = IsCheckBoxSelected("Email me when someone adds a review", cbx1);
            cbx2.Checked = IsCheckBoxSelected("Email me when i have new inbox email", cbx2);
            cbx3.Checked = IsCheckBoxSelected("Email me when i have new lead", cbx3);
            cbx4.Checked = IsCheckBoxSelected("Email me for completing my registration", cbx4);
            cbx5.Checked = IsCheckBoxSelected("Email me when a company adds me as his client", cbx5);
            cbx6.Checked = IsCheckBoxSelected("Email me when someone invites me to team", cbx6);
            cbx7.Checked = IsCheckBoxSelected("Email me when someone invites me to his collaboration tool", cbx7);
            cbx8.Checked = IsCheckBoxSelected("Email me when someone accept my invitation to my collaboration tool", cbx8);
            cbx9.Checked = IsCheckBoxSelected("Email me to complete my registration in order to accept collaboration invitation", cbx9);
            cbx10.Checked = IsCheckBoxSelected("Email me when someone request a demo", cbx10);
            cbx11.Checked = IsCheckBoxSelected("Email me when my collaboration vendor upload a file to library", cbx11);
            cbx12.Checked = IsCheckBoxSelected("Email me when my collaboration partner add new deal registration", cbx12);
            cbx13.Checked = IsCheckBoxSelected("Email me when my collaboration partner accept/reject new deal registration", cbx13);
            cbx14.Checked = IsCheckBoxSelected("Email me when my collaboration partner win/lost new deal registration", cbx14);
            cbx15.Checked = IsCheckBoxSelected("Email me when my collaboration vendor add a new lead distribution", cbx15);
            cbx16.Checked = IsCheckBoxSelected("Email me when my collaboration partner win/lost new lead distribution", cbx16);
            cbx17.Checked = IsCheckBoxSelected("Email me when my collaboration partner add new partner to partner deal", cbx17);
            cbx19.Checked = IsCheckBoxSelected("Email me when someone reject/deny invitation to my PRM account", cbx19);
            cbx20.Checked = IsCheckBoxSelected("Email me when a partner Open/Close deal that has provided to me", cbx20);
            cbx21.Checked = IsCheckBoxSelected("Email me when someone register from partner portal to my PRM account", cbx21);
        }

        private int SetUserEmails(HtmlInputCheckBox cbx, int selectedItemsCount)
        {
            if (cbx.Checked)
            {
                if (!Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, Convert.ToInt32(cbx.Value), session))   // && communitySelected)
                {
                    ElioUserEmailNotificationsSettings newNotification = new ElioUserEmailNotificationsSettings();

                    newNotification.UserId = vSession.User.Id;
                    newNotification.EmaiNotificationsId = Convert.ToInt32(cbx.Value);

                    DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);
                    loader.Insert(newNotification);
                }

                selectedItemsCount++;
            }
            else
            {
                Sql.DeleteUserEmailNotificationSettings(Convert.ToInt32(cbx.Value), vSession.User.Id, session);
            }

            return selectedItemsCount;
        }

        private bool IsCheckBoxSelected(string description, HtmlInputCheckBox cbx)
        {
            bool isSelected = false;

            ElioEmailNotifications notification = Sql.GetElioEmailNotificationByDescription(description, session);
            if (notification != null)
            {
                isSelected = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
                cbx.Value = notification.Id.ToString();
                notification = null;
            }

            return isSelected;
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

                Session.Clear();
                vSession.SubAccountEmailLogin = "";

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

        protected void aPartnerManagement_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aPartnerManagement.Attributes["class"] += " active";
                aPartnerRecruitment.Attributes["class"] = "nav-link py-4 px-6";
                //kt_header_tab_2.Visible = true;
                //kt_header_tab_1.Visible = false;
                kt_header_tab_2.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";
                kt_header_tab_1.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";

                SetMenuSelectedItemActive();
            }
            catch(Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPartnerRecruitment_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aPartnerRecruitment.Attributes["class"] += " active";
                aPartnerManagement.Attributes["class"] = "nav-link py-4 px-6";
                kt_header_tab_1.Attributes["class"] = "tab-pane py-5 p-lg-0 show active";
                kt_header_tab_1.Visible = true;
                kt_header_tab_2.Visible = false;
                kt_header_tab_2.Attributes["class"] = "tab-pane p-5 p-lg-0 justify-content-between";

                SetMenuSelectedItemActive();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPartnerPortalLogin_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "/" + Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-login";
                    Response.Redirect(url, false);

                    vSession.User = null;
                    Session.Clear();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aLocator_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "/" + Regex.Replace(vSession.User.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-locator";
                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnSaveNotifications_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int selectedItemsCount = 0;

                    HtmlInputCheckBox cbx1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox1");
                    HtmlInputCheckBox cbx2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox2");
                    HtmlInputCheckBox cbx3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox3");
                    HtmlInputCheckBox cbx4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox4");
                    HtmlInputCheckBox cbx5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox5");
                    HtmlInputCheckBox cbx6 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox6");
                    HtmlInputCheckBox cbx7 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox7");
                    HtmlInputCheckBox cbx8 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox8");
                    HtmlInputCheckBox cbx9 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox9");
                    HtmlInputCheckBox cbx10 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox10");
                    HtmlInputCheckBox cbx11 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox11");
                    HtmlInputCheckBox cbx12 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox12");
                    HtmlInputCheckBox cbx13 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox13");
                    HtmlInputCheckBox cbx14 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox14");
                    HtmlInputCheckBox cbx15 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox15");
                    HtmlInputCheckBox cbx16 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox16");
                    HtmlInputCheckBox cbx17 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox17");
                    HtmlInputCheckBox cbx19 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox19");
                    HtmlInputCheckBox cbx20 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox20");
                    HtmlInputCheckBox cbx21 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox21");

                    selectedItemsCount = SetUserEmails(cbx1, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx2, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx3, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx4, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx5, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx6, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx7, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx8, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx9, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx10, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx11, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx12, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx13, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx14, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx15, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx16, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx17, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx19, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx20, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx21, selectedItemsCount);

                    LoadEmailSettings();

                    if (selectedItemsCount > 0)
                    {
                        string alert = "Your account settings were successfully updated";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Success, true, true, false);
                    }
                    else
                    {
                        string alert = "No items have been selected";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Info, true, true, false);
                    }

                    //UpdatePanel22.Update();

                    cbx1.Attributes["class"] = "make-switch";
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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