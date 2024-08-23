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

namespace WdS.ElioPlus
{
    public partial class DashboardBillingSelfServicePage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public bool HasDiscount { get; set; }

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

                    SetLinks();

                    aGrowthSignUp.Visible = ((vSession.User == null) || (vSession.User != null) && (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))) ? true : false;

                    if (!aGrowthSignUp.Visible)
                        FixPaymentBtns();

                    if (!IsPostBack)
                    {
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

        # region Methods

        private void FixPaymentBtns()
        {
            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, false, ref showBtn, ref showModal, session);

            if (allowPayment)
            {
                BtnGrowthGoPremium.Visible = showBtn;
                aGrowthPaymentModal.Visible = showModal;
            }
            else
            {
                BtnGrowthGoPremium.Visible = false;
                aGrowthPaymentModal.Visible = false;
            }
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
                //HasDiscount = Sql.HasUserDiscount(vSession.User.Id, session);                
                BtnPayment.Enabled = (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? true : false;
                MessageAlertHistory.Visible = false;
                //RdgOrders.MasterTableView.GetColumn("activate").Display = vSession.User.BillingType == 1 && vSession.User.CustomerStripeId != "";

                UpdateStrings();
                LoadPacketFeatures();
                LoadBillingInfo();

                //FixBillingInvoicesForOldCustomers();
            }

            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
                //divPayments.Visible = liPayments.Visible = true;
            //    LblRenewalHead.Visible = LblRenewal.Visible = true;
            //    LblRenewalHead.Text = "Renewal date: ";

            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }

            //    try
            //    {
            //        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");      //Sql.GetUserRenewalDateFromActiveOrder(vSession.User.Id, session).ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        LblRenewalHead.Visible = LblRenewal.Visible = false;

            //        Logger.Debug(string.Format("User {0} seems to be premium but he has no subscription in his account", vSession.User.Id.ToString()));
            //    }
            //}
            //else
            //{
            //    //divPayments.Visible = liPayments.Visible = Sql.HasUserOrderByStatus(vSession.User.Id, Convert.ToInt32(OrderStatus.Canceled), session);
            //    LblRenewalHead.Visible = LblRenewal.Visible = false;
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            divPricingPlan.Visible = (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? false : true;
            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                bool hasActiveService = Sql.HasActiveServiceSubscription(vSession.User.Id, session);
                if (hasActiveService)
                {
                    divServicePlan.Visible = false;
                }
                else
                {
                    divServicePlan.Visible = true;
                    LblGetElioService.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "20")).Text;
                    aGetElioService.Disabled = false;
                    aGetElioService.HRef = "#PaymentServiceModal";

                    ElioPackets packet = Sql.GetPacketById((int)Packets.PremiumService, session);
                    if (packet != null)
                    {
                        Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                        if (plan != null && !string.IsNullOrEmpty(plan.Id))
                        {
                            LblServiceCost.Text = (Convert.ToDecimal(plan.Amount) / 100).ToString() + " $ / Month";
                        }
                    }
                }
            }
            else
            {
                divServicePlan.Visible = false;
            }

            LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

            //LblDashboard.Text = "Dashboard";
            //LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Billing";
            LblDashSubTitle.Text = "your billing information";
        }

        private void UpdateStrings()
        {
            LblElioService.Text = "Managed Service";
            LblCommitment.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "62")).Text;

            LblSignUpGrowth.Text = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "60")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "31")).Text;
            LblGrowthGoPremium.Text = BtnGrowthGoPremium.Text = (vSession.User != null && vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && string.IsNullOrEmpty(vSession.User.CustomerStripeId)) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "26")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "button", "7")).Text;

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

            //LblBillingCompanyName.Text = "Company Name";
            //LblBillingCompanyVatNumber.Text = "Company Vat Number";
            //LblBillingCompanyPostCode.Text = "Company Post Code";
            //LblBillingCompanyAddress.Text = "Company Billing Address (street, city, country-state, country)";

            BtnSaveBillingDetails.Text = "Save";
            BtnAddNewCard.Text = "Add New Card";

            //LblFullName.Text = "Card Full Name";
            //LblAddress1.Text = "Address 1";
            //LblAddress2.Text = "Address 2";
            //LblOrigin.Text = "Origin";
            //LblCardType.Text = "Card Type";
            //LblCCNumber.Text = "Credit Card Number";
            //LblCvcNumber.Text = "CVC";
            //LblExpMonth.Text = "Expiration Month";
            //LblExpYear.Text = "Expiration Year";
            //LblZipCode.Text = "Zip Code";
            BtnSaveCreditCardDetails.Text = "Update Card";
            BtnAddNewCard.Text = "Add New Card";
            BtnCancelAddNewCard.Text = "Cancel";

            #endregion
        }

        private void ClearErrorFields()
        {
            UcCreditcardMessageControl.Visible = false;
            //divSaveSuccess.Visible = false;
            //divSaveFailure.Visible = false;

            //LblBillingCompanyNameError.Text = string.Empty;
            //LblBillingCompanyVatNumberError.Text = string.Empty;
            //LblBillingCompanyPostCodeError.Text = string.Empty;
            //LblBillingCompanyAddressError.Text = string.Empty;

            //divBillingCompanyName.Visible = false;
            //divBillingCompanyVatNumber.Visible = false;
            //divBillingCompanyPostCode.Visible = false;
            //divBillingCompanyAddress.Visible = false;

            //LblFullNameError.Text = string.Empty;
            //LblAddress1Error.Text = string.Empty;
            //LblAddress2Error.Text = string.Empty;
            //LblOriginError.Text = string.Empty;
            //LblCardTypeError.Text = string.Empty;
            //LblCvcNumberError.Text = string.Empty;
            //LblCCNumberError.Text = string.Empty;
            //LblExpMonthError.Text = string.Empty;
            //LblExpYearError.Text = string.Empty;
            //LblZipCodeError.Text = string.Empty;

            //divFullName.Visible = false;
            //divAddress1.Visible = false;
            //divAddress2.Visible = false;
            //divOrigin.Visible = false;
            //divCardType.Visible = false;
            //divCCNumber.Visible = false;
            //divCCNumber.Visible = false;
            //divExpMonth.Visible = false;
            //divExpYear.Visible = false;
            //divZipCode.Visible = false;
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            aGrowthSignUp.HRef = (vSession.User != null && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage : ControlLoader.SignUp;
        }

        private void LoadPacketFeatures12()
        {
            int? totalLeads = 0;
            int? totalMessages = 0;
            int? totalConnections = 0;
            int? totalCollaborationPartners = 0;
            int? totalFileStorage = 0;

            #region Premium Packet

            decimal totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.Premium), ref totalLeads, ref totalMessages, ref totalConnections, ref totalCollaborationPartners, ref totalFileStorage, session);

            ////LblOldPrice.Text = "$ 249.00 / month";
            ////LblCurrentPrice.Text = totalCost.ToString();
            ////LblMonthlyPlan.Text = "per month";
            ////LblPopular.Text = "Limited";
            ////LblPremConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());
            ////LblPremLeads.Text = "{count} leads".Replace("{count}", totalLeads.ToString());
            ////LblPremMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());
            ////LblPremAdvSearch.Text = "unlimited";

            ////if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && string.IsNullOrEmpty(vSession.User.CustomerStripeId))
            ////{
            ////    LblGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            ////}
            ////else
            ////    aGetElioNow.Visible = false;

            LblManagedText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "63")).Text;

            #endregion
        }

        private void LoadPacketFeatures()
        {
            //List<ElioPacketFeaturesItems> packetsFeaturesItems = Sql.GetPublicPacketTotalCostAndFeaturesById((int)Packets.PremiumSelfService, session);

            int? totalLeads = 0;
            int? totalMessages = 0;
            int? totalConnections = 0;
            int? totalManagePartners = 0;
            int? totalLibraryStorage = 0;

            #region Self Service Packet

            decimal totalCost = Sql.GetPacketTotalCostAndFeatures(Convert.ToInt32(Packets.SelfService), ref totalLeads, ref totalMessages, ref totalConnections, ref totalManagePartners, ref totalLibraryStorage, session);

            LblPremiumGrowthPrice.Text = totalCost.ToString();

            //LblPremiumGrowthLeads.Text = "{count} leads".Replace("{count}", totalLeads.ToString());

            LblPremiumGrowthMessages.Text = "{count} messages".Replace("{count}", totalMessages.ToString());

            LblPremiumGrowthConnections.Text = "{count} matches".Replace("{count}", totalConnections.ToString());

            LblPremiumGrowthManagePartners.Text = "manage {count} partners".Replace("{count}", totalManagePartners.ToString());

            LblPremiumGrowthLibraryStorage.Text = "{count} GB library storage".Replace("{count}", totalLibraryStorage.ToString());

            #endregion

            LblManagedText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "63")).Text;

        }

        public void ClearFields()
        {
            LblPaymentWarningMsgContent.Text = string.Empty;
            divPaymentWarningMsg.Visible = false;

            LblPaymentSuccessMsgContent.Text = string.Empty;
            divPaymentSuccessMsg.Visible = false;

            TbxCardNumber.Text = string.Empty;
            DrpExpMonth1.SelectedIndex = -1;
            TbxExpYear1.Text = string.Empty;
            TbxEmail.Text = string.Empty;
            TbxCVC.Text = string.Empty;

            if (vSession.User != null)
            {
                BtnPayment.Enabled = (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) ? false : true;
                TbxEmail.Text = vSession.User.Email;
            }
            else
                TbxEmail.Text = string.Empty;

            divDiscount.Visible = false;
            //divInfo.Visible = true;
            CbxCouponDiscount.Checked = false;

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat((int)Packets.SelfService, session).ToString() + " $";
        }

        private bool IsValidData()
        {
            bool isValid = true;

            //if (DrpStripePlans.SelectedValue == "0")
            //{
            //    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "9")).Text;
            //    isValid = false;
            //    return isValid;
            //}

            if (string.IsNullOrEmpty(TbxEmail.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "1")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (!Validations.IsEmail(TbxEmail.Text))
                {
                    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "2")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxCardNumber.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "3")).Text;
                isValid = false;
                return isValid;
            }

            if (DrpExpMonth1.SelectedValue == "0")
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "4")).Text;
                isValid = false;
                return isValid;
            }

            if (string.IsNullOrEmpty(TbxExpYear1.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "5")).Text;
                isValid = false;
                return isValid;
            }
            else
            {
                if (Convert.ToInt32(TbxExpYear1.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "7")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            if (string.IsNullOrEmpty(TbxCVC.Text))
            {
                LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "6")).Text;
                isValid = false;
                return isValid;
            }

            if (CbxCouponDiscount.Checked)
            {
                if (string.IsNullOrEmpty(TbxDiscount.Text))
                {
                    LblPaymentWarningMsgContent.Text = "Please enter your Coupon ID";       //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "label", "8")).Text;
                    isValid = false;
                    return isValid;
                }
            }

            return isValid;
        }

        protected void LoadPackets(int selectedPlan, bool activatePlans)
        {
            ElioPackets packet = Sql.GetPacketById((int)Packets.SelfService, session);

            if (packet != null)
            {
                TbxStripePlan.Text = packet.PackDescription;
            }

            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(selectedPlan), session).ToString() + " $";

            if (vSession.User != null)
            {
                TbxEmail.Text = vSession.User.Email;
            }
            else
                TbxEmail.Text = string.Empty;
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

        //private void FixCustomerSubscriptionInvoices()
        //{
        //    if (vSession.User != null)
        //    {
        //        if (vSession.User.BillingType != (int)BillingTypePacket.FreemiumPacketType)
        //        {
        //            if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
        //            {
        //                StripeCustomer customer = Lib.Services.StripeAPI.StripeService.GetCustomer(vSession.User.CustomerStripeId);
        //                if (customer != null && !string.IsNullOrEmpty(customer.Id))
        //                {
        //                    if (customer.Deleted != null && !Convert.ToBoolean(customer.Deleted))
        //                    {
        //                        bool hasSubscriptions = customer.Subscriptions.Count() > 0;
        //                        if (hasSubscriptions)
        //                        {
        //                            foreach (StripeSubscription subscription in customer.Subscriptions)
        //                            {
        //                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
        //                                {
        //                                    bool exists = Sql.ExistCustomerSubscription(vSession.User.CustomerStripeId, subscription.Id, session);
        //                                    if (!exists)
        //                                    {
        //                                        ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

        //                                        string coupon = "";
        //                                        if (subscription.StripeDiscount != null && subscription.StripeDiscount.StripeCoupon != null && !string.IsNullOrEmpty(subscription.StripeDiscount.StripeCoupon.Id))
        //                                            coupon = subscription.StripeDiscount.StripeCoupon.Id;

        //                                        sub.UserId = vSession.User.Id;
        //                                        sub.CustomerId = subscription.CustomerId;
        //                                        sub.SubscriptionId = subscription.Id;
        //                                        sub.CouponId = (coupon != "") ? coupon : "";
        //                                        sub.PlanId = subscription.StripePlan.Id;
        //                                        sub.PlanNickname = subscription.StripePlan.Nickname;
        //                                        sub.CreatedAt = Convert.ToDateTime(subscription.Created);
        //                                        sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
        //                                        sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
        //                                        sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
        //                                        sub.HasDiscount = sub.CouponId != "" ? 1 : 0;
        //                                        sub.Status = subscription.Status;
        //                                        sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
        //                                        sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
        //                                        sub.Amount = (int)subscription.StripePlan.Amount;

        //                                        DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
        //                                        loader.Insert(sub);

        //                                        StripeList<StripeInvoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscription(subscription.CustomerId, subscription.Id);
        //                                        if (invoices != null && invoices.Count() > 0)
        //                                        {
        //                                            foreach (StripeInvoice invoice in invoices)
        //                                            {
        //                                                if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
        //                                                {
        //                                                    bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
        //                                                    if (!existInvoice)
        //                                                    {
        //                                                        ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

        //                                                        subInvoice.UserId = vSession.User.Id;
        //                                                        subInvoice.UserSubscriptionId = sub.Id;
        //                                                        subInvoice.CustomerId = invoice.CustomerId;
        //                                                        subInvoice.InvoiceId = invoice.Id;
        //                                                        subInvoice.ChargeId = invoice.ChargeId;
        //                                                        subInvoice.SubscriptionId = invoice.SubscriptionId;
        //                                                        subInvoice.IsClosed = (bool)invoice.Closed ? 1 : 0;
        //                                                        subInvoice.Currency = invoice.Currency;
        //                                                        subInvoice.Date = Convert.ToDateTime(invoice.Date);
        //                                                        subInvoice.Description = invoice.Description;
        //                                                        subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
        //                                                        subInvoice.InvoicePdf = invoice.InvoicePdf;
        //                                                        subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
        //                                                        subInvoice.Number = invoice.Number;
        //                                                        subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
        //                                                        subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
        //                                                        subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
        //                                                        subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
        //                                                        subInvoice.HasDiscount = sub.HasDiscount;
        //                                                        subInvoice.TotalAmount = invoice.Total;
        //                                                        subInvoice.SubTotalAmount = invoice.Subtotal;
        //                                                        subInvoice.CouponId = "";

        //                                                        DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
        //                                                        invIoader.Insert(subInvoice);
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                    else
        //                                    {
        //                                        ElioUsersSubscriptions sub = Sql.GetSubscriptionBySubID(subscription.Id, session);
        //                                        if (sub != null)
        //                                        {
        //                                            StripeList<StripeInvoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscription(subscription.CustomerId, subscription.Id);
        //                                            if (invoices != null && invoices.Count() > 0)
        //                                            {
        //                                                foreach (StripeInvoice invoice in invoices)
        //                                                {
        //                                                    if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
        //                                                    {
        //                                                        bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
        //                                                        if (!existInvoice)
        //                                                        {
        //                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

        //                                                            subInvoice.UserId = vSession.User.Id;
        //                                                            subInvoice.UserSubscriptionId = sub.Id;
        //                                                            subInvoice.CustomerId = invoice.CustomerId;
        //                                                            subInvoice.InvoiceId = invoice.Id;
        //                                                            subInvoice.ChargeId = invoice.ChargeId;
        //                                                            subInvoice.SubscriptionId = invoice.SubscriptionId;
        //                                                            subInvoice.IsClosed = (bool)invoice.Closed ? 1 : 0;
        //                                                            subInvoice.Currency = invoice.Currency;
        //                                                            subInvoice.Date = Convert.ToDateTime(invoice.Date);
        //                                                            subInvoice.Description = invoice.Description;
        //                                                            subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
        //                                                            subInvoice.InvoicePdf = invoice.InvoicePdf;
        //                                                            subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
        //                                                            subInvoice.Number = invoice.Number;
        //                                                            subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
        //                                                            subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
        //                                                            subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
        //                                                            subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
        //                                                            subInvoice.HasDiscount = sub.HasDiscount;
        //                                                            subInvoice.TotalAmount = invoice.Total;
        //                                                            subInvoice.SubTotalAmount = invoice.Subtotal;

        //                                                            if (invoice.Subscription.StripeDiscount != null)
        //                                                                if (invoice.Subscription.StripeDiscount.StripeCoupon != null)
        //                                                                {
        //                                                                    StripeCoupon coupon = invoice.Subscription.StripeDiscount.StripeCoupon;
        //                                                                    if (coupon != null && !string.IsNullOrEmpty(coupon.Id))
        //                                                                        subInvoice.CouponId = coupon.Id;
        //                                                                    else
        //                                                                        subInvoice.CouponId = "";
        //                                                                }
        //                                                                else
        //                                                                    subInvoice.CouponId = "";
        //                                                            else
        //                                                                subInvoice.CouponId = "";

        //                                                            DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
        //                                                            invIoader.Insert(subInvoice);
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }
        //                                        }
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    else
        //        Response.Redirect(ControlLoader.Login, false);
        //}

        #endregion

        #region Grids

        protected void RdgOrders_OnItemDataBound_old(object sender, GridItemEventArgs e)
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    //Label lblDateCreated = (Label)ControlFinder.FindControlRecursive(item, "LblDateCreated");
                    //lblDateCreated.Text = item["sysdate"].Text;
                    item["sysdate"].Text = string.Empty;

                    //Label lblCurrentPeriodStart = (Label)ControlFinder.FindControlRecursive(item, "LblCurrentPeriodStart");
                    //lblCurrentPeriodStart.Text = item["current_period_start"].Text;
                    item["current_period_start"].Text = string.Empty;

                    //Label lblCurrentPeriodEnd = (Label)ControlFinder.FindControlRecursive(item, "LblCurrentPeriodEnd");
                    //lblCurrentPeriodEnd.Text = item["current_period_end"].Text;
                    item["current_period_end"].Text = string.Empty;

                    Label lblPrice = (Label)ControlFinder.FindControlRecursive(item, "LblPrice");
                    lblPrice.Text = item["cost_with_vat"].Text;
                    item["cost_with_vat"].Text = string.Empty;

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                    Label lblPlan = (Label)ControlFinder.FindControlRecursive(item, "LblPlan");
                    lblPlan.Text = item["plan"].Text;
                    item["plan"].Text = string.Empty;

                    //Label lblMode = (Label)ControlFinder.FindControlRecursive(item, "LblMode");
                    //lblMode.Text = item["mode"].Text;
                    //item["mode"].Text = string.Empty;

                    //Label lblOrderType = (Label)ControlFinder.FindControlRecursive(item, "LblOrderType");

                    //Label lblCanceledAt = (Label)ControlFinder.FindControlRecursive(item, "LblCanceledAt");
                    //lblCanceledAt.Text = item["canceled_at"].Text;
                    item["canceled_at"].Text = string.Empty;

                    HtmlAnchor btnCancelPlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnCancelPlan");
                    HtmlAnchor btnActivatePlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnActivatePlan");
                    //btnCancelPlan.Visible = ((Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder)) && Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)) ? true : false;

                    //if (btnCancelPlan.Visible)
                    //{
                    Label lblBtnCancelPlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnCancelPlan");
                    Label lblBtnActivatePlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnActivatePlan");
                    //lblBtnActivatePlan.Text = "Activate Service Plan";

                    //HtmlAnchor aInvoiceDownload = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aInvoiceDownload");
                    //Label lblInvoiceDownload = (Label)ControlFinder.FindControlRecursive(item, "LblInvoiceDownload");

                    //if (Convert.ToInt32(item["is_paid"].Text) == Convert.ToInt32(OrderStatus.Paid) && item["mode"].Text == OrderMode.Active.ToString() && Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active))
                    //{
                    //    aInvoiceDownload.Visible = true;
                    //    aInvoiceDownload.HRef = ControlLoader.DownloadInvoices(vSession.User.Id, Convert.ToInt32(item["id"].Text));

                    //    //aInvoiceDownload.HRef = "download-invoices?case=StripeInvoices&userID=" + vSession.User.Id.ToString() + "&orderID=" + Convert.ToInt32(item["id"].Text) + "";

                    //    //aInvoiceDownload.HRef = "download-invoices?case=StripeInvoices";
                    //    lblInvoiceDownload.Text = "Download invoice pdf";
                    //}
                    //else
                    //{
                    //    aInvoiceDownload.Visible = false;
                    //    aInvoiceDownload.HRef = "#";
                    //    lblInvoiceDownload.Text = "invoice not available";
                    //}

                    if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)))
                    {
                        btnCancelPlan.Visible = true;
                        lblBtnCancelPlan.Text = "Cancel";
                        btnCancelPlan.Disabled = false;

                        btnActivatePlan.Visible = false;
                        btnActivatePlan.Disabled = true;
                    }
                    else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled)))
                    {
                        btnCancelPlan.Visible = false;
                        lblBtnCancelPlan.Text = "Cancel";
                        btnCancelPlan.Disabled = true;

                        btnActivatePlan.Visible = true;
                        lblBtnActivatePlan.Text = "Activate Plan";
                        btnActivatePlan.Disabled = !Sql.IsUserLastOrder(vSession.User.Id, Convert.ToInt32(item["id"].Text), session);
                    }
                    else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.ServiceNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled)))
                    {
                        btnCancelPlan.Visible = true;
                        lblBtnCancelPlan.Text = "Contact us";
                        btnCancelPlan.Disabled = true;

                        btnActivatePlan.Visible = false;
                        btnActivatePlan.Disabled = true;
                    }
                    //else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.ServiceNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)))
                    //{
                    //    btnCancelPlan.Visible = false;
                    //    lblBtnCancelPlan.Text = "Contact us";
                    //    btnCancelPlan.Disabled = true;

                    //    btnActivatePlan.Visible = true;
                    //    btnActivatePlan.Disabled = false;
                    //    lblBtnActivatePlan.Text = "Activate Service Plan";
                    //}
                    //else
                    //{
                    //    btnCancelPlan.Visible = false;
                    //    btnActivatePlan.Visible = true;
                    //}
                    //}

                    //Label lblTrialLeft = (Label)ControlFinder.FindControlRecursive(item, "LblTrialLeft");

                    switch (item["order_status"].Text)
                    {
                        case "1":

                            if (item["is_paid"].Text == "1")
                                lblStatus.Text = "Active & Paid";
                            else
                                lblStatus.Text = "Active & Not Paid";

                            //lblTrialLeft.Text = (item["order_type"].Text == Convert.ToInt32(OrderType.PacketNewOrder).ToString() || item["order_type"].Text == Convert.ToInt32(OrderType.RefundedNewOrder).ToString()) ? item["trial_left"].Text + "  trial day left" : "No trial avaiable";

                            break;

                        case "-2":

                            lblStatus.Text = "Expired";
                            //lblTrialLeft.Text = "-";

                            break;

                        case "-3":

                            lblStatus.Text = "Inactive";
                            //lblTrialLeft.Text = "-";

                            break;
                    }

                    switch (item["order_type"].Text)
                    {
                        case "1":

                            //lblOrderType.Text = "Packet New Order";
                            btnCancelPlan.HRef = "#ConfirmationModal";

                            btnActivatePlan.HRef = "#PaymentPacketsModal";
                            break;

                        case "2":

                            //lblOrderType.Text = "Refunded";
                            btnCancelPlan.HRef = "#ConfirmationModal";

                            break;

                        case "3":

                            //lblOrderType.Text = "Service New Order";
                            btnCancelPlan.Visible = true;
                            btnActivatePlan.Visible = false;

                            if (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active))
                            {
                                btnCancelPlan.HRef = "#ConfirmationServiceModal";
                                lblBtnCancelPlan.Text = "Cancel";

                            }
                            else if (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled))
                            {
                                btnCancelPlan.HRef = "";
                                btnActivatePlan.HRef = "#ConfirmationActivateServiceModal";
                            }

                            break;
                    }

                    item["trial_left"].Text = string.Empty;
                    item["order_status"].Text = string.Empty;
                    item["order_type"].Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgOrders_OnNeedDataSource_old(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioBillingUserOrders> orders = Sql.GetUserOrders(vSession.User.Id, session);
                    if (orders.Count > 0)
                    {
                        RdgOrders.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("order_status");
                        table.Columns.Add("pack_id");
                        table.Columns.Add("plan");
                        table.Columns.Add("mode");
                        table.Columns.Add("trial_left");
                        table.Columns.Add("sysdate");
                        table.Columns.Add("current_period_start");
                        table.Columns.Add("current_period_end");
                        table.Columns.Add("canceled_at");
                        table.Columns.Add("cost_with_vat");
                        table.Columns.Add("is_paid");
                        table.Columns.Add("order_type");

                        foreach (ElioBillingUserOrders order in orders)
                        {
                            TimeSpan trialdays = (order.Mode == OrderMode.Trialing.ToString()) ? Convert.ToDateTime(order.CurrentPeriodEnd) - Convert.ToDateTime(DateTime.Now) : DateTime.Now - DateTime.Now;
                            ElioPackets packet = Sql.GetPacketById(order.PackId, session);
                            if (packet != null)
                            {
                                table.Rows.Add(order.Id, order.OrderStatus, packet.Id, packet.PackDescription, order.Mode
                                    , trialdays.Days, Convert.ToDateTime(order.Sysdate).ToString("dd/MM/yyyy")
                                    , Convert.ToDateTime(order.CurrentPeriodStart).ToString("dd/MM/yyyy")
                                    , (order.OrderStatus == Convert.ToInt32(OrderStatus.Active)) ? Convert.ToDateTime(order.CurrentPeriodEnd).ToString("dd/MM/yyyy") : "-"
                                    , (Convert.ToInt32(order.OrderStatus) == Convert.ToInt32(OrderStatus.Canceled)) ? Convert.ToDateTime(order.CanceledAt).ToString("dd/MM/yyyy") : "-"
                                    , order.CostWithVat + " $", order.IsPaid, order.OrderType);
                            }
                        }

                        RdgOrders.DataSource = table;
                    }
                    else
                    {
                        RdgOrders.Visible = false;
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

        protected void RdgOrders_OnItemDataBound_v1(object sender, GridItemEventArgs e)
        {
            try
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblDateCreated = (Label)ControlFinder.FindControlRecursive(item, "LblDateCreated");
                    lblDateCreated.Text = item["date_created"].Text;
                    item["date_created"].Text = string.Empty;

                    //Label lblCurrentPeriodStart = (Label)ControlFinder.FindControlRecursive(item, "LblCurrentPeriodStart");
                    //lblCurrentPeriodStart.Text = item["current_period_start"].Text;
                    item["current_period_start"].Text = string.Empty;

                    //Label lblCurrentPeriodEnd = (Label)ControlFinder.FindControlRecursive(item, "LblCurrentPeriodEnd");
                    //lblCurrentPeriodEnd.Text = item["current_period_end"].Text;
                    item["current_period_end"].Text = string.Empty;

                    Label lblPrice = (Label)ControlFinder.FindControlRecursive(item, "LblPrice");
                    lblPrice.Text = item["cost_with_vat"].Text;
                    item["cost_with_vat"].Text = string.Empty;

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                    Label lblPlan = (Label)ControlFinder.FindControlRecursive(item, "LblPlan");
                    lblPlan.Text = item["plan"].Text;
                    item["plan"].Text = string.Empty;

                    //Label lblDateCreated = (Label)ControlFinder.FindControlRecursive(item, "LblDateCreated");
                    //lblDateCreated.Text = item["date_created"].Text;
                    //item["date_created"].Text = string.Empty;

                    //Label lblOrderType = (Label)ControlFinder.FindControlRecursive(item, "LblOrderType");

                    //Label lblCanceledAt = (Label)ControlFinder.FindControlRecursive(item, "LblCanceledAt");
                    //lblCanceledAt.Text = item["canceled_at"].Text;
                    item["canceled_at"].Text = string.Empty;

                    HtmlAnchor btnCancelPlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnCancelPlan");
                    HtmlAnchor btnActivatePlan = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnActivatePlan");
                    //btnCancelPlan.Visible = ((Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder)) && Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)) ? true : false;

                    //if (btnCancelPlan.Visible)
                    //{
                    Label lblBtnCancelPlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnCancelPlan");
                    Label lblBtnActivatePlan = (Label)ControlFinder.FindControlRecursive(item, "LblBtnActivatePlan");
                    //lblBtnActivatePlan.Text = "Activate Service Plan";

                    #region to delete

                    //if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)))
                    //{
                    //    btnCancelPlan.Visible = true;
                    //    lblBtnCancelPlan.Text = "Cancel";
                    //    btnCancelPlan.Disabled = false;

                    //    btnActivatePlan.Visible = false;
                    //    btnActivatePlan.Disabled = true;
                    //}
                    //else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.PacketNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled)))
                    //{
                    //    btnCancelPlan.Visible = false;
                    //    lblBtnCancelPlan.Text = "Cancel";
                    //    btnCancelPlan.Disabled = true;

                    //    btnActivatePlan.Visible = true;
                    //    lblBtnActivatePlan.Text = "Activate Plan";
                    //    btnActivatePlan.Disabled = !Sql.IsUserLastOrder(vSession.User.Id, Convert.ToInt32(item["id"].Text), session);
                    //}
                    //else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.ServiceNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled)))
                    //{
                    //    btnCancelPlan.Visible = true;
                    //    lblBtnCancelPlan.Text = "Contact us";
                    //    btnCancelPlan.Disabled = true;

                    //    btnActivatePlan.Visible = false;
                    //    btnActivatePlan.Disabled = true;
                    //}
                    //else if (Convert.ToInt32(item["order_type"].Text) == Convert.ToInt32(OrderType.ServiceNewOrder) && (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active)))
                    //{
                    //    btnCancelPlan.Visible = false;
                    //    lblBtnCancelPlan.Text = "Contact us";
                    //    btnCancelPlan.Disabled = true;

                    //    btnActivatePlan.Visible = true;
                    //    btnActivatePlan.Disabled = false;
                    //    lblBtnActivatePlan.Text = "Activate Service Plan";
                    //}
                    //else
                    //{
                    //    btnCancelPlan.Visible = false;
                    //    btnActivatePlan.Visible = true;
                    //}
                    //}

                    //Label lblTrialLeft = (Label)ControlFinder.FindControlRecursive(item, "LblTrialLeft");

                    #endregion

                    if (item["mode"].Text == "Active")
                    {
                        #region Service Plan

                        //if (Convert.ToInt32(item["pack_id"].Text) == Convert.ToInt32(Packets.PremiumService))
                        //{
                        //    if (Sql.IsLastServicePayment(Convert.ToInt32(item["payment_id"].Text), session))
                        //    {
                        //        btnCancelPlan.Visible = true;
                        //        btnCancelPlan.HRef = "#ConfirmationServiceModal";
                        //        lblBtnCancelPlan.Text = "Cancel Service plan";
                        //    }
                        //    else
                        //    {
                        //        btnCancelPlan.Visible = false;
                        //        btnCancelPlan.HRef = "#";
                        //        lblBtnCancelPlan.Text = "";
                        //    }
                        //}

                        #endregion

                        if (Sql.IsCurrentUserPacketPayment(vSession.User.Id, Convert.ToInt32(item["id"].Text), session))
                        {
                            switch (item["order_type"].Text)
                            {
                                case "1":
                                    btnCancelPlan.Visible = true;
                                    //lblOrderType.Text = "Packet New Order";
                                    btnCancelPlan.HRef = "#ConfirmationModal";
                                    lblBtnCancelPlan.Text = "Cancel plan";
                                    btnActivatePlan.HRef = "#PaymentPacketsModal";
                                    break;

                                case "2":

                                    //lblOrderType.Text = "Refunded";
                                    btnCancelPlan.HRef = "#ConfirmationModal";
                                    lblBtnCancelPlan.Text = "Cancel plan";
                                    break;

                                case "3":

                                    //lblOrderType.Text = "Service New Order";
                                    btnCancelPlan.Visible = true;
                                    btnActivatePlan.Visible = false;
                                    lblBtnCancelPlan.Text = "Cancel plan";

                                    if (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Active))
                                    {
                                        btnCancelPlan.HRef = "#ConfirmationServiceModal";
                                        lblBtnCancelPlan.Text = "Cancel plan";

                                    }
                                    else if (Convert.ToInt32(item["order_status"].Text) == Convert.ToInt32(OrderStatus.Canceled))
                                    {
                                        btnCancelPlan.HRef = "";
                                        btnActivatePlan.HRef = "#ConfirmationActivateServiceModal";
                                    }

                                    break;
                            }
                        }
                        else
                        {
                            btnCancelPlan.Visible = false;
                            btnActivatePlan.Visible = false;
                        }
                    }
                    else if (item["mode"].Text == "Canceled")
                    {
                        //RdgOrders.MasterTableView.GetColumn("activate").Display = Convert.ToInt32(item["order_type"].Text) == (int)OrderType.PacketNewOrder ? true : false;

                        if (Sql.IsCurrentUserPacketPayment(vSession.User.Id, Convert.ToInt32(item["id"].Text), session))
                        {
                            btnCancelPlan.Visible = false;
                            btnActivatePlan.Visible = true;
                            lblBtnActivatePlan.Text = "Activate plan";
                        }
                        else
                        {
                            btnCancelPlan.Visible = false;
                            btnActivatePlan.Visible = false;
                        }
                    }
                    else
                    {
                        //RdgOrders.MasterTableView.GetColumn("activate").Display = Convert.ToInt32(item["order_type"].Text) == (int)OrderType.PacketNewOrder ? true : false;

                        if (item["order_status"].Text == "-2" && item["is_paid"].Text == "1")
                        {
                            lblStatus.Text = "Expired & Paid";
                        }
                        else if (item["order_status"].Text == "-2" && item["is_paid"].Text == "0")
                        {
                            lblStatus.Text = "Expired & Not Paid";
                        }
                        else if (item["order_status"].Text == "-3" && item["is_paid"].Text == "1")
                        {
                            lblStatus.Text = "Inactive";
                        }

                        btnCancelPlan.Visible = false;
                        btnActivatePlan.Visible = false;
                    }

                    if (Sql.IsCurrentUserPacketPayment(vSession.User.Id, Convert.ToInt32(item["id"].Text), session))
                    {
                        switch (item["order_status"].Text)
                        {
                            case "1":

                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Active & Paid";
                                else
                                    lblStatus.Text = "Active & Not Paid";

                                //lblTrialLeft.Text = (item["order_type"].Text == Convert.ToInt32(OrderType.PacketNewOrder).ToString() || item["order_type"].Text == Convert.ToInt32(OrderType.RefundedNewOrder).ToString()) ? item["trial_left"].Text + "  trial day left" : "No trial avaiable";

                                break;

                            case "-2":

                                lblStatus.Text = "Expired";
                                //lblTrialLeft.Text = "-";

                                break;

                            case "-3":

                                lblStatus.Text = "Inactive";
                                //lblTrialLeft.Text = "-";

                                break;
                        }
                    }
                    else
                    {
                        if (item["order_status"].Text == "1")
                        {
                            if (item["is_paid"].Text == "1")
                                lblStatus.Text = "Active & Paid";
                            else
                                lblStatus.Text = "Active & Not Paid";
                        }
                        else
                        {
                            if (item["order_status"].Text == "-2")
                            {
                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Expired & Paid";
                                else
                                    lblStatus.Text = "Expired & Not Paid";
                            }
                            else if (item["order_status"].Text == "-3")
                            {
                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Inactive & Paid";
                                else
                                    lblStatus.Text = "Inactive & Not Paid";
                            }
                        }
                    }

                    item["trial_left"].Text = string.Empty;
                    item["order_status"].Text = string.Empty;
                    item["order_type"].Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgOrders_OnNeedDataSource_v1(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //List<ElioBillingUserOrders> orders = Sql.GetUserOrders(vSession.User.Id, session);
                    List<ElioBillingUserOrdersPayments> payments = Sql.GetUserOrdersPayments(vSession.User.Id, session);
                    if (payments.Count > 0)
                    {
                        //liBillingHistory.Visible = true;
                        //ElioBillingUserOrders order = Sql.GetOrderById(payments[0].OrderId, session);
                        //if (order != null)
                        //{
                        RdgOrders.Visible = true;
                        MessageAlertHistory.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("order_id");
                        table.Columns.Add("order_status");
                        table.Columns.Add("pack_id");
                        table.Columns.Add("plan");
                        table.Columns.Add("mode");
                        table.Columns.Add("trial_left");
                        table.Columns.Add("date_created");
                        table.Columns.Add("current_period_start");
                        table.Columns.Add("current_period_end");
                        table.Columns.Add("canceled_at");
                        table.Columns.Add("cost_with_vat");
                        table.Columns.Add("is_paid");
                        table.Columns.Add("order_type");

                        foreach (ElioBillingUserOrdersPayments payment in payments)
                        {
                            ElioBillingUserOrders order = Sql.GetOrderById(payment.OrderId, session);
                            if (order != null)
                            {
                                TimeSpan trialdays = (order.Mode == OrderMode.Trialing.ToString()) ? Convert.ToDateTime(payment.CurrentPeriodEnd) - Convert.ToDateTime(DateTime.Now) : DateTime.Now - DateTime.Now;

                                ElioPackets packet = Sql.GetPacketById(payment.PackId, session);
                                if (packet != null)
                                {
                                    table.Rows.Add(payment.Id, payment.OrderId, order.OrderStatus, packet.Id, (packet.Id == Convert.ToInt32(Packets.PremiumService)) ? "Service" : packet.PackDescription, payment.Mode
                                        , trialdays.Days, Convert.ToDateTime(payment.DateCreated).ToString("dd/MM/yyyy")
                                        , Convert.ToDateTime(payment.CurrentPeriodStart).ToString("dd/MM/yyyy")
                                        , (order.OrderStatus == Convert.ToInt32(OrderStatus.Active)) ? Convert.ToDateTime(payment.CurrentPeriodEnd).ToString("dd/MM/yyyy") : "-"
                                        , (Convert.ToInt32(order.OrderStatus) == Convert.ToInt32(OrderStatus.Canceled)) ? Convert.ToDateTime(order.CanceledAt).ToString("dd/MM/yyyy") : "-"
                                        , payment.Amount + " $", order.IsPaid, order.OrderType);
                                }
                            }
                            else
                            {
                                RdgOrders.Visible = false;
                                //divPayments.Visible = liPayments.Visible = false;
                            }
                        }

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

                                lblStatus.CssClass = "label label-sm label-success";

                                if (!string.IsNullOrEmpty(item["coupon_id"].Text) && item["coupon_id"].Text != "&nbsp;" && item["coupon_id"].Text.ToString() != "")
                                    btnCancelPlan.Visible = Sql.CanCancelSubscriptionByCouponRedeemByDate(item["coupon_id"].Text.ToString(), session);
                                else
                                    btnCancelPlan.Visible = true;

                                btnActivatePlan.Visible = false;
                            }
                            else if (item["status"].Text == "canceled")
                            {
                                if (item["is_paid"].Text == "1")
                                    lblStatus.Text = "Canceled & Paid";
                                else
                                    lblStatus.Text = "Canceled & Not Paid";

                                lblStatus.CssClass = "label label-sm label-danger";

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

                        lblStatus.CssClass = "label label-sm label-success";

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

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("user_subscription_id");
                        //table.Columns.Add("user_id");
                        //table.Columns.Add("invoice_id");
                        //table.Columns.Add("charge_id");
                        //table.Columns.Add("subscription_id");
                        //table.Columns.Add("is_closed");
                        //table.Columns.Add("date");
                        //table.Columns.Add("invoice_pdf");
                        //table.Columns.Add("number");
                        //table.Columns.Add("is_paid");
                        //table.Columns.Add("period_start");
                        //table.Columns.Add("period_end");
                        //table.Columns.Add("total_amount");
                        //table.Columns.Add("sub_total_amount");

                        //foreach (ElioUsersSubscriptionsInvoices invoice in invoices)
                        //{
                        //    table.Rows.Add(invoice.Id, invoice.UserSubscriptionId, invoice.UserId, invoice.InvoiceId, invoice.ChargeId,
                        //        invoice.SubscriptionId, invoice.IsClosed, invoice.Date, invoice.InvoicePdf, invoice.Number, invoice.IsPaid,
                        //        invoice.PeriodStart, invoice.PeriodEnd, invoice.TotalAmount, invoice.SubTotalAmount);
                        //}

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

                        vSession.User.HasBillingDetails = 1;
                        vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

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

                //if (BtnAddNewCard.Text == "Add New Card" || (!div5.Visible && !div4.Visible))
                //{
                //    div5.Visible = true;
                //    div4.Visible = true;
                //    BtnAddNewCard.Text = "Save New Card";
                //    BtnCancelAddNewCard.Visible = true;

                //    TbxFullName.Text = string.Empty;
                //    TbxAddress1.Text = string.Empty;
                //    TbxAddress2.Text = string.Empty;
                //    TbxOrigin.Text = string.Empty;
                //    TbxCardType.Text = string.Empty;
                //    TbxCCNumber.Text = string.Empty;
                //    TbxCvcNumber.Text = string.Empty;
                //    TbxExpMonth.Text = string.Empty;
                //    TbxExpYear.Text = string.Empty;
                //    TbxZipCode.Text = string.Empty;

                //    return;
                //}

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

                //ElioUsersCreditCards userCard = null;
                //if (vSession.User.Id == 2386)
                //{
                //    userCard = Sql.GetUserDefaultCreditCardByUserId(vSession.User.Id, session);
                //    vSession.User.CustomerStripeId = userCard.CustomerStripeId;
                //}
                //else
                //    userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);

                //string defaultCard = string.Empty;
                //string mode = string.Empty;

                //if (userCard != null)
                //{
                //    mode = "DELETE";
                //    defaultCard = userCard.CardStripeId;
                //}
                //else
                //{
                //    mode = "ADD";
                //    defaultCard = string.Empty;
                //}

                if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                {
                    try
                    {
                        //Xamarin.Payments.Stripe.StripeCard cardInfo = StripeLib.AddNewCreditCard(mode, vSession.User.CustomerStripeId, defaultCard, TbxCvcNumber.Text,
                        //                                                                        Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text),
                        //                                                                        TbxCCNumber.Text, vSession.User.LastName + " " + vSession.User.FirstName, vSession.User.Address, TbxAddress2.Text,
                        //                                                                        TbxOrigin.Text, TbxZipCode.Text, ref errorMsg);

                        StripeCustomer customer = Lib.Services.StripeAPI.StripeService.GetCustomer(vSession.User.CustomerStripeId);
                        if (customer != null && !string.IsNullOrEmpty(customer.Id))
                        {
                            StripeCard defaultCard = Lib.Services.StripeAPI.StripeService.DeleteCreditCard(customer.Id, customer.DefaultSource);
                            if (defaultCard != null && !string.IsNullOrEmpty(defaultCard.Id))
                            {
                                StripeToken cardToken = Lib.Services.StripeAPI.StripeService.CreateCardToken(TbxCCNumber.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), TbxCvcNumber.Text, customer.Description, false);

                                if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                {
                                    StripeCard card = Lib.Services.StripeAPI.StripeService.CreateCreditCard(customer.Id, cardToken.Id);
                                    if (card != null && !string.IsNullOrEmpty(card.Id))
                                    {
                                        if (card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Pass)
                                        {
                                            GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your Credit Card changed successfully", MessageTypes.Success, true, true, true, true, false);

                                            BtnAddNewCard.Text = "Add New Card";
                                            //BtnCancelAddNewCard.Visible = false;
                                            TbxCCNumber.Text = string.Empty;
                                            TbxCvcNumber.Text = string.Empty;
                                            DrpExpMonth.SelectedValue = "0";
                                            TbxExpYear.Text = string.Empty;
                                        }
                                        else
                                        {
                                            GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card cvc check could not passed. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                                            return;
                                        }
                                    }
                                    else
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                                        return;
                                    }
                                }
                                else
                                {
                                    GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                                    return;
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be changed. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                                Logger.DetailedError(Request.Url.ToString(), string.Format("BtnAddNewCard_OnClick --> User: {0} or customer {1} tried to change his credit card but the old could not be deleted at {2}", vSession.User.Id, vSession.User.CustomerStripeId, DateTime.Now), "DashboardBilling.aspx Page");

                                return;
                            }
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }

                        //if (cardInfo != null)
                        //{
                        //    try
                        //    {
                        //        ElioUsersCreditCards cc = new ElioUsersCreditCards();

                        //        cc.CardStripeId = cardInfo.ID;
                        //        cc.CardFullname = cardInfo.Name;
                        //        cc.Address1 = cardInfo.AddressLine1;
                        //        cc.Address2 = cardInfo.AddressLine2;
                        //        cc.CardType = cardInfo.Type;
                        //        cc.ExpMonth = cardInfo.ExpirationMonth;
                        //        cc.ExpYear = cardInfo.ExpirationYear;
                        //        cc.Origin = cardInfo.AddressCountry;
                        //        cc.CvcCheck = cardInfo.CvcCheck.ToString();
                        //        cc.Fingerprint = cardInfo.Fingerprint;
                        //        cc.ZipCheck = cardInfo.AddressZipCheck;
                        //        cc.CardType = cardInfo.Type;
                        //        cc.IsDefault = 1;
                        //        cc.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                        //        cc.Sysdate = DateTime.Now;
                        //        cc.LastUpdated = DateTime.Now;
                        //        cc.CustomerStripeId = vSession.User.CustomerStripeId;
                        //        cc.UserId = vSession.User.Id;

                        //        DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                        //        loader.Insert(cc);

                        //        if (userCard != null)
                        //        {
                        //            userCard.IsDefault = 0;
                        //            userCard.LastUpdated = DateTime.Now;

                        //            loader.Update(userCard);
                        //        }

                        //        divSaveCreditCardSuccess.Visible = true;
                        //        LblSaveCreditCardSuccess.Text = "Your Credit Card Details saved successfully";
                        //        BtnAddNewCard.Text = "Add New Card";
                        //        //BtnCancelAddNewCard.Visible = false;
                        //        TbxCCNumber.Text = string.Empty;
                        //        TbxCvcNumber.Text = string.Empty;
                        //        DrpExpMonth.SelectedValue = "0";
                        //        TbxExpYear.Text = string.Empty;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //    }
                        //}
                        //else
                        //{
                        //    divSaveCreditCardFailure.Visible = true;
                        //    LblSaveCreditCardFailureError.Text = "Error! ";
                        //    LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";
                        //}
                    }
                    catch (Exception ex)
                    {
                        GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
                else
                {
                    throw new Exception(string.Format("User {0} tried to update his credit card at {1} but customer_stripe_id was empty", vSession.User.Id, DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcCreditcardMessageControl, "Your credit card could not be created. Please try again later or contact with us", MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            //finally
            //{
            //    session.CloseConnection();
            //}
        }

        protected void BtnSearchGoPremium_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
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
                    LoadPackets(packId, false);
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPaymentModal();", true);
                }

                //string selectedPacketId = item["plan_id"].Text;

                //ElioPackets selectedPacket = Sql.GetPacketById(selectedCanceledPacketId, session);
                //if (selectedPacket != null)
                //{
                //    LoadPackets(selectedPacket.Id, false);
                //    DrpStripePlans.Enabled = false;
                //    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPaymentModal();", true);
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

        protected void PaymentStartupModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                int selectedPacket = (int)StripePlans.Elio_Startup_Plan;

                LoadPackets(selectedPacket, true);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPaymentModal();", true);
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

        protected void PaymentGrowthModal_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                int selectedPacket = (int)StripePlans.Elio_SelfService_Plan;

                LoadPackets(selectedPacket, true);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPaymentModal();", true);
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

        protected void BtnPayment_OnClick_old(object sender, EventArgs args)
        {
            string customerResponseId = string.Empty;

            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        divPaymentWarningMsg.Visible = false;
                        LblPaymentWarningMsgContent.Text = string.Empty;
                        divPaymentSuccessMsg.Visible = false;
                        LblPaymentSuccessMsgContent.Text = string.Empty;

                        //string customerStripeServiceSubscriptionEmail = string.Empty;
                        //string cardFingerPrint = string.Empty;
                        //string cardId = string.Empty;
                        //string errorMessage = string.Empty;
                        //string chargeId = string.Empty;
                        //DateTime? startDate = null;
                        //DateTime? currentPeriodStart = null;
                        //DateTime? currentPeriodEnd = null;
                        //string subscriptionStatus = string.Empty;
                        //DateTime? trialPeriodStart = null;
                        //DateTime? trialPeriodEnd = null;
                        //string orderMode = string.Empty;
                        //Xamarin.Payments.Stripe.StripeCard card = null;
                        //Xamarin.Payments.Stripe.StripeCoupon couponDiscount = null;                        
                        //ElioUsersPlanCouponsDiscount userDiscountCoupon = null;

                        int selectedPacketId = (int)Packets.SelfService;

                        if (!IsValidData())
                        {
                            divPaymentWarningMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            divPaymentWarningMsg.Visible = false;
                        }

                        bool isSuccess = false;

                        #region Selected Packet

                        //StripePlans plan = StripePlans.Elio_Startup_Plan;

                        //if (DrpStripePlans.SelectedValue == "1")
                        //    plan = StripePlans.Elio_Premium_Plan;
                        //if (DrpStripePlans.SelectedValue == ((int)StripePlans.Elio_Startup_Plan).ToString())
                        //    plan = StripePlans.Elio_Startup_Plan;
                        //else if (DrpStripePlans.SelectedValue == ((int)StripePlans.Elio_Growth_Plan).ToString())
                        //    plan = StripePlans.Elio_Growth_Plan;

                        #endregion

                        ElioUsers user = vSession.User;
                        isSuccess = StripeApi.PaymentMethodNew(out user, vSession.User.Id, selectedPacketId, TbxCardNumber.Text, DrpExpMonth1.SelectedItem.Text, TbxExpYear1.Text, TbxCVC.Text, TbxDiscount.Text, session);
                        vSession.User = user;

                        if (!isSuccess)
                        {
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = "Something went wrong! Please try again later or contact with us";

                            return;
                        }
                        else
                        {
                            divPaymentSuccessMsg.Visible = true;
                            LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                            BtnPayment.Enabled = false;

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing"), false);

                            try
                            {
                                //EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            ClearFields();
                        }

                        return;

                        #region Old Stripe Way

                        ////if (string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                        ////{
                        ////    #region Subscribe UnRegistered Customer

                        ////    if (CbxCouponDiscount.Checked)
                        ////    {
                        ////        #region Coupon Discount Case

                        ////        if (TbxDiscount.Text.Trim() != string.Empty)
                        ////        {
                        ////            ElioPlanCoupons planCoupon = Sql.GetPlanCoupon(TbxDiscount.Text.Trim().ToUpper(), session);

                        ////            if (planCoupon != null)
                        ////            {
                        ////                userDiscountCoupon = Sql.GetUserPlanCouponsDiscount(vSession.User.Id, planCoupon.Id, session);
                        ////                if (userDiscountCoupon != null)
                        ////                {
                        ////                    #region Valid Coupon Data Check

                        ////                    if (userDiscountCoupon.IsActiveDiscount == 0)
                        ////                    {

                        ////                        divPaymentWarningMsg.Visible = true;
                        ////                        LblPaymentWarningMsgContent.Text = "Coupon is not active. Please contact us for more details";
                        ////                        Logger.DetailedError(string.Format("User {0} tried to register at {1} but coupon ({2})is not active", vSession.User.Id.ToString(), DateTime.Now.ToString(), TbxDiscount.Text));
                        ////                        return;
                        ////                    }

                        ////                    if (userDiscountCoupon.ParentPackId != Convert.ToInt32(DrpStripePlans.SelectedValue))
                        ////                    {
                        ////                        divPaymentWarningMsg.Visible = true;
                        ////                        LblPaymentWarningMsgContent.Text = "Packet does not match with this Coupon. Please select right plan or contact us for more details";
                        ////                        Logger.DetailedError(string.Format("User {0} tried to register at {1} but selected plan ({2}) not match with coupon ({3})", vSession.User.Id.ToString(), DateTime.Now.ToString(), DrpStripePlans.Text, TbxDiscount.Text));
                        ////                        return;
                        ////                    }

                        ////                    #endregion
                        ////                }
                        ////                else
                        ////                {
                        ////                    #region Not Valid Coupon

                        ////                    divPaymentWarningMsg.Visible = true;
                        ////                    LblPaymentWarningMsgContent.Text = "Sorry, but coupon is wrong. Please try again later or contact us.";
                        ////                    Logger.DetailedError(string.Format("User {0} tried to register at {1} but plan coupon discount(ID: {2}) could not be found", vSession.User.Id.ToString(), DateTime.Now.ToString(), planCoupon.Id));
                        ////                    return;

                        ////                    #endregion
                        ////                }

                        ////                if (planCoupon.RedeemBy < DateTime.Now)
                        ////                {
                        ////                    #region Expired Coupon

                        ////                    divPaymentWarningMsg.Visible = true;
                        ////                    LblPaymentWarningMsgContent.Text = "Sorry, but coupon has expired. Please contact us for more details.";
                        ////                    Logger.DetailedError(string.Format("User {0} tried to register at {1} but coupon (ID: {2}) redeemBy has expired at {3}", vSession.User.Id.ToString(), DateTime.Now.ToString(), planCoupon.Id, planCoupon.RedeemBy.ToString()));
                        ////                    return;

                        ////                    #endregion
                        ////                }

                        ////                couponDiscount = StripeLib.CreateGetCoupon(Xamarin.Payments.Stripe.StripeCouponDuration.Repeating, planCoupon.MaxRedemptions, planCoupon.CouponId, planCoupon.PercentOff, planCoupon.MonthDuration, planCoupon.RedeemBy);
                        ////                if (couponDiscount != null)
                        ////                {
                        ////                    divPaymentSuccessMsg.Visible = true;
                        ////                    LblPaymentSuccessMsgContent.Text = "Coupon found successfully";

                        ////                    if (BtnPayment.Text == "Proceed")
                        ////                    {
                        ////                        #region Step 1: Proceed

                        ////                        decimal totalCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                        ////                        //LblTotalCostValue.Text = (planCoupon.AmountOff == null || planCoupon.AmountOff == 0) ? (totalCost - ((planCoupon.PercentOff * totalCost) / 100)).ToString() : Convert.ToDecimal(planCoupon.AmountOff) + " $";
                        ////                        LblTotalCostValue.Text = (couponDiscount.AmountOff == null || couponDiscount.AmountOff == 0) ? (totalCost - ((couponDiscount.PercentOff * totalCost) / 100)).ToString() + " $" : (totalCost - Convert.ToDecimal(couponDiscount.AmountOff) / 100).ToString() + " $";
                        ////                        //divInfo.Visible = false;
                        ////                        BtnPayment.Text = "Subscribe";

                        ////                        return;

                        ////                        #endregion
                        ////                    }
                        ////                    else if (BtnPayment.Text == "Subscribe")
                        ////                    {
                        ////                        #region Step 2 Subscribe

                        ////                        isError = StripeLib.SubscribeUnRegisteredCustomerWithCoupon(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, couponDiscount, planCoupon.PlanId, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                        ////                        if (!isError)
                        ////                        {
                        ////                            if (couponDiscount != null)
                        ////                            {
                        ////                                #region Users Plan Coupons

                        ////                                ElioUsersPlanCoupons userCoupon = new ElioUsersPlanCoupons();
                        ////                                userCoupon.UserId = vSession.User.Id;
                        ////                                userCoupon.PlanCouponsId = planCoupon.Id;
                        ////                                userCoupon.Sysdate = DateTime.Now;
                        ////                                userCoupon.LastUpdate = DateTime.Now;

                        ////                                DataLoader<ElioUsersPlanCoupons> loader = new DataLoader<ElioUsersPlanCoupons>(session);
                        ////                                loader.Insert(userCoupon);

                        ////                                #endregion

                        ////                                #region Selected Packet ID

                        ////                                selectedPacketId = userDiscountCoupon.ParentPackId;

                        ////                                #endregion
                        ////                            }
                        ////                        }
                        ////                        else
                        ////                        {
                        ////                            divPaymentWarningMsg.Visible = true;
                        ////                            LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later";
                        ////                            Logger.DetailedError(string.Format("User {0} tried to register but something went wrong in Stripe", vSession.User.Id.ToString()));
                        ////                            return;
                        ////                        }

                        ////                        #endregion
                        ////                    }
                        ////                }
                        ////                else
                        ////                {
                        ////                    #region Stripe Coupon Error

                        ////                    divPaymentWarningMsg.Visible = true;
                        ////                    LblPaymentWarningMsgContent.Text = "Something went wrong. Could not fild coupon. Please try again later";
                        ////                    Logger.DetailedError(string.Format("User {0} tried to register but coupon could not be found in Stripe", vSession.User.Id.ToString()));
                        ////                    //Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        ////                    return;

                        ////                    #endregion
                        ////                }
                        ////            }
                        ////            else
                        ////            {
                        ////                #region Coupon Not Valid

                        ////                divPaymentWarningMsg.Visible = true;
                        ////                LblPaymentWarningMsgContent.Text = "Coupon ID is wrong. Please try again";
                        ////                Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found", vSession.User.Id.ToString()));
                        ////                return;

                        ////                #endregion
                        ////            }
                        ////        }
                        ////        else
                        ////        {
                        ////            #region Coupon Empty

                        ////            divPaymentWarningMsg.Visible = true;
                        ////            LblPaymentWarningMsgContent.Text = "Please type your Coupon ID in order to get discount";
                        ////            return;

                        ////            #endregion
                        ////        }

                        ////        #endregion
                        ////    }
                        ////    else
                        ////    {
                        ////        #region Subscription of Customer to Stripe

                        ////        //isError = StripeLib.GetInTrial(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode);
                        ////        try
                        ////        {
                        ////            isError = StripeLib.SubscribeUnRegisteredCustomer(plan.ToString(), TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref cardId, ref customerResponseId, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                        ////        }
                        ////        catch (Exception ex)
                        ////        {
                        ////            if (!string.IsNullOrEmpty(customerResponseId))
                        ////            {
                        ////                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        ////                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                        ////                Xamarin.Payments.Stripe.StripeSubscription customerSubscription = payment.Unsubscribe(customerResponseId, false);
                        ////                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.DeleteCustomer(customerResponseId);

                        ////                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        ////                throw ex;
                        ////            }
                        ////        }

                        ////        #endregion
                        ////    }

                        ////    #endregion
                        ////}
                        ////else
                        ////{
                        ////    #region Subscribe Registered Customer

                        ////    try
                        ////    {
                        ////        isError = StripeLib.SubscribeRegisteredCustomer(plan.ToString(), TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.CustomerStripeId, ref errorMessage, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                        ////        customerResponseId = vSession.User.CustomerStripeId;
                        ////    }
                        ////    catch (Exception ex)
                        ////    {
                        ////        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        ////        throw ex;
                        ////    }

                        ////    #endregion
                        ////}

                        ////if (isError)
                        ////{
                        ////    divPaymentWarningMsg.Visible = true;
                        ////    LblPaymentWarningMsgContent.Text = errorMessage;
                        ////    return;
                        ////}

                        ////#region Service check to delete

                        //////if (CbxService.Checked)
                        //////{
                        //////    isError = StripeLib.SubscribeRegisteredCustomerToService(TbxEmail.Text, TbxCardNumber.Text, TbxCVC.Text, Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text), vSession.User.Id, vSession.User.CompanyName, ref errorMessage, ref cardFingerPrint, ref cardId, ref customerResponseId, ref customerStripeServiceSubscriptionEmail, ref chargeId, ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref subscriptionStatus, ref trialPeriodStart, ref trialPeriodEnd, ref orderMode, ref card);
                        //////}

                        //////if (isError)
                        //////{
                        //////    divPaymentWarningMsg.Visible = true;
                        //////    LblPaymentWarningMsgContent.Text = errorMessage;
                        //////    return;
                        //////}

                        ////#endregion

                        ////if (startDate != null && ((trialPeriodStart != null && trialPeriodEnd != null) || (currentPeriodStart != null && currentPeriodEnd != null)))
                        ////{
                        ////    #region Make User Premium

                        ////    try
                        ////    {
                        ////        session.BeginTransaction();

                        ////        selectedPacketId = (userDiscountCoupon != null) ? userDiscountCoupon.ParentPackId : Convert.ToInt32(DrpStripePlans.SelectedValue);   //to do for coupons
                        ////        vSession.User = PaymentLib.MakeUserPremium(vSession.User, selectedPacketId, (couponDiscount != null) ? couponDiscount.PercentOff : null, customerResponseId, cardId, TbxEmail.Text, startDate, trialPeriodStart, trialPeriodEnd, currentPeriodStart, currentPeriodEnd, orderMode, card, session);

                        ////        //if (CbxService.Checked)
                        ////        //{
                        ////        //    PaymentLib.AttachUserToService(vSession.User, customerResponseId, customerStripeServiceSubscriptionEmail, startDate, currentPeriodStart, currentPeriodEnd, orderMode, card, session);
                        ////        //}

                        ////        session.CommitTransaction();
                        ////    }
                        ////    catch (Exception ex)
                        ////    {
                        ////        session.RollBackTransaction();
                        ////        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        ////    }

                        ////    vSession.UserHasExpiredOrder = false;

                        ////    divPaymentSuccessMsg.Visible = true;
                        ////    LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                        ////    BtnPayment.Enabled = false;

                        ////    Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing"), false);

                        ////    try
                        ////    {
                        ////        //EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
                        ////    }
                        ////    catch (Exception ex)
                        ////    {
                        ////        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        ////    }

                        ////    ClearFields();

                        ////    #endregion
                        ////}

                        #endregion

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User

                        BtnPayment.Enabled = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                #region Error

                divPaymentWarningMsg.Visible = true;
                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                if (!string.IsNullOrEmpty(customerResponseId))
                {
                    Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        if (!string.IsNullOrEmpty(customerResponseId))
                        {
                            Xamarin.Payments.Stripe.StripeSubscription customerSubscription = payment.Unsubscribe(customerResponseId, false);
                            Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.DeleteCustomer(customerResponseId);
                        }
                    }
                    catch (Xamarin.Payments.Stripe.StripeException exDeleteCustomer)
                    {
                        //errorMessage = exDeleteCustomer.Message;
                        Logger.DetailedError(Request.Url.ToString(), exDeleteCustomer.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                #endregion
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnPayment_OnClick(object sender, EventArgs args)
        {
            string customerResponseId = string.Empty;

            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        #region Freemium User

                        divPaymentWarningMsg.Visible = false;
                        LblPaymentWarningMsgContent.Text = string.Empty;
                        divPaymentSuccessMsg.Visible = false;
                        LblPaymentSuccessMsgContent.Text = string.Empty;

                        int selectedPacketId = (int)Packets.SelfService;

                        if (!IsValidData())
                        {
                            divPaymentWarningMsg.Visible = true;
                            return;
                        }
                        else
                        {
                            divPaymentWarningMsg.Visible = false;
                        }

                        bool isSuccess = false;

                        if (CbxCouponDiscount.Checked)
                        {
                            if (TbxDiscount.Text.Trim() != string.Empty)
                            {
                                ElioPacketsStripeCoupons planCoupon = Sql.GetPacketStripeCoupon(TbxDiscount.Text.Trim().ToUpper(), session);
                                if (planCoupon != null)
                                {
                                    ElioPackets packet = Sql.GetPacketById(selectedPacketId, session);
                                    if (packet != null)
                                    {
                                        if (packet.stripePlanId == planCoupon.StripePlanId)
                                        {
                                            bool hasCouponError = true;
                                            StripeCoupon stripeCoupon = Lib.Services.StripeAPI.StripeService.GetCoupon(planCoupon.CouponId);
                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                            {
                                                hasCouponError = false;
                                            }
                                            else
                                            {
                                                stripeCoupon = Lib.Services.StripeAPI.StripeService.CreateCoupon(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, planCoupon.PercentOff, planCoupon.RedeemBy);
                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                {
                                                    hasCouponError = false;
                                                }
                                                else
                                                {
                                                    //cooupon could not be created
                                                    hasCouponError = true;
                                                    divPaymentWarningMsg.Visible = true;
                                                    LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact with us";
                                                    Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon ({1}) could not be created in Stripe", vSession.User.Id.ToString(), TbxDiscount.Text.Trim().ToUpper()));
                                                    return;
                                                }
                                            }

                                            if (!hasCouponError)
                                            {
                                                if (stripeCoupon.TimesRedeemed < planCoupon.MaxRedemptions)
                                                {
                                                    if (stripeCoupon.RedeemBy == null || (stripeCoupon.RedeemBy != null && Convert.ToDateTime(stripeCoupon.RedeemBy) >= DateTime.Now))
                                                    {
                                                        divPaymentSuccessMsg.Visible = true;
                                                        LblPaymentSuccessMsgContent.Text = "Coupon found successfully";

                                                        if (BtnPayment.Text == "Proceed")
                                                        {
                                                            #region Step 1: Proceed

                                                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                                                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                                                            {
                                                                decimal totalCost = Convert.ToDecimal(plan.Amount) / 100;       //Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                                                                LblTotalCostValue.Text = (stripeCoupon.AmountOff == null || stripeCoupon.AmountOff == 0) ? (totalCost - ((stripeCoupon.PercentOff * totalCost) / 100)).ToString() + " $" : (totalCost - Convert.ToDecimal(stripeCoupon.AmountOff) / 100).ToString() + " $";
                                                                //divInfo.Visible = false;
                                                                BtnPayment.Text = "Subscribe";

                                                                return;
                                                            }
                                                            else
                                                            {
                                                                divPaymentWarningMsg.Visible = true;
                                                                LblPaymentWarningMsgContent.Text = "Something went wrong with your Plan activation. Please try again or contact with us";
                                                                Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his plan ({1}) could not be found in Stripe for packet amount", vSession.User.Id.ToString(), packet.stripePlanId));

                                                                return;
                                                            }

                                                            #endregion
                                                        }
                                                        else if (BtnPayment.Text == "Subscribe")
                                                        {
                                                            #region Step 2 Subscribe

                                                            //continue to subscribe in Stripe

                                                            #endregion
                                                        }
                                                    }
                                                    else
                                                    {
                                                        divPaymentWarningMsg.Visible = true;
                                                        LblPaymentWarningMsgContent.Text = "Your Coupon has expired. Please contact with us";
                                                        return;
                                                    }
                                                }
                                                else
                                                {
                                                    divPaymentWarningMsg.Visible = true;
                                                    LblPaymentWarningMsgContent.Text = "Your Coupon can not be used any more. Please contact with us";
                                                    return;
                                                }
                                            }
                                            else
                                            {
                                                divPaymentWarningMsg.Visible = true;
                                                LblPaymentWarningMsgContent.Text = "Something went wrong with your Coupon. Please try again or contact with us";
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            divPaymentWarningMsg.Visible = true;
                                            LblPaymentWarningMsgContent.Text = "Coupon is not valid for selected Plan. Please try again";
                                            return;
                                        }
                                    }
                                    else
                                    {

                                        return;
                                    }
                                }
                                else
                                {
                                    divPaymentWarningMsg.Visible = true;
                                    LblPaymentWarningMsgContent.Text = "Coupon is not valid. Please try again";
                                    Logger.DetailedError(string.Format("User {0} tried to register in Stripe but his coupon could not be found in Elio DB", vSession.User.Id.ToString()));
                                    return;
                                }
                            }
                        }

                        ElioUsers user = vSession.User;
                        isSuccess = StripeApi.PaymentMethodNew(out user, vSession.User.Id, selectedPacketId, TbxCardNumber.Text, DrpExpMonth1.SelectedItem.Text, TbxExpYear1.Text, TbxCVC.Text, TbxDiscount.Text, session);
                        vSession.User = user;

                        if (!isSuccess)
                        {
                            divPaymentWarningMsg.Visible = true;
                            LblPaymentWarningMsgContent.Text = "Something went wrong! Please try again later or contact with us";

                            return;
                        }
                        else
                        {
                            divPaymentSuccessMsg.Visible = true;
                            LblPaymentSuccessMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "5")).Text;
                            BtnPayment.Enabled = false;

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "billing-99"), false);

                            try
                            {
                                //EmailSenderLib.SendStripeTrialActivationEmail(TbxEmail.Text, vSession.Lang, session);
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            ClearFields();
                        }

                        return;

                        #endregion
                    }
                    else
                    {
                        #region Not Freemium User

                        BtnPayment.Enabled = false;
                        divPaymentWarningMsg.Visible = true;
                        LblPaymentWarningMsgContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "paymentresults", "message", "6")).Text;

                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                #region Error

                divPaymentWarningMsg.Visible = true;
                LblPaymentWarningMsgContent.Text = "Something went wrong. Please try again later.";
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                if (!string.IsNullOrEmpty(customerResponseId))
                {
                    Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                    try
                    {
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                        if (!string.IsNullOrEmpty(customerResponseId))
                        {
                            Xamarin.Payments.Stripe.StripeSubscription customerSubscription = payment.Unsubscribe(customerResponseId, false);
                            Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.DeleteCustomer(customerResponseId);
                        }
                    }
                    catch (Xamarin.Payments.Stripe.StripeException exDeleteCustomer)
                    {
                        //errorMessage = exDeleteCustomer.Message;
                        Logger.DetailedError(Request.Url.ToString(), exDeleteCustomer.Message.ToString(), ex.StackTrace.ToString());
                    }
                }

                #endregion
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelPayment_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ClearFields();

                    ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "ClosePaymentModal();", true);
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

        protected void CbxCouponDiscount_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                divDiscount.Visible = (CbxCouponDiscount.Checked) ? true : false;
                //divInfo.Visible = !divDiscount.Visible;
                TbxDiscount.Text = "";

                if (!CbxCouponDiscount.Checked)
                {
                    LblPaymentWarningMsgContent.Text = string.Empty;
                    divPaymentWarningMsg.Visible = false;

                    LblPaymentSuccessMsgContent.Text = string.Empty;
                    divPaymentSuccessMsg.Visible = false;

                    BtnPayment.Text = "Subscribe";

                    try
                    {
                        session.OpenConnection();

                        ElioPackets packet = Sql.GetPacketById((int)Packets.SelfService, session);
                        if (packet != null)
                        {
                            Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                            if (plan != null && !string.IsNullOrEmpty(plan.Id))
                            {
                                LblTotalCostValue.Text = (Convert.ToDecimal(plan.Amount) / 100).ToString() + " $";      //Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
                            }
                            else
                                LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat((int)Packets.SelfService, session).ToString() + " $";
                        }
                        else
                            LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat((int)Packets.SelfService, session).ToString() + " $";
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
                    BtnPayment.Text = "Proceed";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxCouponDiscount_OnCheckedChanged_old(object sender, EventArgs args)
        {
            try
            {
                divDiscount.Visible = (CbxCouponDiscount.Checked) ? true : false;
                //divInfo.Visible = !divDiscount.Visible;
                TbxDiscount.Text = "";

                if (!CbxCouponDiscount.Checked)
                {
                    LblPaymentWarningMsgContent.Text = string.Empty;
                    divPaymentWarningMsg.Visible = false;

                    LblPaymentSuccessMsgContent.Text = string.Empty;
                    divPaymentSuccessMsg.Visible = false;

                    BtnPayment.Text = "Subscribe";

                    try
                    {
                        session.OpenConnection();

                        LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat((int)Packets.SelfService, session).ToString() + " $";
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
                    BtnPayment.Text = "Proceed";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxService_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //if (CbxService.Checked)
                //{
                //LblPaymentWarningMsgContent.Text = string.Empty;
                //divPaymentWarningMsg.Visible = false;

                //LblPaymentSuccessMsgContent.Text = string.Empty;
                //divPaymentSuccessMsg.Visible = false;

                //decimal planCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session);
                //decimal serviceCost = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(StripePlans.Elio_Premium_Service_Plan), session);
                //decimal totalCost = planCost + serviceCost;

                //LblTotalCostValue.Text = totalCost.ToString() + " $";
                //}
                //else
                //{
                //    LblTotalCostValue.Text = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(DrpStripePlans.SelectedValue), session).ToString() + " $";
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

        #endregion

        #region Buttons

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