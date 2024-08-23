using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_plan_coupons
    /// </summary>
    [ClassInfo("Elio_plan_coupons")]
    public partial class ElioPlanCoupons : DataObject
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
        /// <summary>
        /// Database Field : packet_id
        /// </summary>
        [FieldInfo("packet_id")]
        public int PacketId { get; set; }
        /// <summary>
        /// Database Field : month_duration
        /// </summary>
        [FieldInfo("month_duration")]
        public int MonthDuration { get; set; }
        /// <summary>
        /// Database Field : plan_id
        /// </summary>
        [FieldInfo("plan_id")]
        public string PlanId { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : redeemBy
        /// </summary>
        [FieldInfo("redeemBy")]
        public DateTime RedeemBy { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : maxRedemptions
        /// </summary>
        [FieldInfo("maxRedemptions")]
        public int MaxRedemptions { get; set; }
        /// <summary>
        /// Database Field : percentOff
        /// </summary>
        [FieldInfo("percentOff")]
        public int PercentOff { get; set; }
        /// <sumary>
        /// Database Field : coupon_duration
        /// </sumary>
        [FieldInfo("coupon_duration")]
        public string CouponDuration { get; set; }
        /// <summary>
        /// Database Field : amountOff
        /// </summary>
        [FieldInfo("amountOff")]
        public decimal AmountOff { get; set; }
    }
}