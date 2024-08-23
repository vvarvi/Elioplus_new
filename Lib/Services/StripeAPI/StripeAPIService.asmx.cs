using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Services;
using Stripe;
using Stripe.Checkout;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Services.Customers.ioAPI;
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
    public class StripeAPIService : System.Web.Services.WebService
    {
        //const string ApiKey = "sk_test_OIySeuUvaObscVisQhrHkhix";

        [WebMethod]
        public static Coupon GetCouponNewApi(string couponID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new CouponService();
            return service.Get(couponID);
        }

        [WebMethod]
        public static Stripe.Coupon CreateCouponNewApi(string couponID, string name, string duration, long amountOff, string currency, int durationInMonths, int maxRedemptions, decimal percentOff, DateTime redeemBy)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CouponCreateOptions
                {
                    PercentOff = percentOff,
                    AmountOff = amountOff,
                    Duration = duration,
                    DurationInMonths = durationInMonths,
                    Currency = currency,
                    RedeemBy = redeemBy,
                    Name = name,
                };

                var service = new CouponService();
                return service.Create(options);
            }
            catch (Exception ex)
            {
                Logger.DetailedError("CLASS:StripeService.asmx --> METHOD:CreateCoupon() --> ERROR: ", ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Plan GetPlanNewApi(string planID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new PlanService();
            Plan plan = service.Get(planID);

            return plan;
        }

        [WebMethod]
        public static Source CreateSEPASourceApi(string type, string currency, string companyName, string email, string iban)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new SourceCreateOptions
            {
                Type = type,
                Currency = currency,

                Owner = new SourceOwnerOptions
                {
                    Name = companyName,
                    Email = email
                    //Address = new AddressOptions
                    //{
                    //    City = "Frankfurt",
                    //    Country = "DE",
                    //    Line1 = "Genslerstraße 24",
                    //    PostalCode = "15230",
                    //    State = "Brandenburg",
                    //},
                },
            };

            options.AddExtraParam("sepa_debit[iban]", iban);

            //var requestOptions = new RequestOptions();
            //requestOptions.StripeAccount = accountId;

            var service = new SourceService();
            Source source = service.Create(options);

            return source;
        }

        [WebMethod]
        public static Token CreateBankAccountTokenApi(string accountType, string accountNumber, string routingNumber, string accountHolderType, string accountHolderName, string countryISO, string currency, string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new TokenCreateOptions
            {
                BankAccount = new TokenBankAccountOptions
                {
                    AccountType= accountType,
                    AccountNumber = accountNumber,
                    //RoutingNumber = routingNumber,
                    AccountHolderType = accountHolderType,     //"company",
                    AccountHolderName = accountHolderName,
                    Country = countryISO,
                    Currency = currency
                }
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new TokenService();
            Token bnkAccountTkn = service.Create(options, requestOptions);

            //var service = new TokenService();
            //Token bnkAccountTkn = service.Create(options);

            return bnkAccountTkn;
        }

        [WebMethod]
        public static BankAccount CreateBankAccountApi(string accountId, string customerId, string bnkSourceId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new BankAccountCreateOptions
            {
                Source = bnkSourceId
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new BankAccountService();
            BankAccount bnkAccount = service.Create(customerId, options, requestOptions);

            return bnkAccount;
        }

        [WebMethod]
        public static IExternalAccount CreateExternalAccountApi(string accountId, string customerId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new ExternalAccountCreateOptions
            {
                ExternalAccount = "btok_1MK9rI2eZvKYlo2CHBwtNOKx",

            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new ExternalAccountService();
            IExternalAccount extAccount = service.Create(accountId, options);

            return extAccount;
        }

        [WebMethod]
        public static PaymentIntent CancelPaymentIntentApi(string paymentIntent)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                //var requestOptions = new RequestOptions();
                //requestOptions.StripeAccount = accountId;

                var service = new PaymentIntentService();
                PaymentIntent payment = service.Cancel(paymentIntent);

                return payment;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static PaymentIntent CreatePaymentIntentApi(string accountId, string customerId, long amount, long feeAmount, string currency)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new PaymentIntentService();
                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = currency,    //usd
                    AutomaticPaymentMethods = new PaymentIntentAutomaticPaymentMethodsOptions
                    {
                        Enabled = true,
                    },
                    ApplicationFeeAmount = feeAmount,
                    Customer = customerId,
                    //OnBehalfOf = "",

                    TransferData = new PaymentIntentTransferDataOptions
                    {
                        Amount = amount - feeAmount,
                        Destination = accountId,
                    }
                };

                PaymentIntent payment = service.Create(createOptions);

                return payment;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Account CreateAccountApi(string accountType, string countryISO, string companyEmail, string businessType)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new AccountCreateOptions
                {
                    Type = accountType,
                    Country = countryISO,
                    Email = companyEmail,
                    BusinessType = businessType,
                    
                    TosAcceptance = new AccountTosAcceptanceOptions { ServiceAgreement = "full" },

                    Capabilities = new AccountCapabilitiesOptions
                    {
                        CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                        Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                    },
                };

                AccountService service = new AccountService();
                Stripe.Account account = service.Create(options);

                return account;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Account UpdateAccountApi(string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new AccountUpdateOptions
                {
                    TosAcceptance = new AccountTosAcceptanceOptions
                    {
                        Date = DateTimeOffset.FromUnixTimeSeconds(1609798905).UtcDateTime,
                        Ip = HttpContext.Current.Request.ServerVariables["remote_addr"],
                    }

                    //TosAcceptance = new AccountTosAcceptanceOptions { ServiceAgreement = "recipient" }

                    //Capabilities = new AccountCapabilitiesOptions
                    //{
                    //    CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                    //    Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                    //},
                };

                AccountService service = new AccountService();
                Stripe.Account account = service.Update(accountId, options);

                return account;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static AccountLink CreateAccountLinkApi(string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new AccountLinkCreateOptions
                {
                    Account = accountId,
                    RefreshUrl = "https://elioplus.com/home",
                    ReturnUrl = "https://elioplus.com/login",
                    Type = "account_onboarding",
                };

                var service = new AccountLinkService();

                AccountLink accountLink = service.Create(options);

                return accountLink;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer GetCustomerNewUnderAccountApi(string accountId, string customerID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new CustomerService();
                Customer customer = service.Get(customerID, null, requestOptions);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer GetCustomerApi(string customerID)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new CustomerService();
                Customer customer = service.Get(customerID);

                return customer;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer CreateCustomerToAccountApi(string firstName, string lastName, string companyName, string email, string country, string city, string state, string phone, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Description = companyName,
                    Name = (lastName != "" && firstName != "") ? lastName + " " + firstName : companyName,
                    Phone = phone,

                    Address = new AddressOptions()
                    {
                        City = city,
                        Country = country,
                        State = state
                    }
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

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
        public static Customer CreateCustomerApi(string firstName, string lastName, string companyName, string email, string country, string city, string state, string phone, string postalCode)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerCreateOptions
                {
                    Email = email,
                    Description = companyName,
                    Name = (lastName != "" && firstName != "") ? lastName + " " + firstName : companyName,
                    Phone = phone,

                    Address = new AddressOptions()
                    {
                        City = city,
                        Country = country,
                        State = state,
                        PostalCode = postalCode
                    },
                    
                    InvoiceSettings = new CustomerInvoiceSettingsOptions()
                    {
                        Footer = "You may be required to account for VAT under the reverse charge procedure according to the local VAT rules in your country."
                    },

                    //TaxIdData = new List<CustomerTaxIdDataOptions> { new CustomerTaxIdDataOptions()
                    //{
                        
                        
                    //} }
                };

                //var requestOptions = new RequestOptions();
                //requestOptions.StripeAccount = accountId;

                var service = new CustomerService();
                Customer cust = service.Create(options, null);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static TaxId CreateCustomerTaxIdApi(string customerId, string type, string taxValue)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new TaxIdCreateOptions
                {
                    Type = type,
                    Value = taxValue
                };

                //var requestOptions = new RequestOptions();
                //requestOptions.StripeAccount = accountId;

                var service = new TaxIdService();
                TaxId tax = service.Create(customerId, options, null);

                return tax;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static TaxId DeleteCustomerTaxIdApi(string customerId, string taxId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();
                                
                var service = new TaxIdService();
                TaxId tax = service.Delete(customerId, taxId);

                return tax;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer UpdateCustomerToAccountApi(string firstName, string lastName, string companyName, string email, string country, string city, string state, string phone, string accountId, string customerId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerUpdateOptions
                {
                    Email = email,
                    Description = companyName,
                    Name = (lastName != "" && firstName != "") ? lastName + " " + firstName : companyName,
                    Phone = phone,

                    Address = new AddressOptions()
                    {
                        City = city,
                        Country = country,
                        State = state
                    }
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new CustomerService();
                Customer cust = service.Update(customerId, options, requestOptions);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Account DeleteAccountApi(string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new AccountService();
                Stripe.Account account = service.Delete(accountId);

                return account;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer DeleteCustomerToAccountApi(string customerId, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new CustomerService();
                Customer cust = service.Delete(customerId, null, requestOptions);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer DeleteCustomerApi(string customerId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var service = new CustomerService();
                Customer cust = service.Delete(customerId, null, null);

                return cust;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Customer CreateExistingCustomerToAccountApi(Customer customer, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new CustomerCreateOptions
                {
                    Email = customer.Email,
                    Description = customer.Description,
                    Phone = customer.Phone
                    //Source = customer.DefaultSourceId
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

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
        public static Token CreateCardTokenNewUnderAccountApi(string accountId, string number, string expMonth, string expYear, string cvc, string name)
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

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new TokenService();
            return service.Create(options, requestOptions);
        }

        [WebMethod]
        public static Stripe.Card DeleteCreditCardNewUnderAccountApi(string accountId, string customerID, string sourceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new Stripe.CardService();
            return service.Delete(customerID, sourceID, null, requestOptions);
        }

        [WebMethod]
        public static Stripe.Card CreateCreditCardNewUnderAccountApi(string accountId, string customerID, string sourceID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new Stripe.CardCreateOptions
            {
                Source = sourceID,
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            var service = new Stripe.CardService();
            return service.Create(customerID, options, requestOptions);
        }

        [WebMethod]
        public static Source CreateCustomerSourceApi(string customerEmail)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "usd",
                    Owner = new SourceOwnerOptions
                    {
                        Email = customerEmail
                    }
                };

                var service = new SourceService();
                Source source = service.Create(options);

                return source;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Product GetProductToAccountApi(string productId, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new ProductService();
                Stripe.Product product = service.Get(productId, null, requestOptions);

                return product;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Product CreateProductToAccountApi(string productName, string type, string description, string accountStatement, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new ProductCreateOptions
                {
                    Name = productName,
                    Type = type,
                    Description = description,
                    StatementDescriptor = accountStatement                    
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new ProductService();
                Stripe.Product product = service.Create(options, requestOptions);

                return product;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Stripe.Price GetPriceToAccountApi(string priceId, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new PriceService();
                Stripe.Price price = service.Get(priceId, null, requestOptions);

                return price;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Price CreatePriceToAccountApi(long amount, string currency, string productId, string priceNickname, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new PriceCreateOptions
                {
                    UnitAmount = amount,
                    Currency = currency,
                    Nickname = priceNickname,
                    //Recurring = new PriceRecurringOptions
                    //{
                    //    Interval = "month",
                    //},
                    Product = productId,
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new PriceService();
                Price price = service.Create(options, requestOptions);

                return price;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static PaymentLink CreatePaymentLinkToPriceApi(string priceId, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var options = new PaymentLinkCreateOptions
                {
                    LineItems = new List<PaymentLinkLineItemOptions>
                      {
                        new PaymentLinkLineItemOptions
                        {
                          Price = priceId,
                          Quantity = 1,
                        },
                      },
                };

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new PaymentLinkService();
                PaymentLink paymentLnk = service.Create(options, requestOptions);

                return paymentLnk;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static PaymentLink GetPaymentLinkToPriceApi(string pLink, string accountId)
        {
            try
            {
                StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

                var requestOptions = new RequestOptions();
                requestOptions.StripeAccount = accountId;

                var service = new PaymentLinkService();
                PaymentLink paymentLnk = service.Get(pLink, null, requestOptions);

                return paymentLnk;
            }
            catch (ServiceStack.Stripe.Types.StripeException ex)
            {
                Logger.DetailedError(ex.Message.ToString(), ex.StackTrace.ToString());
                return null;
            }
        }

        [WebMethod]
        public static Session CreateCheckoutSessionForPriceAndCustomerWithDiscountApi(string priceId, string couponId, ElioUsers user)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            string domain = "https://elioplus.com";

            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://staging99.elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                domain = "https://localhost:44343";
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + ControlLoader.SuccessfulPaymentPage,
                CancelUrl = domain + ControlLoader.CanceledPaymentPage,

                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = priceId,
                        Quantity = 1,
                        //Name = price.Nickname,
                        //Amount = price.UnitAmount,
                        //Currency = price.Currency
                    },
                },

                Mode = "subscription",

                Discounts = new List<SessionDiscountOptions>
                {
                    new SessionDiscountOptions
                    {
                        Coupon = couponId,
                    },
                },

                CustomerEmail = user.Email,
                PaymentMethodTypes = new List<string> { "card" }
                //Customer = user.CustomerStripeId

                //AllowPromotionCodes = true
                //ConsentCollection = new SessionConsentCollectionOptions { TermsOfService = "required" }
            };

            var service = new SessionService();
            Session session = service.Create(options, null);

            return session;
        }

        public static Session CreateCheckoutSessionForPriceAndCustomerApi(string priceId, ElioUsers user, bool customerWay)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            string domain = "https://elioplus.com";

            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://staging99.elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                domain = "https://localhost:44343";
            }

            if (customerWay)
            {
                Customer cust = CreateCustomerApi(user.FirstName, user.LastName, user.CompanyName, user.Email, user.Country, user.City, user.State, user.Phone, "");

                var options = new SessionCreateOptions
                {
                    //PaymentIntentData = new Stripe.Checkout.SessionPaymentIntentDataOptions
                    //{
                    //    SetupFutureUsage = "off_session",
                    //},
                    //CustomerCreation = "always",
                    
                    SuccessUrl = domain + ControlLoader.SuccessfulPaymentPage,
                    CancelUrl = domain + ControlLoader.CanceledPaymentPage,

                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = priceId,
                            Quantity = 1,
                        },
                    },

                    Mode = "subscription",
                    Customer = cust.Id,
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new SessionService();
                Session session = service.Create(options, null);

                return session;
            }
            else
            {
                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain + ControlLoader.SuccessfulPaymentPage,
                    CancelUrl = domain + ControlLoader.CanceledPaymentPage,

                    LineItems = new List<SessionLineItemOptions>
                    {
                        new SessionLineItemOptions
                        {
                            Price = priceId,
                            Quantity = 1,
                            //Name = price.Nickname,
                            //Amount = price.UnitAmount,
                            //Currency = price.Currency
                        },
                    },

                    //AutomaticTax = new SessionAutomaticTaxOptions
                    //{
                    //    Enabled = true,
                    //},

                    //CustomerUpdate = new Stripe.Checkout.SessionCustomerUpdateOptions
                    //{
                    //    Address = "auto",
                    //    Name = "auto",
                    //},

                    //ShippingAddressCollection = new SessionShippingAddressCollectionOptions
                    //{
                    //    AllowedCountries = new List<string> { "US" },
                    //},

                    Mode = "subscription",
                    CustomerEmail = user.Email,
                    PaymentMethodTypes = new List<string> { "card" }
                    //Customer = user.CustomerStripeId

                    //AllowPromotionCodes = true
                    //ConsentCollection = new SessionConsentCollectionOptions { TermsOfService = "required" }
                };

                var service = new SessionService();
                Session session = service.Create(options, null);

                return session;
            }
        }

        [WebMethod]
        public static Session CreateCheckoutSessionForPriceAndExistingCustomerApi(string priceId, ElioUsers user)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            string domain = "https://elioplus.com";

            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://staging99.elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                domain = "https://localhost:44343";
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + ControlLoader.SuccessfulPaymentPage,
                CancelUrl = domain + ControlLoader.CanceledPaymentPage,

                LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                      Price = priceId,
                      Quantity = 1,
                      //Name = price.Nickname,
                      //Amount = price.UnitAmount,
                      //Currency = price.Currency
                    },
                },
                Mode = "subscription",
                //CustomerEmail = user.Email,
                Customer = user.CustomerStripeId,
                PaymentMethodTypes = new List<string> { "card" },
                //AllowPromotionCodes = true
                //ConsentCollection = new SessionConsentCollectionOptions { TermsOfService = "required" }
            };

            var service = new SessionService();
            Session session = service.Create(options, null);

            return session;
        }

        [WebMethod]
        public static Session CreateCheckoutSessionForPriceAndExistingCustomerWithDiscountApi(string priceId, string couponId, ElioUsers user)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            string domain = "https://elioplus.com";

            if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "true" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "false")
            {
                domain = "https://staging99.elioplus.com";
            }
            else if (ConfigurationManager.AppSettings["IsProductionMode"].ToString() == "false" && ConfigurationManager.AppSettings["IsLocalMode"].ToString() == "true")
            {
                domain = "https://localhost:44343";
            }

            var options = new SessionCreateOptions
            {
                SuccessUrl = domain + ControlLoader.SuccessfulPaymentPage,
                CancelUrl = domain + ControlLoader.CanceledPaymentPage,

                LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                      Price = priceId,
                      Quantity = 1,
                      //Name = price.Nickname,
                      //Amount = price.UnitAmount,
                      //Currency = price.Currency
                    },
                },

                Mode = "subscription",

                Discounts = new List<SessionDiscountOptions>
                {
                    new SessionDiscountOptions
                    {
                        Coupon = couponId,
                    },
                },

                CustomerEmail = user.Email,
                Customer = user.CustomerStripeId,
                PaymentMethodTypes = new List<string> { "card" },
                //AllowPromotionCodes = true
                //ConsentCollection = new SessionConsentCollectionOptions { TermsOfService = "required" }
            };

            var service = new SessionService();
            Session session = service.Create(options, null);

            return session;
        }

        [WebMethod]
        public static Session GetCheckoutSessionBySessIdApi(string sessionId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new SessionService();

            Session session = service.Get(sessionId, null);

            return session;
        }

        [WebMethod]
        public static StripeList<Session> GetAllCheckoutSessionsApi()
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new SessionListOptions
            {
                Limit = 10
            };

            var service = new SessionService();

            StripeList<Session> sessLinks = service.List(options, null);

            //foreach (Session item in sessLinks)
            //{
            //    ExpireCheckoutSessionForPriceAndCustomerApi(item.Id);
            //}

            return sessLinks;
        }

        [WebMethod]
        public static Session ExpireCheckoutSessionForPriceAndCustomerApi(string sessionId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new SessionService();
            Session session = service.Expire(sessionId, null);

            return session;
        }

        [WebMethod]
        public static Session CreateCheckoutSessionApi(string currency, long amount, string productName, string productDescription, string accountID)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new SessionCreateOptions
            {
                LineItems = new List<SessionLineItemOptions>
                  {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                          {
                            Currency = currency,
                            UnitAmount = amount,

                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                              Name = productName,
                              Description = productDescription,
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
        public static Charge CreateDirectChargeApi(string receiptEmail, long amount, long? feeAmount, string currencyISO, string cardSource, string accountID, string customerId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var service = new ChargeService();
            var createOptions = new ChargeCreateOptions
            {
                Amount = amount,
                Currency = currencyISO,
                Source = cardSource,
                ReceiptEmail = receiptEmail,
                ApplicationFeeAmount = feeAmount,
                Customer = customerId
            };

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountID;

            Charge s = service.Create(createOptions, requestOptions);

            return s;
        }

        [WebMethod]
        public static StripeList<PaymentIntent> GetAllPaymentsApi(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentIntentListOptions
            {
                Limit = 10
            };

            var service = new PaymentIntentService();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            StripeList<PaymentIntent> payments = service.List(options, requestOptions);

            return payments;
        }

        [WebMethod]
        public static StripeList<PaymentLink> GetAllPaymentLinksApi(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkListOptions
            {
                Limit = 10
            };

            var service = new PaymentLinkService();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            StripeList<PaymentLink> paymentLinks = service.List(options, requestOptions);

            return paymentLinks;
        }

        [WebMethod]
        public static StripeList<LineItem> GetAllPaymentLinksLineItemsApi(string accountId, string pLinkId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PaymentLinkListLineItemsOptions
            {
                Limit = 10
            };

            var service = new PaymentLinkService();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            StripeList<LineItem> lineItems = service.ListLineItems(pLinkId, options, requestOptions);

            //StripeList<PaymentLink> paymentLinks = service.List(options, requestOptions);

            return lineItems;
        }

        [WebMethod]
        public static StripeList<Payout> GetAllPayoutsApi(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new PayoutListOptions
            {
                Limit = 10
            };

            var service = new PayoutService();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            StripeList<Payout> payouts = service.List(options, requestOptions);

            return payouts;
        }

        [WebMethod]
        public static StripeList<Stripe.Invoice> GetAllInvoicesApi(string accountId)
        {
            StripeConfiguration.ApiKey = ConfigurationManager.AppSettings["StripeSecretKey"].ToString();

            var options = new InvoiceListOptions
            {
                Limit = 10
            };

            var service = new InvoiceService();

            var requestOptions = new RequestOptions();
            requestOptions.StripeAccount = accountId;

            StripeList<Stripe.Invoice> invoices = service.List(options, requestOptions);

            return invoices;
        }
    }
}
