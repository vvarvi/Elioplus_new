using IPinfo.Models;
using IPinfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using System.Configuration;
using RestSharp;
using System.Net;
using WdS.ElioPlus.Lib.Services.TheCompaniesAPI.Entities;
using Newtonsoft.Json;
using ServiceStack;
using WdS.ElioPlus.Lib.DBQueries;

namespace WdS.ElioPlus.Lib.Services.TheCompaniesAPI
{
    /// <summary>
    /// Summary description for CompaniesServiceAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class CompaniesServiceAPI : System.Web.Services.WebService
    {

        [WebMethod]
        public static ElioAnonymousCompaniesInfo GetCompaniesInfo(string domain, DBSession session)
        {
            ElioAnonymousCompaniesInfo info = Sql.GetAnonymousCompaniesInfoByDomain(domain, session);
            if (info == null)
            {
                if (ConfigurationManager.AppSettings["CompaniesApiAccessToken"] != null && ConfigurationManager.AppSettings["CompaniesApiAccessToken"].ToString() != "")
                {
                    if (!string.IsNullOrEmpty(domain))
                    {
                        //string ip = "66.87.125.72";

                        //bool existDomain = Sql.ExistDomainAnonymousCompaniesInfo(domain, session);

                        string token = ConfigurationManager.AppSettings["CompaniesApiAccessToken"].ToString();

                        if (token != "")
                        {
                            var client = new RestClient("https://api.thecompaniesapi.com/v1/companies/" + domain + "?token=" + token);

                            var request = new RestRequest(Method.GET);

                            //request.AddQueryParameter("Authorization", "Bearer " + ConfigurationManager.AppSettings["SnitcherKeyToken"].ToString());

                            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                            IRestResponse response = client.Execute(request);

                            if (response != null)
                            {
                                if (response.StatusCode.ToString() == "OK")
                                {
                                    RootCompamies result = JsonConvert.DeserializeObject<RootCompamies>(response.Content);

                                    if (result != null && !string.IsNullOrEmpty(result.domain))
                                    {
                                        info = new ElioAnonymousCompaniesInfo();

                                        info.Domain = (result.domain != null && !string.IsNullOrEmpty(result.domain)) ? result.domain : "";
                                        info.CityName = (result.city != null && !string.IsNullOrEmpty(result.city.name)) ? result.city.name : "";
                                        info.CountryName = (result.country != null && !string.IsNullOrEmpty(result.country.name)) ? result.country.name : "";
                                        info.Description = result.descriptionShort;

                                        if (result.industries != null && result.industries.Count > 0)
                                        {
                                            foreach (string industry in result.industries)
                                            {
                                                info.Industries += industry + ",";
                                            }

                                            if (info.Industries != "" && info.Industries.EndsWith(","))
                                                info.Industries = info.Industries.Substring(0, info.Industries.Length - 1);
                                        }

                                        info.Logo = result.logo;
                                        info.PhoneNumber = result.phoneNumber;
                                        info.Revenue = result.revenue;
                                        info.Linkedin = (result.socialNetworks != null && !string.IsNullOrEmpty(result.socialNetworks.linkedin)) ? result.socialNetworks.linkedin : "";

                                        if (result.state != null && !string.IsNullOrEmpty(result.state.name))
                                            info.StateName = result.state.name;

                                        info.TotalEmployees = (!string.IsNullOrEmpty(result.totalEmployees)) ? result.totalEmployees : "0";
                                        info.YearFounded = (!string.IsNullOrEmpty(result.yearFounded)) ? int.Parse(result.yearFounded) : 0;

                                        info.Sysdate = DateTime.Now;
                                        info.LastUpdated = DateTime.Now;

                                        DataLoader<ElioAnonymousCompaniesInfo> loader = new DataLoader<ElioAnonymousCompaniesInfo>(session);
                                        loader.Insert(info);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return info;
        }
    }
}
