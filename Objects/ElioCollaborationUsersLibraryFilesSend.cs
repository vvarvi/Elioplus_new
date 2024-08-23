using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_library_files_send
    /// </summary>
    [ClassInfo("Elio_collaboration_users_library_files_send")]
    public partial class ElioCollaborationUsersLibraryFilesSend : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : file_id
        /// </summary>
        [FieldInfo("file_id")]
        public int FileId { get; set; }
        /// <summary>
        /// Database Field : mailbox_id
        /// </summary>
        [FieldInfo("mailbox_id")]
        public int MailboxId { get; set; }
        /// <summary>
        /// Database Field : user_id_from
        /// </summary>
        [FieldInfo("user_id_from")]
        public int UserIdFrom { get; set; }
        /// <summary>
        /// Database Field : user_id_to
        /// </summary>
        [FieldInfo("user_id_to")]
        public int UserIdTo { get; set; }
        /// <summary>
        /// Database Field : group_id
        /// </summary>
        [FieldInfo("group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// Database Field : date_send
        /// </summary>
        [FieldInfo("date_send")]
        public DateTime DateSend { get; set; }        
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}
