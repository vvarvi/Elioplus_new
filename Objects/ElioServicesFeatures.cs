using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packet_features_items
    /// </summary>
    [ClassInfo("Elio_services_features")]
    public partial class ElioServicesFeatures : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : service_id
        /// </summary>
        [FieldInfo("service_id")]
        public int ServiceId { get; set; }
        /// <summary>
        /// Database Field : duration
        /// </summary>
        [FieldInfo("duration")]
        public int Duration { get; set; }       
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
        /// <summary>
        /// Database Field : cost
        /// </summary>
        [FieldInfo("cost")]
        public Decimal Cost { get; set; }
        /// <summary>
        /// Database Field : minimum_commitment
        /// </summary>
        [FieldInfo("minimum_commitment")]
        public int MinimumCommitment { get; set; }
    }
}
