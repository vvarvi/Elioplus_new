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
using System.Linq;
using System.Text.RegularExpressions;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI;
using AjaxControlToolkit;

namespace WdS.ElioPlus.pages
{
    public partial class ProfilesSubIndustriesForVendorsPage : System.Web.UI.Page
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

        public string CategoryName
        {
            get
            {
                if (ViewState["CategoryName"] != null)
                    return ViewState["CategoryName"].ToString();
                else
                {
                    string name = "";
                    Uri path = HttpContext.Current.Request.Url;
                    var pathSegs = path.Segments;
                    if (pathSegs.Length > 0)
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

                    return ViewState["CategoryName"].ToString();
                }
            }
            set
            {
                ViewState["CategoryName"] = value;
            }
        }

        public string RootName
        {
            get
            {
                if (ViewState["RootName"] == null)
                {
                    string name = "";

                    try
                    {
                        Uri path = HttpContext.Current.Request.Url;
                        var pathSegs = path.Segments;
                        if (pathSegs.Length > 0)
                        {
                            string segs = pathSegs[1].TrimEnd('/').TrimEnd('-').TrimEnd('_').Trim();

                            if (segs.EndsWith("_"))
                                segs = segs.TrimEnd('_');

                            string[] segsWords = segs.Split('-');

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

                            if (name != "")
                            {
                                if (name == "Partner Programs" || name == "partner programs")
                                    name = "Saas Partner Programs";
                                else if (name == "White Label" || name == "white label")
                                    name = "White Label Partner Programs";
                                else if (name == "Msps" || name == "msps")
                                    name = "Msps Partner Programs";
                                else if (name == "System Integrators" || name == "system integrators")
                                    name = "Systems Integrators Partner Programs";
                            }

                            ViewState["RootName"] = name;
                        }
                        else
                            ViewState["RootName"] = "Partner Programs";
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["RootName"].ToString();
                }
                else
                    return ViewState["RootName"].ToString();
            }
            set
            {
                ViewState["RootName"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(aSearchBy);

                if (!IsPostBack)
                {
                    UpdateStrings();
                    FixPage();
                    SetLinks();

                    //RdgResults.ShowHeader = false;

                    FixPageResultsContent();

                    //PnlResults.Visible = vSession.ShowResultsPanel = true;

                    //if (vSession.SearchQueryString != "")
                    //{
                    //    vSession.ShowResultsPanel = true;
                    //    //PnlResults.Visible = true;
                    //}
                    //else
                    //    Response.Redirect(ControlLoader.SearchByType("vendors"), false);

                    //RdgResults.CurrentPageIndex = (vSession.CurrentGridPageIndex != 0) ? vSession.CurrentGridPageIndex : 0;
                    //RdgResults.MasterTableView.GetColumn("id").Display = false;
                }

                SetBreadCrumb();
            }
            catch (Exception ex)
            {
                Response.Redirect(ControlLoader.SearchForVendors, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        private void FixPageResultsContent()
        {
            string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
            if (path.Length == 0)
            {
                Response.Redirect(ControlLoader.Default(), false);
            }

            if (path.Length > 2)
            {
                string type = GlobalMethods.FixSearchDescriptionBack(path[path.Length - 2]);

                if (type != "vendors" && type != "channel-partners")
                {
                    Response.Redirect(ControlLoader.SearchForVendors, false);
                    return;
                }

                if (type.StartsWith("vendors"))
                    Type = Types.Vendors.ToString().ToLower();
                else
                {
                    Type = EnumHelper.GetDescription(Types.Resellers).ToString();
                    Type = Type.Replace(" ", "-").ToLower();
                }

                string subIndustry = path[path.Length - 1];

                subIndustry = subIndustry.Replace("and", "&").Replace("_", " ");

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(subIndustry, session);
                if (vertical != null)
                {
                    HtmlControl metaHeadDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
                    HtmlControl metaHeadKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");

                    CategoryName = vertical.Description;
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
                    string lnk9 = "";
                    string lnk10 = "";

                    string metaDescription = "";
                    string metaKeywords = "";

                    string pageTopic = GlobalMethods.SetProfilePageContent(vertical.Description, Type.ToLower(), out content1, out content2, out content3,
                        out lnk1, out lnk2, out lnk3, out lnk4,
                        out lnk5, out lnk6, out lnk7, out lnk8,
                        out lnk9, out lnk10,
                        out metaDescription, out metaKeywords);

                    LblResultsTitle.Text = pageTopic;
                    LblResultsContent1.Text = content1;
                    LblResultsContent2.Text = content2;
                    LblResultsContent3.Text = content3;

                    bool hasRelatedCategories = lnk1 != "" || lnk2 != "" || lnk3 != "" || lnk4 != "" || lnk5 != "" || lnk6 != "" || lnk7 != "" || lnk8 != "";

                    if (hasRelatedCategories)
                    {
                        LblFooterRelatedCompanies.Visible = true;
                        LblFooterRelatedCompanies.Text = "Related Categories";
                        LblFooterCompany1.Text = lnk1;
                        LblFooterCompany2.Text = lnk2;
                        LblFooterCompany3.Text = lnk3;
                        LblFooterCompany4.Text = lnk4;
                        LblFooterCompany5.Text = lnk5;
                        LblFooterCompany6.Text = lnk6;
                        LblFooterCompany7.Text = lnk7;
                        LblFooterCompany8.Text = lnk8;

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
                        divRelatedCompanies.Visible = false;
                        LblFooterCompany1.Text = "";
                        LblFooterCompany2.Text = "";
                        LblFooterCompany3.Text = "";
                        LblFooterCompany4.Text = "";
                        LblFooterCompany5.Text = "";
                        LblFooterCompany6.Text = "";
                        LblFooterCompany7.Text = "";
                        LblFooterCompany8.Text = "";
                    }

                    divRelatedCompanies.Visible = lnk1 != "" || lnk2 != "" || lnk3 != "" || lnk4 != "" || lnk5 != "" || lnk6 != "" || lnk7 != "" || lnk8 != "";

                    string program = path[path.Length - 3];
                    if (new string[] { "white-label", "msps", "system-integrators" }.Contains(program))
                    {
                        aFooterCompany1.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk1.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany2.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk2.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany3.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk3.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany4.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk4.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany5.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk5.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany6.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk6.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany7.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk7.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany8.HRef = ControlLoader.SubIndustryPartnerProgramProfiles(program, Type, lnk8.Replace("&", "and").Replace(" ", "_").ToLower());
                        LblResultsContent1.Text = "";
                        LblResultsContent2.Text = "";
                        LblResultsContent3.Text = "";

                        if (program == "white-label")
                        {
                            ProgramID = 1;
                            LblResultsTitle.Text = LblResultsTitle.Text.Replace("a partner program", "a white label partner program");
                        }
                        if (program == "msps")
                        {
                            ProgramID = 7;
                            LblResultsTitle.Text += " for MSPs";
                        }
                        if (program == "system-integrators")
                        {
                            ProgramID = 6;
                            LblResultsTitle.Text += " for Systems Integrators";
                        }

                        metaHeadDescription.Attributes["content"] = metaDescription.ToLower().Replace(vertical.Description.ToLower(), program.Replace("-", " ") + " " + vertical.Description.ToLower());
                        metaHeadKeywords.Attributes["content"] = metaKeywords.ToLower().Replace(vertical.Description.ToLower(), program.Replace("-", " ") + " " + vertical.Description.ToLower());
                    }
                    else
                    {
                        aFooterCompany1.HRef = ControlLoader.SubIndustryProfiles(Type, lnk1.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany2.HRef = ControlLoader.SubIndustryProfiles(Type, lnk2.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany3.HRef = ControlLoader.SubIndustryProfiles(Type, lnk3.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany4.HRef = ControlLoader.SubIndustryProfiles(Type, lnk4.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany5.HRef = ControlLoader.SubIndustryProfiles(Type, lnk5.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany6.HRef = ControlLoader.SubIndustryProfiles(Type, lnk6.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany7.HRef = ControlLoader.SubIndustryProfiles(Type, lnk7.Replace("&", "and").Replace(" ", "_").ToLower());
                        aFooterCompany8.HRef = ControlLoader.SubIndustryProfiles(Type, lnk8.Replace("&", "and").Replace(" ", "_").ToLower());

                        metaHeadDescription.Attributes["content"] = metaDescription;
                        metaHeadKeywords.Attributes["content"] = metaKeywords;
                    }

                    bool mustClose = false;

                    if (session.Connection.State == ConnectionState.Closed)
                    {
                        session.OpenConnection();
                        mustClose = true;
                    }

                    List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", vertical.Id.ToString(), "", ProgramID.ToString(), "0", "0", "", "", "", "", "", "", vSession, session);

                    if (mustClose)
                        session.CloseConnection();

                    LoadRepeater(Navigation.None, users);
                }
                else
                    Response.Redirect(ControlLoader.SearchByType(Type), false);
            }
            else
                Response.Redirect(ControlLoader.SearchByType("vendors"), false);
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

                //LblHeaderCategory.Text = string.Format(" out of {0} partner program in {1}:", users.Count.ToString(), CategoryName);
                //LblHeaderCategory.Text = "<b>Opportunities:</b> Viewing <b>" + users.Count.ToString() + "</b> results out of <b>25000+</b> partnership opportunities on our platform!";
            }
            else
            {
                RdgResults.DataSource = null;
                RdgResults.DataBind();

                //LblHeaderCategory.Text = "<b>Opportunities:</b> Viewing <b>0</b> results out of <b>25000+</b> partnership opportunities on our platform!";

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

        private void FixPage()
        {
            aGetStarted.Visible = LblGetStartedList.Visible = vSession.User == null;
        }

        private void UpdateStrings()
        {
            LblGetStartedList.Text = "Join this List";
        }

        private void SetLinks()
        {
            aGetStarted.HRef = ControlLoader.SignUp;
        }

        private void SetBreadCrumb()
        {
            aBCrHome.HRef = "/" + RootName.ToLower().Replace(" ", "-");
            Label lbl = new Label();
            lbl.Text = RootName + " / ";
            aBCrHome.Controls.Add(lbl);

            if (CategoryName != "")
            {
                if (RootName == "Saas Partner Programs")
                    aBCrCategory.HRef = String.Format("/partner-programs/vendors/{0}", CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                else if (RootName == "White Label Partner Programs")
                    aBCrCategory.HRef = String.Format("/white-label/vendors/{0}", CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                else if (RootName == "Msps Partner Programs")
                    aBCrCategory.HRef = String.Format("/msps/vendors/{0}", CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));
                else if (RootName == "Systems Integrators Partner Programs")
                    aBCrCategory.HRef = String.Format("/system-integrators/vendors/{0}", CategoryName.ToLower().Replace(" ", "_").Replace("&", "and"));

                Label lblCategory = new Label();
                lblCategory.Text = CategoryName.Replace("_", " ").Replace("and", "&") + " / ";
                aBCrCategory.Controls.Add(lblCategory);
            }
        }

        private string GetProgram(string userPartnerProgram)
        {
            if (userPartnerProgram == "White Label")
            {
                return "/white-label-partner-programs";
            }
            else if (userPartnerProgram == "Reseller")
            {
                return "/saas-partner-programs";
            }
            else if (userPartnerProgram == "Managed Service Provider")
            {
                return "/msps-partner-programs";
            }
            else if (userPartnerProgram == "System Integrator")
            {
                return "/systems-integrators-partner-programs";
            }
            else
                return "";
        }

        #endregion

        #region Grids

        protected void RdgResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = e.Item;
                    if (item != null)
                    {
                        ElioUsersSearchInfo row = (ElioUsersSearchInfo)e.Item.DataItem;
                        if (row != null)
                        {
                            if (row.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                HtmlGenericControl featured = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divFeatured");
                                featured.Visible = true;
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

                                    if (RootName == "Saas Partner Programs")
                                        aVert1.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert1.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert1.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert1.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert1.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert2");
                                    Label LblVert2 = (Label)ControlFinder.FindControlRecursive(item, "LblVert2");
                                    aVert2.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert2.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert2.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert2.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert2.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert2.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert3");
                                    Label LblVert3 = (Label)ControlFinder.FindControlRecursive(item, "LblVert3");
                                    aVert3.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert3.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert3.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert3.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert3.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert3.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert4");
                                    Label LblVert4 = (Label)ControlFinder.FindControlRecursive(item, "LblVert4");
                                    aVert4.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert4.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert4.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert4.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert4.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert4.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert5");
                                    Label LblVert5 = (Label)ControlFinder.FindControlRecursive(item, "LblVert5");
                                    aVert5.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert5.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert5.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert5.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert5.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert5.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert6");
                                    Label LblVert6 = (Label)ControlFinder.FindControlRecursive(item, "LblVert6");
                                    aVert6.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert6.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert6.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert6.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert6.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert6.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert7");
                                    Label LblVert7 = (Label)ControlFinder.FindControlRecursive(item, "LblVert7");
                                    aVert7.Visible = true;

                                    if (RootName == "Saas Partner Programs")
                                        aVert7.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "White Label Partner Programs")
                                        aVert7.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("white-label", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Msps Partner Programs")
                                        aVert7.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("msps", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());
                                    else if (RootName == "Systems Integrators Partner Programs")
                                        aVert7.HRef = ControlLoader.SubIndustryPartnerProgramProfiles("system-integrators", "vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert7.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert8");
                                    Label LblVert8 = (Label)ControlFinder.FindControlRecursive(item, "LblVert8");
                                    aVert8.Visible = true;
                                    aVert8.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert8.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert9");
                                    Label LblVert9 = (Label)ControlFinder.FindControlRecursive(item, "LblVert9");
                                    aVert9.Visible = true;
                                    aVert9.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert9.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert10");
                                    Label LblVert10 = (Label)ControlFinder.FindControlRecursive(item, "LblVert10");
                                    aVert10.Visible = true;
                                    aVert10.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert10.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert11");
                                    Label LblVert11 = (Label)ControlFinder.FindControlRecursive(item, "LblVert11");
                                    aVert11.Visible = true;
                                    aVert11.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert11.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert12");
                                    Label LblVert12 = (Label)ControlFinder.FindControlRecursive(item, "LblVert12");
                                    aVert12.Visible = true;
                                    aVert12.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert12.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert13");
                                    Label LblVert13 = (Label)ControlFinder.FindControlRecursive(item, "LblVert13");
                                    aVert13.Visible = true;
                                    aVert13.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert13.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert14");
                                    Label LblVert14 = (Label)ControlFinder.FindControlRecursive(item, "LblVert14");
                                    aVert14.Visible = true;
                                    aVert14.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert14.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (count < userVarticals.Count)
                                {
                                    HtmlAnchor aVert15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVert15");
                                    Label LblVert15 = (Label)ControlFinder.FindControlRecursive(item, "LblVert15");
                                    aVert15.Visible = true;
                                    aVert15.HRef = ControlLoader.SubIndustryProfiles("vendors", userVarticals[count].Description.Replace("&", "and").Replace(" ", "_").ToLower());

                                    LblVert15.Text = userVarticals[count].Description;
                                    count++;
                                }

                                if (userVarticals.Count > 14)
                                {
                                    HtmlAnchor aVertMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aVertMore");

                                    string moreItems = GlobalMethods.FillRadToolTipWithVerticalsRestDescriptions(userVarticals);
                                    if (moreItems != "")
                                    {
                                        Label lblVertMore = (Label)ControlFinder.FindControlRecursive(item, "LblVertMore");
                                        Label LblVertMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblVertMoreNum");
                                        aVertMore.Visible = userVarticals.Count > 15;
                                        aVertMore.HRef = "";

                                        if (aVertMore.Visible)
                                        {
                                            lblVertMore.Text = "more";
                                            LblVertMoreNum.Text = "+" + (userVarticals.Count - 10).ToString() + " ";
                                            aVertMore.Attributes["title"] = moreItems;
                                        }
                                    }
                                    else
                                        aVertMore.Visible = false;
                                }
                            }
                            else
                            {
                                HtmlGenericControl divCategoriesArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divCategoriesArea");
                                divCategoriesArea.Visible = false;
                            }

                            List<ElioPartners> userPartners = Sql.GetUsersPartners(row.Id, session);
                            if (userPartners.Count > 0)
                            {
                                int count = 0;

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr1 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr1");
                                    Label LblPartPr1 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr1");
                                    aPartPr1.Visible = true;
                                    aPartPr1.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr1.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr2 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr2");
                                    Label LblPartPr2 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr2");
                                    aPartPr2.Visible = true;
                                    aPartPr2.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr2.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr3 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr3");
                                    Label LblPartPr3 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr3");
                                    aPartPr3.Visible = true;
                                    aPartPr3.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr3.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr4 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr4");
                                    Label LblPartPr4 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr4");
                                    aPartPr4.Visible = true;
                                    aPartPr4.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr4.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr5 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr5");
                                    Label LblPartPr5 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr5");
                                    aPartPr5.Visible = true;
                                    aPartPr5.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr5.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr6 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr6");
                                    Label LblPartPr6 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr6");
                                    aPartPr6.Visible = true;
                                    aPartPr6.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr6.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr7 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr7");
                                    Label LblPartPr7 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr7");
                                    aPartPr7.Visible = true;
                                    aPartPr7.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr7.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr8 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr8");
                                    Label LblPartPr8 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr8");
                                    aPartPr8.Visible = true;
                                    aPartPr8.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr8.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr9 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr9");
                                    Label LblPartPr9 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr9");
                                    aPartPr9.Visible = true;
                                    aPartPr9.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr9.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr10 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr10");
                                    Label LblPartPr10 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr10");
                                    aPartPr10.Visible = true;
                                    aPartPr10.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr10.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr11 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr11");
                                    Label LblPartPr11 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr11");
                                    aPartPr11.Visible = true;
                                    aPartPr11.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr11.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr12 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr12");
                                    Label LblPartPr12 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr12");
                                    aPartPr12.Visible = true;
                                    aPartPr12.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr12.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr13 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr13");
                                    Label LblPartPr13 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr13");
                                    aPartPr13.Visible = true;
                                    aPartPr13.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr13.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr14 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr14");
                                    Label LblPartPr14 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr14");
                                    aPartPr14.Visible = true;
                                    aPartPr14.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr14.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (count < userPartners.Count)
                                {
                                    HtmlAnchor aPartPr15 = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPr15");
                                    Label LblPartPr15 = (Label)ControlFinder.FindControlRecursive(item, "LblPartPr15");
                                    aPartPr15.Visible = true;
                                    aPartPr15.HRef = GetProgram(userPartners[count].PartnerDescription);

                                    LblPartPr15.Text = userPartners[count].PartnerDescription;
                                    count++;
                                }

                                if (userPartners.Count > 14)
                                {
                                    HtmlAnchor aPartPrMore = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartPrMore");

                                    string moreItems = GlobalMethods.FillRadToolTipWithPartnerProgramRestDescriptions(userPartners);
                                    if (moreItems != "")
                                    {
                                        Label lblPartPrMore = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMore");
                                        Label lblPartPrMoreNum = (Label)ControlFinder.FindControlRecursive(item, "LblPartPrMoreNum");
                                        aPartPrMore.Visible = userPartners.Count > 15;
                                        aPartPrMore.HRef = "";

                                        if (aPartPrMore.Visible)
                                        {
                                            lblPartPrMore.Text = "more";
                                            lblPartPrMoreNum.Text = "+" + (userPartners.Count - 7).ToString() + " ";

                                            HtmlImage iMoreProducts = (HtmlImage)ControlFinder.FindControlRecursive(item, "iMoreProducts");
                                            aPartPrMore.Attributes["title"] = moreItems;
                                        }
                                    }
                                    else
                                        aPartPrMore.Visible = false;
                                }
                            }
                            else
                            {
                                HtmlGenericControl divProductsArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divProductsArea");
                                divProductsArea.Visible = false;
                            }

                            if (row.Overview != "")
                            {
                                if (row.Overview.Length < 35)
                                {
                                    row.Overview = (row.Overview.EndsWith(".")) ? row.Overview : row.Overview + ". ";
                                    row.Overview = row.Overview + Environment.NewLine + "Check our profile for more details.";
                                }
                                else
                                {
                                    row.Overview = GlobalMethods.FixParagraphsView(row.Overview);
                                }

                            }
                            else
                            {
                                row.Overview = "Oups, we are sorry but there are no description data for this company.";
                            }

                            if (row.Overview != "")
                            {
                                row.Overview = row.Overview.Replace("<br/><br/><br/><br/>", "").Replace("<br/><br/><br/>", "").Replace("<br/><br/>", "").Replace("<br/>", "");
                                row.Overview = row.Overview.Replace("<br><br><br><br>", "").Replace("<br><br><br>", "").Replace("<br><br>", "").Replace("<br>", "");

                                Label lblOverview = (Label)ControlFinder.FindControlRecursive(item, "LblOverview");
                                lblOverview.Text = row.Overview;
                            }

                            HtmlAnchor aPartnerPortalLogin = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPartnerPortalLogin");

                            aPartnerPortalLogin.Visible = (vSession.User == null || (vSession.User != null && vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))) ? true : false;

                            if (aPartnerPortalLogin.Visible)
                            {
                                if (vSession.User != null)
                                {
                                    if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
                                        aPartnerPortalLogin.HRef = "/" + Regex.Replace(row.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-login";
                                    //else
                                    //    aPartnerPortalLogin.HRef = "/" + Regex.Replace(row.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
                                }
                                //else
                                //    aPartnerPortalLogin.HRef = "/" + Regex.Replace(row.CompanyName, @"[^A-Za-z0-9]+", "_").Trim().ToLower() + "/partner-free-sign-up";
                            }

                        }
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

        protected void lbtnPrev_Click(object sender, EventArgs e)
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

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", "", "", "", "", "", orderBy, vSession, session);

                LoadRepeater(Navigation.Previous, users); ;
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

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", "", "", "", "", "", orderBy, vSession, session);

                LoadRepeater(Navigation.Next, users);
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

                ElioSubIndustriesGroupItems vertical = Sql.GetSubIndustriesGroupItemByVerticalDescription(CategoryName, session);

                List<ElioUsersSearchInfo> users = GlobalDBMethods.GetSEOSearchResultsNewCP(Type, "0", (vertical != null) ? vertical.Id.ToString() : "0", CategoryName, "0", "0", "0", "", "", "", "", "", orderBy, vSession, session);
                LoadRepeater(Navigation.None, users);
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