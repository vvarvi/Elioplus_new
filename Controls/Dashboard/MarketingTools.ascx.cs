using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class MarketingTools : System.Web.UI.UserControl
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
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "1")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "2")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "3")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "4")).Text;
            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "5")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "6")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "7")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "8")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "9")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "10")).Text;
            Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "11")).Text;
            Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "12")).Text;
            Label13.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "13")).Text;
            Label14.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "14")).Text;
            Label15.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "15")).Text;
            Label16.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "16")).Text;
            Label17.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "17")).Text;
            Label18.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "18")).Text;
            Label19.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "19")).Text;
            Label20.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "20")).Text;
            Label21.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "21")).Text;
            Label22.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "22")).Text;
            Label23.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "23")).Text;
            Label24.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "24")).Text;

            Label25.Text = Label27.Text = Label28.Text = Label29.Text = 
            Label30.Text = Label31.Text = Label32.Text = Label33.Text = 
            Label34.Text = Label35.Text = Label36.Text = Label26.Text = 
                           Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;            
            
            //Label26.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label27.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label28.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label29.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label30.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label31.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label32.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label33.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label34.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label35.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;
            //Label36.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "56")).Text;

            Image1.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "1")).Text;
            Image1.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "1")).Text;
            Image2.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "3")).Text;
            Image2.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "3")).Text;
            Image9.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "5")).Text;
            Image9.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "5")).Text;
            Image10.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "7")).Text;
            Image10.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "7")).Text;
            Image11.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "9")).Text;
            Image11.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "9")).Text;
            Image12.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "11")).Text;
            Image12.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "11")).Text;
            Image13.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "13")).Text;
            Image13.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "13")).Text;
            Image14.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "15")).Text;
            Image14.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "15")).Text;
            Image15.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "17")).Text;
            Image15.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "17")).Text;
            Image16.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "19")).Text;
            Image16.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "19")).Text;
            Image3.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "21")).Text;
            Image3.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "21")).Text;
            Image4.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "23")).Text;
            Image4.AlternateText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "marketingtools", "label", "23")).Text;
        }

        #endregion

        #region Buttons

        #endregion
    }
}