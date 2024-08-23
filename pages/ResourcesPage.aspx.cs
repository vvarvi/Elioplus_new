using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.EMMA;

namespace WdS.ElioPlus.pages
{
    public partial class ResourcesPage : System.Web.UI.Page
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
            finally
            {
                session.CloseConnection();
            }
        }

        #region Methods

        private void FixPage()
        {
            aSignUp.Visible = vSession.User == null;

            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            aSignUp.HRef = ControlLoader.SignUp;
            aPrmSystem.HRef = ControlLoader.ResourcesPartnerManagementSystemPage;
            aManageChannelPartners.HRef = ControlLoader.ResourcesManageChannelPartnersPage;
            aRecruiutmentProccess.HRef = ControlLoader.ResourcesPartnerRecruitmentPtoccessPage;
            aPartneringExamlpes.HRef = ControlLoader.ResourcesPartneringExamplesPage;
            aPartnerManagement.HRef = ControlLoader.ResourcesPartnerManagementPage;
            //aP2PDeals.HRef = ControlLoader.ResourcesPartnerManagementPage;
            //aCollaborationTool.HRef = ControlLoader.ResourcesPartnerManagementPage;

            aComptia.HRef = ControlLoader.ResourcesNetworksComptiaPage;
            aSpiceworks.HRef = ControlLoader.ResourcesNetworksSpiceworksPage;
            aForrester.HRef = "https://www.forrester.com/blogs/channel-software-tech-stack-2021";
            aForrester.Target = "_blank";
            aMedium.HRef = "https://medium.com/@elioplus";
            aMedium.Target = "_blank";
            aGrowann.HRef = "https://www.growann.com/best/partner-management-software-prm";
            aGrowann.Target = "_blank";

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

        protected void BtnSearchGoPremium_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}