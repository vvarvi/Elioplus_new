using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.ImagesHelper;
using System.Web.UI.HtmlControls;
using System.IO;
using WdS.ElioPlus.Lib.Localization;
using System.Data;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.StripePayment;
using System.Configuration;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus
{
    public partial class DashboardEditProfile : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();        

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    if (vSession.User != null && HttpContext.Current.Request.Url.AbsolutePath != null)
                    {
                        string path = HttpContext.Current.Request.Url.AbsolutePath;
                        string[] pathArray = path.Split('/');

                        if (pathArray.Length == 5)
                        {
                            string name = pathArray[3];
                            string type = pathArray[1];

                            if (type == "dashboard")
                            {
                                if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Deleting))
                                {
                                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
                                    return;
                                }
                                else if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                                {
                                    if (NotCorrectName(name, vSession.User.CompanyName))
                                    {
                                        Response.Redirect(ControlLoader.PageDash404, false);
                                        return;
                                    }
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName))
                                    {
                                        if (NotCorrectName(name, vSession.User.FirstName + "-" + vSession.User.LastName))
                                        {
                                            Response.Redirect(ControlLoader.PageDash404, false);
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        if (NotCorrectName(name, vSession.User.Username))
                                        {
                                            Response.Redirect(ControlLoader.PageDash404, false);
                                            return;
                                        }
                                    }
                                }

                                try
                                {
                                    session.OpenConnection();

                                    FixPage();
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
                            else
                            {
                                Response.Redirect(ControlLoader.PageDash404, false);
                            }
                        }
                        else
                        {
                            Response.Redirect(ControlLoader.PageDash404, false);
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Default(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # region Methods

        private bool IsCheckBoxSelected(string description, HtmlInputCheckBox cbx)
        {
            bool isSelected = false;

            ElioEmailNotifications notification = Sql.GetElioEmailNotificationByDescription(description, session);
            if (notification != null)
            {
                isSelected = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
                cbx.Value = notification.Id.ToString();
                notification = null;
            }

            return isSelected;
        }

        private void LoadEmailSettings()
        {
            HtmlInputCheckBox cbx1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox1");
            HtmlInputCheckBox cbx2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox2");
            HtmlInputCheckBox cbx3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox3");
            HtmlInputCheckBox cbx4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox4");
            HtmlInputCheckBox cbx5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox5");
            HtmlInputCheckBox cbx6 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox6");
            HtmlInputCheckBox cbx7 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox7");
            HtmlInputCheckBox cbx8 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox8");
            HtmlInputCheckBox cbx9 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox9");
            HtmlInputCheckBox cbx10 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox10");
            HtmlInputCheckBox cbx11 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox11");
            HtmlInputCheckBox cbx12 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox12");
            HtmlInputCheckBox cbx13 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox13");
            HtmlInputCheckBox cbx14 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox14");
            HtmlInputCheckBox cbx15 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox15");
            HtmlInputCheckBox cbx16 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox16");
            HtmlInputCheckBox cbx17 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox17");
            HtmlInputCheckBox cbx19 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox19");
            HtmlInputCheckBox cbx20 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox20");
            HtmlInputCheckBox cbx21 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox21");
            HtmlInputCheckBox cbx22 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox22");
            HtmlInputCheckBox cbx23 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox23");

            cbx1.Checked = IsCheckBoxSelected(Lbl1.Text, cbx1);
            cbx2.Checked = IsCheckBoxSelected(Lbl2.Text, cbx2);
            cbx3.Checked = IsCheckBoxSelected(Lbl3.Text, cbx3);
            cbx4.Checked = IsCheckBoxSelected(Lbl4.Text, cbx4);
            cbx5.Checked = IsCheckBoxSelected(Lbl5.Text, cbx5);
            cbx6.Checked = IsCheckBoxSelected(Lbl6.Text, cbx6);
            cbx7.Checked = IsCheckBoxSelected(Lbl7.Text, cbx7);
            cbx8.Checked = IsCheckBoxSelected(Lbl8.Text, cbx8);
            cbx9.Checked = IsCheckBoxSelected(Lbl9.Text, cbx9);
            cbx10.Checked = IsCheckBoxSelected(Lbl10.Text, cbx10);
            cbx11.Checked = IsCheckBoxSelected(Lbl11.Text, cbx11);
            cbx12.Checked = IsCheckBoxSelected(Lbl12.Text, cbx12);
            cbx13.Checked = IsCheckBoxSelected(Lbl13.Text, cbx13);
            cbx14.Checked = IsCheckBoxSelected(Lbl14.Text, cbx14);
            cbx15.Checked = IsCheckBoxSelected(Lbl15.Text, cbx15);
            cbx16.Checked = IsCheckBoxSelected(Lbl16.Text, cbx16);
            cbx17.Checked = IsCheckBoxSelected(Lbl17.Text, cbx17);
            cbx19.Checked = IsCheckBoxSelected(Lbl19.Text, cbx19);
            cbx20.Checked = IsCheckBoxSelected(Lbl20.Text, cbx20);
            cbx21.Checked = IsCheckBoxSelected(Lbl21.Text, cbx21);
            cbx22.Checked = IsCheckBoxSelected(Lbl22.Text, cbx22);
            cbx23.Checked = IsCheckBoxSelected(Lbl23.Text, cbx23);

            //List<ElioEmailNotifications> notifications = Sql.GetElioEmailNotifications(session);

            //foreach (ElioEmailNotifications notification in notifications)
            //{
            //    if (Convert.ToInt32(cbx1.Value) == notification.Id)
            //        cbx1.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);    // || communitySelected;
            //    else if (Convert.ToInt32(cbx2.Value) == notification.Id)
            //        cbx2.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx3.Value) == notification.Id)
            //        cbx3.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx4.Value) == notification.Id)
            //        cbx4.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx5.Value) == notification.Id)
            //        cbx5.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx6.Value) == notification.Id)
            //        cbx6.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx7.Value) == notification.Id)
            //        cbx7.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx8.Value) == notification.Id)
            //        cbx8.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx9.Value) == notification.Id)
            //        cbx9.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx10.Value) == notification.Id)
            //        cbx10.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx11.Value) == notification.Id)
            //        cbx11.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx12.Value) == notification.Id)
            //        cbx12.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx13.Value) == notification.Id)
            //        cbx13.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx14.Value) == notification.Id)
            //        cbx14.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx15.Value) == notification.Id)
            //        cbx15.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx16.Value) == notification.Id)
            //        cbx16.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //    else if (Convert.ToInt32(cbx17.Value) == notification.Id)
            //        cbx17.Checked = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);
            //}
        }

        private void LoadEmailsInfoText()
        {
            CbxEmailNotifications.Items.Clear();

            List<ElioEmailNotifications> notifications = Sql.GetElioEmailNotifications(session);

            foreach (ElioEmailNotifications notification in notifications)
            {
                ListItem item = new ListItem();

                //int communityEmailId = 1;
                //bool communitySelected = false;
                //if (notification.Id == 12 || notification.Id == 13 || notification.Id == 14 || notification.Id == 15)
                //{
                //    communitySelected = Sql.ExistCommunityUserEmailNotification(vSession.User.Id, communityEmailId, session);
                //    communityEmailId++;
                //}

                item.Selected = Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, notification.Id, session);    // || communitySelected;

                item.Value = notification.Id.ToString();
                item.Text = notification.Description;

                CbxEmailNotifications.Items.Add(item);
            }
        }

        private bool NotCorrectName(string name, string userInput)
        {
            return (name != Regex.Replace(userInput, @"[^A-Za-z0-9]+", "-").Trim().ToLower()) ? true : false;
        }

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();
            LoadData();
            LoadCountries();
            LoadGeneralInfo();
            LoadBillingInfo();

            BtnAddNewCard.Text = "Add New Card";

            if (vSession.User.AccountStatus == (int)AccountStatus.Deleting)
            {
                liEditAccount.Visible = liEditBillingAccount.Visible = liAccountSettings.Visible = false;
            }
            else if (vSession.User.AccountStatus == (int)AccountStatus.Completed)
            {
                liEditBillingAccount.Visible = (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) || Sql.IsUserAdministrator(vSession.User.Id, session)) ? true : false;

                liAccountSettings.Visible = vSession.User.AccountStatus == (int)AccountStatus.Completed;    // && Sql.IsUserAdministrator(vSession.User.Id, session);
            }

            LoadEmailSettings();

            if (!IsPostBack)
            {

                UcMessageAlert.Visible = false;
            }

            BtnDeleteAccountData.Enabled = vSession.User.AccountStatus == (int)AccountStatus.Completed;
        }

        private void LoadCreditCardInfo()
        {
            if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
            {
                ElioUsersCreditCards cc = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);
                if (cc != null)
                {
                    TbxFullName.Text = cc.CardFullname;
                    TbxAddress1.Text = cc.Address1;
                    TbxAddress2.Text = cc.Address2;
                    TbxCardType.Text = cc.CardType;
                    DrpExpMonth.SelectedItem.Text = cc.ExpMonth.ToString();
                    TbxExpYear.Text = cc.ExpYear.ToString();
                    TbxOrigin.Text = cc.Origin;
                    TbxCardType.Text = cc.CardType;
                }
                else
                {
                    BtnSaveCreditCardDetails.Enabled = false;
                    GlobalMethods.ShowMessageControlDA(UcMessageControl, "You do not have any credit card register yet.", MessageTypes.Info, true, true, false);
                }
            }
            else
            {
                BtnSaveCreditCardDetails.Enabled = false;
                GlobalMethods.ShowMessageControlDA(UcMessageControl, "You do not have any credit card register yet.", MessageTypes.Info, true, true, false);
            }
        }

        private void LoadBillingInfo()
        {
            if (vSession.User.HasBillingDetails == 1)
            {
                #region Load Billing Account Data

                ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(vSession.User.Id, session);

                if (account != null)
                {
                    TbxBillingCompanyName.Text = vSession.User.CompanyName;
                    TbxBillingCompanyAddress.Text = account.CompanyBillingAddress;
                    TbxBillingCompanyPostCode.Text = account.CompanyPostCode;
                    TbxBillingCompanyVatNumber.Text = account.CompanyVatNumber;
                    //TbxUserIdNumber.Text = account.UserIdNumber;
                    //TbxUserVatNumber.Text = account.UserVatNumber;
                    //TbxUserBillingEmail.Text = account.BillingEmail;
                    //account.HasVat = 1;
                    //account.Sysdate = DateTime.Now;
                    //account.LastUpdated = DateTime.Now;
                    //account.IsActive = 1;
                }

                #endregion
            }
        }

        private void LoadData()
        {
            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                if (packet != null)
                {
                    LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        LblBillingType.Text = LblPricingPlan.Text + " user";
                    }
                }

                LblRenewalHead.Text = "Renewal date: ";
                LblRenewalHead.Visible = LblRenewal.Visible = true;

                try
                {
                    LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                }
                catch (Exception)
                {
                    Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                }
            }
            else
            {
                LblRenewalHead.Visible = LblRenewal.Visible = false;
                LblPricingPlan.Text = "You are currently on a free plan";
                LblBillingType.Text = "Freemium user";
            }

            aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;

            LblElioplusDashboard.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " dashboard" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " dashboard" : vSession.User.Username + " dashboard";

            LblDashboard.Text = "Dashboard";
            LblBtnGoPremium.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "label", "17")).Text;
            LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Edit account";
            LblDashSubTitle.Text = "edit your account";
            LblRenewalHead.Text = "Renewal date: ";

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                liLogo.Visible = true;
                ImgCompanyLogo.ImageUrl = !string.IsNullOrEmpty(vSession.User.CompanyLogo) ? vSession.User.CompanyLogo : !string.IsNullOrEmpty(vSession.User.PersonalImage) ? vSession.User.PersonalImage : "/assets/layouts/layout/img/avatar.png";
                ImgCompanyLogo.AlternateText = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName + " on Elioplus" : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName + " on Elioplus" : "Company logo on Elioplus";
                liWebsite.Visible = true;
                HpLnkWebSite.NavigateUrl = HpLnkWebSite.Text = vSession.User.WebSite;
                liOfficialEmail.Visible = true;
                LblOfficialEmail.Text = vSession.User.OfficialEmail;
                liAddress.Visible = true;
                LblAddress.Text = vSession.User.Address;
                liPhone.Visible = true;
                LblPhone.Text = vSession.User.Phone;
                PnlEditLogo.Visible = true;
                divRightPanelInfo.Visible = true;
                LblCompanyName.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName : (!string.IsNullOrEmpty(vSession.User.FirstName) && !string.IsNullOrEmpty(vSession.User.LastName)) ? vSession.User.FirstName + " " + vSession.User.LastName : vSession.User.Username;
                LblOverview.Text = !string.IsNullOrEmpty(vSession.User.Overview) ? GlobalMethods.FixParagraphsView(vSession.User.Overview) : "";
                LblDescription.Text = !string.IsNullOrEmpty(vSession.User.Description) ? GlobalMethods.FixParagraphsView(vSession.User.Description) : "";
                liCountry.Visible = true;
                LblCountry.Text = vSession.User.Country;
                LblRegDate.Text = vSession.User.SysDate != null ? vSession.User.SysDate.ToString("MM/dd/yyyy") : "-";
                liType.Visible = true;
                LblType.Text = liType.Visible ? vSession.User.CompanyType : "";
                liSubcategories.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                liCompanyBusinessInfo.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                divCompanyName.Visible = divCompanyOfficialEmail.Visible = divCompanyWebsite.Visible = divCompanyCountry.Visible = divCompanyAddress.Visible = divCompanyPhone.Visible = divCompanyOverview.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                divIndustriesSelection.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                divProgramsSelection.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                divMarketsSelection.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                divAPIsSelection.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed) && vSession.User.CompanyType != EnumHelper.GetDescription(Types.Resellers).ToString());
                divCompanyProductDemo.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed) && vSession.User.CompanyType == Types.Vendors.ToString());
                liCompanySubcategories.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed) && vSession.User.CompanyType != Types.Developers.ToString());
                liCompanyProducts.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);    //&& vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString());
                PnlEditLogo.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                liCompanyBusinessInfo.Visible = true;

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    divCompanyDescription.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed);
                    divMashape.Visible = true;
                    TbxMashape.Text = vSession.User.MashapeUsername;
                }

                LoadIndustries();
                LoadPrograms();
                LoadMarkets();
                LoadSubcategories();

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    LoadAPIes();
                    LoadIntegrations();

                    RcbxIntegrations.Visible = true;
                    RcbxIntegrations.Entries.Clear();
                    RcbxIntegrations.EmptyMessage = "Start typing the name of the integrations you use";
                    RcbxProducts.Visible = false;
                }
                else
                {
                    LoadProducts();

                    RcbxProducts.Visible = true;
                    RcbxProducts.Entries.Clear();
                    RcbxProducts.EmptyMessage = "Start typing the name of the products you use";
                    RcbxIntegrations.Visible = false;
                }
            }

            LblUsername.Text = vSession.User.Username;
            LblEmail.Text = vSession.User.Email;

            if (vSession.User.UserApplicationType == (int)UserApplicationType.ThirdParty && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic && vSession.User.AccountStatus == (int)AccountStatus.Completed) // && Sql.IsUserAdministrator(vSession.User.Id, session))
            {
                LoadPersonalData();
            }
            else
                liPersonalInfo.Visible = false;
        }

        private void LoadProducts()
        {
            List<ElioRegistrationProducts> products = Sql.GetRegistrationProductsDescriptionByUserId(vSession.User.Id, session);

            CbxUserProductsIntegrationsList.Items.Clear();

            if (products.Count > 0)
            {
                foreach (ElioRegistrationProducts product in products)
                {
                    ListItem item = new ListItem();

                    item.Value = product.Id.ToString();
                    item.Text = product.Description;

                    item.Selected = true;

                    CbxUserProductsIntegrationsList.Items.Add(item);
                }
            }
        }

        private void LoadIntegrations()
        {
            List<ElioRegistrationIntegrations> integrations = Sql.GetUserRegistrationIntegrations(vSession.User.Id, session);

            CbxUserProductsIntegrationsList.Items.Clear();

            if (integrations.Count > 0)
            {
                foreach (ElioRegistrationIntegrations integration in integrations)
                {
                    ListItem item = new ListItem();

                    item.Value = integration.Id.ToString();
                    item.Text = integration.Description;

                    item.Selected = true;

                    CbxUserProductsIntegrationsList.Items.Add(item);
                }
            }
        }

        private void LoadPersonalData()
        {
            ElioUsersPerson person = ClearbitSql.GetPersonByUserId(vSession.User.Id, session);
            if (person != null)
            {
                liPersonalInfo.Visible = true;

                TbxPersonLastName.Text = person.FamilyName;
                TbxPersonFirstName.Text = person.GivenName;
                TbxPersonPhone.Text = person.Phone;
                TbxPersonLocation.Text = person.Location;
                TbxPersonTimeZone.Text = person.TimeZone;
                TbxPersonTitle.Text = person.Title;
                TbxPersonRole.Text = person.Role;
                TbxPersonSeniority.Text = person.Seniority;
                TbxPersonTwitterHandle.Text = person.TwitterHandle;
                TbxPersonAboutMeHandle.Text = person.AboutMeHandle;
                ImgPersonAvatarBckgrd.ImageUrl = (!string.IsNullOrEmpty(person.Avatar)) ? person.Avatar : "https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
                ImgPersonAvatarBckgrd.AlternateText = "Update personal image";
                TbxPersonBio.Text = person.Bio;
            }
            else
                liPersonalInfo.Visible = false;
        }

        private void LoadGeneralInfo()
        {
            TbxCompanyName.Text = !string.IsNullOrEmpty(vSession.User.CompanyName) ? vSession.User.CompanyName : "";
            TbxCompanyUsername.Text = !string.IsNullOrEmpty(vSession.User.Username) ? vSession.User.Username : "";
            TbxCompanyEmail.Text = !string.IsNullOrEmpty(vSession.User.Email) ? vSession.User.Email : "";
            TbxCompanyOfficialEmail.Text = !string.IsNullOrEmpty(vSession.User.OfficialEmail) ? vSession.User.OfficialEmail : "";
            TbxCompanyWebsite.Text = !string.IsNullOrEmpty(vSession.User.WebSite) ? vSession.User.WebSite : "";

            if (!string.IsNullOrEmpty(vSession.User.Country))
            {
                DdlCountries.Items.FindByText(vSession.User.Country).Selected = true;
            }
            else
            {
                DdlCountries.SelectedItem.Value = "0";
            }

            TbxCompanyAddress.Text = !string.IsNullOrEmpty(vSession.User.Address) ? vSession.User.Address : "";
            TbxCompanyPhone.Text = !string.IsNullOrEmpty(vSession.User.Phone) ? vSession.User.Phone : "";
            TbxCompanyProductDemo.Text = !string.IsNullOrEmpty(vSession.User.VendorProductDemoLink) ? vSession.User.VendorProductDemoLink : "";
            ImgPhotoBckgr.ImageUrl = !string.IsNullOrEmpty(vSession.User.CompanyLogo) ? vSession.User.CompanyLogo : "https://www.placehold.it/200x150/EFEFEF/AAAAAA&amp;text=no+image";
            TbxCompanyOverview.Text = !string.IsNullOrEmpty(vSession.User.Overview) ? vSession.User.Overview : "";
            TbxCompanyDescription.Text = !string.IsNullOrEmpty(vSession.User.Description) ? vSession.User.Description : "";
            TbxMashape.Text = !string.IsNullOrEmpty(vSession.User.MashapeUsername) ? vSession.User.MashapeUsername : "";
        }

        private void LoadSubcategories()
        {
            rowSalMark.Visible = false;
            rowCustMan.Visible = false;
            rowProjMan.Visible = false;
            rowOperWork.Visible = false;
            rowTracKMeaus.Visible = false;
            rowAccFin.Visible = false;
            rowHR.Visible = false;
            rowWMSD.Visible = false;
            rowITInfr.Visible = false;
            rowBusUtil.Visible = false;
            rowSecBack.Visible = false;
            rowDesMult.Visible = false;
            rowMisc.Visible = false;
            rowUnCom.Visible = false;
            rowCadPlm.Visible = false;
            rowHrdware.Visible = false;

            Hdn1_1.Value = "0";
            Hdn1_2.Value = "0";
            Hdn1_3.Value = "0";
            Hdn1_4.Value = "0";
            Hdn1_5.Value = "0";
            Hdn1_6.Value = "0";
            Hdn1_7.Value = "0";
            Hdn1_8.Value = "0";
            Hdn1_9.Value = "0";
            Hdn1_10.Value = "0";
            Hdn1_11.Value = "0";
            Hdn1_12.Value = "0";
            Hdn1_13.Value = "0";
            Hdn1_14.Value = "0";
            Hdn1_15.Value = "0";
            Hdn1_16.Value = "0";
            Hdn1_17.Value = "0";
            Hdn1_18.Value = "0";

            Hdn2_1.Value = "0";
            Hdn2_2.Value = "0";
            Hdn2_3.Value = "0";
            Hdn2_4.Value = "0";
            Hdn2_5.Value = "0";
            Hdn2_6.Value = "0";

            Hdn3_1.Value = "0";
            Hdn3_2.Value = "0";
            Hdn3_3.Value = "0";

            Hdn4_1.Value = "0";
            Hdn4_2.Value = "0";
            Hdn4_3.Value = "0";
            Hdn4_4.Value = "0";
            Hdn4_5.Value = "0";
            Hdn4_6.Value = "0";
            Hdn4_7.Value = "0";
            Hdn4_8.Value = "0";
            Hdn4_9.Value = "0";
            Hdn4_10.Value = "0";
            Hdn4_11.Value = "0";
            Hdn4_12.Value = "0";
            Hdn4_13.Value = "0";
            Hdn4_14.Value = "0";
            Hdn4_15.Value = "0";
            Hdn4_16.Value = "0";
            Hdn4_17.Value = "0";
            Hdn4_18.Value = "0";
            Hdn4_19.Value = "0";
            Hdn4_20.Value = "0";
            Hdn4_21.Value = "0";
            Hdn4_22.Value = "0";

            Hdn5_1.Value = "0";
            Hdn5_2.Value = "0";
            Hdn5_3.Value = "0";
            Hdn5_4.Value = "0";
            Hdn5_5.Value = "0";

            Hdn6_1.Value = "0";
            Hdn6_2.Value = "0";
            Hdn6_3.Value = "0";
            Hdn6_4.Value = "0";
            Hdn6_5.Value = "0";

            Hdn7_1.Value = "0";
            Hdn7_2.Value = "0";
            Hdn7_3.Value = "0";
            Hdn7_4.Value = "0";
            Hdn7_5.Value = "0";
            Hdn7_6.Value = "0";
            Hdn7_7.Value = "0";

            Hdn8_1.Value = "0";
            Hdn8_2.Value = "0";
            Hdn8_3.Value = "0";
            Hdn8_4.Value = "0";
            Hdn8_5.Value = "0";
            Hdn8_6.Value = "0";
            Hdn8_7.Value = "0";
            Hdn8_8.Value = "0";
            Hdn8_9.Value = "0";

            Hdn9_1.Value = "0";
            Hdn9_2.Value = "0";
            Hdn9_3.Value = "0";
            Hdn9_4.Value = "0";
            Hdn9_5.Value = "0";
            Hdn9_6.Value = "0";
            Hdn9_7.Value = "0";
            Hdn9_8.Value = "0";
            Hdn9_9.Value = "0";
            Hdn9_10.Value = "0";
            Hdn9_11.Value = "0";
            Hdn9_12.Value = "0";
            Hdn9_13.Value = "0";

            Hdn10_1.Value = "0";
            Hdn10_2.Value = "0";
            Hdn10_3.Value = "0";
            Hdn10_4.Value = "0";
            Hdn10_5.Value = "0";
            Hdn10_6.Value = "0";
            Hdn10_7.Value = "0";
            Hdn10_8.Value = "0";
            Hdn10_9.Value = "0";

            Hdn11_1.Value = "0";
            Hdn11_2.Value = "0";
            Hdn11_3.Value = "0";
            Hdn11_4.Value = "0";
            Hdn11_5.Value = "0";
            Hdn11_6.Value = "0";
            Hdn11_7.Value = "0";
            Hdn11_8.Value = "0";
            Hdn11_9.Value = "0";
            Hdn11_10.Value = "0";
            Hdn11_11.Value = "0";
            Hdn11_12.Value = "0";
            Hdn11_13.Value = "0";
            Hdn11_14.Value = "0";
            Hdn11_15.Value = "0";
            Hdn11_16.Value = "0";

            Hdn12_1.Value = "0";
            Hdn12_2.Value = "0";
            Hdn12_3.Value = "0";
            Hdn12_4.Value = "0";

            Hdn13_1.Value = "0";
            Hdn13_2.Value = "0";
            Hdn13_3.Value = "0";

            Hdn14_1.Value = "0";
            Hdn14_2.Value = "0";
            Hdn14_3.Value = "0";
            Hdn14_4.Value = "0";
            Hdn14_5.Value = "0";
            Hdn14_6.Value = "0";
            Hdn14_7.Value = "0";
            Hdn14_8.Value = "0";
            Hdn14_9.Value = "0";
            Hdn14_10.Value = "0";
            Hdn14_11.Value = "0";
            Hdn14_12.Value = "0";

            Hdn15_1.Value = "0";
            Hdn15_2.Value = "0";
            Hdn15_3.Value = "0";
            Hdn15_4.Value = "0";
            Hdn15_5.Value = "0";
            Hdn15_6.Value = "0";
            Hdn15_7.Value = "0";
            Hdn15_8.Value = "0";

            Hdn16_1.Value = "0";
            Hdn16_2.Value = "0";
            Hdn16_3.Value = "0";
            Hdn16_4.Value = "0";
            Hdn16_5.Value = "0";
            Hdn16_6.Value = "0";
            Hdn16_7.Value = "0";
            Hdn16_8.Value = "0";
            Hdn16_9.Value = "0";

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                rowSalMark.Visible = true;
                LblSalMark.Text = "Sales & Marketing";
                LblSalMarkValue.Text = "'Full registration required'";

                rowCustMan.Visible = true;
                LblCustMan.Text = "Customer Management";
                LblCustManValue.Text = "'Full registration required'";

                rowProjMan.Visible = true;
                LblProjMan.Text = "Project Management";
                LblProjManValue.Text = "'Full registration required'";

                rowOperWork.Visible = true;
                LblOperWork.Text = "Operations & Workflow";
                LblOperWorkValue.Text = "'Full registration required'";

                rowTracKMeaus.Visible = true;
                LblTracKMeaus.Text = "Tracking & Measurement";
                LblTracKMeausValue.Text = "'Full registration required'";

                rowAccFin.Visible = true;
                LblAccFin.Text = "Accounting & Financials";
                LblAccFinValue.Text = "'Full registration required'";

                rowHR.Visible = true;
                LblHR.Text = "HR";
                LblHRValue.Text = "'Full registration required'";

                rowWMSD.Visible = true;
                LblWMSD.Text = "Web Mobile Software Development";
                LblWMSDValue.Text = "'Full registration required'";

                rowITInfr.Visible = true;
                LblITInfr.Text = "IT & Infrastructure";
                LblITInfrValue.Text = "'Full registration required'";

                rowBusUtil.Visible = true;
                LblBusUtil.Text = "Business Utilities";
                LblBusUtilValue.Text = "'Full registration required'";

                rowSecBack.Visible = true;
                LblSecBack.Text = "Data Security & GRC";
                LblSecBackValue.Text = "'Full registration required'";

                rowDesMult.Visible = true;
                LblDesMult.Text = "Design & Multimedia";
                LblDesMultValue.Text = "'Full registration required'";

                rowMisc.Visible = true;
                LblMisc.Text = "Miscellaneous";
                LblMiscValue.Text = "'Full registration required'";

                rowUnCom.Visible = true;
                LblUnCom.Text = "Unified Communications";
                LblUnComValue.Text = "'Full registration required'";

                rowCadPlm.Visible = true;
                LblCadPlm.Text = "CAD & PLM";
                LblCadPlmValue.Text = "'Full registration required'";

                rowHrdware.Visible = true;
                LblHrdware.Text = "Hardware";
                LblHrdwareValue.Text = "'Full registration required'";
            }
            else
            {
                List<ElioSubcategoriesIJSubcategoriesGroups> userSubcategories = Sql.GetUserSubcategoriesAndGroups(vSession.User.Id, session);

                LblSalMarkValue.Text = string.Empty;
                LblCustManValue.Text = string.Empty;
                LblOperWorkValue.Text = string.Empty;
                LblTracKMeausValue.Text = string.Empty;
                LblAccFinValue.Text = string.Empty;
                LblHRValue.Text = string.Empty;
                LblITInfrValue.Text = string.Empty;
                LblBusUtilValue.Text = string.Empty;
                LblDesMultValue.Text = string.Empty;
                LblWMSDValue.Text = string.Empty;
                LblSecBackValue.Text = string.Empty;
                LblMiscValue.Text = string.Empty;
                LblUnComValue.Text = string.Empty;
                LblCadPlmValue.Text=string.Empty;
                LblProjManValue.Text = string.Empty;
                LblHrdwareValue.Text = string.Empty;

                foreach (ElioSubcategoriesIJSubcategoriesGroups sub in userSubcategories)
                {
                    if (sub.SubcategoryGroupDescription == "Sales & Marketing")
                    {
                        rowSalMark.Visible = true;
                        LblSalMark.Text = "Sales & Marketing";
                        LblSalMarkValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Email Marketing")
                        {
                            Hdn1_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Campaign Management")
                        {
                            Hdn1_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Marketing Automation")
                        {
                            Hdn1_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Content Marketing")
                        {
                            Hdn1_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "SEO & SEM")
                        {
                            Hdn1_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Social Media Marketing")
                        {
                            Hdn1_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Affiliate Marketing")
                        {
                            Hdn1_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Surveys & Forms")
                        {
                            Hdn1_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Ad Serving")
                        {
                            Hdn1_9.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Event Management")
                        {
                            Hdn1_10.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Sales Process Management")
                        {
                            Hdn1_11.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Quotes & Orders")
                        {
                            Hdn1_12.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Document Management")
                        {
                            Hdn1_13.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Sales Intelligence")
                        {
                            Hdn1_14.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Engagement Tools")
                        {
                            Hdn1_15.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "POS")
                        {
                            Hdn1_16.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "E-Signature")
                        {
                            Hdn1_17.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "ECM")
                        {
                            Hdn1_18.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Customer Management")
                    {
                        rowCustMan.Visible = true;
                        LblCustMan.Text = "Customer Management";
                        LblCustManValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "CRM")
                        {
                            Hdn2_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Help Desk")
                        {
                            Hdn2_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Live Chat")
                        {
                            Hdn2_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Feedback Management")
                        {
                            Hdn2_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Gamification & Loyalty")
                        {
                            Hdn2_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Chatbot")
                        {
                            Hdn2_6.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Project Management")
                    {
                        rowProjMan.Visible = true;
                        LblProjMan.Text = "Project Management";
                        LblProjManValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Project Management Tools")
                        {
                            Hdn3_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Knowledge Management")
                        {
                            Hdn3_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "File Sharing Software")
                        {
                            Hdn3_3.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Operations & Workflow")
                    {
                        rowOperWork.Visible = true;
                        LblOperWork.Text = "Operations & Workflow";
                        LblOperWorkValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Business Process Management")
                        {
                            Hdn4_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Digital Asset Management")
                        {
                            Hdn4_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "ERP")
                        {
                            Hdn4_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Inventory Management")
                        {
                            Hdn4_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Shipping & Tracking")
                        {
                            Hdn4_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Supply Chain Management")
                        {
                            Hdn4_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Warehouse Management")
                        {
                            Hdn4_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Supply Chain Execution")
                        {
                            Hdn4_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Track Management")
                        {
                            Hdn4_9.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Workflow Management")
                        {
                            Hdn4_10.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Enterprise Asset Management")
                        {
                            Hdn4_11.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Facility Management")
                        {
                            Hdn4_12.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Asset Lifecycle Management")
                        {
                            Hdn4_13.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "CMMS")
                        {
                            Hdn4_14.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Fleet Management")
                        {
                            Hdn4_15.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Change Management")
                        {
                            Hdn4_16.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Procurement")
                        {
                            Hdn4_17.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Field Services Management")
                        {
                            Hdn4_18.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "PRM")
                        {
                            Hdn4_19.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Robotic Process Automation")
                        {
                            Hdn4_20.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "ITSM")
                        {
                            Hdn4_21.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Artificial Intelligence")
                        {
                            Hdn4_22.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Tracking & Measurement")
                    {
                        rowTracKMeaus.Visible = true;
                        LblTracKMeaus.Text = "Tracking & Measurement";
                        LblTracKMeausValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Analytics Software")
                        {
                            Hdn5_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Business Intelligence")
                        {
                            Hdn5_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Data Visualization")
                        {
                            Hdn5_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Competitive Intelligence")
                        {
                            Hdn5_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Location Intelligence")
                        {
                            Hdn5_5.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Accounting & Financials")
                    {
                        rowAccFin.Visible = true;
                        LblAccFin.Text = "Accounting & Financials";
                        LblAccFinValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Accounting")
                        {
                            Hdn6_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Payment Processing")
                        {
                            Hdn6_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Time & Expenses")
                        {
                            Hdn6_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Billing & Invoicing")
                        {
                            Hdn6_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Budgeting")
                        {
                            Hdn6_5.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "HR")
                    {
                        rowHR.Visible = true;
                        LblHR.Text = "HR";
                        LblHRValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Applicant Tracking")
                        {
                            Hdn7_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "HR Administration")
                        {
                            Hdn7_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Payroll")
                        {
                            Hdn7_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Performance Management")
                        {
                            Hdn7_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Recruiting")
                        {
                            Hdn7_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Learning Management System")
                        {
                            Hdn7_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Time & Expense")
                        {
                            Hdn7_7.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Web Mobile Software Development")
                    {
                        rowWMSD.Visible = true;
                        LblWMSD.Text = "Web Mobile Software Development";

                        LblWMSDValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "API Tools")
                        {
                            Hdn8_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Bug Trackers")
                        {
                            Hdn8_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Development Tools")
                        {
                            Hdn8_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "eCommerce")
                        {
                            Hdn8_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Frameworks & Libraries")
                        {
                            Hdn8_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Mobile Development")
                        {
                            Hdn8_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Optimization")
                        {
                            Hdn8_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Usability Testing")
                        {
                            Hdn8_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Websites")
                        {
                            Hdn8_9.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "IT & Infrastructure")
                    {
                        rowITInfr.Visible = true;
                        LblITInfr.Text = "IT & Infrastructure";
                        LblITInfrValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Cloud Integration (iPaaS)")
                        {
                            Hdn9_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Cloud Management")
                        {
                            Hdn9_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Cloud Storage")
                        {
                            Hdn9_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Remote Access")
                        {
                            Hdn9_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Virtualization")
                        {
                            Hdn9_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Web Hosting")
                        {
                            Hdn9_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Web Monitoring")
                        {
                            Hdn9_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Big Data")
                        {
                            Hdn9_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Data Warehousing")
                        {
                            Hdn9_9.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Databases")
                        {
                            Hdn9_10.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Data Integration")
                        {
                            Hdn9_11.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Data Management")
                        {
                            Hdn9_12.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Networking")
                        {
                            Hdn9_13.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Business Utilities")
                    {
                        rowBusUtil.Visible = true;
                        LblBusUtil.Text = "Business Utilities";
                        LblBusUtilValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Calendar & Scheduling")
                        {
                            Hdn10_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Email")
                        {
                            Hdn10_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Note Taking")
                        {
                            Hdn10_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Password Management")
                        {
                            Hdn10_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Presentations")
                        {
                            Hdn10_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Productivity Suites")
                        {
                            Hdn10_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Spreadsheets")
                        {
                            Hdn10_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Task Management")
                        {
                            Hdn10_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Time Management")
                        {
                            Hdn10_9.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Data Security & GRC")
                    {
                        rowSecBack.Visible = true;
                        LblSecBack.Text = "Data Security & GRC";
                        LblSecBackValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Cybersecurity")
                        {
                            Hdn11_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Vulnerability Management")
                        {
                            Hdn11_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Firewall")
                        {
                            Hdn11_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Mobile Data Security")
                        {
                            Hdn11_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Backup & Restore")
                        {
                            Hdn11_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Data Masking")
                        {
                            Hdn11_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Identity Management")
                        {
                            Hdn11_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Risk Management")
                        {
                            Hdn11_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Penetration Testing")
                        {
                            Hdn11_9.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Application Security")
                        {
                            Hdn11_10.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Governance, Risk & Compliance (GRC)")
                        {
                            Hdn11_11.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Compliance")
                        {
                            Hdn11_12.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Fraud Prevention")
                        {
                            Hdn11_13.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Email Security")
                        {
                            Hdn11_14.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Endpoint Security")
                        {
                            Hdn11_15.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "VPN")
                        {
                            Hdn11_16.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Design & Multimedia")
                    {
                        rowDesMult.Visible = true;
                        LblDesMult.Text = "Design & Multimedia";
                        LblDesMultValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Graphic Design")
                        {
                            Hdn12_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Infographics")
                        {
                            Hdn12_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Video Editing")
                        {
                            Hdn12_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Video Management System")
                        {
                            Hdn12_4.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Miscellaneous")
                    {
                        rowMisc.Visible = true;
                        LblMisc.Text = "Miscellaneous";
                        LblMiscValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "eLearning")
                        {
                            Hdn13_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Healthcare")
                        {
                            Hdn13_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Simulation Software")
                        {
                            Hdn13_3.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Unified Communications")
                    {
                        rowUnCom.Visible = true;
                        LblUnCom.Text = "Unified Communications";
                        LblUnComValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "Chat & Web Conference")
                        {
                            Hdn14_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "VOIP")
                        {
                            Hdn14_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Mobility")
                        {
                            Hdn14_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Collaboration")
                        {
                            Hdn14_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Conferencing")
                        {
                            Hdn14_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Unified Messaging")
                        {
                            Hdn14_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Unified Communications")
                        {
                            Hdn14_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Team Collaboration")
                        {
                            Hdn14_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Video Conferencing")
                        {
                            Hdn14_9.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Contact Center")
                        {
                            Hdn14_10.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Connectivity")
                        {
                            Hdn14_11.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "WiFi")
                        {
                            Hdn14_12.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "CAD & PLM")
                    {
                        rowCadPlm.Visible = true;
                        LblCadPlm.Text = "CAD & PLM";
                        LblCadPlmValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "General-Purpose CAD")
                        {
                            Hdn15_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "CAM")
                        {
                            Hdn15_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "PLM")
                        {
                            Hdn15_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "PDM (Product Data Management)")
                        {
                            Hdn15_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "BIM")
                        {
                            Hdn15_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "3D Architecture")
                        {
                            Hdn15_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "3D CAD")
                        {
                            Hdn15_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "GIS")
                        {
                            Hdn15_8.Value = "1";
                        }
                    }
                    else if (sub.SubcategoryGroupDescription == "Hardware")
                    {
                        rowHrdware.Visible = true;
                        LblHrdware.Text = "Hardware";
                        LblHrdwareValue.Text += sub.SubCategoryDescription + ", ";

                        if (sub.SubCategoryDescription == "CCTV Video Surveillance")
                        {
                            Hdn16_1.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Access Control Systems")
                        {
                            Hdn16_2.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Perimeter Security Systems")
                        {
                            Hdn16_3.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Alarms and Automation")
                        {
                            Hdn16_4.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "IoT")
                        {
                            Hdn16_5.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Monitors Cameras and Sensors")
                        {
                            Hdn16_6.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Switches")
                        {
                            Hdn16_7.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Intercom Systems")
                        {
                            Hdn16_8.Value = "1";
                        }
                        if (sub.SubCategoryDescription == "Servers")
                        {
                            Hdn16_9.Value = "1";
                        }
                    }
                }

                LblSalMarkValue.Text = !string.IsNullOrEmpty(LblSalMarkValue.Text) ? LblSalMarkValue.Text.Substring(0, LblSalMarkValue.Text.Length - 2) : "";
                LblCustManValue.Text = !string.IsNullOrEmpty(LblCustManValue.Text) ? LblCustManValue.Text.Substring(0, LblCustManValue.Text.Length - 2) : "";
                LblProjManValue.Text = !string.IsNullOrEmpty(LblProjManValue.Text) ? LblProjManValue.Text.Substring(0, LblProjManValue.Text.Length - 2) : "";
                LblOperWorkValue.Text = !string.IsNullOrEmpty(LblOperWorkValue.Text) ? LblOperWorkValue.Text.Substring(0, LblOperWorkValue.Text.Length - 2) : "";
                LblTracKMeausValue.Text = !string.IsNullOrEmpty(LblTracKMeausValue.Text) ? LblTracKMeausValue.Text.Substring(0, LblTracKMeausValue.Text.Length - 2) : "";
                LblAccFinValue.Text = !string.IsNullOrEmpty(LblAccFinValue.Text) ? LblAccFinValue.Text.Substring(0, LblAccFinValue.Text.Length - 2) : "";
                LblHRValue.Text = !string.IsNullOrEmpty(LblHRValue.Text) ? LblHRValue.Text.Substring(0, LblHRValue.Text.Length - 2) : "";
                LblWMSDValue.Text = !string.IsNullOrEmpty(LblWMSDValue.Text) ? LblWMSDValue.Text.Substring(0, LblWMSDValue.Text.Length - 2) : "";
                LblITInfrValue.Text = !string.IsNullOrEmpty(LblITInfrValue.Text) ? LblITInfrValue.Text.Substring(0, LblITInfrValue.Text.Length - 2) : "";
                LblBusUtilValue.Text = !string.IsNullOrEmpty(LblBusUtilValue.Text) ? LblBusUtilValue.Text.Substring(0, LblBusUtilValue.Text.Length - 2) : "";
                LblSecBackValue.Text = !string.IsNullOrEmpty(LblSecBackValue.Text) ? LblSecBackValue.Text.Substring(0, LblSecBackValue.Text.Length - 2) : "";
                LblDesMultValue.Text = !string.IsNullOrEmpty(LblDesMultValue.Text) ? LblDesMultValue.Text.Substring(0, LblDesMultValue.Text.Length - 2) : "";
                LblMiscValue.Text = !string.IsNullOrEmpty(LblMiscValue.Text) ? LblMiscValue.Text.Substring(0, LblMiscValue.Text.Length - 2) : "";
                LblUnComValue.Text = !string.IsNullOrEmpty(LblUnComValue.Text) ? LblUnComValue.Text.Substring(0, LblUnComValue.Text.Length - 2) : "";
                LblCadPlmValue.Text = !string.IsNullOrEmpty(LblCadPlmValue.Text) ? LblCadPlmValue.Text.Substring(0, LblCadPlmValue.Text.Length - 2) : "";
                LblHrdwareValue.Text = !string.IsNullOrEmpty(LblHrdwareValue.Text) ? LblHrdwareValue.Text.Substring(0, LblHrdwareValue.Text.Length - 2) : "";

                ScriptManager.RegisterStartupScript(this, GetType(), "SetVerticals", "SetVerticals();", true);
            }
        }

        private void LoadIndustries()
        {
            rowIndustry.Visible = false;
            HdnIndAdvMark.Value = "0";
            HdnIndCommun.Value = "0";
            HdnIndConsWeb.Value = "0";
            HdnIndDigMed.Value = "0";
            HdnIndEcom.Value = "0";
            HdnIndEduc.Value = "0";
            HdnIndEnter.Value = "0";
            HdnIndEntGam.Value = "0";
            HdnIndHard.Value = "0";
            HdnIndMob.Value = "0";
            HdnIndNetHos.Value = "0";
            HdnIndSocMed.Value = "0";
            HdnIndSoft.Value = "0";
            HdnInd14.Value = "0";
            HdnInd15.Value = "0";
            HdnInd16.Value = "0";
            HdnInd17.Value = "0";
            HdnInd18.Value = "0";
            HdnInd19.Value = "0";
            HdnInd20.Value = "0";
            HdnInd21.Value = "0";
            HdnInd22.Value = "0";
            HdnInd23.Value = "0";
            HdnInd24.Value = "0";
            HdnInd25.Value = "0";
            HdnInd26.Value = "0";

            HdnInd27.Value = "0";
            HdnInd28.Value = "0";
            HdnInd29.Value = "0";
            HdnInd30.Value = "0";
            HdnInd31.Value = "0";
            HdnInd32.Value = "0";
            HdnInd33.Value = "0";
            HdnInd34.Value = "0";
            HdnInd35.Value = "0";
            HdnInd36.Value = "0";
            HdnInd37.Value = "0";
            HdnInd38.Value = "0";
            HdnInd39.Value = "0";

            HdnInd40.Value = "0";
            HdnInd41.Value = "0";
            HdnInd42.Value = "0";

            LblIndustryValue.Text = string.Empty;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                rowIndustry.Visible = true;
                LblIndustry.Text = "Industry";
                LblIndustryValue.Text = "'Full registration required!'";
            }
            else
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString() || vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    rowIndustry.Visible = true;
                    LblIndustry.Text = "Industry";

                    List<ElioIndustries> userIndustries = Sql.GetUsersIndustries(vSession.User.Id, session);

                    if (userIndustries.Count > 0)
                    {
                        foreach (ElioIndustries industry in userIndustries)
                        {
                            LblIndustryValue.Text += industry.IndustryDescription + ", ";

                            if (industry.IndustryDescription == "Advertising/Marketing")
                            {
                                HdnIndAdvMark.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Communications")
                            {
                                HdnIndCommun.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Consumer Web")
                            {
                                HdnIndConsWeb.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Digital Media")
                            {
                                HdnIndDigMed.Value = "1";
                            }
                            else if (industry.IndustryDescription == "E-Commerce")
                            {
                                HdnIndEcom.Value = "1";
                            }
                            else if (industry.IndustryDescription == "eLearning")
                            {
                                HdnIndEduc.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Enterprise")
                            {
                                HdnIndEnter.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Entertainment/Games")
                            {
                                HdnIndEntGam.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Hardware")
                            {
                                HdnIndHard.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Mobile")
                            {
                                HdnIndMob.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Network/Hosting")
                            {
                                HdnIndNetHos.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Social Media")
                            {
                                HdnIndSocMed.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Software")
                            {
                                HdnIndSoft.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Aerospace and Defense")
                            {
                                HdnInd14.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Automotive")
                            {
                                HdnInd15.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Banking")
                            {
                                HdnInd16.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Chemicals & Life Sciences")
                            {
                                HdnInd17.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Distribution")
                            {
                                HdnInd18.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Electronics")
                            {
                                HdnInd19.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Energy")
                            {
                                HdnInd20.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Engineering & Construction")
                            {
                                HdnInd21.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Equipment Service and Rental")
                            {
                                HdnInd22.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Fashion")
                            {
                                HdnInd23.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Financial Services and Insurance")
                            {
                                HdnInd24.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Food and Beverage/CPG")
                            {
                                HdnInd25.Value = "1";
                            }
                            else if (industry.IndustryDescription == "General Industry")
                            {
                                HdnInd26.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Healthcare")
                            {
                                HdnInd27.Value = "1";
                            }
                            else if (industry.IndustryDescription == "High Tech & Communications")
                            {
                                HdnInd28.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Hospitality")
                            {
                                HdnInd29.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Legal Services")
                            {
                                HdnInd30.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Logistics and 3PL")
                            {
                                HdnInd31.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Manufacturing")
                            {
                                HdnInd32.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Media")
                            {
                                HdnInd33.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Oil & Gas")
                            {
                                HdnInd34.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Professional Services")
                            {
                                HdnInd35.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Public Sector")
                            {
                                HdnInd36.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Real Estate")
                            {
                                HdnInd37.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Retail")
                            {
                                HdnInd38.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Shipping")
                            {
                                HdnInd39.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Telecommunications")
                            {
                                HdnInd40.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Travel & Transportation")
                            {
                                HdnInd41.Value = "1";
                            }
                            else if (industry.IndustryDescription == "Utilities")
                            {
                                HdnInd42.Value = "1";
                            }
                        }

                        LblIndustryValue.Text = LblIndustryValue.Text.Substring(0, LblIndustryValue.Text.Length - 2);
                    }
                    else
                    {
                        LblIndustryValue.Text = "-";
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "SetIndustries", "SetIndustries();", true);
                }
            }
        }

        private void LoadPrograms()
        {
            rowProgram.Visible = false;
            HdnProgWhiteL.Value = "0";
            HdnProgResel.Value = "0";
            HdnProgVAR.Value = "0";
            HdnProgDistr.Value = "0";
            HdnProgAPIprg.Value = "0";
            HdnProgSysInteg.Value = "0";
            HdnProgServProv.Value = "0";

            LblProgramValue.Text = string.Empty;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                rowProgram.Visible = true;
                LblProgram.Text = "Partner program";
                LblProgramValue.Text = "'Full registration required!'";
            }
            else
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString() || vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    rowProgram.Visible = true;
                    LblProgram.Text = "Partner program";                    

                    List<ElioPartners> userPrograms = Sql.GetUsersPartners(vSession.User.Id, session);

                    if (userPrograms.Count > 0)
                    {
                        foreach (ElioPartners program in userPrograms)
                        {
                            LblProgramValue.Text += program.PartnerDescription + ", ";

                            if (program.PartnerDescription == "White Label")
                            {
                                HdnProgWhiteL.Value = "1";
                            }
                            else if (program.PartnerDescription == "Reseller")
                            {
                                HdnProgResel.Value = "1";
                            }
                            else if (program.PartnerDescription == "Value Added Reseller (VAR)")
                            {
                                HdnProgVAR.Value = "1";
                            }
                            else if (program.PartnerDescription == "Distributor")
                            {
                                HdnProgDistr.Value = "1";
                            }
                            else if (program.PartnerDescription == "API Program (Developers)")
                            {
                                HdnProgAPIprg.Value = "1";
                            }
                            else if (program.PartnerDescription == "System Integrator")
                            {
                                HdnProgSysInteg.Value = "1";
                            }
                            else if (program.PartnerDescription == "Managed Service Provider")
                            {
                                HdnProgServProv.Value = "1";
                            }                        
                        }                        

                        LblProgramValue.Text = LblProgramValue.Text.Substring(0, LblProgramValue.Text.Length - 2);
                    }
                    else
                    {
                        LblProgramValue.Text = "-";
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "SetPrograms", "SetPrograms();", true);
                }
            }
        }

        private void LoadMarkets()
        {
            rowMarket.Visible = false;
            HdnMarkConsum.Value = "0";
            HdnMarkSOHO.Value = "0";
            HdnMarkSmallMid.Value = "0";
            HdnMarkEnter.Value = "0";

            LblMarketValue.Text = string.Empty;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                rowMarket.Visible = true;
                LblMarket.Text = "Market specialisation";
                LblMarketValue.Text = "'Full registration required!'";
            }
            else
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString() || vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    rowMarket.Visible = true;
                    LblMarket.Text = "Market specialisation";                    

                    List<ElioMarkets> userMarkets = Sql.GetUsersMarkets(vSession.User.Id, session);

                    if (userMarkets.Count > 0)
                    {
                        foreach (ElioMarkets market in userMarkets)
                        {
                            LblMarketValue.Text += market.MarketDescription + ", ";

                            if (market.MarketDescription == "Consumers (B2C)")
                            {
                                HdnMarkConsum.Value = "1";   
                            }
                            else if (market.MarketDescription == "Small office - home office: SOHO (B2B)")
                            {
                                HdnMarkSOHO.Value = "1";
                            }
                            else if (market.MarketDescription == "Small & mid-sized businesses (B2B)")
                            {
                                HdnMarkSmallMid.Value = "1";
                            }
                            else if (market.MarketDescription == "Enterprise (B2B)")
                            {
                                HdnMarkEnter.Value = "1";
                            }                            
                        }

                        LblMarketValue.Text = LblMarketValue.Text.Substring(0, LblMarketValue.Text.Length - 2);
                    }
                    else
                    {
                        LblMarketValue.Text = "-";
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "SetMarkets", "SetMarkets();", true);
                }
            }
        }
        
        private void LoadAPIes()
        {
            rowAPI.Visible = false;
            HdnAPIBusServ.Value = "0";
            HdnAPIMedEnter.Value = "0";
            HdnAPIRetEcom.Value = "0";
            HdnAPIGeol.Value = "0";
            HdnAPISoc.Value = "0";
            HdnAPIHeal.Value = "0";

            LblAPIValue.Text = string.Empty;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
            {
                rowAPI.Visible = true;
                LblAPI.Text = "API";
                LblAPIValue.Text = "'Full registration required!'";
            }
            else
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    rowAPI.Visible = true;
                    LblAPI.Text = "API";                    

                    List<ElioApies> userAPIes = Sql.GetUsersApies(vSession.User.Id, session);

                    if (userAPIes.Count > 0)
                    {
                        foreach (ElioApies api in userAPIes)
                        {
                            LblAPIValue.Text += api.ApiDescription + ", ";

                            if (api.ApiDescription == "Business Services")
                            {
                                HdnAPIBusServ.Value = "1";   
                            }
                            else if (api.ApiDescription == "Media & Entertainment")
                            {
                                HdnAPIMedEnter.Value = "1";   
                            }
                            else if (api.ApiDescription == "Retail & eCommerce")
                            {
                                HdnAPIRetEcom.Value = "1";  
                            }
                            else if (api.ApiDescription == "Geolocation")
                            {
                                HdnAPIGeol.Value = "1";   
                            }
                            else if (api.ApiDescription == "Social")
                            {
                                HdnAPISoc.Value = "1";   
                            }
                            else if (api.ApiDescription == "Health")
                            {
                                HdnAPIHeal.Value = "1";   
                            }                            
                        }

                        LblAPIValue.Text = LblAPIValue.Text.Substring(0, LblAPIValue.Text.Length - 2);
                    }
                    else
                    {
                        LblAPIValue.Text = "-";
                    }

                    ScriptManager.RegisterStartupScript(this, GetType(), "SetAPIs", "SetAPIs();", true);
                }
            }
        }

        private void LoadCountries()
        {
            DdlCountries.Items.Clear();

            List<ElioCountries> countries = Sql.GetPublicCountries(session);

            ListItem item = new ListItem();
            item.Text = "Select Country";
            item.Value = "0";

            DdlCountries.Items.Add(item);
            
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();

                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }
        }

        private void UpdateStrings()
        {
            LblRemoveImg.Text = "Remove";
            LblChangeImg.Text = "Change";
            LblSelectImg.Text = "Select image";
            LblAccountOverview.Text = "Account overview";
            LblEditAccount.Text = "Edit account";
            LblEditBillingAccount.Text = "Edit Billing Account";
            LblAccountSettings.Text = "Settings";
            LblDeleteAccount.Text = "Delete Account";
            LblDownloadCompanyData.Text = "Download My Data";
            LblEmailNotificationSettings.Text = "Email Notifications";
            LblCompanyCharacteristics.Text = "Business characteristics";
            LblSubcategories.Text = "Industry verticals";
            LblCharacteristic.Text = "Category";
            LblValue.Text = "Value";
            LblSubcategoryGroup.Text = "Category";
            LblSubcategoryItem.Text = "Vertical";
            LblCompanyGeneralInfo.Text = "General information";
            LblPersonalInfo.Text = "Personal information";
            LblCompanyBusinessInfo.Text = "Business information";
            LblCompanySubcategories.Text = "Industry verticals";
            LblCompanyProducts.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Product Integrations" : "Industry Products";
            LblCompanyProductsSelection.Text = vSession.User.CompanyType == Types.Vendors.ToString() ? "Industry Integrations Selection" : "Industry Products Selection";
            LblCompanyPassword.Text = "Change password";
            LblElioBillingDetails.Text = "Billing Information";
            LblStripeCreditCardDetails.Text = "Credit Card Information";
            LblCompany.Text = "Company";
            LblCompanyUsername.Text = "Username";
            LblCompanyEmail.Text = "Email";
            LblCompanyOfficialEmail.Text = "Official email";
            LblCompanyWebsite.Text = "Website";
            LblCompanyCountry.Text = "Country";
            LblCompanyAddress.Text = "Address";
            LblCompanyPhone.Text = "Phone";
            LblCompanyProductDemo.Text = "Product Demo Link";
            LblCompanyOverview.Text = "Overview";
            LblCompanyDescription.Text = "Partner Program Description";
            BtnSaveGeneral.Text = BtnSaveBusinessSettings.Text = "Save";
            BtnCancelGeneral.Text = BtnCanceBusinessSettings.Text = "Cancel";
            LblIndAdvMark.Text = "Advertising/Marketing";
            LblIndCommun.Text = "Communications";
            LblIndConsWeb.Text = "Consumer Web";
            LblIndDigMed.Text = "Digital Media";
            LblIndEcom.Text = "E-Commerce";
            LblIndEduc.Text = "eLearning";
            LblIndEnter.Text = "Enterprise";
            LblIndEntGam.Text = "Entertainment/Games";
            LblIndHard.Text = "Hardware";
            LblIndMob.Text = "Mobile";
            LblIndNetHos.Text = "Network/Hosting";
            LblIndSocMed.Text = "Social Media";
            LblIndSoft.Text = "Software";
            LblInd14.Text = "Aerospace and Defense";
            LblInd15.Text = " Automotive";
            LblInd16.Text = " Banking";
            LblInd17.Text = " Chemicals & Life Sciences";
            LblInd18.Text = " Distribution";
            LblInd19.Text = " Electronics";
            LblInd20.Text = " Energy";
            LblInd21.Text = " Engineering & Construction";
            LblInd22.Text = " Equipment Service and Rental";
            LblInd23.Text = " Fashion";
            LblInd24.Text = " Financial Services and Insurance";
            LblInd25.Text = " Food and Beverage/CPG";
            LblInd26.Text = " General Industry";
            LblInd27.Text = " Healthcare";
            LblInd28.Text = " High Tech & Communications";
            LblInd29.Text = " Hospitality";
            LblInd30.Text = " Legal Services";
            LblInd31.Text = " Logistics and 3PL";
            LblInd32.Text = " Manufacturing";
            LblInd33.Text = " Media";
            LblInd34.Text = " Oil & Gas";
            LblInd35.Text = " Professional Services";
            LblInd36.Text = " Public Sector";
            LblInd37.Text = " Real Estate";
            LblInd38.Text = " Retail";
            LblInd39.Text = " Shipping";
            LblInd40.Text = " Telecommunications";
            LblInd41.Text = " Travel & Transportation";
            LblInd42.Text = " Utilities";

            LblProgWhiteL.Text = "White Label";
            LblProgResel.Text = "Reseller";
            LblProgVAR.Text = "Value Added Reseller (VAR)";
            LblProgDistr.Text = "Distributor";
            LblProgAPIprg.Text = "API Program (Developers)";
            LblProgSysInteg.Text = "System Integrator";
            LblProgServProv.Text = "Managed Service Provider";
            LblMarkConsum.Text = "Consumers (B2C)";
            LblMarkSOHO.Text = "Small office - home office: SOHO (B2B)";
            LblMarkSmallMid.Text = "Small & mid-sized businesses (B2B)";
            LblMarkEnter.Text = "Enterprise (B2B)";
            LblAPIBusServ.Text = "Business Services";
            LblAPIMedEnter.Text = "Media & Entertainment";
            LblAPIRetEcom.Text = "Retail & eCommerce";
            LblAPIGeol.Text = "Geolocation";
            LblAPISoc.Text = "Social";
            LblAPIHeal.Text = "Health";

            Lbl1_1.Text = "Email Marketing";
            Lbl1_2.Text = "Campaign Management";
            Lbl1_3.Text = "Marketing Automation";
            Lbl1_4.Text = "Content Marketing";
            Lbl1_5.Text = "SEO & SEM";
            Lbl1_6.Text = "Social Media Marketing";
            Lbl1_7.Text = "Affiliate Marketing";
            Lbl1_8.Text = "Surveys & Forms";
            Lbl1_9.Text = "Ad Serving";
            Lbl1_10.Text = "Event Management";
            Lbl1_11.Text = "Sales Process Management";
            Lbl1_12.Text = "Quotes & Orders";
            Lbl1_13.Text = "Document Management";
            Lbl1_14.Text = "Sales Intelligence";
            Lbl1_15.Text = "Engagement Tools";
            Lbl1_16.Text = "POS";
            Lbl1_17.Text = "E-Signature";
            Lbl1_18.Text = "ECM";

            Lbl2_1.Text = "CRM";
            Lbl2_2.Text = "Help Desk";
            Lbl2_3.Text = "Live Chat";
            Lbl2_4.Text = "Feedback Management";
            Lbl2_5.Text = "Gamification & Loyalty";
            Lbl2_6.Text = "Chatbot";

            Lbl3_1.Text = "Project Management Tools";
            Lbl3_2.Text = "Knowledge Management";
            Lbl3_3.Text = "File Sharing Software";

            Lbl4_1.Text = "Business Process Management";
            Lbl4_2.Text = "Digital Asset Management";
            Lbl4_3.Text = "ERP";
            Lbl4_4.Text = "Inventory Management";
            Lbl4_5.Text = "Shipping & Tracking";
            Lbl4_6.Text = "Supply Chain Management";
            Lbl4_7.Text = "Warehouse Management";
            Lbl4_8.Text = "Supply Chain Execution";
            Lbl4_9.Text = "Track Management";
            Lbl4_10.Text = "Workflow Management";
            Lbl4_11.Text = "Enterprise Asset Management";
            Lbl4_12.Text = "Facility Management";
            Lbl4_13.Text = "Asset Lifecycle Management";
            Lbl4_14.Text = "CMMS";
            Lbl4_15.Text = "Fleet Management";
            Lbl4_16.Text = "Change Management";
            Lbl4_17.Text = "Procurement";
            Lbl4_18.Text = "Field Services Management";
            Lbl4_19.Text = "PRM";
            Lbl4_20.Text = "Robotic Process Automation";
            Lbl4_21.Text = "ITSM";
            Lbl4_22.Text = "Artificial Intelligence";

            Lbl5_1.Text = "Analytics Software";
            Lbl5_2.Text = "Business Intelligence";
            Lbl5_3.Text = "Data Visualization";
            Lbl5_4.Text = "Competitive Intelligence";
            Lbl5_5.Text = "Location Intelligence";

            Lbl6_1.Text = "Accounting";
            Lbl6_2.Text = "Payment Processing";
            Lbl6_3.Text = "Time & Expenses";
            Lbl6_4.Text = "Billing & Invoicing";
            Lbl6_5.Text = "Budgeting";

            Lbl7_1.Text = "Applicant Tracking";
            Lbl7_2.Text = "HR Administration";
            Lbl7_3.Text = "Payroll";
            Lbl7_4.Text = "Performance Management";
            Lbl7_5.Text = "Recruiting";
            Lbl7_6.Text = "Learning Management System";
            Lbl7_7.Text = "Time & Expense";

            Lbl8_1.Text = "API Tools";
            Lbl8_2.Text = "Bug Trackers";
            Lbl8_3.Text = "Development Tools";
            Lbl8_4.Text = "eCommerce";
            Lbl8_5.Text = "Frameworks & Libraries";
            Lbl8_6.Text = "Mobile Development";
            Lbl8_7.Text = "Optimization";
            Lbl8_8.Text = "Usability Testing";
            Lbl8_9.Text = "Websites";

            Lbl9_1.Text = "Cloud Integration (iPaaS)";
            Lbl9_2.Text = "Cloud Management";
            Lbl9_3.Text = "Cloud Storage";
            Lbl9_4.Text = "Remote Access";
            Lbl9_5.Text = "Virtualization";
            Lbl9_6.Text = "Web Hosting";
            Lbl9_7.Text = "Web Monitoring";
            Lbl9_8.Text = "Big Data";
            Lbl9_9.Text = "Data Warehousing";
            Lbl9_10.Text = "Databases";
            Lbl9_11.Text = "Data Integration";
            Lbl9_12.Text = "Data Management";
            Lbl9_13.Text = "Networking";

            Lbl10_1.Text = "Calendar & Scheduling";
            Lbl10_2.Text = "Email";
            Lbl10_3.Text = "Note Taking";
            Lbl10_4.Text = "Password Management";
            Lbl10_5.Text = "Presentations";
            Lbl10_6.Text = "Productivity Suites";
            Lbl10_7.Text = "Spreadsheets";
            Lbl10_8.Text = "Task Management";
            Lbl10_9.Text = "Time Management";

            Lbl11_1.Text = "Cybersecurity";
            Lbl11_2.Text = "Vulnerability Management";
            Lbl11_3.Text = "Firewall";
            Lbl11_4.Text = "Mobile Data Security";
            Lbl11_5.Text = "Backup & Restore";
            Lbl11_6.Text = "Data Masking";
            Lbl11_7.Text = "Identity Management";
            Lbl11_8.Text = "Risk Management";
            Lbl11_9.Text = "Penetration Testing";
            Lbl11_10.Text = "Application Security";
            Lbl11_11.Text = "Governance, Risk & Compliance (GRC)";
            Lbl11_12.Text = "Compliance";
            Lbl11_13.Text = "Fraud Prevention";
            Lbl11_14.Text = "Email Security";
            Lbl11_15.Text = "Endpoint Security";
            Lbl11_16.Text = "VPN";

            Lbl12_1.Text = "Graphic Design";
            Lbl12_2.Text = "Infographics";
            Lbl12_3.Text = "Video Editing";
            Lbl12_4.Text = "Video Management System";

            Lbl13_1.Text = "eLearning";
            Lbl13_2.Text = "Healthcare";
            Lbl13_3.Text = "Simulation Software";

            Lbl14_1.Text = "Chat & Web Conference";
            Lbl14_2.Text = "VOIP";
            Lbl14_3.Text = "Mobility";
            Lbl14_4.Text = "Collaboration";
            Lbl14_5.Text = "Conferencing";
            Lbl14_6.Text = "Unified Messaging";
            Lbl14_7.Text = "Unified Communications";
            Lbl14_8.Text = "Team Collaboration";
            Lbl14_9.Text = "Video Conferencing";
            Lbl14_10.Text = "Contact Center";
            Lbl14_11.Text = "Connectivity";
            Lbl14_12.Text = "WiFi";

            Lbl15_1.Text = "General-Purpose CAD";
            Lbl15_2.Text = "CAM";
            Lbl15_3.Text = "PLM";
            Lbl15_4.Text = "PDM (Product Data Management)";
            Lbl15_5.Text = "BIM";
            Lbl15_6.Text = "3D Architecture";
            Lbl15_7.Text = "3D CAD";
            Lbl15_8.Text = "GIS";

            Lbl16_1.Text = "CCTV Video Surveillance";
            Lbl16_2.Text = "Access Control Systems";
            Lbl16_3.Text = "Perimeter Security Systems";
            Lbl16_4.Text = "Alarms and Automation";
            Lbl16_5.Text = "IoT";
            Lbl16_6.Text = "Monitors Cameras and Sensors";
            Lbl16_7.Text = "Switches";
            Lbl16_8.Text = "Intercom Systems";
            Lbl16_9.Text = "Servers";

            LblCurPasw.Text = "Current password";
            LblNewPasw.Text = "New password";
            LblRetNewPasw.Text = "Retype password";
            BtnSavePasw.Text = "Save";
            BtnCancelPasw.Text = "Cancel";
            LblMashape.Text = "Mashape username";
            LblIndustriesTitle.Text = "Industries selection";
            LblProgramsTitle.Text = "Partner programs selection";
            LblMarketsTitle.Text = "Market Specialisation selection";
            LblAPIsTitle.Text = "API selection";
            LblIndustryVerticalsEdit.Text = "Industry verticals selection";
            LblVertSalMark.Text = "Sales & Marketing";
            LblVertCustMan.Text = "Customer Management";
            LblVertProjMan.Text = "Project Management";
            LblVertOperWork.Text = "Operations & Workflow";
            LblVertTrackMeaus.Text = "Tracking & Measurement";
            LblVertAccFin.Text = "Accounting & Financials";
            LblVertHR.Text = "HR";
            LblVertWMSD.Text = "Web Mobile Software Development";
            LblVertITInfr.Text = "IT & Infrastructure";
            LblVertBussUtil.Text = "Business Utilities";
            LblVertSecBack.Text = "Data Security & GRC";
            LblVertDesMult.Text = "Design & Multimedia";
            LblVertMisc.Text = "Miscellaneous";
            LblVertUnCom.Text = "Unified Communications";
            LblVertCadPlm.Text = "CAD & PLM";
            LblVertHrdware.Text = "Hardware";

            BtnSaveVerticals.Text = "Save";
            BtnSubmitProducts.Text = "Save";
            BtnCancelVerticals.Text = "Cancel";
            BtnCancelSubmitProducts.Text = "Cancel";
            BtnSubmitLogo.Text = "Upload & Save logo";
            LblLogoHeader.Text = "Logo";
            LblUserHeader.Text = "Username";
            LblEmailHeader.Text = "E-mail";
            LblWebsiteHeader.Text = "Website";
            LblOffEmailHeader.Text = "Official E-mail";
            LblAddressHeader.Text = "Address";
            LblPhoneHeader.Text = "Phone";
            LblLogoUpdateHeader.Text = "Update your logo";
            LblOverviewHeader.Text = "Overview";
            LblDescriptionHeader.Text = "Description";

            LblBillingCompanyName.Text = "Company Name";
            LblBillingCompanyVatNumber.Text = "Company Vat Number";
            LblBillingCompanyPostCode.Text = "Company Post Code";
            LblBillingCompanyAddress.Text = "Company Billing Address (street, city, country-state, country)";
            
            BtnSaveBillingDetails.Text = "Save";

            LblFullName.Text = "Card Full Name";
            LblAddress1.Text = "Address 1";
            LblAddress2.Text = "Address 2";
            LblOrigin.Text = "Origin";
            LblCardType.Text = "Card Type";
            LblCCNumber.Text = "Credit Card Number";
            LblCvcNumber.Text = "CVC";
            LblExpMonth.Text = "Expiration Month";
            LblExpYear.Text = "Expiration Year";
            LblZipCode.Text = "Zip Code";
            BtnSaveCreditCardDetails.Text = "Update Card";
            BtnAddNewCard.Text = "Add New Card";
            BtnCancelAddNewCard.Text = "Cancel";

            LblPersonLastName.Text = "Last name";
            LblPersonFirstName.Text = "First Name";
            LblPersonPhone.Text = "Phone";
            LblPersonLocation.Text = "Location";
            LblPersonTimeZone.Text = "Time Zone";
            LblPersonTitle.Text = "Title";
            LblPersonRole.Text = "Role";
            LblPersonSeniority.Text = "Seniority";
            LblPersonTwitterHandle.Text = "Twitter";
            LblPersonAboutMeHandle.Text = "About Me";
            LblPersonAvatarHeader.Text = "Update your image";
            LblPersonAvatarSuccess.Text = "Done! ";
            LblPersonAvatarSuccessMsg.Text = "Personal image was changed successfully.";
            LblPersonAvatarError.Text = "Error! ";
            //LblPersonAvatarErrorMsg.Text = "";
            BtnAvatarUpload.Text = "Upload & Save Image";
            LblPersonBio.Text = "Bio";
            LblPersonSuccessTitle.Text = "Done! ";
            LblPersonSuccessMsg.Text = "Your personal data saved successfully";
            LblPersonWarningTitle.Text = "Error! ";
            LblPersonWarningMsg.Text = "Something went wrong. Your data could not be saved. Please try again later or contact with us.";
            BtnPersonSaveGeneral.Text = "Save";
            BtnPersonCancelGeneral.Text = "Cancel";

            BtnDeleteAccountData.Text = "Delete my account";
            BtnDeleteConfirm.Text = "Proceed";
            BtnBack.Text = "Back";
            LblConfMsg.Text = "A request about closing your account will be send to our support team and all of your company data will be permantly erased. Are you sure you want to proceed?";
            LblConfTitle.Text = "Delete User Confirmation";
            Label lblSaveNotificationsText = (Label)ControlFinder.FindControlRecursive(RbtnSaveNotifications, "LblSaveNotificationsText");
            lblSaveNotificationsText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "18")).Text;
        }

        private void SetLinks()
        {
            aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
            //aBtnGoPremium.HRef = ControlLoader.Dashboard(vSession.User, "billing");
        }

        private void UpdateIndustries(int industryId, string userSelection)
        {
            DataLoader<ElioUsersIndustries> loader = new DataLoader<ElioUsersIndustries>(session);
            //ElioUsersIndustries newIndustry = new ElioUsersIndustries();
            //var industry = userIndustries.Where(i => i.Id == industryId);

            ElioUsersIndustries userIndustry = Sql.GetUsersIndustriesByIndustryId(vSession.User.Id, industryId, session);
                        
            if (userSelection == "1")  
            {
                if (userIndustry == null)
                {
                    userIndustry = new ElioUsersIndustries();

                    userIndustry.UserId = vSession.User.Id;
                    userIndustry.IndustryId = industryId;
                    loader.Insert(userIndustry);
                }
            }
            else
            {
                if (userIndustry != null)
                {
                    loader.Delete(userIndustry);
                    //Sql.DeleteUserIndustry(vSession.User.Id, industryId, session);
                }
            }
        }

        private void UpdatePrograms(int programId, string userSelection)
        {
            DataLoader<ElioUsersPartners> loader = new DataLoader<ElioUsersPartners>(session);
            //ElioUsersPartners newProgram = new ElioUsersPartners();
            //List<ElioPartners> userPrograms = Sql.GetUsersPartners(vSession.User.Id, session);
            //var program = userPrograms.Where(i => i.Id == programId);

            ElioUsersPartners userProgram = Sql.GetUsersPartnerProgramsById(vSession.User.Id, programId, session);            
            if (userSelection == "1")
            {
                if (userProgram == null)
                {
                    userProgram = new ElioUsersPartners();

                    userProgram.UserId = vSession.User.Id;
                    userProgram.PartnerId = programId;
                    loader.Insert(userProgram);
                }
            }
            else
            {
                if (userProgram != null)
                {
                    loader.Delete(userProgram);
                    //Sql.DeleteUserPartnerProgramByPartnerID(vSession.User.Id, programId, session);
                }
            }
        }

        private void UpdateMarkets(int marketId, string userSelection)
        {
            DataLoader<ElioUsersMarkets> loader = new DataLoader<ElioUsersMarkets>(session);
            ElioUsersMarkets userMarket = Sql.GetUsersMarketsByMarketId(vSession.User.Id, marketId, session);

            if (userSelection == "1")
            {
                if (userMarket == null)
                {
                    userMarket = new ElioUsersMarkets();

                    userMarket.UserId = vSession.User.Id;
                    userMarket.MarketId = marketId;
                    loader.Insert(userMarket);
                }
            }
            else
            {
                if (userMarket != null)
                {
                    loader.Delete(userMarket);
                    //Sql.DeleteUserMarket(vSession.User.Id, marketId, session);
                }
            }
        }

        private void UpdateAPIs(int apiId, string userSelection)
        {
            DataLoader<ElioUsersApies> loader = new DataLoader<ElioUsersApies>(session);
            ElioUsersApies userAPI = Sql.GetUsersApiesByApiId(vSession.User.Id, apiId, session);

            if (userSelection == "1")
            {
                if (userAPI == null)
                {
                    userAPI = new ElioUsersApies();

                    userAPI.UserId = vSession.User.Id;
                    userAPI.ApiId = apiId;
                    loader.Insert(userAPI);
                }
            }
            else
            {
                if (userAPI != null)
                {
                    loader.Delete(userAPI);
                    //Sql.DeleteUserApi(vSession.User.Id, apiId, session);
                }
            }
        }

        private void UpdateVerticals(int verticalId, int verticalGroupId)
        {
            try
            {
                DataLoader<ElioUsersSubIndustriesGroupItems> loader = new DataLoader<ElioUsersSubIndustriesGroupItems>(session);
                ElioUsersSubIndustriesGroupItems newVertical = new ElioUsersSubIndustriesGroupItems();

                newVertical.UserId = vSession.User.Id;
                newVertical.SubIndustryGroupId = verticalGroupId;
                newVertical.SubIndustryGroupItemId = verticalId;
                loader.Insert(newVertical);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }            
        }

        private void ClearErrorFields()
        {
            divSaveSuccess.Visible = false;
            divSaveFailure.Visible = false;

            LblBillingCompanyNameError.Text = string.Empty;
            LblBillingCompanyVatNumberError.Text = string.Empty;
            LblBillingCompanyPostCodeError.Text = string.Empty;
            LblBillingCompanyAddressError.Text = string.Empty;

            divBillingCompanyName.Visible = false;
            divBillingCompanyVatNumber.Visible = false;
            divBillingCompanyPostCode.Visible = false;
            divBillingCompanyAddress.Visible = false;
            
            LblFullNameError.Text = string.Empty;
            LblAddress1Error.Text = string.Empty;
            LblAddress2Error.Text = string.Empty;
            LblOriginError.Text = string.Empty;
            LblCardTypeError.Text = string.Empty;
            LblCvcNumberError.Text = string.Empty;
            LblCCNumberError.Text = string.Empty;
            LblExpMonthError.Text = string.Empty;
            LblExpYearError.Text = string.Empty;
            LblZipCodeError.Text = string.Empty;

            divFullName.Visible = false;
            divAddress1.Visible = false;
            divAddress2.Visible = false;
            divOrigin.Visible = false;
            divCardType.Visible = false;
            divCCNumber.Visible = false;
            divCCNumber.Visible = false;
            divExpMonth.Visible = false;
            divExpYear.Visible = false;
            divZipCode.Visible = false;
        }

        private void ClearPersonalErrorFields()
        {
            divPersonLastNameError.Visible = false;
            divPersonFirstNameError.Visible = false;
            divPersonPhoneError.Visible = false;
            divPersonLocationError.Visible = false;
            divPersonTimeZoneError.Visible = false;
            divPersonTitleError.Visible = false;
            divPersonRoleError.Visible = false;
            divPersonSeniorityError.Visible = false;
            divPersonTwitterHandleError.Visible = false;
            divPersonAboutMeHandleError.Visible = false;
            divPersonAvatarError.Visible = false;
            divPersonAvatarSuccess.Visible = false;
            divPersonBioError.Visible = false;
        }

        private void IsPersonalSuccess(bool isValidData)
        {
            if (isValidData)
            {
                divPersonSuccessMessage.Visible = true;
                LblPersonSuccessTitle.Text = "Done! ";
                LblPersonSuccessMsg.Text = "Your personal data saved successfully";

                divPersonWarningMessage.Visible = false;
            }
            else
            {
                divPersonWarningMessage.Visible = true;
                LblPersonWarningTitle.Text = "Error! ";
                LblPersonWarningMsg.Text = "Something went wrong. Your data could not be saved. Please try again later or contact with us.";

                divPersonSuccessMessage.Visible = false;
            }
        }
                
        private int SetUserEmails(HtmlInputCheckBox cbx, int selectedItemsCount)
        {
            if (cbx.Checked)
            {
                if (!Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, Convert.ToInt32(cbx.Value), session))   // && communitySelected)
                {
                    ElioUserEmailNotificationsSettings newNotification = new ElioUserEmailNotificationsSettings();

                    newNotification.UserId = vSession.User.Id;
                    newNotification.EmaiNotificationsId = Convert.ToInt32(cbx.Value);

                    DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);
                    loader.Insert(newNotification);
                }

                selectedItemsCount++;
            }
            else
            {
                Sql.DeleteUserEmailNotificationSettings(Convert.ToInt32(cbx.Value), vSession.User.Id, session);
            }

            return selectedItemsCount;
        }

        private void SaveVerticals()
        {
            int counter = 0;

            #region check for nothing or max

            List<HiddenField> hdnFlds = new List<HiddenField>();

            hdnFlds.Add(Hdn1_1);
            hdnFlds.Add(Hdn1_2);
            hdnFlds.Add(Hdn1_3);
            hdnFlds.Add(Hdn1_4);
            hdnFlds.Add(Hdn1_5);
            hdnFlds.Add(Hdn1_6);
            hdnFlds.Add(Hdn1_7);
            hdnFlds.Add(Hdn1_8);
            hdnFlds.Add(Hdn1_9);
            hdnFlds.Add(Hdn1_10);
            hdnFlds.Add(Hdn1_11);
            hdnFlds.Add(Hdn1_12);
            hdnFlds.Add(Hdn1_13);
            hdnFlds.Add(Hdn1_14);
            hdnFlds.Add(Hdn1_15);
            hdnFlds.Add(Hdn1_16);
            hdnFlds.Add(Hdn1_17);
            hdnFlds.Add(Hdn1_18);
            hdnFlds.Add(Hdn2_1);
            hdnFlds.Add(Hdn2_2);
            hdnFlds.Add(Hdn2_3);
            hdnFlds.Add(Hdn2_4);
            hdnFlds.Add(Hdn2_5);
            hdnFlds.Add(Hdn2_6);
            hdnFlds.Add(Hdn3_1);
            hdnFlds.Add(Hdn3_2);
            hdnFlds.Add(Hdn3_3);
            hdnFlds.Add(Hdn4_1);
            hdnFlds.Add(Hdn4_2);
            hdnFlds.Add(Hdn4_3);
            hdnFlds.Add(Hdn4_4);
            hdnFlds.Add(Hdn4_5);
            hdnFlds.Add(Hdn4_6);
            hdnFlds.Add(Hdn4_7);
            hdnFlds.Add(Hdn4_8);
            hdnFlds.Add(Hdn4_9);
            hdnFlds.Add(Hdn4_10);
            hdnFlds.Add(Hdn4_11);
            hdnFlds.Add(Hdn4_12);
            hdnFlds.Add(Hdn4_13);
            hdnFlds.Add(Hdn4_14);
            hdnFlds.Add(Hdn4_15);
            hdnFlds.Add(Hdn4_16);
            hdnFlds.Add(Hdn4_17);
            hdnFlds.Add(Hdn4_18);
            hdnFlds.Add(Hdn4_19);
            hdnFlds.Add(Hdn4_20);
            hdnFlds.Add(Hdn4_21);
            hdnFlds.Add(Hdn4_22);
            hdnFlds.Add(Hdn5_1);
            hdnFlds.Add(Hdn5_2);
            hdnFlds.Add(Hdn5_3);
            hdnFlds.Add(Hdn5_4);
            hdnFlds.Add(Hdn5_5);
            hdnFlds.Add(Hdn6_1);
            hdnFlds.Add(Hdn6_2);
            hdnFlds.Add(Hdn6_3);
            hdnFlds.Add(Hdn6_4);
            hdnFlds.Add(Hdn6_5);
            hdnFlds.Add(Hdn7_1);
            hdnFlds.Add(Hdn7_2);
            hdnFlds.Add(Hdn7_3);
            hdnFlds.Add(Hdn7_4);
            hdnFlds.Add(Hdn7_5);
            hdnFlds.Add(Hdn7_6);
            hdnFlds.Add(Hdn7_7);
            hdnFlds.Add(Hdn8_1);
            hdnFlds.Add(Hdn8_2);
            hdnFlds.Add(Hdn8_3);
            hdnFlds.Add(Hdn8_4);
            hdnFlds.Add(Hdn8_5);
            hdnFlds.Add(Hdn8_6);
            hdnFlds.Add(Hdn8_7);
            hdnFlds.Add(Hdn8_8);
            hdnFlds.Add(Hdn8_9);
            hdnFlds.Add(Hdn9_1);
            hdnFlds.Add(Hdn9_2);
            hdnFlds.Add(Hdn9_3);
            hdnFlds.Add(Hdn9_4);
            hdnFlds.Add(Hdn9_5);
            hdnFlds.Add(Hdn9_6);
            hdnFlds.Add(Hdn9_7);
            hdnFlds.Add(Hdn9_8);
            hdnFlds.Add(Hdn9_9);
            hdnFlds.Add(Hdn9_10);
            hdnFlds.Add(Hdn9_11);
            hdnFlds.Add(Hdn9_12);
            hdnFlds.Add(Hdn9_13);
            hdnFlds.Add(Hdn10_1);
            hdnFlds.Add(Hdn10_2);
            hdnFlds.Add(Hdn10_3);
            hdnFlds.Add(Hdn10_4);
            hdnFlds.Add(Hdn10_5);
            hdnFlds.Add(Hdn10_6);
            hdnFlds.Add(Hdn10_7);
            hdnFlds.Add(Hdn10_8);
            hdnFlds.Add(Hdn10_9);
            hdnFlds.Add(Hdn11_1);
            hdnFlds.Add(Hdn11_2);
            hdnFlds.Add(Hdn11_3);
            hdnFlds.Add(Hdn11_4);
            hdnFlds.Add(Hdn11_5);
            hdnFlds.Add(Hdn11_6);
            hdnFlds.Add(Hdn11_7);
            hdnFlds.Add(Hdn11_8);
            hdnFlds.Add(Hdn11_9);
            hdnFlds.Add(Hdn11_10);
            hdnFlds.Add(Hdn11_11);
            hdnFlds.Add(Hdn11_12);
            hdnFlds.Add(Hdn11_13);
            hdnFlds.Add(Hdn11_14);
            hdnFlds.Add(Hdn11_15);
            hdnFlds.Add(Hdn11_16);
            hdnFlds.Add(Hdn12_1);
            hdnFlds.Add(Hdn12_2);
            hdnFlds.Add(Hdn12_3);
            hdnFlds.Add(Hdn12_4);
            hdnFlds.Add(Hdn13_1);
            hdnFlds.Add(Hdn13_2);
            hdnFlds.Add(Hdn13_3);
            hdnFlds.Add(Hdn14_1);
            hdnFlds.Add(Hdn14_2);
            hdnFlds.Add(Hdn14_3);
            hdnFlds.Add(Hdn14_4);
            hdnFlds.Add(Hdn14_5);
            hdnFlds.Add(Hdn14_6);
            hdnFlds.Add(Hdn14_7);
            hdnFlds.Add(Hdn14_8);
            hdnFlds.Add(Hdn14_9);
            hdnFlds.Add(Hdn14_10);
            hdnFlds.Add(Hdn14_11);
            hdnFlds.Add(Hdn14_12);
            hdnFlds.Add(Hdn15_1);
            hdnFlds.Add(Hdn15_2);
            hdnFlds.Add(Hdn15_3);
            hdnFlds.Add(Hdn15_4);
            hdnFlds.Add(Hdn15_5);
            hdnFlds.Add(Hdn15_6);
            hdnFlds.Add(Hdn15_7);
            hdnFlds.Add(Hdn15_8);
            hdnFlds.Add(Hdn16_1);
            hdnFlds.Add(Hdn16_2);
            hdnFlds.Add(Hdn16_3);
            hdnFlds.Add(Hdn16_4);
            hdnFlds.Add(Hdn16_5);
            hdnFlds.Add(Hdn16_6);
            hdnFlds.Add(Hdn16_7);
            hdnFlds.Add(Hdn16_8);
            hdnFlds.Add(Hdn16_9);

            foreach (HiddenField hf in hdnFlds)
            {
                if (hf.Value == "1")
                {
                    counter++;
                }

                if (counter > 15)
                    break;
            }

            if (counter < 1)
            {
                divVerticalFailure.Visible = true;
                LblVerticalFailure.Text = "Warning! ";
                LblVerticalFailureContent.Text = "You have to select at least one industry vertical!";
                LoadData();
                return;
            }
            else if (counter > 15)
            {
                divVerticalFailure.Visible = true;
                LblVerticalFailure.Text = "Warning! ";
                LblVerticalFailureContent.Text = "You are able to select max 15 (fifteen) industry verticals! Currently selected: " + counter.ToString();
                LoadData();
                return;
            }
            else
            {
                foreach (HiddenField hf in hdnFlds)
                {
                    switch (hf.ID)
                    {
                        case "Hdn1_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_1.Text, 1, Hdn1_1.Value, session);

                                break;
                            }
                        case "Hdn1_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_2.Text, 1, Hdn1_2.Value, session);
                                //UpdateVerticals(2, 1);
                                break;
                            }
                        case "Hdn1_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_3.Text, 1, Hdn1_3.Value, session);
                                //UpdateVerticals(3, 1);
                                break;
                            }
                        case "Hdn1_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_4.Text, 1, Hdn1_4.Value, session);
                                //UpdateVerticals(4, 1);
                                break;
                            }
                        case "Hdn1_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_5.Text, 1, Hdn1_5.Value, session);
                                //UpdateVerticals(5, 1);
                                break;
                            }
                        case "Hdn1_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_6.Text, 1, Hdn1_6.Value, session);
                                //UpdateVerticals(6, 1);
                                break;
                            }
                        case "Hdn1_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_7.Text, 1, Hdn1_7.Value, session);
                                //UpdateVerticals(7, 1);
                                break;
                            }
                        case "Hdn1_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_8.Text, 1, Hdn1_8.Value, session);
                                //UpdateVerticals(8, 1);
                                break;
                            }
                        case "Hdn1_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_9.Text, 1, Hdn1_9.Value, session);
                                //UpdateVerticals(9, 1);
                                break;
                            }
                        case "Hdn1_10":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_10.Text, 1, Hdn1_10.Value, session);
                                //UpdateVerticals(10, 1);
                                break;
                            }
                        case "Hdn1_11":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_11.Text, 1, Hdn1_11.Value, session);
                                //UpdateVerticals(11, 1);
                                break;
                            }
                        case "Hdn1_12":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_12.Text, 1, Hdn1_12.Value, session);
                                //UpdateVerticals(12, 1);
                                break;
                            }
                        case "Hdn1_13":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_13.Text, 1, Hdn1_13.Value, session);
                                //UpdateVerticals(13, 1);
                                break;
                            }
                        case "Hdn1_14":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_14.Text, 1, Hdn1_14.Value, session);
                                //UpdateVerticals(14, 1);
                                break;
                            }
                        case "Hdn1_15":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_15.Text, 1, Hdn1_15.Value, session);
                                //UpdateVerticals(15, 1);
                                break;
                            }
                        case "Hdn1_16":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_16.Text, 1, Hdn1_16.Value, session);
                                //UpdateVerticals(16, 1);
                                break;
                            }
                        case "Hdn1_17":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_17.Text, 1, Hdn1_17.Value, session);
                                //UpdateVerticals(17, 1);
                                break;
                            }
                        case "Hdn1_18":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl1_18.Text, 1, Hdn1_18.Value, session);
                                //UpdateVerticals(95, 1);
                                break;
                            }
                        case "Hdn2_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_1.Text, 2, Hdn2_1.Value, session);
                                //UpdateVerticals(18, 2);
                                break;
                            }
                        case "Hdn2_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_2.Text, 2, Hdn2_2.Value, session);
                                //UpdateVerticals(19, 2);
                                break;
                            }
                        case "Hdn2_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_3.Text, 2, Hdn2_3.Value, session);
                                //UpdateVerticals(20, 2);
                                break;
                            }
                        case "Hdn2_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_4.Text, 2, Hdn2_4.Value, session);
                                //UpdateVerticals(21, 2);
                                break;
                            }
                        case "Hdn2_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_5.Text, 2, Hdn2_5.Value, session);
                                //UpdateVerticals(22, 2);
                                break;
                            }
                        case "Hdn2_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl2_6.Text, 2, Hdn2_6.Value, session);
                                //UpdateVerticals(120, 2);
                                break;
                            }
                        case "Hdn3_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl3_1.Text, 3, Hdn3_1.Value, session);
                                //UpdateVerticals(23, 3);
                                break;
                            }
                        case "Hdn3_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl3_2.Text, 3, Hdn3_2.Value, session);
                                //UpdateVerticals(24, 3);
                                break;
                            }
                        case "Hdn3_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl3_3.Text, 3, Hdn3_3.Value, session);
                                //UpdateVerticals(25, 3);
                                break;
                            }
                        case "Hdn4_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_1.Text, 4, Hdn4_1.Value, session);
                                //UpdateVerticals(27, 4);
                                break;
                            }
                        case "Hdn4_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_2.Text, 4, Hdn4_2.Value, session);
                                //UpdateVerticals(28, 4);
                                break;
                            }
                        case "Hdn4_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_3.Text, 4, Hdn4_3.Value, session);
                                //UpdateVerticals(29, 4);
                                break;
                            }
                        case "Hdn4_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_4.Text, 4, Hdn4_4.Value, session);
                                //UpdateVerticals(30, 4);
                                break;
                            }
                        case "Hdn4_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_5.Text, 4, Hdn4_5.Value, session);
                                //UpdateVerticals(31, 4);
                                break;
                            }
                        case "Hdn4_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_6.Text, 4, Hdn4_6.Value, session);
                                //UpdateVerticals(32, 4);
                                break;
                            }
                        case "Hdn4_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_7.Text, 4, Hdn4_7.Value, session);
                                //UpdateVerticals(83, 4);
                                break;
                            }
                        case "Hdn4_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_8.Text, 4, Hdn4_8.Value, session);
                                //UpdateVerticals(84, 4);
                                break;
                            }
                        case "Hdn4_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_9.Text, 4, Hdn4_9.Value, session);
                                //UpdateVerticals(111, 4);
                                break;
                            }
                        case "Hdn4_10":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_10.Text, 4, Hdn4_10.Value, session);
                                //UpdateVerticals(112, 4);
                                break;
                            }
                        case "Hdn4_11":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_11.Text, 4, Hdn4_11.Value, session);
                                //UpdateVerticals(113, 4);
                                break;
                            }
                        case "Hdn4_12":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_12.Text, 4, Hdn4_12.Value, session);
                                //UpdateVerticals(114, 4);
                                break;
                            }
                        case "Hdn4_13":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_13.Text, 4, Hdn4_13.Value, session);
                                //UpdateVerticals(115, 4);
                                break;
                            }
                        case "Hdn4_14":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_14.Text, 4, Hdn4_14.Value, session);
                                //UpdateVerticals(116, 4);
                                break;
                            }
                        case "Hdn4_15":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_15.Text, 4, Hdn4_15.Value, session);
                                //UpdateVerticals(117, 4);
                                break;
                            }
                        case "Hdn4_16":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_16.Text, 4, Hdn4_16.Value, session);
                                //UpdateVerticals(118, 4);
                                break;
                            }
                        case "Hdn4_17":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_17.Text, 4, Hdn4_17.Value, session);
                                //UpdateVerticals(119, 4);
                                break;
                            }
                        case "Hdn4_18":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_18.Text, 4, Hdn4_18.Value, session);
                                //UpdateVerticals(127, 4);
                                break;
                            }
                        case "Hdn4_19":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_19.Text, 4, Hdn4_19.Value, session);
                                //UpdateVerticals(127, 4);
                                break;
                            }
                        case "Hdn4_20":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_20.Text, 4, Hdn4_20.Value, session);
                                //UpdateVerticals(127, 4);
                                break;
                            }
                        case "Hdn4_21":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_21.Text, 4, Hdn4_21.Value, session);
                                //UpdateVerticals(127, 4);
                                break;
                            }
                        case "Hdn4_22":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl4_22.Text, 4, Hdn4_22.Value, session);
                                //UpdateVerticals(127, 4);
                                break;
                            }
                        case "Hdn5_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl5_1.Text, 5, Hdn5_1.Value, session);
                                //UpdateVerticals(33, 5);
                                break;
                            }
                        case "Hdn5_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl5_2.Text, 5, Hdn5_2.Value, session);
                                //UpdateVerticals(34, 5);
                                break;
                            }
                        case "Hdn5_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl5_3.Text, 5, Hdn5_3.Value, session);
                                //UpdateVerticals(35, 5);
                                break;
                            }
                        case "Hdn5_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl5_4.Text, 5, Hdn5_4.Value, session);
                                //UpdateVerticals(36, 5);
                                break;
                            }
                        case "Hdn5_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl5_5.Text, 5, Hdn5_5.Value, session);
                                //UpdateVerticals(110, 5);
                                break;
                            }
                        case "Hdn6_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl6_1.Text, 6, Hdn6_1.Value, session);
                                //UpdateVerticals(37, 6);
                                break;
                            }
                        case "Hdn6_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl6_2.Text, 6, Hdn6_2.Value, session);
                                //UpdateVerticals(38, 6);
                                break;
                            }
                        case "Hdn6_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl6_3.Text, 6, Hdn6_3.Value, session);
                                //UpdateVerticals(39, 6);
                                break;
                            }
                        case "Hdn6_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl6_4.Text, 6, Hdn6_4.Value, session);
                                //UpdateVerticals(40, 6);
                                break;
                            }
                        case "Hdn6_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl6_5.Text, 6, Hdn6_5.Value, session);
                                //UpdateVerticals(41, 6);
                                break;
                            }
                        case "Hdn7_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_1.Text, 7, Hdn7_1.Value, session);
                                //UpdateVerticals(42, 7);
                                break;
                            }
                        case "Hdn7_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_2.Text, 7, Hdn7_2.Value, session);
                                //UpdateVerticals(43, 7);
                                break;
                            }
                        case "Hdn7_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_3.Text, 7, Hdn7_3.Value, session);
                                //UpdateVerticals(44, 7);
                                break;
                            }
                        case "Hdn7_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_4.Text, 7, Hdn7_4.Value, session);
                                //UpdateVerticals(45, 7);
                                break;
                            }
                        case "Hdn7_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_5.Text, 7, Hdn7_5.Value, session);
                                //UpdateVerticals(46, 7);
                                break;
                            }
                        case "Hdn7_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_6.Text, 7, Hdn7_6.Value, session);
                                //UpdateVerticals(47, 7);
                                break;
                            }
                        case "Hdn7_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl7_7.Text, 7, Hdn7_7.Value, session);
                                //UpdateVerticals(48, 7);
                                break;
                            }
                        case "Hdn8_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_1.Text, 8, Hdn8_1.Value, session);
                                //UpdateVerticals(49, 8);
                                break;
                            }
                        case "Hdn8_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_2.Text, 8, Hdn8_2.Value, session);
                                //UpdateVerticals(50, 8);
                                break;
                            }
                        case "Hdn8_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_3.Text, 8, Hdn8_3.Value, session);
                                //UpdateVerticals(51, 8);
                                break;
                            }
                        case "Hdn8_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_4.Text, 8, Hdn8_4.Value, session);
                                //UpdateVerticals(52, 8);
                                break;
                            }
                        case "Hdn8_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_5.Text, 8, Hdn8_5.Value, session);
                                //UpdateVerticals(53, 8);
                                break;
                            }
                        case "Hdn8_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_6.Text, 8, Hdn8_6.Value, session);
                                //UpdateVerticals(54, 8);
                                break;
                            }
                        case "Hdn8_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_7.Text, 8, Hdn8_7.Value, session);
                                //UpdateVerticals(55, 8);
                                break;
                            }
                        case "Hdn8_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_8.Text, 8, Hdn8_8.Value, session);
                                //UpdateVerticals(56, 8);
                                break;
                            }
                        case "Hdn8_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl8_9.Text, 8, Hdn8_9.Value, session);
                                //UpdateVerticals(57, 8);
                                break;
                            }
                        case "Hdn9_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_1.Text, 9, Hdn9_1.Value, session);
                                //UpdateVerticals(58, 9);
                                break;
                            }
                        case "Hdn9_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_2.Text, 9, Hdn9_2.Value, session);
                                //UpdateVerticals(59, 9);
                                break;
                            }
                        case "Hdn9_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_3.Text, 9, Hdn9_3.Value, session);
                                //UpdateVerticals(60, 9);
                                break;
                            }
                        case "Hdn9_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_4.Text, 9, Hdn9_4.Value, session);
                                //UpdateVerticals(61, 9);
                                break;
                            }
                        case "Hdn9_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_5.Text, 9, Hdn9_5.Value, session);
                                //UpdateVerticals(62, 9);
                                break;
                            }
                        case "Hdn9_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_6.Text, 9, Hdn9_6.Value, session);
                                //UpdateVerticals(63, 9);
                                break;
                            }
                        case "Hdn9_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_7.Text, 9, Hdn9_7.Value, session);
                                //UpdateVerticals(64, 9);
                                break;
                            }
                        case "Hdn9_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_8.Text, 9, Hdn9_8.Value, session);
                                //UpdateVerticals(65, 9);
                                break;
                            }
                        case "Hdn9_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_9.Text, 9, Hdn9_9.Value, session);
                                //UpdateVerticals(87, 9);
                                break;
                            }
                        case "Hdn9_10":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_10.Text, 9, Hdn9_10.Value, session);
                                //UpdateVerticals(88, 9);
                                break;
                            }
                        case "Hdn9_11":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_11.Text, 9, Hdn9_11.Value, session);
                                //UpdateVerticals(90, 9);
                                break;
                            }
                        case "Hdn9_12":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_12.Text, 9, Hdn9_12.Value, session);
                                //UpdateVerticals(91, 9);
                                break;
                            }
                        case "Hdn9_13":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl9_13.Text, 9, Hdn9_13.Value, session);
                                //UpdateVerticals(91, 9);
                                break;
                            }
                        case "Hdn10_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_1.Text, 10, Hdn10_1.Value, session);
                                //UpdateVerticals(66, 10);
                                break;
                            }
                        case "Hdn10_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_2.Text, 10, Hdn10_2.Value, session);
                                //UpdateVerticals(67, 10);
                                break;
                            }
                        case "Hdn10_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_3.Text, 10, Hdn10_3.Value, session);
                                //UpdateVerticals(68, 10);
                                break;
                            }
                        case "Hdn10_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_4.Text, 10, Hdn10_4.Value, session);
                                //UpdateVerticals(69, 10);
                                break;
                            }
                        case "Hdn10_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_5.Text, 10, Hdn10_5.Value, session);
                                //UpdateVerticals(70, 10);
                                break;
                            }
                        case "Hdn10_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_6.Text, 10, Hdn10_6.Value, session);
                                //UpdateVerticals(71, 10);
                                break;
                            }
                        case "Hdn10_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_7.Text, 10, Hdn10_7.Value, session);
                                //UpdateVerticals(72, 10);
                                break;
                            }
                        case "Hdn10_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_8.Text, 10, Hdn10_8.Value, session);
                                //UpdateVerticals(73, 10);
                                break;
                            }
                        case "Hdn10_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl10_9.Text, 10, Hdn10_9.Value, session);
                                //UpdateVerticals(74, 10);
                                break;
                            }
                        case "Hdn11_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_1.Text, 11, Hdn11_1.Value, session);
                                //UpdateVerticals(75, 11);
                                break;
                            }
                        case "Hdn11_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_2.Text, 11, Hdn11_2.Value, session);
                                //UpdateVerticals(76, 11);
                                break;
                            }
                        case "Hdn11_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_3.Text, 11, Hdn11_3.Value, session);
                                //UpdateVerticals(77, 11);
                                break;
                            }
                        case "Hdn11_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_4.Text, 11, Hdn11_4.Value, session);
                                //UpdateVerticals(78, 11);
                                break;
                            }
                        case "Hdn11_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_5.Text, 11, Hdn11_5.Value, session);
                                //UpdateVerticals(79, 11);
                                break;
                            }
                        case "Hdn11_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_6.Text, 11, Hdn11_6.Value, session);
                                //UpdateVerticals(89, 11);
                                break;
                            }
                        case "Hdn11_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_7.Text, 11, Hdn11_7.Value, session);
                                //UpdateVerticals(93, 11);
                                break;
                            }
                        case "Hdn11_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_8.Text, 11, Hdn11_8.Value, session);
                                //UpdateVerticals(94, 11);
                                break;
                            }
                        case "Hdn11_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_9.Text, 11, Hdn11_9.Value, session);
                                //UpdateVerticals(121, 11);
                                break;
                            }
                        case "Hdn11_10":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_10.Text, 11, Hdn11_10.Value, session);
                                //UpdateVerticals(122, 11);
                                break;
                            }
                        case "Hdn11_11":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_11.Text, 11, Hdn11_11.Value, session);
                                //UpdateVerticals(123, 11);
                                break;
                            }
                        case "Hdn11_12":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_12.Text, 11, Hdn11_12.Value, session);
                                //UpdateVerticals(124, 11);
                                break;
                            }
                        case "Hdn11_13":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_13.Text, 11, Hdn11_13.Value, session);
                                //UpdateVerticals(125, 11);
                                break;
                            }
                        case "Hdn11_14":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_14.Text, 11, Hdn11_14.Value, session);
                                //UpdateVerticals(125, 11);
                                break;
                            }
                        case "Hdn11_15":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_15.Text, 11, Hdn11_15.Value, session);
                                //UpdateVerticals(125, 11);
                                break;
                            }
                        case "Hdn11_16":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl11_16.Text, 11, Hdn11_16.Value, session);
                                //UpdateVerticals(125, 11);
                                break;
                            }
                        case "Hdn12_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl12_1.Text, 12, Hdn12_1.Value, session);
                                //UpdateVerticals(80, 12);
                                break;
                            }
                        case "Hdn12_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl12_2.Text, 12, Hdn12_2.Value, session);
                                //UpdateVerticals(81, 12);
                                break;
                            }
                        case "Hdn12_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl12_3.Text, 12, Hdn12_3.Value, session);
                                //UpdateVerticals(82, 12);
                                break;
                            }
                        case "Hdn12_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl12_4.Text, 12, Hdn12_4.Value, session);
                                //UpdateVerticals(82, 12);
                                break;
                            }
                        case "Hdn13_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl13_1.Text, 13, Hdn13_1.Value, session);
                                //UpdateVerticals(85, 13);
                                break;
                            }
                        case "Hdn13_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl13_2.Text, 13, Hdn13_2.Value, session);
                                //UpdateVerticals(86, 13);
                                break;
                            }
                        case "Hdn13_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl13_3.Text, 13, Hdn13_3.Value, session);
                                //UpdateVerticals(126, 13);
                                break;
                            }
                        case "Hdn14_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_1.Text, 14, Hdn14_1.Value, session);
                                //UpdateVerticals(96, 14);
                                break;
                            }
                        case "Hdn14_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_2.Text, 14, Hdn14_2.Value, session);
                                //UpdateVerticals(97, 14);
                                break;
                            }
                        case "Hdn14_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_3.Text, 14, Hdn14_3.Value, session);
                                //UpdateVerticals(98, 14);
                                break;
                            }
                        case "Hdn14_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_4.Text, 14, Hdn14_4.Value, session);
                                //UpdateVerticals(99, 14);
                                break;
                            }
                        case "Hdn14_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_5.Text, 14, Hdn14_5.Value, session);
                                //UpdateVerticals(100, 14);
                                break;
                            }
                        case "Hdn14_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_6.Text, 14, Hdn14_6.Value, session);
                                //UpdateVerticals(101, 14);
                                break;
                            }
                        case "Hdn14_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_7.Text, 14, Hdn14_7.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn14_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_8.Text, 14, Hdn14_8.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn14_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_9.Text, 14, Hdn14_9.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn14_10":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_10.Text, 14, Hdn14_10.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn14_11":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_11.Text, 14, Hdn14_11.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn14_12":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl14_12.Text, 14, Hdn14_12.Value, session);
                                //UpdateVerticals(102, 14);
                                break;
                            }
                        case "Hdn15_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_1.Text, 15, Hdn15_1.Value, session);
                                //UpdateVerticals(103, 15);
                                break;
                            }
                        case "Hdn15_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_2.Text, 15, Hdn15_2.Value, session);
                                //UpdateVerticals(104, 15);
                                break;
                            }
                        case "Hdn15_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_3.Text, 15, Hdn15_3.Value, session);
                                //UpdateVerticals(105, 15);
                                break;
                            }
                        case "Hdn15_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_4.Text, 15, Hdn15_4.Value, session);
                                //UpdateVerticals(106, 15);
                                break;
                            }
                        case "Hdn15_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_5.Text, 15, Hdn15_5.Value, session);
                                //UpdateVerticals(107, 15);
                                break;
                            }
                        case "Hdn15_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_6.Text, 15, Hdn15_6.Value, session);
                                //UpdateVerticals(108, 15);
                                break;
                            }
                        case "Hdn15_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_7.Text, 15, Hdn15_7.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn15_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl15_8.Text, 15, Hdn15_8.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_1":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_1.Text, 16, Hdn16_1.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_2":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_2.Text, 16, Hdn16_2.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_3":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_3.Text, 16, Hdn16_3.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_4":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_4.Text, 16, Hdn16_4.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_5":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_5.Text, 16, Hdn16_5.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_6":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_6.Text, 16, Hdn16_6.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_7":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_7.Text, 16, Hdn16_7.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_8":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_8.Text, 16, Hdn16_8.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                        case "Hdn16_9":
                            {
                                Sql.FixUserSubIndustriesGroupItemIdByDescription(vSession.User.Id, Lbl16_9.Text, 16, Hdn16_9.Value, session);
                                //UpdateVerticals(109, 15);
                                break;
                            }
                    }
                }
            }

            #endregion

            if (counter > 0 && counter < 16)
            {
                divVerticalsSuccess.Visible = true;
                LblVerticalSuccess.Text = "Done! ";
                LblVerticalSuccessContent.Text = "Your changes were saved successfully!";
                LoadData();
            }
        }

        private bool ExistItemToList(CheckBoxList cbxList, string newItemText, int newItemValue)
        {
            if (cbxList.Items.Count > 0)
            {
                foreach (ListItem listItem in cbxList.Items)
                {
                    if (newItemText != "")
                    {
                        if (listItem.Text == newItemText)
                            return true;
                    }
                    else if (newItemValue > 0)
                    {
                        if (listItem.Value == newItemValue.ToString())
                            return true;
                    }
                }
            }

            return false;
        }

        # endregion

        # region Buttons

        protected void RbtnSaveNotifications_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    int selectedItemsCount = 0;

                    HtmlInputCheckBox cbx1 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox1");
                    HtmlInputCheckBox cbx2 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox2");
                    HtmlInputCheckBox cbx3 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox3");
                    HtmlInputCheckBox cbx4 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox4");
                    HtmlInputCheckBox cbx5 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox5");
                    HtmlInputCheckBox cbx6 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox6");
                    HtmlInputCheckBox cbx7 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox7");
                    HtmlInputCheckBox cbx8 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox8");
                    HtmlInputCheckBox cbx9 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox9");
                    HtmlInputCheckBox cbx10 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox10");
                    HtmlInputCheckBox cbx11 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox11");
                    HtmlInputCheckBox cbx12 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox12");
                    HtmlInputCheckBox cbx13 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox13");
                    HtmlInputCheckBox cbx14 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox14");
                    HtmlInputCheckBox cbx15 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox15");
                    HtmlInputCheckBox cbx16 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox16");
                    HtmlInputCheckBox cbx17 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox17");
                    HtmlInputCheckBox cbx19 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox19");
                    HtmlInputCheckBox cbx20 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox20");
                    HtmlInputCheckBox cbx21 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox21");
                    HtmlInputCheckBox cbx22 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox22");
                    HtmlInputCheckBox cbx23 = (HtmlInputCheckBox)ControlFinder.FindControlRecursive(this, "Checkbox23");

                    selectedItemsCount = SetUserEmails(cbx1, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx2, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx3, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx4, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx5, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx6, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx7, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx8, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx9, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx10, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx11, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx12, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx13, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx14, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx15, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx16, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx17, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx19, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx20, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx21, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx22, selectedItemsCount);
                    selectedItemsCount = SetUserEmails(cbx23, selectedItemsCount);

                    LoadEmailSettings();

                    if (selectedItemsCount > 0)
                    {
                        string alert = "Your account settings were successfully updated";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Success, true, true, false);
                    }
                    else
                    {
                        string alert = "No items have been selected";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Info, true, true, false);
                    }

                    //UpdatePanel22.Update();

                    cbx1.Attributes["class"] = "make-switch";
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

        protected void BtnCancelAddNewCard_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearErrorFields();

                //div5.Visible = false;
                //div4.Visible = false;
                BtnAddNewCard.Text = "Add New Card";
                //BtnCancelAddNewCard.Visible = false;
                TbxCCNumber.Text = string.Empty;
                TbxCvcNumber.Text = string.Empty;
                DrpExpMonth.SelectedValue = "0";
                TbxExpYear.Text = string.Empty;
                divSaveCreditCardSuccess.Visible = false;
                LblSaveCreditCardSuccess.Text = string.Empty;
                divSaveCreditCardFailure.Visible = false;
                LblSaveCreditCardFailure.Text = string.Empty;

                //LoadCreditCardInfo();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnAddNewCard_OnClick(object sender, EventArgs args)
        {
            try
            {
                ClearErrorFields();

                if (vSession.LoggedInSubAccountRoleID > 0)
                {
                    divSaveCreditCardFailure.Visible = true;
                    LblSaveCreditCardFailureError.Text = "Error! ";
                    LblSaveCreditCardFailure.Text = "You are not authorized to make any change on this account!";

                    return;
                }

                string errorMsg = string.Empty;

                if (TbxCCNumber.Text.Trim() == string.Empty)
                {
                    divCCNumber.Visible = true;
                    LblCCNumberError.Text = "Please enter card number";
                    return;
                }

                if (TbxCvcNumber.Text.Trim() == string.Empty)
                {
                    divCvcNumber.Visible = true;
                    LblCvcNumberError.Text = "Please enter cvc number";
                    return;
                }

                if (DrpExpMonth.SelectedValue == "0")
                {
                    divExpMonth.Visible = true;
                    LblExpMonthError.Text = "Please select card expiration month";
                    return;
                }

                if (TbxExpYear.Text.Trim() == string.Empty)
                {
                    divExpYear.Visible = true;
                    LblExpYearError.Text = "Please enter card expiration year";
                    return;
                }
                else if (!Validations.IsNumberOnly(TbxExpYear.Text.Trim()))
                {
                    divExpYear.Visible = true;
                    LblExpYearError.Text = "Please enter valid year";
                    return;
                }
                else if ((Convert.ToInt32(TbxExpYear.Text) < 0 || Convert.ToInt32(TbxExpYear.Text) <= 00 || Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))) || (Convert.ToInt32(DrpExpMonth.SelectedValue) <= DateTime.Now.Month && Convert.ToInt32(TbxExpYear.Text) == Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2))))
                {
                    divExpYear.Visible = true;
                    LblExpYearError.Text = "Card is expired. Please enter new card";
                    return;
                }

                //ElioUsersCreditCards userCard = null;
              
                //userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);

                //string defaultCard = string.Empty;
                //string mode = string.Empty;

                //if (userCard != null)
                //{
                //    mode = "DELETE";
                //    defaultCard = userCard.CardStripeId;
                //}
                //else
                //{
                //    mode = "ADD";
                //    defaultCard = string.Empty;
                //}

                if (!string.IsNullOrEmpty(vSession.User.CustomerStripeId))
                {
                    try
                    {
                        bool success = Lib.Services.StripeAPI.StripeApi.AddNewCardNew(vSession.User, TbxCCNumber.Text, DrpExpMonth.SelectedItem.Text, TbxExpYear.Text, TbxCvcNumber.Text, out errorMsg);
                        if (success)
                        {
                            divSaveCreditCardSuccess.Visible = true;
                            LblSaveCreditCardSuccess.Text = "Your Credit Card changed successfully";
                            BtnAddNewCard.Text = "Add New Card";
                            TbxCCNumber.Text = string.Empty;
                            TbxCvcNumber.Text = string.Empty;
                            DrpExpMonth.SelectedValue = "0";
                            TbxExpYear.Text = string.Empty;
                        }
                        else
                        {
                            divSaveCreditCardFailure.Visible = true;
                            LblSaveCreditCardFailureError.Text = "Error! ";
                            LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";
                        }

                        //Xamarin.Payments.Stripe.StripeCard cardInfo = StripeLib.AddNewCreditCard(mode, vSession.User.CustomerStripeId, defaultCard, TbxCvcNumber.Text,
                        //                                                                        Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text),
                        //                                                                        TbxCCNumber.Text, vSession.User.LastName + " " + vSession.User.FirstName, vSession.User.Address, TbxAddress2.Text,
                        //                                                                        TbxOrigin.Text, TbxZipCode.Text, ref errorMsg);
                        //if (cardInfo != null)
                        //{
                        //    try
                        //    {
                        //        ElioUsersCreditCards cc = new ElioUsersCreditCards();

                        //        cc.CardStripeId = cardInfo.ID;
                        //        cc.CardFullname = cardInfo.Name;
                        //        cc.Address1 = cardInfo.AddressLine1;
                        //        cc.Address2 = cardInfo.AddressLine2;
                        //        cc.CardType = cardInfo.Type;
                        //        cc.ExpMonth = cardInfo.ExpirationMonth;
                        //        cc.ExpYear = cardInfo.ExpirationYear;
                        //        cc.Origin = cardInfo.AddressCountry;
                        //        cc.CvcCheck = cardInfo.CvcCheck.ToString();
                        //        cc.Fingerprint = cardInfo.Fingerprint;
                        //        cc.ZipCheck = cardInfo.AddressZipCheck;
                        //        cc.CardType = cardInfo.Type;
                        //        cc.IsDefault = 1;
                        //        cc.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                        //        cc.Sysdate = DateTime.Now;
                        //        cc.LastUpdated = DateTime.Now;
                        //        cc.CustomerStripeId = vSession.User.CustomerStripeId;
                        //        cc.UserId = vSession.User.Id;

                        //        DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                        //        loader.Insert(cc);

                        //        if (userCard != null)
                        //        {
                        //            userCard.IsDefault = 0;
                        //            userCard.LastUpdated = DateTime.Now;

                        //            loader.Update(userCard);
                        //        }

                        //        divSaveCreditCardSuccess.Visible = true;
                        //        LblSaveCreditCardSuccess.Text = "Your Credit Card Details saved successfully";
                        //        BtnAddNewCard.Text = "Add New Card";
                        //        //BtnCancelAddNewCard.Visible = false;
                        //        TbxCCNumber.Text = string.Empty;
                        //        TbxCvcNumber.Text = string.Empty;
                        //        DrpExpMonth.SelectedValue = "0";
                        //        TbxExpYear.Text = string.Empty;
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        //    }
                        //}
                        //else
                        //{
                        //    divSaveCreditCardFailure.Visible = true;
                        //    LblSaveCreditCardFailureError.Text = "Error! ";
                        //    LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";
                        //}
                    }
                    catch (Exception ex)
                    {
                        divSaveCreditCardFailure.Visible = true;
                        LblSaveCreditCardFailureError.Text = "Error! ";
                        LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";

                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
                else
                {
                    throw new Exception(string.Format("User {0} tried to update his credit card at {1} but customer_stripe_id was empty", vSession.User.Id, DateTime.Now.ToString()));
                }
            }
            catch (Exception ex)
            {
                divSaveCreditCardFailure.Visible = true;
                LblSaveCreditCardFailureError.Text = "Error! ";
                LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSaveCreditCardDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ClearErrorFields();

                string errorMsg = string.Empty;

                //if (TbxExpMonth.Text.Trim() == string.Empty)
                //{
                //    divExpMonth.Visible = true;
                //    LblExpMonthError.Text = "Please enter card expiration month";
                //    return;
                //}                
                //else if (Convert.ToInt32(TbxExpMonth.Text) <= Convert.ToInt32(0) || TbxExpMonth.Text == "00" || Convert.ToInt32(TbxExpMonth.Text) > Convert.ToInt32(12))
                //{
                //    divExpMonth.Visible = true;
                //    LblExpMonthError.Text = "Please enter valid card expiration month";
                //    return;
                //}

                if (DrpExpMonth.SelectedValue == "0")
                {
                    divExpMonth.Visible = true;
                    LblExpMonthError.Text = "Please enter card expiration month";
                    return;
                }

                if (TbxExpYear.Text.Trim() == string.Empty)
                {
                    divExpYear.Visible = true;
                    LblExpYearError.Text = "Please enter card expiration year";
                    return;
                }
                else if (Convert.ToInt32(TbxExpYear.Text) < 0 || Convert.ToInt32(TbxExpYear.Text) <= 00 || Convert.ToInt32(TbxExpYear.Text) < Convert.ToInt32(DateTime.Now.Year.ToString().Substring(2, 2)))
                {
                    divExpYear.Visible = true;
                    LblExpYearError.Text = "Please enter card expiration greater than today";
                    return;
                }

                ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);

                if (userCard != null)
                {
                    try
                    {
                        Xamarin.Payments.Stripe.StripeCard cardInfo = StripeLib.UpdateCreditCard(vSession.User.CustomerStripeId, userCard.CardStripeId,
                                                                                                 Convert.ToInt32(DrpExpMonth.SelectedItem.Text), Convert.ToInt32(TbxExpYear.Text),
                                                                                                 TbxFullName.Text, TbxAddress1.Text, TbxAddress2.Text,
                                                                                                 TbxOrigin.Text, TbxZipCode.Text, ref errorMsg);
                        if (cardInfo != null)
                        {
                            try
                            {
                                session.OpenConnection();

                                userCard.CardFullname = cardInfo.Name;
                                userCard.Address1 = cardInfo.AddressLine1;
                                userCard.Address2 = cardInfo.AddressLine2;
                                userCard.CardType = cardInfo.Type;
                                userCard.ExpMonth = cardInfo.ExpirationMonth;
                                userCard.ExpYear = cardInfo.ExpirationYear;
                                userCard.Origin = cardInfo.Country;
                                userCard.CvcCheck = cardInfo.CvcCheck.ToString();
                                userCard.ZipCheck = cardInfo.AddressZipCheck.ToString();
                                userCard.Fingerprint = cardInfo.Fingerprint;
                                userCard.IsDefault = 1;
                                userCard.IsDeleted = (cardInfo.Deleted) ? 1 : 0;
                                userCard.Sysdate = DateTime.Now;
                                userCard.LastUpdated = DateTime.Now;
                                userCard.CustomerStripeId = vSession.User.CustomerStripeId;
                                userCard.UserId = vSession.User.Id;

                                DataLoader<ElioUsersCreditCards> loader = new DataLoader<ElioUsersCreditCards>(session);
                                loader.Update(userCard);

                                divSaveCreditCardSuccess.Visible = true;
                                LblSaveCreditCardSuccess.Text = "Your Credit Card Details updated successfully";
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
                        else
                        {
                            divSaveCreditCardFailure.Visible = true;
                            LblSaveCreditCardFailureError.Text = "Error! ";
                            LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";
                            Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0} and Stripe ID {1}, tried to change his credit card but did not found", vSession.User.Id, vSession.User.CustomerStripeId));
                            
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        divSaveCreditCardFailure.Visible = true;
                        LblSaveCreditCardFailureError.Text = "Error! ";
                        LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";
                        
                        Logger.DetailedError(Request.Url.ToString(), ex.Message, errorMsg);
                        return;
                    }
                }
                else
                {
                    divSaveCreditCardFailure.Visible = true;
                    LblSaveCreditCardFailureError.Text = "Error! ";
                    LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";

                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0} and Stripe ID {1}, tried to change his credit card but did not found", vSession.User.Id, vSession.User.CustomerStripeId));
                    return;
                }
            }
            catch (Exception ex)
            {
                divSaveCreditCardFailure.Visible = true;
                LblSaveCreditCardFailureError.Text = "Error! ";
                LblSaveCreditCardFailure.Text = "Your credit card could not be created. Please try egain later or contact with us";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnSaveBillingDetails_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ClearErrorFields();

                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        divSaveFailure.Visible = true;
                        LblSaveBillingfailureError.Text = "Error! ";
                        LblSaveBillingfailure.Text = "You are not authorized to make any change on this account!";

                        return;
                    }

                    if (TbxBillingCompanyName.Text == "")
                    {
                        divSaveFailure.Visible = true;
                        LblSaveBillingfailureError.Text = "Error! ";
                        LblSaveBillingfailure.Text = "One or more fields have invalid data.";
                        divBillingCompanyName.Visible = true;
                        LblBillingCompanyNameError.Text = "Enter company name!";
                        
                        return;
                    }

                    if (TbxBillingCompanyAddress.Text == "")
                    {
                        divSaveFailure.Visible = true;
                        LblSaveBillingfailureError.Text = "Error! ";
                        LblSaveBillingfailure.Text = "One or more fields have invalid data.";
                        divBillingCompanyAddress.Visible = true;
                        LblBillingCompanyAddressError.Text = "Enter company address!";

                        return;
                    }

                    if (TbxBillingCompanyPostCode.Text == "")
                    {
                        divSaveFailure.Visible = true;
                        LblSaveBillingfailureError.Text = "Error! ";
                        LblSaveBillingfailure.Text = "One or more fields have invalid data.";
                        divBillingCompanyPostCode.Visible = true;
                        LblBillingCompanyPostCodeError.Text = "Enter company post code!";

                        return;
                    }

                    if (TbxBillingCompanyVatNumber.Text == "")
                    {
                        divSaveFailure.Visible = true;
                        LblSaveBillingfailureError.Text = "Error! ";
                        LblSaveBillingfailure.Text = "One or more fields have invalid data.";
                        divBillingCompanyVatNumber.Visible = true;
                        LblBillingCompanyVatNumberError.Text = "Enter company vat number!";

                        return;
                    }

                    if (vSession.User.HasBillingDetails == 0)
                    {
                        #region New Billing Account Data

                        ElioUsersBillingAccount account = new ElioUsersBillingAccount();

                        account.UserId = vSession.User.Id;
                        account.CompanyBillingAddress = TbxBillingCompanyAddress.Text;
                        account.CompanyPostCode = TbxBillingCompanyPostCode.Text;
                        account.CompanyVatNumber = TbxBillingCompanyVatNumber.Text;
                        account.CompanyVatOffice = "";
                        account.PostCode = "";
                        account.UserIdNumber = "";
                        account.UserVatNumber = "";
                        account.BillingEmail = vSession.User.Email;
                        account.HasVat = 1;
                        account.Sysdate = DateTime.Now;
                        account.LastUpdated = DateTime.Now;
                        account.IsActive = 1;

                        DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                        loader0.Insert(account);

                        #endregion

                        #region Update User / Session

                        vSession.User.HasBillingDetails = 1;
                        vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                        #endregion
                    }
                    else
                    {
                        #region Update Billing Account Data

                        ElioUsersBillingAccount account = Sql.GetUserAccountByUserId(vSession.User.Id, session);

                        if (account != null)
                        {
                            account.CompanyBillingAddress = TbxBillingCompanyAddress.Text;
                            account.CompanyPostCode = TbxBillingCompanyPostCode.Text;
                            account.CompanyVatNumber = TbxBillingCompanyVatNumber.Text;
                            account.CompanyVatOffice = "";
                            account.PostCode = "";
                            account.UserIdNumber = "";
                            account.UserVatNumber = "";
                            account.BillingEmail = vSession.User.Email;
                            account.HasVat = 1;
                            account.Sysdate = DateTime.Now;
                            account.LastUpdated = DateTime.Now;
                            account.IsActive = 1;

                            DataLoader<ElioUsersBillingAccount> loader0 = new DataLoader<ElioUsersBillingAccount>(session);
                            loader0.Update(account);
                        }

                        #endregion
                    }

                    divSaveSuccess.Visible = true;
                    LblSaveBillingSuccess.Text = "Your Billing Account Details saved successfully";
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

        protected void BtnAddEmail_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divWarningMsg.Visible = false;
                divSuccessMsg.Visible = false;
                LblWarningMsg.Text = string.Empty;
                LblWarningMsgContent.Text = string.Empty;
                LblSuccessMsg.Text = string.Empty;
                LblSuccessMsgContent.Text = string.Empty;

                if (TbxMoreEmails.Text == string.Empty)
                {
                    divWarningMsg.Visible = true;
                    LblWarningMsg.Text = "Error!";
                    LblWarningMsgContent.Text = "Enter Email";
                    return;
                }
                else
                {
                    if (!Validations.IsEmail(TbxMoreEmails.Text))
                    {
                        divWarningMsg.Visible = true;
                        LblWarningMsg.Text = "Error!";
                        LblWarningMsgContent.Text = "Email is invalid";
                        return;
                    }

                    if (Sql.IsEmailRegistered(TbxMoreEmails.Text, session))
                    {
                        divWarningMsg.Visible = true;
                        LblWarningMsg.Text = "Warning!";
                        LblWarningMsgContent.Text = "Email is already registered";
                        return;
                    }

                    if (Sql.IsUserEmailRegistered(vSession.User.Id, TbxMoreEmails.Text, session))
                    {
                        divWarningMsg.Visible = true;
                        LblWarningMsg.Text = "Warning!";
                        LblWarningMsgContent.Text = "Email exist";
                        return;
                    }
                }

                ElioUserEmails email = new ElioUserEmails();

                email.UserId = vSession.User.Id;
                email.Email = TbxMoreEmails.Text;
                email.Sysdate = DateTime.Now;
                email.LastUpdate = DateTime.Now;

                DataLoader<ElioUserEmails> loader = new DataLoader<ElioUserEmails>(session);
                loader.Insert(email);

                divSuccessMsg.Visible = true;
                LblSuccessMsg.Text = "Done!";
                LblSuccessMsgContent.Text = "Successfully saved";

                RdgEmails.Rebind();

                TbxMoreEmails.Text = string.Empty;
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

        protected void BtnChangePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    divCurPaswError.Visible = false;
                    divNewPaswError.Visible = false;
                    divRetNewPaswError.Visible = false;
                    divPaswSuccess.Visible = false;
                    divPaswFailure.Visible = false;

                    if (string.IsNullOrEmpty(TbxCurPasw.Text))
                    {
                        divCurPaswError.Visible = true;
                        LblCurPaswError.Text = "Enter your current password!";
                        LoadIndustries();
                        LoadPrograms();
                        LoadMarkets();
                        LoadSubcategories();
                        LoadAPIes();
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(vSession.SubAccountEmailLogin))
                        {
                            if (TbxCurPasw.Text != vSession.User.Password)
                            {
                                divCurPaswError.Visible = true;
                                LblCurPaswError.Text = "Wrong password!";
                                LoadIndustries();
                                LoadPrograms();
                                LoadMarkets();
                                LoadSubcategories();
                                LoadAPIes();
                                return;
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(TbxNewPasw.Text))
                    {
                        divNewPaswError.Visible = true;
                        LblNewPaswError.Text = "Enter a new password!";
                        LoadIndustries();
                        LoadPrograms();
                        LoadMarkets();
                        LoadSubcategories();
                        LoadAPIes();
                        return;
                    }
                    else
                    {
                        if (TbxNewPasw.Text.Length < 8)
                        {
                            divNewPaswError.Visible = true;
                            LblNewPaswError.Text = "Password must be at least 8 characters!";
                            LoadIndustries();
                            LoadPrograms();
                            LoadMarkets();
                            LoadSubcategories();
                            LoadAPIes();
                            return;
                        }

                        if (!Validations.IsPasswordCharsValid(TbxNewPasw.Text))
                        {
                            divNewPaswError.Visible = true;
                            LblNewPaswError.Text = "Password contains invalid characters! Allowed symbols: !@#$%^*()_.";
                            LoadIndustries();
                            LoadPrograms();
                            LoadMarkets();
                            LoadSubcategories();
                            LoadAPIes();
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(TbxRetNewPasw.Text))
                    {
                        divRetNewPaswError.Visible = true;
                        LblRetNewPaswError.Text = "Retype your new password!";
                        LoadIndustries();
                        LoadPrograms();
                        LoadMarkets();
                        LoadSubcategories();
                        LoadAPIes();
                        return;
                    }
                    else
                    {
                        if (TbxNewPasw.Text != TbxRetNewPasw.Text)
                        {
                            divRetNewPaswError.Visible = true;
                            LblRetNewPaswError.Text = "Passwords don't match!";
                            LoadIndustries();
                            LoadPrograms();
                            LoadMarkets();
                            LoadSubcategories();
                            LoadAPIes();
                            return;
                        }
                    }

                    if (vSession.LoggedInSubAccountRoleID == 0)
                    {
                        #region Update new password

                        vSession.User.Password = TbxNewPasw.Text;
                        vSession.User.PasswordEncrypted = MD5.Encrypt(vSession.User.Password);
                        vSession.User.LastUpdated = DateTime.Now;

                        vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                        #endregion
                    }
                    else
                    {
                        #region Update Sub-Account Password too if has

                        ElioUsersSubAccounts subAccount = Sql.GetSubAccountByEmail(vSession.SubAccountEmailLogin, session);
                        if (subAccount != null)
                        {
                            subAccount.Password = TbxNewPasw.Text;
                            subAccount.PasswordEncrypted = MD5.Encrypt(subAccount.Password);
                            subAccount.LastUpdated = DateTime.Now;

                            GlobalDBMethods.UpDateSubUser(subAccount, session);
                        }
                        else
                        {
                            divPaswFailure.Visible = true;
                            LblPaswFailure.Text = "Error! ";
                            LblPaswFailureContent.Text = "Password could not be updated";
                            return;
                        }

                        #endregion
                    }

                    divPaswSuccess.Visible = true;
                    LblPaswSuccess.Text = "Done! ";
                    LblPaswSuccessContent.Text = "Password was changed successfully";
                    LoadIndustries();
                    LoadPrograms();
                    LoadMarkets();
                    LoadSubcategories();
                    LoadAPIes();
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

        protected void BtnCancelChangePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divLogoSuccess.Visible = false;
                    divLogoFailure.Visible = false;
                    divCompanyNameError.Visible = false;
                    divCompanyUsernameError.Visible = false;                    
                    divCompanyEmailError.Visible = false;
                    divCompanyOfficialEmailError.Visible = false;
                    divCompanyWebsiteError.Visible = false;
                    divCompanyCountryError.Visible = false;
                    divCompanyAddressError.Visible = false;
                    divCompanyPhoneError.Visible = false;
                    divCompanyProductDemoError.Visible = false;
                    divMashapeError.Visible = false;
                    divCompanyOverviewError.Visible = false;
                    divCompanyDescriptionError.Visible = false;
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;
                    divCurPaswError.Visible = false;
                    divNewPaswError.Visible = false;
                    divRetNewPaswError.Visible = false;
                    divPaswSuccess.Visible = false;
                    divPaswFailure.Visible = false;
                    divBusinessSuccess.Visible = false;
                    divBusinessFailure.Visible = false;

                    LblLogoSuccess.Text = string.Empty;
                    LblLogoSuccessContent.Text = string.Empty;
                    LblLogoFailure.Text = string.Empty;
                    LblLogoFailureContent.Text = string.Empty;
                    LblCompanyNameError.Text = string.Empty;
                    LblCompanyUsernameError.Text = string.Empty;
                    LblCompanyEmailError.Text = string.Empty;
                    LblCompanyOfficialEmailError.Text = string.Empty;
                    LblCompanyWebsiteError.Text = string.Empty;
                    LblCompanyCountryError.Text = string.Empty;
                    LblCompanyAddressError.Text = string.Empty;
                    LblCompanyPhoneError.Text = string.Empty;
                    LblCompanyProductDemoError.Text = string.Empty;
                    LblMashapeError.Text = string.Empty;
                    LblCompanyOverviewError.Text = string.Empty;
                    LblCompanyDescriptionError.Text = string.Empty;
                    LblGeneralFailure.Text = string.Empty;
                    LblGeneralFailureContent.Text = string.Empty;
                    LblCurPaswError.Text = string.Empty;
                    LblNewPaswError.Text = string.Empty;
                    LblRetNewPaswError.Text = string.Empty;
                    LblPaswFailure.Text = string.Empty;
                    LblPaswFailureContent.Text = string.Empty;
                    LblPaswSuccess.Text = string.Empty;
                    LblPaswSuccessContent.Text = string.Empty;
                    LblBusinessSuccess.Text = string.Empty;
                    LblBusinessSuccessContent.Text = string.Empty;
                    LblBusinessFailure.Text = string.Empty;
                    LblBusinessFailureContent.Text = string.Empty;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"), false);
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
        }

        protected void BtnSaveGeneral_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();
                    
                    divCompanyNameError.Visible = false;
                    divCompanyUsernameError.Visible = false;
                    divCompanyEmailError.Visible = false;
                    divCompanyOfficialEmailError.Visible = false;
                    divCompanyCountryError.Visible = false;
                    divCompanyAddressError.Visible = false;
                    divCompanyPhoneError.Visible = false;
                    divCompanyProductDemoError.Visible = false;
                    divCompanyOverviewError.Visible = false;
                    divCompanyDescriptionError.Visible = false;
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;
                    divCompanyWebsiteError.Visible = false;

                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        divGeneralFailure.Visible = true;
                        LblGeneralFailure.Text = "Error! ";
                        LblGeneralFailureContent.Text = "You are not authorized to make any change on this account";
                        LoadData();
                        return;
                    }

                    string currentCompanyName = vSession.User.CompanyName;

                    if (!string.IsNullOrEmpty(TbxCompanyUsername.Text))
                    {
                        if (TbxCompanyUsername.Text.Length < 8)
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyUsernameError.Visible = true;
                            LblCompanyUsernameError.Text = "Username must be at least 8 characters!";
                            divCompanyUsernameError.Focus();
                            LoadData();
                            return;
                        }

                        if (!Validations.IsUsernameCharsValid(TbxCompanyUsername.Text))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyUsernameError.Visible = true;
                            LblCompanyUsernameError.Text = "Username contains invalid characters. Allowed symbols: _.@!$";
                            divCompanyUsernameError.Focus();
                            LoadData();
                            return;
                        }

                        if (Sql.ExistUsernameToOtherUser(TbxCompanyUsername.Text, vSession.User.Id, session))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyUsernameError.Visible = true;
                            LblCompanyUsernameError.Text = "This username is not available!";
                            divCompanyUsernameError.Focus();
                            LoadData();
                            return;
                        }
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblGeneralFailure.Text = "Error! ";
                        LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                        divCompanyUsernameError.Visible = true;
                        LblCompanyUsernameError.Text = "Username cannot be empty!";
                        divCompanyUsernameError.Focus();
                        LoadData();
                        return;
                    }

                    if (!string.IsNullOrEmpty(TbxCompanyEmail.Text))
                    {
                        if (!Validations.IsValidEmail(TbxCompanyEmail.Text))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyEmailError.Visible = true;
                            LblCompanyEmailError.Text = "Enter a valid email!";
                            divCompanyEmailError.Focus();
                            LoadData();
                            return;
                        }

                        if (Sql.ExistEmailToOtherUser(TbxCompanyEmail.Text, vSession.User.Id, session))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyEmailError.Visible = true;
                            LblCompanyEmailError.Text = "This email is not available!";
                            divCompanyEmailError.Focus();
                            LoadData();
                            return;
                        }
                    }
                    else
                    {
                        divGeneralFailure.Visible = true;
                        LblGeneralFailure.Text = "Error! ";
                        LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                        divCompanyEmailError.Visible = true;
                        LblCompanyEmailError.Text = "Email cannot be empty!";
                        divCompanyEmailError.Focus();
                        LoadData();
                        return;
                    }

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        if (string.IsNullOrEmpty(TbxCompanyName.Text))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyNameError.Visible = true;
                            LblCompanyNameError.Text = "Company name cannot be empty!";
                            divCompanyNameError.Focus();
                            LoadData();
                            return;
                        }

                        if (!string.IsNullOrEmpty(TbxCompanyOfficialEmail.Text))
                        {
                            if (!Validations.IsEmail(TbxCompanyOfficialEmail.Text))
                            {
                                divGeneralFailure.Visible = true;
                                LblGeneralFailure.Text = "Error! ";
                                LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                                divCompanyOfficialEmailError.Visible = true;
                                LblCompanyOfficialEmailError.Text = "Enter a valid email!";
                                divCompanyOfficialEmailError.Focus();
                                LoadData();
                                return;
                            }

                            if (Sql.ExistEmailToOtherUser(TbxCompanyOfficialEmail.Text, vSession.User.Id, session))
                            {
                                divGeneralFailure.Visible = true;
                                LblGeneralFailure.Text = "Error! ";
                                LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                                divCompanyOfficialEmailError.Visible = true;
                                LblCompanyOfficialEmailError.Text = "This email is not available!";
                                divCompanyOfficialEmailError.Focus();
                                LoadData();
                                return;
                            }
                        }

                        if (string.IsNullOrEmpty(TbxCompanyWebsite.Text))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyWebsiteError.Visible = true;
                            LblCompanyWebsiteError.Text = "Website cannot be empty!";
                            divCompanyWebsiteError.Focus();
                            LoadData();
                            return;
                        }

                        if (DdlCountries.SelectedItem.Value == "0")
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyCountryError.Visible = true;
                            LblCompanyCountryError.Text = "You must select Country!";
                            divCompanyCountryError.Focus();
                            LoadData();
                            return;
                        }

                        if (string.IsNullOrEmpty(TbxCompanyAddress.Text))
                        {
                            divGeneralFailure.Visible = true;
                            LblGeneralFailure.Text = "Error! ";
                            LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                            divCompanyAddressError.Visible = true;
                            LblCompanyAddressError.Text = "Address cannot be empty!";
                            divCompanyAddressError.Focus();
                            LoadData();
                            return;
                        }

                        //if (string.IsNullOrEmpty(TbxCompanyPhone.Text))
                        //{
                        //    divGeneralFailure.Visible = true;
                        //    LblGeneralFailure.Text = "Error! ";
                        //    LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                        //    divCompanyPhoneError.Visible = true;
                        //    LblCompanyPhoneError.Text = "Phone cannot be empty!";
                        //    divCompanyPhoneError.Focus();
                        //    LoadData();
                        //    return;
                        //}

                        //if (vSession.User.CompanyType == Types.Vendors.ToString())
                        //{
                        //    if (string.IsNullOrEmpty(TbxCompanyProductDemo.Text))
                        //    {
                        //        divGeneralFailure.Visible = true;
                        //        LblGeneralFailure.Text = "Error! ";
                        //        LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                        //        divCompanyProductDemoError.Visible = true;
                        //        LblCompanyProductDemoError.Text = "Product Demo Link cannot be empty!";
                        //        divCompanyProductDemoError.Focus();
                        //        LoadData();
                        //        return;
                        //    }
                        //}

                        if (vSession.User.UserApplicationType == (int)UserApplicationType.Elioplus)
                        {
                            if (string.IsNullOrEmpty(TbxCompanyOverview.Text))
                            {
                                divGeneralFailure.Visible = true;
                                LblGeneralFailure.Text = "Error! ";
                                LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                                divCompanyOverviewError.Visible = true;
                                LblCompanyOverviewError.Text = "Overview cannot be empty!";
                                divCompanyOverviewError.Focus();
                                LoadData();
                                return;
                            }
                        }

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (string.IsNullOrEmpty(TbxCompanyDescription.Text))
                            {
                                divGeneralFailure.Visible = true;
                                LblGeneralFailure.Text = "Error! ";
                                LblGeneralFailureContent.Text = "One or more fields have invalid data.";
                                divCompanyDescriptionError.Visible = true;
                                LblCompanyDescriptionError.Text = "Description cannot be empty!";
                                divCompanyDescriptionError.Focus();
                                LoadData();
                                return;
                            }
                        }

                        vSession.User.CompanyName = TbxCompanyName.Text;
                        vSession.User.OfficialEmail = TbxCompanyOfficialEmail.Text;
                        vSession.User.WebSite = (TbxCompanyWebsite.Text.StartsWith("http://") || TbxCompanyWebsite.Text.StartsWith("https://")) ? TbxCompanyWebsite.Text : "https://" + TbxCompanyWebsite.Text;
                        vSession.User.Country = DdlCountries.SelectedItem.Text;
                        vSession.User.CompanyRegion = Sql.GetRegionByCountryId(Convert.ToInt32(DdlCountries.SelectedValue), session);
                        vSession.User.Address = TbxCompanyAddress.Text;
                        vSession.User.Phone = TbxCompanyPhone.Text;

                        if (vSession.User.CompanyType == Types.Vendors.ToString() && TbxCompanyProductDemo.Text.Trim() != string.Empty)
                            vSession.User.VendorProductDemoLink = TbxCompanyProductDemo.Text;
                        else
                            vSession.User.VendorProductDemoLink = string.Empty;

                        if (!string.IsNullOrEmpty(TbxCompanyOverview.Text))
                        {
                            vSession.User.Overview = GlobalMethods.FixStringEntryToParagraphs(TbxCompanyOverview.Text);
                        }

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (!string.IsNullOrEmpty(TbxCompanyDescription.Text))
                            {
                                vSession.User.Description = GlobalMethods.FixStringEntryToParagraphs(TbxCompanyDescription.Text);
                            }
                        }

                        vSession.User.MashapeUsername = TbxMashape.Text;
                    }

                    vSession.User.Username = TbxCompanyUsername.Text;
                    vSession.User.UsernameEncrypted = MD5.Encrypt(vSession.User.Username);
                    vSession.User.Email = TbxCompanyEmail.Text;                    

                    vSession.User.LastUpdated = DateTime.Now;

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    loader.Update(vSession.User);

                    divGeneralSuccess.Visible = true;
                    LblGeneralSuccess.Text = "Done! ";
                    LblGeneralSuccessContent.Text = "Your changes were saved successfully!";

                    LoadData();

                    if (vSession.User.CompanyName != currentCompanyName)
                    {
                        string path = HttpContext.Current.Request.Url.AbsolutePath;
                        string[] pathArray = path.Split('/');
                        string name = pathArray[3];

                        if (NotCorrectName(name, vSession.User.CompanyName))
                        {
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"), false);
                        }
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

        protected void BtnCancelGeneral_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divLogoSuccess.Visible = false;
                    divLogoFailure.Visible = false;
                    divCompanyNameError.Visible = false;
                    divCompanyUsernameError.Visible = false;
                    divCompanyEmailError.Visible = false;
                    divCompanyOfficialEmailError.Visible = false;
                    divCompanyWebsiteError.Visible = false;
                    divCompanyCountryError.Visible = false;
                    divCompanyAddressError.Visible = false;
                    divCompanyPhoneError.Visible = false;
                    divCompanyProductDemoError.Visible = false;
                    divMashapeError.Visible = false;
                    divCompanyOverviewError.Visible = false;
                    divCompanyDescriptionError.Visible = false;
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;
                    divCurPaswError.Visible = false;
                    divNewPaswError.Visible = false;
                    divRetNewPaswError.Visible = false;
                    divPaswSuccess.Visible = false;
                    divPaswFailure.Visible = false;
                    divBusinessSuccess.Visible = false;
                    divBusinessFailure.Visible = false;

                    LblLogoSuccess.Text = string.Empty;
                    LblLogoSuccessContent.Text = string.Empty;
                    LblLogoFailure.Text = string.Empty;
                    LblLogoFailureContent.Text = string.Empty;
                    LblCompanyNameError.Text = string.Empty;
                    LblCompanyUsernameError.Text = string.Empty;
                    LblCompanyEmailError.Text = string.Empty;
                    LblCompanyOfficialEmailError.Text = string.Empty;
                    LblCompanyWebsiteError.Text = string.Empty;
                    LblCompanyCountryError.Text = string.Empty;
                    LblCompanyAddressError.Text = string.Empty;
                    LblCompanyPhoneError.Text = string.Empty;
                    LblCompanyProductDemoError.Text = string.Empty;
                    LblMashapeError.Text = string.Empty;
                    LblCompanyOverviewError.Text = string.Empty;
                    LblCompanyDescriptionError.Text = string.Empty;
                    LblGeneralFailure.Text = string.Empty;
                    LblGeneralFailureContent.Text = string.Empty;
                    LblCurPaswError.Text = string.Empty;
                    LblNewPaswError.Text = string.Empty;
                    LblRetNewPaswError.Text = string.Empty;
                    LblPaswFailure.Text = string.Empty;
                    LblPaswFailureContent.Text = string.Empty;
                    LblPaswSuccess.Text = string.Empty;
                    LblPaswSuccessContent.Text = string.Empty;
                    LblBusinessSuccess.Text = string.Empty;
                    LblBusinessSuccessContent.Text = string.Empty;
                    LblBusinessFailure.Text = string.Empty;
                    LblBusinessFailureContent.Text = string.Empty;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"), false);
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
        }

        protected void BtnSubmitVerticals_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        divVerticalFailure.Visible = true;
                        LblVerticalFailure.Text = "Warning! ";
                        LblVerticalFailureContent.Text = "You are not authorized to make any change on this account!";
                        LoadData();
                        return;
                    }

                    divVerticalFailure.Visible = false;
                    divVerticalsSuccess.Visible = false;

                    SaveVerticals();
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

        protected void BtnCancelSubmitVerticals_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divLogoSuccess.Visible = false;
                    divLogoFailure.Visible = false;
                    divCompanyNameError.Visible = false;
                    divCompanyUsernameError.Visible = false;
                    divCompanyEmailError.Visible = false;
                    divCompanyOfficialEmailError.Visible = false;
                    divCompanyWebsiteError.Visible = false;
                    divCompanyCountryError.Visible = false;
                    divCompanyAddressError.Visible = false;
                    divCompanyPhoneError.Visible = false;
                    divCompanyProductDemoError.Visible = false;
                    divMashapeError.Visible = false;
                    divCompanyOverviewError.Visible = false;
                    divCompanyDescriptionError.Visible = false;
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;
                    divCurPaswError.Visible = false;
                    divNewPaswError.Visible = false;
                    divRetNewPaswError.Visible = false;
                    divPaswSuccess.Visible = false;
                    divPaswFailure.Visible = false;
                    divBusinessSuccess.Visible = false;
                    divBusinessFailure.Visible = false;

                    LblLogoSuccess.Text = string.Empty;
                    LblLogoSuccessContent.Text = string.Empty;
                    LblLogoFailure.Text = string.Empty;
                    LblLogoFailureContent.Text = string.Empty;
                    LblCompanyNameError.Text = string.Empty;
                    LblCompanyUsernameError.Text = string.Empty;
                    LblCompanyEmailError.Text = string.Empty;
                    LblCompanyOfficialEmailError.Text = string.Empty;
                    LblCompanyWebsiteError.Text = string.Empty;
                    LblCompanyCountryError.Text = string.Empty;
                    LblCompanyAddressError.Text = string.Empty;
                    LblCompanyPhoneError.Text = string.Empty;
                    LblCompanyProductDemoError.Text = string.Empty;
                    LblMashapeError.Text = string.Empty;
                    LblCompanyOverviewError.Text = string.Empty;
                    LblCompanyDescriptionError.Text = string.Empty;
                    LblGeneralFailure.Text = string.Empty;
                    LblGeneralFailureContent.Text = string.Empty;
                    LblCurPaswError.Text = string.Empty;
                    LblNewPaswError.Text = string.Empty;
                    LblRetNewPaswError.Text = string.Empty;
                    LblPaswFailure.Text = string.Empty;
                    LblPaswFailureContent.Text = string.Empty;
                    LblPaswSuccess.Text = string.Empty;
                    LblPaswSuccessContent.Text = string.Empty;
                    LblBusinessSuccess.Text = string.Empty;
                    LblBusinessSuccessContent.Text = string.Empty;
                    LblBusinessFailure.Text = string.Empty;
                    LblBusinessFailureContent.Text = string.Empty;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"), false);
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
        }

        protected void BtnSaveBusinessSettings_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        LblBusinessFailure.Text = "Error! ";
                        LblBusinessFailureContent.Text = "You are not authorized to make any change on this account";
                        divBusinessFailure.Visible = true;
                        divBusinessFailure.Focus();

                        ScriptManager.RegisterStartupScript(this, GetType(), "SetIndustries1", "SetIndustries();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetPrograms1", "SetPrograms();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetMarkets1", "SetMarkets();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetAPIs1", "SetAPIs();", true);

                        return;
                    }

                    session.OpenConnection();
                    session.BeginTransaction();

                    divBusinessSuccess.Visible = false;
                    divBusinessFailure.Visible = false;

                    bool hasSelectedIndustry = (HdnIndAdvMark.Value.ToString() == "0" && HdnIndCommun.Value.ToString() == "0" && HdnIndConsWeb.Value.ToString() == "0" && HdnIndDigMed.Value.ToString() == "0"
                        && HdnIndEcom.Value.ToString() == "0" && HdnIndEduc.Value.ToString() == "0" && HdnIndEnter.Value.ToString() == "0" && HdnIndEntGam.Value.ToString() == "0"
                        && HdnIndHard.Value.ToString() == "0" && HdnIndMob.Value.ToString() == "0" && HdnIndNetHos.Value.ToString() == "0" && HdnIndSocMed.Value.ToString() == "0"
                        && HdnIndSoft.Value.ToString() == "0"
                        && HdnInd14.Value.ToString() == "0" && HdnInd15.Value.ToString() == "0" && HdnInd16.Value.ToString() == "0" && HdnInd17.Value.ToString() == "0"
                        && HdnInd18.Value.ToString() == "0" && HdnInd19.Value.ToString() == "0" && HdnInd20.Value.ToString() == "0" && HdnInd21.Value.ToString() == "0"
                        && HdnInd22.Value.ToString() == "0" && HdnInd23.Value.ToString() == "0" && HdnInd24.Value.ToString() == "0" && HdnInd25.Value.ToString() == "0"
                        && HdnInd26.Value.ToString() == "0" && HdnInd27.Value.ToString() == "0" && HdnInd28.Value.ToString() == "0" && HdnInd29.Value.ToString() == "0"
                        && HdnInd30.Value.ToString() == "0" && HdnInd31.Value.ToString() == "0" && HdnInd32.Value.ToString() == "0" && HdnInd33.Value.ToString() == "0"
                        && HdnInd34.Value.ToString() == "0" && HdnInd35.Value.ToString() == "0" && HdnInd36.Value.ToString() == "0" && HdnInd37.Value.ToString() == "0"
                        && HdnInd38.Value.ToString() == "0" && HdnInd39.Value.ToString() == "0" && HdnInd40.Value.ToString() == "0" && HdnInd41.Value.ToString() == "0"
                        && HdnInd42.Value.ToString() == "0") ? false : true;

                    bool hasSelectedProgram = (HdnProgWhiteL.Value.ToString() == "0" && HdnProgResel.Value.ToString() == "0" && HdnProgVAR.Value.ToString() == "0" && HdnProgDistr.Value.ToString() == "0" && HdnProgAPIprg.Value.ToString() == "0" && HdnProgSysInteg.Value.ToString() == "0" && HdnProgServProv.Value.ToString() == "0") ? false : true;

                    bool hasSelectedMarket = (HdnMarkConsum.Value.ToString() == "0" && HdnMarkSOHO.Value.ToString() == "0" && HdnMarkSmallMid.Value.ToString() == "0" && HdnMarkEnter.Value.ToString() == "0") ? false : true;

                    if (hasSelectedIndustry && hasSelectedProgram && hasSelectedMarket)
                    {
                        UpdateIndustries(1, HdnIndAdvMark.Value.ToString());
                        UpdateIndustries(2, HdnIndCommun.Value.ToString());
                        UpdateIndustries(3, HdnIndConsWeb.Value.ToString());
                        UpdateIndustries(4, HdnIndDigMed.Value.ToString());
                        UpdateIndustries(5, HdnIndEcom.Value.ToString());
                        UpdateIndustries(6, HdnIndEduc.Value.ToString());
                        UpdateIndustries(7, HdnIndEnter.Value.ToString());
                        UpdateIndustries(8, HdnIndEntGam.Value.ToString());
                        UpdateIndustries(9, HdnIndHard.Value.ToString());
                        UpdateIndustries(10, HdnIndMob.Value.ToString());
                        UpdateIndustries(11, HdnIndNetHos.Value.ToString());
                        UpdateIndustries(12, HdnIndSocMed.Value.ToString());
                        UpdateIndustries(13, HdnIndSoft.Value.ToString());
                        UpdateIndustries(14, HdnInd14.Value.ToString());
                        UpdateIndustries(15, HdnInd15.Value.ToString());
                        UpdateIndustries(16, HdnInd16.Value.ToString());
                        UpdateIndustries(17, HdnInd17.Value.ToString());
                        UpdateIndustries(18, HdnInd18.Value.ToString());
                        UpdateIndustries(19, HdnInd19.Value.ToString());
                        UpdateIndustries(20, HdnInd20.Value.ToString());
                        UpdateIndustries(21, HdnInd21.Value.ToString());
                        UpdateIndustries(22, HdnInd22.Value.ToString());
                        UpdateIndustries(23, HdnInd23.Value.ToString());
                        UpdateIndustries(24, HdnInd24.Value.ToString());
                        UpdateIndustries(25, HdnInd25.Value.ToString());
                        UpdateIndustries(26, HdnInd26.Value.ToString());

                        UpdateIndustries(27, HdnInd27.Value.ToString());
                        UpdateIndustries(28, HdnInd28.Value.ToString());
                        UpdateIndustries(29, HdnInd29.Value.ToString());
                        UpdateIndustries(30, HdnInd30.Value.ToString());
                        UpdateIndustries(31, HdnInd31.Value.ToString());
                        UpdateIndustries(32, HdnInd32.Value.ToString());
                        UpdateIndustries(33, HdnInd33.Value.ToString());
                        UpdateIndustries(34, HdnInd34.Value.ToString());
                        UpdateIndustries(35, HdnInd35.Value.ToString());
                        UpdateIndustries(36, HdnInd36.Value.ToString());
                        UpdateIndustries(37, HdnInd37.Value.ToString());
                        UpdateIndustries(38, HdnInd38.Value.ToString());
                        UpdateIndustries(39, HdnInd39.Value.ToString());

                        UpdateIndustries(40, HdnInd40.Value.ToString());
                        UpdateIndustries(41, HdnInd41.Value.ToString());
                        UpdateIndustries(42, HdnInd42.Value.ToString());

                        UpdatePrograms(1, HdnProgWhiteL.Value.ToString());
                        UpdatePrograms(2, HdnProgResel.Value.ToString());
                        UpdatePrograms(3, HdnProgVAR.Value.ToString());
                        UpdatePrograms(4, HdnProgDistr.Value.ToString());
                        UpdatePrograms(5, HdnProgAPIprg.Value.ToString());
                        UpdatePrograms(6, HdnProgSysInteg.Value.ToString());
                        UpdatePrograms(7, HdnProgServProv.Value.ToString());

                        UpdateMarkets(1, HdnMarkConsum.Value.ToString());
                        UpdateMarkets(2, HdnMarkSOHO.Value.ToString());
                        UpdateMarkets(3, HdnMarkSmallMid.Value.ToString());
                        UpdateMarkets(4, HdnMarkEnter.Value.ToString());

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            UpdateAPIs(1, HdnAPIBusServ.Value.ToString());
                            UpdateAPIs(2, HdnAPIMedEnter.Value.ToString());
                            UpdateAPIs(3, HdnAPIRetEcom.Value.ToString());
                            UpdateAPIs(4, HdnAPIGeol.Value.ToString());
                            UpdateAPIs(5, HdnAPISoc.Value.ToString());
                            UpdateAPIs(6, HdnAPIHeal.Value.ToString());
                        }

                        session.CommitTransaction();

                        LblBusinessSuccess.Text = "Done! ";
                        LblBusinessSuccessContent.Text = "Changes saved successfully.";
                        divBusinessSuccess.Visible = true;
                        divBusinessSuccess.Focus();
                        LoadData();
                    }
                    else
                    {
                        LblBusinessFailure.Text = "Error! ";
                        LblBusinessFailureContent.Text = "You have to select at least one category of industry, partner program and market specialisation.";
                        divBusinessFailure.Visible = true;
                        divBusinessFailure.Focus();

                        ScriptManager.RegisterStartupScript(this, GetType(), "SetIndustries1", "SetIndustries();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetPrograms1", "SetPrograms();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetMarkets1", "SetMarkets();", true);
                        ScriptManager.RegisterStartupScript(this, GetType(), "SetAPIs1", "SetAPIs();", true);

                        return;
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Default(), false);
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();

                LblBusinessFailure.Text = "Error! ";
                LblBusinessFailureContent.Text = "Something wrong happened and update was not successful, please try again or contact us.";
                divBusinessFailure.Visible = true;
                divBusinessFailure.Focus();
                LoadData();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {                
                session.CloseConnection();
            }
        }

        protected void BtnCanceBusinessSettings_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    divLogoSuccess.Visible = false;
                    divLogoFailure.Visible = false;
                    divCompanyNameError.Visible = false;
                    divCompanyUsernameError.Visible = false;
                    divCompanyEmailError.Visible = false;
                    divCompanyOfficialEmailError.Visible = false;
                    divCompanyWebsiteError.Visible = false;
                    divCompanyCountryError.Visible = false;
                    divCompanyAddressError.Visible = false;
                    divCompanyPhoneError.Visible = false;
                    divCompanyProductDemoError.Visible = false;
                    divMashapeError.Visible = false;
                    divCompanyOverviewError.Visible = false;
                    divCompanyDescriptionError.Visible = false;
                    divGeneralSuccess.Visible = false;
                    divGeneralFailure.Visible = false;
                    divCurPaswError.Visible = false;
                    divNewPaswError.Visible = false;
                    divRetNewPaswError.Visible = false;
                    divPaswSuccess.Visible = false;
                    divPaswFailure.Visible = false;
                    divBusinessSuccess.Visible = false;
                    divBusinessFailure.Visible = false;

                    LblLogoSuccess.Text = string.Empty;
                    LblLogoSuccessContent.Text = string.Empty;
                    LblLogoFailure.Text = string.Empty;
                    LblLogoFailureContent.Text = string.Empty;
                    LblCompanyNameError.Text = string.Empty;
                    LblCompanyUsernameError.Text = string.Empty;
                    LblCompanyEmailError.Text = string.Empty;
                    LblCompanyOfficialEmailError.Text = string.Empty;
                    LblCompanyWebsiteError.Text = string.Empty;
                    LblCompanyCountryError.Text = string.Empty;
                    LblCompanyAddressError.Text = string.Empty;
                    LblCompanyPhoneError.Text = string.Empty;
                    LblCompanyProductDemoError.Text = string.Empty;
                    LblMashapeError.Text = string.Empty;
                    LblCompanyOverviewError.Text = string.Empty;
                    LblCompanyDescriptionError.Text = string.Empty;
                    LblGeneralFailure.Text = string.Empty;
                    LblGeneralFailureContent.Text = string.Empty;
                    LblCurPaswError.Text = string.Empty;
                    LblNewPaswError.Text = string.Empty;
                    LblRetNewPaswError.Text = string.Empty;
                    LblPaswFailure.Text = string.Empty;
                    LblPaswFailureContent.Text = string.Empty;
                    LblPaswSuccess.Text = string.Empty;
                    LblPaswSuccessContent.Text = string.Empty;
                    LblBusinessSuccess.Text = string.Empty;
                    LblBusinessSuccessContent.Text = string.Empty;
                    LblBusinessFailure.Text = string.Empty;
                    LblBusinessFailureContent.Text = string.Empty;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"), false);
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
        }

        protected void BtnSubmitLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    divLogoFailure.Visible = false;
                    divLogoSuccess.Visible = false;

                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        LblLogoFailure.Text = "Error! ";
                        LblLogoFailureContent.Text = "You are not authorized to make any change on this account";
                        divLogoFailure.Visible = true;
                        divLogoFailure.Focus();
                        LoadData();
                        return;
                    }

                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["LogoTargetFolder"].ToString()))
                    {
                        string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"]).ToString();
                        string serverMapPathTargetFolder = Server.MapPath(logoTargetFolder);
                        int maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);
                        
                        var logo = CompanyLogo.PostedFile;
                        var logoSize = logo.ContentLength;
                        var logoType = logo.ContentType;
                        var logoName = logo.FileName;

                        if (logo != null && logo.ContentLength > 0)
                        {
                            if (logoType == "image/png" || logoType == "image/jpeg" || logoType == "image/jpg")
                            {
                                if (logoSize < maxSize)
                                {
                                    string fileExtension = UpLoadImage.GetFilenameExtension(logo.FileName);
                                    string formattedName = ImageResize.ChangeFileName(logoName, fileExtension);
                                    //if (logoType == "image/png")
                                    //{
                                    //    formattedName = ImageResize.ChangeFileName(logoName, ".png");
                                    //}
                                    //else if (logoType == "image/jpeg")
                                    //{
                                    //    formattedName = ImageResize.ChangeFileName(logoName, ".jpeg");
                                    //}
                                    //else if (logoType == "image/jpg")
                                    //{
                                    //    formattedName = ImageResize.ChangeFileName(logoName, ".jpg");
                                    //}

                                    serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\";

                                    #region Create File Directory

                                    if (!Directory.Exists(serverMapPathTargetFolder))
                                        Directory.CreateDirectory(serverMapPathTargetFolder);

                                    #endregion

                                    #region Delete old files in directory if exist

                                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                                    foreach (FileInfo fi in originaldir.GetFiles())
                                    {
                                        fi.Delete();
                                    }

                                    #endregion

                                    logo.SaveAs(serverMapPathTargetFolder + formattedName);

                                    #region Update User

                                    vSession.User.CompanyLogo = logoTargetFolder + vSession.User.GuId + "/" + formattedName;

                                    vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                                    #endregion

                                    #region Update Images

                                    ImgPhotoBckgr.ImageUrl = vSession.User.CompanyLogo;
                                    Image imgUserPhoto = (Image)ControlFinder.FindControlBackWards(Master, "ImgUserPhoto");
                                    if (imgUserPhoto != null)
                                        imgUserPhoto.ImageUrl = vSession.User.CompanyLogo;

                                    #endregion

                                    LblLogoSuccess.Text = "Done! ";
                                    LblLogoSuccessContent.Text = "Image was changed successfully.";
                                    divLogoSuccess.Visible = true;
                                    divLogoSuccess.Focus();
                                    LoadData();
                                }
                                else
                                {
                                    LblLogoFailure.Text = "Error! ";
                                    LblLogoFailureContent.Text = "Image size is too large. Max size is 100 kb.";
                                    divLogoFailure.Visible = true;
                                    divLogoFailure.Focus();
                                    LoadData();
                                    return;
                                }
                            }
                            else
                            {
                                LblLogoFailure.Text = "Error! ";
                                LblLogoFailureContent.Text = "Image type must be .png, .jpeg or .jpg";
                                divLogoFailure.Visible = true;
                                divLogoFailure.Focus();
                                LoadData();
                                return;
                            }
                        }
                        else
                        {
                            LblLogoFailure.Text = "Error! ";
                            LblLogoFailureContent.Text = "You have not selected an image or something went wrong. Try again or contact us.";
                            divLogoFailure.Visible = true;
                            divLogoFailure.Focus();
                            LoadData();
                            return;
                        }
                    }
                    else
                    {
                        LblLogoFailure.Text = "Error! ";
                        LblLogoFailureContent.Text = "Something went wrong. Please try again later or contact with us.";
                        divLogoFailure.Visible = true;
                        divLogoFailure.Focus();
                        LoadData();

                        Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to upload logo at {1}, but file could not be uploaded because 'AppSettings --> LogoTargetFolder' is missing from config.", vSession.User.Id.ToString(), DateTime.Now.ToString()), "PAGE --> DashboardEditProfile.aspx");
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

        protected void ImgBtnEdit_OnClick(object sender,EventArgs args)
        {
            try
            {
                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                Label lblMoreEmail = (Label)ControlFinder.FindControlRecursive(item, "LblMoreEmail");
                lblMoreEmail.Visible = false;
                RadTextBox rtbxEmail = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxEmail");
                rtbxEmail.Visible=true;

                ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                imgBtnEdit.Visible = false;

                ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");
                imgBtnSave.Visible = true;

                ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                imgBtnCancel.Visible = true;

                Panel divPartnerEmailError = (Panel)ControlFinder.FindControlRecursive(item, "divPartnerEmailError");
                divPartnerEmailError.Visible = false;
                Label lblPartnerEmailError = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerEmailError");
                lblPartnerEmailError.Text = string.Empty;
            }
            catch(Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                Label lblMoreEmail = (Label)ControlFinder.FindControlRecursive(item, "LblMoreEmail");
                lblMoreEmail.Visible = true;

                ElioUserEmails email = Sql.GetUserMoreEmailById(Convert.ToInt32(item["id"].Text), session);
                if (email != null)
                {
                    lblMoreEmail.Text = email.Email;
                }

                RadTextBox rtbxEmail = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxEmail");
                rtbxEmail.Visible = false;

                ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                imgBtnEdit.Visible = true;

                ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");
                imgBtnSave.Visible = false;

                ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                imgBtnCancel.Visible = false;

                Panel divPartnerEmailError = (Panel)ControlFinder.FindControlRecursive(item, "divPartnerEmailError");
                divPartnerEmailError.Visible = false;
                Label lblPartnerEmailError = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerEmailError");
                lblPartnerEmailError.Text = string.Empty;
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

        protected void ImgBtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                Panel divPartnerEmailError = (Panel)ControlFinder.FindControlRecursive(item, "divPartnerEmailError");
                divPartnerEmailError.Visible = false;
                Label lblPartnerEmailError = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerEmailError");
                lblPartnerEmailError.Text = string.Empty;

                ElioUserEmails email = Sql.GetUserMoreEmailById(Convert.ToInt32(item["id"].Text), session);
                if (email != null)
                {
                    DataLoader<ElioUserEmails> loader = new DataLoader<ElioUserEmails>(session);
                    loader.Delete(email);
                }

                RdgEmails.Rebind();
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

        protected void ImgBtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                Panel divPartnerEmailError = (Panel)ControlFinder.FindControlRecursive(item, "divPartnerEmailError");
                divPartnerEmailError.Visible = false;
                Label lblPartnerEmailError = (Label)ControlFinder.FindControlRecursive(item, "LblPartnerEmailError");
                lblPartnerEmailError.Text = string.Empty;

                RadTextBox rtbxEmail = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxEmail");

                if (rtbxEmail.Text == string.Empty)
                {
                    divPartnerEmailError.Visible = true;
                    lblPartnerEmailError.Text = "Please enter email";
                    return;
                }
                else
                {
                    if (!Validations.IsEmail(rtbxEmail.Text))
                    {
                        divPartnerEmailError.Visible = true;
                        lblPartnerEmailError.Text = "Email is invalid";
                        return;
                    }

                    if (Sql.IsEmailRegistered(rtbxEmail.Text, session))
                    {
                        divPartnerEmailError.Visible = true;
                        lblPartnerEmailError.Text = "Email is already registered";
                        return;
                    }

                    if (Sql.ExistUserEmailToOtherUser(vSession.User.Id, rtbxEmail.Text, session))
                    {
                        divPartnerEmailError.Visible = true;
                        lblPartnerEmailError.Text = "Email is already registered";
                        return;
                    }
                }

                ElioUserEmails email = Sql.GetUserMoreEmailById(Convert.ToInt32(item["id"].Text), session);
                if (email != null)
                {
                    email.Email = rtbxEmail.Text;
                    email.LastUpdate = DateTime.Now;

                    DataLoader<ElioUserEmails> loader = new DataLoader<ElioUserEmails>(session);
                    loader.Update(email);
                }

                Label lblMoreEmail = (Label)ControlFinder.FindControlRecursive(item, "LblMoreEmail");
                lblMoreEmail.Visible = true;
                lblMoreEmail.Text = email.Email;

                rtbxEmail.Visible = false;

                ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                imgBtnEdit.Visible = true;

                ImageButton imgBtnSave = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSave");
                imgBtnSave.Visible = false;

                ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");
                imgBtnCancel.Visible = false;


                divPartnerEmailError.Visible = false;

                lblPartnerEmailError.Text = string.Empty;
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

        protected void BtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                divWarningMsg.Visible = false;
                divSuccessMsg.Visible = false;
                LblWarningMsg.Text = string.Empty;
                LblWarningMsgContent.Text = string.Empty;
                LblSuccessMsg.Text = string.Empty;
                LblSuccessMsgContent.Text = string.Empty;
                TbxMoreEmails.Text=string.Empty;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseAddMoreEmailsPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnAvatarUpload_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioUsersPerson person = ClearbitSql.GetPersonByUserId(vSession.User.Id, session);
                    if (person != null)
                    {
                        divPersonAvatarSuccess.Visible = false;
                        divPersonAvatarError.Visible = false;

                        if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["PersonalImageTargetFolder"].ToString()))
                        {
                            string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["PersonalImageTargetFolder"]).ToString();
                            string serverMapPathTargetFolder = Server.MapPath(logoTargetFolder);
                            int maxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

                            var logo = avatarInput.PostedFile;
                            var logoSize = logo.ContentLength;
                            var logoType = logo.ContentType;
                            var logoName = logo.FileName;

                            if (logo != null && logo.ContentLength > 0)
                            {
                                if (logoType == "image/png" || logoType == "image/jpeg" || logoType == "image/jpg")
                                {
                                    if (logoSize < maxSize)
                                    {
                                        string fileExtension = UpLoadImage.GetFilenameExtension(logo.FileName);
                                        string formattedName = ImageResize.ChangeFileName(logoName, fileExtension);

                                        serverMapPathTargetFolder = serverMapPathTargetFolder + vSession.User.GuId + "\\";

                                        #region Create File Directory

                                        if (!Directory.Exists(serverMapPathTargetFolder))
                                            Directory.CreateDirectory(serverMapPathTargetFolder);

                                        #endregion

                                        #region Delete old files in directory if exist

                                        DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                                        foreach (FileInfo fi in originaldir.GetFiles())
                                        {
                                            fi.Delete();
                                        }

                                        #endregion

                                        logo.SaveAs(serverMapPathTargetFolder + formattedName);

                                        #region Update Person Logo

                                        person.Avatar = logoTargetFolder + vSession.User.GuId + "/" + formattedName;

                                        GlobalDBMethods.UpDatePerson(person, session);

                                        #endregion

                                        #region Update Images

                                        ImgPersonAvatarBckgrd.ImageUrl = person.Avatar;
                                        //Image imgUserPhoto = (Image)ControlFinder.FindControlBackWards(Master, "ImgUserPhoto");
                                        //if (imgUserPhoto != null)
                                        //    imgUserPhoto.ImageUrl = person.Avatar;

                                        #endregion

                                        divPersonAvatarSuccess.Visible = true;
                                        divPersonAvatarError.Visible = false;
                                        LblPersonAvatarSuccess.Text = "Done! ";
                                        LblPersonAvatarSuccessMsg.Text = "Personal image was changed successfully.";

                                        return;
                                    }
                                    else
                                    {
                                        divPersonAvatarSuccess.Visible = true;
                                        divPersonAvatarError.Visible = false;
                                        LblPersonAvatarError.Text = "Error! ";
                                        LblPersonAvatarErrorMsg.Text = "Image size is too large. Max size is " + maxSize + " bytes.";

                                        return;
                                    }
                                }
                                else
                                {
                                    divPersonAvatarSuccess.Visible = true;
                                    divPersonAvatarError.Visible = false;
                                    LblPersonAvatarError.Text = "Error! ";
                                    LblPersonAvatarErrorMsg.Text = "Image type must be .png, .jpeg or .jpg";
                                   
                                    return;
                                }
                            }
                            else
                            {
                                divPersonAvatarSuccess.Visible = true;
                                divPersonAvatarError.Visible = false;
                                LblPersonAvatarError.Text = "Error! ";
                                LblPersonAvatarErrorMsg.Text = "You have not selected an image or something went wrong. Try again or contact us.";
                                
                                return;
                            }
                        }
                        else
                        {
                            divPersonAvatarSuccess.Visible = true;
                            divPersonAvatarError.Visible = false;
                            LblPersonAvatarError.Text = "Error! ";
                            LblPersonAvatarErrorMsg.Text = "Something went wrong. Please try again later or contact with us.";

                            Logger.DetailedError(Request.Url.ToString(), string.Format("User {0} tried to upload personal image at {1}, but file could not be uploaded because 'AppSettings --> PersonalImageTargetFolder' is missing from config.", vSession.User.Id.ToString(), DateTime.Now.ToString()), "PAGE --> DashboardEditProfile.aspx");
                            return;
                        }
                    }
                    else
                    {
                        divPersonAvatarSuccess.Visible = false;
                        divPersonAvatarError.Visible = true;
                        LblPersonAvatarError.Text = "Error! ";
                        LblPersonAvatarErrorMsg.Text = "Something went wrong and your personal image could not be uploaded. Please try again later or contact with us.";

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

        protected void BtnPersonSaveGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ElioUsersPerson person = ClearbitSql.GetPersonByUserId(vSession.User.Id, session);
                    if (person != null)
                    {
                        person.FamilyName = TbxPersonLastName.Text;
                        person.GivenName = TbxPersonFirstName.Text;
                        person.Phone = TbxPersonPhone.Text;
                        person.Location = TbxPersonLocation.Text;
                        person.TimeZone = TbxPersonTimeZone.Text;
                        person.Title = TbxPersonTitle.Text;
                        person.Role = TbxPersonRole.Text;
                        person.Seniority = TbxPersonSeniority.Text;
                        person.TwitterHandle = TbxPersonTwitterHandle.Text;
                        person.AboutMeHandle = TbxPersonAboutMeHandle.Text;
                        person.Avatar = ImgPersonAvatarBckgrd.ImageUrl;
                        person.Bio = TbxPersonBio.Text;
                        person.IsPublic = (int)AccountPublicStatus.IsPublic;
                        person.IsActive = 1;

                        DataLoader<ElioUsersPerson> loader = new DataLoader<ElioUsersPerson>(session);
                        loader.Update(person);
                                                
                        ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
                        if (user != null)
                        {
                            user.PersonalImage = person.Avatar;
                            user.Position = person.Title + "," + person.Seniority;
                            user.LastName = person.FamilyName;
                            user.FirstName = person.GivenName;

                            vSession.User = GlobalDBMethods.UpDateUser(user, session);
                        }

                        IsPersonalSuccess(true);
                    }
                    else
                    {
                        IsPersonalSuccess(false);
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

        protected void BtnPersonCancelGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                ClearPersonalErrorFields();
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "edit-company-profile"));
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

        protected void RbtnSaveNotifications_Click_vs1(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (vSession.LoggedInSubAccountRoleID > 0)
                    {
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, "You are not authorized to make any change on this account!", MessageTypes.Error, true, true, false);

                        return;
                    }

                    int selectedItemsCount = 0;
                    foreach (ListItem item in CbxEmailNotifications.Items)
                    {
                        if (item.Selected)
                        {
                            //int communityEmailId = 1;
                            //bool communitySelected = false;
                            //if (Convert.ToInt32(item.Value) == 12 || Convert.ToInt32(item.Value) == 13 || Convert.ToInt32(item.Value) == 14 || Convert.ToInt32(item.Value) == 15)
                            //{
                            //    communitySelected = Sql.ExistCommunityUserEmailNotification(vSession.User.Id, communityEmailId, session);
                            //    communityEmailId++;
                            //}

                            selectedItemsCount++;
                            if (!Sql.ExistUserEmailNotificationsSettingsById(vSession.User.Id, Convert.ToInt32(item.Value), session))   // && communitySelected)
                            {
                                ElioUserEmailNotificationsSettings newNotification = new ElioUserEmailNotificationsSettings();

                                newNotification.UserId = vSession.User.Id;
                                newNotification.EmaiNotificationsId = Convert.ToInt32(item.Value);

                                DataLoader<ElioUserEmailNotificationsSettings> loader = new DataLoader<ElioUserEmailNotificationsSettings>(session);
                                loader.Insert(newNotification);
                            }
                        }
                        else
                        {
                            Sql.DeleteUserEmailNotificationSettings(Convert.ToInt32(item.Value), vSession.User.Id, session);
                        }
                    }

                    LoadEmailsInfoText();
                    //LoadEmailSettings();

                    if (selectedItemsCount > 0)
                    {
                        string alert = "Your account settings were successfully updated";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Success, true, true, false);
                    }
                    else
                    {
                        string alert = "No items have been selected";
                        GlobalMethods.ShowMessageControlDA(MessageControlEmailNotifications, alert, MessageTypes.Info, true, true, false);
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

        protected void BtnDeleteAccountData_Click(object sender, EventArgs e)
        {
            try
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfDeleteUserPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnBack_Click(object sender, EventArgs e)
        {
            UcMessageAlert.Visible = false;

            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);
        }

        protected void BtnDeleteConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();
                //session.BeginTransaction();

                if (vSession.User != null)
                {
                    if (!Sql.IsUserDeleted(vSession.User.Id, session))
                    {
                        try
                        {
                            EmailSenderLib.ContactElioplus(vSession.User.CompanyName, vSession.User.Email, "Delete Company Account Request", "I want to delete all of my company data and close my account. Please do that for me.", vSession.User.Phone, vSession.Lang, session);
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        vSession.User = Sql.GetUserById(vSession.User.Id, session);
                        if (vSession.User != null)
                        {
                            vSession.User.AccountStatus = (int)AccountStatus.Deleting;
                            vSession.User.LastUpdated = DateTime.Now;

                            DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                            loader.Update(vSession.User);

                            BtnDeleteAccountData.Enabled = false;
                        }

                        #region Delete User

                        //ElioUsersDeleted deletedUser = new ElioUsersDeleted();

                        //deletedUser.UserId = vSession.User.Id;
                        //deletedUser.Username = vSession.User.Username;
                        //deletedUser.UsernameEncrypted = vSession.User.UsernameEncrypted;
                        //deletedUser.Password = vSession.User.Password;
                        //deletedUser.PasswordEncrypted = vSession.User.PasswordEncrypted;
                        //deletedUser.SysDate = vSession.User.SysDate;
                        //deletedUser.LastUpdated = vSession.User.LastUpdated;
                        //deletedUser.LastLogin = vSession.User.LastLogin;
                        //deletedUser.UserLoginCount = vSession.User.UserLoginCount;
                        //deletedUser.Ip = vSession.User.Ip;
                        //deletedUser.Phone = vSession.User.Phone;
                        //deletedUser.Address = vSession.User.Address;
                        //deletedUser.Country = vSession.User.Country;
                        //deletedUser.WebSite = vSession.User.WebSite;
                        //deletedUser.AccountStatus = vSession.User.AccountStatus;
                        //deletedUser.Overview = vSession.User.Overview;
                        //deletedUser.Description = vSession.User.Description;
                        //deletedUser.CompanyLogo = vSession.User.CompanyLogo;
                        //deletedUser.CompanyName = vSession.User.CompanyName;
                        //deletedUser.CompanyType = vSession.User.CompanyType;
                        //deletedUser.OfficialEmail = vSession.User.OfficialEmail;
                        //deletedUser.FeaturesNo = vSession.User.FeaturesNo;
                        //deletedUser.MashapeUsername = vSession.User.MashapeUsername;
                        //deletedUser.LastName = vSession.User.LastName;
                        //deletedUser.FirstName = vSession.User.FirstName;
                        //deletedUser.PersonalImage = vSession.User.PersonalImage;
                        //deletedUser.Position = vSession.User.Position;
                        //deletedUser.Email = vSession.User.Email;
                        //deletedUser.GuId = vSession.User.GuId;
                        //deletedUser.IsPublic = vSession.User.IsPublic;
                        //deletedUser.CustomerStripeId = vSession.User.CustomerStripeId;
                        //deletedUser.VendorProductDemoLink = vSession.User.VendorProductDemoLink;
                        //deletedUser.CommunityStatus = vSession.User.CommunityStatus;
                        //deletedUser.CommunityProfileCreated = vSession.User.CommunityProfileCreated;
                        //deletedUser.CommunityProfileLastUpdated = vSession.User.CommunityProfileLastUpdated;
                        //deletedUser.CommunitySummaryText = vSession.User.CommunitySummaryText;
                        //deletedUser.LinkedInUrl = vSession.User.LinkedInUrl;
                        //deletedUser.LinkedInUrl = vSession.User.LinkedinId;
                        //deletedUser.TwitterUrl = vSession.User.TwitterUrl;
                        //deletedUser.HasBillingDetails = vSession.User.HasBillingDetails;
                        //deletedUser.BillingType = vSession.User.BillingType;
                        //deletedUser.UserApplicationType = vSession.User.UserApplicationType;
                        //deletedUser.DateDeleted = DateTime.Now;
                        //deletedUser.ActionByUserId = vSession.User.Id;

                        //GlobalDBMethods.InsertDeletedUser(deletedUser, session);

                        //vSession.User.Email = "deleted_" + vSession.User.Email;
                        //vSession.User.IsPublic = (int)AccountPublicStatus.IsNotPublic;
                        //GlobalDBMethods.UpDateUser(vSession.User, session);
                        ////GlobalDBMethods.DeleteUser(user, session);

                        //session.CommitTransaction();

                        #endregion

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);

                        string alert = "Your request was send successfully.";

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfDeleteUserPopUp();", true);

                        string alert = "We have already your request about closing your account.";

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                //session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    vSession.ViewStateDataStoreDS = new DataSet();
                    DataTable usrTbl = Sql.GetUserByIdAsDataTable(vSession.User.Id, session);
                    if (usrTbl.Rows.Count > 0)
                    {
                        usrTbl.TableName = "User Table";
                        vSession.ViewStateDataStoreDS.Tables.Add(usrTbl);

                        if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString() && vSession.User.IsPublic == (int)AccountPublicStatus.IsPublic && vSession.User.AccountStatus == (int)AccountStatus.Completed)
                        {
                            DataTable personTbl = Sql.GetPersonByUserIdAsDataTable(vSession.User.Id, session);
                            if (personTbl.Rows.Count > 0)
                            {
                                personTbl.TableName = "Person Table";
                                vSession.ViewStateDataStoreDS.Tables.Add(personTbl);
                            }

                            DataTable companyTbl = Sql.GetPersonCompanyByUserIdAsDataTable(vSession.User.Id, session);
                            if (companyTbl.Rows.Count > 0)
                            {
                                companyTbl.TableName = "Company Table";
                                vSession.ViewStateDataStoreDS.Tables.Add(companyTbl);
                            }
                        }

                        DataTable partnerTbl = Sql.GetUsersPartnersAsDataTable(vSession.User.Id, session);
                        if (partnerTbl.Rows.Count > 0)
                        {
                            partnerTbl.TableName = "Partner Program Table";
                            vSession.ViewStateDataStoreDS.Tables.Add(partnerTbl);
                        }

                        DataTable apiTbl = Sql.GetUsersApiesAsDataTable(vSession.User.Id, session);
                        if (apiTbl.Rows.Count > 0)
                        {
                            apiTbl.TableName = "Api Table";
                            vSession.ViewStateDataStoreDS.Tables.Add(apiTbl);
                        }

                        DataTable industryTbl = Sql.GetUsersIndustriesAsDataTable(vSession.User.Id, session);
                        if (industryTbl.Rows.Count > 0)
                        {
                            industryTbl.TableName = "Industry Table";
                            vSession.ViewStateDataStoreDS.Tables.Add(industryTbl);
                        }

                        DataTable marketsTbl = Sql.GetUsersMarketsAsDataTable(vSession.User.Id, session);
                        if (marketsTbl.Rows.Count > 0)
                        {
                            marketsTbl.TableName = "Markets Table";
                            vSession.ViewStateDataStoreDS.Tables.Add(marketsTbl);
                        }

                        if (vSession.ViewStateDataStoreDS != null)
                            Response.Redirect("download-csv?case=MyCompanyData", false);
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

        protected void ImgBtnExport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                if (vSession.ViewStateDataStore != null)
                    Response.Redirect("download-csv?case=MyCompanyData", false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnInvoiceExport_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LinkButton LnkBtn = (LinkButton)sender;
                GridDataItem item = (GridDataItem)LnkBtn.NamingContainer;

                int userId = Convert.ToInt32(item["userId"].ToString());

                if (vSession.ViewStateDataStore != null)
                    Response.Redirect("download-invoices?case=StripeInvoices&userID=" + userId.ToString(), false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                session.OpenConnection();

                divProductsFailure.Visible = divProductsSuccess.Visible = false;
                ListItem item = new ListItem();
                string itemDescription = "";

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    if (RcbxIntegrations.Text  == "")
                    {
                        divProductsFailure.Visible = true;
                        LblProductsFailureContent.Text = "Please type/select integration name and then press Add button!";

                        return;
                    }

                    if (RcbxIntegrations.Text != "")
                        itemDescription = RcbxIntegrations.Text.TrimStart(' ').TrimEnd(' ').TrimEnd(',');
                }
                else
                {
                    if (RcbxProducts.Text == "")
                    {
                        divProductsFailure.Visible = true;
                        LblProductsFailureContent.Text = "Please type/select product name and then press Add button!";

                        return;
                    }

                    if (RcbxProducts.Text != "")
                        itemDescription = RcbxProducts.Text.TrimStart(' ').TrimEnd(' ').TrimEnd(',');
                }

                if (itemDescription != "" && Validations.ContainsSpecialCharForRegProducts(itemDescription))
                {
                    divProductsFailure.Visible = true;
                    LblProductsFailureContent.Text = "This product contains invalid characters. Please type another";

                    return;
                }

                bool exist = ExistItemToList(CbxUserProductsIntegrationsList, itemDescription, 0);
                if (!exist)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        ElioRegistrationIntegrations integration = Sql.GetRegistrationIntegrationsIDByDescription(itemDescription, session);
                        if (integration == null)
                        {
                            integration = new ElioRegistrationIntegrations();
                            integration.Description = itemDescription;
                            integration.IsPublic = 1;
                            integration.Sysdate = DateTime.Now;

                            DataLoader<ElioRegistrationIntegrations> insertLoader = new DataLoader<ElioRegistrationIntegrations>(session);
                            insertLoader.Insert(integration);
                        }

                        if (integration != null)
                        {
                            item.Value = integration.Id.ToString();
                            item.Text = (!string.IsNullOrEmpty(integration.Description)) ? integration.Description : itemDescription;

                            item.Selected = true;
                        }
                    }
                    else
                    {
                        ElioRegistrationProducts product = Sql.GetRegistrationProductsIDByDescription(itemDescription, session);
                        if (product == null)
                        {
                            product = new ElioRegistrationProducts();
                            product.Description = itemDescription;
                            product.IsPublic = 1;
                            product.Sysdate = DateTime.Now;

                            DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                            insertLoader.Insert(product);
                        }

                        if (product != null)
                        {
                            item.Value = product.Id.ToString();
                            item.Text = (!string.IsNullOrEmpty(product.Description)) ? product.Description : itemDescription;

                            item.Selected = true;
                        }
                    }

                    CbxUserProductsIntegrationsList.Items.Add(item);

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                        RcbxIntegrations.Entries.Clear();
                    else
                        RcbxProducts.Entries.Clear();
                }
                else
                {
                    divProductsFailure.Visible = true;
                    LblProductsFailureContent.Text = "This product already exists in your list. Please select another";

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

        protected void BtnSubmitProducts_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divProductsSuccess.Visible = false;
                divProductsFailure.Visible = false;

                int itemsInsertedCount = 0;
                int itemsRemovedCount = 0;
                List<ListItem> itemsToRemove = new List<ListItem>();

                #region Integrations/Products

                foreach (ListItem item in CbxUserProductsIntegrationsList.Items)
                {
                    string itemDescription = item.Text;
                    if (item.Text != "")
                    {
                        if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                        {
                            if (Convert.ToInt32(item.Value) > 0)
                            {
                                bool exist = Sql.ExistUserRegistrationProduct(vSession.User.Id, Convert.ToInt32(item.Value), session);

                                if (item.Selected)
                                {
                                    if (!exist)
                                    {
                                        ElioUsersRegistrationProducts userProducts = new ElioUsersRegistrationProducts();
                                        userProducts.UserId = vSession.User.Id;
                                        userProducts.RegProductsId = Convert.ToInt32(item.Value);

                                        DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);
                                        loader.Insert(userProducts);

                                        itemsInsertedCount++;
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //delete
                                        session.ExecuteQuery(@"DELETE FROM Elio_users_registration_products
                                                                WHERE user_id = @user_id
                                                                AND reg_products_id = @reg_products_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                        , DatabaseHelper.CreateIntParameter("@reg_products_id", Convert.ToInt32(item.Value)));

                                        itemsToRemove.Add(item);

                                        itemsRemovedCount++;
                                    }
                                }
                            }
                            else
                            {
                                divProductsFailure.Visible = true;
                                LblProductsFailureContent.Text = "Product could not be saved! Please try again later or contact us.";
                                return;
                            }
                        }
                        else
                        {
                            if (Convert.ToInt32(item.Value) > 0)
                            {
                                bool exist = Sql.ExistUserRegistrationIntegration(vSession.User.Id, Convert.ToInt32(item.Value), session);

                                if (item.Selected)
                                {
                                    if (!exist)
                                    {
                                        ElioUsersRegistrationIntegrations userIntegrations = new ElioUsersRegistrationIntegrations();
                                        userIntegrations.UserId = vSession.User.Id;
                                        userIntegrations.RegIntegrationsId = Convert.ToInt32(item.Value);

                                        DataLoader<ElioUsersRegistrationIntegrations> loader = new DataLoader<ElioUsersRegistrationIntegrations>(session);
                                        loader.Insert(userIntegrations);

                                        itemsInsertedCount++;
                                    }
                                }
                                else
                                {
                                    if (exist)
                                    {
                                        //delete
                                        session.ExecuteQuery(@"DELETE FROM Elio_users_registration_integrations
                                                                WHERE user_id = @user_id
                                                                AND reg_integrations_id = @reg_integrations_id"
                                                        , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                        , DatabaseHelper.CreateIntParameter("@reg_integrations_id", Convert.ToInt32(item.Value)));

                                        itemsToRemove.Add(item);

                                        itemsRemovedCount++;
                                    }
                                }
                            }
                            else
                            {
                                divProductsFailure.Visible = true;
                                LblProductsFailureContent.Text = "Integration could not be saved! Please try again later or contact us.";
                                return;
                            }
                        }
                    }
                }

                if (itemsToRemove.Count > 0)
                {
                    for (int i = 0; i < itemsToRemove.Count; i++)
                    {
                        CbxUserProductsIntegrationsList.Items.Remove(itemsToRemove[i]);
                    }
                }

                #endregion

                divProductsSuccess.Visible = true;
                LblProductsSuccess.Text = "Done! ";
                LblProductsSuccessContent.Text = "Your changes were saved successfully!";
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

        protected void BtnCancelSubmitProducts_OnClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divProductsSuccess.Visible = false;
                divProductsFailure.Visible = false;

                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers))
                    LoadProducts();
                else
                    LoadIntegrations();
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

        protected void RdgEmails_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    imgBtnEdit.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;

                    ImageButton imgBtnDelete = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnDelete");
                    imgBtnDelete.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "1")).Text;

                    Label lblMoreEmail = (Label)ControlFinder.FindControlRecursive(item, "LblMoreEmail");
                    lblMoreEmail.Text = item["email"].Text;

                    RadTextBox rtbxEmail = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxEmail");
                    rtbxEmail.Text = item["email"].Text;
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

        protected void RdgEmails_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    List<ElioUserEmails> emails = Sql.GetUserMoreEmails(vSession.User.Id, session);

                    if (emails.Count > 0)
                    {
                        RdgEmails.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("email");

                        foreach (ElioUserEmails email in emails)
                        {
                            table.Rows.Add(email.Id, email.Email);
                        }

                        RdgEmails.DataSource = table;
                    }
                    else
                    {
                        RdgEmails.Visible = false;
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
    }
}