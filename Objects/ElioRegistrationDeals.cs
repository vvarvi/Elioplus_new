using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_registration_deals
    /// </summary>
    [ClassInfo("Elio_registration_deals")]
    public partial class ElioRegistrationDeals : DataObject
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
        /// Database Field : address
        /// </summary>
        [FieldInfo("address")]
        public string Address { get; set; }
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : domain
        /// </summary>
        [FieldInfo("domain")]
        public string Domain { get; set; }
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
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
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
        /// Database Field : expected_closed_date
        /// </summary>
        [FieldInfo("expected_closed_date")]
        public DateTime ExpectedClosedDate { get; set; }
        /// <summary>
        /// Database Field : product
        /// </summary>
        [FieldInfo("product")]
        public string Product { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : deal_result
        /// </summary>
        [FieldInfo("deal_result")]
        public string DealResult { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : month_duration
        /// </summary>
        [FieldInfo("month_duration")]
        public int MonthDuration { get; set; }
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : date_viewed
        /// </summary>
        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }
        /// <summary>
        /// Database Field : forecasting_percent
        /// </summary>
        [FieldInfo("forecasting_percent")]
        public string ForecastingPercent { get; set; }
        /// <summary>
        /// Database Field : amount
        /// </summary>
        [FieldInfo("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Database Field : cur_id
        /// </summary>
        [FieldInfo("cur_id")]
        public string CurId { get; set; }
        /// <summary>
        /// Database Field : created_by_user_id
        /// </summary>
        [FieldInfo("created_by_user_id")]
        public int CreatedByUserId { get; set; }
        /// <summary>
        /// Database Field : divisional_name
        /// </summary>
        [FieldInfo("divisional_name")]
        public string DivisionalName { get; set; }
        /// <summary>
        /// Database Field : city
        /// </summary>
        [FieldInfo("city")]
        public string City { get; set; }
        /// <summary>
        /// Database Field : country
        /// </summary>
        [FieldInfo("country")]
        public string Country { get; set; }
        /// <summary>
        /// Database Field : job_title
        /// </summary>
        [FieldInfo("job_title")]
        public string JobTitle { get; set; }
        /// <summary>
        /// Database Field : divisional_postal_code
        /// </summary>
        [FieldInfo("divisional_postal_code")]
        public string DivisionalPostalCode { get; set; }
        /// <summary>
        /// Database Field : proposed_solutions
        /// </summary>
        [FieldInfo("proposed_solutions")]
        public string ProposedSolutions { get; set; }
        /// <summary>
        /// Database Field : change_status_date
        /// </summary>
        [FieldInfo("change_status_date")]
        public DateTime? ChangeStatusDate { get; set; }
        /// <summary>
        /// Database Field : is_dublicate
        /// </summary>
        [FieldInfo("is_dublicate")]
        public int IsDublicate { get; set; }
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
