using System;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using System.Text.RegularExpressions;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class CompanyDataViewMode : System.Web.UI.UserControl
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
                    LoadCompanyData();

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
                    {
                        PhCompanyCategoriesData.Controls.Clear();

                        ControlLoader.LoadElioControls(this, PhCompanyCategoriesData, ControlLoader.CompanyCategoriesData);
                    }
                }
                else
                {
                    #region Redirect To Home

                    Response.Redirect(vSession.Page = ControlLoader.Default(), false);

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

        private void LoadCompanyData()
        {
            ImgLogo.ImageUrl = vSession.User.CompanyLogo;
            ImgLogo.AlternateText = vSession.User.CompanyName;
            ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
            if (packet != null)
            {
                RtbxUserBillingType.Text = packet.PackDescription + " packet user";
            }

            //RtbxUserBillingType.Text = (vSession.User.BillingType == Convert.ToInt32(BillingType.Premium)) ? BillingType.Premium.ToString() + " user" : BillingType.Freemium.ToString() + " user";
            RtbxUsername.Text = vSession.User.Username;
            RtbxEmail.Text = vSession.User.Email;

            PnlRegisteredUserInfo.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
            PnlLogo.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
            RbtnRegister.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? false : true;

            if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                RtbxOfficialEmail.Text = (!string.IsNullOrEmpty(vSession.User.OfficialEmail)) ? vSession.User.OfficialEmail : "-";
                RtbxCompanyName.Text = vSession.User.CompanyName;
                HpLnkWebSite.NavigateUrl = vSession.User.WebSite;
                HpLnkWebSite.Text = vSession.User.WebSite;
                RcbxCountries.Text = vSession.User.Country;
                RtbxAddress.Text = vSession.User.Address;

                string prefix=GlobalDBMethods.FindPrefixByCountryName(vSession.User.Country,session);
                if ((!string.IsNullOrEmpty(prefix)) && (vSession.User.Phone != prefix))
                {
                    RtbxPhone.Visible = true;
                    RtbxPhone.Text = vSession.User.Phone;
                }
                else
                {
                    RtbxPhone.Text = "-";
                }
                
                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    if (string.IsNullOrEmpty(vSession.User.MashapeUsername))
                    {
                        LblmashapeText.Text = "Mashape Username";
                        LblmashapeLink.Text = "Create free Mashape account ";
                    }
                    else
                    {
                        LblmashapeText.Text = "Mashape Account";
                        LblmashapeLink.Visible = false;
                        HpLnk.NavigateUrl = "https://www.mashape.com/?utm_source=myelio&utm_medium=website&utm_campaign=partners/" + vSession.User.MashapeUsername;
                        HpLnk.Text = "https://www.mashape.com/" + vSession.User.MashapeUsername;
                    }
                }
                else
                {
                    LblmashapeText.Visible = false;
                    LblmashapeLink.Visible = false;
                    HpLnk.Visible = false;
                }

                LblOverViewValue.Text = GlobalMethods.FixParagraphsView(vSession.User.Overview);
                LblDescriptionValue.Text = GlobalMethods.FixParagraphsView(vSession.User.Description);
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
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "17")).Text;
            LblUserBillingType.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "57")).Text;
            LblUsername.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "18")).Text;
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "19")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "20")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "21")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "22")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "23")).Text;
            Label11.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "24")).Text;
            Label12.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "25")).Text;
            LblmashapeText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "26")).Text;
            LblOverView.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "27")).Text;
            LblDescription.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "28")).Text;            

            Label lblEditText = (Label)ControlFinder.FindControlRecursive(RbtnEdit, "LblEditText");
            lblEditText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "5")).Text;

            Label lblRegisterText = (Label)ControlFinder.FindControlRecursive(RbtnRegister, "LblRegisterText");
            lblRegisterText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "13")).Text;
        }

        #endregion

        #region Buttons

        protected void RbtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                PlaceHolder phCompanydata = (PlaceHolder)ControlFinder.FindControlBackWards(this, "PhCompanydata");
                phCompanydata.Controls.Clear();

                vSession.LoadedDashboardCompanyEditControl = ControlLoader.CompanyDataEditMode;
                ControlLoader.LoadElioControls(this, phCompanydata, vSession.LoadedDashboardCompanyEditControl);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnRegister_OnClick(object sender, EventArgs args)
        {
            try
            {
                Response.Redirect(vSession.Page = ControlLoader.SignUp, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region To Delete

        protected void RbtnUpdate_OnClick(object sender, EventArgs args)
        {
            //    try
            //    {
            //        session.OpenConnection();

            //        bool hasSeslectedItem = false;
            //        string alert = "You must select at least one {notselectedcategory} category";

            //        ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
            //        if (user != null)
            //        {
            //            session.BeginTransaction();

            //            string category = string.Empty;
            //            try
            //            {
            //                if (user.CompanyType != Types.Developers.ToString())
            //                {
            #region Fix Industry List

            //                    List<CheckBoxList> cbxIndustryList = new List<CheckBoxList>();
            //                    cbxIndustryList.Add(cb1);
            //                    cbxIndustryList.Add(cb2);
            //                    cbxIndustryList.Add(cb3);

            //                    hasSeslectedItem = Methods.FixUserIndustriesByCheckBoxList(cbxIndustryList, vSession.User.Id, session);
            //                    if (!hasSeslectedItem)
            //                    {
            //                        //error      
            //                        category = Category.Industry.ToString();
            //                        throw new Exception();
            //                    }

            #endregion

            #region Fix Partner List

            //                    List<CheckBoxList> cbxPartnerList = new List<CheckBoxList>();
            //                    cbxPartnerList.Add(cb4);
            //                    cbxPartnerList.Add(cb5);
            //                    cbxPartnerList.Add(cb6);

            //                    hasSeslectedItem = Methods.FixUserPartnersByCheckBoxList(cbxPartnerList, vSession.User.Id, session);
            //                    if (!hasSeslectedItem)
            //                    {
            //                        //error
            //                        category = Category.Partner.ToString();
            //                        throw new Exception();
            //                    }

            #endregion

            #region Fix Market List

            //                    List<CheckBoxList> cbxMarketList = new List<CheckBoxList>();
            //                    cbxMarketList.Add(cb7);
            //                    cbxMarketList.Add(cb8);

            //                    hasSeslectedItem = Methods.FixUserMarketsByCheckBoxList(cbxMarketList, vSession.User.Id, session);
            //                    if (!hasSeslectedItem)
            //                    {
            //                        //error
            //                        category = Category.Market.ToString();
            //                        throw new Exception();
            //                    }

            #endregion
            //                }

            #region Fix Api List

            //                List<CheckBoxList> cbxApiList = new List<CheckBoxList>();
            //                cbxApiList.Add(cb9);
            //                cbxApiList.Add(cb10);
            //                cbxApiList.Add(cb11);

            //                hasSeslectedItem = Methods.FixUserApiesByCheckBoxList(cbxApiList, vSession.User.Id, session);
            //                if (!hasSeslectedItem && user.CompanyType == Types.Developers.ToString())
            //                {
            //                    //error
            //                    category = Category.Api.ToString();
            //                    throw new Exception();
            //                }

            #endregion
            //            }
            //            catch (Exception)
            //            {
            //                session.RollBackTransaction();
            //                Globalization.ShowMessageControl(UcMessageAlert, alert.Replace("{notselectedcategory}", category), MessageTypes.Error, true, true, false);

            //                return;
            //            }

            //            session.CommitTransaction();
            //            hasSeslectedItem = true;

            //            alert = "Your changes were successfully commited";
            //            Globalization.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        session.RollBackTransaction();
            //        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            //    }
            //    finally
            //    {
            //        session.CloseConnection();
        }

        #endregion

        #endregion
    }
}