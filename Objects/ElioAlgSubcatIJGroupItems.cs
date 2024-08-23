using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioAlgSubcatIJGroupItems : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("subcategory_id")]
        public int SubcategoryId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }
    }
}