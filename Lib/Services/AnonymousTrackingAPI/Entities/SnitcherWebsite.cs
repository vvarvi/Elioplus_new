using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.AnonymousTrackingAPI.Entities
{
    public class SnitcherWebsite
    {
        public int CurrentPage { get; set; }

        public string WebsiteId { get; set; }

        public string Url { get; set; }

        public string TrackingScriptId { get; set; }

        public string TrackingScriptSnippet { get; set; }

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

        public List<SnitcherLead> Leads { get; set; }
    }
}