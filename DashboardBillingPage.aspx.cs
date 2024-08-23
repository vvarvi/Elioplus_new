using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.Localization;
using Telerik.Web.UI;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Configuration;
using System.Net;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using Stripe;
using WdS.ElioPlus.Lib.Services.StripeAPI.Enums;
using ServiceStack.Stripe.Types;
using ServiceStack.Stripe;
using Stripe.Checkout;

namespace WdS.ElioPlus
{
    public partial class DashboardBillingPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(aChekoutGrowthData);
                scriptManager.RegisterPostBackControl(aChekoutStartupData);
                scriptManager.RegisterPostBackControl(aChekoutGrowthAuto);
                scriptManager.RegisterPostBackControl(aChekoutStartupAuto);
                scriptManager.RegisterPostBackControl(aChekoutStartup);
                scriptManager.RegisterPostBackControl(aChekoutGrowth);
                scriptManager.RegisterPostBackControl(aGetElioService);

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

                    //if (GlobalMethods.ShowSelfServicePage(vSession.User.Id))
                    //{
                    //    Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing-99"), false);
                    //    return;
                    //}

                    SetLinks();

                    //aChekoutStartup.Visible = aChekoutGrowth.Visible = aChekoutStartupAuto.Visible = aChekoutGrowthAuto.Visible = aChekoutStartupData.Visible = aChekoutGrowthData.Visible = aGetElioService.Visible = ((vSession.User == null) || (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))) ? false : true;

                    //if (!aEnterpriseSignUp.Visible)
                    //    FixPaymentBtns();

                    if (!IsPostBack)
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

        private void CheckOut(int packId, DBSession session)
        {
            ElioUsers user = vSession.User;

            ElioPackets packet = Sql.GetPacketById(packId, session);
            if (packet != null)
            {
                bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                if (!setEpiredSess)
                {
                    Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                    return;
                }

                Session chSession = null;

                ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCouponByUserPlanId(user.Id, packet.stripePlanId, session);

                if (planCoupon != null)
                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerWithDiscountApi(packet.stripePlanId, planCoupon.CouponId, user);
                else
                    chSession = Lib.Services.StripeAPI.StripeAPIService.CreateCheckoutSessionForPriceAndCustomerApi(packet.stripePlanId, user, false);

                if (chSession != null)
                {
                    StripeUsersCheckoutSessions uSession = new StripeUsersCheckoutSessions();

                    uSession.UserId = user.Id;
                    uSession.StripePlanId = packet.stripePlanId;
                    uSession.CheckoutSessionId = chSession.Id;
                    uSession.CheckoutUrl = chSession.Url;
                    uSession.DateCreated = DateTime.Now;
                    uSession.LastUpdate = DateTime.Now;

                    DataLoader<StripeUsersCheckoutSessions> loader = new DataLoader<StripeUsersCheckoutSessions>(session);
                    loader.Insert(uSession);

                    Response.Redirect(chSession.Url, false);
                }
            }
        }

        private void FixPaymentBtns()
        {
            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, false, ref showBtn, ref showModal, session);

            //if (allowPayment)
            //{
            //    BtnEnterpriseGoPremium.Visible = BtnStartUpGoPremium.Visible = BtnGrowthGoPremium.Visible = showBtn;
            //    aEnterpriseGoPremium.Visible = aStartupModal.Visible = aGrowthPaymentModal.Visible = showModal;

            //    //BtnGoPremium.Visible = showBtn;
            //    //aGoPremium.Visible = false; // showModal;
            //}
            //else
            //{
            //    BtnEnterpriseGoPremium.Visible = BtnStartUpGoPremium.Visible = BtnGrowthGoPremium.Visible = false;
            //    aEnterpriseGoPremium.Visible = aStartupModal.Visible = aGrowthPaymentModal.Visible = false;

