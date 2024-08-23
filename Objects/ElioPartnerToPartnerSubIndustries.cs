using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_partner_to_partner_sub_industries
    /// </summary>
    [ClassInfo("Elio_partner_to_partner_sub_industries")]
    public partial class ElioPartnerToPartnerSubIndustries : DataObject
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
        /// Database Field : p2p_id
        /// </summary>
        [FieldInfo("p2p_id")]
        public int P2pId { get; set; }
        /// <summary>
        /// Database Field : sub_industry_group_item_id
        /// </summary>
        [FieldInfo("sub_industry_group_item_id")]
        public int SubIndustryGroupItemId { get; set; }
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
    }
}
