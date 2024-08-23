using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_library_group_members
    /// </summary>
    [ClassInfo("Elio_collaboration_users_library_group_members")]
    public partial class ElioCollaborationUsersLibraryGroupMembers : DataObject
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
        /// Database Field : vendor_reseller_id
        /// </summary>
        [FieldInfo("vendor_reseller_id")]
        public int VendorResellerId { get; set; }
        /// <summary>
        /// Database Field : library_group_id
        /// </summary>
        [FieldInfo("library_group_id")]
        public int LibraryGroupId { get; set; }
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
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
