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

namespace WdS.ElioPlus.Management
{
    public partial class AdminUserManagement : Telerik.Web.UI.RadAjaxPage
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
                    UcMessageAlert.Visible = false;
                    UcStripeMessageAlert.Visible = false;
                    UcMessageControlCriteria.Visible = false;

                    UpdateStrings();

                    ShowChart();

                    if (!IsPostBack)
                    {
                        #region Load Data

                        LoadCompanyTypes();
                        LoadStatus();
                        LoadPublicStatus();
                        LoadBillingType();
                        LoadApplicationType();
                        LoadCompanies();
                        LoadDashboardData();
                        LoadRoles();

                        #endregion

                        ResetPacketStatusFields();
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
        }

        private void LoadDashboardData()
        {

        }
        
        private void UpdateStrings()
        {
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "2")).Text;

            Label5.Text = "Welcome to E.M.A. - (Elioplus Management Area)";      //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "1")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "2")).Text;
            LblStatus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "5")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "6")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "7")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "9")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "10")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "11")).Text;            
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "12")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "23")).Text;
            LblAddRole.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "13")).Text;
            LblRoles.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "14")).Text;

            LblPacketStatusUserId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "18")).Text;
            LblPacketStatusConnections.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "19")).Text;
            LblStartingDate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "20")).Text;
            LblExpirationDate.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "21")).Text;

            RdgElioUsers.MasterTableView.GetColumn("company_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "1")).Text;
            RdgElioUsers.MasterTableView.GetColumn("billing_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "36")).Text;
            RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "37")).Text;
            RdgElioUsers.MasterTableView.GetColumn("company_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "2")).Text;
            RdgElioUsers.MasterTableView.GetColumn("email").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "3")).Text;
            RdgElioUsers.MasterTableView.GetColumn("is_public").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "4")).Text;
            RdgElioUsers.MasterTableView.GetColumn("account_status").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "5")).Text;
            RdgElioUsers.MasterTableView.GetColumn("features_no").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "6")).Text;
            RdgElioUsers.MasterTableView.GetColumn("available_connections_count").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "40")).Text;
            RdgElioUsers.MasterTableView.GetColumn("last_login").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "7")).Text;
            RdgElioUsers.MasterTableView.GetColumn("login_times").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "8")).Text;
            RdgElioUsers.MasterTableView.GetColumn("count").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "9")).Text;
            RdgElioUsers.MasterTableView.GetColumn("first_send").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "10")).Text;
            RdgElioUsers.MasterTableView.GetColumn("last_send").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "11")).Text;
            RdgElioUsers.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "12")).Text;

            Label lblSearchText = (Label)ControlFinder.FindControlRecursive(RbtnSearch, "LblSearchText");
            lblSearchText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "11")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;

            Label lblAddConnectionsText = (Label)ControlFinder.FindControlRecursive(RbtnAddConnections, "LblAddConnectionsText");
            lblAddConnectionsText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "22")).Text;
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
            Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");
            RadComboBox rcbxStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxStatus");
            Label lblPublic = (Label)ControlFinder.FindControlRecursive(item, "LblPublic");
            RadComboBox rcbxPublic = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxPublic");
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail");
            RadTextBox rtbxFeature = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxFeature");
            RadComboBox rcbxBillingType = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxBillingType");
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
                    user.FeaturesNo = (string.IsNullOrEmpty(rtbxFeature.Text)) ? 0 : Convert.ToInt32(rtbxFeature.Text);
                    user.LastUpdated = DateTime.Now;
                    user.IsPublic = Convert.ToInt32(rcbxPublic.SelectedValue);
                    user.CustomerStripeId = rtbxStripeCustomerId.Text;

                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rcbxBillingType.SelectedItem.Value == Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString() && rtbxStripeCustomerId.Text == string.Empty)
                    {
                        #region Cancel Virtual Premium User

                        DateTime canceledAt = DateTime.Now;
                        ElioUsersCreditCards userCard = null;

                        try
                        {
                            session.BeginTransaction();

                            user = PaymentLib.MakeUserFreemium(user, canceledAt, userCard, session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                            GlobalMethods.ShowMessageControl(UcMessageAlert, string.Format("Virtual User with ID {0}, could not be canceled, something went wrong", user.Id.ToString()), MessageTypes.Error, true, true, false);

                            return;
                        }

                        alert = string.Format("Virtual User with ID {0}, canceled successfully. ", user.Id.ToString());

                        #endregion
                    }
                    else if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rcbxBillingType.SelectedItem.Value == Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString() && rtbxStripeCustomerId.Text != string.Empty)
                    {
                        #region Cancel Stripe Registered User

                        DateTime? canceledAt = null;
                        bool successUnsubscription = false;
                        string stripeUnsubscribeError = string.Empty;
                        string defaultCreditCard = string.Empty;

                        ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);
                        if (userCard != null)
                            defaultCreditCard = userCard.CardStripeId;

                        try
                        {
                            successUnsubscription = StripeLib.UnSubscribeCustomer(ref canceledAt, user.CustomerStripeId, defaultCreditCard, ref stripeUnsubscribeError);
                        }
                        catch (Exception ex)
                        {
                            GlobalMethods.ShowMessageControl(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        if (successUnsubscription)
                        {
                            #region Make User Fremium to Elio

                            try
                            {
                                session.BeginTransaction();

                                PaymentLib.MakeUserFreemium(user, canceledAt, userCard, session);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                                GlobalMethods.ShowMessageControl(UcMessageAlert, string.Format("Premium User with ID {0}, could not be set as Fremium, but canceled successfully from Stripe with stripe customer ID {1}, something went wrong", user.Id.ToString(), user.CustomerStripeId.ToString()), MessageTypes.Error, true, true, false);
                                return;
                            }

                            #endregion
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControl(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                            return;
                        }

                        alert = string.Format("Premium User with ID {0}, canceled and unsubscribed from Stripe too successfully. ", user.Id.ToString());

                        #endregion
                    }
                    else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text != string.Empty && rcbxBillingType.SelectedItem.Value != Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString())
                    {
                        #region Convert Elio Fremium User to Premium with already Stripe Subscription

                        DateTime? startDate = null;
                        DateTime? currentPeriodStart = null;
                        DateTime? currentPeriodEnd = null;
                        DateTime? trialPeriodStart = null;
                        DateTime? trialPeriodEnd = null;
                        DateTime? canceledAt = null;
                        string orderMode = string.Empty;
                        Xamarin.Payments.Stripe.StripeCard cardInfo = null;

                        Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, rtbxStripeCustomerId.Text);

                        if (startDate != null && ((currentPeriodStart != null && currentPeriodEnd != null) || (trialPeriodStart != null && trialPeriodEnd != null)))
                        {
                            try
                            {
                                session.BeginTransaction();

                                user = PaymentLib.MakeUserPremium(user, Convert.ToInt32(Packets.Premium), null, rtbxStripeCustomerId.Text, "", lblEmail.Text, subscription.Start, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, subscription.TrialStart, subscription.TrialEnd, orderMode, cardInfo, session);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                GlobalMethods.ShowMessageControl(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                return;
                            }
                        }

                        alert = string.Format("Fremium User with ID {0}, but with Stripe Subscription already and customer ID {1}, was converted to Premium successfully. ", user.Id.ToString(), user.CustomerStripeId.ToString());

                        #endregion
                    }
                    else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text == string.Empty && rcbxBillingType.SelectedItem.Value != Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString())
                    {
                        #region Make Virtual Premium User with no Stripe Subscription

                        try
                        {
                            session.BeginTransaction();

                            user = PaymentLib.MakeUserVirtualPremium(user, Convert.ToInt32(rcbxBillingType.SelectedItem.Value), rtbxStripeCustomerId.Text, lblEmail.Text, DateTime.Now, DateTime.Now, DateTime.Now.AddDays(14), DateTime.Now, DateTime.Now.AddDays(14), OrderMode.Trialing.ToString(), session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            GlobalMethods.ShowMessageControl(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            return;
                        }

                        alert = string.Format("User with ID {0}, was made Virtual Premium successfully. ", user.Id.ToString());

                        #endregion
                    }

                    #region Update User

                    user.BillingType = Convert.ToInt32(rcbxBillingType.SelectedItem.Value);

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
                else
                {
                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "3")).Text;
                }

                #region Fix Controls State

                lblCategory.Visible = isSaveMode;
                lblStatus.Visible = isSaveMode;
                lblPublic.Visible = isSaveMode;
                rcbxPublic.Visible = !isSaveMode;
                lblFeature.Visible = isSaveMode;
                lblBillingType.Visible = isSaveMode;
                lblStripeCustomerId.Visible = isSaveMode;
                rcbxStatus.Visible = !isSaveMode;
                rcbxCategory.Visible = !isSaveMode;
                rtbxFeature.Visible = !isSaveMode;
                rcbxBillingType.Visible = !isSaveMode;
                rtbxStripeCustomerId.Visible = !isSaveMode;
                imgBtnSaveChanges.Visible = !isSaveMode;
                imgBtnCancel.Visible = !isSaveMode;
                imgBtnEditCompany.Visible = isSaveMode;

                rtbxFeature.Text = user.FeaturesNo.ToString();
                rcbxBillingType.FindItemByValue(user.BillingType.ToString()).Selected = true;
                rtbxStripeCustomerId.Text = user.CustomerStripeId;
                rcbxStatus.FindItemByValue(user.AccountStatus.ToString()).Selected = true;
                rcbxPublic.FindItemByValue(user.IsPublic.ToString()).Selected = true;
                rcbxCategory.FindItemByText(user.CompanyType).Selected = true;

                #endregion

                #region Show Message Alert

                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false);

                #endregion

                #endregion
            }
            else
            {
                #region User could not be find for Edit

                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "7")).Text;

                RdgElioUsers.Rebind();

                GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);

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

        private void LoadCompanies()
        {
            List<ElioUsers> companies = Sql.GetAllFullRegisteredUsers(session);

            RcbxName.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "42")).Text;
            RcbxName.Items.Add(item);

            foreach (ElioUsers company in companies)
            {
                item = new RadComboBoxItem();
                item.Value = company.Id.ToString();
                item.Text = company.CompanyName;

                RcbxName.Items.Add(item);
            }
        }

        private DataTable RetrieveSpecificTypeOfUsers(int userId, string type, DBSession session)
        {
            DataTable table = new DataTable();

            List<ElioUsers> users = Sql.GetUsersByCompanyType(type, session);
            if (users.Count > 0)
            {
                table.Columns.Add("id");
                table.Columns.Add("connection_id");
                table.Columns.Add("company_name");
                table.Columns.Add("user_application_type");
                table.Columns.Add("phone");
                table.Columns.Add("address");
                table.Columns.Add("country");
                table.Columns.Add("email");
                table.Columns.Add("website");
                table.Columns.Add("sysdate");
                table.Columns.Add("last_updated");
                table.Columns.Add("current_period_start");
                table.Columns.Add("current_period_end");
                table.Columns.Add("status");

                foreach (ElioUsers user in users)
                {
                    table.Rows.Add(userId, user.Id, user.CompanyName, (user.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? UserApplicationType.Elioplus.ToString() : UserApplicationType.ThirdParty.ToString(), "", "", user.Country, user.Email, user.WebSite, "", "", "", "", "");
                }
            }
            return table;
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
                        RdgElioUsers.MasterTableView.GetColumn("company_name").Display = (RcbxName.SelectedValue != "0" || user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        //RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").Display = (RcbxBillingType.SelectedValue != "0") ? true : false;
                        RdgElioUsers.MasterTableView.GetColumn("account_status").Display = (RcbxStatus.SelectedValue != "-1") ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("company_type").Display = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("features_no").Display = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
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
                        Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");

                        ImageButton imgBtnLoginAsCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLoginAsCompany");
                        imgBtnLoginAsCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "4")).Text.Replace("{comapnyname}", (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed) ? user.CompanyName : user.Username));

                        ImageButton imgBtnPreviewCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreviewCompany");
                        imgBtnPreviewCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "5")).Text;

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
                        lblFeature.Text = user.FeaturesNo.ToString();

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
                    table.Columns.Add("last_login");
                    table.Columns.Add("login_times");
                    table.Columns.Add("count");
                    table.Columns.Add("first_send");
                    table.Columns.Add("last_send");

                    foreach (ElioUsers user in users)
                    {
                        List<ElioUsersNotificationEmails> emails = Sql.UserNotificationEmailCount(user.Id, session);

                        string firstdate = "";
                        string lastdate = "";
                        if (emails.Count > 0)
                        {
                            lastdate = emails[emails.Count - 1].NotificationEmailDate.ToString();
                            firstdate = emails[0].NotificationEmailDate.ToString();
                        }
                        else
                        {
                            lastdate = "-";
                            firstdate = "-";
                        }

                        table.Rows.Add(user.Id, (user.UserLoginCount > 0) ? user.LastLogin : user.SysDate, user.UserLoginCount, emails.Count, firstdate, lastdate);
                    }

                    RdgElioUsers.DataSource = table;
                }
                else
                {
                    RdgElioUsers.Visible = false;

                    string alert = "You have no company profiles to cover your seach criteria";
                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
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
                                    e.DetailTableView.DataSource = RetrieveSpecificTypeOfUsers(userId, EnumHelper.GetDescription(Types.Resellers).ToString(), session);
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
                                GlobalMethods.ShowMessageControl(UcMessageControlCriteria, string.Format("Coonections could not be added beacuse user with ID {0} has no packet status features. Fatal Error...", RtbxPacketStatusUserId.Text), MessageTypes.Error, true, true, false);
                                return;
                            }

                            RdgElioUsers.Rebind();

                            GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "Connections added Successfully to User", MessageTypes.Success, true, true, false);

                            ResetPacketStatusFields();
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "Add valid Dates", MessageTypes.Error, true, true, false);
                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "Add valid Connections Count", MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "Add valid User Id to Add Connections To", MessageTypes.Error, true, true, false);
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

        private bool AddUserConnections(GridDataItem item, int userId, int connectionId, bool insertConnection, DBSession session)
        {
            Image imgStatus = (Image)ControlFinder.FindControlRecursive(item, "ImgStatus");

            ElioUsers user = Sql.GetUserById(userId, session);

            if (user.CompanyType == Types.Vendors.ToString())
            {
                #region Vendor

                if (insertConnection)
                {
                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                        if (vendorPacketFeatures != null)
                        {
                            if (vendorPacketFeatures.AvailableConnectionsCount > 0)
                            {
                                ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                                if (order == null)
                                {
                                    order = Sql.HasUserOrderByServicePacketStatusUse(user.Id, Convert.ToInt32(Packets.PremiumService), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                }

                                if (order != null)
                                {
                                    ElioUsersConnections vendorConnection = new ElioUsersConnections();

                                    vendorConnection.UserId = userId;
                                    vendorConnection.ConnectionId = connectionId;
                                    vendorConnection.SysDate = DateTime.Now;
                                    vendorConnection.LastUpdated = DateTime.Now;
                                    vendorConnection.CanBeViewed = 1;
                                    vendorConnection.CurrentPeriodStart = Convert.ToDateTime(order.CurrentPeriodStart);
                                    vendorConnection.CurrentPeriodEnd = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) : Convert.ToDateTime(order.CurrentPeriodEnd).AddMonths(1);
                                    vendorConnection.Status = true;
                                    vendorConnection.IsNew = 1;

                                    DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                                    loader.Insert(vendorConnection);

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

                                    item["current_period_start"].Text = vendorConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                    item["current_period_end"].Text = vendorConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                    imgStatus.Visible = true;
                                    imgStatus.ImageUrl = "~/images/icons/small/success.png";

                                    vendorPacketFeatures.AvailableConnectionsCount--;
                                    vendorPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                    loader1.Update(vendorPacketFeatures);
                                }

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
                                                resellerPacketFeatures.AvailableConnectionsCount--;
                                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                                DataLoader<ElioUserPacketStatus> loader2 = new DataLoader<ElioUserPacketStatus>(session);
                                                loader2.Update(resellerPacketFeatures);
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControl(UcMessageAlert, "You have no available connections to add to this user", MessageTypes.Warning, true, true, false);
                                return false;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "You are not allowed to add connection to Fremium user", MessageTypes.Warning, true, true, false);
                        return false;
                    }
                }
                else
                {
                    //ElioUsersConnections vendorConnection = Sql.GetConnection(userId, connectionId, session);

                    //if (vendorConnection != null)
                    //{
                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        ElioUserPacketStatus vendorPacketFeatures = Sql.GetUserPacketStatusFeatures(userId, session);
                        if (vendorPacketFeatures != null)
                        {

                            //ElioBillingUserOrders order = Sql.GetUserOrderByStatusAndUse(user.Id, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                            //if (order != null)
                            //{
                            //if (connection.CurrentPeriodStart >= order.CurrentPeriodStart && connection.CurrentPeriodEnd <= order.CurrentPeriodEnd)
                            //{
                            vendorPacketFeatures.AvailableConnectionsCount++;
                            vendorPacketFeatures.LastUpdate = DateTime.Now;

                            DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                            loader.Update(vendorPacketFeatures);
                            //}
                            //}
                        }
                    }

                    Sql.DeleteConnection(userId, connectionId, session);
                    //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                    //loader1.Delete(vendorConnection);
                    //}

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
                                resellerPacketFeatures.AvailableConnectionsCount++;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                loader.Update(resellerPacketFeatures);
                            }
                        }
                    }

                    Sql.DeleteConnection(connectionId, userId, session);
                    //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                    //loader1.Delete(resellerConnection);
                    //}

                    item["current_period_start"].Text = "-";
                    item["current_period_end"].Text = "-";
                    imgStatus.Visible = true;
                    imgStatus.ImageUrl = "~/images/icons/small/error.png";

                    return false;
                }

                #endregion
            }
            else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                #region reseller

                if (insertConnection)
                {
                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);
                        if (resellerPacketFeatures != null)
                        {
                            if (resellerPacketFeatures.AvailableConnectionsCount > 0)
                            {
                                resellerPacketFeatures.AvailableConnectionsCount--;
                                resellerPacketFeatures.LastUpdate = DateTime.Now;

                                DataLoader<ElioUserPacketStatus> loader1 = new DataLoader<ElioUserPacketStatus>(session);
                                loader1.Update(resellerPacketFeatures);

                                ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                                if (order != null)
                                {
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

                                    DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);

                                    loader.Insert(resellerConnection);

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

                                    item["current_period_start"].Text = resellerConnection.CurrentPeriodStart.ToString("MM/dd/yyyy");
                                    item["current_period_end"].Text = resellerConnection.CurrentPeriodEnd.ToString("MM/dd/yyyy");
                                    imgStatus.Visible = true;
                                    imgStatus.ImageUrl = "~/images/icons/small/success.png";
                                }

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
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControl(UcMessageAlert, "You have no available connections to add to this user", MessageTypes.Warning, true, true, false);
                                return false;
                            }
                        }

                        return true;
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "You are not allowed to add connection to Fremium user", MessageTypes.Warning, true, true, false);
                        return false;
                    }
                }
                else
                {
                    //ElioUsersConnections connection = Sql.GetConnection(connectionId, userId, session);

                    //if (connection != null)
                    //{
                    ElioUserPacketStatus resellerPacketFeatures = Sql.GetUserPacketStatusFeatures(connectionId, session);
                    if (resellerPacketFeatures != null)
                    {
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);
                            if (order != null)
                            {
                                if (Convert.ToDateTime(item["current_period_start"].Text) >= order.CurrentPeriodStart && Convert.ToDateTime(item["current_period_end"].Text) <= order.CurrentPeriodEnd)
                                {
                                    resellerPacketFeatures.AvailableConnectionsCount++;
                                    resellerPacketFeatures.LastUpdate = DateTime.Now;

                                    DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);

                                    loader.Update(resellerPacketFeatures);
                                }
                            }
                        }
                    }

                    Sql.DeleteConnection(connectionId, userId, session);
                    //DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                    //loader1.Delete(connection);

                    item["current_period_start"].Text = "-";
                    item["current_period_end"].Text = "-";
                    imgStatus.Visible = true;
                    imgStatus.ImageUrl = "~/images/icons/small/error.png";
                    //}

                    return false;
                }

                #endregion
            }
            else
                return false;
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
                //            GlobalMethods.ShowMessageControl(UcMessageAlert, "User has no more available connections for current period!", MessageTypes.Warning, true, true, false);
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
                //                        GlobalMethods.ShowMessageControl(UcMessageAlert, "User has no more available connections for current period!", MessageTypes.Warning, true, true, false);
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
                if (RcbxName.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND company_name='" + RcbxName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyName.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND company_name LIKE '" + RtbxCompanyName.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyEmail.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND email LIKE '" + RtbxCompanyEmail.Text.Trim() + "%' ";
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
                    vSession.SearchQueryString += " AND id IN (" + Validations.ReturnValidIdsWithCommaDelimiterForSearch(RtbxUserId.Text.Trim()) + ")";
                    hasSelectedCriteria = true;
                }
                if (RtbxStripeCustomerId.Text != string.Empty)
                {
                    vSession.SearchQueryString += " AND customer_stripe_id='" + RtbxStripeCustomerId.Text + "'";
                    hasSelectedCriteria = true;
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
                RcbxName.SelectedValue = "0";
                RtbxCompanyName.Text = string.Empty;
                RtbxCompanyEmail.Text = string.Empty;
                RcbxBillingType.SelectedValue = "0";
                RtbxUserId.Text = string.Empty;
                RtbxStripeCustomerId.Text = string.Empty;
                RcbxApplicationType.SelectedValue = "0";
                RtbxUserToAssignRole.Text = string.Empty;
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
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                FixUsersGrid(item, false, false);
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

                GlobalMethods.ShowMessageControl(UcMessageAlert, ex.Message.ToString(), MessageTypes.Warning, true, true, false);
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
                        EmailSenderLib.SendNotificationEmailToNotFullRegisteredUser(user, emails, false, vSession.Lang, session);
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        GlobalMethods.ShowMessageControl(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
                    }

                    Sql.InsertUserNotificationEmailsStatistics(user.Id, vSession.User.Id, session);

                    RdgElioUsers.Rebind();

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "4")).Text.Replace("{companyemail}", user.Email);
                    if (!string.IsNullOrEmpty(user.OfficialEmail))
                    {
                        alert += Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "5")).Text + " " + user.OfficialEmail;
                    }

                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControl(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
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
                    GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "You must type User Id to get his roles", MessageTypes.Error, true, true, false);
                    return;
                }

                LoadRoles();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControl(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
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
                    GlobalMethods.ShowMessageControl(UcMessageControlCriteria, alert, (hasSelectRole) ? MessageTypes.Success : MessageTypes.Error, true, true, false);
                }
                else
                {
                    GlobalMethods.ShowMessageControl(UcMessageControlCriteria, "You must type User Id to save any role", MessageTypes.Error, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                GlobalMethods.ShowMessageControl(UcMessageAlert, ex.Message.ToString(), MessageTypes.Error, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}