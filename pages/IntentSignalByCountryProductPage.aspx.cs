using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WdS.ElioPlus.pages
{
    public partial class IntentSignalByCountryProductPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public int PageIndex
        {
            get
            {
                if (ViewState["PageIndex"] != null)
                    return Convert.ToInt32(ViewState["PageIndex"].ToString());
                else
                    return 1;
            }
            set
            {
                if (value > 1)
                    ViewState["PageIndex"] = value;
                else
                    ViewState["PageIndex"] = 1;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    PageIndex = 1;
                    FixPage();
                    SetLinks();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        private void FixPage()
        {
            //aSignUp.Visible = vSession.User == null;
            aLoadLess.Visible = PageIndex > 1;
        }        

        private void SetLinks()
        {
            
        }

        private void LoadData(DBSession session)
        {
            aLoadLess.Visible = PageIndex > 1;

            bool containsCountry = false;
            bool containsProduct = false;
            string countryName = "";
            string productName = "";

            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            if (path.Length > 0)
            {
                if (path.Length > 2)
                {
                    if (path[path.Length - 1].Contains("OR") || path[path.Length - 1].Contains("XOR") || path[path.Length - 1].Contains("now") || path[path.Length - 1].Contains("(") || path[path.Length - 1].Contains("'") || path[path.Length - 1].Contains("sleep") || path[path.Length - 1].Contains("sysdate") || path[path.Length - 1].Contains("||") || path[path.Length - 1].Length > 40)
                    {
                        Response.Redirect(ControlLoader.IntentSignals, false);
                        return;
                    }

                    if (!path[path.Length - 1].TrimEnd('/').TrimStart('/').Contains("intent-signals"))
                    {
                        List<string> defaultCountries = "australia,canada,hong-kong,india,singapore,south-africa,united-arab-emirates,united-kingdom,united-states".Split(',').ToList();
                        if (defaultCountries.Count > 0)
                        {
                            foreach (string country in defaultCountries)
                            {
                                if (country != "" && country.Contains(path[path.Length - 1]))
                                {
                                    containsCountry = true;
                                    break;
                                }
                            }
                        }

                        containsProduct = !containsCountry;
                    }
                    else
                        Response.Redirect(ControlLoader.IntentSignals, false);
                }
                else
                    Response.Redirect(ControlLoader.IntentSignals, false);
            }
            else
                Response.Redirect(ControlLoader.IntentSignals, false);

            if (containsCountry)
            {
                countryName = path[path.Length - 1].Replace("-", " ").TrimEnd();
                FixContent(true);
            }
            else if (containsProduct)
            {
                productName = path[path.Length - 1].Replace("_", " ").TrimEnd();
                FixContent(false);
            }
            else
            {
                Response.Redirect(ControlLoader.IntentSignals, false);
            }

            DataTable table = Sql.GetIntentSignalsAndRFQsDataTop30(PageIndex, countryName, productName, session);

            if (table != null && table.Rows.Count > 0)
            {
                aLoadMore.Visible = table.Rows.Count > 30;
                RdgIntentData.Visible = true;
                UcMessageDataAlert.Visible = false;

                RdgIntentData.DataSource = table;
                RdgIntentData.DataBind();
            }
            else
            {
                RdgIntentData.DataSource = null;
                RdgIntentData.Visible = false;
                aLoadMore.Visible = false;

                GlobalMethods.ShowMessageControl(UcMessageDataAlert, "There are no data", MessageTypes.Info, true, true, false, false, false);

            }
        }

        private void FixContent(bool containsCountry)
        {
            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            string keyName = path[path.Length - 1].ToString();

            //Lbl31.Text = "This list is being updated multiple times during the day.";

            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            if (containsCountry)
            {                
                switch (keyName)
                {
                    case "australia":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like Sydney, Melbourne and Brisbane.";
                        metaKeywords.Attributes["content"] = "lead generation Australia, lead generation Sydney, lead generation Melbourne, lead generation Brisbane, RFQs Australia";
                        pgTitle.InnerText = "Lead generation network for local IT providers in Australia";

                        //Lbl1.Text = "RFQs and leads for the IT industry in Australia";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in Australia";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "canada":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like Toronto, Montreal and Calgary.";
                        metaKeywords.Attributes["content"] = "lead generation Canada, lead generation Toronto, lead generation Montreal, lead generation Calgary, RFQs Canada";
                        pgTitle.InnerText = "Lead generation network for local IT companies and MSPs in Canada";

                        //Lbl1.Text = "RFQs and leads for the IT industry in Canada";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in Canada";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "hong-kong":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in Hong Kong.";
                        metaKeywords.Attributes["content"] = "lead generation Hong Kong, RFQs Hong Kong, intent data Hong Kong";
                        pgTitle.InnerText = "Lead generation network for local IT companies in Hong Kong";

                        //Lbl1.Text = "RFQs and leads for the IT industry in Hong Kong";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in Hong Kong";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "india":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like Mumbai, Delhi and Bangalore.";
                        metaKeywords.Attributes["content"] = "lead generation India, lead generation Mumbai, lead generation Delhi, lead generation Bangalore, RFQs India";
                        pgTitle.InnerText = "Lead generation network for local IT companies in India";

                        //Lbl1.Text = "RFQs and leads for the IT industry in India";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in India";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "singapore":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in Singapore.";
                        metaKeywords.Attributes["content"] = "lead generation Singapore, intent data leads Singapore, RFQs Singapore";
                        pgTitle.InnerText = "Lead generation network for local IT companies in Singapore";

                        //Lbl1.Text = "RFQs and leads for the IT industry in Singapore";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in Singapore";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "south-africa":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like Cape Town, Johannesburg and Durban.";
                        metaKeywords.Attributes["content"] = "lead generation South Africa, lead generation Cape Town, lead generation Johannesburg, lead generation Durban, RFQs South Africa";
                        pgTitle.InnerText = "Lead generation network for local IT companies in South Africa";

                        //Lbl1.Text = "RFQs and leads for the IT industry in South Africa";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in South Africa";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "united-arab-emirates":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like Dubai, Abu Dhabi and Sharjah.";
                        metaKeywords.Attributes["content"] = "lead generation United Arab Emirates, lead generation Dubai, lead generation Abu Dhabi, lead generation Sharjah, RFQs United Arab Emirates";
                        pgTitle.InnerText = "Lead generation network for local IT companies in the UAE";

                        //Lbl1.Text = "RFQs and leads for the IT industry in the United Arab Emirates";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in the United Arab Emirates";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "united-kingdom":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like London, Birmingham and Manchester.";
                        metaKeywords.Attributes["content"] = "lead generation United Kingdom, lead generation London, lead generation Birmingham, lead generation Manchester, RFQs United Kingdom";
                        pgTitle.InnerText = "Lead generation network for local IT companies in the UK";

                        //Lbl1.Text = "RFQs and leads for the IT industry in the UK";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in the UK";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "united-states":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads for more than 5000 technologies focused on local IT companies in cities like New York, Los Angeles and Chicago.";
                        metaKeywords.Attributes["content"] = "lead generation United States, lead generation New York, lead generation Los Angeles, lead generation Chicago, RFQs United States";
                        pgTitle.InnerText = "Lead generation network for local IT companies in the US";

                        //Lbl1.Text = "RFQs and leads for the IT industry in the US";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for more than 5.000 different technologies in 125 product categories.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on product/technologies in the US";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;
                }
            }
            else
            {
                switch (keyName)
                {
                    case "sap":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on SAP products and services. Browse implementation, training and support leads for SAP technologies.";
                        metaKeywords.Attributes["content"] = "lead generation SAP, SAP leads, Successfactors leads, SAP Hana leads, SAP Business One leads";
                        pgTitle.InnerText = "Discover leads and RFQs for SAP partners – SAP lead generation network";

                        //Lbl1.Text = "RFQs and leads for SAP partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for SAP products like SAP Business One, Hana, Fiore, Successfactors and others.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for SAP partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "salesforce":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Salesforce products and services. Browse implementation, training and support leads for Salesforce technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Salesforce, Salesforce CRM leads, Salesforce RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Salesforce partners – Salesforce lead generation network";

                        //Lbl1.Text = "RFQs and leads for Salesforce partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Salesforce products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Salesforce partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "mcafee":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on McAfee products and services. Browse implementation, training and support leads for McAfee technologies.";
                        metaKeywords.Attributes["content"] = "lead generation McAfee, McAfee leads, McAfee RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for McAfee partners – McAfee lead generation network";

                        //Lbl1.Text = "RFQs and leads for McAfee partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for McAfee products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for McAfee partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "cisco":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Cisco products and services. Browse implementation, training and support leads for Cisco technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Cisco, Cisco leads, Cisco RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Cisco partners – Cisco lead generation network";

                        //Lbl1.Text = "RFQs and leads for Cisco partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Cisco products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Cisco partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "avaya":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Avaya products and services. Browse implementation, training and support leads for Avaya technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Avaya, Avaya leads, Avaya RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Avaya partners – Avaya lead generation network";

                        //Lbl1.Text = "RFQs and leads for Avaya partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Avaya products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Avaya partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "ibm":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on IBM products and services. Browse implementation, training and support leads for IBM technologies.";
                        metaKeywords.Attributes["content"] = "lead generation IBM, IBM leads, IBM RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for IBM partners – IBM lead generation network";

                        //Lbl1.Text = "RFQs and leads for IBM partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for IBM products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for IBM partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "microsoft":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Microsoft products and services. Browse implementation, training and support leads for Microsoft technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Microsoft, Microsoft leads, Microsoft RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Microsoft partners – Microsoft lead generation network";

                        //Lbl1.Text = "RFQs and leads for Microsoft partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Microsoft products like Microsoft Dynamics, Office 365, Microsoft 365, Azure and others.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Microsoft partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "fortinet":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Fortinet products and services. Browse implementation, training and support leads for Fortinet technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Fortinet, Fortinet leads, Fortinet RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Fortinet partners – Fortinet lead generation network";

                        //Lbl1.Text = "RFQs and leads for Fortinet partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Fortinet products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Fortinet partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "sophos":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Sophos products and services. Browse implementation, training and support leads for Sophos technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Sophos, Sophos leads, Sophos RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Sophos partners – Sophos lead generation network";

                        //Lbl1.Text = "RFQs and leads for Sophos partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Sophos products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Sophos partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "sage":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Sage products and services. Browse implementation, training and support leads for Sage technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Sage, Sage leads, Sage RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Sage partners – Sage lead generation network";

                        //Lbl1.Text = "RFQs and leads for Sage partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Sage products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Sage partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "oracle":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Oracle products and services. Browse implementation, training and support leads for Oracle technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Oracle, Oracle leads, Oracle RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Oracle partners – Oracle lead generation network";

                        //Lbl1.Text = "RFQs and leads for Oracle partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Oracle products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Oracle partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "arista-networks":
                    case "arista":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Arista Networks products and services. Browse implementation, training and support leads for Arista Networks technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Arista Networks, Arista Networks leads, Arista Networks RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Arista Networks partners – Arista Networks lead generation network";

                        //Lbl1.Text = "RFQs and leads for Arista Networks partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Arista Networks products for more than 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Arista Networks partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "zoho":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Zoho products and services. Browse implementation, training and support leads for Zoho technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Zoho, Zoho leads, Zoho RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Zoho partners – Zoho lead generation network";

                        //Lbl1.Text = "RFQs and leads for Zoho partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Zoho products like Zoho CRM, Zoho Books, Zoho Mail and others.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Zoho partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "cloudflare":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Cloudflare products and services. Browse implementation, training and support leads for Cloudflare technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Cloudflare, Cloudflare leads, Cloudflare RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Cloudflare partners – Cloudflare lead generation network";

                        //Lbl1.Text = "RFQs and leads for Cloudflare partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Cloudflare products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Cloudflare partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "crowdstrike":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on CrowdStrike products and services. Browse implementation, training and support leads for CrowdStrike technologies.";
                        metaKeywords.Attributes["content"] = "lead generation CrowdStrike, CrowdStrike leads, CrowdStrike RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for CrowdStrike partners – CrowdStrike lead generation network";

                        //Lbl1.Text = "RFQs and leads for CrowdStrike partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for CrowdStrike products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for CrowdStrike partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "dell":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Dell products and services. Browse implementation, training and support leads for Dell technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Dell, Dell leads, Dell RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Dell partners – Dell lead generation network";

                        //Lbl1.Text = "RFQs and leads for Dell partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Dell products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Dell partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "google":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Google products and services. Browse implementation, training and support leads for Google technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Google, Google leads, Google RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Google partners – Google lead generation network";

                        //Lbl1.Text = "RFQs and leads for Google partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Google products like Google Workplace, Google Cloud, Google Ads and more.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Google partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "kofax":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Kofax products and services. Browse implementation, training and support leads for Kofax technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Kofax, Kofax leads, Kofax RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Kofax partners – Kofax lead generation network";

                        //Lbl1.Text = "RFQs and leads for Kofax partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Kofax products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Kofax partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "lenovo":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Lenovo products and services. Browse implementation, training and support leads for Lenovo technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Lenovo, Lenovo leads, Lenovo RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Lenovo partners – Lenovo lead generation network";

                        //Lbl1.Text = "RFQs and leads for Lenovo partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Lenovo products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Lenovo partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "meraki":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Cisco Meraki products and services. Browse implementation, training and support leads for Meraki technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Meraki, Meraki leads, Meraki RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Meraki partners – Meraki lead generation network";

                        //Lbl1.Text = "RFQs and leads for Cisco Meraki partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Cisco Meraki products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Meraki partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "mimecast":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Mimecast products and services. Browse implementation, training and support leads for Mimecast technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Mimecast, Mimecast leads, Mimecast RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Mimecast partners – Mimecast lead generation network";

                        //Lbl1.Text = "RFQs and leads for Mimecast partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Mimecast products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Mimecast partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "netapp":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on NetApp products and services. Browse implementation, training and support leads for NetApp technologies.";
                        metaKeywords.Attributes["content"] = "lead generation NetApp, NetApp leads, NetApp RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for NetApp partners – NetApp lead generation network";

                        //Lbl1.Text = "RFQs and leads for NetApp partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for NetApp products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for NetApp partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "netsuite":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on NetSuite products and services. Browse implementation, training and support leads for NetSuite technologies.";
                        metaKeywords.Attributes["content"] = "lead generation NetSuite, NetSuite leads, NetSuite RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for NetSuite partners – NetSuite lead generation network";

                        //Lbl1.Text = "RFQs and leads for NetSuite partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for NetSuite products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for NetSuite partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "ringcentral":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on RingCentral products and services. Browse implementation, training and support leads for RingCentral technologies.";
                        metaKeywords.Attributes["content"] = "lead generation RingCentral, RingCentral leads, RingCentral RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for RingCentral partners – RingCentral lead generation network";

                        //Lbl1.Text = "RFQs and leads for RingCentral partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for RingCentral products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for RingCentral partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "symantec":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on Symantec products and services. Browse implementation, training and support leads for Symantec technologies.";
                        metaKeywords.Attributes["content"] = "lead generation Symantec, Symantec leads, Symantec RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for Symantec partners – Symantec lead generation network";

                        //Lbl1.Text = "RFQs and leads for Symantec partners";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for Symantec products for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for Symantec partners worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "crm-software":
                    case "crm":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on local IT providers that offer CRM software products and services. Browse implementation, training and support leads for CRM software technologies.";
                        metaKeywords.Attributes["content"] = "lead generation CRM software, CRM software leads, CRM software RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for CRM software companies and local IT providers";

                        //Lbl1.Text = "RFQs and leads for CRM providers";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for CRM software providers for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for CRM software providers worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "erp-software":
                    case "erp":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on local IT providers that offer ERP software products and services. Browse implementation, training and support leads for ERP software technologies.";
                        metaKeywords.Attributes["content"] = "lead generation ERP software, ERP software leads, ERP software RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for ERP software companies and local IT providers";

                        //Lbl1.Text = "RFQs and leads for ERP providers";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for ERP software providers for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for ERP software providers worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;

                    case "pos-software":
                    case "pos":

                        metaDescription.Attributes["content"] = "Discover new RFQs and intent data leads focused exclusively on local IT providers that offer POS software products and services. Browse implementation, training and support leads for POS software technologies.";
                        metaKeywords.Attributes["content"] = "lead generation POS software, POS software leads, POS software RFQs";
                        pgTitle.InnerText = "Discover leads and RFQs for POS software companies and local IT providers";

                        //Lbl1.Text = "RFQs and leads for POS providers";
                        //Lbl2.Text = "Discover daily requests for quotes and leads for POS software providers for more that 100+ countries worldwide.";
                        //Lbl3.Text = "View a sample of the latest quote requests and leads uploaded on our platform based on the location for POS software providers worldwide";
                        //Lbl4.Text = "Create an account for your company to showcase your products";
                        //Lbl5.Text = "Receive quotation requests from local businesses based on your targeting";
                        //Lbl6.Text = "Get access to intent data for businesses that have shown interest for your expertise";
                        //Lbl7.Text = "";
                        //Lbl8.Text = "RFQs";
                        //Lbl9.Text = "You will be receiving quote requests from local businesses based on your product portfolio and technologies.";
                        //Lbl10.Text = "Technologies";
                        //Lbl11.Text = "Our intent data uncover businesses that are in the market not only for product categories but also for specific technologies.";
                        //Lbl12.Text = "Local";
                        //Lbl13.Text = "We host more than 10.000 technology categories on a local level to connect you with businesses that are looking for your expertise.";

                        break;
                }
            }
        }

        #endregion

        #region Grids

        protected void RdgIntentData_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;
                        if (row != null)
                        {
                            Label lblProducts = (Label)ControlFinder.FindControlRecursive(item, "LblProducts");
                            HtmlImage imgCompanyLogo = (HtmlImage)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");

                            imgCompanyLogo.Src = row["company_logo"].ToString() != "" ? row["company_logo"].ToString() : "~/images/no_logo_company.png";

                            string itemProducts = row["product"].ToString();

                            if (itemProducts != "" && itemProducts.EndsWith(","))
                                itemProducts = itemProducts.Substring(0, itemProducts.Length - 1);

                            string[] products = itemProducts.Split(',');

                            if (products.Length > 0)
                            {
                                if (products[0] != "")
                                    lblProducts.Text = products[0];
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgIntentData_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    LoadData(session);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region Buttons

        protected void aLoadNext_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ++PageIndex;
                LoadData(session);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aLoadLess_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                --PageIndex;
                LoadData(session);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}