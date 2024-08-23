using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_notification_emails")]
    public partial class ElioUsersNotificationEmails : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("notification_email_date")]
        public DateTime NotificationEmailDate { get; set; }

        [FieldInfo("send_by_user")]
        public int SendByUser { get; set; }

    }
}