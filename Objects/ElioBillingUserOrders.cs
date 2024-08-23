using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_user_orders
    /// </summary>
    [ClassInfo("Elio_billing_user_orders")]
    public partial class ElioBillingUserOrders : DataObject
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
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime? Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : current_period_start
        /// </summary>
        [FieldInfo("current_period_start")]
        public DateTime? CurrentPeriodStart { get; set; }
        /// <summary>
        /// Database Field : current_period_end
        /// </summary>
        [FieldInfo("current_period_end")]
        public DateTime? CurrentPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : is_ready_to_use
        /// </summary>
        [FieldInfo("is_ready_to_use")]
        public int IsReadyToUse { get; set; }
        /// <summary>
        /// Database Field : order_status
        /// </summary>
        [FieldInfo("order_status")]
        public int OrderStatus { get; set; }
        /// <summary>
        /// Database Field : cost_with_no_vat
        /// </summary>
        [FieldInfo("cost_with_no_vat")]
        public Decimal CostWithNoVat { get; set; }
        /// <summary>
        /// Database Field : cost_with_vat
        /// </summary>
        [FieldInfo("cost_with_vat")]
        public Decimal CostWithVat { get; set; }
        /// <summary>
        /// Database Field : cost_vat
        /// </summary>
        [FieldInfo("cost_vat")]
        public Decimal CostVat { get; set; }
        /// <summary>
        /// Database Field : order_type
        /// </summary>
        [FieldInfo("order_type")]
        public int OrderType { get; set; }
        /// <summary>
        /// Database Field : order_payment_way
        /// </summary>
        [FieldInfo("order_payment_way")]
        public string OrderPaymentWay { get; set; }
        /// <summary>
        /// Database Field : admin_name
        /// </summary>
        [FieldInfo("admin_name")]
        public string AdminName { get; set; }
        /// <summary>
        /// Database Field : admin_id
        /// </summary>
        [FieldInfo("admin_id")]
        public int AdminId { get; set; }
        /// <summary>
        /// Database Field : is_paid
        /// </summary>
        [FieldInfo("is_paid")]
        public int IsPaid { get; set; }
        /// <summary>
        /// Database Field : canceled_at
        /// </summary>
        [FieldInfo("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        /// <summary>
        /// Database Field : mode
        /// </summary>
        [FieldInfo("mode")]
        public string Mode { get; set; }
    }
}
