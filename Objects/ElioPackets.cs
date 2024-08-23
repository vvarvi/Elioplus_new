using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packets
    /// </summary>
    [ClassInfo("Elio_packets")]
    public partial class ElioPackets : DataObject
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
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : vat
        /// </summary>
        [FieldInfo("vat")]
        public Decimal Vat { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_default")]
        public int IsDefault { get; set; }
        /// <summary>
        /// Database Field : product_id
        /// </summary>
        [FieldInfo("product_id")]
        public int productId { get; set; }
        /// <summary>
        /// Database Field : stripe_plan_id
        /// </summary>
        [FieldInfo("stripe_plan_id")]
        public string stripePlanId { get; set; }
        /// <summary>
        /// Database Field : stripe_plan_old_code
        /// </summary>
        [FieldInfo("stripe_plan_old_code")]
        public string StripePlanOldCode { get; set; }
    }
}
