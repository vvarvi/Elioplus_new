using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_onboarding_file_types
    /// </summary>
    [ClassInfo("Elio_onboarding_users_video_library_files")]
    public partial class ElioOnboardingUsersVideoLibraryFiles : DataObject
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
        /// Database Field : video_name
        /// </summary>
        [FieldInfo("video_name")]
        public string VideoName { get; set; }       
        /// <summary>
        /// Database Field : video_link
        /// </summary>
        [FieldInfo("video_link")]
        public string VideoLink { get; set; }
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
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
