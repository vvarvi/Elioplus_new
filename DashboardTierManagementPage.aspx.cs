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
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Services.CurrencyConverterAPI.Models;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;

namespace WdS.ElioPlus
{
    public partial class DashboardTierManagementPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public enum Action
        {
            INSERT = 1,
            DELETE = 2,
            NONE = 0
        }

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
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }

                    if (!IsPostBack)
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

        #region Methods

        private void FixPage()
        {
            RdgFormPermissions.ShowHeader = false;

            UpdateStrings();
            SetLinks();
            ResetFields();

            if (vSession.User.CompanyType == Types.Vendors.ToString())
            {
                divCurrencyArea.Visible = true;
                LoadCurrency();
            }

            #region To delete

            //divPgToolbar.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

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

            //aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            #endregion

            FixTierManagementSettingsArea();
            FixButtons();
            //ImgBtnAddTier.Visible = ImgBtnAddTier1.Visible = ImgBtnAddTier2.Visible = ImgBtnAddTier3.Visible = ImgBtnAddTier4.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, "DashboardTierManagementPage", Actions.Add, session);
            RBtnSave.Visible = RBtnSavePermissions.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Edit, session);

            ImgBtnRemove4.Visible = ImgBtnRemove3.Visible = ImgBtnRemove2.Visible = ImgBtnRemove1.Visible = ImgBtnRemoveTier.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Delete, session);
        }

        private void FixTierManagementSettingsArea()
        {
            try
            {
                bool hasTiers = Sql.ExistTiersByUser(vSession.User.Id, session);
                if (!hasTiers)
                {
                    divTierManagementArea.Visible = true;
                    divTierPermissionsArea.Visible = false;
                }
                else
                {
                    divTierManagementArea.Visible = true;
                    divTierPermissionsArea.Visible = true;
                    GetTiersByUser();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void GetTiersByUser()
        {
            try
            {
                List<ElioTierManagementUsersSettings> tiers = Sql.GetTierManagementUserSettings(vSession.User.Id, session);

                if (tiers.Count > 0)
                {
                    //RdBtnRevenuesType.SelectedItem.Value = tiers[0].PeriodId.ToString();
                    RdBtnRevenuesType1.Checked = tiers[0].PeriodId == 1;
                    RdBtnRevenuesType2.Checked = !RdBtnRevenuesType1.Checked;

                    if (tiers.Count >= 1)
                    {
                        divRow1.Visible = true;
                        HdnTierID1.Value = tiers[0].Id.ToString();
                        TbxTierName.Text = tiers[0].Description;
                        TbxCommision.Text = tiers[0].Commision.ToString();
                        TbxSalesVolumeFr.Text = tiers[0].FromVolume.ToString();
                        TbxSalesVolume.Text = tiers[0].ToVolume.ToString();
                    }

                    if (tiers.Count >= 2)
                    {
                        divRow2.Visible = true;
                        HdnTierID2.Value = tiers[1].Id.ToString();
                        TextBox1.Text = tiers[1].Description;
                        TextBox2.Text = tiers[1].Commision.ToString();
                        TextBox3Fr.Text = tiers[1].FromVolume.ToString();
                        TextBox3.Text = tiers[1].ToVolume.ToString();
                    }

                    if (tiers.Count >= 3)
                    {
                        divRow3.Visible = true;
                        HdnTierID3.Value = tiers[2].Id.ToString();
                        TextBox4.Text = tiers[2].Description;
                        TextBox5.Text = tiers[2].Commision.ToString();
                        TextBox6Fr.Text = tiers[2].FromVolume.ToString();
                        TextBox6.Text = tiers[2].ToVolume.ToString();
                    }

                    if (tiers.Count >= 4)
                    {
                        divRow4.Visible = true;
                        HdnTierID4.Value = tiers[3].Id.ToString();
                        TextBox7.Text = tiers[3].Description;
                        TextBox8.Text = tiers[3].Commision.ToString();
                        TextBox9Fr.Text = tiers[3].FromVolume.ToString();
                        TextBox9.Text = tiers[3].ToVolume.ToString();
                    }

                    if (tiers.Count == 5)
                    {
                        divRow2.Visible = true;
                        HdnTierID5.Value = tiers[4].Id.ToString();
                        TextBox10.Text = tiers[4].Description;
                        TextBox11.Text = tiers[4].Commision.ToString();
                        TextBox12Fr.Text = tiers[4].FromVolume.ToString();
                        TextBox12.Text = tiers[4].ToVolume.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            //    if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            //    {
            //        ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            //        if (packet != null)
            //        {
            //            LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
            //        }
            //    }
            //    else
            //    {
            //        LblPricingPlan.Text = "You are currently on a free plan";
            //    }

            //    LblElioplusDashboard.Text = "";

            //    LblDashboard.Text = "Dashboard";

            //    aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;

            //    if (aBtnGoPremium.Visible)
            //    {
            //        LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "19")).Text;
            //        LblPricingPlan.Visible = false;
            //    }

            //    LblGoFull.Text = "Complete your registration";
            //    LblDashPage.Text = "Tier Management";
            //    LblDashSubTitle.Text = "";
        }

        private void ResetFields()
        {
            try
            {
                divRow2.Visible = divRow3.Visible = divRow4.Visible = divRow5.Visible = false;

                HdnTierID.Value = HdnTierID1.Value = HdnTierID2.Value = HdnTierID3.Value = HdnTierID4.Value = HdnTierID5.Value = "0";
                TbxTierName.Text = TbxCommision.Text = TbxSalesVolumeFr.Text = TbxSalesVolume.Text = "";
                TextBox1.Text =
                TextBox2.Text =
                TextBox3Fr.Text =
                TextBox3.Text =
                TextBox4.Text =
                TextBox5.Text =
                TextBox6Fr.Text =
                TextBox6.Text =
                TextBox7.Text =
                TextBox8.Text =
                TextBox9Fr.Text =
                TextBox9.Text =
                TextBox10.Text =
                TextBox11.Text =
                TextBox12Fr.Text =
                TextBox12.Text = "";

                RdBtnRevenuesType1.Checked = true;
                RdBtnRevenuesType2.Checked = false;

                UcMessageCurrencyAlertControl.Visible = UcMessageAlertControl.Visible = UcMessagePermissionsAlertControl.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void InsertOrUpdateTier(int tierId, int periodId, string tierDescription, decimal commision, decimal volumeFrom, decimal volumeTo, List<int> formsIDs, out int insertedTierId)
        {
            insertedTierId = 0;

            DataLoader<ElioTierManagementUsersSettings> loader = new DataLoader<ElioTierManagementUsersSettings>(session);

            if (tierId == 0)
            {
                ElioTierManagementUsersSettings tier = new ElioTierManagementUsersSettings();

                tier.UserId = vSession.User.Id;
                tier.PeriodId = periodId;
                tier.Description = tierDescription;
                tier.Commision = commision;
                tier.FromVolume = volumeFrom;
                tier.ToVolume = volumeTo;
                tier.Sysdate = DateTime.Now;
                tier.LastUpdate = DateTime.Now;
                tier.IsActive = 1;
                tier.IsPublic = 1;

                loader.Insert(tier);

                insertedTierId = tier.Id;

                DataLoader<ElioTierManagementUsersPermissions> loaderPermission = new DataLoader<ElioTierManagementUsersPermissions>(session);

                for (int i = 0; i < formsIDs.Count; i++)
                {
                    bool exist = Sql.ExistTierUserPermissionByFormId(vSession.User.Id, tier.Id, formsIDs[i], session);
                    if (!exist)
                    {
                        ElioTierManagementUsersPermissions permission = new ElioTierManagementUsersPermissions();

                        permission.UserId = vSession.User.Id;
                        permission.FormId = formsIDs[i];
                        permission.TierId = tier.Id;
                        permission.Sysdate = DateTime.Now;
                        permission.LastUpdate = DateTime.Now;
                        permission.IsActive = 1;

                        loaderPermission.Insert(permission);
                    }
                }
            }
            else
            {
                ElioTierManagementUsersSettings tier = Sql.GetTierManagementUserSettingsById(vSession.User.Id, tierId, session);
                if (tier != null)
                {
                    tier.UserId = vSession.User.Id;
                    tier.PeriodId = periodId;
                    tier.Description = tierDescription;
                    tier.Commision = commision;
                    tier.FromVolume = volumeFrom;
                    tier.ToVolume = volumeTo;
                    tier.LastUpdate = DateTime.Now;
                    tier.IsActive = 1;
                    tier.IsPublic = 1;

                    loader.Update(tier);

                    insertedTierId = tier.Id;
                }
            }
        }

        private bool InsertOrDeleteFormPermissions(string TierDescription, int formId, bool isChecked)
        {
            Action action = Action.NONE;
            bool success = true;

            bool hasFormPermission = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, TierDescription, session);
            if (hasFormPermission && !isChecked)
                action = Action.DELETE;
            else if (!hasFormPermission && isChecked)
                action = Action.INSERT;

            if (action != Action.NONE)
            {
                int tierID = Sql.GetTierManagementUserSettingsByDescription(vSession.User.Id, TierDescription, session);
                if (tierID > 0)
                {
                    switch (action)
                    {
                        case Action.INSERT:

                            ElioTierManagementUsersPermissions permission = new ElioTierManagementUsersPermissions();

                            permission.UserId = vSession.User.Id;
                            permission.FormId = formId;
                            permission.TierId = tierID;
                            permission.Sysdate = DateTime.Now;
                            permission.LastUpdate = DateTime.Now;
                            permission.IsActive = 1;

                            DataLoader<ElioTierManagementUsersPermissions> loader = new DataLoader<ElioTierManagementUsersPermissions>(session);
                            loader.Insert(permission);

                            bool insert = permission.Id > 0;
                            if (!insert)
                            {
                                success = false;
                            }

                            break;

                        case Action.DELETE:

                            bool deleted = Sql.DeleteTierUserFormPermissionsByTierId(vSession.User.Id, formId, tierID, session);
                            if (!deleted)
                            {
                                success = false;
                            }

                            break;
                    }
                }
            }

            return success;
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
            bool hasItemSelected = false;
            DrpCurrency.Items.Clear();
            string defaultUserCurId = Sql.GetUserCurID(vSession.User.Id, session);

            aDeleteCurrency.Visible = defaultUserCurId != "";

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

                    if (defaultUserCurId != "" && defaultUserCurId == currency.CurId && !hasItemSelected)
                    {
                        itm.Selected = true;
                        hasItemSelected = true;
                    }
                    else if (defaultUserCurId == "" && (currency.Name == vSession.User.Country || currency.Name.Contains(vSession.User.Country)) && !hasItemSelected)
                    {
                        itm.Selected = true;
                        hasItemSelected = true;
                    }

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
                        countryCurr = countryCurr.OrderBy(x => x.Id).ToList();

                        ListItem itm = new ListItem();

                        itm.Value = "NO";
                        itm.Text = "Select currency";
                        DrpCurrency.Items.Add(itm);

                        foreach (Country currency in countryCurr)
                        {
                            itm = new ListItem();

                            itm.Value = currency.Id;
                            itm.Text = currency.Id + "(" + currency.CurrencySymbol + "-" + currency.CurrencyId + ")";

                            if (defaultUserCurId != "" && defaultUserCurId == currency.Id && !hasItemSelected)
                            {
                                itm.Selected = true;
                                hasItemSelected = true;
                            }
                            else if (defaultUserCurId == "" && (currency.Name == vSession.User.Country || currency.Name.Contains(vSession.User.Country)) && !hasItemSelected)
                            {
                                itm.Selected = true;
                                hasItemSelected = true;
                            }

                            DrpCurrency.Items.Add(itm);

                            //Logger.Debug("Count: " + count.ToString() + " --> " + currency.CurrencyId + "-" + currency.Alpha3 + "-" + currency.CurrencyName + "-" + currency.CurrencySymbol + "-" + currency.Id + "-" + currency.Name);
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

        private void FixButtons()
        {
            if (divRow5.Visible)
            {
                ImgBtnAddTier4.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Add, session);
                ImgBtnAddTier.Visible = ImgBtnAddTier1.Visible = ImgBtnAddTier2.Visible = ImgBtnAddTier3.Visible = false;
            }
            else
            {
                if (divRow4.Visible)
                {
                    ImgBtnAddTier3.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Add, session);
                    ImgBtnAddTier.Visible = ImgBtnAddTier1.Visible = ImgBtnAddTier2.Visible = ImgBtnAddTier4.Visible = false;
                }
                else
                {
                    if (divRow3.Visible)
                    {
                        ImgBtnAddTier2.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Add, session);
                        ImgBtnAddTier.Visible = ImgBtnAddTier1.Visible = ImgBtnAddTier3.Visible = ImgBtnAddTier4.Visible = false;
                    }
                    else
                    {
                        if (divRow2.Visible)
                        {
                            ImgBtnAddTier1.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Add, session);
                            ImgBtnAddTier.Visible = ImgBtnAddTier2.Visible = ImgBtnAddTier3.Visible = ImgBtnAddTier4.Visible = false;
                        }
                        else
                        {
                            if (divRow1.Visible)
                            {
                                ImgBtnAddTier.Visible = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardTierManagementPage", Actions.Add, session);
                                ImgBtnAddTier1.Visible = ImgBtnAddTier2.Visible = ImgBtnAddTier3.Visible = ImgBtnAddTier4.Visible = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Grids

        protected void RdgFormPermissions_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    #region Parent

                    GridDataItem item = (GridDataItem)e.Item;

                    Label lblDescription1 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription1");
                    CheckBox cbxSelectTier1 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier1");

                    Label lblDescription2 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription2");
                    CheckBox cbxSelectTier2 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier2");

                    Label lblDescription3 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription3");
                    CheckBox cbxSelectTier3 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier3");

                    Label lblDescription4 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription4");
                    CheckBox cbxSelectTier4 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier4");

                    Label lblDescription5 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription5");
                    CheckBox cbxSelectTier5 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier5");

                    lblDescription1.Text = item["description1"].Text;
                    lblDescription2.Text = item["description2"].Text;
                    lblDescription3.Text = item["description3"].Text;
                    lblDescription4.Text = item["description4"].Text;
                    lblDescription5.Text = item["description5"].Text;

                    int formId = Convert.ToInt32(item["form_id"].Text);
                    if (formId > 0)
                    {
                        if (item["description1"].Text != "" && item["description1"].Text != "&nbsp;")
                            cbxSelectTier1.Checked = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, item["description1"].Text, session);
                        else
                            cbxSelectTier1.Visible = false;

                        if (item["description2"].Text != "" && item["description2"].Text != "&nbsp;")
                            cbxSelectTier2.Checked = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, item["description2"].Text, session);
                        else
                            cbxSelectTier2.Visible = false;

                        if (item["description3"].Text != "" && item["description3"].Text != "&nbsp;")
                            cbxSelectTier3.Checked = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, item["description3"].Text, session);
                        else
                            cbxSelectTier3.Visible = false;

                        if (item["description4"].Text != "" && item["description4"].Text != "&nbsp;")
                            cbxSelectTier4.Checked = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, item["description4"].Text, session);
                        else
                            cbxSelectTier4.Visible = false;

                        if (item["description5"].Text != "" && item["description5"].Text != "&nbsp;")
                            cbxSelectTier5.Checked = Sql.HasTierManagementUserFormPermission(vSession.User.Id, formId, item["description5"].Text, session);
                        else
                            cbxSelectTier5.Visible = false;
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

        protected void RdgFormPermissions_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioTierManagementForms> forms = Sql.GetTierManagementForms(session);
                    if (forms.Count > 0)
                    {
                        List<ElioTierManagementUsersSettings> tiers = Sql.GetTierManagementUserSettings(vSession.User.Id, session);
                        if (tiers.Count > 0)
                        {
                            RdgFormPermissions.Visible = true;
                            divTierPermissionsArea.Visible = true;

                            DataTable table = new DataTable();

                            table.Columns.Add("form_id");
                            table.Columns.Add("menu_name");
                            table.Columns.Add("page_name");
                            table.Columns.Add("description1");
                            table.Columns.Add("description2");
                            table.Columns.Add("description3");
                            table.Columns.Add("description4");
                            table.Columns.Add("description5");

                            string description1 = "";
                            string description2 = "";
                            string description3 = "";
                            string description4 = "";
                            string description5 = "";

                            if (tiers.Count <= 5)
                            {
                                for (int i = 0; i < tiers.Count; i++)
                                {
                                    if (tiers[i].Description != "" && i == 0)
                                    {
                                        description1 = tiers[i].Description;
                                        //tierId1 = tiers[i].Id;
                                    }
                                    else if (tiers[i].Description != "" && i == 1)
                                    {
                                        description2 = tiers[i].Description;
                                        //tierId2 = tiers[i].Id;
                                    }
                                    else if (tiers[i].Description != "" && i == 2)
                                    {
                                        description3 = tiers[i].Description;
                                        //tierId3 = tiers[i].Id;
                                    }
                                    else if (tiers[i].Description != "" && i == 3)
                                    {
                                        description4 = tiers[i].Description;
                                        //tierId4 = tiers[i].Id;
                                    }
                                    else if (tiers[i].Description != "" && i == 4)
                                    {
                                        description5 = tiers[i].Description;
                                        //tierId5 = tiers[i].Id;
                                    }
                                }
                            }

                            foreach (ElioTierManagementForms form in forms)
                            {
                                table.Rows.Add(form.Id, form.MenuName + " available for: ", form.PageName, description1, description2, description3, description4, description5);
                            }

                            RdgFormPermissions.Visible = true;
                            RdgFormPermissions.DataSource = table;
                            UcMessagePermissionsAlertControl.Visible = false;
                        }
                        else
                        {
                            RdgFormPermissions.Visible = false;
                            divTierPermissionsArea.Visible = false;
                        }
                    }
                    else
                    {
                        RdgFormPermissions.Visible = false;
                        divTierPermissionsArea.Visible = false;
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

        #endregion

        #region Buttons

        protected void BtnSaveCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageCurrencyAlertControl.Visible = false;

                if (vSession.User != null)
                {
                    if (DrpCurrency.SelectedValue != "NO")
                    {
                        DataLoader<ElioUsersCurrency> loader = new DataLoader<ElioUsersCurrency>(session);

                        ElioUsersCurrency currency = Sql.GetUserCurrency(vSession.User.Id, session);
                        if (currency == null)
                        {
                            currency = new ElioUsersCurrency();

                            currency.UserId = vSession.User.Id;
                            currency.CurId = DrpCurrency.SelectedValue;
                            currency.Sysdate = DateTime.Now;
                            currency.LastUpdate = DateTime.Now;
                            currency.IsPublic = 1;

                            loader.Insert(currency);
                        }
                        else
                        {
                            currency.CurId = DrpCurrency.SelectedValue;
                            currency.LastUpdate = DateTime.Now;

                            loader.Update(currency);
                        }

                        aDeleteCurrency.Visible = true;
                        GlobalMethods.ShowMessageControlDA(UcMessageCurrencyAlertControl, "Currency saved successfully", MessageTypes.Success, true, true, true, true, false);

                        UpdatePanelContent.Update();
                    }
                    else
                        GlobalMethods.ShowMessageControlDA(UcMessageCurrencyAlertControl, "Please select a currency in order to save it", MessageTypes.Error, true, true, true, true, false);
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

        protected void aAddNewTier_ServerClick(object sender, EventArgs e)
        {
            try
            {
                if (divRow2.Visible == false)
                {
                    divRow2.Visible = true;
                }
                else if (divRow3.Visible == false)
                {
                    divRow3.Visible = true;
                }
                else if (divRow4.Visible == false)
                {
                    divRow4.Visible = true;
                }
                else if (divRow5.Visible == false)
                {
                    divRow5.Visible = true;
                }
                else
                {
                    LblMessageAlertfMsg.Text = "If you want to save more than 5 tiers, please contact us!";
                    BtnRemoveTier.Visible = false;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }

                FixButtons();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RBtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    bool hasTier = false;
                    int insertedTierId = 0;

                    if (!RdBtnRevenuesType1.Checked && !RdBtnRevenuesType2.Checked)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must select type of Sales/Revenues", MessageTypes.Warning, true, true, true, true, false);
                        return;
                    }
                    else if (RdBtnRevenuesType1.Checked && RdBtnRevenuesType2.Checked)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must select one type", MessageTypes.Warning, true, true, true, true, false);
                        return;
                    }

                    if (divRow1.Visible)
                    {
                        if (TbxTierName.Text == "" || TbxCommision.Text == "" || TbxSalesVolumeFr.Text == "" || TbxSalesVolume.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must fill all the above values!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDecimal(TbxSalesVolume.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "To volume can not be zero", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(TbxSalesVolumeFr.Text) >= Convert.ToDecimal(TbxSalesVolume.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    if (divRow2.Visible)
                    {
                        if (TextBox1.Text == "" || TextBox2.Text == "" || TextBox3Fr.Text == "" || TextBox3.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must fill all the above values!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDecimal(TextBox3Fr.Text) == 0 || Convert.ToDecimal(TextBox3.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "From volume or To volume can not be zero", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(TextBox3Fr.Text) >= Convert.ToDecimal(TextBox3.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    if (divRow3.Visible)
                    {
                        if (TextBox4.Text == "" || TextBox5.Text == "" || TextBox6Fr.Text == "" || TextBox6.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must fill all the above values!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDecimal(TextBox6Fr.Text) == 0 || Convert.ToDecimal(TextBox6.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "From volume or To volume can not be zero", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(TextBox6Fr.Text) >= Convert.ToDecimal(TextBox6.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    if (divRow4.Visible)
                    {
                        if (TextBox7.Text == "" || TextBox8.Text == "" || TextBox9Fr.Text == "" || TextBox9.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must fill all the above values!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDecimal(TextBox9Fr.Text) == 0 || Convert.ToDecimal(TextBox9.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "From volume or To volume can not be zero", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(TextBox9Fr.Text) >= Convert.ToDecimal(TextBox9.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    if (divRow5.Visible)
                    {
                        if (TextBox10.Text == "" || TextBox11.Text == "" || TextBox12Fr.Text == "" || TextBox12.Text == "")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "You must fill all the above values!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            if (Convert.ToDecimal(TextBox12Fr.Text) == 0 || Convert.ToDecimal(TextBox12.Text) == 0)
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "From volume or To volume can not be zero", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            if (Convert.ToDecimal(TextBox12Fr.Text) >= Convert.ToDecimal(TextBox12.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                        }
                    }

                    int selectedType = (RdBtnRevenuesType1.Checked) ? 1 : 2;

                    List<int> formsIDs = Sql.GetTierManagementFormsIdsArray(session);
                    if (formsIDs.Count > 0)
                    {
                        if (TbxTierName.Text != "" && TbxCommision.Text != "" && TbxSalesVolumeFr.Text != "" && TbxSalesVolume.Text != "")
                        {
                            if (Convert.ToDecimal(TbxSalesVolumeFr.Text) >= Convert.ToDecimal(TbxSalesVolume.Text))
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Please fill the volumes with valid values! From volume must be less than To volume", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            InsertOrUpdateTier(Convert.ToInt32(HdnTierID1.Value), selectedType, TbxTierName.Text, Convert.ToDecimal(TbxCommision.Text), Convert.ToDecimal(TbxSalesVolumeFr.Text), Convert.ToDecimal(TbxSalesVolume.Text), formsIDs, out insertedTierId);
                            if (insertedTierId > 0)
                            {
                                HdnTierID1.Value = insertedTierId.ToString();
                                hasTier = true;
                            }
                        }

                        if (TextBox1.Text != "" && TextBox2.Text != "" && TextBox3Fr.Text != "" && TextBox3.Text != "")
                        {
                            insertedTierId = 0;
                            InsertOrUpdateTier(Convert.ToInt32(HdnTierID2.Value), selectedType, TextBox1.Text, Convert.ToDecimal(TextBox2.Text), Convert.ToDecimal(TextBox3Fr.Text), Convert.ToDecimal(TextBox3.Text), formsIDs, out insertedTierId);
                            if (insertedTierId > 0)
                            {
                                HdnTierID2.Value = insertedTierId.ToString();
                                hasTier = true;
                            }
                        }

                        if (TextBox4.Text != "" && TextBox5.Text != "" && TextBox6Fr.Text != "" && TextBox6.Text != "")
                        {
                            insertedTierId = 0;
                            InsertOrUpdateTier(Convert.ToInt32(HdnTierID3.Value), selectedType, TextBox4.Text, Convert.ToDecimal(TextBox5.Text), Convert.ToDecimal(TextBox6Fr.Text), Convert.ToDecimal(TextBox6.Text), formsIDs, out insertedTierId);
                            if (insertedTierId > 0)
                            {
                                HdnTierID3.Value = insertedTierId.ToString();
                                hasTier = true;
                            }
                        }

                        if (TextBox7.Text != "" && TextBox8.Text != "" && TextBox9Fr.Text != "" && TextBox9.Text != "")
                        {
                            insertedTierId = 0;
                            InsertOrUpdateTier(Convert.ToInt32(HdnTierID4.Value), selectedType, TextBox7.Text, Convert.ToDecimal(TextBox8.Text), Convert.ToDecimal(TextBox9Fr.Text), Convert.ToDecimal(TextBox9.Text), formsIDs, out insertedTierId);
                            if (insertedTierId > 0)
                            {
                                HdnTierID4.Value = insertedTierId.ToString();
                                hasTier = true;
                            }
                        }

                        if (TextBox10.Text != "" && TextBox11.Text != "" && TextBox12Fr.Text != "" && TextBox12.Text != "")
                        {
                            insertedTierId = 0;
                            InsertOrUpdateTier(Convert.ToInt32(HdnTierID5.Value), selectedType, TextBox10.Text, Convert.ToDecimal(TextBox11.Text), Convert.ToDecimal(TextBox12Fr.Text), Convert.ToDecimal(TextBox12.Text), formsIDs, out insertedTierId);
                            if (insertedTierId > 0)
                            {
                                HdnTierID5.Value = insertedTierId.ToString();
                                hasTier = true;
                            }
                        }

                        if (!hasTier)
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                        else
                        {
                            divTierPermissionsArea.Visible = true;
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Your tiers saved successfully!", MessageTypes.Success, true, true, true, true, false);

                            RdgFormPermissions.Rebind();
                        }
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RBtnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetFields();

                GetTiersByUser();
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

        protected void RBtnSavePermissions_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    bool success = true;

                    foreach (GridDataItem item in RdgFormPermissions.Items)
                    {
                        if (item is GridDataItem)
                        {
                            #region Find Controls

                            Label lblDescription1 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription1");
                            CheckBox cbxSelectTier1 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier1");

                            Label lblDescription2 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription2");
                            CheckBox cbxSelectTier2 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier2");

                            Label lblDescription3 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription3");
                            CheckBox cbxSelectTier3 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier3");

                            Label lblDescription4 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription4");
                            CheckBox cbxSelectTier4 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier4");

                            Label lblDescription5 = (Label)ControlFinder.FindControlRecursive(item, "LblDescription5");
                            CheckBox cbxSelectTier5 = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectTier5");

                            #endregion

                            int formId = Convert.ToInt32(item["form_id"].Text);
                            if (formId > 0)
                            {
                                if (lblDescription1.Text != "" && lblDescription1.Text != "&nbsp;")
                                {
                                    success = InsertOrDeleteFormPermissions(lblDescription1.Text, formId, cbxSelectTier1.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (lblDescription2.Text != "" && lblDescription2.Text != "&nbsp;")
                                {
                                    success = InsertOrDeleteFormPermissions(lblDescription2.Text, formId, cbxSelectTier2.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (lblDescription3.Text != "" && lblDescription3.Text != "&nbsp;")
                                {
                                    success = InsertOrDeleteFormPermissions(lblDescription3.Text, formId, cbxSelectTier3.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (lblDescription4.Text != "" && lblDescription4.Text != "&nbsp;")
                                {
                                    success = InsertOrDeleteFormPermissions(lblDescription4.Text, formId, cbxSelectTier4.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }

                                if (lblDescription5.Text != "" && lblDescription5.Text != "&nbsp;")
                                {
                                    success = InsertOrDeleteFormPermissions(lblDescription5.Text, formId, cbxSelectTier5.Checked);

                                    if (!success)
                                    {
                                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be updated. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                                        return;
                                    }
                                }
                            }
                        }
                    }

                    if (success)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your permissions updated successfully", MessageTypes.Success, true, true, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessagePermissionsAlertControl, "Your Permissions could not be saved. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RBtnCancelPermissions_Click(object sender, EventArgs e)
        {
            try
            {
                RdgFormPermissions.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemoveTier_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnTierID1.Value == "0")
                {
                    divRow1.Visible = false;
                    TbxTierName.Text = TbxCommision.Text = TbxSalesVolume.Text = "";
                    HdnTierID1.Value = "0";

                    divRow1.Visible = true;
                    ImgBtnRemoveTier.Visible = true;
                    FixButtons();
                }
                else
                {
                    HdnTierID.Value = HdnTierID1.Value;
                    BtnRemoveTier.Visible = true;
                    BtnRemoveCurrency.Visible = false;
                    LblMessageAlertfMsg.Text = "You are going to delete this tier. Do you want to proceed?";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove1_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnTierID2.Value == "0")
                {
                    divRow2.Visible = false;
                    TextBox1.Text = TextBox2.Text = TextBox3.Text = "";
                    HdnTierID2.Value = "0";
                    FixButtons();
                }
                else
                {
                    HdnTierID.Value = HdnTierID2.Value;
                    BtnRemoveTier.Visible = true;
                    BtnRemoveCurrency.Visible = false;
                    LblMessageAlertfMsg.Text = "You are going to delete this tier. Do you want to proceed?";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnTierID3.Value == "0")
                {
                    divRow3.Visible = false;
                    TextBox4.Text = TextBox5.Text = TextBox6.Text = "";
                    HdnTierID3.Value = "0";
                    FixButtons();
                }
                else
                {
                    HdnTierID.Value = HdnTierID3.Value;
                    BtnRemoveTier.Visible = true;
                    BtnRemoveCurrency.Visible = false;
                    LblMessageAlertfMsg.Text = "You are going to delete this tier. Do you want to proceed?";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove3_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnTierID4.Value == "0")
                {
                    divRow4.Visible = false;
                    TextBox7.Text = TextBox8.Text = TextBox9.Text = "";
                    HdnTierID4.Value = "0";
                    FixButtons();
                }
                else
                {
                    HdnTierID.Value = HdnTierID4.Value;
                    BtnRemoveTier.Visible = true;
                    BtnRemoveCurrency.Visible = false;
                    LblMessageAlertfMsg.Text = "You are going to delete this tier. Do you want to proceed?";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnRemove4_Click(object sender, EventArgs e)
        {
            try
            {
                if (HdnTierID5.Value == "0")
                {
                    divRow5.Visible = false;
                    TextBox10.Text = TextBox11.Text = TextBox12.Text = "";
                    HdnTierID5.Value = "0";
                    FixButtons();
                }
                else
                {
                    HdnTierID.Value = HdnTierID5.Value;
                    BtnRemoveTier.Visible = true;
                    BtnRemoveCurrency.Visible = false;
                    LblMessageAlertfMsg.Text = "You are going to delete this tier. Do you want to proceed?";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnRemoveTier_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (HdnTierID.Value != "0")
                    {
                        bool deleted = false;

                        try
                        {
                            session.BeginTransaction();

                            deleted = Sql.DeleteTierUserPermissionsForTierId(vSession.User.Id, Convert.ToInt32(HdnTierID.Value), session);
                            deleted = Sql.DeleteTierUserSettingsById(vSession.User.Id, Convert.ToInt32(HdnTierID.Value), session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            throw ex;
                        }

                        if (deleted)
                        {
                            ResetFields();
                            GetTiersByUser();
                            RdgFormPermissions.Rebind();

                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Tier deleted successfully", MessageTypes.Success, true, true, true, true, false);
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Tier could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                            throw new Exception(string.Format("User with ID: {0} tried to delete tier with ID: {1}, at {2}, but it could not be deleted", vSession.User.Id, HdnTierID.Value, DateTime.Now.ToString()));
                        }
                    }
                    else
                    {
                        //error can not delete tier
                        GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Tier could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);

                    UpdatePanelContent.Update();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessageAlertControl, "Tier could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);

                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aDeleteCurrency_ServerClick(object sender, EventArgs e)
        {
            try
            {
                UcMessageCurrencyAlertControl.Visible = false;
                BtnRemoveTier.Visible = false;
                BtnRemoveCurrency.Visible = true;
                LblMessageAlertfMsg.Text = "You are going to delete your currency. Do you want to proceed?";

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnRemoveCurrency_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    try
                    {
                        bool deleted = Sql.DeleteUserCurrency(vSession.User.Id, session);

                        if (deleted)
                        {
                            ResetFields();
                            LoadCurrency();

                            GlobalMethods.ShowMessageControlDA(UcMessageCurrencyAlertControl, "Currency deleted successfully", MessageTypes.Success, true, true, true, true, false);

                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageCurrencyAlertControl, "Currency could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);
                            throw new Exception(string.Format("User with ID: {0} tried to delete Currency with CUR_ID: {1}, at {2}, but it could not be deleted", vSession.User.Id, DrpCurrency.SelectedValue, DateTime.Now.ToString()));
                        }

                        UpdatePanelContent.Update();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(UcMessageCurrencyAlertControl, "Currency could not be deleted. Please try again later!", MessageTypes.Error, true, true, true, true, false);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfirmationPopUp();", true);
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region Dropdown Lists

        #endregion
    }
}