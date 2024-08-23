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
using Stripe;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Services.StripeAPI.Enums;
using ServiceStack.Stripe;
using System.Security.Principal;
using WdS.ElioPlus.SalesforceDC;
using System.Threading;
using ServiceStack;
using System.Globalization;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnersCommissionsPaymentsPage : System.Web.UI.Page
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

                    //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    //scriptManager.RegisterPostBackControl(aPaymentLink);

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    //bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardPartnersCommissionsPage", Actions.View, session);
                    //if (!hasRight)
                    //{
                    //    Response.Redirect(ControlLoader.PageDash405, false);
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
                //aBtnExportPdf.Visible = aBtnExportCsv.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
                UcSendMessageAlert.Visible = UcSendMessageAlertPending.Visible = UcSendMessageAlertPast.Visible = false;

                UpdateStrings();
                SetLinks();
                GetCollaborationAssignedUsersTable();
            }

            divSuccess.Visible = false;
            LblSuccessMsg.Text = "";

            divFailure.Visible = false;
            LblFailureMsg.Text = "";

            RdgCommisionsPending.MasterTableView.GetColumn("actions").Display = RdgCommisionsPast.MasterTableView.GetColumn("actions").Display = vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString();
        }

        private void SetLinks()
        {
            aCommissionBillingDetails.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-billing");
            aCommissionFeesTerms.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-fees");
            aCommissionPayments.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-payments");
        }

        private void UpdateStrings()
        {
            string partners = (vSession.User.CompanyType == Types.Vendors.ToString()) ? EnumHelper.GetDescription(Types.Resellers).ToString() : Types.Vendors.ToString();
        }

        private void GetCollaborationAssignedUsersTable()
        {
            DataTable usersTbl = SqlCollaboration.GetCollaborationAllOrAssignedUsersByUserTypeTbl(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (usersTbl.Rows.Count > 0)
            {
                DrpPartnersPending.Items.Clear();
                DrpPartners.Items.Clear();

                if (usersTbl.Rows.Count == 1)
                {
                    DrpPartnersPending.DataSource = usersTbl;

                    DrpPartnersPending.DataValueField = "id";
                    DrpPartnersPending.DataTextField = "company_name";

                    DrpPartnersPending.DataBind();

                    DrpPartnersPending.Items.FindByValue(usersTbl.Rows[0]["id"].ToString()).Selected = true;
                    DrpPartnersPending.SelectedItem.Value = usersTbl.Rows[0]["id"].ToString();
                    DrpPartnersPending.SelectedItem.Text = usersTbl.Rows[0]["company_name"].ToString();

                    DrpPartnersPending.Enabled = false;

                    DrpPartners.DataSource = usersTbl;

                    DrpPartners.DataValueField = "id";
                    DrpPartners.DataTextField = "company_name";

                    DrpPartners.DataBind();

                    DrpPartners.Items.FindByValue(usersTbl.Rows[0]["id"].ToString()).Selected = true;
                    DrpPartners.SelectedItem.Value = usersTbl.Rows[0]["id"].ToString();
                    DrpPartners.SelectedItem.Text = usersTbl.Rows[0]["company_name"].ToString();

                    DrpPartners.Enabled = false;
                }
                else
                {
                    DataRow row = usersTbl.NewRow();
                    row["id"] = "0";
                    row["company_name"] = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select Reseller" : "Select Vendor";
                    usersTbl.Rows.Add(row);

                    EnumerableRowCollection<DataRow> users = from user in usersTbl.AsEnumerable()
                                                             orderby user.Field<int>("id")
                                                             select user;

                    DataView dv = users.AsDataView();

                    DrpPartnersPending.DataSource = dv;
                    DrpPartnersPending.DataValueField = "id";
                    DrpPartnersPending.DataTextField = "company_name";

                    DrpPartnersPending.DataBind();

                    DrpPartnersPending.Enabled = true;

                    DrpPartners.DataSource = dv;
                    DrpPartners.DataValueField = "id";
                    DrpPartners.DataTextField = "company_name";

                    DrpPartners.DataBind();

                    DrpPartners.Enabled = true;
                }
            }
        }

        private void DeleteInvitationFromGrid(int vendorResellerId, RadGrid rdg)
        {
            SqlCollaboration.DeleteCollaborationById(vendorResellerId, session);

            rdg.Rebind();

            ShowPopUpModalWithText("Successfull Delete", "You deleted this partner from your list successfully.");
        }

        private void ShowPopUpModalWithText(string title, string content)
        {
            LblInvitationSendTitle.Text = title;
            LblSuccessfullSendfMsg.Text = content;

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenSendInvitationPopUp();", true);
        }

        private void SetDealLinkUrl(GridDataItem item, int partnerId)
        {
            string url = "";

            string sessionId = GlobalDBMethods.FixUrlByUserRole(vSession.User.Id, partnerId, session);
            Session[sessionId] = partnerId.ToString();

            url = ControlLoader.Dashboard(vSession.User, "management-tier") + "?partnerViewID=" + sessionId;

            HtmlAnchor aMoreDetails = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aMoreDetails");
            aMoreDetails.HRef = ControlLoader.Dashboard(vSession.User, "management-tier") + "?partnerViewID=" + sessionId;
        }

        private void ShowPopUpModalAlert(string title, string content, MessageTypes type)
        {
            LblMessageTitle.Text = title;
            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, content, type, true, true, true, false, false);

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenAssignPartnerPopUp();", true);
        }

        public void ShowTab(int tab)
        {
            switch (tab)
            {
                case 1:

                    aPendingPayments.Attributes["class"] = "nav-link active";
                    tab_1_1.Attributes["class"] = "tab-pane fade show active";
                    tab_1_1.Visible = true;
                    RdgCommisionsPending.Rebind();

                    aPayments.Attributes["class"] = aPastPayments.Attributes["class"] = "nav-link";
                    tab_1_2.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                    tab_1_2.Visible = tab_1_3.Visible = false;

                    break;

                case 2:

                    aPayments.Attributes["class"] = "nav-link active";
                    tab_1_2.Attributes["class"] = "tab-pane fade show active";
                    tab_1_2.Visible = true;
                    RdgCommisions.Rebind();

                    aPendingPayments.Attributes["class"] = aPastPayments.Attributes["class"] = "nav-link";
                    tab_1_1.Attributes["class"] = tab_1_3.Attributes["class"] = "tab-pane fade";
                    tab_1_1.Visible = tab_1_3.Visible = false;

                    break;

                case 3:

                    aPastPayments.Attributes["class"] = "nav-link active";
                    tab_1_3.Attributes["class"] = "tab-pane fade show active";
                    tab_1_3.Visible = true;
                    RdgCommisionsPast.Rebind();

                    aPendingPayments.Attributes["class"] = aPayments.Attributes["class"] = "nav-link";
                    tab_1_1.Attributes["class"] = tab_1_2.Attributes["class"] = "tab-pane fade";
                    tab_1_1.Visible = tab_1_2.Visible = false;

                    break;
            }
        }

        private void FixItemData(GridDataItem item)
        {
            ElioUsers company = null;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
                company = Sql.GetUserById(Convert.ToInt32(item["reseller_id"].Text), session);
            else
                company = Sql.GetUserById(Convert.ToInt32(item["vendor_id"].Text), session);

            if (company != null)
            {
                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");
                aCompanyName.HRef = (company.UserApplicationType == Convert.ToInt32(UserApplicationType.Elioplus)) ? ControlLoader.Profile(company) : ControlLoader.PersonProfile(company);
                aCompanyName.Target = "_blank";
            }

            //if (vSession.User.CompanyType == Types.Vendors.ToString())
            //{
            //    string responseURL = "";

            //    StripeUsersAccountsCustomers customer = SqlCollaboration.GetCustomerJoinAccountByVendResId(Convert.ToInt32(item["vendor_id"].Text), Convert.ToInt32(item["reseller_id"].Text), session);
            //    if (customer != null)
            //    {
            //        ElioLeadDistributions lead = Sql.GetLeadDistributionById(Convert.ToInt32(item["id"].Text), session);
            //        if (lead != null)
            //        {
            //            StripeUsersAccountsProducts accProduct = SqlCollaboration.GetAccountProductByStripeAccountIDAndServiceId(customer.StripeAccountId, lead.Id, session);
            //            if (accProduct == null)
            //            {
            //                Stripe.Product stripeProduct = Lib.Services.StripeAPI.StripeAPIService.CreateProductToAccountApi(lead.CompanyName, "service", "Leads services for channel partners and vendors", vSession.User.CompanyName.ToUpper(), customer.StripeAccountId);
            //                if (!string.IsNullOrEmpty(stripeProduct.Id))
            //                {
            //                    accProduct = new StripeUsersAccountsProducts();

            //                    accProduct.StripeAccountId = customer.StripeAccountId;
            //                    accProduct.StripeCustomerId = customer.StripeCustomerId;
            //                    accProduct.ProductId = stripeProduct.Id;
            //                    accProduct.ElioServiceId = lead.Id;
            //                    accProduct.Name = stripeProduct.Name;
            //                    accProduct.Type = stripeProduct.Type;
            //                    accProduct.DateCreated = DateTime.Now;
            //                    accProduct.LastUpdated = DateTime.Now;

            //                    DataLoader<StripeUsersAccountsProducts> loaderPr = new DataLoader<StripeUsersAccountsProducts>(session);
            //                    loaderPr.Insert(accProduct);
            //                }
            //            }

            //            PaymentLink paymentLink = null;
            //            StripeUsersAccountsProductsPrices accPrice = SqlCollaboration.GetAccountPricetByServiceIdAndStripeAccountID(accProduct.Id, accProduct.ProductId, session);
            //            if (accPrice == null)
            //            {
            //                decimal amount = Convert.ToDecimal(item["amount"].Text);
            //                Stripe.Price priceItem = Lib.Services.StripeAPI.StripeAPIService.CreatePriceToAccountApi((long)amount, item["currency"].Text, accProduct.ProductId, "Won Lead for company " + lead.CompanyName, customer.StripeAccountId);
            //                if (priceItem != null)
            //                {
            //                    paymentLink = Lib.Services.StripeAPI.StripeAPIService.CreatePaymentLinkToPriceApi(priceItem.Id, customer.StripeAccountId);
            //                    if (paymentLink != null)
            //                    {
            //                        StripeUsersAccountsProductsPrices newPrice = new StripeUsersAccountsProductsPrices();

            //                        newPrice.AccountProductId = accProduct.Id;
            //                        newPrice.StripeProductId = accProduct.ProductId;
            //                        newPrice.PriceId = priceItem.Id;
            //                        newPrice.Amount = (decimal)amount;
            //                        newPrice.Currency = item["currency"].Text;
            //                        newPrice.NickName = priceItem.Nickname;
            //                        newPrice.PaymentLinkId = paymentLink.Id;
            //                        newPrice.PaymentLink = paymentLink.Url;
            //                        newPrice.Status = "active";
            //                        newPrice.DateCreated = DateTime.Now;
            //                        newPrice.LastUpdated = DateTime.Now;

            //                        DataLoader<StripeUsersAccountsProductsPrices> priceLoader = new DataLoader<StripeUsersAccountsProductsPrices>(session);
            //                        priceLoader.Insert(newPrice);

            //                        responseURL = newPrice.PaymentLink;
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                //Stripe.Price priceItem = Lib.Services.StripeAPI.StripeAPIService.GetPriceToAccountApi(accPrice.PriceId, customer.StripeAccountId);
                            
            //                paymentLink = Lib.Services.StripeAPI.StripeAPIService.GetPaymentLinkToPriceApi(accPrice.PaymentLinkId, customer.StripeAccountId);
            //                if (paymentLink != null)
            //                {
            //                    responseURL = accPrice.PaymentLink;
            //                }
            //            }

            //            if (responseURL != "")
            //            {
            //                HtmlAnchor aPaymentLink = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPaymentLink");
            //                aPaymentLink.Visible = true;
            //                aPaymentLink.HRef = responseURL;
            //                aPaymentLink.Target = "_blank";
            //            }
            //        }
            //    }
            //}
        }

        private void FixItemAmount(GridDataItem item, bool isPending)
        {
            if (item != null)
            {
                Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");

                Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");
                HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");

                int id = Convert.ToInt32(item["id"].Text);
                if (id > 0)
                {
                    if (lblEditAmount.Text == "Edit amount")
                    {
                        lblAmount.Visible = false;

                        tbxAmount.Visible = true;
                        tbxAmount.Text = lblAmount.Text;
                        aCancelEditAmount.Visible = true;

                        lblEditAmount.Text = "Save amount";
                    }
                    else
                    {
                        if (tbxAmount.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Please, add amount.", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (!Validations.IsNumeric(tbxAmount.Text.Trim()))
                            {
                                GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Please, add only numbers for the amount.", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(tbxAmount.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Please, add amount over 0.", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }

                        if (tbxAmount.Text.Trim() != lblAmount.Text.Trim())
                        {
                            ElioLeadDistributions lead = Sql.GetLeadDistributionById(id, session);
                            if (lead != null)
                            {
                                lead.Amount = Convert.ToDecimal(tbxAmount.Text.Trim());

                                DataLoader<ElioLeadDistributions> loader = new DataLoader<ElioLeadDistributions>(session);
                                loader.Update(lead);

                                item["amount"].Text = lead.Amount.ToString();

                                GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Amount updated successfully.", MessageTypes.Success, true, true, true, true, false);
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Sorry, something went wrong. Please, try again later.", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Amount did not changed.", MessageTypes.Info, true, true, true, true, false);
                        }

                        lblAmount.Visible = true;
                        tbxAmount.Visible = false;

                        lblAmount.Text = tbxAmount.Text;
                        aCancelEditAmount.Visible = false;

                        lblEditAmount.Text = "Edit amount";
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Sorry, something went wrong. Please, try again later.", MessageTypes.Error, true, true, true, true, false);
                    return;
                }
            }
        }

        private void CancelItemAmount(GridDataItem item, bool isPending)
        {
            if (item != null)
            {
                Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");

                Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");
                HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");

                lblAmount.Visible = true;

                tbxAmount.Visible = false;
                lblAmount.Text = item["amount"].Text;
                aCancelEditAmount.Visible = false;

                lblEditAmount.Text = "Edit amount";

                GlobalMethods.ShowMessageControlDA(isPending ? UcSendMessageAlertPending : UcSendMessageAlertPast, "Edit Amount canceled.", MessageTypes.Info, true, true, true, true, false);
            }
        }

        # endregion

        #region Grids

        protected void RdgCommisionsPast1_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                    TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");
                    Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");

                    lblAmount.Text = item["amount"].Text;
                    tbxAmount.Text = lblAmount.Text;

                    lblEditAmount.Text = "Edit amount";

                    HtmlAnchor aPaymentMethod = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPaymentMethod");
                    aPaymentMethod.HRef = item["url"].Text;
                    aPaymentMethod.Target = "_blank";

                    HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                    aPayNow.Visible = item["stripe_customer_id"].Text != "";

                    HtmlAnchor aEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditAmount");
                    HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");
                    //HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");

                    aEditAmount.Visible = aPayNow.Visible = aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

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

        protected void RdgCommisionsPast1_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = new DataTable();
                    //StripeUsersAccounts strAccount = null;
                    //string stripeAccountId = "";
                    List<StripeUsersAccounts> strAccounts = null;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        //if (DrpPartnersPending.SelectedItem.Value == "0")
                        //{
                        //    RdgCommisionsPending.Visible = false;
                        //    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "Select partner to see his history payments.", MessageTypes.Info, true, true, false, false, false);
                        //    return;
                        //}

                        strAccounts = SqlCollaboration.GetStripeAccountsByVendor(vSession.User.Id, session);
                    }
                    else
                    {
                        StripeUsersAccounts strAccount = SqlCollaboration.GetStripeAccountByUserId(vSession.User.Id, session);
                        //stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);

                        if (strAccount != null)
                        {
                            strAccounts = new List<StripeUsersAccounts>();
                            strAccounts.Add(strAccount);
                        }
                    }

                    if (strAccounts.Count > 0)
                    {
                        RdgCommisionsPast.Visible = true;
                        UcSendMessageAlertPast.Visible = false;

                        table.Columns.Add("id");
                        table.Columns.Add("name");
                        table.Columns.Add("amount");
                        table.Columns.Add("currency");
                        table.Columns.Add("fee");
                        table.Columns.Add("commision");
                        table.Columns.Add("date");
                        table.Columns.Add("status");
                        table.Columns.Add("url");
                        table.Columns.Add("stripe_account_id");
                        table.Columns.Add("stripe_customer_id");

                        foreach (StripeUsersAccounts strAccount in strAccounts)
                        {
                            if (strAccount != null)
                            {
                                //stripeAccountId = "acct_1M4RHKPKiaS6v4n7";
                                StripeList<PaymentLink> paymentLinks = Lib.Services.StripeAPI.StripeAPIService.GetAllPaymentLinksApi(strAccount.StripeAccountId);

                                if (paymentLinks != null && paymentLinks.Data.Count > 0)
                                {
                                    StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByAccountIdAndStripeAccountId(strAccount.Id, strAccount.StripeAccountId, session);
                                    if (customer != null)
                                    {
                                        Customer custExist = Lib.Services.StripeAPI.StripeAPIService.GetCustomerNewUnderAccountApi(strAccount.StripeAccountId, customer.StripeCustomerId);
                                        if (custExist != null && !string.IsNullOrEmpty(custExist.Id))
                                        {
                                            foreach (PaymentLink item in paymentLinks.Data)
                                            {
                                                StripeList<LineItem> payments = Lib.Services.StripeAPI.StripeAPIService.GetAllPaymentLinksLineItemsApi(strAccount.StripeAccountId, item.Id);
                                                foreach (LineItem itm in payments)
                                                {
                                                    Stripe.Price priceItem = Lib.Services.StripeAPI.StripeAPIService.GetPriceToAccountApi(itm.Price.Id, strAccount.StripeAccountId);

                                                    string appFeePerCent = "1 %";
                                                    //string amount = itm.AmountTotal.ToString();
                                                    if (item.ApplicationFeePercent != null)
                                                        appFeePerCent = item.ApplicationFeePercent.ToString() + "%";

                                                    table.Rows.Add(item.Id.Substring(0, 10), priceItem.Nickname, itm.AmountTotal, itm.Currency.ToUpper(), (item.ApplicationFeeAmount != null) ? item.ApplicationFeeAmount : (1 / 100) * itm.AmountTotal, appFeePerCent, priceItem.Created.Year + "-" + priceItem.Created.Month + "-" + priceItem.Created.Day, (item.Active) ? "Active" : "Paid", item.Url, strAccount.StripeAccountId, !string.IsNullOrEmpty(custExist.DefaultSourceId) ? customer.StripeCustomerId : "");
                                                    break;
                                                }
                                            }

                                            RdgCommisionsPast.DataSource = table;

                                            //RdgCommisions.DataBind();
                                            //divExportAreaButton.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    RdgCommisionsPast.Visible = false;
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "There are no payments or transfers by or from you.", MessageTypes.Info, true, true, false, false, false);
                                }
                            }
                            else
                            {
                                RdgCommisionsPast.Visible = false;

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "Please set up your account first, in order to send payments to your Channel Partners.", MessageTypes.Info, true, true, false, false, false);
                                else
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "Please set up your account first, in order to receive payments from your Vendors.", MessageTypes.Info, true, true, false, false, false);
                            }
                        }
                    }
                    else
                    {
                        RdgCommisionsPast.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "None of your partners have set up their account, in order to send them payments.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "Please set up your account first, in order to receive payments from your Vendors.", MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgCommisionsPast_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    FixItemData(item);

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");

                        bool hasAccountToStripe = SqlCollaboration.ExistAccountAPIByChannelPartner(Convert.ToInt32(item["reseller_id"].Text), session);
                        if (hasAccountToStripe)
                        {
                            int methodType = SqlCollaboration.GetVendorPaymentMethod(vSession.User.Id, session);

                            aPayNow.Visible = methodType == (int)PaymentMethodType.CardPayment;
                        }
                        else
                            aPayNow.Visible = false;

                        StripeUsersAccountsCustomersSettings userSetting = SqlCollaboration.GetStripeCustomerSettingsByVendorId(vSession.User.Id, session);
                        if (userSetting != null)
                        {
                            if (userSetting.IsActive == 1)
                            {
                                item["payment_date"].Text = Convert.ToDateTime(item["last_update"].Text).AddDays(userSetting.PaymentDaysAfter).ToString("yyyy-MM-dd");
                            }
                        }

                        if (Convert.ToDateTime(item["payment_date"].Text) < DateTime.Now)
                        {
                            Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                            lblStatus.Text = "Past Due";
                            lblStatus.CssClass = "label label-lg label-light-danger label-inline";
                        }
                    }

                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                    TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");
                    Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");

                    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                    string sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
                    NumberFormatInfo nfiGR = new CultureInfo("el-GR", false).NumberFormat;

                    string myNumber = item["amount"].Text.ToString(nfiGR);
                    string am = item["amount"].Text.ToString(nfi);
                    
                    nfiGR.NumberDecimalSeparator = ".";
                    myNumber = myNumber.ToString(nfiGR);

                    //decimal amount = Convert.ToDecimal(item["amount"].Text);
                    lblAmount.Text = item["amount"].Text;
                    tbxAmount.Text = lblAmount.Text;

                    lblEditAmount.Text = "Edit amount";

                    HtmlAnchor aEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditAmount");
                    //HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");
                    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");

                    aEditAmount.Visible = aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
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

        protected void RdgCommisionsPast_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable leads = SqlCollaboration.GetUserLeadsForPaymentsTbl(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, DealResultStatus.Won.ToString(), (int)Lib.Services.StripeAPI.Enums.PaymentStatus.NotPaid, true, session);
                    if (leads != null && leads.Rows.Count > 0)
                    {
                        RdgCommisionsPast.Visible = true;
                        UcSendMessageAlertPast.Visible = false;

                        RdgCommisionsPast.DataSource = leads;
                    }
                    else
                    {
                        RdgCommisionsPast.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "There are no upcoming payments to any of your partners.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPast, "There are no upcoming payments from any of your vendors.", MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgCommisionsPending1_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                    TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");
                    Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");

                    lblAmount.Text = item["amount"].Text;
                    tbxAmount.Text = lblAmount.Text;

                    lblEditAmount.Text = "Edit amount";

                    HtmlAnchor aPaymentMethod = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPaymentMethod");
                    aPaymentMethod.HRef = item["url"].Text;
                    aPaymentMethod.Target = "_blank";

                    HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                    aPayNow.Visible = item["stripe_customer_id"].Text != "";

                    HtmlAnchor aEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditAmount");
                    //HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");
                    //HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");

                    aEditAmount.Visible = aPayNow.Visible = aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

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

        protected void RdgCommisionsPending1_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = new DataTable();
                    List<StripeUsersAccounts> strAccounts = null;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        strAccounts = SqlCollaboration.GetStripeAccountsByVendor(vSession.User.Id, session);
                    }
                    else
                    {
                        StripeUsersAccounts strAccount = SqlCollaboration.GetStripeAccountByUserId(vSession.User.Id, session);
                        //stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);

                        if (strAccount != null)
                        {
                            strAccounts = new List<StripeUsersAccounts>();
                            strAccounts.Add(strAccount);
                        }
                    }

                    if (strAccounts != null && strAccounts.Count > 0)
                    {
                        RdgCommisionsPending.Visible = true;
                        UcSendMessageAlertPending.Visible = false;

                        table.Columns.Add("id");
                        table.Columns.Add("name");
                        table.Columns.Add("amount");
                        table.Columns.Add("currency");
                        table.Columns.Add("fee");
                        table.Columns.Add("commision");
                        table.Columns.Add("date");
                        table.Columns.Add("status");
                        table.Columns.Add("url");
                        table.Columns.Add("stripe_account_id");
                        table.Columns.Add("stripe_customer_id");

                        foreach (StripeUsersAccounts strAccount in strAccounts)
                        {
                            if (strAccount != null)
                            {
                                //stripeAccountId = "acct_1M4RHKPKiaS6v4n7";
                                StripeList<PaymentLink> paymentLinks = Lib.Services.StripeAPI.StripeAPIService.GetAllPaymentLinksApi(strAccount.StripeAccountId);

                                if (paymentLinks != null && paymentLinks.Data.Count > 0)
                                {
                                    StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByAccountIdAndStripeAccountId(strAccount.Id, strAccount.StripeAccountId, session);
                                    if (customer != null)
                                    {
                                        Customer custExist = Lib.Services.StripeAPI.StripeAPIService.GetCustomerNewUnderAccountApi(strAccount.StripeAccountId, customer.StripeCustomerId);
                                        if (custExist != null && !string.IsNullOrEmpty(custExist.Id))
                                        {
                                            foreach (PaymentLink item in paymentLinks.Data)
                                            {
                                                StripeList<LineItem> payments = Lib.Services.StripeAPI.StripeAPIService.GetAllPaymentLinksLineItemsApi(strAccount.StripeAccountId, item.Id);
                                                foreach (LineItem itm in payments)
                                                {
                                                    Stripe.Price priceItem = Lib.Services.StripeAPI.StripeAPIService.GetPriceToAccountApi(itm.Price.Id, strAccount.StripeAccountId);

                                                    string appFeePerCent = "1 %";
                                                    //string amount = itm.AmountTotal.ToString();
                                                    if (item.ApplicationFeePercent != null)
                                                        appFeePerCent = item.ApplicationFeePercent.ToString() + "%";

                                                    table.Rows.Add(item.Id.Substring(0, 10), priceItem.Nickname, itm.AmountTotal, itm.Currency.ToUpper(), (item.ApplicationFeeAmount != null) ? item.ApplicationFeeAmount : (1 / 100) * itm.AmountTotal, appFeePerCent, priceItem.Created.Year + "-" + priceItem.Created.Month + "-" + priceItem.Created.Day, (item.Active) ? "Active" : "Paid", item.Url, strAccount.StripeAccountId, !string.IsNullOrEmpty(custExist.DefaultSourceId) ? customer.StripeCustomerId : "");
                                                    break;
                                                }
                                            }

                                            RdgCommisionsPending.DataSource = table;

                                            //RdgCommisions.DataBind();
                                            //divExportAreaButton.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    RdgCommisionsPending.Visible = false;
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "There are no payments or transfers by or from you.", MessageTypes.Info, true, true, false, false, false);
                                }
                            }
                            else
                            {
                                RdgCommisionsPending.Visible = false;

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "Please set up your account first, in order to send payments to your Channel Partners.", MessageTypes.Info, true, true, false, false, false);
                                else
                                    GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "Please set up your account first, in order to receive payments from your Vendors.", MessageTypes.Info, true, true, false, false, false);
                            }
                        }
                    }
                    else
                    {
                        RdgCommisionsPending.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "None of your partners have set up their account, in order to send them payments.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "Please set up your account first, in order to receive payments from your Vendors.", MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgCommisionsPending_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    FixItemData(item);
                    
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                        
                        bool hasAccountToStripe = SqlCollaboration.ExistAccountAPIByChannelPartner(Convert.ToInt32(item["reseller_id"].Text), session);
                        if (hasAccountToStripe)
                        {
                            int methodType = SqlCollaboration.GetVendorPaymentMethod(vSession.User.Id, session);

                            aPayNow.Visible = methodType == (int)PaymentMethodType.CardPayment;
                        }
                        else
                            aPayNow.Visible = false;

                        StripeUsersAccountsCustomersSettings userSetting = SqlCollaboration.GetStripeCustomerSettingsByVendorId(vSession.User.Id, session);
                        if (userSetting != null)
                        {
                            if (userSetting.IsActive == 1)
                            {
                                item["payment_date"].Text = Convert.ToDateTime(item["last_update"].Text).AddDays(userSetting.PaymentDaysAfter).ToString("yyyy-MM-dd");
                            }
                        }

                        if (Convert.ToDateTime(item["payment_date"].Text) < DateTime.Now)
                        {
                            Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
                            lblStatus.Text = "Past Due";
                            lblStatus.CssClass = "label label-lg label-light-danger label-inline";
                        }
                    }

                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                    TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");
                    Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");

                    lblAmount.Text = item["amount"].Text;
                    tbxAmount.Text = lblAmount.Text;

                    lblEditAmount.Text = "Edit amount";

                    //HtmlAnchor aPaymentMethod = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPaymentMethod");
                    //aPaymentMethod.HRef = item["url"].Text;
                    //aPaymentMethod.Target = "_blank";

                    HtmlAnchor aEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditAmount");
                    //HtmlAnchor aCancelEditAmount = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCancelEditAmount");
                    //HtmlAnchor aPayNow = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPayNow");
                    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");

                    aEditAmount.Visible = aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
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

        protected void RdgCommisionsPending_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //List<ElioLeadDistributions> leads = SqlCollaboration.GetUserLeadsForPayments(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, DealResultStatus.Won.ToString(), session);
                    DataTable leads = SqlCollaboration.GetUserLeadsForPaymentsTbl(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, DealResultStatus.Won.ToString(), (int)Lib.Services.StripeAPI.Enums.PaymentStatus.NotPaid, false, session);
                    if (leads!=null && leads.Rows.Count > 0)
                    {
                        RdgCommisionsPending.Visible = true;
                        UcSendMessageAlertPending.Visible = false;

                        //DataTable table = new DataTable();

                        //table.Columns.Add("id");
                        //table.Columns.Add("partner_name");
                        //table.Columns.Add("company_logo");
                        //table.Columns.Add("client");
                        //table.Columns.Add("client_email");
                        //table.Columns.Add("location");
                        //table.Columns.Add("amount");
                        //table.Columns.Add("currency");                        
                        //table.Columns.Add("last_update");
                        //table.Columns.Add("payment_date");
                        //table.Columns.Add("payment_status");
                        ////table.Columns.Add("url");
                        ////table.Columns.Add("stripe_account_id");
                        ////table.Columns.Add("stripe_customer_id");

                        //foreach (ElioLeadDistributions lead in leads)
                        //{
                        //    ElioUsers partner = Sql.GetUserById(lead.ResellerId, session);
                        //    if (partner != null)
                        //        table.Rows.Add(lead.Id, lead.CollaborationVendorResellerId, lead.VendorId, lead.ResellerId, partner.CompanyName, lead.CompanyName, partner.Country, lead.Email, lead.Website, lead.CreatedDate.ToShortDateString(), lead.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), lead.LeadResult, lead.IsPublic, lead.IsNew);
                        //    else
                        //        table.Rows.Add(lead.Id, lead.CollaborationVendorResellerId, lead.VendorId, lead.ResellerId, "", lead.CompanyName, "", lead.Email, lead.Website, lead.CreatedDate.ToShortDateString(), lead.Status == 1 ? DealStatus.Open.ToString() : DealStatus.Expired.ToString(), lead.LeadResult, lead.IsPublic, lead.IsNew);
                        //}

                        RdgCommisionsPending.DataSource = leads;
                    }
                    else
                    {
                        RdgCommisionsPending.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "There are no pending payments to any of your partners.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlertPending, "There are no pending payments from any of your vendors.", MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgCommisions1_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                    TextBox tbxAmount = (TextBox)ControlFinder.FindControlRecursive(item, "TbxAmount");
                    Label lblEditAmount = (Label)ControlFinder.FindControlRecursive(item, "LblEditAmount");

                    lblAmount.Text = item["amount"].Text;
                    tbxAmount.Text = lblAmount.Text;

                    lblEditAmount.Text = "Edit amount";

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

        protected void RdgCommisions1_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable table = new DataTable();
                    List<StripeUsersAccounts> strAccounts = null;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        //if (DrpPartners.SelectedItem.Value == "0")
                        //{
                        //    RdgCommisions.Visible = false;
                        //    GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "Select partner to see his history payments.", MessageTypes.Info, true, true, false, false, false);
                        //    return;
                        //}

                        strAccounts = SqlCollaboration.GetStripeAccountsByVendor(vSession.User.Id, session);
                        //if (strAccount != null)
                        //    stripeAccountId = strAccount.StripeAccountId;
                    }
                    else
                    {
                        StripeUsersAccounts strAccount = SqlCollaboration.GetStripeAccountByUserId(vSession.User.Id, session);
                        if (strAccount != null)
                        {
                            strAccounts = new List<StripeUsersAccounts>();
                            strAccounts.Add(strAccount);

                            //stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);
                        }
                    }
                    if (strAccounts.Count > 0)
                    {
                        RdgCommisions.Visible = true;
                        UcSendMessageAlert.Visible = false;

                        table.Columns.Add("id");
                        table.Columns.Add("amount");
                        table.Columns.Add("fee");
                        table.Columns.Add("commision");
                        table.Columns.Add("description");
                        table.Columns.Add("email");
                        table.Columns.Add("date");
                        table.Columns.Add("payment_date");
                        table.Columns.Add("status");

                        foreach (StripeUsersAccounts strAccount in strAccounts)
                        {
                            //stripeAccountId = "acct_1M4RHKPKiaS6v4n7";
                            Stripe.StripeList<PaymentIntent> payments = Lib.Services.StripeAPI.StripeAPIService.GetAllPaymentsApi(strAccount.StripeAccountId);

                            if (payments != null && payments.Data.Count > 0)
                            {
                                foreach (PaymentIntent item in payments.Data)
                                {
                                    string custEmail = "";
                                    if (!string.IsNullOrEmpty(item.CustomerId))
                                    {
                                        Customer customer = Lib.Services.StripeAPI.StripeAPIService.GetCustomerNewUnderAccountApi(strAccount.StripeAccountId, item.CustomerId);
                                        if (customer != null && !string.IsNullOrEmpty(customer.Id))
                                        {
                                            custEmail = customer.Email;
                                        }
                                    }
                                    
                                    table.Rows.Add(item.Id, item.Amount, item.ApplicationFeeAmount, "4 %", item.Description != null ? item.Description : "", custEmail, item.Created.Year + "-" + item.Created.Month + "-" + item.Created.Day, item.Created.AddDays(2).Year + "-" + item.Created.AddDays(2).Month + "-" + item.Created.AddDays(2).Day, item.Status);
                                }

                                RdgCommisions.DataSource = table;

                                //RdgCommisions.DataBind();
                                //divExportAreaButton.Visible = true;
                            }
                            else
                            {
                                RdgCommisions.Visible = false;
                                GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "There are no payments by or from you.", MessageTypes.Info, true, true, false, false, false);
                            }
                        }
                    }
                    else
                    {
                        RdgCommisions.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "Please set up your account first, in order to send payments to your Channel Partners.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "Please set up your account first, in order to receive payments from your Vendors.", MessageTypes.Info, true, true, false, false, false);
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

        protected void RdgCommisions_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    FixItemData(item);
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

        protected void RdgCommisions_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    DataTable leads = SqlCollaboration.GetUserLeadsForPaymentsTbl(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, (int)DealStatus.Closed, DealResultStatus.Won.ToString(), (int)Lib.Services.StripeAPI.Enums.PaymentStatus.Paid, false, session);
                    if (leads != null && leads.Rows.Count > 0)
                    {
                        RdgCommisions.Visible = true;
                        UcSendMessageAlert.Visible = false;

                        RdgCommisions.DataSource = leads;
                    }
                    else
                    {
                        RdgCommisions.Visible = false;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "There are no paid payments to any of your partners.", MessageTypes.Info, true, true, false, false, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcSendMessageAlert, "There are no paid payments from any of your vendors.", MessageTypes.Info, true, true, false, false, false);
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

        # region Buttons

        protected void aPaymentLink_ServerClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                                
                if (vSession.User != null)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor imgBtn = (HtmlAnchor)sender;
                        GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                        divSuccess.Visible = divFailure.Visible = false;
                        //string responseURL = "";

                        //if (item != null)
                        //{
                        //    ElioLeadDistributions lead = Sql.GetLeadDistributionById(Convert.ToInt32(item["id"].Text), session);
                        //    if (lead != null)
                        //    {
                        //        StripeUsersAccountsCustomers customer = SqlCollaboration.GetCustomerJoinAccountByVendResId(Convert.ToInt32(item["vendor_id"].Text), Convert.ToInt32(item["reseller_id"].Text), session);
                        //        if (customer != null)
                        //        {
                        //            StripeUsersAccountsProducts accProduct = SqlCollaboration.GetAccountProductByStripeAccountIDAndServiceId(customer.StripeAccountId, lead.Id, session);
                        //            if (accProduct == null)
                        //            {
                        //                Stripe.Product stripeProduct = Lib.Services.StripeAPI.StripeAPIService.CreateProductToAccountApi(lead.CompanyName, "service", "Leads services for channel partners and vendors", vSession.User.CompanyName.ToUpper(), customer.StripeAccountId);
                        //                if (!string.IsNullOrEmpty(stripeProduct.Id))
                        //                {
                        //                    accProduct = new StripeUsersAccountsProducts();

                        //                    accProduct.StripeAccountId = customer.StripeAccountId;
                        //                    accProduct.StripeCustomerId= customer.StripeCustomerId;
                        //                    accProduct.ProductId = stripeProduct.Id;
                        //                    accProduct.ElioServiceId = lead.Id;
                        //                    accProduct.Name = stripeProduct.Name;
                        //                    accProduct.Type = stripeProduct.Type;
                        //                    accProduct.DateCreated = DateTime.Now;
                        //                    accProduct.LastUpdated = DateTime.Now;

                        //                    DataLoader<StripeUsersAccountsProducts> loaderPr = new DataLoader<StripeUsersAccountsProducts>(session);
                        //                    loaderPr.Insert(accProduct);
                        //                }
                        //            }

                        //            PaymentLink paymentLink = null;
                        //            StripeUsersAccountsProductsPrices accPrice = SqlCollaboration.GetAccountPricetByServiceIdAndStripeAccountID(accProduct.Id, accProduct.ProductId, session);
                        //            if (accPrice == null)
                        //            {
                        //                decimal amount = Convert.ToDecimal(item["amount"].Text);
                        //                Stripe.Price priceItem = Lib.Services.StripeAPI.StripeAPIService.CreatePriceToAccountApi((long)amount, item["currency"].Text, accProduct.ProductId, "Won Lead for company " + lead.CompanyName, customer.StripeAccountId);
                        //                if (priceItem != null)
                        //                {
                        //                    paymentLink = Lib.Services.StripeAPI.StripeAPIService.CreatePaymentLinkToPriceApi(priceItem.Id, customer.StripeAccountId);
                        //                    if (paymentLink != null)
                        //                    {
                        //                        StripeUsersAccountsProductsPrices newPrice = new StripeUsersAccountsProductsPrices();

                        //                        newPrice.AccountProductId = accProduct.Id;
                        //                        newPrice.StripeProductId = accProduct.ProductId;
                        //                        newPrice.PriceId = priceItem.Id;
                        //                        newPrice.Amount = (decimal)amount;
                        //                        newPrice.Currency = item["currency"].Text;
                        //                        newPrice.NickName = priceItem.Nickname;
                        //                        newPrice.PaymentLinkId = paymentLink.Id;
                        //                        newPrice.PaymentLink = paymentLink.Url;
                        //                        newPrice.DateCreated = DateTime.Now;
                        //                        newPrice.LastUpdated = DateTime.Now;

                        //                        DataLoader<StripeUsersAccountsProductsPrices> priceLoader = new DataLoader<StripeUsersAccountsProductsPrices>(session);
                        //                        priceLoader.Insert(newPrice);

                        //                        responseURL = newPrice.PaymentLink;
                        //                    }
                        //                }
                        //            }
                        //            else
                        //            {
                        //                paymentLink = Lib.Services.StripeAPI.StripeAPIService.GetPaymentLinkToPriceApi(accPrice.PaymentLinkId, customer.StripeAccountId);
                        //                if (paymentLink != null)
                        //                {
                        //                    responseURL = accPrice.PaymentLink;
                        //                }
                        //            }

                        //            Response.Redirect(responseURL, false);
                        //        }
                        //    }
                        //}
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

        protected void aPayNow_ServerClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor imgBtn = (HtmlAnchor)sender;
                        GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                        divSuccess.Visible = divFailure.Visible = false;

                        if (item != null)
                        {
                            int resellerId = Convert.ToInt32(item["reseller_id"].Text);
                            int vendorId = Convert.ToInt32(item["vendor_id"].Text);
                            if (resellerId > 0 && vendorId > 0 && item["id"].Text != "0" && vendorId == vSession.User.Id)
                            {
                                StripeUsersAccountsCustomers customer = SqlCollaboration.GetCustomerJoinAccountByVendResId(vendorId, resellerId, session);
                                if (customer != null)
                                {
                                    Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");

                                    HdnId.Value = item["id"].Text;
                                    HdnStripeAccountId.Value = customer.StripeAccountId;
                                    HdnStripeCustomerId.Value = customer.StripeCustomerId;
                                    HdnAmount.Value = lblAmount.Text;
                                    HdnCurrency.Value = item["currency"].Text.ToLower();
                                    HdnCommissionFee.Value = (item["tier_commission"].Text != "") ? item["tier_commission"].Text.Replace(" %", "").Trim() : "0";

                                    LblConfMsg.Text = "Are you sure you want to proceed with this payment now?";
                                    BtnDelete.Visible = false;
                                    BtnProceedPayment.Visible = true;

                                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
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
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnProceedPayment_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        Customer custExist = Lib.Services.StripeAPI.StripeAPIService.GetCustomerNewUnderAccountApi(HdnStripeAccountId.Value, HdnStripeCustomerId.Value);
                        if (custExist != null && !string.IsNullOrEmpty(custExist.DefaultSourceId))
                        {
                            decimal feeAmmount = 0;
                            string commissionFeeAmmount = HdnCommissionFee.Value;
                            if (commissionFeeAmmount != "" && commissionFeeAmmount.Contains("%"))
                                commissionFeeAmmount = commissionFeeAmmount.Replace(" %", "").Replace("%", "").Trim();

                            try
                            {
                                feeAmmount = Convert.ToDecimal(commissionFeeAmmount);
                            }
                            catch (Exception ex)
                            {
                                divSuccess.Visible = false;
                                divFailure.Visible = true;
                                LblFailureMsg.Text = "Sorry, your payment could not be done.";

                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());

                                return;
                            }

                            double amount = Convert.ToDouble(HdnAmount.Value);
                            int id = Convert.ToInt32(HdnId.Value);
                            if (id > 0)
                            {
                                Charge charge = Lib.Services.StripeAPI.StripeService.CreateDirectCharge(custExist.Email, (long)amount, (long)feeAmmount, HdnCurrency.Value, custExist.DefaultSourceId, HdnStripeAccountId.Value, custExist.Id);
                                if (charge != null && !string.IsNullOrEmpty(charge.Id) && charge.Paid)
                                {
                                    bool success = SqlCollaboration.UpdateLeadPaymentStatus(id, (int)PaymentStatus.Paid, charge.Id, session);
                                    if (!success)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), "ERROR --> SqlCollaboration.UpdateLeadPaymentStatus(id, (int)PaymentStatus.Paid, session)", string.Format("User {0} create successfull charge with ID {1}, but Lead with ID {2} could not be set payment status as paid at {3}", vSession.User.Id, charge.Id, id, DateTime.Now));
                                    }

                                    divSuccess.Visible = true;
                                    LblSuccessMsg.Text = "Your payment was successfull";

                                    divFailure.Visible = false;
                                    BtnProceedPayment.Visible = false;

                                    RdgCommisionsPending.Rebind();
                                }
                                else
                                {
                                    divSuccess.Visible = false;
                                    divFailure.Visible = true;
                                    LblFailureMsg.Text = "Sorry, your payment could not be done.";

                                }
                            }
                            else
                            {
                                divSuccess.Visible = false;
                                divFailure.Visible = true;
                                LblFailureMsg.Text = "Sorry, your payment could not be done.";
                            }
                        }
                        else
                        {
                            divSuccess.Visible = false;
                            divFailure.Visible = true;
                            LblFailureMsg.Text = "Sorry, your payment could not be done. Your payment source could not be found.";
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                divSuccess.Visible = false;
                divFailure.Visible = true;
                LblFailureMsg.Text = "Sorry, your payment could not be done.";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aEditAmountPending_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                FixItemAmount(item, true);
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

        protected void aCancelEditAmountPending_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                CancelItemAmount(item, true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aEditAmountPast_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                FixItemAmount(item, false);
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

        protected void aCancelEditAmountPast_ServerClick(object sender, EventArgs e)
        {
            try
            {
                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                CancelItemAmount(item, false);
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
                session.OpenConnection();

                if (vSession.User != null)
                {
                    divSuccess.Visible = divFailure.Visible = false;

                    if (HdnStripeAccountId.Value != "")
                    {
                        try
                        {
                            //PaymentIntent payment = Lib.Services.StripeAPI.StripeAPIService.CancelPaymentIntentApi(HdnPaymentId.Value);

                            //HdnPaymentId.Value = "0";

                            //divSuccess.Visible = true;
                            //LblSuccessMsg.Text = "You canceled your payment successfully";

                            //RdgCommisions.Rebind();

                            //UpdatePanelContent.Update();

                            //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

                            //HdnPartnerUserId.Value = "0";
                            //divFailure.Visible = false;
                            //LblFailureMsg.Text = "";
                        }
                        catch (Exception ex)
                        {
                            divFailure.Visible = true;
                            LblFailureMsg.Text = "Your payment could not be canceled. Please try again later";
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }
                    }
                    else
                    {
                        divFailure.Visible = true;
                        LblFailureMsg.Text = "Your payment could not be canceled. Please try again later";
                        Logger.DetailedError(Request.Url.ToString(), "Your payment could not be canceled. Please try again later", "Your payment could not be canceled. Please try again later");
                        return;
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

        protected void aDeleteConfirmed_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                divSuccess.Visible = divFailure.Visible = false;

                if (item != null)
                {
                    int resellerId = Convert.ToInt32(item["reseller_id"].Text);
                    int vendorId = Convert.ToInt32(item["vendor_id"].Text);
                    if (resellerId > 0 && vendorId > 0 && vendorId == vSession.User.Id)
                    {
                        StripeUsersAccountsCustomers customer = SqlCollaboration.GetCustomerJoinAccountByVendResId(vendorId, resellerId, session);
                        if (customer != null)
                        {
                            Label lblAmount = (Label)ControlFinder.FindControlRecursive(item, "LblAmount");
                            HdnStripeAccountId.Value = customer.StripeAccountId;
                            HdnStripeCustomerId.Value = customer.StripeCustomerId;
                            HdnAmount.Value = lblAmount.Text;
                            HdnCurrency.Value = item["currency"].Text;

                            LblConfMsg.Text = "Are you sure you want to cancel this payment?";
                            BtnDelete.Visible = true;
                            BtnProceedPayment.Visible = false;

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
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

        protected void BtnSearchPending_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgCommisionsPending.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSearchConfirmed_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    RdgCommisions.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPendingPayments_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowTab(1);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPayments_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowTab(2);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aPastPayments_ServerClick(object sender, EventArgs e)
        {
            try
            {
                ShowTab(3);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }



        protected void aSaveAssignToMember_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcAssignmentMessageAlert.Visible = false;

                if (HdnPartId.Value != "0" && HdnVendResId.Value != "0")
                {
                    if (DrpTeamMembers.SelectedValue == "0")
                    {
                        bool isAssigned = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), 0, null, session);
                        if (isAssigned)
                        {

                            return;
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Please select member to assign this partner!", MessageTypes.Error, true, true, true, false, false);
                            return;
                        }
                    }
                    else
                    {
                        bool isAssignedToOther = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue), false, session);
                        if (isAssignedToOther)
                        {
                            return;
                        }

                        bool isAssignedToMember = Sql.IsPartnerAssigned(vSession.User.Id, Convert.ToInt32(HdnPartId.Value), Convert.ToInt32(DrpTeamMembers.SelectedValue), true, session);
                        if (isAssignedToMember)
                        {
                            GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "This partner is already assigned to this member!", MessageTypes.Info, true, true, true, false, false);
                            return;
                        }
                        else
                        {

                        }
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner could not be assigned! Please try again later or contact us!", MessageTypes.Error, true, true, true, false, false);
                    return;
                }
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcAssignmentMessageAlert, "Partner could not be assigned! Please try again later or contact us!", MessageTypes.Error, true, true, true, false, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aBtnExportPdf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=pdf";

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCriteriaPdf_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=pdf&mode=a";

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCsv_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=csv";

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aBtnExportCriteriaCsv_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    string url = "download-csv?case=PartnersReportData&type=csv&mode=a";

                    if (RtbxCompanyNameEmailConfirmed.Text != "")
                    {
                        url += "&partnername=" + RtbxCompanyNameEmailConfirmed.Text.Replace(" ", "_").Replace("&", "%").ToLower();
                    }

                    Response.Redirect(url, false);
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aEditTierStatus_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        HtmlAnchor imgBtn = (HtmlAnchor)sender;
                        GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                        if (item != null)
                        {
                            HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                            HtmlGenericControl divSave = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divSave");
                            Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                            DropDownList rcbxTierStatus = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                            rcbxTierStatus.Items.Clear();

                            //DataTable tblTierStatus = Sql.GetCollaborationUserTierStatusTable(vSession.User.Id, session);
                            //List<ElioTierManagementUsersSettings> tierSettings = Sql.GetTierManagementUserSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                            if (vSession.User.Id != 41078)
                            {
                                List<ElioTierManagementUsersSettings> tierSettings = Sql.GetTierManagementUserSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                                if (tierSettings.Count > 0)
                                {
                                    ListItem rcbxItem = new ListItem();

                                    rcbxItem.Value = "0";
                                    rcbxItem.Text = "Select";
                                    rcbxTierStatus.Items.Add(rcbxItem);

                                    foreach (ElioTierManagementUsersSettings tier in tierSettings)
                                    {
                                        rcbxItem = new ListItem();

                                        rcbxItem.Value = tier.Id.ToString();
                                        rcbxItem.Text = tier.Description;
                                        rcbxTierStatus.Items.Add(rcbxItem);
                                    }

                                    rcbxTierStatus.DataBind();
                                }
                            }
                            else
                            {
                                List<ElioTierManagementCustom> tierSettingsCustom = Sql.GetTierManagementCustomSettings(vSession.User.Id, session);       //SqlCollaboration.GetCollaborationUserTierStatus(vSession.User.Id, session);

                                if (tierSettingsCustom.Count > 0)
                                {
                                    ListItem rcbxItem = new ListItem();

                                    rcbxItem.Value = "0";
                                    rcbxItem.Text = "Select";
                                    rcbxTierStatus.Items.Add(rcbxItem);

                                    foreach (ElioTierManagementCustom tier in tierSettingsCustom)
                                    {
                                        rcbxItem = new ListItem();

                                        rcbxItem.Value = tier.Id.ToString();
                                        rcbxItem.Text = tier.Description;
                                        rcbxTierStatus.Items.Add(rcbxItem);
                                    }

                                    rcbxTierStatus.DataBind();
                                }
                            }

                            //else
                            //{
                            ////rcbxTierStatus.DataSource = SqlCollaboration.GetCollaborationDefaultTierStatusTable(session);
                            ////rcbxTierStatus.DataTextField = "status_description";
                            ////rcbxTierStatus.DataValueField = "id";

                            //List<ElioCollaborationTierDefaultStatus> defaultTierStatus = SqlCollaboration.GetCollaborationDefaultTierStatus(session);

                            //if (defaultTierStatus.Count > 0)
                            //{
                            //    RadComboBoxItem rcbxItem = new RadComboBoxItem();

                            //    rcbxItem.Value = "0";
                            //    rcbxItem.Text = "Select";
                            //    rcbxTierStatus.Items.Add(rcbxItem);

                            //    foreach (ElioCollaborationTierDefaultStatus status in defaultTierStatus)
                            //    {
                            //        rcbxItem = new RadComboBoxItem();

                            //        rcbxItem.Value = status.Id.ToString();
                            //        rcbxItem.Text = status.StatusDescription;
                            //        rcbxTierStatus.Items.Add(rcbxItem);
                            //    }

                            //    rcbxTierStatus.DataBind();
                            //}
                            //}

                            string selectedTierStatus = item["tier_status"].Text;
                            if (!string.IsNullOrEmpty(selectedTierStatus) && selectedTierStatus != "&nbsp;")
                            {
                                rcbxTierStatus.SelectedItem.Text = selectedTierStatus;
                                //rcbxTierStatus.FindItemByText(selectedTierStatus).Selected = true;
                            }

                            divEdit.Visible = false;
                            divSave.Visible = true;
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
                
        protected void aCancelTierStatus_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    HtmlAnchor imgBtn = (HtmlAnchor)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    if (item != null)
                    {
                        HtmlGenericControl divEdit = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divEdit");
                        HtmlGenericControl divSave = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divSave");
                        Label lblTierStatus = (Label)ControlFinder.FindControlRecursive(item, "LblTierStatus");
                        DropDownList rcbxTierStatus = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxTierStatus");

                        rcbxTierStatus.Items.Clear();

                        lblTierStatus.Text = (!string.IsNullOrEmpty(item["tier_status"].Text) && item["tier_status"].Text != "&nbsp;") ? item["tier_status"].Text : "Select";

                        divEdit.Visible = true;
                        divSave.Visible = false;
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

        protected void ImgBtnCollaborationRoom_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                vSession.VendorsResellersList.Clear();

                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(item["id"].Text), session);
                if (vendRes != null)
                    GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.SimpleMode, Mode.Any, vSession.VendorsResellersList, vendRes);

                Response.Redirect(ControlLoader.Dashboard(vSession.User, "collaboration-chat-room"), false);
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

        #region DropDownLists


        #endregion
    }
}