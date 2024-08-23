using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packets_stripe_coupons
    /// </summary>
    [ClassInfo("Elio_packets_stripe_coupons")]
    public partial class ElioPacketsStripeCoupons : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : coupon_id
        /// </summary>
        [FieldInfo("coupon_id")]
        public string CouponId { get; set; }
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : stripe_plan_id
        /// </summary>
        [FieldInfo("stripe_plan_id")]
        public string StripePlanId { get; set; }
        /// <summary>
        /// Database Field : name
        /// </summary>
        [FieldInfo("name")]
        public string Name { get; set; }
        /// <summary>
        /// Database Field : duration
        /// </summary>
        [FieldInfo("duration")]
        public string Duration { get; set; }
        /// <summary>
        /// Database Field : amount_off
        /// </summary>
        [FieldInfo("amount_off")]
        public float AmountOff { get; set; }
        /// <summary>
        /// Database Field : currency
        /// </summary>
        [FieldInfo("currency")]
        public string Currency { get; set; }
        /// <summary>
        /// Database Field : duration_in_months
        /// </summary>
        [FieldInfo("duration_in_months")]
        public int DurationInMonths { get; set; }
        /// <summary>
        /// Database Field : max_redemptions
        /// </summary>
        [FieldInfo("max_redemptions")]
        public int MaxRedemptions { get; set; }
        /// <summary>
        /// Database Field : percent_off
        /// </summary>
        [FieldInfo("percent_off")]
        public float PercentOff { get; set; }
        /// <summary>
        /// Database Field : redeem_by
        /// </summary>
        [FieldInfo("redeem_by")]
        public DateTime RedeemBy { get; set; }
        /// <summary>
        /// Database Field : date_created
        /// </summary>
        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }
    }
}