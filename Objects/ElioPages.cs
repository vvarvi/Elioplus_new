using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_pages")]
    public partial class ElioPages : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("page_link")]
        public string page_link { get; set; }

        [FieldInfo("category")]
        public string category { get; set; }

        [FieldInfo("is_public")]
        public int is_public { get; set; }
    }
}