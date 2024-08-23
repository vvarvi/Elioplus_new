using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using Stripe;
using System.Web.UI.WebControls;
using DocumentFormat.OpenXml.Wordprocessing;
using WdS.ElioPlus.Lib.Services.StripeAPI.Enums;
using Microsoft.VisualStudio.TextManager.Interop;
using WdS.ElioPlus.SalesforceDC;
using ServiceStack;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnersCommissionsBillingPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(BtnConfigureAccountOnboarding);

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

                    if (!IsPostBack)
                    {
                        FixPage();
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

        #region Methods

        private void FixPage()
        {
            LoadCountries();            
            SetLinks();

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divAccountStatus.Visible = true;
                divPaymentMethods.Visible = true;

                divStep0.Visible = true;

                CbxMan.Checked = true;
                CbxAut.Checked = false;

                LblHeaderInfo1.Text = "Step 2: Company Details";
                LblHeaderInfo2.Text = "Step 3: Billing Details";
                LblBillingSettings.Text = CbxMan.Checked ? "Step 2: Billing Settings" : "Step 4: Billing Settings";
                divStep1.Visible = divStep2.Visible = CbxAut.Checked;

                divToolbarReseller.Visible = false;
                divVendorArea.Visible = true;
                divVendorActions.Visible = true;

                divResellerArea.Visible = false;
                divResellerActions.Visible = false;

                ResetFields();
                ResetFields2();
                ResetFields3();

                UpdateStrings();
                LoadUserSettings();
            }
            else
            {
                divAccountStatus.Visible = false;
                divPaymentMethods.Visible = false;

                divStep0.Visible = false;
                
                divToolbarReseller.Visible = true;
                divResellerArea.Visible = true;
                divResellerActions.Visible = true;

                divVendorArea.Visible = false;
                divVendorActions.Visible = false;
                divStep2.Visible = false;
                divStep3.Visible = false;

                //string stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);
                StripeUsersAccounts stripeAccount = SqlCollaboration.GetStripeAccountByUser(vSession.User.Id, session);
                if (stripeAccount != null)
                {
                    BtnCreateStripeAccount.Visible = false;
                    BtnCancelStripeAccount.Visible = false;

                    BtnConfigureAccountOnboarding.Visible = true;

                    if (stripeAccount.Status == "active")
                    {
                        divAccountStatus.Visible = true;
                        iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-information";
                        RttpAccountStatus.Text = "Your account is configured";

                        BtnConfigureAccountOnboarding.Enabled = false;
                    }
                    else
                    {
                        divAccountStatus.Visible = true;
                        iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-warning";
                        RttpAccountStatus.Text = "Your account is not configured";

                        BtnConfigureAccountOnboarding.Enabled = true;
                    }
                }
                else
                {
                    BtnCreateStripeAccount.Visible = true;
                    BtnCancelStripeAccount.Visible = true;

                    BtnConfigureAccountOnboarding.Visible = false;

                    divAccountStatus.Visible = true;
                    iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-warning";
                    RttpAccountStatus.Text = "Your account is not created";
                }
            }
        }

        private void LoadUserSettings()
        {
            bool mustClose = false;
            if (session.Connection.State == ConnectionState.Closed)
            {
                session.OpenConnection();
                mustClose = true;
            }

            StripeUsersAccountsCustomersSettings userSetting = SqlCollaboration.GetStripeCustomerSettingsByVendorId(vSession.User.Id, session);
            if (userSetting != null)
            {
                CbxMan.Checked = userSetting.PaymentMethod == (int)Lib.Services.StripeAPI.Enums.PaymentMethod.Manual;
                CbxAut.Checked = userSetting.PaymentMethod == (int)Lib.Services.StripeAPI.Enums.PaymentMethod.Automatic;

                if (CbxMan.Checked)
                {
                    CbxAut.Checked = false;
                    divStep1.Visible = divStep2.Visible = divStep3.Visible = false;
                    LblBillingSettings.Text = "Step 2: Billing Settings";

                }
                else
                {
                    CbxAut.Checked = true;
                    divStep1.Visible = divStep2.Visible = divStep3.Visible = true;
                    LblBillingSettings.Text = "Step 4: Billing Settings";
                }

                //DrpDaysAfter.SelectedItem.Value = DrpDaysAfter.SelectedItem.Text = userSetting.PaymentDaysAfter.ToString();
                //DrpDaysAfter.DataBind();
                //DrpFirstNot.SelectedItem.Value = DrpFirstNot.SelectedItem.Text = userSetting.FirstNotificationDaysBefore.ToString();
                //DrpFirstNot.DataBind();
                //DrpSecondNot.SelectedItem.Value = DrpSecondNot.SelectedItem.Text = userSetting.SecondNotificationDaysBefore.ToString();
                //DrpSecondNot.DataBind();
                //CbxDisableNotif.Checked = userSetting.IsActive == 0 ? true : false;

                PnlNotifications.Enabled = !CbxDisableNotif.Checked;
            }
            else
            {
                CbxMan.Checked = true;
                CbxAut.Checked = false;

                //DrpDaysAfter.SelectedItem.Value = "30";
                //DrpFirstNot.SelectedItem.Value = "5";
                //DrpSecondNot.SelectedItem.Value = "2";
            }

            if (mustClose)
                session.CloseConnection();
        }

        private void GetCollaborationAssignedUsersTable()
        {
            DataTable usersTbl = SqlCollaboration.GetCollaborationAllOrAssignedUsersByUserTypeTbl(vSession.User, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (usersTbl.Rows.Count > 0)
            {
                //DrpPartners.Items.Clear();

                if (usersTbl.Rows.Count == 1)
                {
                    //DrpPartners.DataSource = usersTbl;

                    //DrpPartners.DataValueField = "id";
                    //DrpPartners.DataTextField = "company_name";

                    //DrpPartners.DataBind();

                    //DrpPartners.Items.FindByValue(usersTbl.Rows[0]["id"].ToString()).Selected = true;
                    //DrpPartners.SelectedItem.Value = usersTbl.Rows[0]["id"].ToString();
                    //DrpPartners.SelectedItem.Text = usersTbl.Rows[0]["company_name"].ToString();

                    //DrpPartners.Enabled = false;
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

                    //DrpPartners.DataSource = dv;
                    //DrpPartners.DataValueField = "id";
                    //DrpPartners.DataTextField = "company_name";

                    //DrpPartners.DataBind();

                    //DrpPartners.Enabled = true;
                }
            }
        }

        private void SetLinks()
        {
            aCommissionBillingDetails.HRef = aCommissionBillingDetailsReseller.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-billing");
            aCommissionFeesTerms.HRef = aCommissionFeesTermsReseller.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-fees");
            aCommissionPayments.HRef = aCommissionPaymentsReseller.HRef = ControlLoader.Dashboard(vSession.User, "partner-commissions-payments");
        }

        private void UpdateStrings()
        {
            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                if (vSession.User.AccountStatus == (int)AccountStatus.Active)
                {
                    TbxFirstName.Text = vSession.User.FirstName;
                    TbxLastName.Text = vSession.User.LastName;
                    TbxOrganiz.Text = vSession.User.CompanyName;
                    TbxEmail.Text = vSession.User.Email;
                    TbxCity.Text = vSession.User.City;
                    TbxState.Text = vSession.User.State;
                    TbxPhone.Text = vSession.User.Phone;

                    ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vSession.User.Country, session);
                    if (countryCurrency != null && DrpCompanyCountry.SelectedItem != null)
                    {
                        DrpCompanyCountry.SelectedItem.Value = countryCurrency.CurId;
                        DrpCompanyCountry.SelectedItem.Text = countryCurrency.Name;
                    }
                    else
                    {
                        if (DrpCompanyCountry.SelectedItem != null)
                            DrpCompanyCountry.SelectedItem.Text = vSession.User.Country;
                    }
                }
            }
        }

        private void ShowUploadMessages(string content, string title, MessageTypes type)
        {
            LblFileUploadTitle.Text = title;
            LblFileUploadfMsg.Text = content;
            GlobalMethods.ShowMessageControlDA(UploadMessageAlert, content, type, true, true, true, false, false);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
        }

        private void ResetFields3()
        {
            UcMessageControlSettingsAllert.Visible = false;

            CbxDisableNotif.Checked = false;
            DrpDaysAfter.Items.Clear();
            DrpSecondNot.Items.Clear();
            DrpFirstNot.Items.Clear();

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                StripeUsersAccountsCustomersSettings userSetting = SqlCollaboration.GetStripeCustomerSettingsByVendorId(vSession.User.Id, session);
                if (userSetting != null)
                {
                    for (int i = 1; i < 61; i++)
                    {
                        System.Web.UI.WebControls.ListItem itemDays = new System.Web.UI.WebControls.ListItem();
                        itemDays.Value = i.ToString();
                        itemDays.Text = i.ToString();

                        if (i == userSetting.PaymentDaysAfter)
                            itemDays.Selected = true;

                        DrpDaysAfter.Items.Add(itemDays);

                        if (i < 31)
                        {
                            System.Web.UI.WebControls.ListItem itemFirst = new System.Web.UI.WebControls.ListItem();
                            itemFirst.Value = i.ToString();
                            itemFirst.Text = i.ToString();

                            if (i == userSetting.FirstNotificationDaysBefore)
                                itemFirst.Selected = true;

                            DrpFirstNot.Items.Add(itemFirst);

                            System.Web.UI.WebControls.ListItem itemSec = new System.Web.UI.WebControls.ListItem();
                            itemSec.Value = i.ToString();
                            itemSec.Text = i.ToString();

                            if (i == userSetting.SecondNotificationDaysBefore)
                                itemSec.Selected = true;

                            DrpSecondNot.Items.Add(itemSec);
                        }
                    }
                }
                else
                {
                    for (int i = 1; i < 31; i++)
                    {
                        System.Web.UI.WebControls.ListItem itemDays = new System.Web.UI.WebControls.ListItem();
                        itemDays.Value = i.ToString();
                        itemDays.Text = i.ToString();

                        if (i == 30)
                            itemDays.Selected = true;

                        DrpDaysAfter.Items.Add(itemDays);

                        if (i < 31)
                        {
                            System.Web.UI.WebControls.ListItem itemSec = new System.Web.UI.WebControls.ListItem();
                            itemSec.Value = i.ToString();
                            itemSec.Text = i.ToString();

                            if (i == 2)
                                itemSec.Selected = true;

                            DrpSecondNot.Items.Add(itemSec);

                            System.Web.UI.WebControls.ListItem itemFirst = new System.Web.UI.WebControls.ListItem();
                            itemFirst.Value = i.ToString();
                            itemFirst.Text = i.ToString();

                            if (i == 5)
                                itemFirst.Selected = true;

                            DrpFirstNot.Items.Add(itemFirst);
                        }
                    }
                }
            }
        }

        private void ResetFields2()
        {
            TbxCreditCardNumber.Text = string.Empty;
            TbxCreditCardExpirationDate.Text = string.Empty;
            TbxCreditCardCVC.Text = string.Empty;

            UcMessageAlertBilling.Visible = false;

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divPaymentMethods.Visible = true;

                int methodType = SqlCollaboration.GetVendorPaymentMethod(vSession.User.Id, session);
                if (methodType == (int)PaymentMethodType.None)
                {
                    iPaymentMethods.Attributes["class"] = "icon-2x text-dark-50 flaticon2-warning";
                    RttpPaymentMethods.Text = "You have not added any payment method";
                }
                else if (methodType == (int)PaymentMethodType.CardPayment)
                {
                    iPaymentMethods.Attributes["class"] = "icon-2x text-dark-50 flaticon2-information";
                    RttpPaymentMethods.Text = "You have added your Credit Card as payment method";
                }
                else if (methodType == (int)PaymentMethodType.BankPayment)
                {
                    iPaymentMethods.Attributes["class"] = "icon-2x text-dark-50 flaticon2-information";
                    RttpPaymentMethods.Text = "You have added your Bank Account as payment method";
                }
            }
            else
                divPaymentMethods.Visible = false;
        }

        private void ResetFields()
        {
            TbxFirstName.Text = string.Empty;
            TbxLastName.Text = string.Empty;
            TbxOrganiz.Text = string.Empty;
            TbxEmail.Text = string.Empty;
            TbxCity.Text = String.Empty;
            TbxState.Text = String.Empty;
            TbxPhone.Text = string.Empty;

            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vSession.User.Country, session);
            if (countryCurrency != null && DrpCompanyCountry.SelectedItem != null)
            {
                DrpCompanyCountry.SelectedItem.Value = countryCurrency.CurId;
                DrpCompanyCountry.SelectedItem.Text = countryCurrency.Name;
            }
            else
            {
                if (DrpCompanyCountry.SelectedItem != null)
                    DrpCompanyCountry.SelectedItem.Text = vSession.User.Country;
            }

            bool existCustomer = SqlCollaboration.ExistVendorAsCustomerToAccount(vSession.User.Id, "active", session);
            if (existCustomer)
            {
                divAccountStatus.Visible = true;
                iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-information";
                RttpAccountStatus.Text = "Your account is activated";
            }
            else
            {
                divAccountStatus.Visible = true;
                iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-warning";
                RttpAccountStatus.Text = "Your account is not activated yet";
            }

            UcMessageAlert.Visible = false;
        }

        private void LoadCountries()
        {
            DrpAccountCountry.Items.Clear();
            DrpCompanyCountry.Items.Clear();
            DrpBankCountry.Items.Clear();

            List<ElioCountries> countries = Sql.GetPublicCountries(session);

            System.Web.UI.WebControls.ListItem itm = new System.Web.UI.WebControls.ListItem();
            itm.Text = "Select Country";
            itm.Value = "0";

            DrpAccountCountry.Items.Add(itm);
            DrpCompanyCountry.Items.Add(itm);
            DrpBankCountry.Items.Add(itm);

            foreach (ElioCountries country in countries)
            {
                itm = new System.Web.UI.WebControls.ListItem();

                itm.Value = country.Iso2;
                itm.Text = country.CountryName;
                itm.Selected = vSession.User.Country == country.CountryName;

                DrpAccountCountry.Items.Add(itm);
                DrpCompanyCountry.Items.Add(itm);
                DrpBankCountry.Items.Add(itm);
            }
        }

        public void ShowTab(int tab)
        {
            switch (tab)
            {
                case 1:

                    aCardData.Attributes["class"] = "nav-link active";
                    tab_1_1.Attributes["class"] = "tab-pane fade show active";
                    tab_1_1.Visible = true;

                    aBankData.Attributes["class"] = "nav-link";
                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    tab_1_2.Visible = false;

                    break;

                case 2:

                    aBankData.Attributes["class"] = "nav-link active";
                    tab_1_2.Attributes["class"] = "tab-pane fade show active";
                    tab_1_2.Visible = true;

                    aCardData.Attributes["class"] = "nav-link";
                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    tab_1_1.Visible = false;

                    break;
            }
        }

        # endregion

        #region Grids

        #endregion

        #region Buttons

        protected void BtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    UcMessageAlert.Visible = false;

                    #region Check Fields

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        #region Vendor validation check

                        if (TbxFirstName.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add first name", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add first name", "Warning", MessageTypes.Error);
                            return;
                        }

                        if (TbxLastName.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add last name", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add last name", "Warning", MessageTypes.Error);
                            return;
                        }

                        if (TbxOrganiz.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add company name", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add company name", "Warning", MessageTypes.Error);
                            return;
                        }

                        if (TbxEmail.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please enter Business Email", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please enter Business Email", "Warning", MessageTypes.Error);
                            return;
                        }
                        else
                        {
                            if (!Validations.IsEmail(TbxEmail.Text))
                            {
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please enter a valid Email Address", MessageTypes.Error, true, true, true, true, false);
                                ShowUploadMessages("Please enter a valid Email Address", "Warning", MessageTypes.Error);
                                return;
                            }
                            else
                            {

                            }
                        }

                        if (DrpCompanyCountry.SelectedItem.Value == "0")
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add comments", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please select country", "Warning", MessageTypes.Error);
                            return;
                        }

                        if (TbxCity.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add comments", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add city", "Warning", MessageTypes.Error);
                            return;
                        }
                        
                        if (TbxState.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add comments", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add state", "Warning", MessageTypes.Error);
                            return;
                        }

                        if (TbxPhone.Text == string.Empty)
                        {
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add company website", MessageTypes.Error, true, true, true, true, false);
                            ShowUploadMessages("Please add phone number", "Warning", MessageTypes.Error);
                            return;
                        }

                        #endregion

                        List<StripeUsersAccounts> strAccounts = SqlCollaboration.GetStripeAccountsByVendor(vSession.User.Id, session);

                        if (strAccounts.Count > 0)
                        {
                            int createdCustomersCount = 0;

                            foreach (StripeUsersAccounts strAccount in strAccounts)
                            {
                                if (strAccount != null && !string.IsNullOrEmpty(strAccount.StripeAccountId))
                                {
                                    StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByUserIdAndStripeAccountId(vSession.User.Id, strAccount.StripeAccountId, session);
                                    if (customer == null)
                                    {
                                        Customer strCust = Lib.Services.StripeAPI.StripeAPIService.CreateCustomerToAccountApi(TbxFirstName.Text, TbxLastName.Text, TbxOrganiz.Text, TbxEmail.Text, DrpCompanyCountry.SelectedItem.Text, TbxCity.Text, TbxState.Text, TbxPhone.Text, strAccount.StripeAccountId);
                                        if (!string.IsNullOrEmpty(strCust.Id))
                                        {
                                            customer = new StripeUsersAccountsCustomers();

                                            customer.AccountId = strAccount.Id;
                                            customer.UserId = vSession.User.Id;
                                            customer.StripeAccountId = strAccount.StripeAccountId;
                                            customer.StripeCustomerId = strCust.Id;
                                            customer.CustomerEmail = strCust.Email;
                                            customer.DateCreated = DateTime.Now;
                                            customer.LastUpdated = DateTime.Now;
                                            customer.Status = "active";

                                            DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);
                                            loader.Insert(customer);

                                            createdCustomersCount++;
                                        }
                                    }
                                    else
                                        continue;
                                }
                                else
                                {
                                    ShowUploadMessages("Selected Partner must set his account first, so you can be his client and set up yours after!", "Information", MessageTypes.Info);
                                    return;
                                }
                            }

                            if (createdCustomersCount > 0)
                            {
                                divAccountStatus.Visible = true;
                                iAccountStatus.Attributes["class"] = "icon-2x text-dark-50 flaticon2-information";
                                RttpAccountStatus.Text = "Your account is activated";

                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Your account is created successfully. You can make payments with your Channel Partners.", MessageTypes.Success, true, true, true, true, false);
                            }
                            else
                            {
                                ShowUploadMessages("Company details already exists", "Information", MessageTypes.Info);
                                return;
                            }
                        }
                        else
                        {
                            ShowUploadMessages("None of your partners have set up their account! You can not save your company details yet.", "Information", MessageTypes.Info);
                            return;
                        }
                    }

                    #endregion

                    UpdatePanelContent.Update();
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

        protected void BtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ResetFields();
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

        protected void BtnSaveBillingDetails_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (tab_1_1.Visible)
                        {
                            List<StripeUsersAccounts> strAccounts = SqlCollaboration.GetStripeAccountsByVendor(vSession.User.Id, session);
                            if (strAccounts.Count > 0)
                            {
                                foreach (StripeUsersAccounts strAccount in strAccounts)
                                {
                                    if (strAccount != null)
                                    {
                                        if (TbxCreditCardNumber.Text == string.Empty)
                                        {
                                            ShowUploadMessages("Please enter credit card number", "Warning", MessageTypes.Error);
                                            return;
                                        }

                                        if (TbxCreditCardExpirationDate.Text == string.Empty)
                                        {
                                            ShowUploadMessages("Please enter card expiration month", "Warning", MessageTypes.Error);
                                            return;
                                        }
                                        else
                                        {
                                            if (TbxCreditCardExpirationDate.Text.Split('-').Length > 1)
                                            {
                                                int month = Convert.ToInt32(((TbxCreditCardExpirationDate.Text.Split('-')[1])));
                                                int year = Convert.ToInt32(((TbxCreditCardExpirationDate.Text.Split('-')[0])));

                                                if ((year < DateTime.Now.Year) || ((month <= DateTime.Now.Month) && year <= DateTime.Now.Year))
                                                {
                                                    ShowUploadMessages("Credit Card is expired", "Warning", MessageTypes.Error);
                                                    return;
                                                }
                                            }
                                        }

                                        if (TbxCreditCardCVC.Text == string.Empty)
                                        {
                                            ShowUploadMessages("Please enter card verification value ", "Warning", MessageTypes.Error);
                                            return;
                                        }

                                        StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByUserIdAndStripeAccountId(vSession.User.Id, strAccount.StripeAccountId, session);
                                        if (customer != null)
                                        {
                                            Customer strCustomer = Lib.Services.StripeAPI.StripeAPIService.GetCustomerNewUnderAccountApi(strAccount.StripeAccountId, customer.StripeCustomerId);
                                            if (!string.IsNullOrEmpty(strCustomer.Id))
                                            {
                                                string[] expDates = TbxCreditCardExpirationDate.Text.Split('-').ToArray();

                                                if (!string.IsNullOrEmpty(strCustomer.DefaultSourceId))
                                                {
                                                    try
                                                    {
                                                        Card card = Lib.Services.StripeAPI.StripeAPIService.DeleteCreditCardNewUnderAccountApi(strAccount.StripeAccountId, strCustomer.Id, strCustomer.DefaultSourceId);
                                                        if (card != null)
                                                        {
                                                            if (!(bool)card.Deleted)
                                                            {
                                                                throw new Exception(string.Format("Customer Stripe ID {0} could not delete his previous card on Stripe at {1}", strCustomer.Id, DateTime.Now));
                                                            }
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        Logger.DetailedError(Request.Url.ToString(), ex.Message, string.Format("User ID {0}", vSession.User.Id));
                                                    }
                                                }

                                                Token cardToken = Lib.Services.StripeAPI.StripeAPIService.CreateCardTokenNewUnderAccountApi(strAccount.StripeAccountId, TbxCreditCardNumber.Text.Trim().Replace("-", "").Replace(" ", ""), expDates[1], expDates[0], TbxCreditCardCVC.Text, strCustomer.Description);
                                                if (!string.IsNullOrEmpty(cardToken.Id))
                                                {
                                                    Card card = Lib.Services.StripeAPI.StripeAPIService.CreateCreditCardNewUnderAccountApi(strAccount.StripeAccountId, strCustomer.Id, cardToken.Id);
                                                    if (!string.IsNullOrEmpty(card.Id))
                                                    {
                                                        bool successUpdate = SqlCollaboration.UpdatePaymentMethodTypeToStripeCustomerAccount(strAccount.StripeAccountId, customer.StripeCustomerId, (int)PaymentMethodType.CardPayment, session);
                                                        if (successUpdate)
                                                            continue;
                                                        else
                                                        {
                                                            throw new Exception(string.Format("Payment method could not be updated from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                                        }
                                                    }
                                                    else
                                                        throw new Exception(string.Format("Card could not be created on Stripe from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                                }
                                                else
                                                    throw new Exception(string.Format("Card Token could not be created on Stripe from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                            }
                                            else
                                                throw new Exception(string.Format("Customer from Stripe cound not be found from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                        }
                                        else
                                        {
                                            Customer strCust = Lib.Services.StripeAPI.StripeAPIService.CreateCustomerToAccountApi(TbxFirstName.Text, TbxLastName.Text, TbxOrganiz.Text, TbxEmail.Text, DrpCompanyCountry.SelectedItem.Text, TbxCity.Text, TbxState.Text, TbxPhone.Text, strAccount.StripeAccountId);
                                            if (!string.IsNullOrEmpty(strCust.Id))
                                            {
                                                customer = new StripeUsersAccountsCustomers();

                                                customer.AccountId = strAccount.Id;
                                                customer.UserId = vSession.User.Id;
                                                customer.StripeAccountId = strAccount.StripeAccountId;
                                                customer.StripeCustomerId = strCust.Id;
                                                customer.CustomerEmail = strCust.Email;
                                                customer.DateCreated = DateTime.Now;
                                                customer.LastUpdated = DateTime.Now;
                                                customer.Status = "active";

                                                DataLoader<StripeUsersAccountsCustomers> loader = new DataLoader<StripeUsersAccountsCustomers>(session);
                                                loader.Insert(customer);

                                                string[] expDates = TbxCreditCardExpirationDate.Text.Split('-').ToArray();

                                                Token cardToken = Lib.Services.StripeAPI.StripeAPIService.CreateCardTokenNewUnderAccountApi(strAccount.StripeAccountId, TbxCreditCardNumber.Text.Trim().Replace("-", "").Replace(" ", ""), expDates[1], expDates[0], TbxCreditCardCVC.Text, strCust.Description);
                                                if (!string.IsNullOrEmpty(cardToken.Id))
                                                {
                                                    Card card = Lib.Services.StripeAPI.StripeAPIService.CreateCreditCardNewUnderAccountApi(strAccount.StripeAccountId, strCust.Id, cardToken.Id);
                                                    if (!string.IsNullOrEmpty(card.Id))
                                                    {
                                                        bool successUpdate = SqlCollaboration.UpdatePaymentMethodTypeToStripeCustomerAccount(strAccount.StripeAccountId, customer.StripeCustomerId, (int)PaymentMethodType.CardPayment, session);
                                                        if (successUpdate)
                                                            continue;
                                                        else
                                                        {
                                                            throw new Exception(string.Format("Payment method could not be updated from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                                        }
                                                    }
                                                    else
                                                        throw new Exception(string.Format("Card could not be created on Stripe from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                                }
                                                else
                                                    throw new Exception(string.Format("Card Token could not be created on Stripe from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                            }
                                            else
                                                throw new Exception(string.Format("Customer to Stripe cound not be created from user id {0}, at {1} for stripe account id {2} and stripe customer id {3}", vSession.User.Id, DateTime.Now, strAccount.StripeAccountId, customer.StripeCustomerId));
                                        }
                                    }
                                    else
                                    {
                                        ShowUploadMessages("Selected Partner must set his account first, so you can be his client and set up yours after!", "Information", MessageTypes.Info);
                                        return;
                                    }
                                }

                                ResetFields2();

                                GlobalMethods.ShowMessageControlDA(UcMessageAlertBilling, "Your credit card source is created to Stripe successfully", MessageTypes.Success, true, true, true, true, false);
                            }
                            else
                            {
                                ShowUploadMessages("None of your partners have set up their account!", "Information", MessageTypes.Info);
                                return;
                            }
                        }
                        else if (tab_1_2.Visible)
                        {
                            if (TbxBankAccountNumber.Text == "")
                            {
                                ShowUploadMessages("Please enter IBAN", "Warning", MessageTypes.Error);
                                return;
                            }

                            if (DrpBankCountry.SelectedValue == "0")
                            {
                                ShowUploadMessages("Please select Bank Country", "Warning", MessageTypes.Error);
                                return;
                            }

                            StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByVendorId(vSession.User.Id, session);
                            if (customer != null)
                            {
                                ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(DrpBankCountry.SelectedItem.Text, session);
                                if (countryCurrency != null)
                                {
                                    Token bankToken = Lib.Services.StripeAPI.StripeAPIService.CreateBankAccountTokenApi("checking", TbxBankAccountNumber.Text, "", "company", vSession.User.CompanyName, DrpBankCountry.SelectedValue, countryCurrency.CurrencyId, customer.StripeAccountId);

                                    if (bankToken != null)
                                    {
                                        BankAccount bnkAccount = Lib.Services.StripeAPI.StripeAPIService.CreateBankAccountApi(customer.StripeAccountId, customer.StripeCustomerId, bankToken.Id);
                                        if (bnkAccount != null)
                                        {

                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                                else
                                {

                                }
                            }
                            else
                            {

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
                GlobalMethods.ShowMessageControlDA(UcMessageAlertBilling, "Sorry, something went wrong! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                return;
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnClear2_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetFields2();
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

        protected void BtnCreateStripeAccount_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                    {
                        string stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);

                        if (string.IsNullOrEmpty(stripeAccountId))
                        {
                            if (DrpAccountCountry.SelectedItem.Value == "0")
                            {
                                ShowUploadMessages("Please select country", "Warning", MessageTypes.Error);
                                return;
                            }

                            ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(DrpAccountCountry.SelectedItem.Text, session);
                            if (countryCurrency != null)
                            {
                                Stripe.Account account = Lib.Services.StripeAPI.StripeAPIService.CreateAccountApi(DrpAccountType.SelectedItem.Text.ToLower(), countryCurrency.CurId, vSession.User.Email, "company");

                                if (account != null && !string.IsNullOrEmpty(account.Id))
                                {
                                    StripeUsersAccounts strAccount = new StripeUsersAccounts();
                                    
                                    strAccount.UserId = vSession.User.Id;
                                    strAccount.StripeAccountId = account.Id;
                                    strAccount.DateCreated = DateTime.Now;
                                    strAccount.LastUpdated = DateTime.Now;
                                    strAccount.Status = "active";
                                    strAccount.Email = vSession.User.Email;
                                    strAccount.AccountType = DrpAccountType.SelectedItem.Text.ToLower();
                                    strAccount.CountryIso = countryCurrency.CurId;
                                    strAccount.BussinessType = DrpBusinessType.SelectedItem.Text.ToLower();

                                    DataLoader<StripeUsersAccounts> loaderStr = new DataLoader<StripeUsersAccounts>(session);
                                    loaderStr.Insert(strAccount);

                                    BtnConfigureAccountOnboarding.Visible = true;
                                    BtnCreateStripeAccount.Visible = false;
                                    BtnCancelStripeAccount.Visible = false;
                                 
                                    ShowUploadMessages("Your account is created successfully", "Success", MessageTypes.Success);
                                }
                                else
                                {
                                    throw new Exception("Sorry, something went wrong! Please try again later or contact us.");
                                }
                            }
                            else
                            {
                                throw new Exception("Sorry, something went wrong with your country! Please try again later or contact us.");
                            }
                        }
                        else
                        {
                            throw new Exception("Your account is already created.");
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                if(ex.Message == "Your account is already created.")
                {
                    ShowUploadMessages(ex.Message, "Information", MessageTypes.Info);
                    return;
                }
                else
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    ShowUploadMessages(ex.Message, "Warning", MessageTypes.Error);
                    return;
                }
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCancelStripeAccount_Click(object sender, EventArgs e)
        {
            try
            {
                DrpAccountType.SelectedIndex = -1;
                DrpAccountCountry.SelectedIndex = -1;
                DrpBusinessType.SelectedIndex = -1;

                UcMessageAlert.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnConfigureAccountOnboarding_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    string stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);

                    if (!string.IsNullOrEmpty(stripeAccountId))
                    {
                        AccountLink accountLink = Lib.Services.StripeAPI.StripeAPIService.CreateAccountLinkApi(stripeAccountId);

                        if (accountLink != null && !string.IsNullOrEmpty(accountLink.Url))
                        {
                            //aConfigureAccountOnboarding.Target = "_blank";
                            Response.Redirect(accountLink.Url, false);
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

        protected void aConfigureAccountOnboarding_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    string stripeAccountId = SqlCollaboration.GetStripeAccountID(vSession.User.Id, session);

                    if (!string.IsNullOrEmpty(stripeAccountId))
                    {
                        AccountLink accountLink = Lib.Services.StripeAPI.StripeAPIService.CreateAccountLinkApi(stripeAccountId);

                        if (accountLink != null && !string.IsNullOrEmpty(accountLink.Url))
                        {
                            //aConfigureAccountOnboarding.Target = "_blank";
                            Response.Redirect(accountLink.Url, false);
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

        protected void aCardData_ServerClick(object sender, EventArgs e)
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

        protected void aBankData_ServerClick(object sender, EventArgs e)
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

        protected void BtnClear3_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetFields3();
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

        protected void BtnSaveSettings_Click(object sender, EventArgs e)
        {
            string message = "";

            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {                    
                    UcMessageControlSettingsAllert.Visible = false;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        StripeUsersAccountsCustomers customer = SqlCollaboration.GetStripeAccountsCustomerByVendorId(vSession.User.Id, session);
                        if (customer != null)
                        {                            
                            DataLoader<StripeUsersAccountsCustomersSettings> loader = new DataLoader<StripeUsersAccountsCustomersSettings>(session);

                            StripeUsersAccountsCustomersSettings settings = SqlCollaboration.GetStripeCustomerSettingsByVendorId(vSession.User.Id, session);
                            if (settings != null)
                            {
                                settings.PaymentMethod = CbxMan.Checked ? (int)Lib.Services.StripeAPI.Enums.PaymentMethod.Manual : (int)Lib.Services.StripeAPI.Enums.PaymentMethod.Automatic;
                                settings.PaymentDaysAfter = Convert.ToInt32(DrpDaysAfter.SelectedItem.Text);
                                settings.FirstNotificationDaysBefore = Convert.ToInt32(DrpFirstNot.SelectedItem.Text);
                                settings.SecondNotificationDaysBefore = Convert.ToInt32(DrpSecondNot.SelectedItem.Text);
                                settings.LastUpdated = DateTime.Now;
                                settings.IsActive = CbxDisableNotif.Checked ? 0 : 1;

                                loader.Update(settings);
                                message = "Your Billing Settings are updated successfully";
                            }
                            else
                            {
                                settings = new StripeUsersAccountsCustomersSettings();

                                settings.UserId = vSession.User.Id;
                                settings.CustomerId = customer.StripeCustomerId;
                                settings.PaymentMethod = (int)Lib.Services.StripeAPI.Enums.PaymentMethod.Automatic;
                                settings.PaymentDaysAfter = Convert.ToInt32(DrpDaysAfter.SelectedItem.Text);
                                settings.FirstNotificationDaysBefore = Convert.ToInt32(DrpFirstNot.SelectedItem.Text);
                                settings.SecondNotificationDaysBefore = Convert.ToInt32(DrpSecondNot.SelectedItem.Text);
                                settings.DateCreated = DateTime.Now;
                                settings.LastUpdated = DateTime.Now;
                                settings.IsActive = CbxDisableNotif.Checked ? 0 : 1;

                                loader.Insert(settings);
                                message = "Your Billing Settings are saved successfully";
                            }

                            GlobalMethods.ShowMessageControlDA(UcMessageControlSettingsAllert, message, MessageTypes.Success, true, true, true, true, false);
                        }
                        else
                        {
                            //create customer first
                            message = "You must save your Company Details first";
                            GlobalMethods.ShowMessageControlDA(UcMessageControlSettingsAllert, message, MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Sorry, something went wrong and your Settings could not be saved successfully. Please try again or contact us.";
                GlobalMethods.ShowMessageControlDA(UcMessageControlSettingsAllert, message, MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region Dropdown Lists

        #endregion

        #region CheckBoxes

        protected void Cbxl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                foreach (System.Web.UI.WebControls.ListItem item in Cbxl.Items)
                {
                    if(item.Value == "0")
                    {
                        
                    }
                }
                if (Cbxl.SelectedItem.Value == "1")
                {
                    divStep1.Visible = divStep2.Visible = true;
                }
                else
                {
                    divStep1.Visible = divStep2.Visible = false;
                    Cbxl.SelectedItem.Value = "0";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxMan_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CbxMan.Checked)
                {
                    CbxAut.Checked = false;
                    divStep1.Visible = divStep2.Visible = false;
                    LblBillingSettings.Text = "Step 2: Billing Settings";

                }
                else
                {
                    CbxAut.Checked = true;
                    divStep1.Visible = divStep2.Visible = true;
                    LblBillingSettings.Text = "Step 4: Billing Settings";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxAut_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CbxAut.Checked)
                {
                    CbxMan.Checked = false;
                    divStep1.Visible = divStep2.Visible = true;
                    LblBillingSettings.Text = "Step 4: Billing Settings";
                }
                else
                {
                    CbxMan.Checked = true;
                    divStep1.Visible = divStep2.Visible = false;
                    LblBillingSettings.Text = "Step 2: Billing Settings";
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxDisableNotif_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (CbxDisableNotif.Checked)
                {
                    PnlNotifications.Enabled = false;
                }
                else
                {
                    PnlNotifications.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}