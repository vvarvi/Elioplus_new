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
    public partial class DealsOpenGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int CollaborationId
        {
            get
            {
                return (ViewState["CollaborationId"] != null) ? Convert.ToInt32(ViewState["CollaborationId"].ToString()) : -1;
            }
            set
            {
                ViewState["CollaborationId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (!IsPostBack)
                    {
                        LoadDealResultStatusOpen();
                    }
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

        #region methods

        public void Bind()
        {
            try
            {
                session.OpenConnection();

                //int vendorId = -1;
                //int resellerId = -1;
                //int collaborationId = -1;

                //if (DrpPartners.SelectedItem.Value != "0")
                //{
                //    vendorId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.User.Id : Convert.ToInt32(DrpPartners.SelectedItem.Value);
                //    resellerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(DrpPartners.SelectedItem.Value) : vSession.User.Id;
                //    collaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);
                //}

                List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByStatusAndActivity(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Open, (int)DealActivityStatus.Approved, "", CollaborationId, "", session);

                if (deals.Count > 0)
                {
                    RdgDealsOpen.Visible = true;
                    //divOpenNoResults.Visible = false;
                    //divSearch.Visible = true;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("collaboration_vendor_reseller_id");
                    table.Columns.Add("vendor_id");
                    table.Columns.Add("reseller_id");
                    table.Columns.Add("partner_name");
                    table.Columns.Add("company_name");
                    table.Columns.Add("partner_location");
                    table.Columns.Add("email");
                    table.Columns.Add("website");
                    table.Columns.Add("month_duration");
                    table.Columns.Add("status");
                    table.Columns.Add("deal_result");
                    table.Columns.Add("is_active");
                    table.Columns.Add("is_new");

                    foreach (ElioRegistrationDealsIJUsers deal in deals)
                    {
                        //ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //if (partner != null)
                        table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, deal.PartnerName, deal.CompanyName, deal.PartnerLocation, deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                        //else
                        //    table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, "", deal.CompanyName, "", deal.Email, deal.Website, deal.CreatedDate.AddMonths(deal.MonthDuration).ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                    }

                    UcConnectionsMessageAlert.Visible = false;
                    RdgDealsOpen.Visible = true;
                    RdgDealsOpen.DataSource = table;
                    RdgDealsOpen.DataBind();
                }
                else
                {
                    RdgDealsOpen.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, "There are no Deals", MessageTypes.Info, true, true, false, false, false);
                    //divOpenNoResults.Visible = true;
                    //divSearch.Visible = false;
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

        private void SetDealLinkUrl(RepeaterItem item, ElioUsers companyDeal)
        {
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;

                string url = "";

                string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(row["id"].ToString()), session);
                Session[sessionId] = row["id"].ToString();

                url = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;

                HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-view") + "?dealViewID=" + sessionId;
            }
        }

        private void LoadDealResultStatusOpen()
        {
            //DdlDealResultOpen.Items.Clear();

            //DropDownListItem item = new DropDownListItem();

            //item.Value = "0";
            //item.Text = "Deal's result";
            //DdlDealResultOpen.Items.Add(item);

            //item = new DropDownListItem();

            //item.Value = Convert.ToInt32(DealResultStatus.Pending).ToString();
            //item.Text = DealResultStatus.Pending.ToString();
            //DdlDealResultOpen.Items.Add(item);

            //item = new DropDownListItem();

            //item.Value = Convert.ToInt32(DealResultStatus.Won).ToString();
            //item.Text = DealResultStatus.Won.ToString();
            //DdlDealResultOpen.Items.Add(item);

            //item = new DropDownListItem();
            //item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            //item.Text = DealResultStatus.Lost.ToString();
            //DdlDealResultOpen.Items.Add(item);
        }

        # endregion

        #region Grids

        protected void RdgDealsOpen_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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
                            Label lblClientName = (Label)ControlFinder.FindControlRecursive(item, "LblClientName");
                            lblClientName.Text = row["company_name"].ToString();

                            ElioUsers company = Sql.GetUserById(Convert.ToInt32(row["reseller_id"].ToString()), session);

                            if (company != null)
                            {
                                Image ImgLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgLogo");
                                ImgLogo.ImageUrl = "../../.." + company.CompanyLogo;
                                ImgLogo.AlternateText = company.CompanyName + " logo";

                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                                Label lblCompanyNameContent = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyNameContent");
                                Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                                lblWebsite.Text = aWebsite.HRef = row["website"].ToString();     //company.WebSite;
                                aWebsite.Target = "_blank";

                                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                                aCompanyName.Target = "_blank";
                                lblCompanyNameContent.Text = company.CompanyName;

                                HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                                HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    if (Convert.ToInt32(row["is_new"].ToString()) == 1 && row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                                    {
                                        divNotification.Visible = true;
                                    }
                                    else
                                        divNotification.Visible = false;
                                }
                                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    divNotification.Visible = divClientNotification.Visible = Convert.ToInt32(row["is_new"].ToString()) == 1;
                                }
                                else
                                    divNotification.Visible = divClientNotification.Visible = false;

                                HtmlGenericControl spanNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationMsg");
                                HtmlGenericControl spanClientNotificationMsg = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanClientNotificationMsg");
                                HtmlGenericControl spanActiveStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanActiveStatus");

                                if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                                {
                                    spanNotificationMsg.Attributes["title"] = spanClientNotificationMsg.Attributes["title"] = "new approved deal";

                                    lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Rejected).ToString())
                                {
                                    lblActiveStatus.Text = DealActivityStatus.Rejected.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-warning label-inline";
                                }
                                else
                                {
                                    lblActiveStatus.Text = "Not Confirmed";
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                Label lblResultStatus = (Label)ControlFinder.FindControlRecursive(item, "LblResultStatus");
                                HtmlGenericControl spanResultStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanResultStatus");

                                if (row["deal_result"].ToString() == DealResultStatus.Won.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Won.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Lost.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Lost.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-danger label-inline";
                                }
                                else if (row["deal_result"].ToString() == DealResultStatus.Pending.ToString())
                                {
                                    lblResultStatus.Text = DealResultStatus.Pending.ToString();
                                    spanResultStatus.Attributes["class"] = "label label-lg label-light-primary label-inline";
                                }

                                SetDealLinkUrl(item, company);

                                ////string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                                ////Session[sessionId] = item["id"].Text.ToString();

                                ////HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                                ////aEdit.HRef = ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit") + "?dealViewID=" + sessionId;

                                ////HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
                                ////aMoreDetails.HRef = ControlLoader.PersonProfile(company);
                                ////aMoreDetails.Target = "_blank";

                                ////Label lblMoreDetails = (Label)ControlFinder.FindControlRecursive(item, "LblMoreDetails");
                                ////lblMoreDetails.Text = "more details";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgDealsOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    Bind();
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

        #region Buttons

        protected void BtnSearchOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    //if (DdlDealResultOpen.SelectedItem.Value != "0")
                    //    AdvancedSerch = DdlDealResultOpen.SelectedItem.Text;
                    //else
                    //    AdvancedSerch = "";

                    //RdgDealsPending.Rebind();
                    Bind();
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
    }
}