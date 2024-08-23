using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web.UI;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Linq;
using System.Data;

namespace WdS.ElioPlus.pages
{
    public partial class SearchPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            scriptManager.RegisterPostBackControl(aSearch);

            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    GlobalMethods.ClearCriteriaSession(vSession, true);

                    UpdateStrings();
                    SetLinks();
                    LoadTypes();
                    LoadVerticals();
                    LoadCountries();

                    SetSearchTool();

                    //if (!string.IsNullOrEmpty(vSession.SearchQueryString))
                    //{
                    //    string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
                    //    if (path.Length > 0)
                    //    {
                    //        int startPage = 1;
                    //        int endPage = 70;
                    //        int itemsPerPage = 70;

                    //        Uri uri = new Uri(HttpContext.Current.Request.Url.ToString());
                    //        string page = HttpUtility.ParseQueryString(uri.Query).Get("page");
                    //        if (!string.IsNullOrEmpty(page))
                    //        {
                    //            int pageIndex = Convert.ToInt32(page);
                    //            int nextPage = pageIndex + 1;
                    //            startPage = (pageIndex - 1) * itemsPerPage;
                    //            endPage = (nextPage - 1) * itemsPerPage;

                    //        }

                    //        vSession.SearchQueryString = GlobalDBMethods.GetSearchResultsWithPaging(startPage, endPage, DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Value : "1", DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Text : Types.Vendors.ToString(), "0", DdlVertical.SelectedItem.Value, "0", "0", "0", DdlCountry.SelectedItem.Text, DdlCountry.SelectedItem.Value, "", "", vSession, session);
                    //        if (!string.IsNullOrEmpty(vSession.SearchQueryString))
                    //        {
                    //            //RdgResults.Rebind();
                    //        }
                    //    }
                    //}
                    //else
                    //{
                    //    vSession.TechnologyCategory = "";
                    //    GlobalMethods.ClearCriteriaSession(vSession, true);
                    //}
                }

                //RdgResults.ShowHeader = false;

                //PnlResults.Visible = (vSession.ShowResultsPanel) ? true : false;

                //if (!PnlResults.Visible)
                //{
                    //LoadPanelData();
                //}
                //else
                //{
                    //divNoResults.Visible = false;
                //}

                //RdgResults.CurrentPageIndex = (vSession.CurrentGridPageIndex != 0) ? vSession.CurrentGridPageIndex : 0;
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

        private void BuildPagination(int resultsCount, int pageItemsCount)
        {
            int CurrentPage = 0;
            Uri uri = new Uri(HttpContext.Current.Request.Url.ToString());
            string currentUrlPage = HttpUtility.ParseQueryString(uri.Query).Get("page");
            if (!string.IsNullOrEmpty(currentUrlPage))
            {
                CurrentPage = Convert.ToInt32(currentUrlPage);
            }

            int pages = resultsCount / pageItemsCount;
            int pagesMod = resultsCount % pageItemsCount;
            bool notBreak = false;

            if (pages > 0)
            {
                HtmlGenericControl ul = new HtmlGenericControl();
                ul.ID = "ulPaginationList";
                ul.Attributes["class"] = "pagination";

                HtmlGenericControl li = new HtmlGenericControl();
                li.ID = "liPaginationList_1";

                if (CurrentPage == 0)
                    li.Attributes["class"] = "active";

                HtmlAnchor aLink = new HtmlAnchor();
                aLink.ID = "aLink_1";
                aLink.Attributes["style"] = @"position: relative;
                                                    float: left;
                                                    padding: 6px 12px;
                                                    line - height: 1.42857;
                                                    text - decoration: none;
                                                    color: #337ab7;
                                                    background - color: #fff;
                                                    border: 1px solid #ddd;
                                                    margin - left: -1px; ";

                string url = Request.Url.AbsolutePath;
                aLink.HRef = url;

                Label lbl = new Label();
                lbl.ID = "Lbl_1";
                lbl.Text = "1";

                aLink.Controls.Add(lbl);
                li.Controls.Add(aLink);
                ul.Controls.Add(li);

                if (pages > 1)
                {
                    for (int i = 2; i <= pages; i++)
                    {
                        if (i <= 31)
                        {
                            li = new HtmlGenericControl();
                            li.ID = "liPaginationList_" + i.ToString();

                            if (CurrentPage == i)
                                li.Attributes["class"] = "active";

                            aLink = new HtmlAnchor();
                            aLink.ID = "aLink_" + i.ToString();
                            aLink.Attributes["style"] = @"position: relative;
                                                    float: left;
                                                    padding: 6px 12px;
                                                    line - height: 1.42857;
                                                    text - decoration: none;
                                                    color: #337ab7;
                                                    background - color: #fff;
                                                    border: 1px solid #ddd;
                                                    margin - left: -1px; ";

                            var pageValue = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                            pageValue.Set("page", i.ToString());
                            url = Request.Url.AbsolutePath;
                            aLink.HRef = url + "?" + pageValue;

                            lbl = new Label();
                            lbl.ID = "Lbl_" + i.ToString();
                            lbl.Text = i.ToString();

                            aLink.Controls.Add(lbl);
                            li.Controls.Add(aLink);
                            ul.Controls.Add(li);
                        }
                        else
                        {
                            li = new HtmlGenericControl();
                            li.ID = "liPaginationList_" + pages.ToString();

                            if (CurrentPage == i)
                                li.Attributes["class"] = "active";

                            aLink = new HtmlAnchor();
                            aLink.ID = "aLink_" + pages.ToString();
                            aLink.Attributes["style"] = @"position: relative;
                                                    float: left;
                                                    padding: 6px 12px;
                                                    line - height: 1.42857;
                                                    text - decoration: none;
                                                    color: #337ab7;
                                                    background - color: #fff;
                                                    border: 1px solid #ddd;
                                                    margin - left: -1px; ";

                            var pageValue = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                            pageValue.Set("page", pages.ToString());
                            url = Request.Url.AbsolutePath;
                            aLink.HRef = url + "?" + pageValue;

                            lbl = new Label();
                            lbl.ID = "Lbl_" + i.ToString();
                            lbl.Text = ">>";

                            aLink.Controls.Add(lbl);
                            li.Controls.Add(aLink);
                            ul.Controls.Add(li);

                            notBreak = true;

                            break;
                        }
                    }

                    if (pagesMod > 0 && !notBreak)
                    {
                        li = new HtmlGenericControl();
                        li.ID = "liPaginationList_" + (pages + 1).ToString();

                        if (CurrentPage == pages + 1)
                            li.Attributes["class"] = "active";

                        aLink = new HtmlAnchor();
                        aLink.ID = "aLink_" + (pages + 1).ToString();
                        aLink.Attributes["style"] = @"position: relative;
                                                    float: left;
                                                    padding: 6px 12px;
                                                    line - height: 1.42857;
                                                    text - decoration: none;
                                                    color: #337ab7;
                                                    background - color: #fff;
                                                    border: 1px solid #ddd;
                                                    margin - left: -1px; ";

                        var pageValue = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        pageValue.Set("page", (pages + 1).ToString());
                        url = Request.Url.AbsolutePath;
                        aLink.HRef = url + "?" + pageValue;

                        lbl = new Label();
                        lbl.ID = "Lbl_" + (pages + 1).ToString();
                        lbl.Text = (pages + 1).ToString();

                        aLink.Controls.Add(lbl);
                        li.Controls.Add(aLink);
                        ul.Controls.Add(li);
                    }
                }
                else if (pages == 1 && pagesMod > 0)
                {
                    li = new HtmlGenericControl();
                    li.ID = "liPaginationList_2";

                    if (CurrentPage == 2)
                        li.Attributes["class"] = "active";

                    aLink = new HtmlAnchor();
                    aLink.ID = "aLink_2";
                    aLink.Attributes["style"] = @"position: relative;
                                                    float: left;
                                                    padding: 6px 12px;
                                                    line - height: 1.42857;
                                                    text - decoration: none;
                                                    color: #337ab7;
                                                    background - color: #fff;
                                                    border: 1px solid #ddd;
                                                    margin - left: -1px; ";

                    var pageValue = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                    pageValue.Set("page", "2");
                    url = Request.Url.AbsolutePath;
                    aLink.HRef = url + "?" + pageValue;

                    lbl = new Label();
                    lbl.ID = "Lbl_2";
                    lbl.Text = "2";

                    aLink.Controls.Add(lbl);
                    li.Controls.Add(aLink);
                    ul.Controls.Add(li);
                }

                //PhPaginationContent.Controls.Clear();
                //PhPaginationContent.Controls.Add(ul);
            }
        }

        private void LoadPanelData()
        {
            //divNoResults.Visible = true;
            //LblNoResults.Text = "Welcome! ";
            //LblNoResultsContent.Text = "This is our search area. Select your criteria on your left and press Search!";
            //LblMoreThan.Text = "More than ";
            //LblUsersNumber.Text = "120.000";
            //LblUsersAction.Text = " companies are using Elioplus";
            //LblNoWait.Text = "What are you waiting for?";
            //LblStep1Header.Text = "Choose a category";
            //LblStep1.Text = "1";
            //LblStep1Content.Text = "Select if you are browsing for partner programs by vendors or looking for channel partners";
            //LblStep2.Text = "2";
            //LblStep2Header.Text = "Target specific locations";
            //LblStep2Content.Text = "Choose between 125 different products and select the market you are interested in";
            //LblStep3.Text = "3";
            //LblStep3Header.Text = "Get connected";
            //LblStep3Content.Text = "Browse thousands of IT companies around the world and get connected by signing up for free";

            //if (vSession.User == null)
            //{
            //    LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "17")).Text;
            //    aGetElioNow.HRef = ControlLoader.SignUp;

            //    LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "13")).Text;
            //    aStepsButton.HRef = ControlLoader.Login;
            //}
            //else
            //{
            //    if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //    {
            //        LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "14")).Text;
            //        aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");

            //        LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "14")).Text;
            //        aStepsButton.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");
            //    }
            //    else
            //    {
            //        LblGetElioNow.Text = BtnGetElioNow.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "6")).Text;
            //        aGetElioNow.HRef = ControlLoader.Pricing;

            //        LblStepsButton.Text = BtnStepsButton.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "62")).Text;
            //        aStepsButton.HRef = ControlLoader.Pricing;
            //    }
            //}
        }

        private void SetSearchTool()
        {
            //divSearchCountry.Visible = true;
            //divSearchName.Visible = true;

            if (DdlCategory.SelectedValue == Convert.ToInt32(Types.Vendors).ToString())
            {
                #region Vendors

                DdlVertical.Enabled = true;

                #endregion
            }
            else if (DdlCategory.SelectedValue == Convert.ToInt32(Types.Resellers).ToString())
            {
                #region Resellers

                #endregion
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
            aSignUp.Visible = vSession.User == null;
        }

        private void SetLinks()
        {
            aSignUp.HRef = ControlLoader.SignUp;
            aRegister.HRef = (vSession.User == null) ? ControlLoader.SignUp : ControlLoader.Pricing;
        }

        private void LoadVerticals()
        {
            DdlVertical.Items.Clear();

            DdlVertical.Items.Add(new ListItem("-- Select Category --", "0"));

            List<ElioSubIndustriesGroupItems> verticals = new List<ElioSubIndustriesGroupItems>();

            verticals = Sql.GetAllVerticals(session);

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

        private void LoadTypes()
        {
            DdlCategory.Items.Clear();

            //DdlCategory.Items.Add(new ListItem("-- Select Type --", "0"));

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

        private void LoadCountries()
        {
            DdlCountry.Items.Clear();

            DdlCountry.Items.Add(new ListItem("-- Select Country --", "0"));

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

        #endregion

        #region Grids

        protected void RdgResults_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgResults_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridNoRecordsItem)
                {
                    GridNoRecordsItem item = (GridNoRecordsItem)e.Item;
                    Literal ltlNoDataFound = (Literal)ControlFinder.FindControlRecursive(item, "LtlNoDataFound");
                    ltlNoDataFound.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "grid", "1", "literal", "1")).Text;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgResults_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        item["id"].Text = user.Id.ToString();
                        //RdgResults.MasterTableView.GetColumn("id").Display = false;

                        Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");
                        lblFeature.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "10")).Text;

                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                        {
                            HtmlGenericControl featured = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divFeatured");
                            featured.Visible = true;
                        }

                        HtmlAnchor companyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        companyLogo.HRef = ControlLoader.Profile(user);

                        Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                        imgCompanyLogo.ImageUrl = (user.CompanyLogo != "") ? user.CompanyLogo : "/Images/no_logo.jpg";
                        imgCompanyLogo.AlternateText = user.CompanyName + " on Elioplus";

                        Label lblCompanyAddress = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyAddress");
                        lblCompanyAddress.Text = user.Country;

                        Label lblCompany = (Label)ControlFinder.FindControlRecursive(item, "LblCompany");
                        lblCompany.Text = user.CompanyName;

                        HtmlAnchor companyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                        companyName.HRef = ControlLoader.Profile(user);

                        Image r1 = (Image)ControlFinder.FindControlRecursive(item, "r1");
                        Image r2 = (Image)ControlFinder.FindControlRecursive(item, "r2");
                        Image r3 = (Image)ControlFinder.FindControlRecursive(item, "r3");
                        Image r4 = (Image)ControlFinder.FindControlRecursive(item, "r4");
                        Image r5 = (Image)ControlFinder.FindControlRecursive(item, "r5");

                        Label lblTotalRatingValue = (Label)ControlFinder.FindControlRecursive(item, "LblTotalRatingValue");

                        decimal avg = 0;

                        if (item["company_type"].Text == Types.Vendors.ToString())
                            avg = Convert.ToDecimal(item["r"].Text);  //Sql.GetCompanyAverageRating(user.Id, session);                        

                        int average = Convert.ToInt32(avg);

                        lblTotalRatingValue.Text = "(" + avg.ToString("0.00") + ")";

                        #region Average Area

                        switch (average)
                        {
                            case 0:
                                r1.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                                break;

                            case 1:
                                r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                                r2.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                break;

                            case 2:
                                r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                                r3.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                                break;

                            case 3:
                                r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                                r4.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";
                                break;

                            case 4:
                                r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                                r5.ImageUrl = "~/images/ratings/ratingStarEmpty.gif";

                                break;

                            case 5:
                                r1.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r2.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r3.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r4.ImageUrl = "~/images/ratings/ratingStarFilled.gif";
                                r5.ImageUrl = "~/images/ratings/ratingStarFilled.gif";

                                break;
                        }

                        #endregion

                        Label lblIndustry = (Label)ControlFinder.FindControlRecursive(item, "LblIndustry");
                        lblIndustry.Text = "Industry";

                        Label lblIndustryValue = (Label)ControlFinder.FindControlRecursive(item, "LblIndustryValue");
                        lblIndustryValue.Text = string.Empty;

                        HtmlGenericControl iMore = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "iMoreIndustries");
                        iMore.Attributes["title"] = string.Empty;
                        iMore.Visible = false;

                        bool hasCompanyData = false;
                        ElioUsersPersonCompanies company = null;

                        //List<string> userIndustries = Sql.GetIndustiesDescriptionsIJUserIndustries(user.Id, session);
                        string userIndustriesDescription = Sql.GetIndustiesDescriptionsIJUserIndustriesAsString(user.Id, session);

                        if (user.UserApplicationType == (int)UserApplicationType.ThirdParty && userIndustriesDescription == "")
                        {
                            hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);
                            if (hasCompanyData)
                                company = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
                        }

                        if (userIndustriesDescription != "")
                        {
                            lblIndustryValue.Text = (userIndustriesDescription.Split(',').ToArray()[0] != "") ? userIndustriesDescription.Split(',').ToArray()[0] : userIndustriesDescription.Replace(", ", "");
                            iMore.Attributes["title"] = (userIndustriesDescription.Split(',').ToArray().Length > 1) ? userIndustriesDescription.Replace(", ", Environment.NewLine) : userIndustriesDescription.Replace(", ", "");
                            iMore.Visible = true;
                        }
                        else
                        {
                            if (hasCompanyData && company != null)
                            {
                                if (string.IsNullOrEmpty(user.CompanyLogo))
                                    if (!string.IsNullOrEmpty(company.Logo))
                                        imgCompanyLogo.ImageUrl = company.Logo;

                                string tagnames = ClearbitSql.GetPersonCompanyTagsAsString(company.Id, session);

                                if (tagnames != "")
                                {
                                    lblIndustryValue.Text = (tagnames.Split(',').ToArray()[0] != "") ? (tagnames.Split(',').ToArray()[0].Length <= 22) ? tagnames.Split(',').ToArray()[0] : tagnames.Split(',').ToArray()[0].Substring(0, 22) + "..." : tagnames.Replace(", ", "");
                                    iMore.Attributes["title"] = (tagnames.Split(',').ToArray().Length > 1) ? tagnames.Replace(", ", Environment.NewLine) : tagnames.Replace(", ", "");
                                    iMore.Visible = true;

                                    //List<ElioUsersPersonCompanyTags> tags = ClearbitSql.GetPersonCompanyTags(company.Id, session);
                                    //List<string> companyTags = new List<string>();

                                    //foreach (ElioUsersPersonCompanyTags tag in tags)
                                    //{
                                    //    companyTags.Add(tag.TagName);
                                    //}

                                    //lblIndustryValue.Text = tags[0].TagName;
                                    //if (companyTags.Count > 1)
                                    //{
                                    //    iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithIndustriesDescriptions(companyTags);
                                    //    iMore.Visible = true;
                                    //}
                                }
                                else
                                    lblIndustryValue.Text = "-";
                            }
                            else
                                lblIndustryValue.Text = "-";
                        }

                        //if (userIndustries.Count > 0)
                        //{
                        //    lblIndustryValue.Text = userIndustries[0];
                        //    if (userIndustries.Count > 1)
                        //    {
                        //        iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithIndustriesDescriptions(userIndustries);
                        //        iMore.Visible = true;
                        //    }
                        //}
                        //else
                        //{
                        //    if (hasCompanyData && company != null)
                        //    {
                        //        if (string.IsNullOrEmpty(user.CompanyLogo))
                        //            if (!string.IsNullOrEmpty(company.Logo))
                        //                imgCompanyLogo.ImageUrl = company.Logo;

                        //        List<ElioUsersPersonCompanyTags> tags = ClearbitSql.GetPersonCompanyTags(company.Id, session);

                        //        if (tags.Count > 0)
                        //        {
                        //            List<string> companyTags = new List<string>();

                        //            foreach (ElioUsersPersonCompanyTags tag in tags)
                        //            {
                        //                companyTags.Add(tag.TagName);
                        //            }

                        //            lblIndustryValue.Text = tags[0].TagName;
                        //            if (companyTags.Count > 1)
                        //            {
                        //                iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithIndustriesDescriptions(companyTags);
                        //                iMore.Visible = true;
                        //            }
                        //        }
                        //        else
                        //            lblIndustryValue.Text = "-";
                        //    }
                        //    else
                        //        lblIndustryValue.Text = "-";
                        //}

                        Label lblViews = (Label)ControlFinder.FindControlRecursive(item, "LblViews");
                        lblViews.Text = "Views";
                        lblViews.Visible = false;

                        Label lblViewsValue = (Label)ControlFinder.FindControlRecursive(item, "LblViewsValue");
                        //lblViewsValue.Text = item["totalViews"].Text;

                        if (item["address"].Text == "&nbsp;")
                            lblViewsValue.Text = "-";
                        else
                            lblViewsValue.Text = (item["address"].Text.Length > 20) ? item["address"].Text.Substring(0, 20) + "..." : item["address"].Text;

                        //lblViewsValue.Text = Sql.GetCompanyTotalViews(user.Id, session).ToString();

                        Label lblOverview = (Label)ControlFinder.FindControlRecursive(item, "LblOverview");
                        if (user.Overview.Length >= 250)
                        {
                            lblOverview.Text = GlobalMethods.FixParagraphsView(user.Overview.Substring(0, 250) + "...");
                        }
                        else
                        {
                            lblOverview.Text = GlobalMethods.FixParagraphsView(user.Overview);

                            if (string.IsNullOrEmpty(user.Overview))
                            {
                                string[] countries = new string[] { "Afghanistan", "Australia", "Bahrain", "Bangladesh", "Bahamas", "Barbados", "Cyprus", "India", "Ireland", "Jamaica", "New Zealand", "Saudi Arabia", "Singapore", "South Africa", "United Arab Emirates", "United Kingdom", "United States" };

                                if (hasCompanyData && company != null)
                                {
                                    if (!string.IsNullOrEmpty(company.Description))
                                    {
                                        if (company.Description.Length >= 250)
                                        {
                                            lblOverview.Text = GlobalMethods.FixParagraphsView(company.Description.Substring(0, 250) + "...");
                                        }
                                        else
                                        {
                                            lblOverview.Text = GlobalMethods.FixParagraphsView(company.Description);
                                        }
                                    }
                                    else
                                    {
                                        if (countries.Contains(user.Country))
                                        {
                                            lblOverview.Text = "View the solutions, services and product portfolio of " + user.CompanyName;
                                        }
                                        else
                                        {
                                            lblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
                                        }
                                    }
                                }
                                else
                                {
                                    if (countries.Contains(user.Country))
                                    {
                                        lblOverview.Text = "View the solutions, services and product portfolio of " + user.CompanyName;
                                    }
                                    else
                                    {
                                        lblOverview.Text = "Oups, we are sorry but there are no description data for this company.";
                                    }
                                }
                            }
                            else
                            {
                                if (user.Overview.Length < 35)
                                {
                                    user.Overview = (user.Overview.EndsWith(".")) ? user.Overview : user.Overview + ". ";
                                    lblOverview.Text = user.Overview + Environment.NewLine + "Check our profile for more details.";
                                }
                            }
                        }

                        if (lblOverview.Text != "")
                        {
                            lblOverview.Text = lblOverview.Text.Replace("<br/><br/><br/><br/>", "").Replace("<br/><br/><br/>", "").Replace("<br/><br/>", "").Replace("<br/>", "");
                            lblOverview.Text = lblOverview.Text.Replace("<br><br><br><br>", "").Replace("<br><br><br>", "").Replace("<br><br>", "").Replace("<br>", "");
                        }

                        Label lblViewProfile = (Label)ControlFinder.FindControlRecursive(item, "LblViewProfile");
                        lblViewProfile.Text = "View profile";

                        HtmlAnchor viewProfile = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewProfile");
                        viewProfile.HRef = ControlLoader.Profile(user);
                    }
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

        protected void RdgResults_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                //divInfoMsg.Visible = false;
                //divWarningMsg.Visible = false;
                //divGoPremium.Visible = false;
                //divSuccessMsg.Visible = false;

                session.OpenConnection();

                DataTable resultsTbl = null;

                int startPage = 1;
                int endPage = 70;
                int itemsPerPage = 70;

                Uri uri = new Uri(HttpContext.Current.Request.Url.ToString());
                string page = HttpUtility.ParseQueryString(uri.Query).Get("page");
                if (!string.IsNullOrEmpty(page))
                {
                    int pageIndex = Convert.ToInt32(page);
                    int nextPage = pageIndex + 1;
                    startPage = (pageIndex - 1) * itemsPerPage;
                    endPage = (nextPage - 1) * itemsPerPage;
                }

                vSession.SearchQueryString = GlobalDBMethods.GetSearchResultsWithPaging(startPage, endPage, DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Value : "1", DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Text : Types.Vendors.ToString(), "0", DdlVertical.SelectedItem.Value, "0", "0", "0", DdlCountry.SelectedItem.Value != "0" ? DdlCountry.SelectedItem.Text : "", DdlCountry.SelectedItem.Value != "0" ? DdlCountry.SelectedItem.Value : "0", "", "", vSession, session);

                int resultsCount = GlobalDBMethods.GetSearchResultsCount(DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Text : Types.Vendors.ToString(), DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Value : "1", "0", DdlVertical.SelectedItem.Value, "0", "0", "0", "0", (DdlCountry.SelectedItem.Value != "0") ? DdlCountry.SelectedItem.Text : "", (DdlCountry.SelectedItem.Value != "0") ? DdlCountry.SelectedItem.Value : "0", "", "", vSession, session);

                if (!string.IsNullOrEmpty(vSession.SearchQueryString))
                {
                    resultsTbl = session.GetDataTable(vSession.SearchQueryString);
                    BuildPagination(resultsCount, 70);
                }
                else
                {
                    vSession.ShowResultsPanel = false;
                    //PnlResults.Visible = false;
                }

                if (resultsTbl.Rows.Count > 0)     //(results.Count > 0)
                {
                    //RdgResults.Visible = true;
                    //PnlResults.Visible = true;
                    //divNoResults.Visible = false;
                    vSession.ShowResultsPanel = true;

                    //divSuccessMsg.Visible = true;
                    //LblSuccessMsg.Text = "Opportunities: ";
                    //LblSuccessMsgContent.Text = resultsCount.ToString() + " results out of 25000+ partnership opportunities on our platform!";

                    //if ((vSession.User != null) && (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)))
                    //{
                    //    divInfoMsg.Visible = false;
                    //    divGoPremium.Visible = false;
                    //}
                    //else
                    //{
                    //    divInfoMsg.Visible = true;
                    //    LblGoPremium.Text = BtnGoPremium.Text = LblInfoMsg.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "62")).Text;
                    //    LblInfoMsgContent.Text = "Unlock all the search features and discover products and companies more relevant to your needs!";

                    //    divGoPremium.Visible = true;
                    //}

                    //RdgResults.DataSource = resultsTbl;
                }
                else
                {
                    GlobalMethods.ClearCriteriaSession(vSession, false);
                    //RdgResults.Visible = false;
                    //divWarningMsg.Visible = true;
                    //LblWarningMsg.Text = "Try again! ";
                    //LblWarningMsgContent.Text = "No results were found that match your criteria.";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void BtnSearchGoPremium_OnClick(object sender, EventArgs args)
        {
            if (vSession.User == null)
            {
                Response.Redirect(ControlLoader.Login, false);
            }
        }

        protected void ImgBtnCompanyLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtnCompanyLogo = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtnCompanyLogo.NamingContainer;

                SeeCompanyDetails(item);
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

        protected void LnkBtnDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton lnkBtnDetails = (LinkButton)sender;
                GridDataItem item = (GridDataItem)lnkBtnDetails.NamingContainer;

                SeeCompanyDetails(item);
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

        protected void aSearch_ServerClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.CategoryViewState = DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Value : "1";
                vSession.VerticalViewState = DdlVertical.SelectedItem.Value;
                vSession.CountryViewState = DdlCountry.SelectedItem.Value;

                //vSession.SearchQueryString = GlobalDBMethods.GetSearchResults(DdlCategory.SelectedValue, DdlCategory.SelectedItem.Text, "0", DdlVertical.SelectedValue, "0", "0", "0", DdlCountry.SelectedItem.Text, DdlCountry.SelectedItem.Value, string.Empty, vSession, session);
                //vSession.SearchQueryString = GlobalDBMethods.GetSearchResultsWithPaging(1, 50, DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Value : "1", DdlCategory.SelectedItem.Value != "0" ? DdlCategory.SelectedItem.Text : Types.Vendors.ToString(), "0", DdlVertical.SelectedItem.Value, "0", "0", "0", DdlCountry.SelectedItem.Value != "0" ? DdlCountry.SelectedItem.Text : "", DdlCountry.SelectedItem.Value != "0" ? DdlCountry.SelectedItem.Value : "0", "", "", vSession, session);

                //vSession.ShowResultsPanel = true;
                //vSession.CurrentGridPageIndex = 0;
                //vSession.TechnologyCategory = "";

                if (DdlCategory.SelectedItem.Value == "0")
                    DdlCategory.Items.FindByValue((Convert.ToInt32(Types.Vendors)).ToString()).Selected = true;

                //RdgResults.Rebind();

                Response.Redirect(ControlLoader.SearchResults, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Combo

        protected void DdlCategory_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                DdlVertical.SelectedValue = "0";
                DdlCountry.SelectedValue = "0";
                //PnlResults.Visible = false;

                LoadPanelData();

                GlobalMethods.ClearSearchSession(DdlCategory.SelectedItem.Value, vSession);
                GlobalMethods.ClearCriteriaSession(vSession, false);
                SetSearchTool();
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