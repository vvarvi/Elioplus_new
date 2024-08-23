using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class ThankYouPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User == null)
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
                else
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        SetLinks();
                        GlobalMethods.ClearCriteriaSession(vSession, false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {
            PgTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "6")).Text;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divVendorsArea.Visible = true;
                divChannelPartnersArea.Visible = false;
                aContactUs.Visible = false;

                LblSuccessRegistrationContent.Text = "You can access your account to manage potential partnerships and grow your partner network";
                LblVendorsTitle.Text = "Launch you Partner Portal";
                LblContent.Text = "Launch your partner portal to manage your channel partners, automate the deal registration process and increase your partner sales.";
                LblContent2.Text = "<b>What’s included?</b> Partner directory, onboarding, deal registration, lead distribution, CRM integrations, analytics, partner locator and many more features.";
            }
            else
            {
                divVendorsArea.Visible = false;
                divChannelPartnersArea.Visible = true;
                aContactUs.Visible = true;
                aContactUs.HRef = ControlLoader.ContactUs;

                LblSuccessRegistrationContent.Text = "You can access your account to manage your partnerships and get new leads";
                LblVendorsTitle.Text = "Get access to leads and requests for proposals";
                LblContent.Text = "Discover new leads and get access to Requests for Proposals based on your location and product expertise. To learn more please ";
                LblContent2.Text = "";
            }

            LblSuccessRegister.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "1")).Text;
            LblFindNewPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "2")).Text;
            LblRecruitNewPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "7")).Text + " + " + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "3")).Text;
            //LblUsePrm.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "3")).Text;
            LblCopyright.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "8")).Text;
            LblCopyright2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "9")).Text;
            LblCopyrightElioplus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "label", "10")).Text;

            //BtnDashboard.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "thankyoupage", "button", "1")).Text;
        }

        private void SetLinks()
        {
            aBookDemo.HRef = "https://calendly.com/elioplus";
            aCopyRight.HRef = ControlLoader.Default();
        }

        #endregion

        #region Buttons

        protected void BtnDashboard_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect((vSession.User != null) ? ControlLoader.Dashboard(vSession.User, "home") : ControlLoader.Default(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                //Response.Redirect(ControlLoader.Default, false);
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}