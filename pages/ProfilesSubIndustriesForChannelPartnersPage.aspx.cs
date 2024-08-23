using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Web.UI;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.Linq;
using ServiceStack;

namespace WdS.ElioPlus.pages
{
    public partial class ProfilesSubIndustriesForChannelPartnersPage : System.Web.UI.Page
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

                        if (!pathSegs[1].Contains("profile"))
                        {
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

                        if (pathSegs.Length >= 6)
                        {
                            if (pathSegs.Length == 6)
                            {
                                if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                                {
                                    name = "";
                                    ViewState["CityName"] = name;

                                    return ViewState["CityName"].ToString();
                                }
                                else
                                {
                                    string trans = pathSegs[2].TrimEnd('/').TrimEnd('-');
                                    if (trans.Length == 2)
                                    {
                                        name = "";
                                        ViewState["CityName"] = name;

                                        return ViewState["CityName"].ToString();
                                    }
                                    else
                                    {
                                        string segs = pathSegs[3].TrimEnd('/');

                                        string[] segsWords = segs.Split('-');

                                        foreach (var word in segsWords)
                                        {
                                            if (word != "")
                                                name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                        }
                                    }
                                }
                            }
                            else if (pathSegs.Length == 7 && pathSegs[2].TrimEnd('/').Length == 2)
                            {
                                string trans = pathSegs[2].TrimEnd('/');

                                string segs = pathSegs[4].TrimEnd('/');

                                string[] segsWords = segs.Split('-');

                                foreach (var word in segsWords)
                                {
                                    if (word != "")
                                        name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                }
                            }
                            else
                            {
                                if (pathSegs.Length == 7)
                                {
                                    if (CountryName == "United States" || CountryName == "United Kingdom" || CountryName == "Australia" || CountryName == "India")
                                    {
                                        string segs = pathSegs[4].TrimEnd('/');

                                        string[] segsWords = segs.Split('-');

                                        foreach (var word in segsWords)
                                        {
                                            if (word != "")
                                                name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                        }
                                    }
                                }

                                if (name.EndsWith(" "))
                                    name = name.TrimEnd();

                                ViewState["CityName"] = name;
                            }
                        }
                        else
                            ViewState["CityName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    if (name.EndsWith(" "))
                        name = name.TrimEnd();

                    ViewState["CityName"] = name;

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

                        if (pathSegs.Length == 5 && pathSegs[2].TrimEnd('/').Length == 2)
                        {
                            string segs = pathSegs[2].TrimEnd('/').TrimEnd('-');

                            if (segs.Length == 2)
                            {
                                name = segs.TrimEnd().ToLower();
                            }

                            ViewState["Translation"] = name;
                        }
                        else if (pathSegs.Length >= 6 && pathSegs.Length <= 8)
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

        public string CategoryName
        {
            get
            {
                if (ViewState["CategoryName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        if (pathSegs.Length >= 4)
                        {
                            string segs = pathSegs[pathSegs.Length - 1].TrimEnd('/').TrimEnd('-').TrimEnd('_').Trim();

                            if (segs.EndsWith("_"))
                                segs = segs.TrimEnd('_');

                            if (segs.Contains("'"))
                                segs = segs.Replace("'", "");

                            string[] segsWords = segs.Split('_');

                            if (segsWords.Length == 1)
                            {
                                foreach (var word in segsWords)
                                {
                                    if (word != "")
                                        name += char.ToUpper(word[0]) + word.Substring(1);
                                }
                            }
                            else if (segsWords.Length > 1)
                            {
                                foreach (var word in segsWords)
                                {
                                    if (word != "")
                                        name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                                }
                            }

                            if (name.EndsWith(" "))
                                name = name.TrimEnd();

                            //string[] segsWords2 = name.Split('-');

                            //if (segsWords2.Length > 0)
                            //{
                            //    name = "";

                            //    if (segsWords2.Length == 1)
                            //    {
                            //        foreach (var word in segsWords2)
                            //        {
                            //            name += char.ToUpper(word[0]) + word.Substring(1);
                            //        }
                            //    }
                            //    else if (segsWords2.Length > 1)
                            //    {
                            //        foreach (var word in segsWords2)
                            //        {
                            //            name += char.ToUpper(word[0]) + word.Substring(1) + " ";
                            //        }
                            //    }

                            //    if (name.EndsWith(" "))
                            //        name = name.TrimEnd();
                            //}

                            if (name != "")
                            {
                                if (name.Contains("and"))
                                    name = name.Replace("and", "&");
                                else if (name.Contains("And"))
                                    name = name.Replace("And", "&");
                            }

                            ViewState["CategoryName"] = name;
                        }
                        else
                            ViewState["CategoryName"] = "";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["CategoryName"].ToString();
                }
                else
                    return ViewState["CategoryName"].ToString();
            }
            set
            {
                ViewState["CategoryName"] = value;
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

        public bool IsProduct
        {
            get
            {
                return ViewState["IsProduct"] == null ? false : (bool)ViewState["IsProduct"];
            }
            set
            {
                ViewState["IsProduct"] = value;
            }
        }

        public bool IsProgram
        {
            get
            {
                return ViewState["IsProgram"] == null ? false : (bool)ViewState["IsProgram"];
            }
            set
            {
                ViewState["IsProgram"] = value;
            }
        }

        public int ProgramID
        {
            get
            {
                if (ViewState["ProgramID"] != null)
                    return Convert.ToInt32(ViewState["ProgramID"].ToString());
                else
                    return 0;
            }
            set
            {
                ViewState["ProgramID"] = value;
            }
        }

        public bool AddScript
        {
            get
            {
                return ViewState["AddScript"] == null ? false : (bool)ViewState["AddScript"];
            }
            set
            {
                ViewState["AddScript"] = value;
            }
        }

        public int LeadID
        {
            get
            {
                return ViewState["LeadID"] == null ? 0 : (int)ViewState["LeadID"];
            }
            set
            {
                ViewState["LeadID"] = value;
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

        public int RFPCompanyID
        {
            get
            {
                return ViewState["RFPCompanyID"] == null ? 0 : (int)ViewState["RFPCompanyID"];
            }
            set
            {
                ViewState["RFPCompanyID"] = value;
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

        #endregion

        protected void Page_PreInit(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (SetSnitcherScript())
                    GetStatistics();
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(aSearchBy);

                bool isValidUrl = true;
                
                if (!IsPostBack)
                {
                    if (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count > 0)
                        vSession.UsersSearchInfoList.Clear();

                    LeadID = 0;
                    ProgramID = 0;
                    VerticalID = 0;
                    RFPCompanyID = 0;
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

        private void GetStatistics()
        {
            try
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                string ip = HttpContext.Current.Request.ServerVariables["remote_addr"];

                Logger.StatisticsData("GetStatistics() for pages requests: ", Request.Url.ToString(), path, ip);

                //Lib.Services.IpRegistryServiceAPI.IpRegistryServiceAPI.GetRegistryInfo(ip, Request.Url.ToString(), path, session);
                Lib.Services.IpInfoAPI.IpInfoServiceAPI.GetInfo(ip, Request.Url.ToString(), path, session);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }


        private bool SetSnitcherScript()
        {
            AddScript = GlobalDBMethods.SetSnitcherScriptNew2(CountryName, CategoryName, session);

            return AddScript;
        }

        private void LoadCountries()
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            DdlCountries.Items.Clear();

            System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new System.Web.UI.WebControls.ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }

            if (mustClose)
                session.CloseConnection();
        }

        private void LoadCategoryCities(string country)
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            DrpCategoryCities.Items.Clear();
            List<ElioCities> cities = null;

            if (StateName != "")
            {
                if (country != CountryName)
                {
                    cities = Sql.GetCitiesByCategoryName(CategoryName, country, "", IsVertical, session);
                }
                else
                    cities = Sql.GetCitiesByCategoryName(CategoryName, country, StateName, IsVertical, session);
            }
            else
                cities = Sql.GetCitiesByCategoryName(CategoryName, country, "", IsVertical, session);

            if (cities.Count > 0)
            {
                List<ElioCities> citiesLst = new List<ElioCities>();
                ElioCities defaultValue = new ElioCities();
                defaultValue.Id = 0;
                defaultValue.City = "Select City";
                citiesLst.Add(defaultValue);
                citiesLst.AddRange(cities);

                DrpCategoryCities.DataValueField = "Id";
                DrpCategoryCities.DataTextField = "City";
                DrpCategoryCities.DataSource = citiesLst;
                DrpCategoryCities.DataBind();
                DrpCategoryCities.Enabled = true;
            }
            else
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem();
                item.Value = "0";
                item.Text = "No cities";

                DrpCategoryCities.Items.Add(item);
                DrpCategoryCities.Items.FindByValue("0").Selected = true;

                DrpCategoryCities.Enabled = false;
            }

            if (mustClose)
                session.CloseConnection();
        }

        private void LoadCategoryCountries()
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            DrpCategoryCountries.Items.Clear();

            List<ElioCountries> countries = Sql.GetCountriesByCategoryNameNew(IsVertical, CategoryName, session);
            if (countries.Count > 0)
            {
                DrpCategoryCountries.Enabled = true;
                aSearchBy.Visible = true;

                List<ElioCountries> countriesLst = new List<ElioCountries>();
                ElioCountries defaultValue = new ElioCountries();
                defaultValue.Id = 0;
                defaultValue.CountryName = "Select Country";
                countriesLst.Add(defaultValue);
                countriesLst.AddRange(countries);

                DrpCategoryCountries.DataValueField = "Id";
                DrpCategoryCountries.DataTextField = "CountryName";

                DrpCategoryCountries.DataSource = countriesLst;
                DrpCategoryCountries.DataBind();
            }
            else
            {
                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = "No countries";

                DrpCategoryCountries.Items.Add(item);
                DrpCategoryCountries.Items.FindByValue("0").Selected = true;

                DrpCategoryCountries.Enabled = false;
                aSearchBy.Visible = false;
            }

            if (mustClose)
                session.CloseConnection();
        }

        protected void LoadRepeater(Navigation navigation, List<ElioUsersSearchInfo> users)
        {
            bool mustClose = false;

            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            divWarningMsg.Visible = false;

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

                LblSuccessMsgContent.Text = "<b>Opportunities:</b> Viewing <b>0</b> results out of <b>25000+</b> partnership opportunities on our platform! Learn more <a href=\"/channel-partner-recruitment\" class=\"text-xs lg:text-xs py-5px px-10px bg-gray rounded-20px text-blue\">here</a>";

                NowViewing = 0;
                lbtnPrevBottom.Visible = lbtnNextBottom.Visible = false;

                divWarningMsg.Visible = true;
                LblWarningMsg.Text = "Try again! ";
                LblWarningMsgContent.Text = "No results were found that match your criteria.";
                //GlobalMethods.ShowMessageControl(UcNoResults, "Try again! No results were found that match your criteria.", MessageTypes.Info, true, true, false, false, false);
            }

            if (mustClose)
                session.CloseConnection();
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
                if (string.IsNullOrEmpty(Type) || string.IsNullOrEmpty(CategoryName))
                {
                    if (vSession.User == null)
                        Response.Redirect(ControlLoader.Default(), false);
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                }
            }

            #region Exceptions

            if (CategoryName == "general purpose cad")
                CategoryName = "general-purpose cad";
            else if (CategoryName == "e signature")
                CategoryName = "e-signature";

            #endregion

            ElioSubIndustriesGroupItems vertical = null;

            if (CategoryName == "Managed-service-providers")
            {
                #region MSPs

                IsProduct = false;
                IsVertical = false;
                IsProgram = true;

                VerticalID = 0;
                ProgramID = 7;

                #endregion
            }
            else
            {
                #region get vertical / technology

                IsProgram = false;
                ProgramID = 0;
                                
                vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                IsVertical = vertical != null;
                IsProduct = vertical == null;

                if (vertical != null)
                    VerticalID = vertical.Id;

                #endregion
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
                vSession.UsersSearchInfoList = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (IsVertical) ? VerticalID.ToString() : "0", (IsProduct) ? CategoryName : "", (IsProgram) ? ProgramID.ToString() : "0", "0", "0", RegionName, CountryName, StateName, CityName, "", orderBy, vSession, session);
            }

            //List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (IsVertical) ? VerticalID.ToString() : "0", (IsProduct) ? CategoryName : "", (IsProgram) ? ProgramID.ToString() : "0", "0", "0", RegionName, CountryName, StateName, CityName, "", orderBy, vSession, session);

            if (mustClose)
                session.CloseConnection();

            LoadRepeater(Navigation.None, vSession.UsersSearchInfoList);

            HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            if (Translation == "")
            {
                if (RegionName == "" && CountryName == "" && CityName == "")
                {
                    if (path[1].Contains("profile") && path.Length == 4)
                    {
                        divAllTransArea.Visible = true;

                        aTranslateES.HRef = "/" + path[1] + "/es/" + path[2] + "/" + path[3];
                        aTranslateEN.HRef = "/" + path[1] + "/" + path[2] + "/" + path[3];
                        aTranslatePT.HRef = "/" + path[1] + "/pt/" + path[2] + "/" + path[3];
                        aTranslateDE.HRef = "/" + path[1] + "/de/" + path[2] + "/" + path[3];
                        aTranslatePL.HRef = "/" + path[1] + "/pl/" + path[2] + "/" + path[3];
                        aTranslateIT.HRef = "/" + path[1] + "/it/" + path[2] + "/" + path[3];
                        aTranslateNL.HRef = "/" + path[1] + "/nl/" + path[2] + "/" + path[3];
                        aTranslateFR.HRef = "/" + path[1] + "/fr/" + path[2] + "/" + path[3];
                    }

                    #region initialize element values

                    string pageTitle = "";
                    string content1 = "";
                    string content2 = "";
                    string content3 = "";
                    string metaDescription = "";
                    string metaKeywords = "";
                    string lnk1 = "";
                    string lnk2 = "";
                    string lnk3 = "";
                    string lnk4 = "";
                    string lnk5 = "";
                    string lnk6 = "";
                    string lnk7 = "";
                    string lnk8 = "";
                    string lnk9 = "";
                    string lnk10 = "";

                    #endregion

                    #region Page Content

                    pageTitle = GlobalMethods.SetProfilePageContent(CategoryName, Type.ToLower(), out content1, out content2, out content3,
                        out lnk1, out lnk2, out lnk3, out lnk4,
                        out lnk5, out lnk6, out lnk7, out lnk8,
                        out lnk9, out lnk10,
                        out metaDescription, out metaKeywords);

                    LblResultsTitle.Text = pageTitle;
                    LblResultsContent1.Text = content1;
                    LblResultsContent2.Text = content2;
                    LblResultsContent3.Text = content3;

                    bool hasRelatedCategories = lnk1 != "" || lnk2 != "" || lnk3 != "" || lnk4 != "" || lnk5 != "" || lnk6 != "" || lnk7 != "" || lnk8 != "";

                    if (hasRelatedCategories)
                    {
                        //LblFooterRelatedCompanies.Visible = true;
                        //LblFooterRelatedCompanies.Text = "Related Categories";
                        //LblFooterCompany1.Text = lnk1;
                        //LblFooterCompany2.Text = lnk2;
                        //LblFooterCompany3.Text = lnk3;
                        //LblFooterCompany4.Text = lnk4;
                        //LblFooterCompany5.Text = lnk5;
                        //LblFooterCompany6.Text = lnk6;
                        //LblFooterCompany7.Text = lnk7;
                        //LblFooterCompany8.Text = lnk8;

                        //aFooterCompany1.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk1.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany2.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk2.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany3.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk3.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany4.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk4.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany5.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk5.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany6.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk6.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany7.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk7.Replace("&", "and").Replace(" ", "-").ToLower());
                        //aFooterCompany8.HRef = ControlLoader.SubIndustryProfiles("channel-partners", lnk8.Replace("&", "and").Replace(" ", "-").ToLower());
                    }
                    else
                    {
                        //LblFooterRelatedCompanies.Visible = false;
                        //LblFooterCompany1.Text = "";
                        //LblFooterCompany2.Text = "";
                        //LblFooterCompany3.Text = "";
                        //LblFooterCompany4.Text = "";
                        //LblFooterCompany5.Text = "";
                        //LblFooterCompany6.Text = "";
                        //LblFooterCompany7.Text = "";
                        //LblFooterCompany8.Text = "";
                    }

                    metaHeadDescription.Attributes["content"] = metaDescription;
                    metaHeadKeywords.Attributes["content"] = metaKeywords;

                    #endregion

                    SetSEOData(false, IsProduct);
                }
                else
                    SetSEOData(IsVertical, IsProduct);
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
                    if (IsVertical)
                    {
                        GlobalMethods.GetCityVerticalTranslationsContent(CityName, Translation.ToLower(), vertical.Description, out mtDescription, out mtKeywords, out pageResultsTitle, out pageContent1, out disclaimer, out tabResultsTitle);
                    }
                    else
                    {
                        GlobalMethods.GetCityProductTranslationsContent(CityName, Translation.ToLower(), CategoryName, out mtDescription, out mtKeywords, out pageResultsTitle, out pageContent1, out disclaimer, out tabResultsTitle);
                    }

                    metaHeadDescription.Attributes["content"] = mtDescription;
                    metaHeadKeywords.Attributes["content"] = mtKeywords;
                    LblResultsTitle.Text = pageResultsTitle;
                    LblResultsContent1.Text = pageContent1;

                    HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");

                    if (pgTitle != null)
                        pgTitle.InnerText = tabResultsTitle;
                }
                else if (CountryName != "")
                {
                    if (IsVertical)
                    {
                        GlobalMethods.GetCountryVerticalTranslationsContent(CountryName, Translation.ToLower(), vertical.Description, out mtDescription, out mtKeywords, out pageResultsTitle, out pageContent1, out disclaimer, out tabResultsTitle);
                    }
                    else
                    {
                        GlobalMethods.GetCountryProductTranslationsContent(CountryName, Translation.ToLower(), CategoryName, out mtDescription, out mtKeywords, out pageResultsTitle, out pageContent1, out disclaimer, out tabResultsTitle);
                    }

                    metaHeadDescription.Attributes["content"] = mtDescription;
                    metaHeadKeywords.Attributes["content"] = mtKeywords;
                    LblResultsTitle.Text = pageResultsTitle;
                    LblResultsContent1.Text = pageContent1;

                    HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");

                    if (pgTitle != null)
                        pgTitle.InnerText = tabResultsTitle;
                }
                else
                {
                    if (RegionName == "" && CountryName == "" && CityName == "")
                    {
                        if (path[1].Contains("profile") && path[2].Length == 2 && path.Length == 5)
                        {
                            divAllTransArea.Visible = true;

                            aTranslateES.HRef = "/" + path[1] + "/es/" + path[3] + "/" + path[4];
                            aTranslateEN.HRef = "/" + path[1] + "/" + path[3] + "/" + path[4];
                            aTranslatePT.HRef = "/" + path[1] + "/pt/" + path[3] + "/" + path[4];
                            aTranslateDE.HRef = "/" + path[1] + "/de/" + path[3] + "/" + path[4];
                            aTranslatePL.HRef = "/" + path[1] + "/pl/" + path[3] + "/" + path[4];
                            aTranslateIT.HRef = "/" + path[1] + "/it/" + path[3] + "/" + path[4];
                            aTranslateNL.HRef = "/" + path[1] + "/nl/" + path[3] + "/" + path[4];
                            aTranslateFR.HRef = "/" + path[1] + "/fr/" + path[3] + "/" + path[4];

                            SetSEOData(false, IsProduct);
                        }
                    }
                }

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

        private void FixPage()
        {
            FixPageResultsContent();
            LoadCategoryCountries();
            LoadCategoryCities(CountryName);

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

        private void SetSEOData(bool isVertical, bool isProduct)
        {
            //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
            LblRFPsFormText.Text = "Request Quote";

            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

            if (CityName != "")
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                if (pgTitle != null)
                {
                    if (isVertical)
                        pgTitle.InnerText = String.Format("Best companies offering {0} solutions in {1}", CategoryName, CityName);
                    else if (isProduct)
                        pgTitle.InnerText = String.Format("Best {0} partners and resellers in {1}", CategoryName, CityName);
                    else
                        pgTitle.InnerText = String.Format("Best Managed IT Service Providers near you in {1}", CategoryName, CityName);
                }

                if (isVertical)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} solutions companies in {1}", CategoryName, CityName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} solutions providers that are currently on our platform to help you with implementation, training or consulting services in {1}", CategoryName, CityName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("The best IT providers, MSPs, resellers, affiliates and consultants that offer {0} software solutions and products in {1}.", CategoryName, CityName);
                    metaKeywords.Attributes["content"] = string.Format("{0} software companies {1}, {0} software partners {1}, {0} software resellers {1}", CategoryName, CityName);
                }
                else if (isProduct)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} partners and resellers in {1}", CategoryName, CityName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} resellers or channel partners that are currently on our platform to help you with implementation, training or consulting services in {1}", CategoryName, CityName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("The best channel partners, resellers, affiliates and consultants that offer {0} solutions and products in {1}.", CategoryName, CityName);
                    metaKeywords.Attributes["content"] = string.Format("{0} resellers {1}, {0} partners {1}, {0} channel partners {1}", CategoryName, CityName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
                else    //Program
                {
                    LblResultsTitle.Text = string.Format("Find the best Managed Service Providers in {1}", CategoryName, CityName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best Managed IT Service Providers near you, in the city of {1}, to help you with implementation, support and maintenance of your IT infrastructure and software applications.", CategoryName, CityName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Discover all the IT companies near you, in {1}, that offer Managed IT Services to help you implement, support and maintain your IT infrastructure and applications.", CategoryName, CityName);
                    metaKeywords.Attributes["content"] = string.Format("managed service providers {1}, managed service providers near {1}, best managed service providers {1}", CategoryName, CityName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
            }
            else if (StateName != "")
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                if (pgTitle != null)
                {
                    if (isVertical)
                        pgTitle.InnerText = String.Format("Best {0} solutions MSPs, IT companies and agencies in {1}", CategoryName, StateName);
                    else if (isProduct)
                        pgTitle.InnerText = String.Format("Best {0} partners, resellers and MSPs in {1}", CategoryName, StateName);
                    else
                        pgTitle.InnerText = String.Format("Best Managed IT Service Providers near you in {1}", CategoryName, StateName);
                }

                if (isVertical)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} solutions companies in {1}", CategoryName, StateName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} solutions Managed Service Providers (MSPs), IT companies, consultants, agencies to help you with implementation, licensing, training and support services in {1}", CategoryName, StateName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Browse the best Managed Service Providers (MSPs) and IT companies that offer {0} software implementation services and licensing solutions in {1}.", CategoryName, StateName);
                    metaKeywords.Attributes["content"] = string.Format("{0} companies {1}, {0} managed service providers {1}, {0} resellers {1}", CategoryName, StateName);
                }
                else if (isProduct)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} partners in {1}", CategoryName, StateName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} partners like resellers, VARs, Managed Service Providers, consultants and affiliates to help you with implementation, licensing, training and support services in {1}", CategoryName, StateName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Browse the best {0} partners, resellers, Managed Service Providers and consultants to help you with implementation services and licensing solutions in {1}.", CategoryName, StateName);
                    metaKeywords.Attributes["content"] = string.Format("{0} partners {1}, {0} resellers {1}, {0} consultants {1}", CategoryName, StateName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = "";    // String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
                else
                {
                    LblResultsTitle.Text = string.Format("Find the best Managed Service Providers in {1}", CategoryName, StateName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best Managed IT Service Providers near you, in the city of {1}, to help you with implementation, support and maintenance of your IT infrastructure and software applications.", CategoryName, StateName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Discover all the IT companies near you, in {1}, that offer Managed IT Services to help you implement, support and maintain your IT infrastructure and applications.", CategoryName, StateName);
                    metaKeywords.Attributes["content"] = string.Format("managed service providers {1}, managed service providers near {1}, best managed service providers {1}", CategoryName, StateName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = "";    // String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
            }
            else if (CountryName != "")
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                if (pgTitle != null)
                {
                    if (isVertical)
                        pgTitle.InnerText = String.Format("Best {0} solutions companies in {1}", CategoryName, CountryName);
                    else if (isProduct)
                        pgTitle.InnerText = String.Format("Best {0} partners and resellers in {1}", CategoryName, CountryName);
                    else
                        pgTitle.InnerText = String.Format("Best Managed IT Service Providers near you in {1}", CategoryName, CountryName);
                }

                if (isVertical)
                {
                    LblResultsTitle.Text = string.Format("Browse the best {0} IT companies in {1}", CategoryName, CountryName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} solutions companies like resellers, IT providers, MSPs, consultants and other channel companies in {1}. You can browse the companies based on their location or the products they offer.", CategoryName, CountryName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Browse the best {0} IT providers in {1}. You can filter the businesses based on their location and technologies they offer.", CategoryName, CountryName);
                    metaKeywords.Attributes["content"] = string.Format("{0} companies in {1}, {0} resellers in {1}, {0} IT providers in {1}", CategoryName, CountryName);
                }
                else if (isProduct)
                {
                    LblResultsTitle.Text = string.Format("Discover the best {0} partners and resellers in {1}", CategoryName, CountryName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} resellers or channel partners that are currently on our platform to help you with implementation, training or consulting services in {1}. You can locate the {0} partners based on their city and use additional filters like industries supported.", CategoryName, CountryName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("The best channel partners, resellers, affiliates and consultants that offer {0} solutions and products in {1}.", CategoryName, CountryName);
                    metaKeywords.Attributes["content"] = string.Format("{0} resellers {1}, {0} partners {1}, {0} channel partners {1}, {0} partner locator {1}", CategoryName, CountryName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
                else
                {
                    LblResultsTitle.Text = string.Format("Find the best Managed Service Providers in {1}", CategoryName, CountryName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best Managed IT Service Providers near you, in the city of {1}, to help you with implementation, support and maintenance of your IT infrastructure and software applications.", CategoryName, CountryName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Discover all the IT companies near you, in {1}, that offer Managed IT Services to help you implement, support and maintain your IT infrastructure and applications.", CategoryName, CountryName);
                    metaKeywords.Attributes["content"] = string.Format("managed service providers {1}, managed service providers near {1}, best managed service providers {1}", CategoryName, CountryName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
            }
            else if (RegionName != "")
            {
                HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                if (pgTitle != null)
                {
                    if (isVertical)
                        pgTitle.InnerText = String.Format("Best {0} solutions companies in {1}", CategoryName, RegionName);
                    else if (isProduct)
                        pgTitle.InnerText = String.Format("Best {0} partners and resellers in {1}", CategoryName, RegionName);
                    else
                        pgTitle.InnerText = String.Format("Best Managed IT Service Providers near you in {1}", CategoryName, RegionName);
                }

                if (isVertical)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} solutions companies in {1}", CategoryName, RegionName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} solutions providers that are currently on our platform to help you with implementation, training or consulting services in {1}.", CategoryName, RegionName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("The best IT providers, MSPs, resellers, affiliates and consultants that offer {0} software solutions and products in {1}.", CategoryName, RegionName);
                    metaKeywords.Attributes["content"] = string.Format("{0} software companies {1}, {0} software partners {1}, {0} software resellers {1}", CategoryName, RegionName);
                }
                else if (isProduct)
                {
                    LblResultsTitle.Text = string.Format("Find the best {0} partners and resellers in {1}", CategoryName, RegionName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best {0} resellers or channel partners that are currently on our platform to help you with implementation, training or consulting services in {1}.", CategoryName, RegionName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("The best channel partners, resellers, affiliates and consultants that offer {0} solutions and products in {1}.", CategoryName, RegionName);
                    metaKeywords.Attributes["content"] = string.Format("{0} resellers {1}, {0} partners {1}, {0} channel partners {1}", CategoryName, RegionName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
                else
                {
                    LblResultsTitle.Text = string.Format("Find the best Managed Service Providers in {1}", CategoryName, RegionName);

                    LblResultsContent1.Text = string.Format("Find in the list below the best Managed IT Service Providers near you, in the city of {1}, to help you with implementation, support and maintenance of your IT infrastructure and software applications.", CategoryName, RegionName);
                    LblResultsContent2.Text = "";

                    metaDescription.Attributes["content"] = string.Format("Discover all the IT companies near you, in {1}, that offer Managed IT Services to help you implement, support and maintain your IT infrastructure and applications.", CategoryName, RegionName);
                    metaKeywords.Attributes["content"] = string.Format("managed service providers {1}, managed service providers near {1}, best managed service providers {1}", CategoryName, RegionName);

                    //LblDisclaimer.Visible = LblDisclaimerText.Visible = true;
                    //LblDisclaimerText.Text = String.Format("This list is not an official {0} resellers list nor is affiliated with or in any way associated with {0}.", CategoryName);
                }
            }
            else
            {
                if (!isVertical && isProduct)
                {
                    HtmlGenericControl pgTitle = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "PgTitle");
                    if (pgTitle != null)
                    {
                        switch (Translation)
                        {
                            case "es":

                                LblRFPsFormText.Text = "Solicitud de Cotización";

                                pgTitle.InnerText = String.Format("Los mejores socios y revendedores de {0} - localizador de socios de {0}", CategoryName);

                                LblResultsTitle.Text = string.Format("Encuentra las mejores socios y revendedores de {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Encuentre en la lista a continuación los mejores revendedores o socios de {0} que se encuentran actualmente en nuestra plataforma para ayudarlo con los servicios de implementación, capacitación o consultoría. Puede localizar a los socios de {0} en función de su ciudad y utilizar filtros adicionales como las industrias admitidas.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("Los mejores socios, revendedores, afiliados y consultores que ofrecen soluciones y productos de {0}. Busque un socio de {0} cerca de usted.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("Socios de {0}, revendedores de {0}, localizador de socios de {0}", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "pt":

                                LblRFPsFormText.Text = "Solicitação de Cotação";

                                pgTitle.InnerText = String.Format("Melhores parceiros e revendedores da {0} - Localizador de parceiros {0}", CategoryName);

                                LblResultsTitle.Text = string.Format("Encontre os melhores parceiros e revendedores {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Encontre na lista abaixo um revendedor ou parceiro {0} que está atualmente em nossa plataforma para ajudá-lo com serviços de implementação, treinamento ou consultoria. Você pode localizar os parceiros da {0} com base em suas cidades e usar filtros adicionais, como setores com suporte.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("Os melhores parceiros, revendedores, afiliados e consultores que oferecem soluções e produtos {0}. Localize um parceiro {0} perto de você.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("Parceiros {0}, revendedores {0}, localizador de parceiros {0}", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "de":

                                LblRFPsFormText.Text = "Angebot anfordern";

                                pgTitle.InnerText = String.Format("Beste {0}-Partner und Wiederverkäufer. {0} Partnersuche", CategoryName);

                                LblResultsTitle.Text = string.Format("Finden Sie die besten {0}-Partner und -Wiederverkäufer", CategoryName);

                                LblResultsContent1.Text = string.Format("Finden Sie in der Liste unten einen {0}-Reseller oder Partner, der sich derzeit auf unserer Plattform befindet, um Sie bei der Implementierung, Schulung oder Beratungsdiensten zu unterstützen. Sie können die {0}-Partner anhand ihrer Stadt suchen und zusätzliche Filter wie unterstützte Branchen verwenden.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("Die besten Partner, Wiederverkäufer, verbundenen Unternehmen und Berater, die {0}-Lösungen und -Produkte anbieten. {0}-Partnersuche.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("{0}-Händler, {0}-Partner, {0}-Partnersuche", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "pl":

                                LblRFPsFormText.Text = "Poproś o wycenę";

                                pgTitle.InnerText = String.Format("Najlepsi partnerzy i sprzedawcy {0} — lokalizator partnerów {0}", CategoryName);

                                LblResultsTitle.Text = string.Format("Znajdź najlepszych partnerów i sprzedawców {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Znajdź na poniższej liście odsprzedawcę lub partnera firmy {0}, który obecnie korzysta z naszej platformy, aby pomóc Ci we wdrożeniu, szkoleniu lub usługach konsultingowych. Możesz zlokalizować partnerów {0} na podstawie ich miasta i użyć dodatkowych filtrów, takich jak obsługiwane branże.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("Najlepsi partnerzy handlowi, sprzedawcy, podmioty stowarzyszone i konsultanci oferujący rozwiązania i produkty firmy {0}. Znajdź najbliższego partnera {0}.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("Partnerzy {0}, sprzedawcy {0}, lokalizator partnerów {0}", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "it":

                                LblRFPsFormText.Text = "Richiedi Preventivo";

                                pgTitle.InnerText = String.Format("I migliori partner commerciali e rivenditori {0} - localizzatore di partner {0}", CategoryName);

                                LblResultsTitle.Text = string.Format("Trova i migliori partner commerciali e rivenditori {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Trova nell'elenco seguente un rivenditore {0} o un partner commerciale attualmente sulla nostra piattaforma per aiutarti con l'implementazione, la formazione o i servizi di consulenza. Puoi individuare i partner commerciali {0} in base alla loro città e utilizzare filtri aggiuntivi come i settori supportati.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("I migliori business partner, rivenditori, affiliati e consulenti che offrono soluzioni e prodotti {0}. Trova un partner commerciale {0} vicino a te.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("Partner commerciali {0}, rivenditori {0}, localizzatore di partner commerciali {0}", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "nl":

                                LblRFPsFormText.Text = "Uw offerte aanvragen";

                                pgTitle.InnerText = String.Format("Beste {0}-zakenpartner en -wederverkopers - {0}-partnerzoeker", CategoryName);

                                LblResultsTitle.Text = string.Format("Vind de beste zakelijke partners en resellers van {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Zoek in de onderstaande lijst een {0}-wederverkoper of zakelijke partner die momenteel op ons platform is om u te helpen met implementatie-, training- of adviesdiensten. U kunt de {0}-partners vinden op basis van hun stad en extra filters gebruiken, zoals ondersteunde sectoren.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("De beste zakenpartners, resellers, gelieerde ondernemingen en consultants die {0}-oplossingen en -producten aanbieden. Zoek een {0}-partner bij u in de buurt.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("{0}-zakenpartners, {0}-wederverkopers, {0}-partnerzoeker", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;

                            case "fr":

                                LblRFPsFormText.Text = "Obtenir un devis";

                                pgTitle.InnerText = String.Format("Meilleurs partenaires et revendeurs {0} - Localisateur de partenaires {0}", CategoryName);

                                LblResultsTitle.Text = string.Format("Trouvez les meilleurs partenaires et revendeurs {0}", CategoryName);

                                LblResultsContent1.Text = string.Format("Trouvez dans la liste ci-dessous un revendeur ou partenaire {0} qui est actuellement sur notre plate-forme pour vous aider avec des services de mise en œuvre, de formation ou de conseil. Vous pouvez localiser les partenaires {0} en fonction de leur ville et utiliser des filtres supplémentaires tels que les secteurs pris en charge.", CategoryName);
                                LblResultsContent2.Text = "";

                                metaDescription.Attributes["content"] = string.Format("Les meilleurs partenaires de distribution, revendeurs, affiliés et consultants qui proposent des solutions et des produits {0}. Recherchez un partenaire {0} près de chez vous.", CategoryName);
                                metaKeywords.Attributes["content"] = string.Format("Partenaires {0}, revendeurs {0}, localisateur de partenaires {0}", CategoryName);

                                //LblDisclaimer.Visible = LblDisclaimerText.Visible = false;
                                //LblDisclaimerText.Text = "";

                                break;
                        }
                    }
                }
                else if (!IsVertical && !isProduct)
                {

                }
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

            if (CategoryName != "")
            {
                if (RegionName != "" && CountryName != "" && StateName != "" && CityName != "")
                {
                    aBCrCategory.HRef = String.Format("/{0}/{1}/{2}/{3}/channel-partners/{4}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), StateName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName != "" && CityName != "" && StateName == "")
                {
                    aBCrCategory.HRef = String.Format("/{0}/{1}/{2}/channel-partners/{3}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName != "" && CityName == "" && StateName == "")
                {
                    aBCrCategory.HRef = String.Format("/{0}/{1}/channel-partners/{2}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName == "" && CityName == "" && StateName == "")
                {
                    aBCrCategory.HRef = String.Format("/{0}/channel-partners/{1}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else
                {
                    aBCrCategory.HRef = String.Format("/profile/channel-partners/{0}", CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }

                Label lblCategory = new Label();
                lblCategory.Text = CategoryName.Replace("_", " ").Replace("and", "&") + " / ";
                aBCrCategory.Controls.Add(lblCategory);
            }

            //aITLearnMore.HRef = ControlLoader.IntentSignals;
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
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/{3}/channel-partners/{4}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), trans, CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName != "" && trans != "" && CityName == "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/channel-partners/{3}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), trans, CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
            }
            else if (CountryName != "" && Translation != "")
            {
                divTransArea.Visible = true;
                ImgTransFlag.Src = "/assets_out/images/flags/UK.svg";

                if (RegionName != "" && CountryName != "" && CityName != "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/{2}/channel-partners/{3}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CityName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
                else if (RegionName != "" && CountryName != "" && CityName == "")
                {
                    aTranslate.HRef = String.Format("/{0}/{1}/channel-partners/{2}", RegionName.ToLower().Replace(" ", "-").Replace("&", "and"), CountryName.ToLower().Replace(" ", "-").Replace("&", "and"), CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                }
            }

            if (!aTranslate.HRef.StartsWith("/"))
                aTranslate.HRef = "/" + aTranslate.HRef;
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

            LeadID = 0;
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
                                    //aPartPrMore.Visible = true;
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
                session.OpenConnection();

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                IsVertical = vertical != null;
                VerticalID = vertical != null ? vertical.Id : 0;

                string orderBy = "";

                if (DrpSortList.SelectedValue == "0")
                {
                    orderBy = "";
                }
                else if (DrpSortList.SelectedValue == "1")
                {
                    orderBy = " company_name asc ";
                }
                else if (DrpSortList.SelectedValue == "2")
                {
                    orderBy = " company_name desc ";
                }
                else if (DrpSortList.SelectedValue == "3")
                {
                    orderBy = " sysdate asc ";
                }
                else if (DrpSortList.SelectedValue == "4")
                {
                    orderBy = " sysdate desc ";
                }

                if (vSession.UsersSearchInfoList == null || (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count == 0))
                {
                    vSession.UsersSearchInfoList = new List<ElioUsersSearchInfo>();
                    vSession.UsersSearchInfoList = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                }

                //List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                LoadRepeater(Navigation.Previous, vSession.UsersSearchInfoList);
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

        protected void lbtnNext_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                IsVertical = vertical != null;
                VerticalID = vertical != null ? vertical.Id : 0;

                string orderBy = "";

                if (DrpSortList.SelectedValue == "0")
                {
                    orderBy = "";
                }
                else if (DrpSortList.SelectedValue == "1")
                {
                    orderBy = " company_name asc ";
                }
                else if (DrpSortList.SelectedValue == "2")
                {
                    orderBy = " company_name desc ";
                }
                else if (DrpSortList.SelectedValue == "3")
                {
                    orderBy = " sysdate asc ";
                }
                else if (DrpSortList.SelectedValue == "4")
                {
                    orderBy = " sysdate desc ";
                }

                if (vSession.UsersSearchInfoList == null || (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count == 0))
                {
                    vSession.UsersSearchInfoList = new List<ElioUsersSearchInfo>();
                    vSession.UsersSearchInfoList = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                }

                //List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                LoadRepeater(Navigation.Next, vSession.UsersSearchInfoList);
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

        protected void aSearchBy_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string url = "";
                string region = "";
                string country = "";
                string city = "";

                if (DrpCategoryCountries.SelectedValue != "0")
                {
                    country = DrpCategoryCountries.SelectedItem.Text.ToLower().Replace(" ", "-").Replace("&", "and");

                    if (DrpCategoryCountries.SelectedItem.Text == "France")
                        country = "fr/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Canada")
                        country = "ca/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Spain")
                        country = "es/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Germany")
                        country = "de/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Portugal" || DrpCategoryCountries.SelectedItem.Text == "Brazil")
                        country = "pt/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Austria")
                        country = "at/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Italy")
                        country = "it/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Argentina" || DrpCategoryCountries.SelectedItem.Text == "Bolivia" || DrpCategoryCountries.SelectedItem.Text == "Chile"
                        || DrpCategoryCountries.SelectedItem.Text == "Colombia" || DrpCategoryCountries.SelectedItem.Text == "Costa Rica" || DrpCategoryCountries.SelectedItem.Text == "Dominican Republic"
                        || DrpCategoryCountries.SelectedItem.Text == "Ecuador" || DrpCategoryCountries.SelectedItem.Text == "El Salvador" || DrpCategoryCountries.SelectedItem.Text == "Guatemala"
                        || DrpCategoryCountries.SelectedItem.Text == "Honduras" || DrpCategoryCountries.SelectedItem.Text == "Mexico" || DrpCategoryCountries.SelectedItem.Text == "Panama"
                        || DrpCategoryCountries.SelectedItem.Text == "Paraguay" || DrpCategoryCountries.SelectedItem.Text == "Peru" || DrpCategoryCountries.SelectedItem.Text == "Puerto Rico"
                        || DrpCategoryCountries.SelectedItem.Text == "Uruguay" || DrpCategoryCountries.SelectedItem.Text == "Venezuela")
                        country = "la/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Netherlands")
                        country = "nl/" + country;
                    else if (DrpCategoryCountries.SelectedItem.Text == "Poland")
                        country = "pl/" + country;

                    region = Sql.GetRegionByCountryTbl(DrpCategoryCountries.SelectedItem.Text, session);

                    url = "/" + region.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + country + "/";

                    if (DrpCategoryCities.SelectedValue != "0")
                    {
                        city = DrpCategoryCities.SelectedItem.Text.ToLower().Replace(" ", "-").Replace("&", "and");

                        string[] specCountries = { "United States", "United Kingdom", "India", "Australia" };

                        if (specCountries.Contains(DrpCategoryCountries.SelectedItem.Text))
                        {
                            string state = Sql.GetStateByCountryCityTbl(DrpCategoryCountries.SelectedItem.Text, DrpCategoryCities.SelectedItem.Text, session);

                            if (state != "")
                                url += state.ToLower().Replace(" ", "-").Replace("&", "and") + "/";
                        }

                        url += city + "/";
                    }

                    url += "channel-partners/" + CategoryName.ToLower().Replace(" ", "_").Replace("&", "and");
                }
                else
                {
                    if (DrpCategoryCities.SelectedValue != "0")
                    {
                        string regCountry = Sql.GetRegionCountryByCityTbl(DrpCategoryCities.SelectedItem.Text, session);
                        if (regCountry != "")
                        {
                            string[] paths = regCountry.Split('/').ToArray();
                            if (paths.Length > 0 && paths.Length == 2)
                            {
                                if (paths[1] != "")
                                {
                                    country = paths[1].Replace("-", " ").Replace("and", "&");

                                    if (country == "france")
                                        country = "fr/" + country;
                                    else if (country == "canada")
                                        country = "ca/" + country;
                                    else if (country == "spain")
                                        country = "es/" + country;
                                    else if (country == "germany")
                                        country = "de/" + country;
                                    else if (country == "portugal" || country == "brazil")
                                        country = "pt/" + country;
                                    else if (country == "austria")
                                        country = "at/" + country;
                                    else if (country == "italy")
                                        country = "it/" + country;
                                    else if (country == "argentina" || country == "bolivia" || country == "chile"
                                        || country == "colombia" || country == "costa rica" || country == "dominican republic"
                                        || country == "ecuador" || country == "el salvador" || country == "guatemala"
                                        || country == "honduras" || country == "mexico" || country == "panama"
                                        || country == "paraguay" || country == "peru" || country == "puerto rico"
                                        || country == "uruguay" || country == "venezuela")
                                        country = "la/" + country;
                                    else if (country == "netherlands")
                                        country = "nl/" + country;
                                    else if (country == "poland")
                                        country = "pl/" + country;

                                    if (paths[0] != "")
                                        regCountry = paths[0] + "/" + country.ToLower().Replace(" ", "-").Replace("&", "and");
                                }
                            }

                            city = DrpCategoryCities.SelectedItem.Text.ToLower().Replace(" ", "-").Replace("&", "and");

                            if (StateName == "")
                                url = "/" + regCountry + "/" + city + "/channel-partners/" + CategoryName.ToLower().Replace(" ", "_").Replace("&", "and");
                            else
                                url = "/" + regCountry + "/" + StateName.ToLower().Replace(" ", "-").Replace("&", "and") + "/" + city + "/channel-partners/" + CategoryName.ToLower().Replace(" ", "_").Replace("&", "and");
                        }
                    }
                }

                if (url != "")
                    Response.Redirect(url, false);
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

        protected void aRFPsForm_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                RepeaterItem item = (RepeaterItem)btn.NamingContainer;

                if (item != null)
                {
                    ElioUsersSearchInfo row = (ElioUsersSearchInfo)item.DataItem;
                    if (row != null)
                    {
                        //if (item != null)
                        //{
                        ResetRFPsFields();
                        LoadCountries();

                        //string companyId = (item.FindControl("HdnCompanyID") as HiddenField).Value;

                        RFPCompanyID = row.Id;

                        if (RFPCompanyID > 0)
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenRFPsPopUp();", true);
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

        protected void BtnProceed_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divDemoSuccessMsg.Visible = divDemoWarningMsg.Visible = false;

                if (RFPCompanyID > 0)
                {
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

                        if (LeadID == 0)
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
                            lead.RfpMessageCompanyIdIsFor = RFPCompanyID.ToString();

                            loader.Insert(lead);

                            LeadID = lead.Id;

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
                            lead = Sql.GetSnitcherWebsiteLeadById(LeadID, session);
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
                                lead.RfpMessageCompanyIdIsFor = RFPCompanyID.ToString();

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
                else
                {
                    throw new Exception("RFPCompanyID is 0!");
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

        protected void DrpCategoryCountries_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (DrpCategoryCountries.SelectedValue != "0")
                {
                    LoadCategoryCities(DrpCategoryCountries.SelectedItem.Text);
                }
                else
                    LoadCategoryCities("");
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

        protected void DrpSortList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string orderBy = "";

                if (DrpSortList.SelectedValue == "0")
                {
                    orderBy = "";
                }
                else if (DrpSortList.SelectedValue == "1")
                {
                    orderBy = " company_name asc ";
                }
                else if (DrpSortList.SelectedValue == "2")
                {
                    orderBy = " company_name desc ";
                }
                else if (DrpSortList.SelectedValue == "3")
                {
                    orderBy = " sysdate asc ";
                }
                else if (DrpSortList.SelectedValue == "4")
                {
                    orderBy = " sysdate desc ";
                }

                ElioSubIndustriesGroupItems vertical = null;

                vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                IsVertical = vertical != null;
                VerticalID = vertical != null ? vertical.Id : 0;

                if (vSession.UsersSearchInfoList == null || (vSession.UsersSearchInfoList != null && vSession.UsersSearchInfoList.Count == 0))
                {
                    vSession.UsersSearchInfoList = new List<ElioUsersSearchInfo>();
                    vSession.UsersSearchInfoList = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                }

                //List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", RegionName, CountryName, "", CityName, "", orderBy, vSession, session);
                LoadRepeater(Navigation.None, vSession.UsersSearchInfoList);
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
    }
}