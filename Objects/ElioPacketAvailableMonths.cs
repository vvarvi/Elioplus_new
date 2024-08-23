using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packet_available_months
    /// </summary>
    [ClassInfo("Elio_packet_available_months")]
    public partial class ElioPacketAvailableMonths  : DataObject
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
        /// Database Field : months
        /// </summary>
        [FieldInfo("months")]
        public int Months { get; set; }
        /// <summary>
        /// Database Field : is_default
        /// </summary>
        [FieldInfo("is_default")]
        public int IsDefault { get; set; }
    }
}
