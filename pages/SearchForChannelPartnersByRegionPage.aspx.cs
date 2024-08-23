using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using AjaxControlToolkit.HtmlEditor.ToolbarButtons;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;
using System.Linq;

namespace WdS.ElioPlus.pages
{
    public partial class SearchForChannelPartnersByRegionPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public string RegionName
        {
            get
            {
                if (ViewState["RegionName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length == 3)
                        {
                            string region = pathSegs[pathSegs.Length - 2].TrimEnd('/').TrimEnd('-');
                            string[] regionWords = region.Split('-');

                            foreach (var word in regionWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            //if (name.Contains("and"))
                            //    name = name.Replace("and", "&");

                            ViewState["RegionName"] = name;
                        }
                        else
                            ViewState["RegionName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["RegionName"].ToString().TrimEnd();
                }
                else
                    return ViewState["RegionName"].ToString().TrimEnd();
            }
            set
            {
                ViewState["RegionName"] = value;
            }
        }

        public string Type
        {
            get
            {
                if (ViewState["Type"] == null)
                {
                    string typeName = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        string type = pathSegs[pathSegs.Length - 1].TrimEnd('/').TrimEnd('-');
                        string[] typeWords = type.Split('-');

                        foreach (var word in typeWords)
                        {
                            if (word != "")
                                typeName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }

                        ViewState["Type"] = typeName.TrimEnd();
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["Type"].ToString();
                }
                else
                    return ViewState["Type"].ToString().TrimEnd();
            }
            set
            {
                ViewState["Type"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    string validUrl = "";
                    bool isValidUrl = GlobalDBMethods.IsCorrectPage(ref validUrl, session);
                    if (isValidUrl)
                        FixPage();
                    else
                        Response.Redirect(validUrl, false);
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
            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
            if (path.Length == 3)
            {
                if (string.IsNullOrEmpty(Type))
                {
                    if (vSession.User == null)
                        Response.Redirect(ControlLoader.Default(), false);
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }

                if (RegionName == "")
                    Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                else
                {
                    if (RegionName.ToLower() == "africa" || RegionName.ToLower() == "asia pacific" || RegionName.ToLower() == "europe" || RegionName.ToLower() == "middle east" || RegionName.ToLower() == "north america" || RegionName.ToLower() == "south america")
                    {
                        UpdateStrings();
                        GetCountriesByRegion();
                        SetSEOData();
                        SetBreadCrumb();
                        LoadVerticals();
                        LoadTechnologies();
                        FixPopularLinks();
                    }
                    else
                        Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                }
            }
        }

        private void FixPopularLinks()
        {
            if (RegionName != "")
            {
                apopBigData.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/big_data";
                apopAnalSoft.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/analytics_software";
                apopEcommerce.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/ecommerce";
                apopAccounting.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/accounting";
                apopDatabases.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/databases";
                apopCrm.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/crm";
                apopAdServing.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/ad_serving";
            }
        }

        private void SetSEOData()
        {
            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            if (pgTitle != null)
                pgTitle.InnerText = String.Format("Browse the best IT companies in {0} by country and city", RegionName);

            LblResultsTitle.Text = string.Format("Search for Channel Partners in {0}", RegionName);
            LblResultsTitle2.Text = string.Format("Channel Partners in {0} Region", RegionName);
            LblResultsTitle3.Text = string.Format("Vendor Technologies in {0} Region:", RegionName);
            LblResultsTitle4.Text = string.Format("Browse the best software IT resellers and SaaS channel partners in countries within the {0} region.", RegionName);
            LblResultsTitle5.Text = string.Format("Popular categories in {0} Region:", RegionName);
            LblProductCategories.Text = string.Format("All product categories in {0} Region:", RegionName);

            LblRegion.Text = RegionName;

            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            if (metaDescription != null)
                metaDescription.Attributes["content"] = string.Format("Discover the best IT companies, like Managed Service Providers, resellers and software companies in {0}", RegionName);

            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");
            if (metaKeywords != null)
                metaKeywords.Attributes["content"] = string.Format("Discover the best IT companies, like Managed Service Providers, resellers and software companies in {0}", RegionName);
        }

        private void SetBreadCrumb()
        {
            aBCrHome.HRef = ControlLoader.SearchForChannelPartners;
            Label lbl = new Label();
            lbl.Text = "Channel-Partners" + " / ";
            aBCrHome.Controls.Add(lbl);

            aBCrRegion.HRef = String.Format("/{0}/channel-partners", RegionName.ToLower().Replace(" ", "-"));
            Label lblRegion = new Label();
            lblRegion.Text = RegionName + " / ";
            aBCrRegion.Controls.Add(lblRegion);
        }

        private void UpdateStrings()
        {
            //LblSubIndustriesResults.Text = LblCountriesResults.Text = Type != "" ? "Find " + Type + " by " : "Find companies by ";

            //LblSearchCategory.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "59")).Text;
            //LblSearchIndustry.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "60")).Text;
            //LblSearchVertical.Text = "In this vertical";
            //LblSearchProgram.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "61")).Text;
            //LblSearchMarket.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "62")).Text;
            //LblSearchApi.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "63")).Text;
            //LblSearchCountry.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "11")).Text;
            //LblSearchName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "12")).Text;
            //BtnSearch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "58")).Text;
            //Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "label", "9")).Text;
        }

        private void GetCountriesByRegion()
        {
            DataTable table = Sql.GetCountriesByRegionTbl(RegionName, session);
            if (table != null && table.Rows.Count > 0)
            {
                LblByCountry.Text = RegionName;

                RptCountries.Visible = true;
                RptCountries.DataSource = table;
                RptCountries.DataBind();
            }
            else
                RptCountries.Visible = false;
        }

        private void LoadVerticals()
        {
            if (RegionName != "")
            {
                string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                foreach (string letter in letters)
                {
                    DataTable table = Sql.GetVerticalsForSearchResultsByLetter(Type, letter, RegionName, "", "", "", session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        switch (letter)
                        {
                            case "A":
                                RptA.DataSource = table;
                                RptA.DataBind();
                                break;

                            case "B":
                                RptB.DataSource = table;
                                RptB.DataBind();
                                break;

                            case "C":
                                RptC.DataSource = table;
                                RptC.DataBind();
                                break;

                            case "D":
                                RptD.DataSource = table;
                                RptD.DataBind();
                                break;

                            case "E":
                                RptE.DataSource = table;
                                RptE.DataBind();
                                break;

                            case "F":
                                RptF.DataSource = table;
                                RptF.DataBind();
                                break;

                            case "G":
                                RptG.DataSource = table;
                                RptG.DataBind();
                                break;

                            case "H":
                                RptH.DataSource = table;
                                RptH.DataBind();
                                break;

                            case "I":
                                RptI.DataSource = table;
                                RptI.DataBind();
                                break;

                            case "J":
                                RptJ.DataSource = table;
                                RptJ.DataBind();
                                break;

                            case "K":
                                RptK.DataSource = table;
                                RptK.DataBind();
                                break;

                            case "L":
                                RptL.DataSource = table;
                                RptL.DataBind();
                                break;

                            case "M":
                                RptM.DataSource = table;
                                RptM.DataBind();
                                break;

                            case "N":
                                RptN.DataSource = table;
                                RptN.DataBind();
                                break;

                            case "O":
                                RptO.DataSource = table;
                                RptO.DataBind();
                                break;

                            case "P":
                                RptP.DataSource = table;
                                RptP.DataBind();
                                break;

                            case "Q":
                                RptQ.DataSource = table;
                                RptQ.DataBind();
                                break;

                            case "R":
                                RptR.DataSource = table;
                                RptR.DataBind();
                                break;

                            case "S":
                                RptS.DataSource = table;
                                RptS.DataBind();
                                break;

                            case "T":
                                RptT.DataSource = table;
                                RptT.DataBind();
                                break;

                            case "U":
                                RptU.DataSource = table;
                                RptU.DataBind();
                                break;

                            case "V":
                                RptV.DataSource = table;
                                RptV.DataBind();
                                break;

                            case "W":
                                RptW.DataSource = table;
                                RptW.DataBind();
                                break;

                            case "X":
                                RptX.DataSource = table;
                                RptX.DataBind();
                                break;

                            case "Y":
                                RptY.DataSource = table;
                                RptY.DataBind();
                                break;

                            case "Z":
                                RptZ.DataSource = table;
                                RptZ.DataBind();
                                break;
                        }
                    }
                    else
                    {

                    }
                }                
            }
            else
            {
                Response.Redirect(ControlLoader.SearchForChannelPartners, false);
            }            
        }

        private void LoadTechnologies()
        {
            if (RegionName != "")
            {
                string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                foreach (string letter in letters)
                {
                    DataTable table = Sql.GetTechnologiesForSearchResultsByLetter(Type, letter, RegionName, "", "", "", session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        switch (letter)
                        {
                            case "A":
                                RptTechA.DataSource = table;
                                RptTechA.DataBind();
                                break;

                            case "B":
                                RptTechB.DataSource = table;
                                RptTechB.DataBind();
                                break;

                            case "C":
                                RptTechC.DataSource = table;
                                RptTechC.DataBind();
                                break;

                            case "D":
                                RptTechD.DataSource = table;
                                RptTechD.DataBind();
                                break;

                            case "E":
                                RptTechE.DataSource = table;
                                RptTechE.DataBind();
                                break;

                            case "F":
                                RptTechF.DataSource = table;
                                RptTechF.DataBind();
                                break;

                            case "G":
                                RptTechG.DataSource = table;
                                RptTechG.DataBind();
                                break;

                            case "H":
                                RptTechH.DataSource = table;
                                RptTechH.DataBind();
                                break;

                            case "I":
                                RptTechI.DataSource = table;
                                RptTechI.DataBind();
                                break;

                            case "J":
                                RptTechJ.DataSource = table;
                                RptTechJ.DataBind();
                                break;

                            case "K":
                                RptTechK.DataSource = table;
                                RptTechK.DataBind();
                                break;

                            case "L":
                                RptTechL.DataSource = table;
                                RptTechL.DataBind();
                                break;

                            case "M":
                                RptTechM.DataSource = table;
                                RptTechM.DataBind();
                                break;

                            case "N":
                                RptTechN.DataSource = table;
                                RptTechN.DataBind();
                                break;

                            case "O":
                                RptTechO.DataSource = table;
                                RptTechO.DataBind();
                                break;

                            case "P":
                                RptTechP.DataSource = table;
                                RptTechP.DataBind();
                                break;

                            case "Q":
                                RptTechQ.DataSource = table;
                                RptTechQ.DataBind();
                                break;

                            case "R":
                                RptTechR.DataSource = table;
                                RptTechR.DataBind();
                                break;

                            case "S":
                                RptTechS.DataSource = table;
                                RptTechS.DataBind();
                                break;

                            case "T":
                                RptTechT.DataSource = table;
                                RptTechT.DataBind();
                                break;

                            case "U":
                                RptTechU.DataSource = table;
                                RptTechU.DataBind();
                                break;

                            case "V":
                                RptTechV.DataSource = table;
                                RptTechV.DataBind();
                                break;

                            case "W":
                                RptTechW.DataSource = table;
                                RptTechW.DataBind();
                                break;

                            case "X":
                                RptTechX.DataSource = table;
                                RptTechX.DataBind();
                                break;

                            case "Y":
                                RptTechY.DataSource = table;
                                RptTechY.DataBind();
                                break;

                            case "Z":
                                RptTechZ.DataSource = table;
                                RptTechZ.DataBind();
                                break;
                        }
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                Response.Redirect(ControlLoader.SearchForChannelPartners, false);
            }
        }

        #endregion

        #region Grids

        protected void RdgTechnologies1_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aNavigate = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aNavigate");
                    aNavigate.HRef = ControlLoader.SubIndustryProfiles("channel-partners", item["link"].Text);

                    Label lblUrl = (Label)ControlFinder.FindControlRecursive(aNavigate, "LblUrl");
                    lblUrl.Text = item["company_name"].Text;
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

        protected void RdgTechnologies2_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aNavigate = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aNavigate");
                    aNavigate.HRef = ControlLoader.SubIndustryProfiles("channel-partners", item["link"].Text);

                    Label lblUrl = (Label)ControlFinder.FindControlRecursive(aNavigate, "LblUrl");
                    lblUrl.Text = item["company_name"].Text;
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

        protected void RptTechnologies1_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    if (RegionName != "")
                    {
                        DataTable table = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, true, Type, RegionName, "", "", "", session);

                        if (table.Rows.Count > 0)
                        {
                            //RptTechnologies1.DataSource = table;
                            //RptTechnologies1.DataBind();
                        }
                        else
                        {
                            //RptTechnologies1.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.SearchForChannelPartners, false);
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

        protected void RptTechnologies2_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    if (RegionName != "")
                    {
                        DataTable table = Sql.GetTechnologiesWithUsersCountForSearchResultsByTop(false, false, Type, RegionName, "", "", "", session);

                        if (table.Rows.Count > 0)
                        {
                            //RptTechnologies2.DataSource = table;
                            //RptTechnologies2.DataBind();
                        }
                        else
                        {
                            //RptTechnologies2.Visible = false;
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.SearchForChannelPartners, false);
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

        #endregion

        #region Buttons

        #endregion

        #region Combo

        #endregion
    }
}