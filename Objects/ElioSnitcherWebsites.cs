using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_snitcher_websites
    /// </summary>
    [ClassInfo("Elio_snitcher_websites")]
    public partial class ElioSnitcherWebsites : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : website_id
        /// </summary>
        [FieldInfo("website_id")]
        public string WebsiteId { get; set; }
        /// <summary>
        /// Database Field : url
        /// </summary>
        [FieldInfo("url")]
        public string Url { get; set; }
        /// <summary>
        /// Database Field : tracking_script_id
        /// </summary>
        [FieldInfo("tracking_script_id")]
        public string TrackingScriptId { get; set; }
        /// <summary>
        /// Database Field : tracking_script_snippet
        /// </summary>
        [FieldInfo("tracking_script_snippet")]
        public string TrackingScriptSnippet { get; set; }
        /// <summary>
        /// Database Field : path
        /// </summary>
        //[FieldInfo("path")]
        //public string Path { get; set; }
        /// <summary>
        /// Database Field : first_page_url
        /// </summary>
        //[FieldInfo("first_page_url")]
        //public string FirstPageUrl { get; set; }
        /// <summary>
        /// Database Field : total
        /// </summary>
        //[FieldInfo("total")]
        //public int Total { get; set; }
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