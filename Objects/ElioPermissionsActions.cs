using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_permissions_actions
    /// </summary>
    [ClassInfo("Elio_permissions_actions")]
    public partial class ElioPermissionsActions : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : action_name
        /// </summary>
        [FieldInfo("action_name")]
        public string ActionName { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
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
    }
}
