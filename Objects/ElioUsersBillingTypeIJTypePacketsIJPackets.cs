using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersBillingTypeIJTypePacketsIJPackets : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : type_description
        /// </summary>
        [FieldInfo("type_description")]
        public string TypeDescription { get; set; }        
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : billing_type_id
        /// </summary>
        [FieldInfo("billing_type_id")]
        public int BillingTypeId { get; set; }
        /// <summary>
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
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
        /// Database Field : is_default
        /// </summary>
        [FieldInfo("is_default")]
        public int IsDefault { get; set; }
        /// <summary>
        /// Database Field : vat
        /// </summary>
        [FieldInfo("vat")]
        public Decimal Vat { get; set; }
    }
}
