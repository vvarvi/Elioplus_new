using System;
using System.Linq;
using Stripe;
using Xamarin.Payments.Stripe;
using WdS.ElioPlus.Lib.Enums;
using System.Configuration;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using System.Net;
using System.Collections.Generic;

namespace WdS.ElioPlus.Lib.StripePayment
{
    public class StripeLib
    {
        public static bool GetInTrial(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string customerResponseId,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode)
        {
            bool isError = false;

            try
            {
                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(StripePlans.Elio_Premium_Plan.ToString());

                Xamarin.Payments.Stripe.StripeCreditCardToken cardToken = payment.CreateToken(new StripeCreditCardInfo
                {
                    CVC = cardCvc,
                    ExpirationMonth = expMonth,
                    ExpirationYear = expYear,
                    Number = cardNumber
                });

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                {
                    Email = email,
                    Description = customerName,
                    Plan = planInfoResponse.ID,
                    DefaultCardId = cardToken.ID
                });

                Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                {
                    Card = new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    },
                    Plan = planInfoResponse.ID,
                    Prorate = false
                });

                if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                {
                    orderMode = subResponse.Status.ToString();

                    subscriptionStatus = subResponse.Status.ToString();
                    trialPeriodStart = subResponse.TrialStart;
                    trialPeriodEnd = subResponse.TrialEnd;

                    startDate = subResponse.Start;
                    currentPeriodStart = subResponse.CurrentPeriodStart;
                    currentPeriodEnd = subResponse.CurrentPeriodEnd;
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static void GetInvoices(ElioUsers user)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer cus = payment.GetCustomer(user.CustomerStripeId);

            if (cus != null)
            {
                Xamarin.Payments.Stripe.StripeSubscription sub = payment.GetSubscription(cus.ID);
                if (sub != null)
                {
                    Xamarin.Payments.Stripe.StripeInvoice invoice = payment.GetUpcomingInvoice(cus.ID);

                    if (invoice != null)
                    {
                       

                        Xamarin.Payments.Stripe.StripeLineItem item = new StripeLineItem();
                        item.Amount = 298;
                        item.Currency = "UD";
                        item.Description = "Customer Invoice";
                        item.ID = "";
                        item.LiveMode = false;
                        //item.Plan = StripeLineItem.Pl
                        item.Quantity = 1;

                        Xamarin.Payments.Stripe.StripeInvoiceItem invItm = new StripeInvoiceItem();
                        
                    }
                }
            }

            //Xamarin.Payments.Stripe.StripeInvoice invoice = payment.GetInvoice
        }

