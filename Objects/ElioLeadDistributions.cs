using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_lead_distributions
    /// </summary>
    [ClassInfo("Elio_lead_distributions")]
    public partial class ElioLeadDistributions : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : collaboration_vendor_reseller_id
        /// </summary>
        [FieldInfo("collaboration_vendor_reseller_id")]
        public int CollaborationVendorResellerId { get; set; }
        /// <summary>
        /// Database Field : vendor_id
        /// </summary>
        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }
        /// <summary>
        /// Database Field : reseller_id
        /// </summary>
        [FieldInfo("reseller_id")]
        public int ResellerId { get; set; }
        /// <summary>
        /// Database Field : status
        /// </summary>
        [FieldInfo("status")]
        public int Status { get; set; }
        /// <summary>
        /// Database Field : last_name
        /// </summary>
        [FieldInfo("last_name")]
        public string LastName { get; set; }
        /// <summary>
        /// Database Field : first_name
        /// </summary>
        [FieldInfo("first_name")]
        public string FirstName { get; set; }
        /// <summary>
        /// Database Field : company_name
        /// </summary>
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }        
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : country
        /// </summary>
        [FieldInfo("country")]
        public string Country { get; set; }
        /// <summary>
        /// Database Field : website
        /// </summary>
        [FieldInfo("website")]
        public string Website { get; set; }
        /// <summary>
        /// Database Field : phone
        /// </summary>
        [FieldInfo("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
        /// <summary>
        /// Database Field : created_date
        /// </summary>
        [FieldInfo("created_date")]
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }        
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : lead_result
        /// </summary>
        [FieldInfo("lead_result")]
        public string LeadResult { get; set; }
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : is_vewed_by_vendor
        /// </summary>
        [FieldInfo("is_vewed_by_vendor")]
        public int isVewedByVendor { get; set; }
        /// <summary>
        /// Database Field : amount
        /// </summary>
        [FieldInfo("amount")]
        public decimal? Amount { get; set; }
        /// <summary>
        /// Database Field : cur_id
        /// </summary>
        [FieldInfo("cur_id")]
        public string CurId { get; set; }
        /// <summary>
        /// Database Field : is_historical_reason
        /// </summary>
        [FieldInfo("is_historical_reason")]
        public int IsHistoricalReason { get; set; }
        /// <summary>
        /// Database Field : payment_status
        /// </summary>
        [FieldInfo("payment_status")]
        public int PaymentStatus { get; set; }
        /// <summary>
        /// Database Field : charge_id
        /// </summary>
        [FieldInfo("charge_id")]
        public string ChargeId { get; set; }
    }
}
