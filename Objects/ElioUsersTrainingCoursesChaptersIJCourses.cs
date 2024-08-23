using System;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersTrainingCoursesChaptersIJCourses : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("category_id")]
        public int CategoryId { get; set; }

        [FieldInfo("course_id")]
        public int CourserId { get; set; }

        [FieldInfo("course_description")]
        public string CourseDescription { get; set; }

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
    }
}