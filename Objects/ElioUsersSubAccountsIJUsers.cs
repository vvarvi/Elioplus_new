using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersSubAccountsIJUsers : DataObject
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
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : password
        /// </summary>
        [FieldInfo("password")]
        public string Password { get; set; }
        /// <summary>
        /// Database Field : team_role_id
        /// </summary>
        [FieldInfo("team_role_id")]
        public int TeamRoleId { get; set; }
        /// <summary>
        /// Database Field : password_encrypted
        /// </summary>
        [FieldInfo("password_encrypted")]
        public string PasswordEncrypted { get; set; }
        /// <summary>
        /// Database Field : guid
        /// </summary>
        [FieldInfo("guid")]
        public string Guid { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : is_confirmed
        /// </summary>
        [FieldInfo("is_confirmed")]
        public int IsConfirmed { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : confirmation_url
        /// </summary>
        [FieldInfo("confirmation_url")]
        public string ConfirmationUrl { get; set; }
        /// <summary>
        /// Database Field : account_status
        /// </summary>
        [FieldInfo("account_status")]
        public int AccountStatus { get; set; }
        /// <summary>
        /// Database Field : community_status
        /// </summary>
        [FieldInfo("community_status")]
        public int CommunityStatus { get; set; }
        /// <summary>
        /// Database Field : personal_image
        /// </summary>
        [FieldInfo("personal_image")]
        public string PersonalImage { get; set; }
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
        /// Database Field : user_login_count
        /// </summary>
        [FieldInfo("user_login_count")]
        public int UserLoginCount { get; set; }
        /// <summary>
        /// Database Field : last_login
        /// </summary>
        [FieldInfo("last_login")]
        public DateTime? LastLogin { get; set; }
        /// <summary>
        /// Database Field : position
        /// </summary>
        [FieldInfo("position")]
        public string Position { get; set; }
        /// <summary>
        /// Database Field : linkedin_url
        /// </summary>
        [FieldInfo("linkedin_url")]
        public string LinkedinUrl { get; set; }
        /// <summary>
        /// Database Field : linkedin_id
        /// </summary>
        [FieldInfo("linkedin_id")]
        public string LinkedinId { get; set; }
        /// <summary>
        /// Database Field : community_summary_text
        /// </summary>
        [FieldInfo("community_summary_text")]
        public string CommunitySummaryText { get; set; }
        /// <summary>
        /// Database Field : twitter_url
        /// </summary>
        [FieldInfo("twitter_url")]
        public string TwitterUrl { get; set; }
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
        /// Database Field : sender_email
        /// </summary>
        [FieldInfo("sender_email")]
        public string SenderEmail { get; set; }
        /// <summary>
        /// Database Field : company_name
        /// </summary>
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }
    }
}
