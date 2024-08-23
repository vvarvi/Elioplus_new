using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities
{
    public class person
    {
        public string id { get; set; }

        public name[] name { get; set; }
    }

    public class name
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string fullName { get; set; }
    }

    public class Content
    {
        public person[] person { get; set; }

    }
}