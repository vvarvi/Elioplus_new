using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities
{
    public partial class ItemDTO
    {
        public long Vid { get; set; }
        public long PortalId { get; set; }
        public bool IsContact { get; set; }
        public Uri ProfileUrl { get; set; }
        public Dictionary<string, string> Properties { get; set; }
        
        //public partial ItemDTO ToDTO()
        //{
        //    ItemDTO t = new ItemDTO();
        //    t.Vid = Vid;
        //    t.PortalId = PortalId;
        //    t.IsContact = IsContact;
        //    t.ProfileUrl = ProfileUrl;
        //    t.Properties = Properties.ToDictionary(
        //        p => p.Key,
        //        p => p.Value
        //    );

        //    return t;
        //}
    }

    
}