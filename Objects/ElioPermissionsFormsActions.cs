using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_permissions_forms_actions
    /// </summary>
    [ClassInfo("Elio_permissions_forms_actions")]
    public partial class ElioPermissionsFormsActions : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : form_id
        /// </summary>
        [FieldInfo("form_id")]
        public int FormId { get; set; }
        /// <summary>
        /// Database Field : action_id
        /// </summary>
        [FieldInfo("action_id")]
        public int ActionId { get; set; }
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
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
