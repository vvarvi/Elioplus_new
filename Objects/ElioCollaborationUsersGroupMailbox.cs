using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_group_mailbox
    /// </summary>
    [ClassInfo("Elio_collaboration_users_group_mailbox")]
    public partial class ElioCollaborationUsersGroupMaibox  : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }        
        /// <summary>
        /// Database Field : group_id
        /// </summary>
        [FieldInfo("group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// Database Field : sender_user_id
        /// </summary>
        [FieldInfo("sender_user_id")]
        public int SenderUserId { get; set; }
        /// <summary>
        /// Database Field : receiver_user_id
        /// </summary>
        [FieldInfo("receiver_user_id")]
        public int ReceiverUserId { get; set; }
        /// <summary>
        /// Database Field : mailbox_id
        /// </summary>
        [FieldInfo("mailbox_id")]
        public int MailboxId { get; set; }
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
        /// Database Field : date_viewed
        /// </summary>
        [FieldInfo("date_viewed")]
        public DateTime? DateViewed { get; set; }
        /// <summary>
        /// Database Field : is_deleted
        /// </summary>
        [FieldInfo("is_deleted")]
        public int IsDeleted { get; set; }
        /// <summary>
        /// Database Field : date_deleted
        /// </summary>
        [FieldInfo("date_deleted")]
        public DateTime? DateDeleted { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
