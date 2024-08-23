using System;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersIJCountries : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : username
        /// </summary>
        [FieldInfo("username")]
        public string Username { get; set; }
        /// <summary>
        /// Database Field : password
        /// </summary>
        [FieldInfo("password")]
        public string Password { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : ip
        /// </summary>
        [FieldInfo("ip")]
        public string Ip { get; set; }
        /// <summary>
        /// Database Field : phone
        /// </summary>
        [FieldInfo("phone")]
        public string Phone { get; set; }
        /// <summary>
        /// Database Field : address
        /// </summary>
        [FieldInfo("address")]
        public string Address { get; set; }
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
        /// Database Field : account_status
        /// </summary>
        [FieldInfo("account_status")]
        public int AccountStatus { get; set; }
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
        /// Database Field : guid
        /// </summary>
        [FieldInfo("guid")]
        public string GuId { get; set; }
        /// <summary>
        /// Database Field : official_email
        /// </summary>
        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }
        /// <summary>
        /// Database Field : features_no
        /// </summary>
        [FieldInfo("features_no")]
        public int FeaturesNo { get; set; }
        /// <summary>
        /// Database Field : username_encrypted
        /// </summary>
        [FieldInfo("username_encrypted")]
        public string UsernameEncrypted { get; set; }
        /// <summary>
        /// Database Field : password_encrypted
        /// </summary>
        [FieldInfo("password_encrypted")]
        public string PasswordEncrypted { get; set; }
        /// <summary>
        /// Database Field : last_login
        /// </summary>
        [FieldInfo("last_login")]
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// Database Field : mashape_username
        /// </summary>
        [FieldInfo("mashape_username")]
        public string MashapeUsername { get; set; }
        /// <summary>
        /// Database Field : user_login_count
        /// </summary>
        [FieldInfo("user_login_count")]
        public int UserLoginCount { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
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
        /// Database Field : personal_image
        /// </summary>
        [FieldInfo("personal_image")]
        public string PersonalImage { get; set; }
        /// <summary>
        /// Database Field : community_profile_created
        /// </summary>
        [FieldInfo("community_profile_created")]
        public DateTime? CommunityProfileCreated { get; set; }
        /// <summary>
        /// Database Field : community_profile_last_updated
        /// </summary>
        [FieldInfo("community_profile_last_updated")]
        public DateTime? CommunityProfileLastUpdated { get; set; }
        /// <summary>
        /// Database Field : position
        /// </summary>
        [FieldInfo("position")]
        public string Position { get; set; }
        /// <summary>
        /// Database Field : community_summary_text
        /// </summary>
        [FieldInfo("community_summary_text")]
        public string CommunitySummaryText { get; set; }
        /// <summary>
        /// Database Field : community_status
        /// </summary>
        [FieldInfo("community_status")]
        public int CommunityStatus { get; set; }
        /// <summary>
        /// Database Field : linkedin_url
        /// </summary>
        [FieldInfo("linkedin_url")]
        public string LinkedInUrl { get; set; }
        /// <summary>
        /// Database Field : twitter_url
        /// </summary>
        [FieldInfo("twitter_url")]
        public string TwitterUrl { get; set; }
        /// <summary>
        /// Database Field : linkedin_id
        /// </summary>
        [FieldInfo("linkedin_id")]
        public string LinkedinId { get; set; }
        /// <summary>
        /// Database Field : has_billing_details
        /// </summary>
        [FieldInfo("has_billing_details")]
        public int HasBillingDetails { get; set; }
        /// <summary>
        /// Database Field : billing_type
        /// </summary>
        [FieldInfo("billing_type")]
        public int BillingType { get; set; }
        /// <summary>
        /// Database Field : customer_stripe_id
        /// </summary>
        [FieldInfo("customer_stripe_id")]
        public string CustomerStripeId { get; set; }
        /// <summary>
        /// Database Field : user_application_type
        /// </summary>
        [FieldInfo("user_application_type")]
        public int UserApplicationType { get; set; }
        /// <summary>
        /// Database Field : vendor_product_demo_link
        /// </summary>
        [FieldInfo("vendor_product_demo_link")]
        public string VendorProductDemoLink { get; set; }

        [FieldInfo("country_id")]
        public int CountryId { get; set; }

        [FieldInfo("country_name")]
        public string CountryName { get; set; }

        [FieldInfo("capital")]
        public string Capital { get; set; }

        [FieldInfo("prefix")]
        public string Prefix { get; set; }

        [FieldInfo("iso2")]
        public string Iso2 { get; set; }

        [FieldInfo("iso3")]
        public string Iso3 { get; set; }

        [FieldInfo("region")]
        public string Region { get; set; }
    }
}
