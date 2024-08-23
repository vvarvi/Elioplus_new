using System;
using System.Collections.Generic;
using System.Text;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_registration_deals_vendor_settings
    /// </summary>
    [ClassInfo("Elio_registration_deals_vendor_settings")]
    public partial class ElioRegistrationDealsVendorSettings : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }
        /// <summary>
        /// Database Field : deal_duration_setting
        /// </summary>
        [FieldInfo("deal_duration_setting")]
        public int DealDurationSetting { get; set; }
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
