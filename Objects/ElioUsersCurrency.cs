using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_currency")]
    public class ElioUsersCurrency : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("cur_id")]
        public string CurId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}