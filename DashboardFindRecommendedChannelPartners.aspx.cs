using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;
using System.Data;
using System.Configuration;

namespace WdS.ElioPlus
{
    public partial class DashboardFindRecommendedChannelPartners : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page Properties

        public int ConnectionId
        {
            get
            {
                if (ViewState["ConnectionId"] != null)
                    return Convert.ToInt32(ViewState["ConnectionId"]);
                else
                    return -1;
            }
            set
            {
                ViewState["ConnectionId"] = value;
            }
        }

        public string CompanyName
        {
            get
            {
                if (ViewState["CompanyName"] != null)
                    return ViewState["CompanyName"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["CompanyName"] = value;
            }
        }

        public DataTable MatchesDataTable
        {
            get
            {
                if (ViewState["MatchesDataTable"] != null)
                    return ViewState["MatchesDataTable"] as DataTable;
                else
                    return null;
            }
            set
            {
                ViewState["MatchesDataTable"] = value;
            }
        }

        #endregion

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
                        Response.Redirect(errorPage, false);
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
                UpdateStrings();
                SetLinks();
                LoadCompanySize();

                UcConnectionsMessageAlert.Visible = false;
                ConnectionId = 0;
                vSession.ViewStateDataStore = null;
                vSession.HasSearchCriteria = false;

                #region top-right menu

                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                    if (packet != null)
                    {
                        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                    }

                    LblRenewalHead.Visible = LblRenewal.Visible = true;
                    LblRenewalHead.Text = "Renwal date: ";

                    try
                    {
                        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                    }
                    catch (Exception)
                    {
                        LblRenewalHead.Visible = LblRenewal.Visible = false;

                        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                    }

                    //divFreemiumUser.Visible = false;
                    //divPremiumUser.Visible = true;
                }
                else
                {
                    //divFreemiumUser.Visible = true;
                    //divPremiumUser.Visible = false;
                    //divConnections.Visible = Sql.HasUserConnections(vSession.User.Id, session);
                    //divPremiumUser.Visible = divConnections.Visible;

                    LblRenewalHead.Visible = LblRenewal.Visible = false;
                    LblPricingPlan.Text = "You are currently on a free plan";
                }

                aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
                aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

                LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

                LblDashboard.Text = "Dashboard";

                if (aBtnGoPremium.Visible)
                {
                    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                    LblPricingPlan.Visible = false;
                }

                LblGoFull.Text = "Complete your registration";
                LblDashPage.Text = "Find partners";
                LblDashSubTitle.Text = "find your future partners";

                #endregion

                if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                {
                    #region Account Completed

                    bool hasConnections = Sql.HasUserConnections(vSession.User.Id, session);

                    if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                    {
                        PnlFindPartners.Visible = true;
                    }

                    List<ElioSubIndustriesGroupItems> userVerticals = Sql.GetUserSubIndustries(vSession.User.Id, session);

                    if (userVerticals.Count == 0)
                    {
                        PnlNotRegisteredOrNoVerticals.Visible = true;
                        PnlFindPartners.Visible = false;

                        return;
                    }

                    List<ElioUsersAlgorithmSubcategories> userAlgorithmVerticals = Sql.GetUserAlgorithmSubcategoriesById(vSession.User.Id, session);

                    PnlNotRegisteredOrNoVerticals.Visible = false;
                    PnlFindPartners.Visible = vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType || (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType && !hasConnections);

                    if (userVerticals.Count > 0)
                    {                       
                        #region Fix CheckBox List

                        if (PnlFindPartners.Visible)
                        {
                            if (!IsPostBack)
                            {
                                FixCheckBoxList(userVerticals, userAlgorithmVerticals);
                            }
                        }
                        else
                        {
                            PnlNotRegisteredOrNoVerticals.Visible = userVerticals.Count == 0;
                        }

                        #endregion
                    }

                    UserOpportunities.Attributes["data-value"] = FindCompanyOpportunities(CbxSubcategories).ToString();
                    LblUserOpportunities.Text = FindCompanyOpportunities(CbxSubcategories).ToString();

                    #endregion
                }
                else
                {
                    #region Account Not Completed

                    PnlNotRegisteredOrNoVerticals.Visible = true;
                    PnlFindPartners.Visible = false;
                    
                    divConnectionsTableHolder.Visible = false;
                    
                    #endregion
                }

