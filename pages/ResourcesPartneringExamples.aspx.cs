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
    public partial class ResourcesPartneringExamples : System.Web.UI.Page
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
            aSignUp.Visible = aGetStarted.Visible = vSession.User == null;

            UpdateStrings();
            SetLinks();
        }

        private void SetLinks()
        {
            aSignUp.HRef = aGetStarted.HRef = ControlLoader.SignUpPrm;
            //aPartnerManagementSystem.HRef = ControlLoader.PrmSoftware;
        }

        private void UpdateStrings()
        {
            //LblCollBenefit0Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "2")).Text;
            //LblCollBenefit0Content2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "3")).Text;
            //LblCollBenefit0Content3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "4")).Text;

            //LblCollBenefit1Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "5")).Text;
            //LblCollBenefit1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "6")).Text;
            //LblCollBenefit2Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "7")).Text;
            //LblCollBenefit2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "8")).Text;
            //LblCollBenefit3Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "9")).Text;
            //LblCollBenefit3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "10")).Text;

            //LblCollBenefit4Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "11")).Text;
            //LblCollBenefit4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "12")).Text;
            //LblCollBenefit5Header.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "13")).Text;
            //LblCollBenefit5Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "14")).Text;
            //Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "15")).Text;
            //Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "16")).Text;

            //Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "17")).Text;
            //Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "18")).Text;
            //Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "19")).Text;
            //Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "20")).Text;

            //LblCollaborationVBenefits2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "partnermanagementsoftware", "label", "1")).Text;
        }

        #endregion

        #region Buttons

        #endregion
    }
}