using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_snitcher_leads_pageviews
    /// </summary>
    [ClassInfo("Elio_snitcher_user_country_products")]
    public partial class ElioSnitcherUserCountryProducts : DataObject
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
        /// Database Field : country
        /// </summary>
        [FieldInfo("country")]
        public string Country { get; set; }
        /// <summary>
        /// Database Field : products
        /// </summary>
        [FieldInfo("products")]
        public string Products { get; set; }
        /// <summary>
        /// Database Field : is_active
        /// </summary>
        [FieldInfo("is_active")]
        public int IsActive { get; set; }
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