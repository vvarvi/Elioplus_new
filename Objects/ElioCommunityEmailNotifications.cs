using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_community_email_notifications")]
    public class ElioCommunityEmailNotifications : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("email_description")]
        public string EmailDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}