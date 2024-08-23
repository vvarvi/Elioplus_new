using System;
using System.Linq;

namespace WdS.ElioPlus.Objects
{
    [ClassInfo("Elio_collaboration_vendor_tier_status")]
    public class ElioCollaborationVendorTierStatus : DataObject
    {
        [FieldInfo("id", IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        [FieldInfo("vendor_id")]
        public int VendorId { get; set; }

        [FieldInfo("status_description")]
        public string StatusDescription { get; set; }

        [FieldInfo("sysdate")]
        public DateTime Sysdate { get; set; }

        [FieldInfo("is_public")]
        public int IsPublic { get; set; }
    }
}