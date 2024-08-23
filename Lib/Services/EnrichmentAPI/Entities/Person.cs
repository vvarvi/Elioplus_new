using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities
{
    public class name
    {
        public string firstName { get; set; }

        public string lastName { get; set; }

        public string fullName { get; set; }
    }
    
    public class person
    {
        public string id { get; set; }

        public List<name> name { get; set; }

        public string email { get; set; }

        public string gender { get; set; }

        public string location { get; set; }

        public string timeZone { get; set; }

        public int utcOffset { get; set; }

        public string bio { get; set; }

        public string site { get; set; }

        public string avatar { get; set; }

        public bool fuzzy { get; set; }

        public bool emailProvider { get; set; }

        public DateTime indexedAt { get; set; }


        public string State { get; set; }

        public string Country { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        

        

        public string CompanyName { get; set; }

        public string Title { get; set; }

        public string Role { get; set; }

        public string Seniority { get; set; }

        public string CompanyDomain { get; set; }

        public string FacebookID { get; set; }

        public string GitHubHandle { get; set; }

        public int GitHubID { get; set; }

        public string GitHubAvatar { get; set; }

        public string GitHubCompany { get; set; }

        public string GitHubBlogUrl { get; set; }

        public string GitHubFollowersCount { get; set; }

        public string TwitterScreenName { get; set; }

        public int TwitterID { get; set; }

        public int TwitterFollowersCount { get; set; }

        public int TwitterFriendsCount { get; set; }

        public string TwitterLocation { get; set; }

        public string TwitterSite { get; set; }

        public int TweetterCount { get; set; }

        public int TweetterFavoritesCount { get; set; }

        public string HTTPTwitterAvatar { get; set; }

        public string LinkedInUrl { get; set; }

        public string GooglePlusHandle { get; set; }

        public string aboutMeHandle { get; set; }

        public string aboutMeBio { get; set; }

        public string aboutMeAvatarURL { get; set; }

        public string GravatarHandle { get; set; }

        public string[] GravatarUrls { get; set; }

        public string GravatarMainAvatarUrl { get; set; }

        public string[] GravatarAvatars { get; set; }

        

        public bool IsFreeEmailProvider { get; set; }

        public string DateRequested { get; set; }

    }

    public class domainAliases
    {
        public string domains { get; set; }
    }

    public class phoneNumbers
    {
        public string phones { get; set; }
    }

    public class company
    {
        public string id { get; set; }

        public string name { get; set; }

        public string legalName { get; set; }

        public string domain { get; set; }

        public domainAliases[] domains { get; set; }

        public List<phoneNumbers> site { get; set; }

        public string location { get; set; }

        public string timeZone { get; set; }

        public int utcOffset { get; set; }

        public string bio { get; set; }

        public string avatar { get; set; }

        public bool fuzzy { get; set; }

        public bool emailProvider { get; set; }

        public DateTime indexedAt { get; set; }
    }
}