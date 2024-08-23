using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Windows.Documents;

namespace WdS.ElioPlus.pages
{
    public partial class FaqPage : System.Web.UI.Page
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
            UpdateStrings();
            SetLinks();
        }

        private void UpdateStrings()
        {
            //LblFaqTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "1")).Text;
            //LblFaq1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "2")).Text;
            //LblFaq1Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "3")).Text;
            //LblFaq2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "4")).Text;
            //LblFaq2Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "5")).Text;
            //LblFaq12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "8")).Text;
            //LblFaq12Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "9")).Text;
            //LblFaq13.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "27")).Text;
            //LblFaq13Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "28")).Text;
            //LblFaq14.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "29")).Text;
            //LblFaq14Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "30")).Text;
            //LblFaq3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "6")).Text;
            //LblFaq3Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "7")).Text;
            //LblFaq5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "10")).Text;
            //LblFaq5Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "11")).Text;
            //LblFaqMatchTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "24")).Text;
            //LblFaq6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "12")).Text;
            //LblFaq6Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "13")).Text;
            //LblFaq7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "14")).Text;
            //LblFaq7Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "15")).Text;
            //LblFaq4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "31")).Text;
            //LblFaq4Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "32")).Text;
            //LblFaq8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "16")).Text;
            //LblFaq8Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "17")).Text;
            //LblFaq9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "18")).Text;
            //LblFaq9Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "19")).Text;
            //LblFaq10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "20")).Text;
            //LblFaq10Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "21")).Text;
            //LblFaq11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "22")).Text;
            //LblFaq11Content.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "23")).Text;

            //LblMoreQuestions.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "25")).Text;
            //LblGetInTouch.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "faq", "label", "26")).Text;
        }

        private void SetLinks()
        {
           
        }

        #endregion
    }
}