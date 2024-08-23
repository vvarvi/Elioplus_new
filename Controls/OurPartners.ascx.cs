using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus.Controls
{
    public partial class OurPartners : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LblOurPartners.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "label", "51")).Text;

                ImgPartner1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "20")).Text;
                ImgPartner2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "21")).Text;
                ImgPartner3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "22")).Text;
                ImgPartner4.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "23")).Text;
                ImgPartner5.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "24")).Text;
                ImgPartner6.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "default", "literal", "25")).Text;
            }
        }
    }
}