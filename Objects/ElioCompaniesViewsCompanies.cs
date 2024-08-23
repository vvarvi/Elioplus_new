using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_companies_views_companies")]
    public class ElioCompaniesViewsCompanies : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("company_views_company_id")]
        public int CompanyViewsCompanyId { get; set; }       

        [FieldInfo("interested_company_id")]
        public int InterestedCompanyId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

        [FieldInfo("is_deleted")]
        public int IsDeleted { get; set; }

        [FieldInfo("can_be_viewed")]
        public int CanBeViewed { get; set; }
    }
}