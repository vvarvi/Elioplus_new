using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_training_courses_chapters")]
    public partial class ElioUsersTrainingCoursesChapters : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("category_id")]
        public int CategoryId { get; set; }

        [FieldInfo("course_id")]
        public int CourserId { get; set; }

        [FieldInfo("chapter_title")]
        public string ChapterTitle { get; set; }

        [FieldInfo("chapter_text")]
        public string ChapterText { get; set; }

        [FieldInfo("chapter_file_path")]
        public string ChapterFilePath { get; set; }

        [FieldInfo("chapter_file_name")]
        public string ChapterFileName { get; set; }

        [FieldInfo("chapter_link")]
        public string ChapterLink { get; set; }
        
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_viewed")]
        public int IsViewed { get; set; }

        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }
       
        [FieldInfo("last_updated")]
        public DateTime LastUpDated { get; set; }
    }
}