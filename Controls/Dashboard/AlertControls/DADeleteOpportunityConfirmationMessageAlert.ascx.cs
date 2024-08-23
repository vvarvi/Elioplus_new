using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.StripePayment;
using WdS.ElioPlus.Lib.DBQueries;
using Telerik.Web.UI;

namespace WdS.ElioPlus.Controls.Dashboard.AlertControls
{
    public partial class DADeleteOpportunityConfirmationMessageAlert : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    FixPage();
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void FixPage()
        {
            divGeneralSuccess.Visible = false;
            divGeneralFailure.Visible = false;
        }

        #region Buttons

        protected void BtnProccedMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["opportunityViewID"] != null)
                    {
                        int oppId = Convert.ToInt32(Session[Request.QueryString["opportunityViewID"]]);
                        if (oppId > 0)
                        {
                            Sql.DeleteAllUserOpportunitiesNotes(oppId, session);

                            Sql.DeleteUserOpportunity(oppId, session);

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities-view"), false);
                        }
                        else
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblCancelFailure.Text = "Something has gone wrong! Please try again later or contact with us.";
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "opportunities-view"), false);
                    }
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

        protected void BtnCancelMessage_OnClick(object sender, EventArgs args)
        {
            try
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}