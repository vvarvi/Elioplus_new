using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioCollaborationVendorsResellersIJUsers : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : master_user_id
        /// </summary>
        [FieldInfo("master_user_id")]
        public int MasterUserId { get; set; }
        /// <summary>
        /// Database Field : partner_user_id
        /// </summary>
        [FieldInfo("partner_user_id")]
        public int PartnerUserId { get; set; }
        /// <summary>
        /// Database Field : company_name
        /// </summary>
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }
        /// <summary>
        /// Database Field : company_logo
        /// </summary>
        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }
        /// <summary>
        /// Database Field : company_type
        /// </summary>
        [FieldInfo("company_type")]
        public string CompanyType { get; set; }
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
        /// Database Field : country
        /// </summary>
        [FieldInfo("country")]
        public string Country { get; set; }
        /// <summary>
        /// Database Field : region
        /// </summary>
        [FieldInfo("region")]
        public string Region { get; set; } 
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
        /// Database Field : invitation_status
        /// </summary>
        [FieldInfo("invitation_status")]
        public string InvitationStatus { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : tier_status
        /// </summary>
        [FieldInfo("tier_status")]
        public string TierStatus { get; set; }
        /// <summary>
        /// Database Field : score
        /// </summary>
        [FieldInfo("score")]
        public string Score { get; set; }
    }
}