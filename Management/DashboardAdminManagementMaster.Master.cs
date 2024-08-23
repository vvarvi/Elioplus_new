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

namespace WdS.ElioPlus.Management
{
    public partial class DashboardAdminManagementMaster : System.Web.UI.MasterPage
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

            aSubMenuOpportunities.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
            aSubMenuOpportunitiesTasks.HRef = ControlLoader.Dashboard(vSession.User, "opportunities-tasks");
            aSubMenuDemoRequests.HRef = ControlLoader.Dashboard(vSession.User, "admin-demo-requests-management");
                        
            aUserAdminPage.HRef = ControlLoader.AdminPage;
            aManagement.HRef = ControlLoader.AdminUserManagement;
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

            LblMenuManagement.Text = "Management";
            LblSubMenuOpportunities.Text = "Reports";
            LblSubMenuOpportunitiesTasks.Text = "Tasks";
            LblSubMenuDemoRequests.Text = "Demo Requests";
            LblCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("footer", "sitemaster", "literal", "9")).Text;   //"Copyright @ 2016. Design by";
            LblCopyrightContent.Text = " Elioplus Team!";

            LblUserAdminPage.Text = "Admin page";
            LblManagementPage.Text = "Management";
            LblUserLogout.Text = "Logout";
        }

        private void SetMenuSelectedItemActive()
        {
            string path = HttpContext.Current.Request.Url.AbsolutePath;
                       
            if (path.Contains("/opportunities"))
            {
                liMenuManagement.Attributes["class"] += " active";

                if (path.Contains("/opportunities") || path.Contains("/opportunities-add-edit"))
                {
                    liSubMenuManagement.Attributes["class"] += " active";
                    liSubMenuOpportunitiesTasks.Attributes["class"] = " nav-item";
                }
                if (path.Contains("/opportunities-tasks") || path.Contains("/opportunities-view-tasks"))
                {
                    liSubMenuManagement.Attributes["class"] = " nav-item";
                    liSubMenuOpportunitiesTasks.Attributes["class"] += " active";                    
                }
                if (path.Contains("/admin-demo-requests-management"))
                {
                    liSubMenuDemoRequests.Attributes["class"] = " nav-item";
                    liSubMenuDemoRequests.Attributes["class"] += " active";
                }

                spanOpportunitiesArrow.Attributes["class"] = "arrow open";
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

                liUserOptions.Attributes["data-step"] = "1";
                liUserOptions.Attributes["data-intro"] = "Use the drop-down menu to view or edit your company's public profile as well as your community profile!";
                liUserOptions.Attributes["data-position"] = "left";

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

            aSubMenuOpportunitiesTasks.Visible =
                aSubMenuOpportunities.Visible = false;

                aMenuManagement.Visible =
                aSubMenuDemoRequests.Visible =
            aUserAdminPage.Visible =
                aManagement.Visible = Sql.IsUserAdministrator(vSession.User.Id, session);
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