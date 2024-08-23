using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.IO;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class CompanyDataEditMode : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UpdateStrings();
                
                if (vSession.User != null)
                {
                    FixPage();

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        PhCompanyCategoriesData.Controls.Clear();

                        ControlLoader.LoadElioControls(this, PhCompanyCategoriesData, ControlLoader.CompanyCategoriesData);
                    }
                }
                else
                {
                    #region Redirect To Home

                    Response.Redirect(ControlLoader.Default(), false);

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

        #region Methods

        private void FixPage()
        {
            UcPasswordMessageAlert.Visible = false;
            UcNewPasswordMessageAlert.Visible = false;

            if (!vSession.HasChangeSelectedCountry)
            {
                LoadCountries();

                LoadCompanyData();
            }            
        }

        private void LoadCountries()
        {
            RcbxCountries.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "20")).Text;
            item.ToolTip = "0";

            RcbxCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new RadComboBoxItem();

                item.Value = country.Id.ToString();
                item.Text = country.CountryName;
                item.ToolTip = country.Prefix.ToString();

                RcbxCountries.Items.Add(item);
            }

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                RcbxCountries.FindItemByText(vSession.User.Country).Selected = true;
                RtbxPhoneNumberPrefix.Text = RcbxCountries.SelectedItem.ToolTip;
            }
        }

        private void LoadCompanyData()
        {
            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            if (packet != null)
            {
                RtbxUserBillingType.Text = packet.PackDescription + " packet user";
            }

            RtbxUsername.Text = vSession.User.Username;
            RtbxEmail.Text = vSession.User.Email;

            if (!string.IsNullOrEmpty(vSession.User.LinkedinId))
            {
                if (vSession.User.LinkedinId == vSession.User.Password)
                {
                    RtbxPassword.Text = vSession.User.Password;
                    RtbxPassword.TextMode = InputMode.SingleLine;
                }
                else
                {
                    RtbxPassword.Text = string.Empty;
                    RtbxPassword.TextMode = InputMode.Password;
                }
            }

            PnlRegisteredUserInfo.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
            
            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                RtbxOfficialEmail.Text = vSession.User.OfficialEmail;
                RtbxCompanyName.Text = vSession.User.CompanyName;
                RtbxWebSite.Text = vSession.User.WebSite;
                RcbxCountries.FindItemByText(vSession.User.Country).Selected = true;                
                RtbxAddress.Text = vSession.User.Address;
                RtbxPhoneNumberPrefix.Text = "+ " + RcbxCountries.SelectedItem.ToolTip;
                int prefixLength = RcbxCountries.SelectedItem.ToolTip.Length;
                RtbxPhone.Text = (!string.IsNullOrEmpty(vSession.User.Phone)) ? vSession.User.Phone.Substring(prefixLength) : "";

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    LblmashapeLink.Text = "https://www.mashape.com/";
                    RtbxMashapeUsername.Text = (!string.IsNullOrEmpty(vSession.User.MashapeUsername)) ? vSession.User.MashapeUsername : "";
                }
                else
                {
                    LblmashapeText.Visible = false;
                    LblmashapeLink.Visible = false;
                    RtbxMashapeUsername.Visible = false;
                }

                RtbxOverView.Text = vSession.User.Overview;
                RtbxDescription.Text = vSession.User.Description;
            }

            #region To delete

            #region Industry

            //PnlIndustry.Visible = (vSession.User.CompanyType == Types.Developers.ToString()) ? false : true;
            //if (PnlIndustry.Visible)
            //{
            //    cb1.Items.Clear();
            //    cb2.Items.Clear();
            //    cb3.Items.Clear();

            //    int count = 0;
            //    List<ElioIndustries> allIndustries = Sql.GetIndustries(session);
            //    foreach (ElioIndustries industry in allIndustries)
            //    {
            //        ListItem item = new ListItem();

            //        item.Selected = Sql.ExistUserIndustry(vSession.User.Id, industry.Id, session);
            //        //item.Enabled = (item.Selected) ? false : true;
            //        if (count <= 4)
            //        {
            //            item.Text = industry.IndustryDescription;
            //            item.Value = industry.Id.ToString();

            //            cb1.Items.Add(item);
            //            count++;
            //        }
            //        else if (count > 4 && count <= 8)
            //        {
            //            item.Text = industry.IndustryDescription;
            //            item.Value = industry.Id.ToString();

            //            cb2.Items.Add(item);
            //            count++;
            //        }
            //        else
            //        {
            //            item.Text = industry.IndustryDescription;
            //            item.Value = industry.Id.ToString();

            //            cb3.Items.Add(item);
            //            count++;
            //        }
            //    }
            //}

            #endregion

            #region Partner

            //PnlPartner.Visible = (vSession.User.CompanyType == Types.Developers.ToString()) ? false : true;
            //if (PnlPartner.Visible)
            //{
            //    cb4.Items.Clear();
            //    cb5.Items.Clear();
            //    cb6.Items.Clear();

            //    int count = 0;
            //    List<ElioPartners> allPartners = Sql.GetPartners(session);
            //    foreach (ElioPartners partner in allPartners)
            //    {
            //        ListItem item = new ListItem();

            //        item.Selected = Sql.ExistUserPartner(vSession.User.Id, partner.Id, session);
            //        //item.Enabled = (item.Selected) ? false : true;
            //        if (count <= 1)
            //        {
            //            item.Text = partner.PartnerDescription;
            //            item.Value = partner.Id.ToString();

            //            cb4.Items.Add(item);
            //            count++;
            //        }
            //        else if (count > 1 && count <= 3)
            //        {
            //            item.Text = partner.PartnerDescription;
            //            item.Value = partner.Id.ToString();

            //            cb5.Items.Add(item);
            //            count++;
            //        }
            //        else
            //        {
            //            item.Text = partner.PartnerDescription;
            //            item.Value = partner.Id.ToString();

            //            cb6.Items.Add(item);
            //            count++;
            //        }
            //    }
            //}

            #endregion

            #region Market

            //PnlMarket.Visible = (vSession.User.CompanyType == Types.Developers.ToString()) ? false : true;
            //if (PnlMarket.Visible)
            //{
            //    cb7.Items.Clear();
            //    cb8.Items.Clear();

            //    int count = 0;
            //    List<ElioMarkets> allMarkets = Sql.GetMarkets(session);
            //    foreach (ElioMarkets market in allMarkets)
            //    {
            //        ListItem item = new ListItem();

            //        item.Selected = Sql.ExistUserMarket(vSession.User.Id, market.Id, session);
            //        //item.Enabled = (item.Selected) ? false : true;
            //        if (count <= 1)
            //        {
            //            item.Text = market.MarketDescription;
            //            item.Value = market.Id.ToString();

            //            cb7.Items.Add(item);
            //            count++;
            //        }
            //        else
            //        {
            //            item.Text = market.MarketDescription;
            //            item.Value = market.Id.ToString();

            //            cb8.Items.Add(item);
            //            count++;
            //        }
            //    }
            //}

            #endregion

            #region Api

            //PnlApi.Visible = (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString()) ? false : true;
            //if (PnlApi.Visible)
            //{
            //    cb9.Items.Clear();
            //    cb10.Items.Clear();
            //    cb11.Items.Clear();

            //    int count = 0;
            //    List<ElioApies> allApies = Sql.GetApies(session);
            //    foreach (ElioApies api in allApies)
            //    {
            //        ListItem item = new ListItem();

            //        item.Selected = Sql.ExistUserApi(vSession.User.Id, api.Id, session);
            //        //item.Enabled = (item.Selected) ? false : true;
            //        if (count <= 1)
            //        {
            //            item.Text = api.ApiDescription;
            //            item.Value = api.Id.ToString();

            //            cb9.Items.Add(item);
            //            count++;
            //        }
            //        else if (count > 1 && count <= 3)
            //        {
            //            item.Text = api.ApiDescription;
            //            item.Value = api.Id.ToString();

            //            cb10.Items.Add(item);
            //            count++;
            //        }
            //        else
            //        {
            //            item.Text = api.ApiDescription;
            //            item.Value = api.Id.ToString();

            //            cb11.Items.Add(item);
            //            count++;
            //        }
            //    }
            //}

            #endregion

            #endregion
        }

        private void UpdateStrings()
        {
            LblTitle.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "16")).Text;
            LblUserBillingType.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "57")).Text;
            LblUsername.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "18")).Text;
            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "30")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "31")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "32")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "19")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "20")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "21")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "23")).Text;
            Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "24")).Text;
            Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "25")).Text;
            LblmashapeText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "26")).Text;
            LblOverView.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "27")).Text;
            LblDescription.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "28")).Text;

            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;

            Label lblCancelText = (Label)ControlFinder.FindControlRecursive(RbtnCancel, "LblCancelText");
            lblCancelText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "7")).Text;

            Label lblChangePasswordText = (Label)ControlFinder.FindControlRecursive(RbtnChangePassword, "LblChangePasswordText");
            lblChangePasswordText.Text = (RbtnCancelChangePassword.Visible) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "9")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "8")).Text;

            Label lblCancelChangePasswordText = (Label)ControlFinder.FindControlRecursive(RbtnCancelChangePassword, "LblCancelChangePasswordText");
            lblCancelChangePasswordText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "7")).Text;
        }

        private bool DataHasError()
        {
            if (string.IsNullOrEmpty(RtbxUsername.Text))
            {
                LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "1")).Text;
                return true;
            }
            else
            {
                if (RtbxUsername.Text.Length < 8)
                {
                    LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "2")).Text;
                    return true;
                }

                if (!Validations.IsUsernameCharsValid(RtbxUsername.Text))
                {
                    LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "3")).Text;
                    return true;
                }

                if (Sql.ExistUsernameToOtherUser(RtbxUsername.Text, vSession.User.Id, session))
                {
                    LblUsernameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "14")).Text;
                    return true;
                }
            }

            if (string.IsNullOrEmpty(RtbxEmail.Text))
            {
                LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "10")).Text;
                return true;
            }
            else
            {
                if (!Validations.IsEmail(RtbxEmail.Text))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "11")).Text;
                    return true;
                }

                if (Sql.ExistEmailToOtherUser(RtbxEmail.Text, vSession.User.Id, session))
                {
                    LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "12")).Text;
                    return true;
                }
            }

             if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
             {
                 if (string.IsNullOrEmpty(RtbxCompanyName.Text))
                 {
                     LblCompanyNameError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "2")).Text;
                     return true;
                 }

                 if (!string.IsNullOrEmpty(RtbxOfficialEmail.Text))
                 {
                     if (!Validations.IsEmail(RtbxOfficialEmail.Text))
                     {
                         LblOfficialEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "5")).Text;
                         return true;
                     }

                     if (Sql.ExistEmailToOtherUser(RtbxOfficialEmail.Text, vSession.User.Id, session))
                     {
                         LblOfficialEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "12")).Text;
                         return true;
                     }
                 }

                 if (string.IsNullOrEmpty(RtbxWebSite.Text))
                 {
                     LblWeSiteError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "6")).Text;
                     return true;
                 }
                 if (RcbxCountries.SelectedItem.Value == "0")
                 {
                     LblCountryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "7")).Text;
                     return true;
                 }
                 if (string.IsNullOrEmpty(RtbxAddress.Text))
                 {
                     LblAddressError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "8")).Text;
                     return true;
                 }

                 //if (string.IsNullOrEmpty(RtbxPhone.Text))
                 //{
                 //    LblPhoneError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "9")).Text;
                 //    return true;
                 //}
                 if (!string.IsNullOrEmpty(RtbxPhone.Text))
                 {
                     if (!Validations.IsNumber(RtbxPhone.Text))
                     {
                         LblPhoneError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "10")).Text;
                         return true;
                     }
                 }

                 if (string.IsNullOrEmpty(RtbxOverView.Text))
                 {
                     LblOverViewError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "11")).Text;
                     return true;
                 }

                 if (string.IsNullOrEmpty(RtbxDescription.Text))
                 {
                     LblDescriptionError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "label", "12")).Text;
                     return true;
                 }

             }

            if (PnlNewPassword.Visible)
            {
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "message", "1")).Text;
                GlobalMethods.ShowMessageControl(UcPasswordMessageAlert, alert, MessageTypes.Error, true, true, false);
                return true;
            }

            return false;
        }

        #endregion

        #region Buttons

        protected void RbtnChangePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LblPasswordError.Text = string.Empty;
                LblNewPasswordError.Text = string.Empty;
                LblRetypePasswordError.Text = string.Empty;
                UcNewPasswordMessageAlert.Visible = false;

                Label lblChangePasswordText = (Label)ControlFinder.FindControlRecursive(RbtnChangePassword, "LblChangePasswordText");
                bool isEditMode = (lblChangePasswordText.Text == Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "8")).Text) ? true : false;

                if (!isEditMode)
                {
                    if (string.IsNullOrEmpty(RtbxPassword.Text))
                    {
                        LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "5")).Text;
                        return;
                    }
                    else
                    {
                        if (RtbxPassword.Text != vSession.User.Password)
                        {
                            LblPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "15")).Text;
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(RtbxNewPassword.Text))
                    {
                        LblNewPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "16")).Text;
                        return;
                    }
                    else
                    {                        
                        if (RtbxNewPassword.Text.Length < 8)
                        {
                            LblNewPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "17")).Text;
                            return;
                        }

                        if (!Validations.IsPasswordCharsValid(RtbxNewPassword.Text))
                        {
                            LblNewPasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "18")).Text;
                            return;
                        }
                    }

                    if (string.IsNullOrEmpty(RtbxRetypePassword.Text))
                    {
                        LblRetypePasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "20")).Text;
                        return;
                    }
                    else
                    {
                        if (RtbxNewPassword.Text != RtbxRetypePassword.Text)
                        {
                            LblRetypePasswordError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "simpleregistration", "message", "19")).Text;
                            return;
                        }
                    }

                    vSession.User.Password = RtbxNewPassword.Text;
                    vSession.User.PasswordEncrypted = MD5.Encrypt(vSession.User.Password);
                    vSession.User.LastUpdated = DateTime.Now;

                    vSession.User = GlobalDBMethods.UpDateUser(vSession.User, session);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "message", "2")).Text;
                    GlobalMethods.ShowMessageControl(UcNewPasswordMessageAlert, alert, MessageTypes.Success, true, true, false);                    
                }

                PnlNewPassword.Visible = isEditMode;
                RbtnCancelChangePassword.Visible = isEditMode;
                lblChangePasswordText.Text = (isEditMode) ? Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "9")).Text : Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "8")).Text;
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

        protected void RbtnCancelChangePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                Label lblChangePasswordText = (Label)ControlFinder.FindControlRecursive(RbtnChangePassword, "LblChangePasswordText");

                RbtnCancelChangePassword.Visible = false;
                PnlNewPassword.Visible = false;
                lblChangePasswordText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "8")).Text;

                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companydataeditmode", "message", "3")).Text;
                GlobalMethods.ShowMessageControl(UcNewPasswordMessageAlert, alert, MessageTypes.Warning, true, true, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcPasswordMessageAlert.Visible = false;

                if (DataHasError()) return;

                if (vSession.User != null)
                {
                    try
                    {
                        session.BeginTransaction();

                        vSession.User.Username = RtbxUsername.Text;
                        vSession.User.UsernameEncrypted = MD5.Encrypt(vSession.User.Username);
                        vSession.User.Email = RtbxEmail.Text;

                        if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                        {
                            vSession.User.OfficialEmail = RtbxOfficialEmail.Text;
                            vSession.User.CompanyName = RtbxCompanyName.Text;
                            vSession.User.WebSite = (RtbxWebSite.Text.StartsWith("http://") || RtbxWebSite.Text.StartsWith("https://")) ? RtbxWebSite.Text : "https://" + RtbxWebSite.Text;
                            vSession.User.Country = RcbxCountries.SelectedItem.Text;
                            vSession.User.Address = RtbxAddress.Text;
                            vSession.User.Phone = RtbxPhoneNumberPrefix.Text.Replace("+ ", "") + RtbxPhone.Text.Replace(" ", string.Empty).Replace("-", string.Empty).Trim();
                            vSession.User.MashapeUsername = RtbxMashapeUsername.Text;

                            vSession.User.Overview = string.Empty;
                            vSession.User.Description = string.Empty;

                            vSession.User.Overview = GlobalMethods.FixStringEntryToParagraphs(RtbxOverView.Text);
                            vSession.User.Description = GlobalMethods.FixStringEntryToParagraphs(RtbxDescription.Text);
                        }

                        vSession.User.LastUpdated = DateTime.Now;

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                        loader.Update(vSession.User);

                        session.CommitTransaction();

                        Label lblUsernameValue = (Label)ControlFinder.FindControlBackWards(this, "LblUsernameValue");
                        lblUsernameValue.Text = vSession.User.Username;

                        PlaceHolder phCompanydata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhCompanydata");
                        phCompanydata.Controls.Clear();

                        vSession.LoadedDashboardCompanyEditControl = ControlLoader.CompanyDataViewMode;
                        ControlLoader.LoadElioControls(this, phCompanydata, vSession.LoadedDashboardCompanyEditControl);

                        GlobalMethods.ClearCriteriaSession(vSession, false);
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

        protected void RbtnCancel_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                {
                    #region Delete Uploaded Images

                    DirectoryInfo originaldir = new DirectoryInfo(Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString()) + vSession.User.GuId + "\\");

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion
                }

                GlobalMethods.ClearCriteriaSession(vSession, false);

                PlaceHolder phCompanydata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhCompanydata");
                phCompanydata.Controls.Clear();

                vSession.LoadedDashboardCompanyEditControl = ControlLoader.CompanyDataViewMode;
                ControlLoader.LoadElioControls(this, phCompanydata, vSession.LoadedDashboardCompanyEditControl);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RcbxCountries_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                RtbxPhoneNumberPrefix.Text = "+ " + RcbxCountries.SelectedItem.ToolTip;

                vSession.HasChangeSelectedCountry = true;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Upload Logo

        protected void Logo_OnFileUploaded(object sender, FileUploadedEventArgs e)
        {
            try
            {
                session.OpenConnection();

                vSession.SuccessfullFileUpload = false;

                if (vSession.User != null)
                {
                    vSession.SuccessfullFileUpload = UpLoadImage.UpLoadCompanyLogo(vSession.User, Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"].ToString()), e, session);

                    #region To delete

                    //if (e.File.ContentLength < maxFileLenght)
                    //{
                    //    if (e.File.GetExtension().ToLower() == ".jpg" || e.File.GetExtension().ToLower() == ".jpeg" || e.File.GetExtension().ToLower() == ".png" || e.File.GetExtension().ToLower() == ".gif")
                    //    {
                    //        string newname = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                            #region Create Logo Directory

                    //        Logo.TargetFolder = "~/Images/Logos/" + vSession.User.GuId + "/";

                    //        if (!Directory.Exists(Server.MapPath(Logo.TargetFolder)))
                    //            Directory.CreateDirectory(Server.MapPath(Logo.TargetFolder));

                    //        vSession.SuccessfullFileUpload = false;

                            #endregion

                            #region Delete old files in directory if exist

                    //        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(Logo.TargetFolder));

                    //        foreach (FileInfo fi in dir.GetFiles())
                    //        {
                    //            fi.Delete();
                    //        }

                            #endregion

                    //        e.File.SaveAs(Server.MapPath(Logo.TargetFolder) + newname);

                            #region Update User

                    //        vSession.User.CompanyLogo = Logo.TargetFolder + newname;

                    //        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    //        loader.Update(vSession.User);

                            #endregion

                    //        vSession.SuccessfullFileUpload = true;
                    //    }
                    //    else
                    //    {
                    //        typeOfUploadError = "Type of file in not correct";
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    typeOfUploadError = "File size is more than 100000 Kb";
                    //    return;
                    //}

                    #endregion
                }
                else
                {
                    Response.Redirect(vSession.Page, false);
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