            //    //BtnGoPremium.Visible = false;
            //    //aGoPremium.Visible = false;
            //}
        }

        private void FixBillingInvoicesForOldCustomers()
        {
            if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
            {
                if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                {
                    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(vSession.User, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                    if (order != null && order.IsPaid == (int)OrderStatus.Paid)
                    {
                        ElioBillingUserOrdersPayments lastPayment = Sql.GetUserLastPaymentsByOrderId(order.Id, session);

                        if (lastPayment != null)
                        {
                            if (lastPayment.CurrentPeriodEnd < DateTime.Now)
                            {
                                DateTime? startDate = null;
                                DateTime? currentPeriodStart = null;
                                DateTime? currentPeriodEnd = null;
                                DateTime? trialPeriodStart = null;
                                DateTime? trialPeriodEnd = null;
                                DateTime? canceledAt = null;
                                string orderMode = string.Empty;

                                Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, vSession.User.CustomerStripeId);

                                if (subscription != null)
                                {
                                    if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
                                    {
                                        if (lastPayment.CurrentPeriodEnd < Convert.ToDateTime(subscription.CurrentPeriodEnd))
                                        {
                                            int paymentCurrentStartMonth = lastPayment.CurrentPeriodStart.Month;
                                            int subscriptionCurrentStartMonth = Convert.ToDateTime(subscription.CurrentPeriodStart).Month;

                                            if (subscriptionCurrentStartMonth - paymentCurrentStartMonth > 0)
                                            {
                                                int addMonths = 1;
                                                for (int i = lastPayment.CurrentPeriodEnd.Month; i < Convert.ToDateTime(subscription.CurrentPeriodEnd).Month; i++)
                                                {
                                                    if (lastPayment.CurrentPeriodEnd.AddMonths(addMonths) < subscription.CurrentPeriodStart)
                                                    {
                                                        ElioBillingUserOrdersPayments newPayment = new ElioBillingUserOrdersPayments();

                                                        newPayment.UserId = vSession.User.Id;
                                                        newPayment.OrderId = order.Id;
                                                        newPayment.PackId = lastPayment.PackId;
                                                        newPayment.DateCreated = lastPayment.DateCreated.AddMonths(addMonths);
                                                        newPayment.LastUpdated = DateTime.Now;
                                                        newPayment.CurrentPeriodStart = lastPayment.CurrentPeriodStart.AddMonths(addMonths);
                                                        newPayment.CurrentPeriodEnd = lastPayment.CurrentPeriodEnd.AddMonths(addMonths);
                                                        newPayment.Amount = lastPayment.Amount;
                                                        newPayment.Comments = "";
                                                        newPayment.ChargeId = Guid.NewGuid().ToString();
                                                        newPayment.Mode = lastPayment.Mode;

                                                        DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                        loader.Insert(newPayment);

                                                        addMonths++;
                                                    }
                                                    else
                                                    {
                                                        ElioBillingUserOrdersPayments newPayment = new ElioBillingUserOrdersPayments();

                                                        newPayment.UserId = vSession.User.Id;
                                                        newPayment.OrderId = order.Id;
                                                        newPayment.PackId = lastPayment.PackId;
                                                        newPayment.DateCreated = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                        newPayment.LastUpdated = DateTime.Now;
                                                        newPayment.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                        newPayment.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                        newPayment.Amount = Convert.ToDecimal(subscription.Plan.Amount) / 100;
                                                        newPayment.Comments = "";
                                                        newPayment.ChargeId = Guid.NewGuid().ToString();
                                                        newPayment.Mode = subscription.Status.ToString();

                                                        DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                        loader.Insert(newPayment);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            DateTime? startDate = null;
                            DateTime? currentPeriodStart = null;
                            DateTime? currentPeriodEnd = null;
                            DateTime? trialPeriodStart = null;
                            DateTime? trialPeriodEnd = null;
                            DateTime? canceledAt = null;
                            string orderMode = string.Empty;

                            Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, vSession.User.CustomerStripeId);

                            if (subscription != null)
                            {
                                if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
                                {
                                    ElioBillingUserOrdersPayments newPayment = new ElioBillingUserOrdersPayments();

                                    newPayment.UserId = vSession.User.Id;
                                    newPayment.OrderId = order.Id;
                                    newPayment.PackId = order.PackId;
                                    newPayment.DateCreated = Convert.ToDateTime(subscription.Start);
                                    newPayment.LastUpdated = newPayment.DateCreated;
                                    newPayment.CurrentPeriodStart = (subscription.TrialStart == null) ? Convert.ToDateTime(subscription.Start) : Convert.ToDateTime(subscription.TrialStart);
                                    newPayment.CurrentPeriodEnd = (subscription.TrialEnd == null) ? Convert.ToDateTime(subscription.Start).AddMonths(1) : Convert.ToDateTime(subscription.TrialEnd);
                                    newPayment.Amount = Convert.ToDecimal(subscription.Plan.Amount) / 100;
                                    newPayment.Comments = "";
                                    newPayment.ChargeId = Guid.NewGuid().ToString();
                                    newPayment.Mode = subscription.Status.ToString();

                                    DataLoader<ElioBillingUserOrdersPayments> loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                    loader.Insert(newPayment);

                                    if (newPayment.CurrentPeriodEnd < Convert.ToDateTime(subscription.CurrentPeriodEnd) && newPayment.CurrentPeriodEnd < DateTime.Now)
                                    {
                                        //int paymentMonths = newPayment.CurrentPeriodEnd.Year * 12 + newPayment.CurrentPeriodEnd.Month;
                                        //int subscriptionMonths = Convert.ToDateTime(subscription.CurrentPeriodEnd).Year * 12 + Convert.ToDateTime(subscription.CurrentPeriodEnd).Month;
                                        //int differenceMonths = subscriptionMonths - paymentMonths;

                                        //if (differenceMonths > 0)
                                        //{

                                        bool addSubscriptionLastMonth = false;

                                        DateTime lastPaymentEndDate = newPayment.CurrentPeriodEnd;    // (subscription.TrialStart == null && subscription.TrialEnd == null) ? newPayment.CurrentPeriodStart : Convert.ToDateTime(subscription.TrialEnd);

                                        while (lastPaymentEndDate < Convert.ToDateTime(subscription.CurrentPeriodEnd))
                                        {
                                            if (lastPaymentEndDate < Convert.ToDateTime(subscription.CurrentPeriodEnd))
                                            {
                                                ElioBillingUserOrdersPayments payment = new ElioBillingUserOrdersPayments();

                                                payment.UserId = vSession.User.Id;
                                                payment.OrderId = order.Id;
                                                payment.PackId = order.PackId;
                                                payment.DateCreated = lastPaymentEndDate;
                                                payment.LastUpdated = payment.DateCreated;
                                                payment.CurrentPeriodStart = lastPaymentEndDate;
                                                payment.CurrentPeriodEnd = payment.CurrentPeriodStart.AddMonths(1);
                                                payment.Amount = newPayment.Amount;
                                                payment.Comments = "";
                                                payment.ChargeId = Guid.NewGuid().ToString();
                                                payment.Mode = newPayment.Mode;

                                                loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                loader.Insert(payment);

                                                lastPaymentEndDate = payment.CurrentPeriodEnd;

                                                if (lastPaymentEndDate.Year == Convert.ToDateTime(subscription.CurrentPeriodEnd).Year && lastPaymentEndDate.Month == Convert.ToDateTime(subscription.CurrentPeriodEnd).AddMonths(-1).Month)
                                                {
                                                    addSubscriptionLastMonth = true;
                                                    break;
                                                }
                                            }
                                        }

                                        if (addSubscriptionLastMonth)
                                        {
                                            if (lastPaymentEndDate < Convert.ToDateTime(subscription.CurrentPeriodEnd))
                                            {
                                                ElioBillingUserOrdersPayments lstPayment = new ElioBillingUserOrdersPayments();

                                                lstPayment.UserId = vSession.User.Id;
                                                lstPayment.OrderId = order.Id;
                                                lstPayment.PackId = order.PackId;
                                                lstPayment.DateCreated = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                lstPayment.LastUpdated = DateTime.Now;
                                                lstPayment.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                lstPayment.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                lstPayment.Amount = Convert.ToDecimal(subscription.Plan.Amount) / 100;
                                                lstPayment.Comments = "";
                                                lstPayment.ChargeId = Guid.NewGuid().ToString();
                                                lstPayment.Mode = subscription.Status.ToString();

                                                loader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                                                loader.Insert(lstPayment);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {
                MessageAlertHistory.Visible = false;

                UpdateStrings();
                //LoadPacketFeatures();
                LoadBillingInfo();
            }

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divPricingPrmPlan.Visible = divPricingPartnerRecruitmentPlan.Visible = vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType);

                //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                //{
                bool hasActiveService = Sql.HasActiveServiceSubscription(vSession.User.Id, session);
                if (!hasActiveService)
                {
                    divServicePlan.Visible = true;
                    LblGetElioService.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "20")).Text;

                    ElioPackets packet = null;

                    if (vSession.User.Id == 34817)
                    {
                        packet = Sql.GetPacketById((int)Packets.AccountManagerService, session);
                        if (packet != null)
                        {
                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                            {
                                LblServiceCost.Text = (Convert.ToDecimal(plan.Amount) / 100).ToString();
                            }
                        }
                    }
                    else if (vSession.User.Id == 35867 || vSession.User.Id == 3399 || vSession.User.Id == 49968)
                    {
                        packet = Sql.GetPacketById((int)Packets.PremiumService, session);
                        if (packet != null)
                        {
                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                            {
                                LblServiceCost.Text = (Convert.ToDecimal(plan.Amount) / 100).ToString();
                            }
                        }
                    }
                    else
                    {
                        packet = Sql.GetPacketById((int)Packets.PremiumService299, session);
                        LblServiceCost.Text = "299";
                    }
                }
                //}
                //else
                //{
                //    divServicePlan.Visible = false;
                //}
            }
            else
            {
                divIntentSignalsPlan.Visible = vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
            }

            LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

            LblDashSubTitle.Text = "your billing information";
        }

        private void UpdateStrings()
        {
            LblElioService.Text = "Account Manager Service";
            //LblCommitment.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "62")).Text;

            //LblSignUpStartUp.Text = LblSignUpGrowth.Text = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "60")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "31")).Text;
            //LblEnterpriseSignUp.Text = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "60")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "66")).Text;
            //LblEnterpriseGoPremium.Text = BtnEnterpriseGoPremium.Text = (vSession.User != null && vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "21")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "button", "2")).Text;
            //LblStartUpGoPremium.Text = BtnStartUpGoPremium.Text = (vSession.User != null && vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "23")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "button", "3")).Text;
            //LblGrowthGoPremium.Text = BtnGrowthGoPremium.Text = (vSession.User != null && vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "24")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "button", "4")).Text;

            RdgOrders.MasterTableView.GetColumn("status").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "2")).Text;
            RdgOrders.MasterTableView.GetColumn("plan_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "13")).Text;
            RdgOrders.MasterTableView.GetColumn("date").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "5")).Text;
            RdgOrders.MasterTableView.GetColumn("total_amount").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "10")).Text;
            RdgOrders.MasterTableView.GetColumn("cancel").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "11")).Text;
            RdgOrders.MasterTableView.GetColumn("activate").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "14")).Text;
            RdgOrders.MasterTableView.GetColumn("invoice_pdf").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "4", "column", "15")).Text;

            #region Billing Info

            LblElioBillingDetails.Text = "Billing Information";
            LblStripeCreditCardDetails.Text = "Credit Card Information";

            BtnSaveBillingDetails.Text = "Save";
            BtnAddNewCard.Text = "Add New Card";

            BtnSaveCreditCardDetails.Text = "Update Card";
            BtnAddNewCard.Text = "Add New Card";
            BtnCancelAddNewCard.Text = "Cancel";

            //LblManagedText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "63")).Text;

            #endregion
        }

        private void ClearErrorFields()
        {
            UcCreditcardMessageControl.Visible = false;
        }

        private void SetLinks()
        {
            //aStartupSignUp.HRef = aGrowthSignUp.HRef = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : ControlLoader.SignUp;

            //aEnterpriseSignUp.HRef = ControlLoader.ContactUs;
            //aEnterpriseGoPremium.HRef = ControlLoader.ContactUs;
        }

        private void LoadPacketFeatures()
        {
            List<ElioPacketFeaturesItems> packetsFeaturesItems = Sql.GetAllPublicPacketTotalCostAndFeatures(session);

            //decimal freeCost = 0;
            //decimal premiumCost = 0;
            //decimal premiumStartupCost = 0;
            //decimal premiumGrowthCost = 0;

            foreach (ElioPacketFeaturesItems item in packetsFeaturesItems)
            {
                //if (item.PackId == (int)Packets.PremiumStartup)
                //{
                //    premiumStartupCost += item.ItemCostWithVat * item.FreeItemsNo;

                //    if (item.FeatureId == 2)
                //        LblPremiumStartupMessages.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 3)
                //        LblPremiumStartupConnections.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 4)
                //        LblPremiumStartupManagePartners.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 5)
                //        LblPremiumStartupLibraryStorage.Text = item.FreeItemsNo.ToString();
                //}
                //else if (item.PackId == (int)Packets.PremiumGrowth)
                //{
                //    premiumGrowthCost += item.ItemCostWithVat * item.FreeItemsNo;

                //    if (item.FeatureId == 2)
                //        LblPremiumGrowthMessages.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 3)
                //        LblPremiumGrowthConnections.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 4)
                //        LblPremiumGrowthManagePartners.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 5)
                //        LblPremiumGrowthLibraryStorage.Text = item.FreeItemsNo.ToString();
                //}
                //else if (item.PackId == (int)Packets.PremiumEnterprise)
                //{
                //    if (item.FeatureId == 2)
                //        LblPremiumEnterpriseMessages.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 3)
                //        LblPremiumEnterpriseConnections.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 4)
                //        LblPremiumEnterpriseManagePartners.Text = item.FreeItemsNo.ToString();
                //    else if (item.FeatureId == 5)
                //        LblPremiumEnterpriseLibraryStorage.Text = item.FreeItemsNo.ToString();
                //}
            }

            int? totalLeads = 0;
            int? totalMessages = 0;
            int? totalConnections = 0;
            int? totalManagePartners = 0;
            int? totalLibraryStorage = 0;

            #region Premium Packet

            #endregion

            #region Start Up Packet

            decimal totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumStartup), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            //LblPremiumStartupPrice.Text = totalCost.ToString();

            //LblPremiumStartupMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            //LblPremiumStartupConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            //LblPremiumStartupManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            //LblPremiumStartupLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Growth Packet

            totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumGrowth), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            //LblPremiumGrowthPrice.Text = totalCost.ToString();

            //LblPremiumGrowthMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            //LblPremiumGrowthConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            //LblPremiumGrowthManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            //LblPremiumGrowthLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Enterprise Packet

            totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.PremiumEnterprise), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            //LblPremiumEnterprisePrice.Text = totalCost.ToString();

            //LblPremiumEnterpriseMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            //LblPremiumEnterpriseConnections.Text = "custom";    // connections"; //"over {count} connections".Replace("{count}", totalConnections.ToString());

            //LblPremiumEnterpriseManagePartners.Text = "custom"; // partners to manage";  //"over {count} partners to manage".Replace("{count}", totalManagePartners.ToString());

            //LblPremiumEnterpriseLibraryStorage.Text = "custom";      //"over {count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            #region Free Packet

            #endregion
        }

        private void LoadBillingInfo()
        {
            if (vSession.User.HasBillingDetails == 1)
            {
                #region Load Billing Account Data

                ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(vSession.User.Id, session);

                if (account != null)
                {
                    TbxBillingCompanyName.Text = vSession.User.CompanyName;
                    TbxBillingCompanyAddress.Text = account.CompanyBillingAddress;
                    TbxBillingCompanyPostCode.Text = account.CompanyPostCode;
                    TbxBillingCompanyVatNumber.Text = account.CompanyVatNumber;
                    //TbxUserIdNumber.Text = account.UserIdNumber;
                    //TbxUserVatNumber.Text = account.UserVatNumber;
                    //TbxUserBillingEmail.Text = account.BillingEmail;
                    //account.HasVat = 1;
                    //account.Sysdate = DateTime.Now;
                    //account.LastUpdated = DateTime.Now;
                    //account.IsActive = 1;
                }

                #endregion
            }
        }

        private void FixCustomerSubscriptionInvoices()
        {
            if (vSession.User != null)
            {
                if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                {
                    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                    {
                        StripeCustomer customer = Lib.Services.StripeAPI.StripeService.GetCustomer(vSession.User.CustomerStripeId);
                        if (customer != null && !string.IsNullOrEmpty(customer.Id))
                        {
                            if (!Convert.ToBoolean(customer.Deleted))
                            {
                                bool hasSubscriptions = customer.Subscriptions.TotalCount > 0;
                                if (hasSubscriptions)
                                {
                                    foreach (StripeSubscription subscription in customer.Subscriptions.Data)
                                    {
                                        if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                        {
                                            bool exists = Sql.ExistCustomerSubscription(vSession.User.CustomerStripeId, subscription.Id, session);
                                            if (!exists)
                                            {
                                                ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                string coupon = "";
                                                //if (subscription.StripeDiscount != null && subscription.StripeDiscount.StripeCoupon != null && !string.IsNullOrEmpty(subscription.StripeDiscount.StripeCoupon.Id))
                                                //    coupon = subscription.StripeDiscount.StripeCoupon.Id;

                                                sub.UserId = vSession.User.Id;
                                                sub.CustomerId = subscription.Customer;
                                                sub.SubscriptionId = subscription.Id;
                                                sub.CouponId = (coupon != "") ? coupon : "";
                                                sub.PlanId = subscription.Plan.Id;
                                                sub.PlanNickname = subscription.Plan.Nickname;
                                                sub.CreatedAt = Convert.ToDateTime(subscription.Start);
                                                sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                sub.HasDiscount = sub.CouponId != "" ? 1 : 0;
                                                sub.Status = subscription.Status.ToString();
                                                sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                sub.Amount = (int)subscription.Plan.Amount;

                                                DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                loader.Insert(sub);

                                                StripeList<StripeInvoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscription(subscription.Customer, subscription.Id);
                                                if (invoices != null && invoices.Count() > 0)
                                                {
                                                    foreach (StripeInvoice invoice in invoices)
                                                    {
                                                        if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                                        {
                                                            bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
                                                            if (!existInvoice)
                                                            {
                                                                ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                                subInvoice.UserId = vSession.User.Id;
                                                                subInvoice.UserSubscriptionId = sub.Id;
                                                                subInvoice.CustomerId = invoice.Customer;
                                                                subInvoice.InvoiceId = invoice.Id;
                                                                subInvoice.ChargeId = invoice.Charge;
                                                                subInvoice.SubscriptionId = subscription.Id;
                                                                subInvoice.IsClosed = (bool)invoice.Closed ? 1 : 0;
                                                                subInvoice.Currency = invoice.Currency;
                                                                subInvoice.Date = Convert.ToDateTime(invoice.Date);
                                                                subInvoice.Description = "Invoice for customer " + invoice.Customer;
                                                                subInvoice.HostedInvoiceUrl = "invoice.HostedInvoiceUrl";
                                                                subInvoice.InvoicePdf = "invoice.InvoicePdf";
                                                                subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                                subInvoice.Number = invoice.Charge;
                                                                subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                                subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
                                                                subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
                                                                subInvoice.ReceiptNumber = invoice.Charge; //(invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                                subInvoice.HasDiscount = sub.HasDiscount;
                                                                subInvoice.TotalAmount = invoice.Total;
                                                                subInvoice.SubTotalAmount = invoice.Subtotal;
                                                                subInvoice.CouponId = "";

                                                                DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                                invIoader.Insert(subInvoice);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                ElioUsersSubscriptions sub = Sql.GetSubscriptionBySubID(subscription.Id, session);
                                                if (sub != null)
                                                {
                                                    StripeList<StripeInvoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscription(subscription.Customer, subscription.Id);
                                                    if (invoices != null && invoices.Count() > 0)
                                                    {
                                                        foreach (StripeInvoice invoice in invoices)
                                                        {
                                                            if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                                            {
                                                                bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
                                                                if (!existInvoice)
                                                                {
                                                                    ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                                    subInvoice.UserId = vSession.User.Id;
                                                                    subInvoice.UserSubscriptionId = sub.Id;
                                                                    subInvoice.CustomerId = invoice.Customer;
                                                                    subInvoice.InvoiceId = invoice.Id;
                                                                    subInvoice.ChargeId = invoice.Charge;
                                                                    subInvoice.SubscriptionId = subscription.Id;
                                                                    subInvoice.IsClosed = (bool)invoice.Closed ? 1 : 0;
                                                                    subInvoice.Currency = invoice.Currency;
                                                                    subInvoice.Date = Convert.ToDateTime(invoice.Date);
                                                                    subInvoice.Description = "Invoice for customer " + invoice.Customer;
                                                                    subInvoice.HostedInvoiceUrl = "invoice.HostedInvoiceUrl";
                                                                    subInvoice.InvoicePdf = "invoice.InvoicePdf";
                                                                    subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                                    subInvoice.Number = invoice.Charge;
                                                                    subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                                    subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
                                                                    subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
                                                                    subInvoice.ReceiptNumber = invoice.Charge; //(invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                                    subInvoice.HasDiscount = sub.HasDiscount;
                                                                    subInvoice.TotalAmount = invoice.Total;
                                                                    subInvoice.SubTotalAmount = invoice.Subtotal;

                                                                    if (invoice.Discount != null)
                                                                        if (invoice.Discount.Coupon != null)
                                                                        {
                                                                            StripeCoupon coupon = invoice.Discount.Coupon;
                                                                            if (coupon != null && !string.IsNullOrEmpty(coupon.Id))
                                                                                subInvoice.CouponId = coupon.Id;
                                                                            else
                                                                                subInvoice.CouponId = "";
                                                                        }
                                                                        else
                                                                            subInvoice.CouponId = "";
                                                                    else
                                                                        subInvoice.CouponId = "";

                                                                    DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                                    invIoader.Insert(subInvoice);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
                Response.Redirect(ControlLoader.Login, false);
        }

        #endregion

        #region Grids

        protected void RdgOrders_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblDateCreated = (Label)ControlFinder.FindControlRecursive(item, "LblDateCreated");
                    lblDateCreated.Text = Convert.ToDateTime(item["date"].Text).ToString("dd/MM/yyyy");

                    Label lblPrice = (Label)ControlFinder.FindControlRecursive(item, "LblPrice");
                    lblPrice.Text = (Convert.ToDecimal(item["total_amount"].Text) / 100).ToString() + " $";

                    Label lblPlan = (Label)ControlFinder.FindControlRecursive(item, "LblPlan");
                    //lblPlan.Text = Sql.GetPlanDescriptionBySubscriptionID(item["subscription_id"].Text, session);                    

                    #region custom replacement (for SpinBackUp)

                    int packetPrice = Convert.ToInt32(Convert.ToDecimal(item["total_amount"].Text) / 100);
                    if (packetPrice == 199)
                        lblPlan.Text = "Service 199";
                    else
                        lblPlan.Text = item["plan_nickname"].Text;

                    #endregion

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                    Label lblInvoiceUrl = (Label)ControlFinder.FindControlRecursive(item, "LblInvoiceUrl");
                    lblInvoiceUrl.Text = item["number"].Text;

                    HtmlAnchor aInvoiceUrl = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aInvoiceUrl");
                    aInvoiceUrl.HRef = item["invoice_pdf"].Text;
                    aInvoiceUrl.Target = "_blank";

                    HtmlAnchor btnCancelPlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnCancelPlan");

                    //int packId = Sql.GetPacketIdBySubscriptionID(item["subscription_id"].Text, session);
                    //if (packId == (int)Packets.PremiumService)
                    //    btnCancelPlan.HRef = "#ConfirmationServiceModal";
                    //else
                    //    btnCancelPlan.HRef = "#ConfirmationModal";

                    HtmlAnchor btnActivatePlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnActivatePlan");
                    btnActivatePlan.HRef = "#PaymentPacketsModal";

                    Label lblBtnCancelPlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnCancelPlan");
                    Label lblBtnActivatePlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnActivatePlan");

                    bool isLastInvoice = Sql.IsPlanSubscriptionLastInvoice(item["subscription_id"].Text, item["invoice_id"].Text, session) || Sql.IsServiceSubscriptionLastInvoice(item["subscription_id"].Text, item["invoice_id"].Text, session);
                    if (isLastInvoice)
                    {
                        if (!string.IsNullOrEmpty(item["status"].Text))
                        {
                            if (item["status"].Text == "active")
                            {
                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Active & Paid";
                                else
                                    lblStatus.Text = "Active & Not Paid";

                                lblStatus.CssClass = "label label-lg label-light-success label-inline";

                                if (!string.IsNullOrEmpty(item["coupon_id"].Text) && item["coupon_id"].Text != "&nbsp;" && item["coupon_id"].Text.ToString() != "")
                                    btnCancelPlan.Visible = Sql.CanCancelSubscriptionByCouponRedeemByDate(item["coupon_id"].Text.ToString(), session);
                                else
                                    btnCancelPlan.Visible = true;

                                btnActivatePlan.Visible = false;
                            }
                            else if (item["status"].Text == "canceled")
                            {
                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Canceled";
                                else
                                    lblStatus.Text = "Canceled & Not Paid";

                                lblStatus.CssClass = "label label-lg label-light-danger label-inline";

                                btnCancelPlan.Visible = false;
                                btnActivatePlan.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        if (item["is_paid"].Text == "1")
                            lblStatus.Text = "Active & Paid";
                        else
                            lblStatus.Text = "Active & Not Paid";

                        lblStatus.CssClass = "label label-lg label-light-success label-inline";

                        btnCancelPlan.Visible = false;
                        btnActivatePlan.Visible = false;
                    }

                    #region to delete

                    //if (!string.IsNullOrEmpty(item["status"].Text))
                    //{
                    //    if (item["status"].Text == "active")
                    //    {
                    //        if (item["is_paid"].Text == "1")
                    //            lblStatus.Text = "Active & Paid";
                    //        else
                    //            lblStatus.Text = "Active & Not Paid";

                    //        btnCancelPlan.Visible = true;
                    //        btnActivatePlan.Visible = false;
                    //        lblStatus.CssClass = "label label-sm label-success";
                    //    }
                    //    else if (item["status"].Text == "canceled")
                    //    {
                    //        if (item["is_paid"].Text == "1")
                    //            lblStatus.Text = "Canceled & Paid";
                    //        else
                    //            lblStatus.Text = "Canceled & Not Paid";

                    //        btnCancelPlan.Visible = false;
                    //        btnActivatePlan.Visible = true;
                    //        lblStatus.CssClass = "label label-sm label-danger";
                    //    }
                    //}

                    #endregion

                    item["date"].Text = string.Empty;
                    item["period_start"].Text = string.Empty;
                    item["period_end"].Text = string.Empty;
                    item["total_amount"].Text = string.Empty;
                    item["is_paid"].Text = string.Empty;
                    item["status"].Text = string.Empty;
                    item["invoice_pdf"].Text = string.Empty;
                    item["number"].Text = string.Empty;
                    item["plan_id"].Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgOrders_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //List<ElioUsersSubscriptionsInvoices> invoices = Sql.GetUserSubscriptionInvoices(vSession.User.Id, session);
                    DataTable table = Sql.GetUserSubscriptionInvoicesDataTable(vSession.User.Id, session);
                    if (table.Rows.Count > 0)
                    {
                        RdgOrders.Visible = true;
                        MessageAlertHistory.Visible = false;

                        RdgOrders.DataSource = table;
                    }
                    else
                    {
                        RdgOrders.Visible = false;
                        GlobalMethods.ShowMessageControlDA(MessageAlertHistory, "You have no purchases history", MessageTypes.Info, true, true, false);
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

        #region Payment Buttons

        protected void BtnSaveBillingDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ClearErrorFields();

                    if (TbxBillingCompanyName.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcBillingMessageControl, "Enter company name!", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxBillingCompanyAddress.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcBillingMessageControl, "Enter company address!", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxBillingCompanyPostCode.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcBillingMessageControl, "Enter company post code!", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxBillingCompanyVatNumber.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcBillingMessageControl, "Enter company vat number!", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (vSession.User.HasBillingDetails == 0)
                    {
                        #region New Billing Account Data

                        ElioUsersBillingAccount account = new ElioUsersBillingAccount();

                        account.UserId = vSession.User.Id;
                        account.CompanyBillingAddress = TbxBillingCompanyAddress.Text;
                        account.CompanyPostCode = TbxBillingCompanyPostCode.Text;
                        account.CompanyVatNumber = TbxBillingCompanyVatNumber.Text;
                        account.CompanyVatOffice = "";
                        account.PostCode = "";
                        account.UserIdNumber = "";
                        account.UserVatNumber = "";
                        account.BillingEmail = vSession.User.Email;
                        account.HasVat = 1;
                        account.Sysdate = DateTime.Now;
                        account.LastUpdated = DateTime.Now;
                        account.IsActive = 1;

                        DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                        loader0.Insert(account);

                        #endregion

                        #region Update User / Session

                        //vSession.User.HasBillingDetails = 1;
                        //vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                        #endregion
                    }
                    else
                    {
                        #region Update Billing Account Data

                        ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(vSession.User.Id, session);

                        if (account != null)
                        {
                            account.CompanyBillingAddress = TbxBillingCompanyAddress.Text;
                            account.CompanyPostCode = TbxBillingCompanyPostCode.Text;
                            account.CompanyVatNumber = TbxBillingCompanyVatNumber.Text;
                            account.CompanyVatOffice = "";
                            account.PostCode = "";
                            account.UserIdNumber = "";
                            account.UserVatNumber = "";
                            account.BillingEmail = vSession.User.Email;
                            account.HasVat = 1;
                            account.Sysdate = DateTime.Now;
                            account.LastUpdated = DateTime.Now;
                            account.IsActive = 1;

                            DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                            loader0.Update(account);
                        }

                        #endregion
                    }

                    if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                    {
                        ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(vSession.User.Id, session);

                        if (account != null)
                        {
                            string type = "eu_vat";
                            string[] countriesEU = new string[] {
"Austria"
,"Belgium"
,"Bulgaria"
,"Croatia"
,"Cyprus"
,"Czech Republic"
,"Denmark"
,"Estonia"
,"Finland"
,"France"
,"Germany"
,"Greece"
,"Hungary"
,"Ireland"
,"Italy"
,"Latvia"
,"Lithuania"
,"Luxembourg"
,"Malta"
,"Netherlands"
,"Poland"
,"Portugal"
,"Romania"
,"Slovakia"
,"Slovenia"
,"Spain"
,"Sweden"
,"United Kingdom" };

                            if (countriesEU.Contains(vSession.User.Country))
                            {
                                type = "eu_vat";
                            }
                            else if (vSession.User.Country == "Australia")
                            {
                                type = "au_abn";
                            }
                            else if (vSession.User.Country == "Canada")
                            {
                                type = "ca_bn";
                            }
                            else if (vSession.User.Country == "Chile")
                            {
                                type = "cl_tin";
                            }
                            else if (vSession.User.Country == "Iceland")
                            {
                                type = "is_vat";
                            }
                            else if (vSession.User.Country == "India")
                            {
                                type = "in_gst";
                            }
                            else if (vSession.User.Country == "Mexico")
                            {
                                type = "mx_rfc";
                            }
                            else if (vSession.User.Country == "New Zealand")
                            {
                                type = "nz_gst";
                            }
                            else if (vSession.User.Country == "Norway")
                            {
                                type = "no_vat";
                            }
                            else if (vSession.User.Country == "Russia")
                            {
                                type = "ru_inn";
                            }
                            else if (vSession.User.Country == "Saudi Arabia")
                            {
                                type = "sa_vat";
                            }
                            else if (vSession.User.Country == "Singapore")
                            {
                                type = "sg_gst";
                            }
                            else if (vSession.User.Country == "South Africa")
                            {
                                type = "za_vat";
                            }
                            else if (vSession.User.Country == "South Korea")
                            {
                                type = "kr_brn";
                            }
                            else if (vSession.User.Country == "Spain")
                            {
                                type = "es_cif";
                            }
                            else if (vSession.User.Country == "Switzerland")
                            {
                                type = "ch_vat";
                            }
                            else if (vSession.User.Country == "Thailand")
                            {
                                type = "th_vat";
                            }
                            else if (vSession.User.Country == "Turkey")
                            {
                                type = "tr_tin";
                            }
                            else if (vSession.User.Country == "United Arab Emirates")
                            {
                                type = "ae_trn";
                            }
                            else if (vSession.User.Country == "United Kingdom")
                            {
                                type = "gb_vat";
                            }

                            if (string.IsNullOrEmpty(account.StripeTaxId))
                            {
                                TaxId txId = Lib.Services.StripeAPI.StripeAPIService.CreateCustomerTaxIdApi(vSession.User.CustomerStripeId, type, TbxBillingCompanyVatNumber.Text);

                                if (txId != null && !string.IsNullOrEmpty(txId.Id))
                                {
                                    account.StripeTaxId = txId.Id;
                                    account.LastUpdated = DateTime.Now;

                                    DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                                    loader0.Update(account);
                                }
                            }
                            else
                            {
                                TaxId txIdDlt = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerTaxIdApi(vSession.User.CustomerStripeId, account.StripeTaxId);
                                if (txIdDlt != null && (bool)txIdDlt.Deleted)
                                {
                                    TaxId txId = Lib.Services.StripeAPI.StripeAPIService.CreateCustomerTaxIdApi(vSession.User.CustomerStripeId, type, TbxBillingCompanyVatNumber.Text);

                                    if (txId != null && !string.IsNullOrEmpty(txId.Id))
                                    {
                                        account.StripeTaxId = txId.Id;
                                        account.LastUpdated = DateTime.Now;

                                        DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                                        loader0.Update(account);
                                    }
                                }
                            }
                        }
                    }

                    GlobalMethods.ShowMessageControlDA(UcBillingMessageControl, "Your Billing Account Details saved successfully", MessageTypes.Success, true, true, true, true, false);
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

        protected void BtnSaveCreditCardDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ClearErrorFields();

                string errorMsg = string.Empty;

                //if (TbxExpMonth.Text.Trim() == string.Empty)
                //{
                //    divExpMonth.Visible = true;
                //    LblExpMonthError.Text = "Please enter card expiration month";
                //    return;
                //}                
                //else if (Convert.ToInt32(TbxExpMonth.Text) <= Convert.ToInt32(0) || TbxExpMonth.Text == "00" || Convert.ToInt32(TbxExpMonth.Text) > Convert.ToInt32(12))
                //{
                //    divExpMonth.Visible = true;
                //    LblExpMonthError.Text = "Please enter valid card expiration month";
                //    return;
                //}

                if (DrpExpMonth.SelectedValue == "0")
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter card expiration month", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                if (TbxExpYear.Text.Trim() == string.Empty)
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter card expiration year", MessageTypes.Error, true, true, true, true, false);

                    return;
                }
                else if (Convert.ToInt32(TbxExpYear.Text) < 0 || Convert.ToInt32(TbxExpYear.Text) <= 00 || Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter card expiration greater than today", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);

                if (userCard != null)
                {
                    try
                    {
                        Xamarin.Payments.Stripe.StripeCard cardInfo = StripeLib.UpdateCreditCard(vSession.User.CustomerStripeId, userCard.CardStripeId,
                                                                                                 Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text),
                                                                                                 "TbxFullName.Text", "TbxAddress1.Text", "TbxAddress2.Text",
                                                                                                 "TbxOrigin.Text", "TbxZipCode.Text", ref errorMsg);
                        if (cardInfo != null)
                        {
                            try
                            {
                                session.OpenConnection();

                                userCard.CardFullname = cardInfo.Name;
                                userCard.Address1 = cardInfo.AddressLine1;
                                userCard.Address2 = cardInfo.AddressLine2;
                                userCard.CardType = cardInfo.Type;
                                userCard.ExpMonth = cardInfo.ExpirationMonth;
                                userCard.ExpYear = cardInfo.ExpirationYear;
                                userCard.Origin = cardInfo.Country;
                                userCard.CvcCheck = cardInfo.CvcCheck.ToString();
                                userCard.ZipCheck = cardInfo.AddressZipCheck.ToString();
                                userCard.Fingerprint = cardInfo.Fingerprint;
                                userCard.IsDefault = 1;
                                userCard.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                                userCard.Sysdate = DateTime.Now;
                                userCard.LastUpdated = DateTime.Now;
                                userCard.CustomerStripeId = vSession.User.CustomerStripeId;
                                userCard.UserId = vSession.User.Id;

                                DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                                loader.Update(userCard);

                                GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your Credit Card Details updated successfully", MessageTypes.Success, true, true, true, true, false);
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
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0} and Stripe ID {1}, tried to change his credit card but did not found", vSession.User.Id, vSession.User.CustomerStripeId));

                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                        Logger.DetailedError(Request.Url.ToString(), ex.Message, errorMsg);
                        return;
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0} and Stripe ID {1}, tried to change his credit card but did not found", vSession.User.Id, vSession.User.CustomerStripeId));
                    return;
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelAddNewCard_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearErrorFields();

                //div5.Visible = false;
                //div4.Visible = false;
                BtnAddNewCard.Text = "Add New Card";
                //BtnCancelAddNewCard.Visible = false;
                TbxCCNumber.Text = string.Empty;
                TbxCvcNumber.Text = string.Empty;
                DrpExpMonth.SelectedValue = "0";
                TbxExpYear.Text = string.Empty;
                //divSaveCreditCardSuccess.Visible = false;
                //LblSaveCreditCardSuccess.Text = string.Empty;
                //divSaveCreditCardFailure.Visible = false;
                //LblSaveCreditCardFailure.Text = string.Empty;
                UcCreditcardMessageControl.Visible = false;
                //LoadCreditCardInfo();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnAddNewCard_OnClick(object sender, EventArgs args)
        {
            try
            {
                //session.OpenConnection();

                ClearErrorFields();

                string errorMsg = string.Empty;

                if (TbxCCNumber.Text.Trim() == string.Empty)
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter card number", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                if (TbxCvcNumber.Text.Trim() == string.Empty)
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter cvc number", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                if (DrpExpMonth.SelectedValue == "0")
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please select card expiration month", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                if (TbxExpYear.Text.Trim() == string.Empty)
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter card expiration year", MessageTypes.Error, true, true, true, true, false);

                    return;
                }
                else if (!Validations.IsNumberOnly(TbxExpYear.Text.Trim()))
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Please enter valid year", MessageTypes.Error, true, true, true, true, false);

                    return;
                }
                else if ((Convert.ToInt32(TbxExpYear.Text) < 0 || Convert.ToInt32(TbxExpYear.Text) <= 00 || Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))) || (Convert.ToInt32(DrpExpMonth.SelectedValue) <= DateTime.Now.Month && Convert.ToInt32(TbxExpYear.Text) == Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))))
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Card is expired. Please enter new card", MessageTypes.Error, true, true, true, true, false);

                    return;
                }

                bool success = Lib.Services.StripeAPI.StripeApi.AddNewCardNew(vSession.User, TbxCCNumber.Text, DrpExpMonth.SelectedItem.Text, TbxExpYear.Text, TbxCvcNumber.Text, out errorMsg);
                if (success)
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your Credit Card changed successfully", MessageTypes.Success, true, true, true, true, false);

                    BtnAddNewCard.Text = "Add New Card";
                    TbxCCNumber.Text = string.Empty;
                    TbxCvcNumber.Text = string.Empty;
                    DrpExpMonth.SelectedValue = "0";
                    TbxExpYear.Text = string.Empty;
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, errorMsg, MessageTypes.Error, true, true, true, true, false);

                    return;
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void PaymentCanceledModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor abtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)abtn.NamingContainer;

                int packId = Sql.GetPacketIdBySubscriptionID(item["subscription_id"].Text, session);
                if (packId == (int)Packets.PremiumService)
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenServicePaymentModal();", true);
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPaymentModal();", true);
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

        protected void aChekoutGrowth_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(9, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutStartup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(8, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutGrowthAuto_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(57, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutStartupAuto_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(56, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutGrowthData_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(59, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aChekoutStartupData_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        CheckOut(58, session);

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User


                        #endregion
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

        protected void aGetElioService_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int selectedPacketId = (int)Packets.AccountManagerService;

                    if (vSession.User.Id == 34817)
                        selectedPacketId = (int)Packets.PremiumService299;
                    else if (vSession.User.Id == 35867 || vSession.User.Id == 3399)
                        selectedPacketId = (int)Packets.PremiumService;

                    CheckOut(selectedPacketId, session);
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

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                //if (vSession.User != null)
                //{
                //    DateTime? canceledAt = null;
                //    string stripeUnsubscribeError = string.Empty;
                //    bool successUnsubscription = false;

                //    try
                //    {
                //        successUnsubscription = StripeLib.UnSubscribeCustomer(ref canceledAt, vSession.User.CustomerStripeId, ref stripeUnsubscribeError);
                //    }
                //    catch (Exception ex)
                //    {
                //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                //    }

                //    if (successUnsubscription)
                //    {
                //        try
                //        {
                //            session.OpenConnection();
                //            session.BeginTransaction();

                //            vSession.User = PaymentLib.MakeUserFreemium(vSession.User, canceledAt, session);

                //            session.CommitTransaction();
                //        }
                //        catch (Exception ex)
                //        {
                //            session.RollBackTransaction();
                //            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                //        }
                //        finally
                //        {
                //            session.CloseConnection();
                //        }

                //        RdgOrders.Rebind();

                //        FixPage();

                //        divGeneralSuccess.Visible = true;
                //        LblGeneralSuccess.Text = "Done! ";
                //        LblCancelSuccess.Text = "Your premium plan cancel successfully!";
                //    }
                //    else
                //    {
                //        divGeneralFailure.Visible = true;
                //        LblGeneralFailure.Text = "Error! ";
                //        LblCancelFailure.Text = "Your premium plan could not be canceled. Please try again later or contact with us";
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnInvoiceExport_Click(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LinkButton LnkBtn = (LinkButton)sender;
                    GridDataItem item = (GridDataItem)LnkBtn.NamingContainer;

                    //string guid = Guid.NewGuid().ToString();
                    //Session[guid] = item["id"].Text.ToString();

                    Response.Redirect("download-invoices?case=StripeInvoices&userID=" + vSession.User.Id.ToString() + "&paymentID=" + Convert.ToInt32(item["id"].Text) + "", false);
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

        protected void BtnCancelPlan_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor aBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                    int packId = Sql.GetPacketIdBySubscriptionID(item["subscription_id"].Text, session);
                    if (packId == (int)Packets.PremiumService)
                    {
                        UcConfirmationServiceMessageAlert.SubscriptionId = item["subscription_id"].Text;
                        UcConfirmationServiceMessageAlert.Refresh();
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenServiceConfirmationModal();", true);
                    }
                    else
                    {
                        UcConfirmationMessageAlert.SubscriptionId = item["subscription_id"].Text;
                        UcConfirmationMessageAlert.Refresh();
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationModal();", true);
                    }
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

        protected void BtnSearch_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region DropDownList

        #endregion        

        #region Tabs

        protected void aBillingHistory_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aBillingHistory.Attributes["class"] = "nav-link active";
                tab_1_1.Attributes["class"] = "tab-pane fade show active";
                tab_1_1.Visible = true;
                RdgOrders.Rebind();

                aEditBillingAccount.Attributes["class"] = "nav-link";
                tab_1_2.Attributes["class"] = "tab-pane fade";
                tab_1_2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aEditBillingAccount_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aEditBillingAccount.Attributes["class"] = "nav-link active";
                tab_1_2.Attributes["class"] = "tab-pane fade show active";
                tab_1_2.Visible = true;

                aBillingHistory.Attributes["class"] = "nav-link";
                tab_1_1.Attributes["class"] = "tab-pane fade";
                tab_1_1.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aElioBillingDetails_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aElioBillingDetails.Attributes["class"] = "nav-link active";
                tab_2_1.Attributes["class"] = "tab-pane fade show active";
                tab_2_1.Visible = true;
                UcBillingMessageControl.Visible = false;

                aStripeCreditCardDetails.Attributes["class"] = "nav-link";
                tab_2_2.Attributes["class"] = "tab-pane fade";
                tab_2_2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aStripeCreditCardDetails_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aStripeCreditCardDetails.Attributes["class"] = "nav-link active";
                tab_2_2.Attributes["class"] = "tab-pane fade show active";
                tab_2_2.Visible = true;
                UcCreditcardMessageControl.Visible = false;

                aElioBillingDetails.Attributes["class"] = "nav-link";
                tab_2_1.Attributes["class"] = "tab-pane fade";
                tab_2_1.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion        
    }
}