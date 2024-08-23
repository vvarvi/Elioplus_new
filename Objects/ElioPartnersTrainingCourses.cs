using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_partners_training_courses")]
    public partial class ElioPartnersTrainingCourses : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }

        [FieldInfo("partner_id")]
        public int PartnerId { get; set; }

        [FieldInfo("category_id")]
        public int CategoryId { get; set; }

        [FieldInfo("course_id")]
        public int CourseId { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }
       
        [FieldInfo("last_updated")]
        public DateTime? LastUpDated { get; set; }
    }
}