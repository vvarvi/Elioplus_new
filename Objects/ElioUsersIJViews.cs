using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    public class ElioUsersIJViews : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("company_id")]
        public int CompanyId { get; set; }

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

        [FieldInfo("account_status")]
        public int AccountStatus { get; set; }

        [FieldInfo("company_logo")]
        public string CompanyLogo { get; set; }

        [FieldInfo("views")]
        public int Views { get; set; }
    }
}