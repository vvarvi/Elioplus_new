using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_user_partner_program_rating")]
    public class ElioUserPartnerProgramRating : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("rate")]
        public int Rate { get; set; }

        [FieldInfo("company_id")]
        public int CompanyId { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("visitor_id")]
        public int VisitorId { get; set; }
    }
}