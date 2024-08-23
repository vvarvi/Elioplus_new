using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_payments
    /// </summary>
    [ClassInfo("Elio_billing_payments")]
    public partial class ElioBillingPayments : DataObject
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
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : total_amount
        /// </summary>
        [FieldInfo("total_amount")]
        public Decimal TotalAmount { get; set; }
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
        /// Database Field : payment_type
        /// </summary>
        [FieldInfo("payment_way")]
        public string PaymentWay { get; set; }
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
        /// <summary>
        /// Database Field : paypal_transaction_id
        /// </summary>
        [FieldInfo("payment_charge_id")]
        public string PaymentChargeId { get; set; }
    }
}
