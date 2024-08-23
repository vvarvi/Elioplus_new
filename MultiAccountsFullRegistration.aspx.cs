using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.ImagesHelper;
using Telerik.Web.UI;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.IO;

namespace WdS.ElioPlus
{
    public partial class MultiAccountsFullRegistration : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                        PageTitle();

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                    }

                    ControlLoader.LoadElioControls(Page, PhFullRegistration, ControlLoader.MultiAccountsFullRegistration);
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
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

        #region Methods

        private void PageTitle()
        {
            string metaDescription = "";
            string metaKeywords = "";

            PgTitle.InnerText = GlobalMethods.SetPageTitle(HttpContext.Current.Request.Url.AbsolutePath, vSession.Lang, vSession.ElioCompanyDetailsView, out metaDescription, out metaKeywords, session);

            //MetaDescription = metaDescription;
            //MetaKeywords = metaKeywords;
        }

        private void UpdateStrings()
        {

        }

        #endregion
    }
}