using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ServiceStack.Stripe;
using ServiceStack.Stripe.Types;
using Stripe;
using Stripe.Checkout;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.StripeAPI
{
    public class StripeApi
    {
        public class CardToken
        {
            public string id { get; set; }
        }

        public static bool AddNewCardNew(ElioUsers user, string cardNumber, string expMonth, string expYear, string cvc, out string errorMessage)
        {
            errorMessage = "";

            if (!string.IsNullOrEmpty(user.CustomerStripeId))
            {
                Customer customer = Lib.Services.StripeAPI.StripeService.GetCustomerNew(user.CustomerStripeId);
                if (customer != null && !string.IsNullOrEmpty(customer.Id))
                {
                    Stripe.Card defaultCard = Lib.Services.StripeAPI.StripeService.DeleteCreditCardNew(customer.Id, customer.DefaultSourceId);
                    if (defaultCard != null && (bool)defaultCard.Deleted)
                    {
                        Token cardToken = Lib.Services.StripeAPI.StripeService.CreateCardTokenNew(cardNumber, expMonth, expYear, cvc, customer.Description);

                        if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                        {
                            Card card = Lib.Services.StripeAPI.StripeService.CreateCreditCardNew(customer.Id, cardToken.Id);
                            if (card != null && !string.IsNullOrEmpty(card.Id))
                            {
                                if (card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Pass.ToString().ToLower() || card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown.ToString().ToLower() || card.CvcCheck.ToLower() == "unavailable")
                                {
                                    errorMessage = "";
                                    return true;
                                }
                                else
                                {
                                    errorMessage = "Sorry, you can not add/change your credit card because CVC check failed. Please, try again later or contact us";
                                    Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but CVC check failed", user.Id), string.Format("CustomerID: {0}", user.CustomerStripeId));
                                    return false;
                                }
                            }
                            else
                            {
                                errorMessage = "Sorry, you can not add/change your credit card. Please, try again later or contact us";
                                Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but new card could not be created on there", user.Id), string.Format("CustomerID: {0}", user.CustomerStripeId));
                                return false;
                            }
                        }
                        else
                        {
                            errorMessage = "Sorry, you can not add/change your credit card. Please, try again later or contact us";
                            Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but card Token could not be created on there", user.Id), string.Format("CustomerID: {0}", user.CustomerStripeId));
                            return false;
                        }
                    }
                    else
                    {
                        errorMessage = "Sorry, you can not add/change your credit card. Please, try again later or contact us";
                        Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but could not Delete default card on there", user.Id), string.Format("CustomerID: {0}", user.CustomerStripeId));
                        return false;
                    }
                }
                else
                {
                    errorMessage = "Sorry, you can not add/change your credit card. Please, try again later or contact us";
                    Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but could not Get Customer from there", user.Id), string.Format("CustomerID: {0}", user.CustomerStripeId));
                    return false;
                }
            }
            else
            {
                errorMessage = "Sorry, you can not add/change your credit card. Please, try again later or contact us";
                Logger.DetailedError("DashboardBillingPage.aspx --> BtnAddNewCard_OnClick --> WdS.ElioPlus.Lib.Services.StripeAPI", string.Format("User {0} tried to Add/Change New Card to Stripe but could not Get Customer from there", user.Id), "CustomerID is NULL");
                return false;
            }
        }

        public static bool PaymentMethodNew(out ElioUsers user, int userId, int packId, string number, string expMonth, string expYear, string cvc, string coupon, DBSession session)
        {
            bool hasDiscount = !string.IsNullOrEmpty(coupon);

            user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                Customer customer = null;
                if (string.IsNullOrEmpty(user.CustomerStripeId))
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
                    customer = Lib.Services.StripeAPI.StripeService.GetCustomerNew(user.CustomerStripeId);
                }

                if (customer != null && !string.IsNullOrEmpty(customer.Id))
                {
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

                        user = GlobalDBMethods.UpDateUser(user, session);

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

                    ElioPackets packet = Sql.GetPacketById(packId, session);
                    if (packet != null)
                    {
                        #region Update User With Billing Details Status

                        //if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                        //{
                        //    user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                        //    user = GlobalDBMethods.UpDateUser(user, session);
                        //}

                        #endregion

                        Plan plan = Lib.Services.StripeAPI.StripeAPIService.GetPlanNewApi(packet.stripePlanId);
                        if (plan != null && !string.IsNullOrEmpty(plan.Id))
                        {
                            StripeList<Subscription> subs = customer.Subscriptions;
                            bool hasSubscriptions = subs != null && subs.Data.Count() > 0;

                            Token cardToken = null;
                            Stripe.Card card = null;
                            string cardID = "";

                            if (!hasSubscriptions)
                            {
                                if (string.IsNullOrEmpty(customer.DefaultSourceId) && cardID == "")
                                {
                                    cardToken = Lib.Services.StripeAPI.StripeService.CreateCardTokenNew(number, expMonth, expYear, cvc, customer.Description);

                                    if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                    {
                                        card = Lib.Services.StripeAPI.StripeService.CreateCreditCardNew(customer.Id, cardToken.Id);
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            if (card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Pass.ToString().ToLower() || card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown.ToString().ToLower() || card.CvcCheck.ToLower() == "unavailable")
                                            {
                                                cardID = card.Id;

                                                Coupon stripeCoupon = null;

                                                if (hasDiscount)
                                                {
                                                    ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                    if (planCoupon != null)
                                                    {
                                                        if (plan.Id == planCoupon.StripePlanId)
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.GetCouponNewApi(planCoupon.CouponId);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.CreateCouponNewApi(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, (long)planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, (decimal)planCoupon.PercentOff, planCoupon.RedeemBy);
                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                {
                                                                    coupon = stripeCoupon.Id;
                                                                }
                                                                else
                                                                {
                                                                    //cooupon could not be created
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //wrong packet to plan
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //wrong selected packet for coupon to have discount
                                                        return false;
                                                    }
                                                }

                                                Subscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlanNew(customer.Id, plan.Id, coupon);
                                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id) && subscription.Status.ToLower() == "active")
                                                {
                                                    ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                    sub.UserId = user.Id;
                                                    sub.CustomerId = subscription.CustomerId;
                                                    sub.SubscriptionId = subscription.Id;
                                                    sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                    sub.PlanId = plan.Id;
                                                    sub.PlanNickname = plan.Nickname;
                                                    sub.CreatedAt = System.Convert.ToDateTime(subscription.Created);
                                                    sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                    sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                    sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                    sub.HasDiscount = hasDiscount ? 1 : 0;
                                                    sub.Status = subscription.Status.ToString();
                                                    sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                    sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                    sub.Amount = (int)plan.Amount;

                                                    DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                    loader.Insert(sub);

                                                    StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(subscription.CustomerId, subscription.Id);
                                                    if (invoices != null && invoices.Data.Count() > 0)
                                                    {
                                                        foreach (Stripe.Invoice invoice in invoices)
                                                        {
                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                            subInvoice.UserId = user.Id;
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
                                                                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                            }
                                                        }

                                                        #endregion

                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                        {
                                                            user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion

                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        //invoice could not be created
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //subscription failure

                                                    customer = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerApi(customer.Id);
                                                    if (customer != null && (bool)customer.Deleted)
                                                    {
                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType > 1 && packet.Id != (int)Packets.PremiumService)
                                                        {
                                                            user.BillingType = 1;
                                                            user.CustomerStripeId = "";

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion
                                                    }

                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //card cvc check failed
                                                string cardCvcCheck = card.CvcCheck.ToString();
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //card could not be created in stripe
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //cardToken could not be created in stripe
                                        return false;
                                    }
                                }
                                else
                                {
                                    cardID = customer.DefaultSourceId;

                                    Coupon stripeCoupon = null;

                                    cardToken = Lib.Services.StripeAPI.StripeService.CreateCardTokenNew(number, expMonth, expYear, cvc, customer.Description);

                                    if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                    {
                                        card = Lib.Services.StripeAPI.StripeService.CreateCreditCardNew(customer.Id, cardToken.Id);
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            if (card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Pass.ToString().ToLower() || card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown.ToString().ToLower() || card.CvcCheck.ToLower() == "unavailable")
                                            {
                                                cardID = card.Id;

                                                if (hasDiscount)
                                                {
                                                    ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                    if (planCoupon != null)
                                                    {
                                                        if (plan.Id == planCoupon.StripePlanId)
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.GetCouponNewApi(planCoupon.CouponId);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.CreateCouponNewApi(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, (long)planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, (decimal)planCoupon.PercentOff, planCoupon.RedeemBy);
                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                {
                                                                    coupon = stripeCoupon.Id;
                                                                }
                                                                else
                                                                {
                                                                    //cooupon could not be created
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //wrong packet to plan
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //wrong selected packet for coupon to have discount
                                                        return false;
                                                    }
                                                }

                                                Subscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlanNew(customer.Id, plan.Id, coupon);
                                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id) && subscription.Status.ToLower() == "active")
                                                {
                                                    ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                    sub.UserId = user.Id;
                                                    sub.CustomerId = subscription.CustomerId;
                                                    sub.SubscriptionId = subscription.Id;
                                                    sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                    sub.PlanId = plan.Id;
                                                    sub.PlanNickname = plan.Nickname;
                                                    sub.CreatedAt = Convert.ToDateTime(subscription.Created);
                                                    sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                    sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                    sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                    sub.HasDiscount = hasDiscount ? 1 : 0;
                                                    sub.Status = subscription.Status.ToString();
                                                    sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                    sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                    sub.Amount = (int)plan.Amount;

                                                    DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                    loader.Insert(sub);

                                                    StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(subscription.CustomerId, subscription.Id);
                                                    if (invoices != null && invoices.Data.Count() > 0)
                                                    {
                                                        foreach (Stripe.Invoice invoice in invoices)
                                                        {
                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                            subInvoice.UserId = user.Id;
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

                                                        #region Fix Packet Features Items for Premium User

                                                        if (packet.Id != (int)Packets.PremiumService)
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
                                                                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                            }
                                                        }

                                                        #endregion

                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                        {
                                                            user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion

                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        //invoice could not be created
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //subscription failure

                                                    customer = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerApi(customer.Id);
                                                    if (customer != null && (bool)customer.Deleted)
                                                    {
                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType > 1 || !string.IsNullOrEmpty(user.CustomerStripeId))
                                                        {
                                                            user.BillingType = 1;
                                                            user.CustomerStripeId = "";

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion
                                                    }

                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //cvcCheck not passed
                                                string CvcCheck = card.CvcCheck.ToString();
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //card could not be created
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //card token could not be created
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                StripeList<IPaymentSource> cusSources = customer.Sources;
                                bool hasSources = cusSources != null && cusSources.Data.Count() > 0;

                                if (hasSources)
                                {
                                    foreach (Card item in cusSources)
                                    {
                                        Card source = new Card();
                                        source = item;

                                        card = source;
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            cardID = card.Id;
                                            break;
                                        }
                                    }
                                }

                                cardToken = Lib.Services.StripeAPI.StripeService.CreateCardTokenNew(number, expMonth, expYear, cvc, customer.Description);

                                if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                {
                                    card = Lib.Services.StripeAPI.StripeService.CreateCreditCardNew(customer.Id, cardToken.Id);
                                    if (card != null && !string.IsNullOrEmpty(card.Id))
                                    {
                                        if (card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Pass.ToString().ToLower() || card.CvcCheck.ToLower() == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown.ToString().ToLower() || card.CvcCheck.ToLower() == "unavailable")
                                        {
                                            cardID = card.Id;

                                            Coupon stripeCoupon = null;

                                            if (hasDiscount)
                                            {
                                                ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                if (planCoupon != null)
                                                {
                                                    if (plan.Id == planCoupon.StripePlanId)
                                                    {
                                                        stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.GetCouponNewApi(planCoupon.CouponId);
                                                        if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                        {
                                                            coupon = stripeCoupon.Id;
                                                        }
                                                        else
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeAPIService.CreateCouponNewApi(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, (long)planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, (decimal)planCoupon.PercentOff, planCoupon.RedeemBy);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                //cooupon could not be created
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //wrong selected packet for coupon to have discount
                                                    return false;
                                                }
                                            }

                                            Subscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlanNew(customer.Id, plan.Id, coupon);
                                            if (subscription != null && !string.IsNullOrEmpty(subscription.Id) && subscription.Status.ToLower() == "active")
                                            {
                                                ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                sub.UserId = user.Id;
                                                sub.CustomerId = subscription.CustomerId;
                                                sub.SubscriptionId = subscription.Id;
                                                sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                sub.PlanId = plan.Id;
                                                sub.PlanNickname = plan.Nickname;
                                                sub.CreatedAt = Convert.ToDateTime(subscription.Created);
                                                sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                sub.HasDiscount = hasDiscount ? 1 : 0;
                                                sub.Status = subscription.Status.ToString();
                                                sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                sub.Amount = (int)plan.Amount;

                                                DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                loader.Insert(sub);

                                                StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(subscription.CustomerId, subscription.Id);
                                                if (invoices != null && invoices.Data.Count() > 0)
                                                {
                                                    foreach (Stripe.Invoice invoice in invoices)
                                                    {
                                                        ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                        subInvoice.UserId = user.Id;
                                                        subInvoice.UserSubscriptionId = sub.Id;
                                                        subInvoice.CustomerId = invoice.CustomerId;
                                                        subInvoice.InvoiceId = invoice.Id;
                                                        subInvoice.ChargeId = "";
                                                        subInvoice.SubscriptionId = sub.Id.ToString();
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
                                                            Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                        }
                                                    }

                                                    #endregion

                                                    #region Update User With Billing Details Status

                                                    if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                    {
                                                        user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                        user = GlobalDBMethods.UpDateUser(user, session);
                                                    }

                                                    #endregion

                                                    return true;
                                                }
                                                else
                                                {
                                                    //invoice could not be created
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //subscription failure

                                                customer = Lib.Services.StripeAPI.StripeAPIService.DeleteCustomerApi(customer.Id);
                                                if (customer != null && (bool)customer.Deleted)
                                                {
                                                    #region Update User With Billing Details Status

                                                    if (user.BillingType > 1 && packet.Id != (int)Packets.PremiumService)
                                                    {
                                                        user.BillingType = 1;
                                                        user.CustomerStripeId = "";

                                                        user = GlobalDBMethods.UpDateUser(user, session);
                                                    }

                                                    #endregion
                                                }

                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //cvc check failed
                                            string cvcCheck = card.CvcCheck.ToString();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //card could not be found in stripe
                                        return false;
                                    }
                                }
                                else
                                {
                                    //card token could not be created
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //Stripe plan not found
                            return false;
                        }
                    }
                    else
                    {
                        //Elio packet not found
                        return false;
                    }
                }
                else
                {
                    //user not exists in stripe
                    return false;
                }
            }
            else
            {
                //user not logged in
                return false;
            }
        }

        public static bool PaymentMethodOld(out ElioUsers user, int userId, int packId, string number, int expMonth, int expYear, string cvc, string coupon, DBSession session)
        {
            bool hasDiscount = !string.IsNullOrEmpty(coupon);

            user = Sql.GetUserById(userId, session);

            if (user != null)
            {
                //StripeCustomer customer = null;
                Customer customer = null;
                if (string.IsNullOrEmpty(user.CustomerStripeId))
                {
                    //customer = Lib.Services.StripeAPI.StripeService.CreateCustomer(user.CompanyName, user.Email, 0);
                    customer = Lib.Services.StripeAPI.StripeService.CreateCustomerNew(user.Email, user.CompanyName, user.Phone);
                    if (customer != null && !string.IsNullOrEmpty(customer.Id))
                    {
                        user.CustomerStripeId = customer.Id;
                        user = GlobalDBMethods.UpDateUser(user, session);
                    }
                }
                else
                {
                    customer = Lib.Services.StripeAPI.StripeService.CreateCustomerNew(user.Email, user.CompanyName, user.Phone);
                }

                if (customer != null && !string.IsNullOrEmpty(customer.Id))
                {
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

                        user = GlobalDBMethods.UpDateUser(user, session);

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

                    ElioPackets packet = Sql.GetPacketById(packId, session);
                    if (packet != null)
                    {
                        StripePlan plan = Lib.Services.StripeAPI.StripeService.GetPlan(packet.stripePlanId);
                        if (plan != null && !string.IsNullOrEmpty(plan.Id))
                        {
                            StripeList<Subscription> subs = customer.Subscriptions;
                            bool hasSubscriptions = subs != null && subs.Count() > 0;

                            StripeToken cardToken = null;
                            StripeCard card = null;
                            string cardID = "";

                            if (!hasSubscriptions)
                            {
                                if (string.IsNullOrEmpty(customer.DefaultSourceId) && cardID == "")
                                {
                                    cardToken = Lib.Services.StripeAPI.StripeService.CreateCardToken(number, expMonth, expYear, cvc, customer.Description, false);

                                    if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                    {
                                        card = Lib.Services.StripeAPI.StripeService.CreateCreditCard(customer.Id, cardToken.Id);
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            if (card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Pass || card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown)
                                            {
                                                cardID = card.Id;

                                                StripeCoupon stripeCoupon = null;

                                                if (hasDiscount)
                                                {
                                                    ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                    if (planCoupon != null)
                                                    {
                                                        if (plan.Id == planCoupon.StripePlanId)
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeService.GetCoupon(planCoupon.CouponId);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                stripeCoupon = Lib.Services.StripeAPI.StripeService.CreateCoupon(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, planCoupon.PercentOff, planCoupon.RedeemBy);
                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                {
                                                                    coupon = stripeCoupon.Id;
                                                                }
                                                                else
                                                                {
                                                                    //cooupon could not be created
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //wrong packet to plan
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //wrong selected packet for coupon to have discount
                                                        return false;
                                                    }
                                                }

                                                StripeSubscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlan(customer.Id, plan.Id, coupon);
                                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                                {
                                                    ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                    sub.UserId = user.Id;
                                                    sub.CustomerId = subscription.Customer;
                                                    sub.SubscriptionId = subscription.Id;
                                                    sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                    sub.PlanId = subscription.Plan.Id;
                                                    sub.PlanNickname = subscription.Plan.Nickname;
                                                    sub.CreatedAt = Convert.ToDateTime(subscription.Start);
                                                    sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                    sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                    sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                    sub.HasDiscount = hasDiscount ? 1 : 0;
                                                    sub.Status = subscription.Status.ToString();
                                                    sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                    sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                    sub.Amount = subscription.Plan.Amount;

                                                    DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                    loader.Insert(sub);

                                                    StripeList<StripeInvoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscription(subscription.Customer, subscription.Id);
                                                    if (invoices != null && invoices.Count() > 0)
                                                    {
                                                        foreach (StripeInvoice invoice in invoices)
                                                        {
                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                            subInvoice.UserId = user.Id;
                                                            subInvoice.UserSubscriptionId = sub.Id;
                                                            subInvoice.CustomerId = invoice.Customer;
                                                            subInvoice.InvoiceId = invoice.Id;
                                                            subInvoice.ChargeId = invoice.Charge;
                                                            subInvoice.SubscriptionId = sub.Id.ToString();
                                                            subInvoice.IsClosed = (bool)invoice.Closed ? 1 : 0;
                                                            subInvoice.Currency = invoice.Currency;
                                                            subInvoice.Date = Convert.ToDateTime(invoice.Date);
                                                            subInvoice.Description = "Invoice for customer " + invoice.Customer;
                                                            subInvoice.HostedInvoiceUrl = "invoice.HostedInvoiceUrl";
                                                            subInvoice.InvoicePdf = "invoice.InvoicePdf";
                                                            subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                            subInvoice.Number = "invoice.Number";
                                                            subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                            subInvoice.PeriodStart = Convert.ToDateTime(invoice.PeriodStart);
                                                            subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);
                                                            subInvoice.ReceiptNumber = "ReceiptNumber";  // (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                            subInvoice.HasDiscount = sub.HasDiscount;
                                                            subInvoice.TotalAmount = invoice.Total;
                                                            subInvoice.SubTotalAmount = invoice.Subtotal;

                                                            if (invoice.Discount != null)
                                                                if (invoice.Discount.Coupon != null)
                                                                {
                                                                    StripeCoupon stripeSubCoupon = invoice.Discount.Coupon;
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
                                                                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                            }
                                                        }

                                                        #endregion

                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                        {
                                                            user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion

                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        //invoice could not be created
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //subscription failure
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //card cvc check failed
                                                string cardCvcCheck = card.CvcCheck.ToString();
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //card could not be created in stripe
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //cardToken could not be created in stripe
                                        return false;
                                    }
                                }
                                else
                                {
                                    cardID = customer.DefaultSourceId;

                                    StripeCoupon stripeCoupon = null;

                                    cardToken = Lib.Services.StripeAPI.StripeService.CreateCardToken(number, expMonth, expYear, cvc, customer.Description, false);

                                    if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                    {
                                        card = Lib.Services.StripeAPI.StripeService.CreateCreditCard(customer.Id, cardToken.Id);
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            if (card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Pass || card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown)
                                            {
                                                cardID = card.Id;

                                                if (hasDiscount)
                                                {
                                                    ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                    if (planCoupon != null)
                                                    {
                                                        if (plan.Id == planCoupon.StripePlanId)
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeService.GetCoupon(planCoupon.CouponId);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                stripeCoupon = Lib.Services.StripeAPI.StripeService.CreateCoupon(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, planCoupon.PercentOff, planCoupon.RedeemBy);
                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                {
                                                                    coupon = stripeCoupon.Id;
                                                                }
                                                                else
                                                                {
                                                                    //cooupon could not be created
                                                                    return false;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //wrong packet to plan
                                                            return false;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //wrong selected packet for coupon to have discount
                                                        return false;
                                                    }
                                                }

                                                StripeSubscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlan(customer.Id, plan.Id, coupon);
                                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                                {
                                                    ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                    sub.UserId = user.Id;
                                                    sub.CustomerId = subscription.Customer;
                                                    sub.SubscriptionId = subscription.Id;
                                                    sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                    sub.PlanId = subscription.Plan.Id;
                                                    sub.PlanNickname = subscription.Plan.Nickname;
                                                    sub.CreatedAt = Convert.ToDateTime(subscription.Start);
                                                    sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                    sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                    sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                    sub.HasDiscount = hasDiscount ? 1 : 0;
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
                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                            subInvoice.UserId = user.Id;
                                                            subInvoice.UserSubscriptionId = sub.Id;
                                                            subInvoice.CustomerId = invoice.Customer;
                                                            subInvoice.InvoiceId = invoice.Id;
                                                            subInvoice.ChargeId = invoice.Charge;
                                                            subInvoice.SubscriptionId = sub.Id.ToString();
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
                                                            subInvoice.ReceiptNumber = invoice.Charge;  // (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                            subInvoice.HasDiscount = sub.HasDiscount;
                                                            subInvoice.TotalAmount = invoice.Total;
                                                            subInvoice.SubTotalAmount = invoice.Subtotal;

                                                            if (invoice.Discount != null)
                                                                if (invoice.Discount.Coupon != null)
                                                                {
                                                                    StripeCoupon stripeSubCoupon = invoice.Discount.Coupon;
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

                                                        #region Fix Packet Features Items for Premium User

                                                        if (packet.Id != (int)Packets.PremiumService)
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
                                                                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                            }
                                                        }

                                                        #endregion

                                                        #region Update User With Billing Details Status

                                                        if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                        {
                                                            user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                            user = GlobalDBMethods.UpDateUser(user, session);
                                                        }

                                                        #endregion

                                                        return true;
                                                    }
                                                    else
                                                    {
                                                        //invoice could not be created
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //subscription failure
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //cvcCheck not passed
                                                string CvcCheck = card.CvcCheck.ToString();
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //card could not be created
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //card token could not be created
                                        return false;
                                    }
                                }
                            }
                            else
                            {
                                StripeList<IPaymentSource> cusSources = customer.Sources;
                                bool hasSources = cusSources != null && cusSources.Count() > 0;

                                if (hasSources)
                                {
                                    foreach (StripeCard item in cusSources.Data)
                                    {
                                        StripeCard source = new StripeCard();
                                        source = item;

                                        card = source;
                                        if (card != null && !string.IsNullOrEmpty(card.Id))
                                        {
                                            cardID = card.Id;
                                            break;
                                        }
                                    }
                                }

                                cardToken = Lib.Services.StripeAPI.StripeService.CreateCardToken(number, expMonth, expYear, cvc, customer.Description, false);

                                if (cardToken != null && !string.IsNullOrEmpty(cardToken.Id))
                                {
                                    //card = Lib.Services.StripeAPI.StripeService.GetCreditCard(customer.Id, cardID);
                                    card = Lib.Services.StripeAPI.StripeService.CreateCreditCard(customer.Id, cardToken.Id);
                                    if (card != null && !string.IsNullOrEmpty(card.Id))
                                    {
                                        if (card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Pass || card.CvcCheck == ServiceStack.Stripe.Types.StripeCvcCheck.Unknown)
                                        {
                                            cardID = card.Id;

                                            StripeCoupon stripeCoupon = null;

                                            if (hasDiscount)
                                            {
                                                ElioPacketsStripeCoupons planCoupon = Sql.GetPlanStripeCoupon(coupon.Trim().ToUpper(), session);
                                                if (planCoupon != null)
                                                {
                                                    if (plan.Id == planCoupon.StripePlanId)
                                                    {
                                                        stripeCoupon = Lib.Services.StripeAPI.StripeService.GetCoupon(planCoupon.CouponId);
                                                        if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                        {
                                                            coupon = stripeCoupon.Id;
                                                        }
                                                        else
                                                        {
                                                            stripeCoupon = Lib.Services.StripeAPI.StripeService.CreateCoupon(planCoupon.CouponId, planCoupon.Name, planCoupon.Duration, planCoupon.AmountOff, planCoupon.Currency, planCoupon.DurationInMonths, planCoupon.MaxRedemptions, planCoupon.PercentOff, planCoupon.RedeemBy);
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                            {
                                                                coupon = stripeCoupon.Id;
                                                            }
                                                            else
                                                            {
                                                                //cooupon could not be created
                                                                return false;
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return false;
                                                    }
                                                }
                                                else
                                                {
                                                    //wrong selected packet for coupon to have discount
                                                    return false;
                                                }
                                            }

                                            StripeSubscription subscription = Lib.Services.StripeAPI.StripeService.CreateExistingCustomerSubscriptionToPlan(customer.Id, plan.Id, coupon);
                                            if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                            {
                                                ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                sub.UserId = user.Id;
                                                sub.CustomerId = subscription.Customer;
                                                sub.SubscriptionId = subscription.Id;
                                                sub.CouponId = (coupon != "") ? stripeCoupon.Id : "";
                                                sub.PlanId = subscription.Plan.Id;
                                                sub.PlanNickname = subscription.Plan.Nickname;
                                                sub.CreatedAt = Convert.ToDateTime(subscription.Start);
                                                sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                sub.HasDiscount = hasDiscount ? 1 : 0;
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
                                                        ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                        subInvoice.UserId = user.Id;
                                                        subInvoice.UserSubscriptionId = sub.Id;
                                                        subInvoice.CustomerId = invoice.Customer;
                                                        subInvoice.InvoiceId = invoice.Id;
                                                        subInvoice.ChargeId = invoice.Charge;
                                                        subInvoice.SubscriptionId = sub.Id.ToString();
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
                                                        subInvoice.ReceiptNumber = invoice.Charge;  // (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                        subInvoice.HasDiscount = sub.HasDiscount;
                                                        subInvoice.TotalAmount = invoice.Total;
                                                        subInvoice.SubTotalAmount = invoice.Subtotal;

                                                        if (invoice.Discount != null)
                                                            if (invoice.Discount.Coupon != null)
                                                            {
                                                                StripeCoupon stripeSubCoupon = invoice.Discount.Coupon;
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
                                                            Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                        }
                                                    }

                                                    #endregion

                                                    #region Update User With Billing Details Status

                                                    if (user.BillingType == 1 && packet.Id != (int)Packets.PremiumService)
                                                    {
                                                        user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);

                                                        user = GlobalDBMethods.UpDateUser(user, session);
                                                    }

                                                    #endregion

                                                    return true;
                                                }
                                                else
                                                {
                                                    //invoice could not be created
                                                    return false;
                                                }
                                            }
                                            else
                                            {
                                                //subscription failure
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            //cvc check failed
                                            string cvcCheck = card.CvcCheck.ToString();
                                            return false;
                                        }
                                    }
                                    else
                                    {
                                        //card could not be found in stripe
                                        return false;
                                    }
                                }
                                else
                                {
                                    //card token could not be created
                                    return false;
                                }
                            }
                        }
                        else
                        {
                            //Stripe plan not found
                            return false;
                        }
                    }
                    else
                    {
                        //Elio packet not found
                        return false;
                    }
                }
                else
                {
                    //user not exists in stripe
                    return false;
                }
            }
            else
            {
                //user not logged in
                return false;
            }
        }

        public static bool FixCustomerSubscriptionInvoicesNew(ElioUsers user, DBSession session)
        {
            bool hasInserted = false;
            if (user != null)
            {
                if (user.BillingType != (int)BillingTypePacket.FreemiumPacketType)
                {
                    if (!string.IsNullOrEmpty(user.CustomerStripeId))
                    {
                        List<ElioUsersSubscriptions> subscriptions = Sql.GetCustomerExpiredSubscription(user.Id, user.CustomerStripeId, session);

                        if (subscriptions.Count > 0)
                        {
                            foreach (ElioUsersSubscriptions sub in subscriptions)
                            {
                                if (!string.IsNullOrEmpty(sub.SubscriptionId))
                                {
                                    #region Update Subscription

                                    StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(sub.CustomerId, sub.SubscriptionId);
                                    if (invoices != null && invoices.Data.Count() > 0)
                                    {
                                        foreach (Stripe.Invoice invoice in invoices)
                                        {
                                            if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                            {
                                                bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
                                                if (!existInvoice)
                                                {
                                                    #region Insert new Invoice

                                                    ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                    subInvoice.UserId = user.Id;
                                                    subInvoice.UserSubscriptionId = sub.Id;
                                                    subInvoice.CustomerId = invoice.CustomerId;
                                                    subInvoice.InvoiceId = invoice.Id;
                                                    subInvoice.ChargeId = "";
                                                    subInvoice.SubscriptionId = sub.SubscriptionId;
                                                    subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                                    subInvoice.Currency = invoice.Currency;
                                                    subInvoice.Date = Convert.ToDateTime(invoice.Created);
                                                    subInvoice.Description = invoice.Description;
                                                    subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                                    subInvoice.InvoicePdf = invoice.InvoicePdf;
                                                    subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                    subInvoice.Number = invoice.Number;
                                                    subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                    subInvoice.PeriodStart = invoice.PeriodStart;
                                                    subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
                                                    subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                    subInvoice.HasDiscount = sub.HasDiscount;
                                                    subInvoice.TotalAmount = (int)invoice.Total;
                                                    subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                                    if (invoice.Discount != null)
                                                        if (invoice.Discount.Coupon != null)
                                                        {
                                                            Coupon stripeCoupon = invoice.Discount.Coupon;
                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                subInvoice.CouponId = stripeCoupon.Id;
                                                            else
                                                                subInvoice.CouponId = "";
                                                        }
                                                        else
                                                            subInvoice.CouponId = "";
                                                    else
                                                        subInvoice.CouponId = "";

                                                    DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                    invIoader.Insert(subInvoice);

                                                    hasInserted = true;

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Update Invoice

                                                    ElioUsersSubscriptionsInvoices subInvoice = Sql.GetInvoiceByInvoiceID(invoice.Id, session);
                                                    if (subInvoice != null)
                                                    {
                                                        subInvoice.UserId = user.Id;
                                                        subInvoice.UserSubscriptionId = sub.Id;
                                                        subInvoice.CustomerId = invoice.CustomerId;
                                                        subInvoice.InvoiceId = invoice.Id;
                                                        subInvoice.ChargeId = "";
                                                        subInvoice.SubscriptionId = sub.SubscriptionId;
                                                        subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                                        subInvoice.Currency = invoice.Currency;
                                                        subInvoice.Date = Convert.ToDateTime(invoice.Created);
                                                        subInvoice.Description = invoice.Description;
                                                        subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                                        subInvoice.InvoicePdf = invoice.InvoicePdf;
                                                        subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                        subInvoice.Number = invoice.Number;
                                                        subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                        subInvoice.PeriodStart = invoice.PeriodStart;
                                                        subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
                                                        subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                        subInvoice.HasDiscount = sub.HasDiscount;
                                                        subInvoice.TotalAmount = (int)invoice.Total;
                                                        subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                                        if (invoice.Discount != null)
                                                            if (invoice.Discount.Coupon != null)
                                                            {
                                                                Coupon stripeCoupon = invoice.Discount.Coupon;
                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                    subInvoice.CouponId = stripeCoupon.Id;
                                                                else
                                                                    subInvoice.CouponId = "";
                                                            }
                                                            else
                                                                subInvoice.CouponId = "";
                                                        else
                                                            subInvoice.CouponId = "";

                                                        DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                        invIoader.Update(subInvoice);
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }
                                    }

                                    Subscription stripeSub = Lib.Services.StripeAPI.StripeService.GetSubscriptionNew(sub.SubscriptionId);
                                    if (stripeSub != null && !string.IsNullOrEmpty(stripeSub.Id))
                                    {
                                        sub.Status = stripeSub.Status.ToString();
                                        if (stripeSub.CanceledAt != null)
                                            sub.CanceledAt = Convert.ToDateTime(stripeSub.CanceledAt);

                                        if (stripeSub.Items.Data[0].Plan != null && stripeSub.Items.Data[0].Plan.Id != sub.PlanId)
                                        {
                                            ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(stripeSub.Items.Data[0].Plan.Id, session);
                                            if (elioPacket != null)
                                                sub.PlanId = elioPacket.stripePlanId;
                                            else
                                                sub.PlanId = stripeSub.Items.Data[0].Plan.Id;

                                            //sub.PlanId = stripeSub.StripePlan.Id;
                                            sub.PlanNickname = (stripeSub.Items.Data[0].Plan != null && stripeSub.Items.Data[0].Plan.Nickname != null) ? stripeSub.Items.Data[0].Plan.Nickname : "";
                                            sub.Amount = stripeSub.Items.Data[0].Plan != null ? (int)stripeSub.Items.Data[0].Plan.Amount : 0;
                                        }

                                        sub.CurrentPeriodStart = Convert.ToDateTime(stripeSub.CurrentPeriodStart);
                                        sub.CurrentPeriodEnd = Convert.ToDateTime(stripeSub.CurrentPeriodEnd);

                                        DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                        loader.Update(sub);
                                    }

                                    #endregion
                                }
                            }

                            #region Packet Status Features

                            ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

                            if ((packetFeatures != null && packetFeatures.ExpirationDate < DateTime.Now) || packetFeatures == null)
                            {
                                ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
                                if (userSubscription != null)
                                {
                                    int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
                                    if (packId > 0)
                                    {
                                        if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
                                            packId = (int)Packets.Premium;

                                        ElioPackets packet = Sql.GetPacketById(packId, session);
                                        if (packet != null && packet.Id != (int)Packets.PremiumService && packet.Id != (int)Packets.ServiceFollowUp && packet.Id != (int)Packets.SelfService && packet.Id != (int)Packets.AccountManagerService && packet.Id != (int)Packets.PremiumService299)
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

                                                //int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                //int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                //int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                                                //int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
                                                //int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

                                                //double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                                                #endregion

                                                #region Insert / Update Packet Status Features

                                                //ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

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
                            }

                            #endregion
                        }
                        else
                        {
                            #region From Checkout Session Insert/Update Subscription/Invoices

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

                                    if (user.BillingType == (int)BillingTypePacket.FreemiumPacketType)
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

                                    user = GlobalDBMethods.UpDateUser(user, session);

                                    ElioPackets packet = Sql.GetPacketByStripePlanId(userSession.StripePlanId, session);
                                    if (packet != null)
                                    {
                                        Subscription subscription = Lib.Services.StripeAPI.StripeService.GetSubscriptionNew(createdSession.SubscriptionId);
                                        if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
                                        {
                                            bool exists = Sql.ExistCustomerSubscription(user.CustomerStripeId, subscription.Id, session);
                                            if (!exists)
                                            {
                                                #region Insert New Subscription

                                                ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                                                sub.UserId = user.Id;
                                                sub.CustomerId = subscription.CustomerId;
                                                sub.SubscriptionId = subscription.Id;
                                                sub.CouponId = (subscription.Discount != null && !string.IsNullOrEmpty(subscription.Discount.Coupon.Id)) ? subscription.Discount.Coupon.Id : "";
                                                sub.PlanId = userSession.StripePlanId;
                                                sub.PlanNickname = packet.PackDescription;
                                                sub.CreatedAt = System.Convert.ToDateTime(subscription.Created);
                                                sub.CurrentPeriodStart = System.Convert.ToDateTime(subscription.CurrentPeriodStart);
                                                sub.CurrentPeriodEnd = System.Convert.ToDateTime(subscription.CurrentPeriodEnd);
                                                sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                                                sub.HasDiscount = createdSession.AmountSubtotal != createdSession.AmountTotal ? 1 : 0;
                                                sub.Status = subscription.Status.ToString();
                                                sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                                                sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                                                sub.Amount = (int)createdSession.AmountTotal;

                                                DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                loader.Insert(sub);

                                                hasInserted = true;

                                                #endregion

                                                StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(sub.CustomerId, sub.SubscriptionId);
                                                if (invoices != null && invoices.Data.Count() > 0)
                                                {
                                                    foreach (Stripe.Invoice invoice in invoices)
                                                    {
                                                        if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                                        {
                                                            bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
                                                            if (!existInvoice)
                                                            {
                                                                #region Insert New Invoice

                                                                Stripe.Invoice invoiceNew = Lib.Services.StripeAPI.StripeService.GetInvoiceByInvoiceIDNew(createdSession.InvoiceId);
                                                                if (invoiceNew != null && !string.IsNullOrEmpty(invoiceNew.Id))
                                                                {
                                                                    ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                                    subInvoice.UserId = user.Id;
                                                                    subInvoice.UserSubscriptionId = sub.Id;
                                                                    subInvoice.CustomerId = subscription.CustomerId;
                                                                    subInvoice.InvoiceId = invoiceNew.Id;
                                                                    subInvoice.ChargeId = "";
                                                                    subInvoice.SubscriptionId = subscription.Id;
                                                                    subInvoice.IsClosed = (bool)invoiceNew.Paid ? 1 : 0;
                                                                    subInvoice.Currency = invoiceNew.Currency;
                                                                    subInvoice.Date = System.Convert.ToDateTime(invoiceNew.Created);
                                                                    subInvoice.Description = invoiceNew.Description;
                                                                    subInvoice.HostedInvoiceUrl = invoiceNew.HostedInvoiceUrl;
                                                                    subInvoice.InvoicePdf = invoiceNew.InvoicePdf;
                                                                    subInvoice.NextPaymentAttempt = invoiceNew.NextPaymentAttempt != null ? invoiceNew.NextPaymentAttempt : null;
                                                                    subInvoice.Number = invoiceNew.Number;
                                                                    subInvoice.IsPaid = (bool)invoiceNew.Paid ? 1 : 0;
                                                                    subInvoice.PeriodStart = System.Convert.ToDateTime(invoiceNew.PeriodStart);
                                                                    subInvoice.PeriodEnd = System.Convert.ToDateTime(invoiceNew.PeriodEnd);
                                                                    subInvoice.ReceiptNumber = (invoiceNew.ReceiptNumber != null) ? invoiceNew.ReceiptNumber : "";
                                                                    subInvoice.HasDiscount = sub.HasDiscount;
                                                                    subInvoice.TotalAmount = (int)invoiceNew.Total;
                                                                    subInvoice.SubTotalAmount = (int)invoiceNew.Subtotal;

                                                                    if (invoiceNew.Discount != null)
                                                                        if (invoiceNew.Discount.Coupon != null)
                                                                        {
                                                                            Coupon stripeSubCoupon = invoiceNew.Discount.Coupon;
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
                                                            }
                                                            else
                                                            {
                                                                #region Update Invoice

                                                                ElioUsersSubscriptionsInvoices subInvoice = Sql.GetInvoiceByInvoiceID(invoice.Id, session);
                                                                if (subInvoice != null)
                                                                {
                                                                    subInvoice.UserId = user.Id;
                                                                    subInvoice.UserSubscriptionId = sub.Id;
                                                                    subInvoice.CustomerId = invoice.CustomerId;
                                                                    subInvoice.InvoiceId = invoice.Id;
                                                                    subInvoice.ChargeId = "";
                                                                    subInvoice.SubscriptionId = sub.SubscriptionId;
                                                                    subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                                                    subInvoice.Currency = invoice.Currency;
                                                                    subInvoice.Date = System.Convert.ToDateTime(invoice.Created);
                                                                    subInvoice.Description = invoice.Description;
                                                                    subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                                                    subInvoice.InvoicePdf = invoice.InvoicePdf;
                                                                    subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                                    subInvoice.Number = invoice.Number;
                                                                    subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                                    subInvoice.PeriodStart = invoice.PeriodStart;
                                                                    subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? System.Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
                                                                    subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                                    subInvoice.HasDiscount = sub.HasDiscount;
                                                                    subInvoice.TotalAmount = (int)invoice.Total;
                                                                    subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                                                    if (invoice.Discount != null)
                                                                        if (invoice.Discount.Coupon != null)
                                                                        {
                                                                            Coupon stripeCoupon = invoice.Discount.Coupon;
                                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                                subInvoice.CouponId = stripeCoupon.Id;
                                                                            else
                                                                                subInvoice.CouponId = "";
                                                                        }
                                                                        else
                                                                            subInvoice.CouponId = "";
                                                                    else
                                                                        subInvoice.CouponId = "";

                                                                    DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                                    invIoader.Update(subInvoice);
                                                                }

                                                                #endregion
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                #region Update Subscription

                                                ElioUsersSubscriptions sub = Sql.GetSubscriptionBySubID(subscription.Id, session);
                                                if (sub != null)
                                                {
                                                    StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(sub.CustomerId, sub.SubscriptionId);
                                                    if (invoices != null && invoices.Data.Count() > 0)
                                                    {
                                                        foreach (Stripe.Invoice invoice in invoices)
                                                        {
                                                            if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
                                                            {
                                                                bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
                                                                if (!existInvoice)
                                                                {
                                                                    #region Insert new Invoice

                                                                    ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

                                                                    subInvoice.UserId = user.Id;
                                                                    subInvoice.UserSubscriptionId = sub.Id;
                                                                    subInvoice.CustomerId = invoice.CustomerId;
                                                                    subInvoice.InvoiceId = invoice.Id;
                                                                    subInvoice.ChargeId = "";
                                                                    subInvoice.SubscriptionId = sub.SubscriptionId;
                                                                    subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                                                    subInvoice.Currency = invoice.Currency;
                                                                    subInvoice.Date = System.Convert.ToDateTime(invoice.Created);
                                                                    subInvoice.Description = invoice.Description;
                                                                    subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                                                    subInvoice.InvoicePdf = invoice.InvoicePdf;
                                                                    subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                                    subInvoice.Number = invoice.Number;
                                                                    subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                                    subInvoice.PeriodStart = invoice.PeriodStart;
                                                                    subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? System.Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
                                                                    subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                                    subInvoice.HasDiscount = sub.HasDiscount;
                                                                    subInvoice.TotalAmount = (int)invoice.Total;
                                                                    subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                                                    if (invoice.Discount != null)
                                                                        if (invoice.Discount.Coupon != null)
                                                                        {
                                                                            Coupon stripeCoupon = invoice.Discount.Coupon;
                                                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                                subInvoice.CouponId = stripeCoupon.Id;
                                                                            else
                                                                                subInvoice.CouponId = "";
                                                                        }
                                                                        else
                                                                            subInvoice.CouponId = "";
                                                                    else
                                                                        subInvoice.CouponId = "";

                                                                    DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                                    invIoader.Insert(subInvoice);

                                                                    hasInserted = true;

                                                                    #endregion
                                                                }
                                                                else
                                                                {
                                                                    #region Update Invoice

                                                                    ElioUsersSubscriptionsInvoices subInvoice = Sql.GetInvoiceByInvoiceID(invoice.Id, session);
                                                                    if (subInvoice != null)
                                                                    {
                                                                        subInvoice.UserId = user.Id;
                                                                        subInvoice.UserSubscriptionId = sub.Id;
                                                                        subInvoice.CustomerId = invoice.CustomerId;
                                                                        subInvoice.InvoiceId = invoice.Id;
                                                                        subInvoice.ChargeId = "";
                                                                        subInvoice.SubscriptionId = sub.SubscriptionId;
                                                                        subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
                                                                        subInvoice.Currency = invoice.Currency;
                                                                        subInvoice.Date = System.Convert.ToDateTime(invoice.Created);
                                                                        subInvoice.Description = invoice.Description;
                                                                        subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
                                                                        subInvoice.InvoicePdf = invoice.InvoicePdf;
                                                                        subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
                                                                        subInvoice.Number = invoice.Number;
                                                                        subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
                                                                        subInvoice.PeriodStart = invoice.PeriodStart;
                                                                        subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? System.Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
                                                                        subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                                                        subInvoice.HasDiscount = sub.HasDiscount;
                                                                        subInvoice.TotalAmount = (int)invoice.Total;
                                                                        subInvoice.SubTotalAmount = (int)invoice.Subtotal;

                                                                        if (invoice.Discount != null)
                                                                            if (invoice.Discount.Coupon != null)
                                                                            {
                                                                                Coupon stripeCoupon = invoice.Discount.Coupon;
                                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                                                    subInvoice.CouponId = stripeCoupon.Id;
                                                                                else
                                                                                    subInvoice.CouponId = "";
                                                                            }
                                                                            else
                                                                                subInvoice.CouponId = "";
                                                                        else
                                                                            subInvoice.CouponId = "";

                                                                        DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                                                        invIoader.Update(subInvoice);
                                                                    }

                                                                    #endregion
                                                                }
                                                            }
                                                        }
                                                    }

                                                    Subscription stripeSub = Lib.Services.StripeAPI.StripeService.GetSubscriptionNew(sub.SubscriptionId);
                                                    if (stripeSub != null && !string.IsNullOrEmpty(stripeSub.Id))
                                                    {
                                                        sub.Status = stripeSub.Status.ToString();
                                                        if (stripeSub.CanceledAt != null)
                                                            sub.CanceledAt = System.Convert.ToDateTime(stripeSub.CanceledAt);

                                                        if (stripeSub.Items.Data[0].Plan != null && stripeSub.Items.Data[0].Plan.Id != sub.PlanId)
                                                        {
                                                            ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(stripeSub.Items.Data[0].Plan.Id, session);
                                                            if (elioPacket != null)
                                                                sub.PlanId = elioPacket.stripePlanId;
                                                            else
                                                                sub.PlanId = stripeSub.Items.Data[0].Plan.Id;

                                                            //sub.PlanId = stripeSub.StripePlan.Id;
                                                            sub.PlanNickname = (stripeSub.Items.Data[0].Plan != null && stripeSub.Items.Data[0].Plan.Nickname != null) ? stripeSub.Items.Data[0].Plan.Nickname : "";
                                                            sub.Amount = stripeSub.Items.Data[0].Plan != null ? (int)stripeSub.Items.Data[0].Plan.Amount : 0;
                                                        }

                                                        sub.CurrentPeriodStart = System.Convert.ToDateTime(stripeSub.CurrentPeriodStart);
                                                        sub.CurrentPeriodEnd = System.Convert.ToDateTime(stripeSub.CurrentPeriodEnd);

                                                        DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                                                        loader.Update(sub);
                                                    }
                                                }

                                                #endregion
                                            }

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
                                                    Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                                                }
                                            }

                                            #endregion
                                        }
                                    }

                                    bool setEpiredSess = Sql.SetUSerCheckoutSessionsExpired(user.Id, session);
                                    if (!setEpiredSess)
                                    {
                                        Logger.DetailedError("", string.Format("DashboardHomePage: Page_Load: FixCustomerSubscriptionInvoicesNew --> After success payment User {0} could not set expired his checkout sessions on Elio", user.Id), string.Format("After success payment User {0} could not proceed to payment on Stripe at {1}", user.Id, DateTime.Now));
                                    }

                                }
                            }

                            #endregion
                        }
                    }
                }
            }

            return hasInserted;
        }

        //public static bool FixCustomerSubscriptionInvoices(ElioUsers user, DBSession session)
        //{
        //    bool hasInserted = false;
        //    if (user != null)
        //    {
        //        if (user.BillingType != (int)BillingTypePacket.FreemiumPacketType)
        //        {
        //            if (!string.IsNullOrEmpty(user.CustomerStripeId))
        //            {
        //                bool needsUpdate = Sql.CustomerNeedsPlanSubscriptionUpdate(user.Id, user.CustomerStripeId, session) || Sql.CustomerNeedsServiceSubscriptionUpdate(user.Id, user.CustomerStripeId, session);
        //                if (needsUpdate)
        //                {
        //                    Customer customer = Lib.Services.StripeAPI.StripeService.GetCustomerNew(user.CustomerStripeId);
        //                    if (customer != null && !string.IsNullOrEmpty(customer.Id))
        //                    {
        //                        if (customer.Deleted != null && Convert.ToBoolean(customer.Deleted))
        //                            return false;

        //                        bool hasSubscriptions = customer.Subscriptions != null;
        //                        if (hasSubscriptions)
        //                        {
        //                            foreach (Subscription subscription in customer.Subscriptions)
        //                            {
        //                                if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
        //                                {
        //                                    bool exists = Sql.ExistCustomerSubscription(user.CustomerStripeId, subscription.Id, session);
        //                                    if (!exists)
        //                                    {
        //                                        #region Insert New Subscription

        //                                        ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

        //                                        string coupon = "";
        //                                        if (subscription.Discount != null && subscription.Discount.Coupon != null && !string.IsNullOrEmpty(subscription.Discount.Coupon.Id))
        //                                            coupon = subscription.Discount.Coupon.Id;

        //                                        sub.UserId = user.Id;
        //                                        sub.CustomerId = subscription.CustomerId;
        //                                        sub.SubscriptionId = subscription.Id;
        //                                        sub.CouponId = (coupon != "") ? coupon : "";

        //                                        if (subscription.Items.Data[0].Plan != null)
        //                                        {
        //                                            ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(subscription.Items.Data[0].Plan.Id, session);
        //                                            if (elioPacket != null)
        //                                                sub.PlanId = elioPacket.stripePlanId;
        //                                            else
        //                                                sub.PlanId = subscription.Items.Data[0].Plan != null ? subscription.Items.Data[0].Plan.Id : "";
        //                                        }
        //                                        else
        //                                            sub.PlanId = "";

        //                                        sub.PlanNickname = (subscription.Items.Data[0].Plan != null && subscription.Items.Data[0].Plan.Nickname != null) ? subscription.Items.Data[0].Plan.Nickname : "";
        //                                        sub.CreatedAt = Convert.ToDateTime(subscription.StartDate);
        //                                        sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
        //                                        sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
        //                                        sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
        //                                        sub.HasDiscount = sub.CouponId != "" ? 1 : 0;
        //                                        sub.Status = subscription.Status.ToString();
        //                                        sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
        //                                        sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
        //                                        sub.Amount = (subscription.Items.Data[0].Plan != null) ? (int)subscription.Items.Data[0].Plan.Amount : 0;

        //                                        DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
        //                                        loader.Insert(sub);

        //                                        hasInserted = true;

        //                                        StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(subscription.CustomerId, subscription.Id);
        //                                        if (invoices != null && invoices.Count() > 0)
        //                                        {
        //                                            foreach (Stripe.Invoice invoice in invoices)
        //                                            {
        //                                                if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
        //                                                {
        //                                                    bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
        //                                                    if (!existInvoice)
        //                                                    {
        //                                                        #region Insert New Invoice

        //                                                        ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

        //                                                        subInvoice.UserId = user.Id;
        //                                                        subInvoice.UserSubscriptionId = sub.Id;
        //                                                        subInvoice.CustomerId = invoice.CustomerId;
        //                                                        subInvoice.InvoiceId = invoice.Id;
        //                                                        subInvoice.ChargeId = "";
        //                                                        subInvoice.SubscriptionId = subscription.Id;
        //                                                        subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
        //                                                        subInvoice.Currency = invoice.Currency;
        //                                                        subInvoice.Date = Convert.ToDateTime(invoice.Created);
        //                                                        subInvoice.Description = invoice.Description;
        //                                                        subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
        //                                                        subInvoice.InvoicePdf = invoice.InvoicePdf;
        //                                                        subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
        //                                                        subInvoice.Number = invoice.Number;
        //                                                        subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
        //                                                        subInvoice.PeriodStart = invoice.PeriodStart;
        //                                                        subInvoice.PeriodEnd = Convert.ToDateTime(invoice.PeriodEnd);     //invoice.PeriodEnd != null ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.DueDate != null ? Convert.ToDateTime(invoice.DueDate) : subInvoice.PeriodStart.AddMonths(1);
        //                                                        subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
        //                                                        subInvoice.HasDiscount = sub.HasDiscount;
        //                                                        subInvoice.TotalAmount = (int)invoice.Total;
        //                                                        subInvoice.SubTotalAmount = (int)invoice.Subtotal;

        //                                                        if (invoice.Discount != null)
        //                                                            if (invoice.Discount.Coupon != null)
        //                                                            {
        //                                                                Coupon stripeCoupon = invoice.Discount.Coupon;
        //                                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
        //                                                                    subInvoice.CouponId = stripeCoupon.Id;
        //                                                                else
        //                                                                    subInvoice.CouponId = "";
        //                                                            }
        //                                                            else
        //                                                                subInvoice.CouponId = "";
        //                                                        else
        //                                                            subInvoice.CouponId = "";

        //                                                        DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
        //                                                        invIoader.Insert(subInvoice);

        //                                                        hasInserted = true;

        //                                                        #endregion
        //                                                    }
        //                                                }
        //                                            }
        //                                        }

        //                                        #endregion
        //                                    }
        //                                    else
        //                                    {
        //                                        #region Update Subscription

        //                                        ElioUsersSubscriptions sub = Sql.GetSubscriptionBySubID(subscription.Id, session);
        //                                        if (sub != null)
        //                                        {
        //                                            StripeList<Stripe.Invoice> invoices = Lib.Services.StripeAPI.StripeService.GetCustomerInvoicesBySubscriptionNew(subscription.CustomerId, subscription.Id);
        //                                            if (invoices != null && invoices.Count() > 0)
        //                                            {
        //                                                foreach (Stripe.Invoice invoice in invoices)
        //                                                {
        //                                                    if (invoice != null && !string.IsNullOrEmpty(invoice.Id))
        //                                                    {
        //                                                        bool existInvoice = Sql.ExistInvoice(invoice.Id, session);
        //                                                        if (!existInvoice)
        //                                                        {
        //                                                            #region Insert new Invoice

        //                                                            ElioUsersSubscriptionsInvoices subInvoice = new ElioUsersSubscriptionsInvoices();

        //                                                            subInvoice.UserId = user.Id;
        //                                                            subInvoice.UserSubscriptionId = sub.Id;
        //                                                            subInvoice.CustomerId = invoice.CustomerId;
        //                                                            subInvoice.InvoiceId = invoice.Id;
        //                                                            subInvoice.ChargeId = "";
        //                                                            subInvoice.SubscriptionId = subscription.Id;
        //                                                            subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
        //                                                            subInvoice.Currency = invoice.Currency;
        //                                                            subInvoice.Date = Convert.ToDateTime(invoice.Created);
        //                                                            subInvoice.Description = invoice.Description;
        //                                                            subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
        //                                                            subInvoice.InvoicePdf = invoice.InvoicePdf;
        //                                                            subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
        //                                                            subInvoice.Number = invoice.Number;
        //                                                            subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
        //                                                            subInvoice.PeriodStart = invoice.PeriodStart;
        //                                                            subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
        //                                                            subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
        //                                                            subInvoice.HasDiscount = sub.HasDiscount;
        //                                                            subInvoice.TotalAmount = (int)invoice.Total;
        //                                                            subInvoice.SubTotalAmount = (int)invoice.Subtotal;

        //                                                            if (invoice.Discount != null)
        //                                                                if (invoice.Discount.Coupon != null)
        //                                                                {
        //                                                                    Coupon stripeCoupon = invoice.Discount.Coupon;
        //                                                                    if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
        //                                                                        subInvoice.CouponId = stripeCoupon.Id;
        //                                                                    else
        //                                                                        subInvoice.CouponId = "";
        //                                                                }
        //                                                                else
        //                                                                    subInvoice.CouponId = "";
        //                                                            else
        //                                                                subInvoice.CouponId = "";

        //                                                            DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
        //                                                            invIoader.Insert(subInvoice);

        //                                                            hasInserted = true;

        //                                                            #endregion
        //                                                        }
        //                                                        else
        //                                                        {
        //                                                            #region Update Invoice

        //                                                            ElioUsersSubscriptionsInvoices subInvoice = Sql.GetInvoiceByInvoiceID(invoice.Id, session);
        //                                                            if (subInvoice != null)
        //                                                            {
        //                                                                subInvoice.UserId = user.Id;
        //                                                                subInvoice.UserSubscriptionId = sub.Id;
        //                                                                subInvoice.CustomerId = invoice.CustomerId;
        //                                                                subInvoice.InvoiceId = invoice.Id;
        //                                                                subInvoice.ChargeId = "";
        //                                                                subInvoice.SubscriptionId = subscription.Id;
        //                                                                subInvoice.IsClosed = (bool)invoice.Paid ? 1 : 0;
        //                                                                subInvoice.Currency = invoice.Currency;
        //                                                                subInvoice.Date = Convert.ToDateTime(invoice.Created);
        //                                                                subInvoice.Description = invoice.Description;
        //                                                                subInvoice.HostedInvoiceUrl = invoice.HostedInvoiceUrl;
        //                                                                subInvoice.InvoicePdf = invoice.InvoicePdf;
        //                                                                subInvoice.NextPaymentAttempt = invoice.NextPaymentAttempt != null ? invoice.NextPaymentAttempt : null;
        //                                                                subInvoice.Number = invoice.Number;
        //                                                                subInvoice.IsPaid = (bool)invoice.Paid ? 1 : 0;
        //                                                                subInvoice.PeriodStart = invoice.PeriodStart;
        //                                                                subInvoice.PeriodEnd = (invoice.PeriodEnd != null) ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.PeriodStart.AddMonths(1);   //Convert.ToDateTime(invoice.DueDate);
        //                                                                subInvoice.ReceiptNumber = (invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
        //                                                                subInvoice.HasDiscount = sub.HasDiscount;
        //                                                                subInvoice.TotalAmount = (int)invoice.Total;
        //                                                                subInvoice.SubTotalAmount = (int)invoice.Subtotal;

        //                                                                if (invoice.Discount != null)
        //                                                                    if (invoice.Discount.Coupon != null)
        //                                                                    {
        //                                                                        Coupon stripeCoupon = invoice.Discount.Coupon;
        //                                                                        if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
        //                                                                            subInvoice.CouponId = stripeCoupon.Id;
        //                                                                        else
        //                                                                            subInvoice.CouponId = "";
        //                                                                    }
        //                                                                    else
        //                                                                        subInvoice.CouponId = "";
        //                                                                else
        //                                                                    subInvoice.CouponId = "";

        //                                                                DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
        //                                                                invIoader.Update(subInvoice);
        //                                                            }

        //                                                            #endregion
        //                                                        }
        //                                                    }
        //                                                }
        //                                            }

        //                                            StripeSubscription stripeSub = Lib.Services.StripeAPI.StripeService.GetSubscription(sub.SubscriptionId);
        //                                            if (stripeSub != null && !string.IsNullOrEmpty(stripeSub.Id))
        //                                            {
        //                                                sub.Status = stripeSub.Status.ToString();
        //                                                if (stripeSub.CanceledAt != null)
        //                                                    sub.CanceledAt = Convert.ToDateTime(stripeSub.CanceledAt);

        //                                                if (stripeSub.Plan != null && stripeSub.Plan.Id != sub.PlanId)
        //                                                {
        //                                                    ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(stripeSub.Plan.Id, session);
        //                                                    if (elioPacket != null)
        //                                                        sub.PlanId = elioPacket.stripePlanId;
        //                                                    else
        //                                                        sub.PlanId = stripeSub.Plan.Id;

        //                                                    //sub.PlanId = stripeSub.StripePlan.Id;
        //                                                    sub.PlanNickname = (subscription.Items.Data[0].Plan != null && stripeSub.Plan.Nickname != null) ? stripeSub.Plan.Nickname : "";
        //                                                    sub.Amount = subscription.Items.Data[0].Plan != null ? (int)stripeSub.Plan.Amount : 0;
        //                                                }

        //                                                sub.CurrentPeriodStart = Convert.ToDateTime(stripeSub.CurrentPeriodStart);
        //                                                sub.CurrentPeriodEnd = Convert.ToDateTime(stripeSub.CurrentPeriodEnd);

        //                                                DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
        //                                                loader.Update(sub);
        //                                            }
        //                                        }

        //                                        #endregion
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                    #region Packet Status Features

        //                    ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

        //                    if ((packetFeatures != null && packetFeatures.ExpirationDate < DateTime.Now) || packetFeatures == null)
        //                    {
        //                        ElioUsersSubscriptions userSubscription = Sql.GetUserSubscription(user.Id, user.CustomerStripeId, session);
        //                        if (userSubscription != null)
        //                        {
        //                            int packId = Sql.GetPacketIdBySubscriptionID(userSubscription.SubscriptionId, session);
        //                            if (packId > 0)
        //                            {
        //                                if (packId == (int)Packets.PremiumDiscountNoTrial || packId == (int)Packets.PremiumDiscountTrial || packId == (int)Packets.PremiumtNoTrial25 || packId == (int)Packets.PremiumDiscount20 || packId == (int)Packets.Premium_No_Trial)
        //                                    packId = (int)Packets.Premium;

        //                                ElioPackets packet = Sql.GetPacketById(packId, session);
        //                                if (packet != null && packet.Id != (int)Packets.PremiumService && packet.Id != (int)Packets.ServiceFollowUp && packet.Id != (int)Packets.SelfService && packet.Id != (int)Packets.AccountManagerService && packet.Id != (int)Packets.PremiumService299)
        //                                {
        //                                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(packet.Id, session);
        //                                    if (items.Count > 0)
        //                                    {
        //                                        #region Get Packet Features Items

        //                                        int totalLeads = 0;
        //                                        int totalMessages = 0;
        //                                        int totalConnections = 0;
        //                                        int totalManagePartners = 0;
        //                                        int totalLibraryStorage = 0;

        //                                        for (int i = 0; i < items.Count; i++)
        //                                        {
        //                                            if (items[i].ItemDescription == "Leads")
        //                                            {
        //                                                totalLeads = items[i].FreeItemsNo;
        //                                            }
        //                                            else if (items[i].ItemDescription == "Messages")
        //                                            {
        //                                                totalMessages = items[i].FreeItemsNo;
        //                                            }
        //                                            else if (items[i].ItemDescription == "Connections")
        //                                            {
        //                                                totalConnections = items[i].FreeItemsNo;        // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
        //                                            }
        //                                            else if (items[i].ItemDescription == "ManagePartners")
        //                                            {
        //                                                totalManagePartners = items[i].FreeItemsNo;
        //                                            }
        //                                            else if (items[i].ItemDescription == "LibraryStorage")
        //                                            {
        //                                                totalLibraryStorage = items[i].FreeItemsNo;
        //                                            }
        //                                        }

        //                                        #endregion

        //                                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

        //                                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
        //                                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
        //                                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
        //                                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);
        //                                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, userSubscription.CurrentPeriodStart, userSubscription.CurrentPeriodEnd, session);

        //                                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

        //                                        #endregion

        //                                        #region Insert / Update Packet Status Features

        //                                        //ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

        //                                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

        //                                        if (packetFeatures == null)
        //                                        {
        //                                            packetFeatures = new ElioUserPacketStatus();

        //                                            packetFeatures.UserId = user.Id;
        //                                            packetFeatures.PackId = items[0].Id;
        //                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
        //                                            packetFeatures.AvailableLeadsCount = totalLeads;
        //                                            packetFeatures.AvailableMessagesCount = totalMessages;
        //                                            packetFeatures.AvailableConnectionsCount = totalConnections;
        //                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
        //                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
        //                                            packetFeatures.Sysdate = DateTime.Now;
        //                                            packetFeatures.LastUpdate = DateTime.Now;
        //                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);       //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
        //                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

        //                                            loader4.Insert(packetFeatures);
        //                                        }
        //                                        else
        //                                        {
        //                                            packetFeatures.PackId = items[0].Id;
        //                                            packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(packet.Id, session);
        //                                            packetFeatures.AvailableLeadsCount = totalLeads;
        //                                            packetFeatures.AvailableMessagesCount = totalMessages;
        //                                            packetFeatures.AvailableConnectionsCount = totalConnections;
        //                                            packetFeatures.AvailableManagePartnersCount = totalManagePartners;
        //                                            packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
        //                                            packetFeatures.LastUpdate = DateTime.Now;
        //                                            packetFeatures.StartingDate = Convert.ToDateTime(userSubscription.CurrentPeriodStart);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
        //                                            packetFeatures.ExpirationDate = Convert.ToDateTime(userSubscription.CurrentPeriodEnd);      //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

        //                                            loader4.Update(packetFeatures);
        //                                        }

        //                                        #endregion
        //                                    }
        //                                    else
        //                                    {
        //                                        Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
        //                                    }
        //                                }
        //                            }
        //                        }
        //                    }

        //                    #endregion
        //                }
        //            }
        //        }
        //    }

        //    return hasInserted;
        //}

        public static bool GetUserSubscriptionInvoices(string subscriptionId, int userId, string customerId, DBSession session)
        {
            bool hasInserted = false;

            StripeSubscription subscription = Lib.Services.StripeAPI.StripeService.GetSubscription(subscriptionId);
            if (subscription != null && !string.IsNullOrEmpty(subscription.Id))
            {
                bool exists = Sql.ExistCustomerSubscription(customerId, subscription.Id, session);
                if (!exists)
                {
                    ElioUsersSubscriptions sub = new ElioUsersSubscriptions();

                    string coupon = "";
                    //if (subscription.StripeDiscount != null && subscription.StripeDiscount.StripeCoupon != null && !string.IsNullOrEmpty(subscription.StripeDiscount.StripeCoupon.Id))
                    //    coupon = subscription.StripeDiscount.StripeCoupon.Id;

                    sub.UserId = userId;
                    sub.CustomerId = subscription.Customer;
                    sub.SubscriptionId = subscription.Id;
                    sub.CouponId = (coupon != "") ? coupon : "";

                    if (subscription.Plan != null)
                    {
                        ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(subscription.Plan.Id, session);
                        if (elioPacket != null)
                            sub.PlanId = elioPacket.stripePlanId;
                        else
                            sub.PlanId = subscription.Plan != null ? subscription.Plan.Id : "";
                    }
                    else
                        sub.PlanId = "";

                    sub.PlanNickname = (subscription.Plan != null && subscription.Plan.Nickname != null) ? subscription.Plan.Nickname : "";
                    sub.CreatedAt = Convert.ToDateTime(subscription.Start);
                    sub.CurrentPeriodStart = Convert.ToDateTime(subscription.CurrentPeriodStart);
                    sub.CurrentPeriodEnd = Convert.ToDateTime(subscription.CurrentPeriodEnd);
                    sub.CanceledAt = (subscription.CanceledAt != null) ? subscription.CanceledAt : null;
                    sub.HasDiscount = sub.CouponId != "" ? 1 : 0;
                    sub.Status = subscription.Status.ToString();
                    sub.TrialPeriodStart = (subscription.TrialStart != null) ? subscription.TrialStart : null;
                    sub.TrialPeriodEnd = subscription.TrialEnd != null ? subscription.TrialEnd : null;
                    sub.Amount = (subscription.Plan != null) ? (int)subscription.Plan.Amount : 0;

                    DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                    loader.Insert(sub);

                    hasInserted = true;

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

                                    subInvoice.UserId = userId;
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
                                    subInvoice.PeriodStart = invoice.PeriodStart;
                                    subInvoice.PeriodEnd = invoice.PeriodEnd;     //invoice.PeriodEnd != null ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.DueDate != null ? Convert.ToDateTime(invoice.DueDate) : subInvoice.PeriodStart.AddMonths(1);
                                    subInvoice.ReceiptNumber = invoice.Charge;  //(invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                    subInvoice.HasDiscount = sub.HasDiscount;
                                    subInvoice.TotalAmount = invoice.Total;
                                    subInvoice.SubTotalAmount = invoice.Subtotal;

                                    if (invoice.Discount != null)
                                        if (invoice.Discount.Coupon != null)
                                        {
                                            StripeCoupon stripeCoupon = invoice.Discount.Coupon;
                                            if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                subInvoice.CouponId = stripeCoupon.Id;
                                            else
                                                subInvoice.CouponId = "";
                                        }
                                        else
                                            subInvoice.CouponId = "";
                                    else
                                        subInvoice.CouponId = "";

                                    DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                    invIoader.Insert(subInvoice);

                                    hasInserted = true;
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

                                        subInvoice.UserId = userId;
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
                                        subInvoice.PeriodStart = invoice.PeriodStart;
                                        subInvoice.PeriodEnd = invoice.PeriodEnd;         //(invoice.PeriodEnd != null) ? Convert.ToDateTime(invoice.PeriodEnd) : invoice.DueDate != null ? Convert.ToDateTime(invoice.DueDate) : subInvoice.PeriodStart.AddMonths(1);
                                        subInvoice.ReceiptNumber = invoice.Charge;  //(invoice.ReceiptNumber != null) ? invoice.ReceiptNumber : "";
                                        subInvoice.HasDiscount = sub.HasDiscount;
                                        subInvoice.TotalAmount = invoice.Total;
                                        subInvoice.SubTotalAmount = invoice.Subtotal;

                                        if (invoice.Discount != null)
                                            if (invoice.Discount.Coupon != null)
                                            {
                                                StripeCoupon stripeCoupon = invoice.Discount.Coupon;
                                                if (stripeCoupon != null && !string.IsNullOrEmpty(stripeCoupon.Id))
                                                    subInvoice.CouponId = stripeCoupon.Id;
                                                else
                                                    subInvoice.CouponId = "";
                                            }
                                            else
                                                subInvoice.CouponId = "";
                                        else
                                            subInvoice.CouponId = "";

                                        DataLoader<ElioUsersSubscriptionsInvoices> invIoader = new DataLoader<ElioUsersSubscriptionsInvoices>(session);
                                        invIoader.Insert(subInvoice);

                                        hasInserted = true;
                                    }
                                }
                            }
                        }

                        StripeSubscription stripeSub = Lib.Services.StripeAPI.StripeService.GetSubscription(sub.SubscriptionId);
                        if (stripeSub != null && !string.IsNullOrEmpty(stripeSub.Id))
                        {
                            sub.Status = stripeSub.Status.ToString();
                            if (stripeSub.CanceledAt != null)
                                sub.CanceledAt = Convert.ToDateTime(stripeSub.CanceledAt);

                            if (stripeSub.Plan != null && stripeSub.Plan.Id != sub.PlanId)
                            {
                                ElioPackets elioPacket = Sql.GetPacketByStripePlanOldCode(stripeSub.Plan.Id, session);
                                if (elioPacket != null)
                                    sub.PlanId = elioPacket.stripePlanId;
                                else
                                    sub.PlanId = stripeSub.Plan.Id;

                                //sub.PlanId = stripeSub.StripePlan.Id;
                                sub.PlanNickname = (subscription.Plan != null && stripeSub.Plan.Nickname != null) ? stripeSub.Plan.Nickname : "";
                                sub.Amount = subscription.Plan != null ? (int)stripeSub.Plan.Amount : 0;
                            }

                            sub.CurrentPeriodStart = Convert.ToDateTime(stripeSub.CurrentPeriodStart);
                            sub.CurrentPeriodEnd = Convert.ToDateTime(stripeSub.CurrentPeriodEnd);

                            DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                            loader.Update(sub);
                        }
                    }
                }
            }

            return hasInserted;
        }

        public static void Charges()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //StripeConfiguration.SetApiKey("sk_test_OIySeuUvaObscVisQhrHkhix");

                var client = new RestClient(" https://api-tls12.stripe.com");

                var request = new RestRequest("v1/charges", Method.POST);

                request.AddHeader("Authorization", "Bearer sk_test_OIySeuUvaObscVisQhrHkhix");

                //IRestResponse response = client.Execute(request);

                //var tokenCard = JsonConvert.DeserializeObject<JToken>(response.Content);
            }
            catch (ServiceStack.Stripe.Types.StripeException e)
            {
                switch (e.Message.ToString())
                {
                    case "card_error":
                        //Console.WriteLine("   Code: " + e.StripeError.Code);
                        //Console.WriteLine("Message: " + e.StripeError.Message);
                        break;
                    case "api_connection_error":
                        break;
                    case "api_error":
                        break;
                    case "authentication_error":
                        break;
                    case "invalid_request_error":
                        break;
                    case "rate_limit_error":
                        break;
                    case "validation_error":
                        break;
                    default:
                        // Unknown Error Type
                        break;
                }
            }
        }

        public static void CreateTokes(string token, string cardNumber, int expMonth, int expYear, int cvc)
        {
            try
            {
                var client = new RestClient("https://api.stripe.com");

                var request = new RestRequest("v1/tokens", Method.POST);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                request.AddParameter("card[number]", cardNumber);
                request.AddParameter("card[exp_month]", expMonth);
                request.AddParameter("card[exp_year]", expYear);
                request.AddParameter("card[cvc]", cvc);

                IRestResponse response = client.Execute(request);

                JToken tokenCard = JsonConvert.DeserializeObject<JToken>(response.Content);

            }
            catch (Exception ex)
            {
                Logger.DetailedError("Class:Stripe.cs --> Method:CreateTokes -->", ex.ToString());
            }
        }

        public static bool RetrieveCustomer(string customer)
        {
            bool success = false;

            try
            {
                var client = new RestClient("https://api.stripe.com/");
                //emailAddress = "uwe.richter@stp-online.de";
                var request = new RestRequest("v1/customers/", Method.GET);
                request.AddParameter("ID", customer);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    string error = "";
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for customer:" + customer;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for customer:" + customer;
                    }
                    else
                    {
                        error = "Something went wrong for customer:" + customer;
                    }

                    Logger.DetailedError("Class:Stripe.cs --> Method:RetrieveCustomer -->", error.ToString());

                    return false;
                }

                var customerStripeDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                //JToken person = customerStripeDictionary["person"];
                //JToken company = customerStripeDictionary["company"];

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                return true;

                #region to delete

                //if (combinedPersonCompanyResponse != null)
                //{
                //    if (session.Connection.State == System.Data.ConnectionState.Closed)
                //        session.OpenConnection();

                //    DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);

                //    if (person.HasValues)
                //    {
                //        #region person response

                //        string personId = combinedPersonCompanyResponse["person"]["id"].ToString();
                //        string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                //        string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                //        string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                //        string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                //        string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                //        string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                //        string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                //        string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                //        string city = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                //        string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                //        string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                //        string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                //        string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                //        string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                //        string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                //        string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                //        string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                //        string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                //        string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                //        string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                //        string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                //        string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                //        string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                //        string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                //        string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                //        string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                //        string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                //        string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                //        string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                //        string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                //        string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                //        string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                //        string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                //        string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                //        string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                //        string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                //        string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                //        string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                //        string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                //        string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                //        string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                //        string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                //        string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                //        string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                //        string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                //        string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                //        string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                //        string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();
                //        if (gravatar != "")
                //        {
                //            string gravatarUrls = combinedPersonCompanyResponse["person"]["gravatar"]["urls"].ToString();
                //            if (gravatarUrls != "")
                //            {
                //                string gravatarUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["value"].ToString();
                //                string gravatarUrlsTitle = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["title"].ToString();
                //                string personAvatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                //                if (personAvatar != "")
                //                {
                //                    string gravatarAvatarsUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["url"].ToString();
                //                    string gravatarAvatarsUrlsType = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["type"].ToString();
                //                }
                //            }
                //        }

                //        string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                //        string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                //        string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                //        #endregion

                //        #region Elio Users Person

                //        elioPerson = new ElioUsersPerson();

                //        elioPerson.ClearbitPersonId = personId;
                //        elioPerson.UserId = user.Id;
                //        elioPerson.GivenName = givenName;
                //        elioPerson.FamilyName = familyName;
                //        elioPerson.Email = personEmail;
                //        elioPerson.Phone = "";
                //        elioPerson.Location = location;
                //        if (!string.IsNullOrEmpty(city))
                //            elioPerson.Location += ", " + city;
                //        if (!string.IsNullOrEmpty(state))
                //            elioPerson.Location += ", " + state;
                //        elioPerson.TimeZone = timeZone;
                //        elioPerson.Bio = bio;
                //        elioPerson.Avatar = avatar;
                //        elioPerson.Title = title;
                //        elioPerson.Role = role;
                //        elioPerson.Seniority = seniority;
                //        elioPerson.TwitterHandle = twitterHandle;
                //        elioPerson.LinkedinHandle = linkedinHandle;
                //        elioPerson.AboutMeHandle = linkedinAboutmeHandle;
                //        elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                //        elioPerson.LastUpdate = DateTime.Now;
                //        elioPerson.IsPublic = 1;
                //        elioPerson.IsActive = 1;
                //        elioPerson.IsClaimed = 0;

                //        //bool existPerson = ClearbitSql.ExistsClearbitPerson(user.Id, personId, session);
                //        ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);
                //        if (elioPersonInDB == null)
                //        {
                //            personloader.Insert(elioPerson);
                //        }
                //        else
                //        {
                //            elioPerson.Id = elioPersonInDB.Id;
                //            elioPerson.LastUpdate = DateTime.Now;
                //            personloader.Update(elioPerson);
                //        }

                //        #endregion

                //        #region Elio Users Update credentials

                //        user.Address = elioPerson.Location;
                //        user.PersonalImage = elioPerson.Avatar;
                //        //user.TwitterUrl = elioPerson.TwitterHandle;
                //        //user.LinkedInUrl = elioPerson.LinkedinHandle;
                //        user.Position = elioPerson.Title + "," + elioPerson.Seniority;
                //        user.LastName = elioPerson.FamilyName;
                //        user.FirstName = elioPerson.GivenName;
                //        user.Country = (string.IsNullOrEmpty(user.Country)) ? country : user.Country;

                //        #endregion
                //    }
                //    else
                //    {
                //        #region Elio User to Clearbit Person

                //        string personId = Guid.NewGuid().ToString();

                //        elioPerson = new ElioUsersPerson();

                //        elioPerson.ClearbitPersonId = personId;
                //        elioPerson.UserId = user.Id;
                //        elioPerson.GivenName = user.FirstName;
                //        elioPerson.FamilyName = user.LastName;
                //        elioPerson.Email = (!string.IsNullOrEmpty(user.Email)) ? user.Email : emailAddress;
                //        elioPerson.Phone = user.Phone;
                //        elioPerson.Location = user.Address;
                //        elioPerson.TimeZone = "";
                //        elioPerson.Bio = "";
                //        elioPerson.Avatar = "";
                //        elioPerson.Title = user.Position;
                //        elioPerson.Role = "";
                //        elioPerson.Seniority = "";
                //        elioPerson.TwitterHandle = "";
                //        elioPerson.LinkedinHandle = user.LinkedInUrl;
                //        elioPerson.AboutMeHandle = "";
                //        elioPerson.DateInserted = user.SysDate;
                //        elioPerson.LastUpdate = DateTime.Now;
                //        elioPerson.IsPublic = 1;
                //        elioPerson.IsActive = 1;
                //        elioPerson.IsClaimed = 0;

                //        ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);
                //        if (elioPersonInDB == null)
                //        {
                //            personloader.Insert(elioPerson);
                //        }
                //        else
                //        {
                //            elioPerson.Id = elioPersonInDB.Id;
                //            elioPerson.LastUpdate = DateTime.Now;
                //            personloader.Update(elioPerson);
                //        }

                //        #endregion
                //    }

                //    if (company.HasValues)
                //    {
                //        string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                //        string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                //        string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();

                //        if (elioPerson != null)
                //        {
                //            #region company response

                //            string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                //            string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                //            string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                //            string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                //            string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                //            string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                //            string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                //            string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                //            string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                //            string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                //            string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();
                //            string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                //            string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                //            string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                //            string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                //            string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                //            string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                //            string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                //            string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                //            string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                //            string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                //            string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                //            string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                //            string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                //            string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                //            string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                //            string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                //            string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                //            string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                //            string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                //            string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                //            string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                //            string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                //            string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                //            string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                //            string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                //            string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                //            string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                //            string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                //            string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                //            string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                //            string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                //            string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                //            string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                //            string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                //            string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                //            string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                //            string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                //            string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                //            string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                //            string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                //            string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                //            string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                //            string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                //            string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                //            string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                //            string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                //            #endregion

                //            #region Elio Users Company

                //            elioCompany = new ElioUsersPersonCompanies();

                //            elioCompany.ClearbitCompanyId = companyId;
                //            elioCompany.ElioPersonId = elioPerson.Id;
                //            elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                //            elioCompany.UserId = user.Id;
                //            elioCompany.Name = companyName;
                //            elioCompany.Domain = companyDomain;
                //            elioCompany.Sector = companySector;
                //            elioCompany.IndustryGroup = companIndustryGroup;
                //            elioCompany.Industry = companyIndustry;
                //            elioCompany.SubIndustry = companySubIndustry;
                //            elioCompany.Description = companyDescription;
                //            elioCompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                //            elioCompany.Location = companyLocation;
                //            //elioCompany.FundAmount = companyfu
                //            elioCompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                //            elioCompany.EmployeesRange = companyEmployeesRange;
                //            elioCompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                //            elioCompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                //            elioCompany.FacebookHandle = companyFacebookHandle;
                //            elioCompany.FacebookLikes = (companyFacebookLikes != "") ? Convert.ToInt32(companyFacebookLikes) : 0;
                //            elioCompany.LinkedinHandle = companyLinkedinHandle;
                //            elioCompany.TwitterHandle = companyTwitterHandle;
                //            elioCompany.TwitterId = companyTwitterId;
                //            elioCompany.TwitterBio = companyTwitterBio;
                //            elioCompany.TwitterAvatar = companyTwtitterAvatar;
                //            elioCompany.TwitterFollowers = (companyTwitterFollowers != "") ? Convert.ToInt32(companyTwitterFollowers) : 0;
                //            elioCompany.TwitterFollowing = (companyTwitterFollowing != "") ? Convert.ToInt32(companyTwitterFollowing) : 0;
                //            elioCompany.TwitterSite = companyTwitterSite;
                //            elioCompany.TwitterLocation = companyTwitterLocation;
                //            elioCompany.CrunchbaseHandle = companyCrunchbaseHandle;
                //            elioCompany.CompanyPhone = companyPhone;
                //            elioCompany.Logo = companyLogo;
                //            elioCompany.Type = companyType;
                //            elioCompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                //            elioCompany.LastUpdate = DateTime.Now;
                //            elioCompany.IsPublic = 1;
                //            elioCompany.IsActive = 1;

                //            DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);

                //            ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(user.Id, companyId, session);

                //            if (elioCompanyInDB == null)
                //            {
                //                companyLoader.Insert(elioCompany);
                //            }
                //            else
                //            {
                //                elioCompany.Id = elioCompanyInDB.Id;
                //                elioCompany.LastUpdate = DateTime.Now;
                //                companyLoader.Update(elioCompany);
                //            }

                //            #endregion

                //            #region Elio Users Update credentials

                //            user.WebSite = (string.IsNullOrEmpty(user.WebSite)) ? (!elioCompany.Domain.StartsWith("www") && !elioCompany.Domain.StartsWith("http")) ? "www." + elioCompany.Domain : elioCompany.Domain : user.WebSite;
                //            user.Country = (string.IsNullOrEmpty(user.Country)) ? companyCountry : user.Country;
                //            user.CompanyLogo = (string.IsNullOrEmpty(user.CompanyLogo)) ? elioCompany.Logo : user.CompanyLogo;
                //            user.TwitterUrl = elioCompany.TwitterHandle;
                //            user.Description = elioCompany.Description;

                //            #endregion
                //        }
                //        else
                //        {
                //            throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                //        }

                //        if (elioCompany != null)
                //        {
                //            #region Elio Company Phones

                //            foreach (string phone in companySitePhoneNumbers)
                //            {
                //                if (phone != "")
                //                {
                //                    string number = Regex.Replace(phone, @"\\r\\n", "");
                //                    number = number.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                //                    if (number != "")
                //                    {
                //                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                //                        companyPhoneNumber.ElioPersonCompanyId = elioCompany.Id;
                //                        companyPhoneNumber.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                //                        companyPhoneNumber.UserId = user.Id;
                //                        companyPhoneNumber.PhoneNumber = number;
                //                        companyPhoneNumber.Sysdate = DateTime.Now;
                //                        companyPhoneNumber.LastUpdate = DateTime.Now;

                //                        DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);

                //                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumberInDB = ClearbitSql.GetPersonCompanyPhoneByPhone(user.Id, elioCompany.ClearbitCompanyId, number, session);

                //                        if (companyPhoneNumberInDB == null)
                //                        {
                //                            phoneLoader.Insert(companyPhoneNumber);
                //                        }
                //                        else
                //                        {
                //                            companyPhoneNumber.Id = companyPhoneNumberInDB.Id;
                //                            companyPhoneNumber.LastUpdate = DateTime.Now;
                //                            phoneLoader.Update(companyPhoneNumber);
                //                        }
                //                    }
                //                }
                //            }

                //            #endregion

                //            #region Elio Company Tags

                //            foreach (string companyTag in companyTags)
                //            {
                //                if (companyTag != "")
                //                {
                //                    string tagName = Regex.Replace(companyTag, @"\\r\\n", "");
                //                    tagName = tagName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                //                    if (tagName != "")
                //                    {
                //                        ElioUsersPersonCompanyTags tag = new ElioUsersPersonCompanyTags();

                //                        tag.ElioPersonCompanyId = elioCompany.Id;
                //                        tag.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                //                        tag.UserId = user.Id;
                //                        tag.TagName = tagName;
                //                        tag.Sysdate = DateTime.Now;
                //                        tag.LastUpdate = DateTime.Now;
                //                        tag.IsPublic = 1;
                //                        tag.IsActive = 1;

                //                        DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);

                //                        ElioUsersPersonCompanyTags tagInDB = ClearbitSql.GetPersonCompanyTagsByTagName(user.Id, elioCompany.ClearbitCompanyId, tagName, session);

                //                        if (tagInDB == null)
                //                        {
                //                            tagLoader.Insert(tag);
                //                        }
                //                        else
                //                        {
                //                            tag.Id = tagInDB.Id;
                //                            tag.LastUpdate = DateTime.Now;
                //                            tagLoader.Update(tag);
                //                        }
                //                    }
                //                }
                //            }

                //            #endregion
                //        }
                //        else
                //        {
                //            throw new Exception("ElioCompany did not find by id for user with email: " + emailAddress + " and for phones or tags");
                //        }
                //    }

                //    success = true;

                //    if (success)
                //    {
                //        #region Elio Users Update credentials

                //        user.IsPublic = (int)AccountPublicStatus.IsPublic;
                //        user.AccountStatus = (int)AccountStatus.Completed;
                //        user.LastUpdated = DateTime.Now;
                //        user = GlobalDBMethods.UpDateUser(user, session);

                //        #endregion
                //    }
                //}
                //else
                //{
                //    success = false;
                //}

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error", ex.Message.ToString(), ex.StackTrace.ToString());
                success = false;
            }

            return success;
        }

        //public string CreateToken(string cardNumber, int cardExpMonth, int cardExpYear, string cardCVC)
        //{
        //    HttpClient client = new HttpClient();
        //    //await client.PostAsync("https://myname.azurewebsites.net/api/payment",
        //    //                       new StringContent(JsonConvert.SerializeObject(new PaymentModel()
        //    //                       {
        //    //                           Amount = 80.56M,
        //    //                           Token = "whatYouReceivedFromStripe"
        //    //                       }),
        //    //                                         Encoding.UTF8,
        //    //                                         "application/json"));

        //    StripeConfiguration.SetApiKey("pk_test_xxxxxxxxxxxxxxxxx");

        //    var tokenOptions = new StripeTokenCreateOptions()
        //    {
        //        Card = new StripeCreditCardOptions()
        //        {
        //            Number = cardNumber,
        //            ExpirationYear = cardExpYear,
        //            ExpirationMonth = cardExpMonth,
        //            Cvc = cardCVC
        //        }
        //    };

        //    var tokenService = new StripeTokenService();
        //    StripeToken stripeToken = tokenService.Create(tokenOptions);

        //    return stripeToken.Id; // This is the token
        //}
    }
}