using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_blacklisted_emails")]
    public class ElioBlacklistedEmails : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("email")]
        public string Domain{ get; set; }

        [FieldInfo("is_black_listed")]
        public int IsBlackListed { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}