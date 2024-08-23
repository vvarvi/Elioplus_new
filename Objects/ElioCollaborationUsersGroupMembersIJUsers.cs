using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioCollaborationUsersGroupMembersIJUsers : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : creator_user_id
        /// </summary>
        [FieldInfo("creator_user_id")]
        public int CreatorUserId { get; set; }
        /// <summary>
        /// Database Field : collaboration_group_id
        /// </summary>
        [FieldInfo("collaboration_group_id")]
        public int CollaborationGroupId { get; set; }
        /// <summary>
        /// Database Field : group_retailor_id
        /// </summary>
        [FieldInfo("group_retailor_id")]
        public int GroupRetailorId { get; set; }
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
        /// Database Field : date_created
        /// </summary>
        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }        
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}