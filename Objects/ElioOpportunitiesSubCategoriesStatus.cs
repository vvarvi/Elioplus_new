using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_opportunities_sub_categories_status
    /// </summary>
    [ClassInfo("Elio_opportunities_sub_categories_status")]
    public partial class ElioOpportunitiesSubCategoriesStatus : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : opportunity_status_id
        /// </summary>
        [FieldInfo("opportunity_status_id")]
        public int OpportunityStatusId { get; set; } 
         /// <summary>
        /// Database Field : sub_step_description
        /// </summary>
        [FieldInfo("sub_step_description")]
        public string SubStepDescription { get; set; }        
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}