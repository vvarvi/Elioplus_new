using System;
using System.Linq;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    public partial class ElioUsersFavoritesIJUsers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("company_id")]
        public int CompanyId { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }

        [FieldInfo("company_type")]
        public string CompanyType { get; set; }

        [FieldInfo("sysdate")]
        public DateTime SysDate { get; set; }
    }
}