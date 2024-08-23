using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_training_categories")]
    public partial class ElioUsersTrainingCategories : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("category_description")]
        public string CategoryDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }
       
        [FieldInfo("last_updated")]
        public DateTime LastUpDated { get; set; }
    }
}