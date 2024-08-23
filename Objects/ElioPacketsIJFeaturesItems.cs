using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// </summary>
    public partial class ElioPacketsIJFeaturesItems : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : pack_description
        /// </summary>
        [FieldInfo("pack_description")]
        public string PackDescription { get; set; }
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
        /// Database Field : free_items_trial_no
        /// </summary>
        [FieldInfo("free_items_trial_no")]
        public int FreeItemsTrialNo { get; set; }
        /// <summary>
        /// Database Field : item_cost_with_no_vat
        /// </summary>
        [FieldInfo("item_cost_with_no_vat")]
        public decimal ItemCostWithNoVat { get; set; }
        /// <summary>
        /// Database Field : item_cost_with_vat
        /// </summary>
        [FieldInfo("item_cost_with_vat")]
        public decimal ItemCostWithVat { get; set; }
        /// <summary>
        /// Database Field : vat
        /// </summary>
        [FieldInfo("vat")]
        public decimal Vat { get; set; }
        /// <summary>
        /// Database Field : item_description
        /// </summary>
        [FieldInfo("item_description")]
        public string ItemDescription { get; set; }
    }
}
