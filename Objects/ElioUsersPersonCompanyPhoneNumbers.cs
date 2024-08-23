using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_users_person_company_phone_numbers
    /// </summary>
    [ClassInfo("Elio_users_person_company_phone_numbers")]
    public partial class ElioUsersPersonCompanyPhoneNumbers : DataObject
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
        /// Database Field : elio_person_company_id
        /// </summary>
        [FieldInfo("elio_person_company_id")]
        public int ElioPersonCompanyId { get; set; }
        /// <summary>
        /// Database Field : clearbit_company_id
        /// </summary>
        [FieldInfo("clearbit_company_id")]
        public string ClearbitCompanyId { get; set; }
        /// <summary>
        /// Database Field : phone_number
        /// </summary>
        [FieldInfo("phone_number")]
        public string PhoneNumber { get; set; }       
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
