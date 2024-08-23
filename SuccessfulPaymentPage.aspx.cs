using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using Stripe.Checkout;
using Stripe;
using Org.BouncyCastle.Bcpg;
using Telerik.Web.UI.Skins;
using ServiceStack;

namespace WdS.ElioPlus
{
    public partial class SuccessfulPaymentPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        //if (vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        //{
                        ElioUsers user = Sql.GetUserById(vSession.User.Id, session);

                        StripeUsersCheckoutSessions userSession = Sql.GetUserStripeCheckoutSession(user.Id, 0, session);
                        if (userSession != null)
                        {
                            Session createdSession = Lib.Services.StripeAPI.StripeAPIService.GetCheckoutSessionBySessIdApi(userSession.CheckoutSessionId);
                            if (createdSession != null)
                            {
                                Customer customer = null;
                                if (string.IsNullOrEmpty(createdSession.CustomerId))
                                {
                                    customer = Lib.Services.StripeAPI.StripeService.CreateCustomerNew(user.Email, user.CompanyName, user.Phone);
                                    if (customer != null && !string.IsNullOrEmpty(customer.Id))
                                    {
                                        user.CustomerStripeId = customer.Id;
                                        //user = GlobalDBMethods.UpDateUser(user, session);
                                    }
                                }
                                else
                                {
                                    //customer = Lib.Services.StripeAPI.StripeService.GetCustomerNew(vSession.User.CustomerStripeId);
                                    user.CustomerStripeId = createdSession.CustomerId;
                                }

                                user.BillingType = Sql.GetPremiumBillingTypeIdByStripePlanId(userSession.StripePlanId, session);

                                if (user.HasBillingDetails == 0)
                                {
                                    #region New Billing Account Data

                                    ElioUsersBillingAccount account = new ElioUsersBillingAccount();

                                    account.UserId = user.Id;
                                    account.BillingEmail = user.Email;
                                    account.HasVat = 1;
                                    account.Sysdate = DateTime.Now;
                                    account.LastUpdated = DateTime.Now;
                                    account.IsActive = 1;

                                    DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                                    loader0.Insert(account);

                                    user.HasBillingDetails = 1;

                                    //vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                                    #endregion
                                }
                                else
                                {
                                    #region Update Billing Account Data

                                    //ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(user.Id, session);

                                    //if (account != null)
                                    //{
                                    //    account.BillingEmail = user.Email;
                                    //    account.LastUpdated = DateTime.Now;

                                    //    DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                                    //    loader0.Update(account);
                                    //}

                                    #endregion
                                }

                                ElioPackets packet = Sql.GetPacketByStripePlanId(userSession.StripePlanId, session);
                                if (packet != null)
                                {
                                    if (packet.Id != (int)Packets.PremiumService && packet.Id != (int)Packets.ServiceFollowUp && packet.Id != (int)Packets.SelfService && packet.Id != (int)Packets.AccountManagerService && packet.Id != (int)Packets.PremiumService299)
                                    {
                                        vSession.User = GlobalDBMethods.UpDateUser(user, session);
                                    }

                                    Subscription subscription = Lib.Services.StripeAPI.StripeService.GetSubscriptionNew(createdSession.SubscriptionId);
                                    if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                    {
                                        #region Insert New Subscription

                                        ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                        sub.UserId = vSession.User.Id;
                                        sub.CustomerId = subscription.CustomerId;
                                        sub.SubscriptionId = subscription.Id;
                                        sub.CouponId = (subscription.Discount != null && !string.IsNullOrEmpty(subscription.Discount.Coupon.Id)) ? subscription.Discount.Coupon.Id : "";
                                        sub.PlanId = userSession.StripePlanId;
                                        sub.PlanNickname = packet.PackDescription;
                                        sub.CreatedAt = Convert.ToDateTime(subscription.Created);
                                        sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                        sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                        sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                        sub.HasDiscount = createdSession.AmountSubtotal != createdSession.AmountTotal ? 1 : 0;
                                        sub.Status = subscription.Status.ToString();
                                        sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                        sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                        sub.Amount = (int)createdSession.AmountTotal;

                                        DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                        loader.Insert(sub);

                                        #endregion

                                        #region Insert New Invoice

                                        Stripe.Invoice invoice = Lib.Services.StripeAPI.StripeService.GetInvoiceByInvoiceIDNew(createdSession.InvoiceId);
                                        if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                        {
                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                            subInvoice.UserId = vSession.User.Id;
                                            subInvoice.UserSubscriptionId = sub.Id;
                                            subInvoice.CustomerId = subscription.CustomerId;
                                            subInvoice.InvoiceId = invoice.Id;
                                            subInvoice.ChargeId = "";
                                            subInvoice.SubscriptionId = subscription.Id;
                                            subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                            subInvoice.Currency = invoice.Currency;
                                            subInvoice.Date = Convert.ToDateTime(invoice.Created);
                                            subInvoice.Description = invoice.Description;
                                            subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                            subInvoice.InvoicePdf = invoice.InvoicePdf;
                                            subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                            subInvoice.Number = invoice.Number;
                                            subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                            subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
                                            subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
                                            subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                            subInvoice.HasDiscount = sub.HasDiscount;
                                            subInvoice.TotalAmount = (int)invoice.Total;
                                            subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                            if (invoice.Discount != null)
                                                if (invoice.Discount.Coupon != null)
                                                {
                                                    Coupon stripeSubCoupon = invoice.Discount.Coupon;
                                                    if (stripeSubCoupon != null && !string.IsNullOrEmpty(stripeSubCoupon.Id))
                                                        subInvoice.CouponId = stripeSubCoupon.Id;
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

                                        #endregion

                                        #region Fix Packet Features Items for Premium User

                                        if (packet.Id != (int)Packets.PremiumService && packet.Id != (int)Packets.ServiceFollowUp && packet.Id != (int)Packets.SelfService && packet.Id != (int)Packets.AccountManagerService && packet.Id != (int)Packets.PremiumService299)
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

                                                #region Insert / Update Packet Status Features

                                                ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

                                                DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

                                                if (packetFeatures == null)
                                                {
                                                    packetFeatures = new ElioUserPacketStatus();

                                                    packetFeatures.UserId = vSession.User.Id;
                                                    packetFeatures.PackId = items[0].Id;
                                                    packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
                                                    packetFeatures.AvailableLeadsCount = totalLeads;
                                                    packetFeatures.AvailableMessagesCount = totalMessages;
                                                    packetFeatures.AvailableConnectionsCount = totalConnections;
                                                    packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                                                    packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                                                    packetFeatures.Sysdate = DateTime.Now;
                                                    packetFeatures.LastUpdate = DateTime.Now;
                                                    packetFeatures.StartingDate = Convert.ToDateTime(subscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                    packetFeatures.ExpirationDate = Convert.ToDateTime(subscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

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
                                                    packetFeatures.StartingDate = Convert.ToDateTime(subscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                                                    packetFeatures.ExpirationDate = Convert.ToDateTime(subscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                                                    loader4.Update(packetFeatures);
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                                            }
                                        }

                                        #endregion
                                    }
                                }

                                bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                                if (!setEpiredSess)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("After success payment User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("After success payment User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                                }

                                UpdateStrings();
                                SetLinks();
                                GlobalMethods.ClearCriteriaSession(vSession, false);
                            }
                        }
                        //}
                        //else
                        //    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"));
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

        #region Methods

        private void UpdateStrings()
        {
            PgTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "6")).Text;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divVendorsArea.Visible = true;
                divChannelPartnersArea.Visible = false;
                aContactUs.Visible = false;

                LblSuccessRegistrationContent.Text = "You can access your account to manage potential partnerships and grow your partner network";
                LblVendorsTitle.Text = "Launch you Partner Portal";
                LblContent.Text = "Launch your partner portal to manage your channel partners, automate the deal registration process and increase your partner sales.";
                LblContent2.Text = "<b>What’s included?</b> Partner directory, onboarding, deal registration, lead distribution, CRM integrations, analytics, partner locator and many more features.";
            }
            else
            {
                divVendorsArea.Visible = false;
                divChannelPartnersArea.Visible = true;
                aContactUs.Visible = true;
                aContactUs.HRef = ControlLoader.ContactUs;

                LblSuccessRegistrationContent.Text = "You can access your account to manage your partnerships and get new leads";
                LblVendorsTitle.Text = "Get access to leads and requests for proposals";
                LblContent.Text = "Discover new leads and get access to Requests for Proposals based on your location and product expertise. To learn more please ";
                LblContent2.Text = "";
            }

            LblSuccessRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "1")).Text;
            LblFindNewPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "2")).Text;
            LblRecruitNewPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "7")).Text + " + " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "3")).Text;
            //LblUsePrm.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "3")).Text;
            LblCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "8")).Text;
            LblCopyright2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "9")).Text;
            LblCopyrightElioplus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "10")).Text;

            //BtnDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "button", "1")).Text;
        }

        private void SetLinks()
        {
            aBookDemo.HRef = "https://calendly.com/elioplus";
            aCopyRight.HRef = ControlLoader.Default();
        }

        #endregion

        #region Buttons

        protected void BtnDashboard_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect((vSession.User != null) ? ControlLoader.Dashboard(vSession.User, "home") : ControlLoader.Default(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                //Response.Redirect(ControlLoader.Default, false);
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}