using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_anonymous_ip_info_domains")]
    public class ElioAnonymousIpInfoDomains : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("info_id")]
        public int InfoId { get; set; }

        [FieldInfo("domain")]
        public string Domain { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("lastupdate")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}