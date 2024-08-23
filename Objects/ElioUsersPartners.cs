using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_partners")]
    public class ElioUsersPartners : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("partner_id")]
        public int PartnerId { get; set; }
    }
}