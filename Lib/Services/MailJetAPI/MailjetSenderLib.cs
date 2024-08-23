using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus.Lib.Services.MailJetAPI
{
    public class MailjetSenderLib
    {
        public string error;
        public int status;

        public static async System.Threading.Tasks.Task ListAllContactsRunAsync()
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["apiKey"], ConfigurationManager.AppSettings["apiSecret"]);

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Mailjet.Client.Resources.Contact.Resource,
                };

                MailjetResponse response = await client.GetAsync(request);

                //var clnt = new RestClient("https://api.mailjet.com/v3.1/");

                //var req = new RestRequest("send/", Method.POST);
                //req.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["MailJetApiPublicKey"].ToString() + ":" + ConfigurationManager.AppSettings["MailJetApiSecretKey"].ToString());
                //req.AddParameter("Content-Type: application/json", "");
                //IRestResponse res = clnt.Execute(req);

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());

                    int contactsCount = response.GetCount();

                    //var contactsList = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(res.Content);
                }
                else
                {
                    Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                    Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                    Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task CreateContactRunAsync(string newEmailAddress)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

                MailjetRequest request = new MailjetRequest()
                {
                    Resource = Mailjet.Client.Resources.Contact.Resource,
                }
                .Property(Mailjet.Client.Resources.Contact.Email, newEmailAddress);

                MailjetResponse response = await client.PostAsync(request);

                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    throw new Exception(string.Format("ErrorInfo: {0}\n ErrorMessage: {1}\n", response.GetErrorInfo(), response.GetErrorMessage()));
                }

                Console.ReadLine();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void UpdateContactResource(string emailAddress, string emailName)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Mailjet.Client.Resources.Contact.Resource,
                    ResourceId = ResourceId.Alphanumeric(emailAddress),
                    Body = new JObject 
                    {
                        {
                            Mailjet.Client.Resources.Contact.Name, emailName 
                        },
                    },
                };

                MailjetResponse response = client.PutAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    throw new Exception(string.Format("StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}\n", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task ViewResourceRunAsync(string emailAddress)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

                MailjetRequest request = new MailjetRequest()
                {
                    Resource = Mailjet.Client.Resources.Contact.Resource,
                    ResourceId = ResourceId.Alphanumeric(emailAddress),
                };

                MailjetResponse response = await client.GetAsync(request);

                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    throw new Exception(string.Format("ErrorInfo: {0}\n ErrorMessage: {1}\n", response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void DeleteResource(string emailAddress)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

                MailjetRequest request = new MailjetRequest
                {
                    Resource = Listrecipient.Resource,
                    ResourceId = ResourceId.Alphanumeric(emailAddress),
                };

                MailjetResponse response = client.DeleteAsync(request).Result;

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Console.WriteLine(response.GetData());
                }
                else
                {
                    throw new Exception(string.Format("ErrorInfo: {0}\n ErrorMessage: {1}\n", response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task SendEmailRunAsync(string fromEmailAddress, string fromEmailName, string toEmailAddress, string toEmailName, string subject, string bodyTextPart, string bodyHtmlPart)
        {
            try
            {
                //MailjetClient client = new MailjetClient("29ae3496f098909763b1972a1b45e4ba", "3404d1436d08ec3765ce35446e2ce951")
                
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"].ToString(), ConfigurationManager.AppSettings["MailJetApiSecretKey"].ToString())
                {
                    Version = ApiVersion.V3_1,
                };
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource,
                }
                   .Property(Send.Messages, new JArray 
               {
                    new JObject {{ "From", new JObject {{"Email", fromEmailAddress}, {"Name", fromEmailName}}},
                                { "To", 
                                    new JArray { new JObject {{"Email", toEmailAddress}, {"Name", toEmailName}}}},
                            { "Subject", subject},
                            {"TextPart", bodyTextPart},{"HTMLPart", bodyHtmlPart}}});

                MailjetResponse response = await client.PostAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    Logger.Debug("mailjetSenderLib.cs --> Success Send MESSAGE at " + DateTime.Now.ToString() + ". FROM: " + fromEmailAddress + " TO: " + toEmailAddress);
                }
                else
                {
                    Logger.DetailedError("mailjetSenderLib.cs --> IsSuccessStatusCode = false at " + DateTime.Now.ToString(), string.Format("StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}\n", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage()));
                    throw new Exception(string.Format("StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}\n ErrorData: {3}\n", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage(), response.GetData()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task CreateNewSenderEmailAddressRunAsync(string senderEmailAddress, string senderName, bool isDefaultSender = false)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"])
                {
                    Version = ApiVersion.V3_1
                };

                MailjetRequest request = new MailjetRequest()
                {
                    Resource = Sender.Resource,
                }
                 .Property(Sender.EmailType, "unknown")
                 .Property(Sender.IsDefaultSender, isDefaultSender)
                 .Property(Sender.Name, senderName)
                 .Property(Sender.Email, senderEmailAddress);

                MailjetResponse response = await client.PostAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    Logger.Debug("mailjetSenderLib.cs --> Success Create New Sender Email Address MESSAGE at " + DateTime.Now.ToString() + ". SENDER NAME: " + senderName + " WITH EMAIL ADDRESS: " + senderEmailAddress + "\n" + string.Format("StatusCode: {0}\n", response.StatusCode));
                    Logger.Debug(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Logger.Debug(string.Format("Response Data: " + response.GetData()));
                }
                else
                {
                    Logger.DetailedError("mailjetSenderLib.cs --> CreateNewSenderEmailAddressRunAsync --> IsSuccessStatusCode = false at " + DateTime.Now.ToString(), string.Format("ErrorData: {0}\n", response.GetData()));
                    throw new Exception(string.Format("CreateNewSenderEmailAddressRunAsync StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task CreateDNSRecordRunAsync(long resourceID)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"])
                {
                    Version = ApiVersion.V3_1
                };

                MailjetRequest request = new MailjetRequest()
                {
                    Resource = Dns.Resource,
                    ResourceId = ResourceId.Numeric(resourceID)
                };
                
                MailjetResponse response = await client.PostAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    Logger.Debug("mailjetSenderLib.cs --> Success Create DNS Record MESSAGE at " + DateTime.Now.ToString() + ". Resource ID: " + resourceID.ToString() + ". " + string.Format("StatusCode: {0}\n", response.StatusCode));
                    Logger.Debug(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Logger.Debug(string.Format("Response Data: " + response.GetData()));
                }
                else
                {
                    Logger.DetailedError("mailjetSenderLib.cs --> CreateDNSRecordRunAsync --> IsSuccessStatusCode = false at " + DateTime.Now.ToString(), string.Format("ErrorData: {0}\n", response.GetData()));
                    throw new Exception(string.Format("CreateDNSRecordRunAsync StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async System.Threading.Tasks.Task DNSCheckRunAsync(long resourceID)
        {
            try
            {
                MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"])
                {
                    Version = ApiVersion.V3_1
                };

                MailjetRequest request = new MailjetRequest()
                {
                    Resource = DnsCheck.Resource,
                    ResourceId = ResourceId.Numeric(resourceID)
                };

                MailjetResponse response = await client.PostAsync(request);


                if (response.IsSuccessStatusCode)
                {
                    Logger.Debug("mailjetSenderLib.cs --> Success DNS Check MESSAGE at " + DateTime.Now.ToString() + ". Resource ID: " + resourceID.ToString() + ". " + string.Format("StatusCode: {0}\n", response.StatusCode));
                    Logger.Debug(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                    Logger.Debug(string.Format("Response Data: " + response.GetData()));
                }
                else
                {
                    Logger.DetailedError("mailjetSenderLib.cs --> DNSCheckRunAsync --> IsSuccessStatusCode = false at " + DateTime.Now.ToString(), string.Format("ErrorData: {0}\n", response.GetData()));
                    throw new Exception(string.Format("DNSCheckRunAsync StatusCode: {0}\n ErrorInfo: {1}\n ErrorMessage: {2}", response.StatusCode, response.GetErrorInfo(), response.GetErrorMessage()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static class StatusCode
        {
            public const int OK = 200;
            public const int Created = 201;
            public const int NoContent = 204;
            public const int NotModified = 304;
            public const int BadRequest = 400;
            public const int Unauthorized = 401;
            public const int Forbidden = 403;
            public const int NotFound = 404;
            public const int MethodNotAllowed = 405;
            public const int TooManyRequests = 429;
            public const int InternalServerError = 500;
        }
    }
}