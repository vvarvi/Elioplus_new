using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_partner_to_partner_deals
    /// </summary>
    [ClassInfo("Elio_partner_to_partner_deals")]
    public partial class ElioPartnerToPartnerDeals : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : reseller_id
        /// </summary>
        [FieldInfo("reseller_id")]
        public int ResellerId { get; set; }
        /// <summary>
        /// Database Field : partner_user_id
        /// </summary>
        [FieldInfo("partner_user_id")]
        public int PartnerUserId { get; set; }
        /// <summary>
        /// Database Field : opportunity_name
        /// </summary>
        [FieldInfo("opportunity_name")]
        public string OpportunityName { get; set; }
        /// <summary>
        /// Database Field : product_description
        /// </summary>
        [FieldInfo("product_description")]
        public string ProductDescription { get; set; }
        /// <summary>
        /// Database Field : deal_value
        /// </summary>
        [FieldInfo("deal_value")]
        public Decimal DealValue { get; set; }
        /// <summary>
        /// Database Field : opportunity_description
        /// </summary>
        [FieldInfo("opportunity_description")]
        public string OpportunityDescription { get; set; }
        /// <summary>
        /// Database Field : country_id
        /// </summary>
        [FieldInfo("country_id")]
        public int CountryId { get; set; }
        /// <summary>
        /// Database Field : status
        /// </summary>
        [FieldInfo("status")]
        public int Status { get; set; }       
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
        /// Database Field : date_closed
        /// </summary>
        [FieldInfo("date_closed")]
        public DateTime? DateClosed { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : canceled_at
        /// </summary>
        [FieldInfo("canceled_at")]
        public DateTime? CanceledAt { get; set; }
        /// <summary>
        /// Database Field : canceled_by
        /// </summary>
        [FieldInfo("canceled_by")]
        public int? CanceledBy { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : currency
        /// </summary>
        [FieldInfo("currency")]
        public string Currency { get; set; }
    }
}
