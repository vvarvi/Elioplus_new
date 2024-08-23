using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using iTextSharp.text.pdf.parser;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.EnrichmentAPI
{
    public class ClearBit
    {
        ////public T DeserializeResponse<T>(this JObject obj) where T : new()
        ////{
        ////    T result = new T();
        ////    var props = typeof(T).GetProperties().Where(p => p.CanWrite).ToList();
        ////    var objectDictionary = obj as IDictionary<string, JToken>;

        ////    foreach (var prop in props)
        ////    {
        ////        var name = prop.Name.GetNameVariants(CultureInfo.CurrentCulture).FirstOrDefault(n => objectDictionary.ContainsKey(n));
        ////        var value = name != null ? obj[name] : null;

        ////        if (value == null) continue;

        ////        var type = prop.PropertyType;

        ////        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
        ////        {
        ////            type = type.GetGenericArguments()[0];
        ////        }

        ////        // This is a problem. I need a way to convert JToken value into an object of Type type
        ////        prop.SetValue(result, ConvertValue(type, value), null);
        ////    }

        ////    return result;
        ////}

        public class SearchResult
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public string Url { get; set; }
        }

        public static void Fix()
        {
            #region Json Response

            string googleSearchText = @"{
                                  'responseData': {
                                    'results': [
                                      {
                                        'GsearchResultClass': 'GwebSearch',
                                        'unescapedUrl': 'http://en.wikipedia.org/wiki/Paris_Hilton',
                                        'url': 'http://en.wikipedia.org/wiki/Paris_Hilton',
                                        'visibleUrl': 'en.wikipedia.org',
                                        'cacheUrl': 'http://www.google.com/search?q=cache:TwrPfhd22hYJ:en.wikipedia.org',
                                        'title': '<b>Paris Hilton</b> - Wikipedia, the free encyclopedia',
                                        'titleNoFormatting': 'Paris Hilton - Wikipedia, the free encyclopedia',
                                        'content': '[1] In 2006, she released her debut album...'
                                      },
                                      {
                                        'GsearchResultClass': 'GwebSearch',
                                        'unescapedUrl': 'http://www.imdb.com/name/nm0385296/',
                                        'url': 'http://www.imdb.com/name/nm0385296/',
                                        'visibleUrl': 'www.imdb.com',
                                        'cacheUrl': 'http://www.google.com/search?q=cache:1i34KkqnsooJ:www.imdb.com',
                                        'title': '<b>Paris Hilton</b>',
                                        'titleNoFormatting': 'Paris Hilton',
                                        'content': 'Self: Zoolander. Socialite <b>Paris Hilton</b>...'
                                      }
                                    ],
                                    'cursor': { 
                                      'pages': [
                                        {
                                          'start': '0',
                                          'label': 1
                                        },
                                        {
                                          'start': '4',
                                          'label': 2
                                        },
                                        {
                                          'start': '8',
                                          'label': 3
                                        },
                                        {
                                          'start': '12',
                                          'label': 4
                                        }
                                      ],
                                      'estimatedResultCount': '59600000',
                                      'currentPageIndex': 0,
                                      'moreResultsUrl': 'http://www.google.com/search?oe=utf8&ie=utf8...'
                                    }
                                  },
                                  'responseDetails': null,
                                  'responseStatus': 200
                                }";

            #endregion
            
            JObject googleSearch = JObject.Parse(googleSearchText);

            // get JSON result objects into a list
            IList<JToken> results = googleSearch["responseData"]["results"].Children().ToList();

            // serialize JSON results into .NET objects
            IList<SearchResult> searchResults = new List<SearchResult>();
            foreach (JToken result in results)
            {
                // JToken.ToObject is a helper method that uses JsonSerializer internally
                SearchResult searchResult = result.ToObject<SearchResult>();
                searchResults.Add(searchResult);
            }
        }

        public Dictionary<String, Object> Dyn2Dict(JToken dynObj)
        {
            var dictionary = new Dictionary<string, object>();
            foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(dynObj))
            {
                object obj = propertyDescriptor.GetValue(dynObj);
                dictionary.Add(propertyDescriptor.Name, obj);
            }
            return dictionary;
        }

        public static bool GetRFPCompanyByEmail(ElioSnitcherWebsiteLeads lead, DBSession session)
        {
            bool success = false;
            
            try
            {
                var client = new RestClient("https://person.clearbit.com");      //var client = new RestClient("https://person-stream.clearbit.com");
                //emailAddress = "oleksiy@rioks.com";
                var request = new RestRequest("v2/combined/find", Method.GET);
                request.AddParameter("email", lead.LeadCompanyEmail);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    string error = "";
                    int caseIndex = 0;
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for snitcher lead with eimail:" + lead.LeadCompanyEmail;
                        caseIndex = 0;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for snitcher lead with eimail:" + lead.LeadCompanyEmail;
                        caseIndex = 1;
                    }
                    else
                    {
                        error = "Something went wrong for snitcher lead with eimail:" + lead.LeadCompanyEmail;
                        caseIndex = 2;
                    }

                    if (caseIndex == 1)
                        Logger.DetailedClearBitError(lead.LeadCompanyEmail);

                    return false;
                }

                var personCompanyDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                JToken person = personCompanyDictionary["person"];
                JToken company = personCompanyDictionary["company"];

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (combinedPersonCompanyResponse != null)
                {
                    if (session.Connection.State == System.Data.ConnectionState.Closed)
                        session.OpenConnection();

                    if (person.HasValues)
                    {
                        #region person response

                        string personId = combinedPersonCompanyResponse["person"]["id"].ToString();
                        string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                        string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                        string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                        string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                        //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                        string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                        string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                        string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                        string city = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                        string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                        string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                        string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                        string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                        string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                        string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                        string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                        string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                        string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                        string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                        string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                        string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                        string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                        string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                        string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                        string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                        string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                        string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                        string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                        string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                        string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                        string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                        string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                        string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                        string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                        string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                        string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                        string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                        string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                        string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                        string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                        string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                        string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                        string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                        //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                        //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                        //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                        string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                        string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();

                        string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                        string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                        string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                        #endregion

                        if (lead.IsApiLead == (int)ApiLeadCategory.isSnitcherLead)
                        {
                            #region Elio Snitcher

                            lead.LeadFirstName = givenName;
                            lead.LeadLastName = familyName;
                            lead.LeadCity = city;
                            lead.LeadCountry = country;

                            #endregion
                        }

                        #region Elio Snitcher Social

                        lead.LeadTwitterHandle = twitterHandle;
                        lead.LeadLinkedinHandle = linkedinHandle;
                        lead.LeadCompanyAddress = location;
                        lead.LeadCompanyLogo = avatar;
                        lead.LeadCompanyWebsite = site;

                        #endregion

                        lead.LeadCompanyLogo = (avatar != "") ? avatar : lead.LeadCompanyLogo;
                    }

                    if (company.HasValues)
                    {
                        string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                        string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                        string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();
                        
                        if (lead != null)
                        {
                            #region company response-no

                            string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                            string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                            string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                            string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                            string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                            string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                            string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                            string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                            string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                            string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                            string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();
                            string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                            string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                            string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                            string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                            string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                            string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                            string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                            string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                            string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                            string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                            string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                            string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                            string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                            string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                            string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                            string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                            string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                            string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                            string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                            string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                            string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                            string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                            string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                            string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                            string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                            string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                            string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                            string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                            string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                            string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                            string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                            string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                            string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                            string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                            string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                            string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                            string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                            string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                            string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                            string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                            string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                            string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                            string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                            string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                            string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                            string[] companyTechCategories = combinedPersonCompanyResponse["company"]["techCategories"].ToString().Split(',').ToArray();

                            string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                            #endregion

                            if (lead.IsApiLead == (int)ApiLeadCategory.isSnitcherLead)
                            {
                                #region Elio Snitcher

                                lead.SessionReferrer = companyDomain;
                                lead.LeadCountry = (companyCountry != "") ? companyCountry : lead.LeadCountry;
                                lead.LeadCity = (companyCity != "") ? companyCity : lead.LeadCity;
                                lead.LeadCompanyAddress = (companyLocation != "") ? companyLocation : lead.LeadCompanyAddress;
                                lead.LeadCompanyIndustry = companyIndustry != "" ? companyIndustry : companySubIndustry;
                                lead.LeadCompanyFounded = (!string.IsNullOrEmpty(companyFoundedYear)) ? companyFoundedYear : "0";
                                lead.LeadCompanySize = (!string.IsNullOrEmpty(companyEmployees)) ? companyEmployees : "0";
                                lead.LeadFacebookHandle = companyFacebookHandle;
                                lead.LeadTwitterHandle = (lead.LeadTwitterHandle == "") ? companyTwitterHandle : "";                                
                                lead.LeadCompanyWebsite = companyDomain != "" ? companyDomain : lead.LeadCompanyWebsite;

                                #endregion

                                #region Elio Url / Tags

                                foreach (string companyTag in companyTags)
                                {
                                    if (companyTag != "")
                                    {
                                        string tagName = Regex.Replace(companyTag, @"\\r\\n", "");
                                        tagName = tagName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Replace("_", " ").Trim();

                                        if (tagName != "")
                                        {
                                            DataLoader<ElioSnitcherLeadsPageviews> pageLoader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                            ElioSnitcherLeadsPageviews page = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, tagName, tagName, session);

                                            if (page == null)
                                            {
                                                page = new ElioSnitcherLeadsPageviews();

                                                page.ElioWebsiteLeadsId = lead.Id;
                                                page.LeadId = lead.LeadId;
                                                page.Url = tagName.Trim();
                                                page.Product = tagName.Trim();
                                                page.TimeSpent = 1;
                                                page.ActionTime = lead.Sysdate;
                                                page.Sysdate = DateTime.Now;
                                                page.LastUpdate = DateTime.Now;

                                                pageLoader.Insert(page);
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region Elio Url / Tech

                                foreach (string tech in companyTech)
                                {
                                    if (tech != "")
                                    {
                                        string techName = Regex.Replace(tech, @"\\r\\n", "");
                                        techName = techName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Replace("_", " ").Trim();

                                        if (techName != "")
                                        {
                                            DataLoader<ElioSnitcherLeadsPageviews> pageLoader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                            ElioSnitcherLeadsPageviews page = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, techName, techName, session);

                                            if (page == null)
                                            {
                                                page = new ElioSnitcherLeadsPageviews();

                                                page.ElioWebsiteLeadsId = lead.Id;
                                                page.LeadId = lead.LeadId;
                                                page.Url = techName.Trim();
                                                page.Product = techName.Trim();
                                                page.TimeSpent = 1;
                                                page.ActionTime = lead.Sysdate;
                                                page.Sysdate = DateTime.Now;
                                                page.LastUpdate = DateTime.Now;

                                                pageLoader.Insert(page);
                                            }
                                        }
                                    }
                                }

                                #endregion

                                #region Elio Url / TechCategories

                                foreach (string techCategory in companyTechCategories)
                                {
                                    if (techCategory != "")
                                    {
                                        string techCategoryName = Regex.Replace(techCategory, @"\\r\\n", "");
                                        techCategoryName = techCategoryName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Replace("_", " ").Trim();

                                        if (techCategoryName != "")
                                        {
                                            DataLoader<ElioSnitcherLeadsPageviews> pageLoader = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                            ElioSnitcherLeadsPageviews page = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, techCategoryName, techCategoryName, session);

                                            if (page == null)
                                            {
                                                page = new ElioSnitcherLeadsPageviews();

                                                page.ElioWebsiteLeadsId = lead.Id;
                                                page.LeadId = lead.LeadId;
                                                page.Url = techCategoryName.Trim();
                                                page.Product = techCategoryName.Trim();
                                                page.TimeSpent = 1;
                                                page.ActionTime = lead.Sysdate;
                                                page.Sysdate = DateTime.Now;
                                                page.LastUpdate = DateTime.Now;

                                                pageLoader.Insert(page);
                                            }
                                        }
                                    }
                                }

                                #endregion
                            }

                            lead.LeadCompanyLogo = (companyLogo != "") ? companyLogo : lead.LeadCompanyLogo;

                            if (lead.IsApiLead == (int)ApiLeadCategory.isSnitcherLead)
                            {
                                #region Phone Numbers

                                string phoneNumbers = "";

                                foreach (string phone in companySitePhoneNumbers)
                                {
                                    if (phone != "")
                                    {
                                        string number = Regex.Replace(phone, @"\\r\\n", "");
                                        number = number.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                                        if (number != "")
                                        {
                                            phoneNumbers += number + ",";
                                        }
                                    }
                                }

                                if (phoneNumbers.Length > 0)
                                    phoneNumbers = phoneNumbers.Substring(0, phoneNumbers.Length - 1);

                                lead.LeadCompanyPhone = phoneNumbers != "" ? phoneNumbers : companyPhone;

                                #endregion
                            }
                        }
                    }

                    success = true;

                    if (success)
                    {
                        #region Elio Users Update credentials

                        lead.IsPublic = (int)AccountPublicStatus.IsPublic;
                        lead.IsConfirmed = 1;
                        lead.LastUpdate = DateTime.Now;

                        DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);
                        loader.Update(lead);

                        #endregion
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error at, " + DateTime.Now.ToString() + ", for email address: " + lead.LeadCompanyEmail, ex.Message.ToString(), ex.StackTrace.ToString());
                return false;
            }

            return true;
        }

        public static ElioUsers GetCombinedPersonCompanyByEmail_v2(ElioUsers user)
        {
            bool success = false;
            ElioUsersPerson elioPerson = null;
            ElioUsersPersonCompanies elioCompany = null;
            
            try
            {
                var client = new RestClient("https://person.clearbit.com");      //var client = new RestClient("https://person-stream.clearbit.com");
                //emailAddress = "oleksiy@rioks.com";
                var request = new RestRequest("v2/combined/find", Method.GET);
                request.AddParameter("email", user.Email);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    string error = "";
                    int caseIndex = 0;
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for clearbit person with eimail:" + user.Email;
                        caseIndex = 0;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for clearbit person with eimail:" + user.Email;
                        caseIndex = 1;
                    }
                    else
                    {
                        error = "Something went wrong for clearbit person with eimail:" + user.Email;
                        caseIndex = 2;
                    }

                    //Logger.DetailedError("Class:ClearBit.cs --> Method:FindCombinedPersonCompanyByEmail_v2 -->", error.ToString());

                    if (caseIndex == 1)
                        Logger.DetailedClearBitError(user.Email);

                    return user;
                }

                var personCompanyDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                JToken person = personCompanyDictionary["person"];
                JToken company = personCompanyDictionary["company"];

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (combinedPersonCompanyResponse != null)
                {
                    //if (session.Connection.State == System.Data.ConnectionState.Closed)
                    //    session.OpenConnection();

                    //DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);

                    if (person.HasValues)
                    {
                        #region person response

                        string personId = combinedPersonCompanyResponse["person"]["id"].ToString();
                        string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                        string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                        string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                        string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                        //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                        string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                        string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                        string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                        string city = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                        string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                        string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                        string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                        string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                        string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                        string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                        string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                        string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                        string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                        string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                        string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                        string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                        string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                        string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                        string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                        string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                        string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                        string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                        string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                        string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                        string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                        string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                        string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                        string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                        string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                        string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                        string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                        string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                        string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                        string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                        string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                        string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                        string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                        string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                        //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                        //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                        //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                        string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                        string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();

                        string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                        string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                        string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                        #endregion

                        #region Elio Users Person-no

                        elioPerson = new ElioUsersPerson();

                        elioPerson.ClearbitPersonId = personId;
                        elioPerson.UserId = user.Id;
                        elioPerson.GivenName = givenName;
                        elioPerson.FamilyName = familyName;
                        elioPerson.Email = personEmail;
                        elioPerson.Phone = "";
                        elioPerson.Location = location;
                        if (!string.IsNullOrEmpty(city))
                            elioPerson.Location += ", " + city;
                        if (!string.IsNullOrEmpty(state))
                            elioPerson.Location += ", " + state;
                        elioPerson.TimeZone = timeZone;
                        elioPerson.Bio = bio;
                        elioPerson.Avatar = avatar;
                        elioPerson.Title = title;
                        elioPerson.Role = role;
                        elioPerson.Seniority = seniority;
                        elioPerson.TwitterHandle = twitterHandle;
                        elioPerson.LinkedinHandle = linkedinHandle;
                        elioPerson.AboutMeHandle = "";  //linkedinAboutmeHandle;
                        elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                        elioPerson.LastUpdate = DateTime.Now;
                        elioPerson.IsPublic = 1;
                        elioPerson.IsActive = 1;
                        elioPerson.IsClaimed = 0;

                        //ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserId(user.Id, session);
                        //if (elioPersonInDB == null)
                        //{
                        //    personloader.Insert(elioPerson);
                        //}
                        //else
                        //{
                        //    elioPerson.Id = elioPersonInDB.Id;
                        //    elioPerson.LastUpdate = DateTime.Now;
                        //    personloader.Update(elioPerson);
                        //}

                        #endregion

                        #region Elio Users Update credentials-yes

                        //user.Address = elioPerson.Location;
                        user.PersonalImage = elioPerson.Avatar;
                        //user.TwitterUrl = elioPerson.TwitterHandle;
                        //user.LinkedInUrl = elioPerson.LinkedinHandle;
                        user.Position = elioPerson.Title + "," + elioPerson.Seniority;
                        user.LastName = elioPerson.FamilyName;
                        user.FirstName = elioPerson.GivenName;
                        user.Country = (string.IsNullOrEmpty(user.Country)) ? country : user.Country;

                        #endregion
                    }
                    else
                    {
                        #region Elio User to Clearbit Person

                        string personId = Guid.NewGuid().ToString();

                        elioPerson = new ElioUsersPerson();

                        elioPerson.ClearbitPersonId = personId;
                        elioPerson.UserId = user.Id;
                        elioPerson.GivenName = user.FirstName;
                        elioPerson.FamilyName = user.LastName;
                        elioPerson.Email = (!string.IsNullOrEmpty(user.Email)) ? user.Email : "";
                        elioPerson.Phone = user.Phone;
                        elioPerson.Location = user.Address;
                        elioPerson.TimeZone = "";
                        elioPerson.Bio = "";
                        elioPerson.Avatar = "";
                        elioPerson.Title = user.Position;
                        elioPerson.Role = "";
                        elioPerson.Seniority = "";
                        elioPerson.TwitterHandle = "";
                        elioPerson.LinkedinHandle = user.LinkedInUrl;
                        elioPerson.AboutMeHandle = "";
                        elioPerson.DateInserted = user.SysDate;
                        elioPerson.LastUpdate = DateTime.Now;
                        elioPerson.IsPublic = 1;
                        elioPerson.IsActive = 1;
                        elioPerson.IsClaimed = 0;

                        //ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserId(user.Id, session);
                        //if (elioPersonInDB == null)
                        //{
                        //    personloader.Insert(elioPerson);
                        //}
                        //else
                        //{
                        //    elioPerson.Id = elioPersonInDB.Id;
                        //    elioPerson.LastUpdate = DateTime.Now;
                        //    personloader.Update(elioPerson);
                        //}

                        #endregion
                    }

                    if (company.HasValues)
                    {
                        string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                        string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                        string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();

                        if (elioPerson != null)
                        {
                            #region company response-no

                            string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                            string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                            string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                            string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                            string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                            string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                            string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                            string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                            string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                            string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                            string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();
                            string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                            string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                            string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                            string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                            string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                            string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                            string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                            string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                            string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                            string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                            string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                            string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                            string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                            string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                            string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                            string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                            string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                            string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                            string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                            string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                            string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                            string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                            string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                            string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                            string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                            string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                            string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                            string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                            string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                            string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                            string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                            string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                            string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                            string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                            string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                            string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                            string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                            string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                            string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                            string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                            string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                            string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                            string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                            string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                            string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                            string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                            #endregion

                            #region Elio Users Company-no

                            elioCompany = new ElioUsersPersonCompanies();

                            elioCompany.ClearbitCompanyId = companyId;
                            elioCompany.ElioPersonId = elioPerson.Id;
                            elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                            elioCompany.UserId = user.Id;
                            elioCompany.Name = companyName;
                            elioCompany.Domain = companyDomain;
                            elioCompany.Sector = companySector;
                            elioCompany.IndustryGroup = companIndustryGroup;
                            elioCompany.Industry = companyIndustry;
                            elioCompany.SubIndustry = companySubIndustry;
                            elioCompany.Description = companyDescription;
                            elioCompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                            elioCompany.Location = companyLocation;
                            //elioCompany.FundAmount = companyfu
                            elioCompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                            elioCompany.EmployeesRange = companyEmployeesRange;
                            elioCompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                            elioCompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                            elioCompany.FacebookHandle = companyFacebookHandle;
                            elioCompany.FacebookLikes = (companyFacebookLikes != "") ? Convert.ToInt32(companyFacebookLikes) : 0;
                            elioCompany.LinkedinHandle = companyLinkedinHandle;
                            elioCompany.TwitterHandle = companyTwitterHandle;
                            elioCompany.TwitterId = companyTwitterId;
                            elioCompany.TwitterBio = companyTwitterBio;
                            elioCompany.TwitterAvatar = companyTwtitterAvatar;
                            elioCompany.TwitterFollowers = (companyTwitterFollowers != "") ? Convert.ToInt32(companyTwitterFollowers) : 0;
                            elioCompany.TwitterFollowing = (companyTwitterFollowing != "") ? Convert.ToInt32(companyTwitterFollowing) : 0;
                            elioCompany.TwitterSite = companyTwitterSite;
                            elioCompany.TwitterLocation = companyTwitterLocation;
                            elioCompany.CrunchbaseHandle = companyCrunchbaseHandle;
                            elioCompany.CompanyPhone = companyPhone;
                            elioCompany.Logo = companyLogo;
                            elioCompany.Type = companyType;
                            elioCompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                            elioCompany.LastUpdate = DateTime.Now;
                            elioCompany.IsPublic = 1;
                            elioCompany.IsActive = 1;

                            //DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);

                            //ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
                            //if (elioCompanyInDB == null)
                            //{
                            //    companyLoader.Insert(elioCompany);
                            //}
                            //else
                            //{
                            //    elioCompany.Id = elioCompanyInDB.Id;
                            //    elioCompany.LastUpdate = DateTime.Now;
                            //    companyLoader.Update(elioCompany);
                            //}

                            #endregion

                            #region Elio Users Update credentials-yes

                            user.WebSite = (string.IsNullOrEmpty(user.WebSite)) ? (!elioCompany.Domain.StartsWith("www") && !elioCompany.Domain.StartsWith("http")) ? "www." + elioCompany.Domain : elioCompany.Domain : user.WebSite;
                            user.Country = (string.IsNullOrEmpty(user.Country)) ? companyCountry : user.Country;
                            user.CompanyLogo = (string.IsNullOrEmpty(user.CompanyLogo)) ? (elioCompany.Logo != "") ? elioCompany.Logo : (user.PersonalImage != "") ? user.PersonalImage : "" : user.CompanyLogo;
                            user.TwitterUrl = elioCompany.TwitterHandle;
                            user.Description = elioCompany.Description;
                            user.CompanyName = (!string.IsNullOrEmpty(companyName)) ? companyName : "";

                            #endregion
                        }
                        else
                        {
                            return user;
                            //throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                        }

                        if (elioCompany != null)
                        {
                            #region Elio Company Phones

                            foreach (string phone in companySitePhoneNumbers)
                            {
                                if (phone != "")
                                {
                                    string number = Regex.Replace(phone, @"\\r\\n", "");
                                    number = number.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                                    if (number != "")
                                    {
                                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                                        companyPhoneNumber.ElioPersonCompanyId = elioCompany.Id;
                                        companyPhoneNumber.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        companyPhoneNumber.UserId = user.Id;
                                        companyPhoneNumber.PhoneNumber = number;
                                        companyPhoneNumber.Sysdate = DateTime.Now;
                                        companyPhoneNumber.LastUpdate = DateTime.Now;

                                        //DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);

                                        //ElioUsersPersonCompanyPhoneNumbers companyPhoneNumberInDB = ClearbitSql.GetPersonCompanyPhoneByPhone(user.Id, elioCompany.ClearbitCompanyId, number, session);

                                        //if (companyPhoneNumberInDB == null)
                                        //{
                                        //    phoneLoader.Insert(companyPhoneNumber);
                                        //}
                                        //else
                                        //{
                                        //    companyPhoneNumber.Id = companyPhoneNumberInDB.Id;
                                        //    companyPhoneNumber.LastUpdate = DateTime.Now;
                                        //    phoneLoader.Update(companyPhoneNumber);
                                        //}
                                    }
                                }
                            }

                            #endregion

                            #region Elio Company Tags

                            foreach (string companyTag in companyTags)
                            {
                                if (companyTag != "")
                                {
                                    string tagName = Regex.Replace(companyTag, @"\\r\\n", "");
                                    tagName = tagName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                                    if (tagName != "")
                                    {
                                        ElioUsersPersonCompanyTags tag = new ElioUsersPersonCompanyTags();

                                        tag.ElioPersonCompanyId = elioCompany.Id;
                                        tag.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        tag.UserId = user.Id;
                                        tag.TagName = tagName;
                                        tag.Sysdate = DateTime.Now;
                                        tag.LastUpdate = DateTime.Now;
                                        tag.IsPublic = 1;
                                        tag.IsActive = 1;

                                        //DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);

                                        //ElioUsersPersonCompanyTags tagInDB = ClearbitSql.GetPersonCompanyTagsByTagName(user.Id, elioCompany.ClearbitCompanyId, tagName, session);

                                        //if (tagInDB == null)
                                        //{
                                        //    tagLoader.Insert(tag);
                                        //}
                                        //else
                                        //{
                                        //    tag.Id = tagInDB.Id;
                                        //    tag.LastUpdate = DateTime.Now;
                                        //    tagLoader.Update(tag);
                                        //}
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            return user;
                            //throw new Exception("ElioCompany did not find by id for user with email: " + emailAddress + " and for phones or tags");
                        }
                    }
                    else
                    {
                        #region Elio Users Company

                        elioCompany = new ElioUsersPersonCompanies();

                        string companyId = Guid.NewGuid().ToString();

                        elioCompany.ClearbitCompanyId = companyId;
                        elioCompany.ElioPersonId = elioPerson.Id;
                        elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                        elioCompany.UserId = user.Id;
                        elioCompany.Name = user.CompanyName;
                        elioCompany.Domain = user.WebSite;
                        elioCompany.Sector = "";

                        //List<ElioIndustries> industries = Sql.GetUsersIndustries(user.Id, session);

                        //if (industries.Count > 0)
                        //{
                        //    foreach (ElioIndustries industry in industries)
                        //    {
                        //        elioCompany.Industry += industry.IndustryDescription + ", ";
                        //    }
                        //    elioCompany.Industry = (industries.Count > 0) ? elioCompany.Industry.Substring(0, elioCompany.Industry.Length - 2) : "-";
                        //}
                        //else
                        //    elioCompany.Industry = "";

                        elioCompany.IndustryGroup = "";

                        //List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(user.Id, session);
                        //if (profileSubcategories.Count > 0)
                        //{
                        //    foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
                        //    {
                        //        elioCompany.SubIndustry += subcategory.DescriptionSubcategory + ", ";
                        //    }
                        //    elioCompany.SubIndustry = (profileSubcategories.Count > 0) ? elioCompany.SubIndustry.Substring(0, elioCompany.SubIndustry.Length - 2) : "-";
                        //}
                        //else
                        //    elioCompany.SubIndustry = "";

                        elioCompany.Description = user.Description;
                        elioCompany.FoundedYear = 0;
                        elioCompany.Location = user.Address;
                        //elioCompany.FundAmount = companyfu
                        elioCompany.EmployeesNumber = 0;
                        elioCompany.EmployeesRange = "";
                        elioCompany.AnnualRevenue = 0;
                        elioCompany.AnnualRevenueRange = "";
                        elioCompany.FacebookHandle = "";
                        elioCompany.FacebookLikes = 0;
                        elioCompany.LinkedinHandle = user.LinkedInUrl;
                        elioCompany.TwitterHandle = "";
                        elioCompany.TwitterId = "";
                        elioCompany.TwitterBio = "";
                        elioCompany.TwitterAvatar = "";
                        elioCompany.TwitterFollowers = 0;
                        elioCompany.TwitterFollowing = 0;
                        elioCompany.TwitterSite = "";
                        elioCompany.TwitterLocation = "";
                        elioCompany.CrunchbaseHandle = "";
                        elioCompany.CompanyPhone = user.Phone;
                        elioCompany.Logo = user.CompanyLogo;
                        elioCompany.Type = user.CompanyType;
                        elioCompany.DateInserted = user.SysDate;
                        elioCompany.LastUpdate = DateTime.Now;
                        elioCompany.IsPublic = 1;
                        elioCompany.IsActive = 1;

                        //DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);

                        //ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);

                        //if (elioCompanyInDB == null)
                        //{
                        //    companyLoader.Insert(elioCompany);
                        //}
                        //else
                        //{
                        //    elioCompany.Id = elioCompanyInDB.Id;
                        //    elioCompany.LastUpdate = DateTime.Now;
                        //    companyLoader.Update(elioCompany);
                        //}

                        #endregion
                    }

                    success = true;

                    if (success)
                    {
                        #region Elio Users Update credentials

                        user.IsPublic = (int)AccountPublicStatus.IsPublic;
                        //user.AccountStatus = (int)AccountStatus.Completed;
                        user.LastUpdated = DateTime.Now;
                        //user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion

                        #region Insert User Email Notifications Settings

                        //GlobalDBMethods.FixUserEmailNotificationsSettingsData(user, session);

                        #endregion

                        #region User Features

                        //ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                        //if (freeFeatures != null)
                        //{
                        //    ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                        //    if (userPackStatus == null)
                        //    {
                        //        userPackStatus = new ElioUserPacketStatus();

                        //        userPackStatus.UserId = user.Id;
                        //        userPackStatus.PackId = freeFeatures.PackId;
                        //        userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                        //        userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                        //        userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                        //        userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                        //        userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                        //        userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                        //        userPackStatus.Sysdate = DateTime.Now;
                        //        userPackStatus.LastUpdate = DateTime.Now;
                        //        userPackStatus.StartingDate = DateTime.Now;
                        //        userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                        //        DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                        //        loader.Insert(userPackStatus);
                        //    }
                        //}

                        #endregion
                    }
                }
                else
                {
                    success = false;
                    //return user;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error at, " + DateTime.Now.ToString() + ", for email address: " + user.Email, ex.Message.ToString(), ex.StackTrace.ToString());
                success = false;
            }

            return user;
        }

        public static bool FindCombinedPersonCompanyByEmail_v2(ElioUsers user, string emailAddress, DBSession session)
        {
            bool success = false;
            ElioUsersPerson elioPerson = null;
            ElioUsersPersonCompanies elioCompany = null;

            try
            {
                var client = new RestClient("https://person.clearbit.com");      //var client = new RestClient("https://person-stream.clearbit.com");
                //emailAddress = "oleksiy@rioks.com";
                var request = new RestRequest("v2/combined/find", Method.GET);
                request.AddParameter("email", emailAddress);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                IRestResponse response = client.Execute(request);
                
                if (response.StatusCode.ToString() != "OK")
                {
                    string error = "";
                    int caseIndex = 0;
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for clearbit person with eimail:" + emailAddress;
                        caseIndex = 0;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for clearbit person with eimail:" + emailAddress;
                        caseIndex = 1;
                    }
                    else
                    {
                        error = "Something went wrong for clearbit person with eimail:" + emailAddress;
                        caseIndex = 2;
                    }

                    //Logger.DetailedError("Class:ClearBit.cs --> Method:FindCombinedPersonCompanyByEmail_v2 -->", error.ToString());

                    if (caseIndex == 1)
                        Logger.DetailedClearBitError(emailAddress);

                    return false;
                }

                var personCompanyDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                JToken person = personCompanyDictionary["person"];
                JToken company = personCompanyDictionary["company"];

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (combinedPersonCompanyResponse != null)
                {
                    if (session.Connection.State == System.Data.ConnectionState.Closed)
                        session.OpenConnection();
                    
                    DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);

                    if (person.HasValues)
                    {
                        #region person response

                        string personId = combinedPersonCompanyResponse["person"]["id"].ToString();
                        string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                        string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                        string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                        string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                        //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                        string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                        string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                        string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                        string city = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                        string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                        string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                        string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                        string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                        string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                        string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                        string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                        string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                        string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                        string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                        string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                        string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                        string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                        string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                        string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                        string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                        string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                        string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                        string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                        string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                        string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                        string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                        string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                        string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                        string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                        string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                        string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                        string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                        string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                        string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                        string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                        string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                        string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                        string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                        //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                        //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                        //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                        string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                        string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();
                        //if (gravatar != "")
                        //{
                        //    string gravatarUrls = combinedPersonCompanyResponse["person"]["gravatar"]["urls"].ToString();
                        //    if (gravatarUrls != "")
                        //    {
                        //        string gravatarUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["value"].ToString();
                        //        string gravatarUrlsTitle = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["title"].ToString();
                        //        string personAvatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                        //        if (personAvatar != "")
                        //        {
                        //            string gravatarAvatarsUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["url"].ToString();
                        //            string gravatarAvatarsUrlsType = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["type"].ToString();
                        //        }
                        //    }
                        //}

                        string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                        string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                        string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                        #endregion

                        #region Elio Users Person

                        elioPerson = new ElioUsersPerson();

                        elioPerson.ClearbitPersonId = personId;
                        elioPerson.UserId = user.Id;
                        elioPerson.GivenName = givenName;
                        elioPerson.FamilyName = familyName;
                        elioPerson.Email = personEmail;
                        elioPerson.Phone = "";
                        elioPerson.Location = location;
                        if (!string.IsNullOrEmpty(city))
                            elioPerson.Location += ", " + city;
                        if (!string.IsNullOrEmpty(state))
                            elioPerson.Location += ", " + state;
                        elioPerson.TimeZone = timeZone;
                        elioPerson.Bio = bio;
                        elioPerson.Avatar = avatar;
                        elioPerson.Title = title;
                        elioPerson.Role = role;
                        elioPerson.Seniority = seniority;
                        elioPerson.TwitterHandle = twitterHandle;
                        elioPerson.LinkedinHandle = linkedinHandle;
                        elioPerson.AboutMeHandle = "";   //linkedinAboutmeHandle;
                        elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                        elioPerson.LastUpdate = DateTime.Now;
                        elioPerson.IsPublic = 1;
                        elioPerson.IsActive = 1;
                        elioPerson.IsClaimed = 0;

                        //bool existPerson = ClearbitSql.ExistsClearbitPerson(user.Id, personId, session);
                        //ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);

                        ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserId(user.Id, session);
                        if (elioPersonInDB == null)
                        {
                            personloader.Insert(elioPerson);
                        }
                        else
                        {
                            elioPerson.Id = elioPersonInDB.Id;
                            elioPerson.LastUpdate = DateTime.Now;
                            personloader.Update(elioPerson);
                        }

                        #endregion

                        #region Elio Users Update credentials

                        //user.Address = elioPerson.Location;
                        user.PersonalImage = elioPerson.Avatar;
                        //user.TwitterUrl = elioPerson.TwitterHandle;
                        //user.LinkedInUrl = elioPerson.LinkedinHandle;
                        user.Position = elioPerson.Title + "," + elioPerson.Seniority;
                        user.LastName = elioPerson.FamilyName;
                        user.FirstName = elioPerson.GivenName;
                        user.Country = (string.IsNullOrEmpty(user.Country)) ? country : user.Country;

                        #endregion
                    }
                    else
                    {                        
                        #region Elio User to Clearbit Person

                        string personId = Guid.NewGuid().ToString();

                        elioPerson = new ElioUsersPerson();

                        elioPerson.ClearbitPersonId = personId;
                        elioPerson.UserId = user.Id;
                        elioPerson.GivenName = user.FirstName;
                        elioPerson.FamilyName = user.LastName;
                        elioPerson.Email = (!string.IsNullOrEmpty(user.Email)) ? user.Email : emailAddress;
                        elioPerson.Phone = user.Phone;
                        elioPerson.Location = user.Address;
                        elioPerson.TimeZone = "";
                        elioPerson.Bio = "";
                        elioPerson.Avatar = "";
                        elioPerson.Title = user.Position;
                        elioPerson.Role = "";
                        elioPerson.Seniority = "";
                        elioPerson.TwitterHandle = "";
                        elioPerson.LinkedinHandle = user.LinkedInUrl;
                        elioPerson.AboutMeHandle = "";
                        elioPerson.DateInserted = user.SysDate;
                        elioPerson.LastUpdate = DateTime.Now;
                        elioPerson.IsPublic = 1;
                        elioPerson.IsActive = 1;
                        elioPerson.IsClaimed = 0;

                        //ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);

                        ElioUsersPerson elioPersonInDB = ClearbitSql.GetPersonByUserId(user.Id, session);
                        if (elioPersonInDB == null)
                        {
                            personloader.Insert(elioPerson);
                        }
                        else
                        {
                            //elioPerson.Id = elioPersonInDB.Id;
                            //elioPerson.LastUpdate = DateTime.Now;
                            //personloader.Update(elioPerson);

                            personloader.Delete(elioPersonInDB);
                        }

                        #endregion
                    }

                    if (company.HasValues)
                    {
                        string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                        string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                        string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();

                        if (elioPerson != null)
                        {
                            #region company response

                            string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                            string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                            string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                            string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                            string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                            string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                            string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                            string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                            string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                            string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                            string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();
                            string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                            string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                            string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                            string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                            string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                            string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                            string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                            string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                            string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                            string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                            string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                            string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                            string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                            string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                            string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                            string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                            string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                            string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                            string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                            string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                            string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                            string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                            string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                            string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                            string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                            string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                            string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                            string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                            string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                            string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                            string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                            string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                            string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                            string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                            string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                            string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                            string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                            string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                            string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                            string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                            string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                            string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                            string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                            string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                            string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                            string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                            #endregion

                            #region Elio Users Company

                            elioCompany = new ElioUsersPersonCompanies();

                            elioCompany.ClearbitCompanyId = companyId;
                            elioCompany.ElioPersonId = elioPerson.Id;
                            elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                            elioCompany.UserId = user.Id;
                            elioCompany.Name = companyName;
                            elioCompany.Domain = companyDomain;
                            elioCompany.Sector = companySector;
                            elioCompany.IndustryGroup = companIndustryGroup;
                            elioCompany.Industry = companyIndustry;
                            elioCompany.SubIndustry = companySubIndustry;
                            elioCompany.Description = companyDescription;
                            elioCompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                            elioCompany.Location = companyLocation;
                            //elioCompany.FundAmount = companyfu
                            elioCompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                            elioCompany.EmployeesRange = companyEmployeesRange;
                            elioCompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                            elioCompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                            elioCompany.FacebookHandle = companyFacebookHandle;
                            elioCompany.FacebookLikes = (companyFacebookLikes != "") ? Convert.ToInt32(companyFacebookLikes) : 0;
                            elioCompany.LinkedinHandle = companyLinkedinHandle;
                            elioCompany.TwitterHandle = companyTwitterHandle;
                            elioCompany.TwitterId = companyTwitterId;
                            elioCompany.TwitterBio = companyTwitterBio;
                            elioCompany.TwitterAvatar = companyTwtitterAvatar;
                            elioCompany.TwitterFollowers = (companyTwitterFollowers != "") ? Convert.ToInt32(companyTwitterFollowers) : 0;
                            elioCompany.TwitterFollowing = (companyTwitterFollowing != "") ? Convert.ToInt32(companyTwitterFollowing) : 0;
                            elioCompany.TwitterSite = companyTwitterSite;
                            elioCompany.TwitterLocation = companyTwitterLocation;
                            elioCompany.CrunchbaseHandle = companyCrunchbaseHandle;
                            elioCompany.CompanyPhone = companyPhone;
                            elioCompany.Logo = companyLogo;
                            elioCompany.Type = companyType;
                            elioCompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                            elioCompany.LastUpdate = DateTime.Now;
                            elioCompany.IsPublic = 1;
                            elioCompany.IsActive = 1;

                            DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);

                            //ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(user.Id, companyId, session);

                            #region CultureInfo

                            //CultureInfo culture1 = CultureInfo.CurrentCulture;
                            //string cultNamew = culture1.Name;
                            //string cutlDispName = culture1.DisplayName;

                            //if (Thread.CurrentThread.CurrentCulture.Name != "en-US")
                            //{
                            //    // If current culture is not fr-FR, set culture to fr-FR.
                            //    CultureInfo.DefaultThreadCurrentCulture = CultureInfo.CreateSpecificCulture("fr-FR");
                            //    CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.CreateSpecificCulture("fr-FR");
                            //}

                            //string s = companyDescription.ToLower(new CultureInfo("el-GR"));

                            #endregion

                            ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
                            if (elioCompanyInDB == null)
                            {
                                companyLoader.Insert(elioCompany);
                            }
                            else
                            {
                                elioCompany.Id = elioCompanyInDB.Id;
                                elioCompany.LastUpdate = DateTime.Now;
                                companyLoader.Update(elioCompany);
                            }

                            #endregion

                            #region Elio Users Update credentials

                            user.WebSite = (string.IsNullOrEmpty(user.WebSite)) ? (!elioCompany.Domain.StartsWith("www") && !elioCompany.Domain.StartsWith("http")) ? "www." + elioCompany.Domain : elioCompany.Domain : user.WebSite;
                            user.Country = (string.IsNullOrEmpty(user.Country)) ? companyCountry : user.Country;
                            user.CompanyLogo = (string.IsNullOrEmpty(user.CompanyLogo)) ? elioCompany.Logo : user.CompanyLogo;
                            user.TwitterUrl = elioCompany.TwitterHandle;
                            user.Description = elioCompany.Description;

                            #endregion
                        }
                        else
                        {
                            throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                        }

                        if (elioCompany != null)
                        {
                            #region Elio Company Phones

                            foreach (string phone in companySitePhoneNumbers)
                            {
                                if (phone != "")
                                {
                                    string number = Regex.Replace(phone, @"\\r\\n", "");
                                    number = number.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                                    if (number != "")
                                    {
                                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                                        companyPhoneNumber.ElioPersonCompanyId = elioCompany.Id;
                                        companyPhoneNumber.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        companyPhoneNumber.UserId = user.Id;
                                        companyPhoneNumber.PhoneNumber = number;
                                        companyPhoneNumber.Sysdate = DateTime.Now;
                                        companyPhoneNumber.LastUpdate = DateTime.Now;

                                        DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);

                                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumberInDB = ClearbitSql.GetPersonCompanyPhoneByPhone(user.Id, elioCompany.ClearbitCompanyId, number, session);

                                        if (companyPhoneNumberInDB == null)
                                        {
                                            phoneLoader.Insert(companyPhoneNumber);
                                        }
                                        else
                                        {
                                            companyPhoneNumber.Id = companyPhoneNumberInDB.Id;
                                            companyPhoneNumber.LastUpdate = DateTime.Now;
                                            phoneLoader.Update(companyPhoneNumber);
                                        }
                                    }
                                }
                            }

                            #endregion

                            #region Elio Company Tags

                            foreach (string companyTag in companyTags)
                            {
                                if (companyTag != "")
                                {
                                    string tagName = Regex.Replace(companyTag, @"\\r\\n", "");
                                    tagName = tagName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();

                                    if (tagName != "")
                                    {
                                        ElioUsersPersonCompanyTags tag = new ElioUsersPersonCompanyTags();

                                        tag.ElioPersonCompanyId = elioCompany.Id;
                                        tag.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        tag.UserId = user.Id;
                                        tag.TagName = tagName;
                                        tag.Sysdate = DateTime.Now;
                                        tag.LastUpdate = DateTime.Now;
                                        tag.IsPublic = 1;
                                        tag.IsActive = 1;

                                        DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);

                                        ElioUsersPersonCompanyTags tagInDB = ClearbitSql.GetPersonCompanyTagsByTagName(user.Id, elioCompany.ClearbitCompanyId, tagName, session);

                                        if (tagInDB == null)
                                        {
                                            tagLoader.Insert(tag);
                                        }
                                        else
                                        {
                                            tag.Id = tagInDB.Id;
                                            tag.LastUpdate = DateTime.Now;
                                            tagLoader.Update(tag);
                                        }
                                    }
                                }
                            }

                            #endregion
                        }
                        else
                        {
                            throw new Exception("ElioCompany did not find by id for user with email: " + emailAddress + " and for phones or tags");
                        }
                    }
                    else
                    {
                        #region Elio Users Company

                        elioCompany = new ElioUsersPersonCompanies();

                        string companyId = Guid.NewGuid().ToString();

                        elioCompany.ClearbitCompanyId = companyId;
                        elioCompany.ElioPersonId = elioPerson.Id;
                        elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                        elioCompany.UserId = user.Id;
                        elioCompany.Name = user.CompanyName;
                        elioCompany.Domain = user.WebSite;
                        elioCompany.Sector = "";

                        List<ElioIndustries> industries = Sql.GetUsersIndustries(user.Id, session);

                        if (industries.Count > 0)
                        {
                            foreach (ElioIndustries industry in industries)
                            {
                                elioCompany.Industry += industry.IndustryDescription + ", ";
                            }
                            elioCompany.Industry = (industries.Count > 0) ? elioCompany.Industry.Substring(0, elioCompany.Industry.Length - 2) : "-";
                        }
                        else
                            elioCompany.Industry = "";

                        elioCompany.IndustryGroup = "";

                        List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> profileSubcategories = Sql.GetUserSubcategoriesById(user.Id, session);
                        if (profileSubcategories.Count > 0)
                        {
                            foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers subcategory in profileSubcategories)
                            {
                                elioCompany.SubIndustry += subcategory.DescriptionSubcategory + ", ";
                            }
                            elioCompany.SubIndustry = (profileSubcategories.Count > 0) ? elioCompany.SubIndustry.Substring(0, elioCompany.SubIndustry.Length - 2) : "-";
                        }
                        else
                            elioCompany.SubIndustry = "";

                        elioCompany.Description = user.Description;
                        elioCompany.FoundedYear = 0;
                        elioCompany.Location = user.Address;
                        //elioCompany.FundAmount = companyfu
                        elioCompany.EmployeesNumber = 0;
                        elioCompany.EmployeesRange = "";
                        elioCompany.AnnualRevenue = 0;
                        elioCompany.AnnualRevenueRange = "";
                        elioCompany.FacebookHandle = "";
                        elioCompany.FacebookLikes = 0;
                        elioCompany.LinkedinHandle = user.LinkedInUrl;
                        elioCompany.TwitterHandle = "";
                        elioCompany.TwitterId = "";
                        elioCompany.TwitterBio = "";
                        elioCompany.TwitterAvatar = "";
                        elioCompany.TwitterFollowers = 0;
                        elioCompany.TwitterFollowing = 0;
                        elioCompany.TwitterSite = "";
                        elioCompany.TwitterLocation = "";
                        elioCompany.CrunchbaseHandle = "";
                        elioCompany.CompanyPhone = user.Phone;
                        elioCompany.Logo = user.CompanyLogo;
                        elioCompany.Type = user.CompanyType;
                        elioCompany.DateInserted = user.SysDate;
                        elioCompany.LastUpdate = DateTime.Now;
                        elioCompany.IsPublic = 1;
                        elioCompany.IsActive = 1;

                        DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);

                        //ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(user.Id, companyId, session);
                        ElioUsersPersonCompanies elioCompanyInDB = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);

                        if (elioCompanyInDB == null)
                        {
                            companyLoader.Insert(elioCompany);
                        }
                        else
                        {
                            elioCompany.Id = elioCompanyInDB.Id;
                            elioCompany.LastUpdate = DateTime.Now;
                            companyLoader.Update(elioCompany);
                        }

                        #endregion
                    }

                    success = true;

                    if (success)
                    {
                        #region Elio Users Update credentials

                        user.IsPublic = (int)AccountPublicStatus.IsPublic;
                        user.AccountStatus = (int)AccountStatus.Completed;
                        user.LastUpdated = DateTime.Now;
                        user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion

                        #region Insert User Email Notifications Settings

                        GlobalDBMethods.FixUserEmailNotificationsSettingsData(user, session);

                        #endregion

                        #region User Features

                        ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                        if (freeFeatures != null)
                        {
                            ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                            if (userPackStatus == null)
                            {
                                userPackStatus = new ElioUserPacketStatus();

                                userPackStatus.UserId = user.Id;
                                userPackStatus.PackId = freeFeatures.PackId;
                                userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                                userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                                userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                                userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                                userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                                userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                                userPackStatus.Sysdate = DateTime.Now;
                                userPackStatus.LastUpdate = DateTime.Now;
                                userPackStatus.StartingDate = DateTime.Now;
                                userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                                DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                                loader.Insert(userPackStatus);
                            }
                        }

                        #endregion
                    }
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error at, " + DateTime.Now.ToString() + ", for email address: " + emailAddress, ex.Message.ToString(), ex.StackTrace.ToString());
                success = false;
            }

            return success;
        }

        public static bool FindCombinedPersonCompanyByEmail(ElioUsers user, string emailAddress, DBSession session)
        {
            bool success = false;
            ElioUsersPerson elioPerson = null;
            ElioUsersPersonCompanies elioCompany = null;
            List<ElioUsersPersonCompanyPhoneNumbers> phones = null;
            List<ElioUsersPersonCompanyTags> tags = null;

            try
            {
                var client = new RestClient("https://person.clearbit.com");

                var request = new RestRequest("v2/combined/find", Method.GET);
                request.AddParameter("email", emailAddress);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                //switch (response.StatusCode.ToString())
                //{
                //    case "400":
                //    case "404":
                //    case "202":
                //        {
                //            return false;
                //        }

                //    case "200":
                //        break;

                //    default:
                //        break;
                //}

                if (response.StatusCode.ToString() != "OK")
                {
                    if (response.ErrorMessage != "")
                    {
                        string error = response.ErrorMessage;
                        Logger.DetailedError("Class:ClearBit.cs --> Method:FindCombinedPersonCompanyByEmail --> Email:" + emailAddress, error.ToString(), error.ToString());
                    }

                    return false;
                }
                //else if (response.StatusCode.ToString() == "404")
                //{
                //    return false;
                //}
                //else if (response.StatusCode.ToString() == "202")
                //{
                //    return false;
                //}

                var personCompanyDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                JToken person = personCompanyDictionary["person"];
                JToken company = personCompanyDictionary["company"];

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (combinedPersonCompanyResponse != null)
                {
                    if (person.HasValues)
                    {
                        string personId = combinedPersonCompanyResponse["person"]["id"].ToString();

                        bool existsPerson = ClearbitSql.ExistsClearbitPerson(user.Id, personId, session);
                        if (!existsPerson)
                        {
                            #region person response
                            
                            string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                            string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                            string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                            string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                            //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                            string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                            string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                            string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                            string geo = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                            string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                            string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                            string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                            string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                            string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                            string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                            string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                            string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                            string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                            string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                            string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                            string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                            string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                            string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                            string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                            string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                            string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                            string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                            string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                            string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                            string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                            string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                            string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                            string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                            string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                            string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                            string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                            string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                            string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                            string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                            string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                            string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                            string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                            string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                            //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                            //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                            //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                            string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                            string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();
                            if (gravatar != "")
                            {
                                string gravatarUrls = combinedPersonCompanyResponse["person"]["gravatar"]["urls"].ToString();
                                if (gravatarUrls != "")
                                {
                                    string gravatarUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["value"].ToString();
                                    string gravatarUrlsTitle = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["title"].ToString();
                                    string personAvatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                                    if (personAvatar != "")
                                    {
                                        string gravatarAvatarsUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["url"].ToString();
                                        string gravatarAvatarsUrlsType = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["type"].ToString();
                                    }
                                }
                            }

                            string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                            string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                            string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                            #endregion

                            #region Elio Users Person

                            elioPerson = new ElioUsersPerson();

                            elioPerson.ClearbitPersonId = personId;
                            elioPerson.UserId = user.Id;
                            elioPerson.GivenName = givenName;
                            elioPerson.FamilyName = familyName;
                            elioPerson.Email = personEmail;
                            elioPerson.Phone = "";
                            elioPerson.Location = location;
                            elioPerson.TimeZone = timeZone;
                            elioPerson.Bio = bio;
                            elioPerson.Avatar = avatar;
                            elioPerson.Title = title;
                            elioPerson.Role = role;
                            elioPerson.Seniority = seniority;
                            elioPerson.TwitterHandle = twitterHandle;
                            elioPerson.LinkedinHandle = linkedinHandle;
                            elioPerson.AboutMeHandle = "";     //linkedinAboutmeHandle;
                            elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                            elioPerson.LastUpdate = DateTime.Now;
                            elioPerson.IsPublic = 1;
                            elioPerson.IsActive = 1;
                            elioPerson.IsClaimed = 0;

                            DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);
                            personloader.Insert(elioPerson);

                            #endregion

                            #region Elio Users Update credentials

                            user.Address = elioPerson.Location; 
                            user.PersonalImage = elioPerson.Avatar;
                            //user.TwitterUrl = elioPerson.TwitterHandle;
                            user.LinkedInUrl = elioPerson.LinkedinHandle;
                            user.Position = elioPerson.Title + "," + elioPerson.Seniority;
                            user.LastName = elioPerson.FamilyName;
                            user.FirstName = elioPerson.GivenName;
                            user.Country = (string.IsNullOrEmpty(user.Country))?country:user.Country;

                            #endregion
                        }
                        else
                        {
                            elioPerson = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);
                        }
                    }
                   
                    if (company.HasValues)
                    {
                        string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                        string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                        string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();

                        bool existsCompany = ClearbitSql.ExistsClearbitCompany(user.Id, companyId, session);
                        if (!existsCompany)
                        {
                            if (elioPerson != null)
                            {
                                #region company response
                                
                                string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                                string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                                string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                                string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();                                
                                string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                                string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                                string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                                string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                                string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                                string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                                string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();                                
                                string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                                string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                                string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                                string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                                string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                                string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                                string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                                string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                                string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                                string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                                string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                                string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                                string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                                string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                                string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                                string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                                string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                                string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                                string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                                string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                                string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                                string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                                string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                                string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                                string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                                string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                                string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                                string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                                string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                                string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                                string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                                string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                                string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                                string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                                string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                                string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                                string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                                string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                                string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                                string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                                string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                                string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                                string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                                string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                                string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                                //string[] companyTechCategories = combinedPersonCompanyResponse["company"]["techCategories"].ToString().Split(',').ToArray();
                                string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                                #endregion

                                #region Elio Users Company

                                elioCompany = new ElioUsersPersonCompanies();

                                elioCompany.ClearbitCompanyId = companyId;
                                elioCompany.ElioPersonId = elioPerson.Id;
                                elioCompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                                elioCompany.UserId = user.Id;
                                elioCompany.Name = companyName;
                                elioCompany.Domain = companyDomain;
                                elioCompany.Sector = companySector;
                                elioCompany.IndustryGroup = companIndustryGroup;
                                elioCompany.Industry = companyIndustry;
                                elioCompany.SubIndustry = companySubIndustry;
                                elioCompany.Description = companyDescription;
                                elioCompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                                elioCompany.Location = companyLocation;
                                //elioCompany.FundAmount = companyfu
                                elioCompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                                elioCompany.EmployeesRange = companyEmployeesRange;
                                elioCompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                                elioCompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                                elioCompany.FacebookHandle = companyFacebookHandle;
                                elioCompany.TwitterHandle = companyTwitterHandle;
                                elioCompany.CrunchbaseHandle = companyCrunchbaseHandle;
                                elioCompany.Logo = companyLogo;
                                elioCompany.Type = companyType;                                
                                elioCompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                                elioCompany.LastUpdate = DateTime.Now;
                                elioCompany.IsPublic = 1;
                                elioCompany.IsActive = 1;

                                DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);
                                companyLoader.Insert(elioCompany);

                                #endregion

                                #region Elio Users Update credentials

                                user.WebSite = (string.IsNullOrEmpty(user.WebSite))?elioCompany.Domain:user.WebSite;
                                user.Country = (string.IsNullOrEmpty(user.Country)) ? companyCountry : user.Country;
                                user.CompanyLogo = (string.IsNullOrEmpty(user.CompanyLogo)) ? elioCompany.Logo : user.CompanyLogo;
                                user.TwitterUrl = elioCompany.TwitterHandle;
                                user.Description = elioCompany.Description;

                                #endregion
                            }
                            else
                            {
                                throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                            }
                        }
                        else
                        {
                            elioCompany = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(user.Id, companyId, session);
                        }

                        bool hasCompanyPhones = ClearbitSql.HasClearbitCompanyPhones(user.Id, companyId, session);
                        if (!hasCompanyPhones)
                        {
                            if (elioCompany != null)
                            {
                                #region Elio Company Phones

                                foreach (string phone in companySitePhoneNumbers)
                                {
                                    if (phone != "")
                                    {
                                        string number = Regex.Replace(phone, @"\\r\\n", "");
                                        number = number.Replace(" \"","").Replace(@"\","").Replace(@"""","").Replace("[", "").Replace("]", "").Trim();
                                        
                                        ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                                        companyPhoneNumber.ElioPersonCompanyId = elioCompany.Id;
                                        companyPhoneNumber.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        companyPhoneNumber.UserId = user.Id;
                                        companyPhoneNumber.PhoneNumber = number;
                                        companyPhoneNumber.Sysdate = DateTime.Now;
                                        companyPhoneNumber.LastUpdate = DateTime.Now;

                                        DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
                                        phoneLoader.Insert(companyPhoneNumber);
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                throw new Exception("ElioCompany did not find by id for phoneNumbers");
                            }
                        }
                        else
                        {
                            phones = ClearbitSql.GetPersonCompanyPhonesByUserIdAndCompanyId(user.Id, elioCompany.ClearbitCompanyId, session);
                        }

                        bool hasCompanyTags = ClearbitSql.HasClearbitCompanyTags(user.Id, companyId, session);
                        if (!hasCompanyTags)
                        {
                            if (elioCompany != null)
                            {
                                #region Elio Company Tags

                                foreach (string companyTag in companyTags)
                                {
                                    if (companyTag != "")
                                    {
                                        string tagName = Regex.Replace(companyTag, @"\\r\\n", "");
                                        tagName = tagName.Replace(" \"", "").Replace(@"\", "").Replace(@"""", "").Replace("[", "").Replace("]", "").Trim();
                                        
                                        ElioUsersPersonCompanyTags tag = new ElioUsersPersonCompanyTags();

                                        tag.ElioPersonCompanyId = elioCompany.Id;
                                        tag.ClearbitCompanyId = elioCompany.ClearbitCompanyId;
                                        tag.UserId = user.Id;
                                        tag.TagName = tagName;
                                        tag.Sysdate = DateTime.Now;
                                        tag.LastUpdate = DateTime.Now;
                                        tag.IsPublic = 1;
                                        tag.IsActive = 1;

                                        DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);
                                        tagLoader.Insert(tag);
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                throw new Exception("ElioCompany did not find by id for tags");
                            }
                        }
                        else
                        {
                            tags = ClearbitSql.GetPersonCompanyTagsByUserIdAndCompanyId(user.Id, elioCompany.ClearbitCompanyId, session);
                        }
                    }

                    success = true;

                    if (success)
                    {
                        if (user.IsPublic == (int)AccountPublicStatus.IsNotPublic)  //to do
                        {
                            user.IsPublic = (int)AccountPublicStatus.IsPublic;
                            user.AccountStatus = (int)AccountStatus.Completed;
                            user.LastUpdated = DateTime.Now;
                            user = GlobalDBMethods.UpDateUser(user, session);
                        }
                    }
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error", ex.Message.ToString(), ex.StackTrace.ToString());
                success = false;
            }

            return success;
        }

        public static bool FindCombinedPersonCompanyByEmailWithOut(string emailAddress, DBSession session, out ElioUsersPerson elioPerson, out ElioUsersPersonCompanies eliocompany, out List<ElioUsersPersonCompanyPhoneNumbers> phones, out List<ElioUsersPersonCompanyTags> tags, out ElioUsers user)
        {
            bool success = false;
            elioPerson = null;
            eliocompany = null;
            phones = null;
            tags = null;
            user = new ElioUsers();
            if (user != null)
            {
                try
                {
                    var client = new RestClient("https://person.clearbit.com");

                    var request = new RestRequest("v2/combined/find", Method.GET);
                    request.AddParameter("email", emailAddress);
                    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                    IRestResponse response = client.Execute(request);

                    var personCompanyDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                    JToken person = personCompanyDictionary["person"];
                    JToken company = personCompanyDictionary["company"];

                    JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                    if (combinedPersonCompanyResponse != null)
                    {
                        if (person.HasValues)
                        {
                            #region person response

                            string personId = combinedPersonCompanyResponse["person"]["id"].ToString();
                            string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                            string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                            string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                            string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                            //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();
                            string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                            string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                            string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();
                            string geo = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                            string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                            string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                            string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                            string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                            string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                            string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();
                            string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                            string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                            string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                            string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                            string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                            string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                            string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                            string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();
                            string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                            string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                            string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                            string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                            string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                            string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                            string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                            string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();
                            string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                            string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                            string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                            string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                            string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                            string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                            string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                            string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                            string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                            string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();
                            string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();
                            string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();
                            //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                            //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                            //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();
                            string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                            string gravatar = combinedPersonCompanyResponse["person"]["gravatar"].ToString();
                            if (gravatar != "")
                            {
                                string gravatarUrls = combinedPersonCompanyResponse["person"]["gravatar"]["urls"].ToString();
                                if (gravatarUrls != "")
                                {
                                    string gravatarUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["value"].ToString();
                                    string gravatarUrlsTitle = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["title"].ToString();
                                    string personAvatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                                    if (personAvatar != "")
                                    {
                                        string gravatarAvatarsUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["url"].ToString();
                                        string gravatarAvatarsUrlsType = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["type"].ToString();
                                    }
                                }
                            }

                            string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                            string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                            string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                            #endregion

                            bool existsPerson = ClearbitSql.ExistsClearbitPerson(user.Id, personId, session);

                            if (!existsPerson)
                            {
                                #region Elio Users Person

                                elioPerson = new ElioUsersPerson();

                                elioPerson.ClearbitPersonId = personId;
                                elioPerson.UserId = user.Id;
                                elioPerson.GivenName = givenName;
                                elioPerson.FamilyName = familyName;
                                elioPerson.Email = personEmail;
                                elioPerson.Phone = "";
                                elioPerson.Location = location;
                                elioPerson.TimeZone = timeZone;
                                elioPerson.Bio = bio;
                                elioPerson.Avatar = avatar;
                                elioPerson.Title = title;
                                elioPerson.Role = role;
                                elioPerson.Seniority = seniority;
                                elioPerson.TwitterHandle = twitterHandle;
                                elioPerson.LinkedinHandle = linkedinHandle;
                                elioPerson.AboutMeHandle = "";     //linkedinAboutmeHandle;
                                elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                                elioPerson.LastUpdate = DateTime.Now;
                                elioPerson.IsPublic = 1;
                                elioPerson.IsActive = 1;
                                elioPerson.IsClaimed = 0;

                                DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);
                                personloader.Insert(elioPerson);

                                #endregion
                            }
                            else
                            {
                                elioPerson = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(user.Id, personId, session);
                            }
                        }

                        if (company.HasValues)
                        {
                            #region company response

                            string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();
                            string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                            string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                            string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                            string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                            string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                            string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
                            string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                            string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                            string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                            string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                            string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                            string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();
                            string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();
                            string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                            string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                            string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                            string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                            string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();
                            string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                            string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                            string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                            string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                            string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                            string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                            string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                            string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                            string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                            string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                            string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();
                            string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();
                            string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                            string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();
                            string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();
                            string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                            string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                            string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                            string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                            string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                            string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                            string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                            string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();
                            string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();
                            string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                            string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                            string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();
                            string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                            string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                            string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                            string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                            string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                            string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                            string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                            string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                            string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                            string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                            string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();
                            string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();
                            string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();
                            string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                            #endregion

                            bool existsCompany = ClearbitSql.ExistsClearbitCompany(user.Id, companyId, session);

                            if (!existsCompany)
                            {
                                if (elioPerson != null)
                                {
                                    #region Elio Users Company

                                    eliocompany = new ElioUsersPersonCompanies();

                                    eliocompany.ClearbitCompanyId = companyId;
                                    eliocompany.ElioPersonId = elioPerson.Id;
                                    eliocompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                                    eliocompany.UserId = user.Id;
                                    eliocompany.Name = companyName;
                                    eliocompany.Domain = companyDomain;
                                    eliocompany.Sector = companySector;
                                    eliocompany.IndustryGroup = companIndustryGroup;
                                    eliocompany.Industry = companyIndustry;
                                    eliocompany.SubIndustry = companySubIndustry;
                                    eliocompany.Description = companyDescription;
                                    eliocompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                                    eliocompany.Location = companyLocation;
                                    //eliocompany.FundAmount = companyfu
                                    eliocompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                                    eliocompany.EmployeesRange = companyEmployeesRange;
                                    eliocompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                                    eliocompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                                    eliocompany.FacebookHandle = companyFacebookHandle;
                                    eliocompany.TwitterHandle = companyTwitterHandle;
                                    eliocompany.CrunchbaseHandle = companyCrunchbaseHandle;
                                    eliocompany.Logo = companyLogo;
                                    eliocompany.Type = companyType;
                                    eliocompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                                    eliocompany.LastUpdate = DateTime.Now;
                                    eliocompany.IsPublic = 1;
                                    eliocompany.IsActive = 1;

                                    DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);
                                    companyLoader.Insert(eliocompany);

                                    #endregion
                                }
                                else
                                {
                                    throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                                }
                            }
                            else
                            {
                                eliocompany = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(user.Id, companyId, session);
                            }

                            bool hasCompanyPhones = ClearbitSql.HasClearbitCompanyPhones(user.Id, companyId, session);

                            if (!hasCompanyPhones)
                            {
                                if (eliocompany != null)
                                {
                                    #region Elio Company Phones

                                    foreach (string phone in companySitePhoneNumbers)
                                    {
                                        if (phone != "")
                                        {
                                            ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                                            companyPhoneNumber.ElioPersonCompanyId = eliocompany.Id;
                                            companyPhoneNumber.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                            companyPhoneNumber.UserId = user.Id;
                                            companyPhoneNumber.PhoneNumber = phone;
                                            companyPhoneNumber.Sysdate = DateTime.Now;
                                            companyPhoneNumber.LastUpdate = DateTime.Now;

                                            DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
                                            phoneLoader.Insert(companyPhoneNumber);
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    throw new Exception("ElioCompany did not find by id for phoneNumbers");
                                }
                            }
                            else
                            {
                                phones = ClearbitSql.GetPersonCompanyPhonesByUserIdAndCompanyId(user.Id, eliocompany.ClearbitCompanyId, session);
                            }

                            bool hasCompanyTags = ClearbitSql.HasClearbitCompanyTags(user.Id, companyId, session);

                            if (!hasCompanyTags)
                            {
                                if (eliocompany != null)
                                {
                                    #region Elio Company Tags

                                    foreach (string companyTag in companyTags)
                                    {
                                        if (companyTag != "")
                                        {
                                            ElioUsersPersonCompanyTags tag = new ElioUsersPersonCompanyTags();

                                            tag.ElioPersonCompanyId = eliocompany.Id;
                                            tag.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                            tag.UserId = user.Id;
                                            tag.TagName = companyTag;
                                            tag.Sysdate = DateTime.Now;
                                            tag.LastUpdate = DateTime.Now;
                                            tag.IsPublic = 1;
                                            tag.IsActive = 1;

                                            DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);
                                            tagLoader.Insert(tag);
                                        }
                                    }

                                    #endregion
                                }
                                else
                                {
                                    throw new Exception("ElioCompany did not find by id for tags");
                                }
                            }
                            else
                            {
                                tags = ClearbitSql.GetPersonCompanyTagsByUserIdAndCompanyId(user.Id, eliocompany.ClearbitCompanyId, session);
                            }
                        }

                        success = true;

                        if (success)
                        {
                            user.IsPublic = (int)AccountPublicStatus.IsPublic;
                            user.LastUpdated = DateTime.Now;
                            user = GlobalDBMethods.UpDateUser(user, session);
                        }
                    }
                    else
                    {
                        success = false;
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError("Service Clearbit class error", ex.Message.ToString(), ex.StackTrace.ToString());
                    success = false;
                }
            }

            return success;
        }

        public static bool UpdateCombinedPersonCompanyByEmail(int userId, string emailAddress, DBSession session, out ElioUsersPerson elioPerson, out ElioUsersPersonCompanies eliocompany, out List<ElioUsersPersonCompanyPhoneNumbers> phones, out List<ElioUsersPersonCompanyTags> tags)
        {
            bool success = false;
            elioPerson = null;
            eliocompany = null;
            phones = null;
            tags = null;

            try
            {
                var client = new RestClient("https://person.clearbit.com");

                var request = new RestRequest("v2/combined/find", Method.GET);
                request.AddParameter("email", emailAddress);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());
                
                IRestResponse response = client.Execute(request);

                JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (combinedPersonCompanyResponse != null)
                {
                    #region person response

                    string personId = combinedPersonCompanyResponse["person"]["id"].ToString();

                    string fullName = combinedPersonCompanyResponse["person"]["name"]["fullName"].ToString();
                    string givenName = combinedPersonCompanyResponse["person"]["name"]["givenName"].ToString();
                    string familyName = combinedPersonCompanyResponse["person"]["name"]["familyName"].ToString();
                    string personEmail = combinedPersonCompanyResponse["person"]["email"].ToString();
                    //string gender = combinedPersonCompanyResponse["person"]["gender"].ToString();

                    string location = combinedPersonCompanyResponse["person"]["location"].ToString();
                    string timeZone = combinedPersonCompanyResponse["person"]["timeZone"].ToString();
                    string utcOffset = combinedPersonCompanyResponse["person"]["utcOffset"].ToString();

                    string geo = combinedPersonCompanyResponse["person"]["geo"]["city"].ToString();
                    string state = combinedPersonCompanyResponse["person"]["geo"]["state"].ToString();
                    string stateCode = combinedPersonCompanyResponse["person"]["geo"]["stateCode"].ToString();
                    string country = combinedPersonCompanyResponse["person"]["geo"]["country"].ToString();
                    string countryCode = combinedPersonCompanyResponse["person"]["geo"]["countryCode"].ToString();
                    string lat = combinedPersonCompanyResponse["person"]["geo"]["lat"].ToString();
                    string lng = combinedPersonCompanyResponse["person"]["geo"]["lng"].ToString();

                    string bio = combinedPersonCompanyResponse["person"]["bio"].ToString();
                    string site = combinedPersonCompanyResponse["person"]["site"].ToString();
                    string avatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                    string domain = combinedPersonCompanyResponse["person"]["employment"]["domain"].ToString();
                    string name = combinedPersonCompanyResponse["person"]["employment"]["name"].ToString();
                    string title = combinedPersonCompanyResponse["person"]["employment"]["title"].ToString();
                    string role = combinedPersonCompanyResponse["person"]["employment"]["role"].ToString();
                    string seniority = combinedPersonCompanyResponse["person"]["employment"]["seniority"].ToString();

                    string facebookHandle = combinedPersonCompanyResponse["person"]["facebook"]["handle"].ToString();
                    string githubHandle = combinedPersonCompanyResponse["person"]["github"]["handle"].ToString();
                    string githubId = combinedPersonCompanyResponse["person"]["github"]["id"].ToString();
                    string githubavatar = combinedPersonCompanyResponse["person"]["github"]["avatar"].ToString();
                    string githubCompany = combinedPersonCompanyResponse["person"]["github"]["company"].ToString();
                    string githubBlog = combinedPersonCompanyResponse["person"]["github"]["blog"].ToString();
                    string githubFollowers = combinedPersonCompanyResponse["person"]["github"]["followers"].ToString();
                    string githubFollowing = combinedPersonCompanyResponse["person"]["github"]["following"].ToString();

                    string twitterHandle = combinedPersonCompanyResponse["person"]["twitter"]["handle"].ToString();
                    string twitterId = combinedPersonCompanyResponse["person"]["twitter"]["id"].ToString();
                    string twitterBio = combinedPersonCompanyResponse["person"]["twitter"]["bio"].ToString();
                    string twitterFollowers = combinedPersonCompanyResponse["person"]["twitter"]["followers"].ToString();
                    string twitterFollowing = combinedPersonCompanyResponse["person"]["twitter"]["following"].ToString();
                    string twitterStatuses = combinedPersonCompanyResponse["person"]["twitter"]["statuses"].ToString();
                    string twitterFavorites = combinedPersonCompanyResponse["person"]["twitter"]["favorites"].ToString();
                    string twitterLocation = combinedPersonCompanyResponse["person"]["twitter"]["location"].ToString();
                    string twitterSite = combinedPersonCompanyResponse["person"]["twitter"]["site"].ToString();
                    string twitterAvatar = combinedPersonCompanyResponse["person"]["twitter"]["avatar"].ToString();

                    string linkedinHandle = combinedPersonCompanyResponse["person"]["linkedin"]["handle"].ToString();

                    string linkedinGoogleplusHandle = combinedPersonCompanyResponse["person"]["googleplus"]["handle"].ToString();

                    //string linkedinAboutmeHandle = combinedPersonCompanyResponse["person"]["aboutme"]["handle"].ToString();
                    //string linkedinAboutmeBio = combinedPersonCompanyResponse["person"]["aboutme"]["bio"].ToString();
                    //string linkedinAboutmeAvatar = combinedPersonCompanyResponse["person"]["aboutme"]["avatar"].ToString();

                    string gravatarHandle = combinedPersonCompanyResponse["person"]["gravatar"]["handle"].ToString();
                    string gravatarUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["value"].ToString();
                    string gravatarUrlsTitle = combinedPersonCompanyResponse["person"]["gravatar"]["urls"][0]["title"].ToString();

                    string personAvatar = combinedPersonCompanyResponse["person"]["avatar"].ToString();
                    string gravatarAvatarsUrlsValue = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["url"].ToString();
                    string gravatarAvatarsUrlsType = combinedPersonCompanyResponse["person"]["gravatar"]["avatars"][0]["type"].ToString();

                    string fuzzy = combinedPersonCompanyResponse["person"]["fuzzy"].ToString();
                    string emailProvider = combinedPersonCompanyResponse["person"]["emailProvider"].ToString();
                    string indexedAt = combinedPersonCompanyResponse["person"]["indexedAt"].ToString();

                    #endregion

                    elioPerson = ClearbitSql.GetPersonByUserIdAndClearbitPersonId(userId, personId, session);

                    if (elioPerson != null)
                    {
                        #region Elio Users Person

                        elioPerson.ClearbitPersonId = personId;
                        elioPerson.UserId = userId;
                        elioPerson.GivenName = givenName;
                        elioPerson.FamilyName = familyName;
                        elioPerson.Email = personEmail;
                        elioPerson.Phone = "";
                        elioPerson.Location = location;
                        elioPerson.TimeZone = timeZone;
                        elioPerson.Bio = bio;
                        elioPerson.Avatar = avatar;
                        elioPerson.Title = title;
                        elioPerson.Role = role;
                        elioPerson.Seniority = seniority;
                        elioPerson.TwitterHandle = twitterHandle;
                        elioPerson.LinkedinHandle = linkedinHandle;
                        elioPerson.AboutMeHandle = "";     //linkedinAboutmeHandle;
                        elioPerson.DateInserted = (indexedAt != null) ? Convert.ToDateTime(indexedAt) : DateTime.Now;
                        elioPerson.LastUpdate = DateTime.Now;
                        elioPerson.IsPublic = 1;
                        elioPerson.IsActive = 1;
                        elioPerson.IsClaimed = 0;

                        DataLoader<ElioUsersPerson> personloader = new DataLoader<ElioUsersPerson>(session);
                        personloader.Update(elioPerson);

                        #endregion
                    }

                    #region company response

                    string companyId = combinedPersonCompanyResponse["company"]["id"].ToString();

                    string companyName = combinedPersonCompanyResponse["company"]["name"].ToString();
                    string companyLegalName = combinedPersonCompanyResponse["company"]["legalName"].ToString();
                    string companyDomain = combinedPersonCompanyResponse["company"]["domain"].ToString();
                    string[] companyDomainAliases = combinedPersonCompanyResponse["company"]["domainAliases"].ToString().Split(',').ToArray();
                    string[] companySitePhoneNumbers = combinedPersonCompanyResponse["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();
                    string[] companyEmailAddresses = combinedPersonCompanyResponse["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();

                    string companySector = combinedPersonCompanyResponse["company"]["category"]["sector"].ToString();
                    string companIndustryGroup = combinedPersonCompanyResponse["company"]["category"]["industryGroup"].ToString();
                    string companyIndustry = combinedPersonCompanyResponse["company"]["category"]["industry"].ToString();
                    string companySubIndustry = combinedPersonCompanyResponse["company"]["category"]["subIndustry"].ToString();
                    string companySicCode = combinedPersonCompanyResponse["company"]["category"]["sicCode"].ToString();
                    string companyNaicsCode = combinedPersonCompanyResponse["company"]["category"]["naicsCode"].ToString();

                    string[] companyTags = combinedPersonCompanyResponse["company"]["tags"].ToString().Split(',').ToArray();

                    string companyDescription = combinedPersonCompanyResponse["company"]["description"].ToString();
                    string companyFoundedYear = combinedPersonCompanyResponse["company"]["foundedYear"].ToString();
                    string companyLocation = combinedPersonCompanyResponse["company"]["location"].ToString();
                    string companyTimeZone = combinedPersonCompanyResponse["company"]["timeZone"].ToString();
                    string companyUtcOffset = combinedPersonCompanyResponse["company"]["utcOffset"].ToString();

                    string companyStreetNumber = combinedPersonCompanyResponse["company"]["geo"]["streetNumber"].ToString();
                    string companyStreetName = combinedPersonCompanyResponse["company"]["geo"]["streetName"].ToString();
                    string companysubPremiseS = combinedPersonCompanyResponse["company"]["geo"]["subPremise"].ToString();
                    string companyCity = combinedPersonCompanyResponse["company"]["geo"]["city"].ToString();
                    string companyPostalCode = combinedPersonCompanyResponse["company"]["geo"]["postalCode"].ToString();
                    string companyState = combinedPersonCompanyResponse["company"]["geo"]["state"].ToString();
                    string companyStateCode = combinedPersonCompanyResponse["company"]["geo"]["stateCode"].ToString();
                    string companyCountry = combinedPersonCompanyResponse["company"]["geo"]["country"].ToString();
                    string companyCountryCode = combinedPersonCompanyResponse["company"]["geo"]["countryCode"].ToString();
                    string companyLat = combinedPersonCompanyResponse["company"]["geo"]["lat"].ToString();
                    string companyLng = combinedPersonCompanyResponse["company"]["geo"]["lng"].ToString();

                    string companyLogo = combinedPersonCompanyResponse["company"]["logo"].ToString();

                    string companyFacebookHandle = combinedPersonCompanyResponse["company"]["facebook"]["handle"].ToString();
                    string companyFacebookLikes = combinedPersonCompanyResponse["company"]["facebook"]["likes"].ToString();

                    string companyLinkedinHandle = combinedPersonCompanyResponse["company"]["linkedin"]["handle"].ToString();

                    string companyTwitterHandle = combinedPersonCompanyResponse["company"]["twitter"]["handle"].ToString();
                    string companyTwitterId = combinedPersonCompanyResponse["company"]["twitter"]["id"].ToString();
                    string companyTwitterBio = combinedPersonCompanyResponse["company"]["twitter"]["bio"].ToString();
                    string companyTwitterFollowers = combinedPersonCompanyResponse["company"]["twitter"]["followers"].ToString();
                    string companyTwitterFollowing = combinedPersonCompanyResponse["company"]["twitter"]["following"].ToString();
                    string companyTwitterLocation = combinedPersonCompanyResponse["company"]["twitter"]["location"].ToString();
                    string companyTwitterSite = combinedPersonCompanyResponse["company"]["twitter"]["site"].ToString();
                    string companyTwtitterAvatar = combinedPersonCompanyResponse["company"]["twitter"]["avatar"].ToString();

                    string companyCrunchbaseHandle = combinedPersonCompanyResponse["company"]["crunchbase"]["handle"].ToString();

                    string companyEmailProvider = combinedPersonCompanyResponse["company"]["emailProvider"].ToString();
                    string companyType = combinedPersonCompanyResponse["company"]["type"].ToString();
                    string companyTicker = combinedPersonCompanyResponse["company"]["ticker"].ToString();

                    string companyIdentifiers = combinedPersonCompanyResponse["company"]["identifiers"]["usEIN"].ToString();
                    string companyPhone = combinedPersonCompanyResponse["company"]["phone"].ToString();
                    string companyAlexaUsRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaUsRank"].ToString();
                    string companyAlexaGlobalRank = combinedPersonCompanyResponse["company"]["metrics"]["alexaGlobalRank"].ToString();
                    string companyEmployees = combinedPersonCompanyResponse["company"]["metrics"]["employees"].ToString();
                    string companyEmployeesRange = combinedPersonCompanyResponse["company"]["metrics"]["employeesRange"].ToString();
                    string companyMarketCap = combinedPersonCompanyResponse["company"]["metrics"]["marketCap"].ToString();
                    string companyRaised = combinedPersonCompanyResponse["company"]["metrics"]["raised"].ToString();
                    string companyAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["annualRevenue"].ToString();
                    string companyEstimatedAnnualRevenue = combinedPersonCompanyResponse["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
                    string companyFiscalYearEnd = combinedPersonCompanyResponse["company"]["metrics"]["fiscalYearEnd"].ToString();

                    string companyIndexedAt = combinedPersonCompanyResponse["company"]["indexedAt"].ToString();

                    string[] companyTech = combinedPersonCompanyResponse["company"]["tech"].ToString().Split(',').ToArray();

                    string companyParentDomain = combinedPersonCompanyResponse["company"]["parent"]["domain"].ToString();

                    #endregion

                    eliocompany = ClearbitSql.GetPersonCompanyByUserIdAndCompanyId(userId, companyId, session);

                    if (eliocompany != null)
                    {
                        if (elioPerson != null)
                        {
                            #region Elio Users Company

                            eliocompany = new ElioUsersPersonCompanies();

                            eliocompany.ClearbitCompanyId = companyId;
                            eliocompany.ElioPersonId = elioPerson.Id;
                            eliocompany.ClearbitPersonId = elioPerson.ClearbitPersonId;
                            eliocompany.UserId = userId;
                            eliocompany.Name = companyName;
                            eliocompany.Domain = companyDomain;
                            eliocompany.Sector = companySector;
                            eliocompany.IndustryGroup = companIndustryGroup;
                            eliocompany.Industry = companyIndustry;
                            eliocompany.SubIndustry = companySubIndustry;
                            eliocompany.Description = companyDescription;
                            eliocompany.FoundedYear = (!string.IsNullOrEmpty(companyFoundedYear)) ? Convert.ToInt32(companyFoundedYear) : 0;
                            eliocompany.Location = companyLocation;
                            //eliocompany.FundAmount = companyfu
                            eliocompany.EmployeesNumber = (!string.IsNullOrEmpty(companyEmployees)) ? Convert.ToInt32(companyEmployees) : 0;
                            eliocompany.EmployeesRange = companyEmployeesRange;
                            eliocompany.AnnualRevenue = (!string.IsNullOrEmpty(companyAnnualRevenue)) ? Convert.ToDecimal(companyAnnualRevenue) : 0;
                            eliocompany.AnnualRevenueRange = companyEstimatedAnnualRevenue;
                            eliocompany.FacebookHandle = companyFacebookHandle;
                            eliocompany.TwitterHandle = companyTwitterHandle;
                            eliocompany.CrunchbaseHandle = companyCrunchbaseHandle;
                            eliocompany.Logo = companyLogo;
                            eliocompany.Type = companyType;
                            eliocompany.DateInserted = (companyIndexedAt != null) ? Convert.ToDateTime(companyIndexedAt) : DateTime.Now;
                            eliocompany.LastUpdate = DateTime.Now;
                            eliocompany.IsPublic = 1;
                            eliocompany.IsActive = 1;

                            DataLoader<ElioUsersPersonCompanies> companyLoader = new DataLoader<ElioUsersPersonCompanies>(session);
                            companyLoader.Update(eliocompany);

                            #endregion
                        }
                        else
                        {
                            throw new Exception("ElioPerson did not find by id for user with email: " + emailAddress);
                        }
                    }

                    if (eliocompany != null)
                    {
                        #region Elio Company Phones

                        foreach (string phone in companySitePhoneNumbers)
                        {
                            ElioUsersPersonCompanyPhoneNumbers companyPhoneNumber = ClearbitSql.GetPersonCompanyPhoneByPhone(userId, eliocompany.ClearbitCompanyId, phone, session);
                            if (companyPhoneNumber != null)
                            {
                                companyPhoneNumber.ElioPersonCompanyId = eliocompany.Id;
                                companyPhoneNumber.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                companyPhoneNumber.UserId = userId;
                                companyPhoneNumber.PhoneNumber = phone;
                                companyPhoneNumber.Sysdate = DateTime.Now;
                                companyPhoneNumber.LastUpdate = DateTime.Now;

                                DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
                                phoneLoader.Update(companyPhoneNumber);
                            }
                            else
                            {
                                companyPhoneNumber = new ElioUsersPersonCompanyPhoneNumbers();

                                companyPhoneNumber.ElioPersonCompanyId = eliocompany.Id;
                                companyPhoneNumber.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                companyPhoneNumber.UserId = userId;
                                companyPhoneNumber.PhoneNumber = phone;
                                companyPhoneNumber.Sysdate = DateTime.Now;
                                companyPhoneNumber.LastUpdate = DateTime.Now;

                                DataLoader<ElioUsersPersonCompanyPhoneNumbers> phoneLoader = new DataLoader<ElioUsersPersonCompanyPhoneNumbers>(session);
                                phoneLoader.Insert(companyPhoneNumber);
                            }
                        }

                        #endregion
                    }
                    
                    if (eliocompany != null)
                    {
                        #region Elio Company Tags

                        foreach (string companyTag in companyTags)
                        {
                            ElioUsersPersonCompanyTags tag = ClearbitSql.GetPersonCompanyTagsByTagName(userId, eliocompany.ClearbitCompanyId, companyTag, session);

                            if (tag != null)
                            {
                                tag.ElioPersonCompanyId = eliocompany.Id;
                                tag.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                tag.UserId = userId;
                                tag.TagName = companyTag;
                                tag.Sysdate = DateTime.Now;
                                tag.LastUpdate = DateTime.Now;
                                tag.IsPublic = 1;
                                tag.IsActive = 1;

                                DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);
                                tagLoader.Update(tag);
                            }
                            else
                            {
                                tag.ElioPersonCompanyId = eliocompany.Id;
                                tag.ClearbitCompanyId = eliocompany.ClearbitCompanyId;
                                tag.UserId = userId;
                                tag.TagName = companyTag;
                                tag.Sysdate = DateTime.Now;
                                tag.LastUpdate = DateTime.Now;
                                tag.IsPublic = 1;
                                tag.IsActive = 1;

                                DataLoader<ElioUsersPersonCompanyTags> tagLoader = new DataLoader<ElioUsersPersonCompanyTags>(session);
                                tagLoader.Insert(tag);
                            }
                        }

                        #endregion
                    }
                    else
                    {
                        throw new Exception("ElioCompany did not find by id for tags");
                    }

                    success = true;
                }
                else
                {
                    success = false;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Clearbit class error", ex.Message.ToString(), ex.StackTrace.ToString());
                success = false;
            }

            return success;
        }

        public static void GetJson(string email)
        {
            var client = new RestClient("https://person.clearbit.com");

            var request = new RestRequest("v2/combined/find", Method.GET);
            request.AddParameter("email", email);
            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

            //var json = JsonConvert.SerializeObject(parameters);
            //json = JsonConvert.SerializeObject(header);


            //RestRequest request = new RestRequest("CreateInvoice", Method.POST);
            //request.AddJsonBody(parameters);

            IRestResponse response = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(response.Content);


            var dict = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            var pp = JsonConvert.DeserializeObject<dynamic>(response.Content);
            dynamic dict2 = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(response.Content);
            //dynamic dict3 = JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(response.Content);

            JToken person = dict["person"];
            JToken company = dict["company"];

            JObject googleSearch = JObject.Parse(response.Content);

            ////foreach (JProperty property in googleSearch.Properties())
            ////{
            ////    if (property.Name == "person")
            ////    {
            ////        foreach (JProperty item in googleSearch["person"])
            ////        {
            ////            if (item.Name == "id")
            ////            {
            ////                string val1 = item.Value.ToString();
                            
            ////                googleSearch = JObject.Parse(property.Value.ToString());

            ////                break;
            ////            }
            ////            else if (property.Name == "name")
            ////            {
            ////                foreach (JProperty itms in googleSearch.Properties())
            ////                {

            ////                }
            ////            }
            ////        }
            ////    }
            ////}

            #region person response

            string id = googleSearch["person"]["id"].ToString();

            string fullName = googleSearch["person"]["name"]["fullName"].ToString();
            string givenName = googleSearch["person"]["name"]["givenName"].ToString();
            string familyName = googleSearch["person"]["name"]["familyName"].ToString();
            string personEmail = googleSearch["person"]["email"].ToString();
            //string gender = googleSearch["person"]["gender"].ToString();
            
            string location = googleSearch["person"]["location"].ToString();
            string timeZone = googleSearch["person"]["timeZone"].ToString();
            string utcOffset = googleSearch["person"]["utcOffset"].ToString();

            string geo = googleSearch["person"]["geo"]["city"].ToString();
            string state = googleSearch["person"]["geo"]["state"].ToString();
            string stateCode = googleSearch["person"]["geo"]["stateCode"].ToString();
            string country = googleSearch["person"]["geo"]["country"].ToString();
            string countryCode = googleSearch["person"]["geo"]["countryCode"].ToString();
            string lat = googleSearch["person"]["geo"]["lat"].ToString();
            string lng = googleSearch["person"]["geo"]["lng"].ToString();

            string bio = googleSearch["person"]["bio"].ToString();
            string site = googleSearch["person"]["site"].ToString();
            string avatar = googleSearch["person"]["avatar"].ToString();
            string domain = googleSearch["person"]["employment"]["domain"].ToString();
            string name = googleSearch["person"]["employment"]["name"].ToString();
            string title = googleSearch["person"]["employment"]["title"].ToString();
            string role = googleSearch["person"]["employment"]["role"].ToString();
            string seniority = googleSearch["person"]["employment"]["seniority"].ToString();

            string facebookHandle = googleSearch["person"]["facebook"]["handle"].ToString();
            string githubHandle = googleSearch["person"]["github"]["handle"].ToString();
            string githubId = googleSearch["person"]["github"]["id"].ToString();
            string githubavatar = googleSearch["person"]["github"]["avatar"].ToString();
            string githubCompany = googleSearch["person"]["github"]["company"].ToString();
            //string githubBlog = googleSearch["person"]["github"]["blog"].ToString();
            string githubFollowers = googleSearch["person"]["github"]["followers"].ToString();
            string githubFollowing = googleSearch["person"]["github"]["following"].ToString();

            string twitterHandle = googleSearch["person"]["twitter"]["handle"].ToString();
            string twitterId = googleSearch["person"]["twitter"]["id"].ToString();
            string twitterBio = googleSearch["person"]["twitter"]["bio"].ToString();            
            string twitterFollowers = googleSearch["person"]["twitter"]["followers"].ToString();
            string twitterFollowing = googleSearch["person"]["twitter"]["following"].ToString();
            string twitterStatuses = googleSearch["person"]["twitter"]["statuses"].ToString();
            string twitterFavorites = googleSearch["person"]["twitter"]["favorites"].ToString();
            string twitterLocation = googleSearch["person"]["twitter"]["location"].ToString();
            string twitterSite = googleSearch["person"]["twitter"]["site"].ToString();
            string twitterAvatar = googleSearch["person"]["twitter"]["avatar"].ToString();

            string linkedinHandle = googleSearch["person"]["linkedin"]["handle"].ToString();

            string linkedinGoogleplusHandle = googleSearch["person"]["googleplus"]["handle"].ToString();

            //string linkedinAboutmeHandle = googleSearch["person"]["aboutme"]["handle"].ToString();
            //string linkedinAboutmeBio = googleSearch["person"]["aboutme"]["bio"].ToString();
            //string linkedinAboutmeAvatar = googleSearch["person"]["aboutme"]["avatar"].ToString();

            string gravatarHandle = googleSearch["person"]["gravatar"]["handle"].ToString();
            string gravatarUrlsValue = googleSearch["person"]["gravatar"]["urls"][0]["value"].ToString();
            string gravatarUrlsTitle = googleSearch["person"]["gravatar"]["urls"][0]["title"].ToString();

            string personAvatar = googleSearch["person"]["avatar"].ToString();
            string gravatarAvatarsUrlsValue = googleSearch["person"]["gravatar"]["avatars"][0]["url"].ToString();
            string gravatarAvatarsUrlsType = googleSearch["person"]["gravatar"]["avatars"][0]["type"].ToString();

            string fuzzy = googleSearch["person"]["fuzzy"].ToString();
            string emailProvider = googleSearch["person"]["emailProvider"].ToString();
            string indexedAt = googleSearch["person"]["indexedAt"].ToString();

            #endregion

            #region company response

            string companyId = googleSearch["company"]["id"].ToString();

            string companyName = googleSearch["company"]["name"].ToString();
            string companyLegalName = googleSearch["company"]["legalName"].ToString();
            string companyDomain = googleSearch["company"]["domain"].ToString();
            string[] companyDomainAliases = googleSearch["company"]["domainAliases"].ToString().Split(',').ToArray();
            string[] companySitePhoneNumbers = googleSearch["company"]["site"]["phoneNumbers"].ToString().Split(',').ToArray();            
            string[] companyEmailAddresses = googleSearch["company"]["site"]["emailAddresses"].ToString().Split(',').ToArray();
            
            string companySector = googleSearch["company"]["category"]["sector"].ToString();
            string companIndustryGroup = googleSearch["company"]["category"]["industryGroup"].ToString();
            string companyIndustry = googleSearch["company"]["category"]["industry"].ToString();
            string companySubIndustry = googleSearch["company"]["category"]["subIndustry"].ToString();
            string companySicCode = googleSearch["company"]["category"]["sicCode"].ToString();
            string companyNaicsCode = googleSearch["company"]["category"]["naicsCode"].ToString();

            string[] companyTags = googleSearch["company"]["tags"].ToString().Split(',').ToArray();

            string companyDescription = googleSearch["company"]["description"].ToString();
            string companyFoundedYear = googleSearch["company"]["foundedYear"].ToString();
            string companyLocation = googleSearch["company"]["location"].ToString();
            string companyTimeZone = googleSearch["company"]["timeZone"].ToString();
            string companyUtcOffset = googleSearch["company"]["utcOffset"].ToString();

            string companyStreetNumber = googleSearch["company"]["geo"]["streetNumber"].ToString();
            string companyStreetName = googleSearch["company"]["geo"]["streetName"].ToString();
            string companysubPremiseS = googleSearch["company"]["geo"]["subPremise"].ToString();
            string companyCity = googleSearch["company"]["geo"]["city"].ToString();
            string companyPostalCode = googleSearch["company"]["geo"]["postalCode"].ToString();
            string companyState = googleSearch["company"]["geo"]["state"].ToString();
            string companyStateCode = googleSearch["company"]["geo"]["stateCode"].ToString();
            string companyCountry = googleSearch["company"]["geo"]["country"].ToString();
            string companyCountryCode = googleSearch["company"]["geo"]["countryCode"].ToString();
            string companyLat = googleSearch["company"]["geo"]["lat"].ToString();
            string companyLng = googleSearch["company"]["geo"]["lng"].ToString();

            string companyLogo = googleSearch["company"]["logo"].ToString();

            string companyFacebookHandle = googleSearch["company"]["facebook"]["handle"].ToString();
            string companyFacebookLikes = googleSearch["company"]["facebook"]["likes"].ToString();

            string companyLinkedinHandle = googleSearch["company"]["linkedin"]["handle"].ToString();

            string companyTwitterHandle = googleSearch["company"]["twitter"]["handle"].ToString();
            string companyTwitterId = googleSearch["company"]["twitter"]["id"].ToString();
            string companyTwitterBio = googleSearch["company"]["twitter"]["bio"].ToString();
            string companyTwitterFollowers = googleSearch["company"]["twitter"]["followers"].ToString();
            string companyTwitterFollowing = googleSearch["company"]["twitter"]["following"].ToString();
            string companyTwitterLocation = googleSearch["company"]["twitter"]["location"].ToString();
            string companyTwitterSite = googleSearch["company"]["twitter"]["site"].ToString();
            string companyTwtitterAvatar = googleSearch["company"]["twitter"]["avatar"].ToString();

            string companyCrunchbaseHandle = googleSearch["company"]["crunchbase"]["handle"].ToString();

            string companyEmailProvider = googleSearch["company"]["emailProvider"].ToString();
            string companyType = googleSearch["company"]["type"].ToString();
            string companyTicker = googleSearch["company"]["ticker"].ToString();

            string companyIdentifiers = googleSearch["company"]["identifiers"]["usEIN"].ToString();
            string companyPhone = googleSearch["company"]["phone"].ToString();
            string companyAlexaUsRank = googleSearch["company"]["metrics"]["alexaUsRank"].ToString();
            string companyAlexaGlobalRank = googleSearch["company"]["metrics"]["alexaGlobalRank"].ToString();
            string companyEmployees = googleSearch["company"]["metrics"]["employees"].ToString();
            string companyEmployeesRange = googleSearch["company"]["metrics"]["employeesRange"].ToString();
            string companyMarketCap = googleSearch["company"]["metrics"]["marketCap"].ToString();
            string companyRaised = googleSearch["company"]["metrics"]["raised"].ToString();
            string companyAnnualRevenue = googleSearch["company"]["metrics"]["annualRevenue"].ToString();
            string companyEstimatedAnnualRevenue = googleSearch["company"]["metrics"]["estimatedAnnualRevenue"].ToString();
            string companyFiscalYearEnd = googleSearch["company"]["metrics"]["fiscalYearEnd"].ToString();

            string companyIndexedAt = googleSearch["company"]["indexedAt"].ToString();

            string[] companyTech = googleSearch["company"]["tech"].ToString().Split(',').ToArray();

            string companyParentDomain = googleSearch["company"]["parent"]["domain"].ToString();

            #endregion

            IList<JToken> results = googleSearch["person"].Children().ToList();
                        
            ////foreach (JToken result in results)
            ////{
            ////    if (i == 0)
            ////    {
            ////        //foreach (JProperty child in result)
            ////        //{
            ////        //    string id10 = child["id"].ToString();
            ////        //}
                    
            ////    }
            ////    else if (i == 1)
            ////    {
            ////        foreach (JProperty child in result["name"])
            ////        {
            ////            string fullName = child["fullName"].ToString();
            ////            string givenName = child["givenName"].ToString();
            ////            string familyName = child["familyName"].ToString();
            ////        }

            ////        string fullName1 = result["name"]["fullName"].ToString();
            ////        string givenName1 = result["name"]["givenName"].ToString();
            ////        string familyName1 = result["name"]["familyName"].ToString();
            ////    }

            ////    i++;
            ////}

            foreach (JProperty child in googleSearch["person"])
            {

            }

            string json = @"[
                          {
                            'Title': 'Json.NET is awesome!',
                            'Author': {
                              'Name': 'James Newton-King',
                              'Twitter': '@JamesNK',
                              'Picture': '/jamesnk.png'
                            },
                            'Date': '2013-01-23T19:30:00',
                            'BodyHtml': '&lt;h3&gt;Title!&lt;/h3&gt;\r\n&lt;p&gt;Content!&lt;/p&gt;'
                          }
                        ]";

            JArray blogPostArray = JArray.Parse(json);

            //IList<BlogPost> blogPosts = blogPostArray.Select(p => new BlogPost
            //{
            //    Title = (string)p["Title"],
            //    AuthorName = (string)p["Author"]["Name"],
            //    AuthorTwitter = (string)p["Author"]["Twitter"],
            //    PostedDate = (DateTime)p["Date"],
            //    Body = HttpUtility.HtmlDecode((string)p["BodyHtml"])
            //}).ToList();

            //string body = blogPosts[0].Body;

            string json2 = @"
            {
                ""car"": {
                    ""type"": [{
                        ""sedan"": {
                            ""make"": ""honda"",
                            ""model"": ""civic""
                        }
                    },
                    {
                        ""coupe"": {
                            ""make"": ""ford"",
                            ""model"": ""escort""
                        }
                    }]
                }
            }";

            JObject obj = JObject.Parse(json2);
            JToken token = obj["car"]["type"][0]["sedan"]["make"];
            string output = token.Path + " -> " + token.ToString();
        }

        public static void FindByEmail2(string email)
        {
            var client = new RestClient("https://person.clearbit.com");

            var request = new RestRequest("v2/combined/find", Method.GET);
            request.AddParameter("email", email);
            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

            //var json = JsonConvert.SerializeObject(parameters);
            //json = JsonConvert.SerializeObject(header);

           
            //RestRequest request = new RestRequest("CreateInvoice", Method.POST);
            //request.AddJsonBody(parameters);

            IRestResponse response = client.Execute(request);
             
            dynamic content = JsonConvert.DeserializeObject(response.Content);

            
            var dict = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            var pp = JsonConvert.DeserializeObject<dynamic>(response.Content);

            JToken person = dict["person"];
            JToken company = dict["company"];
            //JArray ar2 = JArray.Parse(response.Content);
            foreach (var item in person)
            {
                IEnumerable<JToken> itms = item.Values<JToken>().ToList();
               
                string jsn = item.ToString();
                JObject jo = JObject.Parse(jsn);

                if (item.Path == "name")
                {
                    name n = JsonConvert.DeserializeObject<name>(jsn);
                    name n2 = JsonConvert.DeserializeObject<name>(jo["name"].ToString());

                    foreach (var i in itms)
                    {
                        //string s = (string)i;
                    }
                }
            }


            //JToken results = JsonConvert.DeserializeObject < Dictionary<string, JToken>(response.Content);
            JObject googleSearch = JObject.Parse(response.Content);

            JArray a = (JArray)dict["person"];
            IList<person> prsn = a.ToObject<IList<person>>();

            // get JSON result objects into a  
            IList<JToken> results = googleSearch["person"].Children().ToList();

            person pers = JsonConvert.DeserializeObject<person>(response.Content);

            person searchResult = new person();

            foreach (JToken result in results)
            {
                //string id = (string)result["id"];

                //searchResult = result.ToObject<person>();                
            }

            //foreach (JProperty child in googleSearch["person"])
            //{
            //    play = child["name"]["fullName"].ToString();

            //    string latlong = Convert.ToString(play);
               
            //}

            foreach (var child in googleSearch["person"]["name"])
            {
                string full = child.First.ToString();
                string givenName = child.Next.ToString();
                string familyName = child.Last.ToString();
            }

            foreach (var tokn in googleSearch.SelectTokens("person.name[*].*.fullName"))
            {
                string latlong = Convert.ToString(tokn);
            }


            

            foreach (JToken item in dict["person"])
            {
                IList<JToken> jt = item.ToList();
            }

            //string id = (string)dyn["id"];
            string jsonT = person.ToString();
            var obj = (JObject)JsonConvert.DeserializeObject(jsonT);
            Type type = typeof(string);
            var i1 = System.Convert.ChangeType(obj["id"].ToString(), type);
            //var i2 = JsonConvert.DeserializeObject(obj["email"].ToString(), type);
            Type list = typeof(List<string>);
            //var i3 = JsonConvert.DeserializeObject(obj["name"].ToString(), type);
            //string[] i3 = JsonConvert.DeserializeObject<JArray>(obj["name"], type).ToArray();

            Dictionary<string, string> dictPerson = ((IEnumerable<KeyValuePair<string, JToken>>)person).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());

            foreach (KeyValuePair<string, string> entry in dictPerson)
            {
                if (entry.Key == "id")
                {
                    string value = entry.Value;
                }

                if (entry.Key == "name")
                {
                    List<string> names = new List<string>();
                    //string json = entry.Value;

                    var jname = JsonConvert.DeserializeObject<Dictionary<string, JArray>>(entry.ToString());
                    JToken name = jname["name"];

                    Dictionary<string, string> ddict = ((IEnumerable<KeyValuePair<string, JToken>>)name).ToDictionary(kvp => kvp.Key, kvp => kvp.Value.ToString());
                    foreach (KeyValuePair<string, string> nam in ddict)
                    {
                        if (nam.Key == "fullName")
                        {
                            string fullName = nam.Value;
                        }
                        else if (nam.Key == "givenName")
                        {
                            string givenName = nam.Value;
                        }
                        else if (nam.Key == "familyName")
                        {
                            string familyName = nam.Value;
                        }
                    }

                    
                    //Dictionary<string, List<string>> c = JsonConvert.DeserializeObject < Dictionary<string, List<string>>>(entry.Key);

                    //foreach (KeyValuePair<string, List<string>> n in c)
                    //{
                    //    names.Add(n.Value.ToString());
                    //}
                }
            }

            person p = JsonConvert.DeserializeObject<person>(response.Content);

            //var client = new RestClient("https://person.clearbit.com");

            //var request = new RestRequest("v2/combined/find", Method.GET);

            //request.AddParameter("email", email); // adds to POST or URL querystring based on Method
            //request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

            //IRestResponse response = client.Execute(request);
            ////var content = response.Content; // raw content as string

            //dynamic content = JsonConvert.DeserializeObject<dynamic>(response.Content);

            ////Content[] p = JsonConvert.DeserializeObject<Content[]>(response.Content);

            //IRestResponse<person> response2 = client.Execute<person>(request);

            try
            {
                if (response != null)
                {
                    foreach (KeyValuePair<string, JToken> entry in dict)
                    {
                        string key = entry.Key;
                        dynamic values = entry.Value;
                        
                        if (key == "person")
                        {
                            foreach (var item in dict.Values)
                            {
                                JToken val = item;                                
                            }
                        }
                    }

                    Dictionary<string, JToken>.ValueCollection valueColl = dict.Values;

                    foreach (dynamic s in valueColl)
                    {
                        dynamic value = s;
                    }

                    Dictionary<string, JToken>.KeyCollection keyColl = dict.Keys;

                    foreach (string s in keyColl)
                    {
                        string key = s;
                    }

                    for (int index = 0; index < dict.Count; index++)
                    {
                        var item = dict.ElementAt(index);
                        var itemKey = item.Key;
                        var itemValue = item.Value;
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return null;
        }

        public static void FindByEmail(string email)
        {
            //int id = 0;
            //string type = "";
            //string body = "";
            //int status = 0;
            //string error = "";

            var client = new RestClient("https://person.clearbit.com");

            var request = new RestRequest("v2/combined/find", Method.GET);

            request.AddParameter("email", email); // adds to POST or URL querystring based on Method
            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

            IRestResponse response = client.Execute(request);
            //var content = response.Content; // raw content as string

            dynamic content = JsonConvert.DeserializeObject<dynamic>(response.Content);

            //Content[] p = JsonConvert.DeserializeObject<Content[]>(response.Content);

            IRestResponse<person> response2 = client.Execute<person>(request);
            
            try
            { 
                if (response != null)
                {
                    foreach (var item in response.Content)
                    {
                        
                    }
                    //string id = (string)response.Content[0].personid;
                }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //return null;
        }

        public static person FindByDomain(string companyDomain)
        {
            //int id = 0;
            //string type = "";
            //string body = "";
            //int status = 0;
            //string error = "";

            var client = new RestClient("https://person.clearbit.com");

            var request = new RestRequest("v2/companies/find", Method.GET);
            request.AddParameter("domain", companyDomain); // adds to POST or URL querystring based on Method
            //request.AddUrlSegment("id", "123"); // replaces matching token in request.Resource

            // easily add HTTP Headers
            //request.AddHeader("header", "value");

            // execute the request
            //RestResponse response1 = client.Execute(request);

            try
            {
                IRestResponse response = client.Execute(request);
                var content = response.Content; // raw content as string

                // or automatically deserialize result
                // return content type is sniffed but can be explicitly set via RestClient.AddHandler();
                IRestResponse<person> response2 = client.Execute<person>(request);
                //var name = response2.Data.FirstName;

               // var result = JsonConvert.DeserializeObject<List<Person>>(request.ToString());
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return null;
        }

        public static void Something()
        {
            //GET
            var client = new RestClient("192.168.0.1");
            var request = new RestRequest("api/item/", Method.GET);
            var queryResult = client.Execute<List<person>>(request).Data;

            client.Execute(request);

            //POST
            var client0 = new RestClient("http://192.168.0.1");
            var request0 = new RestRequest("api/item/", Method.POST);
            request0.RequestFormat = DataFormat.Json;
            //request.AddBody(new Person
            //{
            //    FirstName = "",
            //    LastName = "19.99"
            //});
            client0.Execute(request);


            //DELETE
            var item = new person();
            var client1 = new RestClient("http://192.168.0.1");
            var request1 = new RestRequest("api/item/{id}", Method.DELETE);
            request1.AddParameter("id", "idItem");


            IRestResponse response = client.Execute<person>(request);
        }
    }
}