        public static bool SubscribeUnRegisteredCustomerWithCoupon(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            Xamarin.Payments.Stripe.StripeCoupon couponDiscount,
            string planId,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string customerResponseId,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(planId);
                
                Xamarin.Payments.Stripe.StripeCustomerInfo custInfo = new StripeCustomerInfo();
                custInfo.Email = email;
                custInfo.Description = customerName;
                custInfo.Plan = planInfoResponse.ID;
                //custInfo.Coupon = couponDiscount.ID;
                custInfo.Card = new StripeCreditCardInfo();
                custInfo.Card.CVC = cardCvc;
                custInfo.Card.ExpirationMonth = expMonth;
                custInfo.Card.ExpirationYear = expYear;
                custInfo.Card.Number = cardNumber;

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(custInfo);

                //Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                //{
                //    Email = email,
                //    Description = customerName,
                //    Plan = planInfoResponse.ID,
                //    Coupon = couponDiscount.ID,
                //    Card = new StripeCreditCardInfo
                //    {
                //        CVC = cardCvc,
                //        ExpirationMonth = expMonth,
                //        ExpirationYear = expYear,
                //        Number = cardNumber
                //    }
                //    //Coupon = couponDiscount.ID
                //});

                if (!string.IsNullOrEmpty(customerResponse.ID))
                {
                    //Xamarin.Payments.Stripe.StripeDiscount disc = new Xamarin.Payments.Stripe.StripeDiscount();
                    //disc.Coupon = couponDiscount.ID;
                    //customerResponse.Discount = disc;
                    //disc.Customer = customerResponse.ID;
                    //custInfo.Coupon = couponDiscount.ID;
                    //customerResponse = payment .UpdateCustomer(customerResponse.ID, custInfo);

                    //card = customerResponse.Card;
                    customerResponseId = customerResponse.ID;
                    //customerResponse.DefaultCard = card.ID;

                    //card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    //{
                    //    CVC = cardCvc,
                    //    ExpirationMonth = expMonth,
                    //    ExpirationYear = expYear,
                    //    Number = cardNumber
                    //});

                    //if (card.CvcCheck == StripeCvcCheck.Pass)
                    //{
                    //cardFingerPrint = card.Fingerprint;
                    Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                    {
                        //Card = new StripeCreditCardInfo
                        //{
                        //    CVC = cardCvc,
                        //    ExpirationMonth = expMonth,
                        //    ExpirationYear = expYear,
                        //    Number = cardNumber
                        //},
                        Plan = planInfoResponse.ID,
                        Prorate = true
                        //Coupon = disc.Coupon
                    });

                    if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                    {
                        //Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(planInfoResponse.Amount,
                        //    planInfoResponse.Currency,
                        //    new StripeCreditCardInfo
                        //    {
                        //        CVC = cardCvc,
                        //        ExpirationMonth = expMonth,
                        //        ExpirationYear = expYear,
                        //        Number = cardNumber
                        //    },
                        //"Elioplus Premium Packet Charge");
                        //chargeId = charge.ID;

                        orderMode = subResponse.Status.ToString();
                        subscriptionStatus = subResponse.Status.ToString();
                        trialPeriodStart = subResponse.TrialStart;
                        trialPeriodEnd = subResponse.TrialEnd;

                        startDate = subResponse.Start;
                        currentPeriodStart = subResponse.CurrentPeriodStart;
                        currentPeriodEnd = subResponse.CurrentPeriodEnd;
                    }
                    //}
                    //else
                    //{

                    //}
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                DateTime? canceledAt = null;
                //bool successUnsubscription = false;
                string stripeUnsubscribeError = string.Empty;
                string defaultCreditCard = string.Empty;

                try
                {
                    if (!string.IsNullOrEmpty(customerResponseId))
                        StripeLib.UnSubscribeCustomer(ref canceledAt, customerResponseId, defaultCreditCard, ref stripeUnsubscribeError);
                }
                catch (Xamarin.Payments.Stripe.StripeException unSubscribeEx)
                {
                    Logger.DetailedError(unSubscribeEx.Message.ToString(), ex.StackTrace.ToString());
                }

                isError = true;
                errorMessage = ex.Message;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                throw ex;
            }

