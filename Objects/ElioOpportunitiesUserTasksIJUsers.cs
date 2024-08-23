using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    public class ElioOpportunitiesUserTasksIJUsers : DataObject
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
        /// Database Field : opportunity_user_id
        /// </summary>
        [FieldInfo("opportunity_id")]
        public int OpportunityId { get; set; }
        /// <summary>
        /// Database Field : task_subject
        /// </summary>
        [FieldInfo("task_subject")]
        public string TaskSubject { get; set; }
        /// <summary>
        /// Database Field : task_content
        /// </summary>
        [FieldInfo("task_content")]
        public string TaskContent { get; set; }
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
        /// Database Field : remind_date
        /// </summary>
        [FieldInfo("remind_date")]
        public DateTime RemindDate { get; set; }
        /// <summary>
        /// Database Field : status
        /// </summary>
        [FieldInfo("status")]
        public string Status { get; set; }
        /// <summary>
        /// Database Field : organization_name
        /// </summary>
        [FieldInfo("organization_name")]
        public string OrganizationName { get; set; }
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
    }
}