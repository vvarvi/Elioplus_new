using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus.Controls.Benefits
{
    public partial class VendorsBenefits : System.Web.UI.UserControl
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
            LabelBen24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "1")).Text;
            LblVendorBen1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "2")).Text;
            LblVendorBen2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "3")).Text;
            LabelBen28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "4")).Text;
            LabelBen21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "5")).Text;
            LabelBen3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "6")).Text;
            LabelBen31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "7")).Text;
            LabelBen23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "8")).Text;
            LabelBen5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "vendorsbenefits", "label", "9")).Text;
        }

        #endregion
    }
}