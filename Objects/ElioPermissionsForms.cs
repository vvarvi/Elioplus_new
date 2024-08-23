using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_permissions_forms
    /// </summary>
    [ClassInfo("Elio_permissions_forms")]
    public partial class ElioPermissionsForms : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : form_title
        /// </summary>
        [FieldInfo("form_title")]
        public string FormTitle { get; set; }
        /// <summary>
        /// Database Field : form_name
        /// </summary>
        [FieldInfo("form_name")]
        public string FormName { get; set; }
        /// <summary>
        /// Database Field : form_description
        /// </summary>
        [FieldInfo("form_description")]
        public string FormDescription { get; set; }
        /// <summary>
        /// Database Field : form_path
        /// </summary>
        [FieldInfo("form_path")]
        public string FormPath { get; set; }
        /// <summary>
        /// Database Field : form_id
        /// </summary>
        [FieldInfo("form_id")]
        public string FormId { get; set; }
        /// <summary>
        /// Database Field : is_modal
        /// </summary>
        [FieldInfo("is_modal")]
        public int IsModal { get; set; }
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
