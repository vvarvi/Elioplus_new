using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.CurrencyConverter;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models;
using System.Globalization;
using System.Threading;

namespace WdS.ElioPlus
{
    public partial class DashboardRandstadDealsAddEditPage : System.Web.UI.Page
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
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        if (vSession.User.Id != GlobalMethods.GetRandstadCustomerID())
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit"), false);
                            return;
                        }
                    }
                    else
                    {
                        bool isCustomPartner = SqlCollaboration.IsPartnerOfCustomVendor(GlobalMethods.GetRandstadCustomerID(), vSession.User.Id, session);
                        if (!isCustomPartner)
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration-add-edit"), false);
                            return;
                        }
                    }

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

        private void FixVendorItems()
        {
            divVendorActionsA.Visible = true;

            divResellerActionsA.Visible = false;
        }

        private void FixResellerItems()
        {
            divResellerActionsA.Visible = true;

            divVendorActionsA.Visible = false;
        }

        private void FixPage()
        {
            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();
            //divVendorsArea.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            UpdateStrings();
            SetLinks();

            #region Top Packet Button

            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = true;
            //    LblRenewalHead.Text = "Renewal date: ";

            //    try
            //    {
            //        LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
            //    }
            //    catch (Exception)
            //    {
            //        LblRenewalHead.Visible = LblRenewal.Visible = false;

            //        Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
            //    }
            //}
            //else
            //{
            //    LblRenewalHead.Visible = LblRenewal.Visible = false;
            //}

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            #endregion

            //AllowEdit(vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString());

            //divResellerActionsA.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
            //divVendorActionsA.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            ResetFields();
            LoadDealStatus();
            LoadDealResultStatus();
            LoadDealActiveStatus();
            LoadDealMonthDuration();
            LoadCurrency();

            GlobalDBMethods.FixUserOpenDealsStatus(vSession.User, session);

            if (Request.QueryString["dealVendorViewID"] == null)
            {
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
            }
            else
            {
                int vendorId = Convert.ToInt32(Session[Request.QueryString["dealVendorViewID"]]);
                if (vendorId == 0)
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                TbxMonthDuration.Visible = true;
                DdlMonthDuration.Visible = false;
                //DdlDealStatus.SelectedItem.Value = (Convert.ToInt32(DealStatus.Open).ToString());
                //DdlDealStatus.SelectedItem.Text = DealStatus.Open.ToString();
                //DdlDealStatus.FindItemByValue(Convert.ToInt32(DealStatus.Open).ToString()).Selected = true;
                DdlDealStatus.Enabled = false;

                if (vendorId > 0)
                {
                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                    {
                        LblPartnerHeaderInfo.Text = "Step 1: You have selected Vendor";

                        bool successLoaded = LoadPartnerData(vendorId);
                        if (!successLoaded)
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                        LblCrmInfo.Visible = true;
                        divFreemiumArea.Visible = false;
                        divPremiumArea.Visible = true;
                        DdlForeCasting.Enabled = true;

                        string apiKey = "";
                        bool hasIntegration = Sql.HasUserCrmIntegrationOrRegisterKey(vSession.User.Id, out apiKey, session);
                        if (!hasIntegration)
                        {
                            aGoToIntegrations.Visible = true;
                            BtnGetLeads.Visible = false;
                        }
                        else
                        {
                            if (apiKey == "")
                            {
                                aGoToIntegrations.Visible = true;
                                BtnGetLeads.Visible = false;
                            }
                            else
                            {
                                aGoToIntegrations.Visible = false;
                                BtnGetLeads.Visible = true;
                            }
                        }
                    }
                    else
                    {
                        LblPartnerHeaderInfo.Text = "Step 1: Select your Partner";
                        divVendorsList.Visible = true;
                        divSelectedPartnerHeader.Visible = false;

                        GetCollaborationUsers();

                        BtnSave.Enabled = false;
                        AllowEdit(false);
                        LblCrmInfo.Visible = BtnGetLeads.Visible = false;
                    }
                }
                else
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
            }
        }

        private void LoadCurrency1()
        {
            GlobalMethods.GetDrpCurrencies(DrpCurrency);

            if (DrpCurrency.Items.Count > 0)
            {
                divCurrencyArea.Visible = true;

                DrpCurrency.DataBind();
            }
            else
                divCurrencyArea.Visible = true;
        }

        private void LoadCurrency()
        {
            DrpCurrency.Items.Clear();
            string defaultUserCurId = "";      //Sql.GetUserCurID(vSession.User.Id, session);

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
            {
                ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vSession.User.Country, session);
                if (countryCurrency != null)
                {
                    defaultUserCurId = countryCurrency.CurId;
                }
            }
            else
            {
                defaultUserCurId = Sql.GetUserCurID(vSession.User.Id, session);
                if (defaultUserCurId == "")
                {
                    ElioCurrenciesCountries countryCurrency = Sql.GetCurrencyCountryByCountryName(vSession.User.Country, session);
                    if (countryCurrency != null)
                    {
                        defaultUserCurId = countryCurrency.CurId;
                    }
                }
            }

            List<ElioCurrenciesCountries> currCountries = Sql.GetCurrenciesCountries(session);
            if (currCountries.Count > 0)
            {
                #region Get from DB

                ListItem itm = new ListItem();

                itm.Value = "NO";
                itm.Text = "Select currency";

                DrpCurrency.Items.Add(itm);

                foreach (ElioCurrenciesCountries currency in currCountries)
                {
                    itm = new ListItem();

                    itm.Value = currency.CurId;
                    itm.Text = currency.CurId + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";

                    if ((defaultUserCurId != "" && defaultUserCurId == currency.CurId) || currency.Name.Contains(vSession.User.Country))
                        itm.Selected = true;
                    else if (defaultUserCurId == "" && currency.Name == vSession.User.Country)
                        itm.Selected = true;

                    DrpCurrency.Items.Add(itm);
                }

                DrpCurrency.DataBind();

                #endregion
            }
            else
            {
                #region Get from API

                try
                {
                    int count = 1;
                    List<Country> countryCurr = Lib.Services.CurrencyConverterAPI.CurrencyConverter.ConverterLib.GetAllCountries();
                    if (countryCurr != null && countryCurr.Count > 0)
                    {
                        countryCurr.OrderBy(x => x.Id).ToList();

                        ListItem itm = new ListItem();

                        itm.Value = "NO";
                        itm.Text = "Select currency";
                        DrpCurrency.Items.Add(itm);

                        foreach (Country currency in countryCurr)
                        {
                            itm = new ListItem();

                            itm.Value = currency.Id;
                            itm.Text = currency.Id + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";

                            if (defaultUserCurId != "" && defaultUserCurId == currency.Id)
                                itm.Selected = true;
                            else if (defaultUserCurId == "" && currency.Name == vSession.User.Country)
                                itm.Selected = true;

                            DrpCurrency.Items.Add(itm);

                            Logger.Debug("Count: " + count.ToString() + " --> " + currency.CurrencyId + "-" + currency.Alpha3 + "-" + currency.CurrencyName + "-" + currency.CurrencySymbol + "-" + currency.Id + "-" + currency.Name);
                            count++;

                            #region Insert / Update Elio Currencies Countries

                            try
                            {
                                DataLoader<ElioCurrenciesCountries> loader = new DataLoader<ElioCurrenciesCountries>(session);

                                ElioCurrenciesCountries curCountry = Sql.GetCurrencyCountryByValues(currency.Alpha3, currency.Id, currency.CurrencyId, currency.CurrencyName, currency.Name, session);
                                if (curCountry == null)
                                {
                                    curCountry = new ElioCurrenciesCountries();

                                    curCountry.Alpha3 = currency.Alpha3;
                                    curCountry.CurId = currency.Id;
                                    curCountry.CurrencyId = currency.CurrencyId;
                                    curCountry.CurrencyName = currency.CurrencyName;
                                    curCountry.CurrencySymbol = currency.CurrencySymbol;
                                    curCountry.Name = currency.Name;
                                    curCountry.Sysdate = DateTime.Now;
                                    curCountry.LastUpdate = DateTime.Now;
                                    curCountry.IsPublic = 1;

                                    loader.Insert(curCountry);
                                }
                                else
                                {
                                    curCountry.Alpha3 = currency.Alpha3;
                                    curCountry.CurId = currency.Id;
                                    curCountry.CurrencyId = currency.CurrencyId;
                                    curCountry.CurrencyName = currency.CurrencyName;
                                    curCountry.CurrencySymbol = currency.CurrencySymbol;
                                    curCountry.Name = currency.Name;
                                    curCountry.LastUpdate = DateTime.Now;

                                    loader.Update(curCountry);
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            #endregion
                        }

                        DrpCurrency.DataBind();
                    }
                    else
                        divCurrencyArea.Visible = false;
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion
            }
        }

        private void AllowEdit(bool allow)
        {
            BtnSave.Visible = BtnClear.Visible = allow;
            TbxFirstName.ReadOnly = TbxLastName.ReadOnly = TbxOrganiz.ReadOnly = TbxAddress.ReadOnly = !allow;
            TbxEmail.ReadOnly = TbxOpportunityDescription.ReadOnly = TbxWebsite.ReadOnly = TbxPhone.ReadOnly = TbxProduct.ReadOnly = TbxAmount.ReadOnly = !allow;
            RdpExpectedClosedDate.Enabled = DdlDealResult.Enabled = DdlMonthDuration.Enabled = DrpCurrency.Enabled = allow;
            DdlIsActive.Enabled = false;
            DdlForeCasting.Enabled = allow;

            //DdlDealStatus.Enabled = allow;
            //divDealsResults.Visible = allow;

            if (vSession.User.CompanyType == Types.Vendors.ToString() && Request.QueryString["dealViewID"] != null)
            {
                BtnClear.Visible = false;
                BtnSave.Visible = true;
                TbxFirstName.ReadOnly = TbxLastName.ReadOnly = TbxOrganiz.ReadOnly = TbxAddress.ReadOnly = true;
                TbxEmail.ReadOnly = TbxOpportunityDescription.ReadOnly = TbxWebsite.ReadOnly = TbxPhone.ReadOnly = TbxProduct.ReadOnly = TbxAmount.ReadOnly = true;
                RdpExpectedClosedDate.Enabled = false;
                DdlDealStatus.Enabled = DdlDealResult.Enabled = false;
                DdlIsActive.Enabled = false;
                DdlMonthDuration.Enabled = true;
                //divVendorsArea.Visible = true;
            }
            else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && Convert.ToInt32(Session[Request.QueryString["dealVendorViewID"]]) != 0)
            {
                //divVendorsArea.Visible = true;
                DdlIsActive.Enabled = false;
                DdlMonthDuration.Enabled = false;
            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            aGoToIntegrations.HRef = ControlLoader.Dashboard(vSession.User, "integrations");
        }

        private void UpdateStrings()
        {
            //if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //{
            //    ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //    if (packet != null)
            //    {
            //        LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //    }
            //}
            //else
            //{
            //    LblPricingPlan.Text = "You are currently on a free plan";
            //}

            //LblElioplusDashboard.Text = "";

            //LblDashboard.Text = "Dashboard";

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //if (aBtnGoPremium.Visible)
            //{
            //    LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //    LblPricingPlan.Visible = false;
            //}

            ////LblGoFull.Text = "Complete your registration";
            //LblDashPage.Text = "Deal Registration";
            //LblDashSubTitle.Text = "";
            //LblSelectPlan.Text = "You have to select a Vendor to add deal registration to";
            //LblStatus.Text = "Open/Expired Status";
            //LblIsActive.Text = "Approved/Rejected Status";
            //LblMonthDuration.Text = "Month Duration";
            //LblForeCasting.Text = "Forecasting";
            //LblDealResult.Text = "Won/Lost Status";
            //LblFirstName.Text = "First Name";
            //LblFNameHelp.Text = "Fill in your customer first name";
            //LblLastName.Text = "Last Name";
            //LblLNameHelp.Text = "Fill in your customer's last name";
            //LblOrganiz.Text = "Organisation Name";
            //LblOrganizHelp.Text = "Fill in your customer's organisation/company name";
            //LblAddress.Text = "Location / Address";
            //LblAddressHelp.Text = "Fill in your customer's location/address of the company";
            //LblEmail.Text = "Business Email";
            //LblEmailHelp.Text = "Fill in your customer's business email";
            //LblWebsite.Text = "Website";
            //LblWebsiteHelp.Text = "Fill in your customer's website";
            //LblOpportunityDescription.Text = "Opportunity Description";
            //LblOpportunityDescriptionHelp.Text = "Fill in your customer's description";
            //LblPhone.Text = "Phone";
            //LblPhoneHelp.Text = "Fill in your customer's phone";
            //LblProduct.Text = "Product";
            //LblProductHelp.Text = "Fill in your customer's product";
            //LblAmount.Text = "Amount";
            //LblAmountHelp.Text = "Fill in your deal's amount";
            //LblExpectedClosedDate.Text = "Expected closed date of deal (mm/dd/yyyy)";
            //LblExpectedClosedDateHelp.Text = "Fill in your customer's deal expected closed date";
        }

        private void ShowPopUpModalWithText(string title, string content, MessageTypes type)
        {
            LblMessageTitle.Text = title;
            GlobalMethods.ShowMessageControlDA(UcMessageAlert, content, type, true, true, true, false, false);

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
        }

        private void ResetPanelItems()
        {

        }

        private void ResetFields()
        {
            //HdnDealRegistrationId.Value = "0";
            //HdnCollaborationId.Value = "0";
            //HdnResellerId.Value = "0";
            //HdnVendorId.Value = "0";

            //TbxUserId.Text = string.Empty;
            TbxFirstName.Text = string.Empty;
            TbxLastName.Text = string.Empty;
            TbxOrganiz.Text = string.Empty;
            TbxAddress.Text = string.Empty;
            TbxOpportunityDescription.Text = string.Empty;
            TbxEmail.Text = string.Empty;
            TbxPhone.Text = string.Empty;
            TbxAmount.Text = string.Empty;
            TbxWebsite.Text = string.Empty;
            TbxProduct.Text = string.Empty;
            TbxMonthDuration.Text = string.Empty;
            RdpExpectedClosedDate.SelectedDate = null;
            DdlForeCasting.SelectedIndex = -1;

            UcMessageAlert.Visible = false;
        }

        private void GetCollaborationUsers()
        {
            List<ElioUsers> users = SqlCollaboration.GetCollaborationUsersByUserType(vSession.User, CollaborateInvitationStatus.Confirmed.ToString(), session);

            if (users.Count > 0)
            {
                divVendorsList.Visible = true;

                DrpPartners.Items.Clear();

                ListItem item = new ListItem();
                item.Value = "0";
                item.Text = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "Select Reseller" : "Select Vendor";

                DrpPartners.Items.Add(item);

                foreach (ElioUsers user in users)
                {
                    item = new ListItem();
                    item.Value = user.Id.ToString();
                    item.Text = user.CompanyName;

                    DrpPartners.Items.Add(item);
                }
            }
            else
            {
                divVendorsList.Visible = false;
            }
        }

        private void LoadDealStatus()
        {
            DdlDealStatus.Items.Clear();

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "-- Select your deal's status --";

            DdlDealStatus.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealStatus.Open).ToString();
            item.Text = DealStatus.Open.ToString();
            item.Selected = true;
            DdlDealStatus.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealStatus.Closed).ToString();
            item.Text = DealStatus.Closed.ToString();
            DdlDealStatus.Items.Add(item);

            item = new ListItem();
            item.Value = Convert.ToInt32(DealStatus.Expired).ToString();
            item.Text = DealStatus.Expired.ToString();
            DdlDealStatus.Items.Add(item);

            //DdlDealStatus.FindItemByText(DealStatus.Open.ToString()).Selected = true;
        }

        private void LoadDealResultStatus()
        {
            DdlDealResult.Items.Clear();

            ListItem item = new ListItem();

            item.Value = Convert.ToInt32(DealResultStatus.Pending).ToString();
            item.Text = DealResultStatus.Pending.ToString();
            item.Selected = true;
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Qualified).ToString();
            item.Text = RandstadDealResultStatus.Qualified.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Solution_Developed).ToString();
            item.Text = RandstadDealResultStatus.Solution_Developed.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Proposal_Presented).ToString();
            item.Text = RandstadDealResultStatus.Proposal_Presented.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Negotiation_and_Close).ToString();
            item.Text = RandstadDealResultStatus.Negotiation_and_Close.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Awarded_Contract_Pending).ToString();
            item.Text = RandstadDealResultStatus.Awarded_Contract_Pending.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(RandstadDealResultStatus.Won).ToString();
            item.Text = RandstadDealResultStatus.Won.ToString();
            DdlDealResult.Items.Add(item);

            item = new ListItem();
            item.Value = Convert.ToInt32(DealResultStatus.Lost).ToString();
            item.Text = DealResultStatus.Lost.ToString();
            DdlDealResult.Items.Add(item);
        }

        private void LoadDealActiveStatus()
        {
            DdlIsActive.Items.Clear();

            ListItem item = new ListItem();

            item.Value = Convert.ToInt32(DealActivityStatus.NotConfirmed).ToString();
            item.Text = DealActivityStatus.NotConfirmed.ToString();
            item.Selected = true;

            DdlIsActive.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealActivityStatus.Approved).ToString();
            item.Text = DealActivityStatus.Approved.ToString();

            DdlIsActive.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealActivityStatus.Rejected).ToString();
            item.Text = DealActivityStatus.Rejected.ToString();
            DdlIsActive.Items.Add(item);
        }

        private void LoadDealMonthDuration()
        {
            DdlMonthDuration.Items.Clear();

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "-- Select this deal's month duration --";

            DdlMonthDuration.Items.Add(item);

            for (int i = 1; i <= 12; i++)
            {
                item = new ListItem();

                item.Value = i.ToString();
                item.Text = (i == 1) ? i.ToString() + " month" : i.ToString() + " months";
                DdlMonthDuration.Items.Add(item);
            }
        }

        private bool LoadPartnerData(int partnerId)
        {
            ElioUsers partner = Sql.GetUserById(partnerId, session);
            if (partner != null)
            {
                LblDealPartnerName.Text = partner.CompanyName;
                //LblPhoneContent.Text = (!string.IsNullOrEmpty(partner.Phone)) ? partner.Phone : "-";
                LblAddressContent.Text = partner.Address;
                aWebsiteContent.HRef = partner.WebSite;
                aWebsiteContent.Target = "_blank";
                LblWebsiteContent.Text = partner.WebSite;
                aEmailContent.HRef = "mailto:" + partner.Email;
                LblEmailContent.Text = partner.Email;
                //aMoreDetails.HRef = ControlLoader.PersonProfile(partner);
                //aMoreDetails.Target = "_blank";
                //LblMoreDetailsContent.Text = "view profile";
                ImgCompanyLogo.ImageUrl = partner.CompanyLogo;
                divSelectedPartnerHeader.Visible = true;

                ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(partnerId, session);

                if (monthSettings != null)
                {
                    DdlMonthDuration.SelectedIndex = -1;

                    DdlMonthDuration.SelectedItem.Value = monthSettings.DealDurationSetting.ToString();
                    //DdlMonthDuration.FindItemByValue(monthSettings.DealDurationSetting.ToString()).Selected = true;
                    TbxMonthDuration.Text = monthSettings.DealDurationSetting.ToString();
                }
                else
                {
                    TbxMonthDuration.Text = "6";
                }
            }

            return partner != null;
        }

        private void LoadAPIs()
        {
            DataTable crms = Sql.GetAllCrmUserIntegrationsTbl(vSession.User.Id, session);           //GetPublicCrmIntegrationsTable(session);

            if (crms.Rows.Count > 0)
            {
                DrpUserCrmList.Items.Clear();

                DataRow row = crms.NewRow();
                row["id"] = "0";
                row["crm_name"] = "Select CRM";
                row["crm_description"] = "";
                row["date_created"] = DateTime.Now;
                row["is_public"] = "1";

                crms.Rows.Add(row);
                crms.DefaultView.Sort = "id";

                DrpUserCrmList.DataValueField = "id";
                DrpUserCrmList.DataTextField = "crm_name";
                DrpUserCrmList.DataSource = crms;

                DrpUserCrmList.DataBind();
            }
            else
            {
                divFreemiumArea.Visible = true;
                divPremiumArea.Visible = false;
                GlobalMethods.ShowMessageControlDA(FreemiumMessageControl, "This service is not available yet", MessageTypes.Info, true, true, false);
            }
        }

        #endregion

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

                    int dealId = -1;
                    int vendorId = -1;
                    int resellerId = -1;
                    DealActionMode mode = DealActionMode.INSERT;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        vendorId = vSession.User.Id;
                        resellerId = Convert.ToInt32(DrpPartners.SelectedValue);
                    }
                    else
                    {
                        vendorId = GlobalMethods.GetRandstadCustomerID();
                        resellerId = vSession.User.Id;
                    }

                    #region Delete

                    //if (Request.QueryString["dealViewID"] != null)
                    //    dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]);
                    //else
                    //{
                    //    if (Request.QueryString["dealVendorViewID"] != null && (Session[Request.QueryString["dealVendorViewID"]]).ToString() != "0")
                    //    {
                    //        vendorId = Convert.ToInt32(Session[Request.QueryString["dealVendorViewID"]]);
                    //        if (vendorId == 0)
                    //        {
                    //            if (vSession.User.CompanyType == Types.Vendors.ToString())
                    //                vendorId = vSession.User.Id;
                    //            else
                    //            {
                    //                if (divVendorsList.Visible)
                    //                {
                    //                    if (DrpPartners.SelectedItem.Value != "0")
                    //                        vendorId = Convert.ToInt32(DrpPartners.SelectedItem.Value);
                    //                    else
                    //                    {
                    //                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                    //                        return;
                    //                    }
                    //                }
                    //                else
                    //                {
                    //                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                    //                    return;
                    //                }
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (divVendorsList.Visible)
                    //        {
                    //            if(DrpPartners.SelectedItem.Value!="0")
                    //                vendorId = Convert.ToInt32(DrpPartners.SelectedItem.Value);
                    //            else
                    //            {
                    //                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                    //                return;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                    //            return;
                    //        }
                    //    }
                    //}

                    #endregion

                    mode = (dealId == -1) ? DealActionMode.INSERT : DealActionMode.UPDATE;
                    string domain = "";

                    #region Check Fields

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        #region Vendor validation check

                        if (DrpPartners.SelectedValue == "0")
                        {
                            ShowPopUpModalWithText("Warning", "Please select one partner to attach the new deal", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select one partner to attach the new deal", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }

                        #endregion
                    }

                    #region Reseller validation check

                    if (DdlDealStatus.SelectedValue == "0")
                    {
                        ShowPopUpModalWithText("Warning", "Please select Deal's status", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select Deal's status", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (DdlDealResult.SelectedValue == "0")
                    {
                        ShowPopUpModalWithText("Warning", "Please select Deal's stage status", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select Deal's stage status", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxFirstName.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Customer first name", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Customer first name", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxLastName.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Customer last name", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Customer last name", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxOrganiz.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Organisation name", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Organisation name", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxAddress.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Organisation location", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Organisation location", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxEmail.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please enter Email", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please enter Email", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }
                    else
                    {
                        if (!Validations.IsEmail(TbxEmail.Text))
                        {
                            ShowPopUpModalWithText("Warning", "Please enter a valid Email Address", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please enter a valid Email Address", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }

                        domain = GlobalMethods.GetCompanyDomainFromEmailAddress(TbxEmail.Text);        //GlobalMethods.GetCompanyDomainFromWebsite_v2(TbxWebsite.Text.Trim());

                        if (domain == "")
                        {
                            ShowPopUpModalWithText("Warning", "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }

                    if (TbxWebsite.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Organisation website", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Organisation website", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (TbxPhone.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Organisation phone", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Organisation phone", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    //if (TbxProduct.Text == string.Empty)
                    //{
                    //    divOpportError.Visible = divOpportErrorBottom.Visible = true;
                    //    LblOpportErrorCont.Text = LblOpportErrorContBottom.Text = "Please add Organisation product";
                    //    return;
                    //}

                    if (TbxAmount.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add deal's amount", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add deal's amount", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }
                    else
                    {
                        try
                        {
                            decimal amount;
                            if (TbxAmount.Text != string.Empty && (!Validations.IsNumeric(TbxAmount.Text.Trim()) || !Decimal.TryParse(TbxAmount.Text, out amount)))
                            {
                                ShowPopUpModalWithText("Warning", "Please add a valid deal's amount", MessageTypes.Error);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add a valid deal's amount", MessageTypes.Error, true, true, true, true, false);

                                return;
                            }

                            if (TbxAmount.Text != "" && Validations.IsNegativeNumber(TbxAmount.Text))
                            {
                                ShowPopUpModalWithText("Warning", "Please add positive deal's amount", MessageTypes.Error);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add positive deal's amount", MessageTypes.Error, true, true, true, true, false);

                                return;
                            }
                        }
                        catch (FormatException ex)
                        {
                            ShowPopUpModalWithText("Warning", "Please add a valid deal's amount", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add a valid deal's amount ", MessageTypes.Error, true, true, true, true, false);
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            return;
                        }
                    }

                    if (divCurrencyArea.Visible)
                    {
                        if (DrpCurrency.SelectedValue == "0")
                        {
                            ShowPopUpModalWithText("Warning", "Please select your currency", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select your currency", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }

                    if (TbxOpportunityDescription.Text == string.Empty)
                    {
                        ShowPopUpModalWithText("Warning", "Please add Organisation description", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Organisation description", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (DdlForeCasting.SelectedValue == "0")
                    {
                        ShowPopUpModalWithText("Warning", "Please add Deal Forecasting", MessageTypes.Error);
                        //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add Deal Forecasting", MessageTypes.Error, true, true, true, true, false);

                        return;
                    }

                    if (mode == DealActionMode.INSERT)
                    {
                        if (RdpExpectedClosedDate.SelectedDate == null)
                        {
                            ShowPopUpModalWithText("Warning", "Please add deal expected closed date", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add deal expected closed date", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                        else
                        {
                            if (Convert.ToDateTime(RdpExpectedClosedDate.SelectedDate) < DateTime.Now)
                            {
                                ShowPopUpModalWithText("Warning", "Please add deal expected closed date after today", MessageTypes.Error);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please add deal expected closed date after today", MessageTypes.Error, true, true, true, true, false);

                                return;
                            }
                        }
                    }

                    #endregion

                    #endregion

                    DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                    if (mode == DealActionMode.UPDATE)
                    {
                        if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && Sql.ExistDomainToOtherDealByReseller(dealId, vSession.User.Id, domain, session))
                        {
                            ShowPopUpModalWithText("Warning", "Sorry, but this domain is already saved by you to other deal!", MessageTypes.Info);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but this domain is already saved by you to other deal!", MessageTypes.Info, true, true, true, true, false);

                            return;
                        }

                        #region Update Deal

                        ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);
                        if (deal != null)
                        {
                            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                #region Reseller

                                //deal.VendorId = vendorId;
                                deal.ResellerId = vSession.User.Id;
                                //deal.Status = Convert.ToInt32(DdlOppStatus.SelectedItem.Value);
                                deal.LastName = TbxLastName.Text;
                                deal.FirstName = TbxFirstName.Text;
                                deal.CompanyName = TbxOrganiz.Text;
                                deal.Address = TbxAddress.Text;
                                deal.Email = TbxEmail.Text;
                                deal.Domain = domain;
                                deal.Website = TbxWebsite.Text;
                                deal.Phone = TbxPhone.Text;
                                deal.Amount = TbxAmount.Text != "" ? Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", TbxAmount.Text)) : 0;
                                deal.Description = TbxOpportunityDescription.Text;
                                deal.LastUpdate = DateTime.Now;
                                deal.ExpectedClosedDate = Convert.ToDateTime(RdpExpectedClosedDate.SelectedDate);
                                deal.Product = TbxProduct.Text;
                                deal.IsPublic = 1;
                                //deal.IsActive = 0;
                                deal.MonthDuration = Convert.ToInt32(TbxMonthDuration.Text);       //Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);

                                if (DdlForeCasting.SelectedValue != "0")
                                    deal.ForecastingPercent = DdlForeCasting.SelectedValue;
                                else
                                    deal.ForecastingPercent = null;

                                deal.DealResult = DdlDealResult.SelectedItem.Text;

                                if (deal.DealResult != DealResultStatus.Pending.ToString())
                                {
                                    deal.Status = Convert.ToInt32(DealStatus.Closed);

                                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && deal.ResellerId == vSession.User.Id)
                                    {
                                        int isNew = 1;
                                        if (deal.IsActive != (int)DealActivityStatus.NotConfirmed && deal.IsNew == isNew)
                                        {
                                            #region Set New Deal as Viewed

                                            deal.IsNew = 0;
                                            deal.DateViewed = DateTime.Now;

                                            //DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);
                                            //loader.Update(deal);

                                            #endregion
                                        }
                                    }

                                    try
                                    {
                                        ElioUsers vendor = Sql.GetUserById(deal.VendorId, session);
                                        if (vendor != null)
                                            EmailSenderLib.SendNewDealRegistrationWonLostEmail(vendor.Email, vSession.User.CompanyName, DdlDealResult.SelectedItem.Text, deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                        else
                                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, accepted or rejected new registration deal at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending notification Email");
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        throw ex;
                                    }
                                }

                                #endregion
                            }
                            else if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                #region Vendor

                                deal.Status = Convert.ToInt32(DdlDealStatus.SelectedItem.Value);
                                deal.LastUpdate = DateTime.Now;
                                deal.IsActive = Convert.ToInt32(DdlIsActive.SelectedItem.Value);
                                deal.MonthDuration = (DdlMonthDuration.Visible) ? Convert.ToInt32(DdlMonthDuration.SelectedItem.Value) : Convert.ToInt32(TbxMonthDuration.Text);

                                #endregion
                            }

                            loader.Update(deal);

                            ShowPopUpModalWithText("Success", "Deal changes were updated successfully!", MessageTypes.Success);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal changes were updated successfully!", MessageTypes.Success, true, true, true, true, false);
                        }

                        #endregion
                    }
                    else if (mode == DealActionMode.INSERT)
                    {
                        if (vendorId > 0 && resellerId > 0)
                        {
                            if (domain != "email.com" && domain != "gmail.com" && domain != "hotmail.com" && domain != "yahoo.com" && domain != "yahoo.gr" && domain != "outlook.com" && domain != "outlook.gr")
                            {
                                if (Sql.ExistDealForVendor(vendorId, resellerId, domain, session))
                                {
                                    ShowPopUpModalWithText("Warning", "Sorry, but this deal is already saved by another reseller!", MessageTypes.Info);
                                    //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but this deal is already saved by another reseller!", MessageTypes.Info, true, true, true, true, false);

                                    return;
                                }

                                if (Sql.ExistDealByReseller(vendorId, resellerId, domain, session))
                                {
                                    ShowPopUpModalWithText("Warning", "Sorry, but this deal is already saved by you!", MessageTypes.Info);
                                    //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, but this deal is already saved by you!", MessageTypes.Info, true, true, true, true, false);

                                    return;
                                }
                            }

                            int collaborationId = SqlCollaboration.GetCollaborationId(vendorId, resellerId, session);

                            if (collaborationId != -1)
                            {
                                #region Insert new Deal

                                ElioRegistrationDeals deal = new ElioRegistrationDeals();

                                deal.CollaborationVendorResellerId = collaborationId;
                                deal.VendorId = vendorId;
                                deal.ResellerId = resellerId;
                                deal.Status = (int)DealStatus.Open;  //Convert.ToInt32(DdlOppStatus.SelectedItem.Value);
                                deal.LastName = TbxLastName.Text;
                                deal.FirstName = TbxFirstName.Text;
                                deal.CompanyName = TbxOrganiz.Text;
                                deal.Address = TbxAddress.Text;
                                deal.Email = TbxEmail.Text;
                                deal.Domain = domain;
                                deal.Website = TbxWebsite.Text;
                                deal.Phone = TbxPhone.Text;
                                deal.Amount = TbxAmount.Text != "" ? Convert.ToDecimal(string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", TbxAmount.Text)) : 0;
                                deal.Description = TbxOpportunityDescription.Text;
                                deal.CreatedDate = DateTime.Now;
                                deal.LastUpdate = DateTime.Now;
                                deal.DateViewed = null;
                                deal.ExpectedClosedDate = Convert.ToDateTime(RdpExpectedClosedDate.SelectedDate);
                                deal.Product = (TbxProduct.Text.Trim() != "") ? TbxProduct.Text : "";
                                deal.IsPublic = 1;
                                deal.IsNew = 1;
                                deal.IsActive = (int)DealActivityStatus.NotConfirmed;
                                deal.DealResult = DdlDealResult.SelectedItem.Text;
                                deal.CreatedByUserId = vSession.User.Id;

                                if (divCurrencyArea.Visible)
                                    deal.CurId = DrpCurrency.SelectedValue;

                                if (DdlForeCasting.SelectedValue != "0")
                                    deal.ForecastingPercent = DdlForeCasting.SelectedValue;

                                ElioRegistrationDealsVendorSettings monthSettings = Sql.GetVendorDealMonthSettings(vendorId, session);
                                if (monthSettings != null)
                                    deal.MonthDuration = monthSettings.DealDurationSetting; // Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);
                                else
                                    deal.MonthDuration = 6;

                                loader.Insert(deal);

                                string dealReceiverCompanyName = (vSession.User.CompanyType == Types.Vendors.ToString()) ? DrpPartners.SelectedItem.Text : LblDealPartnerName.Text;

                                FixPage();
                                //BtnSave.Visible = false;
                                //BtnClear.Visible = false;
                                //AllowEdit(false);

                                ShowPopUpModalWithText("Success", dealReceiverCompanyName != "" ? "The Deal has been sent to " + dealReceiverCompanyName + " successfully!" : "The Deal has been sent successfully!", MessageTypes.Success);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "The Deal has been sent to " + LblDealPartnerName.Text + " successfully!", MessageTypes.Success, true, true, true, true, false);

                                try
                                {
                                    GlobalDBMethods.SaveDealMonthDurationSetting(deal.VendorId, 6, session);
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError("DashboardDealRegistrationAddEdit.aspx --> ERROR:GlobalDBMethods.SaveDealMonthDurationSetting()", ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                #endregion

                                try
                                {
                                    if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                                    {
                                        ElioUsers user = Sql.GetUserById(deal.VendorId, session);
                                        if (user != null)
                                            EmailSenderLib.SendNewDealRegistrationEmail(user.Email, vSession.User.CompanyName, false, vSession.Lang, session);
                                        else
                                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded a new registration deal at {1}, but no vendor email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardRandstadDealsAddEdit.aspx --> ERROR sending notification Email");
                                    }
                                    else
                                    {
                                        ElioUsers user = Sql.GetUserById(deal.ResellerId, session);
                                        if (user != null)
                                            EmailSenderLib.SendNewDealRegistrationEmail(user.Email, vSession.User.CompanyName, false, vSession.Lang, session);
                                        else
                                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded a new registration deal at {1}, but no vendor email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardRandstadDealsAddEdit.aspx --> ERROR sending notification Email");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    throw ex;
                                }
                            }
                            else
                            {
                                ShowPopUpModalWithText("Warning", "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error);
                                //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                                return;
                            }
                        }
                        else
                        {
                            ShowPopUpModalWithText("Warning", "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error);
                            //GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
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

        protected void BtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ResetFields();
                    LoadDealStatus();
                    LoadDealResultStatus();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnReject_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["dealViewID"] != null)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]), session);
                        if (deal != null)
                        {
                            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                            deal.IsActive = Convert.ToInt32(DealActivityStatus.Rejected);
                            deal.Status = Convert.ToInt32(DealStatus.Closed);
                            deal.DealResult = RandstadDealResultStatus.Lost.ToString();
                            deal.MonthDuration = Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);
                            deal.LastUpdate = DateTime.Now;

                            loader.Update(deal);

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal " + DealActivityStatus.Rejected.ToString() + " successfully!", MessageTypes.Success, true, true, true, true, false);
                            
                            LoadDealActiveStatus();

                            ListItem item = new ListItem();

                            item.Value = Convert.ToInt32(DealActivityStatus.Rejected).ToString();
                            item.Text = DealActivityStatus.Rejected.ToString();
                            DdlIsActive.Items.Add(item);
                            DdlDealResult.SelectedItem.Value = deal.DealResult.ToString();
                            //DdlDealResult.FindItemByValue(deal.IsActive.ToString()).Selected = true;

                            BtnReject.Visible = false;
                            BtnApprove.Visible = true;

                            try
                            {
                                ElioUsers reseller = Sql.GetUserById(deal.ResellerId, session);
                                if (reseller != null)
                                    EmailSenderLib.SendNewDealRegistrationAcceptRejectEmail(reseller.Email, vSession.User.CompanyName, reseller.CompanyName, "Rejected", deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, rejected new registration deal at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending notification Email");
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                throw ex;
                            }
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void BtnApprove_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["dealViewID"] != null)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(Convert.ToInt32(Session[Request.QueryString["dealViewID"].ToString()]), session);
                        if (deal != null)
                        {
                            DataLoader<ElioRegistrationDeals> loader = new DataLoader<ElioRegistrationDeals>(session);

                            deal.IsActive = Convert.ToInt32(DealActivityStatus.Approved);
                            deal.MonthDuration = Convert.ToInt32(DdlMonthDuration.SelectedItem.Value);
                            deal.LastUpdate = DateTime.Now;

                            loader.Update(deal);

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal " + DealActivityStatus.Approved.ToString() + " successfully!", MessageTypes.Success, true, true, true, true, false);
                            
                            LoadDealActiveStatus();

                            ListItem item = new ListItem();

                            item.Value = Convert.ToInt32(DealActivityStatus.Approved).ToString();
                            item.Text = DealActivityStatus.Approved.ToString();
                            DdlIsActive.Items.Add(item);
                            DdlIsActive.SelectedItem.Value = deal.IsActive.ToString();
                            //DdlIsActive.FindItemByValue(deal.IsActive.ToString()).Selected = true;

                            BtnApprove.Visible = false;
                            BtnReject.Visible = true;

                            try
                            {
                                ElioUsers reseller = Sql.GetUserById(deal.ResellerId, session);
                                if (reseller != null)
                                    EmailSenderLib.SendNewDealRegistrationAcceptRejectEmail(reseller.Email, vSession.User.CompanyName, reseller.CompanyName, "Approved", deal.CompanyName, vSession.LoggedInSubAccountRoleID > 0, vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, approved new registration deal at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending notification Email");
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, something went wrong. Please try again later, or contact with us!", MessageTypes.Error, true, true, true, true, false);

                            return;
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void BtnGetLeads_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    #region Fix Data

                    LoadAPIs();
                    divCrmErrorMessage.Visible = false;
                    LblLeadCrmErrorMessage.Text = "";
                    DrpUserCrmList.SelectedIndex = -1;
                    //divApiKeyArea.Visible = false;
                    divSearchLeadInfo.Visible = false;

                    #endregion

                    if (DrpUserCrmList.Items.Count == 0)
                    {
                        #region No Integrations
                        
                        //Response.Write("<script>");
                        //Response.Write("window.open('" + ControlLoader.Dashboard(vSession.User, "integrations") + "')");
                        //Response.Write("</script>");
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                        return;

                        #endregion
                    }
                    else
                    {
                        if (DrpUserCrmList.Items.Count == 2)
                        {
                            ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegration(vSession.User.Id, session);

                            if (integration == null || (integration != null && string.IsNullOrEmpty(integration.UserApiKey)))
                            {
                                #region Go Add Integration (API KEY)

                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                                return;

                                #endregion
                            }
                            else
                            {
                                if (Request.QueryString["dealViewID"] == null)
                                {
                                    #region New Deal

                                    #region Show search by email area

                                    divFreemiumArea.Visible = false;
                                    divPremiumArea.Visible = true;
                                    divCrmListArea.Visible = false;
                                    divSearchLeadInfo.Visible = true;
                                    TbxLeadEmail.Text = "";

                                    UpdatePanel8.Update();

                                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenLeadsServiceModal();", true);

                                    #endregion

                                    #endregion
                                }
                                else
                                {
                                    #region View - Old Deal

                                    int leadId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                                    if (leadId > 0)
                                    {
                                        ElioRegistrationDeals deal = Sql.GetDealById(leadId, session);

                                        if (deal != null)
                                        {
                                            #region Send New Lead To CRM

                                            string crmResponse = Lib.Services.CRMs.HubspotAPI.HubspotService.CreateOrUpdateContactLead(integration.UserApiKey, deal, null);

                                            if (crmResponse != System.Net.HttpStatusCode.OK.ToString())
                                            {
                                                if (crmResponse == System.Net.HttpStatusCode.Conflict.ToString().ToUpper())
                                                {
                                                    #region Lead exists in CRM

                                                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal already exists in your CRM.", MessageTypes.Info, true, true, true, true, false);
                                                    
                                                    return;

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Bad response

                                                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                                    
                                                    Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR creating new lead to CRM with response: " + crmResponse, string.Format("deal with ID: {0} could not be created to CRM by user: {1}, at {2} with bad response: {3}", deal.Id, vSession.User.Id, DateTime.Now.ToString(), crmResponse));

                                                    #endregion
                                                }
                                            }
                                            else
                                            {
                                                #region Save connection Elio-deal with Crm-deal

                                                try
                                                {
                                                    bool exists = Sql.ExistCrmUserDeal(integration.CrmIntegrationId, deal.Id, session);
                                                    if (!exists)
                                                    {
                                                        ElioCrmUserDeals crmDeal = new ElioCrmUserDeals();

                                                        crmDeal.CrmIntegrationId = integration.CrmIntegrationId;
                                                        crmDeal.CrmDeadId = "";
                                                        crmDeal.DealId = deal.Id;
                                                        crmDeal.DateInsert = DateTime.Now;
                                                        crmDeal.LastUpdate = DateTime.Now;
                                                        crmDeal.IsActive = 1;

                                                        DataLoader<ElioCrmUserDeals> loader = new DataLoader<ElioCrmUserDeals>(session);
                                                        loader.Insert(crmDeal);
                                                    }
                                                }
                                                catch (Exception ex)
                                                {
                                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                                }

                                                #endregion

                                                #region Success lead create/update to CRM

                                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal was sent/updated to your CRM successfully", MessageTypes.Success, true, true, true, true, false);
                                                
                                                #endregion
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            #region Lead not found By ID

                                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                            
                                            Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM", string.Format("deal with ID: {0} could not be send to CRM by user: {1}, at {2}", deal.Id, vSession.User.Id, DateTime.Now.ToString()));

                                            #endregion
                                        }
                                    }
                                    else
                                    {
                                        #region No Lead found by Session URL

                                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                        
                                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM because it could not be found by session url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                                        #endregion
                                    }

                                    #endregion
                                }
                            }
                        }
                        else
                        {
                            #region More than one integrations

                            divFreemiumArea.Visible = false;
                            divPremiumArea.Visible = true;

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenLeadsServiceModal();", true);

                            #endregion
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

        protected void BtnGetData_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divCrmErrorMessage.Visible = false;
                LblLeadCrmErrorMessage.Text = "";

                ElioCrmUserIntegrations userCrmIntegration = null;

                if (divCrmListArea.Visible)
                {
                    if (DrpUserCrmList.SelectedItem.Value != "0")
                    {
                        userCrmIntegration = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                    }
                    else
                    {
                        divCrmErrorMessage.Visible = true;
                        LblLeadCrmErrorMessage.Text = "Please select CRM";
                        return;
                    }
                }
                else
                {
                    userCrmIntegration = Sql.GetUserCrmIntegration(vSession.User.Id, session);
                }

                if (userCrmIntegration == null)
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Sorry, something went wrong. Please try again later";
                    return;
                }

                if (TbxLeadEmail.Text.Trim() == "")
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Please fill deal's email address";
                    return;
                }
                else
                {
                    if (TbxLeadEmail.Text.Trim() != "" && !Validations.IsEmail(TbxLeadEmail.Text.Trim()))
                    {
                        divCrmErrorMessage.Visible = true;
                        LblLeadCrmErrorMessage.Text = "Please add a valid email address";
                        return;
                    }
                }

                ElioRegistrationDeals lead = Lib.Services.CRMs.HubspotAPI.HubspotService.GetDealDetailByEmail(vSession.User.Id, userCrmIntegration.UserApiKey, TbxLeadEmail.Text, session);
                if (lead != null)
                {
                    TbxFirstName.Text = lead.FirstName;
                    TbxLastName.Text = lead.LastName;
                    TbxOrganiz.Text = lead.CompanyName;
                    TbxEmail.Text = lead.Email;
                    TbxWebsite.Text = lead.Website;
                    TbxPhone.Text = lead.Phone;
                    TbxAmount.Text = (lead.Amount > 0) ? string.Format("{0:000" + Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator + "00}", lead.Amount) : "0";
                    TbxOpportunityDescription.Text = lead.Description;

                    lead.ResellerId = Convert.ToInt32(DrpPartners.SelectedItem.Value);

                    TbxLeadEmail.Text = "";

                    UpdatePanel8.Update();
                    UpdatePanelContent.Update();

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseLeadsServiceModal();", true);
                }
                else
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Sorry, no lead was found with this email";
                    return;
                }
            }
            catch (Exception ex)
            {
                divCrmErrorMessage.Visible = true;
                LblLeadCrmErrorMessage.Text = "Sorry, no lead was found";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnSendData_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                #region Check Data

                if (DrpUserCrmList.SelectedItem.Value == "0")
                {
                    divCrmErrorMessage.Visible = true;
                    LblLeadCrmErrorMessage.Text = "Please select CRM";
                    return;
                }

                //if (TbxApiKey.Text.Trim() == "")
                //{
                //    divCrmErrorMessage.Visible = true;
                //    LblLeadCrmErrorMessage.Text = "Please add selected CRM api key";
                //    return;
                //}

                #endregion

                if (Request.QueryString["dealViewID"] != null)
                {
                    int leadId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                    if (leadId > 0)
                    {
                        ElioRegistrationDeals deal = Sql.GetDealById(leadId, session);

                        if (deal != null)
                        {
                            #region Send New Lead To CRM

                            ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                            if (integration != null)
                            {
                                string crmResponse = Lib.Services.CRMs.HubspotAPI.HubspotService.CreateOrUpdateContactLead(integration.UserApiKey, deal, null);

                                if (crmResponse != System.Net.HttpStatusCode.OK.ToString())
                                {
                                    if (crmResponse == System.Net.HttpStatusCode.Conflict.ToString().ToUpper())
                                    {
                                        #region Lead exists in CRM

                                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal already exists in " + DrpUserCrmList.SelectedItem.Text + " CRM.", MessageTypes.Info, true, true, true, true, false);
                                        
                                        #endregion
                                    }
                                    else
                                    {
                                        #region Bad response

                                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                                        
                                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR creating new lead to CRM with response: " + crmResponse, string.Format("deal with ID: {0} could not be created to CRM by user: {1}, at {2} with bad response: {3}", deal.Id, vSession.User.Id, DateTime.Now.ToString(), crmResponse));

                                        #endregion
                                    }
                                }
                                else
                                {
                                    #region Save connection Elio-deal with Crm-deal

                                    try
                                    {
                                        bool exists = Sql.ExistCrmUserDeal(Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), deal.Id, session);
                                        if (!exists)
                                        {
                                            ElioCrmUserDeals crmDeal = new ElioCrmUserDeals();

                                            crmDeal.CrmIntegrationId = Convert.ToInt32(DrpUserCrmList.SelectedItem.Value);
                                            crmDeal.CrmDeadId = "";
                                            crmDeal.DealId = deal.Id;
                                            crmDeal.DateInsert = DateTime.Now;
                                            crmDeal.LastUpdate = DateTime.Now;
                                            crmDeal.IsActive = 1;

                                            DataLoader<ElioCrmUserDeals> loader = new DataLoader<ElioCrmUserDeals>(session);
                                            loader.Insert(crmDeal);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    }

                                    #endregion

                                    #region Success lead create/update to CRM

                                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal was sent/updated to selected CRM successfully", MessageTypes.Success, true, true, true, true, false);
                                    
                                    #endregion
                                }
                            }
                            else
                            {

                            }

                            #endregion
                        }
                        else
                        {
                            #region Lead not found By ID

                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                            
                            Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM", string.Format("deal with ID: {0} could not be send to CRM by user: {1}, at {2}", deal.Id, vSession.User.Id, DateTime.Now.ToString()));

                            #endregion
                        }
                    }
                    else
                    {
                        #region No Lead found by Session URL

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                        
                        Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending lead to CRM because it could not be found by session url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                        #endregion
                    }
                }
                else
                {
                    #region No Lead found by URL

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Deal could not be send to CRM. Please try again later", MessageTypes.Error, true, true, true, true, false);
                    
                    Logger.DetailedError(Request.Url.ToString(), "DashboardDealRegistrationAddEdit.aspx --> ERROR sending deal to CRM because it could not be found by url", string.Format("deal could not be not be found by url and  not sent to CRM by user: {0}, at {1}", vSession.User.Id, DateTime.Now.ToString()));

                    #endregion
                }

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseLeadsServiceModal();", true);

                UpdatePanelContent.Update();
            }
            catch (Exception ex)
            {
                divCrmErrorMessage.Visible = true;
                LblLeadCrmErrorMessage.Text = "Sorry, deal could not be send to " + DrpUserCrmList.SelectedItem.Text + " CRM";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnCheckCrmLead_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    UcMessageAlert.Visible = false;

                    ElioCrmUserIntegrations integration = Sql.GetUserCrmIntegration(vSession.User.Id, session);

                    if (integration != null)
                    {
                        bool exist = Lib.Services.CRMs.HubspotAPI.HubspotService.SearchContactByEmai(integration.UserApiKey, TbxEmail.Text);

                        if (exist)
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "This deal already exists in your CRM", MessageTypes.Info, true, true, true, true, false);
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "This deal does not exists in your CRM", MessageTypes.Info, true, true, true, true, false);
                        }
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
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

        # endregion

        #region Dropdown Lists

        protected void DrpPartners_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LoadAPIs();

                    if (DrpPartners.SelectedItem.Value == "0")
                    {
                        LblPartnerHeaderInfo.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "Step 1: Select your Vendor" : "Step 1: Select your Partner";
                        BtnSave.Enabled = false;
                        divSelectedPartnerHeader.Visible = false;
                        ResetFields();
                        AllowEdit(false);
                        LblCrmInfo.Visible = BtnGetLeads.Visible = false;
                        //divVendorsArea.Visible = false;
                    }
                    else
                    {
                        int partnerId = -1;

                        if (vSession.User.CompanyType ==Types.Vendors.ToString())
                        {
                            partnerId = Convert.ToInt32(DrpPartners.SelectedItem.Value);
                        }
                        else
                        {
                            partnerId = GlobalMethods.GetRandstadCustomerID();
                        }

                        if (partnerId > 0)
                        {
                            LblPartnerHeaderInfo.Text = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? "Step 1: You have selected Vendor" : "Step 1: You have selected Partner";
                            AllowEdit(true);
                            DdlDealResult.Enabled = false;

                            bool successLoaded = LoadPartnerData(partnerId);
                            if (!successLoaded)
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);

                            BtnSave.Enabled = true;

                            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers) && Request.QueryString["dealViewID"] == null)
                            {
                                LblCrmInfo.Visible = true;
                                divFreemiumArea.Visible = false;
                                divPremiumArea.Visible = true;

                                string apiKey = "";
                                bool hasIntegration = Sql.HasUserCrmIntegrationOrRegisterKey(vSession.User.Id, out apiKey, session);
                                if (!hasIntegration)
                                {
                                    aGoToIntegrations.Visible = true;
                                    BtnGetLeads.Visible = false;
                                }
                                else
                                {
                                    if (apiKey == "")
                                    {
                                        aGoToIntegrations.Visible = true;
                                        BtnGetLeads.Visible = false;
                                    }
                                    else
                                    {
                                        aGoToIntegrations.Visible = false;
                                        BtnGetLeads.Visible = true;
                                    }
                                }
                            }
                            else
                            {
                                LblCrmInfo.Visible = BtnGetLeads.Visible = false;
                            }
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deals"), false);
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

        protected void DrpUserCrmList_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    TbxLeadEmail.Text = "";
                    //CbxSaveApiKey.Checked = false;
                    divCrmErrorMessage.Visible = false;
                    LblLeadCrmErrorMessage.Text = "";

                    if (DrpUserCrmList.SelectedItem.Value != "0")
                    {
                        //divApiKeyArea.Visible = true;

                        if (Request.QueryString["dealVendorViewID"] == null && Request.QueryString["dealViewID"] != null)
                        {
                            divSearchLeadInfo.Visible = false;
                            BtnSendData.Visible = true;
                            BtnGetData.Visible = false;
                        }
                        else if (Request.QueryString["dealVendorViewID"] != null && Request.QueryString["dealViewID"] == null)
                        {
                            divSearchLeadInfo.Visible = true;
                            BtnSendData.Visible = false;
                            BtnGetData.Visible = true;
                        }

                        ElioCrmUserIntegrations userCrm = Sql.GetUserCrmIntegrationByCrmID(vSession.User.Id, Convert.ToInt32(DrpUserCrmList.SelectedItem.Value), session);
                        if (userCrm != null && string.IsNullOrEmpty(userCrm.UserApiKey))
                        {
                            #region Go Add Integration (API KEY)

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "integrations"), false);
                            return;

                            #endregion
                        }
                    }
                    else
                    {
                        divSearchLeadInfo.Visible = BtnSendData.Visible = false;
                        BtnGetData.Visible = true;
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

        protected void DrpCurrency_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                string vendorCurrencyID = "";
                string resellerCurrencyID = "";

                if (TbxAmount.Text != "")
                {
                    if (DrpCurrency.SelectedValue != "0")
                    {
                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            ElioCurrenciesCountries vendorCurrency = Sql.GetCurrencyCountriesIJUserCurrencyByUserID(vSession.User.Id, session);
                            vendorCurrencyID = vendorCurrency.CurrencyId;
                        }
                        else
                        {
                            int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                            if (dealId > 0)
                            {
                                ElioRegistrationDeals deal = Sql.GetDealById(dealId, session);

                                if (deal != null && deal.CurId!= "")
                                {
                                    ElioCurrenciesCountries currency = Sql.GetCurrencyCountryByCurId(deal.CurId, session);
                                    if (currency != null)
                                    {
                                        resellerCurrencyID = currency.CurrencyId;
                                    }
                                }
                            }
                        }

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (vendorCurrencyID != "" && resellerCurrencyID != "")
                            {
                                double newAmount = ConverterLib.Convert(Convert.ToDouble(TbxAmount.Text), vendorCurrencyID, resellerCurrencyID);
                                if (newAmount > 0)
                                {
                                    TbxAmount.Text = newAmount.ToString();
                                }
                            }
                        }
                    }
                }
                else
                {
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill deals amount first in order to get the  currency", MessageTypes.Info, true, true, true, true, false);
                    return;
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
    }
}