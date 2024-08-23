using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_user_orders_payments
    /// </summary>
    [ClassInfo("Elio_billing_user_orders_payments")]
    public partial class ElioBillingUserOrdersPayments : DataObject
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
        /// Database Field : order_id
        /// </summary>
        [FieldInfo("order_id")]
        public int OrderId { get; set; }
        /// <summary>
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
        /// <summary>
        /// Database Field : date_created
        /// </summary>
        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
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
        /// Database Field : amount
        /// </summary>
        [FieldInfo("amount")]
        public Decimal Amount { get; set; }        
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
        /// <summary>
        /// Database Field : charge_id
        /// </summary>
        [FieldInfo("charge_id")]
        public string ChargeId { get; set; }
        /// <summary>
        /// Database Field : mode
        /// </summary>
        [FieldInfo("mode")]
        public string Mode { get; set; }
    }
}
