using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup : DataObject
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
        /// <summary>
        /// Database Field : description
        /// </summary>
        [FieldInfo("description")]
        public string Description { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : opportunities
        /// </summary>
        [FieldInfo("opportunities")]
        public int Opportunities { get; set; }
        /// <summary>
        /// Database Field : group_id
        /// </summary>
        [FieldInfo("group_id")]
        public int GroupId { get; set; }
        /// <summary>
        /// Database Field : group_description
        /// </summary>
        [FieldInfo("group_description")]
        public string GroupDescription { get; set; }
    }
}
