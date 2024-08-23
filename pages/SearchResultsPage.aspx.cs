using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Web;
using System.Linq;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using System.Collections.Generic;
using System.Data;
using System.Web.WebPages;
using AjaxControlToolkit;

namespace WdS.ElioPlus.pages
{
    public partial class SearchResultsPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        #region Properties

        public enum Navigation
        {
            None,
            First,
            Next,
            Previous,
            Last,
            Pager,
            Sorting
        }

        public int NowViewing
        {
            get
            {
                object obj = ViewState["_NowViewing"];
                if (obj == null)
                    return 0;
                else
                    return (int)obj;
            }
            set
            {
                this.ViewState["_NowViewing"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    UcNoResults.Visible = false;

                    UpdateStrings();
                    SetSearchTool();
                    SetLinks();
                    LoadTypes();
                    LoadIndustries();
                    LoadVerticals();
                    LoadPrograms();
                    LoadMarkets();
                    LoadApies();
                    LoadCountries();                    
                    LoadPanelData();                    
                    
                    LoadRepeater(Navigation.None);
                    
                    //if (!string.IsNullOrEmpty(vSession.SearchQueryString))
                    //{
                    //    LoadRepeater(Navigation.None);
                    //}
                    //else
                    //{
                    //    vSession.TechnologyCategory = "";
                    //    GlobalMethods.ClearCriteriaSession(vSession, true);
                    //    lbtnNextBottom.Visible = lbtnPrevBottom.Visible = false;
                    //    GlobalMethods.ShowMessageControl(UcNoResults, "Try again! No results were found that match your criteria.", MessageTypes.Info, true, true, false, false, false);
                    //}
                }

                FixPaymentBtns();
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

        private void LoadPanelData()
        {
            TbxCompanyName.Text = (vSession.CompanyNameViewState != string.Empty) ? vSession.CompanyNameViewState : string.Empty;
            LblType.Text = DdlCategory.SelectedItem.Text;
            searchResultType.HRef = (DdlCategory.SelectedItem.Text == "Vendors") ? ControlLoader.SearchForVendors : ControlLoader.SearchForChannelPartners;

            if (vSession.VerticalViewState != "0" || DdlVertical.SelectedItem.Value != "0")
            {
                LblHeader2.Text = " in category ";
                LblCategory.Text = DdlVertical.SelectedItem.Text;
            }
            else
            {
                if (DdlIndustry.SelectedItem.Value != "0")
                {
                    LblHeader2.Text = " in industry ";
                    LblCategory.Text = DdlIndustry.SelectedItem.Text;
                }
                else
                {
                    LblHeader2.Text = "";
                    LblCategory.Text = string.Empty;
                }
            }

            if (vSession.CountryViewState != "0" || DdlCountry.SelectedItem.Value != "0")
            {
                LblCountry.Text = DdlCountry.SelectedItem.Text;
                LblHeader3.Text = " based in ";
            }
            else
            {
                LblCountry.Text = "";
                LblHeader3.Text = "";
            }

            if (DdlCategory.SelectedItem.Text == "Channel Partners")
            {
                if (DdlIndustry.SelectedItem.Value != "0")
                {
                    LblHeader2.Text = " in industry ";
                    LblCategory.Text = DdlIndustry.SelectedItem.Text;
                }
                else
                {
                    LblHeader2.Text = "";
                    LblCategory.Text = string.Empty;
                }
            }

            //GlobalMethods.ShowMessageControl(UcNoResults, "Welcome! This is our search area. Select your criteria on your left and press Search!", MessageTypes.Info, true, true, false, false, false);

            //LblMoreThan.Text = "More than ";
            //LblUsersNumber.Text = "120.000";
            //LblUsersAction.Text = " companies are using Elioplus";
            //LblNoWait.Text = "What are you waiting for?";
            //LblSteps.Text = "3 Simple Steps to Get you started with Elioplus";
            //LblStep1Header.Text = "Choose a category";
            //LblStep1.Text = "1";
            //LblStep1Content.Text = "Start by selecting a category, between vendors and resellers according to your needs!";
            //LblStep2.Text = "2";
            //LblStep2Header.Text = "Pick an industry";
            //LblStep2Content.Text = "Select one of the industries displayed to refine your search and discover the most relevant products and companies!";
            //LblStep3.Text = "3";
            //LblStep3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "32")).Text;
            //LblStep3Content.Text = "Go premium to unlock all the features in the criteria area to be able to find exact matches for what you are looking for!";

            if (vSession.User == null)
            {
                //LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "17")).Text;
                //aGetElioNow.HRef = ControlLoader.SignUp;

                //LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "13")).Text;
                //aStepsButton.HRef = ControlLoader.Login;

                aSearchGoPremium.HRef = ControlLoader.SignUp;
            }
            else
            {
                if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    //LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "14")).Text;
                    //aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");

                    //LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "14")).Text;
                    //aStepsButton.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");

                    aSearchGoPremium.HRef = ControlLoader.Pricing;
                }
                else
                {
                    //LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "6")).Text;
                    //aGetElioNow.HRef = ControlLoader.Pricing;

                    //LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "62")).Text;
                    //aStepsButton.HRef = ControlLoader.Pricing;

                    aSearchGoPremium.HRef = ControlLoader.Pricing;
                }
            }
        }

        private void FixPaymentBtns()
        {
            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, false, ref showBtn, ref showModal, session);

            if (allowPayment)
            {
                //BtnSearchGoPremium.Visible = false; // showBtn;
                aSearchGoPremium.Visible = true; // showModal;
                //BtnGetElioNow.Visible = BtnStepsButton.Visible = BtnGoPremium.Visible = false;
                //aGetElioNow.Visible = aStepsButton.Visible = aGoPremium.Visible = true;
            }
            else
            {
                //BtnSearchGoPremium.Visible = false;
                //BtnGetElioNow.Visible = BtnStepsButton.Visible = BtnGoPremium.Visible = false;
                aSearchGoPremium.Visible = false;
                //aGetElioNow.Visible = aStepsButton.Visible = aGoPremium.Visible = false;
            }
        }

