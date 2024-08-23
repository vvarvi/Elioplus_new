using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packets
    /// </summary>
    [ClassInfo("Elio_users_billing_type")]
    public partial class ElioUsersBillingType : DataObject
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
    }
}
