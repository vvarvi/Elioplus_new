using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Objects;

namespace HangfireDashboard.Objects
{
    public partial class ElioCollaborationVendorResellerInvitationsIJUsers : DataObject
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
        /// Database Field : vendor_reseller_id
        /// </summary>
        [FieldInfo("vendor_reseller_id")]
        public int VendorResellerId { get; set; }
        /// <summary>
        /// Database Field : partner_user_id
        /// </summary>
        [FieldInfo("partner_user_id")]
        public int PartnerUserId { get; set; }
        /// <summary>
        /// Database Field : user_invitation_id
        /// </summary>
        [FieldInfo("user_invitation_id")]
        public int UserInvitationId { get; set; }
        /// <summary>
        /// Database Field : invitation_step_description
        /// </summary>
        [FieldInfo("invitation_step_description")]
        public string InvitationStepDescription { get; set; }
        /// <summary>
        /// Database Field : recipient_email
        /// </summary>
        [FieldInfo("recipient_email")]
        public string RecipientEmail { get; set; }
        /// <summary>
        /// Database Field : send_date
        /// </summary>
        [FieldInfo("send_date")]
        public DateTime SendDate { get; set; }
        /// <summary>
        /// Database Field : last_updated
        /// </summary>
        [FieldInfo("last_updated")]
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Database Field : is_new
        /// </summary>
        [FieldInfo("is_new")]
        public int IsNew { get; set; }
        /// <summary>
        /// Database Field : email
        /// </summary>
        [FieldInfo("email")]
        public string Email { get; set; }
        /// <summary>
        /// Database Field : company_name
        /// </summary>
        [FieldInfo("company_name")]
        public string CompanyName { get; set; }
    }
}