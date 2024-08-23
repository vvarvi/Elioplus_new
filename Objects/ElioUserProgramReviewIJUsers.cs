using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    public class ElioUserProgramReviewIJUsers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("visitor_id")]
        public int VisitorId { get; set; }

        [FieldInfo("username")]
        public string Username { get; set; }

        [FieldInfo("company_name")]
        public string ComapnyName { get; set; }

        [FieldInfo("company_logo")]
        public string ComapnyLogo { get; set; }

        [FieldInfo("account_status")]
        public int AccountStatus { get; set; }

        [FieldInfo("company_id")]
        public int CompanyId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }

        [FieldInfo("review")]
        public string Review { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}