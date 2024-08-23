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
    public partial class SearchForChannelPartnersByRegionCountryCityPage : System.Web.UI.Page
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

                        if (pathSegs.Length == 5)
                        {
                            string region = pathSegs[pathSegs.Length - 4].TrimEnd('/').TrimEnd('-');
                            string[] regionWords = region.Split('-');

                            foreach (var word in regionWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            //if (name != "" && name.Contains("and"))
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

                    return ViewState["RegionName"].ToString();
                }
                else
                    return ViewState["RegionName"].ToString();
            }
            set
            {
                ViewState["RegionName"] = value;
            }
        }

        public string CountryName
        {
            get
            {
                if (ViewState["CountryName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length == 5)
                        {
                            string country = pathSegs[pathSegs.Length - 3].TrimEnd('/').TrimEnd('-');
                            string[] countryWords = country.Split('-');

                            foreach (var word in countryWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            //if (name != "" && name.Contains("and"))
                            //    name = name.Replace("and", "&");

                            ViewState["CountryName"] = name;
                        }
                        else
                            ViewState["CountryName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["CountryName"].ToString();
                }
                else
                    return ViewState["CountryName"].ToString();
            }
            set
            {
                ViewState["CountryName"] = value;
            }
        }

        public string CityOrStateName
        {
            get
            {
                if (ViewState["CityOrStateName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length == 5)
                        {
                            string country = pathSegs[pathSegs.Length - 2].TrimEnd('/').TrimEnd('-');
                            string[] countryWords = country.Split('-');

                            foreach (var word in countryWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            //if (name != "" && name.Contains("and"))
                            //    name = name.Replace("and", "&");

                            ViewState["CityOrStateName"] = name;
                        }
                        else
                            ViewState["CityOrStateName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["CityOrStateName"].ToString();
                }
                else
                    return ViewState["CityOrStateName"].ToString();
            }
            set
            {
                ViewState["CityOrStateName"] = value;
            }
        }

        public string Type
        {
            get
            {
                if (ViewState["Type"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        string type = pathSegs[pathSegs.Length - 1].TrimEnd('/').TrimEnd('-');
                        string[] typeWords = type.Split('-');

                        foreach (var word in typeWords)
                        {
                            if (word != "")
                                name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }

                        if (name.EndsWith(" "))
                            name = name.TrimEnd();

                        ViewState["Type"] = name;
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["Type"].ToString();
                }
                else
                    return ViewState["Type"].ToString();
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

        private void FixPopularLinks()
        {
            if (RegionName != "" && CountryName != "" && CityOrStateName != "")
            {
                apopBigData.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/big_data";
                apopAnalSoft.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/analytics_software";
                apopEcommerce.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/ecommerce";
                apopAccounting.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/accounting";
                apopDatabases.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/databases";
                apopCrm.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/crm";
                apopAdServing.HRef = "/" + RegionName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CountryName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + CityOrStateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/channel-partners/ad_serving";
            }
        }

        private void FixPage()
        {
            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
            if (path.Length == 5)
            {
                if (string.IsNullOrEmpty(Type))
                {
                    if (vSession.User == null)
                        Response.Redirect(ControlLoader.Default(), false);
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }

                if (RegionName == "" || CountryName == "" || CityOrStateName == "")
                    Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                else
                {
                    if (RegionName.ToLower() == "africa" || RegionName.ToLower() == "asia pacific" || RegionName.ToLower() == "europe" || RegionName.ToLower() == "middle east" || RegionName.ToLower() == "north america" || RegionName.ToLower() == "south america")
                    {
                        bool existCountry = Sql.ExistCountryInCountries(CountryName, session);
                        if (existCountry)
                        {
                            UpdateStrings();
                            GetCountriesByRegion();

                            if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                            {
                                GetCitiesByCountryState();
                                GetStatesByCountry();
                                divStatesArea.Visible = true;
                            }
                            else
                            {
                                GetCitiesByRegionCountry();
                            }

                            SetSEOData();
                            SetBreadCrumb();
                            LoadVerticals();
                            LoadTechnologies();
                            FixPopularLinks();
                        }
                        else
                            Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                    }
                    else
                        Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                }
            }
        }

        private void SetSEOData()
        {
            HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
            {
                #region Is State Case

                if (pgTitle != null)
                    pgTitle.InnerText = String.Format("Browse the best IT companies and Managed Service Providers in {0}", CityOrStateName);

                LblResultsTitle.Text = string.Format("Find the best IT companies and MSPs in {0}", CityOrStateName);

                if (metaDescription != null)
                    metaDescription.Attributes["content"] = string.Format("Discover the best IT companies, like Managed Service Providers, resellers, agencies, web studios and software companies in {0}.", CityOrStateName);

                if (metaKeywords != null)
                    metaKeywords.Attributes["content"] = string.Format("IT companies {0}, software companies {0}, managed service providers {0}", CityOrStateName);

                #endregion
            }
            else
            {
                #region Is City Case

                int numberOfCompanies = 0;
                try
                {
                    numberOfCompanies = Sql.GetNumberOfCompaniesInCityArea(Type, RegionName, CountryName, "", CityOrStateName, session);
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }

                if (pgTitle != null)
                    pgTitle.InnerText = String.Format("The best {0} IT companies and MSPs in {1}", numberOfCompanies, CityOrStateName);

                LblResultsTitle.Text = string.Format("Browse {0} IT companies in {1}", numberOfCompanies, CityOrStateName);

                //LblSubIndustriesResults.Text = string.Format("Discover the best IT companies in {0}, including software & cloud resellers, MSPs, Value Added resellers, consultants, web studios and agencies. Browse businesses by solution category or the products they sell to help you with licensing, implementation, support and training services.", CityOrStateName);

                if (metaDescription != null)
                    metaDescription.Attributes["content"] = string.Format("Browse {0} IT companies in {1} to help you with your licensing, implentation, support and training needs.", numberOfCompanies, CityOrStateName);

                if (metaKeywords != null)
                    metaKeywords.Attributes["content"] = string.Format("IT companies {0}, software companies {0}, managed service providers {0}", CityOrStateName);

                #endregion
            }
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

            aBCrCountry.HRef = String.Format("/{0}/{1}/channel-partners", RegionName.ToLower().Replace(" ", "-"), CountryName.ToLower().Replace(" ", "-"));
            Label lblCountry = new Label();
            lblCountry.Text = CountryName + " / ";
            aBCrCountry.Controls.Add(lblCountry);

            aBCrCity.HRef = String.Format("/{0}/{1}/{2}/channel-partners", RegionName.ToLower().Replace(" ", "-"), CountryName.ToLower().Replace(" ", "-"), CityOrStateName.ToLower().Replace(" ", "-"));
            Label lblCity = new Label();
            lblCity.Text = CityOrStateName + " / ";
            aBCrCity.Controls.Add(lblCity);
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

        private void GetCitiesByRegionCountry()
        {
            DataTable table = Sql.GetCitiesByRegionCountryTbl(RegionName, CountryName, session);
            if (table != null && table.Rows.Count > 0)
            {
                LblCities.Text = "country " + CountryName;

                RptCities.Visible = true;
                RptCities.DataSource = table;
                RptCities.DataBind();
            }
            else
                RptCities.Visible = false;
        }

        private void GetCitiesByCountryState()
        {
            DataTable table = Sql.GetCitiesByRegionCountryStateTbl(CountryName, CityOrStateName, session);
            if (table != null && table.Rows.Count > 0)
            {
                LblCities.Text = "state " + CityOrStateName;

                RptCities.Visible = true;
                RptCities.DataSource = table;
                RptCities.DataBind();
            }
            else
                RptCities.Visible = false;
        }

        private void GetStatesByCountry()
        {
            DataTable table = Sql.GetStatesByCountryTbl(CountryName, session);
            if (table != null && table.Rows.Count > 0)
            {
                LblStates.Text = CountryName;

                RptStates.Visible = true;
                RptStates.DataSource = table;
                RptStates.DataBind();
            }
            else
                RptStates.Visible = false;
        }

        private void LoadVerticals()
        {
            if (RegionName != "" && CountryName != "")
            {
                string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                foreach (string letter in letters)
                {
                    DataTable table = null;

                    if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                        table = Sql.GetVerticalsForSearchResultsByLetter(Type, letter, RegionName, CountryName, CityOrStateName, "", session);
                    else
                        table = Sql.GetVerticalsForSearchResultsByLetter(Type, letter, RegionName, CountryName, "", CityOrStateName, session);

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
            if (RegionName != "" && CountryName != "")
            {
                string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

                foreach (string letter in letters)
                {
                    DataTable table = null;

                    if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                        table = Sql.GetTechnologiesForSearchResultsByLetter(Type, letter, RegionName, CountryName, CityOrStateName, "", session);
                    else
                        table = Sql.GetTechnologiesForSearchResultsByLetter(Type, letter, RegionName, CountryName, "", CityOrStateName, session);

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

        
        #endregion

        #region Buttons

        #endregion

        #region Combo

        #endregion
    }
}