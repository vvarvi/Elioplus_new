using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Controls.Benefits
{
    public partial class DevelopersBenefits : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStrings();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {
            LabelDevelopers24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "1")).Text;
            LblDevelopersBen1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "2")).Text;
            LblDevelopersBen2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "3")).Text;
            LabelDevelopers28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "4")).Text;
            LabelDevelopers21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "5")).Text;
            LabelDevelopersBen7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "6")).Text;
            LabelDevelopersBen31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "7")).Text;
            LabelDevelopersBen23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "8")).Text;
            LlbDevelopers8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "developersbenefits", "label", "9")).Text;
        }

        #endregion
    }
}