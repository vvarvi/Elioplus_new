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
using Telerik.Web.UI;
using System.Data;
using System.Web;
using System.Linq;
using ServiceStack;

namespace WdS.ElioPlus.pages
{
    public partial class SearchForChannelPartnersPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

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
                        string type = pathSegs[pathSegs.Length - 1].TrimEnd('/');
                        string[] typeWords = type.Split('-');

                        foreach (var word in typeWords)
                        {
                            typeName += char.ToUpper(word[0]) + word.Substring(1) + " ";
                        }

                        ViewState["Type"] = typeName.TrimEnd();
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }

                    return ViewState["Type"].ToString().TrimEnd();
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
                    {
                        string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
                        if (path.Length > 0)
                        {
                            if (string.IsNullOrEmpty(Type))
                            {
                                if (vSession.User == null)
                                    Response.Redirect(ControlLoader.Default(), false);
                                else
                                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                            }

                            UpdateStrings();
                            LoadVerticals();
                            GetRegions();
                            SetLinks();
                            LoadTechnologies();
                            FixPopularLinks();

                            aBCrHome.HRef = ControlLoader.SearchForChannelPartners;
                            Label lbl = new Label();
                            lbl.Text = "Channel-Partners" + " / ";
                            aBCrHome.Controls.Add(lbl);
                        }
                        else
                            Response.Redirect(ControlLoader.Default(), false);
                    }
                    else
                        Response.Redirect(ControlLoader.Default(), false);
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
            apopBigData.HRef = "/profile/channel-partners/big_data";
            apopAnalSoft.HRef = "/profile/channel-partners/analytics_software";
            apopEcommerce.HRef = "/profile/channel-partners/ecommerce";
            apopAccounting.HRef = "/profile/channel-partners/accounting";
            apopDatabases.HRef = "/profile/channel-partners/databases";
            apopCrm.HRef = "/profile/channel-partners/crm";
            apopAdServing.HRef = "/profile/channel-partners/ad_serving";
        }

        private void SetLinks()
        {
            //aViewAllProducts.HRef = ControlLoader.SearchForChannelPartnerships;
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

        private void GetCountriesByRegion(string regionName, Repeater rpt, Label lblRegion)
        {
            lblRegion.Text = regionName;

            DataTable table = Sql.GetCountriesByRegionTbl(regionName, session);
            if (table != null && table.Rows.Count > 0)
            {
                rpt.Visible = true;
                rpt.DataSource = table;
                rpt.DataBind();
            }
            else
            {
                //lblRegion.Text = "";
                rpt.Visible = false;
            }
        }

        protected void GetRegions()
        {
            //DataTable table = Sql.GetRegionsFromCountries(session);
            //if (table != null && table.Rows.Count > 0)
            //{
            //    //RptRegions.Visible = true;

            //    //RptRegions.DataSource = table;
            //    //RptRegions.DataBind();

            //    if (table.Rows.Count == 6)
            //    {
            GetCountriesByRegion("Africa", RptReg1, LblReg1);
            GetCountriesByRegion("Asia Pacific", RptReg2, LblReg2);
            GetCountriesByRegion("Europe", RptReg3, LblReg3);
            GetCountriesByRegion("Middle East", RptReg4, LblReg4);
            GetCountriesByRegion("North America", RptReg5, LblReg5);
            GetCountriesByRegion("South America", RptReg6, LblReg6);
            //    }
            //}
            //else
            //{
            //    //RptRegions.Visible = false;
            //}
        }

        private void LoadVerticals()
        {
            DataTable table = Sql.GetVerticalsForSearchResultsOrderByLetter(Type, session);

            DataTable tblA = new DataTable();
            tblA.Columns.Add("description");
            tblA.Columns.Add("link");

            DataTable tblB = new DataTable();
            tblB.Columns.Add("description");
            tblB.Columns.Add("link");

            DataTable tblC = new DataTable();
            tblC.Columns.Add("description");
            tblC.Columns.Add("link");

            DataTable tblD = new DataTable();
            tblD.Columns.Add("description");
            tblD.Columns.Add("link");

            DataTable tblE = new DataTable();
            tblE.Columns.Add("description");
            tblE.Columns.Add("link");

            DataTable tblF = new DataTable();
            tblF.Columns.Add("description");
            tblF.Columns.Add("link");

            DataTable tblG = new DataTable();
            tblG.Columns.Add("description");
            tblG.Columns.Add("link");

            DataTable tblH = new DataTable();
            tblH.Columns.Add("description");
            tblH.Columns.Add("link");

            DataTable tblI = new DataTable();
            tblI.Columns.Add("description");
            tblI.Columns.Add("link");

            DataTable tblJ = new DataTable();
            tblJ.Columns.Add("description");
            tblJ.Columns.Add("link");

            DataTable tblK = new DataTable();
            tblK.Columns.Add("description");
            tblK.Columns.Add("link");

            DataTable tblL = new DataTable();
            tblL.Columns.Add("description");
            tblL.Columns.Add("link");

            DataTable tblM = new DataTable();
            tblM.Columns.Add("description");
            tblM.Columns.Add("link");

            DataTable tblN = new DataTable();
            tblN.Columns.Add("description");
            tblN.Columns.Add("link");

            DataTable tblO = new DataTable();
            tblO.Columns.Add("description");
            tblO.Columns.Add("link");

            DataTable tblP = new DataTable();
            tblP.Columns.Add("description");
            tblP.Columns.Add("link");

            DataTable tblQ = new DataTable();
            tblQ.Columns.Add("description");
            tblQ.Columns.Add("link");

            DataTable tblR = new DataTable();
            tblR.Columns.Add("description");
            tblR.Columns.Add("link");

            DataTable tblS = new DataTable();
            tblS.Columns.Add("description");
            tblS.Columns.Add("link");

            DataTable tblT = new DataTable();
            tblT.Columns.Add("description");
            tblT.Columns.Add("link");

            DataTable tblU = new DataTable();
            tblU.Columns.Add("description");
            tblU.Columns.Add("link");

            DataTable tblV = new DataTable();
            tblV.Columns.Add("description");
            tblV.Columns.Add("link");

            DataTable tblW = new DataTable();
            tblW.Columns.Add("description");
            tblW.Columns.Add("link");

            DataTable tblX = new DataTable();
            tblX.Columns.Add("description");
            tblX.Columns.Add("link");

            DataTable tblY = new DataTable();
            tblY.Columns.Add("description");
            tblY.Columns.Add("link");

            DataTable tblZ = new DataTable();
            tblZ.Columns.Add("description");
            tblZ.Columns.Add("link");

            foreach (DataRow row in table.Rows)
            {
                if (row["description"].ToString().ToUpper().StartsWith("A") || row["description"].ToString().ToUpper().StartsWith("3"))
                {
                    tblA.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("B"))
                {
                    tblB.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("C"))
                {
                    tblC.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("D"))
                {
                    tblD.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("E"))
                {
                    tblE.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("F"))
                {
                    tblF.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("G"))
                {
                    tblG.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("H"))
                {
                    tblH.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("I"))
                {
                    tblI.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("J"))
                {
                    tblJ.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("K"))
                {
                    tblK.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("L"))
                {
                    tblL.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("M"))
                {
                    tblM.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("N"))
                {
                    tblN.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("O"))
                {
                    tblO.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("P"))
                {
                    tblP.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("Q"))
                {
                    tblQ.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("R"))
                {
                    tblR.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("S"))
                {
                    tblS.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("T"))
                {
                    tblT.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("U"))
                {
                    tblU.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("V"))
                {
                    tblV.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("W"))
                {
                    tblW.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("X"))
                {
                    tblX.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("Y"))
                {
                    tblY.Rows.Add(row["description"], row["link"]);
                }
                else if (row["description"].ToString().ToUpper().StartsWith("Z"))
                {
                    tblZ.Rows.Add(row["description"], row["link"]);
                }
            }

            RptA.DataSource= tblA;
            RptA.DataBind();
            RptB.DataSource= tblB;
            RptB.DataBind();
            RptC.DataSource = tblC;
            RptC.DataBind();
            RptD.DataSource = tblD;
            RptD.DataBind();
            RptE.DataSource = tblE;
            RptE.DataBind();
            RptF.DataSource = tblF;
            RptF.DataBind();
            RptG.DataSource = tblG;
            RptG.DataBind();
            RptH.DataSource = tblH;
            RptH.DataBind();
            RptI.DataSource = tblI;
            RptI.DataBind();
            RptJ.DataSource = tblJ;
            RptJ.DataBind();
            RptK.DataSource = tblK;
            RptK.DataBind();
            RptL.DataSource = tblL;
            RptL.DataBind();
            RptM.DataSource = tblM;
            RptM.DataBind();
            RptN.DataSource = tblN;
            RptN.DataBind();
            RptO.DataSource = tblO;
            RptO.DataBind();
            RptP.DataSource = tblP;
            RptP.DataBind();
            RptQ.DataSource = tblQ;
            RptQ.DataBind();
            RptR.DataSource = tblR;
            RptR.DataBind();
            RptS.DataSource = tblS;
            RptS.DataBind();
            RptT.DataSource = tblT;
            RptT.DataBind();
            RptU.DataSource = tblU;
            RptU.DataBind();
            RptV.DataSource = tblV;
            RptV.DataBind();
            RptW.DataSource = tblW;
            RptW.DataBind();
            RptX.DataSource = tblX;
            RptX.DataBind();
            RptY.DataSource = tblY;
            RptY.DataBind();
            RptZ.DataSource = tblZ;
            RptZ.DataBind();

            //string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            //foreach (string letter in letters)
            //{
            //    DataTable table = Sql.GetVerticalsForSearchResultsByLetter(Type, letter, session);

            //    if (table != null && table.Rows.Count > 0)
            //    {
            //        switch (letter)
            //        {
            //            case "A":
            //                RptA.DataSource = table;
            //                RptA.DataBind();
            //                break;

            //            case "B":
            //                RptB.DataSource = table;
            //                RptB.DataBind();
            //                break;

            //            case "C":
            //                RptC.DataSource = table;
            //                RptC.DataBind();
            //                break;

            //            case "D":
            //                RptD.DataSource = table;
            //                RptD.DataBind();
            //                break;

            //            case "E":
            //                RptE.DataSource = table;
            //                RptE.DataBind();
            //                break;

            //            case "F":
            //                RptF.DataSource = table;
            //                RptF.DataBind();
            //                break;

            //            case "G":
            //                RptG.DataSource = table;
            //                RptG.DataBind();
            //                break;

            //            case "H":
            //                RptH.DataSource = table;
            //                RptH.DataBind();
            //                break;

            //            case "I":
            //                RptI.DataSource = table;
            //                RptI.DataBind();
            //                break;

            //            case "J":
            //                RptJ.DataSource = table;
            //                RptJ.DataBind();
            //                break;

            //            case "K":
            //                RptK.DataSource = table;
            //                RptK.DataBind();
            //                break;

            //            case "L":
            //                RptL.DataSource = table;
            //                RptL.DataBind();
            //                break;

            //            case "M":
            //                RptM.DataSource = table;
            //                RptM.DataBind();
            //                break;

            //            case "N":
            //                RptN.DataSource = table;
            //                RptN.DataBind();
            //                break;

            //            case "O":
            //                RptO.DataSource = table;
            //                RptO.DataBind();
            //                break;

            //            case "P":
            //                RptP.DataSource = table;
            //                RptP.DataBind();
            //                break;

            //            case "Q":
            //                RptQ.DataSource = table;
            //                RptQ.DataBind();
            //                break;

            //            case "R":
            //                RptR.DataSource = table;
            //                RptR.DataBind();
            //                break;

            //            case "S":
            //                RptS.DataSource = table;
            //                RptS.DataBind();
            //                break;

            //            case "T":
            //                RptT.DataSource = table;
            //                RptT.DataBind();
            //                break;

            //            case "U":
            //                RptU.DataSource = table;
            //                RptU.DataBind();
            //                break;

            //            case "V":
            //                RptV.DataSource = table;
            //                RptV.DataBind();
            //                break;

            //            case "W":
            //                RptW.DataSource = table;
            //                RptW.DataBind();
            //                break;

            //            case "X":
            //                RptX.DataSource = table;
            //                RptX.DataBind();
            //                break;

            //            case "Y":
            //                RptY.DataSource = table;
            //                RptY.DataBind();
            //                break;

            //            case "Z":
            //                RptZ.DataSource = table;
            //                RptZ.DataBind();
            //                break;
            //        }
            //    }
            //    else
            //    {

            //    }
            //}
        }

        private void LoadTechnologies()
        {
            DataTable tA = new DataTable();
            tA.Columns.Add("description");
            tA.Columns.Add("link");

            DataTable tB = new DataTable();
            tB.Columns.Add("description");
            tB.Columns.Add("link");

            DataTable tC = new DataTable();
            tC.Columns.Add("description");
            tC.Columns.Add("link");

            DataTable tD = new DataTable();
            tD.Columns.Add("description");
            tD.Columns.Add("link");

            DataTable tE = new DataTable();
            tE.Columns.Add("description");
            tE.Columns.Add("link");

            DataTable tF = new DataTable();
            tF.Columns.Add("description");
            tF.Columns.Add("link");

            DataTable tG = new DataTable();
            tG.Columns.Add("description");
            tG.Columns.Add("link");

            DataTable tH = new DataTable();
            tH.Columns.Add("description");
            tH.Columns.Add("link");

            DataTable tI = new DataTable();
            tI.Columns.Add("description");
            tI.Columns.Add("link");

            DataTable tJ = new DataTable();
            tJ.Columns.Add("description");
            tJ.Columns.Add("link");

            DataTable tK = new DataTable();
            tK.Columns.Add("description");
            tK.Columns.Add("link");

            DataTable tL = new DataTable();
            tL.Columns.Add("description");
            tL.Columns.Add("link");

            DataTable tM = new DataTable();
            tM.Columns.Add("description");
            tM.Columns.Add("link");

            DataTable tN = new DataTable();
            tN.Columns.Add("description");
            tN.Columns.Add("link");

            DataTable tO = new DataTable();
            tO.Columns.Add("description");
            tO.Columns.Add("link");

            DataTable tP = new DataTable();
            tP.Columns.Add("description");
            tP.Columns.Add("link");

            DataTable tQ = new DataTable();
            tQ.Columns.Add("description");
            tQ.Columns.Add("link");

            DataTable tR = new DataTable();
            tR.Columns.Add("description");
            tR.Columns.Add("link");

            DataTable tS = new DataTable();
            tS.Columns.Add("description");
            tS.Columns.Add("link");

            DataTable tT = new DataTable();
            tT.Columns.Add("description");
            tT.Columns.Add("link");

            DataTable tU = new DataTable();
            tU.Columns.Add("description");
            tU.Columns.Add("link");

            DataTable tV = new DataTable();
            tV.Columns.Add("description");
            tV.Columns.Add("link");

            DataTable tW = new DataTable();
            tW.Columns.Add("description");
            tW.Columns.Add("link");

            DataTable tX = new DataTable();
            tX.Columns.Add("description");
            tX.Columns.Add("link");

            DataTable tY = new DataTable();
            tY.Columns.Add("description");
            tY.Columns.Add("link");

            DataTable tZ = new DataTable();
            tZ.Columns.Add("description");
            tZ.Columns.Add("link");

            List<string> technologies = Lib.SearchResults.SearchLib.GetChannelPartnersSearchTechnologies();

            if (technologies.Count > 0)
            {
                for (int i = 1; i < technologies.Count; i++)
                {
                    if (technologies[i].ToUpper().StartsWith("A"))
                        tA.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("B"))
                        tB.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("C"))
                        tC.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("D"))
                        tD.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("E"))
                        tE.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("F"))
                        tF.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("G"))
                        tG.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("H"))
                        tH.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("I"))
                        tI.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("J"))
                        tJ.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("K"))
                        tK.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("L"))
                        tL.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("M"))
                        tM.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("N"))
                        tN.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("O"))
                        tO.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("P"))
                        tP.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("Q"))
                        tQ.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("R"))
                        tR.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("S"))
                        tS.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("T"))
                        tT.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("U"))
                        tU.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("V"))
                        tV.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("W"))
                        tW.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("X"))
                        tX.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("Y"))
                        tY.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());
                    else if (technologies[i].ToUpper().StartsWith("Z"))
                        tZ.Rows.Add(technologies[i].TrimEnd(' '), "/profile/channel-partners/" + technologies[i].TrimEnd(' ').Replace(" ", "_").Replace("&", "and").Trim().ToLower());

                }

                RptTechA.DataSource = tA;
                RptTechA.DataBind();

                RptTechB.DataSource = tB;
                RptTechB.DataBind();

                RptTechC.DataSource = tC;
                RptTechC.DataBind();

                RptTechD.DataSource = tD;
                RptTechD.DataBind();

                RptTechE.DataSource = tE;
                RptTechE.DataBind();

                RptTechF.DataSource = tF;
                RptTechF.DataBind();

                RptTechG.DataSource = tG;
                RptTechG.DataBind();

                RptTechH.DataSource = tH;
                RptTechH.DataBind();

                RptTechI.DataSource = tI;
                RptTechI.DataBind();

                RptTechJ.DataSource = tJ;
                RptTechJ.DataBind();

                RptTechK.DataSource = tK;
                RptTechK.DataBind();

                RptTechL.DataSource = tL;
                RptTechL.DataBind();

                RptTechM.DataSource = tM;
                RptTechM.DataBind();

                RptTechN.DataSource = tN;
                RptTechN.DataBind();

                RptTechO.DataSource = tO;
                RptTechO.DataBind();

                RptTechP.DataSource = tP;
                RptTechP.DataBind();

                RptTechQ.DataSource = tQ;
                RptTechQ.DataBind();

                RptTechR.DataSource = tR;
                RptTechR.DataBind();

                RptTechS.DataSource = tS;
                RptTechS.DataBind();

                RptTechT.DataSource = tT;
                RptTechT.DataBind();

                RptTechU.DataSource = tU;
                RptTechU.DataBind();

                RptTechV.DataSource = tV;
                RptTechV.DataBind();

                RptTechW.DataSource = tW;
                RptTechW.DataBind();

                RptTechX.DataSource = tX;
                RptTechX.DataBind();

                RptTechY.DataSource = tY;
                RptTechY.DataBind();

                RptTechZ.DataSource = tZ;
                RptTechZ.DataBind();
            }
            else
            {

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