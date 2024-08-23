using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Controls.Benefits
{
    public partial class ResellersBenefits : System.Web.UI.UserControl
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
            LabelResBen24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "1")).Text;
            LblResellerBen1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "2")).Text;
            LblResellerBen2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "3")).Text;
            LabelResBen28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "4")).Text;
            LabelResBen21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "5")).Text;
            LabelResBen7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "6")).Text;
            LabelRessBen31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "7")).Text;
            LabelResBen23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "8")).Text;
            LabelResBen8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "resellersbenefits", "label", "9")).Text;
        }

        #endregion
    }
}