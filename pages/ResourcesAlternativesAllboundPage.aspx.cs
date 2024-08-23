using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;

namespace WdS.ElioPlus.pages
{
    public partial class ResourcesAlternativesAllboundPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    FixPage();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixPage()
        {
            aGetStarted.Visible = vSession.User == null;

            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            aGetStarted.HRef = ControlLoader.SignUpPrm;

            aAllbound.HRef = ControlLoader.ResourcesAlternativesAllboundPage;
            aImpactCompany.HRef = ControlLoader.ResourcesAlternativesImpactCompanyPage;
            aImpartner.HRef = ControlLoader.ResourcesAlternativesImpartnerPage;
            aMindmatrix.HRef = ControlLoader.ResourcesAlternativesMindmatrixPage;
            aModelN.HRef = ControlLoader.ResourcesAlternativesModelNPage;
            aPartnerize.HRef = ControlLoader.ResourcesAlternativesPartnerizePage;
            aSalesforceCommunities.HRef = ControlLoader.ResourcesAlternativesSalesforceCommunitiesPage;
            aZiftSolutions.HRef = ControlLoader.ResourcesAlternativesZiftSolutionsPage;
            aPartnerStack.HRef = ControlLoader.ResourcesAlternativesPartnerStackPage;
            aMyPrm.HRef = ControlLoader.ResourcesAlternativesMyPrmPage;
            aChanneltivity.HRef = ControlLoader.ResourcesAlternativesChanneltivity;
        }

        private void UpdateStrings()
        {
            try
            {

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