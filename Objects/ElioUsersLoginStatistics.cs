using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_login_statistics")]
    public partial class ElioUsersLoginStatistics : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("datein")]
        public DateTime Datein { get; set; }

        [FieldInfo("ip")]
        public string Ip { get; set; }

        [FieldInfo("session_id")]
        public string SessionId { get; set; }
    }
}