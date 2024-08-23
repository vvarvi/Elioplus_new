
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using Xamarin.Payments.Stripe;

namespace WdS.ElioPlus.Lib.StripePayment
{
    public class ws_Stripe
    {
        Xamarin.Payments.Stripe.StripePayment Payment;     //new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

        string StripeSecretKey;

        public ws_Stripe(Xamarin.Payments.Stripe.StripePayment payment, string stripeSecretKey)
        {
            Payment = payment;
            StripeSecretKey = stripeSecretKey;
        }

        public static Xamarin.Payments.Stripe.StripePlanInfo CreatePlanInfo()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlanInfo plan = new Xamarin.Payments.Stripe.StripePlanInfo();

                plan.ID = "My_Startup_Plan";
                plan.Currency = "USD";
                plan.Name = "My_Elioplus_Startup_Plan";
                plan.TrialPeriod = 0;
                plan.Interval = Xamarin.Payments.Stripe.StripePlanInterval.Month;
                plan.Amount = 298;
                plan.IntervalCount = 0;

                payment.CreatePlan(plan);

                return plan;
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        public static Xamarin.Payments.Stripe.StripePlan CreatePlan()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

                Xamarin.Payments.Stripe.StripePlan plan = new Xamarin.Payments.Stripe.StripePlan();

                plan.ID = "My_Startup_Plan";
                plan.Currency = "USD";
                plan.Name = "My_Elioplus_Startup_Plan";
                plan.TrialPeriod = 0;
                plan.Interval = Xamarin.Payments.Stripe.StripePlanInterval.Month;
                plan.Amount = 298;
                plan.LiveMode = false;
                
                return plan;
            }
            catch (Xamarin.Payments.Stripe.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        public static StripePlan CreatePlan(string planName, string planID, int amount, string currency, int? trialPeriod, StripePlanInterval interval, int? intervalCount)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            StripePlanInfo plan = new StripePlanInfo();

            plan.Name = planName;
            plan.ID = planID;
            plan.Amount = amount;
            plan.Currency = currency;
            plan.TrialPeriod = trialPeriod;
            plan.Interval = interval;
            plan.IntervalCount = intervalCount;

            return payment.CreatePlan(plan);
        }

        public static StripePlan GetPlanByID(string planID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            return payment.GetPlan(planID);
        }

