using System;
using System.Linq;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus.Controls.TermsPrivacy
{
    public partial class MyData : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    UpDateStrings();
                    SetLinks();
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }
            }
        }

        #region Methods

        private void UpDateStrings()
        {
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "9")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "10")).Text;
        }

        private void SetLinks()
        {
            aSearch.HRef = ControlLoader.Search;
            aContactUs.HRef = ControlLoader.ContactUs;
        }

        #endregion
    }
}