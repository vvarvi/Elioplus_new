using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    public class ElioProductServicesCategoriesIJItems : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("category_description")]
        public string CategoryDescription { get; set; }

        [FieldInfo("service_description")]
        public string ServiceDescription { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }

        [FieldInfo("is_active")]
        public int IsActive { get; set; }
    }
}