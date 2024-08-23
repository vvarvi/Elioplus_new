using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("user_id")]
        public int UserId { get; set; }

        [FieldInfo("sub_industry_group_item_id")]
        public int SubIndustryGroupItemId { get; set; }

        [FieldInfo("sub_industies_group_id")]
        public int SubIndustryGroupId { get; set; }

        [FieldInfo("description")]
        public string DescriptionSubcategory { get; set; }

        [FieldInfo("country")]
        public string Country { get; set; }

        [FieldInfo("website")]
        public string WebSite { get; set; }

        [FieldInfo("email")]
        public string Email { get; set; }

        [FieldInfo("official_email")]
        public string OfficialEmail { get; set; }

        [FieldInfo("company_name")]
        public string CompanyName { get; set; }
    }
}