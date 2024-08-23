using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [Serializable]

    [ClassInfo("connection_search_list")]
    public class ConnectionSearchList : DataObject
    {
        [FieldInfo("connection_id", IsPrimaryKey = true, IsIdentity = true)]        
        public int ConnectionId { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("user_application_type")]
        public string UserApplicationType { get; set; }

        [FieldInfo("is_public")]
        public string IsPublic { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }

        [FieldInfo("avatar")]
        public string Avatar { get; set; }

        [FieldInfo("logo")]
        public string Logo { get; set; }

        [FieldInfo("company_website")]
        public string CompanyWebsite { get; set; }

        [FieldInfo("sysdate")]
        public string Sysdate { get; set; }

        [FieldInfo("industry")]
        public string Industry { get; set; }

        [FieldInfo("categories")]
        public string Categories { get; set; }

        [FieldInfo("products")]
        public string Products { get; set; }

        [FieldInfo("annual_revenue_range")]
        public string AnnualRevenueRange { get; set; }

        [FieldInfo("employees_range")]
        public string EmployeesRange { get; set; }

        [FieldInfo("programs")]
        public string Programs { get; set; }

        [FieldInfo("overview")]
        public string Overview { get; set; }
    }
}