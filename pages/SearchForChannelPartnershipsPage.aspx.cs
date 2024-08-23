using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Linq;

namespace WdS.ElioPlus.pages
{
    public partial class SearchForChannelPartnershipsPage : System.Web.UI.Page
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

                            //UpdateStrings();
                            SetLinks();
                            LoadTechnologies();
                            LblTechnoCount.Text = Sql.GetRegistrationProductsCount(session).ToString();
                        }
                        else
                            Response.Redirect(ControlLoader.Default(), false);
                    }
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

        private void SetLinks()
        {
            aBCrHome.HRef = ControlLoader.SearchForChannelPartners;
            Label lbl = new Label();
            lbl.Text = "Channel-Partners" + " / ";
            aBCrHome.Controls.Add(lbl);
        }

        private void UpdateStrings()
        {

        }

        private void LoadTechnologies()
        {            
            string[] letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            foreach (string letter in letters)
            {
                DataTable table = Sql.GetRegistrationProductsTableByLetter(letter, session);

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

        #endregion

        #region Grids

        #endregion

        #region Buttons

        #endregion

        #region Combo

        #endregion
    }
}