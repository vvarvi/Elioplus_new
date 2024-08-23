using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;

namespace WdS.ElioPlus
{
    public partial class Benefits : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateStrings();

                if (vSession.LoadedBenefitsControl != string.Empty)
                {
                    ControlLoader.LoadElioControls(Page, PhBenefitsContent, vSession.LoadedBenefitsControl);

                    ControlLoader.LoadElioControls(Page, PhFeatureCompanies, ControlLoader.FeatureCompanies);
                }
                else
                {
                    Response.Redirect(ControlLoader.HowItWorks, false);
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
            if (vSession.LoadedBenefitsControl == ControlLoader.VendorsBenefits)
            {
                Lbl2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "benefits", "label", "1")).Text;
            }
            else if (vSession.LoadedBenefitsControl == ControlLoader.ResellersBenefits)
            {
                Lbl2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "benefits", "label", "2")).Text;
            }
            else
            {
                Lbl2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "benefits", "label", "3")).Text;
            }
            
        }

        #endregion
    }
}