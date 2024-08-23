using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioUsersSubIndustriesGroupItemsIJSubIndustriesGroupItems : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sub_industry_group_item_id")]
        public int SubIndustryGroupItemId { get; set; }

        [FieldInfo("description")]
        public string Description { get; set; }
    }
}