using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace WdS.ElioPlus
{
    public partial class DashboardRandstadDealsPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public string AdvancedSerch
        {
            get
            {
                if (ViewState["AdvancedSerch"] != null)
                {
                    return ViewState["AdvancedSerch"].ToString();
                }

                return "";
            }
            set
            {
                ViewState["AdvancedSerch"] = value;
            }
        }

        public bool ShowGridColumns
        {
            get
            {
                return (ViewState["ShowGridColumns"] != null) ? Convert.ToBoolean(ViewState["ShowGridColumns"].ToString()) : true;
            }
            set
            {
                ViewState["ShowGridColumns"] = value;
            }
        }

        public int CollaborationId
        {
            get
            {
                return (ViewState["CollaborationId"] != null) ? Convert.ToInt32(ViewState["CollaborationId"].ToString()) : -1;
            }
            set
            {
                ViewState["CollaborationId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (vSession.User.Id != GlobalMethods.GetRandstadCustomerID())
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
                            return;
                        }
                    }
                    else
                    {
                        bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(GlobalMethods.GetRandstadCustomerID(), vSession.User.Id, session);
                        if (!isCustomPartner)
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
                            return;
                        }
                    }

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(BtnAddNewDeal);

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

                    if (!IsPostBack)
                    {
                        ShowGridColumns = true;
                        FixPage();
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
            }
        }

        #region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divVendorSettings.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                
                CollaborationId = -1;
                GetCollaborationUsers();
                LoadDealResultStatus();

                UpdateStrings();
                SetLinks();
                ResetFields();

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                    LoadDealDurationVendorSettings();

                GlobalDBMethods.FixUserOpenDealsStatus(vSession.User, session);

                //if (vSession.User.CompanyType == Types.Vendors.ToString())
                //    GlobalMethods.ShowToolTipMessageControl(InfoMessageControl, "Need customization?", MessageTypes.Info, true, true, false, true, "If you need to add/remove fields to the form below, please contact us");
            }
        }

        private void GetCollaborationUsers()
        {
            List<ElioUsers> users = SqlCollaboration.GetCollaborationUsersByUserType(vSession.User, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (users.Count > 0)
            {
                divVendorsList.Visible = true;
                
                DrpPartners.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select Reseller" : "Select Vendor";

                DrpPartners.Items.Add(item);

                foreach (ElioUsers user in users)
                {
                    item = new ListItem();
                    item.Value = user.Id.ToString();
                    item.Text = user.CompanyName;

                    DrpPartners.Items.Add(item);
                }

                if (users.Count == 1)
                {
                    DrpPartners.Items.FindByValue(users[0].Id.ToString()).Selected = true;
                    DrpPartners.SelectedItem.Value = users[0].Id.ToString();
                    DrpPartners.SelectedItem.Text = users[0].CompanyName;

                    //DrpPartners.Enabled = false;
                }
            }
            else
            {
                divVendorsList.Visible = false;
                BtnAddNewDeal.Visible = false;
            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }
            //}
            //else
            //{
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            LblSelectPlan.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "You have to select a Vendor to see or add deal registration to" : "Select a company to view its deals";
            LblPendingDeals.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "Pending Deals" : "New Deals";

            LblVendorDurationSettingHelp.Text = "Default month duration for all of your deals";
            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Deal Registration";
            //LblDashSubTitle.Text = "";

            //LblPendingNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealActivityStatus.NotConfirmed.ToString() + " Deals, from this partner!" : "You have no " + DealActivityStatus.NotConfirmed.ToString() + " Deals!";
            //LblOpenNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Open.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Open.ToString() + " Deals!";
            //LblClosedNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Closed.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Closed.ToString() + " Deals!";
        }

        private void ResetFields()
        {
            RtbxCompanyNameOpen.Text = string.Empty;
            RtbxCompanyNamePast.Text = string.Empty;

            divDealSuccessBottom.Visible = false;
            divDealErrorBottom.Visible = false;
            TbxVendorDurationSetting.Visible = true;
            DrpVendorDurationSetting.Visible = false;
            BtnVendorDurationSetting.Text = "Edit";
            BtnCancelVendorDurationSetting.Visible = false;
            //spanInputGroup.Attributes["style"] = "float:left;";

            UcOpenMessage.Visible = UcPendingMessage.Visible = UcPastMessage.Visible = false;
        }

        private void LoadDealResultStatus()
        {
            DdlDealResultPast.Items.Clear();
            DdlDealResultOpen.Items.Clear();
            
            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "Deal's stages";
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);
            
            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Pending).ToString();
            item.Text = RandstadDealResultStatus.Pending.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Qualified).ToString();
            item.Text = RandstadDealResultStatus.Qualified.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Solution_Developed).ToString();
            item.Text = RandstadDealResultStatus.Solution_Developed.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Proposal_Presented).ToString();
            item.Text = RandstadDealResultStatus.Proposal_Presented.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Negotiation_and_Close).ToString();
            item.Text = RandstadDealResultStatus.Negotiation_and_Close.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Awarded_Contract_Pending).ToString();
            item.Text = RandstadDealResultStatus.Awarded_Contract_Pending.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Won).ToString();
            item.Text = RandstadDealResultStatus.Won.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);
            
            item = new ListItem();
            item.Value = Convert.ToInt32(RandstadDealResultStatus.Lost).ToString();
            item.Text = RandstadDealResultStatus.Lost.ToString();
            DdlDealResultPast.Items.Add(item);
            DdlDealResultOpen.Items.Add(item);
        }

        private void LoadDealMonthDuration()
        {
            DrpVendorDurationSetting.Items.Clear();

            ListItem vItem = new ListItem();

            vItem.Value = "0";
            vItem.Text = "-- Default deal duration --";

            DrpVendorDurationSetting.Items.Add(vItem);

            for (int i = 1; i <= 12; i++)
            {
                vItem = new ListItem();

                vItem.Value = i.ToString();
                vItem.Text = (i == 1) ? i.ToString() + " month" : i.ToString() + " months";
                DrpVendorDurationSetting.Items.Add(vItem);
            }
        }

        private void LoadDealDurationVendorSettings()
        {
            DrpVendorDurationSetting.Items.Clear();

            ListItem vItem = new ListItem();

            vItem.Value = "0";
            vItem.Text = "-- select default deal month duration --";

            DrpVendorDurationSetting.Items.Add(vItem);

            for (int i = 1; i <= 12; i++)
            {
                vItem = new ListItem();

                vItem.Value = i.ToString();
                vItem.Text = (i == 1) ? i.ToString() + " month" : i.ToString() + " months";
                DrpVendorDurationSetting.Items.Add(vItem);
            }

            ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(vSession.User.Id, session);

            if (monthSettings != null)
            {
                DrpVendorDurationSetting.SelectedItem.Value = monthSettings.DealDurationSetting.ToString();
                DrpVendorDurationSetting.SelectedItem.Text = (monthSettings.DealDurationSetting == 1) ? monthSettings.DealDurationSetting.ToString() + " month" : monthSettings.DealDurationSetting.ToString() + " months";
                //DrpVendorDurationSetting.FindItemByValue(monthSettings.DealDurationSetting.ToString()).Selected = true;
                TbxVendorDurationSetting.Text = DrpVendorDurationSetting.SelectedItem.Text;
            }
            else
            {
                TbxVendorDurationSetting.Text = "-- select default deal month duration --";
            }
        }

        private void SaveDealMonthDurationSetting()
        {
            if (DrpVendorDurationSetting.SelectedItem.Value != "0")
            {
                if (!Sql.HasDealMonthDurationSettings(vSession.User.Id, session))
                {
                    Sql.InsertUserDealMonthDuration(vSession.User.Id, Convert.ToInt32(DrpVendorDurationSetting.SelectedItem.Value), session);
                    LblDealSuccContBottom.Text = "You just set your default deals duration settings successfully";
                }
                else
                {
                    Sql.UpdateUserDealMonthDurationSettings(vSession.User.Id, Convert.ToInt32(DrpVendorDurationSetting.SelectedItem.Value), session);
                    LblDealSuccContBottom.Text = "You just updated your default deals duration settings successfully";
                }

                divDealSuccessBottom.Visible = true;
            }
            else
            {
                Sql.DeleteUserDealMonthDuration(vSession.User.Id, session);

                divDealErrorBottom.Visible = true;
                LblDealErrorContBottom.Text = "You just deleted your default deals duration settings";
            }
        }

        public void BindPending()
        {
            try
            {
                session.OpenConnection();

                int vendorId = -1;
                int resellerId = -1;

                if (DrpPartners.SelectedItem != null && DrpPartners.SelectedItem.Value != "0")
                {
                    vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                    resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                    CollaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                }
                else
                    CollaborationId = -1;

                List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByActiveStatus(vSession.User, (int)DealActivityStatus.NotConfirmed, CollaborationId, TbxPending.Text, session);

                if (deals.Count > 0)
                {
                    RdgDealsPending.Visible = true;
                    UcPendingMessage.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("collaboration_vendor_reseller_id");
                    table.Columns.Add("vendor_id");
                    table.Columns.Add("reseller_id");
                    table.Columns.Add("partner_name");
                    table.Columns.Add("company_name");
                    table.Columns.Add("partner_location");
                    table.Columns.Add("email");
                    table.Columns.Add("website");
                    table.Columns.Add("sysdate");
                    table.Columns.Add("status");
                    table.Columns.Add("deal_result");
                    table.Columns.Add("is_active");
                    table.Columns.Add("is_new");

                    foreach (ElioRegistrationDealsIJUsers deal in deals)
                    {
                        //ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //if (partner != null)
                        table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, deal.PartnerName, deal.CompanyName, deal.PartnerLocation, deal.Email, deal.Website, deal.CreatedDate.ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                        //else
                        //    table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, "", deal.CompanyName, "", deal.Email, deal.Website, deal.CreatedDate.ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                    }

                    RdgDealsPending.DataSource = table;
                    RdgDealsPending.DataBind();
                }
                else
                {
                    RdgDealsPending.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcPendingMessage, "There are no Deals", MessageTypes.Info, true, true, false, false, false);
                    //divPendingNoResults.Visible = true;
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

        public void BindPast()
        {
            try
            {
                session.OpenConnection();

                int vendorId = -1;
                int resellerId = -1;

                if (DrpPartners.SelectedItem != null && DrpPartners.SelectedItem.Value != "0")
                {
                    vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                    resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                    CollaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                }
                else
                    CollaborationId = -1;

                string statusList = "";
                string activityList = "";

                if (AdvancedSerch == "")
                {
                    statusList = "'" + ((int)DealStatus.Closed + "','" + (int)DealStatus.Expired).ToString() + "'";
                    activityList = "'" + ((int)DealActivityStatus.Approved + "','" + (int)DealActivityStatus.Rejected).ToString() + "'";
                }
                else
                {
                    if (AdvancedSerch == ((int)DealActivityStatus.Rejected).ToString())
                    {
                        statusList = "'" + (int)DealStatus.Closed + "'";
                        activityList = "'" + (int)DealActivityStatus.Rejected + "'";
                    }
                    else
                    {
                        statusList = "'" + ((int)DealStatus.Closed + "','" + (int)DealStatus.Expired).ToString() + "'";
                        activityList = "'" + ((int)DealActivityStatus.Approved + "','" + (int)DealActivityStatus.Rejected).ToString() + "'";
                    }
                }

                List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByStatusAndActivityList(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, statusList, activityList, AdvancedSerch, CollaborationId, RtbxCompanyNamePast.Text, session);

                if (deals.Count > 0)
                {
                    RdgPastDeals.Visible = true;
                    UcPastMessage.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("collaboration_vendor_reseller_id");
                    table.Columns.Add("vendor_id");
                    table.Columns.Add("reseller_id");
                    table.Columns.Add("partner_name");
                    table.Columns.Add("company_name");
                    table.Columns.Add("partner_location");
                    table.Columns.Add("email");
                    table.Columns.Add("website");
                    table.Columns.Add("month_duration");
                    table.Columns.Add("status");
                    table.Columns.Add("deal_result");
                    table.Columns.Add("is_active");

                    foreach (ElioRegistrationDealsIJUsers deal in deals)
                    {
                        //ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //if (partner != null)
                        table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, deal.PartnerName, deal.CompanyName, deal.PartnerLocation, deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive);
                        //else
                        //    table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, "", deal.CompanyName, "", deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive);
                    }

                    RdgPastDeals.DataSource = table;
                    RdgPastDeals.DataBind();
                }
                else
                {
                    RdgPastDeals.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcPastMessage, "There are no Deals", MessageTypes.Info, true, true, false, false, false);
                    //divClosedNoResults.Visible = true;
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

        public void BindOpen()
        {
            try
            {
                session.OpenConnection();

                int vendorId = -1;
                int resellerId = -1;
                UcOpenMessage.Visible = false;

                if (DrpPartners.SelectedItem != null && DrpPartners.SelectedItem.Value != "0")
                {
                    vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                    resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                    CollaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                }
                else
                    CollaborationId = -1;

                List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByStatusAndActivity(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Open, (int)DealActivityStatus.Approved, AdvancedSerch, CollaborationId, RtbxCompanyNameOpen.Text, session);

                if (deals.Count > 0)
                {
                    RdgDealsOpen.Visible = true;
                    //divOpenNoResults.Visible = false;
                    //divSearch.Visible = true;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("collaboration_vendor_reseller_id");
                    table.Columns.Add("vendor_id");
                    table.Columns.Add("reseller_id");
                    table.Columns.Add("partner_name");
                    table.Columns.Add("company_name");
                    table.Columns.Add("partner_location");
                    table.Columns.Add("email");
                    table.Columns.Add("website");
                    table.Columns.Add("month_duration");
                    table.Columns.Add("status");
                    table.Columns.Add("deal_result");
                    table.Columns.Add("is_active");
                    table.Columns.Add("is_new");

                    foreach (ElioRegistrationDealsIJUsers deal in deals)
                    {
                        //ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //if (partner != null)
                        table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, deal.PartnerName, deal.CompanyName, deal.PartnerLocation, deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                        //else
                        //    table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, "", deal.CompanyName, "", deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                    }

                    RdgDealsOpen.DataSource = table;
                    RdgDealsOpen.DataBind();
                }
                else
                {
                    RdgDealsOpen.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcOpenMessage, "There are no Deals", MessageTypes.Info, true, true, false, false, false);
                    //divOpenNoResults.Visible = true;
                    //divSearch.Visible = false;
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

        private void SetDealLinkUrl(RepeaterItem item)
        {
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;

                string url = "";

                string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(row["id"].ToString()), session);
                Session[sessionId] = row["id"].ToString();

                url = ControlLoader.Dashboard(vSession.User, "deals-view") + "?dealViewID=" + sessionId;
                //url = ControlLoader.Dashboard(vSession.User, ControlLoader.DealsView(Convert.ToInt32(row["vendor_id"].ToString()))) + "?dealViewID=" + sessionId;

                HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                aEdit.HRef = url;
            }
        }

        #endregion

        #region Grids

        protected void RdgDealsOpen_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (vSession.User != null)
                        {
                            HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                            aDelete.Visible = row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString();

                            Label lblClientName = (Label)ControlFinder.FindControlRecursive(item, "LblClientName");
                            lblClientName.Text = row["company_name"].ToString();

                            ElioUsers company = null;

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                                company = Sql.GetUserById(Convert.ToInt32(row["reseller_id"].ToString()), session);
                            else
                                company = Sql.GetUserById(Convert.ToInt32(row["vendor_id"].ToString()), session);

                            if (company != null)
                            {
                                Image ImgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                                ImgLogo.ImageUrl = company.CompanyLogo;
                                ImgLogo.AlternateText = company.CompanyName + " logo";

                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                                Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                                Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                                lblWebsite.Text = aWebsite.HRef = row["website"].ToString();     //company.WebSite;
                                aWebsite.Target = "_blank";

                                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                                aCompanyName.Target = "_blank";
                                lblCompanyNameContent.Text = company.CompanyName;

                                Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                                lblCountry.Text = company.Country;

                                HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                                HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");
                                HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew");

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    if (Convert.ToInt32(hdnIsNew.Value) == 1 && row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                                    {
                                        divNotification.Visible = true;
                                    }
                                    else
                                        divNotification.Visible = false;
                                }
                                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    divClientNotification.Visible = Convert.ToInt32(hdnIsNew.Value) == 1;
                                }
                                else
                                    divNotification.Visible = divClientNotification.Visible = false;

                                HtmlGenericControl spanNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationMsg");
                                HtmlGenericControl spanClientNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanClientNotificationMsg");
                                HtmlGenericControl spanActiveStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanActiveStatus");

                                if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                                {
                                    spanNotificationMsg.Attributes["title"] = spanClientNotificationMsg.Attributes["title"] = "new approved deal";

                                    lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Rejected.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-warning label-inline";
                                }
                                else
                                {
                                    lblActiveStatus.Text = "Not Confirmed";
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                Label lblResultStatus = (Label)ControlFinder.FindControlRecursive(item, "LblResultStatus");
                                HtmlGenericControl spanResultStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanResultStatus");

                                if (row["deal_result"].ToString() == DealResultStatus.Won.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Won.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Lost.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Lost.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-danger label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Pending.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Pending.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                SetDealLinkUrl(item);

                                int commentsCount = Sql.GetNewCommentsCountByDealId(Convert.ToInt32(row["id"]), session);
                                if (commentsCount > 0)
                                {
                                    HtmlGenericControl divCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCommentNotification");
                                    Label lblCommentNotificationCount = (Label)ControlFinder.FindControlRecursive(item, "LblCommentNotificationCount");
                                    HtmlGenericControl spanCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCommentNotification");

                                    divCommentNotification.Visible = true;
                                    lblCommentNotificationCount.Text = commentsCount.ToString();
                                    spanCommentNotification.Attributes["title"] = commentsCount == 1 ? "New unread comment" : "New unread comments";
                                }

                                ////string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                                ////Session[sessionId] = item["id"].Text.ToString();

                                ////HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                                ////aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit") + "?dealViewID=" + sessionId;

                                ////HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
                                ////aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                                ////aMoreDetails.Target = "_blank";

                                ////Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                                ////lblMoreDetails.Text = "more details";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgDealsOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    BindOpen();
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

        protected void RdgDealsPending_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        HtmlControl tdbCountry = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbCountry");
                        HtmlControl tdbClientEmail = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbClientEmail");
                        HtmlControl tdbWebsite = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbWebsite");
                        HtmlControl tdbActiveStatus = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbActiveStatus");
                        HtmlControl tdbResultStatus = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbResultStatus");

                        if (tdbCountry != null && tdbClientEmail != null && tdbWebsite != null && tdbActiveStatus != null && tdbResultStatus != null)
                            tdbCountry.Visible = tdbClientEmail.Visible = tdbWebsite.Visible = tdbActiveStatus.Visible = tdbResultStatus.Visible = ShowGridColumns;

                        if (vSession.User != null)
                        {
                            HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                            aDelete.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers);

                            Label lblClientName = (Label)ControlFinder.FindControlRecursive(item, "LblClientName");
                            lblClientName.Text = row["company_name"].ToString();

                            ElioUsers company = null;

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                                company = Sql.GetUserById(Convert.ToInt32(row["reseller_id"].ToString()), session);
                            else
                                company = Sql.GetUserById(Convert.ToInt32(row["vendor_id"].ToString()), session);

                            if (company != null)
                            {
                                Image ImgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                                ImgLogo.ImageUrl = company.CompanyLogo;
                                ImgLogo.AlternateText = company.CompanyName + " logo";

                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                                Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");

                                Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                                lblCountry.Text = company.Country;

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                                lblWebsite.Text = aWebsite.HRef = row["website"].ToString();  //company.WebSite;
                                aWebsite.Target = "_blank";

                                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                                aCompanyName.Target = "_blank";
                                lblCompanyNameContent.Text = company.CompanyName;

                                HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                                HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");
                                HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew");

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    if (Convert.ToInt32(hdnIsNew.Value) == 1 && row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                                    {
                                        divNotification.Visible = true;
                                    }
                                    else
                                        divNotification.Visible = false;
                                }
                                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    divClientNotification.Visible = Convert.ToInt32(hdnIsNew.Value) == 1;
                                    divNotification.Visible = false;
                                }
                                else
                                    divNotification.Visible = divClientNotification.Visible = false;

                                Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                                if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary green";
                                }
                                else if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Rejected.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary red";
                                }
                                else
                                {
                                    lblActiveStatus.Text = "Not Confirmed";
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary yellow";
                                }

                                Label lblResultStatus = (Label)ControlFinder.FindControlRecursive(item, "LblResultStatus");

                                if (row["deal_result"].ToString() == DealResultStatus.Won.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Won.ToString();
                                    lblResultStatus.CssClass = "btn default green-stripe";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Lost.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Lost.ToString();
                                    lblResultStatus.CssClass = "btn default red-stripe";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Pending.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Pending.ToString();
                                    lblResultStatus.CssClass = "btn default yellow-stripe";
                                }

                                SetDealLinkUrl(item);

                                int commentsCount = Sql.GetNewCommentsCountByDealId(Convert.ToInt32(row["id"]), session);
                                if (commentsCount > 0)
                                {
                                    HtmlGenericControl divCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCommentNotification");
                                    Label lblCommentNotificationCount = (Label)ControlFinder.FindControlRecursive(item, "LblCommentNotificationCount");
                                    HtmlGenericControl spanCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCommentNotification");

                                    divCommentNotification.Visible = true;
                                    lblCommentNotificationCount.Text = commentsCount.ToString();
                                    spanCommentNotification.Attributes["title"] = commentsCount == 1 ? "New unread comment" : "New unread comments";
                                }

                                ////string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                                ////Session[sessionId] = item["id"].Text.ToString();

                                ////HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                                ////aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit") + "?dealViewID=" + sessionId;

                                ////HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
                                ////aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                                ////aMoreDetails.Target = "_blank";

                                ////Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                                ////lblMoreDetails.Text = "more details";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgDealsPending_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    BindPending();
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

        protected void RdgPastDeals_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (vSession.User != null)
                        {
                            ElioUsers company = null;

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                                company = Sql.GetUserById(Convert.ToInt32(row["reseller_id"].ToString()), session);
                            else
                                company = Sql.GetUserById(Convert.ToInt32(row["vendor_id"].ToString()), session);

                            if (company != null)
                            {
                                //HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                                //aDelete.Visible = row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString();

                                Image ImgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                                ImgLogo.ImageUrl = company.CompanyLogo;
                                ImgLogo.AlternateText = company.CompanyName + " logo";

                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                                Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                                lblWebsite.Text = aWebsite.HRef = row["website"].ToString();     //company.WebSite;
                                aWebsite.Target = "_blank";

                                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                                aCompanyName.Target = "_blank";
                                lblCompanyNameContent.Text = company.CompanyName;

                                Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                                lblCountry.Text = company.Country;

                                Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");
                                HtmlGenericControl spanActiveStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanActiveStatus");

                                if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Rejected.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-warning label-inline";
                                }
                                else
                                {
                                    lblActiveStatus.Text = "Not Confirmed";
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                Label lblResultStatus = (Label)ControlFinder.FindControlRecursive(item, "LblResultStatus");
                                HtmlGenericControl spanResultStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanResultStatus");

                                if (row["deal_result"].ToString() == DealResultStatus.Won.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Won.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Lost.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Lost.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-danger label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Pending.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Pending.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                SetDealLinkUrl(item);

                                int commentsCount = Sql.GetNewCommentsCountByDealId(Convert.ToInt32(row["id"]), session);
                                if (commentsCount > 0)
                                {
                                    HtmlGenericControl divCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCommentNotification");
                                    Label lblCommentNotificationCount = (Label)ControlFinder.FindControlRecursive(item, "LblCommentNotificationCount");
                                    HtmlGenericControl spanCommentNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCommentNotification");

                                    divCommentNotification.Visible = true;
                                    lblCommentNotificationCount.Text = commentsCount.ToString();
                                    spanCommentNotification.Attributes["title"] = commentsCount == 1 ? "New unread comment" : "New unread comments";
                                }

                                ////string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                                ////Session[sessionId] = item["id"].Text.ToString();

                                ////HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                                ////aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit") + "?dealViewID=" + sessionId;

                                ////HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
                                ////aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                                ////aMoreDetails.Target = "_blank";

                                ////Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                                ////lblMoreDetails.Text = "more details";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgPastDeals_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    BindPast();
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

        #region Buttons

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = false;

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    if (imgBtn != null)
                    {
                        RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                        if (item != null)
                        {
                            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                            if (hdnId != null)
                            {
                                TbxId.Value = hdnId.Value;

                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                            }
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = false;

                    int dealId = Convert.ToInt32(TbxId.Value);
                    ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);
                    if (deal != null)
                    {
                        try
                        {
                            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                            deal.IsActive = (int)DealActivityStatus.Deleted;
                            deal.LastUpdate = DateTime.Now;

                            loader.Update(deal);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            throw ex;
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                        if (tab_1_1.Visible)
                        {
                            BindPending();
                            GlobalMethods.ShowMessageControlDA(UcPendingMessage, "Deal was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
                        }
                        else if (tab_1_2.Visible)
                        {
                            BindOpen();
                            GlobalMethods.ShowMessageControlDA(UcOpenMessage, "Deal was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
                        }
                        else
                        {
                            BindPast();
                            GlobalMethods.ShowMessageControlDA(UcPastMessage, "Deal was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
                        }

                        TbxId.Value = "0";

                        UpdatePanelContent.Update();
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcPopUpConfirmationMessageAlert, "Deal could not be deleted.", MessageTypes.Error, true, true, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcPopUpConfirmationMessageAlert, "Deal could not be deleted.", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnAddNewDeal_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    //divDealsInfo.Visible = true;
                    //AllowEdit(true);
                    //ResetFields();
                    //LoadDealStatus();
                    //LoadDealResultStatus();
                    //LoadDealActiveStatus();
                    //LoadDealMonthDuration();

                    string sessionId = "";

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, vSession.User.Id, session);
                        Session[sessionId] = vSession.User.Id.ToString();
                    }
                    else
                    {
                        sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, GlobalMethods.GetRandstadCustomerID(), session);
                        Session[sessionId] = GlobalMethods.GetRandstadCustomerID().ToString();
                    }

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals-add-edit") + "?dealVendorViewID=" + sessionId, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchPending_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    BindPending();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (DdlDealResultOpen.SelectedItem.Value != "0")
                        AdvancedSerch = DdlDealResultOpen.SelectedItem.Text;
                    else
                        AdvancedSerch = "";

                    BindOpen();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchPast_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (DdlDealResultPast.SelectedItem.Value != "0")
                    {
                        if (DdlDealResultPast.SelectedItem.Text != DealActivityStatus.Rejected.ToString())
                            AdvancedSerch = DdlDealResultPast.SelectedItem.Text;
                        else if (DdlDealResultPast.SelectedItem.Text == DealActivityStatus.Rejected.ToString())
                            AdvancedSerch = DdlDealResultPast.SelectedItem.Value;
                    }
                    else
                        AdvancedSerch = "";

                    BindPast();                    
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnVendorDurationSetting_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    divDealSuccessBottom.Visible = false;
                    divDealErrorBottom.Visible = false;

                    if (TbxVendorDurationSetting.Visible && BtnVendorDurationSetting.Text == "Edit")
                    {
                        TbxVendorDurationSetting.Visible = false;
                        DrpVendorDurationSetting.Visible = true;
                        BtnVendorDurationSetting.Text = "Save";
                        BtnCancelVendorDurationSetting.Visible = true;

                        //spanInputGroup.Attributes["style"] = "";
                    }
                    else
                    {
                        try
                        {
                            session.OpenConnection();

                            SaveDealMonthDurationSetting();

                            LoadDealDurationVendorSettings();

                            TbxVendorDurationSetting.Visible = true;
                            DrpVendorDurationSetting.Visible = false;
                            BtnVendorDurationSetting.Text = "Edit";
                            BtnCancelVendorDurationSetting.Visible = false;
                            //spanInputGroup.Attributes["style"] = "float:left;";
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
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancelVendorDurationSetting_Click(object sender, EventArgs e)
        {
            try
            {
                divDealSuccessBottom.Visible = false;
                divDealErrorBottom.Visible = false;

                TbxVendorDurationSetting.Visible = true;
                DrpVendorDurationSetting.Visible = false;
                BtnVendorDurationSetting.Text = "Edit";
                BtnCancelVendorDurationSetting.Visible = false;
                //spanInputGroup.Attributes["style"] = "float:left;";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Dropdown Lists

        protected void DrpPartners_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetFields();
                LoadDealDurationVendorSettings();
                
                //BtnAddNewDeal.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();

                BindOpen();
                BindPending();
                BindPast();
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

        protected void DrpVendorDurationSetting_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            try
            {
                session.OpenConnection();

                divDealSuccessBottom.Visible = false;
                divDealErrorBottom.Visible = false;

                SaveDealMonthDurationSetting();
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

        #region Tabs

        protected void aPendingDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aPendingDeals.Attributes["class"] = "nav-link active";
                tab_1_1.Attributes["class"] = "tab-pane fade show active";
                tab_1_1.Visible = true;
                BindPending();

                aOpenDeals.Attributes["class"] = aClosedDeals.Attributes["class"] = "nav-link";
                tab_1_2.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                tab_1_2.Visible = tab_1_3.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aOpenDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aOpenDeals.Attributes["class"] = "nav-link active";
                tab_1_2.Attributes["class"] = "tab-pane fade show active";
                tab_1_2.Visible = true;
                BindOpen();

                aPendingDeals.Attributes["class"] = aClosedDeals.Attributes["class"] = "nav-link";
                tab_1_1.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                tab_1_1.Visible = tab_1_3.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aClosedDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aClosedDeals.Attributes["class"] = "nav-link active";
                tab_1_3.Attributes["class"] = "tab-pane fade show active";
                tab_1_3.Visible = true;
                BindPast();

                aPendingDeals.Attributes["class"] = aOpenDeals.Attributes["class"] = "nav-link";
                tab_1_1.Attributes["class"] = tab_1_2.Attributes["class"] = "tab-pane fade";
                tab_1_1.Visible = tab_1_2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}