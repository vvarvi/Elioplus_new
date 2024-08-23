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
    public partial class DealsPendingGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private bool _hideGridColumns { get; set; }
        public bool ShowGridColumns
        {
            get
            {
                return (ViewState["ShowGridColumns"] != null) ? Convert.ToBoolean(ViewState["ShowGridColumns"].ToString()) : true;
            }
            set
            {
                ViewState["ShowGridColumns"] = value;
            }
        }

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
                        UcConnectionsMessageAlert.Visible = false;
                        tdCountry.Visible = tdClientEmail.Visible = tdClientWebsite.Visible = tdIsActive.Visible = tdResultStstus.Visible = ShowGridColumns;
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

                List<ElioRegistrationDealsIJUsers> deals = Sql.GetUserDealsIJUsersByActiveStatus(vSession.User, (int)DealActivityStatus.NotConfirmed, CollaborationId, TbxPending.Text, session);

                if (deals.Count > 0)
                {
                    RdgDealsPending.Visible = true;
                    UcConnectionsMessageAlert.Visible = false;

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
                    table.Columns.Add("sysdate");
                    table.Columns.Add("status");
                    table.Columns.Add("deal_result");
                    table.Columns.Add("is_active");
                    table.Columns.Add("is_new");

                    foreach (ElioRegistrationDealsIJUsers deal in deals)
                    {
                        //ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //if (partner != null)
                        table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, deal.PartnerName, deal.CompanyName, deal.PartnerLocation, deal.Email, deal.Website, deal.CreatedDate.ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                        //else
                        //    table.Rows.Add(deal.Id, deal.CollaborationVendorResellerId, deal.VendorId, deal.ResellerId, "", deal.CompanyName, "", deal.Email, deal.Website, deal.CreatedDate.ToShortDateString(), deal.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), deal.DealResult, deal.IsActive, deal.IsNew);
                    }

                    RdgDealsPending.DataSource = table;
                    RdgDealsPending.DataBind();
                }
                else
                {
                    RdgDealsPending.Visible = false;
                    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, "There are no Deals", MessageTypes.Info, true, true, false, false, false);
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

        # endregion

        #region Grids

        protected void RdgDealsPending_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        HtmlControl tdbCountry = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbCountry");
                        HtmlControl tdbClientEmail = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbClientEmail");
                        HtmlControl tdbWebsite = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbWebsite");
                        HtmlControl tdbActiveStatus = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbActiveStatus");
                        HtmlControl tdbResultStatus = (HtmlControl)ControlFinder.FindControlRecursive(item, "tdbResultStatus");
                        
                        if (tdbCountry != null && tdbClientEmail != null && tdbWebsite != null && tdbActiveStatus != null && tdbResultStatus != null)
                            tdbCountry.Visible = tdbClientEmail.Visible = tdbWebsite.Visible = tdbActiveStatus.Visible = tdbResultStatus.Visible = ShowGridColumns;

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

                                HtmlAnchor aWebsite = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aWebsite");
                                Label lblWebsite = (Label)ControlFinder.FindControlRecursive(item, "LblWebsite");

                                lblWebsite.Text = aWebsite.HRef = row["website"].ToString();  //company.WebSite;
                                aWebsite.Target = "_blank";
                                                                
                                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                                aCompanyName.Target = "_blank";
                                lblCompanyNameContent.Text = company.CompanyName;

                                HtmlGenericControl divNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divNotification");
                                HtmlGenericControl divClientNotification = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divClientNotification");
                                HiddenField hdnIsNew = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnIsNew");

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    if (Convert.ToInt32(hdnIsNew.Value) == 1 && row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString())
                                    {
                                        divNotification.Visible = true;
                                    }
                                    else
                                        divNotification.Visible = false;
                                }
                                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                {
                                    divNotification.Visible = divClientNotification.Visible = Convert.ToInt32(hdnIsNew.Value) == 1;
                                }
                                else
                                    divNotification.Visible = divClientNotification.Visible = false;

                                Label lblActiveStatus = (Label)ControlFinder.FindControlRecursive(item, "LblActiveStatus");
                                HtmlGenericControl spanActiveStatus = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanActiveStatus");

                                if (row["is_active"].ToString() == Convert.ToInt32(DealActivityStatus.Approved).ToString())
                                {
                                    spanActiveStatus.Attributes["class"] = "label label-lg label-light-success label-inline";
                                    lblActiveStatus.Text = DealActivityStatus.Approved.ToString();
                                    lblActiveStatus.ToolTip = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "The specific Deal has been" + lblActiveStatus.Text + " from you." : "The specific Deal has " + lblActiveStatus.Text + " from your Vendor.";
                                    //lblActiveStatus.CssClass = "btn btn-cta btn-cta-secondary green";
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
                                    spanResultStatus.Attributes["class"] = "label label-lg label-danger label-inline";
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

        protected void RdgDealsPending_OnNeedDataSource(object sender, EventArgs args)
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

        protected void BtnSearchPending_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
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