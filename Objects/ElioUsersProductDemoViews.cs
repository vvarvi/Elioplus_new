using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_collaboration_users_invitations
    /// </summary>
    [ClassInfo("Elio_users_product_demo_views")]
    public partial class ElioUsersProductDemoViews : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : add_by_user_id
        /// </summary>
        [FieldInfo("user_id")]
        public int UserId { get; set; }
        /// <summary>
        /// Database Field : visitor_company_id
        /// </summary>
        [FieldInfo("visitor_company_id")]
        public int VisitorCompanyId { get; set; }       
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
        /// <summary>
        /// Database Field : count
        /// </summary>
        [FieldInfo("count")]
        public int Count { get; set; }
    }
}