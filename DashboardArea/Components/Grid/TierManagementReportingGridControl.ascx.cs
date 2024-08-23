using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Dashboard.Components.Grid
{
    public partial class TierManagementReportingGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int ReportId
        {
            get
            {
                return (ViewState["ReportId"] != null) ? Convert.ToInt32(ViewState["ReportId"].ToString()) : -1;
            }
            set
            {
                ViewState["ReportId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (Request.QueryString["partnerViewID"] != null)
                    {
                        int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                        if (partnerId > 0)
                        {
                            FixPage();
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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

        #region methods

        private void FixPage()
        {

        }

        public void Bind(int partnerId)
        {
            try
            {
                session.OpenConnection();

                DataTable table = Sql.GetPartnerTierManagementReportingTbl(partnerId, false, session);
                if (table.Rows.Count > 0)
                {
                    UcMessageAlertGrid.Visible = false;
                    RdgReporting.Visible = true;
                    RdgReporting.DataSource = table;
                    RdgReporting.DataBind();
                }
                else
                {
                    RdgReporting.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertGrid, "There are no Reports", MessageTypes.Info, true, true, false, false, false);
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

        # endregion

        #region Grids

        protected void RdgReporting_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (vSession.User != null)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgReporting_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (Request.QueryString["partnerViewID"] != null)
                    {
                        int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                        if (partnerId > 0)
                        {
                            Bind(partnerId);
                        }
                    }
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessageAlertGrid, "Reports can not be loaded", MessageTypes.Info, true, true, true, true, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void aEdit_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (Request.QueryString["partnerViewID"] != null)
                    {
                        int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                        if (partnerId > 0)
                        {
                            UcPopUpConfirmationMessageAlert.Visible = false;

                            HtmlAnchor imgBtn = (HtmlAnchor)sender;
                            if (imgBtn != null)
                            {
                                RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                                if (item != null)
                                {
                                    HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                                    if (hdnId != null)
                                    {
                                        ReportId = Convert.ToInt32(hdnId.Value);

                                        Page p = (Page)ControlFinder.FindControlBackWards(this, "");
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (Request.QueryString["partnerViewID"] != null)
                    {
                        int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                        if (partnerId > 0)
                        {
                            UcPopUpConfirmationMessageAlert.Visible = false;

                            HtmlAnchor imgBtn = (HtmlAnchor)sender;
                            if (imgBtn != null)
                            {
                                RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                                if (item != null)
                                {
                                    HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnId");

                                    if (hdnId != null)
                                    {
                                        ReportId = Convert.ToInt32(hdnId.Value);

                                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmPopUp();", true);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcPopUpConfirmationMessageAlert.Visible = UcMessageAlertGrid.Visible = false;

                    if (Request.QueryString["partnerViewID"] != null)
                    {
                        int partnerId = Convert.ToInt32(Session[Request.QueryString["partnerViewID"]]);
                        if (partnerId > 0 && ReportId > 0)
                        {
                            Sql.DeleteUserTierManagementReportById(ReportId, partnerId, session);

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmPopUp();", true);

                            Bind(partnerId);

                            ReportId = -1;

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertGrid, "Report was deleted successfully.", MessageTypes.Success, true, true, true, true, false);
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
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
    }
}