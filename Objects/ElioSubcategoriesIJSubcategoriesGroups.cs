using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioSubcategoriesIJSubcategoriesGroups : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }                

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sub_industry_group_item_id")]
        public int SubIndustryGroupItemId { get; set; }

        [FieldInfo("sub_industry_group_id")]
        public int SubIndustryGroupId { get; set; }

        [FieldInfo("description")]
        public string SubCategoryDescription { get; set; }

        [FieldInfo("group_description")]
        public string SubcategoryGroupDescription { get; set; }
    }
}