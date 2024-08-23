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
    public partial class TermsConditionsPage : System.Web.UI.Page
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

        private void SetLinks()
        {
            aPrivacy.HRef = ControlLoader.Privacy;
            aMyData.HRef = ControlLoader.MyData;
            aFaq.HRef = ControlLoader.Faq;
        }

        private void UpdateStrings()
        {
            try
            {
                //LtrTermsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "terms", "literal", "1")).Text;
                //LblAboutUsTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "15")).Text;
                //LblAboutContent1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "16")).Text;
                //LblAboutContent2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "17")).Text;
                //LblMeetTeam.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "19")).Text;
                //LblVagelis.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "5")).Text;
                //LblCTO.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "18")).Text;
                //LblDimitris.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "13")).Text;
                //LblCPO.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "20")).Text;
                //LblChristos.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "11")).Text;
                //LblMCS.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "21")).Text;
                //LblIlias.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "8")).Text;
                //LblCEO.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "label", "22")).Text;
                //ImgCTO.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "alternate", "1")).Text;
                //ImgCPO.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "about", "alternate", "2")).Text;
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