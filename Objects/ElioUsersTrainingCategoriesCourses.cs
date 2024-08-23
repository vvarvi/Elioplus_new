using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_training_categories_courses")]
    public partial class ElioUsersTrainingCategoriesCourses : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("category_id")]
        public int CategoryId { get; set; }

        [FieldInfo("course_description")]
        public string CourseDescription { get; set; }

        [FieldInfo("overview_text")]
        public string OverviewText { get; set; }

        [FieldInfo("overview_image_path")]
        public string OverviewImagePath { get; set; }

        [FieldInfo("overview_image_type")]
        public string OverviewImageType { get; set; }

        [FieldInfo("overview_image_name")]
        public string OverviewImageName { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }
       
        [FieldInfo("last_updated")]
        public DateTime LastUpDated { get; set; }
    }
}