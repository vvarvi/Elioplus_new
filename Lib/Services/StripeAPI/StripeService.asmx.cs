using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using ServiceStack.Stripe;
using ServiceStack.Stripe.Types;
using Stripe;
using Stripe.Checkout;
using Stripe.Issuing;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Lib.Services.StripeAPI
{
    /// <summary>
    /// Summary description for StripeService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class StripeService : System.Web.Services.WebService
    {
        //const string ApiKey = "sk_test_OIySeuUvaObscVisQhrHkhix";

        [WebMethod]
        public static Session CreateCheckoutSession(string accountID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string>
                  {
                    "card",
                  },
                LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                          {
                            Currency = "eur",
                            UnitAmount = 150,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                              Name = "Lead",
                              Description = "New won Lead",
                            },
                          },

                      //Name = "New Lead won",
                      //Amount = 1500,
                      //Currency = "eur",
                      Quantity = 1,
                    },
                  },
                //PaymentIntentData = new SessionPaymentIntentDataOptions
                //{
                //    ApplicationFeeAmount = 123,
                //},
                Mode = "payment",
                SuccessUrl = "https://elioplus.com/success",
                CancelUrl = "https://example.com/cancel",
            };

            var requestOptions = new RequestOptions
            {
                StripeAccount = accountID,
            };

            var service = new SessionService();
            Session session = service.Create(options, requestOptions);

            return session;
            // 303 redirect to session.Url
        }

        [WebMethod]
        public static PaymentLink CreateDestinationBehalfOnPaymentLink(string accountID, string priceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkCreateOptions
            {
                LineItems = new List<PaymentLinkLineItemOptions>
                {
                    new PaymentLinkLineItemOptions
                    {
                        Price = priceID,
                        Quantity = 1,
                    },
                },
                OnBehalfOf = accountID,
                TransferData = new PaymentLinkTransferDataOptions
                {
                    Destination = accountID,
                },
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;

            var service = new PaymentLinkService();
            var paymentLink = service.Create(options, requestOptions);

            return paymentLink;
        }

        [WebMethod]
        public static PaymentLink CreateDestinationPaymentLink(string accountID, string priceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkCreateOptions
            {
                LineItems = new List<PaymentLinkLineItemOptions>
                {
                    new PaymentLinkLineItemOptions
                    {
                        Price = priceID,
                        Quantity = 1,
                    },
                },

                TransferData = new PaymentLinkTransferDataOptions
                {
                    Destination = accountID,
                },
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;

            var service = new PaymentLinkService();
            var paymentLink = service.Create(options, requestOptions);

            return paymentLink;
        }

        [WebMethod]
        public static PaymentLink CreateDirectPaymentLink(string accountID, string priceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkCreateOptions
            {
                LineItems = new List<PaymentLinkLineItemOptions>
                {
                    new PaymentLinkLineItemOptions
                    {
                        Price = priceID,
                        Quantity = 1,
                    },
                },
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;

            var service = new PaymentLinkService();
            var paymentLink = service.Create(options, requestOptions);

            return paymentLink;
        }

        [WebMethod]
        public static PaymentLink CreatePaymentLink(string customerID, string priceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkCreateOptions
            {
                LineItems = new List<PaymentLinkLineItemOptions>
                {
                    new PaymentLinkLineItemOptions
                    {
                        Price = priceID,
                        Quantity = 1,                        
                    },
                },
                
                AfterCompletion = new PaymentLinkAfterCompletionOptions
                {
                    Type = "redirect",
                    Redirect = new PaymentLinkAfterCompletionRedirectOptions
                    {
                        Url = "https://elioplus.com/successful-registration",
                    },
                    
                },

                AllowPromotionCodes = true,
            };

            var service = new PaymentLinkService();
            var paymentLink = service.Create(options, null);

            return paymentLink;
        }

        [WebMethod]
        public static PaymentIntentService CreateDirectPaymentService(string accountID, long amount, long? feeAmount, string currencyISO)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new PaymentIntentService();
            var createOptions = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = currencyISO,
                ApplicationFeeAmount = feeAmount,
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;
            service.Create(createOptions, requestOptions);

            return service;
        }

        [WebMethod]
        public static Charge CreateDirectCharge(string receiptEmail, long amount, long? feeAmount, string currencyISO, string cardSource, string accountID, string customerId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new ChargeService();
            var createOptions = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = currencyISO,
                Source = cardSource,
                ReceiptEmail = receiptEmail,
                ApplicationFeeAmount = (feeAmount / 100) * amount,
                Customer = customerId
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;

            Charge s = service.Create(createOptions, requestOptions);

            return s;
        }

        [WebMethod]
        public static PaymentIntentService CreateDestinationCharge(string accountID, long amount, long? feeAmount, string currencyISO)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var createOptions = new PaymentIntentCreateOptions
            {
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },

                Amount = amount,
                Currency = currencyISO,
                OnBehalfOf = accountID
            };

            var service = new PaymentIntentService();
            service.Create(createOptions);

            return service;
        }

        [WebMethod]
        public static AccountService DeleteAccountNew(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new AccountService();
            service.Delete(accountId);

            return service;
        }

        [WebMethod]
        public static AccountService UpdateAccountNew()
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountUpdateOptions
            {
                BusinessType = "company",

                BusinessProfile = new AccountBusinessProfileOptions
                {
                    SupportEmail = "info@elioplus.com",
                    ProductDescription = "Lead generation platform that offers also SaaS tools for vendors to manage their channel netwrok.",
                    Name = "Elioplus, Inc.",
                    Url = "elioplus.com",
                    SupportPhone = "+15105581230",

                    SupportAddress = new AddressOptions
                    {
                        City = "Wilmington",
                        Country = "US",
                        PostalCode = "19810",
                        Line1 = "108 West 13th Street, Suite 105",
                        State = "DE"
                    }
                },

                //Capabilities = new AccountCapabilitiesOptions
                //{
                //    CardPayments = new AccountCapabilitiesCardPaymentsOptions
                //    {
                //        Requested = true,
                //    }
                //    //Transfers = new AccountCapabilitiesTransfersOptions
                //    //{
                //    //    Requested = true,
                //    //},
                //},

                Settings = new AccountSettingsOptions
                {
                    CardPayments = new AccountSettingsCardPaymentsOptions
                    {
                        DeclineOn = new AccountSettingsCardPaymentsDeclineOnOptions
                        {
                            AvsFailure = false,
                            CvcFailure = false
                        }
                    },

                    Payments = new AccountSettingsPaymentsOptions
                    {
                        StatementDescriptor = "ELIOPLUS.COM"
                    },

                    Payouts = new AccountSettingsPayoutsOptions
                    {
                        Schedule = new AccountSettingsPayoutsScheduleOptions
                        {
                            DelayDays = "2",
                            Interval = "daily"
                        }
                    }
                }
            };
            var service = new AccountService();
            service.Update("acct_1LxxdBPL5s2Sz2q7", options);

            //var options = new AccountCreateOptions
            //{
            //    Type = "standard",
            //    Country = countryISO,
            //    Email = "jenny.rosen@example.com",
            //    //Company = new AccountCompanyOptions
            //    //{
            //    //    Address = new AddressOptions
            //    //    {
            //    //        Country = "",
            //    //        City = "",
            //    //        State = "",
            //    //    },

            //    //    Name = "",
            //    //    Phone = ""
            //    //},

            //    BusinessProfile = new AccountBusinessProfileOptions
            //    {
            //        Name = "Elioplus, Inc.",
            //        ProductDescription = "Lead generation platform that offers also SaaS tools for vendors to manage their channel netwrok.",
            //        SupportPhone = "+15105581230",
            //        SupportEmail = "info@elioplus.com",
            //        Url = "elioplus.com",

            //        SupportAddress = new AddressOptions
            //        {
            //            State = "DE",
            //            City = "Wilmington",
            //            Country = "US",
            //            PostalCode = "19810",
            //            Line1 = "3511 Silverside Road, Suite 105"
            //        }
            //    }
            //};

            //var service = new AccountService();
            //service.Create(options);

            return service;
        }

        [WebMethod]
        public static AccountService UpdateAggreementAccountNew()
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountUpdateOptions
            {
                TosAcceptance = new AccountTosAcceptanceOptions
                {
                    Date = DateTimeOffset.FromUnixTimeSeconds(1609798905).UtcDateTime,
                    Ip = HttpContext.Current.Request.ServerVariables["remote_addr"],
                },
            };

            var service = new AccountService();

            service.Update("acct_1LzmIZSJaBITq7dN", options);

            return service;
        }

        [WebMethod]
        public static void UploadDocToAccountNew()
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var filename = "/images/success.png";
            using (FileStream stream = System.IO.File.Open(filename, FileMode.Open))
            {
                var options = new FileCreateOptions
                {
                    File = stream,
                    Purpose = FilePurpose.IdentityDocument,
                };
                var service = new FileService();
                var upload = service.Create(options);

                string token = upload.Id;

                AttachDocToAccountNew(token);
            }
        }

        [WebMethod]
        public static AccountService AttachDocToAccountNew(string fileToken)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountUpdateOptions
            {
                Company = new AccountCompanyOptions
                {
                    Verification = new AccountCompanyVerificationOptions
                    {
                        Document = new AccountCompanyVerificationDocumentOptions
                        {
                            Front = fileToken,
                        },
                    },
                },
            };

            var service = new AccountService();

            service.Update("acct_1Lzn6oPA3rixMjb8", options);

            return service;
        }

        [WebMethod]
        public static AccountService CreateAccountNew(ElioUsers user, string countryISO, string accountType)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountCreateOptions
            {
                Type = "standard",
                Country = "GR",
                Email = user.Email,

                BusinessType = "company",
                //Capabilities = new AccountCapabilitiesOptions
                //{
                //    CardPayments = new AccountCapabilitiesCardPaymentsOptions
                //    {
                //        Requested = true
                //    },

                //    BankTransferPayments = new AccountCapabilitiesBankTransferPaymentsOptions
                //    {
                //        Requested = true
                //    }

                //},

                Company = new AccountCompanyOptions
                {
                    Address = new AddressOptions
                    {
                        Country = "GR",
                        City = user.City,
                        State = user.State,
                    },

                    Name = user.CompanyName,
                    //Verification = new AccountCompanyVerificationOptions
                    //{
                    //    Document = new AccountCompanyVerificationDocumentOptions
                    //    {
                    //        Back = "",
                    //        Front = ""
                    //    }
                    //}
                    //Phone = "9118008140578"
                },

                //Capabilities = new AccountCapabilitiesOptions
                //{
                //    CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                //    Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                //},

                //TosAcceptance = new AccountTosAcceptanceOptions
                //{
                //    ServiceAgreement = "full",
                //    Date = DateTime.Now,    // DateTimeOffset.FromUnixTimeSeconds(1609798905).UtcDateTime,
                //    Ip = HttpContext.Current.Request.ServerVariables["remote_addr"],
                //},
                BusinessProfile = new AccountBusinessProfileOptions
                {
                    Name = user.CompanyName,
                    ProductDescription = "Lead generation platform that offers also SaaS tools for vendors to manage their channel netwrok.",
                    SupportPhone = user.Phone,
                    SupportEmail = "info@peoplecert.org",
                    Url = user.WebSite,

                    SupportAddress = new AddressOptions
                    {
                        State = user.State,
                        City = user.City,
                        Country = "GR",
                        PostalCode = "15342",
                        Line1 = user.Address
                    }
                },

                DefaultCurrency = "USD",

                Settings = new AccountSettingsOptions
                {
                    CardPayments = new AccountSettingsCardPaymentsOptions
                    {
                        DeclineOn = new AccountSettingsCardPaymentsDeclineOnOptions
                        {
                            AvsFailure = false,
                            CvcFailure = false
                        }
                    },

                    Payments = new AccountSettingsPaymentsOptions
                    {
                        StatementDescriptor = "PEOPLECERT HELLAS"
                    },

                    //Payouts = new AccountSettingsPayoutsOptions
                    //{
                    //    Schedule = new AccountSettingsPayoutsScheduleOptions
                    //    {
                    //        DelayDays = "3",
                    //        Interval = "daily"                            
                    //    }
                    //}                    
                }
            };

            var service = new AccountService();
            service.Create(options);

            return service;
        }

        [WebMethod]
        public static Stripe.Account CreateStandardAccountNew(ElioUsers user, string countryISO, string accountType)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountCreateOptions
            {
                Type = "standard",
                Country = "US",
                Email = user.Email,
                BusinessType = "company",
            };

            AccountService service = new AccountService();
            Stripe.Account acc = service.Create(options);

            return acc;
        }

        [WebMethod]
        public static AccountLink CreateAccountLinkNew(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new AccountLinkCreateOptions
            {
                Account = accountId,
                RefreshUrl = "https://elioplus.com/home",
                ReturnUrl = "https://elioplus.com/login",
                Type = "account_onboarding",
            };

            //var requestOptions = new RequestOptions();
            //requestOptions.StripeAccount = "acct_1M4RHKPKiaS6v4n7";

            var service = new AccountLinkService();

            AccountLink accLink = service.Create(options);

            return accLink;
        }

        [WebMethod]
        public static string CreateAccountNew2(string companyName, string accountType, string countryISO, string email, string businessType)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/accounts", Method.POST);
            request.AddParameter("type", accountType);
            request.AddParameter("business_type", businessType);
            request.AddParameter("country", countryISO);
            request.AddParameter("email", email);
            request.AddParameter("name", companyName);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            var accountDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            if (accountDictionary != null)
            {
                JToken obj = accountDictionary["object"];
                JToken id = accountDictionary["id"];
                JToken business_type = accountDictionary["business_type"];
                JToken country = accountDictionary["country"];
                JToken created = accountDictionary["created"];
                JToken companyEmail = accountDictionary["email"];

                string idKey = id.ToString();

                return idKey;
            }
            else
                return "";
        }

        [WebMethod]
        public static string CreateAccountLinkNew2(string accountId, string returnUrl, string refreshUrl)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/account_links", Method.POST);
            request.AddParameter("account", accountId);
            request.AddParameter("return_url", "https://localhost:44343/login");
            request.AddParameter("refresh_url", "https://localhost:44343/home");
            request.AddParameter("type", "account_onboarding");

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            var accountLinkDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            if (accountLinkDictionary != null)
            {
                JToken obj = accountLinkDictionary["object"];
                JToken created = accountLinkDictionary["created"];
                JToken expires_at = accountLinkDictionary["expires_at"];
                JToken url = accountLinkDictionary["url"];

                string urlKey = url.ToString();

                return urlKey;
            }
            else
                return "";
        }

        [WebMethod]
        public static StripeProduct CreateProduct(string name, string type)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/products", Method.POST);
            request.AddParameter("name", name);
            request.AddParameter("type", type);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeProduct product = JsonConvert.DeserializeObject<StripeProduct>(response.Content);

            return product;
        }

        [WebMethod]
        public static StripeProduct GetProduct(string productID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/products/" + productID, Method.GET);
            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeProduct product = JsonConvert.DeserializeObject<StripeProduct>(response.Content);

            return product;
        }

        [WebMethod]
        public static StripeProduct UpdateProduct(string productID, bool active, string name, string description)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/products/" + productID, Method.POST);

            request.AddParameter("active", active);

            if (!string.IsNullOrEmpty(name))
                request.AddParameter("name", name);
            if (!string.IsNullOrEmpty(description))
                request.AddParameter("description", description);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeProduct product = JsonConvert.DeserializeObject<StripeProduct>(response.Content);

            return product;
        }

        [WebMethod]
        public static List<StripeProduct> GetAllProducts(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/products?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            List<StripeProduct> products = JsonConvert.DeserializeObject<List<StripeProduct>>(response.Content);

            return products;
        }

        [WebMethod]
        public static StripeProduct DeleteProduct(string productID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/products/" + productID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeProduct product = JsonConvert.DeserializeObject<StripeProduct>(response.Content);

            return product;
        }

        [WebMethod]
        public static Customer CreateCustomerToAccountNew(ElioUsers customer, string accountID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerCreateOptions
                {
                    Email = customer.Email,
                    Description = customer.CompanyName,
                    Phone = customer.Phone
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountID;

                var service = new CustomerService();
                Customer cust = service.Create(options, requestOptions);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Cardholder CreateCardHolderNew(ElioUsers user)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CardholderCreateOptions
                {
                    Type = "company",
                    Name = user.CompanyName,
                    Email = user.Email,
                    PhoneNumber = user.Phone,

                    Billing = new CardholderBillingOptions
                    {
                        Address = new AddressOptions
                        {
                            Line1 = "1234 Main Street",
                            City = "San Francisco",
                            State = "CA",
                            Country = "US",
                            PostalCode = "94111",
                        },
                    }
                };

                var service = new CardholderService();
                Cardholder card = service.Create(options);

                return card;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer CreateCustomerNew(string email, string companyName, string phone)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Description = companyName,
                    Phone = phone
                };
                                
                var service = new CustomerService();
                Customer cust = service.Create(options);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer GetCustomerNew(string customerID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new CustomerService();
                Customer customer = service.Get(customerID);

                //Customer cust = JsonConvert.DeserializeObject<Customer>(customer.StripeResponse.Content);
                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripeCustomer CreateCustomer(string description, string email, int accountBalance)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");
                //emailAddress = "uwe.richter@stp-online.de";
                var request = new RestRequest("v1/customers", Method.POST);
                request.AddParameter("description", description);
                request.AddParameter("email", email);
                request.AddParameter("account_balance", accountBalance);
                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                StripeCustomer customer = JsonConvert.DeserializeObject<StripeCustomer>(response.Content);

                return customer;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripeCustomer GetCustomer(string customerID)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/customers/" + customerID, Method.GET);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                StripeCustomer customer = JsonConvert.DeserializeObject<StripeCustomer>(response.Content);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer GetCustomerOld(string customerID)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/customers/" + customerID, Method.GET);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                Customer customer = JsonConvert.DeserializeObject<Customer>(response.Content);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripeCustomer UpdateCustomer(string customerID, string description, int account_balance, string coupon, string default_source, string email, string source)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/customers/" + customerID, Method.POST);

                if (!string.IsNullOrEmpty(description))
                    request.AddParameter("description", description);
                if (account_balance > 0)
                    request.AddParameter("account_balance", account_balance);
                if (!string.IsNullOrEmpty(coupon))
                    request.AddParameter("coupon", coupon);
                if (!string.IsNullOrEmpty(default_source))
                    request.AddParameter("default_source", default_source);
                if (!string.IsNullOrEmpty(email))
                    request.AddParameter("email", email);
                if (!string.IsNullOrEmpty(source))
                    request.AddParameter("source", source);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                StripeCustomer customer = JsonConvert.DeserializeObject<StripeCustomer>(response.Content);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static List<StripeCustomer> GetAllCustomers(int limit)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/customers?limit=" + limit, Method.GET);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                List<StripeCustomer> customers = JsonConvert.DeserializeObject<List<StripeCustomer>>(response.Content);

                return customers;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripeCustomer DeleteCustomer(string customerID)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/customers/" + customerID, Method.DELETE);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                StripeCustomer customer = JsonConvert.DeserializeObject<StripeCustomer>(response.Content);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripePlan CreatePlan(string currency, string interval, string product, string nickname, int amount)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/plans", Method.POST);
            request.AddParameter("currency", currency);
            request.AddParameter("interval", interval);
            request.AddParameter("product", product);
            request.AddParameter("nickname", nickname);
            request.AddParameter("amount", amount);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripePlan plan = JsonConvert.DeserializeObject<StripePlan>(response.Content);

            return plan;
        }
                
        [WebMethod]
        public static StripePlan GetPlan(string planID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/plans/" + planID, Method.POST);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripePlan plan = JsonConvert.DeserializeObject<StripePlan>(response.Content);

            return plan;
        }

        [WebMethod]
        public static StripePlan UpdatePlan(string planID, string nickName)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/plans/" + planID, Method.POST);
            request.AddParameter("nickname", nickName);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripePlan plan = JsonConvert.DeserializeObject<StripePlan>(response.Content);

            return plan;
        }

        [WebMethod]
        public static List<StripePlan> GetAllPlans(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/plans?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(response.Content);
            List<StripePlan> plans = new List<StripePlan>();

            //Dictionary<StripePlan, IEnumerable<StripePlan>> customerDictionary = JsonConvert.DeserializeObject<Dictionary<StripePlan, IEnumerable<StripePlan>>>(response.Content);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);
            //List<StripePlan> plans = (List<StripePlan>)JsonConvert.DeserializeObject(response.Content, typeof(List<StripePlan>));
            //List<StripePlan> plans = JsonConvert.DeserializeObject<List<StripePlan>>(response.Content) as List<StripePlan>;
            //var result = JsonConvert.DeserializeObject<List<Dictionary<string, Dictionary<string, string>>>>(response.Content);

            //StripePlan plans = JsonConvert.DeserializeObject<StripePlan>(response.Content);

            return plans;
        }

        [WebMethod]
        public static StripePlan DeletePlan(string planID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/plans/" + planID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripePlan plan = JsonConvert.DeserializeObject<StripePlan>(response.Content);

            return plan;
        }

        [WebMethod]
        public static StripeSubscription CreateExistingCustomerSubscriptionFull(string customerID, decimal applicationFeePercent, string billing, DateTime billingCycleAnchor, bool cancelAtPeriodEnd, string coupon, int daysUntilDue, string defaultSource, bool prorate, decimal taxPercent, DateTime trialEnd, bool trialFromPlan, int trialPeriodDays)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.POST);
            request.AddParameter("customer", customerID);

            if (applicationFeePercent > 0)
                request.AddParameter("application_fee_percent", applicationFeePercent);
            if (!string.IsNullOrEmpty(billing))
                request.AddParameter("billing", billing);
            if (billingCycleAnchor != null)
                request.AddParameter("billing_cycle_anchor", billingCycleAnchor);

            request.AddParameter("cancel_at_period_end", cancelAtPeriodEnd);

            if (coupon != "")
                request.AddParameter("coupon", coupon);
            if (daysUntilDue > 0)
                request.AddParameter("days_until_due", daysUntilDue);
            if (defaultSource != null && defaultSource.ToString() != "")
                request.AddParameter("default_source", defaultSource);

            request.AddParameter("prorate", prorate);

            if (taxPercent > 0)
                request.AddParameter("tax_percent", taxPercent);
            if (trialEnd != null)
                request.AddParameter("trial_end", trialEnd);

            request.AddParameter("trial_from_plan", trialFromPlan);

            if (trialPeriodDays > 0)
                request.AddParameter("trial_period_days", trialPeriodDays);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static StripeSubscription CreateExistingCustomerSubscriptionToOneTimePrice(string customerID, string planID, string coupon)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.POST);
            request.AddParameter("customer", customerID);
            request.AddParameter("items[0][price]", planID);

            if (coupon != "")
                request.AddParameter("coupon", coupon);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static Subscription CreateExistingCustomerSubscriptionToPlanNew(string customerID, string planID, string coupon)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new SubscriptionCreateOptions
            {
                Customer = customerID,
                Coupon = coupon,
                Items = new List<SubscriptionItemOptions>
                  {
                    new SubscriptionItemOptions
                    {
                      Price = planID,
                    },
                  },
            };

            var service = new SubscriptionService();
            return service.Create(options);
        }

        [WebMethod]
        public static StripeSubscription CreateExistingCustomerSubscriptionToPlan(string customerID, string planID, string coupon)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.POST);
            request.AddParameter("customer", customerID);
            request.AddParameter("items[0][plan]", planID);

            if (coupon != "")
                request.AddParameter("coupon", coupon);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static StripeSubscription CreateExistingCustomerSubscription(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.POST);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static Subscription GetSubscriptionNew(string subscriptionID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new SubscriptionService();
                Subscription subscription = service.Get(subscriptionID);

                return subscription;

            }
            catch (Exception)
            {
                return null;
            }
        }

        [WebMethod]
        public static StripeSubscription GetSubscription(string subscriptionID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions/" + subscriptionID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static StripeSubscription UpdateSubscription(string subscriptionID, string customerID, decimal applicationFeePercent, string billing, DateTime billingCycleAnchor, bool cancelAtPeriodEnd, string coupon, int daysUntilDue, string defaultSource, bool prorate, int taxPercent, DateTime prorationDate, DateTime trialEnd, bool trialFromPlan)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions/" + subscriptionID, Method.POST);
            request.AddParameter("customer", customerID);

            if (applicationFeePercent > 0)
                request.AddParameter("application_fee_percent", applicationFeePercent);
            if (!string.IsNullOrEmpty(billing))
                request.AddParameter("billing", billing);
            if (billingCycleAnchor != null)
                request.AddParameter("billing_cycle_anchor", billingCycleAnchor);

            request.AddParameter("cancel_at_period_end", cancelAtPeriodEnd);

            if (coupon != "")
                request.AddParameter("coupon", coupon);
            if (daysUntilDue > 0)
                request.AddParameter("days_until_due", daysUntilDue);
            if (defaultSource != "")
                request.AddParameter("default_source", defaultSource);

            request.AddParameter("prorate", prorate);

            if (prorationDate != null)
                request.AddParameter("proration_date", prorationDate);
            if (taxPercent > 0)
                request.AddParameter("tax_percent", taxPercent);
            if (trialEnd != null)
                request.AddParameter("trial_end", trialEnd);

            request.AddParameter("trial_from_plan", trialFromPlan);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static Subscription CancelSubscriptionNewApi(string subscriptionID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new SubscriptionService();
            Subscription subscription = service.Cancel(subscriptionID);

            return subscription;
        }

        [WebMethod]
        public static StripeSubscription CancelSubscription(string subscriptionID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions/" + subscriptionID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeSubscription subscription = JsonConvert.DeserializeObject<StripeSubscription>(response.Content);

            return subscription;
        }

        [WebMethod]
        public static List<StripeSubscription> GetAllSubscriptions(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeSubscription> subscriptions = JsonConvert.DeserializeObject<List<StripeSubscription>>(response.Content);

            return subscriptions;
        }

        [WebMethod]
        public static List<StripeSubscription> GetAllSubscriptionsByStatus(int limit, ServiceStack.Stripe.Types.StripeSubscriptionStatus status)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions?limit=" + limit, Method.GET);
            request.AddParameter("status", status.ToString());

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeSubscription> subscriptions = JsonConvert.DeserializeObject<List<StripeSubscription>>(response.Content);

            return subscriptions;
        }

        //[WebMethod]
        //public static StripeSubscriptionItem CreateSubscriptionItem(string subscriptionID, string planID, int quantity)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/subscription_items/", Method.POST);
        //    request.AddParameter("subscription", subscriptionID);
        //    request.AddParameter("plan", planID);
        //    request.AddParameter("quantity", quantity);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSubscriptionItem subscriptionItem = JsonConvert.DeserializeObject<StripeSubscriptionItem>(response.Content);

        //    return subscriptionItem;
        //}

        //[WebMethod]
        //public static StripeSubscriptionItem GetSubscriptionItem(string subscriptionItemID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/subscription_items/" + subscriptionItemID, Method.GET);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSubscriptionItem subscriptionItem = JsonConvert.DeserializeObject<StripeSubscriptionItem>(response.Content);

        //    return subscriptionItem;
        //}

        //[WebMethod]
        //public static StripeSubscriptionItem UpdateSubscriptionItem(string subscriptionItemID, string planID, int quantity)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/subscription_items/" + subscriptionItemID, Method.POST);
        //    request.AddParameter("plan", planID);
        //    request.AddParameter("quantity", quantity);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSubscriptionItem subscriptionItem = JsonConvert.DeserializeObject<StripeSubscriptionItem>(response.Content);

        //    return subscriptionItem;
        //}

        //[WebMethod]
        //public static StripeSubscriptionItem DeleteSubscriptionItem(string subscriptionItemID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/subscription_items/" + subscriptionItemID, Method.DELETE);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSubscriptionItem subscriptionItem = JsonConvert.DeserializeObject<StripeSubscriptionItem>(response.Content);

        //    return subscriptionItem;
        //}

        //[WebMethod]
        //public static List<StripeSubscriptionItem> GetAllSubscriptionSubscriptionItems(string subscriptionID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/subscription_items?subscription=" + subscriptionID, Method.GET);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    List<StripeSubscriptionItem> subscriptionItems = JsonConvert.DeserializeObject<List<StripeSubscriptionItem>>(response.Content);

        //    return subscriptionItems;
        //}

        [WebMethod]
        public static StripeCard CreateCard1(string sourceID, string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/customers/" + customerID + "/sources", Method.POST);
            request.AddParameter("source", sourceID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCard CreateCardHolder(string type, string email, string phoneNumber, string name, string address, string city, string state, bool country)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cardholders", Method.POST);
            request.AddParameter("type", type);
            request.AddParameter("name", name);
            request.AddParameter("email", email);
            request.AddParameter("phone_number", phoneNumber);
            request.AddParameter("billing[name]", name);
            request.AddParameter("billing[address][line1]", address);
            request.AddParameter("billing[address][city]", city);
            request.AddParameter("billing[address][state]", state);
            request.AddParameter("billing[address][country]", country);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;


        }

        [WebMethod]
        public static StripeCard CreateCard(string currency, string type, string email, bool isDefault, DateTime created, int expMonth, int expYear, bool liveMode, string name, string status)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards", Method.POST);
            request.AddParameter("currency", currency);
            request.AddParameter("type", type);
            request.AddParameter("created", created);
            request.AddParameter("exp_month", expMonth);
            request.AddParameter("exp_year", expYear);
            request.AddParameter("livemode", liveMode);
            request.AddParameter("name", name);
            request.AddParameter("status", status);
            request.AddParameter("is_default", isDefault);
            request.AddParameter("email", email);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCard GetCard(string cardID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards/" + cardID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCard GetCardDetails(string cardID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards/" + cardID + "/details", Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCard UpdateCard(string cardID, int orderID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards/" + cardID, Method.POST);
            request.AddParameter("metadata[order_id]", orderID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static List<StripeCard> GetAllCards(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeCard> cards = JsonConvert.DeserializeObject<List<StripeCard>>(response.Content);

            return cards;
        }

        [WebMethod]
        public static StripeCard DeleteCard(string planID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/cards/" + planID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCharge CreateCharge(string currency, int amount, string customerID, string description, string source)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/charges", Method.POST);
            request.AddParameter("amount", amount);
            request.AddParameter("currency", currency);
            request.AddParameter("source", source);
            request.AddParameter("description", description);

            if (!string.IsNullOrEmpty(customerID))
                request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCharge charge = JsonConvert.DeserializeObject<StripeCharge>(response.Content);

            return charge;
        }

        [WebMethod]
        public static StripeCharge GetCharge(string chargeID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/issuing/charges/" + chargeID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCharge charge = JsonConvert.DeserializeObject<StripeCharge>(response.Content);

            return charge;
        }

        [WebMethod]
        public static StripeCharge UpdateCharge(string chargeID, string description)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/charges/" + chargeID, Method.POST);
            request.AddParameter("description", description);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCharge charge = JsonConvert.DeserializeObject<StripeCharge>(response.Content);

            return charge;
        }

        [WebMethod]
        public static List<StripeCharge> GetAllCharges(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/charges?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeCharge> charges = JsonConvert.DeserializeObject<List<StripeCharge>>(response.Content);

            return charges;
        }

        [WebMethod]
        public static StripeCharge DeleteCharge(string planID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/charges/" + planID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCharge charge = JsonConvert.DeserializeObject<StripeCharge>(response.Content);

            return charge;
        }

        //[WebMethod]
        //public static StripeSource CreateSource(string type, string currency, int amount, string email, string name)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/sources", Method.POST);
        //    request.AddParameter("type", type);

        //    if (!string.IsNullOrEmpty(currency))
        //        request.AddParameter("currency", currency);
        //    if (amount > 0)
        //        request.AddParameter("amount", amount);
        //    //if (!string.IsNullOrEmpty(address))
        //    //    request.AddParameter("owner[address]", address);
        //    if (!string.IsNullOrEmpty(email))
        //        request.AddParameter("owner[email]", email);
        //    //if (!string.IsNullOrEmpty(name))
        //    //    request.AddParameter("owner[name]", name);
        //    //if (!string.IsNullOrEmpty(phone))
        //    //    request.AddParameter("owner[phone]", phone);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSource source = JsonConvert.DeserializeObject<StripeSource>(response.Content);

        //    return source;
        //}

        //[WebMethod]
        //public static StripeSource GetSource(string sourceID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/sources/" + sourceID, Method.GET);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSource source = JsonConvert.DeserializeObject<StripeSource>(response.Content);

        //    return source;
        //}

        //[WebMethod]
        //public static StripeSource UpdateSource(string sourceID, string owner)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/sources/" + sourceID, Method.POST);
        //    request.AddParameter("owner", owner);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSource source = JsonConvert.DeserializeObject<StripeSource>(response.Content);

        //    return source;
        //}

        //[WebMethod]
        //public static StripeSource AttachSource(string customerID, string sourceID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/customers/" + customerID + "/sources", Method.POST);
        //    request.AddParameter("source", sourceID);
        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSource source = JsonConvert.DeserializeObject<StripeSource>(response.Content);

        //    return source;
        //}

        //[WebMethod]
        //public static StripeSource DetachSource(string customerID, string sourceID)
        //{
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

        //    var client = new RestClient("https://api.stripe.com/");

        //    var request = new RestRequest("v1/customers/" + customerID + "sources/" + sourceID, Method.DELETE);

        //    request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

        //    IRestResponse response = client.Execute(request);

        //    StripeSource source = JsonConvert.DeserializeObject<StripeSource>(response.Content);

        //    return source;
        //}
                
        [WebMethod]
        public static StripeCoupon CreateCoupon(string couponID, string name, string duration, float amountOff, string currency, int durationInMonths, int maxRedemptions, float percentOff, DateTime redeemBy)
        {
            try
            {
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                var client = new RestClient("https://api.stripe.com/");

                var request = new RestRequest("v1/coupons", Method.POST);
                request.AddParameter("id", couponID);

                if (!string.IsNullOrEmpty(name))
                    request.AddParameter("name", name);
                if (percentOff > 0 && amountOff == 0)
                    request.AddParameter("percent_off", percentOff);
                if (amountOff > 0 && percentOff == 0)
                    request.AddParameter("amount_off", amountOff);
                //if (redeemBy != null)
                //{
                //    int redeemByUnix = (Int32)(redeemBy.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                //    request.AddParameter("redeem_by", redeemByUnix);
                //}
                if (!string.IsNullOrEmpty(currency))
                    request.AddParameter("currency", currency);

                request.AddParameter("duration", duration);
                if (duration == "repeating" && durationInMonths > 0)
                    request.AddParameter("duration_in_months", durationInMonths);
                else if (duration == "repeating" && durationInMonths == 0)
                    throw new Exception("Duration parammeter is repeating and duration_in_months parameter is 0");

                if (maxRedemptions > 0)
                    request.AddParameter("max_redemptions", maxRedemptions);

                request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

                IRestResponse response = client.Execute(request);

                StripeCoupon coupon = JsonConvert.DeserializeObject<StripeCoupon>(response.Content);

                return coupon;
            }
            catch (Exception ex)
            {
                Logger.DetailedError("CLASS:StripeService.asmx --> METHOD:CreateCoupon() --> ERROR: ", ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static StripeDiscount CreateDiscount(string couponID, string name, string duration, int amountOff, string currency, int durationInMonths, int maxRedemptions, float percentOff, DateTime redeemBy)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons", Method.POST);
            request.AddParameter("id", couponID);

            if (!string.IsNullOrEmpty(name))
                request.AddParameter("name", name);
            if (percentOff > 0 && amountOff == 0)
                request.AddParameter("percent_off", percentOff);
            if (amountOff > 0 && percentOff == 0)
                request.AddParameter("amount_off", amountOff);
            if (redeemBy != null)
                request.AddParameter("redeem_by", redeemBy);
            if (!string.IsNullOrEmpty(currency))
                request.AddParameter("currency", currency);
            if (!string.IsNullOrEmpty(duration))
                request.AddParameter("duration", duration);
            if (durationInMonths > 0)
                request.AddParameter("duration_in_months", durationInMonths);
            if (maxRedemptions > 0)
                request.AddParameter("max_redemptions", maxRedemptions);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeDiscount discount = JsonConvert.DeserializeObject<StripeDiscount>(response.Content);

            return discount;
        }

        [WebMethod]
        public static StripeCoupon GetCoupon(string couponID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons/" + couponID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCoupon coupon = JsonConvert.DeserializeObject<StripeCoupon>(response.Content);

            return coupon;
        }

        [WebMethod]
        public static List<StripeCoupon> GetCoupons(string couponID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons/" + couponID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeCoupon> coupons = JsonConvert.DeserializeObject<List<StripeCoupon>>(response.Content);

            return coupons;
        }

        [WebMethod]
        public static StripeCoupon UpdateCoupon(string couponID, string name)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons/" + couponID, Method.POST);
            request.AddParameter("name", name);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCoupon coupon = JsonConvert.DeserializeObject<StripeCoupon>(response.Content);

            return coupon;
        }

        [WebMethod]
        public static StripeCoupon DeleteCoupon(string couponID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons/" + couponID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCoupon coupon = JsonConvert.DeserializeObject<StripeCoupon>(response.Content);

            return coupon;
        }

        [WebMethod]
        public static List<StripeCoupon> GetAllCoupons(int limit)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/coupons?limit=" + limit, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeCoupon> coupons = JsonConvert.DeserializeObject<List<StripeCoupon>>(response.Content);

            return coupons;
        }

        [WebMethod]
        public static StripeDiscount DeleteCustomerDiscount(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/customers/" + customerID + "/discount", Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeDiscount customerDiscount = JsonConvert.DeserializeObject<StripeDiscount>(response.Content);

            return customerDiscount;
        }

        [WebMethod]
        public static StripeDiscount DeleteSubscriptionDiscount(string subscriptionID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions/" + subscriptionID + "/discount", Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeDiscount subscriptionDiscount = JsonConvert.DeserializeObject<StripeDiscount>(response.Content);

            return subscriptionDiscount;
        }

        [WebMethod]
        public static Token CreateCardTokenNew(string number, string expMonth, string expYear, string cvc, string name)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = number,
                    ExpMonth = expMonth,
                    ExpYear = expYear,
                    Cvc = cvc,
                    Name = name
                },
            };
            var service = new TokenService();
            return service.Create(options);
        }

        [WebMethod]
        public static StripeToken CreateCardToken(string number, int expMonth, int expYear, string cvc, string name, bool isVirtual)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/tokens", Method.POST);
            request.AddParameter("card[number]", number);
            request.AddParameter("card[exp_month]", expMonth);
            request.AddParameter("card[exp_year]", expYear);
            request.AddParameter("card[cvc]", cvc);
            request.AddParameter("card[name]", name);

            //isVirtual = false;
            //if(isVirtual)
            //{
            //    request.AddParameter("card[status]", "active");
            //    request.AddParameter("card[type]", "virtual");
            //}

            //if (!string.IsNullOrEmpty(addressLine1))
            //    request.AddParameter("address_line1", addressLine1);
            //if (!string.IsNullOrEmpty(addressCity))
            //    request.AddParameter("address_city", addressCity);
            //if (!string.IsNullOrEmpty(addressState))
            //    request.AddParameter("address_state", addressState);
            //if (!string.IsNullOrEmpty(addressZip))
            //    request.AddParameter("address_zip", addressZip);
            //if (!string.IsNullOrEmpty(addressCountry))
            //    request.AddParameter("address_country", addressCountry);
            //if (!string.IsNullOrEmpty(customerID))
            //    request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeToken cardToken = JsonConvert.DeserializeObject<StripeToken>(response.Content);

            return cardToken;
        }

        [WebMethod]
        public static StripeCard CreateCreditCardToken(string number, int expMonth, int expYear, string cvc, string name)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/tokens", Method.POST);
            request.AddParameter("card[number]", number);
            request.AddParameter("card[exp_month]", expMonth);
            request.AddParameter("card[exp_year]", expYear);
            request.AddParameter("card[cvc]", cvc);
            request.AddParameter("card[name]", name);

            //if (!string.IsNullOrEmpty(addressLine1))
            //    request.AddParameter("address_line1", addressLine1);
            //if (!string.IsNullOrEmpty(addressCity))
            //    request.AddParameter("address_city", addressCity);
            //if (!string.IsNullOrEmpty(addressState))
            //    request.AddParameter("address_state", addressState);
            //if (!string.IsNullOrEmpty(addressZip))
            //    request.AddParameter("address_zip", addressZip);
            //if (!string.IsNullOrEmpty(addressCountry))
            //    request.AddParameter("address_country", addressCountry);
            //if (!string.IsNullOrEmpty(customerID))
            //    request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard cardToken = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return cardToken;
        }

        [WebMethod]
        public static StripeToken GetCardToken(string tokenID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/tokens/" + tokenID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeToken cardToken = JsonConvert.DeserializeObject<StripeToken>(response.Content);

            return cardToken;
        }

        [WebMethod]
        public static Stripe.Card CreateCreditCardNew(string customerID, string sourceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new Stripe.CardCreateOptions
            {
                Source = sourceID,
            };

            var service = new Stripe.CardService();
            return service.Create(customerID, options);
        }

        [WebMethod]
        public static StripeCard CreateCreditCard(string customerID, string sourceID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/customers/" + customerID + "/sources", Method.POST);
            request.AddParameter("source", sourceID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static Stripe.Card DeleteCreditCardNew(string customerID, string sourceID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var service = new Stripe.CardService();

            Stripe.Card card = service.Delete(
              customerID,
              sourceID);

            return card;
        }

        [WebMethod]
        public static StripeCard DeleteCreditCard(string customerID, string sourceID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/customers/" + customerID + "/sources/" + sourceID, Method.DELETE);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeCard GetCreditCard(string customerID, string cardID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/customers/" + customerID + "/sources/" + cardID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeCard card = JsonConvert.DeserializeObject<StripeCard>(response.Content);

            return card;
        }

        [WebMethod]
        public static StripeProduct CreateInvoice(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices", Method.POST);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeProduct product = JsonConvert.DeserializeObject<StripeProduct>(response.Content);

            return product;
        }

        [WebMethod]
        public static StripeInvoice CreateInvoiceBySubscription(string customerID, string subscriptionID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices", Method.POST);
            request.AddParameter("customer", customerID);

            if (!string.IsNullOrEmpty(subscriptionID))
                request.AddParameter("subscription", subscriptionID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeInvoice invoice = JsonConvert.DeserializeObject<StripeInvoice>(response.Content);

            return invoice;
        }

        [WebMethod]
        public static StripeInvoice GetInvoice(string invoiceID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices/" + invoiceID, Method.GET);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeInvoice invoice = JsonConvert.DeserializeObject<StripeInvoice>(response.Content);

            return invoice;
        }

        [WebMethod]
        public static Stripe.Invoice GetInvoiceByInvoiceIDNew(string invoiceID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new Stripe.InvoiceService();
                Stripe.Invoice invoice = service.Get(invoiceID);

                return invoice;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static StripeList<Stripe.Invoice> GetCustomerInvoicesBySubscriptionNew(string customerID, string subscriptionID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new InvoiceListOptions
                {
                    Subscription = subscriptionID,
                    Customer = customerID,
                };
                var service = new Stripe.InvoiceService();
                StripeList<Stripe.Invoice> invoices = service.List(options);

                return invoices;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [WebMethod]
        public static StripeList<StripeInvoice> GetCustomerInvoicesBySubscription(string customerID, string subscriptionID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices", Method.GET);
            request.AddParameter("customer", customerID);

            if (!string.IsNullOrEmpty(subscriptionID))
                request.AddParameter("subscription", subscriptionID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            //var customerDictionary = JsonConvert.DeserializeObject<Dictionary<string, JToken>>(response.Content);

            StripeList<StripeInvoice> invoices = JsonConvert.DeserializeObject<StripeList<StripeInvoice>>(response.Content);

            return invoices;
        }

        [WebMethod]
        public static List<StripeInvoice> GetCustomerInvoices(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices", Method.GET);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(response.Content);

            List<StripeInvoice> invoices = new List<StripeInvoice>();

            foreach (var item in content.data)
            {
                StripeInvoice invoice = new StripeInvoice();
                //invoice.Id = (string)item.id;
                //invoice.InvoicePdf = (string)item.invoice_pdf;
                //invoice.AmountPaid = (int)item.amount_paid;
                invoice = item;
                invoices.Add(invoice);
            }

            //List<StripeInvoice> invoices = JsonConvert.DeserializeObject<List<StripeInvoice>>(response.Content);

            return invoices;
        }

        [WebMethod]
        public static List<StripeInvoice> GetCustomerInvoices_v2(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/invoices", Method.GET);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            List<StripeInvoice> invoicesList = new List<StripeInvoice>();

            StripeList<StripeInvoice> invoices = JsonConvert.DeserializeObject<StripeList<StripeInvoice>>(response.Content);

            foreach (StripeInvoice invoice in invoices)
            {
                invoicesList.Add(invoice);
            }

            return invoicesList;
        }

        [WebMethod]
        public static List<StripeSubscription> GetCustomerSubscriptions(string customerID)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.GET);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            dynamic content = JsonConvert.DeserializeObject(response.Content);

            List<StripeSubscription> subscriptions = new List<StripeSubscription>();

            foreach (var item in content.data)
            {
                StripeSubscription subscription = new StripeSubscription();
                subscription = item;

                subscriptions.Add(subscription);
            }

            return subscriptions;
        }

        [WebMethod]
        public static StripeList<StripeSubscription> GetCustomerSubscriptions_v2(string customerID, out List<StripeSubscription> subs)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var client = new RestClient("https://api.stripe.com/");

            var request = new RestRequest("v1/subscriptions", Method.GET);
            request.AddParameter("customer", customerID);

            request.AddHeader("Authorization", "Bearer " + ConfigurationManager.AppSettings["StripeSecretKey"].ToString());

            IRestResponse response = client.Execute(request);

            StripeList<StripeSubscription> subscriptions = JsonConvert.DeserializeObject<StripeList<StripeSubscription>>(response.Content);

            subs = new List<StripeSubscription>();

            foreach (StripeSubscription subscription in subscriptions)
            {
                subs.Add(subscription);
            }

            return subscriptions;
        }
    }
}
