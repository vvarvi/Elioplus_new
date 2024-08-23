using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_person
    /// </summary>
    [ClassInfo("Elio_users_person")]
    public partial class ElioUsersPerson : DataObject
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
    }
}
