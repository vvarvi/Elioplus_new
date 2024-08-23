using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [Serializable]

    [ClassInfo("connection_list")]
    public class ConnectionList : DataObject
    {
        [FieldInfo("connection_id", IsPrimaryKey = true, IsIdentity = true)]        
        public int ConnectionId { get; set; }

        [FieldInfo("connection_user_id")]
        public int ConnectionUserId { get; set; }

        [FieldInfo("connection_company_id")]
        public int ConnectionCompanyId { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("user_application_type")]
        public int UserApplicationType { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_new")]
        public int IsNew { get; set; }

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

        [FieldInfo("can_be_viewed")]
        public int CanBeViewed { get; set; }

        [FieldInfo("company_email")]
        public string CompanyEmail { get; set; }

        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }
    }
}