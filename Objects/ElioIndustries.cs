using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_industries")]
    public class ElioIndustries : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("industry_description")]
        public string IndustryDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}