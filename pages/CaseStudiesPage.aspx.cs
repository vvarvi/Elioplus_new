using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;

namespace WdS.ElioPlus.pages
{
    public partial class CaseStudiesPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    FixPage();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();

            aSignUp.Visible = aSignUpB.Visible = vSession.User == null;
        }

        private void SetLinks()
        {
            aRequestDemo.HRef = "https://calendly.com/elioplus";
            aSignUp.HRef = aSignUpB.HRef = ControlLoader.SignUp;
        }

        private void UpdateStrings()
        {
            
        }

        #endregion
    }
}