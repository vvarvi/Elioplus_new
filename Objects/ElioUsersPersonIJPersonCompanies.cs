using System;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersPersonIJPersonCompanies : DataObject
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
        /// Database Field : clearbit_person_id
        /// </summary>
        [FieldInfo("clearbit_person_id")]
        public string ClearbitPersonId { get; set; }
        /// <summary>
        /// Database Field : given_name
        /// </summary>
        [FieldInfo("given_name")]
        public string GivenName { get; set; }
        /// <summary>
        /// Database Field : family_name
        /// </summary>
        [FieldInfo("family_name")]
        public string FamilyName { get; set; }
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : phone
        /// </summary>
        [FieldInfo("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Database Field : location
        /// </summary>
        [FieldInfo("location")]
        public string Location { get; set; }
        /// <summary>
        /// Database Field : time_zone
        /// </summary>
        [FieldInfo("time_zone")]
        public string TimeZone { get; set; }
        /// <summary>
        /// Database Field : bio
        /// </summary>
        [FieldInfo("bio")]
        public string Bio { get; set; }
        /// <summary>
        /// Database Field : avatar
        /// </summary>
        [FieldInfo("avatar")]
        public string Avatar { get; set; }
        /// <summary>
        /// Database Field : title
        /// </summary>
        [FieldInfo("title")]
        public string Title { get; set; }
        /// <summary>
        /// Database Field : role
        /// </summary>
        [FieldInfo("role")]
        public string Role { get; set; }
        /// <summary>
        /// Database Field : seniority
        /// </summary>
        [FieldInfo("seniority")]
        public string Seniority { get; set; }
        /// <summary>
        /// Database Field : twitter_handle
        /// </summary>
        [FieldInfo("twitter_handle")]
        public string TwitterHandle { get; set; }
        /// <summary>
        /// Database Field : linkedin_handle
        /// </summary>
        [FieldInfo("linkedin_handle")]
        public string LinkedinHandle { get; set; }
        /// <summary>
        /// Database Field : about_me_handle
        /// </summary>
        [FieldInfo("about_me_handle")]
        public string AboutMeHandle { get; set; }
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
        /// <summary>
        /// Database Field : is_claimed
        /// </summary>
        [FieldInfo("is_claimed")]
        public int IsClaimed { get; set; }
        /// <summary>
        /// Database Field : elio_person_company_id
        /// </summary>
        [FieldInfo("elio_person_company_id")]
        public int ElioPersonCompanyId { get; set; }
        /// <summary>
        /// Database Field : clearbit_company_id
        /// </summary>
        [FieldInfo("clearbit_company_id")]
        public string ClearbitCompanyId { get; set; }
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
    }
}
