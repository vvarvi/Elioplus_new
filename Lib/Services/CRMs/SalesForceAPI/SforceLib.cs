using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using SalesforceSharp;
using SalesforceSharp.Security;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.SalesforceDC;
//using Salesforce.Common;
//using Salesforce.Force;
//using System.Dynamic;

namespace WdS.ElioPlus.Lib.Services.CRMs.SalesForceAPI
{
    public class SforceLib
    {
        public static string SFCreateOrUpdateLead(string username, string password, string token, ElioLeadDistributions elioLead)
        {
            try
            {
                string responseMessage = "";

                #region Login Service

                SforceService sfService = new SforceService();
                LoginResult loginResult = new LoginResult();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                loginResult = sfService.login(username, password + token);

                #endregion

                #region Set Login Results to Service

                sfService.Url = loginResult.serverUrl;
                sfService.SessionHeaderValue = new SessionHeader();
                sfService.SessionHeaderValue.sessionId = loginResult.sessionId;

                #endregion

                #region Check if exists

                QueryResult qr = new QueryResult();
                string query = "Select FirstName,LastName,Phone,Company,Email,Country,Website,Description,CreatedDate,LastModifiedDate,Status from Lead where email='" + elioLead.Email + "'";      //varvitsiotisvag@yahoo.gr;

                qr = sfService.query(query);
                int count = qr.size;
                bool exist = false;
                                
                sObject[] records = qr.records;

                if (records.Length > 0)
                    exist = true;

                #endregion

                #region SF Lead Details

                WdS.ElioPlus.SalesforceDC.Lead sfLead = new WdS.ElioPlus.SalesforceDC.Lead();
                sfLead.FirstName = elioLead.FirstName;
                sfLead.LastName = elioLead.LastName;
                sfLead.Phone = elioLead.Phone;
                sfLead.Company = elioLead.CompanyName;
                sfLead.Email = elioLead.Email;
                sfLead.Country = "";
                sfLead.Website = elioLead.Website;
                sfLead.Description = elioLead.Comments;
                sfLead.CreatedDate = DateTime.Now;
                sfLead.LastModifiedDate = DateTime.Now;

                string leadStatus = elioLead.Status == (int)DealStatus.Open ? DealStatus.Open.ToString() : elioLead.Status == (int)DealStatus.Closed ? DealStatus.Closed.ToString() : DealStatus.None.ToString();
                sfLead.Status = leadStatus;

                #endregion

                if (!exist)
                {
                    #region Insert New Lead

                    SaveResult[] create = sfService.create(new sObject[] { sfLead });
                    if (create[0].success)
                    {
                        string id = create[0].id;
                        responseMessage = "OK";
                    }
                    else
                    {
                        responseMessage = create[0].errors[0].message;
                    }

                    #endregion
                }
                else
                {
                    #region Update Lead

                    SaveResult[] update = sfService.update(new sObject[] { sfLead });
                    if (update[0].success)
                    {
                        string id = update[0].id;
                        responseMessage = "OK";
                    }
                    else
                    {
                        responseMessage = update[0].errors[0].message;
                    }

                    #endregion
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string SFCreateOrUpdateDeal(string username, string password, string token, ElioRegistrationDeals elioDeal)
        {
            try
            {
                string responseMessage = "";

                #region Login Service

                SforceService sfService = new SforceService();
                LoginResult loginResult = new LoginResult();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                loginResult = sfService.login(username, password + token);

                #endregion

                #region Set Login Results to Service

                sfService.Url = loginResult.serverUrl;
                sfService.SessionHeaderValue = new SessionHeader();
                sfService.SessionHeaderValue.sessionId = loginResult.sessionId;

                #endregion

                #region Check if exists

                QueryResult qr = new QueryResult();
                string query = "Select FirstName,LastName,Phone,Company,Email,Country,Website,Description,CreatedDate,LastModifiedDate,Status from Contact where email='" + elioDeal.Email + "'";      //varvitsiotisvag@yahoo.gr;

                qr = sfService.query(query);
                int count = qr.size;
                bool exist = false;

                sObject[] records = qr.records;

                if (records.Length > 0)
                    exist = true;

                #endregion

                #region SF Contact Details

                WdS.ElioPlus.SalesforceDC.Contact sfcontact = new WdS.ElioPlus.SalesforceDC.Contact();
                sfcontact.FirstName = elioDeal.FirstName;
                sfcontact.LastName = elioDeal.LastName;
                sfcontact.Phone = elioDeal.Phone;
                sfcontact.Name = elioDeal.CompanyName;
                sfcontact.Email = elioDeal.Email;
                sfcontact.Description = elioDeal.Description;
                sfcontact.CreatedDate = DateTime.Now;
                sfcontact.LastModifiedDate = DateTime.Now;

                string dealStatus = elioDeal.Status == (int)DealStatus.Open ? DealStatus.Open.ToString() : elioDeal.Status == (int)DealStatus.Closed ? DealStatus.Closed.ToString() : DealStatus.None.ToString();
                //sfcontact.Status = dealStatus;

                #endregion

                if (!exist)
                {
                    #region Insert New Contact

                    SaveResult[] create = sfService.create(new sObject[] { sfcontact });
                    if (create[0].success)
                    {
                        string id = create[0].id;
                        responseMessage = "OK";
                    }
                    else
                    {
                        responseMessage = create[0].errors[0].message;
                    }

                    #endregion
                }
                else
                {
                    #region Update Contact

                    SaveResult[] update = sfService.update(new sObject[] { sfcontact });
                    if (update[0].success)
                    {
                        string id = update[0].id;
                        responseMessage = "OK";
                    }
                    else
                    {
                        responseMessage = update[0].errors[0].message;
                    }

                    #endregion
                }

                return responseMessage;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ElioLeadDistributions SFGetLeadByEmail(int vendorId, string email, string username, string password, string token)
        {
            try
            {
                ElioLeadDistributions lead = null;

                SforceService sfService = new SforceService();
                LoginResult loginResult = new LoginResult();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                loginResult = sfService.login(username, password + token);

                sfService.Url = loginResult.serverUrl;
                sfService.SessionHeaderValue = new SessionHeader();
                sfService.SessionHeaderValue.sessionId = loginResult.sessionId;

                QueryResult qr = new QueryResult();

                string query = "Select FirstName,LastName,Phone,Company,Email,Country,Website,Description,CreatedDate,LastModifiedDate,Status from Lead where email='" + email + "'";      //varvitsiotisvag@yahoo.gr;

                qr = sfService.query(query);
                bool done = false;
                int count = qr.size;

                while (!done)
                {
                    sObject[] records = qr.records;

                    if (records.Length == 0)
                    {
                        return null;
                        //no lead found with this email
                    }
                    else
                    {
                        for (int i = 0; i < records.Length; i++)
                        {
                            lead = new ElioLeadDistributions();
                            sObject news = qr.records[i];
                            SalesforceDC.Lead l = news as SalesforceDC.Lead;

                            lead.FirstName = l.FirstName;
                            lead.LastName = l.LastName;
                            lead.Email = l.Email;
                            lead.Phone = l.Phone;
                            lead.CompanyName = l.Company;
                            lead.Country = l.Country;
                            lead.Website = l.Website;
                            lead.Comments = l.Description;
                            lead.CreatedDate = (l.CreatedDate != null) ? Convert.ToDateTime(l.CreatedDate) : DateTime.Now;
                            lead.LastUpdate = (l.LastModifiedDate != null) ? Convert.ToDateTime(l.LastModifiedDate) : DateTime.Now;
                            lead.Status = l.Status.ToUpper() == "OPEN" ? (int)DealStatus.Open : (int)DealStatus.Closed;
                            lead.IsNew = 1;
                            lead.IsPublic = 1;
                            lead.isVewedByVendor = 0;
                            lead.LeadResult = "Pending";
                            lead.VendorId = vendorId;
                            lead.Amount = 0;
                            lead.ResellerId = 0;
                            lead.CollaborationVendorResellerId = 0;
                        }

                        if (qr.done)
                            done = true;
                        else
                        {
                            break;
                            //more than on lead with this email
                            //sfService.queryMore(qr.queryLocator);
                        }
                    }
                }

                return lead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ElioLeadDistributions UpdateElioLeadFromSFLeadByEmail(int vendorId, ElioLeadDistributions lead, string username, string password, string token)
        {
            try
            {
                if (lead != null && !string.IsNullOrEmpty(lead.Email))
                {
                    #region Login Service

                    SforceService sfService = new SforceService();
                    LoginResult loginResult = new LoginResult();
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    loginResult = sfService.login(username, password + token);

                    #endregion

                    #region Set Login Results to Service

                    sfService.Url = loginResult.serverUrl;
                    sfService.SessionHeaderValue = new SessionHeader();
                    sfService.SessionHeaderValue.sessionId = loginResult.sessionId;

                    #endregion

                    #region Query

                    QueryResult qr = new QueryResult();

                    string query = "Select FirstName,LastName,Phone,Company,Email,Country,Website,Description,CreatedDate,LastModifiedDate,Status from Lead where email='" + lead.Email + "'";      //varvitsiotisvag@yahoo.gr;

                    qr = sfService.query(query);
                    bool done = false;
                    int count = qr.size;

                    #endregion

                    while (!done)
                    {
                        sObject[] records = qr.records;

                        if (records.Length == 0)
                        {
                            #region No Results

                            return null;
                            //no lead found with this email

                            #endregion
                        }
                        else
                        {
                            #region Lead Details

                            for (int i = 0; i < records.Length; i++)
                            {
                                sObject news = qr.records[i];
                                SalesforceDC.Lead l = news as SalesforceDC.Lead;

                                lead.FirstName = l.FirstName;
                                lead.LastName = l.LastName;
                                lead.Email = l.Email;
                                lead.Phone = l.Phone;
                                lead.CompanyName = l.Company;
                                lead.Country = l.Country;
                                lead.Website = l.Website;
                                lead.Comments = l.Description;
                                lead.CreatedDate = (l.CreatedDate != null) ? Convert.ToDateTime(l.CreatedDate) : DateTime.Now;
                                lead.LastUpdate = (l.LastModifiedDate != null) ? Convert.ToDateTime(l.LastModifiedDate) : DateTime.Now;
                                lead.Status = l.Status.ToUpper() == "OPEN" ? (int)DealStatus.Open : (int)DealStatus.Closed;
                                lead.IsNew = 1;
                                lead.IsPublic = 1;
                                lead.isVewedByVendor = 0;
                                lead.LeadResult = "Pending";
                                lead.VendorId = vendorId;
                                lead.Amount = 0;
                                lead.ResellerId = 0;
                                lead.CollaborationVendorResellerId = 0;
                            }

                            if (qr.done)
                                done = true;
                            else
                            {
                                break;
                                //more than on lead with this email
                                //sfService.queryMore(qr.queryLocator);
                            }

                            #endregion
                        }
                    }
                }
                else
                {
                    //Lead was empty or no email passed
                    return null;
                }

                return lead;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static bool SFExistLeadByEmail(string email, string username, string password, string token)
        {
            try
            {
                SforceService sfService = new SforceService();
                LoginResult loginResult = new LoginResult();
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                loginResult = sfService.login(username, password + token);

                sfService.Url = loginResult.serverUrl;
                sfService.SessionHeaderValue = new SessionHeader();
                sfService.SessionHeaderValue.sessionId = loginResult.sessionId;

                QueryResult qr = new QueryResult();

                string query = "Select FirstName,LastName,Phone,Company,Email,Country,Website,Description,CreatedDate,LastModifiedDate,Status from Lead where email='" + email + "'";      //varvitsiotisvag@yahoo.gr;

                qr = sfService.query(query);
                int count = qr.size;

                sObject[] records = qr.records;

                if (records.Length == 0)
                    return false;
                else
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static readonly string SecurityToken = ConfigurationManager.AppSettings["SecurityToken"];
        private static readonly string ConsumerKey = ConfigurationManager.AppSettings["ConsumerKey"];
        private static readonly string ConsumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"];
        private static readonly string Username = "vagvarvi@elioplus.com";   //ConfigurationManager.AppSettings["Username"];
        private static readonly string Password = "tornadovag1985!A";        //ConfigurationManager.AppSettings["Password"] + SecurityToken;
        private static readonly string IsSandboxUser = ConfigurationManager.AppSettings["IsSandboxUser"];

        /*
        private static async Task RunSample()
        {
            try
            {
                //var auth = new AuthenticationClient();

                //// Authenticate with Salesforce
                //Console.WriteLine("Authenticating with Salesforce");
                //var url = IsSandboxUser.Equals("true", StringComparison.CurrentCultureIgnoreCase)
                //    ? "https://test.salesforce.com/services/oauth2/token"
                //    : "https://login.salesforce.com/services/oauth2/token";

                //await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);
                //Console.WriteLine("Connected to Salesforce");

                //var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);

                //// retrieve all accounts
                //Console.WriteLine("Get Accounts");

                //const string qry = "SELECT ID, LastName FROM Account";
                //var accts = new List<Account>();
                //var results = await client.QueryAsync<Account>(qry);
                //var totalSize = results.TotalSize;

                //Console.WriteLine("Queried " + totalSize + " records.");

                //accts.AddRange(results.Records);
                //var nextRecordsUrl = results.NextRecordsUrl;

                //if (!string.IsNullOrEmpty(nextRecordsUrl))
                //{
                //    Console.WriteLine("Found nextRecordsUrl.");

                //    while (true)
                //    {
                //        var continuationResults = await client.QueryContinuationAsync<Account>(nextRecordsUrl);
                //        totalSize = continuationResults.TotalSize;
                //        Console.WriteLine("Queried an additional " + totalSize + " records.");

                //        accts.AddRange(continuationResults.Records);
                //        if (string.IsNullOrEmpty(continuationResults.NextRecordsUrl)) break;

                //        //pass nextRecordsUrl back to client.QueryAsync to request next set of records
                //        nextRecordsUrl = continuationResults.NextRecordsUrl;
                //    }
                //}
                //Console.WriteLine("Retrieved accounts = " + accts.Count() + ", expected size = " + totalSize);

                //// Create a sample record

                //Console.WriteLine("Creating test record.");
                //var accountRecord = new PersonAccount() { LastName = "TestAccount" };

                //var createdAccRecord = await client.CreateAsync("Account", accountRecord);
                //Console.WriteLine("await Id return:" + " " + createdAccRecord.Id);
                //if (createdAccRecord == null)
                //{
                //    Console.WriteLine("Failed to create test record.");
                //    return;
                //}

                //Console.WriteLine("Successfully created test record.");

                //// Update the sample record
                //// Shows that anonymous types can be used as well
                //Console.WriteLine("Updating test record.");
                //accountRecord.LastName = "TestUpdate";
                //var success = await client.UpdateAsync("Account", createdAccRecord.Id, accountRecord);
                //if (!string.IsNullOrEmpty(success.Errors.ToString()))
                //{
                //    Console.WriteLine("Failed to update test record!");
                //    return;
                //}

                //Console.WriteLine("Successfully updated the record.");

                //// Retrieve the sample record
                //// How to retrieve a single record if the id is known
                //Console.WriteLine("Retrieving the record by ID.");
                //accountRecord = await client.QueryByIdAsync<PersonAccount>("Account", createdAccRecord.Id);
                //if (accountRecord == null)
                //{
                //    Console.WriteLine("Failed to retrieve the record by ID!");
                //    return;
                //}

                //Console.WriteLine("Retrieved the record by ID.");

                //// Query for record by LastName
                //Console.WriteLine("Querying the record by LastName.");
                //var accounts = await client.QueryAsync<PersonAccount>("SELECT ID, LastName FROM Account WHERE LastName = '" + accountRecord.LastName + "'");
                //accountRecord = accounts.Records.FirstOrDefault();
                //if (accountRecord == null)
                //{
                //    Console.WriteLine("Failed to retrieve account by query!");
                //    return;
                //}

                //Console.WriteLine("Retrieved the record by LastName.");

                //// Delete account
                //Console.WriteLine("Deleting the record by ID.");
                //var deleted = await client.DeleteAsync("Account", createdAccRecord.Id);
                //if (!deleted)
                //{
                //    Console.WriteLine("Failed to delete the record by ID!");
                //    return;
                //}
                //Console.WriteLine("Deleted the record by ID.");

                //// Selecting multiple accounts into a dynamic
                //Console.WriteLine("Querying multiple records.");
                //var dynamicAccounts = await client.QueryAsync<dynamic>("SELECT ID, LastName FROM Account LIMIT 10");
                //foreach (dynamic acct in dynamicAccounts.Records)
                //{
                //    Console.WriteLine("Account - " + acct.LastName);
                //}

                //// Creating parent - child records using a Dynamic
                //Console.WriteLine("Creating a parent record (Account)");
                //dynamic a = new ExpandoObject();
                //a.Name = "Account from .Net Toolkit";
                //var acc = await client.CreateAsync("Account", a);
                //if (acc == null)
                //{
                //    Console.WriteLine("Failed to create parent record.");
                //    return;
                //}

                //Console.WriteLine("Creating a child record (Contact)");
                //dynamic c = new ExpandoObject();
                //c.FirstName = "Joe";
                //c.LastName = "Blow";
                //c.AccountId = acc.Id;
                //var con = await client.CreateAsync("Contact", c);
                //if (con.Id == null)
                //{
                //    Console.WriteLine("Failed to create child record.");
                //    return;
                //}

                //Console.WriteLine("Deleting parent and child");

                //// Delete account (also deletes contact)
                //Console.WriteLine("Deleting the Account by Id.");
                //deleted = await client.DeleteAsync("Account", acc.Id);
                //if (!deleted)
                //{
                //    Console.WriteLine("Failed to delete the record by ID!");
                //    return;
                //}

                //Console.WriteLine("Deleted the Account and Contact.");
            }
            catch (Exception ex)
            {

            }
        }
        */

        private class PersonAccount
        {
            public const String SObjectTypeName = "Account";
            public String Id { get; set; }
            public String LastName { get; set; }
        }

        private const string AUTHORIZE_ENDPOINT = "https://login.salesforce.com/services/oauth2/authorize";
        private const string LOGIN_ENDPOINT = "https://login.salesforce.com/services/oauth2/token";
        private const string API_ENDPOINT = "/services/data/v36.0/";

        public class SalesforceClient1
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string Token { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string AuthToken { get; set; }
            public string InstanceUrl { get; set; }

            public void Authorize()
            {
                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                    String jsonResponse;
                    using (var client = new HttpClient())
                    {
                        var request = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"response_type", "code"},
                        {"client_id", ConsumerKey},
                        {"redirect_uri", "https://elioplus.com/api/callback"}
                    }
                                );

                        request.Headers.Add("X-PrettyPrint", "1");
                        var response = client.PostAsync(AUTHORIZE_ENDPOINT, request).Result;
                        jsonResponse = response.Content.ReadAsStringAsync().Result;
                    }

                    var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                    string AuthorizationCode = values["authorization"];
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            public void Login()
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                String jsonResponse;
                using (var client = new HttpClient())
                {
                    var request = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type", "password"},
                        {"client_id", ClientId},
                        {"client_secret", ClientSecret},
                        {"username", Username},
                        {"password", Password + Token}
                    }
                            );

                    request.Headers.Add("X-PrettyPrint", "1");
                    var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                }

                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                AuthToken = values["access_token"];
                InstanceUrl = values["instance_url"];
            }
            public string Query(string soqlQuery)
            {
                using (var client = new HttpClient())
                {
                    string restRequest = InstanceUrl + API_ENDPOINT + "query/?q=" + soqlQuery;
                    var request = new HttpRequestMessage(HttpMethod.Get, restRequest);
                    request.Headers.Add("Authorization", "Bearer " + AuthToken);
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Headers.Add("X-PrettyPrint", "1");
                    var response = client.SendAsync(request).Result;
                    return response.Content.ReadAsStringAsync().Result;
                }
            }
        }

        public class Account
        {
            public const String SObjectTypeName = "Account";
            public string Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
        }

        public static void Do()
        {
            var client = new SalesforceClient();
            var authFlow = new UsernamePasswordAuthenticationFlow(ConsumerKey, ConsumerSecret, Username, Password);

            try
            {
                client.Authenticate(authFlow);
            }
            catch (SalesforceException ex)
            {
                Console.WriteLine("Authentication failed: {0} : {1}", ex.Error, ex.Message);
            }

            var record = client.FindById<Account>("Account", "<ID>");

            //client.Create("Account", new Account()
            //{ Name = "name created", Description = "description created" }));

            //// Using an anonymous.
            //client.Create("Account", new { Name = "name created", Description = "description created" }));

            //// Using a class. Ever required property should be set.
            //client.Update("Account", "<record id>", new Account()
            //{ Name = "name updated", Description = "description updated" }));

            //// Using an anonymous. Only required properties will be updated.
            //client.Update("Account", "<record id>", new { Description = "description updated" }));

            client.Delete("Account", "<ID>");

            var records = client.Query<Account>("SELECT id, name, description FROM Account");

            foreach (var r in records)
            {
                Console.WriteLine("{0}: {1}", r.Id, r.Name);
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true)]
        public static void ConnectSF()
        {
            try
            {
                var client = new RestClient("https://login.salesforce.com/services/oauth2/token");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                //request.AddHeader("Accept", "*/*");
                //request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["ClearbitSecretKey"].ToString());

                request.AddParameter("username", Username);
                request.AddParameter("password", Password + "YiPJAFQvAN774uHX3clVXbZqA");
                request.AddParameter("grant_type", "password");
                request.AddParameter("client_id", ConsumerKey);
                request.AddParameter("client_secret", ConsumerSecret);

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;

                IRestResponse response = client.Execute(request);

                var model = JsonConvert.DeserializeObject<JObject>(response.Content);
                if (model != null)
                {
                    var dictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
                    JToken access_token = dictionary["access_token"];
                    JToken instance_url = dictionary["instance_url"];
                    JToken id = dictionary["id"];
                    JToken token_type = dictionary["token_type"];
                    JToken issued_at = dictionary["issued_at"];
                    JToken signature = dictionary["signature"];

                }
                else
                {
                    //empty model
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void LoginSF()
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;

                String jsonResponse;
                using (var client = new HttpClient())
                {
                    var request = new FormUrlEncodedContent(new Dictionary<string, string>
                    {
                        {"grant_type", "password"},
                        {"client_id", ConsumerKey},
                        {"client_secret", ConsumerSecret},
                        {"username", Username},
                        {"password", Password + "YiPJAFQvAN774uHX3clVXbZqA"}
                    }
                            );

                    request.Headers.Add("X-PrettyPrint", "1");
                    var response = client.PostAsync(LOGIN_ENDPOINT, request).Result;
                    jsonResponse = response.Content.ReadAsStringAsync().Result;
                }

                var values = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonResponse);
                string authToken = values["access_token"];
                string instanceUrl = values["instance_url"];
                string id = values["id"];
                string token_type = values["token_type"];
                string issued_at = values["issued_at"];
                string signature = values["signature"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //private static async Task RunSample()
        //{
        //    var auth = new AuthenticationClient();

        //    // Authenticate with Salesforce
        //    Console.WriteLine("Authenticating with Salesforce");
        //    var url = IsSandboxUser.Equals("true", StringComparison.CurrentCultureIgnoreCase)
        //        ? "https://test.salesforce.com/services/oauth2/token"
        //        : "https://login.salesforce.com/services/oauth2/token";

        //    await auth.UsernamePasswordAsync(ConsumerKey, ConsumerSecret, Username, Password, url);
        //    Console.WriteLine("Connected to Salesforce");

        //    var client = new ForceClient(auth.InstanceUrl, auth.AccessToken, auth.ApiVersion);

        //    // retrieve all accounts
        //    Console.WriteLine("Get Accounts");

        //    const string qry = "SELECT ID, LastName FROM Account";
        //    var accts = new List<Account>();
        //    var results = await client.QueryAsync<Account>(qry);
        //    var totalSize = results.TotalSize;

        //    Console.WriteLine("Queried " + totalSize + " records.");

        //    accts.AddRange(results.Records);
        //    var nextRecordsUrl = results.NextRecordsUrl;

        //    if (!string.IsNullOrEmpty(nextRecordsUrl))
        //    {
        //        Console.WriteLine("Found nextRecordsUrl.");

        //        while (true)
        //        {
        //            var continuationResults = await client.QueryContinuationAsync<Account>(nextRecordsUrl);
        //            totalSize = continuationResults.TotalSize;
        //            Console.WriteLine("Queried an additional " + totalSize + " records.");

        //            accts.AddRange(continuationResults.Records);
        //            if (string.IsNullOrEmpty(continuationResults.NextRecordsUrl)) break;

        //            //pass nextRecordsUrl back to client.QueryAsync to request next set of records
        //            nextRecordsUrl = continuationResults.NextRecordsUrl;
        //        }
        //    }
        //    Console.WriteLine("Retrieved accounts = " + accts.Count() + ", expected size = " + totalSize);

        //    // Create a sample record

        //    Console.WriteLine("Creating test record.");
        //    var accountRecord = new PersonAccount() { LastName = "TestAccount" };

        //    var createdAccRecord = await client.CreateAsync("Account", accountRecord);
        //    Console.WriteLine("await Id return:" + " " + createdAccRecord.Id);
        //    if (createdAccRecord == null)
        //    {
        //        Console.WriteLine("Failed to create test record.");
        //        return;
        //    }

        //    Console.WriteLine("Successfully created test record.");

        //    // Update the sample record
        //    // Shows that anonymous types can be used as well
        //    Console.WriteLine("Updating test record.");
        //    accountRecord.LastName = "TestUpdate";
        //    var success = await client.UpdateAsync("Account", createdAccRecord.Id, accountRecord);
        //    if (!string.IsNullOrEmpty(success.Errors.ToString()))
        //    {
        //        Console.WriteLine("Failed to update test record!");
        //        return;
        //    }

        //    Console.WriteLine("Successfully updated the record.");

        //    // Retrieve the sample record
        //    // How to retrieve a single record if the id is known
        //    Console.WriteLine("Retrieving the record by ID.");
        //    accountRecord = await client.QueryByIdAsync<PersonAccount>("Account", createdAccRecord.Id);
        //    if (accountRecord == null)
        //    {
        //        Console.WriteLine("Failed to retrieve the record by ID!");
        //        return;
        //    }

        //    Console.WriteLine("Retrieved the record by ID.");

        //    // Query for record by LastName
        //    Console.WriteLine("Querying the record by LastName.");
        //    var accounts = await client.QueryAsync<PersonAccount>("SELECT ID, LastName FROM Account WHERE LastName = '" + accountRecord.LastName + "'");
        //    accountRecord = accounts.Records.FirstOrDefault();
        //    if (accountRecord == null)
        //    {
        //        Console.WriteLine("Failed to retrieve account by query!");
        //        return;
        //    }

        //    Console.WriteLine("Retrieved the record by LastName.");

        //    // Delete account
        //    Console.WriteLine("Deleting the record by ID.");
        //    var deleted = await client.DeleteAsync("Account", createdAccRecord.Id);
        //    if (!deleted)
        //    {
        //        Console.WriteLine("Failed to delete the record by ID!");
        //        return;
        //    }
        //    Console.WriteLine("Deleted the record by ID.");

        //    // Selecting multiple accounts into a dynamic
        //    Console.WriteLine("Querying multiple records.");
        //    var dynamicAccounts = await client.QueryAsync<dynamic>("SELECT ID, LastName FROM Account LIMIT 10");
        //    foreach (dynamic acct in dynamicAccounts.Records)
        //    {
        //        Console.WriteLine("Account - " + acct.LastName);
        //    }

        //    // Creating parent - child records using a Dynamic
        //    Console.WriteLine("Creating a parent record (Account)");
        //    dynamic a = new ExpandoObject();
        //    a.Name = "Account from .Net Toolkit";
        //    var acc = await client.CreateAsync("Account", a);
        //    if (acc == null)
        //    {
        //        Console.WriteLine("Failed to create parent record.");
        //        return;
        //    }

        //    Console.WriteLine("Creating a child record (Contact)");
        //    dynamic c = new ExpandoObject();
        //    c.FirstName = "Joe";
        //    c.LastName = "Blow";
        //    c.AccountId = acc.Id;
        //    var con = await client.CreateAsync("Contact", c);
        //    if (con.Id == null)
        //    {
        //        Console.WriteLine("Failed to create child record.");
        //        return;
        //    }

        //    Console.WriteLine("Deleting parent and child");

        //    // Delete account (also deletes contact)
        //    Console.WriteLine("Deleting the Account by Id.");
        //    deleted = await client.DeleteAsync("Account", acc.Id);
        //    if (!deleted)
        //    {
        //        Console.WriteLine("Failed to delete the record by ID!");
        //        return;
        //    }

        //    Console.WriteLine("Deleted the Account and Contact.");
        //}
    }
}