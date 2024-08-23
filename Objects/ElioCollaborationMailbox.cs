using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_mailbox
    /// </summary>
    [ClassInfo("Elio_collaboration_mailbox")]
    public partial class ElioCollaborationMailbox : DataObject
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
