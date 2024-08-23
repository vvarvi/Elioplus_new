using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_library_files
    /// </summary>
    [ClassInfo("Elio_onboarding_library_files_default_categories")]
    public partial class ElioOnboardingLibraryFilesDefaultCategories : DataObject
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
