using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;

namespace WdS.ElioPlus
{
    public partial class _405_NotAuthenticatedPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ImgPgNotFound.AlternateText = "Elioplus authentication page";
                Lbl404.Text = "405";
                LblProblem.Text = "Houston, we have a problem.";
                LblNotFound.Text = "Actually, you have no authentication for the page you are looking for.";
                LblNotFound2.Text = "Contact with your master account and get permission first!";
                LblReturnHome.Text = "Return Home";
                aReturnHome.HRef = HttpContext.Current.Request.Url.AbsolutePath.Contains("dashboard") && vSession.User != null ? ControlLoader.Dashboard(vSession.User, "home") : HttpContext.Current.Request.Url.AbsolutePath.Contains("community") ? ControlLoader.CommunityPosts : ControlLoader.Default();                
            }
            catch (Exception ex)
            {                
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }
    }
}