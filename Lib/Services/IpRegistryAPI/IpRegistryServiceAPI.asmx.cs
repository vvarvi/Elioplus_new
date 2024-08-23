using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Web.Services;
using IPinfo;
using IPinfo.Models;
using MailJet.Client.Response;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using WdS.ElioPlus.Lib.Services.IpInfoAPI.Entities;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Lib.Services.IpRegistryServiceAPI
{
    /// <summary>
    /// Summary description for IpInfoServiceAPI
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class IpRegistryServiceAPI : System.Web.Services.WebService
    {

        [WebMethod]
        public static void GetRegistryInfo(string ip, string url, string path, DBSession session)
        {
            if (ConfigurationManager.AppSettings["IpRegistryAccessToken"] != null && ConfigurationManager.AppSettings["IpRegistryAccessToken"].ToString() != "")
            {
                if (!string.IsNullOrEmpty(ip) && !string.IsNullOrEmpty(url) && !string.IsNullOrEmpty(path))
                {
                    //string ip = "66.87.125.72";

                    string token = ConfigurationManager.AppSettings["IpRegistryAccessToken"].ToString();

                    if (token != "")
                    {
                        var client = new RestClient("https://api.ipregistry.co/" + ip + "?key=" + token);

                        var request = new RestRequest(Method.GET);

                        request.AddQueryParameter("Authorization", "Bearer " + ConfigurationManager.AppSettings["SnitcherKeyToken"].ToString());

                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;


                        IRestResponse response = client.Execute(request);

                        //Console.WriteLine(ipResponse.Content);

                        if (response != null)
                        {
                            if (response.StatusCode.ToString() == "OK")
                            {
                                var responseContentDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                                var result = JsonConvert.DeserializeObject<IpRegistryInfo>(response.Content);

                                if (result != null)
                                {
                                    IpRegistryInfo resp = (IpRegistryInfo)result;

                                    if (resp != null)
                                    {
                                        if (resp.Company.Type.ToLower() != "hosting" && resp.Company.Type.ToLower() != "isp")
                                        {
                                            ElioAnonymousRegistryInfo info = new ElioAnonymousRegistryInfo();

                                            info.Url = url;
                                            info.Path = path;
                                            info.IpAddress = ip;
                                            info.Type = resp.Type;
                                            info.HostName = resp.Hostname;
                                            info.CompanyDomain = resp.Company.Domain;
                                            info.CompanyName = resp.Company.Name;
                                            info.CompanyType = resp.Company.Type;
                                            info.Sysdate = DateTime.Now;
                                            info.LastUpdated = DateTime.Now;

                                            DataLoader<ElioAnonymousRegistryInfo> loader = new DataLoader<ElioAnonymousRegistryInfo>(session);
                                            loader.Insert(info);
                                        }
                                    }
                                }
                            }
                            else
                            {

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
