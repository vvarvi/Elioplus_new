using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_partners
    /// </summary>
    [ClassInfo("Elio_collaboration_vendors_resellers")]
    public partial class ElioCollaborationVendorsResellers : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
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
        /// Database Field : is_confirmed
        /// </summary>
        [FieldInfo("invitation_status")]
        public string InvitationStatus { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : tier_status
        /// </summary>
        [FieldInfo("tier_status")]
        public string TierStatus { get; set; }
        /// <summary>
        /// Database Field : is_set_by_vendor
        /// </summary>
        [FieldInfo("is_set_by_vendor")]
        public int IsSetByVendor { get; set; }
    }
}
