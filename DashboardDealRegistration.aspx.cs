using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus
{
    public partial class DashboardDealRegistration : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(BtnAddNewDeal);

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

        #region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                divVendorSettings.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                BtnAddNewDeal.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();

                GetCollaborationUsers();
                LoadDealResultStatusOpen();
                LoadDealResultStatusPast();

                RdgDealsOpen.MasterTableView.GetColumn("month_duration").Display = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();

                RdgDealsPending.MasterTableView.GetColumn("partner_name").Display = vSession.User.CompanyType == Types.Vendors.ToString();
                RdgDealsOpen.MasterTableView.GetColumn("partner_name").Display = vSession.User.CompanyType == Types.Vendors.ToString();
                RdgPastDeals.MasterTableView.GetColumn("partner_name").Display = vSession.User.CompanyType == Types.Vendors.ToString();

                RdgDealsPending.MasterTableView.GetColumn("partner_location").Display = vSession.User.CompanyType == Types.Vendors.ToString();
                RdgDealsOpen.MasterTableView.GetColumn("partner_location").Display = vSession.User.CompanyType == Types.Vendors.ToString();
                RdgPastDeals.MasterTableView.GetColumn("partner_location").Display = vSession.User.CompanyType == Types.Vendors.ToString();

                UpdateStrings();
                SetLinks();
                ResetFields();

                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = true;
                    LblRenewalHead.Text = "Renewal date: ";

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
                }

                aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                    LoadDealDurationVendorSettings();

                GlobalDBMethods.FixUserOpenDealsStatus(vSession.User, session);

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                    GlobalMethods.ShowToolTipMessageControlDA(InfoMessageControl, "Need customization?", MessageTypes.Info, true, true, false, true, "If you need to add/remove fields to the form below, please contact us");
            }
        }

        private void GetCollaborationUsers()
        {
            List<ElioUsers> users = SqlCollaboration.GetCollaborationUsersByUserType(vSession.User, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (users.Count > 0)
            {
                divDealRegistrationInvitationToPartners.Visible = false;
                divVendorsList.Visible = true;

                DrpPartners.Items.Clear();

                DropDownListItem item = new DropDownListItem();
                item.Value = "0";
                item.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select Reseller" : "Select Vendor";

                DrpPartners.Items.Add(item);

                foreach (ElioUsers user in users)
                {
                    item = new DropDownListItem();
                    item.Value = user.Id.ToString();
                    item.Text = user.CompanyName;

                    DrpPartners.Items.Add(item);
                }
            }
            else
            {
                divDealRegistrationInvitationToPartners.Visible = true;
                divVendorsList.Visible = false;

                LblDealRegistrationTitle.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "You do not have any Vendors yet. Click the button below to Invite them." : "You do not have any Resellers as partners to collaborate with yet. Click the button below to Invite them.";
                aInvitationToPartners.HRef = ControlLoader.Dashboard(vSession.User, "partner-directory-invitations");   //collaboration-new-partners
                divInvitationToPartners.Visible = true;
                divDealsResults.Visible = false;
            }
        }

        private void SetLinks()
        {
            aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                if (packet != null)
                {
                    LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                }
            }
            else
            {
                LblPricingPlan.Text = "You are currently on a free plan";
            }

            LblElioplusDashboard.Text = "";

            LblDashboard.Text = "Dashboard";

            aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            if (aBtnGoPremium.Visible)
            {
                LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
                LblPricingPlan.Visible = false;
            }

            LblSelectPlan.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "You have to select a Vendor to see or add deal registration to" : "You have to select a Reseller in order to see his deal registrations";
            LblPendingDeals.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "Pending Deals" : "New Deals";

            LblVendorDurationSettingHelp.Text = "Default month duration for all of your deals";
            LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Deal Registration";
            LblDashSubTitle.Text = "";

            LblPendingNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealActivityStatus.NotConfirmed.ToString() + " Deals, from this partner!" : "You have no " + DealActivityStatus.NotConfirmed.ToString() + " Deals!";
            LblOpenNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Open.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Open.ToString() + " Deals!";
            LblClosedNoResultsContent.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Closed.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Closed.ToString() + " Deals!";
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
            spanInputGroup.Attributes["style"] = "float:left;";
        }

        private void LoadDealResultStatusPast()
        {
            DdlDealResultPast.Items.Clear();

            DropDownListItem item = new DropDownListItem();

            item.Value = "0";
            item.Text = "Deal's result";
            DdlDealResultPast.Items.Add(item);

            item = new DropDownListItem();

            item.Value = Convert.ToInt32(DealResultStatus.Pending).ToString();
            item.Text = DealResultStatus.Pending.ToString();
            DdlDealResultPast.Items.Add(item);

            item = new DropDownListItem();

            item.Value = Convert.ToInt32(DealResultStatus.Won).ToString();
            item.Text = DealResultStatus.Won.ToString();
            DdlDealResultPast.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            item.Text = DealResultStatus.Lost.ToString();
            DdlDealResultPast.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(DealActivityStatus.Rejected).ToString();
            item.Text = DealActivityStatus.Rejected.ToString();
            DdlDealResultPast.Items.Add(item);
        }

        private void LoadDealResultStatusOpen()
        {
            DdlDealResultOpen.Items.Clear();

            DropDownListItem item = new DropDownListItem();

            item.Value = "0";
            item.Text = "Deal's result";
            DdlDealResultOpen.Items.Add(item);

            item = new DropDownListItem();

            item.Value = Convert.ToInt32(DealResultStatus.Pending).ToString();
            item.Text = DealResultStatus.Pending.ToString();
            DdlDealResultOpen.Items.Add(item);

            item = new DropDownListItem();

            item.Value = Convert.ToInt32(DealResultStatus.Won).ToString();
            item.Text = DealResultStatus.Won.ToString();
            DdlDealResultOpen.Items.Add(item);

            item = new DropDownListItem();
            item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            item.Text = DealResultStatus.Lost.ToString();
            DdlDealResultOpen.Items.Add(item);
        }

        private void LoadDealMonthDuration()
        {
            DrpVendorDurationSetting.Items.Clear();

            DropDownListItem vItem = new DropDownListItem();

            vItem.Value = "0";
            vItem.Text = "-- Default deal duration --";

            DrpVendorDurationSetting.Items.Add(vItem);

            for (int i = 1; i <= 12; i++)
            {
                vItem = new DropDownListItem();

                vItem.Value = i.ToString();
                vItem.Text = (i == 1) ? i.ToString() + " month" : i.ToString() + " months";
                DrpVendorDurationSetting.Items.Add(vItem);
            }
        }

        private void LoadDealDurationVendorSettings()
        {
            DrpVendorDurationSetting.Items.Clear();

            DropDownListItem vItem = new DropDownListItem();

            vItem.Value = "0";
            vItem.Text = "-- select default deal month duration --";

            DrpVendorDurationSetting.Items.Add(vItem);

            for (int i = 1; i <= 12; i++)
            {
                vItem = new DropDownListItem();

                vItem.Value = i.ToString();
                vItem.Text = (i == 1) ? i.ToString() + " month" : i.ToString() + " months";
                DrpVendorDurationSetting.Items.Add(vItem);
            }

            ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(vSession.User.Id, session);

            if (monthSettings != null)
            {
                DrpVendorDurationSetting.FindItemByValue(monthSettings.DealDurationSetting.ToString()).Selected = true;
                TbxVendorDurationSetting.Text = DrpVendorDurationSetting.SelectedItem.Text;
            }
            else
                TbxVendorDurationSetting.Text = "select default deal month duration";
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

        private void SetDealLinkUrl(GridDataItem item, ElioUsers companyDeal)
        {
            string url = "";

            string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
            Session[sessionId] = item["id"].Text.ToString();

            url = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;

            HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
            aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;
        }

        # endregion

        #region Grids

        protected void RdgDealsPending_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblClientName = (Label)ControlFinder.FindControlRecursive(item, "LblClientName");
                    lblClientName.Text = item["company_name"].Text;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["reseller_id"].Text), session);
                    if (company != null)
                    {
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");

                        HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                        Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                        lblWebsite.Text = aWebsite.HRef = item["website"].Text;  //company.WebSite;
                        aWebsite.Target = "_blank";

                        aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                        aCompanyName.Target = "_blank";
                        lblCompanyNameContent.Text = company.CompanyName;

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (Convert.ToInt32(item["is_new"].Text) == 1 && item["is_active"].Text == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                            {
                                divNotification.Visible = true;
                            }
                            else
                                divNotification.Visible = false;
                        }
                        else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                        {
                            divNotification.Visible = divClientNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1;
                        }
                        else
                            divNotification.Visible = divClientNotification.Visible = false;

                        Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                        if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                        {
                            lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                            lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                            lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary green";
                        }
                        else if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
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

                        if (item["deal_result"].Text == DealResultStatus.Won.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Won.ToString();
                            lblResultStatus.CssClass = "btn default green-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Lost.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Lost.ToString();
                            lblResultStatus.CssClass = "btn default red-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Pending.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Pending.ToString();
                            lblResultStatus.CssClass = "btn default yellow-stripe";
                        }

                        SetDealLinkUrl(item, company);

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
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RdgDealsPending_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int vendorId = -1;
                    int resellerId = -1;
                    int collaborationId = -1;

                    if (DrpPartners.SelectedItem.Value != "0")
                    {
                        vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                        resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                        collaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                    }

                    List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByActiveStatus(vSession.User, (int)DealActivityStatus.NotConfirmed, collaborationId, "", session);

                    if (deals.Count > 0)
                    {
                        RdgDealsPending.Visible = true;
                        divPendingNoResults.Visible = false;

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
                    }
                    else
                    {
                        RdgDealsPending.Visible = false;
                        divPendingNoResults.Visible = true;
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

        protected void RdgDealsOpen_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblClientName = (Label)ControlFinder.FindControlRecursive(item, "LblClientName");
                    lblClientName.Text = item["company_name"].Text;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["reseller_id"].Text), session);

                    if (company != null)
                    {
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                        Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                        HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                        Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                        lblWebsite.Text = aWebsite.HRef = item["website"].Text;     //company.WebSite;
                        aWebsite.Target = "_blank";

                        aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                        aCompanyName.Target = "_blank";
                        lblCompanyNameContent.Text = company.CompanyName;

                        HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                        HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (Convert.ToInt32(item["is_new"].Text) == 1 && item["is_active"].Text == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                            {
                                divNotification.Visible = true;
                            }
                            else
                                divNotification.Visible = false;
                        }
                        else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                        {                            
                            divNotification.Visible = divClientNotification.Visible = Convert.ToInt32(item["is_new"].Text) == 1;
                        }
                        else
                            divNotification.Visible = divClientNotification.Visible = false;

                        HtmlGenericControl spanNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationMsg");
                        HtmlGenericControl spanClientNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanClientNotificationMsg");

                        if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                        {
                            spanNotificationMsg.Attributes["title"] = spanClientNotificationMsg.Attributes["title"] = "new approved deal";

                            lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                            lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                            lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary green";
                        }
                        else if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
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

                        if (item["deal_result"].Text == DealResultStatus.Won.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Won.ToString();
                            lblResultStatus.CssClass = "btn default green-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Lost.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Lost.ToString();
                            lblResultStatus.CssClass = "btn default red-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Pending.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Pending.ToString();
                            lblResultStatus.CssClass = "btn default yellow-stripe";
                        }

                        SetDealLinkUrl(item, company);

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
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RdgDealsOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int vendorId = -1;
                    int resellerId = -1;
                    int collaborationId = -1;

                    if (DrpPartners.SelectedItem.Value != "0")
                    {
                        vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                        resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                        collaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                    }

                    List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByStatusAndActivity(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Open, (int)DealActivityStatus.Approved, AdvancedSerch, collaborationId, RtbxCompanyNameOpen.Text, session);

                    if (deals.Count > 0)
                    {
                        RdgDealsOpen.Visible = true;
                        divOpenNoResults.Visible = false;
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
                    }
                    else
                    {
                        RdgDealsOpen.Visible = false;
                        divOpenNoResults.Visible = true;
                        //divSearch.Visible = false;
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

        protected void RdgPastDeals_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers company = Sql.GetUserById(Convert.ToInt32(item["reseller_id"].Text), session);

                    if (company != null)
                    {
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");

                        HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                        Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                        lblWebsite.Text = aWebsite.HRef = item["website"].Text;     //company.WebSite;
                        aWebsite.Target = "_blank";

                        aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                        aCompanyName.Target = "_blank";
                        lblCompanyNameContent.Text = company.CompanyName;

                        Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                        if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                        {
                            lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                            lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                            lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary green";
                        }
                        else if (item["is_active"].Text == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
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

                        if (item["deal_result"].Text == DealResultStatus.Won.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Won.ToString();
                            lblResultStatus.CssClass = "btn default green-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Lost.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Lost.ToString();
                            lblResultStatus.CssClass = "btn default red-stripe";
                        }
                        else if (item["deal_result"].Text == DealResultStatus.Pending.ToString())
                        {
                            lblResultStatus.Text = DealResultStatus.Pending.ToString();
                            lblResultStatus.CssClass = "btn default yellow-stripe";
                        }

                        SetDealLinkUrl(item, company);

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
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RdgPastDeals_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int vendorId = -1;
                    int resellerId = -1;
                    int collaborationId = -1;

                    if (DrpPartners.SelectedItem.Value != "0")
                    {
                        vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                        resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                        collaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                    }

                    //List<ElioRegistrationDeals> deals = SqlCollaboration.GetUserDealsByStatusAndActivity(vSession.User, (int)DealStatus.Closed, (int)DealActivityStatus.Approved, "", collaborationId, "", session);

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

                    List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByStatusAndActivityList(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, statusList, activityList, AdvancedSerch, collaborationId, RtbxCompanyNamePast.Text, session);
                    
                    if (deals.Count > 0)
                    {
                        RdgPastDeals.Visible = true;
                        divClosedNoResults.Visible = false;

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
                    }
                    else
                    {
                        RdgPastDeals.Visible = false;
                        divClosedNoResults.Visible = true;
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

        #endregion

        #region Buttons

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
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

                    string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(DrpPartners.SelectedItem.Value), session);
                    Session[sessionId] = DrpPartners.SelectedItem.Value;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit") + "?dealVendorViewID=" + sessionId, false);
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

                    RdgDealsPending.Rebind();
                    RdgDealsOpen.Rebind();
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

                    RdgDealsPending.Rebind();
                    RdgDealsOpen.Rebind();
                    RdgPastDeals.Rebind();
                    //RdgDealsClosed.Rebind();
                    //RdgDealsExpired.Rebind();
                    //RdgDealsRejected.Rebind();
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

                        spanInputGroup.Attributes["style"] = "";
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
                            spanInputGroup.Attributes["style"] = "float:left;";
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
                spanInputGroup.Attributes["style"] = "float:left;";
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

                //AllowEdit(DrpPartners.SelectedItem.Value != "0");
                ResetFields();
                LoadDealDurationVendorSettings();

                //divPendingNoResults.Visible = false;
                //divOpenNoResults.Visible = false;
                //divExpiredNoResults.Visible = false;
                //divDealsInfo.Visible = false;

                //if (DrpPartners.SelectedItem.Value == "0")
                //    BtnAddNewDeal.Visible = false;
                //else
                //{
                //    BtnAddNewDeal.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
                //}

                RdgDealsPending.Rebind();
                RdgDealsOpen.Rebind();
                RdgPastDeals.Rebind();
                //RdgDealsClosed.Rebind();
                //RdgDealsExpired.Rebind();
                //RdgDealsRejected.Rebind();
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
                liPendingDeals.Attributes["class"] = "active";
                tab_1_1.Attributes["class"] = "tab-pane active";
                tab_1_1.Visible = true;

                liOpenDeals.Attributes["class"] = "";
                tab_1_2.Attributes["class"] = "tab-pane";
                tab_1_2.Visible = false;

                liClosedDeals.Attributes["class"] = "";
                tab_1_3.Attributes["class"] = "tab-pane";
                tab_1_3.Visible = false;
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
                liOpenDeals.Attributes["class"] = "active";
                tab_1_2.Attributes["class"] = "tab-pane active";
                tab_1_2.Visible = true;

                liPendingDeals.Attributes["class"] = "";
                tab_1_1.Attributes["class"] = "tab-pane";
                tab_1_1.Visible = false;

                liClosedDeals.Attributes["class"] = "";
                tab_1_3.Attributes["class"] = "tab-pane";
                tab_1_3.Visible = false;
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
                liClosedDeals.Attributes["class"] = "active";
                tab_1_3.Attributes["class"] = "tab-pane active";
                tab_1_3.Visible = true;

                liPendingDeals.Attributes["class"] = "";
                tab_1_1.Attributes["class"] = "tab-pane";
                tab_1_1.Visible = false;

                liOpenDeals.Attributes["class"] = "";
                tab_1_2.Attributes["class"] = "tab-pane";
                tab_1_2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}