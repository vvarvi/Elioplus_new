using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table Elio_snitcher_website_leads
    /// </summary>
    [ClassInfo("Elio_snitcher_website_leads")]
    public partial class ElioSnitcherWebsiteLeads : DataObject
    {
        /// <summary>
        /// Database Field : id
        /// </summary>
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// Database Field : website_id
        /// </summary>
        [FieldInfo("website_id")]
        public string WebsiteId { get; set; }
        /// <summary>
        /// Database Field : elio_snitcher_website_id
        /// </summary>
        [FieldInfo("elio_snitcher_website_id")]
        public int ElioSnitcherWebsiteId { get; set; }
        /// <summary>
        /// Database Field : session_referrer
        /// </summary>
        [FieldInfo("session_referrer")]
        public string SessionReferrer { get; set; }
        /// <summary>
        /// Database Field : session_operating_system
        /// </summary>
        [FieldInfo("session_operating_system")]
        public string SessionOperatingSystem { get; set; }
        /// <summary>
        /// Database Field : session_browser
        /// </summary>
        [FieldInfo("session_browser")]
        public string SessionBrowser { get; set; }
        /// <summary>
        /// Database Field : session_device_type
        /// </summary>
        [FieldInfo("session_device_type")]
        public string SessionDeviceType { get; set; }
        /// <summary>
        /// Database Field : session_campaign
        /// </summary>
        [FieldInfo("session_campaign")]
        public string SessionCampaign { get; set; }
        /// <summary>
        /// Database Field : session_start
        /// </summary>
        [FieldInfo("session_start")]
        public DateTime? SessionStart { get; set; }
        /// <summary>
        /// Database Field : session_duration
        /// </summary>
        [FieldInfo("session_duration")]
        public int SessionDuration { get; set; }
        /// <summary>
        /// Database Field : session_total_pageviews
        /// </summary>
        [FieldInfo("session_total_pageviews")]
        public int SessionTotalPageviews { get; set; }
        /// <summary>
        /// Database Field : lead_id
        /// </summary>
        [FieldInfo("lead_id")]
        public string LeadId { get; set; }
        /// <summary>
        /// Database Field : lead_last_seen
        /// </summary>
        [FieldInfo("lead_last_seen")]
        public DateTime? LeadLastSeen { get; set; }
        /// <summary>
        /// Database Field : lead_first_name
        /// </summary>
        [FieldInfo("lead_first_name")]
        public string LeadFirstName { get; set; }
        /// <summary>
        /// Database Field : lead_last_name
        /// </summary>
        [FieldInfo("lead_last_name")]
        public string LeadLastName { get; set; }
        /// <summary>
        /// Database Field : lead_company_name
        /// </summary>
        [FieldInfo("lead_company_name")]
        public string LeadCompanyName { get; set; }
        /// <summary>
        /// Database Field : lead_company_logo
        /// </summary>
        [FieldInfo("lead_company_logo")]
        public string LeadCompanyLogo { get; set; }
        /// <summary>
        /// Database Field : lead_company_website
        /// </summary>
        [FieldInfo("lead_company_website")]
        public string LeadCompanyWebsite { get; set; }
        /// <summary>
        /// Database Field : lead_country
        /// </summary>
        [FieldInfo("lead_country")]
        public string LeadCountry { get; set; }
        /// <summary>
        /// Database Field : lead_city
        /// </summary>
        [FieldInfo("lead_city")]
        public string LeadCity { get; set; }
        /// <summary>
        /// Database Field : lead_company_address
        /// </summary>
        [FieldInfo("lead_company_address")]
        public string LeadCompanyAddress { get; set; }
        /// <summary>
        /// Database Field : lead_company_founded
        /// </summary>
        [FieldInfo("lead_company_founded")]
        public string LeadCompanyFounded { get; set; }
        /// <summary>
        /// Database Field : lead_company_size
        /// </summary>
        [FieldInfo("lead_company_size")]
        public string LeadCompanySize { get; set; }
        /// <summary>
        /// Database Field : lead_company_industry
        /// </summary>
        [FieldInfo("lead_company_industry")]
        public string LeadCompanyIndustry { get; set; }
        /// <summary>
        /// Database Field : lead_company_phone
        /// </summary>
        [FieldInfo("lead_company_phone")]
        public string LeadCompanyPhone { get; set; }
        /// <summary>
        /// Database Field : lead_company_email
        /// </summary>
        [FieldInfo("lead_company_email")]
        public string LeadCompanyEmail { get; set; }
        /// <summary>
        /// Database Field : lead_company_contacts
        /// </summary>
        [FieldInfo("lead_company_contacts")]
        public string LeadCompanyContacts { get; set; }
        /// <summary>
        /// Database Field : lead_linkedin_handle
        /// </summary>
        [FieldInfo("lead_linkedin_handle")]
        public string LeadLinkedinHandle { get; set; }
        /// <summary>
        /// Database Field : lead_facebook_handle
        /// </summary>
        [FieldInfo("lead_facebook_handle")]
        public string LeadFacebookHandle { get; set; }
        /// <summary>
        /// Database Field : lead_youtube_handle
        /// </summary>
        [FieldInfo("lead_youtube_handle")]
        public string LeadYoutubeHandle { get; set; }
        /// <summary>
        /// Database Field : lead_instagram_handle
        /// </summary>
        [FieldInfo("lead_instagram_handle")]
        public string LeadInstagramHandle { get; set; }
        /// <summary>
        /// Database Field : lead_twitter_handle
        /// </summary>
        [FieldInfo("lead_twitter_handle")]
        public string LeadTwitterHandle { get; set; }
        /// <summary>
        /// Database Field : lead_pinterest_handle
        /// </summary>
        [FieldInfo("lead_pinterest_handle")]
        public string LeadPinterestHandle { get; set; }
        /// <summary>
        /// Database Field : lead_angellist_handle
        /// </summary>
        [FieldInfo("lead_angellist_handle")]
        public string LeadAngellistHandle { get; set; }
        /// <summary>
        /// Database Field : message
        /// </summary>
        [FieldInfo("message")]
        public string Message { get; set; }
        /// <summary>
        /// Database Field : is_api_lead
        /// </summary>
        [FieldInfo("is_api_lead")]
        public int IsApiLead { get; set; }
        /// <summary>
        /// Database Field : is_public
        /// </summary>
        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
        /// <summary>
        /// Database Field : is_approved
        /// </summary>
        [FieldInfo("is_approved")]
        public int IsApproved { get; set; }
        /// <summary>
        /// Database Field : sysdate
        /// </summary>
        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }
        /// <summary>
        /// Database Field : last_update
        /// </summary>
        [FieldInfo("last_update")]
        public DateTime LastUpdate { get; set; }
        /// <summary>
        /// Database Field : is_confirmed
        /// </summary>
        [FieldInfo("is_confirmed")]
        public int IsConfirmed { get; set; }
        /// <summary>
        /// Database Field : rfp_message_company_id_is_for
        /// </summary>
        [FieldInfo("rfp_message_company_id_is_for")]
        public string RfpMessageCompanyIdIsFor { get; set; }
    }
}