            return isError;
        }

        public static bool SubscribeUnRegisteredCustomerWithCouponDiscount(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            Xamarin.Payments.Stripe.StripeCoupon coupon,
            string planId,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string customerResponseId,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(planId);

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                {
                    Email = email,
                    Description = customerName,
                    Plan = planInfoResponse.ID,
                    //Card = new StripeCreditCardInfo
                    //{
                    //    CVC = cardCvc,
                    //    ExpirationMonth = expMonth,
                    //    ExpirationYear = expYear,
                    //    Number = cardNumber
                    //},
                    Coupon = coupon.ID
                });

                if (!string.IsNullOrEmpty(customerResponse.ID))
                {
                    customerResponseId = customerResponse.ID;

                    Xamarin.Payments.Stripe.StripeCard card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    });

                    if (card.CvcCheck == StripeCvcCheck.Pass)
                    {
                        cardFingerPrint = card.Fingerprint;
                        Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                        {
                            //Card = new StripeCreditCardInfo
                            //{
                            //    CVC = cardCvc,
                            //    ExpirationMonth = expMonth,
                            //    ExpirationYear = expYear,
                            //    Number = cardNumber
                            //},
                            Plan = planInfoResponse.ID,
                            Prorate = false
                        });

                        if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                        {
                            int amount = Convert.ToInt32(coupon.PercentOff) * planInfoResponse.Amount;

                            Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(amount,
                                planInfoResponse.Currency,
                                new StripeCreditCardInfo
                                {
                                    CVC = cardCvc,
                                    ExpirationMonth = expMonth,
                                    ExpirationYear = expYear,
                                    Number = cardNumber
                                },
                            "Elioplus Premium Packet Charge");
                            chargeId = charge.ID;

                            orderMode = subResponse.Status.ToString();
                            subscriptionStatus = subResponse.Status.ToString();
                            trialPeriodStart = subResponse.TrialStart;
                            trialPeriodEnd = subResponse.TrialEnd;

                            startDate = subResponse.Start;
                            currentPeriodStart = subResponse.CurrentPeriodStart;
                            currentPeriodEnd = subResponse.CurrentPeriodEnd;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static void CreateCardToken()
        {
            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCreditCardToken cardToken = payment.CreateToken(new Xamarin.Payments.Stripe.StripeCreditCardInfo
            {
                Number = "",
                CVC = "",
                FullName = "",
                ExpirationMonth = 12,
                ExpirationYear = 2023,
            });

            Xamarin.Payments.Stripe.StripeCustomer cust = payment.CreateCustomer(new StripeCustomerInfo
            {
                DefaultCardId = cardToken.Card.ID,
                
            });
        }

        public static bool SubscribeUnRegisteredCustomer(
            string stripePlan,
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string cardId,
            ref string customerResponseId,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(stripePlan);

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                {
                    Email = email,
                    Description = customerName,
                    Plan = planInfoResponse.ID,

                    Card = new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    }
                });

                if (!string.IsNullOrEmpty(customerResponse.ID))
                {
                    customerResponseId = customerResponse.ID;

                    card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    });
                    
                    if (card.CvcCheck == StripeCvcCheck.Pass)
                    {
                        cardFingerPrint = card.Fingerprint;
                        cardId = card.ID;
                        customerResponse.DefaultCard = card.ID;

                        try
                        {
                            customerResponse = UpdateCustomersDefaultCard(customerResponse.ID, card.ID, ref errorMessage);
                            //if (setDefault.DefaultCard != null)
                        }
                        catch (Xamarin.Payments.Stripe.StripeException ex)
                        {
                            errorMessage = ex.Message;
                            errorMessage = string.Format("Setting customer default card returned error {0}", errorMessage);
                        }

                        Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                        {
                            //Card = new StripeCreditCardInfo
                            //{
                            //    CVC = cardCvc,
                            //    ExpirationMonth = expMonth,
                            //    ExpirationYear = expYear,
                            //    Number = cardNumber
                            //},
                            Plan = planInfoResponse.ID,
                            Prorate = false
                        });

                        if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                        {
                            //Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(planInfoResponse.Amount,
                            //    planInfoResponse.Currency,
                            //    new StripeCreditCardInfo
                            //    {
                            //        CVC = cardCvc,
                            //        ExpirationMonth = expMonth,
                            //        ExpirationYear = expYear,
                            //        Number = cardNumber
                            //    },
                            //"Elioplus Premium Packet Charge");
                            //chargeId = charge.ID;

                            orderMode = subResponse.Status.ToString();
                            subscriptionStatus = subResponse.Status.ToString();
                            trialPeriodStart = subResponse.TrialStart;
                            trialPeriodEnd = subResponse.TrialEnd;

                            startDate = subResponse.Start;
                            currentPeriodStart = subResponse.CurrentPeriodStart;
                            currentPeriodEnd = subResponse.CurrentPeriodEnd;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                if (!string.IsNullOrEmpty(customerResponseId))
                {
                    Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                    try
                    {
                        if (!string.IsNullOrEmpty(customerResponseId))
                        {
                            Xamarin.Payments.Stripe.StripeSubscription customerSubscription = payment.Unsubscribe(customerResponseId, false);
                            Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.DeleteCustomer(customerResponseId);
                        }
                    }
                    catch (Xamarin.Payments.Stripe.StripeException exDeleteCustomer)
                    {
                        errorMessage = exDeleteCustomer.Message;
                    }
                }

                isError = true;
                errorMessage += " " + ex.Message;
            }

            return isError;
        }

        private static string FixCustomerEmail(string email, int customerId)
        {
            string[] emailArgs = email.Split('@').ToArray();

            if (emailArgs.Length > 1)
                return email = emailArgs[0] + "_" + customerId.ToString() + "@" + emailArgs[1];
            else
                return customerId.ToString() + "_" + email;
        }

        public static bool SubscribeVirtualUnRegisteredCustomerToService(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string cardId,
            ref string customerResponseId,
            ref string customerStripeServiceSubscriptionEmail,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(StripePlans.Elio_Premium_Service_Plan.ToString());

                //email = FixCustomerEmail(email, customerId);

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                {
                    Email = email,
                    Description = customerName,
                    Plan = planInfoResponse.ID
                    //Card = new StripeCreditCardInfo
                    //{
                    //    CVC = cardCvc,
                    //    ExpirationMonth = expMonth,
                    //    ExpirationYear = expYear,
                    //    Number = cardNumber
                    //}
                });

                if (!string.IsNullOrEmpty(customerResponse.ID))
                {
                    customerResponseId = customerResponse.ID;
                    customerStripeServiceSubscriptionEmail = email;

                    card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    });

                    if (card.CvcCheck == StripeCvcCheck.Pass)
                    {
                        cardFingerPrint = card.Fingerprint;
                        cardId = card.ID;

                        Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                        {
                            //Card = new StripeCreditCardInfo
                            //{
                            //    CVC = cardCvc,
                            //    ExpirationMonth = expMonth,
                            //    ExpirationYear = expYear,
                            //    Number = cardNumber
                            //},
                            Plan = planInfoResponse.ID,
                            Prorate = false
                        });

                        if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                        {
                            orderMode = subResponse.Status.ToString();
                            subscriptionStatus = subResponse.Status.ToString();
                            trialPeriodStart = subResponse.TrialStart;
                            trialPeriodEnd = subResponse.TrialEnd;

                            startDate = subResponse.Start;
                            currentPeriodStart = subResponse.CurrentPeriodStart;
                            currentPeriodEnd = subResponse.CurrentPeriodEnd;
                        }
                    }
                    else
                    {

                    }
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static bool SubscribeRegisteredCustomerToService(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            int customerId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string cardId,
            ref string customerResponseId,
            ref string customerStripeServiceSubscriptionEmail,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(StripePlans.Elio_Premium_Service_Plan.ToString());

                if (customerId != 2386)
                    email = FixCustomerEmail(email, customerId);

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(new StripeCustomerInfo
                {
                    Email = email,
                    Description = customerName,
                    Plan = planInfoResponse.ID,
                    Card = new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    }
                });

                if (!string.IsNullOrEmpty(customerResponse.ID))
                {
                    customerResponseId = customerResponse.ID;
                    customerStripeServiceSubscriptionEmail = email;

                    card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    });

                    if (card.CvcCheck == StripeCvcCheck.Pass)
                    {
                        cardFingerPrint = card.Fingerprint;
                        cardId = card.ID;

                        Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                        {
                            //Card = new StripeCreditCardInfo
                            //{
                            //    CVC = cardCvc,
                            //    ExpirationMonth = expMonth,
                            //    ExpirationYear = expYear,
                            //    Number = cardNumber
                            //},
                            Plan = planInfoResponse.ID,
                            Prorate = false
                        });

                        if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                        {
                            orderMode = subResponse.Status.ToString();
                            subscriptionStatus = subResponse.Status.ToString();
                            trialPeriodStart = subResponse.TrialStart;
                            trialPeriodEnd = subResponse.TrialEnd;

                            startDate = subResponse.Start;
                            currentPeriodStart = subResponse.CurrentPeriodStart;
                            currentPeriodEnd = subResponse.CurrentPeriodEnd;
                        }
                    }
                    else
                    {

                    }

                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static bool ReActivateRegisteredCustomerToService(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            string customerStripeId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string cardId,
            ref string customerStripeServiceSubscriptionEmail,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(StripePlans.Elio_Premium_Service_Plan.ToString());

                Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.GetCustomer(customerStripeId);

                if (customerResponse.Deleted == false)
                {
                    if (!string.IsNullOrEmpty(customerResponse.ID))
                    {
                        customerStripeServiceSubscriptionEmail = customerResponse.Email;

                        card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                        {
                            CVC = cardCvc,
                            ExpirationMonth = expMonth,
                            ExpirationYear = expYear,
                            Number = cardNumber
                        });

                        if (card.CvcCheck == StripeCvcCheck.Pass)
                        {
                            cardFingerPrint = card.Fingerprint;
                            cardId = card.ID;

                            Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                            {
                                Plan = planInfoResponse.ID,
                                Prorate = false
                            });

                            if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                            {
                                orderMode = subResponse.Status.ToString();
                                subscriptionStatus = subResponse.Status.ToString();
                                trialPeriodStart = subResponse.TrialStart;
                                trialPeriodEnd = subResponse.TrialEnd;

                                startDate = subResponse.Start;
                                currentPeriodStart = subResponse.CurrentPeriodStart;
                                currentPeriodEnd = subResponse.CurrentPeriodEnd;
                            }
                        }
                        else
                        {
                            isError = true;
                            errorMessage = "Card Cvc Check status is not Pass";
                        }
                    }
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static bool SubscribeRegisteredCustomer(
            string stripePlan,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            string customerStripeId,
            ref string errorMessage,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode,
            ref Xamarin.Payments.Stripe.StripeCard card)
        {
            bool isError = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.GetCustomer(customerStripeId);

            if (customerResponse.Deleted == false)
            {
                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(stripePlan);
                if (planInfoResponse != null)
                {
                    card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                    {
                        CVC = cardCvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = cardNumber
                    });

                    if (card.CvcCheck == StripeCvcCheck.Pass)
                    {
                        //cardFingerPrint = card.Fingerprint;
                        //cardId = card.ID;
                        customerResponse.DefaultCard = card.ID;

                        try
                        {
                            customerResponse = UpdateCustomersDefaultCard(customerResponse.ID, card.ID, ref errorMessage);
                            //if (setDefault.DefaultCard != null)
                        }
                        catch (Xamarin.Payments.Stripe.StripeException ex)
                        {
                            errorMessage = ex.Message;
                            errorMessage = string.Format("Setting customer default card returned error {0}", errorMessage);
                        }

                        Xamarin.Payments.Stripe.StripeSubscription newSub = payment.Subscribe(customerStripeId, new StripeSubscriptionInfo
                        {
                            //Card = new StripeCreditCardInfo
                            //{
                            //    CVC = cardCvc,
                            //    ExpirationMonth = expMonth,
                            //    ExpirationYear = expYear,
                            //    Number = cardNumber
                            //},
                            Plan = planInfoResponse.ID,
                            Prorate = false
                        });

                        orderMode = newSub.Status.ToString();

                        //Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(planInfoResponse.Amount,
                        //    planInfoResponse.Currency,
                        //    new StripeCreditCardInfo
                        //    {
                        //        CVC = cardCvc,
                        //        ExpirationMonth = expMonth,
                        //        ExpirationYear = expYear,
                        //        Number = cardNumber
                        //    },
                        //"Elioplus Premium Packet Charge");
                        //chargeId = charge.ID;

                        startDate = newSub.Start;

                        //Xamarin.Payments.Stripe.StripeSubscription sub = payment.GetSubscription(customerStripeId);

                        if (newSub.Status == StripeSubscriptionStatus.Active)
                        {
                            currentPeriodStart = newSub.CurrentPeriodStart;
                            currentPeriodEnd = newSub.CurrentPeriodEnd;

                            isError = false;
                        }
                        else if (newSub.Status == StripeSubscriptionStatus.Trialing)
                        {
                            trialPeriodStart = newSub.TrialStart;
                            trialPeriodEnd = newSub.TrialEnd;

                            isError = false;
                        }
                    }
                }
                else
                {
                    //error: plan did not created
                }
            }
            else
            {

            }

            return isError;
        }

        public static bool SubscribeRegisteredCustomerToServiceWithNewSubscription(
            string email,
            string cardNumber,
            string cardCvc,
            int expMonth,
            int expYear,
            string customerStripeId,
            string customerName,
            ref string errorMessage,
            ref string cardFingerPrint,
            ref string cardId,
            ref string customerResponseId,
            ref string chargeId,
            ref DateTime? startDate,
            ref DateTime? currentPeriodStart,
            ref DateTime? currentPeriodEnd,
            ref string subscriptionStatus,
            ref DateTime? trialPeriodStart,
            ref DateTime? trialPeriodEnd,
            ref string orderMode)
        {
            bool isError = false;

            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan planInfoResponse = payment.GetPlan(StripePlans.Elio_Premium_Service_Plan.ToString());

                Xamarin.Payments.Stripe.StripeCustomer customer = payment.GetCustomer(customerStripeId);

                if (customer.Deleted == false)
                {
                    Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customer.ID, new StripeSubscriptionInfo
                    {
                        //Card = new StripeCreditCardInfo
                        //{
                        //    CVC = cardCvc,
                        //    ExpirationMonth = expMonth,
                        //    ExpirationYear = expYear,
                        //    Number = cardNumber
                        //},
                        Plan = planInfoResponse.ID,
                        Prorate = false,
                    });

                    if (subResponse.Status == StripeSubscriptionStatus.Active)
                    {
                        //Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(planInfoResponse.Amount,
                        //    planInfoResponse.Currency,
                        //    new StripeCreditCardInfo
                        //    {
                        //        CVC = cardCvc,
                        //        ExpirationMonth = expMonth,
                        //        ExpirationYear = expYear,
                        //        Number = cardNumber
                        //    },
                        //"Elioplus Premium Service Charge");
                        //chargeId = charge.ID;

                        orderMode = subResponse.Status.ToString();
                        subscriptionStatus = subResponse.Status.ToString();
                        startDate = subResponse.Start;
                        currentPeriodStart = subResponse.CurrentPeriodStart;
                        currentPeriodEnd = subResponse.CurrentPeriodEnd;
                    }
                }
                else
                {
                    isError = true;
                    errorMessage = "Sorry, something went wrong. You could not be charged. Contact with us.";
                }
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                isError = true;
                errorMessage = ex.Message;
            }

            return isError;
        }

        public static Xamarin.Payments.Stripe.StripeCoupon CreateGetCoupon(StripeCouponDuration couponDuration, int maxRedemptions, string couponId, int percent, int monthDuration, DateTime redeemBy)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCoupon couponResponse = null;
            try
            {
                couponResponse = payment.GetCoupon(couponId);
            }
            catch (Exception ex)
            {
                if (ex.Message == "No such coupon: " + couponId)
                {
                    couponResponse = payment.CreateCoupon(new StripeCouponInfo
                    {
                        Duration = couponDuration,
                        MaxRedemptions = maxRedemptions,
                        ID = couponId,
                        PercentOff = percent,
                        MonthsForDuration = monthDuration,
                        RedeemBy = redeemBy
                    });
                }
                else
                    throw ex;
            }

            return couponResponse;
        }

        public static Xamarin.Payments.Stripe.StripeCoupon GetCoupon(string couponId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCoupon stripeCouponResponse = payment.GetCoupon(couponId);

            return stripeCouponResponse;
        }

        public static StripeCustomerInfo CreateCustomer(string email, string customerName, string planId, StripeCreditCardInfo card)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StripeCustomerInfo sCustomer = new StripeCustomerInfo();

            //    sCustomer.Card = card;
            //    sCustomer.Email = email;
            //    sCustomer.Description = customerName;
            //    sCustomer.Plan = planId;

            return sCustomer;
        }

        public static StripeCreditCardInfo CreateCard(string cvc, int expMonth, int expYear, string ccNumber)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StripeCreditCardInfo cc = new StripeCreditCardInfo();

            //    cc.CVC = cvc;
            //    cc.ExpirationMonth = expMonth;
            //    cc.ExpirationYear = expYear;
            //    cc.Number = ccNumber;

            return cc;
        }

        public static ChargeCreateOptions CreateCharge(int packetPrice, string currency, string customerId, string chargeDescription)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            ChargeCreateOptions options = new ChargeCreateOptions();

            //    options.Amount = packetPrice;
            //    options.Currency = currency;
            //    options.CustomerId = customerId;
            //    options.Description = chargeDescription;

            return options;
        }

        public static Xamarin.Payments.Stripe.StripeCharge Charge(int packetPrice, string currency, string customerId, string chargeDescription, bool chargeMode, bool isPaid)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripeCharge options = new Xamarin.Payments.Stripe.StripeCharge();
            
            //    options.Amount = packetPrice;
            //    options.Currency = currency;
            //    options.Description = chargeDescription;
            //    options.Created = DateTime.Now;
            //    options.Customer = customerId;
            //    options.LiveMode = chargeMode;
            //    options.Paid = isPaid;

            return options;
        }

        public static StripeSubscriptionInfo CreateSubscription(StripeCreditCardInfo creditCardInfo, string planId, bool prorate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StripeSubscriptionInfo subInfo = new StripeSubscriptionInfo();

            //    subInfo.Card = creditCardInfo;
            //    subInfo.Plan = planId;
            //    subInfo.Prorate = prorate;

            return subInfo;
        }

        public static TokenCreateOptions GetToken(TokenCreateOptions cc, string customerId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            TokenCreateOptions opt = new TokenCreateOptions();
            
            //    opt.Card = cc;
            //    opt.CustomerId = customerId;

            return opt;
        }

        public static StripePlanInfo CreatePlan()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StripePlanInfo plan = new StripePlanInfo();

            plan.Amount = 9900;
            plan.Currency = "usd";
            plan.ID = "Elio_Premium_Plan";
            plan.Name = "Elioplus_Plan";
            plan.TrialPeriod = 0;
            plan.Interval = StripePlanInterval.Month;

            return plan;
        }

        public static Xamarin.Payments.Stripe.StripeSubscription GetCustomerSubscriptionInfo(ref DateTime? startDate, ref DateTime? currentPeriodStart, ref DateTime? currentPeriodEnd, ref DateTime? trialPeriodStart, ref DateTime? trialPeriodEnd, ref DateTime? canceledAt, ref string orderMode, string customerId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeSubscription sub = new Xamarin.Payments.Stripe.StripeSubscription();
            try
            {
                sub = payment.GetSubscription(customerId);
            }
            catch (Exception ex)
            {
                if (ex.Message.Trim() == "No active subscriptions for customer:")
                {
                    if (sub.Status == StripeSubscriptionStatus.Unknown)
                    {
                        startDate = sub.Start = DateTime.Now;
                        currentPeriodStart = sub.TrialStart = DateTime.Now;
                        currentPeriodEnd = sub.TrialEnd = DateTime.Now.AddMonths(1);
                        canceledAt = sub.CanceledAt = DateTime.Now;
                        orderMode = StripeSubscriptionStatus.Unknown.ToString();

                        return sub;
                    }
                }
            }

            startDate = sub.Start;

            if (sub.Status == StripeSubscriptionStatus.Active)
            {
                currentPeriodStart = sub.CurrentPeriodStart;
                currentPeriodEnd = sub.CurrentPeriodEnd;

                orderMode = StripeSubscriptionStatus.Active.ToString();
            }
            else if (sub.Status == StripeSubscriptionStatus.Trialing)
            {
                trialPeriodStart = sub.TrialStart;
                trialPeriodEnd = sub.TrialEnd;

                orderMode = StripeSubscriptionStatus.Trialing.ToString();
            }
            else if (sub.Status == StripeSubscriptionStatus.Canceled)
            {
                currentPeriodStart = sub.TrialStart;
                currentPeriodEnd = sub.TrialEnd;
                canceledAt = sub.CanceledAt;

                orderMode = StripeSubscriptionStatus.Canceled.ToString();
            }
            else if (sub.Status == StripeSubscriptionStatus.Unpaid)
            {

            }
            else if (sub.Status == StripeSubscriptionStatus.Unknown)
            {

            }

            return sub;
        }

        public static bool GetSubscriptionAndUnSubscribeCustomer(ref  DateTime? canceledAt, string customerId, ref string error)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            bool successUnsubscription = false;

            Xamarin.Payments.Stripe.StripeSubscription sub = payment.GetSubscription(customerId);

            try
            {

            }
            catch (Exception ex)
            {
                error = ex.Message;

                if (error == string.Format("No such customer: {0}", customerId) || error == string.Format("No active subscriptions for customer: {0}", customerId))
                    successUnsubscription = true;

                Logger.DetailedError("StripeLib", ex.Message.ToString(), ex.StackTrace.ToString());
            }

            if (sub.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Canceled)
            {
                //Xamarin.Payments.Stripe.StripeCustomer customer = payment.DeleteCustomer(customerId);
                //canceledAt = sub.CanceledAt;
                //if (customer.Deleted)
                //{
                successUnsubscription = true;
                //}
            }

            return successUnsubscription;
        }

        public static bool UnSubscribeCustomer(ref  DateTime? canceledAt, string customerId, string defaultCreditCard, ref string error)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            bool successUnsubscription = false;

            Xamarin.Payments.Stripe.StripeSubscription sub = new Xamarin.Payments.Stripe.StripeSubscription();

            try
            {
                sub = payment.Unsubscribe(customerId, false);
            }
            catch (Exception ex)
            {
                error = ex.Message;

                if (error == string.Format("No such customer: {0}", customerId) || error == string.Format("No active subscriptions for customer: {0}", customerId))
                    successUnsubscription = true;

                successUnsubscription = true;
                Logger.DetailedError("StripeLib", ex.Message.ToString(), ex.StackTrace.ToString());
            }

            if (sub.Status == Xamarin.Payments.Stripe.StripeSubscriptionStatus.Canceled)
            {
                successUnsubscription = true;

                if (!string.IsNullOrEmpty(defaultCreditCard))
                {
                    try
                    {
                        Xamarin.Payments.Stripe.StripeCard card = payment.DeleteCard(customerId, defaultCreditCard);
                        if (card.Deleted)
                            successUnsubscription = true;
                        else
                            error = "Credit Card could not be deleted";
                    }
                    catch (Xamarin.Payments.Stripe.StripeException ex)
                    {
                        error = ex.Message;
                    }
                    //Xamarin.Payments.Stripe.StripeCustomer customer = payment.DeleteCustomer(customerId);
                    //canceledAt = sub.CanceledAt;
                    //if (customer.Deleted)
                    //{

                    //}
                }
                else
                {
                    error = "User unsubscribed successfully but he had no credit card registered in ElioUsersCreditCards table";
                }
            }

            return successUnsubscription;
        }

        public static Xamarin.Payments.Stripe.StripePlan GetPlanById(string planId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripePlan plan = payment.GetPlan(planId);
            
            return (plan != null) ? plan : null;
        }

        public static Xamarin.Payments.Stripe.StripePlanInfo CreatePlan(Xamarin.Payments.Stripe.StripePayment payment)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePlanInfo plan = new Xamarin.Payments.Stripe.StripePlanInfo();

            plan.ID = "Elio_Premium_Plan";
            plan.Currency = "USD";
            plan.Name = "Elioplus_Plan";
            plan.TrialPeriod = 1;
            plan.Interval = Xamarin.Payments.Stripe.StripePlanInterval.Month;
            plan.Amount = 99;

            Xamarin.Payments.Stripe.StripePlan oldPlan = payment.GetPlan(plan.ID);

            payment.CreatePlan(plan);

            return plan;
        }

        public static Xamarin.Payments.Stripe.StripeCard AddNewCreditCard(string mode, string customerId, string defaultCard, string cvc, int expMonth, int expYear, string ccNumber, string customerCardFullName, string address1, string address2, string country, string zipCode, ref string errorMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer customer = payment.GetCustomer(customerId);

            if (customer.Deleted == false)
            {
                Xamarin.Payments.Stripe.StripeCard deleteCard = null;

                bool deleted = false;
                if (mode == "DELETE")
                {
                    deleteCard = payment.DeleteCard(customer.ID, defaultCard);

                    if (!deleteCard.Deleted)
                    {
                        return null;
                    }
                    else
                        deleted = deleteCard.Deleted;
                }
                else if (mode == "ADD")
                {
                    deleted = true;
                }

                if (deleted)
                {
                    Xamarin.Payments.Stripe.StripeCard newCard = payment.CreateCard(customer.ID, new StripeCreditCardInfo
                    {
                        CVC = cvc,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        Number = ccNumber,
                        FullName = customerCardFullName,
                        AddressLine1 = address1,
                        AddressLine2 = address2,
                        Country = country,
                        ZipCode = zipCode
                    });

                    if (newCard.CvcCheck == StripeCvcCheck.Pass)
                    {
                        Xamarin.Payments.Stripe.StripeCustomer setDefault = UpdateCustomersDefaultCard(customer.ID, newCard.ID, ref errorMessage);
                        if (setDefault.DefaultCard != null)
                            return newCard;
                        else
                        {
                            errorMessage = string.Format("Setting customer default card returned error {0}", errorMessage);
                            return newCard;
                        }
                    }
                    else
                    {
                        errorMessage = string.Format("Stripe Cvc Check did not pass but return {0}", newCard.CvcCheck.ToString());
                        return null;
                    }
                }
                else
                {
                    errorMessage = string.Format("Card with ID {0}, could not be deleted", defaultCard);
                    return null;
                }
            }
            else
            {
                errorMessage = string.Format("Customer with ID {0}, is deleted from Stripe", customerId);
                return null;
            }
        }

        public static Xamarin.Payments.Stripe.StripeCard UpdateCreditCard(string customerId, string cardId, int expMonth, int expYear, string customerCardFullName, string address1, string address2, string country, string zipCode, ref string errorMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer customer = payment.GetCustomer(customerId);

            if (customer.Deleted == false)
            {
                try
                {
                    Xamarin.Payments.Stripe.StripeCard updateCard = payment.UpdateCard(customer.ID, new StripeUpdateCreditCardInfo
                    {
                        ID = cardId,
                        AddressLine1 = address1,
                        AddressLine2 = address2,
                        ExpirationMonth = expMonth,
                        ExpirationYear = expYear,
                        FullName = customerCardFullName,
                        Country = country,
                        ZipCode = zipCode
                    });

                    return updateCard;
                }
                catch (Xamarin.Payments.Stripe.StripeException ex)
                {
                    errorMessage = ex.Message;
                    return null;
                }
            }
            else
            {
                errorMessage = string.Format("Customer with ID {0}, is deleted from Stripe", customerId);
                return null;
            }
        }

        public static Xamarin.Payments.Stripe.StripeCustomer UpdateCustomersDefaultCard(string customerId, string cardId, ref string errorMessage)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer updateCustomer = payment.UpdateCustomer(customerId, new StripeCustomerInfo
            {
                DefaultCardId = cardId
            });

            updateCustomer.DefaultCard = cardId;

            return updateCustomer;
        }

        public static Xamarin.Payments.Stripe.StripeInvoice GetCustomerInvoice(string invoiceID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeSubscription sub = payment.GetSubscription("");

            
            Xamarin.Payments.Stripe.StripeInvoice invoice = payment.GetInvoice(invoiceID);

            return invoice;
        }

        public static ChargeCreateOptions GetCharges(string customerId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            ChargeCreateOptions options = new ChargeCreateOptions();

            //    options.Amount = packetPrice;
            //    options.Currency = currency;
            //    options.CustomerId = customerId;
            //    options.Description = chargeDescription;

            //IEnumerable<Xamarin.Payments.Stripe.StripeCharge> charges = payment.GetCharges(1, 10, customerId, null);

            //var ch = payment.GetCharges(1, 10, customerId, null);

            return options;
        }
    }
}