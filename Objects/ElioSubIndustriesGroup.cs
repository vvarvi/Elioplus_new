using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_sub_industries_group")]
    public class ElioSubIndustriesGroup : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("group_description")]
        public string SubIndustryDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}