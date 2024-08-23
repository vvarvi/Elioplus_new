using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_permissions_users_roles
    /// </summary>
    [ClassInfo("Elio_permissions_users_roles")]
    public partial class ElioPermissionsUsersRoles : DataObject
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
        /// Database Field : role_name
        /// </summary>
        [FieldInfo("role_name")]
        public string RoleName { get; set; }
        /// <summary>
        /// Database Field : role_description
        /// </summary>
        [FieldInfo("role_description")]
        public string RoleDescription { get; set; }
        /// <summary>
        /// Database Field : is_system
        /// </summary>
        [FieldInfo("is_system")]
        public int IsSystem { get; set; }
        /// <summary>
        /// Database Field : parent_role_id
        /// </summary>
        [FieldInfo("parent_role_id")]
        public int ParentRoleId { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
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
