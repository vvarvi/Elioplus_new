using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class ElioCompaniesViewsCompaniesIJCompanies : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("interested_company_id")]
        public int InterestedCompanyId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("phone")]
        public string Phone { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }
    }
}