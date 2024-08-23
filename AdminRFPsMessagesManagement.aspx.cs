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
    public partial class AdminRFPsMessagesManagement : Telerik.Web.UI.RadAjaxPage
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

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

                    if (!IsPostBack)
                    {
                        #region Load Data

                        LoadPublicStatus();

                        vSession.SearchQueryString = "";
                        RcbxApproved.SelectedValue = "-2";

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
                            products += pageview.Url + ", ";
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

                DataTable table = null;

                if (vSession.SearchQueryString == "")
                    table = Sql.GetRFPsRequestsTbl(1, (int)ApiLeadCategory.isRFQMessage, session);
                else
                {
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                //if (demos.Count > 0)
                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersApprovedDemoRequests.Visible = true;
                    UcMessageAlertApproved.Visible = false;

                    RdgElioUsersApprovedDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersApprovedDemoRequests.Visible = false;

                    string alert = "There are no approved RFPs messages";
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

                    aBtnApprove.Visible = aBtnReject.Visible = item["is_approved"].Text == "0";

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
                            products += pageview.Url + ", ";
                        }

                        item["product"].Text = (products.Length > 0) ? products.Substring(0,products.Length - 2) : "";
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

        protected void RdgElioUsersDemoRequests_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioUsersDemoRequestsIJUsers> demos = new List<ElioUsersDemoRequestsIJUsers>();
                DataTable table = null;

                if (vSession.SearchQueryString == "")
                {
                    table = Sql.GetRFPsRequestsTbl(0, (int)ApiLeadCategory.isRFQMessage, session);
                }
                else
                {
                    table = session.GetDataTable(vSession.SearchQueryString);
                }

                if (table != null && table.Rows.Count > 0)
                {
                    RdgElioUsersDemoRequests.Visible = true;
                    UcMessageAlert.Visible = false;

                    RdgElioUsersDemoRequests.DataSource = table;
                }
                else
                {
                    RdgElioUsersDemoRequests.Visible = false;

                    string alert = "There are no RFPs messages";
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
                                        WHERE 1 = 1 and is_api_lead = 2 ";

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
                            EmailSenderLib.SendRFQApprovedRequestMessageNotificationEmail(ApiLeadCategory.isRFQMessage, lead.Id, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            mailMessage = " Email notification could not be sent.";
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
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "RFPs Mesasge was successfully approved and got data from clearbit." + mailMessage, MessageTypes.Success, true, true, false, true, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "RFPs Mesasge was successfully approved but not found on clearbit." + mailMessage, MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Mesasge could not be approved", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsMessagesManagement.aspx --> BtnSendEmail_ServerClick", string.Format("Mesasge with ID {0}, could not be updated as approved at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Mesasge could not be found because of request ID. ", MessageTypes.Error, true, true, false, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Please try again later to approve the mesasge.", MessageTypes.Error, true, true, false, true, false);
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
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Approved RFPs Mesasge has confirmed data from clearbit", MessageTypes.Success, true, true, false, true, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Approved RFPs Mesasge could not have confirmed data from clearbit", MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Mesasge could not be approved", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsMesasgesManagement.aspx --> BtnSendEmail_ServerClick", string.Format("Mesasge with ID {0}, could not be updated as approved at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Mesasge could not be found because of request ID. ", MessageTypes.Error, true, true, false, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Please try again later to approve the mesasge.", MessageTypes.Error, true, true, false, true, false);
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

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Mesasge was rejected", MessageTypes.Success, true, true, false, true, false);
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but something went wrong. Mesasge could not be rejected.", MessageTypes.Error, true, true, false, true, false);
                        Logger.DetailedError(Request.Url.ToString(), "AdminRFPsMesasgesManagement.aspx --> BtnReject_ServerClick", string.Format("Mesasge with ID {0}, could not be updated as rejected at {1} by user {2}", leadId, DateTime.Now, vSession.User.Id));
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Mesasge could not be rejected because of request ID. ", MessageTypes.Error, true, true, false, true, false);
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

        #endregion
    }
}