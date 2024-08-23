using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_group_retailors_mailbox
    /// </summary>
    [ClassInfo("Elio_collaboration_users_group_retailors_mailbox")]
    public partial class ElioCollaborationUsersGroupRetailorsMailbox  : DataObject
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
        /// Database Field : collaboration_group_id
        /// </summary>
        [FieldInfo("collaboration_group_id")]
        public int CollaborationGroupId { get; set; }
        /// <summary>
        /// Database Field : collaboration_vendors_resellers_id
        /// </summary>
        [FieldInfo("collaboration_vendors_resellers_id")]
        public int CollaborationVendorsResellersId { get; set; }
        /// <summary>
        /// Database Field : collaboration_users_mailbox
        /// </summary>
        [FieldInfo("collaboration_users_mailbox")]
        public int CollaborationUsersMailbox { get; set; }
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
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
