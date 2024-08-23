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
    public partial class _404 : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ImgPgNotFound.AlternateText = "Elioplus page not found image";
                Lbl404.Text = "404";
                LblProblem.Text = "Houston, we have a problem.";
                LblNotFound.Text = "Actually, the page you are looking for does not exist.";
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