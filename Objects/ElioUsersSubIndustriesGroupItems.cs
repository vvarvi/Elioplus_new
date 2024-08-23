using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_users_sub_industries_group_items")]
    public class ElioUsersSubIndustriesGroupItems : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sub_industry_group_item_id")]
        public int SubIndustryGroupItemId { get; set; }

        [FieldInfo("sub_industry_group_id")]
        public int SubIndustryGroupId { get; set; }
    }
}