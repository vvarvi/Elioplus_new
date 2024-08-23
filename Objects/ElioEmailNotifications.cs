using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_email_notifications")]
    public class ElioEmailNotifications : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("ref_code")]
        public string RefCode { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}