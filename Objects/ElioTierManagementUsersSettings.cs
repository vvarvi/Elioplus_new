using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_users_settings")]
    public class ElioTierManagementUsersSettings : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("period_id")]
        public int PeriodId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("commision")]
        public decimal Commision { get; set; }

        [FieldInfo("from_volume")]
        public decimal FromVolume { get; set; }

        [FieldInfo("to_volume")]
        public decimal ToVolume { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}