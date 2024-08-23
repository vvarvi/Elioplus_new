using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table elio_users
    /// </summary>
    [ClassInfo("elio_users_search_info")]
    public partial class ElioUsersSearchInfo : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : profiles
        /// </summary>
        [FieldInfo("profiles")]
        public string Profiles { get; set; }
        /// <summary>
        /// Database Field : partner_portal_profile
        /// </summary>
        [FieldInfo("partner_portal")]
        public string PartnerPortal { get; set; }
        /// <summary>
        /// Database Field : country
        /// </summary>
        [FieldInfo("country")]
        public string Country { get; set; }
        /// <summary>
        /// Database Field : website
        /// </summary>
        [FieldInfo("website")]
        public string WebSite { get; set; }
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : overview
        /// </summary>
        [FieldInfo("overview")]
        public string Overview { get; set; }
        /// <summary>
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
        /// <summary>
        /// Database Field : company_name
        /// </summary>
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }
        /// <summary>
        /// Database Field : company_type
        /// </summary>
        [FieldInfo("company_type")]
        public string CompanyType { get; set; }
        /// <summary>
        /// Database Field : company_logo
        /// </summary>
        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }
        /// <summary>
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : billing_type
        /// </summary>
        [FieldInfo("billing_type")]
        public int BillingType { get; set; }
        /// <summary>
        /// Database Field : user_application_type
        /// </summary>
        [FieldInfo("user_application_type")]
        public int UserApplicationType { get; set; }
        /// <summary>
        /// Database Field : user_register_type
        /// </summary>
        [FieldInfo("user_register_type")]
        public int UserRegisterType { get; set; }
        /// <summary>
        /// Database Field : company_region
        /// </summary>
        [FieldInfo("company_region")]
        public string CompanyRegion { get; set; }
        /// <summary>
        /// Database Field : state
        /// </summary>
        [FieldInfo("state")]
        public string State { get; set; }
        /// <summary>
        /// Database Field : city
        /// </summary>
        [FieldInfo("city")]
        public string City { get; set; }
    }
}
