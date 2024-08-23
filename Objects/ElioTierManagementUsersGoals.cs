using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_tier_management_users_goals")]
    public class ElioTierManagementUsersGoals : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }

        [FieldInfo("partner_id")]
        public int PartnerId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("year")]
        public string Year { get; set; }

        [FieldInfo("date_from")]
        public DateTime DateFrom { get; set; }

        [FieldInfo("date_to")]
        public DateTime DateTo { get; set; }
    }
}