using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI
{
    /// <summary>
    /// Summary description for HubspotService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class HubspotService : System.Web.Services.WebService
    {
        [WebMethod(Description = "Create Hubspot New Lead")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CreateNewLead(string apiKey, ElioRegistrationDeals deal, ElioLeadDistributions lead)
        {
            string is_Viewed_By_Vendor = "0";
            string dealCountry = "Greece";
            string str = "";
            string responseMessage = "";

            if (deal != null)
            {
                str = "{\r\n  \"properties\": [\r\n    " +
                        "{\r\n      \"property\": \"id\",\r\n      \"value\": \"" + deal.Id.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"collaboration_vendor_reseller_id\",\r\n      \"value\": \"" + deal.CollaborationVendorResellerId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"vendor_id\",\r\n      \"value\": \"" + deal.VendorId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"reseller_id\",\r\n      \"value\": \"" + deal.ResellerId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_status\",\r\n      \"value\": \"" + deal.Status + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + deal.LastName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + deal.FirstName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + deal.CompanyName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + deal.Email + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_country\",\r\n      \"value\": \"" + dealCountry + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + deal.Website + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + deal.Phone + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"comments\",\r\n      \"value\": \"" + deal.Description + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"created_date\",\r\n      \"value\": \"" + deal.CreatedDate.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"last_update\",\r\n      \"value\": \"" + deal.LastUpdate.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"is_public\",\r\n      \"value\": \"" + deal.IsPublic + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_result\",\r\n      \"value\": \"" + deal.DealResult + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"is_new\",\r\n      \"value\": \"" + deal.IsNew + "\"\r\n    },\r\n    " +

                        "{\r\n      \"property\": \"is_viewed_by_vendor\",\r\n      \"value\": \"" + is_Viewed_By_Vendor + "\"\r\n    }\r\n  ]\r\n}";
            }
            else if (lead != null)
            {
                str = "{\r\n  \"properties\": [\r\n    " +
                        "{\r\n      \"property\": \"id\",\r\n      \"value\": \"" + lead.Id.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"collaboration_vendor_reseller_id\",\r\n      \"value\": \"" + lead.CollaborationVendorResellerId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"vendor_id\",\r\n      \"value\": \"" + lead.VendorId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"reseller_id\",\r\n      \"value\": \"" + lead.ResellerId + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_status\",\r\n      \"value\": \"" + lead.Status + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + lead.LastName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + lead.FirstName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + lead.CompanyName + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + lead.Email + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_country\",\r\n      \"value\": \"" + lead.Country + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + lead.Website + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + lead.Phone + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"comments\",\r\n      \"value\": \"" + lead.Comments + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"created_date\",\r\n      \"value\": \"" + lead.CreatedDate.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"last_update\",\r\n      \"value\": \"" + lead.LastUpdate.ToString() + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"is_public\",\r\n      \"value\": \"" + lead.IsPublic + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"lead_result\",\r\n      \"value\": \"" + lead.LeadResult + "\"\r\n    },\r\n    " +
                        "{\r\n      \"property\": \"is_new\",\r\n      \"value\": \"" + lead.IsNew + "\"\r\n    },\r\n    " +

                        "{\r\n      \"property\": \"is_viewed_by_vendor\",\r\n      \"value\": \"" + lead.isVewedByVendor + "\"\r\n    }\r\n  ]\r\n}";
            }

            var client = new RestClient("https://api.hubapi.com/contacts/v1/contact/?hapikey=" + apiKey);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "api.hubapi.com");
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("undefined", str, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            if (response != null)
            {
                if (response.StatusCode.ToString() == "")
                {
                    responseMessage = "SUCCESS";
                }
                else
                {
                    responseMessage = "FAILURE";
                }
            }
            else
            {
                //bad response
                responseMessage = "bad request";
            }

            return responseMessage;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static List<ElioLeadDistributions> GetLeadsDetail(int vendorId, string apiKey, DBSession session)
        {
            //string output = string.Empty;
            //string data = "";

            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/lists/all/contacts/all?hapikey=" + apiKey + "&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");

                IRestResponse response = client.Execute(request);

                RootObject model = JsonConvert.DeserializeObject<RootObject>(response.Content);

                List<ElioLeadDistributions> leads = new List<ElioLeadDistributions>();

                if (model != null)
                {
                    if (model.contacts.Count > 0)
                    {
                        foreach (WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities.Contact contact in model.contacts)
                        {
                            if (!Sql.ExistUserLeadByContactEmail(vendorId, contact.properties.email.value, session))
                            {
                                ElioLeadDistributions lead = new ElioLeadDistributions();

                                lead.LastName = contact.properties.lastname.value;
                                lead.FirstName = contact.properties.firstname.value;
                                lead.Email = contact.properties.email.value;
                                lead.CompanyName = contact.properties.company.value;
                                lead.Country = contact.properties.lead_country.value;
                                lead.Comments = contact.properties.comments.value;
                                lead.CreatedDate = contact.properties.created_date.value;
                                lead.IsNew = 1;
                                lead.IsPublic = 1;
                                lead.isVewedByVendor = 0;
                                lead.LastUpdate = contact.properties.last_update.value;
                                lead.LeadResult = "Pending";
                                lead.Status = (int)DealStatus.Open; // (contact.properties.lead_status != null) ? contact.properties.lead_status.value : (int)DealStatus.Open;
                                lead.VendorId = vendorId;
                                lead.Website = contact.properties.website.value;
                                lead.ResellerId = 0;
                                lead.Phone = contact.properties.phone.value;
                                lead.CollaborationVendorResellerId = 0;

                                DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                loader.Insert(lead);

                                leads.Add(lead);
                            }
                        }
                    }
                    else
                    {
                        //no contacts
                    }
                }
                else
                {
                    //empty model
                }

                //data = response.Content;

                return leads;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static ElioLeadDistributions GetLeadDetailByEmail(int vendorId, string apiKey, string contactEmail, DBSession session)
        {
            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/contact/email/" + contactEmail + "/profile?hapikey=" + apiKey + "");    //"&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");

                IRestResponse response = client.Execute(request);

                //RootObject model = JsonConvert.DeserializeObject<RootObject>(response.Content);
                //JObject obj = JObject.Parse(response.Content);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    string error = response.ErrorMessage.ToString();
                    Exception ex = new Exception(error);
                    throw ex;
                }
                else
                {
                    if (response.ResponseStatus == ResponseStatus.Completed)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                        {
                            return null;
                        }
                    }
                    else
                    {
                        string status = response.StatusDescription;
                        string errorCode = response.StatusCode.ToString();

                        Exception ex = new Exception(errorCode);
                        throw ex;
                    }
                }

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    JToken jToken = JToken.Parse(response.Content);

                    ElioLeadDistributions lead = null;

                    if (jToken != null)
                    {
                        //string vid = jToken["vid"]["value"].ToString();
                        //string portalId = jToken["portal-id"]["value"].ToString();
                        //string profileToken = jToken["profile-token"]["value"].ToString();
                        //string profileUrl = jToken["profile-url"]["value"].ToString();

                        string company = (jToken["properties"]["company"] != null) ? jToken["properties"]["company"]["value"].ToString() : "";
                        string firstname = jToken["properties"]["firstname"]["value"].ToString();
                        string lastname = jToken["properties"]["lastname"]["value"].ToString();
                        string email = jToken["properties"]["email"]["value"].ToString();
                        string phone = (jToken["properties"]["phone"] != null) ? jToken["properties"]["phone"]["value"].ToString() : "";
                        string website = (jToken["properties"]["website"] != null) ? jToken["properties"]["website"]["value"].ToString() : "";
                        string leadStatus = (jToken["properties"]["hs_lead_status"] != null) ? jToken["properties"]["hs_lead_status"]["value"].ToString() : "";
                        string createdate = jToken["properties"]["createdate"]["value"].ToString();
                        string country = (jToken["properties"]["country"] != null) ? jToken["properties"]["country"]["value"].ToString() : "";

                        #region To Delete

                        //var result = jToken["contacts"].ToObject<JArray>()
                        //            .Select(x => new
                        //            {
                        //                vid = Convert.ToInt32(x["vid"]),
                        //                properties = x["properties"].ToObject<Dictionary<string, JToken>>()
                        //                                            .Select(y => new
                        //                                            {
                        //                                                Key = y.Key,
                        //                                                Value = y.Value["value"].ToString()
                        //                                            }).ToList()
                        //            }).ToList();

                        //foreach (var item in result)
                        //{
                        //    Console.WriteLine(item.vid);

                        //    foreach (var prop in item.properties)
                        //    {
                        //        Console.WriteLine(prop.Key + " - " + prop.Value);
                        //    }

                        //    Console.WriteLine();
                        //}

                        //ContactListAPI_Result m = JsonConvert.DeserializeObject<ContactListAPI_Result>(response.Content);

                        //var result = m.ToDTO();

                        //var foo = ContactListAPI_Result.FromJson(response.Content);
                        //var result = foo.ToDTO();

                        //Console.WriteLine("Print all properties:");
                        //foreach (var p in result.Properties)
                        //{
                        //    string key = p.Key;
                        //    string value = p.Value;
                        //    //Console.WriteLine(p.Key + " - " + p.Value);
                        //}

                        //Console.WriteLine("\n\n Print one particular");
                        //Console.WriteLine(result.Properties["company"]);

                        //List<ElioLeadDistributions> leads = new List<ElioLeadDistributions>();

                        #endregion

                        //if (!Sql.ExistUserLeadByContactEmail(vendorId, contact.properties.email.value, session))

                        lead = new ElioLeadDistributions();

                        lead.LastName = lastname;
                        lead.FirstName = firstname;
                        lead.Email = email;
                        lead.CompanyName = company;
                        lead.Country = country;
                        lead.Comments = "";
                        lead.CreatedDate = DateTime.Now;    // Convert.ToDateTime(createdate);
                        lead.IsNew = 1;
                        lead.IsPublic = 1;
                        lead.isVewedByVendor = 0;
                        lead.LastUpdate = DateTime.Now;
                        lead.LeadResult = "Pending";
                        lead.Status = leadStatus != "" ? leadStatus.ToUpper() == "OPEN" ? (int)DealStatus.Open : (int)DealStatus.Closed : (int)DealStatus.Open; // (contact.properties.lead_status != null) ? contact.properties.lead_status.value : (int)DealStatus.Open;
                        lead.VendorId = vendorId;
                        lead.Website = website;
                        lead.Amount = 0;
                        lead.ResellerId = 0;
                        lead.Phone = phone;
                        lead.CollaborationVendorResellerId = 0;
                    }
                    else
                    {
                        return null;
                    }

                    return lead;
                }
                else
                {
                    string status = response.StatusDescription;
                    string errorCode = response.StatusCode.ToString();

                    Exception ex = new Exception(errorCode);
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static ElioRegistrationDeals GetDealDetailByEmail(int vendorId, string apiKey, string contactEmail, DBSession session)
        {
            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/contact/email/" + contactEmail + "/profile?hapikey=" + apiKey + "");    //"&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");

                IRestResponse response = client.Execute(request);

                //RootObject model = JsonConvert.DeserializeObject<RootObject>(response.Content);
                //JObject obj = JObject.Parse(response.Content);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    string error = response.ErrorMessage.ToString();
                    Exception ex = new Exception(error);
                    throw ex;
                }

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string status = response.StatusDescription;
                    string errorCode = response.StatusCode.ToString();

                    Exception ex = new Exception(errorCode);
                    throw ex;
                }
                else
                {
                    JToken jToken = JToken.Parse(response.Content);

                    ElioRegistrationDeals deal = null;

                    if (jToken != null)
                    {
                        //string vid = jToken["vid"]["value"].ToString();
                        //string portalId = jToken["portal-id"]["value"].ToString();
                        //string profileToken = jToken["profile-token"]["value"].ToString();
                        //string profileUrl = jToken["profile-url"]["value"].ToString();

                        string company = jToken["properties"]["company"]["value"].ToString();
                        string firstname = jToken["properties"]["firstname"]["value"].ToString();
                        string lastname = jToken["properties"]["lastname"]["value"].ToString();
                        string email = jToken["properties"]["email"]["value"].ToString();
                        string phone = jToken["properties"]["phone"]["value"].ToString();
                        string website = jToken["properties"]["website"]["value"].ToString();
                        string leadStatus = jToken["properties"]["hs_lead_status"]["value"].ToString();
                        string createdate = jToken["properties"]["createdate"]["value"].ToString();

                        #region To Delete

                        //var result = jToken["contacts"].ToObject<JArray>()
                        //            .Select(x => new
                        //            {
                        //                vid = Convert.ToInt32(x["vid"]),
                        //                properties = x["properties"].ToObject<Dictionary<string, JToken>>()
                        //                                            .Select(y => new
                        //                                            {
                        //                                                Key = y.Key,
                        //                                                Value = y.Value["value"].ToString()
                        //                                            }).ToList()
                        //            }).ToList();

                        //foreach (var item in result)
                        //{
                        //    Console.WriteLine(item.vid);

                        //    foreach (var prop in item.properties)
                        //    {
                        //        Console.WriteLine(prop.Key + " - " + prop.Value);
                        //    }

                        //    Console.WriteLine();
                        //}

                        //ContactListAPI_Result m = JsonConvert.DeserializeObject<ContactListAPI_Result>(response.Content);

                        //var result = m.ToDTO();

                        //var foo = ContactListAPI_Result.FromJson(response.Content);
                        //var result = foo.ToDTO();

                        //Console.WriteLine("Print all properties:");
                        //foreach (var p in result.Properties)
                        //{
                        //    string key = p.Key;
                        //    string value = p.Value;
                        //    //Console.WriteLine(p.Key + " - " + p.Value);
                        //}

                        //Console.WriteLine("\n\n Print one particular");
                        //Console.WriteLine(result.Properties["company"]);

                        //List<ElioLeadDistributions> leads = new List<ElioLeadDistributions>();

                        #endregion

                        //if (!Sql.ExistUserLeadByContactEmail(vendorId, contact.properties.email.value, session))

                        deal = new ElioRegistrationDeals();

                        deal.LastName = lastname;
                        deal.FirstName = firstname;
                        deal.Email = email;
                        deal.CompanyName = company;
                        deal.Description = "";
                        deal.CreatedDate = DateTime.Now;
                        deal.IsNew = 1;
                        deal.IsPublic = 1;
                        deal.LastUpdate = DateTime.Now;
                        deal.DealResult = "Pending";
                        deal.Status = (int)DealStatus.Open; // (contact.properties.lead_status != null) ? contact.properties.lead_status.value : (int)DealStatus.Open;
                        deal.VendorId = vendorId;
                        deal.Website = website;
                        deal.ResellerId = 0;
                        deal.Phone = phone;
                        deal.CollaborationVendorResellerId = 0;
                        deal.Product = "";
                        deal.ForecastingPercent = "";
                    }
                    else
                    {
                        return null;
                    }

                    return deal;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(Description = "Create Hubspot New Contact/Lead")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CreateNewContactLead(string apiKey, ElioRegistrationDeals deal, ElioLeadDistributions lead)
        {
            string str = "";
            string responseMessage = "";

            if (deal != null)
            {
                #region Deal

                if (!string.IsNullOrEmpty(deal.Email))
                {
                    string dealStatus = deal.Status == (int)DealStatus.Open ? DealStatus.Open.ToString().ToUpper() : DealStatus.New.ToString().ToUpper();

                    str = "{\r\n  \"properties\": [\r\n    " +
                            "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + dealStatus + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + deal.LastName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + deal.FirstName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + deal.CompanyName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + deal.Email + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + deal.Website + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + deal.Phone + "\"\r\n    }\r\n  ]\r\n}";
                }
                else
                {
                    responseMessage = System.Net.HttpStatusCode.BadRequest.ToString().ToUpper();
                    return responseMessage;
                }

                #endregion
            }
            else if (lead != null)
            {
                #region Lead

                if (!string.IsNullOrEmpty(lead.Email))
                {
                    string leadStatus = lead.Status == (int)DealStatus.Open ? DealStatus.Open.ToString().ToUpper() : DealStatus.New.ToString().ToUpper();

                    str = "{\r\n  \"properties\": [\r\n    " +
                            "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + lead.Email + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + lead.FirstName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + lead.LastName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + lead.Website + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + lead.CompanyName + "\"\r\n    },\r\n   " +
                            "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + leadStatus + "\"\r\n    },\r\n  " +
                            "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + lead.Phone + "\"\r\n    }\r\n    ]\r\n}";

                    //str = "{\r\n  \"properties\": [\r\n    " +

                    //        "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + lead.LastName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + lead.FirstName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + lead.CompanyName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + lead.Email + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + lead.Website + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + lead.Phone + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"createdate\",\r\n      \"value\": \"" + lead.CreatedDate.ToString() + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + leadStatus + "\"\r\n    }\r\n    " + "]\r\n}";
                }
                else
                {
                    responseMessage = System.Net.HttpStatusCode.BadRequest.ToString().ToUpper();
                    return responseMessage;
                }

                #endregion
            }

            #region Request

            var client = new RestClient("https://api.hubapi.com/contacts/v1/contact/?hapikey=" + apiKey);

            var request = new RestRequest(Method.POST);
            request.AddHeader("Host", "api.hubapi.com");
            request.AddHeader("Accept", "*/*");            
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json; charset=utf-8", str, ParameterType.RequestBody);

            #endregion

            #region Response Status Codes

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                responseMessage = response.StatusCode.ToString().ToUpper();
            }
            else
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    responseMessage = response.StatusCode.ToString().ToUpper();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    responseMessage = response.StatusCode.ToString().ToUpper();
                }
                else
                {
                    string status = response.StatusDescription;
                    string errorCode = response.StatusCode.ToString();
                    responseMessage = status;

                    Exception ex = new Exception(errorCode);
                    throw ex;
                }
            }

            #endregion

            return responseMessage;
        }

        [WebMethod(Description = "Create or Update Hubspot Contact/Lead")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static string CreateOrUpdateContactLead(string apiKey, ElioRegistrationDeals deal, ElioLeadDistributions lead)
        {
            string str = "";
            string responseMessage = "";
            string email = (deal != null) ? deal.Email : (lead != null) ? lead.Email : "";

            if (!string.IsNullOrEmpty(email))
            {
                if (deal != null)
                {
                    #region Deal

                    string dealStatus = deal.Status == (int)DealStatus.Open ? DealStatus.Open.ToString().ToUpper() : DealStatus.New.ToString().ToUpper();

                    str = "{\r\n  \"properties\": [\r\n    " +
                            "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + dealStatus + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + deal.LastName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + deal.FirstName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + deal.CompanyName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + deal.Email + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + deal.Website + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + deal.Phone + "\"\r\n    }\r\n  ]\r\n}";

                    #endregion
                }
                else if (lead != null)
                {
                    #region Lead

                    string leadStatus = lead.Status == (int)DealStatus.Open ? DealStatus.Open.ToString().ToUpper() : DealStatus.New.ToString().ToUpper();

                    str = "{\r\n  \"properties\": [\r\n    " +
                            "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + lead.Email + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + lead.FirstName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + lead.LastName + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + lead.Website + "\"\r\n    },\r\n    " +
                            "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + lead.CompanyName + "\"\r\n    },\r\n   " +
                            "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + leadStatus + "\"\r\n    },\r\n  " +
                            "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + lead.Phone + "\"\r\n    }\r\n    ]\r\n}";

                    //str = "{\r\n  \"properties\": [\r\n    " +

                    //        "{\r\n      \"property\": \"lastname\",\r\n      \"value\": \"" + lead.LastName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"firstname\",\r\n      \"value\": \"" + lead.FirstName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"company\",\r\n      \"value\": \"" + lead.CompanyName + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"email\",\r\n      \"value\": \"" + lead.Email + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"website\",\r\n      \"value\": \"" + lead.Website + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"phone\",\r\n      \"value\": \"" + lead.Phone + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"createdate\",\r\n      \"value\": \"" + lead.CreatedDate.ToString() + "\"\r\n    },\r\n    " +
                    //        "{\r\n      \"property\": \"hs_lead_status\",\r\n      \"value\": \"" + leadStatus + "\"\r\n    }\r\n    " + "]\r\n}";

                    #endregion
                }

                #region Request

                var client = new RestClient("https://api.hubapi.com/contacts/v1/contact/createOrUpdate/email/" + email + "/?hapikey=" + apiKey);

                var request = new RestRequest(Method.POST);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");
                request.AddHeader("Content-Type", "application/json");
                request.AddParameter("application/json; charset=utf-8", str, ParameterType.RequestBody);

                #endregion

                #region Response Status Codes

                IRestResponse response = client.Execute(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    responseMessage = response.StatusCode.ToString().ToUpper();
                }
                else
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                    {
                        responseMessage = response.StatusCode.ToString().ToUpper();
                    }
                    else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                    {
                        responseMessage = response.StatusCode.ToString().ToUpper();
                    }
                    else
                    {
                        string status = response.StatusDescription;
                        string errorCode = response.StatusCode.ToString();
                        responseMessage = status;

                        Exception ex = new Exception(errorCode);
                        throw ex;
                    }
                }

                #endregion
            }
            else
            {
                responseMessage = System.Net.HttpStatusCode.BadRequest.ToString().ToUpper();
            }

            return responseMessage;
        }

        [WebMethod(Description = "Search Hubspot Contact by email")]
        [ScriptMethod(UseHttpGet = true)]
        public static bool SearchContactByEmai(string apiKey, string contactEmail)
        {
            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/search/query?q=" + contactEmail + "&hapikey=" + apiKey + "");    //"&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");

                IRestResponse response = client.Execute(request);

                if (response.ResponseStatus != ResponseStatus.Completed)
                {
                    string error = response.ErrorMessage.ToString();
                    Exception ex = new Exception(error);
                    throw ex;
                }

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    string status = response.StatusDescription;
                    string errorCode = response.StatusCode.ToString();

                    Exception ex = new Exception(errorCode);
                    throw ex;
                }
                else
                {
                    JToken jToken = JToken.Parse(response.Content);

                    if (jToken != null)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ContactListAPI_Result FromJson(string json)
        {
            ContactListAPI_Result r = new ContactListAPI_Result();

            r = JsonConvert.DeserializeObject<ContactListAPI_Result>(json);

            return r;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static List<ElioRegistrationDeals> GetDealDetailByEmaiCompanyName(int vendorId, string apiKey, string email, string companyName, DBSession session)
        {
            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/lists/all/contacts/all?hapikey=" + apiKey + "&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");

                IRestResponse response = client.Execute(request);

                RootObject model = JsonConvert.DeserializeObject<RootObject>(response.Content);

                List<ElioRegistrationDeals> leads = new List<ElioRegistrationDeals>();

                if (model != null)
                {
                    if (model.contacts.Count > 0)
                    {
                        foreach (WdS.ElioPlus.Lib.Services.CRMs.HubspotAPI.Entities.Contact contact in model.contacts)
                        {
                            //if (!Sql.ExistUserLeadByContactEmail(vendorId, contact.properties.email.value, session))
                            //{
                            ElioRegistrationDeals lead = new ElioRegistrationDeals();

                            lead.LastName = contact.properties.lastname.value;
                            lead.FirstName = contact.properties.firstname.value;
                            lead.Email = contact.properties.email.value;
                            lead.CompanyName = contact.properties.company.value;
                            //lead.Country = contact.properties.lead_country.value;
                            lead.Description = contact.properties.comments.value;
                            lead.CreatedDate = contact.properties.created_date.value;
                            lead.IsNew = 1;
                            lead.IsPublic = 1;
                            //lead.isVewedByVendor = 0;
                            lead.LastUpdate = contact.properties.last_update.value;
                            lead.DealResult = "Pending";
                            lead.Status = (int)DealStatus.Open; // (contact.properties.lead_status != null) ? contact.properties.lead_status.value : (int)DealStatus.Open;
                            lead.VendorId = vendorId;
                            lead.Website = contact.properties.website.value;
                            lead.ResellerId = 0;
                            lead.Phone = contact.properties.phone.value;
                            lead.CollaborationVendorResellerId = 0;
                            lead.Product = "";
                            lead.ForecastingPercent = "";

                            leads.Add(lead);
                            //}
                        }
                    }
                    else
                    {
                        //no contacts
                    }
                }
                else
                {
                    //empty model
                }

                //data = response.Content;

                return leads;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static bool ExistLeadInCRM(string apiKey, string contactEmail)
        {
            //string output = string.Empty;
            //string data = "";

            try
            {
                var client = new RestClient("https://api.hubapi.com/contacts/v1/lists/all/contacts/all?hapikey=" + apiKey + "&property=id&property=collaboration_vendor_reseller_id&property=vendor_id&property=reseller_id&property=lead_status&property=firstname&property=lastname&property=company&property=email&property=lead_country&property=website&property=phone&property=comments&property=created_date&property=last_update&property=is_public&property=lead_result&property=is_new&property=is_viewed_by_vendor");
                var request = new RestRequest(Method.GET);
                request.AddHeader("Host", "api.hubapi.com");
                request.AddHeader("Accept", "*/*");
                request.AddParameter("email", contactEmail);

                IRestResponse response = client.Execute(request);

                RootObject model = JsonConvert.DeserializeObject<RootObject>(response.Content);

                List<ElioLeadDistributions> leads = new List<ElioLeadDistributions>();

                if (model != null)
                {
                    if (model.contacts.Count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
