using System;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using System.Configuration;

namespace WdS.ElioPlus.Lib.PaymentGateway
{
    public class PayPalPostSubmitter
    {
        public class SetExpressCheckoutResults
        {
            public string ack;
            public string token;
            public string response;
        }

        public class ExpressCheckoutPostResults
        {
            public string firstName;
            public string lastName;
            public string email;
            public string payerId;
            public string payerStatus;
            public string ack;
            public string response;
            public string amt;
        }

        public class DoExpressCheckoutPaymentResults
        {
            public string ack;
            public string paymentStatus;
            public string reasonCode;
            public string transactionType;
            public string transactionId;
            public string paymentType;
            public string orderTime;
            public string currencyCode;
            public string taxAmt;
            public string pendingReason;
            public string feeAmt;
            public string response;
        }

        public class DoDirectPayment
        {
            public string paymentAction;
            public string ipAddress;
            public string returnFmfDetails;
            public string creditCardType;
            public string acct;
            public string expDate;
            public string cvv2;
            public string startDate;
            public string issueNumber;
            public string email;
            public string firstName;
            public string lastName;
            public string street;
            public string city;
            public string state;
            public string countryCode;
            public string zip;
            public string amt;
            public string curencyCode;
            public string itemAmt;
            public string shippingAmt;
            public string insuranceAmt;
            public string shipDiscAmt;
            public string handlingAmt;
            public string taxAmt;
            public string desc;
            public string buttonSource;
            public string notifyUrl;
            public string recuring;
            public string l_nameN;
            public string l_descN;
            public string l_amtN;
            public string l_numberN;
            public string l_qtyN;
            public string l_taxAmtN;
        }

        public class DoDirectPaymentResults
        {
            public string response;
            public string result;
            public string resmsg;
            public string status;
            public string ack;
            public string CurrencyCode;
            public string transactionId;
            public string amt;
            public string timestamp;
            public string correlationId;
            public string l_errorCode0;
            public string l_shortMessage0;
            public string l_longMessage0;
        }

        private PostSubmitter _postSubmitter = new PostSubmitter();
        private PaypalCredentials _paypalCredentials;

        public PayPalPostSubmitter(PaypalCredentials paypalCredentials)
        {
            _postSubmitter.Url = ConfigurationManager.AppSettings["PaypalApi"];
            _paypalCredentials = paypalCredentials;
        }

        public ExpressCheckoutPostResults GetExpressCheckoutDetails(HttpServerUtility server, string token)
        {
            ExpressCheckoutPostResults postResults = new ExpressCheckoutPostResults();
            _postSubmitter.PostItems.Clear();
            _postSubmitter.PostItems.Add("method", server.UrlEncode("GetExpressCheckoutDetails"));
            _postSubmitter.PostItems.Add("USER", server.UrlEncode(_paypalCredentials.Username));
            _postSubmitter.PostItems.Add("pwd", server.UrlEncode(_paypalCredentials.PaypalPassword));
            _postSubmitter.PostItems.Add("signature", server.UrlEncode(_paypalCredentials.PaypalSignature));
            _postSubmitter.PostItems.Add("version", server.UrlEncode(_paypalCredentials.Version));
            _postSubmitter.PostItems.Add("token", server.UrlEncode(token));
            postResults.response = _postSubmitter.Post();

            string[] resArray = postResults.response.Split('&');
            foreach (string s in resArray)
            {
                if (s.StartsWith("ACK"))
                    postResults.ack = server.UrlDecode(s.Replace("ACK=", ""));

                if (s.StartsWith("FIRSTNAME"))
                    postResults.firstName = server.UrlDecode(s.Replace("FIRSTNAME=", ""));

                if (s.StartsWith("LASTNAME"))
                    postResults.lastName = server.UrlDecode(s.Replace("LASTNAME=", ""));

                if (s.StartsWith("EMAIL"))
                    postResults.email = server.UrlDecode(s.Replace("EMAIL=", ""));

                if (s.StartsWith("PAYERID"))
                    postResults.payerId = server.UrlDecode(s.Replace("PAYERID=", ""));

                if (s.StartsWith("PAYERSTATUS"))
                    postResults.payerStatus = server.UrlDecode(s.Replace("PAYERSTATUS=", ""));

                if (s.StartsWith("AMT"))
                    postResults.amt = server.UrlDecode(s.Replace("AMT=", ""));
            }

            return postResults;
        }

