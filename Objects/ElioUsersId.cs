using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users")]
    public partial class ElioUsersId : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("billing_type")]
        public int BillingType { get; set; }
    }
}