        private void SetSearchTool()
        {
            divSearchCountry.Visible = true;
            divSearchName.Visible = true;

            if (DdlCategory.SelectedValue == Convert.ToInt32(Types.Vendors).ToString())
            {
                #region Vendors

                divSearchIndustry.Visible = true;
                divSearchVertical.Visible = true;
                divSearchProgram.Visible = true;
                divSearchMarket.Visible = true;
                divSearchApi.Visible = true;

                DdlIndustry.Enabled = true;
                DdlVertical.Enabled = true;
                DdlProgram.Enabled = true;

                divSearchIndustryCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                divSearchVerticalCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                divSearchProgramCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                divSearchCountryCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";

                divIndustryInfo.Visible = false;
                divVerticalInfo.Visible = false;
                divProgramInfo.Visible = false;

                if ((vSession.User != null && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    DdlMarket.Enabled = true;
                    DdlApi.Enabled = true;

                    divMarketInfo.Visible = false;
                    divApiInfo.Visible = false;

                    divSearchMarketCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                    divSearchApiCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                }
                else
                {
                    DdlMarket.Enabled = false;
                    divMarketInfo.Visible = true;
                    LblMarketInfo.Text = "Premium ";
                    LblMarketInfoContent.Text = "feature";
                    divSearchMarketCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2 pointer-events-none opacity-60";

                    DdlApi.Enabled = false;
                    divApiInfo.Visible = true;
                    LblApiInfo.Text = "Premium ";
                    LblApiInfoContent.Text = "feature";
                    divSearchApiCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2 pointer-events-none opacity-60";
                }

                #endregion
            }
            else if (DdlCategory.SelectedValue == Convert.ToInt32(Types.Resellers).ToString())
            {
                #region Resellers

                divSearchIndustry.Visible = true;
                divSearchVertical.Visible = true;
                divSearchProgram.Visible = true;
                divSearchMarket.Visible = true;
                divSearchApi.Visible = false;

                DdlIndustry.Enabled = true;
                DdlMarket.Enabled = true;

                divSearchIndustryCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                divSearchMarketCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";

                divIndustryInfo.Visible = false;
                //divVerticalInfo.Visible = false;
                divMarketInfo.Visible = false;

                if ((vSession.User != null && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
                {
                    DdlProgram.Enabled = true;
                    divProgramInfo.Visible = false;
                    divSearchProgramCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                }
                else
                {
                    DdlProgram.Enabled = false;
                    divProgramInfo.Visible = true;
                    LblProgramInfo.Text = "Premium ";
                    LblProgramInfoContent.Text = "feature";
                    divSearchProgramCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2 pointer-events-none opacity-60";
                }
                #endregion
            }

            if ((vSession.User != null) && (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)) || (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session)))
            {
                DdlCountry.Enabled = true;
                divSearchCountryCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                divSearchNameCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2";
                TbxCompanyName.Enabled = true;

                divCountryInfo.Visible = false;
                divNameInfo.Visible = false;
                divSearchGoPremium.Visible = false;
            }
            else
            {
                DdlCountry.Enabled = false;
                divCountryInfo.Visible = true;
                LblCountryInfo.Text = "Premium ";
                LblCountryInfoContent.Text = "feature";
                divSearchCountryCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2 pointer-events-none opacity-60";
                vSession.CountryViewState = "0";

                TbxCompanyName.Enabled = false;
                divNameInfo.Visible = true;
                lblNameInfo.Text = "Premium ";
                LblNameInfoContent.Text = "feature";
                divSearchNameCss.Attributes["class"] = "text-sm lg:text-sm px-20px py-10px w-full lg:w-full text-left flex-col flex gap-5px text-textGray border-gray border-2 rounded-10px justify-center bg-gray2 pointer-events-none opacity-60";

                divSearchGoPremium.Visible = true;
                LblSearchGoPremium.Text = "Upgrade Now!";
            }
        }

        private void SeeCompanyDetails(GridDataItem item)
        {
            vSession.ElioCompanyDetailsView = null;

            if (vSession.User != null)
            {
                vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (vSession.ElioCompanyDetailsView != null)
                {
                    Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
                }
                else
                {
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "11")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "4")).Text;
                    //GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                }
            }
            else
            {
                Response.Redirect(ControlLoader.Login, false);
                //RadWindowManager1.RadAlert(Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "9")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "10")).Text, 560, 200, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "message", "8")).Text, null);
            }
        }

        private void UpdateStrings()
        {
            //LblSearchCategory.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "59")).Text;
            //LblSearchIndustry.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "60")).Text;
            //LblSearchVertical.Text = "In this vertical";
            //LblSearchProgram.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "61")).Text;
            //LblSearchMarket.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "62")).Text;
            //LblSearchApi.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "63")).Text;
            //LblSearchCountry.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "11")).Text;
            //LblSearchName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "12")).Text;
            //BtnSearch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "58")).Text;
        }

        private void SetLinks()
        {
            //aSearchViewMore.HRef = ControlLoader.SearchVerticals;
        }

        private void LoadTypes()
        {
            DdlCategory.Items.Clear();

            List<ElioUserTypes> types = Sql.GetUserPublicTypes(session);
            foreach (ElioUserTypes type in types)
            {
                ListItem item = new ListItem();

                item.Value = type.Id.ToString();
                item.Text = type.Description;

                DdlCategory.Items.Add(item);
            }

            DdlCategory.DataBind();

            if (vSession.CategoryViewState == "0")
                vSession.CategoryViewState = "1";

            DdlCategory.Items.FindByValue(vSession.CategoryViewState).Selected = true;
        }

        private void LoadIndustries()
        {
            DdlIndustry.Items.Clear();

            DdlIndustry.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioIndustries> industries = Sql.GetIndustries(session);
            foreach (ElioIndustries industry in industries)
            {
                ListItem item = new ListItem();

                item.Value = industry.Id.ToString();
                item.Text = industry.IndustryDescription;

                DdlIndustry.Items.Add(item);
            }

            DdlIndustry.DataBind();

            DdlIndustry.Items.FindByValue(vSession.IndustryViewState).Selected = true;
        }

        private void LoadVerticals()
        {
            DdlVertical.Items.Clear();

            DdlVertical.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioSubIndustriesGroupItems> verticals = Sql.GetAllVerticals(session);

            foreach (ElioSubIndustriesGroupItems vertical in verticals)
            {
                ListItem item = new ListItem();

                item.Value = vertical.Id.ToString();
                item.Text = vertical.Description;

                DdlVertical.Items.Add(item);
            }

            DdlVertical.DataBind();

            DdlVertical.Items.FindByValue(vSession.VerticalViewState).Selected = true;
        }

        private void LoadPrograms()
        {
            DdlProgram.Items.Clear();

            DdlProgram.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioPartners> partners = Sql.GetPartners(session);
            foreach (ElioPartners partner in partners)
            {
                ListItem item = new ListItem();

                item.Value = partner.Id.ToString();
                item.Text = partner.PartnerDescription;

                DdlProgram.Items.Add(item);
            }

            DdlProgram.DataBind();

            DdlProgram.Items.FindByValue(vSession.PartnerViewState).Selected = true;
        }

        private void LoadMarkets()
        {
            DdlMarket.Items.Clear();

            DdlMarket.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioMarkets> markets = Sql.GetMarkets(session);
            foreach (ElioMarkets market in markets)
            {
                ListItem item = new ListItem();

                item.Value = market.Id.ToString();
                item.Text = market.MarketDescription;

                DdlMarket.Items.Add(item);
            }

            DdlMarket.DataBind();

            DdlMarket.Items.FindByValue(vSession.MarketViewState).Selected = true;
        }

        private void LoadApies()
        {
            DdlApi.Items.Clear();

            DdlApi.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioApies> apies = Sql.GetApies(session);
            foreach (ElioApies api in apies)
            {
                ListItem item = new ListItem();

                item.Value = api.Id.ToString();
                item.Text = api.ApiDescription;

                DdlApi.Items.Add(item);
            }

            DdlApi.DataBind();

            DdlApi.Items.FindByValue(vSession.ApiViewState).Selected = true;
        }

        private void LoadCountries()
        {
            DdlCountry.Items.Clear();

            DdlCountry.Items.Add(new ListItem("-- Select --", "0"));

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                ListItem item = new ListItem();

                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountry.Items.Add(item);
            }

            DdlCountry.DataBind();

            DdlCountry.Items.FindByValue(vSession.CountryViewState).Selected = true;
        }

        protected void LoadRepeater(Navigation navigation)
        {
            try
            {
                session.OpenConnection();

                UcNoResults.Visible = false;

                List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(DdlCategory.SelectedItem.Text, DdlIndustry.SelectedItem.Value, DdlVertical.SelectedItem.Value, "", DdlProgram.SelectedItem.Value, DdlMarket.SelectedItem.Value, DdlApi.SelectedItem.Value, "", (DdlCountry.SelectedItem.Value == "0") ? "" : DdlCountry.SelectedItem.Text, "", "", TbxCompanyName.Text, "", vSession, session);

                if (users.Count > 0)
                {
                    //Create the object of PagedDataSource
                    PagedDataSource objPds = new PagedDataSource();

                    //Assign our data source to PagedDataSource object
                    objPds.DataSource = users;

                    //Set the allow paging to true
                    objPds.AllowPaging = true;

                    //Set the number of items you want to show
                    objPds.PageSize = 100;

                    //Based on navigation manage the NowViewing
                    switch (navigation)
                    {
                        case Navigation.Next:       //Increment NowViewing by 1
                            NowViewing++;
                            break;
                        case Navigation.Previous:   //Decrement NowViewing by 1
                            NowViewing--;
                            break;
                        default:                    //Default NowViewing set to 0
                            NowViewing = 0;
                            break;
                    }

                    //Set the current page index
                    objPds.CurrentPageIndex = NowViewing;

                    // Disable Prev, Next, First, Last buttons if necessary
                    lbtnPrevBottom.Visible = !objPds.IsFirstPage;
                    lbtnNextBottom.Visible = !objPds.IsLastPage;

                    //Assign PagedDataSource to repeater
                    RdgResults.DataSource = objPds;
                    RdgResults.DataBind();

                    LblSuccessMsgContent.Text = "<b>Opportunities:</b> Viewing <b>" + users.Count.ToString() + "</b> results out of <b>" + (Sql.GetPublicOpportunitiesSum(session) + Sql.GetAllPublicVendorsAndResellers(session)).ToString() + "+</b> partnership opportunities on our platform! Learn more ";
                }
                else
                {
                    RdgResults.DataSource = null;
                    RdgResults.DataBind();

                    LblSuccessMsgContent.Text = "<b>Opportunities:</b> Viewing <b>0</b> results out of <b>" + (Sql.GetPublicOpportunitiesSum(session) + Sql.GetAllPublicVendorsAndResellers(session)).ToString() + "+</b> partnership opportunities on our platform! Learn more ";

                    NowViewing = 0;
                    lbtnPrevBottom.Visible = lbtnNextBottom.Visible = false;

                    GlobalMethods.ShowMessageControl(UcNoResults, "Try again! No results were found that match your criteria.", MessageTypes.Info, true, true, false, false, false);
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

        #endregion

        #region Grids

        protected void RdgResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    if (item != null)
                    {
                        ElioUsersSearchInfo row = (ElioUsersSearchInfo)e.Item.DataItem;
                        if (row != null)
                        {
                            if (row.BillingType > 1)
                            {
                                HtmlGenericControl featured = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divFeatured");
                                featured.Visible = true;

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                HtmlAnchor aContact = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aContact");

                                if (!string.IsNullOrEmpty(row.WebSite))
                                {
                                    aWebsite.Visible = true;
                                }

                                aContact.Visible = true;
                                aContact.HRef = "/" + row.Id + ControlLoader.MessageQuote;
                                aContact.Target = "_blank";
                            }
                            else
                            {
                                HtmlAnchor aRFPsForm = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aRFPsForm");

                                aRFPsForm.Visible = true;
                                aRFPsForm.HRef = "/" + row.Id + ControlLoader.RequestQuote;
                            }

                            bool hasCompanyData = false;
                            ElioUsersPersonCompanies company = null;

                            string urlLink = "";
                            bool existCountry = true;

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                if (row.UserApplicationType == (int)UserApplicationType.ThirdParty)        // && userIndustries.Count == 0)
                                {
                                    hasCompanyData = ClearbitSql.ExistsClearbitCompany(row.Id, session);
                                    if (hasCompanyData)
                                        company = ClearbitSql.GetPersonCompanyByUserId(row.Id, session);

                                    if (hasCompanyData && company != null)
                                    {
                                        if (string.IsNullOrEmpty(row.CompanyLogo))
                                            if (!string.IsNullOrEmpty(company.Logo))
                                            {
                                                HtmlImage imgCompanyLogo = (HtmlImage)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                                                imgCompanyLogo.Src = company.Logo;
                                            }
                                    }
                                }

                                urlLink = (existCountry) ? "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "/profile/channel-partners/";

                                bool hasMSPs = Sql.HasUserMSPsProgram(row.Id, 7, session);

                                if (hasMSPs)
                                {
                                    HtmlGenericControl divMSPs = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divMSPs");
                                    HtmlAnchor aMSPs = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMSPs");

                                    divMSPs.Visible = true;

                                    string lnkMSP = "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/";

                                    if (!string.IsNullOrEmpty(row.State))
                                        lnkMSP += row.State.Replace(" ", "-").ToLower() + "/";

                                    if (!string.IsNullOrEmpty(row.City))
                                        lnkMSP += row.City.Replace(" ", "-").ToLower() + "/";

                                    aMSPs.HRef = lnkMSP + "channel-partners/managed-service-providers";
                                }
                            }
                            else
                            {
                                urlLink = "/partner-programs/vendors/";
                            }

                            if (!urlLink.StartsWith("/"))
                                urlLink = "/" + urlLink;

                            List<ElioSubIndustriesGroupItems> userVarticals = Sql.GetUserSubIndustriesGroupItems(row.Id, session);

                            if (userVarticals.Count > 0)
                            {
                                int count = 0;
                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert1");
                                    Label LblVert1 = (Label)ControlFinder.FindControlRecursive(item, "LblVert1");
                                    aVert1.Visible = true;
                                    aVert1.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert1.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert2");
                                    Label LblVert2 = (Label)ControlFinder.FindControlRecursive(item, "LblVert2");
                                    aVert2.Visible = true;
                                    aVert2.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert2.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert3");
                                    Label LblVert3 = (Label)ControlFinder.FindControlRecursive(item, "LblVert3");
                                    aVert3.Visible = true;
                                    aVert3.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert3.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert4");
                                    Label LblVert4 = (Label)ControlFinder.FindControlRecursive(item, "LblVert4");
                                    aVert4.Visible = true;
                                    aVert4.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert4.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert5");
                                    Label LblVert5 = (Label)ControlFinder.FindControlRecursive(item, "LblVert5");
                                    aVert5.Visible = true;
                                    aVert5.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert5.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert6");
                                    Label LblVert6 = (Label)ControlFinder.FindControlRecursive(item, "LblVert6");
                                    aVert6.Visible = true;
                                    aVert6.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert6.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert7");
                                    Label LblVert7 = (Label)ControlFinder.FindControlRecursive(item, "LblVert7");
                                    aVert7.Visible = true;
                                    aVert7.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert7.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert8");
                                    Label LblVert8 = (Label)ControlFinder.FindControlRecursive(item, "LblVert8");
                                    aVert8.Visible = true;
                                    aVert8.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert8.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert9");
                                    Label LblVert9 = (Label)ControlFinder.FindControlRecursive(item, "LblVert9");
                                    aVert9.Visible = true;
                                    aVert9.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert9.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert10");
                                    Label LblVert10 = (Label)ControlFinder.FindControlRecursive(item, "LblVert10");
                                    aVert10.Visible = true;
                                    aVert10.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert10.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (userVarticals.Count > 9)
                                {
                                    HtmlAnchor aVertMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVertMore");
                                    Label lblVertMore = (Label)ControlFinder.FindControlRecursive(item, "LblVertMore");
                                    Label LblVertMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblVertMoreNum");
                                    aVertMore.Visible = true;
                                    aVertMore.HRef = "";

                                    lblVertMore.Text = "more";
                                    LblVertMoreNum.Text = "+" + (userVarticals.Count - 10).ToString() + " ";

                                    aVertMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithVerticalsRestDescriptions(userVarticals);
                                }
                            }
                            else
                            {
                                HtmlGenericControl divCategoriesArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCategoriesArea");
                                divCategoriesArea.Visible = false;
                            }

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                if (!string.IsNullOrEmpty(row.City))
                                {
                                    HtmlAnchor aAllCityChannelPartners = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aAllCityChannelPartners");
                                    aAllCityChannelPartners.Attributes["class"] = "text-xs lg:text-xs py-5px px-10px bg-gray rounded-20px text-blue";

                                    aAllCityChannelPartners.HRef = "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/" + row.City.Replace(" ", "-").ToLower() + "/all-channel-partners/";

                                    if (row.Country != "United States" && row.Country != "United Kingdom" && row.Country != "Australia" && row.Country != "India" && row.City != "")
                                    {
                                        string link = "/" + row.CompanyRegion.ToLower().Replace(" ", "-").Replace("&", "and") + "/";

                                        if (row.Country == "France")
                                            link += "fr/";
                                        else if (row.Country == "Canada")
                                            link += "ca/";
                                        else if (row.Country == "Spain")
                                            link += "es/";
                                        else if (row.Country == "Germany")
                                            link += "de/";
                                        else if (row.Country == "Portugal" || row.Country == "Brazil")
                                            link += "pt/";
                                        else if (row.Country == "Austria")
                                            link += "at/";
                                        else if (row.Country == "Italy")
                                            link += "it/";
                                        else if (row.Country == "Argentina" || row.Country == "Bolivia" || row.Country == "Chile"
                                            || row.Country == "Colombia" || row.Country == "Costa Rica" || row.Country == "Dominican Republic"
                                            || row.Country == "Ecuador" || row.Country == "El Salvador" || row.Country == "Guatemala"
                                            || row.Country == "Honduras" || row.Country == "Mexico" || row.Country == "Panama"
                                            || row.Country == "Paraguay" || row.Country == "Peru" || row.Country == "Puerto Rico"
                                            || row.Country == "Uruguay" || row.Country == "Venezuela")
                                            link += "la/";
                                        else if (row.Country == "Netherlands")
                                            link += "nl/";
                                        else if (row.Country == "Poland")
                                            link += "pl/";

                                        link += row.Country.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + row.City.ToLower().Replace(" ", "-").Replace("&", "and") + "/all-channel-partners";
                                        aAllCityChannelPartners.HRef = link;
                                    }
                                }
                                else
                                {
                                    HtmlGenericControl divAllCity = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divAllCity");
                                    divAllCity.Visible = false;
                                }
                            }
                            else
                            {
                                HtmlGenericControl divAllCity = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divAllCity");
                                divAllCity.Visible = false;
                            }

                            string productUrl = "";

                            if (row.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                productUrl = (existCountry) ? "/" + row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "/profile/channel-partners/";

                            else
                                productUrl = "/partner-programs/vendors/";

                            if (!productUrl.StartsWith("/"))
                                productUrl = "/" + productUrl;

                            List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(row.Id, session);

                            if (userProducts.Count > 0)
                            {
                                int count = 0;
                                Label lblProducts = (Label)ControlFinder.FindControlRecursive(item, "LblProducts");
                                lblProducts.Text = (row.CompanyType == Types.Vendors.ToString()) ? "Partner Programs:" : "Products:";

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr1");
                                    Label LblPartPr1 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr1");
                                    aPartPr1.Visible = true;
                                    aPartPr1.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr1.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr2");
                                    Label LblPartPr2 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr2");
                                    aPartPr2.Visible = true;
                                    aPartPr2.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr2.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr3");
                                    Label LblPartPr3 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr3");
                                    aPartPr3.Visible = true;
                                    aPartPr3.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr3.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr4");
                                    Label LblPartPr4 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr4");
                                    aPartPr4.Visible = true;
                                    aPartPr4.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr4.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr5");
                                    Label LblPartPr5 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr5");
                                    aPartPr5.Visible = true;
                                    aPartPr5.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr5.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr6");
                                    Label LblPartPr6 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr6");
                                    aPartPr6.Visible = true;
                                    aPartPr6.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr6.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr7");
                                    Label LblPartPr7 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr7");
                                    aPartPr7.Visible = true;
                                    aPartPr7.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr7.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (userProducts.Count > 6)
                                {
                                    HtmlAnchor aPartPrMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPrMore");
                                    Label lblPartPrMore = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMore");
                                    Label lblPartPrMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMoreNum");
                                    aPartPrMore.Visible = true;
                                    aPartPrMore.HRef = "";

                                    lblPartPrMore.Text = "more";
                                    lblPartPrMoreNum.Text = "+" + (userProducts.Count - 7).ToString() + " ";

                                    HtmlImage iMoreProducts = (HtmlImage)ControlFinder.FindControlRecursive(item, "iMoreProducts");
                                    aPartPrMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithRegistrationProductsRestDescriptions(userProducts);
                                }
                            }
                            else
                            {
                                HtmlGenericControl divProductsArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divProductsArea");
                                divProductsArea.Visible = false;
                            }

                            if (string.IsNullOrEmpty(row.Overview) || row.Overview == "&nbsp;")
                            {
                                if (company != null)
                                {
                                    if (!string.IsNullOrEmpty(company.Description))
                                    {
                                        if (company.Description.Length > 1000)
                                        {
                                            row.Overview = GlobalMethods.FixParagraphsView(company.Description.Substring(0, 1000) + " ...");
                                        }
                                        else
                                        {
                                            row.Overview = GlobalMethods.FixParagraphsView(company.Description);
                                        }
                                    }
                                    else
                                    {
                                        row.Overview = "View the solutions, services and product portfolio of " + row.CompanyName;
                                    }
                                }
                                else
                                    row.Overview = "View the solutions, services and product portfolio of the company";
                            }
                            else
                            {
                                if (row.Overview.Length < 35)
                                {
                                    //user.Overview = (row["Overview"].ToString().EndsWith(".")) ? row["Overview"].ToString() : row["Overview"].ToString() + ". ";
                                    row.Overview = row.Overview + Environment.NewLine + "Check our profile for more details.";
                                }
                                else
                                    row.Overview = GlobalMethods.FixParagraphsView(row.Overview);
                            }

                            if (row.Overview != "")
                            {
                                if ((row.Overview.Contains("<br/>") || row.Overview.Contains("<br>")))
                                {
                                    row.Overview = row.Overview.Replace("<br/><br/><br/><br/>", "").Replace("<br/><br/><br/>", "").Replace("<br/><br/>", "").Replace("<br/>", "");
                                    row.Overview = row.Overview.Replace("<br><br><br><br>", "").Replace("<br><br><br>", "").Replace("<br><br>", "").Replace("<br>", "");
                                }

                                Label lblOverview = (Label)ControlFinder.FindControlRecursive(item, "LblOverview");
                                lblOverview.Text = row.Overview;
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

        #endregion

        #region Buttons

        protected void lbtnPrev_Click(object sender, EventArgs e)
        {
            LoadRepeater(Navigation.Previous);
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            LoadRepeater(Navigation.Next);
        }

        protected void BtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                //vSession.SearchQueryString = GlobalDBMethods.GetSearchResultsWithPagingNew(DdlCategory.SelectedItem.Value, DdlCategory.SelectedItem.Text, DdlIndustry.SelectedItem.Value, DdlVertical.SelectedItem.Value, DdlProgram.SelectedItem.Value, DdlMarket.SelectedItem.Value, DdlApi.SelectedItem.Value, DdlCountry.SelectedItem.Text, DdlCountry.SelectedItem.Value, "", TbxCompanyName.Text, vSession, session);

                vSession.ShowResultsPanel = true;
                vSession.CurrentGridPageIndex = 0;
                vSession.TechnologyCategory = "";
                vSession.VerticalViewState = (DdlVertical.SelectedItem.Value == "0") ? "0" : vSession.VerticalViewState;

                LoadRepeater(Navigation.None);

                LoadPanelData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Combo

        protected void DdlCategory_OnTextChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                DdlIndustry.SelectedValue = "0";
                DdlVertical.SelectedValue = "0";
                DdlProgram.SelectedValue = "0";
                DdlMarket.SelectedValue = "0";
                DdlApi.SelectedValue = "0";
                DdlCountry.SelectedValue = "0";
                TbxCompanyName.Text = string.Empty;
                //PnlResults.Visible = false;

                GlobalMethods.ClearSearchSession(DdlCategory.SelectedItem.Value, vSession);
                GlobalMethods.ClearCriteriaSession(vSession, false);
                SetSearchTool();
                LoadPanelData();
                //Response.Redirect(ControlLoader.Search, false);
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