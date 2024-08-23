using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.PaymentGateway;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using System.Configuration;
using WdS.ElioPlus.Lib.StripePayment;
using Stripe;
using ServiceStack.Stripe.Types;

namespace WdS.ElioPlus.Lib.DBQueries
{
    public class PaymentLib
    {
        public static bool ShowPremiumButton(ElioUsers user, ref string btnTxt, DBSession session)
        {
            bool showBtn = false;
            btnTxt = "Go Premium";

            if (ConfigurationManager.AppSettings["ActivatePayment"].ToString() == "true")
            {
                if (user != null)
                {
                    if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                    {
                        showBtn = true;
                        btnTxt = "Go Premium";
                    }
                    else
                    {
                        ElioBillingUserOrders expiredOrder = Sql.GetUserOrderByStatus(user.Id, Convert.ToInt32(OrderStatus.Expired), session);
                        if (expiredOrder != null)
                        {
                            showBtn = true;
                            btnTxt = "Reniew Order";
                        }
                    }
                }
                else
                {
                    showBtn = true;
                }
            }

            return showBtn;
        }

        public static PaypalCredentials GetPayPalCredentialsById(int paypalCredentialsId, DBSession session)
        {
            DataLoader<PaypalCredentials> pLoader = new DataLoader<PaypalCredentials>(session);
            return pLoader.LoadSingle("SELECT * FROM paypal_credentials WHERE id =@id"
                                      , DatabaseHelper.CreateIntParameter("@id", paypalCredentialsId));
        }

        public static PaypalFeedback InsertPayPalFeedBack(int userId, string ip, decimal totalCost, decimal totalFpaValue, string pSource, string step, DBSession session)
        {
            PaypalFeedback paypalFeedback = new PaypalFeedback();

            paypalFeedback.UserId = userId;
            paypalFeedback.PaypalAmount = totalCost;
            paypalFeedback.TransactionStep = step;
            paypalFeedback.TransactionStepDescription = "Initialization";
            paypalFeedback.Sysdate = DateTime.Now;
            paypalFeedback.LastUpdate = DateTime.Now;
            paypalFeedback.PaypalOrderTime = DateTime.Now.ToString();
            paypalFeedback.PaypalTaxAmount = totalFpaValue;
            paypalFeedback.ApplicationSource = pSource.ToLower();
            paypalFeedback.Ip = ip;

            DataLoader<PaypalFeedback> loader = new DataLoader<PaypalFeedback>(session);

            loader.Insert(paypalFeedback);

            return paypalFeedback;
        }

        public static void UpdatePayPalFeedbackForStep1(PaypalFeedback paypalFeedback, PayPalPostSubmitter.SetExpressCheckoutResults setExpressCheckoutResults, HttpServerUtility server, string step, DBSession session)
        {
            DataLoader<PaypalFeedback> pFLoader = new DataLoader<PaypalFeedback>(session);
            if (setExpressCheckoutResults.ack.ToUpper().Equals("SUCCESS"))
                paypalFeedback.PaypalStatus = "STEP SUCCESS";
            else
            {
                paypalFeedback.PaypalStatus = "FAILED";
                paypalFeedback.PaypalReasonCode = server.UrlDecode(setExpressCheckoutResults.response);
            }

            paypalFeedback.PaypalToken = setExpressCheckoutResults.token;
            paypalFeedback.TransactionStep = step;
            paypalFeedback.LastUpdate = DateTime.Now;
            paypalFeedback.TransactionStepDescription = "Setting express checkout";

            pFLoader.Update(paypalFeedback);
        }

        public static void UpdatePayPalFeedbackForStep2(int isPaypalAuthenticated, string step, HttpServerUtility server, PayPalPostSubmitter.ExpressCheckoutPostResults expressCheckoutPostResults, PaypalFeedback paypalFeedback, DBSession session)
        {
            paypalFeedback.TransactionStep = step;
            paypalFeedback.TransactionStepDescription = "Getting express checkout details";
            if (expressCheckoutPostResults.ack.ToUpper().Equals("SUCCESS"))
            {
                paypalFeedback.PaypalFirstname = expressCheckoutPostResults.firstName;
                paypalFeedback.PaypalLastname = expressCheckoutPostResults.lastName;
                paypalFeedback.PaypalPayeremail = expressCheckoutPostResults.email;
                paypalFeedback.PaypalPayerid = expressCheckoutPostResults.payerId;
                paypalFeedback.PaypalPayerstatus = expressCheckoutPostResults.payerStatus;
                paypalFeedback.IsPaypalAuthenticated = isPaypalAuthenticated;
                paypalFeedback.PaypalStatus = "STEP SUCCESS";
            }
            else
            {
                paypalFeedback.PaypalStatus = "FAILED";
                paypalFeedback.PaypalReasonCode = server.UrlDecode(expressCheckoutPostResults.response);
            }

            DataLoader<PaypalFeedback> loader = new DataLoader<PaypalFeedback>(session);

            paypalFeedback.LastUpdate = DateTime.Now;

            loader.Update(paypalFeedback);
        }

        public static void UpdatePayPalFeedbackForStep3(HttpServerUtility server, string step, PaypalFeedback paypalFeedback, PayPalPostSubmitter.DoExpressCheckoutPaymentResults doExpressCheckoutResults, DBSession session)
        {
            paypalFeedback.TransactionStep = step;
            paypalFeedback.TransactionStepDescription = "Doing express checkout payment";
            if (doExpressCheckoutResults.ack.ToUpper().Equals("SUCCESS"))
            {
                paypalFeedback.PaypalTransactionType = doExpressCheckoutResults.transactionType;
                paypalFeedback.PaypalTransactionId = doExpressCheckoutResults.transactionId;
                paypalFeedback.PaypalPaymentType = doExpressCheckoutResults.paymentType;
                paypalFeedback.PaypalOrderTime = doExpressCheckoutResults.orderTime;
                paypalFeedback.PaypalCurrencyCode = doExpressCheckoutResults.currencyCode;
                paypalFeedback.PaypalStatusReason = doExpressCheckoutResults.pendingReason;
                paypalFeedback.PaypalReasonCode = doExpressCheckoutResults.reasonCode;
                paypalFeedback.PaypalFeeAmount = doExpressCheckoutResults.feeAmt;
                paypalFeedback.PaypalStatus = "COMPLETED";
            }
            else
            {
                paypalFeedback.PaypalStatus = "FAILED";
                paypalFeedback.PaypalReasonCode = server.UrlDecode(doExpressCheckoutResults.response);
            }

            paypalFeedback.LastUpdate = DateTime.Now;

            DataLoader<PaypalFeedback> loader = new DataLoader<PaypalFeedback>(session);

            loader.Update(paypalFeedback);
        }

        public static PaypalFeedback GetPayPalFeedback(string token, DBSession session)
        {
            DataLoader<PaypalFeedback> loader = new DataLoader<PaypalFeedback>(session);
            return loader.LoadSingle("SELECT * FROM paypal_feedback WHERE paypal_token=@paypal_token", DatabaseHelper.CreateStringParameter("@paypal_token", token));
        }

        public static ElioBillingUserOrders GetUserOrderByStatus(int userId, int status, DBSession session)
        {
            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            return loader.LoadSingle("select * from Elio_billing_user_orders where user_id=@user_id and order_status=@order_status"
                                        , DatabaseHelper.CreateIntParameter("@user_id", userId)
                                        , DatabaseHelper.CreateIntParameter("@order_status", status));
        }

