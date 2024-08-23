using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities
{
    public partial class ContactListAPI_Result
    {
        [JsonProperty("vid")]
        public long Vid { get; set; }

        [JsonProperty("portal-id")]
        public long PortalId { get; set; }

        [JsonProperty("is-contact")]
        public bool IsContact { get; set; }

        [JsonProperty("profile-url")]
        public Uri ProfileUrl { get; set; }

        [JsonProperty("properties")]
        public Dictionary<string, Dictionary<string, string>> Properties { get; set; }
    }

    public partial class ContactListAPI_Result
    {
        public static ContactListAPI_Result FromJson(string json)
        {
            return JsonConvert.DeserializeObject<ContactListAPI_Result>(json);
        }
        //public static ContactListAPI_Result FromJson(string json) 
        //  => JsonConvert.DeserializeObject<ContactListAPI_Result>(json, Converter.Settings);
        public ItemDTO ToDTO()
        {
            return new ItemDTO
            {
                Vid = Vid,
                PortalId = PortalId,
                IsContact = IsContact,
                ProfileUrl = ProfileUrl,
                Properties =
                    Properties.ToDictionary(
                        p => p.Key,
                        p => p.Value["value"]
                    )
            };
        }

        public partial class ItemDTO
        {
            public long Vid { get; set; }
            public long PortalId { get; set; }
            public bool IsContact { get; set; }
            public Uri ProfileUrl { get; set; }
            public Dictionary<string, string> Properties { get; set; }
        }
    }

}