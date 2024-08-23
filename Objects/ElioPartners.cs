using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_partners")]
    public class ElioPartners : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("partner_description")]
        public string PartnerDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}