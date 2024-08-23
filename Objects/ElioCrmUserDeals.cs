using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_crm_user_deals")]
    public class ElioCrmUserDeals : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("crm_integration_id")]
        public int CrmIntegrationId { get; set; }

        [FieldInfo("crm_deal_id")]
        public string CrmDeadId { get; set; }

        [FieldInfo("deal_id")]
        public int DealId { get; set; }

        [FieldInfo("date_insert")]
        public DateTime DateInsert { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}