        public SetExpressCheckoutResults SetExpressCheckout(string paymentSource, string returnUrl, string userId, string isReseller, string totalAmount, string isHomeDomain, string isYubotoReseller, string resellerId, string isPaypalAuthenticated, string siteId, HttpServerUtility Server, HttpRequest Request, decimal totalPrice, int credits, string chargeMode, string lang, string isGreece)
        {
            SetExpressCheckoutResults setExpressCheckoutResults = new SetExpressCheckoutResults();
            _postSubmitter.PostItems.Clear();
            _postSubmitter.PostItems.Add("method", Server.UrlEncode("SetExpressCheckout"));
            _postSubmitter.PostItems.Add("user", Server.UrlEncode(_paypalCredentials.Username));
            _postSubmitter.PostItems.Add("pwd", Server.UrlEncode(_paypalCredentials.PaypalPassword));
            _postSubmitter.PostItems.Add("signature", Server.UrlEncode(_paypalCredentials.PaypalSignature));
            _postSubmitter.PostItems.Add("version", Server.UrlEncode(_paypalCredentials.Version));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_CURRENCYCODE", Server.UrlEncode(_paypalCredentials.CurrencyCode));
            _postSubmitter.PostItems.Add("LOCALECODE", Server.UrlEncode(_paypalCredentials.LocaleCode));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_PAYMENTACTION", Server.UrlEncode(_paypalCredentials.PaymentAction));
            _postSubmitter.PostItems.Add("customservicenumber", Server.UrlEncode(_paypalCredentials.CustomerServiceNumber));
            _postSubmitter.PostItems.Add("hdrbackcolor", Server.UrlEncode(_paypalCredentials.Hdrbackcolor));
            _postSubmitter.PostItems.Add("hdrbordercolor", Server.UrlEncode(_paypalCredentials.Hdrbordercolor));
            _postSubmitter.PostItems.Add("hdrimg", Server.UrlEncode(_paypalCredentials.Hdrimg));
            _postSubmitter.PostItems.Add("noshipping ", Server.UrlEncode(_paypalCredentials.Noshipping.ToString()));
            _postSubmitter.PostItems.Add("ALLOWNOTE", Server.UrlEncode(_paypalCredentials.AllowNote.ToString()));
            _postSubmitter.PostItems.Add("GIFTMESSAGEENABLE", Server.UrlEncode(_paypalCredentials.GiftMessageEnable.ToString()));
            _postSubmitter.PostItems.Add("GIFTRECEIPTENABLE", Server.UrlEncode(_paypalCredentials.GiftReceiptEnable.ToString()));
            _postSubmitter.PostItems.Add("GIFTWRAPENABLE", Server.UrlEncode(_paypalCredentials.GiftWrapEnable.ToString()));
            string url = (Request.Url.AbsoluteUri.Contains("?") ? Request.Url.AbsoluteUri.Substring(0, Request.Url.AbsoluteUri.IndexOf("?")) : Request.Url.AbsoluteUri);
            _postSubmitter.PostItems.Add("cancelurl", url + "?type=result&var10=" + returnUrl + "&var1=paypal&var2=" + paymentSource + "&result=cancel");
            _postSubmitter.PostItems.Add("returnurl", url + "?type=result&var13=" + lang + "&var12=" + isGreece + "&var10=" + returnUrl + "&var9=" + userId + "&var8=" + isReseller + "&var7=" + totalAmount.Replace(",", ".") + "&var6=" + isYubotoReseller + "&var5=" + isHomeDomain + "&var4=" + resellerId + "&var3=" + isPaypalAuthenticated + "&var11=" + siteId + "&var2=" + paymentSource + "&var1=paypal&result=ok");
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_AMT0", Server.UrlEncode(totalAmount.Replace(",", ".").ToString()));
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_QTY0", Server.UrlEncode("1"));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_ITEMAMT", Server.UrlEncode(totalAmount.Replace(",", ".").ToString()));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_AMT", Server.UrlEncode(totalAmount.Replace(",", ".").ToString()));
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_NAME0", (chargeMode == "CREDITS" ? (credits + " ") : "") + _paypalCredentials.LName);
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_DESC0", _paypalCredentials.LDesc);
            _postSubmitter.Type = PostSubmitter.PostTypeEnum.Post;
            setExpressCheckoutResults.response = _postSubmitter.Post();

