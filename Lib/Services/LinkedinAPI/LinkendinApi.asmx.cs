using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.LinkedinAPI
{
    /// <summary>
    /// Summary description for LinkendinApi
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class LinkendinApi : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public static string LinkedINAuth()
        {
            //This method path is your return URL  
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://www.linkedin.com/oauth/v2/authorization");
                var request = new RestRequest(Method.GET);
                request.AddParameter("response_type", "code");                
                request.AddParameter("client_id", "86r4i8sx9gbse1");
                request.AddParameter("redirect_uri", "http://localhost:51809/Login.aspx");
                request.AddParameter("state", "987654321");
                request.AddParameter("scope", "r_emailaddress%20r_basicprofile");
                IRestResponse response = client.Execute(request);
                var content = response.Content; 

                var linkedinDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                if (linkedinDictionary != null)
                {
                    JToken returnUrl = linkedinDictionary["code"];

                    if(returnUrl!=null)
                        return returnUrl.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static string LinkedINAccessToken(string code, out string expires)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                expires = "";

                //Get Access Token  
                var client = new RestClient("https://www.linkedin.com/oauth/v2/accessToken");
                var request = new RestRequest(Method.POST);
                request.AddParameter("grant_type", "authorization_code");
                request.AddParameter("code", code);
                request.AddParameter("redirect_uri", "https://elioplus.com/login");
                request.AddParameter("client_id", "86r4i8sx9gbse1");
                request.AddParameter("client_secret", "P6x1Eyy3Vp0ySXzc");
                IRestResponse response = client.Execute(request);
                var content = response.Content;

                var linkedinDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                if (linkedinDictionary != null)
                {
                    JToken token = linkedinDictionary["access_token"];
                    JToken expiresIn = linkedinDictionary["expires_in"];

                    if (expiresIn != null)
                        expires = expiresIn.ToString();

                    if (token != null)
                        return token.ToString();
                    else
                        return "";
                }
                else
                    return "";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities.UserData.UserInfo LinkedINGetProfileDetails(string accessToken)
        {
            try
            {
                WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities.UserData.UserInfo userData = new UserData.UserInfo();

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Get Profile Details  
                //var client = new RestClient("https://api.linkedin.com/v2/me?oauth2_access_token=" + accessToken + "");
                var client = new RestClient("https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))&oauth2_access_token=" + accessToken + "");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                //var content = response.Content;
                //var content = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                var linkedinDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                //JObject combinedPersonCompanyResponse = JObject.Parse(response.Content);

                if (linkedinDictionary != null)
                {
                    JToken elements = linkedinDictionary["elements"][0];
                    if (elements != null)
                    {
                        var dictionary = new Dictionary<string, object>();
                        foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(elements))
                        {
                            object obj = propertyDescriptor.GetValue(elements);
                            dictionary.Add(propertyDescriptor.Name, obj);
                        }

                        if (dictionary != null)
                        {
                            foreach (var item in dictionary)
                            {
                                if (item.Key == "handle~")
                                {
                                    JObject emailAddress = JObject.Parse(item.Value.ToString());
                                    if (emailAddress != null)
                                    {
                                        JToken emailToken = emailAddress["emailAddress"];
                                        if (emailToken != null)
                                        {
                                            userData.EmailAddress = emailToken.ToString();
                                            break;
                                        }
                                    }
                                }
                            }

                            if (userData.EmailAddress == "")
                                return null;
                        }
                        else
                            return null;
                    }
                    else
                        return null;
                }
                else
                    return null;

                client = new RestClient("https://api.linkedin.com/v2/me?oauth2_access_token=" + accessToken + "");
                request = new RestRequest(Method.GET);
                response = client.Execute(request);

                var person = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                if (person != null)
                {
                    foreach (var item in person)
                    {
                        if (item.Key == "localizedLastName")
                        {
                            userData.LastName = item.Value.ToString();
                        }
                        else if (item.Key == "localizedFirstName")
                        {
                            userData.FirstName = item.Value.ToString();
                        }
                        else if (item.Key == "profilePicture")
                        {
                            JObject picture = JObject.Parse(item.Value.ToString());
                            if (picture != null)
                            {
                                JToken pictureToken = picture["displayImage"];
                                if (pictureToken != null)
                                    userData.PictureUrl = pictureToken.ToString();
                            }
                        }
                        else if (item.Key == "id")
                        {
                            userData.Id = item.Value.ToString();
                        }
                    }
                }
                else
                    return null;

                if (userData != null && userData.Id != "" && userData.EmailAddress != "")
                    return userData;
                else
                    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public static void LinkedINGetProfileDetailsById(string accessToken)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                //Get Profile Details  
                var client = new RestClient("https://api.linkedin.com/v2/me?oauth2_access_token=" + accessToken + "");
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                //var content = response.Content;
                //var content = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                var linkedinDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

                if (linkedinDictionary != null)
                {
                    //JToken token = linkedinDictionary["localizedLastName"];
                    //JToken expiresIn = linkedinDictionary["expires_in"];

                }

                //WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities.UserData.UserInfo userData = null;   // JsonConvert.DeserializeObject<WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities.UserData.UserInfo>(response.Content);

                //if (userData != null)
                //    return userData;
                //else
                //    return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void LinkedINSaveUserProfileData(WdS.ElioPlus.Lib.Services.LinkedinAPI.Entities.UserData.UserInfo userData, DBSession session)
        {
            try
            {
                if (userData != null)
                {
                    #region Insert New User

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    ElioUsers user = new ElioUsers();

                    user.LinkedinId = userData.Id;
                    user.Username = userData.EmailAddress;
                    user.UsernameEncrypted = MD5.Encrypt(userData.EmailAddress);
                    user.Password = userData.Id;
                    user.PasswordEncrypted = MD5.Encrypt(userData.Id);
                    user.FirstName = userData.FirstName;
                    user.LastName = userData.LastName;
                    user.Email = userData.EmailAddress;
                    user.PersonalImage = (userData.PictureUrl != "") ? userData.PictureUrl : null;
                    user.LinkedInUrl = userData.PublicProfileUrl;
                    user.CommunitySummaryText = (userData.Headline == "undefined") ? userData.Headline : userData.Summary;
                    user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                    user.SysDate = DateTime.Now;
                    user.LastUpdated = DateTime.Now;
                    user.GuId = Guid.NewGuid().ToString();
                    user.IsPublic = 1;
                    user.CommunityProfileCreated = DateTime.Now;
                    user.CommunityProfileLastUpdated = DateTime.Now;
                    user.LastLogin = DateTime.Now;
                    user.TwitterUrl = string.Empty;
                    user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);
                    user.UserApplicationType = Convert.ToInt32(UserApplicationType.Elioplus);

                    user.CommunityStatus = Convert.ToInt32(AccountStatus.Completed);
                    user.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);

                    loader.Insert(user);

                    #endregion
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
