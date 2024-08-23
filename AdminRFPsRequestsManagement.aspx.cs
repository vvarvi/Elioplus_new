using System;
using System.Linq;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Data;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Libero.FusionCharts;
using WdS.ElioPlus.Lib.Enums;
using System.Web;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.StripePayment;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Management
{
    public partial class AdminRFPsRequestsManagement : Telerik.Web.UI.RadAjaxPage
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private string CookieName
        {
            get
            {
                return "lgn";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    UcMessageAlert.Visible = false;
                    UcMessageAlertApproved.Visible = false;
                    UcStripeMessageAlert.Visible = false;
                    UcMessageControlCriteria.Visible = false;

                    UpdateStrings();

                    //ShowChart();

                    if (!IsPostBack)
                    {
                        #region Load Data

                        //LoadCompanyTypes();
                        //LoadStatus();
                        LoadPublicStatus();
                        //LoadBillingType();
                        //LoadApplicationType();
                        //LoadCompanies();

                        vSession.SearchQueryString = "";
                        RcbxApproved.SelectedValue = "-2";
                        RdtLeadsPicker.SelectedDate = null;

                        #endregion
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

        #region Methods
        
        private void LoadTypes(GridDataItem item)
        {
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            rcbxCategory.Items.Clear();

            List<ElioUserTypes> types = Sql.GetUserAllTypes(session);
            foreach (ElioUserTypes type in types)
            {
                RadComboBoxItem rcbxitem = new RadComboBoxItem();

                rcbxitem.Value = type.Id.ToString();
                rcbxitem.Text = type.Description;

                rcbxCategory.Items.Add(rcbxitem);
            }
        }

        private void ShowChart()
        {
            DataTable dt = Sql.GetCompanyViewsByCompanyIdForChart(vSession.User.Id, session);

            dt.Columns.Add("datetime");
            foreach (DataRow row in dt.Rows)
            {
                row["datetime"] = string.Format("{0:dd/MM}", row["date"]);
            }
            LineChart lchart = new LineChart();
            lchart.Background.BgColor = "ffffff";
            lchart.Background.BgAlpha = 50;
            lchart.ChartTitles.Caption = "";
            lchart.Template = new Libero.FusionCharts.Template.OceanTemplate();
            lchart.DataSource = dt;
            lchart.DataTextField = "datetime";
            lchart.DataValueField = "views";
            lchart.Canvas2D.CanvasBgColor = "f25a23";
            lchart.NumberFormat.DecimalPrecision = 0;
        }

        private void UpdateStrings()
        {
            Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "2")).Text;

            Label5.Text = "Elioplus companies which have requested a quote";      //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "1")).Text;
            Label2.Text = "Public Status"; //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "9")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "23")).Text;
            Label4.Text = "Request Status";
            Label lblSearchText = (Label)ControlFinder.FindControlRecursive(RbtnSearch, "LblSearchText");
            lblSearchText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "11")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;
        }

        private void LoadPublicStatus()
        {
            RcbxIsPublic.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "-1";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "41")).Text;
            RcbxIsPublic.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();

            item1.Value = "1";
            item1.Text =Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "43")).Text;
            RcbxIsPublic.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();

            item2.Value = "0";
            item2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "44")).Text;
            RcbxIsPublic.Items.Add(item2);           
        }

        #endregion

        #region Grids

        protected void RdgElioUsersApprovedDemoRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aSuccess = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aSuccess");
                    
                    aSuccess.Title = item["is_confirmed"].Text == "1" ? "approved and confirmed from Clearbit" : "approved and not confirmed from Clearbit";
                    aSuccess.Attributes["class"] = item["is_confirmed"].Text == "1" ? "btn btn-circle btn-sm green" : "btn btn-circle btn-sm yellow";

                    HtmlAnchor aBtnConfirm = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnConfirm");

                    aBtnConfirm.Visible = item["is_confirmed"].Text == "0" ? true : false;                    
                    
                    //TextBox tbxMessage = (TextBox)ControlFinder.FindControlRecursive(item, "TbxMessage");
                    //tbxMessage.Text = item["message"].Text;

                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");
                    rttImgInfo.Text = item["message"].Text;
                    rttImgInfo.Title = "RFP user Message";

                    string products = "";

                    List<ElioSnitcherLeadsPageviews> pageviews = Sql.GetSnitcherLeadPageViewsByElioWebsiteLeadsId(Convert.ToInt32(item["id"].Text), session);
                    if (pageviews.Count > 0)
                    {
                        foreach (ElioSnitcherLeadsPageviews pageview in pageviews)
                        {
                            products += pageview.Product.Trim() + ", ";
                        }

                        item["product"].Text = (products.Length > 0) ? products.Substring(0, products.Length - 2) : "";
                    }

                    #endregion
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

        protected void RdgElioUsersApprovedDemoRequests_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //List<ElioUsersDemoRequestsIJUsers> demos = new List<ElioUsersDemoRequestsIJUsers>();
                DataTable table = null;

                if (vSession.SearchQueryString == "")
                    //demos = Sql.GetUsersDemoRequestsIJUsers(1, session);
                    table = Sql.GetRFPsRequestsTbl(1, (int)ApiLeadCategory.isRFQRequest, session);
                else
                {
                    //DataLoader<ElioUsersDemoRequestsIJUsers> loader = new DataLoader<ElioUsersDemoRequestsIJUsers>(session);
                    //demos = loader.Load(vSession.SearchQueryString);
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                //if (demos.Count > 0)
                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersApprovedDemoRequests.Visible = true;
                    UcMessageAlertApproved.Visible = false;

                    //DataTable table = new DataTable();

                    //table.Columns.Add("id");
                    //table.Columns.Add("request_for_user_id");
                    //table.Columns.Add("company_name");
                    //table.Columns.Add("company_email");
                    //table.Columns.Add("first_name");
                    //table.Columns.Add("last_name");
                    //table.Columns.Add("company_size");
                    //table.Columns.Add("sysdate");
                    //table.Columns.Add("is_approved");
                    //table.Columns.Add("date_approved");
                    //table.Columns.Add("demo_company_name");
                    //table.Columns.Add("demo_company_email");

                    //foreach (ElioUsersDemoRequestsIJUsers request in demos)
                    //{
                    //    table.Rows.Add(request.Id, request.RequestForUserId, request.CompanyName, request.CompanyEmail, request.FirstName, request.LastName,
                    //        request.CompanySize, request.Sysdate.ToString("dd/MM/yyyy"), request.IsApproved, request.DateApproved, request.DemoCompanyName, request.DemoCompanyEmail);
                    //}

                    RdgElioUsersApprovedDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersApprovedDemoRequests.Visible = false;

                    string alert = "There are no approved RFPs requests";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertApproved, alert, MessageTypes.Error, true, true, false, true, false);
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

        protected void RdgElioUsersDemoRequests_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    HtmlAnchor aBtnApprove = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnApprove");
                    HtmlAnchor aBtnReject = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnReject");
                    Label lblProduct = (Label)ControlFinder.FindControlRecursive(item, "LblProduct");
                    TextBox tbxProduct = (TextBox)ControlFinder.FindControlRecursive(item, "TbxProduct");

                    aBtnApprove.Visible = aBtnReject.Visible = item["is_approved"].Text == "0";

                    //TextBox tbxMessage = (TextBox)ControlFinder.FindControlRecursive(item, "TbxMessage");
                    //tbxMessage.Text = item["message"].Text;

                    Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                    RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");
                    rttImgInfo.Text = item["message"].Text;
                    rttImgInfo.Title = "RFP user Message";

                    string products = "";

                    List<ElioSnitcherLeadsPageviews> pageviews = Sql.GetSnitcherLeadPageViewsByElioWebsiteLeadsId(Convert.ToInt32(item["id"].Text), session);

                    if (pageviews.Count == 1)
                    {
                        lblProduct.Text = tbxProduct.Text = item["product"].Text = (pageviews[0].Product.Length > 0) ? pageviews[0].Product.Trim() : "";
                    }
                    else if (pageviews.Count > 1)
                    {
                        ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                        imgBtnEdit.Visible = false;

                        Image imgProductInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgProductInfo");
                        imgProductInfo.Visible = true;
                        RadToolTip rttImgProductInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgProductInfo");
                        rttImgProductInfo.Visible = true;

                        foreach (ElioSnitcherLeadsPageviews pageview in pageviews)
                        {
                            products += pageview.Product.Trim() + ", ";
                        }

                        lblProduct.Text = tbxProduct.Text = item["product"].Text = (products.Length > 0) ? products.Substring(0, products.Length - 2) : "";
                    }
                    else
                        lblProduct.Text = tbxProduct.Text = item["product"].Text = "";

                    #endregion
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

        protected void RdgElioUsersDemoRequests_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsersDemoRequestsIJUsers> demos = new List<ElioUsersDemoRequestsIJUsers>();
                DataTable table = null;

                if (vSession.SearchQueryString == "")
                {
                    //demos = Sql.GetUsersDemoRequestsIJUsers(0, session);
                    table = Sql.GetRFPsRequestsTbl(0, (int)ApiLeadCategory.isRFQRequest, session);
                }
                else
                {
                    //DataLoader<ElioUsersDemoRequestsIJUsers> loader = new DataLoader<ElioUsersDemoRequestsIJUsers>(session);
                    //demos = loader.Load(vSession.SearchQueryString);
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                //if (demos.Count > 0)
                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersDemoRequests.Visible = true;
                    UcMessageAlert.Visible = false;

                    //DataTable table = new DataTable();

                    //table.Columns.Add("id");
                    //table.Columns.Add("request_for_user_id");
                    //table.Columns.Add("company_name");
                    //table.Columns.Add("company_email");
                    //table.Columns.Add("first_name");
                    //table.Columns.Add("last_name");                    
                    //table.Columns.Add("company_size");
                    //table.Columns.Add("sysdate");
                    //table.Columns.Add("is_approved");
                    //table.Columns.Add("date_approved");
                    //table.Columns.Add("demo_company_name");
                    //table.Columns.Add("demo_company_email");

                    //foreach (ElioUsersDemoRequestsIJUsers request in demos)
                    //{
                    //    table.Rows.Add(request.Id, request.RequestForUserId, request.CompanyName, request.CompanyEmail, request.FirstName, request.LastName,
                    //        request.CompanySize, request.Sysdate.ToString("dd/MM/yyyy"), request.IsApproved, request.DateApproved, request.DemoCompanyName, request.DemoCompanyEmail);
                    //}

                    RdgElioUsersDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersDemoRequests.Visible = false;

                    string alert = "There are no RFPs requests";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false, true, false);
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

        protected void RbtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.SearchQueryString = string.Empty;

                vSession.SearchQueryString = @"SELECT r.[id]
                                                ,r.[lead_first_name]
	                                            ,r.[lead_last_name]
	                                            ,r.[lead_company_name]
	                                            ,r.[lead_company_email]
                                                ,r.[lead_country]
	                                            ,r.[lead_company_size]
                                                --,p.url as product
                                                ,r.[message]
	                                            ,r.[sysdate]
	                                            ,r.[is_approved]
                                                ,r.[is_confirmed]
                                        FROM Elio_snitcher_website_leads r 
                                        WHERE 1 = 1 and is_api_lead = 0 ";

                bool hasSelectedCriteria = false;

                if (RcbxIsPublic.SelectedValue != "-1")
                {
                    vSession.SearchQueryString += " AND r.is_public=" + RcbxIsPublic.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }

                if (RtbxCompanyName.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND r.lead_company_name LIKE '" + RtbxCompanyName.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }

                if (RtbxCompanyEmail.Text.Trim() != string.Empty)
                {
                    vSession.SearchQueryString += " AND r.lead_company_email LIKE '" + RtbxCompanyEmail.Text.Trim() + "%' ";
                    hasSelectedCriteria = true;
                }

                if (RcbxApproved.SelectedValue != "-2")
                {
                    vSession.SearchQueryString += " AND r.is_approved=" + RcbxApproved.SelectedValue + "";
                    hasSelectedCriteria = true;
                }

                vSession.SearchQueryString += " order by r.is_approved, r.sysdate desc, r.id";

                if (!hasSelectedCriteria)
                    vSession.SearchQueryString = string.Empty;

                RdgElioUsersDemoRequests.Rebind();
                RdgElioUsersApprovedDemoRequests.Rebind();
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

        protected void RbtnReset_OnClick(object sender, EventArgs args)
        {
            try
            {
                RcbxIsPublic.SelectedValue = "-1";
                RtbxCompanyName.Text = string.Empty;
                RtbxCompanyEmail.Text = string.Empty;
                RcbxApproved.SelectedValue = "-2";
                RdtLeadsPicker.SelectedDate = null;

                vSession.SearchQueryString = string.Empty;

                RdgElioUsersDemoRequests.Rebind();
                RdgElioUsersApprovedDemoRequests.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnApprove_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                int leadId = Convert.ToInt32(item["id"].Text);
                if (leadId > 0)
                {
                    ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadById(leadId, session);
                    if (lead != null)
                    {
                        lead.IsApproved = 1;
                        lead.LastUpdate = DateTime.Now;

                        DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);
                        loader.Update(lead);

                        string mailMessage = "";

                        try
                        {
                            EmailSenderLib.SendRFQApprovedRequestMessageNotificationEmail(ApiLeadCategory.isRFQRequest, lead.Id, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            mailMessage = " Email notification could not be sent. View Logs to find out the reason!";
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        bool foundClearbit = false;

                        try
                        {
                            foundClearbit = Lib.Services.EnrichmentAPI.ClearBit.GetRFPCompanyByEmail(lead, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        RdgElioUsersDemoRequests.Rebind();
                        RdgElioUsersApprovedDemoRequests.Rebind();

                        if (foundClearbit)
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "RFPs Request was successfully approved and got data from clearbit." + mailMessage, MessageTypes.Success, true, true, false, true, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "RFPs Request was successfully approved but not found on clearbit." + mailMessage, MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Request could not be approved", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsRequestsManagement.aspx --> BtnSendEmail_ServerClick", string.Format("Request with ID {0}, could not be updated as approved at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Request could not be found because of request ID. ", MessageTypes.Error, true, true, false, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Please try again later to approve the request.", MessageTypes.Error, true, true, false, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aBtnConfirm_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                int leadId = Convert.ToInt32(item["id"].Text);
                if (leadId > 0)
                {
                    ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadById(leadId, session);
                    if (lead != null)
                    {
                        bool foundClearbit = false;

                        try
                        {
                            foundClearbit = Lib.Services.EnrichmentAPI.ClearBit.GetRFPCompanyByEmail(lead, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        RdgElioUsersDemoRequests.Rebind();
                        RdgElioUsersApprovedDemoRequests.Rebind();

                        if (foundClearbit)
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Approved RFPs Request has confirmed data from clearbit", MessageTypes.Success, true, true, false, true, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Approved RFPs Request could not have confirmed data from clearbit", MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Request could not be approved", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsRequestsManagement.aspx --> BtnSendEmail_ServerClick", string.Format("Request with ID {0}, could not be updated as approved at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Request could not be found because of request ID. ", MessageTypes.Error, true, true, false, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Please try again later to approve the request.", MessageTypes.Error, true, true, false, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnReject_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;
                
                int leadId = Convert.ToInt32(item["id"].Text);
                if (leadId > 0)
                {
                    ElioSnitcherWebsiteLeads lead = Sql.GetSnitcherWebsiteLeadById(leadId, session);
                    if (lead != null)
                    {
                        lead.IsApproved = -1;
                        lead.LastUpdate = DateTime.Now;

                        DataLoader<ElioSnitcherWebsiteLeads> loader = new DataLoader<ElioSnitcherWebsiteLeads>(session);
                        loader.Update(lead);

                        RdgElioUsersDemoRequests.Rebind();
                        RdgElioUsersApprovedDemoRequests.Rebind();

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Request was rejected", MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Request could not be rejected.", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsRequestsManagement.aspx --> BtnReject_ServerClick", string.Format("Request with ID {0}, could not be updated as rejected at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Request could not be rejected because of request ID. ", MessageTypes.Error, true, true, false, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Please try again later to rejected request.", MessageTypes.Error, true, true, false, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RbtnGetLeads_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageControlCriteria.Visible = false;

                if (true)
                {
                    List<ElioSnitcherWebsiteLeads> infoLeads = InsertIpInfoLeads();

                    string message = string.Format("{0} leads were inserted to Elio from Anonymous Ip Info/The Companies API at {1}", infoLeads.Count.ToString(), DateTime.Now);

                    Logger.DetailedError("AdminRFPsRequestsManagement --> RbtnGetLeads_Click()", message);

                    GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, message, MessageTypes.Success, true, true, false, false, false);
                }
                else
                {
                    //List<ElioSnitcherWebsites> websites = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsitesList(1, session);
                    //if (websites != null && websites.Count > 0)
                    //{
                    //    if (websites[0].WebsiteId != "")
                    //    {
                    //        List<ElioSnitcherWebsiteLeads> leads = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeads(websites[0], 1, session);
                    //    }
                    //}
                    //else
                    //{
                    ElioSnitcherWebsites website = Sql.GetSnitcherWebsite("19976", session);
                    if (website != null)
                    {
                        string date = "";

                        if (RdtLeadsPicker.SelectedDate != null)
                        {
                            string year = Convert.ToDateTime(RdtLeadsPicker.SelectedDate).Year.ToString();
                            string month = Convert.ToDateTime(RdtLeadsPicker.SelectedDate).Month.ToString();
                            string day = Convert.ToDateTime(RdtLeadsPicker.SelectedDate).Day.ToString();

                            date = year + "-" + month + "-" + day;
                        }

                        List<ElioSnitcherWebsiteLeads> leads = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeads(website, 1, date, session);

                        if (leads != null && leads.Count > 0)
                        {
                            string message = string.Format("{0} leads were inserted to Elio from Snitcher API", leads.Count.ToString());
                            if (date != "")
                                message += " at date " + date;

                            GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, message, MessageTypes.Success, true, true, false, true, false);
                        }
                        else
                        {
                            string message = string.Format("No new leads were inserted to Elio from Snitcher API", leads.Count.ToString());
                            if (date != "")
                                message += " at date " + date;

                            GlobalMethods.ShowMessageControlDA(UcMessageControlCriteria, message, MessageTypes.Warning, true, true, false, true, false);
                        }
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

        private List<ElioSnitcherWebsiteLeads> InsertIpInfoLeads()
        {
            List<ElioSnitcherWebsiteLeads> infoLeads = new List<ElioSnitcherWebsiteLeads>();

            try
            {
                List<ElioAnonymousIpInfo> leadInfos = Sql.GetAnonymousIpInfoByInsertedStatus(0, session);
                foreach (ElioAnonymousIpInfo leadInfo in leadInfos)
                {
                    session.BeginTransaction();

                    if (!string.IsNullOrEmpty(leadInfo.CompanyDomain))
                    {
                        ElioAnonymousCompaniesInfo companyInfo = Lib.Services.TheCompaniesAPI.CompaniesServiceAPI.GetCompaniesInfo(leadInfo.CompanyDomain, session);

                        if (companyInfo != null)
                        {
                            ElioSnitcherWebsiteLeads infoLead = Lib.Services.AnonymousTrackingAPI.SnitcherService.GetWebsiteLeadsFromInfo(companyInfo, leadInfo, session);

                            if (infoLead != null)
                                infoLeads.Add(infoLead);
                        }
                    }

                    leadInfo.IsInserted = 1;

                    DataLoader<ElioAnonymousIpInfo> loader = new DataLoader<ElioAnonymousIpInfo>(session);
                    loader.Update(leadInfo);

                    session.CommitTransaction();
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }

            return infoLeads;
        }

        protected void ImgBtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    if (item != null)
                    {
                        Label lblProduct = (Label)ControlFinder.FindControlRecursive(item, "LblProduct");
                        TextBox tbxProduct = (TextBox)ControlFinder.FindControlRecursive(item, "TbxProduct");
                        ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                        ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                        ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");

                        lblProduct.Visible = imgBtnEdit.Visible = false;
                        tbxProduct.Visible = imgBtnCancel.Visible = imgBtnSave.Visible = true;
                    }
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went terribly wrong and you can not edit product. Try again later", MessageTypes.Warning, true, true, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (item != null)
                {
                    Label lblProduct = (Label)ControlFinder.FindControlRecursive(item, "LblProduct");
                    TextBox tbxProduct = (TextBox)ControlFinder.FindControlRecursive(item, "TbxProduct");
                    ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                    ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");

                    lblProduct.Visible = imgBtnEdit.Visible = true;
                    lblProduct.Text = item["product"].Text;

                    tbxProduct.Visible = imgBtnCancel.Visible = imgBtnSave.Visible = false;

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "You canceled your edit. No changes were saved.", MessageTypes.Info, true, true, false, false, false);
                }
                else
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went terribly wrong and you can not edit product. Try again later", MessageTypes.Warning, true, true, false, false, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                if (item != null)
                {
                    Label lblProduct = (Label)ControlFinder.FindControlRecursive(item, "LblProduct");
                    TextBox tbxProduct = (TextBox)ControlFinder.FindControlRecursive(item, "TbxProduct");
                    ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                    ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");

                    if (tbxProduct.Text.Trim() == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill product description and try again!", MessageTypes.Warning, true, true, false, false, false);
                        return;
                    }
                    else if (tbxProduct.Text.Trim() == lblProduct.Text.Trim())
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill different product description if you want to update it!", MessageTypes.Warning, true, true, false, false, false);
                        return;
                    }
                    else
                    {
                        int elioWebsiteLeadsId = Convert.ToInt32(item["id"].Text);
                        if (elioWebsiteLeadsId > 0)
                        {
                            int row = session.ExecuteQuery(@"UPDATE Elio_snitcher_leads_pageviews 
                                                    SET product = @product, url = @url
                                                    WHERE elio_website_leads_id = @elio_website_leads_id"
                                            , DatabaseHelper.CreateStringParameter("@product", tbxProduct.Text.Trim())
                                            , DatabaseHelper.CreateStringParameter("@url", tbxProduct.Text.Trim())
                                            , DatabaseHelper.CreateIntParameter("@elio_website_leads_id", elioWebsiteLeadsId));

                            if (row > 0)
                            {
                                lblProduct.Visible = imgBtnEdit.Visible = true;
                                lblProduct.Text = item["product"].Text = tbxProduct.Text.Trim();
                                
                                tbxProduct.Visible = imgBtnCancel.Visible = imgBtnSave.Visible = false;

                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Your product saved successfully.", MessageTypes.Success, true, true, false, false, false);
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went wrong and product could not be change. Update was wrong!", MessageTypes.Warning, true, true, false, false, false);
                            }
                        }
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went wrong and product could not be change. ID not found!", MessageTypes.Warning, true, true, false, false, false);
                    }
                }
                else
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went terribly wrong and you can not edit product. Grid Item could not be found!", MessageTypes.Warning, true, true, false, false, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, ex.Message.ToString(), MessageTypes.Warning, true, true, false);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion
    }
}