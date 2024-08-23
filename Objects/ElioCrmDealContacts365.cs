using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_crm_deal_contacts_365")]
    public class ElioCrmDealContacts365 : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("elio_deal_id")]
        public int ElioDealId { get; set; }

        [FieldInfo("deal_email")]
        public string DealEmail { get; set; }

        [FieldInfo("crm_account_id")]
        public string CrmAccountId { get; set; }

        [FieldInfo("crm_contact_id")]
        public string CrmContactId { get; set; }

        [FieldInfo("date_insert")]
        public DateTime DateInsert { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}