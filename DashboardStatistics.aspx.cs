using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus
{
    public partial class DashboardStatistics : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();
               
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    session.OpenConnection();

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    
                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.PageDash404, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        # region methods

        private void FixPage()
        {
            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            PhStatistics.Controls.Clear();
            vSession.LoadedDashboardControl = ControlLoader.Statistics;
            ControlLoader.LoadElioControls(this, PhStatistics, ControlLoader.Statistics);
        }

        # endregion
    }
}