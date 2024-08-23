using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus
{
    public partial class LandingPage_Cloud : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ControlLoader.LoadElioControls(Page, PhLoginControls, ControlLoader.SignUpControl);
                //aMainMenuSignUp.HRef = "https://www.elioplus.com/free-sign-up";
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnJoinNow_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect("https://www.elioplus.com/free-sign-up", false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}