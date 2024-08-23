using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_vendors_members_resellers
    /// </summary>
    [ClassInfo("Elio_collaboration_vendors_members_resellers")]
    public partial class ElioCollaborationVendorsMembersResellers : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : assign_by_user_id
        /// </summary>
        [FieldInfo("assign_by_user_id")]
        public int AssignByUserId { get; set; }
        /// <summary>
        /// Database Field : sub_account_id
        /// </summary>
        [FieldInfo("sub_account_id")]
        public int SubAccountId { get; set; }
        /// <summary>
        /// Database Field : partner_user_id
        /// </summary>
        [FieldInfo("partner_user_id")]
        public int PartnerUserId { get; set; }
        /// <summary>
        /// Database Field : vendor_reseller_id
        /// </summary>
        [FieldInfo("vendor_reseller_id")]
        public int VendorResellerId { get; set; }
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