                if (vSession.User != null)
                    LoadRequestDemoData();
            }
        }

        private void FixCheckBoxList(List<ElioSubIndustriesGroupItems> userVerticals, List<ElioUsersAlgorithmSubcategories> userAlgorithmVerticals)
        {
            CbxSubcategories.Items.Clear();

            foreach (ElioSubIndustriesGroupItems userVertical in userVerticals)
            {
                ListItem li = new ListItem();
                li.Value = userVertical.Id.ToString();
                li.Text = userVertical.Description;

                if (userAlgorithmVerticals.Count > 0)
                    li.Selected = Sql.ExistUserAlgorithmSubcategory(vSession.User.Id, userVertical.Id, session);
                else
                    li.Selected = true;

                CbxSubcategories.Items.Add(li);
            }
        }

        private int FindCompanyOpportunities(CheckBoxList cbxSubCategoriesList)
        {
            int opportunities = 0;
            int dbOpportunities = 0;
            int otherOpportunities = 0;

            string subcategoriesId = string.Empty;

            foreach (ListItem li in cbxSubCategoriesList.Items)
            {
                if (li.Selected)
                {
                    subcategoriesId += li.Value + ",";
                }
            }

            if (subcategoriesId.Length > 0)
            {
                otherOpportunities = Sql.GetUserOpportunitiesSum(subcategoriesId.Substring(0, subcategoriesId.Length - 1), session);

                dbOpportunities = (!string.IsNullOrEmpty(subcategoriesId)) ? Sql.GetPossibleOpportunitiesForUserBySubCategoriesIdAndCompanyType(subcategoriesId.Substring(0, subcategoriesId.Length - 1), (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString(), session) : 0;
            }

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                opportunities = dbOpportunities + otherOpportunities;
            }
            else
            {
                opportunities = dbOpportunities;
            }

            return opportunities;
        }

        private void UpdateStrings()
        {
            LblNotRegistHeader.Text = "Get connected with your ";
            LblNotRegistSubHeader.Text = "business partners";
            LblActionNotRegist.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "In order to use our MATCHING PROCESS you have to be full registered. Click on the 'Complete registration' option at the top right corner and " : "In order to use our MATCHING PROCESS you must first choose your company's industry verticals. Click on the 'Edit profile' option at the top right corner and then on 'Industry verticals' and ";
            LblActionLink.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "complete your company's profile!" : "save your verticals selection!";

            LblVerticalsSelection.Text = "Your business verticals selection";
            LblAvaiOpportunities.Text = "Partnership opportunities";
            LblAvailPartners.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Resellers on our platform" : "Vendors on our platform";
            LblOpportInfo.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "These are the reseller opportunities available based on your verticals selection. If you change the options on your left you will see the number to adjust." : "These are the vendor opportunities available based on your verticals selection. If you change the options on your left you will see the number to adjust.";

            #region connections Page

            LblGoFullOrReg.Text = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? "No connections? Complete your registration and upgrade to premium!" : "No connections? Upgrade to premium!";

            //LblConnectionsTitle.Text = "Your recent connections. You can have a look at their profile!";
            LblConfTitle.Text = "Confirmation";
            LblConfMsg.Text = "Are you sure you want to delete this connection?";

            #endregion
        }

        private void SetLinks()
        {
            aFullRegist.HRef = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : ControlLoader.Dashboard(vSession.User, "edit-company-profile");
        }
        
        private void ResetDemoRequestFields()
        {
            if (vSession.User == null)
            {
                TbxFirstName.Text = "";
                TbxLastName.Text = "";
                TbxCompanyEmail.Text = "";
                TbxBusinessName.Text = "";
            }

            if (DrpBusinessSize.Items.Count > 0)
                DrpBusinessSize.SelectedValue = "0";

            divDemoWarningMsg.Visible = false;
            divDemoSuccessMsg.Visible = false;

            LblDemoWarningMsgContent.Text = "";
            LblDemoSuccessMsgContent.Text = "";
        }

        private void LoadCompanySize()
        {
            DrpBusinessSize.Items.Clear();

            ListItem item = new ListItem();
            item.Text = "Company Size";
            item.Value = "0";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "Myself Only";
            item.Value = "1";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "2-10 Employees";
            item.Value = "2";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "11-50 Employees";
            item.Value = "3";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "51-200 Employees";
            item.Value = "4";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "201-500 Employees";
            item.Value = "5";

            DrpBusinessSize.Items.Add(item);

            item = new ListItem();
            item.Text = "501+ Employees";
            item.Value = "6";

            DrpBusinessSize.Items.Add(item);

            DrpBusinessSize.DataBind();
        }

        private void LoadRequestDemoData()
        {
            if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {
                TbxFirstName.Text = vSession.User.FirstName;
                TbxLastName.Text = vSession.User.LastName;
                TbxCompanyEmail.Text = vSession.User.Email;
                TbxBusinessName.Text = vSession.User.CompanyName;
            }
            else
            {
                TbxCompanyEmail.Text = vSession.User.Email;
            }
        }

        # endregion

        protected void CbxSubcategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    UserOpportunities.Attributes["data-value"] = FindCompanyOpportunities(CbxSubcategories).ToString();
                    LblUserOpportunities.Text = FindCompanyOpportunities(CbxSubcategories).ToString();
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

        #region Buttons

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

        protected void BtnNew_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor btnNew = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)btnNew.NamingContainer;

                    ElioUsersConnections connection = Sql.GetUserConnection(Convert.ToInt32(item["connection_id"].Text), session);
                    if (connection != null)
                    {
                        connection.IsNew = 0;
                        connection.LastUpdated = DateTime.Now;

                        DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                        loader.Update(connection);
                    }

                    RdgConnections.Rebind();
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

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (vSession.ViewStateDataStore != null)
                        Response.Redirect("download-csv?case=MyMatchesData", false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBookDemo_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                ConnectionId = Convert.ToInt32(item["id"].Text);
                CompanyName = item["company_name"].Text.ToString();

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenBookDemoModal();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancelDemo_Click(object sender, EventArgs e)
        {
            try
            {
                LoadCompanySize();
                ResetDemoRequestFields();
                ConnectionId = -1;
                CompanyName = "";

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseBookDemoModal();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnRequestDemo_Click(object sender, EventArgs e)
        {
            try
            {
                divDemoWarningMsg.Visible = false;
                divDemoSuccessMsg.Visible = false;

                if (TbxFirstName.Text == "")
                {
                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Please fill first name";
                    return;
                }

                if (TbxLastName.Text == "")
                {
                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Please fill last name";
                    return;
                }

                if (TbxCompanyEmail.Text == "")
                {
                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Please fill company email";
                    return;
                }
                else
                {
                    if (!Validations.IsEmail(TbxCompanyEmail.Text))
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsgContent.Text = "Please fill with valid email address";
                        return;
                    }
                }

                if (TbxBusinessName.Text == "")
                {
                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Please fill company name";
                    return;
                }

                if (DrpBusinessSize.SelectedItem.Value == "0")
                {
                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Please select your company's size";
                    return;
                }

                if (vSession.ElioCompanyDetailsView == null)
                {
                    Response.Redirect(ControlLoader.Search, false);
                    return;
                }

                try
                {
                    session.OpenConnection();

                    if (ConnectionId > 0)
                    {
                        bool hasAlreadyRequest = Sql.HasDemoRequestByEmail(TbxCompanyEmail.Text, ConnectionId, session);
                        if (hasAlreadyRequest)
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsgContent.Text = "You have already requested a demo to " + CompanyName;
                            return;
                        }
                        
                        ElioUsersDemoRequests request = new ElioUsersDemoRequests();

                        request.RequestForUserId = ConnectionId;
                        request.FirstName = TbxFirstName.Text;
                        request.LastName = TbxLastName.Text;
                        request.CompanyEmail = TbxCompanyEmail.Text;
                        request.CompanyName = TbxBusinessName.Text;
                        request.CompanySize = DrpBusinessSize.SelectedItem.Text;
                        request.Sysdate = DateTime.Now;
                        request.DateApproved = null;
                        request.IsApproved = 0;

                        DataLoader<ElioUsersDemoRequests> loader = new DataLoader<ElioUsersDemoRequests>(session);
                        loader.Insert(request);

                        LblDemoSuccessMsgContent.Text = "Your request was sent successfully";
                        divDemoSuccessMsg.Visible = true;

                        ConnectionId = -1;
                        CompanyName = "";
                    }
                    else
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsgContent.Text = "Something went wrong. Please try again later.";
                        return;
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                    divDemoWarningMsg.Visible = true;
                    LblDemoWarningMsgContent.Text = "Something went wrong. Please try again later.";
                    return;
                }
                finally
                {
                    session.CloseConnection();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

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
                        ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                        if (user != null)
                        {
                            Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                            imgCompanyLogo.ImageUrl = item["company_logo"].Text;

                            HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                            aWebsite.HRef = item["website"].Text;

                            HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                            HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");

                            HtmlAnchor aViewProfile = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewProfile");
                            aViewProfile.HRef = aCompanyName.HRef = aCompanyLogo.HRef = ControlLoader.Profile(user);

                            Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                            lblCompanyName.Text = item["company_name"].Text;

                            Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");
                            lblWebsite.Text = "Visit website";

                            Label lblBookDemo = (Label)ControlFinder.FindControlRecursive(item, "LblBookDemo");
                            lblBookDemo.Text = "Book a demo";

                            Label lblViewProfile = (Label)ControlFinder.FindControlRecursive(item, "LblViewProfile");
                            lblViewProfile.Text = "View profile";

							imgCompanyLogo.AlternateText = lblCompanyName + "logo";													   
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Login, false);
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

                if (vSession.User != null)
                {
                    List<ElioUsers> partners = new List<ElioUsers>();

                    partners = Sql.GetPremiumVendorsWithSameSubIndustriesGroupItems(vSession.User.Id, session);

                    if (partners.Count > 0)
                    {
                        UcConnectionsMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("company_logo");
                        table.Columns.Add("website");
                        table.Columns.Add("email");
                        table.Columns.Add("first_name");
                        table.Columns.Add("last_name");

                        foreach (ElioUsers user in partners)
                        {
                            table.Rows.Add(user.Id, user.CompanyName, user.CompanyLogo, user.WebSite, user.Email, user.FirstName, user.LastName);
                        }

                        RdgConnections.DataSource = table;
                        RdgConnections.Visible = true;
                    }
                    else
                    {
                        RdgConnections.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, "There are no vendors matching your verticals", MessageTypes.Info, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);

                //UcConnectionsMessageAlert.Visible = false;
                //divConnections.Visible = divConnectionsTableHolder.Visible = true;

                //if (vSession.User != null)
                //{
                //    //int canBeViewed = 1;

                //    //List<ElioUsersConnectionsIJUsersIJPersonIJCompanies> userConnections = new List<ElioUsersConnectionsIJUsersIJPersonIJCompanies>();
                //    DataTable table = null;
                //    if (ViewState["StrQuery"] == null)
                //    {
                //        //userConnections = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompanies(vSession.User.Id, canBeViewed, session);
                //        table = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesTable(vSession.User.Id, 1, session);
                //        FillDataForExport(false);
                //    }
                //    else
                //    {
                //        //DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies> loader = new DataLoader<ElioUsersConnectionsIJUsersIJPersonIJCompanies>(session);
                //        //userConnections = loader.Load(ViewState["StrQuery"].ToString());
                //        table = session.GetDataTable(ViewState["StrQuery"].ToString());
                //    }

                //    if (table.Rows.Count > 0)
                //    {
                //        divConnections.Visible = divConnectionsTableHolder.Visible = true;
                //        RdgConnections.Visible = true;

                //        //DataTable table = new DataTable();

                //        //table.Columns.Add("id");
                //        //table.Columns.Add("company_id");
                //        //table.Columns.Add("company_logo");
                //        //table.Columns.Add("user_application_type");
                //        //table.Columns.Add("company_name");
                //        //table.Columns.Add("country");
                //        //table.Columns.Add("company_email");
                //        //table.Columns.Add("company_website");
                //        //table.Columns.Add("linkedin_url");
                //        //table.Columns.Add("sysdate");
                //        //table.Columns.Add("current_period_start");
                //        //table.Columns.Add("current_period_end");
                //        //table.Columns.Add("is_new");
                //        //table.Columns.Add("avatar");
                //        //table.Columns.Add("logo");

                //        //foreach (ElioUsersConnectionsIJUsersIJPersonIJCompanies con in userConnections)
                //        //{
                //        //    if (con.CanBeViewed == 1)
                //        //    {
                //        //        table.Rows.Add(con.Id, con.ConnectionId, con.CompanyLogo, con.UserApplicationType, con.CompanyName, con.Country, !string.IsNullOrEmpty(con.OfficialEmail) ? con.Email + ", " + con.OfficialEmail : con.Email, con.WebSite, con.Linkedin, con.SysDate.ToString("MM/dd/yyyy"), con.CurrentPeriodStart.ToString("MM/dd/yyyy"), con.CurrentPeriodEnd.ToString("MM/dd/yyyy"), con.IsNew, con.Avatar, con.Logo);
                //        //    }
                //        //}

                //        RdgConnections.DataSource = table;
                //    }
                //    else
                //    {
                //        divConnections.Visible = false;
                //        //divConnectionsTableHolder.Visible = vSession.HasSearchCriteria;
                //        RdgConnections.Visible = false;
                //        ImgBtnExport.Visible = false;
                //        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                //    }
                //}
                //else
                //{
                //    divConnections.Visible = divConnectionsTableHolder.Visible = false;
                //    RdgConnections.Visible = false;

                //    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                //}
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