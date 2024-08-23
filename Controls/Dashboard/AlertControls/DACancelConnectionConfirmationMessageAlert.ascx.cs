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
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Dashboard.AlertControls
{
    public partial class DACancelConnectionConfirmationMessageAlert : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private int _connectionId;
        public int ConnectionId
        {
            get
            {
                return this._connectionId;
            }
            set
            {
                this._connectionId = value;
            }
        }
       
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
            finally
            {
                session.CloseConnection();
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
                if (vSession.User != null)
                {
                    ElioUsersConnections connection = Sql.GetUserConnection(_connectionId, session);

                    if (connection != null)
                    {
                        DataLoader<ElioUsersConnections> loader = new DataLoader<ElioUsersConnections>(session);
                        connection.CanBeViewed = 0;
                        loader.Update(connection);

                        RadGrid rdgConnections = (RadGrid)ControlFinder.FindControlBackWards(this, "RdgConnections");
                        rdgConnections.Rebind();

                        divGeneralSuccess.Visible = true;
                        LblGeneralSuccess.Text = "Done! ";
                        LblCancelSuccess.Text = "Your connection was canceled successfully!";
                    }
                }
                else
                {
                    divGeneralFailure.Visible = true;
                    LblGeneralFailure.Text = "Error! ";
                    LblCancelFailure.Text = "Your connection could not be canceled. Please try again later or contact with us";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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