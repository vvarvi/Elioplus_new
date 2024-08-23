using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_invitations
    /// </summary>
    [ClassInfo("Elio_opportunities_notes")]
    public partial class ElioOpportunitiesNotes : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : add_by_user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : opportunity_user_id
        /// </summary>
        [FieldInfo("opportunity_user_id")]
        public int OpportunityUserId { get; set; }
        /// <summary>
        /// Database Field : note_subject
        /// </summary>
        [FieldInfo("note_subject")]
        public string NoteSubject { get; set; }
        /// <summary>
        /// Database Field : note_content
        /// </summary>
        [FieldInfo("note_content")]
        public string NoteContent { get; set; }
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
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}