using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_billing_user_orders
    /// </summary>
    [ClassInfo("Elio_scheduler_notification_emails")]
    public partial class ElioSchedulerNotificationEmails : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : task_name
        /// </summary>
        [FieldInfo("task_name")]
        public string TaskName { get; set; }
        /// <summary>
        /// Database Field : case_id
        /// </summary>
        [FieldInfo("case_id")]
        public int CaseId { get; set; }
        /// <summary>
        /// Database Field : receiver_email_address
        /// </summary>
        [FieldInfo("receiver_email_address")]
        public string ReceiverEmailAddress { get; set; }
        /// <summary>
        /// Database Field : sender_email_address
        /// </summary>
        [FieldInfo("sender_email_address")]
        public string SenderEmailAddress { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : remind_date
        /// </summary>
        [FieldInfo("remind_date")]
        public DateTime? RemindDate { get; set; }
        /// <summary>
        /// Database Field : is_sent
        /// </summary>
        [FieldInfo("is_sent")]
        public int IsSent { get; set; }
        /// <summary>
        /// Database Field : date_sent
        /// </summary>
        [FieldInfo("date_sent")]
        public DateTime? DateSent { get; set; }
        /// <summary>
        /// Database Field : next_date_sent
        /// </summary>
        [FieldInfo("next_date_sent")]
        public DateTime? NextDateSent { get; set; }
        /// <summary>
        /// Database Field : count
        /// </summary>
        [FieldInfo("count")]
        public int Count { get; set; }
        /// <summary>
        /// Database Field : sent_limit_count
        /// </summary>
        [FieldInfo("sent_limit_count")]
        public int SentLimitCount { get; set; }
        /// <summary>
        /// Database Field : email_template
        /// </summary>
        [FieldInfo("email_template")]
        public string EmailTemplate { get; set; }
        /// <summary>
        /// Database Field : email_message
        /// </summary>
        [FieldInfo("email_message")]
        public string EmailMessage { get; set; }
        /// <summary>
        /// Database Field : email_subject
        /// </summary>
        [FieldInfo("email_subject")]
        public string EmailSubject { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Database Field : response_error
        /// </summary>
        [FieldInfo("response_error")]
        public string ResponseError { get; set; }
    }
}
