using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_training_category_course_permissions")]
    public partial class ElioUsersTrainingCategoryCoursePermissions : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("category_id")]
        public int CategoryId { get; set; }

        [FieldInfo("course_id")]
        public int CourseId { get; set; }

        [FieldInfo("training_group_id")]
        public int TrainingGroupId { get; set; }

        [FieldInfo("tier_id")]
        public int TierId { get; set; }

        [FieldInfo("countries")]
        public string Countries { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}