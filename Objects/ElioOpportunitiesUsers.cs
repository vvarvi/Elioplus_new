using System;
using System.Collections.Generic;
using System.Text;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table elio_users
    /// </summary>
    [ClassInfo("elio_opportunities_users")]
    public partial class ElioOpportunitiesUsers : DataObject
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
        /// Database Field : organization_name
        /// </summary>
        [FieldInfo("organization_name")]
        public string OrganizationName { get; set; }
        /// <summary>
        /// Database Field : occupation
        /// </summary>
        [FieldInfo("occupation")]
        public string Occupation { get; set; }
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
        /// Database Field : guid
        /// </summary>
        [FieldInfo("guid")]
        public string GuId { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
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
        /// Database Field : step_id
        /// </summary>
        [FieldInfo("status_id")]
        public int StatusId { get; set; }
    }
}
