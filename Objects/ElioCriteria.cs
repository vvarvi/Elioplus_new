using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_criteria")]
    public class ElioCriteria : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}