            string[] resArray = setExpressCheckoutResults.response.Split('&');
            foreach (string s in resArray)
            {
                if (s.StartsWith("TOKEN"))
                    setExpressCheckoutResults.token = Server.UrlDecode(s.Replace("TOKEN=", ""));

                if (s.StartsWith("ACK"))
                    setExpressCheckoutResults.ack = Server.UrlDecode(s.Replace("ACK=", ""));
            }

            return setExpressCheckoutResults;
        }

        public SetExpressCheckoutResults SetExpressCheckout(int userId, string returnUrl, string paymentWay, string paymentSource, PaypalCredentials paypalCredentials, HttpServerUtility server, HttpRequest request, decimal totalPrice)
        {
            string name = "Premium Packet";
            string description = "Elioplus Packet";
           
            SetExpressCheckoutResults setExpressCheckoutResults = new SetExpressCheckoutResults();

            _postSubmitter.PostItems.Clear();
            _postSubmitter.PostItems.Add("method", server.UrlEncode("SetExpressCheckout"));

            _postSubmitter.PostItems.Add("user", server.UrlEncode(paypalCredentials.Username));
            _postSubmitter.PostItems.Add("pwd", server.UrlEncode(paypalCredentials.PaypalPassword));
            _postSubmitter.PostItems.Add("signature", server.UrlEncode(paypalCredentials.PaypalSignature));
            _postSubmitter.PostItems.Add("version", server.UrlEncode(paypalCredentials.Version));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_CURRENCYCODE", server.UrlEncode(paypalCredentials.CurrencyCode));
            _postSubmitter.PostItems.Add("LOCALECODE", server.UrlEncode(paypalCredentials.LocaleCode));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_PAYMENTACTION", server.UrlEncode(paypalCredentials.PaymentAction));
            //_postSubmitter.PostItems.Add("customservicenumber", server.UrlEncode(paypalCredentials.CustomerServiceNumber));
            //_postSubmitter.PostItems.Add("hdrbackcolor", (paypalCredentials.Hdrbackcolor));
            //_postSubmitter.PostItems.Add("hdrbordercolor", (paypalCredentials.Hdrbordercolor));
            //_postSubmitter.PostItems.Add("hdrimg", (paypalCredentials.Hdrimg));
            _postSubmitter.PostItems.Add("noshipping ", server.UrlEncode(paypalCredentials.Noshipping.ToString()));
            _postSubmitter.PostItems.Add("ALLOWNOTE", server.UrlEncode(paypalCredentials.AllowNote.ToString()));
            _postSubmitter.PostItems.Add("GIFTMESSAGEENABLE", server.UrlEncode(paypalCredentials.GiftMessageEnable.ToString()));
            _postSubmitter.PostItems.Add("GIFTRECEIPTENABLE", server.UrlEncode(paypalCredentials.GiftReceiptEnable.ToString()));
            _postSubmitter.PostItems.Add("GIFTWRAPENABLE", server.UrlEncode(paypalCredentials.GiftWrapEnable.ToString()));
            string url = (request.Url.AbsoluteUri.Contains("?") ? request.Url.AbsoluteUri.Substring(0, request.Url.AbsoluteUri.IndexOf("?")) : request.Url.AbsoluteUri);
            
            _postSubmitter.PostItems.Add("cancelurl", url + "?type=result&var10=" + returnUrl + "&var1=" + paymentWay.ToLower() + "&var2=" + paymentSource +"&result=cancel");
            _postSubmitter.PostItems.Add("returnurl", url + "?type=result&var10=" + returnUrl + "&var9=" + userId + "&var7=" + totalPrice.ToString().Replace(",",".") + "&var2=" + paymentSource + "&var1=" + paymentWay.ToLower() + "&result=ok");
            
            //_postSubmitter.PostItems.Add("cancelurl", url + "?type=result&var10=" + returnUrl + "&var1=paypal&var2=" + paymentSource + "&result=cancel");
            //_postSubmitter.PostItems.Add("returnurl", url + "?type=result&var10=" + returnUrl + "&var9=" + userId + "&var7=" + totalPrice.ToString().Replace(",", ".") + "&var2=" + paymentSource + "&var1=paypal&result=ok");
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_AMT0", server.UrlEncode(totalPrice.ToString().Replace(",", ".")));
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_QTY0", server.UrlEncode("1"));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_ITEMAMT", server.UrlEncode(totalPrice.ToString().Replace(",", ".")));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_AMT", server.UrlEncode(totalPrice.ToString().Replace(",", ".")));
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_NAME0", name);
            _postSubmitter.PostItems.Add("L_PAYMENTREQUEST_0_DESC0", description);            
            _postSubmitter.Type = PostSubmitter.PostTypeEnum.Post;
            
