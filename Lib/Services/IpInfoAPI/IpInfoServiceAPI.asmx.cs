using System;
using System.Collections.Generic;
using System.Configuration;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Web.Services;
using DocumentFormat.OpenXml.EMMA;
using IPinfo;
using IPinfo.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.SalesforceDC;

namespace WdS.ElioPlus.Lib.Services.IpInfoAPI
{
    /// <summary>
    /// Summary description for IpInfoServiceAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IpInfoServiceAPI : System.Web.Services.WebService
    {

        [WebMethod]
        public static void GetInfo(string ip, string url, string path, DBSession session)
        {
            List<ElioSnitcherWebsiteLeads> infoLeads = new List<ElioSnitcherWebsiteLeads>();

            if (ConfigurationManager.AppSettings["IpInfoAccessToken"] != null && ConfigurationManager.AppSettings["IpInfoAccessToken"].ToString() != "")
            {
                if (!string.IsNullOrEmpty(ip))
                {
                    //string ip = "66.87.125.72";

                    //bool existIp = Sql.ExistIpAnonymousIpInfo(ip, session);
                    ElioAnonymousIpInfo leadInfo = Sql.GetAnonymousIpInfoByIp(ip, session);

                    if (leadInfo == null)
                    {
                        string token = ConfigurationManager.AppSettings["IpInfoAccessToken"].ToString();

                        if (token != "")
                        {
                            TimeSpan timeOut = TimeSpan.FromSeconds(5);
                            IPinfoClient client = new IPinfoClient.Builder()
                        .AccessToken(token) // pass your token string
                        .Build();

                            IPResponse ipResponse = client.IPApi.GetDetails(ip);

                            if (ipResponse != null)
                            {
                                if (ipResponse.Company != null && ipResponse.Company.Type.ToLower() != "hosting" && ipResponse.Company.Type.ToLower() != "isp")
                                {
                                    leadInfo = new ElioAnonymousIpInfo();

                                    leadInfo.Path = path;
                                    leadInfo.Url = url.Split('?')[0];
                                    leadInfo.IpAddress = ipResponse.IP;
                                    leadInfo.City = ipResponse.City;
                                    leadInfo.Region = ipResponse.Region;
                                    leadInfo.Org = ipResponse.Org;
                                    leadInfo.HostName = ipResponse.Hostname;
                                    leadInfo.Timezone = ipResponse.Timezone;

                                    leadInfo.CompanyDomain = ipResponse.Company.Domain;
                                    leadInfo.CompanyName = (ipResponse.Company != null) ? ipResponse.Company.Name : "";
                                    leadInfo.CompanyType = ipResponse.Company.Type;

                                    leadInfo.Country = ipResponse.Country;
                                    leadInfo.CountryName = ipResponse.CountryName;

                                    leadInfo.IsEu = ipResponse.IsEU.ToString();

                                    if (ipResponse.Continent != null)
                                    {
                                        //lead.Continent = new Entities.Continent();

                                        leadInfo.ContinentName = ipResponse.Continent.Name;
                                        leadInfo.ContinentCode = ipResponse.Continent.Code;
                                    }

                                    //if (ipResponse.Carrier != null)
                                    //{
                                    // carrier info may not be available for every ip
                                    // for sample case, response for ip 66.87.125.72 has carrier info
                                    //lead.Carrier.Mcc = ipResponse.Carrier.Mcc;
                                    //lead.Carrier.Mnc = ipResponse.Carrier.Mnc;
                                    //lead.Carrier.Name = ipResponse.Carrier.Name;
                                    //}

                                    leadInfo.Sysdate = DateTime.Now;
                                    leadInfo.LastUpdated = DateTime.Now;

                                    DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);
                                    loader.Insert(leadInfo);

                                    if (ipResponse.Domains.Domains.Count > 0)
                                    {
                                        foreach (string item in ipResponse.Domains.Domains)
                                        {
                                            ElioAnonymousIpInfoDomains domain = new ElioAnonymousIpInfoDomains();

                                            domain.InfoId = leadInfo.Id;
                                            domain.Domain = item;
                                            domain.Sysdate = DateTime.Now;
                                            domain.LastUpdate = DateTime.Now;

                                            DataLoader<ElioAnonymousIpInfoDomains> dLoader = new DataLoader<ElioAnonymousIpInfoDomains>(session);
                                            dLoader.Insert(domain);
                                        }
                                    }

                                    if (!string.IsNullOrEmpty(leadInfo.CompanyDomain))
                                    {
                                        ElioAnonymousCompaniesInfo companyInfo = TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(leadInfo.CompanyDomain, session);

                                        if (companyInfo != null)
                                        {
                                            ElioSnitcherWebsiteLeads infoLead = AnonymousTrackingAPI.SnitcherService.GetWebsiteLeadsFromInfo(companyInfo, leadInfo, session);

                                            if (infoLead != null)
                                            {
                                                leadInfo.IsInserted = 1;

                                                DataLoader<ElioAnonymousIpInfo> loaderAnInfo = new DataLoader<ElioAnonymousIpInfo>(session);
                                                loaderAnInfo.Update(leadInfo);

                                                if (infoLead != null)
                                                    infoLeads.Add(infoLead);
                                            }

                                            //string message = string.Format("{0} leads were inserted to Elio from Anonymous Ip Info/The Companies API at {1}", infoLeads.Count.ToString(), DateTime.Now);

                                            //Logger.DetailedError("GetStatistics() --> IpInfoAPI.IpInfoServiceAPI()", message);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        leadInfo.Path = path;
                        leadInfo.Url = url.Split('?')[0];
                        leadInfo.IsInserted = 0;
                        leadInfo.Sysdate = DateTime.Now;
                        leadInfo.LastUpdated = DateTime.Now;

                        DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);
                        loader.Update(leadInfo);

                        if (!string.IsNullOrEmpty(leadInfo.CompanyDomain))
                        {
                            ElioAnonymousCompaniesInfo companyInfo = TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(leadInfo.CompanyDomain, session);

                            if (companyInfo != null)
                            {
                                ElioSnitcherWebsiteLeads infoLead = AnonymousTrackingAPI.SnitcherService.GetWebsiteLeadsFromInfo(companyInfo, leadInfo, session);

                                if (infoLead != null)
                                {
                                    leadInfo.IsInserted = 1;

                                    DataLoader<ElioAnonymousIpInfo> loaderAnInfo = new DataLoader<ElioAnonymousIpInfo>(session);
                                    loaderAnInfo.Update(leadInfo);

                                    if (infoLead != null)
                                        infoLeads.Add(infoLead);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static string PromptHelper()
        {
            Console.WriteLine("\nOptions:");
            Console.WriteLine("-Enter 0 to quit");
            Console.WriteLine("-Enter ip address:");
            return Console.ReadLine() ?? "";
        }
    }
}
