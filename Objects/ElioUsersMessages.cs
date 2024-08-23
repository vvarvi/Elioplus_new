using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_messages")]
    public class ElioUsersMessages : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("sender_user_id")]
        public int SenderUserId { get; set; }

        [FieldInfo("receiver_user_id")]
        public int ReceiverUserId { get; set; }

        [FieldInfo("receiver_email")]
        public string ReceiverEmail { get; set; }

        [FieldInfo("receiver_official_email")]
        public string ReceiverOfficialEmail { get; set; }

        [FieldInfo("subject")]
        public string Subject { get; set; }

        [FieldInfo("message")]
        public string Message { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("sender_email")]
        public string SenderEmail { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

        [FieldInfo("deleted")]
        public int Deleted { get; set; }

        [FieldInfo("sent")]
        public int Sent { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}