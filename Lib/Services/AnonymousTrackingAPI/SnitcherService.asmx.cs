using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web.Services;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.SalesforceDC;

namespace WdS.ElioPlus.Lib.Services.AnonymousTrackingAPI
{
    /// <summary>
    /// Summary description for SnitcherService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class SnitcherService : System.Web.Services.WebService
    {
        [WebMethod]
        public static List<ElioSnitcherWebsites> GetWebsitesList(int page, DBSession session)
        {
            List<ElioSnitcherWebsites> websitesData = null;

            try
            {
                #region Request

                var client = new RestClient("https://app.snitcher.com");
                var request = new RestRequest("api/v2/websites", Method.GET);

                request.AddHeader("Accept", "Application/Json");
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["SnitcherKeyToken"].ToString());

                if (page > 1)
                    request.AddParameter("page", page);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                #endregion

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    #region Bad Response

                    string error = "";
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for websites";
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for websites";
                    }
                    else
                    {
                        error = "Something went wrong for websites list";
                    }

                    throw new Exception(error);

                    #endregion
                }
                else
                {
                    #region OK Response

                    var responseContentDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                    if (responseContentDictionary != null)
                    {
                        #region response

                        JToken data = responseContentDictionary["data"];
                        if (data.HasValues)
                        {
                            websitesData = new List<ElioSnitcherWebsites>();

                            foreach (JObject item in data)
                            {
                                DataLoader<ElioSnitcherWebsites> loader = new DataLoader<ElioSnitcherWebsites>(session);

                                string id = item["id"].ToString();
                                string url = item["url"].ToString();
                                string trackingScriptId = item["tracking_script_id"].ToString();
                                string trackingScriptSnippet = item["tracking_script_snippet"].ToString();

                                ElioSnitcherWebsites website = Sql.GetSnitcherWebsite(id, session);
                                if (website == null)
                                {
                                    website = new ElioSnitcherWebsites();

                                    website.WebsiteId = id;
                                    website.Url = url;
                                    website.TrackingScriptId = trackingScriptId;
                                    website.TrackingScriptSnippet = trackingScriptSnippet;
                                    website.Sysdate = DateTime.Now;
                                    website.LastUpdate = DateTime.Now;

                                    loader.Insert(website);
                                }
                                else
                                {
                                    website.Url = url;
                                    website.TrackingScriptId = trackingScriptId;
                                    website.TrackingScriptSnippet = trackingScriptSnippet;
                                    website.LastUpdate = DateTime.Now;

                                    loader.Update(website);
                                }

                                if (website != null)
                                {
                                    websitesData.Add(website);
                                }
                            }

                            if (page <= 1)
                            {
                                #region Delete

                                //string currentPage = responseContentDictionary["current_page"].ToString();
                                //string firstPageUrl = responseContentDictionary["first_page_url"].ToString();
                                //string from = responseContentDictionary["from"].ToString();

                                //string lastPageUrl = responseContentDictionary["last_page_url"].ToString();
                                //string nextPageUrl = responseContentDictionary["next_page_url"].ToString();
                                //string path = responseContentDictionary["path"].ToString();
                                //string perPage = responseContentDictionary["per_page"].ToString();
                                //string prevPageUrl = responseContentDictionary["prev_page_url"].ToString();
                                //string to = responseContentDictionary["to"].ToString();
                                //string total = responseContentDictionary["total"].ToString();

                                //websitesData.CurrentPage = Convert.ToInt32(currentPage);
                                //website.FirstPageUrl = firstPageUrl;
                                //websitesData.From = Convert.ToInt32(from);
                                //websitesData.LastPage = Convert.ToInt32(lastPage);
                                //websitesData.LastPageUrl = lastPageUrl;
                                //websitesData.NextPageUrl = nextPageUrl;
                                //website.Path = path;
                                //websitesData.PerPage = Convert.ToInt32(perPage);
                                //websitesData.PrevPageUrl = prevPageUrl;
                                //websitesData.To = Convert.ToInt32(to);
                                //website.Total = Convert.ToInt32(total);

                                #endregion

                                string lastPage = responseContentDictionary["last_page"].ToString();

                                if (lastPage != "")
                                {
                                    int pagesCount = Convert.ToInt32(lastPage);
                                    if (pagesCount > 1)
                                    {
                                        for (int i = 2; i <= pagesCount; i++)
                                        {
                                            GetWebsitesList(i, session);
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion
                }

                return websitesData;
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Snitcher Anonymous Tracking class error at, " + DateTime.Now.ToString() + ", for list of websites", ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static List<ElioSnitcherWebsiteLeads> GetWebsiteLeads(ElioSnitcherWebsites website, int page, string date, DBSession session)
        {
            List<ElioSnitcherWebsiteLeads> leads = null;

            try
            {
                #region Request

                var client = new RestClient("https://app.snitcher.com");
                var request = new RestRequest("api/v2/websites/" + website.WebsiteId + "?paginate=100", Method.GET);
                //request.AddParameter("{website_ID}", websiteId);

                if (page > 1)
                    request.AddParameter("page", page);

                if (date != "")
                    request.AddParameter("date", date);

                request.AddHeader("Accept", "Application/Json");
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["SnitcherKeyToken"].ToString());

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                #endregion

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    #region Bad Response

                    string error = "";
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for website ID:" + website.WebsiteId;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for website ID:" + website.WebsiteId;
                    }
                    else
                    {
                        error = "Something went wrong for website ID:" + website.WebsiteId;
                    }

                    throw new Exception(error);

                    #endregion
                }
                else
                {
                    #region OK Response

                    var responseContentDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                    if (responseContentDictionary != null)
                    {
                        #region response

                        JToken data = responseContentDictionary["data"];

                        if (data != null && data.HasValues)
                        {
                            leads = new List<ElioSnitcherWebsiteLeads>();

                            foreach (JObject item in data)
                            {
                                string leadId = "";

                                try
                                {
                                    leadId = item["lead_id"].ToString();

                                    string sessionReferrer = item["session_referrer"].ToString();
                                    string sessionOperating_system = item["session_operating_system"].ToString();
                                    string sessionBrowser = item["session_browser"].ToString();
                                    string sessionDeviceType = item["session_device_type"].ToString();
                                    string sessionCampaign = item["session_campaign"].ToString();
                                    string sessionStart = item["session_start"].ToString();
                                    string sessionDuration = item["session_duration"].ToString();
                                    string sessionTotalPageviews = item["session_total_pageviews"].ToString();

                                    string leadLastSeen = item["lead_last_seen"].ToString();
                                    string leadCompanyName = item["lead_company_name"].ToString();
                                    string leadCompanyLogo = item["lead_company_logo"].ToString();
                                    string leadCompanyWebsite = item["lead_company_website"].ToString();
                                    string leadCompanyAddress = item["lead_company_address"].ToString();
                                    string leadCompanyFounded = item["lead_company_founded"].ToString();
                                    string leadCompanySize = item["lead_company_size"].ToString();
                                    string leadCompanyIndustry = item["lead_company_industry"].ToString();
                                    string leadCompanyPhone = item["lead_company_phone"].ToString();
                                    string leadCompanyEmail = item["lead_company_email"].ToString();
                                    string leadCompanyContacts = item["lead_company_revealed_contacts"].ToString();
                                    string leadLinkedin = item["lead_linkedin_handle"].ToString();
                                    string leadFacebook = item["lead_facebook_handle"].ToString();
                                    string leadYoutube = item["lead_youtube_handle"].ToString();
                                    string leadInstagram = item["lead_instagram_handle"].ToString();
                                    string leadTwitter = item["lead_twitter_handle"].ToString();
                                    string leadPinterest = item["lead_pinterest_handle"].ToString();
                                    string leadAngellist = item["lead_angellist_handle"].ToString();

                                    DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                                    ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadByWebsiteIdLeadId(website.WebsiteId, leadId, session);

                                    if (lead == null)
                                    {
                                        lead = new ElioSnitcherWebsiteLeads();

                                        lead.WebsiteId = website.WebsiteId;
                                        lead.ElioSnitcherWebsiteId = website.Id;

                                        lead.SessionReferrer = sessionReferrer;
                                        lead.SessionOperatingSystem = sessionOperating_system;
                                        lead.SessionBrowser = sessionBrowser;
                                        lead.SessionDeviceType = sessionDeviceType;
                                        lead.SessionCampaign = sessionCampaign;

                                        if (sessionStart != "")
                                            lead.SessionStart = Convert.ToDateTime(sessionStart);

                                        lead.SessionDuration = (sessionDuration != "") ? Convert.ToInt32(sessionDuration) : 0;
                                        lead.SessionTotalPageviews = (sessionTotalPageviews != "") ? Convert.ToInt32(sessionTotalPageviews) : 0;
                                        lead.LeadId = leadId;

                                        if (leadLastSeen != "")
                                            lead.LeadLastSeen = Convert.ToDateTime(leadLastSeen);

                                        lead.LeadFirstName = "";
                                        lead.LeadLastName = "";

                                        if (leadCompanyName.Length > 150)
                                            leadCompanyName = leadCompanyName.Substring(0, 149);

                                        lead.LeadCompanyName = leadCompanyName;

                                        if (leadCompanyLogo.Length > 250)
                                            leadCompanyLogo = leadCompanyLogo.Substring(0, 249);

                                        lead.LeadCompanyLogo = leadCompanyLogo;
                                        lead.LeadCompanyWebsite = leadCompanyWebsite;

                                        try
                                        {
                                            string[] address = leadCompanyAddress.Split(',').ToArray();
                                            if (address.Length > 0)
                                            {
                                                string leadCountry = address[address.Length - 1].TrimStart(' ').TrimEnd(' ').Trim();
                                                if (leadCountry.Length > 0)
                                                {
                                                    if (leadCountry.Length > 150)
                                                        leadCountry = leadCountry.Substring(0, 149);

                                                    lead.LeadCountry = leadCountry.Trim();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead country from address error", ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        lead.LeadCity = "";

                                        if (leadCompanyAddress.Length > 350)
                                            leadCompanyAddress = leadCompanyAddress.Substring(0, 349);

                                        lead.LeadCompanyAddress = leadCompanyAddress;
                                        lead.LeadCompanyFounded = leadCompanyFounded;
                                        lead.LeadCompanySize = leadCompanySize;
                                        lead.LeadCompanyIndustry = leadCompanyIndustry;
                                        lead.LeadCompanyPhone = leadCompanyPhone;
                                        lead.LeadCompanyEmail = leadCompanyEmail;
                                        lead.LeadCompanyContacts = leadCompanyContacts;
                                        lead.LeadLinkedinHandle = leadLinkedin;
                                        lead.LeadFacebookHandle = leadFacebook;
                                        lead.LeadYoutubeHandle = leadYoutube;
                                        lead.LeadInstagramHandle = leadInstagram;
                                        lead.LeadTwitterHandle = leadTwitter;
                                        lead.LeadPinterestHandle = leadPinterest;
                                        lead.LeadAngellistHandle = leadAngellist;
                                        lead.IsApiLead = (int)ApiLeadCategory.isSnitcherLead;
                                        lead.IsPublic = 1;
                                        lead.IsApproved = 1;
                                        lead.Message = "";
                                        lead.IsConfirmed = 0;

                                        lead.Sysdate = DateTime.Now;
                                        lead.LastUpdate = DateTime.Now;

                                        loader.Insert(lead);
                                    }
                                    else
                                    {
                                        lead.ElioSnitcherWebsiteId = website.Id;

                                        lead.SessionReferrer = sessionReferrer;
                                        lead.SessionOperatingSystem = sessionOperating_system;
                                        lead.SessionBrowser = sessionBrowser;
                                        lead.SessionDeviceType = sessionDeviceType;
                                        lead.SessionCampaign = sessionCampaign;

                                        if (sessionStart != "")
                                            lead.SessionStart = Convert.ToDateTime(sessionStart);

                                        lead.SessionDuration = (sessionDuration != "") ? Convert.ToInt32(sessionDuration) : 0;
                                        lead.SessionTotalPageviews = (sessionTotalPageviews != "") ? Convert.ToInt32(sessionTotalPageviews) : 0;
                                        lead.LeadId = leadId;

                                        if (leadLastSeen != "")
                                            lead.LeadLastSeen = Convert.ToDateTime(leadLastSeen);

                                        if (leadCompanyName.Length > 150)
                                            leadCompanyName = leadCompanyName.Substring(0, 149);

                                        lead.LeadCompanyName = leadCompanyName;

                                        if (leadCompanyLogo.Length > 250)
                                            leadCompanyLogo = leadCompanyLogo.Substring(0, 249);

                                        lead.LeadCompanyLogo = leadCompanyLogo;
                                        lead.LeadCompanyWebsite = leadCompanyWebsite;

                                        try
                                        {
                                            string[] address = leadCompanyAddress.Split(',').ToArray();
                                            if (address.Length > 0)
                                            {
                                                string leadCountry = address[address.Length - 1].TrimStart(' ').TrimEnd(' ').Trim();
                                                if (leadCountry.Length > 0)
                                                {
                                                    if (leadCountry.Length > 150)
                                                        leadCountry = leadCountry.Substring(0, 149);

                                                    lead.LeadCountry = leadCountry.Trim();
                                                }
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead country from address error", ex.Message.ToString(), ex.StackTrace.ToString());
                                        }

                                        if (leadCompanyAddress.Length > 350)
                                            leadCompanyAddress = leadCompanyAddress.Substring(0, 349);

                                        lead.LeadCompanyAddress = leadCompanyAddress;
                                        lead.LeadCompanyFounded = leadCompanyFounded;
                                        lead.LeadCompanySize = leadCompanySize;
                                        lead.LeadCompanyIndustry = leadCompanyIndustry;
                                        lead.LeadCompanyPhone = leadCompanyPhone;
                                        lead.LeadCompanyEmail = leadCompanyEmail;
                                        lead.LeadCompanyContacts = leadCompanyContacts;
                                        lead.LeadLinkedinHandle = leadLinkedin;
                                        lead.LeadFacebookHandle = leadFacebook;
                                        lead.LeadYoutubeHandle = leadYoutube;
                                        lead.LeadInstagramHandle = leadInstagram;
                                        lead.LeadTwitterHandle = leadTwitter;
                                        lead.LeadPinterestHandle = leadPinterest;
                                        lead.LeadAngellistHandle = leadAngellist;
                                        lead.IsConfirmed = 0;

                                        lead.LastUpdate = DateTime.Now;

                                        loader.Update(lead);
                                    }

                                    if (lead != null)
                                    {
                                        for (int i = 0; i < lead.SessionTotalPageviews; i++)
                                        {
                                            string url = item["session_pageviews"][i]["url"].ToString();
                                            string timeSpent = item["session_pageviews"][i]["time_spent"].ToString();
                                            string actionTime = item["session_pageviews"][i]["action_time"].ToString();

                                            string product = "";

                                            try
                                            {
                                                string[] parameters = url.Split('?');
                                                if (parameters.Length > 0)
                                                {
                                                    string[] path = parameters[0].Split('/').ToArray();
                                                    if (path.Length > 0)
                                                    {
                                                        if (path.Length > 3 && path[path.Length - 1] != "")
                                                        {
                                                            product = path[path.Length - 1].Trim();
                                                        }
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead page view product from url error", ex.Message.ToString(), ex.StackTrace.ToString());
                                            }

                                            if (product == "")
                                                continue;

                                            DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                            ElioSnitcherLeadsPageviews leadView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, url, product, session);

                                            if (leadView == null)
                                            {
                                                leadView = new ElioSnitcherLeadsPageviews();

                                                leadView.Url = url;
                                                leadView.Product = product.Replace("_", " ").Replace("and", "&").Trim();
                                                leadView.TimeSpent = (timeSpent != "") ? Convert.ToInt32(timeSpent) : 0;

                                                if (actionTime != "")
                                                    leadView.ActionTime = Convert.ToDateTime(actionTime);

                                                leadView.LeadId = lead.LeadId;
                                                leadView.ElioWebsiteLeadsId = lead.Id;
                                                leadView.Sysdate = DateTime.Now;
                                                leadView.LastUpdate = DateTime.Now;

                                                loaderView.Insert(leadView);
                                            }
                                            else
                                            {
                                                leadView.Product = product.Replace("_", " ").Replace("and", "&").Trim();

                                                if (actionTime != "")
                                                    leadView.ActionTime = Convert.ToDateTime(actionTime);

                                                leadView.LastUpdate = DateTime.Now;

                                                loaderView.Update(leadView);
                                            }
                                        }

                                        leads.Add(lead);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(string.Format("Error for Lead ID {0} at {1}", leadId, DateTime.Now.ToString()), ex.Message.ToString(), ex.StackTrace.ToString());
                                }
                            }

                            //if (leadIDs.EndsWith(","))
                            //    leadIDs = leadIDs.Substring(0, leadIDs.Length - 1);

                            //Logger.Debug("DEBUG MESSAGE: " + leadIDs);

                            #region Delete 

                            //string currentPage = responseContentDictionary["current_page"].ToString();
                            //string firstPageUrl = responseContentDictionary["first_page_url"].ToString();
                            //string from = responseContentDictionary["from"].ToString();
                            //string lastPage = responseContentDictionary["last_page"].ToString();
                            //string lastPageUrl = responseContentDictionary["last_page_url"].ToString();
                            //string nextPageUrl = responseContentDictionary["next_page_url"].ToString();
                            //string path = responseContentDictionary["path"].ToString();
                            //string perPage = responseContentDictionary["per_page"].ToString();
                            //string prevPageUrl = responseContentDictionary["prev_page_url"].ToString();
                            //string to = responseContentDictionary["to"].ToString();
                            //string total = responseContentDictionary["total"].ToString();

                            //SnitcherWebsite website = new SnitcherWebsite();

                            //website.WebsiteId = websiteId;
                            //website.CurrentPage = Convert.ToInt32(currentPage);
                            //website.FirstPageUrl = firstPageUrl;
                            //website.From = Convert.ToInt32(from);
                            //website.LastPage = Convert.ToInt32(lastPage);
                            //website.LastPageUrl = lastPageUrl;
                            //website.NextPageUrl = nextPageUrl;
                            //website.Path = path;
                            //website.PerPage = Convert.ToInt32(perPage);
                            //website.PrevPageUrl = prevPageUrl;
                            //website.To = Convert.ToInt32(to);
                            //website.Total = Convert.ToInt32(total);

                            #endregion

                            if (page <= 1)
                            {
                                string lastPage = responseContentDictionary["last_page"].ToString();
                                if (lastPage != "")
                                {
                                    int pagesCount = Convert.ToInt32(lastPage);
                                    if (pagesCount > 1)
                                    {
                                        List<ElioSnitcherWebsiteLeads> nextPageLeads = null;

                                        for (int i = 2; i <= pagesCount; i++)
                                        {
                                            nextPageLeads = GetWebsiteLeads(website, i, date, session);

                                            if (nextPageLeads != null && nextPageLeads.Count > 0)
                                            {
                                                foreach (ElioSnitcherWebsiteLeads newLead in nextPageLeads)
                                                {
                                                    leads.Add(newLead);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Snitcher Anonymous Tracking class error at, " + DateTime.Now.ToString() + ", for website ID: " + website.WebsiteId, ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }

            return leads;
        }

        [WebMethod]
        public static List<ElioSnitcherWebsiteLeads> GetWebsiteLeadsByNextPage(ElioSnitcherWebsites website, string nextPage, string nextPageDate, DBSession session)
        {
            List<ElioSnitcherWebsiteLeads> leads = null;

            try
            {
                #region Request

                var client = new RestClient("https://app.snitcher.com");
                var request = new RestRequest("api/v2/websites/" + website.WebsiteId + "?paginate=100", Method.GET);
                request.AddParameter("page", nextPage);

                if (nextPageDate != "")
                    request.AddParameter("date", nextPageDate);

                request.AddHeader("Accept", "Application/Json");
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["SnitcherKeyToken"].ToString());

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                #endregion

                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() != "OK")
                {
                    #region Bad Response

                    string error = "";
                    if (response.StatusCode.ToString() == "NotFound")
                    {
                        error = "No data found for website ID:" + website.WebsiteId;
                    }
                    else if (response.StatusCode.ToString() == "Accepted")
                    {
                        error = "Try again later for website ID:" + website.WebsiteId;
                    }
                    else
                    {
                        error = "Something went wrong for website ID:" + website.WebsiteId;
                    }

                    throw new Exception(error);

                    #endregion
                }
                else
                {
                    #region OK Response

                    var responseContentDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                    if (responseContentDictionary != null)
                    {
                        #region response

                        foreach (KeyValuePair<string, JToken> pair in responseContentDictionary)
                        {
                            if (pair.Key == "data")
                            {
                                JObject data = pair.Value as JObject;
                                //JToken data = responseContentDictionary["data"];
                                if (data != null && data.HasValues)
                                {
                                    leads = new List<ElioSnitcherWebsiteLeads>();
                                    
                                    //string leadIDs = "";

                                    //if (nextPageDate != "")
                                    //    leadIDs = "Leads for date: " + nextPageDate + ": ";
                                    //else
                                    //    leadIDs = "Leads for today date: " + DateTime.Now.ToString() + ": ";

                                    foreach (KeyValuePair<string, JToken> property in data)
                                    {
                                        JToken item = property.Value;

                                        string leadId = item["lead_id"].ToString();
                                        //leadIDs += leadId + ",";
                                        string sessionReferrer = item["session_referrer"].ToString();
                                        string sessionOperating_system = item["session_operating_system"].ToString();
                                        string sessionBrowser = item["session_browser"].ToString();
                                        string sessionDeviceType = item["session_device_type"].ToString();
                                        string sessionCampaign = item["session_campaign"].ToString();
                                        string sessionStart = item["session_start"].ToString();
                                        string sessionDuration = item["session_duration"].ToString();
                                        string sessionTotalPageviews = item["session_total_pageviews"].ToString();

                                        string leadLastSeen = item["lead_last_seen"].ToString();
                                        string leadCompanyName = item["lead_company_name"].ToString();
                                        string leadCompanyLogo = item["lead_company_logo"].ToString();
                                        string leadCompanyWebsite = item["lead_company_website"].ToString();
                                        string leadCompanyAddress = item["lead_company_address"].ToString();
                                        string leadCompanyFounded = item["lead_company_founded"].ToString();
                                        string leadCompanySize = item["lead_company_size"].ToString();
                                        string leadCompanyIndustry = item["lead_company_industry"].ToString();
                                        string leadCompanyPhone = item["lead_company_phone"].ToString();
                                        string leadCompanyEmail = item["lead_company_email"].ToString();
                                        string leadCompanyContacts = item["lead_company_contacts"].ToString();
                                        string leadLinkedin = item["lead_linkedin_handle"].ToString();
                                        string leadFacebook = item["lead_facebook_handle"].ToString();
                                        string leadYoutube = item["lead_youtube_handle"].ToString();
                                        string leadInstagram = item["lead_instagram_handle"].ToString();
                                        string leadTwitter = item["lead_twitter_handle"].ToString();
                                        string leadPinterest = item["lead_pinterest_handle"].ToString();
                                        string leadAngellist = item["lead_angellist_handle"].ToString();

                                        DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                                        ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadByWebsiteIdLeadId(website.WebsiteId, leadId, session);

                                        if (lead == null)
                                        {
                                            lead = new ElioSnitcherWebsiteLeads();

                                            lead.WebsiteId = website.WebsiteId;
                                            lead.ElioSnitcherWebsiteId = website.Id;

                                            lead.SessionReferrer = sessionReferrer;
                                            lead.SessionOperatingSystem = sessionOperating_system;
                                            lead.SessionBrowser = sessionBrowser;
                                            lead.SessionDeviceType = sessionDeviceType;
                                            lead.SessionCampaign = sessionCampaign;

                                            if (sessionStart != "")
                                                lead.SessionStart = Convert.ToDateTime(sessionStart);

                                            lead.SessionDuration = (sessionDuration != "") ? Convert.ToInt32(sessionDuration) : 0;
                                            lead.SessionTotalPageviews = (sessionTotalPageviews != "") ? Convert.ToInt32(sessionTotalPageviews) : 0;
                                            lead.LeadId = leadId;

                                            if (leadLastSeen != "")
                                                lead.LeadLastSeen = Convert.ToDateTime(leadLastSeen);

                                            lead.LeadFirstName = "";
                                            lead.LeadLastName = "";
                                            lead.LeadCompanyName = leadCompanyName;
                                            lead.LeadCompanyLogo = leadCompanyLogo;
                                            lead.LeadCompanyWebsite = leadCompanyWebsite;

                                            try
                                            {
                                                string[] address = leadCompanyAddress.Split(',').ToArray();
                                                if (address.Length > 0)
                                                {
                                                    string leadCountry = address[address.Length - 1].TrimStart(' ').TrimEnd(' ').Trim();
                                                    if (leadCountry.Length > 0)
                                                    {
                                                        lead.LeadCountry = leadCountry.Trim();
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead country from address error", ex.Message.ToString(), ex.StackTrace.ToString());
                                            }

                                            lead.LeadCity = "";
                                            lead.LeadCompanyAddress = leadCompanyAddress;
                                            lead.LeadCompanyFounded = leadCompanyFounded;
                                            lead.LeadCompanySize = leadCompanySize;
                                            lead.LeadCompanyIndustry = leadCompanyIndustry;
                                            lead.LeadCompanyPhone = leadCompanyPhone;
                                            lead.LeadCompanyEmail = leadCompanyEmail;
                                            lead.LeadCompanyContacts = leadCompanyContacts;
                                            lead.LeadLinkedinHandle = leadLinkedin;
                                            lead.LeadFacebookHandle = leadFacebook;
                                            lead.LeadYoutubeHandle = leadYoutube;
                                            lead.LeadInstagramHandle = leadInstagram;
                                            lead.LeadTwitterHandle = leadTwitter;
                                            lead.LeadPinterestHandle = leadPinterest;
                                            lead.LeadAngellistHandle = leadAngellist;
                                            lead.IsApiLead = (int)ApiLeadCategory.isSnitcherLead;
                                            lead.IsPublic = 1;
                                            lead.IsApproved = 1;
                                            lead.Message = "";
                                            lead.IsConfirmed = 0;

                                            lead.Sysdate = DateTime.Now;
                                            lead.LastUpdate = DateTime.Now;

                                            loader.Insert(lead);

                                            leads.Add(lead);
                                        }
                                        else
                                        {
                                            lead.ElioSnitcherWebsiteId = website.Id;

                                            lead.SessionReferrer = sessionReferrer;
                                            lead.SessionOperatingSystem = sessionOperating_system;
                                            lead.SessionBrowser = sessionBrowser;
                                            lead.SessionDeviceType = sessionDeviceType;
                                            lead.SessionCampaign = sessionCampaign;

                                            if (sessionStart != "")
                                                lead.SessionStart = Convert.ToDateTime(sessionStart);

                                            lead.SessionDuration = (sessionDuration != "") ? Convert.ToInt32(sessionDuration) : 0;
                                            lead.SessionTotalPageviews = (sessionTotalPageviews != "") ? Convert.ToInt32(sessionTotalPageviews) : 0;
                                            lead.LeadId = leadId;

                                            if (leadLastSeen != "")
                                                lead.LeadLastSeen = Convert.ToDateTime(leadLastSeen);

                                            lead.LeadCompanyName = leadCompanyName;
                                            lead.LeadCompanyLogo = leadCompanyLogo;
                                            lead.LeadCompanyWebsite = leadCompanyWebsite;

                                            try
                                            {
                                                string[] address = leadCompanyAddress.Split(',').ToArray();
                                                if (address.Length > 0)
                                                {
                                                    string leadCountry = address[address.Length - 1].TrimStart(' ').TrimEnd(' ').Trim();
                                                    if (leadCountry.Length > 0)
                                                    {
                                                        lead.LeadCountry = leadCountry.Trim();
                                                    }
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead country from address error", ex.Message.ToString(), ex.StackTrace.ToString());
                                            }

                                            lead.LeadCompanyAddress = leadCompanyAddress;
                                            lead.LeadCompanyFounded = leadCompanyFounded;
                                            lead.LeadCompanySize = leadCompanySize;
                                            lead.LeadCompanyIndustry = leadCompanyIndustry;
                                            lead.LeadCompanyPhone = leadCompanyPhone;
                                            lead.LeadCompanyEmail = leadCompanyEmail;
                                            lead.LeadCompanyContacts = leadCompanyContacts;
                                            lead.LeadLinkedinHandle = leadLinkedin;
                                            lead.LeadFacebookHandle = leadFacebook;
                                            lead.LeadYoutubeHandle = leadYoutube;
                                            lead.LeadInstagramHandle = leadInstagram;
                                            lead.LeadTwitterHandle = leadTwitter;
                                            lead.LeadPinterestHandle = leadPinterest;
                                            lead.LeadAngellistHandle = leadAngellist;
                                            lead.IsConfirmed = 0;

                                            lead.LastUpdate = DateTime.Now;

                                            loader.Update(lead);
                                        }

                                        if (lead != null)
                                        {
                                            for (int i = 0; i < lead.SessionTotalPageviews; i++)
                                            {
                                                string url = item["session_pageviews"][i]["url"].ToString();
                                                string timeSpent = item["session_pageviews"][i]["time_spent"].ToString();
                                                string actionTime = item["session_pageviews"][i]["action_time"].ToString();

                                                string product = "";

                                                try
                                                {
                                                    string[] parameters = url.Split('?');
                                                    if (parameters.Length > 0)
                                                    {
                                                        string[] path = parameters[0].Split('/').ToArray();
                                                        if (path.Length > 0)
                                                        {
                                                            if (path.Length > 3 && path[path.Length - 1] != "")
                                                            {
                                                                product = path[path.Length - 1];
                                                            }
                                                        }
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeads() --> lead page view product from url error", ex.Message.ToString(), ex.StackTrace.ToString());
                                                }

                                                if (product == "")
                                                    continue;

                                                DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                                ElioSnitcherLeadsPageviews leadView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, url, product, session);

                                                if (leadView == null)
                                                {
                                                    leadView = new ElioSnitcherLeadsPageviews();

                                                    leadView.Url = url.Trim();
                                                    leadView.Product = product.Trim();
                                                    leadView.TimeSpent = (timeSpent != "") ? Convert.ToInt32(timeSpent) : 0;

                                                    if (actionTime != "")
                                                        leadView.ActionTime = Convert.ToDateTime(actionTime);

                                                    leadView.LeadId = lead.LeadId;
                                                    leadView.ElioWebsiteLeadsId = lead.Id;
                                                    leadView.Sysdate = DateTime.Now;
                                                    leadView.LastUpdate = DateTime.Now;

                                                    loaderView.Insert(leadView);
                                                }
                                                else
                                                {
                                                    leadView.LastUpdate = DateTime.Now;

                                                    loaderView.Update(leadView);
                                                }
                                            }
                                        }
                                    }

                                    //if (leadIDs.EndsWith(","))
                                    //    leadIDs = leadIDs.Substring(0, leadIDs.Length - 1);

                                    //Logger.Debug("DEBUG MESSAGE: " + leadIDs);
                                }
                            }
                        }

                        #endregion
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Snitcher Anonymous Tracking class error at, " + DateTime.Now.ToString() + ", for website ID: " + website.WebsiteId, ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }

            return leads;
        }

        public static ElioSnitcherWebsiteLeads GetWebsiteLeadsFromInfo(ElioAnonymousCompaniesInfo companyInfo, ElioAnonymousIpInfo leadInfo, DBSession session)
        {
            ElioSnitcherWebsiteLeads lead = null;

            try
            {
                //lead = new ElioSnitcherWebsiteLeads();

                try
                {
                    DataLoader<ElioSnitcherWebsiteLeads> loaderInfo = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                    lead = Sql.GetSnitcherWebsiteLeadByWebsiteIdAndCompanyName(leadInfo.CompanyName, session);

                    if (lead == null)
                    {
                        lead = new ElioSnitcherWebsiteLeads();

                        string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                        int count = 1;
                        while (Sql.GetSnitcherLeadIDByLeadId(number, session) != "" && count < 10)
                        {
                            number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                            count++;
                        }

                        if (count >= 10)
                        {
                            throw new Exception("SnitcherService.asmx.cs --> GetWebsiteLeadsFromInfo() --> ERROR --> string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8) could not created");
                        }

                        lead.LeadId = number;

                        lead.WebsiteId = "19980";
                        lead.ElioSnitcherWebsiteId = 4;

                        lead.SessionReferrer = "https://www.elioplus.com";
                        lead.SessionOperatingSystem = "";
                        lead.SessionBrowser = "";
                        lead.SessionDeviceType = "";
                        lead.SessionCampaign = "";

                        lead.SessionStart = leadInfo.Sysdate;   // DateTime.Now;

                        lead.SessionDuration = 20;
                        lead.SessionTotalPageviews = 1;

                        //lead.LeadId = leadInfo.Id.ToString();

                        lead.LeadLastSeen = Convert.ToDateTime(lead.SessionStart).AddMilliseconds(100);

                        lead.LeadFirstName = "";
                        lead.LeadLastName = "";

                        lead.LeadCompanyName = leadInfo.CompanyName;

                        lead.LeadCompanyLogo = companyInfo.Logo;
                        lead.LeadCompanyWebsite = "";
                        lead.LeadCountry = companyInfo.CountryName;
                        lead.LeadCity = companyInfo.CityName;

                        lead.LeadCompanyAddress = "";
                        lead.LeadCompanyFounded = companyInfo.YearFounded.ToString();
                        lead.LeadCompanySize = companyInfo.TotalEmployees;
                        lead.LeadCompanyIndustry = (companyInfo.Industries.Length > 149) ? companyInfo.Industries.Substring(0, 148) : companyInfo.Industries;
                        lead.LeadCompanyPhone = companyInfo.PhoneNumber;
                        lead.LeadCompanyEmail = "";
                        lead.LeadCompanyContacts = "";
                        lead.LeadLinkedinHandle = companyInfo.Linkedin;
                        lead.LeadFacebookHandle = "";
                        lead.LeadYoutubeHandle = "";
                        lead.LeadInstagramHandle = "";
                        lead.LeadTwitterHandle = "";
                        lead.LeadPinterestHandle = "";
                        lead.LeadAngellistHandle = "";
                        lead.IsApiLead = (int)ApiLeadCategory.isSnitcherLead;
                        lead.IsPublic = 1;
                        lead.IsApproved = 1;
                        lead.Message = "";
                        lead.IsConfirmed = 0;

                        lead.Sysdate = leadInfo.Sysdate;    // DateTime.Now;
                        lead.LastUpdate = leadInfo.LastUpdated; // DateTime.Now;

                        loaderInfo.Insert(lead);

                    }
                    else
                    {
                        //lead.ElioSnitcherWebsiteId = leadInfo.Id;
                        lead.WebsiteId = "19980";
                        lead.ElioSnitcherWebsiteId = 4;

                        lead.SessionReferrer = "https://www.elioplus.com";
                        lead.SessionOperatingSystem = "";
                        lead.SessionBrowser = "";
                        lead.SessionDeviceType = "";
                        lead.SessionCampaign = "";

                        lead.SessionStart = leadInfo.Sysdate;    //DateTime.Now;
                        lead.LeadLastSeen = Convert.ToDateTime(lead.SessionStart).AddMilliseconds(100);
                        lead.LastUpdate = leadInfo.LastUpdated; //DateTime.Now;

                        loaderInfo.Update(lead);
                    }

                    if (lead != null)
                    {
                        string product = "";

                        try
                        {
                            string[] parameters = leadInfo.Path.Split('?');
                            if (parameters.Length > 0)
                            {
                                string[] pathInfo = parameters[0].Split('/').ToArray();
                                if (pathInfo.Length > 0)
                                {
                                    if (pathInfo.Length > 2 && pathInfo[pathInfo.Length - 1] != "")
                                    {
                                        product = pathInfo[pathInfo.Length - 1].Trim();
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError("SnitcherService.asmx.cs --> GetWebsiteLeadsFromInfo() --> lead page view product from url error", ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        product = product.Replace("_", " ").Replace("and", "&").Trim();
                        leadInfo.Url = leadInfo.Url.Replace("https://elioplus.com/", "elioplus.com/");

                        DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                        ElioSnitcherLeadsPageviews leadView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, leadInfo.Url, product, session);

                        if (leadView == null)
                        {
                            leadView = new ElioSnitcherLeadsPageviews();

                            leadView.Url = leadInfo.Url;
                            leadView.Product = product;
                            leadView.TimeSpent = 15;

                            leadView.ActionTime = lead.Sysdate; // DateTime.Now;

                            leadView.LeadId = lead.LeadId.ToString();
                            leadView.ElioWebsiteLeadsId = lead.Id;
                            leadView.Sysdate = lead.Sysdate;    // DateTime.Now;
                            leadView.LastUpdate = lead.LastUpdate;  // DateTime.Now;

                            loaderView.Insert(leadView);
                        }
                        else
                        {
                            leadView.Product = product;

                            leadView.ActionTime = lead.Sysdate;     // DateTime.Now;
                            leadView.LastUpdate = lead.LastUpdate;  // DateTime.Now;

                            loaderView.Update(leadView);
                        }

                        //leads.Add(lead);
                    }
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(string.Format("Error for Lead ID {0} at {1}", leadInfo.Id, DateTime.Now.ToString()), ex.Message.ToString(), ex.StackTrace.ToString());
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("Service Ip Info/Companies Anonymous Tracking class error at, " + DateTime.Now.ToString() + ", for website ID: 19980", ex.Message.ToString(), ex.StackTrace.ToString());
                throw ex;
                //return null;
            }

            return lead;
        }
    }
}
