using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioCollaborationMailboxIJUsersMailBox : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : mailbox_id
        /// </summary>
        [FieldInfo("mailbox_id")]
        public int MailboxId { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : master_user_id
        /// </summary>
        [FieldInfo("master_user_id")]
        public int MasterUserId { get; set; }
        /// <summary>
        /// Database Field : partner_user_id
        /// </summary>
        [FieldInfo("partner_user_id")]
        public int PartnerUserId { get; set; }
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : is_viewed
        /// </summary>
        [FieldInfo("is_viewed")]
        public int IsViewed { get; set; }
        /// <summary>
        /// Database Field : is_deleted
        /// </summary>
        [FieldInfo("is_deleted")]
        public int IsDeleted { get; set; }
        /// <summary>
        /// Database Field : message_content
        /// </summary>
        [FieldInfo("message_content")]
        public string MessageContent { get; set; }
        /// <summary>
        /// Database Field : date_created
        /// </summary>
        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : date_send
        /// </summary>
        [FieldInfo("date_send")]
        public DateTime DateSend { get; set; }
        /// <summary>
        /// Database Field : date_received
        /// </summary>
        [FieldInfo("date_received")]
        public DateTime DateReceived { get; set; }
        /// <summary>
        /// Database Field : date_viewed
        /// </summary>
        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }
        /// <summary>
        /// Database Field : date_deleted
        /// </summary>
        [FieldInfo("date_deleted")]
        public DateTime DateDeleted { get; set; }
        /// <summary>
        /// Database Field : total_reply_comments
        /// </summary>
        [FieldInfo("total_reply_comments")]
        public int TotalReplyComments { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
