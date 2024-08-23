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
using System.Data;
using System.Linq;

namespace WdS.ElioPlus.pages
{
    public partial class SearchForVendorsPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        public string Type { get; private set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    string[] path = HttpContext.Current.Request.Url.AbsolutePath.Split('/').ToArray();
                    if (path.Length >= 2)
                    {
                        Type = Types.Vendors.ToString();
                        FixePage();
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

        private void FixePage()
        {
            UpdateStrings();
            LoadVerticals();

            HtmlControl metaDescription = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaDescription");
            if (metaDescription != null)
                metaDescription.Attributes["content"] = "Find the best SaaS partner programs. Compare between different programs and check partner reviews.";

            HtmlControl metaKeywords = (HtmlControl)ControlFinder.FindControlRecursive(Page, "metaKeywords");
            if (metaKeywords != null)
                metaKeywords.Attributes["content"] = "saas partner program, best saas partner programs";

            aHome.HRef = ControlLoader.Default();
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

            RptA.DataSource = tblA;
            RptA.DataBind();
            RptB.DataSource = tblB;
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
        }

        private void LoadVerticalsOLD()
        {
            string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            foreach (string letter in letters)
            {
                DataTable table = Sql.GetVerticalsForSearchResultsByLetter(Type, letter, session);

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

        #endregion

        #region Grids

        #endregion

        #region Buttons

        #endregion

        #region Combo

        #endregion
    }
}