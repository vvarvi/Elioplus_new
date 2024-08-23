using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_packet_features_items
    /// </summary>
    [ClassInfo("Elio_users_services")]
    public partial class ElioUsersServices : DataObject
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
        /// Database Field : service_id
        /// </summary>
        [FieldInfo("service_id")]
        public int ServiceId { get; set; }        
        /// <summary>
        /// Database Field : started_date
        /// </summary>
        [FieldInfo("started_date")]
        public DateTime StartedDate { get; set; }
        /// <summary>
        /// Database Field : expiration_date
        /// </summary>
        [FieldInfo("expiration_date")]
        public DateTime ExpirationDate { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
        /// <summary>
        /// Database Field : is_canceled
        /// </summary>
        [FieldInfo("is_canceled")]
        public int IsCanceled { get; set; }
        /// <summary>
        /// Database Field : canceled_date
        /// </summary>
        [FieldInfo("canceled_date")]
        public DateTime CanceledDate { get; set; }
        /// <summary>
        /// Database Field : canceled_by
        /// </summary>
        [FieldInfo("canceled_by")]
        public string CanceledBy { get; set; }
        /// <summary>
        /// Database Field : comments
        /// </summary>
        [FieldInfo("comments")]
        public string Comments { get; set; }
    }
}
