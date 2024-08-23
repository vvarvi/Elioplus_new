using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_apies")]
    public class ElioApies : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("api_description")]
        public string ApiDescription { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}