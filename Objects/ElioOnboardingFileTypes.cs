using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_onboarding_file_types
    /// </summary>
    [ClassInfo("Elio_onboarding_file_types")]
    public partial class ElioOnboardingFileTypes : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }       
        /// <summary>
        /// Database Field : file_type
        /// </summary>
        [FieldInfo("file_type")]
        public string FileType { get; set; }
        /// <summary>
        /// Database Field : file_format_extensions
        /// </summary>
        [FieldInfo("file_format_extensions")]
        public string FileFormatExtensions { get; set; }       
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
