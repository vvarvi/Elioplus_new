using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_person_companies
    /// </summary>
    [ClassInfo("Elio_users_person_companies")]
    public partial class ElioUsersPersonCompanies : DataObject
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
        /// Database Field : clearbit_company_id
        /// </summary>
        [FieldInfo("clearbit_company_id")]
        public string ClearbitCompanyId { get; set; }
        /// <summary>
        /// Database Field : elio_person_id
        /// </summary>
        [FieldInfo("elio_person_id")]
        public int ElioPersonId { get; set; }
        /// <summary>
        /// Database Field : clearbit_person_id
        /// </summary>
        [FieldInfo("clearbit_person_id")]
        public string ClearbitPersonId { get; set; }
        /// <summary>
        /// Database Field : name
        /// </summary>
        [FieldInfo("name")]
        public string Name { get; set; }
        /// <summary>
        /// Database Field : domain
        /// </summary>
        [FieldInfo("domain")]
        public string Domain { get; set; }
        /// <summary>
        /// Database Field : sector
        /// </summary>
        [FieldInfo("sector")]
        public string Sector { get; set; }
        /// <summary>
        /// Database Field : industry_group
        /// </summary>
        [FieldInfo("industry_group")]
        public string IndustryGroup { get; set; }
        /// <summary>
        /// Database Field : industry
        /// </summary>
        [FieldInfo("industry")]
        public string Industry { get; set; }
        /// <summary>
        /// Database Field : sub_industry
        /// </summary>
        [FieldInfo("sub_industry")]
        public string SubIndustry { get; set; }
        /// <summary>
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
        /// <summary>
        /// Database Field : founded_year
        /// </summary>
        [FieldInfo("founded_year")]
        public int FoundedYear { get; set; }
        /// <summary>
        /// Database Field : location
        /// </summary>
        [FieldInfo("location")]
        public string Location { get; set; }
        /// <summary>
        /// Database Field : fund_amount
        /// </summary>
        [FieldInfo("fund_amount")]
        public decimal FundAmount { get; set; }
        /// <summary>
        /// Database Field : employees_number
        /// </summary>
        [FieldInfo("employees_number")]
        public int EmployeesNumber { get; set; }
        /// <summary>
        /// Database Field : employees_range
        /// </summary>
        [FieldInfo("employees_range")]
        public string EmployeesRange { get; set; }
        /// <summary>
        /// Database Field : annual_revenue
        /// </summary>
        [FieldInfo("annual_revenue")]
        public decimal AnnualRevenue { get; set; }
        /// <summary>
        /// Database Field : annual_revenue_range
        /// </summary>
        [FieldInfo("annual_revenue_range")]
        public string AnnualRevenueRange { get; set; }
        /// <summary>
        /// Database Field : facebook_handle
        /// </summary>
        [FieldInfo("facebook_handle")]
        public string FacebookHandle { get; set; }
        /// <summary>
        /// Database Field : facebook_likes
        /// </summary>
        [FieldInfo("facebook_likes")]
        public int FacebookLikes { get; set; }
        /// <summary>
        /// Database Field : linkedin_handle
        /// </summary>
        [FieldInfo("linkedin_handle")]
        public string LinkedinHandle { get; set; }
        /// <summary>
        /// Database Field : twitter_handle
        /// </summary>
        [FieldInfo("twitter_handle")]
        public string TwitterHandle { get; set; }
        /// <summary>
        /// Database Field : twitter_id
        /// </summary>
        [FieldInfo("twitter_id")]
        public string TwitterId { get; set; }
        /// <summary>
        /// Database Field : twitter_bio
        /// </summary>
        [FieldInfo("twitter_bio")]
        public string TwitterBio { get; set; }
        /// <summary>
        /// Database Field : twitter_followers
        /// </summary>
        [FieldInfo("twitter_followers")]
        public int TwitterFollowers { get; set; }
        /// <summary>
        /// Database Field : twitter_following
        /// </summary>
        [FieldInfo("twitter_following")]
        public int TwitterFollowing { get; set; }
        /// <summary>
        /// Database Field : twitter_location
        /// </summary>
        [FieldInfo("twitter_location")]
        public string TwitterLocation { get; set; }
        /// <summary>
        /// Database Field : twitter_site
        /// </summary>
        [FieldInfo("twitter_site")]
        public string TwitterSite { get; set; }
        /// <summary>
        /// Database Field : twitter_avatar
        /// </summary>
        [FieldInfo("twitter_avatar")]
        public string TwitterAvatar { get; set; }
        /// <summary>
        /// Database Field : company_phone
        /// </summary>
        [FieldInfo("company_phone")]
        public string CompanyPhone { get; set; }
        /// <summary>
        /// Database Field : crunchbase_handle
        /// </summary>
        [FieldInfo("crunchbase_handle")]
        public string CrunchbaseHandle { get; set; }
        /// <summary>
        /// Database Field : logo
        /// </summary>
        [FieldInfo("logo")]
        public string Logo { get; set; }
        /// <summary>
        /// Database Field : type
        /// </summary>
        [FieldInfo("type")]
        public string Type { get; set; }
        /// <summary>
        /// Database Field : date_inserted
        /// </summary>
        [FieldInfo("date_inserted")]
        public DateTime DateInserted { get; set; }
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
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}
