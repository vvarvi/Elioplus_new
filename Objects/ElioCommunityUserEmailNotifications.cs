using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_community_user_email_notifications")]
    public class ElioCommunityUserEmailNotifications : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("community_email_notifications_id")]
        public int CommunityEmailNotificationsId { get; set; }
    }
}