using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Collections;
using System.Web.Script.Serialization;
using System.IO;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI.Entities;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.Services.StripeAPI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnerToPartnerPage : System.Web.UI.Page
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

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }

                    FixPage();
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

        private void FixPage()
        {
            if (!IsPostBack)
            {
                aAddNewDeal.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();

                UpdateStrings();
                SetLinks();

                GlobalDBMethods.FixUserOpenDealsStatus(vSession.User, session);
            }
        }

        private void SetLinks()
        {
            aAddNewDeal.HRef = ControlLoader.Dashboard(vSession.User, "partner-to-partner-add-edit");
        }

        private void UpdateStrings()
        {

        }

        # endregion

        #region Buttons

        protected void BtnSearchOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {                    
                    RdgDealsOpen.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchMyOpen_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgDealsMyOpen.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchPast_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgPastDeals.Rebind();
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
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
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
        }

        # endregion

        #region Grids

        protected void RdgDealsOpen_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");

                    if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Open)
                    {
                        lblStatus.Text = DealStatus.Open.ToString();
                        lblStatus.CssClass = "label label-lg label-light-success label-inline";
                    }
                    else if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Closed)
                    {
                        lblStatus.Text = DealStatus.Closed.ToString();
                        lblStatus.CssClass = "label label-lg label-light-danger label-inline";
                    }

                    string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                    Session[sessionId] = item["id"].Text.ToString();

                    HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                    aEdit.HRef = ControlLoader.Dashboard(vSession.User, "partner-to-partner-add-edit") + "?p2pViewID=" + sessionId;

                    //Label lblEdit = (Label)ControlFinder.FindControlRecursive(item, "LblEdit");
                    //lblEdit.Text = (Convert.ToInt32(item["reseller_id"].Text) == vSession.User.Id) ? "View/Edit" : "View";
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

        protected void RdgDealsOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //List<ElioPartnerToPartnerDeals> p2pDealsOthers = Sql.GetOtherResellersP2pDealsByCases(vSession.User.Id, (int)DealStatus.Open, RtbxCompanyNameOpen.Text, session);       //GetOtherResellersP2pDeals(vSession.User.Id, (int)DealStatus.Open, session);

                    DataTable table = Sql.GetOtherResellersP2pDealsByCasesTbl(vSession.User.Id, (int)DealStatus.Open, RtbxCompanyNameOpen.Text, session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        RdgDealsOpen.Visible = true;
                        UcOpenMessage.Visible = false;

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("reseller_id");
                        //table.Columns.Add("opportunity_name");
                        //table.Columns.Add("deal_value");
                        //table.Columns.Add("reseller_name");
                        //table.Columns.Add("country_id");
                        //table.Columns.Add("country");
                        //table.Columns.Add("date_created");
                        //table.Columns.Add("last_updated");
                        //table.Columns.Add("status");
                        //table.Columns.Add("is_active");
                        //table.Columns.Add("is_public");

                        //foreach (ElioPartnerToPartnerDeals deal in p2pDealsOthers)
                        //{
                        //    ElioCountries country = Sql.GetCountryById(deal.CountryId, session);
                        //    if (country != null)
                        //    {
                        //        ElioUsers partner = Sql.GetUserById(deal.ResellerId, session);
                        //        if (partner != null)
                        //            table.Rows.Add(deal.Id, deal.ResellerId, deal.OpportunityName, deal.DealValue, partner.CompanyName, deal.CountryId, country.CountryName, deal.DateCreated, deal.LastUpdated, deal.Status, deal.IsActive, deal.IsPublic);
                        //        else
                        //            table.Rows.Add(deal.Id, deal.ResellerId, deal.OpportunityName, deal.DealValue, "", deal.CountryId, country.CountryName, deal.DateCreated, deal.LastUpdated, deal.Status, deal.IsActive, deal.IsPublic);
                        //    }
                        //}

                        RdgDealsOpen.DataSource = table;
                    }
                    else
                    {
                        RdgDealsOpen.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcOpenMessage, (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Open.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Open.ToString() + " Deals!", MessageTypes.Info, true, true, false, true, false);
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

        protected void RdgDealsMyOpen_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");

                    if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Open)
                    {
                        lblStatus.Text = DealStatus.Open.ToString();
                        lblStatus.CssClass = "label label-lg label-light-success label-inline";
                    }
                    else if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Closed)
                    {
                        lblStatus.Text = DealStatus.Closed.ToString();
                        lblStatus.CssClass = "label label-lg label-light-danger label-inline";
                    }

                    string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                    Session[sessionId] = item["id"].Text.ToString();

                    HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                    aEdit.HRef = ControlLoader.Dashboard(vSession.User, "partner-to-partner-add-edit") + "?p2pViewID=" + sessionId;

                    //Label lblEdit = (Label)ControlFinder.FindControlRecursive(item, "LblEdit");
                    //lblEdit.Text = (Convert.ToInt32(item["reseller_id"].Text) == vSession.User.Id) ? "View/Edit" : "View";
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

        protected void RdgDealsMyOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = Sql.GetUserPartnerToPartnerDealsIJCountriesTbl(vSession.User.Id, (int)DealStatus.Open, RtbxCompanyNameMyOpen.Text, session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        RdgDealsMyOpen.Visible = true;
                        UcMyOpenMessage.Visible = false;

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("reseller_id");
                        //table.Columns.Add("opportunity_name");
                        //table.Columns.Add("deal_value");
                        //table.Columns.Add("country_id");
                        //table.Columns.Add("country");
                        //table.Columns.Add("date_created");
                        //table.Columns.Add("last_updated");
                        //table.Columns.Add("status");
                        //table.Columns.Add("is_active");
                        //table.Columns.Add("is_public");

                        //foreach (ElioPartnerToPartnerDeals deal in p2pDeals)
                        //{
                        //    ElioCountries country = Sql.GetCountryById(deal.CountryId, session);
                        //    if (country != null)
                        //    {
                        //        table.Rows.Add(deal.Id, deal.ResellerId, deal.OpportunityName, deal.DealValue, deal.CountryId, country.CountryName, deal.DateCreated, deal.LastUpdated, deal.Status, deal.IsActive, deal.IsPublic);
                        //    }
                        //}

                        RdgDealsMyOpen.DataSource = table;
                    }
                    else
                    {
                        RdgDealsMyOpen.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcMyOpenMessage, (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Open.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Open.ToString() + " Deals!", MessageTypes.Info, true, true, false, true, false);
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

        protected void RdgPastDeals_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                    Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");
                    HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                    Label lblPartnerName = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerName");

                    if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Open)
                    {
                        lblStatus.Text = DealStatus.Open.ToString();
                        lblStatus.CssClass = "label label-lg label-light-success label-inline";
                        aCompanyLogo.Visible = false;
                        imgCompanyLogo.Visible = false;
                        lblPartnerName.Text = "-";
                    }
                    else if (Convert.ToInt32(item["status"].Text) == (int)DealStatus.Closed)
                    {
                        lblStatus.Text = DealStatus.Closed.ToString();
                        lblStatus.CssClass = "label label-lg label-light-danger label-inline";
                        aCompanyLogo.Visible = false;
                        imgCompanyLogo.Visible = false;

                        if (item["partner_user_id"].Text != "0")
                        {
                            ElioUsers partner = Sql.GetUserById(Convert.ToInt32(item["partner_user_id"].Text), session);
                            if (partner != null)
                            {
                                lblPartnerName.Text = partner.CompanyName;
                                imgCompanyLogo.ImageUrl = partner.CompanyLogo;

                                if (partner.UserApplicationType == Convert.ToInt32(UserApplicationType.ThirdParty))
                                    aCompanyLogo.HRef = ControlLoader.PersonProfile(partner);
                                else if (partner.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus))
                                    aCompanyLogo.HRef = ControlLoader.Profile(partner);

                                aCompanyLogo.Target = "_blank";
                                aCompanyLogo.Visible = true;
                                imgCompanyLogo.Visible = true;
                            }
                        }
                    }

                    string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, Convert.ToInt32(item["id"].Text.ToString()), session);
                    Session[sessionId] = item["id"].Text.ToString();

                    HtmlAnchor aEdit = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEdit");
                    aEdit.HRef = ControlLoader.Dashboard(vSession.User, "partner-to-partner-add-edit") + "?p2pViewID=" + sessionId;

                    //Label lblEdit = (Label)ControlFinder.FindControlRecursive(item, "LblEdit");
                    //lblEdit.Text = "View";
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

        protected void RdgPastDeals_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //List<ElioPartnerToPartnerDeals> p2pDeals = Sql.GetUserPartnerToPartnerDeals(vSession.User.Id, (int)DealStatus.Closed, session);

                    DataTable table = Sql.GetUserPartnerToPartnerDealsIJCountriesTbl(vSession.User.Id, (int)DealStatus.Closed, RtbxCompanyNamePast.Text, session);

                    if (table != null && table.Rows.Count > 0)
                    {
                        RdgPastDeals.Visible = true;
                        UcPastMessage.Visible = false;

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("reseller_id");
                        //table.Columns.Add("partner_user_id");
                        //table.Columns.Add("opportunity_name");
                        //table.Columns.Add("deal_value");
                        //table.Columns.Add("country_id");
                        //table.Columns.Add("country");
                        //table.Columns.Add("date_created");
                        //table.Columns.Add("last_updated");
                        //table.Columns.Add("status");
                        //table.Columns.Add("is_active");
                        //table.Columns.Add("is_public");

                        //foreach (ElioPartnerToPartnerDeals deal in p2pDeals)
                        //{
                        //    ElioCountries country = Sql.GetCountryById(deal.CountryId, session);
                        //    if (country != null)
                        //    {
                        //        table.Rows.Add(deal.Id, deal.ResellerId, deal.PartnerUserId, deal.OpportunityName, deal.DealValue, deal.CountryId, country.CountryName, deal.DateCreated, deal.LastUpdated, deal.Status, deal.IsActive, deal.IsPublic);
                        //    }
                        //}

                        RdgPastDeals.DataSource = table;
                    }
                    else
                    {
                        RdgPastDeals.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcPastMessage, (vSession.User.CompanyType == Types.Vendors.ToString()) ? "There are no " + DealStatus.Closed.ToString() + " Deals, from this partner!" : "You have no " + DealStatus.Closed.ToString() + " Deals!", MessageTypes.Info, true, true, false, true, false);
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

        #endregion

        #region Tabs

        protected void aOpenP2pDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aOpenP2pDeals.Attributes["class"] = "nav-link active";
                tab_1_1.Attributes["class"] = "tab-pane fade show active";
                tab_1_1.Visible = true;
                RdgDealsOpen.Rebind();

                aMyOpenP2pDeals.Attributes["class"] = aClosedDeals.Attributes["class"] = "nav-link";
                tab_1_2.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                tab_1_2.Visible = tab_1_3.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aMyOpenP2pDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aMyOpenP2pDeals.Attributes["class"] = "nav-link active";
                tab_1_2.Attributes["class"] = "tab-pane fade show active";
                tab_1_2.Visible = true;
                RdgDealsMyOpen.Rebind();

                aOpenP2pDeals.Attributes["class"] = aClosedDeals.Attributes["class"] = "nav-link";
                tab_1_1.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                tab_1_1.Visible = tab_1_3.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aClosedDeals_ServerClick(object sender, EventArgs e)
        {
            try
            {
                aClosedDeals.Attributes["class"] = "nav-link active";
                tab_1_3.Attributes["class"] = "tab-pane fade show active";
                tab_1_3.Visible = true;
                RdgPastDeals.Rebind();

                aMyOpenP2pDeals.Attributes["class"] = aOpenP2pDeals.Attributes["class"] = "nav-link";
                tab_1_1.Attributes["class"] = tab_1_2.Attributes["class"] = "tab-pane fade";
                tab_1_1.Visible = tab_1_2.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}