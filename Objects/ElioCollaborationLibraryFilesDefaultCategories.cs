using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_library_files_default_categories
    /// </summary>
    [ClassInfo("Elio_collaboration_library_files_default_categories")]
    public partial class ElioCollaborationLibraryFilesDefaultCategories : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }       
        /// <summary>
        /// Database Field : category_description
        /// </summary>
        [FieldInfo("category_description")]
        public string CategoryDescription { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : is_default
        /// </summary>
        [FieldInfo("is_default")]
        public int IsDefault { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
