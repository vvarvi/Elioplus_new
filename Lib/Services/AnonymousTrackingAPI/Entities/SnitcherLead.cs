using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.AnonymousTrackingAPI.Entities
{
    public class SnitcherLead
    {
        public int CurrentPage { get; set; }

        public string SessionReferrer { get; set; }

        public List<PageView> PageViews { get; set; }

        public string SessionOperating_system { get; set; }

        public string SessionBrowser { get; set; }

        public string SessionDeviceType { get; set; }

        public string SessionCampaign { get; set; }

        public DateTime SessionStart { get; set; }

        public int SessionDuration { get; set; }

        public int SessionTotalPageviews { get; set; }

        public string LeadId { get; set; }

        public DateTime LeadLastSeen { get; set; }

        public string LeadCompanyName { get; set; }

        public string LeadCompanyLogo { get; set; }

        public string LeadCompanyWebsite { get; set; }

        public string LeadCompanyAddress { get; set; }

        public string LeadCompanyFounded { get; set; }

        public string LeadCompanySize { get; set; }

        public string LeadCompanyIndustry { get; set; }

        public string LeadCompanyPhone { get; set; }

        public string LeadCompanyEmail { get; set; }

        public string LeadCompanyContacts { get; set; }

        public string LeadLinkedin { get; set; }

        public string LeadFacebook { get; set; }

        public string LeadYoutube { get; set; }

        public string LeadInstagram { get; set; }

        public string LeadTwitter { get; set; }

        public string LeadPinterest { get; set; }

        public string LeadAngellist { get; set; }

        public string FirstPageUrl { get; set; }

        public int From { get; set; }

        public int LastPage { get; set; }

        public string LastPageUrl { get; set; }

        public string NextPageUrl { get; set; }

        public string Path { get; set; }

        public int PerPage { get; set; }

        public string PrevPageUrl { get; set; }

        public int To { get; set; }

        public int Total { get; set; }
    }

    public class PageView
    {
        public string Url { get; set; }
        public int TimeSpent { get; set; }
        public DateTime ActionTime { get; set; }
    }
}