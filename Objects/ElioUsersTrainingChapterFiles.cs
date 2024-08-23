using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_training_chapter_files
    /// </summary>
    [ClassInfo("Elio_users_training_chapter_files")]
    public partial class ElioUsersTrainingChapterFiles : DataObject
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
        /// Database Field : file_title
        /// </summary>
        [FieldInfo("file_title")]
        public string FileTitle { get; set; }
        /// <summary>
        /// Database Field : file_name
        /// </summary>
        [FieldInfo("file_name")]
        public string FileName { get; set; }
        /// <summary>
        /// Database Field : file_path
        /// </summary>
        [FieldInfo("file_path")]
        public string FilePath { get; set; }
        /// <summary>
        /// Database Field : file_type
        /// </summary>
        [FieldInfo("file_type")]
        public string FileType { get; set; }
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
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : category_id
        /// </summary>
        [FieldInfo("category_id")]
        public int CategoryId { get; set; }
        /// <summary>
        /// Database Field : course_id
        /// </summary>
        [FieldInfo("course_id")]
        public int CourseId { get; set; }
        /// <summary>
        /// Database Field : chapter_id
        /// </summary>
        [FieldInfo("chapter_id")]
        public int ChapterId { get; set; }
        /// <summary>
        /// Database Field : preview_file_path
        /// </summary>
        [FieldInfo("preview_file_path")]
        public string PreviewFilePath { get; set; }
    }
}
