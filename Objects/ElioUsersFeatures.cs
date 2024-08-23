using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_features
    /// </summary>
    [ClassInfo("Elio_users_features")]
    public partial class ElioUsersFeatures  : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_billing_type
        /// </summary>
        [FieldInfo("user_billing_type")]
        public int UserBillingType { get; set; }
        /// <summary>
        /// Database Field : pack_id
        /// </summary>
        [FieldInfo("pack_id")]
        public int PackId { get; set; }
        /// <summary>
        /// Database Field : total_leads
        /// </summary>
        [FieldInfo("total_leads")]
        public int TotalLeads { get; set; }
        /// <summary>
        /// Database Field : total_messages
        /// </summary>
        [FieldInfo("total_messages")]
        public int TotalMessages { get; set; }
        /// <summary>
        /// Database Field : total_connections
        /// </summary>
        [FieldInfo("total_connections")]
        public int TotalConnections { get; set; }
        /// <summary>
        /// Database Field : has_search_limit
        /// </summary>
        [FieldInfo("has_search_limit")]
        public int HasSearchLimit { get; set; }
        /// <summary>
        /// Database Field : total_search_results
        /// </summary>
        [FieldInfo("total_search_results")]
        public int TotalSearchResults { get; set; }
        /// <summary>
        /// Database Field : total_manage_partners
        /// </summary>
        [FieldInfo("total_manage_partners")]
        public int TotalManagePartners { get; set; }
        /// <summary>
        /// Database Field : total_library_storage
        /// </summary>
        [FieldInfo("total_library_storage")]
        public int TotalLibraryStorage { get; set; }
    }
}
