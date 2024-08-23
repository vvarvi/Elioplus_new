using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Controls;

namespace WdS.ElioPlus.pages
{
    public partial class MyDataPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    UpdateStrings();
                    SetLinks();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void SetLinks()
        {
            aTerms.HRef = ControlLoader.Terms;
            aPrivacy.HRef = ControlLoader.Privacy;
            aFaq.HRef = ControlLoader.Faq;
        }

        private void UpdateStrings()
        {
            try
            {
                //LtrMyDataTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "8")).Text;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}