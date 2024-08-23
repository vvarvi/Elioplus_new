using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

namespace WdS.ElioPlus.pages
{
    public partial class ProfilesSubIndustriesForChannelPartnersPageAll : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        #region Properties

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

                        if (pathSegs.Length >= 4 && !pathSegs[1].Contains("profile/") && pathSegs[1].TrimEnd('/').Length > 2)
                        {
                            string segs = pathSegs[1].TrimEnd('/').TrimEnd('-');
                            string[] segsWords = segs.Split('-');

                            foreach (var word in segsWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

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

                        if (pathSegs.Length >= 5)
                        {
                            string segs = pathSegs[2].TrimEnd('/').TrimEnd('-');

                            if (segs.Length == 2)
                            {
                                segs = pathSegs[3].TrimEnd('/');

                                string[] segsWords = segs.Split('-');

                                foreach (var word in segsWords)
                                {
                                    if (word != "")
                                        name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                }

                                if (name.EndsWith(" "))
                                    name = name.TrimEnd();

                                ViewState["CountryName"] = name;
                            }
                            else
                            {
                                string[] segsWords = segs.Split('-');

                                foreach (var word in segsWords)
                                {
                                    if (word != "")
                                        name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                }

                                if (name.EndsWith(" "))
                                    name = name.TrimEnd();

                                ViewState["CountryName"] = name;
                            }
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

        public string StateName
        {
            get
            {
                if (ViewState["StateName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length >= 6)
                        {
                            if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                            {
                                if (pathSegs[2].TrimEnd('/').TrimEnd('-').Length != 2)
                                {
                                    string state = pathSegs[3].TrimEnd('/').TrimEnd('-');
                                    string[] stateWords = state.Split('-');

                                    foreach (var word in stateWords)
                                    {
                                        if (word != "")
                                            name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                    }

                                    if (name.EndsWith(" "))
                                        name = name.TrimEnd();
                                }
                                else
                                {
                                    string trans = pathSegs[2].TrimEnd('/').TrimEnd('-');

                                    string state = pathSegs[4].TrimEnd('/').TrimEnd('-');
                                    string[] stateWords = state.Split('-');

                                    foreach (var word in stateWords)
                                    {
                                        if (word != "")
                                            name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                    }

                                    if (name.EndsWith(" "))
                                        name = name.TrimEnd();
                                }

                                ViewState["StateName"] = name;
                            }
                            else
                                ViewState["StateName"] = "";
                        }
                        else
                            ViewState["StateName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["StateName"].ToString();
                }
                else
                    return ViewState["StateName"].ToString();
            }
            set
            {
                ViewState["StateName"] = value;
            }
        }

        public string CityName
        {
            get
            {
                if (ViewState["CityName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length >= 5)
                        {
                            string segs = pathSegs[pathSegs.Length - 2].TrimEnd('/');

                            string[] segsWords = segs.Split('-');

                            foreach (var word in segsWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            ViewState["CityName"] = name;
                        }
                        else
                            ViewState["CityName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["CityName"].ToString();
                }
                else
                    return ViewState["CityName"].ToString();
            }
            set
            {
                ViewState["CityName"] = value;
            }
        }

        public string Translation
        {
            get
            {
                if (ViewState["Translation"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;

                        if (pathSegs.Length >= 6)
                        {
                            string segs = pathSegs[2].TrimEnd('/').TrimEnd('-');

                            if (segs.Length == 2)
                            {
                                name = segs.TrimEnd().ToLower();
                            }

                            ViewState["Translation"] = name;
                        }
                        else
                            ViewState["Translation"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["Translation"].ToString();
                }
                else
                    return ViewState["Translation"].ToString();
            }
            set
            {
                ViewState["Translation"] = value;
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

                        if (pathSegs.Length >= 3)
                        {
                            string segs = pathSegs[pathSegs.Length - 2].TrimEnd('/').TrimEnd('-');
                            string[] segsWords = segs.Split('-');

                            foreach (var word in segsWords)
                            {
                                if (word != "")
                                    name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            ViewState["Type"] = name;
                        }
                        else
                            ViewState["Type"] = "";
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

        public bool IsVertical
        {
            get
            {
                return ViewState["IsVertical"] == null ? false : (bool)ViewState["IsVertical"];
            }
            set
            {
                ViewState["IsVertical"] = value;
            }
        }

        public int VerticalID
        {
            get
            {
                return ViewState["VerticalID"] == null ? 0 : (int)ViewState["VerticalID"];
            }
            set
            {
                ViewState["VerticalID"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                bool isValidUrl = true;

                if (!IsPostBack)
                {
                    if (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count > 0)
                        vSession.UsersSearchInfoList.Clear();

                    string validUrl = "";
                    isValidUrl = GlobalDBMethods.IsCorrectPage(ref validUrl, session);
                    if (isValidUrl)
                        FixPage();
                    else
                        Response.Redirect(validUrl, false);
                }

                if (isValidUrl)
                {
                    SetBreadCrumb();
                    SetTransCrumb();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect(ControlLoader.SearchForChannelPartners, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        private bool IsCorrectPage1()
        {
            string[] paths = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            Uri path = HttpContext.Current.Request.Url;
            var pathSegs = path.Segments;
            string redPath = "/";
            string segs = pathSegs[1].TrimEnd('/').TrimEnd('-');

            if (segs.Contains("asia-pasific"))
            {
                Response.Redirect(HttpContext.Current.Request.Url.AbsolutePath.Replace("asia-pasific", "asia-pacific"), false);
                return false;
            }
            else if (pathSegs.Length == 4 && !pathSegs[1].Contains("profile/"))
            {
                string[] regions = { "africa", "asia-pacific", "europe", "middle-east", "north-america", "south-america" };

                if (!regions.Contains(segs))
                {
                    string region = Sql.GetRegionByCountryTbl(segs.Replace("-", " ").Replace("and", "&"), session);
                    if (region == "")
                    {
                        string regionCountryUrl = Sql.GetRegionCountryByCityTbl(segs.Replace("-", " ").Replace("and", "&"), session);
                        if (regionCountryUrl != "")
                        {
                            redPath += regionCountryUrl + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/");

                            Response.Redirect(redPath, false);
                            return false;
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                            return false;
                        }
                    }
                    else
                    {
                        redPath += region.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/");

                        Response.Redirect(redPath, false);
                        return false;
                    }
                }
            }
            else if (pathSegs.Length == 5 && !pathSegs[1].Replace("-/", "/").Contains("profile/"))
            {
                if (segs.Length == 2)
                {
                    string countryCity = pathSegs[2].TrimEnd('/').TrimEnd('-');
                    string region = Sql.GetRegionByCountryTbl(countryCity.Replace("-", " ").Replace("and", "&"), session);
                    if (region == "")
                    {
                        string regionCountryUrl = Sql.GetRegionTransCountryByCityTbl(countryCity.Replace("-", " ").Replace("and", "&"), segs.ToLower(), session);
                        if (regionCountryUrl != "")
                        {
                            redPath += regionCountryUrl + "/" + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/") + pathSegs[4].Replace("-/", "/");

                            Response.Redirect(redPath, false);
                            return false;
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.SearchForChannelPartners, false);
                            return false;
                        }
                    }
                    else
                    {
                        redPath += region.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + pathSegs[1].Replace("-/", "/") + pathSegs[2].Replace("-/", "/") + pathSegs[3].Replace("-/", "/") + pathSegs[4].Replace("-/", "/");

                        Response.Redirect(redPath, false);
                        return false;
                    }
                }
            }

            return true;
        }

        private void LoadCountries()
        {
            DdlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }
        }

        private void FixPageResultsContent()
        {
            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();

            #region no path -> redirect

            if (path.Length == 0)
            {
                Response.Redirect(ControlLoader.Default(), false);
                return;
            }

            #endregion

            if (path.Length >= 4)
            {
                if (string.IsNullOrEmpty(Type))
                {
                    if (vSession.User == null)
                        Response.Redirect(ControlLoader.Default(), false);
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
            }

            string orderBy = "";

            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            if (vSession.UsersSearchInfoList == null || (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count == 0))
            {
                vSession.UsersSearchInfoList = new List<ElioUsersSearchInfo>();
                vSession.UsersSearchInfoList = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", "0", "", "0", "0", "0", RegionName, CountryName, StateName, CityName, "", orderBy, vSession, session);
            }

            //List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", "0", "", "0", "0", "0", RegionName, CountryName, StateName, CityName, "", orderBy, vSession, session);

            if (mustClose)
                session.CloseConnection();

            if (vSession.UsersSearchInfoList.Count == 0)
                Response.Redirect(ControlLoader.SearchByType("channel-partners"), false);

            LoadRepeater(Navigation.None, vSession.UsersSearchInfoList);

            if (Translation == "")
            {
                SetSEOData();
            }
            else
            {
                string mtDescription = "";
                string mtKeywords = "";
                string pageResultsTitle = "";
                string pageContent1 = "";
                string disclaimer = "";
                string tabResultsTitle = "";

                if (CityName != "")
                {
                    GlobalMethods.GetCityAllChannelPartnersTranslationsContent(CityName, Translation.ToLower(), out mtDescription, out mtKeywords, out pageResultsTitle, out pageContent1, out disclaimer, out tabResultsTitle);

                    HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
                    if (metaHeadDescription != null)
                        metaHeadDescription.Attributes["content"] = mtDescription;

                    HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");
                    if (metaHeadKeywords != null)
                        metaHeadKeywords.Attributes["content"] = mtKeywords;

                    LblResultsTitle.Text = pageResultsTitle;
                    LblResultsContent1.Text = pageContent1;

                    HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");

                    if (pgTitle != null)
                        pgTitle.InnerText = tabResultsTitle;

                    switch (Translation)
                    {
                        case "es":
                        case "la":

                            LblRFPsFormText.Text = "Solicitud de Cotización";

                            break;

                        case "pt":

                            LblRFPsFormText.Text = "Solicitação de Cotação";

                            break;

                        case "de":
                        case "at":

                            LblRFPsFormText.Text = "Angebot anfordern";

                            break;

                        case "pl":

                            LblRFPsFormText.Text = "Poproś o wycenę";

                            break;

                        case "it":

                            LblRFPsFormText.Text = "Richiedi Preventivo";

                            break;

                        case "nl":

                            LblRFPsFormText.Text = "Uw offerte aanvragen";

                            break;

                        case "fr":

                            LblRFPsFormText.Text = "Obtenir un devis";

                            break;
                    }
                }
            }
        }

        private void FixPage()
        {
            FixPageResultsContent();
            
            aRFPsForm.HRef = (Translation == "") ? ControlLoader.RequestQuote : "/" + Translation + ControlLoader.RequestQuote;

            #region Custom Ads Acronis

            if (RegionName == "Asia Pacific" || RegionName == "Africa" || RegionName == "Middle East" || RegionName == "")
            {
                divAcronisAds.Visible = divAcronisAds2.Visible = true;

                LblAdv.Text = "Interested in advertising on our platform? Reach out to us here:";
                //RttpAcronisAds.Text = RttpAcronisAds2.Text = "Interested in advertising on our platform? Reach out to us here:<b><a href=\"https://elioplus.com/contact-us\" target=\"_blank\"> Contact Us</a></b>";
            }
            else
            {
                divMainContent.Attributes["class"] = "grid-cols-1 lg:grid-cols-company gap-30px lg:gap-50px";
            }

            #endregion
        }

        private void SetSEOData()
        {
            //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
            LblRFPsFormText.Text = "Request Quote";

            if (CityName != "")
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                if (pgTitle != null)
                {
                    pgTitle.InnerText = String.Format("The best IT companies, resellers, consultants and agencies in {0}", CityName);
                }

                LblResultsTitle.Text = string.Format("Discover the best IT companies and agencies in {0}", CityName);

                LblResultsContent1.Text = string.Format("Find in the list below the best Managed Service Providers (MSPs), IT companies and agencies that are currently on our platform to help you with new product implementation, monitoring, support services and training for your software & hardware applications in {0}", CityName);
                LblResultsContent2.Text = "";

                HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Master, "metaDescription");
                if (metaHeadDescription != null)
                    metaHeadDescription.Attributes["content"] = string.Format("Find the best Managed Service Providers (MSPs), agencies and IT companies in {0} to help you implement new solutions & products, support and manage your IT tools.", CityName);

                HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Master, "metaKeywords");
                if (metaHeadKeywords != null)
                    metaHeadKeywords.Attributes["content"] = string.Format("Managed service companies {0}, MSPs {0}, IT companies {0}, agencies {0}", CityName);
            }
        }

        private void SetBreadCrumb()
        {
            aBCrHome.HRef = ControlLoader.SearchForChannelPartners;
            Label lbl = new Label();
            lbl.Text = "Channel-Partners" + " / ";
            aBCrHome.Controls.Add(lbl);

            if (RegionName != "")
            {
                aBCrRegion.HRef = String.Format("/{0}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"));
                Label lblRegion = new Label();
                lblRegion.Text = RegionName + " / ";
                aBCrRegion.Controls.Add(lblRegion);
            }

            if (CountryName != "")
            {
                aBCrCountry.HRef = String.Format("/{0}/{1}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"));
                Label lblCountry = new Label();
                lblCountry.Text = CountryName + " / ";
                aBCrCountry.Controls.Add(lblCountry);
            }

            if (StateName != "")
            {
                aBrState.Visible = true;
                aBrState.HRef = String.Format("/{0}/{1}/{2}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), StateName.ToLower().Replace(" ", "-").Replace("&", "and"));
                Label lblState = new Label();
                lblState.Text = StateName + " / ";
                aBrState.Controls.Add(lblState);
            }
            else
                aBrState.Visible = false;

            if (CityName != "" && StateName != "")
            {
                aBCrCity.HRef = String.Format("/{0}/{1}/{2}/{3}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), StateName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
                Label lblCity = new Label();
                lblCity.Text = CityName + " / ";
                aBCrCity.Controls.Add(lblCity);
            }
            else if (CityName != "" && StateName == "")
            {
                aBrState.Visible = false;
                aBCrCity.HRef = String.Format("/{0}/{1}/{2}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
                Label lblCity = new Label();
                lblCity.Text = CityName + " / ";
                aBCrCity.Controls.Add(lblCity);
            }

            if (RegionName != "" && CountryName != "" && StateName != "" && CityName != "")
            {
                aBCrCategory.HRef = String.Format("/{0}/{1}/{2}/{3}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), StateName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
            }
            else if (RegionName != "" && CountryName != "" && CityName != "" && StateName == "")
            {
                aBCrCategory.HRef = String.Format("/{0}/{1}/{2}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
            }
            else if (RegionName != "" && CountryName != "" && CityName == "" && StateName == "")
            {
                aBCrCategory.HRef = String.Format("/{0}/{1}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"));
            }
            else if (RegionName != "" && CountryName == "" && CityName == "" && StateName == "")
            {
                aBCrCategory.HRef = String.Format("/{0}/channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"));
            }
        }

        private void SetTransCrumb()
        {
            if (CountryName != "" && Translation == "")
            {
                string trans = "";
                bool isVisible = false;

                if (CountryName == "France")
                {
                    trans = "fr";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/France.svg";
                }
                else if (CountryName == "Canada")
                {
                    trans = "ca";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Canada.svg";
                }
                else if (CountryName == "Spain")
                {
                    trans = "es";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Spain.svg";
                }
                else if (CountryName == "Germany")
                {
                    trans = "de";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Germany.svg";
                }
                else if (CountryName == "Brazil")
                {
                    trans = "pt";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Portugal.svg";
                }
                else if (CountryName == "Portugal")
                {
                    trans = "pt";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Portugal.svg";
                }
                else if (CountryName == "Austria")
                {
                    trans = "at";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Austria.svg";
                }
                else if (CountryName == "Italy")
                {
                    trans = "it";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Italy.svg";
                }
                else if (CountryName == "Argentina")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Bolivia")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Chile")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Colombia")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Costa Rica")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Dominican Republic")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Ecuador")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "El Salvador")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Guatemala")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Honduras")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Mexico")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Panama")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Paraguay")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Peru")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Puerto Rico")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Uruguay")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Venezuela")
                {
                    trans = "la";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Argentina.svg";
                }
                else if (CountryName == "Netherlands")
                {
                    trans = "nl";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Netherlands.svg";
                }
                else if (CountryName == "Poland")
                {
                    trans = "pl";
                    isVisible = true;
                    ImgTransFlag.Src = "/assets_out/images/flags/Poland.svg";
                }

                divTransArea.Visible = isVisible;

                if (RegionName != "" && CountryName != "" && CityName != "" && trans != "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/{3}/all-channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), trans, CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName != "" && CityName != "" && trans == "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/all-channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
                }
            }
            else if (CountryName != "" && Translation != "")
            {
                divTransArea.Visible = true;
                ImgTransFlag.Src = "/assets_out/images/flags/UK.svg";

                if (RegionName != "" && CountryName != "" && CityName != "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/all-channel-partners", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"));
                }
            }
        }

        private void ResetRFPsFields()
        {
            divDemoWarningMsg.Visible = false;
            divDemoSuccessMsg.Visible = false;

            divStepOne.Visible = true;
            divStepTwo.Visible = false;
            BtnBack.Visible = false;
            BtnProceed.Text = "Next";

            TbxFirstName.Text = "";
            TbxCompanyEmail.Text = "";
            TbxLastName.Text = "";
            TbxBusinessName.Text = "";
            TbxCity.Text = "";
            DdlCountries.SelectedIndex = -1;
            TbxProduct.Text = "";
            TbxNumberUnits.Text = "";
            TbxMessage.Text = "";

            HdnLeadId.Value = "0";
        }

        protected void LoadRepeater(Navigation navigation, List<ElioUsersSearchInfo> users)
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            //UcNoResults.Visible = false;

            //List<ElioUsers> users = GlobalDBMethods.GetUsersSearchResultsNew(DdlCategory.SelectedItem.Value, DdlCategory.SelectedItem.Text, DdlIndustry.SelectedItem.Value, DdlVertical.SelectedItem.Value, DdlProgram.SelectedItem.Value, DdlMarket.SelectedItem.Value, DdlApi.SelectedItem.Value, DdlCountry.SelectedItem.Text, DdlCountry.SelectedItem.Value, "", TbxCompanyName.Text, vSession, session);

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

                //LblHeaderCategory.Text = string.Format(" out of {0} Channel Partners & Resellers in {1}:", users.Count.ToString(), CategoryName);
                //LblSuccessMsgContent.Text = "<b>Opportunities:</b> Viewing <b>" + users.Count.ToString() + "</b> results out of <b>25000+</b> partnership opportunities on our platform!";
            }
            else
            {
                RdgResults.DataSource = null;
                RdgResults.DataBind();

                //LblSuccessMsgContent.Text = "<b>Opportunities:</b> Viewing <b>0</b> results out of <b>25000+</b> partnership opportunities on our platform!";

                NowViewing = 0;
                lbtnPrevBottom.Visible = lbtnNextBottom.Visible = false;

                //GlobalMethods.ShowMessageControl(UcNoResults, "Try again! No results were found that match your criteria.", MessageTypes.Info, true, true, false, false, false);
            }

            if (mustClose)
                session.CloseConnection();
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
                            //string companyId = (item.FindControl("HdnCompanyID") as HiddenField).Value;
                            //int UserID = Convert.ToInt32(companyId);

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
                                Label lblRFPsForm = (Label)ControlFinder.FindControlRecursive(item, "LblRFPsForm");

                                aRFPsForm.Visible = true;
                                aRFPsForm.HRef = (Translation == "") ? "/" + row.Id + ControlLoader.RequestQuote : "/" + Translation + "/" + row.Id + ControlLoader.RequestQuote;

                                if (Translation != "")
                                {
                                    switch (Translation)
                                    {
                                        case "es":
                                        case "la":

                                            lblRFPsForm.Text = "Solicitud de Cotización";

                                            break;

                                        case "pt":

                                            lblRFPsForm.Text = "Solicitação de Cotação";

                                            break;

                                        case "de":
                                        case "at":

                                            lblRFPsForm.Text = "Angebot anfordern";

                                            break;

                                        case "pl":

                                            lblRFPsForm.Text = "Poproś o wycenę";

                                            break;

                                        case "it":

                                            lblRFPsForm.Text = "Richiedi Preventivo";

                                            break;

                                        case "nl":

                                            lblRFPsForm.Text = "Uw offerte aanvragen";

                                            break;

                                        case "fr":

                                            lblRFPsForm.Text = "Obtenir un devis";

                                            break;
                                    }
                                }
                                else
                                    lblRFPsForm.Text = "Get a Quote";
                            }

                            bool hasCompanyData = false;
                            ElioUsersPersonCompanies company = null;

                            if (row.UserApplicationType == (int)UserApplicationType.ThirdParty)        // && userIndustries.Count == 0)
                            {
                                hasCompanyData = ClearbitSql.ExistsClearbitCompany(row.Id, session);
                                if (hasCompanyData)
                                    company = ClearbitSql.GetPersonCompanyByUserId(row.Id, session);
                            }

                            bool existCountry = true;

                            string urlLink = (existCountry) ? row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "profile/channel-partners/";

                            urlLink = urlLink.StartsWith("/") ? urlLink : "/" + urlLink;

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

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert11");
                                    Label LblVert11 = (Label)ControlFinder.FindControlRecursive(item, "LblVert11");
                                    aVert11.Visible = true;
                                    aVert11.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert11.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert12");
                                    Label LblVert12 = (Label)ControlFinder.FindControlRecursive(item, "LblVert12");
                                    aVert12.Visible = true;
                                    aVert12.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert12.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert13");
                                    Label LblVert13 = (Label)ControlFinder.FindControlRecursive(item, "LblVert13");
                                    aVert13.Visible = true;
                                    aVert13.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert13.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert14");
                                    Label LblVert14 = (Label)ControlFinder.FindControlRecursive(item, "LblVert14");
                                    aVert14.Visible = true;
                                    aVert14.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert14.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert15");
                                    Label LblVert15 = (Label)ControlFinder.FindControlRecursive(item, "LblVert15");
                                    aVert15.Visible = true;
                                    aVert15.HRef = urlLink + userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblVert15.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (userVarticals.Count > 14)
                                {
                                    HtmlAnchor aVertMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVertMore");
                                    Label lblVertMore = (Label)ControlFinder.FindControlRecursive(item, "LblVertMore");
                                    Label LblVertMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblVertMoreNum");
                                    aVertMore.Visible = userVarticals.Count > 15;
                                    aVertMore.HRef = "";

                                    if (aVertMore.Visible)
                                    {
                                        lblVertMore.Text = "more";
                                        LblVertMoreNum.Text = "+" + (userVarticals.Count - 15).ToString() + " ";

                                        aVertMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithVerticalsRestDescriptions(userVarticals);
                                    }
                                }
                            }
                            else
                            {
                                HtmlGenericControl divCategoriesArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCategoriesArea");
                                divCategoriesArea.Visible = false;
                            }

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

                            string productUrl = (existCountry) ? row.CompanyRegion.Replace(" ", "-").ToLower() + "/" + row.Country.Replace(" ", "-").ToLower() + "/channel-partners/" : "profile/channel-partners/";

                            productUrl = productUrl.StartsWith("/") ? productUrl : "/" + productUrl;

                            List<ElioRegistrationProducts> userProducts = Sql.GetRegistrationProductsDescriptionByUserId(row.Id, session);

                            if (userProducts.Count > 0)
                            {
                                int count = 0;

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

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr8");
                                    Label LblPartPr8 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr8");
                                    aPartPr8.Visible = true;
                                    aPartPr8.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr8.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr9");
                                    Label LblPartPr9 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr9");
                                    aPartPr9.Visible = true;
                                    aPartPr9.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr9.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr10");
                                    Label LblPartPr10 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr10");
                                    aPartPr10.Visible = true;
                                    aPartPr10.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr10.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr11");
                                    Label LblPartPr11 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr11");
                                    aPartPr11.Visible = true;
                                    aPartPr11.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr11.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr12");
                                    Label LblPartPr12 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr12");
                                    aPartPr12.Visible = true;
                                    aPartPr12.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr12.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr13");
                                    Label LblPartPr13 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr13");
                                    aPartPr13.Visible = true;
                                    aPartPr13.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr13.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr14");
                                    Label LblPartPr14 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr14");
                                    aPartPr14.Visible = true;
                                    aPartPr14.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr14.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (count < userProducts.Count)
                                {
                                    HtmlAnchor aPartPr15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr15");
                                    Label LblPartPr15 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr15");
                                    aPartPr15.Visible = true;
                                    aPartPr15.HRef = productUrl + userProducts[count].Description.Replace("&", "and").Replace(" ", "_").Replace("'", "-").ToLower();

                                    LblPartPr15.Text = userProducts[count].Description;
                                    count++;
                                }

                                if (userProducts.Count > 14)
                                {
                                    HtmlAnchor aPartPrMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPrMore");
                                    Label lblPartPrMore = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMore");
                                    Label lblPartPrMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMoreNum");
                                    aPartPrMore.Visible = userProducts.Count > 15;
                                    aPartPrMore.HRef = "";

                                    if (aPartPrMore.Visible)
                                    {
                                        lblPartPrMore.Text = "more";
                                        lblPartPrMoreNum.Text = "+" + (userProducts.Count - 15).ToString() + " ";

                                        HtmlImage iMoreProducts = (HtmlImage)ControlFinder.FindControlRecursive(item, "iMoreProducts");
                                        aPartPrMore.Attributes["title"] = GlobalMethods.FillRadToolTipWithRegistrationProductsRestDescriptions(userProducts);
                                    }
                                }
                            }
                            else
                            {
                                HtmlGenericControl divProductsArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divProductsArea");
                                divProductsArea.Visible = false;
                            }

                            if (hasCompanyData && company != null)
                            {
                                if (string.IsNullOrEmpty(row.CompanyLogo))
                                    if (!string.IsNullOrEmpty(company.Logo))
                                    {
                                        HtmlImage imgCompanyLogo = (HtmlImage)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                                        imgCompanyLogo.Src = company.Logo;
                                    }
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
                                        if (CountryName != "")
                                        {
                                            row.Overview = GlobalDBMethods.GetOverviewForCompanyByCountry(CountryName, row.CompanyName, session);
                                        }
                                        else
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
            try
            {
                //LoadRepeater(Navigation.Previous);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                //LoadRepeater(Navigation.Next);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aRFPsForm_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetRFPsFields();
                LoadCountries();

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenRFPsPopUp();", true);
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

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                if (divStepOne.Visible)
                {
                    if (TbxProduct.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your product/technology";
                        return;
                    }

                    if (TbxNumberUnits.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill number of users/units";
                        return;
                    }
                    else
                    {
                        if (!Validations.IsNumber(TbxNumberUnits.Text))
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill only numbers for users/units";
                            return;
                        }
                    }

                    if (TbxMessage.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill a message";
                        return;
                    }

                    divStepOne.Visible = false;
                    divStepTwo.Visible = true;

                    BtnProceed.Text = "Save";
                    BtnBack.Visible = true;
                }
                else
                {
                    if (TbxFirstName.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your first name";
                        return;
                    }

                    if (TbxLastName.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your last name";
                        return;
                    }

                    if (TbxCompanyEmail.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your company email";
                        return;
                    }
                    else
                    {
                        if (!Validations.IsEmail(TbxCompanyEmail.Text))
                        {
                            divDemoWarningMsg.Visible = true;
                            LblDemoWarningMsg.Text = "Error! ";
                            LblDemoWarningMsgContent.Text = "Please fill a valid company email";
                            return;
                        }
                    }

                    if (TbxBusinessName.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your company name";
                        return;
                    }

                    if (DdlCountries.SelectedValue == "0" || DdlCountries.SelectedValue == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please select your country";
                        return;
                    }

                    if (TbxPhoneNumber.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your phone number";
                        return;
                    }

                    if (TbxCity.Text == "")
                    {
                        divDemoWarningMsg.Visible = true;
                        LblDemoWarningMsg.Text = "Error! ";
                        LblDemoWarningMsgContent.Text = "Please fill your city";
                        return;
                    }

                    ElioSnitcherWebsiteLeads lead = null;

                    DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);

                    if (HdnLeadId.Value == "0")
                    {
                        lead = new ElioSnitcherWebsiteLeads();

                        lead.WebsiteId = "19976";
                        lead.ElioSnitcherWebsiteId = 2;
                        lead.SessionReferrer = "https://www.elioplus.com";
                        lead.SessionOperatingSystem = "";
                        lead.SessionBrowser = "";
                        lead.SessionDeviceType = "";
                        lead.SessionCampaign = "";
                        lead.SessionStart = DateTime.Now;
                        lead.SessionDuration = 0;
                        lead.SessionTotalPageviews = 1;

                        string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                        int count = 1;
                        while (Sql.GetSnitcherLeadIDByLeadId(number, session) != "" && count < 10)
                        {
                            number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8);
                            count++;
                        }

                        if (count >= 10)
                        {
                            throw new Exception("ERROR -> string number = Guid.NewGuid().GetHashCode().ToString().Substring(0, 8) could not created");
                        }

                        lead.LeadId = number;
                        lead.LeadLastSeen = DateTime.Now;
                        lead.LeadFirstName = TbxFirstName.Text;
                        lead.LeadLastName = TbxLastName.Text;
                        lead.LeadCompanyName = TbxBusinessName.Text;
                        lead.LeadCompanyLogo = "";
                        lead.LeadCompanyWebsite = "";
                        lead.LeadCountry = DdlCountries.SelectedItem.Text;
                        lead.LeadCity = TbxCity.Text;
                        lead.LeadCompanyAddress = "";
                        lead.LeadCompanyFounded = "0";
                        lead.LeadCompanySize = TbxNumberUnits.Text;
                        lead.LeadCompanyIndustry = "";
                        lead.LeadCompanyPhone = "";
                        lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                        lead.LeadCompanyContacts = "";
                        lead.LeadLinkedinHandle = "";
                        lead.LeadFacebookHandle = "";
                        lead.LeadYoutubeHandle = "";
                        lead.LeadInstagramHandle = "";
                        lead.LeadTwitterHandle = "";
                        lead.LeadPinterestHandle = "";
                        lead.LeadAngellistHandle = "";
                        lead.Message = TbxMessage.Text;
                        lead.IsApiLead = (int)ApiLeadCategory.isRFQMessage;
                        lead.IsApproved = 0;
                        lead.IsPublic = 1;
                        lead.IsConfirmed = 0;
                        lead.Sysdate = DateTime.Now;
                        lead.LastUpdate = DateTime.Now;

                        loader.Insert(lead);

                        HdnLeadId.Value = lead.Id.ToString();

                        List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                        if (products.Count > 0)
                        {
                            foreach (string product in products)
                            {
                                if (product != "")
                                {
                                    ElioSnitcherLeadsPageviews pageView = new ElioSnitcherLeadsPageviews();

                                    pageView.LeadId = lead.LeadId;
                                    pageView.ElioWebsiteLeadsId = lead.Id;
                                    pageView.Url = product.Trim();
                                    pageView.Product = product.Trim();
                                    pageView.TimeSpent = 1;
                                    pageView.ActionTime = DateTime.Now;
                                    pageView.Sysdate = DateTime.Now;
                                    pageView.LastUpdate = DateTime.Now;

                                    DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);
                                    loaderView.Insert(pageView);
                                }
                            }
                        }
                    }
                    else
                    {
                        lead = Sql.GetSnitcherWebsiteLeadById(Convert.ToInt32(HdnLeadId.Value), session);
                        if (lead != null)
                        {
                            lead.LeadFirstName = TbxFirstName.Text;
                            lead.LeadLastName = TbxLastName.Text;
                            lead.LeadCompanyName = TbxBusinessName.Text;
                            lead.LeadCountry = DdlCountries.SelectedItem.Text;
                            lead.LeadCity = TbxCity.Text;
                            lead.LeadCompanySize = TbxNumberUnits.Text;
                            lead.LeadCompanyEmail = TbxCompanyEmail.Text;
                            lead.Message = TbxMessage.Text;
                            lead.LastUpdate = DateTime.Now;
                            lead.IsConfirmed = 0;

                            loader.Update(lead);

                            List<string> products = TbxProduct.Text.Trim().Split(',').ToList();
                            if (products.Count > 0)
                            {
                                foreach (string product in products)
                                {
                                    if (product != "")
                                    {
                                        DataLoader<ElioSnitcherLeadsPageviews> loaderView = new DataLoader<ElioSnitcherLeadsPageviews>(session);

                                        ElioSnitcherLeadsPageviews pageView = Sql.GetSnitcherLeadPageViewByLeadIdAndUrlOrProduct(lead.LeadId, product, product, session);
                                        if (pageView == null)
                                        {
                                            pageView = new ElioSnitcherLeadsPageviews();

                                            pageView.LeadId = lead.LeadId;
                                            pageView.ElioWebsiteLeadsId = lead.Id;
                                            pageView.Url = product.Trim();
                                            pageView.Product = product.Trim();
                                            pageView.TimeSpent = 1;
                                            pageView.ActionTime = DateTime.Now;
                                            pageView.Sysdate = DateTime.Now;
                                            pageView.LastUpdate = DateTime.Now;

                                            loaderView.Insert(pageView);
                                        }
                                    }
                                }
                            }
                        }
                    }

                    divDemoSuccessMsg.Visible = true;
                    LblDemoSuccessMsg.Text = "Done! ";
                    LblDemoSuccessMsgContent.Text = "Your request for a quote saved successfully.";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                divDemoWarningMsg.Visible = true;
                LblDemoWarningMsg.Text = "Error! ";
                LblDemoWarningMsgContent.Text = "Something went wrong! Please try again later.";
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            try
            {
                divStepOne.Visible = true;
                divStepTwo.Visible = false;

                divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                BtnBack.Visible = false;
                BtnProceed.Text = "Next";
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

        #region DropDownLists

        #endregion
    }
}