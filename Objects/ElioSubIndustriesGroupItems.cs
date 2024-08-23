using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_sub_industries_group_items")]
    public class ElioSubIndustriesGroupItems : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("sub_industies_group_id")]
        public int SubIndustriesGroupId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("opportunities")]
        public int Opportunities { get; set; }
    }
}