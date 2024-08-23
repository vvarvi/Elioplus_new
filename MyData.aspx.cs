using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class MyData : System.Web.UI.Page
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

                    GlobalMethods.ClearCriteriaSession(vSession, false);
                }

                ControlLoader.LoadElioControls(Page, PhElioplusNutshell, ControlLoader.ElioplusNutshell);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }            
        }

        #region Methods

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {
            try
            {
                LtrMyDataTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "profiledata", "label", "8")).Text;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}