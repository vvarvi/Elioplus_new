using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_opportunities_users_sub_categories_status
    /// </summary>
    [ClassInfo("Elio_opportunities_users_sub_categories_status")]
    public partial class ElioOpportunitiesUsersSubCategoriesStatus : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : opportunity_id
        /// </summary>
        [FieldInfo("opportunity_id")]
        public int OpportunityId { get; set; } 
        /// <summary>
        /// Database Field : opportunity_status_id
        /// </summary>
        [FieldInfo("opportunity_status_id")]
        public int OpportunityStatusId { get; set; }
        /// <summary>
        /// Database Field : sub_categories_status_id
        /// </summary>
        [FieldInfo("sub_categories_status_id")]
        public int SubCategoriesStatusId { get; set; } 
         /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }        
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
    }
}