using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Paypal_feedback
    /// </summary>
    [ClassInfo("Paypal_feedback")]
    public partial class PaypalFeedback  : DataObject
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
        /// Database Field : paypal_amount
        /// </summary>
        [FieldInfo("paypal_amount")]
        public Decimal PaypalAmount { get; set; }
        /// <summary>
        /// Database Field : transaction_step_id
        /// </summary>
        [FieldInfo("transaction_step")]
        public string TransactionStep { get; set; }
        /// <summary>
        /// Database Field : transaction_step_description
        /// </summary>
        [FieldInfo("transaction_step_description")]
        public string TransactionStepDescription { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : paypal_tax_amount
        /// </summary>
        [FieldInfo("paypal_tax_amount")]
        public Decimal PaypalTaxAmount { get; set; }
        /// <summary>
        /// Database Field : ip
        /// </summary>
        [FieldInfo("ip")]
        public string Ip { get; set; }
        /// <summary>
        /// Database Field : application_source
        /// </summary>
        [FieldInfo("application_source")]
        public string ApplicationSource { get; set; }
        /// <summary>
        /// Database Field : paypal_status
        /// </summary>
        [FieldInfo("paypal_status")]
        public string PaypalStatus { get; set; }
        /// <summary>
        /// Database Field : paypal_reason_code
        /// </summary>
        [FieldInfo("paypal_reason_code")]
        public string PaypalReasonCode { get; set; }
        /// <summary>
        /// Database Field : paypal_token
        /// </summary>
        [FieldInfo("paypal_token")]
        public string PaypalToken { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : paypal_firstname
        /// </summary>
        [FieldInfo("paypal_firstname")]
        public string PaypalFirstname { get; set; }
        /// <summary>
        /// Database Field : paypal_lastname
        /// </summary>
        [FieldInfo("paypal_lastname")]
        public string PaypalLastname { get; set; }
        /// <summary>
        /// Database Field : paypal_payer_email
        /// </summary>
        [FieldInfo("paypal_payer_email")]
        public string PaypalPayeremail { get; set; }
        /// <summary>
        /// Database Field : paypal_payer_id
        /// </summary>
        [FieldInfo("paypal_payer_id")]
        public string PaypalPayerid { get; set; }
        /// <summary>
        /// Database Field : paypal_payer_status
        /// </summary>
        [FieldInfo("paypal_payer_status")]
        public string PaypalPayerstatus { get; set; }
        /// <summary>
        /// Database Field : is_paypal_authenticated
        /// </summary>
        [FieldInfo("is_paypal_authenticated")]
        public int IsPaypalAuthenticated { get; set; }
        /// <summary>
        /// Database Field : paypal_transaction_type
        /// </summary>
        [FieldInfo("paypal_transaction_type")]
        public string PaypalTransactionType { get; set; }
        /// <summary>
        /// Database Field : paypal_transaction_id
        /// </summary>
        [FieldInfo("paypal_transaction_id")]
        public string PaypalTransactionId { get; set; }
        /// <summary>
        /// Database Field : paypal_payment_type
        /// </summary>
        [FieldInfo("paypal_payment_type")]
        public string PaypalPaymentType { get; set; }
        /// <summary>
        /// Database Field : paypal_order_time
        /// </summary>
        [FieldInfo("paypal_order_time")]
        public string PaypalOrderTime { get; set; }
        /// <summary>
        /// Database Field : paypal_currency_code
        /// </summary>
        [FieldInfo("paypal_currency_code")]
        public string PaypalCurrencyCode { get; set; }
        /// <summary>
        /// Database Field : paypal_status_reason
        /// </summary>
        [FieldInfo("paypal_status_reason")]
        public string PaypalStatusReason { get; set; }
        /// <summary>
        /// Database Field : paypal_fee_amount
        /// </summary>
        [FieldInfo("paypal_fee_amount")]
        public string PaypalFeeAmount { get; set; }
    }
}
