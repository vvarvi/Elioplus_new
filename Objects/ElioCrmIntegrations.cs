using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_crm_integrations")]
    public class ElioCrmIntegrations : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("crm_name")]
        public string CrmName { get; set; }

        [FieldInfo("crm_description")]
        public string CrmDescription { get; set; }

        [FieldInfo("date_created")]
        public DateTime DateCreated { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("crm_logo")]
        public string CrmLogo { get; set; }

        [FieldInfo("api_key_description")]
        public string ApiKeyDescription { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}