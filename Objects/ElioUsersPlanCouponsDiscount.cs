using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_plan_coupons_discount
    /// </summary>
    [ClassInfo("Elio_users_plan_coupons_discount")]
    public partial class ElioUsersPlanCouponsDiscount : DataObject
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
        /// Database Field : coupon_id
        /// </summary>
        [FieldInfo("plan_coupons_id")]
        public int PlanCouponsId { get; set; }
        /// <summary>
        /// Database Field : parent_pack_id
        /// </summary>
        [FieldInfo("parent_pack_id")]
        public int ParentPackId { get; set; }
        /// <summary>
        /// Database Field : is_active_discount
        /// </summary>
        [FieldInfo("is_active_discount")]
        public int IsActiveDiscount { get; set; }
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
        /// Database Field : request_by_user_id
        /// </summary>
        [FieldInfo("request_by_user_id")]
        public int request_by_user_id { get; set; }
    }
}