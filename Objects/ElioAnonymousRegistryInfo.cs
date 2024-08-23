using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_anonymous_registry_info")]
    public class ElioAnonymousRegistryInfo : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("url")]
        public string Url { get; set; }

        [FieldInfo("ip_address")]
        public string IpAddress { get; set; }

        [FieldInfo("path")]
        public string Path { get; set; }

        [FieldInfo("type")]
        public string Type { get; set; }

        [FieldInfo("host_name")]
        public string HostName { get; set; }

        [FieldInfo("company_domain")]
        public string CompanyDomain { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}