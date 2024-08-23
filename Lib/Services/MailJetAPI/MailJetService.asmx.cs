using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using Mailjet.Client;
using Mailjet.Client.Resources;
using System.Threading.Tasks;
using System.Web.Script.Services;

namespace WdS.ElioPlus.Lib.Services.MailJetAPI
{
    /// <summary>
    /// Summary description for MailJetService
    /// </summary>
    [WebService(Namespace = "https://api.mailjet.com/v3.1/send")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MailJetService : System.Web.Services.WebService
    {
        [WebMethod(Description = "The ticket service")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public async System.Threading.Tasks.Task SendEmail(String fromEmail, String nameTo, String toEmail, String nameFrom)
        {
            System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
            HttpContext.Current.Response.Clear();

            MailjetClient client = new MailjetClient("29ae3496f098909763b1972a1b45e4ba", "3404d1436d08ec3765ce35446e2ce951")
            {
                Version = ApiVersion.V3_1,
            };
            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }
               .Property(Send.Messages, new JArray {
                new JObject {
                 {"From", new JObject {
                  {"Email", fromEmail},
                  {"Name", nameFrom}
                  }},
                 {"To", new JArray {
                  new JObject {
                   {"Email", toEmail},
                   {"Name", nameTo}
                   }
                  }},
                 {"Subject", "Your test email plan!"},
                 {"TextPart", "Dear vag, welcome to Mailjet! May the delivery force be with you!"},
                 {"HTMLPart", "<h3>Dear passenger 1, welcome to Mailjet!</h3><br />May the delivery force be with you!"}
                 }
               });

            var req = JsonConvert.SerializeObject(request);
            HttpContext.Current.Response.BinaryWrite(encoding.GetBytes(req));

            MailjetResponse response = await client.PostAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = JsonConvert.SerializeObject(response);
                HttpContext.Current.Response.BinaryWrite(encoding.GetBytes(json));

                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(response.GetData());
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }
        }

        [WebMethod(Description = "The ticket service")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetTicket(String appName, String version, String user, String pwd, int role)
        {
            DateTime StartExecTime = DateTime.Now;
            try
            {
                System.Text.UTF8Encoding encoding = new System.Text.UTF8Encoding();
                HttpContext.Current.Response.Clear();
                string ticket = "";//createTicket(appName, version, user, pwd, role);
                //HttpContext.Current.Response.BinaryWrite(encoding.GetBytes(ticket));
                return ticket;
            } 
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod(Description = "The ticket service")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static void MakeFirstRequest()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["ApiKey"].ToString() + ":" + ConfigurationManager.AppSettings["ApiSecretKey"].ToString() + "https://api.mailjet.com/v3.1/send");

            var request = new RestRequest("v1/products", Method.POST);
            //request.AddParameter("Content-Type: application", "json");
            //request.AddParameter("type", type);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());
        }

        [WebMethod(Description = "The ticket service")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static async System.Threading.Tasks.Task CreateRunAsync()
        {
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

            MailjetRequest request = new MailjetRequest()
            {
                Resource = Mailjet.Client.Resources.Contact.Resource,
            }
            .Property(Mailjet.Client.Resources.Contact.Email, "Mister@mailjet.com");

            MailjetResponse response = await client.PostAsync(request);

            Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }

        [WebMethod(Description = "The ticket service")]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public static async System.Threading.Tasks.Task SendRunAsync(string emailFrom, string emailName, string recipientEmail)
        {
            MailjetClient client = new MailjetClient(ConfigurationManager.AppSettings["MailJetApiPublicKey"], ConfigurationManager.AppSettings["MailJetApiSecretKey"]);

            MailjetRequest request = new MailjetRequest
            {
                Resource = Send.Resource,
            }

            .Property(Send.FromEmail, emailFrom)
            .Property(Send.FromName, emailName)
            .Property(Send.Subject, "Your email flight plan!")
            .Property(Send.TextPart, "Dear passenger, welcome to Mailjet! May the delivery force be with you!")
            .Property(Send.HtmlPart, "<h3>Dear passenger, welcome to Mailjet!</h3><br />May the delivery force be with you!")
            .Property(Send.Recipients, new JArray {
                new JObject {
                 {"Email", recipientEmail}
                 }
                });

            MailjetResponse response = await client.PostAsync(request);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine(string.Format("Total: {0}, Count: {1}\n", response.GetTotal(), response.GetCount()));
                Console.WriteLine(response.GetData());
            }
            else
            {
                Console.WriteLine(string.Format("StatusCode: {0}\n", response.StatusCode));
                Console.WriteLine(string.Format("ErrorInfo: {0}\n", response.GetErrorInfo()));
                Console.WriteLine(string.Format("ErrorMessage: {0}\n", response.GetErrorMessage()));
            }

            Console.ReadLine();
        }
    }
}
