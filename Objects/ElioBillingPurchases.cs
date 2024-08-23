using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_purchases
    /// </summary>
    [ClassInfo("Elio_billing_purchases")]
    public partial class ElioBillingPurchases  : DataObject
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
        /// Database Field : total_cost
        /// </summary>
        [FieldInfo("total_cost")]
        public Decimal TotalCost { get; set; }
        /// <summary>
        /// Database Field : fpa_value
        /// </summary>
        [FieldInfo("fpa_value")]
        public Decimal FpaValue { get; set; }
        /// <summary>
        /// Database Field : cost_with_no_fpa
        /// </summary>
        [FieldInfo("cost_with_no_fpa")]
        public Decimal CostWithNoFpa { get; set; }
        /// <summary>
        /// Database Field : is_invoice
        /// </summary>
        [FieldInfo("is_invoice")]
        public int IsInvoice { get; set; }
        /// <summary>
        /// Database Field : invoice_printed
        /// </summary>
        [FieldInfo("invoice_printed")]
        public int InvoicePrinted { get; set; }
        /// <summary>
        /// Database Field : print_date
        /// </summary>
        [FieldInfo("print_date")]
        public DateTime? PrintDate { get; set; }
        /// <summary>
        /// Database Field : print_invoice_no
        /// </summary>
        [FieldInfo("print_invoice_no")]
        public string PrintInvoiceNo { get; set; }
        /// <summary>
        /// Database Field : canceled
        /// </summary>
        [FieldInfo("canceled")]
        public int Canceled { get; set; }
        /// <summary>
        /// Database Field : e_invoice
        /// </summary>
        [FieldInfo("e_invoice")]
        public int EInvoice { get; set; }
        /// <summary>
        /// Database Field : is_paid
        /// </summary>
        [FieldInfo("is_paid")]
        public int IsPaid { get; set; }
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
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
        /// Database Field : purchase_type
        /// </summary>
        [FieldInfo("purchase_way")]
        public string PurchaseWay { get; set; }
        /// <summary>
        /// Database Field : discount
        /// </summary>
        [FieldInfo("discount")]
        public Decimal Discount { get; set; }
        /// <summary>
        /// Database Field : purchase_type
        /// </summary>
        [FieldInfo("purchase_type")]
        public int PurchaseType { get; set; }
    }
}
