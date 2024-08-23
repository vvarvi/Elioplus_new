using System;

namespace WdS.ElioPlus.Objects
{
    /// <summary>
    /// Table elio_users
    /// </summary>
    [Serializable]
    public partial class ElioUsersInfo
    {        
        public int Id { get; set; }        
        public string Address { get; set; }        
        public string Country { get; set; }
        public string City { get; set; }
        public string Profile { get; set; }
        public string PortalLoginUrl { get; set; }
        public string WebSite { get; set; }
        public string Industries { get; set; }        
        public string Description { get; set; }        
        public string CompanyName { get; set; }        
        public string CompanyLogo { get; set; }        
    }
}
