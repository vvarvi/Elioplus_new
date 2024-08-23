using System;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Configuration;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus
{
    public partial class CollaborationDashboardMaster : System.Web.UI.MasterPage
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

        //protected void Page_PreInit(object sender, EventArgs args)
        //{
        //    MasterPageFile = "CollaborationToolMaster.Master";
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();
                                       
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        SetLinks();
                        PageTitle();
                        SetMenuSelectedItemActive();
                    }

                    FixPage();
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
            }
        }        

        # region Methods

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.User, out metaDescription, out metaKeywords, session);

            //MetaDescription = metaDescription;
            //MetaKeywords = metaKeywords;
        }

        private void SetLinks()
        {
            aElioplusLogo.HRef = ControlLoader.Default();
            aCopyright.HRef = ControlLoader.About;
            aBtnSearch.HRef = ControlLoader.Search;
            aMenuDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aMenuFindPartners.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");
            aMenuConnections.HRef = ControlLoader.Dashboard(vSession.User, "new-connections");
            aMenuNewClients.HRef = ControlLoader.Dashboard(vSession.User, "new-clients");
            aSubMenuOpportunities.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
            aSubMenuOpportunitiesTasks.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-tasks");
            aMenuTeam.HRef = ControlLoader.Dashboard(vSession.User, "team");
            aMenuCollaborationInvitation.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-invitations");
            aMenuCollaborationPartners.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-partners");
            aMenuCollaborationMailBox.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-empty"); //ControlLoader.DashboardCollaborationChatRoom;   //ControlLoader.Dashboard(vSession.User, "collaboration-chat-room");     //"~/DashboardCollaborationChatRoom.aspx";   //ControlLoader.Dashboard(vSession.User, "collaboration-mailbox");
            aMenuCollaborationChooseVendors.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-choose-vendors");
            aMenuCollaborationLibrary.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-library/" + Guid.NewGuid().ToString());
            aMenuMessages.HRef = ControlLoader.Dashboard(vSession.User, "messages/inbox");
            aMenuLeads.HRef = aUserLeads.HRef = ControlLoader.Dashboard(vSession.User, "leads");
            aMenuFavorites.HRef = ControlLoader.Dashboard(vSession.User, "favourites");
            aMenuBilling.HRef = ControlLoader.Dashboard(vSession.User, "billing");
            aMenuAlgorithm.HRef = ControlLoader.Dashboard(vSession.User, "algorithm");
            aMenuInfoLogs.HRef = ControlLoader.Dashboard(vSession.User, "info-logs");
            aMenuLogs.HRef = ControlLoader.Dashboard(vSession.User, "logs");
            aMenuStatistics.HRef = ControlLoader.Dashboard(vSession.User, "statistics");
            aUserEditProfile.HRef = ControlLoader.Dashboard(vSession.User, "edit-company-profile");
            aAdminElioFinancialIncomeFlow.HRef = ControlLoader.AdminElioFinancialIncomeFlowPage;
            aAdminElioFinancialExpensesFlow.HRef = ControlLoader.AdminElioFinancialExpensesFlowPage;
            aAddThirdPartyUsers.HRef = ControlLoader.AdminAddThirdPartyUsersPage;
            aUserGoFull.HRef = ControlLoader.FullRegistrationPage;
            aUserAdminPage.HRef = ControlLoader.AdminPage;
            aUserAdminStatisticsPage.HRef = ControlLoader.AdminStatisticsPage;
            aUserDashboard.HRef = ControlLoader.Dashboard(vSession.User, "home");
            aUserCommunityProfile.HRef = ControlLoader.CommunityUserProfile(vSession.User);
        }

        private void UpdateStrings()
        {
            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                LblBtnSearch.Text = "Search for Resellers";
            }
            else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                LblBtnSearch.Text = "Search for Vendors";
            }
            else
            {
                LblBtnSearch.Text = "Browse companies";
            }
            LblMenuDashboard.Text = "Dashboard";
            LblMenuFindPartners.Text = "Find new partners";
            LblMenuConnections.Text = "My connections";
            LblMenuNewClients.Text = "Add new clients";
            LblMenuOpportunities.Text = LblSubMenuOpportunities.Text = "Opportunities";
            LblSubMenuOpportunitiesTasks.Text = "Tasks";
            LblMenuTeam.Text = "Team";
            LblMenuCollaboration.Text = "Collaboration tool";
            LblMenuCollaborationInvitation.Text = "My Invitations";
            LblMenuCollaborationPartners.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "My Resellers List" : "My Vendors List";
            LblMenuCollaborationMailBox.Text = "Collaboration room";
            LblMenuCollaborationChooseVendors.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Choose Reseller" : "Choose Vendor";
            LblMenuCollaborationLibrary.Text = "Library";
            LblMenuMessages.Text = "Messages";
            LblMenuLeads.Text = "Leads";
            LblMenuFavorites.Text = "Favourites";
            LblMenuBilling.Text = "Billing";
            LblMenuAlgorithm.Text = "Algorithm";
            LblMenuStatistics.Text = "Statistics";
            LblMenuInfoLogs.Text = "Info Logs";
            LblMenuLogs.Text = "Logs";
            LblCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("footer", "sitemaster", "literal", "9")).Text;   //"Copyright @ 2016. Design by";
            LblCopyrightContent.Text = " Elioplus Team!";
            LblUserProfile.Text = "My profile";
            LblUserDashboard.Text = "Dashboard";
            LblUserEditProfile.Text = "Edit profile";
            LblAdminElioFinancialIncomeFlow.Text = "Financial income flow";
            LblAdminElioFinancialExpensesFlow.Text = "Financial expenses flow";
            LblUserCommunityProfile.Text = "Community profile";
            LblUserLogout.Text = "Logout";
            LblUserAdminPage.Text = "Admin page";
            LblUserAdminStatisticsPage.Text = "Statistics Page";
            LblAddThirdPartyUsers.Text = "Add third party users";
            LblUserTutorial.Text = "Tutorial";
        }

        private void SetMenuSelectedItemActive()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;

            if (path.Contains("/home"))
            {
                liMenuDash.Attributes["class"] += " active";
            }
            else if (path.Contains("/find-new-partners"))
            {
                liMenuPartners.Attributes["class"] += " active";
            }
            else if (path.Contains("/new-connections"))
            {
                liMenuConnections.Attributes["class"] += " active";
            }
            else if (path.Contains("/new-clients"))
            {
                liMenuNewClients.Attributes["class"] += " active";
            }
            else if (path.Contains("/opportunities"))
            {
                liMenuOpportunities.Attributes["class"] += " active";

                if (path.Contains("/opportunities") || path.Contains("/opportunities-add-edit"))
                {
                    liSubMenuOpportunities.Attributes["class"] += " active";
                    liSubMenuOpportunitiesTasks.Attributes["class"] = " nav-item";
                }
                if (path.Contains("/opportunities-tasks") || path.Contains("/opportunities-view-tasks"))
                {
                    liSubMenuOpportunities.Attributes["class"] = " nav-item";
                    liSubMenuOpportunitiesTasks.Attributes["class"] += " active";                    
                }

                spanOpportunitiesArrow.Attributes["class"] = "arrow open";
            }
            else if (path.Contains("/tasks"))
            {
                liMenuOpportunities.Attributes["class"] += " active";
            }
            else if (path.Contains("/team"))
            {
                liMenuTeam.Attributes["class"] += " active";
            }
            else if (path.Contains("/collaboration"))
            {
                liMenuCollaboration.Attributes["class"] += " active";

                if (path.Contains("/collaboration-invitations"))
                    liMenuCollaborationInvitation.Attributes["class"] += " active";
                else if (path.Contains("/collaboration-partners"))
                    liMenuCollaborationPartners.Attributes["class"] += " active";
                else if (path.Contains("/collaboration-mailbox"))
                    liMenuCollaborationMailBox.Attributes["class"] += " active";
                else if (path.Contains("/collaboration-choose-vendors"))
                    liMenuCollaborationChooseVendors.Attributes["class"] += "active";
                else if (path.Contains("/collaboration-library"))
                    liMenuCollaborationLibrary.Attributes["class"] += "active";

                spanCollaborationArrow.Attributes["class"] = "arrow open";
            }
            else if (path.Contains("/messages"))
            {
                liMenuMessages.Attributes["class"] += " active";
            }
            else if (path.Contains("/leads"))
            {
                liMenuLeads.Attributes["class"] += " active";
            }
            else if (path.Contains("/favourites"))
            {
                liMenuFavorites.Attributes["class"] += " active";
            }
            else if (path.Contains("/billing"))
            {
                liMenuBilling.Attributes["class"] += " active";
            }
            else if (path.Contains("/algorithm"))
            {
                liMenuAlgor.Attributes["class"] += " active";
            }
            else if (path.Contains("/statistics"))
            {
                liMenuStats.Attributes["class"] += " active";
            }
            else if (path.Contains("/logs"))
            {
                liMenuLogs.Attributes["class"] += " active";
            }
            else if (path.Contains("/info-logs"))
            {
                liMenuInfoLogs.Attributes["class"] += " active";
            }
        }

        private void FixPage()
        {
            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                #region Account Completed

                int newMessages = Sql.GetCompanyMessages(vSession.User.Id, 1, 0, session);
                UserMessages.Visible = newMessages > 0 ? true : false;
                LblUserMessages.Text = UserMessages.Visible ? newMessages.ToString() : string.Empty;
                LblUserMessagesContent.Text = newMessages > 0 ? "You have " + newMessages.ToString() + " new messages!" : "No new messages";

                int isNew = 1;
                int isDeleted = 0;
                int companyLeads = Sql.GetCompanyRecentLeadsForCurrentMonthByIsNewIsDeletedStatus(vSession.User, isNew, isDeleted, session);
                UserLeads.Visible = companyLeads > 0 ? true : false;
                LblUserLeads.Text = UserLeads.Visible ? companyLeads.ToString() : string.Empty;
                LblUserLeadsContent.Text = companyLeads > 0 ? "You have " + companyLeads.ToString() + " new leads!" : "No new leads";

                //bool hasActiveSubscription = Sql.HasActivePacketSubscription(vSession.User.Id, session);       //Sql.GetUserLastOrderByStatus(vSession.User.Id, Convert.ToInt32(OrderStatus.Canceled), session);
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

                UserNotif.Visible = (!hasActiveSubscription) ? true : false;
                LblUserNotif.Text = UserNotif.Visible ? "1" : "0";
                LblUserNotifContent.Text = !hasActiveSubscription ? "You have canceled your premium plan" : "No new notifications";
                aUserNotif.HRef = (!hasActiveSubscription) ? ControlLoader.Dashboard(vSession.User, "billing") : ControlLoader.Dashboard(vSession.User, "home");

                ImgUserPhoto.ImageUrl = (!string.IsNullOrEmpty(vSession.User.PersonalImage)) ? vSession.User.PersonalImage : vSession.User.CompanyLogo;
                LblUserCompanyName.Text = (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName : vSession.User.CompanyName;

                aUserProfile.Visible = (vSession.User.IsPublic == 1) ? true : false;
                aUserGoFull.Visible = false;
                aUserProfile.HRef = ControlLoader.Profile(vSession.User);
                aMenuBilling.Visible = true;

                liUserOptions.Attributes["data-step"] = "1";
                liUserOptions.Attributes["data-intro"] = "Use the drop-down menu to view or edit your company's public profile as well as your community profile!";
                liUserOptions.Attributes["data-position"] = "left";

                liMenuPartners.Attributes["data-step"] = "2";
                liMenuPartners.Attributes["data-intro"] = "Complete or edit your matching selection so we can start connecting you with potential partners";
                liMenuPartners.Attributes["data-position"] = "right";

                liMenuConnections.Attributes["data-step"] = "3";
                liMenuConnections.Attributes["data-intro"] = "Browse the list of the companies that we have connected you with based on the matching process. You can use the list to follow up or transfer the leads in your CRM!";
                liMenuConnections.Attributes["data-position"] = "right";

                liMenuTeam.Attributes["data-step"] = "4";
                liMenuTeam.Attributes["data-intro"] = "Invite and add your team members to your company's account";
                liMenuTeam.Attributes["data-position"] = "right";

                liMenuMessages.Attributes["data-step"] = "5";
                liMenuMessages.Attributes["data-intro"] = "Send direct messages to other companies that you want to get in touch or reply to requests!";
                liMenuMessages.Attributes["data-position"] = "right";

                liMenuLeads.Attributes["data-step"] = "6";
                liMenuLeads.Attributes["data-intro"] = "Browse the list of companies that viewed your company’s profile!";
                liMenuLeads.Attributes["data-position"] = "right";

                liMenuFavorites.Attributes["data-step"] = "7";
                liMenuFavorites.Attributes["data-intro"] = "Check the companies that have saved your company's public profile and also those that you have saved!";
                liMenuFavorites.Attributes["data-position"] = "right";

                liMenuBilling.Attributes["data-step"] = "8";
                liMenuBilling.Attributes["data-intro"] = "Check your billing status and details!";
                liMenuBilling.Attributes["data-position"] = "right";

                ulUserNotif.Attributes["data-step"] = "9";
                ulUserNotif.Attributes["data-intro"] = "You will be alerted for any new notifications!";
                ulUserNotif.Attributes["data-position"] = "down";

                #endregion
            }
            else
            {
                #region Account Not Completed

                ImgUserPhoto.ImageUrl = "/assets/layouts/layout/img/avatar.png";
                LblUserCompanyName.Text = vSession.User.Username;

                LblUserMessagesContent.Text = "No new messages";
                UserMessages.Visible = false;
                LblUserMessages.Text = string.Empty;

                UserLeads.Visible = false;
                LblUserLeads.Text = string.Empty;
                LblUserLeadsContent.Text = "No new leads";

                UserNotif.Visible = false;
                LblUserNotif.Text = "0";
                LblUserNotifContent.Text = "No new notifications";
                aUserNotif.HRef = ControlLoader.Dashboard(vSession.User, "home");

                aUserProfile.Visible = false;
                aUserGoFull.Visible = true;
                aUserProfile.HRef = "";
                aMenuBilling.Visible = false;
                LblUserGoFull.Text = "Complete registration";

                liUserOptions.Attributes["data-step"] = "1";
                liUserOptions.Attributes["data-intro"] = "Use the drop-down menu to complete your registration or edit your company's profile!";
                liUserOptions.Attributes["data-position"] = "left";

                ulMainMenu.Attributes["data-step"] = "2";
                ulMainMenu.Attributes["data-intro"] = "After you complete your registration, use the main menu to view your leads, send messages and view your favourites. As a vendor or reseller, find your new partners!";
                ulMainMenu.Attributes["data-position"] = "right";

                ulUserNotif.Attributes["data-step"] = "3";
                ulUserNotif.Attributes["data-intro"] = "You will be alerted for any new notifications!";
                ulUserNotif.Attributes["data-position"] = "down";

                divSearchBtn.Attributes["data-step"] = "4";
                divSearchBtn.Attributes["data-intro"] = "Click here to search for companies on our platform!";
                divSearchBtn.Attributes["data-position"] = "down";

                aElioplusLogo.Attributes["data-step"] = "5";
                aElioplusLogo.Attributes["data-intro"] = "Go back to our homepage if you want!";
                aElioplusLogo.Attributes["data-position"] = "right";

                #endregion
            }

            aUserCommunityProfile.Visible = (vSession.User.CommunityStatus == 1) ? true : false;

            //aMenuFindPartners.Visible = vSession.User.CompanyType != Types.Developers.ToString() ? true : false;
            //aMenuConnections.Visible = (vSession.User.CompanyType != Types.Developers.ToString() || Sql.IsUserAdministrator(vSession.User.Id, session)) ? true : false;
            aMenuTeam.Visible =
                aSubMenuOpportunitiesTasks.Visible =
                aSubMenuOpportunities.Visible =
                aMenuOpportunities.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
            
            aMenuCollaboration.Visible = (ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true") ? (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || Sql.IsUserAdministrator(vSession.User.Id, session)) ? true : false : false;     //Sql.IsUserAdministrator(vSession.User.Id, session);
            aMenuNewClients.Visible = Sql.HasRoleByDescription(vSession.User.Id, UserRoles.Supervisor.ToString(), session);

            aMenuAlgorithm.Visible =
                aMenuInfoLogs.Visible =
                aMenuLogs.Visible =
                aMenuStatistics.Visible =
                aUserAdminPage.Visible =
                aUserAdminStatisticsPage.Visible =
                aAddThirdPartyUsers.Visible =
                aAdminElioFinancialIncomeFlow.Visible =
                aAdminElioFinancialExpensesFlow.Visible = Sql.IsUserAdministrator(vSession.User.Id, session) ? true : false;
        }

        protected void Logout_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                    Logger.Info("User {0}, logged out", vSession.User.Id);

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

                Response.Redirect(ControlLoader.Default(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion
    }
}