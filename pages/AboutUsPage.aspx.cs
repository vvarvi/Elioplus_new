using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Windows.Documents.Spreadsheet.Expressions.Functions;

namespace WdS.ElioPlus.pages
{
    public partial class AboutUsPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();

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

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();

            aSignUp.Visible = vSession.User == null;
        }

        private void SetLinks()
        {
            aSignUp.HRef = ControlLoader.SignUp;
            aPartnerRecruitment.HRef = ControlLoader.ChannelPartnerRecruitment;
            aPRMSoftware.HRef = ControlLoader.PrmSoftware;
            aIntentSignals.HRef = ControlLoader.IntentSignals;
            aReferralSoftware.HRef = ControlLoader.ReferalSoftware;
            aMAMarketplace.HRef = ControlLoader.MAMarketplace;
        }

        private void UpdateStrings()
        {
            try
            {
                //LblAboutUsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "15")).Text;
                //LblAboutContent1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "16")).Text;
                //LblAboutContent2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "17")).Text;
                //LblAboutContent3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "23")).Text;
                //LblAboutContent4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "24")).Text;

                ////LblMeetTeam.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "19")).Text;
                //LblVagelis.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "5")).Text;
                //LblCTO.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "18")).Text;
                //LblChristos.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "11")).Text;
                //LblMCS.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "21")).Text;
                //LblIlias.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "8")).Text;
                //LblCEO.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "22")).Text;
                //ImgCTO.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "alternate", "1")).Text;
                //ImgMCS.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "alternate", "3")).Text;
                //ImgCEO.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "alternate", "4")).Text;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}