            setExpressCheckoutResults.response = _postSubmitter.Post();

            string[] resArray = setExpressCheckoutResults.response.Split('&');
            foreach (string s in resArray)
            {
                if (s.StartsWith("TOKEN"))
                    setExpressCheckoutResults.token = server.UrlDecode(s.Replace("TOKEN=", ""));

                if (s.StartsWith("ACK"))
                    setExpressCheckoutResults.ack = server.UrlDecode(s.Replace("ACK=", ""));
            }

            return setExpressCheckoutResults;
        }

        public DoExpressCheckoutPaymentResults DoExpressCkeckoutPayment(HttpServerUtility server, string token, string payerId, string totalPrice)
        {
            DoExpressCheckoutPaymentResults results = new DoExpressCheckoutPaymentResults();
            _postSubmitter.PostItems.Clear();
            _postSubmitter.PostItems.Add("method", server.UrlEncode("DoExpressCheckoutPayment"));
            _postSubmitter.PostItems.Add("USER", server.UrlEncode(_paypalCredentials.Username));
            _postSubmitter.PostItems.Add("pwd", server.UrlEncode(_paypalCredentials.PaypalPassword));
            _postSubmitter.PostItems.Add("signature", server.UrlEncode(_paypalCredentials.PaypalSignature));
            _postSubmitter.PostItems.Add("version", server.UrlEncode(_paypalCredentials.Version));
            _postSubmitter.PostItems.Add("token", server.UrlEncode(token));
            _postSubmitter.PostItems.Add("payerid", server.UrlEncode(payerId));
            //post.PostItems.Add("PAYMENTREQUEST_0_PAYMENTACTION", Server.UrlEncode("Sale"));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_CURRENCYCODE", server.UrlEncode(_paypalCredentials.CurrencyCode));
            _postSubmitter.PostItems.Add("PAYMENTREQUEST_0_AMT", server.UrlEncode(totalPrice));
            results.response = _postSubmitter.Post();

            string[] resArray = results.response.Split('&');
            foreach (string s in resArray)
            {
                if (s.StartsWith("ACK"))
                    results.ack = server.UrlDecode(s.Replace("ACK=", ""));

                if (s.StartsWith("PAYMENTINFO_0_PAYMENTSTATUS"))
                    results.paymentStatus = server.UrlDecode(s.Replace("PAYMENTINFO_0_PAYMENTSTATUS=", ""));

                if (s.StartsWith("PAYMENTINFO_0_TRANSACTIONTYPE"))
                    results.transactionType = server.UrlDecode(s.Replace("PAYMENTINFO_0_TRANSACTIONTYPE=", ""));

                if (s.StartsWith("PAYMENTINFO_0_TRANSACTIONID"))
                    results.transactionId = server.UrlDecode(s.Replace("PAYMENTINFO_0_TRANSACTIONID=", ""));

                if (s.StartsWith("PAYMENTINFO_0_PAYMENTTYPE"))
                    results.paymentType = server.UrlDecode(s.Replace("PAYMENTINFO_0_PAYMENTTYPE=", ""));

                if (s.StartsWith("PAYMENTINFO_0_ORDERTIME"))
                    results.orderTime = server.UrlDecode(s.Replace("PAYMENTINFO_0_ORDERTIME=", ""));

                if (s.StartsWith("PAYMENTINFO_0_CURRENCYCODE"))
                    results.currencyCode = server.UrlDecode(s.Replace("PAYMENTINFO_0_CURRENCYCODE=", ""));

                if (s.StartsWith("PAYMENTINFO_0_TAXAMT"))
                    results.taxAmt = server.UrlDecode(s.Replace("PAYMENTINFO_0_TAXAMT=", ""));

                if (s.StartsWith("PAYMENTINFO_0_PENDINGREASON"))
                    results.pendingReason = server.UrlDecode(s.Replace("PAYMENTINFO_0_PENDINGREASON=", ""));

                if (s.StartsWith("FEEAMT"))
                    results.feeAmt = server.UrlDecode(s.Replace("FEEAMT=", ""));

                if (s.StartsWith("PAYMENTINFO_0_REASONCODE"))
                    results.reasonCode = server.UrlDecode(s.Replace("PAYMENTINFO_0_REASONCODE=", ""));
            }

            return results;
        }

        public DoDirectPaymentResults DoDirectpaymentResults(HttpServerUtility server, string ipAddress, string acct, string creditCardType, 
            string expDate, string ccv2, string firstname, string lastName, string street, string city, string state, string countryCode, 
            string zip, string amount, string currencyCode)
        {
            DoDirectPaymentResults results = new DoDirectPaymentResults();

            _postSubmitter.PostItems.Clear();
            _postSubmitter.PostItems.Add("method", server.UrlEncode("DoDirectPayment"));
            _postSubmitter.PostItems.Add("IPADDRESS", HttpContext.Current.Request.ServerVariables["remote_addr"].ToString());
            _postSubmitter.PostItems.Add("PAYMENTACTION", _paypalCredentials.PaymentAction);
            _postSubmitter.PostItems.Add("CREDITCARDTYPE", "VISA");
            _postSubmitter.PostItems.Add("ACCT", "4032034498002");
            _postSubmitter.PostItems.Add("EXPDATE", "11/2020");
            _postSubmitter.PostItems.Add("CVV2", "743");
            _postSubmitter.PostItems.Add("FIRSTNAME", "Elias");
            _postSubmitter.PostItems.Add("LASTNAME", "Ndreu");
            _postSubmitter.PostItems.Add("STREET", "");
            _postSubmitter.PostItems.Add("CITY", "New York");
            _postSubmitter.PostItems.Add("STATE", "US");
            _postSubmitter.PostItems.Add("COUNTRYCODE", "US");
            _postSubmitter.PostItems.Add("ZIP", "");
            _postSubmitter.PostItems.Add("AMT", "99.00");

            results.response = _postSubmitter.Post();

            string[] resArray = results.response.Split('&');
            foreach (string s in resArray)
            {
                if (s.StartsWith("ACK"))
                    results.ack = server.UrlDecode(s.Replace("ACK=", ""));

                if (s.StartsWith("STATUS"))
                    results.status = server.UrlDecode(s.Replace("STATUS=", ""));

                if (s.StartsWith("RESMSG"))
                    results.resmsg = server.UrlDecode(s.Replace("RESMSG=", ""));

                if (s.StartsWith("RESULT"))
                    results.result = server.UrlDecode(s.Replace("RESULT=", ""));

                if (s.StartsWith("CURRENCYCODE"))
                    results.CurrencyCode = server.UrlDecode(s.Replace("CURRENCYCODE=", ""));

                if (s.StartsWith("TRANSACTIONID"))
                    results.transactionId = server.UrlDecode(s.Replace("TRANSACTIONID=", ""));

                if (s.StartsWith("RESMSG"))
                    results.resmsg = server.UrlDecode(s.Replace("RESMSG=", ""));

                if (s.StartsWith("TIMESTAMP"))
                    results.timestamp = server.UrlDecode(s.Replace("TIMESTAMP=", ""));

                if (s.StartsWith("CORRELATIONID"))
                    results.correlationId = server.UrlDecode(s.Replace("CORRELATIONID=", ""));

                if (s.StartsWith("L_ERRORCODE0"))
                    results.l_errorCode0 = server.UrlDecode(s.Replace("L_ERRORCODE0=", ""));

                if (s.StartsWith("L_SHORTMESSAGE0"))
                    results.l_shortMessage0 = server.UrlDecode(s.Replace("L_SHORTMESSAGE0=", ""));

                if (s.StartsWith("L_LONGMESSAGE0"))
                    results.l_longMessage0 = server.UrlDecode(s.Replace("L_LONGMESSAGE0=", ""));
            }

            return results;
        }
    }
}