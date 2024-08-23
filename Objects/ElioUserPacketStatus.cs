using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_user_packet_status
    /// </summary>
    [ClassInfo("Elio_user_packet_status")]
    public partial class ElioUserPacketStatus : DataObject
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
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
        /// <summary>
        /// Database Field : user_billing_type
        /// </summary>
        [FieldInfo("user_billing_type")]
        public int UserBillingType { get; set; }
        /// <summary>
        /// Database Field : available_leads_count
        /// </summary>
        [FieldInfo("available_leads_count")]
        public int AvailableLeadsCount { get; set; }
        /// <summary>
        /// Database Field : available_messages_count
        /// </summary>
        [FieldInfo("available_messages_count")]
        public int AvailableMessagesCount { get; set; }
        /// <summary>
        /// Database Field : available_connections_count
        /// </summary>
        [FieldInfo("available_connections_count")]
        public int AvailableConnectionsCount { get; set; }        
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
        /// Database Field : expiration_date
        /// </summary>
        [FieldInfo("starting_date")]
        public DateTime? StartingDate { get; set; }
        /// <summary>
        /// Database Field : expiration_date
        /// </summary>
        [FieldInfo("expiration_date")]
        public DateTime? ExpirationDate { get; set; }
        /// <summary>
        /// Database Field : available_managePartners_count
        /// </summary>
        [FieldInfo("available_managePartners_count")]
        public int AvailableManagePartnersCount { get; set; }
        /// <summary>
        /// Database Field : available_libraryStorage_count
        /// </summary>
        [FieldInfo("available_libraryStorage_count")]
        public Decimal AvailableLibraryStorageCount { get; set; }
    }
}
