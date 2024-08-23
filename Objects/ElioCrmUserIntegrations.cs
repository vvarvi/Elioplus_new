using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_crm_user_integrations")]
    public class ElioCrmUserIntegrations : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("crm_integration_id")]
        public int CrmIntegrationId { get; set; }

        [FieldInfo("user_api_key")]
        public string UserApiKey { get; set; }

        [FieldInfo("user_api_secret_key")]
        public string UserApiSecretKey { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}