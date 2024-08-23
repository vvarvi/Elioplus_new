using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_group_members
    /// </summary>
    [ClassInfo("Elio_collaboration_users_group_members")]
    public partial class ElioCollaborationUsersGroupMembers : DataObject
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
        /// Database Field : group_retailor_id
        /// </summary>
        [FieldInfo("group_retailor_id")]
        public int GroupRetailorId { get; set; }
        /// <summary>
        /// Database Field : collaboration_group_id
        /// </summary>
        [FieldInfo("collaboration_group_id")]
        public int CollaborationGroupId { get; set; }        
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
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
