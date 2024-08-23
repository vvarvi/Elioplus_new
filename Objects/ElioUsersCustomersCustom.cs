using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_customers_custom")]
    public class ElioUsersCustomersCustom : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("exist_in_customers")]
        public int ExistInCustomers { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
    }
}