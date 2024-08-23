using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_subscriptions_invoices
    /// </summary>
    [ClassInfo("Elio_users_subscriptions_invoices")]
    public partial class ElioUsersSubscriptionsInvoices : DataObject
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
        /// Database Field : user_subscription_id
        /// </summary>
        [FieldInfo("user_subscription_id")]
        public int UserSubscriptionId { get; set; }
        /// <summary>
        /// Database Field : customer_id
        /// </summary>
        [FieldInfo("customer_id")]
        public string CustomerId { get; set; }
        /// <summary>
        /// Database Field : invoice_id
        /// </summary>
        [FieldInfo("invoice_id")]
        public string InvoiceId { get; set; }
        /// <summary>
        /// Database Field : charge_id
        /// </summary>
        [FieldInfo("charge_id")]
        public string ChargeId { get; set; }
        /// <summary>
        /// Database Field : subscription_id
        /// </summary>
        [FieldInfo("subscription_id")]
        public string SubscriptionId { get; set; }
        /// <summary>
        /// Database Field : is_closed
        /// </summary>
        [FieldInfo("is_closed")]
        public int IsClosed { get; set; }
        /// <summary>
        /// Database Field : currency
        /// </summary>
        [FieldInfo("currency")]
        public string Currency { get; set; }        
        /// <summary>
        /// Database Field : date
        /// </summary>
        [FieldInfo("date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
        /// <summary>
        /// Database Field : hosted_invoice_url
        /// </summary>
        [FieldInfo("hosted_invoice_url")]
        public string HostedInvoiceUrl { get; set; }
        /// <summary>
        /// Database Field : invoice_pdf
        /// </summary>
        [FieldInfo("invoice_pdf")]
        public string InvoicePdf { get; set; }
        /// <summary>
        /// Database Field : next_payment_attempt
        /// </summary>
        [FieldInfo("next_payment_attempt")]
        public DateTime? NextPaymentAttempt { get; set; }
        /// <summary>
        /// Database Field : number
        /// </summary>
        [FieldInfo("number")]
        public string Number { get; set; }
        /// <summary>
        /// Database Field : is_paid
        /// </summary>
        [FieldInfo("is_paid")]
        public int IsPaid { get; set; }
        /// <summary>
        /// Database Field : period_start
        /// </summary>
        [FieldInfo("period_start")]
        public DateTime PeriodStart { get; set; }
        /// <summary>
        /// Database Field : period_end
        /// </summary>
        [FieldInfo("period_end")]
        public DateTime PeriodEnd { get; set; }
        /// <summary>
        /// Database Field : receipt_number
        /// </summary>
        [FieldInfo("receipt_number")]
        public string ReceiptNumber { get; set; }
        /// <summary>
        /// Database Field : has_discount
        /// </summary>
        [FieldInfo("has_discount")]
        public int HasDiscount { get; set; }
        /// <summary>
        /// Database Field : total_amount
        /// </summary>
        [FieldInfo("total_amount")]
        public int TotalAmount { get; set; }
        /// <summary>
        /// Database Field : sub_total_amount
        /// </summary>
        [FieldInfo("sub_total_amount")]
        public int SubTotalAmount { get; set; }
        /// <summary>
        /// Database Field : coupon_id
        /// </summary>
        [FieldInfo("coupon_id")]
        public string CouponId { get; set; }
    }
}
