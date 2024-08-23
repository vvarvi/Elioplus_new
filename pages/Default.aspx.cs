using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.pages
{
    public partial class Default : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();
        protected bool showSelfServiceOffer = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                {
                    GlobalMethods.ClearCriteriaSession(vSession, false);
                    
                    FixPage();
                    UpdateStrings();
                    SetLinks();
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
            aHomeAddAccount.Visible = aSignUp.Visible = vSession.User == null;
        }

        private void UpdateStrings()
        {
            try
            {
                Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "1")).Text;
                Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "2")).Text;
                Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "3")).Text.Replace("{count}", (Sql.GetPublicOpportunitiesSum(session) + Sql.GetAllPublicVendorsAndResellers(session)).ToString());
                Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "4")).Text;
                Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "5")).Text;
                Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "6")).Text;
                Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "7")).Text;
                Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "8")).Text;
                Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "9")).Text;
                Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "10")).Text;
                Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "11")).Text;
                Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "12")).Text;
                Label13.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "13")).Text;
                Label14.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "14")).Text;
                Label15.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "15")).Text;
                Label16.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "16")).Text;
                Label17.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "17")).Text;
                Label18.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "18")).Text;
                Label19.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "19")).Text;
                Label20.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "20")).Text;
                Label21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "21")).Text;
                Label22.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "22")).Text;
                Label23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "23")).Text;
                Label24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "24")).Text;
                Label25.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "25")).Text;
                Label26.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "26")).Text;
                Label27.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "27")).Text;
                Label28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "28")).Text;
                Label29.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "29")).Text;
                //Label30.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "30")).Text;
                Label31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "31")).Text;
                Label32.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "32")).Text;
                Label33.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "33")).Text;
                Label34.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "34")).Text;
                Label35.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "35")).Text;
                Label36.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "36")).Text;
                Label37.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "37")).Text;
                Label38.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "38")).Text;
                Label39.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "39")).Text;
                Label40.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "40")).Text;
                Label41.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "41")).Text;
                Label42.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "42")).Text;
                Label43.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "43")).Text;
                Label44.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "44")).Text;
                Label45.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "45")).Text;
                Label46.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "46")).Text;
                Label47.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "47")).Text;
                Label48.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "48")).Text;
                Label49.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "49")).Text;
                Label50.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "50")).Text;
                Label51.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "51")).Text;
                Label52.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "52")).Text;
                Label53.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "53")).Text;
                Label54.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "54")).Text;
                Label55.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "55")).Text;
                Label56.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "56")).Text;
                Label57.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "57")).Text;
                Label58.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "58")).Text;
                Label59.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "59")).Text;
                Label60.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "60")).Text;
                Label61.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "61")).Text;
                Label62.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "62")).Text;
                Label63.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "63")).Text;
                Label64.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "64")).Text;
                Label65.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "65")).Text;
                Label66.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "66")).Text;
                Label67.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "67")).Text;
                Label68.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "68")).Text;
                Label69.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "69")).Text;
                Label70.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "70")).Text;
                Label71.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "71")).Text;
                Label72.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "72")).Text;
                Label73.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "73")).Text;
                Label74.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "74")).Text;
                Label75.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "75")).Text;


                LblTotalPublicVendors.Text = Sql.GetAllPublicVendors(session).ToString();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void SetLinks()
        {
            aHomeAddAccount.HRef = ControlLoader.SignUp;
            aBrowseCompanies.HRef = aViewAllCompanies.HRef = ControlLoader.Search;
            aPartnerPortal.HRef = aPartnerPortalB.HRef = ControlLoader.PartnerPortal;
            aSearchChannelPartners.HRef = ControlLoader.SearchForChannelPartners;
            aSignUp.HRef = ControlLoader.SignUp;

            try
            {
                ElioUsers userSap = Sql.GetUserById(18297, session);
                if (userSap != null)
                    aSap.HRef = ControlLoader.Profile(userSap);
                ElioUsers userFreshworks = Sql.GetUserById(26011, session);
                if (userFreshworks != null)
                    aFreshworks.HRef = ControlLoader.Profile(userFreshworks);
                ElioUsers userDeloitte = Sql.GetUserById(112873, session);
                if (userDeloitte != null)
                    aDeloitte.HRef = ControlLoader.Profile(userDeloitte);
                ElioUsers userIRI = Sql.GetUserById(2226, session);
                if (userIRI != null)
                    aIRI.HRef = ControlLoader.Profile(userIRI);
                ElioUsers userTotara = Sql.GetUserById(18192, session);
                if (userTotara != null)
                    aTotara.HRef = ControlLoader.Profile(userTotara);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Buttons

        #endregion
    }
}