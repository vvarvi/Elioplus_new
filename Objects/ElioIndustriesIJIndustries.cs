using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class ElioIndustriesIJIndustries : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("industry_description")]
        public string IndustryDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }
    }
}