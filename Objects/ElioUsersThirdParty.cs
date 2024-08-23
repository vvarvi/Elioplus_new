using System;
using System.Collections.Generic;
using System.Text;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table elio_users
    /// </summary>
    [ClassInfo("elio_users_third_party")]
    public partial class ElioUsersThirdParty : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }       
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
        /// Database Field : guid
        /// </summary>
        [FieldInfo("guid")]
        public string GuId { get; set; }        
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
    }
}
