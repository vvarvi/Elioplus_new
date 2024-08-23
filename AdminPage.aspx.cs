using System;
using System.Linq;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Data;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Libero.FusionCharts;
using WdS.ElioPlus.Lib.Enums;
using System.Web;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using System.Configuration;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using System.Web.UI;
//using WdS.ElioPlus.Lib.Services.StripeAPI;

namespace WdS.ElioPlus
{
    public partial class AdminPage : Telerik.Web.UI.RadAjaxPage
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
                session.OpenConnection();

                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    //ShowChart();

                    if (!IsPostBack)
                    {
                        #region Load Data

                        LoadCompanyTypes();
                        LoadStatus();
                        LoadPublicStatus();
                        LoadBillingType();
                        LoadApplicationType();
                        LoadVendorsCompanies();
                        LoadResellersCompanies();
                        LoadThirdPartyCompanies();
                        LoadDashboardData();
                        LoadRoles();

                        #endregion

                        ResetPacketStatusFields();
                        vSession.SearchQueryString = "";
                        HdnUserId.Value = "0";

                        UcMessageAlert.Visible = false;
                        UcStripeMessageAlert.Visible = false;
                        UcMessageControlCriteria.Visible = false;
                        UcMessageControlAddConnections.Visible = false;

                        UpdateStrings();
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

        private void ResetPacketStatusFields()
        {
            RtbxPacketStatusUserId.Text = string.Empty;
            RtbxPacketStatusConnections.Text = string.Empty;
            RdpStartingDate.SelectedDate = null;
            RdpExpirationDate.SelectedDate = null;
        }

        private void LoadRoles()
        {
            List<ElioRoles> roles = Sql.GetAllRoles(session);

            CbxRoles.Items.Clear();

            foreach (ElioRoles role in roles)
            {
                bool isSelected = false;

                if (RtbxUserToAssignRole.Text != "")
                {
                    isSelected = Sql.HasRoleByDescription(Convert.ToInt32(RtbxUserToAssignRole.Text), role.Description, session);
                }

                ListItem itm = new ListItem();
                itm.Text = role.Description;
                itm.Value = role.Id.ToString();
                itm.Selected = isSelected;

                CbxRoles.Items.Add(itm);
            }
        }

        private void LoadTypes(GridDataItem item)
        {
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            rcbxCategory.Items.Clear();

            List<ElioUserTypes> types = Sql.GetUserAllTypes(session);
            foreach (ElioUserTypes type in types)
            {
                RadComboBoxItem rcbxitem = new RadComboBoxItem();

                rcbxitem.Value = type.Id.ToString();
                rcbxitem.Text = type.Description;

                rcbxCategory.Items.Add(rcbxitem);
            }
        }

        private void ShowChart()
        {
            DataTable dt = Sql.GetCompanyViewsByCompanyIdForChart(vSession.User.Id, session);

            dt.Columns.Add("datetime");
            foreach (DataRow row in dt.Rows)
            {
                row["datetime"] = string.Format("{0:dd/MM}", row["date"]);
            }

            LineChart lchart = new LineChart();
            lchart.Background.BgColor = "ffffff";
            lchart.Background.BgAlpha = 50;
            lchart.ChartTitles.Caption = "";
            lchart.Template = new Libero.FusionCharts.Template.OceanTemplate();
            lchart.DataSource = dt;
            lchart.DataTextField = "datetime";
            lchart.DataValueField = "views";
            lchart.Canvas2D.CanvasBgColor = "f25a23";
            lchart.NumberFormat.DecimalPrecision = 0;

            ////ViewsChart.ShowChart(lchart);
            ////ViewsChart.Visible = true;           
        }

        private void LoadDashboardData()
        {
            LblTotalVendors.Text = Sql.GetTotalRegisteredCompaniesByType(Types.Vendors.ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();

            LblTotalResellers.Text = Sql.GetTotalRegisteredCompaniesByType(EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            LblTotalThirdPartyResellers.Text = Sql.GetTotalRegisteredCompaniesByType(EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.ThirdParty), session).ToString();
            LblTotalDevelopers.Text = Sql.GetTotalRegisteredCompaniesByType(Types.Developers.ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            LblTotalRegistered.Text = Sql.GetTotalCompaniesByAccountStatus(Convert.ToInt32(AccountStatus.Completed), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            LblTotalNotRegistered.Text = Sql.GetTotalCompaniesByAccountStatus(Convert.ToInt32(AccountStatus.NotCompleted), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
        }

        private void UpdateStrings()
        {
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "2")).Text;
            LblVendors.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "38")).Text;
            LblDevelopers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "36")).Text;
            LblTResellers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "60")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "37")).Text;
            LblTThirdPartyResellers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "61")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "37")).Text;
            LblRegistered.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "3")).Text;
            LblNotRegistered.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "4")).Text;

            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "1")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "2")).Text;
            LblStatus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "5")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "6")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "7")).Text;
            LblResellersCompanyName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "25")).Text;
            LblThirdPartyCompanyName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "26")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "9")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "10")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "11")).Text;
            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "1")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "12")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "23")).Text;
            LblAddRole.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "13")).Text;
            LblRoles.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "14")).Text;

            LblPacketStatusUserId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "18")).Text;
            LblPacketStatusConnections.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "19")).Text;
            LblStartingDate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "20")).Text;
            LblExpirationDate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "21")).Text;

            LblConfMsg.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "24")).Text;
            BtnDeleteConfirm.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "button", "1")).Text;
            BtnBack.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "button", "2")).Text;

            RdgElioUsers.MasterTableView.GetColumn("company_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "1")).Text;
            RdgElioUsers.MasterTableView.GetColumn("billing_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "36")).Text;
            RdgElioUsers.MasterTableView.GetColumn("country").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "37")).Text;
            RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "38")).Text;
            RdgElioUsers.MasterTableView.GetColumn("company_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "2")).Text;
            RdgElioUsers.MasterTableView.GetColumn("email").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "3")).Text;
            RdgElioUsers.MasterTableView.GetColumn("is_public").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "4")).Text;
            RdgElioUsers.MasterTableView.GetColumn("account_status").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "5")).Text;
            //RdgElioUsers.MasterTableView.GetColumn("features_no").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "6")).Text;
            RdgElioUsers.MasterTableView.GetColumn("available_connections_count").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "40")).Text;
            RdgElioUsers.MasterTableView.GetColumn("last_login").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "7")).Text;
            RdgElioUsers.MasterTableView.GetColumn("login_times").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "8")).Text;
            //RdgElioUsers.MasterTableView.GetColumn("count").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "9")).Text;
            RdgElioUsers.MasterTableView.GetColumn("sysdate").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "15")).Text;
            //RdgElioUsers.MasterTableView.GetColumn("first_send").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "10")).Text;
            //RdgElioUsers.MasterTableView.GetColumn("last_send").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "11")).Text;
            RdgElioUsers.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "12")).Text;

            //RdgElioUsers.MasterTableView.GetColumn("count").Display = false;
            //RdgElioUsers.MasterTableView.GetColumn("first_send").Display = false;
            //RdgElioUsers.MasterTableView.GetColumn("last_send").Display = false;

            Label lblSearchText = (Label)ControlFinder.FindControlRecursive(RbtnSearch, "LblSearchText");
            lblSearchText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "11")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;

            Label lblAddConnectionsText = (Label)ControlFinder.FindControlRecursive(RbtnAddConnections, "LblAddConnectionsText");
            lblAddConnectionsText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "22")).Text;

            Label lblGetClearbitDataText = (Label)ControlFinder.FindControlRecursive(RbtnGetClearbitData, "LblGetClearbitDataText");
            lblGetClearbitDataText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "25")).Text;
        }

        private bool FixUserBillingTypeStatus(RadTextBox rtbxCustomerId, ElioUsers user)
        {
            bool allow = false;

            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                if (rtbxCustomerId.Text != string.Empty)
                {
                    allow = true;
                }
            }

            return allow;
        }

        private void FixUsersGrid(GridDataItem item, bool isSaveMode, bool isCancelClicked)
        {
            #region Find / Reset Controls

            string alert = string.Empty;

            UcMessageAlert.Visible = isSaveMode;
            UcStripeMessageAlert.Visible = isSaveMode;

            RdgElioUsers.MasterTableView.GetColumn("billing_type").Display = true;
            RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").Display = true;

            Label lblCategory = (Label)ControlFinder.FindControlRecursive(item, "LblCategory");
            Label lblBillingType = (Label)ControlFinder.FindControlRecursive(item, "LblBillingType");
            Label lblStripeCustomerId = (Label)ControlFinder.FindControlRecursive(item, "LblStripeCustomerId");
            Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
            //Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");
            RadComboBox rcbxStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxStatus");
            Label lblPublic = (Label)ControlFinder.FindControlRecursive(item, "LblPublic");
            RadComboBox rcbxPublic = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxPublic");
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail");
            Label lblState = (Label)ControlFinder.FindControlRecursive(item, "LblState");
            RadTextBox rtbxState = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxState");
            Label lblCity = (Label)ControlFinder.FindControlRecursive(item, "LblCity");
            RadTextBox rtbxCity = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxCity");
            //RadComboBox rcbxBillingType = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxBillingType");
            RadTextBox rtbxStripeCustomerId = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxStripeCustomerId");
            ImageButton imgBtnSaveChanges = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSaveChanges");
            ImageButton imgBtnEditCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEditCompany");
            ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");

            #endregion

            if (!isSaveMode)
            {
                LoadTypes(item);
            }

            ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
            if (user != null)
            {
                #region Get User for Edit

                if (isSaveMode && !isCancelClicked)
                {
                    user.CompanyType = rcbxCategory.SelectedItem.Text;
                    user.MashapeUsername = (user.CompanyType != Types.Vendors.ToString()) ? "" : user.MashapeUsername;
                    user.AccountStatus = Convert.ToInt32(rcbxStatus.SelectedItem.Value);
                    //user.FeaturesNo = (string.IsNullOrEmpty(rtbxFeature.Text)) ? 0 : Convert.ToInt32(rtbxFeature.Text);
                    user.LastUpdated = DateTime.Now;
                    user.IsPublic = Convert.ToInt32(rcbxPublic.SelectedValue);
                    user.CustomerStripeId = rtbxStripeCustomerId.Text;
                    user.State = rtbxState.Text;
                    user.City = rtbxCity.Text;

                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text == string.Empty)
                    {
                        #region Cancel Virtual Premium User

                        //DateTime canceledAt = DateTime.Now;
                        //ElioUsersCreditCards userCard = null;

                        //try
                        //{
                        //    session.BeginTransaction();

                        //    user = PaymentLib.MakeUserFreemium(user, canceledAt, userCard, session);

                        //    session.CommitTransaction();
                        //}
                        //catch (Exception ex)
                        //{
                        //    session.RollBackTransaction();
                        //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                        //    GlobalMethods.ShowMessageControlDA(UcMessageAlert, string.Format("Virtual User with ID {0}, could not be canceled, something went wrong", user.Id.ToString()), MessageTypes.Error, true, true, false);

                        //    return;
                        //}

                        //alert = string.Format("Virtual User with ID {0}, canceled successfully. ", user.Id.ToString());

                        #endregion
                    }
                    else if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text != string.Empty)
                    {
                        #region Cancel Stripe Registered User

                        //DateTime? canceledAt = null;
                        //bool successUnsubscription = false;
                        //string stripeUnsubscribeError = string.Empty;
                        //string defaultCreditCard = string.Empty;

                        //ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);
                        //if (userCard != null)
                        //    defaultCreditCard = userCard.CardStripeId;

                        //try
                        //{
                        //    successUnsubscription = StripeLib.UnSubscribeCustomer(ref canceledAt, user.CustomerStripeId, defaultCreditCard, ref stripeUnsubscribeError);
                        //}
                        //catch (Exception ex)
                        //{
                        //    GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                        //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //}

                        //if (successUnsubscription)
                        //{
                        //    #region Make User Fremium to Elio

                        //    try
                        //    {
                        //        session.BeginTransaction();

                        //        PaymentLib.MakeUserFreemium(user, canceledAt, userCard, session);

                        //        session.CommitTransaction();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        session.RollBackTransaction();
                        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                        //        GlobalMethods.ShowMessageControlDA(UcMessageAlert, string.Format("Premium User with ID {0}, could not be set as Fremium, but canceled successfully from Stripe with stripe customer ID {1}, something went wrong", user.Id.ToString(), user.CustomerStripeId.ToString()), MessageTypes.Error, true, true, false);
                        //        return;
                        //    }

                        //    #endregion
                        //}
                        //else
                        //{
                        //    GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                        //    return;
                        //}

                        //alert = string.Format("Premium User with ID {0}, canceled and unsubscribed from Stripe too successfully. ", user.Id.ToString());

                        #endregion
                    }
                    else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text != string.Empty)
                    {
                        #region Convert Elio Fremium User to Premium with already Stripe Subscription

                        //DateTime? startDate = null;
                        //DateTime? currentPeriodStart = null;
                        //DateTime? currentPeriodEnd = null;
                        //DateTime? trialPeriodStart = null;
                        //DateTime? trialPeriodEnd = null;
                        //DateTime? canceledAt = null;
                        //string orderMode = string.Empty;
                        //Xamarin.Payments.Stripe.StripeCard cardInfo = null;

                        //Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, rtbxStripeCustomerId.Text);

                        //if (startDate != null && ((currentPeriodStart != null && currentPeriodEnd != null) || (trialPeriodStart != null && trialPeriodEnd != null)))
                        //{
                        //    try
                        //    {
                        //        ElioPackets selectedPacket = Sql.GetPacketByUserBillingTypePacketId(Convert.ToInt32(rcbxBillingType.SelectedValue), session);
                        //        if (selectedPacket != null)
                        //        {
                        //            session.BeginTransaction();

                        //            user = PaymentLib.MakeUserPremium(user, selectedPacket.Id, null, rtbxStripeCustomerId.Text, "", lblEmail.Text, subscription.Start, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, subscription.TrialStart, subscription.TrialEnd, orderMode, cardInfo, session);

                        //            session.CommitTransaction();
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        session.RollBackTransaction();
                        //        GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //        return;
                        //    }
                        //}

                        //alert = string.Format("Fremium User with ID {0}, but with Stripe Subscription already and customer ID {1}, was converted to Premium successfully. ", user.Id.ToString(), user.CustomerStripeId.ToString());

                        #endregion
                    }
                    else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text == string.Empty)
                    {
                        #region Make Virtual Premium User with no Stripe Subscription

                        //try
                        //{
                        //    session.BeginTransaction();

                        //    user = PaymentLib.MakeUserVirtualPremium(user, Convert.ToInt32(rcbxBillingType.SelectedItem.Value), rtbxStripeCustomerId.Text, lblEmail.Text, DateTime.Now, DateTime.Now, DateTime.Now.AddDays(14), DateTime.Now, DateTime.Now.AddDays(14), OrderMode.Trialing.ToString(), session);

                        //    session.CommitTransaction();
                        //}
                        //catch (Exception ex)
                        //{
                        //    session.RollBackTransaction();
                        //    GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                        //    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //    return;
                        //}

                        //alert = string.Format("User with ID {0}, was made Virtual Premium successfully. ", user.Id.ToString());

                        #endregion
                    }

                    #region Update User

                    //user.BillingType = Convert.ToInt32(rcbxBillingType.SelectedItem.Value);

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    loader.Update(user);

                    #endregion

                    #region Rebind Grid

                    RdgElioUsers.Rebind();

                    #endregion

                    alert += Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "1")).Text;

                    #region Edited User is Administrator

                    if (Sql.IsUserAdministrator(user.Id, session))
                    {
                        vSession.User = user;
                        alert += Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "2")).Text;
                    }

                    #endregion
                }
                else if (isCancelClicked)
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "3")).Text;
                }

                #region Fix Controls State

                lblCategory.Visible = isSaveMode;
                lblStatus.Visible = isSaveMode;
                lblPublic.Visible = isSaveMode;
                rcbxPublic.Visible = !isSaveMode;
                lblState.Visible = isSaveMode;
                lblCity.Visible = isSaveMode;
                rtbxState.Visible = !isSaveMode;
                rtbxCity.Visible = !isSaveMode;
                lblBillingType.Visible = isSaveMode;
                lblStripeCustomerId.Visible = isSaveMode;
                rcbxStatus.Visible = !isSaveMode;
                rcbxCategory.Visible = !isSaveMode;
                //rtbxFeature.Visible = !isSaveMode;
                //rcbxBillingType.Visible = !isSaveMode;
                rtbxStripeCustomerId.Visible = !isSaveMode;
                imgBtnSaveChanges.Visible = !isSaveMode;
                imgBtnCancel.Visible = !isSaveMode;
                imgBtnEditCompany.Visible = isSaveMode;

                //rtbxFeature.Text = user.FeaturesNo.ToString();
                //rcbxBillingType.FindItemByValue(user.BillingType.ToString()).Selected = true;
                rtbxStripeCustomerId.Text = user.CustomerStripeId;
                rcbxStatus.FindItemByValue(user.AccountStatus.ToString()).Selected = true;
                rcbxPublic.FindItemByValue(user.IsPublic.ToString()).Selected = true;
                rcbxCategory.FindItemByText(user.CompanyType).Selected = true;

                rtbxState.Text = user.State;
                rtbxCity.Text = user.City;

                #endregion

                #region Show Message Alert

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);

                #endregion

                #endregion
            }
            else
            {
                #region User could not be find for Edit

                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "7")).Text;

                RdgElioUsers.Rebind();

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);

                #endregion
            }
        }

        private void LoadCompanyTypes()
        {
            RcbxCategory.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "39")).Text;
            RcbxCategory.Items.Add(item);

            List<ElioUserTypes> types = Sql.GetUserAllTypes(session);
            foreach (ElioUserTypes type in types)
            {
                item = new RadComboBoxItem();

                item.Value = type.Id.ToString();
                item.Text = type.Description;

                RcbxCategory.Items.Add(item);
            }
        }

        private void LoadStatus()
        {
            RcbxStatus.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "-1";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "40")).Text;
            RcbxStatus.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();

            item1.Value = "0";
            item1.Text = AccountStatus.NotCompleted.ToString();
            RcbxStatus.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();

            item2.Value = "1";
            item2.Text = AccountStatus.Completed.ToString();
            RcbxStatus.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();

            item3.Value = "2";
            item3.Text = AccountStatus.Deleted.ToString();
            RcbxStatus.Items.Add(item3);

            RadComboBoxItem item4 = new RadComboBoxItem();

            item4.Value = "3";
            item4.Text = AccountStatus.Blocked.ToString();
            RcbxStatus.Items.Add(item4);
        }

        private void LoadPublicStatus()
        {
            RcbxIsPublic.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "-1";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "41")).Text;
            RcbxIsPublic.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();

            item1.Value = "1";
            item1.Text =Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "43")).Text;
            RcbxIsPublic.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();

            item2.Value = "0";
            item2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "44")).Text;
            RcbxIsPublic.Items.Add(item2);           
        }

        private void LoadBillingType()
        {
            RcbxBillingType.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "58")).Text;
            RcbxBillingType.Items.Add(item);

            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Value = Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString();
            item2.Text = BillingTypePacket.FreemiumPacketType.ToString();
            RcbxBillingType.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();
            item3.Value = Convert.ToInt32(BillingTypePacket.PremiumPacketType).ToString();
            item3.Text = BillingTypePacket.PremiumPacketType.ToString();
            RcbxBillingType.Items.Add(item3);

            RadComboBoxItem item4 = new RadComboBoxItem();
            item4.Value = Convert.ToInt32(BillingTypePacket.PremiumStartupPacketType).ToString();
            item4.Text = BillingTypePacket.PremiumStartupPacketType.ToString();
            RcbxBillingType.Items.Add(item4);

            RadComboBoxItem item5 = new RadComboBoxItem();
            item5.Value = Convert.ToInt32(BillingTypePacket.PremiumGrowthPacketType).ToString();
            item5.Text = BillingTypePacket.PremiumGrowthPacketType.ToString();
            RcbxBillingType.Items.Add(item5);

            RadComboBoxItem item6 = new RadComboBoxItem();
            item6.Value = Convert.ToInt32(BillingTypePacket.PremiumEnterprisePacketType).ToString();
            item6.Text = BillingTypePacket.PremiumEnterprisePacketType.ToString();
            RcbxBillingType.Items.Add(item6);

            RadComboBoxItem item7 = new RadComboBoxItem();
            item7.Value = Convert.ToInt32(BillingTypePacket.PremiumSelfServicePacketType).ToString();
            item7.Text = BillingTypePacket.PremiumSelfServicePacketType.ToString();
            RcbxBillingType.Items.Add(item7);
        }

        private void LoadApplicationType()
        {
            RcbxApplicationType.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "59")).Text;
            RcbxApplicationType.Items.Add(item);

            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Value = Convert.ToInt32(UserApplicationType.Elioplus).ToString();
            item2.Text = UserApplicationType.Elioplus.ToString();
            RcbxApplicationType.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();
            item3.Value = Convert.ToInt32(UserApplicationType.ThirdParty).ToString();
            item3.Text = UserApplicationType.ThirdParty.ToString();
            RcbxApplicationType.Items.Add(item3);
        }

        private void LoadVendorsCompanies()
        {
            List<ElioUsers> companies = Sql.GetAllVendorsUsersDDL(session);

            RcbxVendorsCompanyName.Items.Clear();

            List<ElioUsers> companiesLst = new List<ElioUsers>();
            ElioUsers defaultValue = new ElioUsers();
            defaultValue.Id = 0;
            defaultValue.CompanyName = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "42")).Text;
            companiesLst.Add(defaultValue);
            companiesLst.AddRange(companies);
            
            RcbxVendorsCompanyName.DataValueField = "Id";
            RcbxVendorsCompanyName.DataTextField = "CompanyName";

            RcbxVendorsCompanyName.DataSource = companiesLst;
            RcbxVendorsCompanyName.DataBind();

            #region to delete (old way)

            //RadComboBoxItem item = new RadComboBoxItem();
            //item.Value = "0";
            //item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "42")).Text;
            //RcbxVendorsCompanyName.Items.Add(item);

            //foreach (ElioUsers company in companies)
            //{
            //    item = new RadComboBoxItem();
            //    item.Value = company.Id.ToString();
            //    item.Text = company.CompanyName;

            //    RcbxVendorsCompanyName.Items.Add(item);
            //}

            #endregion
        }

        private void LoadResellersCompanies()
        {
            List<ElioUsers> companies = null;

            if (ConfigurationManager.AppSettings["EnableFeatures"] != null && ConfigurationManager.AppSettings["EnableFeatures"].ToString().ToLower() == "true")
                companies = Sql.GetAllResellersUsers(session);
            else
                RcbxResellersCompanyName.Enabled = false;

            RcbxResellersCompanyName.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "98")).Text;
            RcbxResellersCompanyName.Items.Add(item);

            if (companies != null)
            {
                foreach (ElioUsers company in companies)
                {
                    item = new RadComboBoxItem();
                    item.Value = company.Id.ToString();
                    item.Text = company.CompanyName;

                    RcbxResellersCompanyName.Items.Add(item);
                }
            }
        }

        private void LoadThirdPartyCompanies()
        {
            List<ElioUsers> companies =null;

            if (ConfigurationManager.AppSettings["EnableFeatures"] != null && ConfigurationManager.AppSettings["EnableFeatures"].ToString().ToLower() == "true")
                companies = Sql.GetAllThirdPartyUsers(session);
            else
                RcbxThirdPartyCompanyName.Enabled = false;
            RcbxThirdPartyCompanyName.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "99")).Text;
            RcbxThirdPartyCompanyName.Items.Add(item);
            
            if (companies != null)
            {
                foreach (ElioUsers company in companies)
                {
                    item = new RadComboBoxItem();
                    item.Value = company.Id.ToString();
                    item.Text = company.CompanyName;

                    RcbxThirdPartyCompanyName.Items.Add(item);
                }
            }
        }

        private DataTable RetrieveSpecificTypeOfUsers(int userId, string type, DBSession session)
        {
            DataTable table = null;

            List<ElioUsers> AllUsers = new List<ElioUsers>();
            bool exist = false;

            if (TbxConnectionUserId.Text == "" && TbxConnectionCompanyName.Text == "" && TbxConnectionCompanyEmail.Text == "")
                AllUsers = Sql.GetUsersByCompanyType(type, session);    //Sql.GetUsersExceptUserActiveConnectionsByCompanyType(userId, type, session);
            else
            {
                if (TbxConnectionUserId.Text.Trim() != "")
                {
                    //string ids = "";
                    //List<string> partnerIDs = TbxConnectionUserId.Text.Trim().Split(',').ToList();
                    //foreach (string id in partnerIDs)
                    //{
                    //    if (id != "")
                    //        ids += "'" + id + "',";
                    //}

                    if (TbxConnectionUserId.Text.EndsWith(","))
                        TbxConnectionUserId.Text = TbxConnectionUserId.Text.Substring(0, TbxConnectionUserId.Text.Length - 1);

                    List<ElioUsers> users = Sql.GetUsersByIds(TbxConnectionUserId.Text, session);

                    foreach (ElioUsers usr in users)
                    {
                        exist = false;
                        foreach (ElioUsers allUser in AllUsers)
                        {
                            if (allUser.Id == usr.Id)
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                            AllUsers.Add(usr);
                    }
                }

                if (TbxConnectionCompanyName.Text != "")
                {
                    string names = "";
                    List<string> partnerNames = TbxConnectionCompanyName.Text.Split(',').ToList();
                    foreach (string name in partnerNames)
                    {
                        if (name != "")
                            names += "'" + name + "',";
                    }

                    if (names.EndsWith(","))
                        names = names.Substring(0, names.Length - 1);

                    List<ElioUsers> users = Sql.GetUsersByCompaniesName(names, session);

                    foreach (ElioUsers usr in users)
                    {
                        exist = false;
                        foreach (ElioUsers allUser in AllUsers)
                        {
                            if (allUser.Id == usr.Id)
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                            AllUsers.Add(usr);
                    }
                }

                if (TbxConnectionCompanyEmail.Text != "")
                {
                    string emails = "";
                    List<string> partnerEmails = TbxConnectionCompanyEmail.Text.Trim().Split(',').ToList();
                    foreach (string email in partnerEmails)
                    {
                        if (email != "")
                            emails += "'" + email + "',";
                    }

                    if (emails.EndsWith(","))
                        emails = emails.Substring(0, emails.Length - 1);

                    List<ElioUsers> users = Sql.GetUsersByCompaniesEmail(emails, session);
                                        
                    foreach (ElioUsers usr in users)
                    {
                        exist = false;
                        foreach (ElioUsers allUser in AllUsers)
                        {
                            if (allUser.Id == usr.Id)
                            {
                                exist = true;
                                break;
                            }
                        }

                        if (!exist)
                            AllUsers.Add(usr);
                    }
                }
            }

            if (AllUsers.Count > 0)
            {
                table = new DataTable();

                table.Columns.Add("id");
                table.Columns.Add("connection_id");
                table.Columns.Add("company_name");
                table.Columns.Add("user_application_type");
                table.Columns.Add("country");
                table.Columns.Add("email");
                table.Columns.Add("current_period_start");
                table.Columns.Add("current_period_end");
                table.Columns.Add("status");

                foreach (ElioUsers user in AllUsers)
                {
                    table.Rows.Add(userId, user.Id, user.CompanyName, (user.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? UserApplicationType.Elioplus.ToString() : UserApplicationType.ThirdParty.ToString(), user.Country, user.Email, "", "", "");
                }
            }
            return table;
        }

        private bool AddUserConnections(GridDataItem item, int userId, int connectionId, bool insertConnection, DBSession session)
        {
            Image imgStatus = (Image)ControlFinder.FindControlRecursive(item, "ImgStatus");

            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                if (user.CompanyType == Types.Vendors.ToString())
                {
                    #region Vendor

                    if (insertConnection)
                    {
                        #region Add Connection to Vendor

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(user.Id, connectionId, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                                if (vendorPacketFeatures != null)
                                {
                                    if (vendorPacketFeatures.ExpirationDate <= DateTime.Now)
                                    {
                                        #region Packet Status Features need update

                                        ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                        if (userSubscription != null)
                                        {
                                            int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
                                            if (packId > 0)
                                            {
                                                if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
                                                    packId = (int)Packets.Premium;

                                                ElioPackets packet = Sql.GetPacketById(packId, session);
                                                if (packet != null && packet.Id != (int)Packets.PremiumService)
                                                {
                                                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(packet.Id, session);
                                                    if (items.Count > 0)
                                                    {
                                                        #region Get Packet Features Items

                                                        int totalLeads = 0;
                                                        int totalMessages = 0;
                                                        int totalConnections = 0;
                                                        int totalManagePartners = 0;
                                                        int totalLibraryStorage = 0;

                                                        for (int i = 0; i < items.Count; i++)
                                                        {
                                                            if (items[i].ItemDescription == "Leads")
                                                            {
                                                                totalLeads = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Messages")
                                                            {
                                                                totalMessages = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Connections")
                                                            {
                                                                totalConnections = items[i].FreeItemsNo;        // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "ManagePartners")
                                                            {
                                                                totalManagePartners = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "LibraryStorage")
                                                            {
                                                                totalLibraryStorage = items[i].FreeItemsNo;
                                                            }
                                                        }

                                                        #endregion

                                                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

                                                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
                                                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

                                                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                                                        #endregion

                                                        #region Insert / Update Packet Status Features

                                                        ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

                                                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

                                                        if (packetFeatures == null)
                                                        {
                                                            packetFeatures = new ElioUserPacketStatus();

                                                            packetFeatures.UserId = user.Id;
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.Sysdate = DateTime.Now;
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Insert(packetFeatures);
                                                        }
                                                        else
                                                        {
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Update(packetFeatures);
                                                        }

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                    vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);

                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Vendor Side

                                        bool hasNoSubscription = false;
                                        string ids = (ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != null && ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != "") ? ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"].ToString() : "";

                                        if (ids != "")
                                        {
                                            string[] customers = ids.Split(',').ToArray();
                                            foreach (string userID in customers)
                                            {
                                                if (Convert.ToInt32(userID) == userId)
                                                {
                                                    hasNoSubscription = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!hasNoSubscription)
                                        {
                                            #region Normal Stripe Case

                                            #region Get User Subscription

                                            ElioUsersSubscriptions sub = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                            //ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                            //if (order == null)
                                            //{
                                            //    order = Sql.HasUserOrderByServicePacketStatusUse(user.Id, Convert.ToInt32(Packets.PremiumService), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                            //}

                                            #endregion

                                            if (sub != null)
                                            {
                                                if (sub.Status.ToLower() != "active")   //custom for now
                                                    Logger.Debug(Request.Url.ToString(), string.Format("AdminPage.aspx --> MESSAGE: Admin added connection ID:{0}, to user {1} at {2}, but his subscription status is {3}", connectionId, user.Id, DateTime.Now.ToString(), sub.SubscriptionId), "Connection added successfully but subscription need to be updated");

                                                DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                                #region Add Vendor Connection

                                                ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                                vendorConnection.UserId = userId;
                                                vendorConnection.ConnectionId = connectionId;
                                                vendorConnection.SysDate = DateTime.Now;
                                                vendorConnection.LastUpdated = DateTime.Now;
                                                vendorConnection.CanBeViewed = 1;
                                                vendorConnection.CurrentPeriodStart = Convert.ToDateTime(sub.CurrentPeriodStart);
                                                vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(sub.CurrentPeriodEnd);
                                                vendorConnection.Status = true;
                                                vendorConnection.IsNew = 1;

                                                loader.Insert(vendorConnection);

                                                #endregion

                                                #region Add Reseller Connection

                                                ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                                resellerConnection.UserId = connectionId;
                                                resellerConnection.ConnectionId = userId;
                                                resellerConnection.SysDate = DateTime.Now;
                                                resellerConnection.LastUpdated = DateTime.Now;
                                                resellerConnection.CanBeViewed = 1;
                                                resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                                resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                                resellerConnection.Status = true;
                                                resellerConnection.IsNew = 1;

                                                loader.Insert(resellerConnection);

                                                #endregion

                                                #region Update Vendor Available Connections Counter

                                                vendorPacketFeatures.AvailableConnectionsCount--;
                                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                                loader1.Update(vendorPacketFeatures);

                                                #endregion

                                                #region Show Success Message of Currrent Period

                                                item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                                item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                                imgStatus.Visible = true;
                                                imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                                imgStatus.AlternateText = "elio account success";

                                                #endregion

                                                //}
                                                //else
                                                //{
                                                //    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "User has no active subscription to add connections", MessageTypes.Warning, true, true, false);
                                                //    return false;
                                                //}
                                            }
                                            else
                                            {
                                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "No subscription found for this user", MessageTypes.Warning, true, true, false);
                                                return false;
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Excluded from Stripe Customers

                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = Convert.ToDateTime(vendorPacketFeatures.StartingDate);
                                            vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(vendorPacketFeatures.ExpirationDate);
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                            resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Update Vendor Available Connections Counter

                                            vendorPacketFeatures.AvailableConnectionsCount--;
                                            vendorPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(vendorPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            imgStatus.Visible = true;
                                            imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                            imgStatus.AlternateText = "elio account success";

                                            #endregion

                                            #endregion
                                        }

                                        #endregion

                                        #region Reseller Side

                                        ElioUsers reseller = Sql.GetUserById(connectionId, session);
                                        if (reseller != null)
                                        {
                                            if (reseller.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
                                                if (resellerPacketFeatures != null)
                                                {
                                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        #region Update Reseller Available Connections Counter

                                                        resellerPacketFeatures.AvailableConnectionsCount--;
                                                        resellerPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(resellerPacketFeatures);

                                                        #endregion
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You have no available connections to add to this user", MessageTypes.Warning, true, true, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "This connection belongs already to this user", MessageTypes.Warning, true, true, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You are not allowed to add connection to Fremium user", MessageTypes.Warning, true, true, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Vendor Delete Connection

                        //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                        //if (vendorConnection != null)
                        //{
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(vendorConnection);
                        //}

                        #endregion

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUsers connectionUser = Sql.GetUserById(connectionId, session);

                        if (connectionUser != null)
                        {
                            if (connectionUser.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                                if (resellerPacketFeatures != null)
                                {
                                    #region Increase Reseller Available Connections Counter

                                    resellerPacketFeatures.AvailableConnectionsCount++;
                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    loader.Update(resellerPacketFeatures);

                                    #endregion
                                }
                            }
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(resellerConnection);
                        //}

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        item["current_period_start"].Text = "-";
                        item["current_period_end"].Text = "-";
                        imgStatus.Visible = true;
                        imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    #region reseller

                    if (insertConnection)
                    {
                        #region Add Connection to Reseller

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(connectionId, user.Id, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                                if (resellerPacketFeatures != null)
                                {
                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Reseller Side

                                        ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                        if (order != null)
                                        {
                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                                            resellerConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);

                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = resellerConnection.CurrentPeriodStart;
                                            vendorConnection.CurrentPeriodEnd = resellerConnection.CurrentPeriodEnd;
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Update Reseller Available Connections Counter

                                            resellerPacketFeatures.AvailableConnectionsCount--;
                                            resellerPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(resellerPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            item["current_period_start"].Text = resellerConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            item["current_period_end"].Text = resellerConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            imgStatus.Visible = true;
                                            imgStatus.ImageUrl = "~/images/icons/small/success.png";

                                            #endregion
                                        }

                                        #endregion

                                        #region Vendor Side

                                        ElioUsers vendor = Sql.GetUserById(userId, session);
                                        if (vendor != null)
                                        {
                                            if (vendor.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vendor.Id, session);
                                                if (vendorPacketFeatures != null)
                                                {
                                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        vendorPacketFeatures.AvailableConnectionsCount--;
                                                        vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(vendorPacketFeatures);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You have no available connections to add to this user", MessageTypes.Warning, true, true, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "This connection belongs already to this user", MessageTypes.Warning, true, true, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You are not allowed to add connection to Fremium user", MessageTypes.Warning, true, true, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                        if (resellerPacketFeatures != null)
                        {
                            #region Increase Reseller Available Connections Counter

                            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                //    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                //    if (order != null)
                                //    {
                                //if (connection.CurrentPeriodStart >= order.CurrentPeriodStart && connection.CurrentPeriodEnd <= order.CurrentPeriodEnd)
                                //{
                                resellerPacketFeatures.AvailableConnectionsCount++;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(resellerPacketFeatures);
                                //}
                                //}
                            }

                            #endregion
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(resellerConnection);

                        item["current_period_start"].Text = "-";
                        item["current_period_end"].Text = "-";
                        imgStatus.Visible = true;
                        imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //}

                        #endregion

                        #region Vendor Delete Connection

                        //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                        //if (vendorConnection != null)
                        //{
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(vendorConnection);
                        //}

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        item["current_period_start"].Text = "-";
                        item["current_period_end"].Text = "-";
                        imgStatus.Visible = true;
                        imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else
                    return false;
            }
            else
                return false;
        }

        private bool AddUserConnectionsInstantly1(int userId, int connectionId, bool insertConnection, DBSession session)
        {
            ElioUsers user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                if (user.CompanyType == Types.Vendors.ToString())
                {
                    #region Vendor

                    if (insertConnection)
                    {
                        #region Add Connection to Vendor

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(user.Id, connectionId, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                                if (vendorPacketFeatures != null)
                                {
                                    if (vendorPacketFeatures.ExpirationDate <= DateTime.Now)
                                    {
                                        #region Packet Status Features need update

                                        ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                        if (userSubscription != null)
                                        {
                                            int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
                                            if (packId > 0)
                                            {
                                                if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
                                                    packId = (int)Packets.Premium;

                                                ElioPackets packet = Sql.GetPacketById(packId, session);
                                                if (packet != null && packet.Id != (int)Packets.PremiumService)
                                                {
                                                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(packet.Id, session);
                                                    if (items.Count > 0)
                                                    {
                                                        #region Get Packet Features Items

                                                        int totalLeads = 0;
                                                        int totalMessages = 0;
                                                        int totalConnections = 0;
                                                        int totalManagePartners = 0;
                                                        int totalLibraryStorage = 0;

                                                        for (int i = 0; i < items.Count; i++)
                                                        {
                                                            if (items[i].ItemDescription == "Leads")
                                                            {
                                                                totalLeads = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Messages")
                                                            {
                                                                totalMessages = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "Connections")
                                                            {
                                                                totalConnections = items[i].FreeItemsNo;        // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "ManagePartners")
                                                            {
                                                                totalManagePartners = items[i].FreeItemsNo;
                                                            }
                                                            else if (items[i].ItemDescription == "LibraryStorage")
                                                            {
                                                                totalLibraryStorage = items[i].FreeItemsNo;
                                                            }
                                                        }

                                                        #endregion

                                                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

                                                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
                                                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

                                                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                                                        #endregion

                                                        #region Insert / Update Packet Status Features

                                                        ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

                                                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

                                                        if (packetFeatures == null)
                                                        {
                                                            packetFeatures = new ElioUserPacketStatus();

                                                            packetFeatures.UserId = user.Id;
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.Sysdate = DateTime.Now;
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Insert(packetFeatures);
                                                        }
                                                        else
                                                        {
                                                            packetFeatures.PackId = items[0].Id;
                                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                            packetFeatures.AvailableLeadsCount = totalLeads;
                                                            packetFeatures.AvailableMessagesCount = totalMessages;
                                                            packetFeatures.AvailableConnectionsCount = totalConnections;
                                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                            packetFeatures.LastUpdate = DateTime.Now;
                                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                            loader4.Update(packetFeatures);
                                                        }

                                                        #endregion
                                                    }
                                                    else
                                                    {
                                                        Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }

                                    vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);

                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Vendor Side

                                        bool hasNoSubscription = false;
                                        string ids = (ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != null && ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"] != "") ? ConfigurationManager.AppSettings["ExcludedCustomersFromStripe"].ToString() : "";

                                        if (ids != "")
                                        {
                                            string[] customers = ids.Split(',').ToArray();
                                            foreach (string userID in customers)
                                            {
                                                if (Convert.ToInt32(userID) == userId)
                                                {
                                                    hasNoSubscription = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (!hasNoSubscription)
                                        {
                                            #region Normal Stripe Case

                                            #region Get User Subscription

                                            ElioUsersSubscriptions sub = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                            //ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                            //if (order == null)
                                            //{
                                            //    order = Sql.HasUserOrderByServicePacketStatusUse(user.Id, Convert.ToInt32(Packets.PremiumService), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                            //}

                                            #endregion

                                            if (sub != null)
                                            {
                                                if (sub.Status.ToLower() != "active")   //custom for now
                                                    Logger.Debug(Request.Url.ToString(), string.Format("AdminPage.aspx --> MESSAGE: Admin added connection ID:{0}, to user {1} at {2}, but his subscription status is {3}", connectionId, user.Id, DateTime.Now.ToString(), sub.SubscriptionId), "Connection added successfully but subscription need to be updated");

                                                DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                                #region Add Vendor Connection

                                                ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                                vendorConnection.UserId = userId;
                                                vendorConnection.ConnectionId = connectionId;
                                                vendorConnection.SysDate = DateTime.Now;
                                                vendorConnection.LastUpdated = DateTime.Now;
                                                vendorConnection.CanBeViewed = 1;
                                                vendorConnection.CurrentPeriodStart = Convert.ToDateTime(sub.CurrentPeriodStart);
                                                vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(sub.CurrentPeriodEnd);
                                                vendorConnection.Status = true;
                                                vendorConnection.IsNew = 1;

                                                loader.Insert(vendorConnection);

                                                #endregion

                                                #region Add Reseller Connection

                                                ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                                resellerConnection.UserId = connectionId;
                                                resellerConnection.ConnectionId = userId;
                                                resellerConnection.SysDate = DateTime.Now;
                                                resellerConnection.LastUpdated = DateTime.Now;
                                                resellerConnection.CanBeViewed = 1;
                                                resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                                resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                                resellerConnection.Status = true;
                                                resellerConnection.IsNew = 1;

                                                loader.Insert(resellerConnection);

                                                #endregion

                                                #region Update Vendor Available Connections Counter

                                                vendorPacketFeatures.AvailableConnectionsCount--;
                                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                                loader1.Update(vendorPacketFeatures);

                                                #endregion

                                                #region Show Success Message of Currrent Period

                                                //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                                //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                                //imgStatus.Visible = true;
                                                //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                                //imgStatus.AlternateText = "elio account success";

                                                #endregion
                                            }
                                            else
                                            {
                                                GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "No subscription found for this user", MessageTypes.Error, true, true, false, false, false);
                                                return false;
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Excluded from Stripe Customers

                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = Convert.ToDateTime(vendorPacketFeatures.StartingDate);
                                            vendorConnection.CurrentPeriodEnd = Convert.ToDateTime(vendorPacketFeatures.ExpirationDate);
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = vendorConnection.CurrentPeriodStart;
                                            resellerConnection.CurrentPeriodEnd = vendorConnection.CurrentPeriodEnd;
                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Update Vendor Available Connections Counter

                                            vendorPacketFeatures.AvailableConnectionsCount--;
                                            vendorPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(vendorPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                            //imgStatus.AlternateText = "elio account success";

                                            #endregion

                                            #endregion
                                        }

                                        #endregion

                                        #region Reseller Side

                                        ElioUsers reseller = Sql.GetUserById(connectionId, session);
                                        if (reseller != null)
                                        {
                                            if (reseller.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
                                                if (resellerPacketFeatures != null)
                                                {
                                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        #region Update Reseller Available Connections Counter

                                                        resellerPacketFeatures.AvailableConnectionsCount--;
                                                        resellerPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(resellerPacketFeatures);

                                                        #endregion
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Vendor Delete Connection

                        //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                        //if (vendorConnection != null)
                        //{
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(vendorConnection);
                        //}

                        #endregion

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUsers connectionUser = Sql.GetUserById(connectionId, session);

                        if (connectionUser != null)
                        {
                            if (connectionUser.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                                if (resellerPacketFeatures != null)
                                {
                                    #region Increase Reseller Available Connections Counter

                                    resellerPacketFeatures.AvailableConnectionsCount++;
                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    loader.Update(resellerPacketFeatures);

                                    #endregion
                                }
                            }
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                        //loader1.Delete(resellerConnection);
                        //}

                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    #region reseller

                    if (insertConnection)
                    {
                        #region Add Connection to Reseller

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            #region Only Not Free User

                            bool isAlreadyConnection = Sql.IsConnection(connectionId, user.Id, session);
                            if (!isAlreadyConnection)
                            {
                                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                                if (resellerPacketFeatures != null)
                                {
                                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                                    {
                                        #region Reseller Side

                                        ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                        if (order != null)
                                        {
                                            DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                            #region Add Reseller Connection

                                            ElioUsersConnections resellerConnection = new ElioUsersConnections();

                                            resellerConnection.UserId = connectionId;
                                            resellerConnection.ConnectionId = userId;
                                            resellerConnection.SysDate = DateTime.Now;
                                            resellerConnection.LastUpdated = DateTime.Now;
                                            resellerConnection.CanBeViewed = 1;
                                            resellerConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                                            resellerConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);

                                            resellerConnection.Status = true;
                                            resellerConnection.IsNew = 1;

                                            loader.Insert(resellerConnection);

                                            #endregion

                                            #region Add Vendor Connection

                                            ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                            vendorConnection.UserId = userId;
                                            vendorConnection.ConnectionId = connectionId;
                                            vendorConnection.SysDate = DateTime.Now;
                                            vendorConnection.LastUpdated = DateTime.Now;
                                            vendorConnection.CanBeViewed = 1;
                                            vendorConnection.CurrentPeriodStart = resellerConnection.CurrentPeriodStart;
                                            vendorConnection.CurrentPeriodEnd = resellerConnection.CurrentPeriodEnd;
                                            vendorConnection.Status = true;
                                            vendorConnection.IsNew = 1;

                                            loader.Insert(vendorConnection);

                                            #endregion

                                            #region Update Reseller Available Connections Counter

                                            resellerPacketFeatures.AvailableConnectionsCount--;
                                            resellerPacketFeatures.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                            loader1.Update(resellerPacketFeatures);

                                            #endregion

                                            #region Show Success Message of Currrent Period

                                            //item["current_period_start"].Text = resellerConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                            //item["current_period_end"].Text = resellerConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                            //imgStatus.Visible = true;
                                            //imgStatus.ImageUrl = "~/images/icons/small/success.png";

                                            #endregion
                                        }

                                        #endregion

                                        #region Vendor Side

                                        ElioUsers vendor = Sql.GetUserById(userId, session);
                                        if (vendor != null)
                                        {
                                            if (vendor.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                                            {
                                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vendor.Id, session);
                                                if (vendorPacketFeatures != null)
                                                {
                                                    if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                                                    {
                                                        vendorPacketFeatures.AvailableConnectionsCount--;
                                                        vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                        DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                        loader2.Update(vendorPacketFeatures);
                                                    }
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "You have no available connections to add to this user", MessageTypes.Error, true, true, false, false, false);
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "This connection belongs already to this user", MessageTypes.Error, true, true, false, false, false);
                                return false;
                            }

                            return true;

                            #endregion
                        }
                        else
                        {
                            #region Free User

                            GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "You are not allowed to add connection to Fremium user", MessageTypes.Error, true, true, false, false, false);
                            return false;

                            #endregion
                        }

                        #endregion
                    }
                    else
                    {
                        #region Delete Specific Connection

                        #region Reseller Delete Connection

                        //ElioUsersConnections resellerConnection = Sql.GetConnection(connectionId, userId, session);

                        //if (resellerConnection != null)
                        //{
                        ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                        if (resellerPacketFeatures != null)
                        {
                            #region Increase Reseller Available Connections Counter

                            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                //    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                //    if (order != null)
                                //    {
                                //if (connection.CurrentPeriodStart >= order.CurrentPeriodStart && connection.CurrentPeriodEnd <= order.CurrentPeriodEnd)
                                //{
                                resellerPacketFeatures.AvailableConnectionsCount++;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(resellerPacketFeatures);
                                //}
                                //}
                            }

                            #endregion
                        }

                        Sql.DeleteConnection(connectionId, userId, session);
                        
                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        
                        #endregion

                        #region Vendor Delete Connection

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                            if (vendorPacketFeatures != null)
                            {
                                #region Increase Vendor Available Connections Counter

                                vendorPacketFeatures.AvailableConnectionsCount++;
                                vendorPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(vendorPacketFeatures);

                                #endregion
                            }
                        }

                        Sql.DeleteConnection(userId, connectionId, session);
                        
                        #endregion

                        #endregion

                        #region Show Message for Delete Connection

                        //item["current_period_start"].Text = "-";
                        //item["current_period_end"].Text = "-";
                        //imgStatus.Visible = true;
                        //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                        //imgStatus.AlternateText = "elioplus account error";

                        #endregion

                        return false;
                    }

                    #endregion
                }
                else
                    return false;
            }
            else
                return false;
        }

        private void AddConnectionsFromModal(int userId, List<ElioUsers> connecitonUsers)
        {
            if (connecitonUsers.Count > 0)
            {
                foreach (ElioUsers conneciton in connecitonUsers)
                {
                    bool added = GlobalDBMethods.AddUserConnectionsInstantlyDA(userId, conneciton.Id, true, UcMessageControlAddConnections, UcMessageControlAddConnections, session);
                    if (!added)
                    {
                        //GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, string.Format("Connection with ID {0} could not be added. Procedure stopped!", conneciton.Id), MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                }

                GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, string.Format("{0} Connections added to this user successfully", connecitonUsers.Count), MessageTypes.Success, true, true, false, false, false);
            }
            else
                GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "No Connection was added to this user", MessageTypes.Error, true, true, false, false, false);
        }

        private void CollapseGridItems(RadGrid rdg)
        {
            foreach (GridDataItem item in rdg.MasterTableView.Items)
            {
                item.Expanded = false;
                foreach (GridDataItem nestedItem in item.ChildItem.NestedTableViews[0].Items)
                {
                    nestedItem.Expanded = false;
                }
            }
        }

        #endregion

        #region Grids
        
        protected void RdgElioUsers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        RdgElioUsers.MasterTableView.GetColumn("company_name").Display = (RcbxVendorsCompanyName.SelectedValue != "0" || RcbxResellersCompanyName.SelectedValue != "0" || RcbxThirdPartyCompanyName.SelectedValue != "0" || user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        //RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").Display = (RcbxBillingType.SelectedValue != "0") ? true : false;
                        RdgElioUsers.MasterTableView.GetColumn("account_status").Display = (RcbxStatus.SelectedValue != "-1") ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("company_type").Display = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        //RdgElioUsers.MasterTableView.GetColumn("features_no").Display = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("is_public").Display = (RcbxIsPublic.SelectedValue != "-1" || RcbxStatus.SelectedValue == "0") ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("billing_type").Display = (RcbxBillingType.SelectedValue != "0") ? false : true;

                        Label lblName = (Label)ControlFinder.FindControlRecursive(item, "LblName");
                        Label lblBillingType = (Label)ControlFinder.FindControlRecursive(item, "LblBillingType");
                        Label lblStripeCustomerId = (Label)ControlFinder.FindControlRecursive(item, "LblStripeCustomerId");
                        Label lblCategory = (Label)ControlFinder.FindControlRecursive(item, "LblCategory");
                        Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail");
                        Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                        Label lblPublic = (Label)ControlFinder.FindControlRecursive(item, "LblPublic");
                        RadComboBox rcbxStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxStatus");
                        RadComboBox rcbxEmail = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxEmail");
                        RadComboBox rcbxPublic = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxPublic");
                        
                        Label lblState = (Label)ControlFinder.FindControlRecursive(item, "LblState");
                        RadTextBox rtbxState = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxState");

                        lblState.Text = user.State;
                        rtbxState.Text = user.State;

                        Label lblCity = (Label)ControlFinder.FindControlRecursive(item, "LblCity");
                        RadTextBox rtbxCity = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxCity");

                        lblCity.Text = user.City;
                        rtbxCity.Text = user.City;

                        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                                                
                        ImageButton imgBtnLoginAsCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLoginAsCompany");
                        imgBtnLoginAsCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "4")).Text.Replace("{comapnyname}", (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed) ? user.CompanyName : user.Username));
                        scriptManager.RegisterPostBackControl(imgBtnLoginAsCompany);

                        ImageButton imgBtnPreviewCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreviewCompany");
                        imgBtnPreviewCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "5")).Text;
                        scriptManager.RegisterPostBackControl(imgBtnPreviewCompany);

                        ImageButton imgBtnEditCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEditCompany");
                        imgBtnEditCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;

                        ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                        imgBtnCancel.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "2")).Text;

                        ImageButton imgBtnSaveChanges = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSaveChanges");
                        imgBtnSaveChanges.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "3")).Text;

                        ImageButton imgBtnSendEmail = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSendEmail");
                        imgBtnSendEmail.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "6")).Text;
                        imgBtnSendEmail.ImageUrl = "~/Images/email_notif.png";
                        imgBtnSendEmail.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? true : false;

                        ImageButton imgBtnGetClearBitData = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnGetClearBitData");
                        imgBtnGetClearBitData.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "7")).Text.Replace("{email}", (!string.IsNullOrEmpty(user.Email)) ? user.Email : user.OfficialEmail);
                        imgBtnGetClearBitData.ImageUrl = "~/Images/clearbit_api_call.png";
                        imgBtnGetClearBitData.Visible = true;   // (!ClearbitSql.ExistsClearbitPerson(user.Id, session) && !ClearbitSql.ExistsClearbitCompany(user.Id, session));
                                                
                        bool hasPersonData = ClearbitSql.ExistsClearbitPerson(user.Id, session);
                        bool hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);

                        Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                        RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");
                        rttImgInfo.Text = user.Description + "</br></br>" + "<h3>Company Overview</h3>" + "</br>" + user.Overview;
                        rttImgInfo.Title = "Company Description/Overview";

                        string clearbitTooltip = "";
                        if (hasPersonData && hasCompanyData)
                        {
                            clearbitTooltip = "(Has person && company data)";
                        }
                        else if (hasPersonData && !hasCompanyData)
                        {
                            clearbitTooltip = "(Has person && no company data)";
                        }
                        else if (!hasPersonData && hasCompanyData)
                        {
                            clearbitTooltip = "(Has company && no person data)";
                        }
                        else if (!hasPersonData && !hasCompanyData)
                        {
                            clearbitTooltip = "(Has no person && no company data)";
                        }

                        imgBtnGetClearBitData.ToolTip += ". " + clearbitTooltip;

                        imgBtnPreviewCompany.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        //imgBtnEditCompany.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;

                        lblName.Text = user.CompanyName;
                        lblStripeCustomerId.Text = user.CustomerStripeId;

                        ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);
                        if (packet != null)
                            lblBillingType.Text = packet.PackDescription + " user type";      //(user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? BillingTypePacket.FreemiumPacketType.ToString() : BillingType.Premium.ToString();
                        
                        lblCategory.Text = user.CompanyType;
                        if (!string.IsNullOrEmpty(user.OfficialEmail) && user.Email != user.OfficialEmail)
                        {
                            rcbxEmail.Items.Clear();
                            RadComboBoxItem rcbxitem = new RadComboBoxItem();

                            rcbxitem.Value = "0";
                            rcbxitem.Text = user.Email;

                            rcbxEmail.Items.Add(rcbxitem);

                            RadComboBoxItem rcbxitem1 = new RadComboBoxItem();
                            rcbxitem1.Value = "1";
                            rcbxitem1.Text = user.OfficialEmail;

                            rcbxEmail.Items.Add(rcbxitem1);
                        }
                        else
                        {
                            lblEmail.Text = user.Email;
                        }

                        rcbxEmail.Visible = (!string.IsNullOrEmpty(user.OfficialEmail) && user.Email != user.OfficialEmail) ? true : false;
                        lblEmail.Visible = (rcbxEmail.Visible) ? false : true;

                        lblStatus.Text = GlobalMethods.GetUserStatusDescription(user.AccountStatus);
                        lblPublic.Text = GlobalMethods.GetUserPublicStatusDescription(user.IsPublic);
                        rcbxPublic.FindItemByValue(user.IsPublic.ToString()).Selected = true;
                        rcbxStatus.FindItemByValue(user.AccountStatus.ToString()).Selected = true;
                        //lblFeature.Text = user.FeaturesNo.ToString();

                        ElioUserPacketStatus packetStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                        if (packetStatus != null)
                            item["available_connections_count"].Text = packetStatus.AvailableConnectionsCount.ToString();
                        else
                            item["available_connections_count"].Text = "0";
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "CompanyItems")
                {
                    #region Resellers

                    GridDataItem item = (GridDataItem)e.Item;
                    
                    int userId = Convert.ToInt32(item["id"].Text);

                    //bool isVendor = Sql.IsUsersSpecificType(userId, Types.Vendors.ToString(), session);
                    ElioUsers user = Sql.FindUserType(userId, session);

                    if (item["connection_id"].Text != "&nbsp;")
                    {
                        int connectionId = Convert.ToInt32(item["connection_id"].Text);

                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                        Image imgStatus = (Image)ControlFinder.FindControlRecursive(item, "ImgStatus");

                        int isUserId = (user.CompanyType == Types.Vendors.ToString()) ? userId : connectionId;
                        int isConnectionId = (user.CompanyType == Types.Vendors.ToString()) ? connectionId : userId;

                        ElioUsersConnections connection = Sql.GetConnection(isUserId, isConnectionId, session);

                        if (connection != null)
                        {
                            cbxSelectUser.Checked = true;
                            item["current_period_start"].Text = connection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                            item["current_period_end"].Text = connection.CurrentPeriodEnd.ToString("MM/dd/yyyy");


                            imgStatus.Visible = true;
                            imgStatus.ImageUrl = (connection.Status) ? "~/images/icons/small/success.png" : "~/images/icons/small/error.png";
                            imgStatus.AlternateText = connection.Status ? "elioplus account success" : "elioplus account error";
                        }
                        else
                        {
                            cbxSelectUser.Checked = false;
                            item["current_period_start"].Text = "-";
                            item["current_period_end"].Text = "-";
                            imgStatus.Visible = false;
                        }
                    }

                    #endregion
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

        protected void RdgElioUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsers> users = new List<ElioUsers>();

                if (vSession.SearchQueryString == string.Empty)
                {
                    users = null;   // Sql.GetAllUsersId(session);
                }
                else
                {
                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    users = loader.Load(vSession.SearchQueryString);
                }

                if (users != null && users.Count > 0)
                {
                    RdgElioUsers.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("country");
                    table.Columns.Add("state");
                    table.Columns.Add("city");
                    table.Columns.Add("last_login");
                    table.Columns.Add("login_times");
                    //table.Columns.Add("count");
                    table.Columns.Add("sysdate");
                    //table.Columns.Add("first_send");
                    //table.Columns.Add("last_send");

                    foreach (ElioUsers user in users)
                    {
                        //List<ElioUsersNotificationEmails> emails = Sql.UserNotificationEmailCount(user.Id, session);

                        //string firstdate = "";
                        //string lastdate = "";
                        //if (emails.Count > 0)
                        //{
                        //    lastdate = emails[emails.Count - 1].NotificationEmailDate.ToString();
                        //    firstdate = emails[0].NotificationEmailDate.ToString();
                        //}
                        //else
                        //{
                        //    lastdate = "-";
                        //    firstdate = "-";
                        //}

                        table.Rows.Add(user.Id, user.Country, user.State, user.City, (user.UserLoginCount > 0) ? user.LastLogin : user.SysDate, user.UserLoginCount, user.SysDate.ToShortDateString());
                    }

                    RdgElioUsers.DataSource = table;
                }
                else
                {
                    RdgElioUsers.Visible = false;

                    string alert = "You have no company profiles to cover your seach criteria";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
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

        protected void RdgElioUsers_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "CompanyItems":
                        {
                            int userId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            //bool isVendor = Sql.IsUsersSpecificType(Convert.ToInt32(userId), Types.Vendors.ToString(), session);
                            ElioUsers user = Sql.FindUserType(Convert.ToInt32(userId), session);
                            if (user != null)
                            {
                                if (user.CompanyType == Types.Vendors.ToString())
                                {
                                    e.DetailTableView.DataSource = RetrieveSpecificTypeOfUsers(userId, EnumHelper.GetDescription(Types.Resellers), session);
                                }
                                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    e.DetailTableView.DataSource = RetrieveSpecificTypeOfUsers(userId, Types.Vendors.ToString(), session);
                                }
                                else
                                {
                                    e.DetailTableView.DataSource = null;
                                    e.DetailTableView.Visible = false;
                                }
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;
                            }
                        }

                        break;
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

        protected void RdgElioUsers_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        #region Buttons

        protected void aBtnAddById_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                UcMessageControlAddConnections.Visible = false;

                if (RtbxUserId.Text != "")
                {
                    if (TbxConnectionUserId.Text.Trim() != "")
                    {
                        //string ids = "";
                        //List<string> partnerIDs = TbxConnectionUserId.Text.Trim().Split(',').ToList();
                        //foreach (string id in partnerIDs)
                        //{
                        //    if (id != "")
                        //        ids += "'" + id + "',";
                        //}

                        if (TbxConnectionUserId.Text.EndsWith(","))
                            TbxConnectionUserId.Text = TbxConnectionUserId.Text.Substring(0, TbxConnectionUserId.Text.Length - 1);

                        List<ElioUsers> users = Sql.GetUsersByIds(TbxConnectionUserId.Text, session);

                        AddConnectionsFromModal(Convert.ToInt32(RtbxUserId.Text), users);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, type user IDs to be added as connections", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, select only one user ID to add connections to", MessageTypes.Error, true, true, false, false, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CommitTransaction();
                session.CloseConnection();
            }
        }

        protected void aBtnAddByCompanyEmail_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                if (RtbxUserId.Text != "")
                {
                    if (TbxConnectionCompanyEmail.Text.Trim() != "")
                    {
                        string emails = "";
                        List<string> partnerEmails = TbxConnectionCompanyEmail.Text.Trim().Split(',').ToList();
                        foreach (string email in partnerEmails)
                        {
                            if (email != "")
                                emails += "'" + email + "',";
                        }

                        if (emails.EndsWith(","))
                            emails = emails.Substring(0, emails.Length - 1);

                        List<ElioUsers> users = Sql.GetUsersByCompaniesEmail(emails, session);

                        AddConnectionsFromModal(Convert.ToInt32(RtbxUserId.Text), users);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, type user emails to be added as connections", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, select only one user ID to add connections to", MessageTypes.Error, true, true, false, false, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CommitTransaction();
                session.CloseConnection();
            }
        }

        protected void aBtnAddByCompanyName_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                if (RtbxUserId.Text != "")
                {
                    if (TbxConnectionCompanyName.Text != "")
                    {
                        string names = "";
                        List<string> partnerNames = TbxConnectionCompanyName.Text.Split(',').ToList();
                        foreach (string name in partnerNames)
                        {
                            if (name != "")
                                names += "'" + name + "',";
                        }

                        if (names.EndsWith(","))
                            names = names.Substring(0, names.Length - 1);

                        List<ElioUsers> users = Sql.GetUsersByCompaniesName(names, session);

                        AddConnectionsFromModal(Convert.ToInt32(RtbxUserId.Text), users);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, type user company names to be added as connections", MessageTypes.Error, true, true, false, false, false);
                        return;
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlAddConnections, "Please, select only one user ID to add connections to", MessageTypes.Error, true, true, false, false, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CommitTransaction();
                session.CloseConnection();
            }
        }

        protected void RbtnAddConnections_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (RtbxPacketStatusUserId.Text.Trim() != string.Empty && Convert.ToInt32(RtbxPacketStatusUserId.Text) > 0)
                {
                    if (RtbxPacketStatusConnections.Text.Trim() != string.Empty && Convert.ToInt32(RtbxPacketStatusConnections.Text) >= 0)
                    {
                        if (RdpStartingDate.SelectedDate != null && RdpExpirationDate.SelectedDate != null)
                        {
                            ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(Convert.ToInt32(RtbxPacketStatusUserId.Text), session);
                            if (userPacketStatus != null)
                            {
                                userPacketStatus.AvailableConnectionsCount = Convert.ToInt32(RtbxPacketStatusConnections.Text);
                                userPacketStatus.StartingDate = RdpStartingDate.SelectedDate;
                                userPacketStatus.ExpirationDate = RdpExpirationDate.SelectedDate;
                                userPacketStatus.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                                loader.Update(userPacketStatus);
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, string.Format("Coonections could not be added beacuse user with ID {0} has no packet status features. Fatal Error...", RtbxPacketStatusUserId.Text), MessageTypes.Error, true, true, false);
                                return;
                            }

                            RdgElioUsers.Rebind();

                            GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Connections added Successfully to User", MessageTypes.Success, true, true, false);

                            ResetPacketStatusFields();
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Add valid Dates", MessageTypes.Error, true, true, false);
                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Add valid Connections Count", MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Add valid User Id to Add Connections To", MessageTypes.Error, true, true, false);
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
        
        protected void CbxSelectUser_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                CheckBox cbx = (CheckBox)sender;
                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                int userId = Convert.ToInt32(item["id"].Text);
                int connectionId = Convert.ToInt32(item["connection_id"].Text);

                cbx.Checked = AddUserConnections(item, userId, connectionId, cbx.Checked, session);

                session.CommitTransaction();

                #region to delete

                //if (cbx.Checked)
                //{
                //if (user.CompanyType == Types.Vendors.ToString())
                //{
                //    ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                //    if (vendorPacketFeatures != null)
                //    {
                //        if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                //        {
                //            if (user.BillingType == Convert.ToInt32(BillingType.Premium))
                //            {
                //                ElioBillingUserOrders order = Sql.GetUserOrderByStatusAndUse(user.Id, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                //                if (order != null)
                //                {
                //                    ElioUsersConnections vendorConnection = new ElioUsersConnections();

                //                    vendorConnection.UserId = userId;
                //                    vendorConnection.ConnectionId = connectionId;
                //                    vendorConnection.SysDate = DateTime.Now;
                //                    vendorConnection.LastUpdated = DateTime.Now;
                //                    vendorConnection.CanBeViewed = 1;
                //                    vendorConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                //                    vendorConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);
                //                    vendorConnection.Status = true;

                //                    DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                //                    loader.Insert(vendorConnection);

                //                    vendorPacketFeatures.AvailableConnectionsCount--;
                //                    vendorPacketFeatures.LastUpdate = DateTime.Now;

                //                    DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                //                    loader1.Update(vendorPacketFeatures);

                //                    item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                //                    item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                //                    imgStatus.Visible = true;
                //                    imgStatus.ImageUrl = "~/images/icons/small/success.png";
                //                }
                //            }
                //        }
                //        else
                //        {
                //            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "User has no more available connections for current period!", MessageTypes.Warning, true, true, false);
                //            cbx.Checked = false;

                //            return;
                //        }
                //    }

                //    ElioUsers reseller = Sql.GetUserById(connectionId, session);
                //    if (reseller != null)
                //    {
                //        if (reseller.BillingType == Convert.ToInt32(BillingType.Premium))
                //        {
                //            ElioBillingUserOrders order = Sql.GetUserOrderByStatusAndUse(reseller.Id, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                //            if (order != null)
                //            {
                //                ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(reseller.Id, session);
                //                if (resellerPacketFeatures != null)
                //                {
                //                    if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                //                    {
                //                        ElioUsersConnections resellerConnection = new ElioUsersConnections();

                //                        resellerConnection.UserId = connectionId;
                //                        resellerConnection.ConnectionId = userId;
                //                        resellerConnection.SysDate = DateTime.Now;
                //                        resellerConnection.LastUpdated = DateTime.Now;
                //                        resellerConnection.CanBeViewed = 1;
                //                        resellerConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                //                        resellerConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);
                //                        resellerConnection.Status = true;

                //                        DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                //                        loader.Insert(resellerConnection);

                //                        resellerPacketFeatures.AvailableConnectionsCount--;
                //                        resellerPacketFeatures.LastUpdate = DateTime.Now;

                //                        DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                //                        loader1.Update(resellerPacketFeatures);
                //                    }
                //                    else
                //                    {
                //                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "User has no more available connections for current period!", MessageTypes.Warning, true, true, false);
                //                        cbx.Checked = false;

                //                        return;
                //                    }
                //                }
                //            }
                //        }
                //    }

                //}
                //else
                //{
                //ElioUsersConnections connection = Sql.GetConnection(userId, connectionId, session);

                //if (connection != null)
                //{
                //    if (connection.CurrentPeriodStart >= packetFeatures.StartingDate && connection.CurrentPeriodEnd <= Convert.ToDateTime(packetFeatures.StartingDate).AddMonths(1))
                //    {
                //        packetFeatures.AvailableConnectionsCount++;
                //        packetFeatures.LastUpdate = DateTime.Now;

                //        DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                //        loader.Update(packetFeatures);
                //    }

                //    DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                //    loader1.Delete(connection);
                //}

                //item["current_period_start"].Text = "-";
                //item["current_period_end"].Text = "-";
                //imgStatus.Visible = true;
                //imgStatus.ImageUrl = "~/images/icons/small/error.png";
                //}

                #endregion
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RbtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.SearchQueryString = string.Empty;

                vSession.SearchQueryString = "SELECT * FROM Elio_users WHERE (1=1) ";
                bool hasSelectedCriteria = false;

                if (RcbxCategory.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND company_type='" + RcbxCategory.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxStatus.SelectedValue != "-1")
                {
                    vSession.SearchQueryString += " AND account_status=" + RcbxStatus.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxIsPublic.SelectedValue != "-1")
                {
                    vSession.SearchQueryString += " AND is_public=" + RcbxIsPublic.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxVendorsCompanyName.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND company_name='" + RcbxVendorsCompanyName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxResellersCompanyName.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND company_name='" + RcbxResellersCompanyName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxThirdPartyCompanyName.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND company_name='" + RcbxThirdPartyCompanyName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyName.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND company_name LIKE '" + RtbxCompanyName.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyEmail.Text.Trim() != string.Empty)
                {
                    string[] emails = RtbxCompanyEmail.Text.Trim().Split(',').ToArray();
                    if (emails.Length > 1)
                    {
                        string companyEmails = string.Join("','", emails);

                        vSession.SearchQueryString += " AND email IN ('" + companyEmails + "') ";
                    }
                    else
                    {
                        vSession.SearchQueryString += " AND email LIKE '" + RtbxCompanyEmail.Text.Trim() + "%' ";
                    }

                    hasSelectedCriteria = true;
                }
                if (RcbxBillingType.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND billing_type='" + RcbxBillingType.SelectedValue + "'";
                    hasSelectedCriteria = true;
                }
                if (RcbxApplicationType.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND user_application_type='" + RcbxApplicationType.SelectedValue + "'";
                    hasSelectedCriteria = true;
                }
                if (RtbxUserId.Text.Trim() != string.Empty)
                {
                    string ids = Validations.ReturnValidIdsWithCommaDelimiterForSearch(RtbxUserId.Text.Trim());
                    if (!string.IsNullOrEmpty(ids))
                    {
                        vSession.SearchQueryString += " AND id IN (" + ids + ")";
                        hasSelectedCriteria = true;
                    }
                }
                if (RtbxStripeCustomerId.Text != string.Empty)
                {
                    vSession.SearchQueryString += " AND customer_stripe_id='" + RtbxStripeCustomerId.Text + "'";
                    hasSelectedCriteria = true;
                }
                if (RdpRegDateFrom.SelectedDate != null)
                {
                    DateTime date = Convert.ToDateTime(RdpRegDateFrom.SelectedDate);
                    string year = date.Year.ToString();
                    string month = date.Month.ToString();
                    string day = date.Day.ToString();

                    vSession.SearchQueryString += " AND sysdate >= cast('" + year + "-" + month + "-" + day + "' as datetime)";
                }
                if (RdpRegDateTo.SelectedDate != null)
                {
                    DateTime date = Convert.ToDateTime(RdpRegDateTo.SelectedDate);
                    string year = date.Year.ToString();
                    string month = date.Month.ToString();
                    string day = date.Day.ToString();

                    vSession.SearchQueryString += " AND sysdate <= cast('" + year + "-" + month + "-" + day + "' as datetime)";
                }

                vSession.SearchQueryString += " ORDER BY id";

                if (hasSelectedCriteria)
                {
                    RdgElioUsers.Rebind();
                }
                else
                {
                    vSession.SearchQueryString = string.Empty;
                    RdgElioUsers.Rebind();
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

        protected void RbtnReset_OnClick(object sender, EventArgs args)
        {
            try
            {
                RcbxCategory.SelectedValue = "0";
                RcbxStatus.SelectedValue = "-1";
                RcbxIsPublic.SelectedValue = "-1";
                RcbxVendorsCompanyName.SelectedValue = "0";
                RcbxResellersCompanyName.SelectedValue = "0";
                RcbxThirdPartyCompanyName.SelectedValue = "0";
                RtbxCompanyName.Text = string.Empty;
                RtbxCompanyEmail.Text = string.Empty;
                RcbxBillingType.SelectedValue = "0";
                RtbxUserId.Text = string.Empty;
                RtbxStripeCustomerId.Text = string.Empty;
                RcbxApplicationType.SelectedValue = "0";
                RtbxUserToAssignRole.Text = string.Empty;
                RdpRegDateFrom.SelectedDate = null;
                RdpRegDateTo.SelectedDate = null;
                CbxRoles.Items.Clear();

                ResetPacketStatusFields();

                vSession.SearchQueryString = string.Empty;

                RdgElioUsers.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnEditCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    if (item != null)
                        FixUsersGrid(item, false, false);
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went terribly wrong and you can not edit company. Try again later", MessageTypes.Warning, true, true, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        protected void ImgBtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                FixUsersGrid(item, true, true);
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

        protected void ImgBtnSaveChanges_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                FixUsersGrid(item, true, false);

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Warning, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnLoginAsCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
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

                    vSession.User = user;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        protected void ImgBtnPreviewCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                vSession.ElioCompanyDetailsView =null;

                if (vSession.User != null)
                {
                    vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        if (vSession.Page != string.Empty)
                        {
                            Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
                        }
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

        protected void ImgBtnSendEmail_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
                    List<string> emails = new List<string>();
                    emails.Add(user.Email);
                    if (!string.IsNullOrEmpty(user.OfficialEmail))
                        emails.Add(user.OfficialEmail);

                    try
                    {
                        EmailSenderLib.SendNotificationEmailToNotFullRegisteredUser(user, emails, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
                    }

                    Sql.InsertUserNotificationEmailsStatistics(user.Id, vSession.User.Id, session);

                    RdgElioUsers.Rebind();

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", user.Email);
                    if (!string.IsNullOrEmpty(user.OfficialEmail))
                    {
                        alert += Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "5")).Text + " " + user.OfficialEmail;
                    }

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgGetUserRoles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageControlCriteria.Visible = false;

                bool isError = false;

                if (RtbxUserToAssignRole.Text.Trim() == string.Empty)
                    isError = true;

                if (isError)
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "You must type User Id to get his roles", MessageTypes.Error, true, true, false);
                    return;
                }

                LoadRoles();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }
         
        protected void ImgBtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageControlCriteria.Visible = false;
                bool hasSelectRole = false;

                if (RtbxUserToAssignRole.Text != string.Empty)
                {
                    foreach (ListItem item in CbxRoles.Items)
                    {
                        bool hasRole = Sql.HasRoleByDescription(Convert.ToInt32(RtbxUserToAssignRole.Text), item.Text, session);

                        if (item.Selected)
                        {
                            if (!hasRole)
                            {
                                DataLoader<ElioUsersRoles> loader = new DataLoader<ElioUsersRoles>(session);
                                ElioUsersRoles newRole = new ElioUsersRoles();

                                newRole.ElioRoleId = Convert.ToInt32(item.Value);
                                newRole.UserId = Convert.ToInt32(RtbxUserToAssignRole.Text);
                                newRole.Sysdate = DateTime.Now;
                                newRole.IsActive = 1;

                                loader.Insert(newRole);
                            }
                        }
                        else
                        {
                            if (hasRole)
                            {
                                session.GetDataTable("DELETE from Elio_users_roles where elio_role_id=@elio_role_id and user_id=@user_id"
                                                    , DatabaseHelper.CreateIntParameter("@elio_role_id", Convert.ToInt32(item.Value))
                                                    , DatabaseHelper.CreateIntParameter("@user_id", Convert.ToInt32(RtbxUserToAssignRole.Text)));
                            }
                        }

                        hasSelectRole = true;
                    }

                    LoadRoles();

                    string alert = (hasSelectRole) ? "You have updated user roles successfully" : "You haven't select any role to add to the user";
                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, alert, (hasSelectRole) ? MessageTypes.Success : MessageTypes.Error, true, true, false);
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "You must type User Id to save any role", MessageTypes.Error, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void ImgBtnGetClearBitData_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
                    //ElioUsersPerson elioPerson = new ElioUsersPerson();
                    //ElioUsersPersonCompanies eliocompany = new ElioUsersPersonCompanies();
                    //List<ElioUsersPersonCompanyPhoneNumbers> phones = new List<ElioUsersPersonCompanyPhoneNumbers>();
                    //List<ElioUsersPersonCompanyTags> tags = new List<ElioUsersPersonCompanyTags>();

                    //string emailAddress = (!string.IsNullOrEmpty(user.Email)) ? user.Email : (!string.IsNullOrEmpty(user.OfficialEmail)) ? user.OfficialEmail : "";
                    bool success = false;

                    if (user.Email != "")
                        //success = ClearBit.FindCombinedPersonCompanyByEmail(emailAddress, session, out elioPerson, out eliocompany, out phones, out tags, out user);
                        success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, user.Email, session);
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Specific user has no email addresses available", MessageTypes.Error, true, true, false);
                        return;
                    }

                    RdgElioUsers.Rebind();

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, (success) ? "Data saved successfully from clearbit for user with email:" + user.Email : "Data could not be saved successfully from clearbit for user with email:" + user.Email + ". Try again later!", (success) ? MessageTypes.Success : MessageTypes.Error, true, true, false);
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

        protected void RbtnGetClearbitData_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                bool success = false;
                int topUsers = 10;
                List<UserEmailAddress> userEmailAddresses = new List<UserEmailAddress>();
                List<UserEmailAddress> userEmailAddressesWithError = new List<UserEmailAddress>();

                if (RtbxCompanyEmail.Text.Trim() == "")
                {
                    List<ElioUsers> users = Sql.GetTopUsersByApplicationTypeAndStatus((int)UserApplicationType.ThirdParty, (int)AccountPublicStatus.IsNotPublic, (int)AccountStatus.NotCompleted, topUsers, session);

                    if (users.Count > 0)
                    {
                        foreach (ElioUsers user in users)
                        {
                            string emailAddress = (!string.IsNullOrEmpty(user.Email)) ? user.Email : (!string.IsNullOrEmpty(user.OfficialEmail)) ? user.OfficialEmail : "";

                            if (emailAddress != "")
                                success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, emailAddress, session);
                            else
                            {
                                Logger.DetailedError(Request.Url.ToString(), "Clearbit data could not be found for user ID:" + user.Id.ToString(), "Error from clearbit API. User email could not be found");
                            }

                            if (!success)
                            {
                                UserEmailAddress address = new UserEmailAddress();
                                address.Email = emailAddress;

                                userEmailAddressesWithError.Add(address);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Data for user ID: " + user.Id.ToString() + " and email address: " + emailAddress + " could not be saved successfully from clearbit. Escaping...", MessageTypes.Error, true, true, false);
                                //break;
                            }
                            else
                            {
                                UserEmailAddress address = new UserEmailAddress();
                                address.Email = emailAddress;

                                userEmailAddresses.Add(address);
                            }
                        }

                        if (userEmailAddresses.Count > 0)
                        {
                            string addressesWithNoError = string.Join(", ", userEmailAddresses.Select(p => p.Email.ToString()));

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Clearbit data saved successfully for " + topUsers + "users and for emails: " + addressesWithNoError, MessageTypes.Success, true, true, false);
                        }

                        if (userEmailAddressesWithError.Count > 0)
                        {
                            string addressesWithError = string.Join(", ", userEmailAddressesWithError.Select(p => p.Email.ToString()));

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertWarning, "Clearbit data could not be saved for emails: " + addressesWithError, MessageTypes.Error, true, true, false);
                        }
                    }
                }
                else
                {
                    string[] emails = RtbxCompanyEmail.Text.Trim().Split(',').ToArray();
                    if (emails.Length > 1)
                    {
                        //string companyEmails = string.Join("','", emails);

                        foreach (string email in emails)
                        {
                            ElioUsers user = Sql.GetUserByEmail(email, session);
                            if (user != null)
                            {
                                string emailAddress = (!string.IsNullOrEmpty(user.Email)) ? user.Email : (!string.IsNullOrEmpty(user.OfficialEmail)) ? user.OfficialEmail : "";
                                if (emailAddress != "")
                                    success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, emailAddress, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), "Email could not be found for user ID:" + user.Id.ToString(), "Error from clearbit API. User email could not be found");

                                if (!success)
                                {
                                    UserEmailAddress address = new UserEmailAddress();
                                    address.Email = emailAddress;

                                    userEmailAddressesWithError.Add(address);
                                }
                                else
                                {
                                    UserEmailAddress address = new UserEmailAddress();
                                    address.Email = emailAddress;

                                    userEmailAddresses.Add(address);
                                }
                            }
                        }

                        if (userEmailAddresses.Count > 0)
                        {
                            string addressesWithNoError = string.Join(", ", userEmailAddresses.Select(p => p.Email.ToString()));

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Clearbit data saved successfully for " + topUsers + "users and for emails: " + addressesWithNoError, MessageTypes.Success, true, true, false);
                        }

                        if (userEmailAddressesWithError.Count > 0)
                        {
                            string addressesWithError = string.Join(", ", userEmailAddressesWithError.Select(p => p.Email.ToString()));

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertWarning, "Clearbit data could not be saved for emails: " + addressesWithError, MessageTypes.Error, true, true, false);
                        }
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

        protected void ImgBtnDeleteUser_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (vSession.User != null)
                {
                    HdnUserId.Value = "0";
                    HdnUserId.Value = item["id"].Text;

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            UcMessageAlert.Visible = false;
            HdnUserId.Value = "0";

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);
        }

        protected void BtnDeleteConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!string.IsNullOrEmpty(HdnUserId.Value) && Convert.ToInt32(HdnUserId.Value) > 0)
                    {
                        ElioUsers user = Sql.GetUserById(Convert.ToInt32(HdnUserId.Value), session);
                        if (user != null)
                        {
                            //if (!Sql.IsUserDeleted(user.Id, session))
                            //{
                            try
                            {
                                session.BeginTransaction();

                                #region Delete User

                                GlobalDBMethods.DeleteUser(user, session);

                                #region Insert User to table with deleted users

                                //ElioUsersDeleted deletedUser = new ElioUsersDeleted();

                                //deletedUser.UserId = user.Id;
                                //deletedUser.Username = user.Username;
                                //deletedUser.UsernameEncrypted = user.UsernameEncrypted;
                                //deletedUser.Password = user.Password;
                                //deletedUser.PasswordEncrypted = user.PasswordEncrypted;
                                //deletedUser.SysDate = user.SysDate;
                                //deletedUser.LastUpdated = user.LastUpdated;
                                //deletedUser.LastLogin = user.LastLogin;
                                //deletedUser.UserLoginCount = user.UserLoginCount;
                                //deletedUser.Ip = user.Ip;
                                //deletedUser.Phone = user.Phone;
                                //deletedUser.Address = user.Address;
                                //deletedUser.Country = user.Country;
                                //deletedUser.WebSite = user.WebSite;
                                //deletedUser.AccountStatus = user.AccountStatus;
                                //deletedUser.Overview = user.Overview;
                                //deletedUser.Description = user.Description;
                                //deletedUser.CompanyLogo = user.CompanyLogo;
                                //deletedUser.CompanyName = user.CompanyName;
                                //deletedUser.CompanyType = user.CompanyType;
                                //deletedUser.OfficialEmail = user.OfficialEmail;
                                //deletedUser.FeaturesNo = user.FeaturesNo;
                                //deletedUser.MashapeUsername = user.MashapeUsername;
                                //deletedUser.LastName = user.LastName;
                                //deletedUser.FirstName = user.FirstName;
                                //deletedUser.PersonalImage = user.PersonalImage;
                                //deletedUser.Position = user.Position;
                                //deletedUser.Email = user.Email;
                                //deletedUser.GuId = user.GuId;
                                //deletedUser.IsPublic = user.IsPublic;
                                //deletedUser.CustomerStripeId = user.CustomerStripeId;
                                //deletedUser.VendorProductDemoLink = user.VendorProductDemoLink;
                                //deletedUser.CommunityStatus = user.CommunityStatus;
                                //deletedUser.CommunityProfileCreated = user.CommunityProfileCreated;
                                //deletedUser.CommunityProfileLastUpdated = user.CommunityProfileLastUpdated;
                                //deletedUser.CommunitySummaryText = user.CommunitySummaryText;
                                //deletedUser.LinkedInUrl = user.LinkedInUrl;
                                //deletedUser.LinkedInUrl = user.LinkedinId;
                                //deletedUser.TwitterUrl = user.TwitterUrl;
                                //deletedUser.HasBillingDetails = user.HasBillingDetails;
                                //deletedUser.BillingType = user.BillingType;
                                //deletedUser.UserApplicationType = user.UserApplicationType;
                                //deletedUser.DateDeleted = DateTime.Now;
                                //deletedUser.ActionByUserId = vSession.User.Id;

                                //GlobalDBMethods.InsertDeletedUser(deletedUser, session);

                                #endregion

                                #endregion

                                #region Delete User From Team Account

                                ElioUsersSubAccounts subAccount = Sql.GetMasterUserSubAccountByEmailAndUserId(user.Email, user.Id, session);
                                if (subAccount != null)
                                {
                                    DataLoader<ElioUsersSubAccounts> subLoader = new DataLoader<ElioUsersSubAccounts>(session);
                                    subLoader.Delete(subAccount);
                                }

                                #endregion

                                #region Update User credentials

                                //user.Email = "deleted_" + user.Email;
                                //user.OfficialEmail = (!string.IsNullOrEmpty(user.OfficialEmail)) ? "deleted_" + user.OfficialEmail : "";
                                //user.Username = "deleted_" + user.Username;
                                //user.Password = "deleted_" + user.Password;
                                //user.UsernameEncrypted = MD5.Encrypt(user.Username);
                                //user.Password = MD5.Encrypt(user.Password);
                                //user.IsPublic = (int)AccountPublicStatus.IsNotPublic;

                                //GlobalDBMethods.UpDateUser(user, session);
                                ////GlobalDBMethods.DeleteUser(user, session);

                                #endregion

                                #region Delete Connections

                                if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    List<ElioUsersConnections> connections = Sql.GetConnectionsByReseller(user.Id, session);

                                    int connectionsCount = connections.Count;
                                    foreach (ElioUsersConnections connection in connections)
                                    {
                                        ElioUsers vendor = Sql.GetUserById(connection.UserId, session);
                                        if (vendor != null)
                                        {
                                            if (vendor.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vendor.CompanyType == Types.Vendors.ToString())
                                            {
                                                ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(vendor.Id, session);
                                                if (vendorPacketFeatures != null)
                                                {
                                                    #region Increase Vendor Available Connections Counter

                                                    vendorPacketFeatures.AvailableConnectionsCount++;
                                                    vendorPacketFeatures.LastUpdate = DateTime.Now;

                                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                                    loader.Update(vendorPacketFeatures);

                                                    #endregion
                                                }

                                                #region Vendor Delete Connection

                                                Sql.DeleteConnection(vendor.Id, user.Id, session);

                                                #endregion
                                            }
                                        }
                                    }

                                    //ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                                    //if (resellerPacketFeatures != null)
                                    //{
                                    #region Increase Reseller Available Connections Counter

                                    //resellerPacketFeatures.AvailableConnectionsCount += connectionsCount;
                                    //resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    //DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                                    //loader.Update(resellerPacketFeatures);

                                    #endregion

                                    #region Delete channel partner packet features

                                    Sql.DeleteUserPacketStatusFeatures(user.Id, session);

                                    //DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                                    //loader.Delete(resellerPacketFeatures);

                                    #endregion

                                    //}

                                    #region Reseller Delete Connection

                                    Sql.DeleteConnectionByUser(user.Id, session);

                                    #endregion
                                }
                                else
                                {
                                    #region Delete Vendor packet features

                                    Sql.DeleteUserPacketStatusFeatures(user.Id, session);

                                    #endregion
                                }

                                #endregion

                                #region Is ThirdParty User

                                if (user.UserApplicationType == (int)UserApplicationType.ThirdParty)
                                {
                                    #region Has Person Data

                                    if (ClearbitSql.ExistsClearbitPerson(user.Id, session))
                                    {
                                        int personCount = ClearbitSql.GetPersonsCountByUserId(user.Id, session);

                                        if (personCount == 1)
                                        {
                                            #region Exists once Clearbit Person - Delete

                                            ElioUsersPerson person = ClearbitSql.GetPersonByUserId(user.Id, session);
                                            if (person != null)
                                            {
                                                person.IsPublic = (int)AccountPublicStatus.IsNotPublic;
                                                person.IsActive = (int)ActiveStatus.IsNotActive;
                                                person.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
                                                loader.Delete(person);
                                                //loader.Update(person);
                                            }

                                            #endregion
                                        }
                                        else if (personCount > 1)
                                        {
                                            #region More than once Clearbit Person

                                            List<ElioUsersPerson> persons = ClearbitSql.GetAllPersonsByUserId(user.Id, session);

                                            foreach (ElioUsersPerson person in persons)
                                            {
                                                person.IsPublic = (int)AccountPublicStatus.IsNotPublic;
                                                person.IsActive = (int)ActiveStatus.IsNotActive;
                                                person.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
                                                loader.Delete(person);
                                                //loader.Update(person);
                                            }

                                            #endregion
                                        }
                                    }

                                    #endregion

                                    #region Has Person Company Data - Delete

                                    if (ClearbitSql.ExistsClearbitCompany(user.Id, session))
                                    {
                                        ElioUsersPersonCompanies company = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
                                        if (company != null)
                                        {
                                            company.IsPublic = (int)AccountPublicStatus.IsNotPublic;
                                            company.IsActive = (int)ActiveStatus.IsNotActive;
                                            company.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUsersPersonCompanies> loader = new DataLoader<ElioUsersPersonCompanies>(session);
                                            loader.Delete(company);
                                            //loader.Update(company);
                                        }
                                    }

                                    #endregion
                                }

                                #endregion

                                #region Delete User Email Notifications Settings

                                Sql.DeleteUserEmailNotificationSettingsAll(user.Id, session);

                                #endregion

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }
                            //}
                            //else
                            //{
                            //    try
                            //    {
                            //        session.BeginTransaction();

                            #region Delete DeletedUser / User

                            //ElioUsersDeleted deletedUser = Sql.GetUserDeletedById(Convert.ToInt32(HdnUserId.Value), session);
                            //if (deletedUser != null)
                            //{
                            //    DataLoader<ElioUsersDeleted> loader = new DataLoader<ElioUsersDeleted>(session);
                            //    loader.Delete(deletedUser);
                            //}

                            //GlobalDBMethods.DeleteUser(user, session);

                            //user.Email = "deleted_" + user.Email;
                            //user.OfficialEmail = (!string.IsNullOrEmpty(user.OfficialEmail)) ? "deleted_" + user.OfficialEmail : "";
                            //user.Username = "deleted_" + user.Username;
                            //user.Password = "deleted_" + user.Password;
                            //user.UsernameEncrypted = MD5.Encrypt(user.Username);
                            //user.Password = MD5.Encrypt(user.Password);
                            //user.IsPublic = (int)AccountPublicStatus.IsNotPublic;

                            //GlobalDBMethods.UpDateUser(user, session);

                            #endregion

                            //session.CommitTransaction();
                            //    }
                            //    catch (Exception ex)
                            //    {
                            //        session.RollBackTransaction();
                            //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            //        throw ex;
                            //    }
                            //}

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);
                            HdnUserId.Value = "0";

                            RdgElioUsers.Rebind();

                            string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "10")).Text;

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
                        }
                        else
                        {
                            #region User could not be find for Edit

                            string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "9")).Text;

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);

                            #endregion
                        }
                    }
                    else
                    {
                        #region User could not be find for Edit

                        string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "9")).Text;

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);

                        #endregion
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "12")).Text;

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCloseSearchConnectionPartnerModal_Click(object sender, EventArgs e)
        {
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseSearchConnectionPartnerModal();", true);
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TbxConnectionUserId.Text = "";
                TbxConnectionCompanyName.Text = "";
                TbxConnectionCompanyEmail.Text = "";

                CollapseGridItems(RdgElioUsers);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchConnectionPartner_Click(object sender, EventArgs e)
        {
            try
            {
                CollapseGridItems(RdgElioUsers);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseSearchConnectionPartnerModal();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnUpdateSubscription_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
                    bool hasUpdated = StripeApi.FixCustomerSubscriptionInvoicesNew(user, session);
                    if (hasUpdated)
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "User subscription has updated successfully", MessageTypes.Success, true, true, false);
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "User subscription isn't updated", MessageTypes.Info, true, true, false);
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

        protected void RtbxUpdateUsers_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageControlCriteria.Visible = false;

                if (RtbxFromUserID.Text != "" && RtbxToUserID.Text != "")
                {
                    //int fromID = Convert.ToInt32(RtbxFromUserID.Text);
                    //int toID = Convert.ToInt32(RtbxToUserID.Text);

                    //if (fromID < 0 || toID < 0)
                    //{
                    //    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "From and To fields must have positive IDs.", MessageTypes.Error, true, true, false, true, false);
                    //    return;
                    //}

                    //if (fromID >= toID)
                    //{
                    //    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "From field must have smaller ID from To field.", MessageTypes.Error, true, true, false, true, false);
                    //    return;
                    //}

                    //string country = RtbxToUserID.Text;

                    //if (RtbxFromUserID.Text == "1" && country == "")
                    //    Lib.Utils.ExcelLib.ReadAccessDB_updateEmptyCitiesStates(1, 10, country, session);
                    //else if (RtbxFromUserID.Text == "2" && country == "")
                    //    Lib.Utils.ExcelLib.ReadAccessDB_updateEmptyCitiesStates(1, 10, country, session);

                    //GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Users cities and metropolitan area updated successfully for country: " + country, MessageTypes.Success, true, true, false, true, false);

                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Done nothing...", MessageTypes.Success, true, true, false, true, false);
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, "Please fill From and To fields with positive IDs.", MessageTypes.Error, true, true, false, true, false);
                    return;
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

        protected void RtbxClearUsersAddressCity_Click(object sender, EventArgs e)
        {
            try
            {
                UcMessageControlCriteria.Visible = false;

                RtbxFromUserID.Text = "";
                RtbxToUserID.Text = "";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public class UserEmailAddress
        {
            public string Email { get; set; }
        }

        #endregion
    }
}