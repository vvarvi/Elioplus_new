using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using System.Data;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web;

namespace WdS.ElioPlus
{
    public partial class ProfilesPartnerTypes : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public string Type
        {
            get
            {
                if (ViewState["Type"] != null)
                    return ViewState["Type"].ToString();
                else
                    return "vendors";
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            HtmlGenericControl header = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "header");
            header.Attributes["class"] = "header headbg-img navbar-fixed-top";

            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    HtmlGenericControl li = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "liMainMenuMore");
                    li.Attributes["class"] = "active nav-item dropdown";

                    UpdateStrings();
                    FixPage();
                    SetLinks();
                }

                RdgResults.ShowHeader = false;
                string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
                if (path.Length > 0)
                {
                    int startPage = 1;
                    int endPage = 50;

                    string page = Request.QueryString["page"];
                    if (!string.IsNullOrEmpty(page))
                    {
                        int pageIndex = Convert.ToInt32(page);
                        int nextPage = pageIndex + 1;
                        //aNextPage.HRef = HttpContext.Current.Request.Url.AbsolutePath + "?page=" + nextPage;
                        startPage = (pageIndex - 1) * 50;
                        endPage = (nextPage - 1) * 50;

                        if (pageIndex > 2)
                        {
                            int previousPage = pageIndex - 1;
                            aPreviousPage.Visible = true;
                            aPreviousPage.HRef = HttpContext.Current.Request.Url.AbsolutePath + "?page=" + previousPage;
                        }
                        else
                        {
                            aPreviousPage.HRef = HttpContext.Current.Request.Url.AbsolutePath;
                            aPreviousPage.Visible = true;
                        }

                        var pageValue = HttpUtility.ParseQueryString(Request.QueryString.ToString());
                        pageValue.Set("page", nextPage.ToString());
                        string url = Request.Url.AbsolutePath;
                        aNextPage.HRef = url + "?" + pageValue;
                    }
                    else
                    {
                        aNextPage.HRef = HttpContext.Current.Request.Url.AbsolutePath + "?page=2";
                        aPreviousPage.Visible = false;
                    }

                    FixPageResultsContent(path, startPage, endPage);
                }
                else
                {
                    //divIndustriesResults.Visible = false;
                    Response.Redirect(ControlLoader.Default(), false);
                }

                PnlResults.Visible = vSession.ShowResultsPanel = true;

                if (vSession.SearchQueryString != "")
                {
                    vSession.ShowResultsPanel = true;
                    PnlResults.Visible = true;
                }
                else
                    Response.Redirect(ControlLoader.SearchByType(path[path.Length - 2]), false);

                RdgResults.CurrentPageIndex = (vSession.CurrentGridPageIndex != 0) ? vSession.CurrentGridPageIndex : 0;
                RdgResults.MasterTableView.GetColumn("id").Display = false;
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

        private void FixPageResultsContent(string[] path, int startPage, int endPage)
        {
            string type = GlobalMethods.FixSearchDescriptionBack(path[path.Length - 2]);
            if (type.StartsWith("vendors"))
                Type = Types.Vendors.ToString().ToLower();
            else
            {
                Type = EnumHelper.GetDescription(Types.Resellers).ToString();
                Type = Type.Replace(" ", "-").ToLower();
            }

            string program = path[path.Length - 3];
            if (program != "")
            {
                program = program.Replace("-", " ");
                if (program == "msps")
                    program = "Managed Service Provider";
                else if (program == "system integrators")
                    program = "system integrator";

                ElioPartners partnerProgram = Sql.GetPartnerByDescription(program, session);
                if (partnerProgram == null)
                {
                    partnerProgram = Sql.GetPartnerByDescription("White Label", session);
                    if (partnerProgram == null)
                    {
                        Response.Redirect(ControlLoader.SearchByType(Type), false);
                    }
                }

                string subIndustry = path[path.Length - 1];
                subIndustry = subIndustry.Replace("and", "&").Replace("_", " ");

                HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Master, "metaDescription");
                HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Master, "metaKeywords");

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(subIndustry, session);
                if (vertical != null)
                {
                    vSession.TechnologyCategory = "";

                    string content1 = "";
                    string content2 = "";
                    string content3 = "";
                    string lnk1 = "";
                    string lnk2 = "";
                    string lnk3 = "";
                    string lnk4 = "";
                    string lnk5 = "";
                    string lnk6 = "";
                    string lnk7 = "";
                    string lnk8 = "";

                    string metaDescription = "";
                    string metaKeywords = "";

                    string pageTopic = GlobalMethods.SetProfilePageContentPartnerProgram(partnerProgram.PartnerDescription, vertical.Description, Type.ToLower(), out content1, out content2, out content3,
                        out lnk1, out lnk2, out lnk3, out lnk4,
                        out lnk5, out lnk6, out lnk7, out lnk8,
                        out metaDescription, out metaKeywords);

                    LblResultsTitle.Text = pageTopic;
                    LblResultsContent1.Text = content1;
                    LblResultsContent2.Text = content2;
                    LblResultsContent3.Text = content3;

                    LblFooterCompany1.Text = lnk1;
                    LblFooterCompany2.Text = lnk2;
                    LblFooterCompany3.Text = lnk3;
                    LblFooterCompany4.Text = lnk4;
                    LblFooterCompany5.Text = lnk5;
                    LblFooterCompany6.Text = lnk6;
                    LblFooterCompany7.Text = lnk7;
                    LblFooterCompany8.Text = lnk8;

                    LblFooterRelatedCompanies.Visible = lnk1 != "" || lnk2 != "" || lnk3 != "" || lnk4 != "" || lnk5 != "" || lnk6 != "" || lnk7 != "" || lnk8 != "";

                    //aFooterCompany1.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk1.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany2.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk2.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany3.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk3.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany4.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk4.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany5.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk5.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany6.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk6.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany7.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk7.Replace("&", "and").Replace(" ", "_").ToLower());
                    //aFooterCompany8.HRef = ControlLoader.PartnerProgramSubIndustryProfiles(Type, lnk8.Replace("&", "and").Replace(" ", "_").ToLower());

                    metaHeadDescription.Attributes["content"] = metaDescription;
                    metaHeadKeywords.Attributes["content"] = metaKeywords;

                    vSession.SearchQueryString = GlobalDBMethods.GetSEOSearchResults(startPage, endPage, Type, "0", vertical.Id.ToString(), "", partnerProgram.Id.ToString(), "0", "0", "", "", "", "", "", "", vSession, session);
                    if (string.IsNullOrEmpty(vSession.SearchQueryString))
                        Response.Redirect(ControlLoader.SearchByType(Type));
                    else
                    {
                        int resultsCount = GlobalDBMethods.GetSearchResultsCount(Type, "1", "0", vertical.Id.ToString(), partnerProgram.Id.ToString(), "0", "0", "0", "", "0", "", "", vSession, session);
                        aNextPage.Visible = resultsCount > 50 && endPage < resultsCount;
                    }
                }
                else
                    Response.Redirect(ControlLoader.SearchByType(Type), false);
            }
            else
                Response.Redirect(ControlLoader.SearchByType(Type), false);
        }

        private void FixPage()
        {
            if (vSession.User == null)
            {
                aGetStarted.Visible = true;
                LblGetStartedList.Visible = true;
            }
            else
            {
                aGetStarted.Visible = false;
                LblGetStartedList.Visible = false;
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
            LblGetStartedList.Text = "Join this List";
        }

        private void SetLinks()
        {
            aGetStarted.HRef = ControlLoader.SignUp;
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

                        decimal avg = Sql.GetCompanyAverageRating(user.Id, session);

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

                        List<string> userIndustries = Sql.GetIndustiesDescriptionsIJUserIndustries(user.Id, session);

                        if (user.UserApplicationType == (int)UserApplicationType.ThirdParty && userIndustries.Count == 0)
                        {
                            hasCompanyData = ClearbitSql.ExistsClearbitCompany(user.Id, session);
                            if (hasCompanyData)
                                company = ClearbitSql.GetPersonCompanyByUserId(user.Id, session);
                        }

                        if (userIndustries.Count > 0)
                        {
                            lblIndustryValue.Text = userIndustries[0];
                            if (userIndustries.Count > 1)
                            {
                                iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithIndustriesDescriptions(userIndustries);
                                iMore.Visible = true;
                            }
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
                                    lblIndustryValue.Text = (tagnames.Split(',').ToArray()[0] != "") ? tagnames.Split(',').ToArray()[0] : tagnames.Replace(", ", "");
                                    iMore.Attributes["title"] = (tagnames.Split(',').ToArray().Length > 1) ? tagnames.Replace(", ", Environment.NewLine) : tagnames.Replace(", ", "");
                                    iMore.Visible = true;
                                }
                                else
                                    lblIndustryValue.Text = "-";

                                //List<ElioUsersPersonCompanyTags> tags = ClearbitSql.GetPersonCompanyTags(company.Id, session);

                                //if (tags.Count > 0)
                                //{
                                //    List<string> companyTags = new List<string>();

                                //    foreach (ElioUsersPersonCompanyTags tag in tags)
                                //    {
                                //        companyTags.Add(tag.TagName);
                                //    }

                                //    lblIndustryValue.Text = tags[0].TagName;
                                //    if (companyTags.Count > 1)
                                //    {
                                //        iMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithIndustriesDescriptions(companyTags);
                                //        iMore.Visible = true;
                                //    }
                                //}
                                //else
                                //    lblIndustryValue.Text = "-";
                            }
                            else
                                lblIndustryValue.Text = "-";
                        }

                        Label lblViews = (Label)ControlFinder.FindControlRecursive(item, "LblViews");
                        lblViews.Text = "Views";

                        Label lblViewsValue = (Label)ControlFinder.FindControlRecursive(item, "LblViewsValue");
                        lblViewsValue.Text = string.Empty;
                        lblViewsValue.Text = Sql.GetCompanyTotalViews(user.Id, session).ToString();

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
                divInfoMsg.Visible = false;
                divWarningMsg.Visible = false;
                divGoPremium.Visible = false;
                //divSuccessMsg.Visible = false;

                session.OpenConnection();

                //List<ElioUsersId> results = new List<ElioUsersId>();
                DataTable companiesResult = new DataTable();

                if (vSession.SearchQueryString != string.Empty)
                {
                    //DataLoader<ElioUsersId> loader = new DataLoader<ElioUsersId>(session);
                    //results = loader.Load(vSession.SearchQueryString);

                    companiesResult = session.GetDataTable(vSession.SearchQueryString);
                }

                if (companiesResult.Rows.Count > 0)
                {
                    RdgResults.Visible = true;
                    PnlResults.Visible = true;
                    divWarningMsg.Visible = false;
                    divSuccessMsg.Visible = false;

                    //divSuccessMsg.Visible = true;
                    //LblSuccessMsg.Text = "Opportunities: ";
                    //LblSuccessMsgContent.Text = companiesResult.Rows.Count.ToString() + " results out of 25000+ partnership opportunities on our platform!";

                    if ((vSession.User != null) && (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType)))
                    {
                        divInfoMsg.Visible = false;
                        divGoPremium.Visible = false;
                    }
                    else
                    {
                        //divInfoMsg.Visible = true;
                        LblGoPremium.Text = BtnGoPremium.Text = LblInfoMsg.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "62")).Text;
                        LblInfoMsgContent.Text = "Unlock all the search features and discover products and companies more relevant to your needs!";

                        //divGoPremium.Visible = true;
                        //aGoPremium.Visible = true;
                    }

                    //DataTable table = new DataTable();

                    //table.Columns.Add("id");

                    //foreach (ElioUsersId user in results)
                    //{
                    //    table.Rows.Add(user.Id);
                    //}

                    RdgResults.DataSource = companiesResult;
                }
                else
                {
                    GlobalMethods.ClearCriteriaSession(vSession, false);
                    RdgResults.Visible = false;
                    //divWarningMsg.Visible = true;
                    PnlResults.Visible = true;
                    divWarningMsg.Visible = true;
                    LblWarningMsg.Text = "Try again! ";
                    LblWarningMsgContent.Text = "No results were found that match specific vertical.";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        #endregion
    }
}