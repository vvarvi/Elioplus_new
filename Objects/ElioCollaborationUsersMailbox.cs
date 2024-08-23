using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_mailbox
    /// </summary>
    [ClassInfo("Elio_collaboration_users_mailbox")]
    public partial class ElioCollaborationUsersMailbox : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : vendors_resellers_id
        /// </summary>
        [FieldInfo("vendors_resellers_id")]
        public int VendorsResellersId { get; set; }
        /// <summary>
        /// Database Field : mailbox_id
        /// </summary>
        [FieldInfo("mailbox_id")]
        public int MailboxId { get; set; }
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