        //public static void CloseUserOrder(ElioUsers user, string chargeId, DBSession session)
        //{
        //    ElioBillingUserOrders order = PaymentLib.GetUserOrderByStatus(user.Id, Convert.ToInt32(OrderStatus.Completed), session);
        //    if (order != null)
        //    {
        //        #region Order

        //        order.OrderStatus = Convert.ToInt32(OrderStatus.Closed);
        //        order.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
        //        order.LastUpdate = DateTime.Now;
        //        order.CurrentPeriodEnd = order.LastUpdate.AddMonths(1);

        //        DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
        //        loader.Update(order);

        //        #endregion

        //        int purchaseType = (user.BillingType == Convert.ToInt32(BillingType.Freemium)) ? Convert.ToInt32(OrderType.NewOrder) : Convert.ToInt32(OrderType.Refunded);

        //        ElioBillingPurchases purchase = Sql.GetPurchaseByOrderIdAndPaidStatusAndType(order.Id, Convert.ToInt32(PurchaseStatus.NotPaid), purchaseType, session);

        //        if (purchase != null)
        //        {
        //            #region Purchase

        //            purchase.IsPaid = Convert.ToInt32(OrderStatus.Paid);
        //            purchase.LastUpdate = DateTime.Now;

        //            DataLoader<ElioBillingPurchases> loader1 = new DataLoader<ElioBillingPurchases>(session);
        //            loader1.Update(purchase);

        //            #endregion

        //            #region Payment

        //            ElioBillingPayments payment = new ElioBillingPayments();

        //            payment.OrderId = order.Id;
        //            payment.PaymentWay = PaymentType.Stripe.ToString();
        //            payment.Sysdate = DateTime.Now;
        //            payment.LastUpdate = DateTime.Now;
        //            payment.TotalAmount = purchase.TotalCost;
        //            payment.UserId = order.UserId;
        //            payment.Comments = string.Empty;
        //            payment.AdminName = string.Empty;
        //            payment.AdminId = 0;
        //            payment.PaymentChargeId = chargeId;

        //            DataLoader<ElioBillingPayments> loader2 = new DataLoader<ElioBillingPayments>(session);
        //            loader2.Insert(payment);

        //            #endregion

        //            #region Purchase - Payment

        //            ElioBillingPaymentsPurchases paymentPurchase = new ElioBillingPaymentsPurchases();

        //            paymentPurchase.PurchaseId = purchase.Id;
        //            paymentPurchase.PaymentId = payment.Id;
        //            paymentPurchase.Sysdate = DateTime.Now;
        //            paymentPurchase.LastUpdate = DateTime.Now;
        //            paymentPurchase.TotalAmount = payment.TotalAmount;
        //            paymentPurchase.UserId = order.UserId;

        //            DataLoader<ElioBillingPaymentsPurchases> loader3 = new DataLoader<ElioBillingPaymentsPurchases>(session);
        //            loader3.Insert(paymentPurchase);

        //            #endregion
        //        }

        //        #region Get Packet Features Items

        //        List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Premium), session);
        //        if (items.Count > 0)
        //        {
        //            int totalLeads = 0;
        //            int totalMessages = 0;
        //            int totalConnections = 0;

        //            for (int i = 0; i < items.Count; i++)
        //            {
        //                if (items[i].ItemDescription == "Leads")
        //                {
        //                    totalLeads = items[i].FreeItemsNo;
        //                }
        //                else if (items[i].ItemDescription == "Messages")
        //                {
        //                    totalMessages = items[i].FreeItemsNo;
        //                }
        //                else if (items[i].ItemDescription == "Connections")
        //                {
        //                    totalConnections = items[i].FreeItemsNo;
        //                }
        //            }

        //            #region User Packet Status Features

        //            ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

        //            DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

        //            if (packetFeatures == null)
        //            {
        //                packetFeatures = new ElioUserPacketStatus();

        //                packetFeatures.UserId = user.Id;
        //                packetFeatures.PackId = items[0].Id;
        //                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                packetFeatures.AvailableLeadsCount = totalLeads;
        //                packetFeatures.AvailableMessagesCount = totalMessages;
        //                packetFeatures.AvailableConnectionsCount = totalConnections;
        //                packetFeatures.Sysdate = DateTime.Now;
        //                packetFeatures.LastUpdate = DateTime.Now;
        //                packetFeatures.StartingDate = DateTime.Now;
        //                packetFeatures.ExpirationDate = packetFeatures.Sysdate.AddMonths(1);

        //                loader4.Insert(packetFeatures);
        //            }
        //            else
        //            {
        //                packetFeatures.PackId = items[0].Id;
        //                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                packetFeatures.AvailableLeadsCount = totalLeads;
        //                packetFeatures.AvailableMessagesCount = totalMessages;
        //                packetFeatures.AvailableConnectionsCount = totalConnections;
        //                packetFeatures.LastUpdate = DateTime.Now;
        //                packetFeatures.StartingDate = DateTime.Now;
        //                packetFeatures.ExpirationDate = packetFeatures.LastUpdate.AddMonths(1);

        //                loader4.Update(packetFeatures);
        //            }

        //            #endregion
        //        }
        //        else
        //        {
        //            //to do
        //            Logger.DetailedError("User :{} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString());
        //        }

        //        #endregion
        //    }
        //}

        //public static bool DeletePendingOrder(ElioUsers user, DBSession session)
        //{
        //    bool deletedSuccessfully = false;

        //    ElioBillingUserOrders pendingOrder = Sql.GetUserPendingOrderToBeDeleted(user.Id, (Convert.ToInt32(OrderStatus.Closed) + "," + Convert.ToInt32(OrderStatus.Completed)).ToString(), Convert.ToInt32(OrderStatus.NotPaid), Convert.ToInt32(OrderStatus.NotReadyToUse), session);
        //    if (pendingOrder != null)
        //    {
        //        try
        //        {
        //            session.BeginTransaction();

        //            ElioBillingPurchases purchase = Sql.GetPurchaseByOrderIdAndPaidStatusAndType(pendingOrder.Id, Convert.ToInt32(PurchaseStatus.NotPaid), Convert.ToInt32(OrderType.NewOrder), session);
        //            if (purchase != null)
        //            {
        //                DataLoader<ElioBillingPurchases> loader2 = new DataLoader<ElioBillingPurchases>(session);
        //                loader2.Delete(purchase);
        //            }

        //            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
        //            loader.Delete(pendingOrder);

        //            deletedSuccessfully = true;

        //            session.CommitTransaction();
        //        }
        //        catch (Exception ex)
        //        {
        //            session.RollBackTransaction();
        //            throw ex;
        //        }
        //    }

        //    return deletedSuccessfully;
        //}

        public static StripeTestingCards GetStripeCard(bool isUsed, DBSession session)
        {
            DataLoader<StripeTestingCards> loader = new DataLoader<StripeTestingCards>(session);
            return loader.LoadSingle("select top 1 * from Stripe_testing_cards where is_used=@is_used", DatabaseHelper.CreateBoolParameter("@is_used", isUsed));
        }

