using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packets
    /// </summary>
    [ClassInfo("Elio_users_billing_type_packets")]
    public partial class ElioUsersBillingTypePackets : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }        
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
    }
}
