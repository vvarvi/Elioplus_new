using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packet_features_items
    /// </summary>
    [ClassInfo("Elio_packet_features_items")]
    public partial class ElioPacketFeaturesItems : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
        /// <summary>
        /// Database Field : feature_id
        /// </summary>
        [FieldInfo("feature_id")]
        public int FeatureId { get; set; }
        /// <summary>
        /// Database Field : free_items_no
        /// </summary>
        [FieldInfo("free_items_no")]
        public int FreeItemsNo { get; set; }
        /// <summary>
        /// Database Field : item_cost_vat
        /// </summary>
        [FieldInfo("item_cost_vat")]
        public Decimal ItemCostVat { get; set; }
        /// <summary>
        /// Database Field : item_cost_with_vat
        /// </summary>
        [FieldInfo("item_cost_with_vat")]
        public Decimal ItemCostWithVat { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : item_cost_with_no_vat
        /// </summary>
        [FieldInfo("item_cost_with_no_vat")]
        public Decimal ItemCostWithNoVat { get; set; }
        /// <summary>
        /// Database Field : free_items__trial_no
        /// </summary>
        [FieldInfo("free_items_trial_no")]
        public int FreeItemsTrialNo { get; set; }
    }
}
