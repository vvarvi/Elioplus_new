using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_billing_account
    /// </summary>
    [ClassInfo("Elio_users_billing_account")]
    public partial class ElioUsersBillingAccount  : DataObject
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
        /// Database Field : company_vat_number
        /// </summary>
        [FieldInfo("company_vat_number")]
        public string CompanyVatNumber { get; set; }
        /// <summary>
        /// Database Field : company_vat_office
        /// </summary>
        [FieldInfo("company_vat_office")]
        public string CompanyVatOffice { get; set; }
        /// <summary>
        /// Database Field : company_post_code
        /// </summary>
        [FieldInfo("company_post_code")]
        public string CompanyPostCode { get; set; }
        /// <summary>
        /// Database Field : user_vat_number
        /// </summary>
        [FieldInfo("user_vat_number")]
        public string UserVatNumber { get; set; }
        /// <summary>
        /// Database Field : user_id_number
        /// </summary>
        [FieldInfo("user_id_number")]
        public string UserIdNumber { get; set; }
        /// <summary>
        /// Database Field : post_code
        /// </summary>
        [FieldInfo("post_code")]
        public string PostCode { get; set; }
        /// <summary>
        /// Database Field : has_vat
        /// </summary>
        [FieldInfo("has_vat")]
        public int HasVat { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime? Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime? LastUpdated { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : company_billing_address
        /// </summary>
        [FieldInfo("company_billing_address")]
        public string CompanyBillingAddress { get; set; }
        /// <summary>
        /// Database Field : billing_email
        /// </summary>
        [FieldInfo("billing_email")]
        public string BillingEmail { get; set; }
        /// <summary>
        /// Database Field : stripe_tax_id
        /// </summary>
        [FieldInfo("stripe_tax_id")]
        public string StripeTaxId { get; set; }
    }
}
