using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_subscriptions
    /// </summary>
    [ClassInfo("Elio_users_subscriptions")]
    public partial class ElioUsersSubscriptions : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : customer_id
        /// </summary>
        [FieldInfo("customer_id")]
        public string CustomerId { get; set; }
        /// <summary>
        /// Database Field : subscription_id
        /// </summary>
        [FieldInfo("subscription_id")]
        public string SubscriptionId { get; set; }
        /// <summary>
        /// Database Field : coupon_id
        /// </summary>
        [FieldInfo("coupon_id")]
        public string CouponId { get; set; }
        /// <summary>
        /// Database Field : plan_id
        /// </summary>
        [FieldInfo("plan_id")]
        public string PlanId { get; set; }
        /// <summary>
        /// Database Field : plan_nickname
        /// </summary>
        [FieldInfo("plan_nickname")]
        public string PlanNickname { get; set; }        
        /// <summary>
        /// Database Field : created_at
        /// </summary>
        [FieldInfo("created_at")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Database Field : current_period_start
        /// </summary>
        [FieldInfo("current_period_start")]
        public DateTime CurrentPeriodStart { get; set; }
        /// <summary>
        /// Database Field : current_period_end
        /// </summary>
        [FieldInfo("current_period_end")]
        public DateTime CurrentPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : canceled_at
        /// </summary>
        [FieldInfo("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        /// <summary>
        /// Database Field : has_discount
        /// </summary>
        [FieldInfo("has_discount")]
        public int HasDiscount { get; set; }
        /// <summary>
        /// Database Field : status
        /// </summary>
        [FieldInfo("status")]
        public string Status { get; set; }
        /// <summary>
        /// Database Field : trial_period_start
        /// </summary>
        [FieldInfo("trial_period_start")]
        public DateTime? TrialPeriodStart { get; set; }
        /// <summary>
        /// Database Field : trial_period_end
        /// </summary>
        [FieldInfo("trial_period_end")]
        public DateTime? TrialPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : cancel_at_period_end
        /// </summary>
        [FieldInfo("cancel_at_period_end")]
        public int CancelAtPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : amount
        /// </summary>
        [FieldInfo("amount")]
        public int Amount { get; set; }
    }
}