        public static void AttachUserToService(ElioUsers user, string customerStripeServiceId, string customerStripeServiceSubscriptionEmail, DateTime? startDate, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, string orderMode, Xamarin.Payments.Stripe.StripeCard cardInfo, DBSession session)
        {
            ElioServicesFeatures serviceFeatures = Sql.GetServiceFeaturesByServiceId(1, session);

            if (serviceFeatures != null)
            {
                #region New Order

                ElioBillingUserOrders order = new ElioBillingUserOrders();

                order.UserId = user.Id;
                order.Sysdate = startDate;
                order.LastUpdate = DateTime.Now;
                order.PackId = Convert.ToInt32(Packets.PremiumService);
                order.CurrentPeriodStart = currentPeriodStart;
                order.CurrentPeriodEnd = Convert.ToDateTime(order.CurrentPeriodStart).AddMonths(serviceFeatures.MinimumCommitment);
                order.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
                order.OrderStatus = Convert.ToInt32(OrderStatus.Active);
                order.CostWithNoVat = Sql.GetPacketTotalCostWithNoVat(Convert.ToInt32(Packets.PremiumService), session);
                order.CostWithVat = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.PremiumService), session);
                order.CostVat = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.PremiumService), session);
                order.OrderPaymentWay = PaymentType.Stripe.ToString();
                order.OrderType = Convert.ToInt32(OrderType.ServiceNewOrder);
                order.Mode = orderMode;
                order.IsPaid = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToInt32(OrderStatus.Paid) : Convert.ToInt32(OrderStatus.NotPaid);

                bool isAdmin = false;
                try
                {
                    isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
                }
                catch (Exception ex)
                {
                    isAdmin = false;
                    Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
                order.AdminId = (isAdmin) ? user.Id : 0;

                DataLoader<ElioBillingUserOrders> orderLoader = new DataLoader<ElioBillingUserOrders>(session);
                orderLoader.Insert(order);

                #endregion

                ElioUsersStripeId userService = Sql.GetUserStripeServiceByUserId(user.Id, session);
                if (userService == null)
                {
                    #region Insert Customer Stripe Id

                    userService = new ElioUsersStripeId();

                    userService.UserId = user.Id;
                    userService.StripePacketCustomerId = user.CustomerStripeId;
                    userService.StripeServiceCustomerId = customerStripeServiceId;
                    userService.StripeServiceCustomerSubscriptionEmail = customerStripeServiceSubscriptionEmail;
                    userService.Sysdate = DateTime.Now;
                    userService.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsersStripeId> loader2 = new DataLoader<ElioUsersStripeId>(session);
                    loader2.Insert(userService);

                    #endregion
                }
                else
                {
                    #region Update Customer Stripe Id

                    userService.StripePacketCustomerId = user.CustomerStripeId;
                    userService.StripeServiceCustomerId = customerStripeServiceId;
                    userService.Sysdate = DateTime.Now;
                    userService.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsersStripeId> loader2 = new DataLoader<ElioUsersStripeId>(session);
                    loader2.Update(userService);

                    #endregion
                }

                #region Credit Card

                if (cardInfo != null)
                {
                    ElioUsersCreditCards cc = new ElioUsersCreditCards();

                    cc.CardStripeId = cardInfo.ID;
                    cc.CardFullname = cardInfo.Name;
                    cc.Address1 = cardInfo.AddressLine1;
                    cc.Address2 = cardInfo.AddressLine2;
                    cc.CardType = cardInfo.Type;
                    cc.ExpMonth = cardInfo.ExpirationMonth;
                    cc.ExpYear = cardInfo.ExpirationYear;
                    cc.Origin = cardInfo.Country;
                    cc.CvcCheck = cardInfo.CvcCheck.ToString();
                    cc.Fingerprint = cardInfo.Fingerprint;
                    cc.IsDefault = 1;
                    cc.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                    cc.Sysdate = DateTime.Now;
                    cc.LastUpdated = DateTime.Now;
                    cc.CustomerStripeId = customerStripeServiceId;
                    cc.UserId = user.Id;

                    DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                    loader.Insert(cc);
                }

                #endregion
            }
        }

        public static bool CancelUserService(ElioUsers user, ElioUsersCreditCards card, DBSession session, out DateTime periodEndlAt)
        {
            bool successCancelation = false;

            periodEndlAt = DateTime.Now;

            ElioBillingUserOrders serviceOrder = Sql.GetUserActiveOrderByPacketAndType(user.Id, Convert.ToInt32(OrderType.ServiceNewOrder), Convert.ToInt32(Packets.PremiumService), session);

            if (serviceOrder != null)
            {
                #region Cancel Service Order

                periodEndlAt = Convert.ToDateTime(serviceOrder.CurrentPeriodEnd);

                serviceOrder.LastUpdate = DateTime.Now;
                serviceOrder.CanceledAt = DateTime.Now;
                serviceOrder.IsReadyToUse = Convert.ToInt32(OrderStatus.NotReadyToUse);
                serviceOrder.OrderStatus = Convert.ToInt32(OrderStatus.Canceled);
                serviceOrder.Mode = OrderMode.Canceled.ToString();
                serviceOrder.AdminId = user.Id;
                serviceOrder.AdminName = user.CompanyName;

                DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
                loader.Update(serviceOrder);

                successCancelation = true;

                #endregion
            }
            else
            {
                successCancelation = false;
            }

            #region Delete / Set Not Default Credit Card

            //ElioUsersCreditCards card = Sql.GetUserDefaultCreditCard(user.Id, session);
            if (card != null)
            {
                card.IsDefault = 0;
                card.IsDeleted = 1;
                card.LastUpdated = DateTime.Now;

                DataLoader<ElioUsersCreditCards> loader01 = new DataLoader<ElioUsersCreditCards>(session);
                loader01.Update(card);
            }

            #endregion

            return successCancelation;
        }

        public static ElioUsers MakeUserPremium(ElioUsers user, int selectedPacketId, int? discountPercentOff, string customerStripeId, string stripeCreditCardId, string billingEmail, DateTime? startDate, DateTime? trialPeriodStart, DateTime? trialPeriodEnd, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, string orderMode, Xamarin.Payments.Stripe.StripeCard cardInfo, DBSession session)
        {
            if (user.HasBillingDetails == 0)
            {
                #region New Billing Account Data
 
                ElioUsersBillingAccount account = new ElioUsersBillingAccount();

                account.UserId = user.Id;
                account.BillingEmail = billingEmail;
                account.HasVat = 1;
                account.Sysdate = DateTime.Now;
                account.LastUpdated = DateTime.Now;
                account.IsActive = 1;

                DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                loader0.Insert(account);

                #endregion
            }
            else
            {
                #region Update Billing Account Data

                ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(user.Id, session);

                if (account != null)
                {
                    account.BillingEmail = billingEmail;
                    account.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                    loader0.Update(account);
                }

                #endregion
            }

            #region Credit Card

            if (cardInfo != null)
            {
                ElioUsersCreditCards cc = new ElioUsersCreditCards();

                cc.CardStripeId = cardInfo.ID;
                cc.CardFullname = cardInfo.Name;
                cc.Address1 = cardInfo.AddressLine1;
                cc.Address2 = cardInfo.AddressLine2;
                cc.CardType = cardInfo.Type;
                cc.ExpMonth = cardInfo.ExpirationMonth;
                cc.ExpYear = cardInfo.ExpirationYear;
                cc.Origin = cardInfo.Country;
                cc.CvcCheck = cardInfo.CvcCheck.ToString();
                cc.Fingerprint = cardInfo.Fingerprint;
                cc.IsDefault = 1;
                cc.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                cc.Sysdate = DateTime.Now;
                cc.LastUpdated = DateTime.Now;
                cc.CustomerStripeId = customerStripeId;
                cc.UserId = user.Id;

                DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                loader.Insert(cc);
            }

            #endregion
                        
            #region New Order - DELETED

            //ElioBillingUserOrders order = new ElioBillingUserOrders();

            //order.UserId = user.Id;
            //order.Sysdate = startDate;
            //order.LastUpdate = DateTime.Now;
            //order.PackId = selectedPacketId;
            //order.CurrentPeriodStart = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
            //order.CurrentPeriodEnd = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;
            //order.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
            //order.OrderStatus = Convert.ToInt32(OrderStatus.Active);
            //order.CostWithNoVat = (discountPercentOff == null) ? Sql.GetPacketTotalCostWithNoVat(selectedPacketId, session) : (Sql.GetPacketTotalCostWithNoVat(selectedPacketId, session) * Convert.ToDecimal(discountPercentOff)) / 100;
            //order.CostWithVat = (discountPercentOff == null) ? Sql.GetPacketTotalCostWithVat(selectedPacketId, session) : (Sql.GetPacketTotalCostWithVat(selectedPacketId, session) * Convert.ToDecimal(discountPercentOff)) / 100;
            //order.CostVat = order.CostWithVat - order.CostWithNoVat;
            //order.OrderPaymentWay = PaymentType.Stripe.ToString();
            //order.OrderType = Convert.ToInt32(OrderType.PacketNewOrder);
            //order.Mode = orderMode;
            //order.IsPaid = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToInt32(OrderStatus.Paid) : Convert.ToInt32(OrderStatus.NotPaid);

            //bool isAdmin = false;
            //try
            //{
            //    isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
            //}
            //catch (Exception ex)
            //{
            //    isAdmin = false;
            //    Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
            //}

            //order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
            //order.AdminId = (isAdmin) ? user.Id : 0;

            //DataLoader<ElioBillingUserOrders> orderLoader = new DataLoader<ElioBillingUserOrders>(session);
            //orderLoader.Insert(order);

            #endregion

            #region Fix Packet Features Items for Premium User

            List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(selectedPacketId, session);
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
                        totalConnections = items[i].FreeItemsNo;    // (order.Mode == OrderMode.Trialing.ToString()) ? items[i].FreeItemsTrialNo : items[i].FreeItemsNo;
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
                    packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(selectedPacketId, session);
                    packetFeatures.AvailableLeadsCount = totalLeads;
                    packetFeatures.AvailableMessagesCount = totalMessages;
                    packetFeatures.AvailableConnectionsCount = totalConnections;
                    packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                    packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                    packetFeatures.Sysdate = DateTime.Now;
                    packetFeatures.LastUpdate = DateTime.Now;
                    packetFeatures.StartingDate = currentPeriodStart != null ? currentPeriodStart : DateTime.Now;   // (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                    packetFeatures.ExpirationDate = currentPeriodEnd != null ? currentPeriodEnd : Convert.ToDateTime(packetFeatures.StartingDate).AddMonths(1);    //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                    loader4.Insert(packetFeatures);
                }
                else
                {
                    packetFeatures.PackId = items[0].Id;
                    packetFeatures.UserBillingType = Sql.GetPremiumBillingTypeIdByPacketId(selectedPacketId, session);
                    packetFeatures.AvailableLeadsCount = totalLeads;
                    packetFeatures.AvailableMessagesCount = totalMessages;
                    packetFeatures.AvailableConnectionsCount = totalConnections;
                    packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                    packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                    packetFeatures.LastUpdate = DateTime.Now;
                    packetFeatures.StartingDate = currentPeriodStart != null ? currentPeriodStart : DateTime.Now;          //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                    packetFeatures.ExpirationDate = currentPeriodEnd != null ? currentPeriodEnd : Convert.ToDateTime(packetFeatures.StartingDate).AddMonths(1);        //(orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                    loader4.Update(packetFeatures);
                }

                #endregion
            }
            else
            {
                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
            }

            #endregion

            #region Update User With Billing Details Status

            user.HasBillingDetails = 1;
            user.BillingType = Sql.GetPremiumBillingTypeIdByPacketId(selectedPacketId, session);
            user.CustomerStripeId = customerStripeId;

            user = GlobalDBMethods.UpDateUser(user, session);

            #endregion

            #region to delete

            #region New Purchase

            //ElioBillingPurchases purchase = new ElioBillingPurchases();

            //purchase.UserId = user.Id;
            //purchase.OrderId = order.Id;
            //purchase.Sysdate = DateTime.Now;
            //purchase.LastUpdate = DateTime.Now;
            //purchase.TotalCost = order.CostWithVat;
            //purchase.FpaValue = Sql.GetPacketVatCost(Convert.ToInt32(Packets.Premium), session);
            //purchase.CostWithNoFpa = order.CostWithNoVat;
            //purchase.IsInvoice = 0;
            //purchase.PrintInvoiceNo = string.Empty;
            //purchase.Canceled = Convert.ToInt32(PurchaseStatus.NotCanceled);
            //purchase.EInvoice = 0;
            //purchase.IsPaid = Convert.ToInt32(PurchaseStatus.Paid);
            //purchase.Comments = string.Empty;                                
            //purchase.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
            //purchase.AdminId = (isAdmin) ? user.Id : 0;
            //purchase.PurchaseWay = PaymentType.Stripe.ToString();
            //purchase.Discount = 0;  //to do
            //purchase.PurchaseType = Convert.ToInt32(PurchaseType.NewOrder);

            //DataLoader<ElioBillingPurchases> purchaseLoader = new DataLoader<ElioBillingPurchases>(session);
            //purchaseLoader.Insert(purchase);

            #endregion

            #region Payment

            //ElioBillingPayments payment = new ElioBillingPayments();

            //payment.OrderId = order.Id;
            //payment.PaymentWay = PaymentType.Stripe.ToString();
            //payment.Sysdate = DateTime.Now;
            //payment.LastUpdate = DateTime.Now;
            //payment.TotalAmount = purchase.TotalCost;
            //payment.UserId = order.UserId;
            //payment.Comments = string.Empty;
            //payment.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
            //payment.AdminId = (isAdmin) ? user.Id : 0;
            //payment.PaymentChargeId = chargeId;

            //DataLoader<ElioBillingPayments> loader2 = new DataLoader<ElioBillingPayments>(session);
            //loader2.Insert(payment);

            #endregion

            #region Purchase - Payment

            //ElioBillingPaymentsPurchases paymentPurchase = new ElioBillingPaymentsPurchases();

            //paymentPurchase.PurchaseId = purchase.Id;
            //paymentPurchase.PaymentId = payment.Id;
            //paymentPurchase.Sysdate = DateTime.Now;
            //paymentPurchase.LastUpdate = DateTime.Now;
            //paymentPurchase.TotalAmount = payment.TotalAmount;
            //paymentPurchase.UserId = order.UserId;

            //DataLoader<ElioBillingPaymentsPurchases> loader3 = new DataLoader<ElioBillingPaymentsPurchases>(session);
            //loader3.Insert(paymentPurchase);

            #endregion

            #endregion

            return user;
        }

        public static ElioUsers MakeUserPremium_v2(ElioUsers user, ElioUsersSubscriptions subscription, List<ElioUsersSubscriptionsInvoices> invoices)
        {


            return user;
        }

        public static ElioUsers MakeUserVirtualPremium(ElioUsers user, int billingTypeId, string customerStripeId, string billingEmail, DateTime? startDate, DateTime? currentPeriodStart, DateTime? currentPeriodEnd, DateTime? trialPeriodStart, DateTime? trialPeriodEnd, string orderMode, DBSession session)
        {
            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(billingTypeId, session);
            if (packet == null)
            {
                throw new Exception("No packet found");
            }

            if (user.HasBillingDetails == 0)
            {
                #region New Billing Account Data

                ElioUsersBillingAccount account = new ElioUsersBillingAccount();

                account.UserId = user.Id;
                account.BillingEmail = billingEmail;
                account.HasVat = 1;
                account.Sysdate = DateTime.Now;
                account.LastUpdated = DateTime.Now;
                account.IsActive = 1;

                DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                loader0.Insert(account);

                #endregion
            }
            else
            {
                #region Update Billing Account Data

                ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(user.Id, session);

                if (account != null)
                {
                    account.BillingEmail = billingEmail;
                    account.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                    loader0.Update(account);
                }

                #endregion
            }

            #region New Order

            ElioBillingUserOrders order = new ElioBillingUserOrders();

            order.UserId = user.Id;
            order.Sysdate = startDate;
            order.LastUpdate = DateTime.Now;
            order.PackId = packet.Id;
            order.CurrentPeriodStart = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
            order.CurrentPeriodEnd = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;
            order.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
            order.OrderStatus = Convert.ToInt32(OrderStatus.Active);
            order.CostWithNoVat = Sql.GetPacketTotalCostWithNoVat(packet.Id, session);
            order.CostWithVat = Sql.GetPacketTotalCostWithVat(packet.Id, session);
            order.CostVat = Sql.GetPacketTotalCostWithVat(packet.Id, session);
            order.OrderPaymentWay = PaymentType.Stripe.ToString();
            order.OrderType = Convert.ToInt32(OrderType.PacketNewOrder);
            order.Mode = orderMode;
            order.IsPaid = (order.Mode == OrderMode.Active.ToString()) ? Convert.ToInt32(OrderStatus.Paid) : Convert.ToInt32(OrderStatus.NotPaid);

            bool isAdmin = false;
            try
            {
                isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
                Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
            }

            order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
            order.AdminId = (isAdmin) ? user.Id : 0;

            DataLoader<ElioBillingUserOrders> orderLoader = new DataLoader<ElioBillingUserOrders>(session);
            orderLoader.Insert(order);

            #endregion

            #region Fix Packet Features Items for Premium User

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
                        totalConnections = items[i].FreeItemsTrialNo;
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
                    packetFeatures.StartingDate = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                    packetFeatures.ExpirationDate = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

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
                    packetFeatures.StartingDate = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodStart : currentPeriodStart;
                    packetFeatures.ExpirationDate = (orderMode == OrderMode.Trialing.ToString()) ? trialPeriodEnd : currentPeriodEnd;

                    loader4.Update(packetFeatures);
                }

                #endregion
            }
            else
            {
                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
            }

            #endregion

            #region Update User With Billing Details Status

            user.HasBillingDetails = 1;
            user.BillingType = billingTypeId;
            user.CustomerStripeId = customerStripeId;

            user = GlobalDBMethods.UpDateUser(user, session);

            #endregion
                
            return user;
        }

        public static ElioUsers MakeUserFreemium(ElioUsers user, DateTime? canceledAt, ElioUsersCreditCards card, DBSession session)
        {
            ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

            if (order != null)
            {
                #region Cancel Active Order

                order.OrderStatus = Convert.ToInt32(OrderStatus.Canceled);
                order.IsReadyToUse = Convert.ToInt32(OrderStatus.NotReadyToUse);
                order.LastUpdate = DateTime.Now;
                order.CanceledAt = (canceledAt != null) ? canceledAt : DateTime.Now;
                order.Mode = OrderMode.Canceled.ToString();

                bool isAdmin = false;
                try
                {
                    isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
                }
                catch (Exception ex)
                {
                    isAdmin = false;
                    Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
                }

                order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
                order.AdminId = (isAdmin) ? user.Id : 0;

                DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
                loader.Update(order);

                #endregion

                #region Cancel Last Payment

                ElioBillingUserOrdersPayments lastPayment = Sql.GetUserLastPaymentsByOrderId(order.Id, session);
                if (lastPayment != null)
                {
                    lastPayment.Mode = OrderMode.Canceled.ToString();
                    lastPayment.LastUpdated = DateTime.Now;

                    DataLoader<ElioBillingUserOrdersPayments> paymentLoader = new DataLoader<ElioBillingUserOrdersPayments>(session);
                    paymentLoader.Update(lastPayment);
                }

                #endregion
            }

            #region Delete / Set Not Default Credit Card

            //ElioUsersCreditCards card = Sql.GetUserDefaultCreditCard(user.Id, session);
            if (card != null)
            {
                card.IsDefault = 0;
                card.IsDeleted = 1;
                card.LastUpdated = DateTime.Now;

                DataLoader<ElioUsersCreditCards> loader01 = new DataLoader<ElioUsersCreditCards>(session);
                loader01.Update(card);
            }

            #endregion

            #region Fix Packet Features Items for Freemium User

            List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Freemium), session);
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
                        totalConnections = items[i].FreeItemsNo;
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
                    packetFeatures.UserBillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                    packetFeatures.AvailableLeadsCount = totalLeads;
                    packetFeatures.AvailableMessagesCount = totalMessages;
                    packetFeatures.AvailableConnectionsCount = totalConnections;
                    packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                    packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                    packetFeatures.Sysdate = DateTime.Now;
                    packetFeatures.LastUpdate = DateTime.Now;
                    packetFeatures.StartingDate = DateTime.Now;
                    packetFeatures.ExpirationDate = DateTime.Now.AddMonths(1);

                    loader4.Insert(packetFeatures);
                }
                else
                {
                    packetFeatures.PackId = items[0].Id;
                    packetFeatures.UserBillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                    packetFeatures.AvailableLeadsCount = totalLeads;
                    packetFeatures.AvailableMessagesCount = totalMessages;
                    packetFeatures.AvailableConnectionsCount = totalConnections;
                    packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                    packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                    packetFeatures.LastUpdate = DateTime.Now;
                    packetFeatures.StartingDate = DateTime.Now;
                    packetFeatures.ExpirationDate = DateTime.Now.AddMonths(1);

                    loader4.Update(packetFeatures);
                }

                #endregion

                #region Cancel Connections

                //if (order.CurrentPeriodStart != null && order.CurrentPeriodEnd != null)
                //{
                //    int canBeViewed = 1;
                //    List<ElioUsersConnections> connections = Sql.GetUserConnectionsDetails(user.Id, canBeViewed, Convert.ToDateTime(order.CurrentPeriodStart).AddDays(-1).ToString(), Convert.ToDateTime(order.CurrentPeriodStart).AddMonths(1).AddDays(1).ToString(), session);

                //    foreach (ElioUsersConnections connection in connections)
                //    {
                //        connection.CanBeViewed = 0;
                //        connection.Status = false;

                //        DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                //        loader1.Update(connection);
                //    }
                //}

                #endregion
            }
            else
            {
                Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
            }

            #endregion

            #region Update User With Billing Details Status

            user.LastUpdated = DateTime.Now;
            user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);

            DataLoader<ElioUsers> loader2 = new DataLoader<ElioUsers>(session);
            loader2.Update(user);

            #endregion

            return user;
        }

        public static ElioUsers  CancelCustomerSubscriptionAndMakeUserFreemium(ElioUsers user, Subscription subscription, bool isServicePlan, DBSession session)
        {
            ElioUsersSubscriptions ElioSubscription = Sql.GetUserSubscriptionBySubID(user.Id, subscription.Id, session);
            if (ElioSubscription != null)
            {
                ElioSubscription.CanceledAt = Convert.ToDateTime(subscription.CanceledAt);
                ElioSubscription.Status = subscription.Status.ToString().ToLower() == "past due" ? "canceled" : subscription.Status.ToString();

                DataLoader<ElioUsersSubscriptions> loader = new DataLoader<ElioUsersSubscriptions>(session);
                loader.Update(ElioSubscription);
            }

            if (!isServicePlan)
            {
                #region Fix Packet Features Items for Freemium User

                List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Freemium), session);
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
                            totalConnections = items[i].FreeItemsNo;
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
                        packetFeatures.UserBillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                        packetFeatures.AvailableLeadsCount = totalLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                        packetFeatures.Sysdate = DateTime.Now;
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = DateTime.Now;
                        packetFeatures.ExpirationDate = DateTime.Now.AddMonths(1);

                        loader4.Insert(packetFeatures);
                    }
                    else
                    {
                        packetFeatures.PackId = items[0].Id;
                        packetFeatures.UserBillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                        packetFeatures.AvailableLeadsCount = totalLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage);
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = DateTime.Now;
                        packetFeatures.ExpirationDate = DateTime.Now.AddMonths(1);

                        loader4.Update(packetFeatures);
                    }

                    #endregion

                    #region Cancel Connections

                    //if (order.CurrentPeriodStart != null && order.CurrentPeriodEnd != null)
                    //{
                    //    int canBeViewed = 1;
                    //    List<ElioUsersConnections> connections = Sql.GetUserConnectionsDetails(user.Id, canBeViewed, Convert.ToDateTime(order.CurrentPeriodStart).AddDays(-1).ToString(), Convert.ToDateTime(order.CurrentPeriodStart).AddMonths(1).AddDays(1).ToString(), session);

                    //    foreach (ElioUsersConnections connection in connections)
                    //    {
                    //        connection.CanBeViewed = 0;
                    //        connection.Status = false;

                    //        DataLoader<ElioUsersConnections> loader1 = new DataLoader<ElioUsersConnections>(session);
                    //        loader1.Update(connection);
                    //    }
                    //}

                    #endregion
                }
                else
                {
                    Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                }

                #endregion

                #region Update User With Billing Details Status

                user.LastUpdated = DateTime.Now;
                user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);

                DataLoader<ElioUsers> loader2 = new DataLoader<ElioUsers>(session);
                loader2.Update(user);

                #endregion
            }

            return user;
        }

        private static void FixUserCurrentMonthOrderAndFeatures(ElioUsers user, ElioBillingUserOrders order, Xamarin.Payments.Stripe.StripeSubscription subscription, DBSession session)
        {
            #region Update Order Current Period After Trial End

            order.Sysdate = subscription.Start;
            order.LastUpdate = DateTime.Now;
            order.CurrentPeriodStart = subscription.CurrentPeriodStart;
            order.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
            order.IsPaid = Convert.ToInt32(OrderStatus.Paid);
            order.Mode = subscription.Status.ToString();

            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            loader.Update(order);

            #endregion

            #region Fix Packet Features Items for User

            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);     //Get packet feature items by user billing packet type id
            if (packet != null)
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
                            totalConnections = items[i].FreeItemsNo;
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

                    int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);
                    int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);

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
                        packetFeatures.UserBillingType = user.BillingType;
                        packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners - totalUserInvitations;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage - totalUserFileSizeGB);
                        packetFeatures.Sysdate = Convert.ToDateTime(subscription.Start);
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = subscription.CurrentPeriodStart;
                        packetFeatures.ExpirationDate = subscription.CurrentPeriodEnd;

                        loader4.Insert(packetFeatures);
                    }
                    else
                    {
                        packetFeatures.PackId = items[0].Id;
                        packetFeatures.UserBillingType = user.BillingType;
                        packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners - totalUserInvitations;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage - totalUserFileSizeGB);
                        packetFeatures.Sysdate = Convert.ToDateTime(subscription.Start);
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = subscription.CurrentPeriodStart;
                        packetFeatures.ExpirationDate = subscription.CurrentPeriodEnd;

                        loader4.Update(packetFeatures);
                    }

                    #endregion
                }
                else
                {
                    Logger.DetailedError(string.Format("Method--> FixUserCurrentMonthOrderAndFeatures, User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                }
            }

            #endregion
        }

        private static void FixUserCurrentMonthOrderAndFeatures_v2(ElioUsers user, ElioBillingUserOrders order, Xamarin.Payments.Stripe.StripeSubscription subscription, DBSession session)
        {
            #region Update Order Current Period / Status as Expired

            order.OrderStatus = (int)OrderStatus.Expired;
            order.LastUpdate = DateTime.Now;
            order.Mode = OrderMode.PastDue.ToString();

            DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
            loader.Update(order);

            #endregion

            #region Add New Order

            ElioBillingUserOrders newOrder = new ElioBillingUserOrders();

            newOrder.UserId = user.Id;
            newOrder.Sysdate = order.Sysdate;
            newOrder.LastUpdate = DateTime.Now;
            newOrder.PackId = order.PackId;
            newOrder.CurrentPeriodStart = subscription.CurrentPeriodStart;
            newOrder.CurrentPeriodEnd = subscription.CurrentPeriodEnd;
            newOrder.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
            newOrder.OrderStatus = Convert.ToInt32(OrderStatus.Active);
            newOrder.CostWithNoVat = subscription.Plan.Amount;
            newOrder.CostWithVat = Sql.GetPacketTotalCostWithVat(order.PackId, session);
            newOrder.CostVat = newOrder.CostWithVat - newOrder.CostWithNoVat;
            newOrder.OrderPaymentWay = PaymentType.Stripe.ToString();
            newOrder.OrderType = Convert.ToInt32(OrderType.PacketNewOrder);
            newOrder.Mode = subscription.Status.ToString();
            newOrder.IsPaid = Convert.ToInt32(OrderStatus.Paid);

            bool isAdmin = false;
            try
            {
                isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
                Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
            }

            order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
            order.AdminId = (isAdmin) ? user.Id : 0;

            DataLoader<ElioBillingUserOrders> orderLoader = new DataLoader<ElioBillingUserOrders>(session);
            orderLoader.Insert(order);

            #endregion

            #region Fix Packet Features Items for User

            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);     //Get packet feature items by user billing packet type id
            if (packet != null)
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
                            totalConnections = items[i].FreeItemsNo;
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

                    int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                    int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);
                    int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, session);

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
                        packetFeatures.UserBillingType = user.BillingType;
                        packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners - totalUserInvitations;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage - totalUserFileSizeGB);
                        packetFeatures.Sysdate = Convert.ToDateTime(subscription.Start);
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = subscription.CurrentPeriodStart;
                        packetFeatures.ExpirationDate = subscription.CurrentPeriodEnd;

                        loader4.Insert(packetFeatures);
                    }
                    else
                    {
                        packetFeatures.PackId = items[0].Id;
                        packetFeatures.UserBillingType = user.BillingType;
                        packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners - totalUserInvitations;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage - totalUserFileSizeGB);
                        packetFeatures.Sysdate = Convert.ToDateTime(subscription.Start);
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = subscription.CurrentPeriodStart;
                        packetFeatures.ExpirationDate = subscription.CurrentPeriodEnd;

                        loader4.Update(packetFeatures);
                    }

                    #endregion
                }
                else
                {
                    Logger.DetailedError(string.Format("Method--> FixUserCurrentMonthOrderAndFeatures, User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
                }
            }

            #endregion
        }

        public static void FixPremiumUserFeaturesPlanAfterTrialPeriodEnd(ElioUsers user, DBSession session)
        {
            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                if (!string.IsNullOrEmpty(user.CustomerStripeId))
                {
                    DateTime? startDate = null;
                    DateTime? currentPeriodStart = null;
                    DateTime? currentPeriodEnd = null;
                    DateTime? trialPeriodStart = null;
                    DateTime? trialPeriodEnd = null;
                    DateTime? canceledAt = null;
                    string orderMode = string.Empty;

                    Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, user.CustomerStripeId);

                    if (subscription != null)
                    {
                        if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
                        {
                            ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user, Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

                            if (order != null)
                            {
                                if (order.IsPaid == Convert.ToInt32(OrderStatus.NotPaid) && order.Mode == OrderMode.Trialing.ToString())
                                {
                                    FixUserCurrentMonthOrderAndFeatures(user, order, subscription, session);
                                }
                                else
                                {
                                    if (order.IsPaid == Convert.ToInt32(OrderStatus.Paid) && order.Mode == OrderMode.Active.ToString())
                                    {
                                        if (order.CurrentPeriodEnd < currentPeriodEnd)
                                        {
                                            FixUserCurrentMonthOrderAndFeatures(user, order, subscription, session);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Logger.DetailedError(string.Format("Customer with ID: {0}, StripeID: {1}, could not find and update his subscription", user.Id.ToString(), user.CustomerStripeId.ToString()));
                    }
                }
            }
            else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeaturesByDate(user.Id, DateTime.Now, session);
                if (packetFeatures != null)
                {
                    List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Freemium), session);
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
                                totalConnections = items[i].FreeItemsNo;
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

                        int startDay = Convert.ToDateTime(packetFeatures.Sysdate).Day;

                        //Check if specific month has less days than startDay, so set that day as startDay                         
                        DateTime startDate = GlobalMethods.ReturnValidDateOfMonth(DateTime.Now.Year, DateTime.Now.Month, startDay);      //Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.Month + "-" + startDay);

                        if (startDay > DateTime.Now.Day)
                        {
                            //Check if specific month has less days than startDay, so set that day as startDay
                            startDate = GlobalMethods.ReturnValidDateOfMonth(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, startDay);        //Convert.ToDateTime(DateTime.Now.Year + "-" + DateTime.Now.AddMonths(-1).Month + "-" + startDay);
                        }

                        #region Get User Already Supplied Leads/Messages/Connections for Current Period

                        int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, startDate, startDate.AddMonths(1), session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                        int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, startDate, startDate.AddMonths(1), session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                        int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, startDate, startDate.AddMonths(1), session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
                        int totalUserInvitations = Sql.GetUserInvitationsForCurrentPeriod(user.Id, CollaborateInvitationStatus.Confirmed.ToString(), startDate, startDate.AddMonths(1), session);
                        int totalUserFilesSize = Sql.GetUserLibraryFilesStorageForCurrentPeriod(user.Id, startDate, startDate.AddMonths(1), session);

                        double totalUserFileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(totalUserFilesSize), "GB");

                        #endregion

                        packetFeatures.PackId = items[0].Id;
                        packetFeatures.UserBillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                        packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
                        packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
                        packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
                        packetFeatures.AvailableManagePartnersCount = totalManagePartners - totalUserInvitations;
                        packetFeatures.AvailableLibraryStorageCount = Convert.ToDecimal(totalLibraryStorage - totalUserFileSizeGB);
                        packetFeatures.Sysdate = DateTime.Now;
                        packetFeatures.LastUpdate = DateTime.Now;
                        packetFeatures.StartingDate = startDate;
                        packetFeatures.ExpirationDate = startDate.AddMonths(1);

                        DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);
                        loader4.Update(packetFeatures);
                    }
                }
            }
        }

        //public static void FixPremiumUserFeaturesPlanAfterTrialPeriodEnd_Old(ElioUsers user, DBSession session)
        //{
        //    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
        //    {
        //        bool hasOrderInTrialMode = Sql.HasOrderByMode(user.Id, OrderMode.Trialing.ToString(), session);

        //        if (hasOrderInTrialMode)
        //        {
        //            DateTime? startDate = null;
        //            DateTime? currentPeriodStart = null;
        //            DateTime? currentPeriodEnd = null;
        //            DateTime? trialPeriodStart = null;
        //            DateTime? trialPeriodEnd = null;
        //            DateTime? canceledAt = null;
        //            string orderMode = string.Empty;

        //            Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, user.CustomerStripeId);

        //            if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Active)
        //            {
        //                if (startDate != null && currentPeriodStart != null && currentPeriodEnd != null && !string.IsNullOrEmpty(orderMode))
        //                {
        //                    ElioBillingUserOrders order = Sql.HasUserOrderByPacketStatusUse(user.Id, Convert.ToInt32(Packets.Premium), Convert.ToInt32(OrderStatus.Active), Convert.ToInt32(OrderStatus.ReadyToUse), session);

        //                    if (order != null)
        //                    {
        //                        #region Update Order Current Period After Trial End

        //                        order.Sysdate = startDate;
        //                        order.LastUpdate = DateTime.Now;
        //                        order.CurrentPeriodStart = currentPeriodStart;
        //                        order.CurrentPeriodEnd = currentPeriodEnd;
        //                        order.IsPaid = Convert.ToInt32(OrderStatus.Paid);
        //                        order.Mode = orderMode;

        //                        DataLoader<ElioBillingUserOrders> loader = new DataLoader<ElioBillingUserOrders>(session);
        //                        loader.Update(order);

        //                        #endregion

        //                        #region Fix Packet Features Items for Freemium User

        //                        List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Premium), session);
        //                        if (items.Count > 0)
        //                        {
        //                            #region Get Packet Features Items

        //                            int totalLeads = 0;
        //                            int totalMessages = 0;
        //                            int totalConnections = 0;
        //                            int totalManagePartners = 0;
        //                            int totalLibraryStorage = 0;

        //                            for (int i = 0; i < items.Count; i++)
        //                            {
        //                                if (items[i].ItemDescription == "Leads")
        //                                {
        //                                    totalLeads = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "Messages")
        //                                {
        //                                    totalMessages = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "Connections")
        //                                {
        //                                    totalConnections = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "ManagePartners")
        //                                {
        //                                    totalManagePartners = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "LibraryStorage")
        //                                {
        //                                    totalLibraryStorage = items[i].FreeItemsNo;
        //                                }
        //                            }

        //                            #endregion

        //                            #region Get User Already Supplied Leads/Messages/Connections for Current Period

        //                            int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, currentPeriodStart, currentPeriodEnd, session);                            //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
        //                            int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, currentPeriodStart, currentPeriodEnd, session);               //must be 0 (it has to be 0 because the counter must begin from 0 for this period)
        //                            int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, currentPeriodStart, currentPeriodEnd, session);      //must be 0 (it has to be 0 because the counter must begin from 0 for this period)

        //                            #endregion

        //                            #region Insert / Update Packet Status Features

        //                            ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

        //                            DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

        //                            if (packetFeatures == null)
        //                            {
        //                                packetFeatures = new ElioUserPacketStatus();

        //                                packetFeatures.UserId = user.Id;
        //                                packetFeatures.PackId = items[0].Id;
        //                                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                                packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
        //                                packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
        //                                packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
        //                                packetFeatures.Sysdate = Convert.ToDateTime(startDate);
        //                                packetFeatures.LastUpdate = DateTime.Now;
        //                                packetFeatures.StartingDate = currentPeriodStart;
        //                                packetFeatures.ExpirationDate = currentPeriodEnd;

        //                                loader4.Insert(packetFeatures);
        //                            }
        //                            else
        //                            {
        //                                packetFeatures.PackId = items[0].Id;
        //                                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                                packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
        //                                packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
        //                                packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
        //                                packetFeatures.Sysdate = Convert.ToDateTime(startDate);
        //                                packetFeatures.LastUpdate = DateTime.Now;
        //                                packetFeatures.StartingDate = currentPeriodStart;
        //                                packetFeatures.ExpirationDate = currentPeriodEnd;

        //                                loader4.Update(packetFeatures);
        //                            }

        //                            #endregion
        //                        }
        //                        else
        //                        {
        //                            Logger.DetailedError(string.Format("User :{0} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString()));
        //                        }

        //                        #endregion
        //                    }
        //                    else
        //                    {
        //                        #region New Order

        //                        order = new ElioBillingUserOrders();

        //                        order.UserId = user.Id;
        //                        order.Sysdate = startDate;
        //                        order.LastUpdate = DateTime.Now;
        //                        order.PackId = Convert.ToInt32(Packets.Premium);
        //                        order.CurrentPeriodStart = currentPeriodStart;
        //                        order.CurrentPeriodEnd = currentPeriodEnd;
        //                        order.IsReadyToUse = Convert.ToInt32(OrderStatus.ReadyToUse);
        //                        order.OrderStatus = Convert.ToInt32(OrderStatus.Active);
        //                        order.CostWithNoVat = Sql.GetPacketTotalCostWithNoVat(Convert.ToInt32(Packets.Premium), session);
        //                        order.CostWithVat = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session);
        //                        order.CostVat = Sql.GetPacketTotalCostWithVat(Convert.ToInt32(Packets.Premium), session);
        //                        order.OrderPaymentWay = PaymentType.Stripe.ToString();
        //                        order.OrderType = Convert.ToInt32(OrderType.PacketNewOrder);
        //                        order.IsPaid = Convert.ToInt32(OrderStatus.Paid);

        //                        bool isAdmin = false;
        //                        try
        //                        {
        //                            isAdmin = (Sql.IsUserAdministrator(user.Id, session)) ? true : false;
        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            isAdmin = false;
        //                            Logger.DetailedError("Query for user admin or not did not found", ex.Message.ToString(), ex.StackTrace.ToString());
        //                        }

        //                        order.AdminName = (isAdmin) ? user.CompanyName : string.Empty;
        //                        order.AdminId = (isAdmin) ? user.Id : 0;
        //                        order.Mode = orderMode;

        //                        DataLoader<ElioBillingUserOrders> orderLoader = new DataLoader<ElioBillingUserOrders>(session);
        //                        orderLoader.Insert(order);

        //                        #endregion

        //                        #region Fix Packet Features Items for Freemium User

        //                        List<ElioPacketsIJFeaturesItems> items = Sql.GetPacketFeaturesItems(Convert.ToInt32(Packets.Premium), session);
        //                        if (items.Count > 0)
        //                        {
        //                            #region Get Packet Features Items

        //                            int totalLeads = 0;
        //                            int totalMessages = 0;
        //                            int totalConnections = 0;

        //                            for (int i = 0; i < items.Count; i++)
        //                            {
        //                                if (items[i].ItemDescription == "Leads")
        //                                {
        //                                    totalLeads = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "Messages")
        //                                {
        //                                    totalMessages = items[i].FreeItemsNo;
        //                                }
        //                                else if (items[i].ItemDescription == "Connections")
        //                                {
        //                                    totalConnections = items[i].FreeItemsNo;
        //                                }
        //                            }

        //                            #endregion

        //                            #region Get User Already Supplied Leads/Messages/Connections for Current Period

        //                            int totalUserLeads = Sql.GetUserLeadsCountByMonthRange(user, currentPeriodStart, currentPeriodEnd, session);
        //                            int totalUserMessages = Sql.GetUserSendMessagesCountByMonthRange(user.Id, currentPeriodStart, currentPeriodEnd, session);
        //                            int totalUserConnections = Sql.GetUserViewableConnectionsForCurrentPeriod(user.Id, currentPeriodStart, currentPeriodEnd, session);

        //                            #endregion

        //                            #region Insert / Update Packet Status Features

        //                            ElioUserPacketStatus packetFeatures = Sql.GetUserPacketStatusFeatures(user.Id, session);

        //                            DataLoader<ElioUserPacketStatus> loader4 = new DataLoader<ElioUserPacketStatus>(session);

        //                            if (packetFeatures == null)
        //                            {
        //                                packetFeatures = new ElioUserPacketStatus();

        //                                packetFeatures.UserId = user.Id;
        //                                packetFeatures.PackId = items[0].Id;
        //                                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                                packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
        //                                packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
        //                                packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
        //                                packetFeatures.Sysdate = Convert.ToDateTime(startDate);
        //                                packetFeatures.LastUpdate = DateTime.Now;
        //                                packetFeatures.StartingDate = currentPeriodStart;
        //                                packetFeatures.ExpirationDate = currentPeriodEnd;

        //                                loader4.Insert(packetFeatures);
        //                            }
        //                            else
        //                            {
        //                                packetFeatures.PackId = items[0].Id;
        //                                packetFeatures.UserBillingType = Convert.ToInt32(BillingType.Premium);
        //                                packetFeatures.AvailableLeadsCount = totalLeads - totalUserLeads;
        //                                packetFeatures.AvailableMessagesCount = totalMessages - totalUserMessages;
        //                                packetFeatures.AvailableConnectionsCount = totalConnections - totalUserConnections;
        //                                packetFeatures.Sysdate = Convert.ToDateTime(startDate);
        //                                packetFeatures.LastUpdate = DateTime.Now;
        //                                packetFeatures.StartingDate = currentPeriodStart;
        //                                packetFeatures.ExpirationDate = currentPeriodEnd;

        //                                loader4.Update(packetFeatures);
        //                            }

        //                            #endregion
        //                        }
        //                        else
        //                        {
        //                            Logger.DetailedError("User :{} packet status features did not inserted at {1}", user.Id.ToString(), DateTime.Now.ToString());
        //                        }

        //                        #endregion

        //                        Logger.DetailedError(string.Format("FixUserPremiumFeaturesPlan--> Customer with ID: {0}, StripeID: {1}, had no active order after trial period end but was inserted now. Order: {2}", user.Id.ToString(), user.CustomerStripeId.ToString(), order.Id.ToString()));
        //                    }
        //                }
        //                else
        //                {
        //                    Logger.DetailedError(string.Format("FixUserPremiumFeaturesPlan--> Customer with ID: {0}, StripeID: {1}, had no subscription dates", user.Id.ToString(), user.CustomerStripeId.ToString()));
        //                }
        //            }
        //            else if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Trialing)
        //            {
        //                // to do
        //            }
        //            else if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Canceled)
        //            {
        //                if (canceledAt != null)
        //                {
        //                    // to do
        //                }
        //            }
        //            else if (subscription.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Unknown)
        //            {
        //                // to do
        //            }
        //        }
        //        else
        //        {
        //            bool hasOrderInActiveMode = Sql.HasOrderByMode(user.Id, OrderMode.Active.ToString(), session);

        //            if (hasOrderInActiveMode)
        //            {

        //            }
        //        }
        //    }
        //}
    }
}