using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class DashboardConnections : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int ConnectionId
        {
            get
            {
                if (ViewState["ConnectionId"] != null)
                    return Convert.ToInt32(ViewState["ConnectionId"]);
                else
                    return 0;
            }
            set
            {
                ViewState["ConnectionId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
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

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
      
                UpdateStrings();
                SetLinks();
                UcConnectionsMessageAlert.Visible = false;
                ConnectionId = 0;
            }

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                LblRenewalHead.Visible = LblRenewal.Visible = true;
                LblRenewalHead.Text = "Renewal date: ";

                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                if (packet != null)
                {
                    LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                }

                try
                {
                    LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                }
                catch (Exception)
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = false;

                    Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                }
            }
            else
            {
                LblRenewalHead.Visible = LblRenewal.Visible = false;
                LblPricingPlan.Text = "You are currently on a free plan";
            }

            aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;
            aGoFullOrReg.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

            LblDashboard.Text = "Dashboard";
            LblBtnGoPremium.Text = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && !string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "label", "17")).Text;
            LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Connections";
            LblDashSubTitle.Text = "connect with your future partners";
        }

        private void SetLinks()
        {
            aGoFullOrReg.HRef = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : ControlLoader.Dashboard(vSession.User, "billing");
            aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            //aBtnGoPremium.HRef = ControlLoader.Dashboard(vSession.User, "billing");
        }

        private void UpdateStrings()
        {            
            LblGoFullOrReg.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "No connections? Complete your registration and upgrade to premium!" : "No connections? Upgrade to premium!";

            LblConnectionsPageInfo.Text = "When you start your free trial and onwards, we'll send day by day your partner program data to potential partner that match with your company. The list below will be updating daily with more partners that you can follow up.";
            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                LblConnectionsPageInfo.Text += " Complete your registration to start your 14-day free trial!";

            LblConnectionsTitle.Text = "Your recent connections. You can have a look at their profile!";
            LblConfTitle.Text = "Confirmation";
            LblConfMsg.Text = "Are you sure you want to delete this connection?";
            //RdgConnections.MasterTableView.GetColumn("company_logo").HeaderText = "Logo";
            //RdgConnections.MasterTableView.GetColumn("company_name").HeaderText = "Name";
            //RdgConnections.MasterTableView.GetColumn("company_email").HeaderText = "E-mail";
            //RdgConnections.MasterTableView.GetColumn("company_website").HeaderText = "Website";
            //RdgConnections.MasterTableView.GetColumn("sysdate").HeaderText = "Date Given";
            //RdgConnections.MasterTableView.GetColumn("actions").HeaderText = "Actions";
        }
       
        # endregion

        # region Grids

        protected void RdgConnections_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridDataItem)
                {
                    session.OpenConnection();

                    GridDataItem item = (GridDataItem)e.Item;

                    if (vSession.User != null)
                    {
                        //ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["company_id"].Text), session);

                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        //Image imgUserApplicationType = (Image)ControlFinder.FindControlRecursive(item, "ImgUserApplicationType");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        if (Convert.ToInt32(item["user_application_type"].Text) == Convert.ToInt32(UserApplicationType.Elioplus))
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.ProfileForConnectionspage(Convert.ToInt32(item["company_id"].Text), item["company_name"].Text, item["company_type"].Text);

                            imgCompanyLogo.ToolTip = "View company's profile";
                            imgCompanyLogo.ImageUrl = item["company_logo"].Text;
                            imgCompanyLogo.AlternateText = "Company logo";
                        }
                        else
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = item["company_website"].Text;

                            aCompanyLogo.Target = aCompanyName.Target = "_blank";
                            imgCompanyLogo.ToolTip = "View company's site";
                            imgCompanyLogo.ImageUrl = "/images/icons/partners_th_party_2.png";
                            imgCompanyLogo.AlternateText = "Third party partners logo";
                        }

                        Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                        lblCompanyNameContent.Text = item["company_name"].Text;

                        Label lblCompanyEmail = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyEmail");
                        lblCompanyEmail.Text = item["company_email"].Text;

                        Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");
                        lblWebsite.Text = item["company_website"].Text;

                        HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                        aWebsite.HRef = item["company_website"].Text;

                        Image imgLinkedin = (Image)ControlFinder.FindControlRecursive(item, "ImgLinkedin");
                        Label lblNoLinkedin = (Label)ControlFinder.FindControlRecursive(item, "LblNoLinkedin");
                        HtmlAnchor aLinkedin = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aLinkedin");

                        if (!string.IsNullOrEmpty(item["linkedin_url"].Text))
                        {
                            imgLinkedin.Visible = true;
                            lblNoLinkedin.Visible = false;

                            aLinkedin.HRef = item["linkedin_url"].Text;
                            imgLinkedin.ToolTip = "View Linkedin profile";
                        }
                        else
                        {
                            imgLinkedin.Visible = false;
                            lblNoLinkedin.Visible = true;
                        }

                        Label lblViewGivenDate = (Label)ControlFinder.FindControlRecursive(item, "LblViewGivenDate");
                        lblViewGivenDate.Text = item["sysdate"].Text;

                        Label lblBtnDelete = (Label)ControlFinder.FindControlRecursive(item, "LblBtnDelete");
                        lblBtnDelete.Text = "";

                        HtmlAnchor btnAddOpportunity = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnAddOpportunity");
                        btnAddOpportunity.HRef = ControlLoader.Dashboard(vSession.User, "opportunities");
                        btnAddOpportunity.Visible = !Sql.IsUserOpportunity(vSession.User.Id, lblCompanyNameContent.Text, session);

                        Image imgOpportunitySuccess = (Image)ControlFinder.FindControlRecursive(item, "ImgOpportunitySuccess");
                        imgOpportunitySuccess.Visible = !btnAddOpportunity.Visible;

                        Label lblOpportunitySuccess = (Label)ControlFinder.FindControlRecursive(item, "LblOpportunitySuccess");
                        lblOpportunitySuccess.Visible = imgOpportunitySuccess.Visible;
                        if (imgOpportunitySuccess.Visible)
                            lblOpportunitySuccess.Text = "Added successfully";

                        Label lblAddOpportunity = (Label)ControlFinder.FindControlRecursive(item, "LblAddOpportunity");
                        lblAddOpportunity.Text = "Add to Opportunities";
                    }
                    else
                    {
                        Response.Redirect(vSession.Page, false);
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

        protected void RdgConnections_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcConnectionsMessageAlert.Visible = false;
                divConnections.Visible = true;

                if (vSession.User != null)
                {
                    //int canBeViewed = 1;

                    //List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> userConnections = new List<ElioUsersConnectionsIJUsersIJPersonIJCompanies>();
                    DataTable table=null;
                    if (ViewState["StrQuery"] == null)
                    {
                        //userConnections = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompanies(vSession.User.Id, canBeViewed, session);
                        table = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesTable(vSession.User.Id, 1, session);
                    }
                    else
                    {
                        //DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
                        //userConnections = loader.Load(ViewState["StrQuery"].ToString());
                        table = session.GetDataTable(ViewState["StrQuery"].ToString());
                    }

                    if (table.Rows.Count > 0)
                    {
                        divConnections.Visible = true;
                        RdgConnections.Visible = true;
                        
                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("company_id");
                        //table.Columns.Add("company_logo");
                        //table.Columns.Add("user_application_type");
                        //table.Columns.Add("company_name");
                        //table.Columns.Add("company_email");
                        //table.Columns.Add("company_website");
                        //table.Columns.Add("linkedin_url");
                        //table.Columns.Add("sysdate");
                        //table.Columns.Add("current_period_start");
                        //table.Columns.Add("current_period_end");
                        //table.Columns.Add("avatar");
                        //table.Columns.Add("logo");

                        //foreach (ElioUsersConnectionsIJUsersIJPersonIJCompanies con in userConnections)
                        //{
                        //    if (con.CanBeViewed == 1)
                        //    {
                        //        table.Rows.Add(con.Id, con.ConnectionId, con.CompanyLogo, con.UserApplicationType, con.CompanyName, !string.IsNullOrEmpty(con.OfficialEmail) ? con.Email + ", " + con.OfficialEmail : con.Email, con.WebSite, con.Linkedin, con.SysDate.ToString("MM/dd/yyyy"), con.CurrentPeriodStart.ToString("MM/dd/yyyy"), con.CurrentPeriodEnd.ToString("MM/dd/yyyy"), con.Avatar, con.Logo);

                        //        if (con.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                        //            divPartnersInfo.Visible = true;
                        //    }
                        //}

                        RdgConnections.DataSource = table;

                        LblConLogo.Text = "Logo";
                        //LblUserApplicationType.Text = "User Type";
                        LblConName.Text = "Name";
                        LblConEmail.Text = "E-mail";
                        LblConWebsite.Text = "Website";
                        LblLinkedin.Text = "Contact Person";
                        LblConDateStarted.Text = "Date";
                        //LblConDateFrom.Text = "From Date";
                        //LblConDateTo.Text = "To Date";
                        LblConAdd.Text = "Actions";
                    }
                    else
                    {
                        divConnections.Visible = false;
                        RdgConnections.Visible = false;

                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    divConnections.Visible = false;
                    RdgConnections.Visible = false;

                    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
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

        # region buttons

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    HtmlAnchor aDelBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)aDelBtn.NamingContainer;

                    ConnectionId = Convert.ToInt32(item["id"].Text);

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (ConnectionId != 0)
                    {
                        ElioUsersConnections connection = Sql.GetUserConnection(ConnectionId, session);

                        if (connection != null)
                        {
                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                            connection.CanBeViewed = 0;
                            loader.Update(connection);

                            RdgConnections.Rebind();
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                        ConnectionId = 0;
                    }
                    else
                    {
                        Logger.DetailedError(string.Format("User {0} tried to delete connection at {1}, but connection ID was 0", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, "Something went wrong! Your connection could not be deleted. Please try again later or contact us", MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default());
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

        protected void BtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    string strQuery = @"select Elio_users_connections.id, Elio_users_connections.user_id, Elio_users_connections.connection_id,
                                       Elio_users_connections.sysdate,  Elio_users_connections.current_period_start, Elio_users_connections.current_period_end, Elio_users_connections.can_be_viewed,
                                       Elio_users.company_name, Elio_users.email, Elio_users.official_email, Elio_users.website,
                                       Elio_users.company_logo, Elio_users.billing_type, Elio_users.user_application_type, Elio_users.country, Elio_users.company_type
                                       from Elio_users_connections
                                       inner join Elio_users on Elio_users.id = Elio_users_connections.connection_id
                                       where Elio_users_connections.user_id=" + vSession.User.Id + " " +
                                       "and can_be_viewed=1";

                    if (RdpConnectionsFrom.SelectedDate != null)
                    {
                        strQuery += " and Elio_users_connections.sysdate>='" + Convert.ToDateTime(RdpConnectionsFrom.SelectedDate).ToString("yyyy/MM/dd") + "'";
                    }

                    if (RdpConnectionsTo.SelectedDate != null)
                    {
                        strQuery += " and Elio_users_connections.sysdate<='" + Convert.ToDateTime(RdpConnectionsTo.SelectedDate).ToString("yyyy/MM/dd") + "'";
                    }

                    strQuery += " order by sysdate desc";

                    ViewState["StrQuery"] = strQuery;
                    RdgConnections.Rebind();
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

        protected void BtnAddOpportunity_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor btn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)btn.NamingContainer;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["company_id"].Text), session);

                    if (company != null)
                    {
                        bool existOpportunity = Sql.IsUserOpportunityByEmailOrName(vSession.User.Id, item["company_name"].Text, item["company_email"].Text, session);
                        if (!existOpportunity)
                        {
                            ElioOpportunitiesUsers opportunity = new ElioOpportunitiesUsers();

                            opportunity.UserId = vSession.User.Id;
                            opportunity.LastName = company.LastName;
                            opportunity.FirstName = company.FirstName;
                            opportunity.OrganizationName = company.CompanyName;
                            opportunity.Occupation = "";
                            opportunity.Address = company.Address;
                            opportunity.Email = company.Email;
                            opportunity.Phone = company.Phone;
                            opportunity.WebSite = company.WebSite;
                            opportunity.LinkedInUrl = company.LinkedInUrl;
                            opportunity.TwitterUrl = company.TwitterUrl;
                            opportunity.SysDate = DateTime.Now;
                            opportunity.LastUpdated = DateTime.Now;
                            opportunity.GuId = Guid.NewGuid().ToString();
                            opportunity.IsPublic = 1;
                            opportunity.StatusId = Convert.ToInt32(OpportunityStep.Contact);

                            DataLoader<ElioOpportunitiesUsers> loader = new DataLoader<ElioOpportunitiesUsers>(session);
                            loader.Insert(opportunity);

                            RdgConnections.Rebind();
                            //Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities"), false);
                            //Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            string alert = "This connection is already saved as your opportunity.";
                            GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, alert, MessageTypes.Error, true, true, false);
                        }
                    }
                    else
                    {
                        string alert = "Something went wrong! This connection could not be added as your contact too. Try again later or contact with us.";
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, alert, MessageTypes.Error, true, true, false);
                    }
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

        # endregion
    }
}