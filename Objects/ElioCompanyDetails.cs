using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioCompanyDetails : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("username")]
        public string Username { get; set; }

        [FieldInfo("password")]
        public string Password { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }

        [FieldInfo("ip")]
        public string Ip { get; set; }

        [FieldInfo("phone")]
        public string Phone { get; set; }

        [FieldInfo("address")]
        public string Address { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("website")]
        public string WebSite { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("overview")]
        public string Overview { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("is_completed")]
        public int IsCompleted { get; set; }

        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }

        [FieldInfo("guid")]
        public string GuId { get; set; }

        [FieldInfo("industry_description")]
        public string IndustryDescription { get; set; }

        [FieldInfo("market_description")]
        public string MarketDescription { get; set; }

        [FieldInfo("partner_description")]
        public string PartnerDescription { get; set; }

        [FieldInfo("api_description")]
        public string ApiDescription { get; set; }
    }
}