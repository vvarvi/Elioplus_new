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
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Management
{
    public partial class AdminDemoRequestsManagement : Telerik.Web.UI.RadAjaxPage
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
                    UcMessageAlertApproved.Visible = false;
                    UcStripeMessageAlert.Visible = false;
                    UcMessageControlCriteria.Visible = false;

                    UpdateStrings();

                    //ShowChart();

                    if (!IsPostBack)
                    {
                        #region Load Data

                        LoadCompanyTypes();
                        LoadStatus();
                        LoadPublicStatus();
                        LoadBillingType();
                        LoadApplicationType();
                        //LoadCompanies();

                        vSession.SearchQueryString = "";
                        RcbxApproved.SelectedValue = "-2";

                        #endregion
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

        private void UpdateStrings()
        {
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "2")).Text;

            Label5.Text = "Elioplus companies which have requested demo";      //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "1")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "2")).Text;
            LblStatus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "5")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "6")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "7")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "9")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "10")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "12")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "23")).Text;
            Label4.Text = "Request Status";
            Label lblSearchText = (Label)ControlFinder.FindControlRecursive(RbtnSearch, "LblSearchText");
            lblSearchText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "11")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;
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

            RdgElioUsersDemoRequests.MasterTableView.GetColumn("billing_type").Display = true;
            RdgElioUsersDemoRequests.MasterTableView.GetColumn("stripe_customer_id").Display = true;

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

                    RdgElioUsersDemoRequests.Rebind();

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

                RdgElioUsersDemoRequests.Rebind();

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

        #endregion

        #region Grids

        protected void RdgElioUsersApprovedDemoRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aSuccess = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSuccess");
                    
                    aSuccess.Visible = true;

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

        protected void RdgElioUsersApprovedDemoRequests_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //List<ElioUsersDemoRequestsIJUsers> demos = new List<ElioUsersDemoRequestsIJUsers>();
                DataTable table = null;

                if (vSession.SearchQueryString == "")
                    //demos = Sql.GetUsersDemoRequestsIJUsers(1, session);
                    table = Sql.GetUsersDemoRequestsIJUsersTbl(1, session);
                else
                {
                    //DataLoader<ElioUsersDemoRequestsIJUsers> loader = new DataLoader<ElioUsersDemoRequestsIJUsers>(session);
                    //demos = loader.Load(vSession.SearchQueryString);
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                //if (demos.Count > 0)
                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersApprovedDemoRequests.Visible = true;
                    UcMessageAlertApproved.Visible = false;

                    //DataTable table = new DataTable();

                    //table.Columns.Add("id");
                    //table.Columns.Add("request_for_user_id");
                    //table.Columns.Add("company_name");
                    //table.Columns.Add("company_email");
                    //table.Columns.Add("first_name");
                    //table.Columns.Add("last_name");
                    //table.Columns.Add("company_size");
                    //table.Columns.Add("sysdate");
                    //table.Columns.Add("is_approved");
                    //table.Columns.Add("date_approved");
                    //table.Columns.Add("demo_company_name");
                    //table.Columns.Add("demo_company_email");

                    //foreach (ElioUsersDemoRequestsIJUsers request in demos)
                    //{
                    //    table.Rows.Add(request.Id, request.RequestForUserId, request.CompanyName, request.CompanyEmail, request.FirstName, request.LastName,
                    //        request.CompanySize, request.Sysdate.ToString("dd/MM/yyyy"), request.IsApproved, request.DateApproved, request.DemoCompanyName, request.DemoCompanyEmail);
                    //}

                    RdgElioUsersApprovedDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersApprovedDemoRequests.Visible = false;

                    string alert = "There are no approved demo requests";
                    GlobalMethods.ShowMessageControl(UcMessageAlertApproved, alert, MessageTypes.Error, true, true, true, true, false);
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

        protected void RdgElioUsersDemoRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aBtnSendEmail = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnSendEmail");
                    //HtmlAnchor aSuccess = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSuccess");
                    HtmlAnchor aBtnReject = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnReject");

                    aBtnSendEmail.Visible = aBtnReject.Visible = item["is_approved"].Text == "0";
                    //aSuccess.Visible = !aBtnSendEmail.Visible;

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

        protected void RdgElioUsersDemoRequests_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsersDemoRequestsIJUsers> demos = new List<ElioUsersDemoRequestsIJUsers>();
                DataTable table = null;

                if (vSession.SearchQueryString == "")
                {
                    //demos = Sql.GetUsersDemoRequestsIJUsers(0, session);
                    table = Sql.GetUsersDemoRequestsIJUsersTbl(0, session);
                }
                else
                {
                    //DataLoader<ElioUsersDemoRequestsIJUsers> loader = new DataLoader<ElioUsersDemoRequestsIJUsers>(session);
                    //demos = loader.Load(vSession.SearchQueryString);
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                //if (demos.Count > 0)
                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersDemoRequests.Visible = true;
                    UcMessageAlert.Visible = false;

                    //DataTable table = new DataTable();

                    //table.Columns.Add("id");
                    //table.Columns.Add("request_for_user_id");
                    //table.Columns.Add("company_name");
                    //table.Columns.Add("company_email");
                    //table.Columns.Add("first_name");
                    //table.Columns.Add("last_name");                    
                    //table.Columns.Add("company_size");
                    //table.Columns.Add("sysdate");
                    //table.Columns.Add("is_approved");
                    //table.Columns.Add("date_approved");
                    //table.Columns.Add("demo_company_name");
                    //table.Columns.Add("demo_company_email");

                    //foreach (ElioUsersDemoRequestsIJUsers request in demos)
                    //{
                    //    table.Rows.Add(request.Id, request.RequestForUserId, request.CompanyName, request.CompanyEmail, request.FirstName, request.LastName,
                    //        request.CompanySize, request.Sysdate.ToString("dd/MM/yyyy"), request.IsApproved, request.DateApproved, request.DemoCompanyName, request.DemoCompanyEmail);
                    //}

                    RdgElioUsersDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersDemoRequests.Visible = false;

                    string alert = "There are no demo requests";
                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, true, true, false);
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

        protected void RbtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.SearchQueryString = string.Empty;

                vSession.SearchQueryString = @"SELECT r.[id]
                                              ,r.[request_for_user_id]
                                              ,r.[first_name]
                                              ,r.[last_name]
                                              ,r.[company_name]
                                              ,r.[company_email]
                                              ,r.[company_size]
                                              ,r.[sysdate]
                                              ,r.[is_approved]
                                              ,r.[date_approved]
                                        , u.company_name as demo_company_name, u.email as demo_company_email 
                                         FROM Elio_users_demo_requests r
                                        inner join Elio_users u
	                                        on r.request_for_user_id = u.id WHERE 1 = 1 ";

                bool hasSelectedCriteria = false;

                if (RcbxCategory.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND u.company_type='" + RcbxCategory.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxStatus.SelectedValue != "-1")
                {
                    vSession.SearchQueryString += " AND u.account_status=" + RcbxStatus.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxIsPublic.SelectedValue != "-1")
                {
                    vSession.SearchQueryString += " AND u.is_public=" + RcbxIsPublic.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxName.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND r.company_name='" + RcbxName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyName.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND r.company_name LIKE '" + RtbxCompanyName.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }
                if (RtbxCompanyEmail.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND r.company_email LIKE '" + RtbxCompanyEmail.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxBillingType.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND u.billing_type=" + RcbxBillingType.SelectedValue + "";
                    hasSelectedCriteria = true;
                }
                if (RcbxApplicationType.SelectedValue != "0")
                {
                    vSession.SearchQueryString += " AND u.user_application_type='" + RcbxApplicationType.SelectedValue + "'";
                    hasSelectedCriteria = true;
                }
                if (RtbxUserId.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND r.id IN (" + Validations.ReturnValidIdsWithCommaDelimiterForSearch(RtbxUserId.Text.Trim()) + ")";
                    hasSelectedCriteria = true;
                }
                if (RcbxApproved.SelectedValue != "-2")
                {
                    vSession.SearchQueryString += " AND r.is_approved=" + RcbxApproved.SelectedValue + "";
                    hasSelectedCriteria = true;
                }

                vSession.SearchQueryString += " order by r.is_approved, r.sysdate desc, u.id";

                if (!hasSelectedCriteria)
                    vSession.SearchQueryString = string.Empty;

                RdgElioUsersDemoRequests.Rebind();
                RdgElioUsersApprovedDemoRequests.Rebind();
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
                RcbxApplicationType.SelectedValue = "0";
                RcbxApproved.SelectedValue = "-2";

                vSession.SearchQueryString = string.Empty;

                RdgElioUsersDemoRequests.Rebind();
                RdgElioUsersApprovedDemoRequests.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSendEmail_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                try
                {
                    EmailSenderLib.ContactElioplusForDemoRequest(item["first_name"].Text, item["last_name"].Text, item["company_name"].Text, item["company_email"].Text, item["demo_company_email"].Text, vSession.Lang, session);
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, but email could not be send.ERROR(" + ex.Message.ToString() + ")", MessageTypes.Error, true, true, false);
                    return; 
                }

                int requestId = Convert.ToInt32(item["id"].Text);
                if (requestId > 0)
                {
                    ElioUsersDemoRequests request = Sql.GetDemoRequestById(requestId, session);
                    if (request != null)
                    {
                        request.IsApproved = 1;
                        request.DateApproved = DateTime.Now;

                        DataLoader<ElioUsersDemoRequests> loader = new DataLoader<ElioUsersDemoRequests>(session);
                        loader.Update(request);

                        RdgElioUsersDemoRequests.Rebind();
                        RdgElioUsersApprovedDemoRequests.Rebind();

                        GlobalMethods.ShowMessageControl(UcMessageAlert, "Request was successfuly approved and send to <b>" + item["demo_company_email"].Text + "</b>", MessageTypes.Success, true, true, true, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, but something went wrong. Request could not be approved, but email was send.", MessageTypes.Error, true, true, true, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminDemoRequestsManagement.aspx --> BtnSendEmail_ServerClick", string.Format("Request with ID {0}, could not be updated as approved at {1} by user {2}, but email was send", requestId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Request could not be send because of request ID. ", MessageTypes.Error, true, true, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, but something went wrong. Please try again later to send email request.", MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnReject_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;
                
                int requestId = Convert.ToInt32(item["id"].Text);
                if (requestId > 0)
                {
                    ElioUsersDemoRequests request = Sql.GetDemoRequestById(requestId, session);
                    if (request != null)
                    {
                        request.IsApproved = -1;
                        request.DateApproved = DateTime.Now;

                        DataLoader<ElioUsersDemoRequests> loader = new DataLoader<ElioUsersDemoRequests>(session);
                        loader.Update(request);

                        RdgElioUsersDemoRequests.Rebind();
                        RdgElioUsersApprovedDemoRequests.Rebind();

                        GlobalMethods.ShowMessageControl(UcMessageAlert, "Request was rejected", MessageTypes.Success, true, true, true, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, but something went wrong. Request could not be rejected.", MessageTypes.Error, true, true, true, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminDemoRequestsManagement.aspx --> BtnReject_ServerClick", string.Format("Request with ID {0}, could not be updated as rejected at {1} by user {2}, but email was send", requestId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControl(UcMessageAlert, "Request could not be rejected because of request ID. ", MessageTypes.Error, true, true, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControl(UcMessageAlert, "Sorry, but something went wrong. Please try again later to rejected request.", MessageTypes.Error, true, true, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion        
    }
}