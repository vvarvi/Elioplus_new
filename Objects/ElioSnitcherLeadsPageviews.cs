using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_snitcher_leads_pageviews
    /// </summary>
    [ClassInfo("Elio_snitcher_leads_pageviews")]
    public partial class ElioSnitcherLeadsPageviews : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : lead_id
        /// </summary>
        [FieldInfo("lead_id")]
        public string LeadId { get; set; }
        /// <summary>
        /// Database Field : elio_website_leads_id
        /// </summary>
        [FieldInfo("elio_website_leads_id")]
        public int ElioWebsiteLeadsId { get; set; }
        /// <summary>
        /// Database Field : url
        /// </summary>
        [FieldInfo("url")]
        public string Url { get; set; }
        /// <summary>
        /// Database Field : product
        /// </summary>
        [FieldInfo("product")]
        public string Product { get; set; }
        /// <summary>
        /// Database Field : time_spent
        /// </summary>
        [FieldInfo("time_spent")]
        public int TimeSpent { get; set; }
        /// <summary>
        /// Database Field : action_time
        /// </summary>
        [FieldInfo("action_time")]
        public DateTime? ActionTime { get; set; }
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
    }
}