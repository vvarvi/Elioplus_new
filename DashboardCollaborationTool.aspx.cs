using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationTool : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    //ElioUsers user = null;
                    //bool isError = false;
                    //string errorPage = string.Empty;
                    //string key = string.Empty;

                    //RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    //if (isError)
                    //{
                    //    Response.Redirect(vSession.Page = errorPage, false);
                    //    return;
                    //}

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

        # region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
         
                UpdateStrings();
                SetLinks();
            }

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                LblRenewalHead.Visible = LblRenewal.Visible = true;
                LblRenewalHead.Text = "Renewal date: ";

                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                if (packet != null)
                {
                    LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                }

                try
                {
                    LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                }
                catch (Exception)
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = false;

                    Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                }
            }
            else
            {
                LblRenewalHead.Visible = LblRenewal.Visible = false;
                LblPricingPlan.Text = "You are currently on a free plan";
            }

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            LblElioplusDashboard.Text = "";

            LblDashboard.Text = "Dashboard";
            //LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "label", "17")).Text;
            //LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Collaboration";
            LblDashSubTitle.Text = "";
        }

        private void SetLinks()
        {            
            //aBtnGoFull.HRef = ControlLoader.FullRegistrationPage;
        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        # endregion

        #region Grids

        protected void RdgMyInvitations_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    imgBtnEdit.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "CompanyItems")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);

                    if (user != null)
                    {

                    }
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

        protected void RdgMyInvitations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCollaborationUsersInvitations> invitations = new List<ElioCollaborationUsersInvitations>();      //Sql.GetAllUserInvitations(vSession.User.Id, session);

                if (invitations.Count > 0)
                {
                    RdgMyInvitations.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("inv_subject");
                    table.Columns.Add("inv_content");
                    table.Columns.Add("date_created");

                    foreach (ElioCollaborationUsersInvitations invitation in invitations)
                    {
                        table.Rows.Add(invitation.Id, invitation.InvSubject, invitation.InvContent, invitation.DateCreated.ToString("dd/MM/yyyy"));
                    }

                    RdgMyInvitations.DataSource = table;
                }
                else
                {
                    RdgMyInvitations.Visible = false;

                    string alert = "You have no invitations";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
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

        #endregion

        # region buttons

        protected void ImgBtnEditCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;
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

        # endregion
    }
}