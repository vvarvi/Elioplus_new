using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
    public partial class PartnerToPartnerDealsLandingPage : System.Web.UI.Page
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
            //aGetInTouch.HRef = ControlLoader.ContactUs;
            //aInfo2.HRef = ControlLoader.ResellersPage;
            //aInfo3.HRef = ControlLoader.Pricing;
            aGetStarted.HRef = aGetStartedNow.HRef = (vSession.User == null) ? ControlLoader.SignUp : ControlLoader.Pricing;
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
                LblGetStartedSubTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "70")).Text;
                LblCollBenefit0Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "60")).Text;
                LblCollBenefit0Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "61")).Text;
                LblCollBenefit0Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "71")).Text;
                LblCollBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "62")).Text;
                LblCollBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "63")).Text;
                LblCollBenefit11Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "73")).Text;
                LblCollBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "64")).Text;
                LblCollBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "65")).Text;
                LblCollBenefit22Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "80")).Text;
                LblCollBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "66")).Text;
                LblCollBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "67")).Text;
                LblCollBenefit33Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "81")).Text;
                LblCollBenefit4Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "68")).Text;
                LblCollBenefit4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "69")).Text;

                ImgCollBenefit0.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "alternate", "1")).Text;
                ImgCollBenefit1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "alternate", "2")).Text;
                ImgCollBenefit2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "alternate", "3")).Text;
                ImgCollBenefit3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "alternate", "4")).Text;
                ImgCollBenefit4.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "alternate", "5")).Text;

                LblHowItWorksCollaborationTool.Text = "How it works - Collaboration Tool";
                LblFeature2Title1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "32")).Text;
                LblFeature2Content1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "33")).Text;
                LblFeature2Title2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "34")).Text;
                LblFeature2Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "35")).Text;
                LblFeature2Title3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "36")).Text;
                LblFeature2Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "37")).Text;
                LblCollaborationVBenefits.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "57")).Text;
                LblCollaborationVendorsBenefitsCnt.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "58")).Text;
                LblCollaborationVendorsBenefitsCnt2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "72")).Text;
                LblCollaborationVBenefits2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "59")).Text;

                //LblMoreInfoTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "howitworkscollaboration", "label", "9")).Text;
                //LblInfo1Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "1")).Text;
                //LblInfo1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "2")).Text;
                //LblInfo2Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "3")).Text;
                //LblInfo2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "4")).Text;
                ////LblInfo2Link.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "5")).Text;
                //LblInfo3Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "5")).Text;
                //LblInfo3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "6")).Text;
                ////LblInfo3Link.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "8")).Text;
                //LblInfo4Title.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "7")).Text;
                //LblInfo4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "recruitmentquestions", "label", "8")).Text;

                LblJoinTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "74")).Text;
                LblDealsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "76")).Text;
                LblNewCustomersTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "78")).Text;

                LblJoinContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "75")).Text;
                LblDealsContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "77")).Text;
                LblNewCustomersContent.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partner2partnerdeals", "label", "79")).Text;

                //LblMoreQuestions.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "51")).Text;
                //LblGetInTouch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "pricing", "label", "52")).Text;
                LblGetStarted.Text = (vSession.User == null) ? "Free Sign Up" : (vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType) ? "Upgrade Now" : "Get Started Now";
                LblGetStartedNow.Text = (vSession.User == null) ? "Get Started Now" : (vSession.User.BillingType == (int)BillingTypePacket.FreemiumPacketType) ? "Upgrade Now" : "Get Started Now";
                LblMoreThan.Text = "More than ";
                LblUsersNumber.Text = "120.000";
                LblUsersAction.Text = " companies are using Elioplus";
                LblNoWait.Text = "What are you waiting for?";

                if (vSession.User == null)
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Add your Company Free";
                    aGetElioNow.HRef = ControlLoader.SignUp;
                }
                else if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Start a new partnership!";
                    //aGetElioNow.HRef = ControlLoader.Dashboard(vSession.User);                    
                }
                else
                {
                    LblGetElioNow.Text = BtnGetElioNow.Text = "Go Premium Now";
                    aGetElioNow.HRef = ControlLoader.Pricing;
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