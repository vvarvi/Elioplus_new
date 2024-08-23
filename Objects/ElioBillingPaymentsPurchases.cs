using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_payments_purchases
    /// </summary>
    [ClassInfo("Elio_billing_payments_purchases")]
    public partial class ElioBillingPaymentsPurchases  : DataObject
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
        /// Database Field : payment_id
        /// </summary>
        [FieldInfo("payment_id")]
        public int PaymentId { get; set; }
        /// <summary>
        /// Database Field : purchase_id
        /// </summary>
        [FieldInfo("purchase_id")]
        public int PurchaseId { get; set; }
        /// <summary>
        /// Database Field : total_amount
        /// </summary>
        [FieldInfo("total_amount")]
        public Decimal TotalAmount { get; set; }
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
    }
}
