using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;

namespace WdS.ElioPlus
{
    public partial class HowItWorks : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlGenericControl li = (HtmlGenericControl)ControlFinder.FindControlBackWards(Master, "liMainMenuProducts");
                li.Attributes["class"] = "active nav-item";

                if (!IsPostBack)
                {
                    UpdateStrings();
                    SetLinks();

                    GlobalMethods.ClearCriteriaSession(vSession, false);
                }

                FixPaymentBtns();
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

        private void FixPaymentBtns()
        {
            bool showBtn = false;
            bool showModal = false;

            bool allowPayment = GlobalDBMethods.AllowPaymentProccess(vSession.User, false, ref showBtn, ref showModal, session);

            if (allowPayment)
            {
                BtnGetElioNow.Visible = false; // showBtn;
                aGetElioNow.Visible = true; // showModal;
            }
            else
            {
                BtnGetElioNow.Visible = false;
                aGetElioNow.Visible = false;
            }
        }

        private void SetLinks()
        {
            aLblVendorBenefit4ContentLearnMore.HRef = aLblResellerBenefit3ContentLearnMore.HRef = ControlLoader.HowItWorksCollaborationPage;
        }

        private void UpdateStrings()
        {
            try
            {
                ImgVendorsFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "1")).Text;
                LblVendorsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "29")).Text;
                ImgResellersFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "2")).Text;
                LblResellersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "30")).Text;
                ImgApiDevelopersFeatures.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "3")).Text;
                LblApiDevelopersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "31")).Text;
                LtrVendorsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "literal", "1")).Text;
                LblVendorBenefit0Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "52")).Text;
                LblVendorBenefit0Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "53")).Text;
                LblVendorBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "32")).Text;
                LblVendorBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "33")).Text;
                ImgVendorBenefit1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "4")).Text;
                ImgVendorBenefit2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "5")).Text;
                ImgVendorBenefit3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "6")).Text;
                LblVendorBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "34")).Text;
                LblVendorBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "35")).Text;
                LblVendorBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "36")).Text;
                LblVendorBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "37")).Text;
                LblVendorBenefit4Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "50")).Text;
                LblVendorBenefit4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "51")).Text;
                LblVendorBenefit4ContentLearnMore.Text = LblResellerBenefit3ContentLearnMore.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "56")).Text;
                LtrResellersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "literal", "2")).Text;
                LblResellerBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "38")).Text;
                LblResellerBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "39")).Text;
                LblResellerBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "40")).Text;
                LblResellerBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "41")).Text;
                LblResellerBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "42")).Text;
                LblResellerBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "43")).Text;
                LblResellerBenefit4Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "54")).Text;
                LblResellerBenefit4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "55")).Text;
                LtrApiDevelopersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "literal", "3")).Text;
                LblApiDeveloperBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "44")).Text;
                LblApiDeveloperBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "45")).Text;
                LblApiDeveloperBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "46")).Text;
                LblApiDeveloperBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "47")).Text;
                LblApiDeveloperBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "48")).Text;
                LblApiDeveloperBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "49")).Text;
                ImgResellerBenefit1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "7")).Text;
                ImgResellerBenefit2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "8")).Text;
                ImgResellerBenefit3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "alternate", "9")).Text;
                LblHowItWorks.Text = "How it works";
                LblFeature1Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "11")).Text;
                LblFeature1Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "29")).Text;
                LblFeature1Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "13")).Text;
                LblFeature1Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "30")).Text;
                LblFeature1Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "12")).Text;
                LblFeature1Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "31")).Text;
                LblFeature2Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "32")).Text;
                LblFeature2Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "33")).Text;
                LblFeature2Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "34")).Text;
                LblFeature2Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "35")).Text;
                LblFeature2Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "36")).Text;
                LblFeature2Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "37")).Text;
                LblFeature3Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "32")).Text;
                LblFeature3Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "38")).Text;
                LblFeature3Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "39")).Text;
                LblFeature3Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "40")).Text;
                LblFeature3Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "41")).Text;
                LblFeature3Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "42")).Text;
                LblFeature4Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "43")).Text;
                LblFeature4Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "44")).Text;
                LblFeature4Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "45")).Text;
                LblFeature4Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "46")).Text;
                LblFeature4Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "47")).Text;
                LblFeature4Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "48")).Text;
                LblVendorsBenefits.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "70")).Text;
                LblResellersBenefits.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "71")).Text;
                LblDevelopersBenefits.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworks", "label", "72")).Text;
                LblMoreThan.Text = "More than ";
                LblUsersNumber.Text = "120.000";
                LblUsersAction.Text = " companies are using Elioplus";
                LblNoWait.Text = "What are you waiting for?";

                if (vSession.User == null)
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Add your Company Free";
                    aGetElioNow.HRef = ControlLoader.SignUp;
                }
                else if (vSession.User!=null && vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Start a new partnership!";
                    aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User, "find-new-partners");                   
                }
                else
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Upgrade Now";
                    //aGetElioNow.HRef = ControlLoader.Pricing;
                }
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