        public static StripePlan GetPlanByPlanInfo(StripePlanInfo plan)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            return payment.GetPlan(plan.ID);
        }

        public static StripeCoupon CreateCouponInfo(string ID, StripeCouponDuration duration, int maxRedemptions, int monthsForDuration, int percentOff, DateTime redeemBy)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            StripeCouponInfo couponInfo = new StripeCouponInfo();

            couponInfo.ID = ID;
            couponInfo.Duration = duration;
            couponInfo.MaxRedemptions = percentOff;
            couponInfo.MonthsForDuration = monthsForDuration;
            couponInfo.PercentOff = percentOff;
            couponInfo.RedeemBy = redeemBy;

            return payment.CreateCoupon(couponInfo);
        }

        public static StripeCreditCardInfo CreateCard()
        {
            StripeCreditCardInfo card = new StripeCreditCardInfo();

            return card;
        }

        public static StripeCoupon CreateCoupon(string couponID, int? amountOff, int? percentOff, bool? isDeleted, StripeCouponDuration duration, bool liveMode, int timesRedeemed)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            StripeCoupon coupon = new StripeCoupon();

            coupon.ID = couponID;
            coupon.AmountOff = amountOff;
            coupon.PercentOff = percentOff;
            coupon.Deleted = isDeleted;
            coupon.Duration = duration;
            coupon.LiveMode = liveMode;
            coupon.TimesRedeemed = timesRedeemed;

            return coupon;
        }

        public static StripeCustomerInfo CreateCustomer(string description, string email, string coupon, StripeCreditCardInfo card, string defaultCard, string plan, DateTime? trialEnd, bool? validate)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomerInfo customerInfo = new StripeCustomerInfo();

            customerInfo.Description = description;
            customerInfo.Email = email;
            customerInfo.Coupon = coupon;
            customerInfo.Card = card;
            customerInfo.DefaultCardId = defaultCard;
            customerInfo.Plan = plan;
            customerInfo.TrialEnd = trialEnd;
            customerInfo.Validate = validate;
            
            payment.CreateCustomer(customerInfo);

            return customerInfo;
        }

        public static StripeCustomer GetCustomer(string customerStripeId)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            Xamarin.Payments.Stripe.StripePayment payment = new Xamarin.Payments.Stripe.StripePayment(ConfigurationManager.AppSettings["StripeSecretKey"]);

            Xamarin.Payments.Stripe.StripeCustomer customer = payment.GetCustomer(customerStripeId);

            return customer;
        }

        public static StripeSubscriptionInfo GetSubscription()
        {
            StripeSubscriptionInfo sub = new StripeSubscriptionInfo();

            return sub;
        }

        public static bool SubscribeCustomer(
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

                Xamarin.Payments.Stripe.StripePlan planInfo = GetPlanByID(planId);

                if (planInfo != null)
                {
                    StripeCreditCardInfo cardInfo = CreateCard();

                    StripeCustomerInfo customerInfo = CreateCustomer(customerName, email, "", cardInfo, "", planInfo.ID, null, true);

                    if (customerInfo != null)
                    {
                        StripeCustomer customer = new StripeCustomer();

                        StripeSubscriptionInfo subscriptionInfo = GetSubscription();

                        if (subscriptionInfo != null)
                        {

                            StripeSubscription subscription = payment.Subscribe(customer.ID, subscriptionInfo);
                        }
                    }
                }

                //Xamarin.Payments.Stripe.StripeCustomerInfo custInfo = new StripeCustomerInfo();
                //custInfo.Email = email;
                //custInfo.Description = customerName;
                //custInfo.Plan = planInfoResponse.ID;
                ////custInfo.Coupon = couponDiscount.ID;
                //custInfo.Card = new StripeCreditCardInfo();
                //custInfo.Card.CVC = cardCvc;
                //custInfo.Card.ExpirationMonth = expMonth;
                //custInfo.Card.ExpirationYear = expYear;
                //custInfo.Card.Number = cardNumber;

                //Xamarin.Payments.Stripe.StripeCustomer customerResponse = payment.CreateCustomer(custInfo);

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

                //if (!string.IsNullOrEmpty(customerResponse.ID))
                //{
                //    //Xamarin.Payments.Stripe.StripeDiscount disc = new Xamarin.Payments.Stripe.StripeDiscount();
                //    //disc.Coupon = couponDiscount.ID;
                //    //customerResponse.Discount = disc;
                //    //disc.Customer = customerResponse.ID;
                //    //custInfo.Coupon = couponDiscount.ID;
                //    //customerResponse = payment .UpdateCustomer(customerResponse.ID, custInfo);

                //    //card = customerResponse.Card;
                //    customerResponseId = customerResponse.ID;
                //    //customerResponse.DefaultCard = card.ID;

                //    //card = payment.CreateCard(customerResponse.ID.ToString(), new StripeCreditCardInfo
                //    //{
                //    //    CVC = cardCvc,
                //    //    ExpirationMonth = expMonth,
                //    //    ExpirationYear = expYear,
                //    //    Number = cardNumber
                //    //});

                //    //if (card.CvcCheck == StripeCvcCheck.Pass)
                //    //{
                //    //cardFingerPrint = card.Fingerprint;
                //    Xamarin.Payments.Stripe.StripeSubscription subResponse = payment.Subscribe(customerResponse.ID, new StripeSubscriptionInfo
                //    {
                //        //Card = new StripeCreditCardInfo
                //        //{
                //        //    CVC = cardCvc,
                //        //    ExpirationMonth = expMonth,
                //        //    ExpirationYear = expYear,
                //        //    Number = cardNumber
                //        //},
                //        Plan = planInfoResponse.ID,
                //        Prorate = true
                //        //Coupon = disc.Coupon
                //    });

                //    if (subResponse.Status == StripeSubscriptionStatus.Trialing || subResponse.Status == StripeSubscriptionStatus.Active)
                //    {
                //        //Xamarin.Payments.Stripe.StripeCharge charge = payment.Charge(planInfoResponse.Amount,
                //        //    planInfoResponse.Currency,
                //        //    new StripeCreditCardInfo
                //        //    {
                //        //        CVC = cardCvc,
                //        //        ExpirationMonth = expMonth,
                //        //        ExpirationYear = expYear,
                //        //        Number = cardNumber
                //        //    },
                //        //"Elioplus Premium Packet Charge");
                //        //chargeId = charge.ID;

                //        orderMode = subResponse.Status.ToString();
                //        subscriptionStatus = subResponse.Status.ToString();
                //        trialPeriodStart = subResponse.TrialStart;
                //        trialPeriodEnd = subResponse.TrialEnd;

                //        startDate = subResponse.Start;
                //        currentPeriodStart = subResponse.CurrentPeriodStart;
                //        currentPeriodEnd = subResponse.CurrentPeriodEnd;
                //    }
                //    //}
                //    //else
                //    //{

                //    //}
                //}
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
    }
}