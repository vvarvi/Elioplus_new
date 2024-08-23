using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{    
    public partial class ElioUserPacketStatusIJBillingOrder : DataObject
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
        public DateTime? Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : current_period_start
        /// </summary>
        [FieldInfo("current_period_start")]
        public DateTime? CurrentPeriodStart { get; set; }
        /// <summary>
        /// Database Field : current_period_end
        /// </summary>
        [FieldInfo("current_period_end")]
        public DateTime? CurrentPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : trial_period_start
        /// </summary>
        [FieldInfo("trial_period_start")]
        public DateTime? TrialPeriodStart { get; set; }
        /// <summary>
        /// Database Field : trial_period_end
        /// </summary>
        [FieldInfo("trial_period_end")]
        public DateTime? TrialPeriodEnd { get; set; }
        /// <summary>
        /// Database Field : mode
        /// </summary>
        [FieldInfo("mode")]
        public string Mode { get; set; }
    }
}
