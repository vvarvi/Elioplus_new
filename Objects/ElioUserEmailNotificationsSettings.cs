using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_user_email_notifications_settings")]
    public class ElioUserEmailNotificationsSettings : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("email_notifications_id")]
        public int EmaiNotificationsId { get; set; }
    }
}