using System;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_partners_training_chapters")]
    public partial class ElioPartnersTrainingChapters : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }

        [FieldInfo("partner_id")]
        public int PartnerId { get; set; }

        [FieldInfo("course_id")]
        public int CourseId { get; set; }

        [FieldInfo("chapter_id")]
        public int ChapterId { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_viewed")]
        public int IsViewed { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }
       
        [FieldInfo("last_updated")]
        public DateTime? LastUpDated { get; set; }
    }
}