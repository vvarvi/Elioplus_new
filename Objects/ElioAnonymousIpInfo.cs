using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_anonymous_ip_info")]
    public class ElioAnonymousIpInfo : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("url")]
        public string Url { get; set; }

        [FieldInfo("ip_address")]
        public string IpAddress { get; set; }

        [FieldInfo("path")]
        public string Path { get; set; }

        [FieldInfo("city")]
        public string City { get; set; }

        [FieldInfo("region")]
        public string Region { get; set; }

        [FieldInfo("company_domain")]
        public string CompanyDomain { get; set; }

        [FieldInfo("org")]
        public string Org { get; set; }

        [FieldInfo("host_name")]
        public string HostName { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("country_name")]
        public string CountryName { get; set; }

        [FieldInfo("is_eu")]
        public string IsEu { get; set; }

        [FieldInfo("continent_name")]
        public string ContinentName { get; set; }

        [FieldInfo("continent_code")]
        public string ContinentCode { get; set; }

        [FieldInfo("timezone")]
        public string Timezone { get; set; }

        [FieldInfo("is_inserted")]
        public int IsInserted { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}