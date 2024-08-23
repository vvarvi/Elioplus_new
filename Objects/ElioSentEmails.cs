using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_sent_emails")]
    public partial class ElioSentEmails : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime? LastUpdate { get; set; }

        [FieldInfo("email_type")]
        public string EmailType { get; set; }

        [FieldInfo("email_description")]
        public string EmailDescription { get; set; }

        [FieldInfo("email_id")]
        public int EmailId { get; set; }

        [FieldInfo("email_subject")]
        public string EmailSubject { get; set; }

        [FieldInfo("email_from")]
        public string EmailFrom { get; set; }

        [FieldInfo("email_to")]
        public string EmailTo { get; set; }

        [FieldInfo("email_body")]
        public string EmailBody { get; set; }

        [FieldInfo("email_cc")]
        public string EmailCc { get; set; }

        [FieldInfo("email_bcc")]
        public string EmailBcc { get; set; }

        [FieldInfo("mailServer")]
        public string MailServer { get; set; }

        [FieldInfo("last_try_date")]
        public DateTime? LastTryDate { get; set; }

        [FieldInfo("repeat_times")]
        public int RepeatTimes { get; set; }

        [FieldInfo("sent")]
        public bool Sent { get; set; }

        [FieldInfo("isBodyHtml")]
        public bool IsBodyHtml { get; set; }

        [FieldInfo("last_error")]
        public string LastError { get; set; }

        [FieldInfo("user_id_from")]
        public int UserIdFrom { get; set; }

        [FieldInfo("user_id_to")]
        public int UserIdTo { get; set; }
    }
}