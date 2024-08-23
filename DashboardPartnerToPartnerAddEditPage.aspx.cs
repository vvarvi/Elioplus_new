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
using WdS.ElioPlus.Lib.EmailNotificationSender;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus
{
    public partial class DashboardPartnerToPartnerAddEditPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public List<ElioSubIndustriesGroupItems> UserSubIndustriesGroupItemsList
        {
            get
            {
                return (ViewState["UserSubIndustriesGroupItemsList"] != null) ? (List<ElioSubIndustriesGroupItems>)ViewState["UserSubIndustriesGroupItemsList"] : new List<ElioSubIndustriesGroupItems>();
            }

            set { ViewState["UserSubIndustriesGroupItemsList"] = value; }
        }

        public DealActionMode P2pMode
        {
            get
            {
                return (ViewState["P2pMode"] != null) ? (DealActionMode)ViewState["P2pMode"] : DealActionMode.INSERT;
            }

            set { ViewState["P2pMode"] = value; }
        }

        public string P2pOpportunityName
        {
            get
            {
                return (ViewState["P2pOpportunityName"] != null) ? ViewState["P2pOpportunityName"].ToString() : "";
            }

            set { ViewState["P2pOpportunityName"] = value; }
        }

        public string P2pOpportunityDescription
        {
            get
            {
                return (ViewState["P2pOpportunityDescription"] != null) ? ViewState["P2pOpportunityDescription"].ToString() : "";
            }

            set { ViewState["P2pOpportunityDescription"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
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

                        if (Request.QueryString["p2pViewID"] != null)
                        {
                            int p2pId = Convert.ToInt32(Session[Request.QueryString["p2pViewID"]]);
                            if (p2pId > 0)
                            {
                                LoadSelectedP2pDealData(p2pId);

                                BtnSave.Enabled = true;

                                UpdatePanelContent.Update();
                            }
                            else
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "partner-to-partner"), false);
                        }
                        else
                        {
                            ResetFields();
                            ResetErrorFields();
                            divSelectedPartnerHeader.Visible = false;
                            P2pMode = DealActionMode.INSERT;
                        }

                        AllowEdit();
                    }

                    LoadMessageBox();
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

        private void LoadMessageBox()
        {
            TbxMessageName.Text = (vSession.User.CompanyName != "") ? vSession.User.CompanyName : "";
            TbxMessageEmail.Text = (vSession.User.Email != "") ? vSession.User.Email : "";
            TbxMessageSubject.Text = (P2pOpportunityName != "") ? P2pOpportunityName : "";
            TbxMessageContent.Text = (P2pOpportunityDescription != "") ? P2pOpportunityDescription : "";
        }

        private void LoadSelectedP2pDealData(int p2pId)
        {
            ElioPartnerToPartnerDeals deal = Sql.GetPartnerToPartnerDealById(p2pId, session);

            if (deal != null)
            {
                TbxOpportunityName.Text = P2pOpportunityName = deal.OpportunityName;
                TbxProduct.Text = deal.ProductDescription;
                TbxDealValue.Text = deal.DealValue.ToString();
                DdlCurrency.SelectedItem.Text = deal.Currency;
                TbxOpportunityDescription.Text = deal.OpportunityDescription;
                DdlCountries.SelectedItem.Value = deal.CountryId.ToString();
                DdlP2pStatus.SelectedItem.Value = deal.Status.ToString();

                TbxMessageName.Text = vSession.User.CompanyName;
                TbxMessageEmail.Text = vSession.User.Email;
                TbxMessageSubject.Text = deal.OpportunityName;
                P2pOpportunityDescription = "Hi, I am interested about your deal with the below description:" + Environment.NewLine + Environment.NewLine + deal.OpportunityDescription + Environment.NewLine + Environment.NewLine + "Please contact with me as soon as posible.";

                P2pMode = (deal.Status == (int)DealStatus.Open) ? (deal.ResellerId == vSession.User.Id) ? DealActionMode.UPDATE : DealActionMode.VIEW : DealActionMode.VIEW;

                aSendMessage.Visible = deal.ResellerId != vSession.User.Id && P2pMode == DealActionMode.VIEW;

                if (P2pMode != DealActionMode.INSERT)
                {
                    //if (P2pMode == DealActionMode.VIEW)
                        //cb1.Items.Clear();

                    List<ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup> dealSubIndustries = Sql.GetP2PSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup(deal.ResellerId, deal.Id, session);
                    foreach (ElioPartnerToPartnerSubIndustriesIJSubIndustriesGroupItemIJSubIndustriesGroup subIn in dealSubIndustries)
                    {
                        if (P2pMode == DealActionMode.UPDATE)
                        {
                            //foreach (ListItem item in cb1.Items)
                            //{
                            //    if (Convert.ToInt32(item.Value) == subIn.SubIndustryGroupItemId)
                            //    {
                            //        item.Selected = true;
                            //        break;
                            //    }
                            //}
                        }
                        else if (P2pMode == DealActionMode.VIEW)
                        {
                            ListItem item = new ListItem();
                            item.Value = subIn.SubIndustryGroupItemId.ToString();
                            item.Text = subIn.Description + " (" + subIn.GroupDescription + ")";

                            item.Selected = true;

                            //cb1.Items.Add(item);
                        }
                    }

                    BtnSave.Text = "Update";
                }
                else
                    BtnSave.Text = "Save";

                if (deal.Status == (int)DealStatus.Open)
                {
                    BtnSave.Visible = true;
                    BtnClear.Visible = true;
                }
                else if (deal.Status == (int)DealStatus.Closed)
                {
                    BtnSave.Visible = false;
                    BtnClear.Visible = false;
                }

                if (deal.ResellerId != vSession.User.Id)
                {
                    LoadPartnerData(deal.ResellerId);
                }
                else
                    divSelectedPartnerHeader.Visible = false;

                UpdatePanelContent.Update();
            }
            else
                Response.Redirect(ControlLoader.Dashboard(vSession.User, "partner-to-partner"), false);
        }

        private bool LoadPartnerData(int partnerId)
        {
            ElioUsers partner = Sql.GetUserById(partnerId, session);
            if (partner != null)
            {
                LblDealPartnerName.Text = partner.CompanyName;
                LblAddressContent.Text = (!string.IsNullOrEmpty(partner.Address)) ? partner.Address : "-";
                aWebsiteContent.HRef = partner.WebSite;
                aWebsiteContent.Target = "_blank";
                LblWebsiteContent.Text = partner.WebSite;
                //aEmailContent.HRef = "mailto:" + partner.Email;
                //LblEmailContent.Text = partner.Email;
                
                ImgCompanyLogo.ImageUrl = partner.CompanyLogo;
                divSelectedPartnerHeader.Visible = true;
            }

            return partner != null;
        }

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
            UpdateStrings();
            SetLinks();

            #region Top Packet Button

            #endregion

            divResellerActionsA.Visible = vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString();
            divVendorActionsA.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

            LoadUserSubcategories();
            LoadCountries();
            LoadP2pDealStatus();
            LoadP2pDealCurrency();
        }

        private void AllowEdit()
        {
            BtnSave.Text = (P2pMode == DealActionMode.INSERT) ? "Save" : "Update";
            TbxOpportunityName.ReadOnly = TbxProduct.ReadOnly = TbxDealValue.ReadOnly = TbxOpportunityDescription.ReadOnly = P2pMode == DealActionMode.VIEW;
            DdlCountries.Enabled = PnlVerticals.Enabled = P2pMode != DealActionMode.VIEW;
            DdlP2pStatus.Enabled = DdlCurrency.Enabled = P2pMode != DealActionMode.VIEW;
            divStatus.Visible = divStatusTitleSection.Visible = P2pMode == DealActionMode.UPDATE;
            BtnSave.Visible = BtnClear.Visible = P2pMode != DealActionMode.VIEW;
        }

        private void LoadCountries()
        {
            DdlCountries.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "Select Country";

            DdlCountries.Items.Add(item);

            List<ElioCountries> countries = Sql.GetPublicCountries(session);
            foreach (ElioCountries country in countries)
            {
                item = new ListItem();
                item.Value = country.Id.ToString();
                item.Text = country.CountryName;

                DdlCountries.Items.Add(item);
            }

            DdlCountries.DataBind();
        }

        private void LoadUserSubcategories()
        {
            //cb1.Items.Clear();

            List<ElioSubIndustriesGroupItems> userVerticals = Sql.GetUserSubIndustries(vSession.User.Id, session);

            int i = 0;
            if (i < userVerticals.Count && i == 0)
            {
                LblCriteria1.Text = userVerticals[i].Description;
                CbxCrit1.Visible = true;
                LblCriteria1.Visible = true;
                HiddenField1.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit1.Visible = false;
                LblCriteria1.Visible = false;
                HdnVert1Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 1)
            {
                CbxCrit2.Visible = true;
                LblCriteria2.Visible = true;
                LblCriteria2.Text = userVerticals[i].Description;
                HiddenField2.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit2.Visible = false;
                LblCriteria2.Visible = false;
                HdnVert2Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 2)
            {
                CbxCrit3.Visible = true;
                LblCriteria3.Visible = true;
                LblCriteria3.Text = userVerticals[i].Description;
                HiddenField3.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit3.Visible = false;
                LblCriteria3.Visible = false;
                HdnVert3Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 3)
            {
                CbxCrit4.Visible = true;
                LblCriteria4.Visible = true;
                LblCriteria4.Text = userVerticals[i].Description;
                HiddenField4.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit4.Visible = false;
                LblCriteria4.Visible = false;
                HdnVert4Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 4)
            {
                CbxCrit5.Visible = true;
                LblCriteria5.Visible = true;
                LblCriteria5.Text = userVerticals[i].Description;
                HiddenField5.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit5.Visible = false;
                LblCriteria5.Visible = false;
                HdnVert5Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 5)
            {
                CbxCrit6.Visible = true;
                LblCriteria6.Visible = true;
                LblCriteria6.Text = userVerticals[i].Description;
                HiddenField6.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit6.Visible = false;
                LblCriteria6.Visible = false;
                HdnVert6Ckd.Value = "-1";
            }

            i++;
            if (i < userVerticals.Count && i == 6)
            {
                CbxCrit7.Visible = true;
                LblCriteria7.Visible = true;
                LblCriteria7.Text = userVerticals[i].Description;
                HiddenField7.Value = userVerticals[i].Id.ToString();
            }
            else
            {
                CbxCrit7.Visible = false;
                LblCriteria7.Visible = false;
                HdnVert7Ckd.Value = "-1";
            }

            ScriptManager.RegisterStartupScript(this, GetType(), "SetCheckedVerticals", "SetCheckedVerticals();", true);

            #region to delete

            //foreach (ElioSubIndustriesGroupItems category in userVerticals)
            //{
            //    ListItem item = new ListItem();

            //    ElioSubIndustriesGroup group = Sql.GetVerticalById(category.SubIndustriesGroupId, session);
            //    if (group != null)
            //    {
            //        item.Text = category.Description + " (" + group.SubIndustryDescription + ")";
            //    }
            //    else
            //        item.Text = category.Description;

            //    item.Value = category.Id.ToString().ToString();

            //    cb1.Items.Add(item);
            //}

            #endregion
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {            
            LblMessageHeader.Text = "Send Message";
            BtnMessageSend.Text = "Send it";
            BtnMessageCancel.Text = "Cancel";
            BtnCloseModal.Text = "X";
        }

        private void ShowUploadMessages(string content, string title, MessageTypes type)
        {
            LblFileUploadTitle.Text = title;
            LblFileUploadfMsg.Text = content;
            GlobalMethods.ShowMessageControlDA(UploadMessageAlert, content, type, false, true, false);
            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
        }

        private void ResetPanelItems()
        {

        }

        private void ResetFields()
        {
            TbxOpportunityName.Text = string.Empty;
            TbxProduct.Text = string.Empty;
            TbxDealValue.Text = string.Empty;
            TbxOpportunityDescription.Text = string.Empty;
            DdlCountries.SelectedItem.Value = "0";
            DdlP2pStatus.SelectedItem.Value = "0";
            DdlCurrency.SelectedItem.Value = "0";

            UcMessageAlert.Visible = false;
        }

        private void ResetErrorFields()
        {
            UcMessageAlert.Visible = false;
        }

        private void LoadP2pDealStatus()
        {
            DdlP2pStatus.Items.Clear();

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "-- Select your deal's status --";

            DdlP2pStatus.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealStatus.Open).ToString();
            item.Text = DealStatus.Open.ToString();
            DdlP2pStatus.Items.Add(item);

            item = new ListItem();

            item.Value = Convert.ToInt32(DealStatus.Closed).ToString();
            item.Text = DealStatus.Closed.ToString();
            DdlP2pStatus.Items.Add(item);
        }

        private void LoadP2pDealCurrency()
        {
            DdlCurrency.Items.Clear();

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "$";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "1";
            item.Text = "€";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "2";
            item.Text = "¥";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "3";
            item.Text = "£";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "4";
            item.Text = "A$";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "5";
            item.Text = "C$";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "6";
            item.Text = "Fr";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "7";
            item.Text = "元";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "8";
            item.Text = "kr";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "9";
            item.Text = "NZ$";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "10";
            item.Text = "HK$";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "11";
            item.Text = "₩";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "12";
            item.Text = "₺";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "13";
            item.Text = "₽";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "14";
            item.Text = "₹";

            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "15";
            item.Text = "R$";
            DdlCurrency.Items.Add(item);

            item = new ListItem();

            item.Value = "16";
            item.Text = "R";

            DdlCurrency.Items.Add(item);
        }

        private void ClearMessageData()
        {
            TbxMessageContent.Text = string.Empty;
            TbxMessageEmail.Text = string.Empty;
            TbxMessageName.Text = string.Empty;
            TbxMessageSubject.Text = string.Empty;

            LblSuccessMsg.Text = string.Empty;
            LblWarningMsg.Text = string.Empty;
            divWarningMsg.Visible = false;
            divSuccessMsg.Visible = false;
        }

        private bool CheckData()
        {
            bool isError = false;

            divWarningMsg.Visible = false;
            divSuccessMsg.Visible = false;

            if (string.IsNullOrEmpty(TbxMessageName.Text))
            {
                LblWarningMsgContent.Text = "Please enter a name!";
                divWarningMsg.Visible = true;
                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxMessageEmail.Text))
            {
                LblWarningMsgContent.Text = "Please enter an email address!";
                divWarningMsg.Visible = true;
                return isError = true;
            }
            else
            {
                if (!Validations.IsEmail(TbxMessageEmail.Text))
                {
                    LblWarningMsgContent.Text = "Please enter a valid email address!";
                    divWarningMsg.Visible = true;
                    return isError = true;
                }
            }

            if (string.IsNullOrEmpty(TbxMessageSubject.Text))
            {
                LblWarningMsgContent.Text = "Please enter a subject!";
                divWarningMsg.Visible = true;
                return isError = true;
            }

            if (string.IsNullOrEmpty(TbxMessageContent.Text))
            {
                LblWarningMsgContent.Text = "Please enter a message!";
                divWarningMsg.Visible = true;
                return isError = true;
            }

            return isError;
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

                    #region Reset Error Fields

                    UcMessageAlert.Visible = false;

                    #endregion

                    #region Check Data

                    if (TbxOpportunityName.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill opportunity name", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }
                    
                    if (TbxProduct.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill product", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }

                    if (DdlCountries.SelectedItem.Value == "0")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select country", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }

                    if (TbxDealValue.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill deal value", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }

                    if (TbxOpportunityDescription.Text == "")
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please fill a short opportunity description", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }

                    bool hasSelectedIndustry = false;
                    //foreach (ListItem item in cb1.Items)
                    //{
                    //    if (item.Selected)
                    //    {
                    //        hasSelectedIndustry = true;
                    //        break;
                    //    }
                    //}

                    if (!hasSelectedIndustry)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select at least one sub-industry from your verticals", MessageTypes.Error, true, true, true, true, false);
                        
                        return;
                    }

                    if (P2pMode == DealActionMode.UPDATE)
                    {
                        if (DdlP2pStatus.SelectedItem.Value == "0")
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Please select your deal's status", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }
                    }

                    #endregion

                    DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);

                    if (P2pMode == DealActionMode.INSERT)
                    {
                        #region Insert new P2P Deal

                        ElioPartnerToPartnerDeals p2p = new ElioPartnerToPartnerDeals();

                        p2p.ResellerId = vSession.User.Id;
                        p2p.PartnerUserId = 0;
                        p2p.OpportunityName = TbxOpportunityName.Text;
                        p2p.ProductDescription = TbxProduct.Text;
                        p2p.DealValue = Convert.ToDecimal(TbxDealValue.Text);
                        p2p.Currency = DdlCurrency.SelectedItem.Text;
                        p2p.OpportunityDescription = TbxOpportunityDescription.Text;
                        p2p.CountryId = Convert.ToInt32(DdlCountries.SelectedItem.Value);
                        p2p.Status = (int)DealStatus.Open;
                        p2p.DateCreated = DateTime.Now;
                        p2p.LastUpdated = DateTime.Now;
                        p2p.DateClosed = null;
                        p2p.IsActive = 1;
                        p2p.IsPublic = 1;
                        p2p.CanceledAt = null;
                        p2p.CanceledBy = null;

                        loader.Insert(p2p);

                        //foreach (ListItem item in cb1.Items)
                        //{
                            if (CbxCrit1.Checked)
                            {
                                ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                                p2pSubIndustry.UserId = vSession.User.Id;
                                p2pSubIndustry.P2pId = p2p.Id;
                                p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField1.Value);
                                p2pSubIndustry.Sysdate = DateTime.Now;
                                p2pSubIndustry.LastUpdated = DateTime.Now;

                                DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                                subLoader.Insert(p2pSubIndustry);
                            }

                        if (CbxCrit2.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField2.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }

                        if (CbxCrit3.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField3.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }

                        if (CbxCrit4.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField4.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }

                        if (CbxCrit5.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField5.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }

                        if (CbxCrit6.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField6.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }

                        if (CbxCrit7.Checked)
                        {
                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                            p2pSubIndustry.UserId = vSession.User.Id;
                            p2pSubIndustry.P2pId = p2p.Id;
                            p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(HiddenField7.Value);
                            p2pSubIndustry.Sysdate = DateTime.Now;
                            p2pSubIndustry.LastUpdated = DateTime.Now;

                            DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                            subLoader.Insert(p2pSubIndustry);
                        }
                        //}

                        List<string> emails = new List<string>();

                        List<ElioUsers> vendors = SqlCollaboration.GetCollaborationVendorsByResellerUserId(p2p.ResellerId, CollaborateInvitationStatus.Confirmed.ToString(), session);
                        foreach (ElioUsers vendor in vendors)
                        {
                            List<ElioUsers> partners = SqlCollaboration.GetP2PCollaborationResellers(vendor.Id, p2p.ResellerId, p2p.CountryId, p2p.Id, session);

                            foreach (ElioUsers partner in partners)
                            {
                                emails.Add(partner.Email);
                            }
                        }

                        try
                        {
                            if (emails.Count > 0)
                                EmailSenderLib.SendPartner2PartnerDealRegistrationEmail(vSession.User.CompanyName, emails, p2p.OpportunityName, p2p.ProductDescription, DdlCountries.SelectedItem.Text, p2p.DealValue.ToString(), p2p.OpportunityDescription, false, vSession.Lang, session);
                            else
                                Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, added new p2p lead at {1}, but no other collaboration partners email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardPartnerToPartnerAddEdit.aspx --> ERROR sending notification Email");
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            throw ex;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Update P2P Deal

                        if (Request.QueryString["p2pViewID"] != null)
                        {
                            int p2pId = Convert.ToInt32(Session[Request.QueryString["p2pViewID"].ToString()]);

                            ElioPartnerToPartnerDeals p2p = Sql.GetPartnerToPartnerDealById(p2pId, session);
                            if (p2p != null)
                            {
                                p2p.OpportunityName = TbxOpportunityName.Text;
                                p2p.ProductDescription = TbxProduct.Text;
                                p2p.CountryId = Convert.ToInt32(DdlCountries.SelectedItem.Value);
                                p2p.DealValue = Convert.ToDecimal(TbxDealValue.Text);
                                p2p.Currency = DdlCurrency.SelectedItem.Text;
                                p2p.OpportunityDescription = TbxOpportunityDescription.Text;
                                p2p.Status = Convert.ToInt32(DdlP2pStatus.SelectedItem.Value);

                                loader.Update(p2p);

                                //foreach (ListItem item in cb1.Items)
                                //{
                                    //bool existP2pSubIndustry = Sql.ExistP2PSubIndustryByUser(p2p.ResellerId, p2p.Id, Convert.ToInt32(item.Value), session);

                                    //if (item.Selected)
                                    //{
                                        //if (!existP2pSubIndustry)
                                        //{
                                            ElioPartnerToPartnerSubIndustries p2pSubIndustry = new ElioPartnerToPartnerSubIndustries();

                                            p2pSubIndustry.UserId = vSession.User.Id;
                                            p2pSubIndustry.P2pId = p2p.Id;
                                            //p2pSubIndustry.SubIndustryGroupItemId = Convert.ToInt32(item.Value);
                                            p2pSubIndustry.Sysdate = DateTime.Now;
                                            p2pSubIndustry.LastUpdated = DateTime.Now;

                                            //DataLoader<ElioPartnerToPartnerSubIndustries> subLoader = new DataLoader<ElioPartnerToPartnerSubIndustries>(session);
                                            //subLoader.Insert(p2pSubIndustry);
                                        //}
                                    //}
                                    //else
                                    //{
                                    //    if (existP2pSubIndustry)
                                    //        Sql.DeleteP2PSubIndustryByUser(p2p.ResellerId, p2p.Id, Convert.ToInt32(item.Value), session);
                                    //}
                                //}
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);

                                Logger.DetailedError(Request.Url.ToString(), "ERROR --> DashboardPartnerToPartnerAddEdit.aspx page", string.Format("User {0} tried to update deal at {1}, but could not find specific deal", vSession.User.Id.ToString(), DateTime.Now));
                                return;
                            }
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Something went wrong. Please try again later!", MessageTypes.Error, true, true, true, true, false);

                            Logger.DetailedError(Request.Url.ToString(), "ERROR --> DashboardPartnerToPartnerAddEdit.aspx page", string.Format("User {0} tried to update deal at {1}, but could not find deal from url viewID", vSession.User.Id.ToString(), DateTime.Now));
                            return;
                        }

                        #endregion
                    }

                    #region Message

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Saved successfully", MessageTypes.Success, true, true, true, true, false);
                    
                    #endregion
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
                    LoadCountries();
                    LoadUserSubcategories();
                    LoadP2pDealStatus();
                    LoadP2pDealCurrency();
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

        protected void BtnBack_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "partner-to-partner"), false);
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

        protected void BtnCancelMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                //ClearMessageData();
                divSuccessMsg.Visible = false;
                divWarningMsg.Visible = false;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseMessagePopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnSend_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (Request.QueryString["p2pViewID"] != null)
                    {
                        int p2pId = Convert.ToInt32(Session[Request.QueryString["p2pViewID"]]);
                        if (p2pId > 0)
                        {
                            ElioUsers p2pUser = Sql.GetP2pDealUserByDealId(p2pId, session);
                            if (p2pUser != null)
                            {
                                bool isError = CheckData();

                                if (isError) return;

                                ElioUserPacketStatus packetStatusFeatures = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);

                                if (packetStatusFeatures != null)
                                {
                                    if (packetStatusFeatures.AvailableMessagesCount > 0)
                                    {
                                        ElioUsersMessages message = new ElioUsersMessages();

                                        try
                                        {
                                            session.BeginTransaction();

                                            message = GlobalDBMethods.InsertCompanyMessage(vSession.User.Id, TbxMessageEmail.Text.Trim(), p2pUser.Id, p2pUser.Email, p2pUser.OfficialEmail, TbxMessageSubject.Text, TbxMessageContent.Text, session);

                                            List<string> emails = new List<string>();
                                            emails.Add(p2pUser.Email);

                                            //EmailNotificationsLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, session);
                                            EmailSenderLib.SendNotificationEmailToCompanyForNewInboxMessage(vSession.User, emails, TbxMessageSubject.Text, TbxMessageContent.Text, false, vSession.Lang, session);

                                            GlobalDBMethods.FixUserEmailAndPacketStatusFeatutes(message, packetStatusFeatures, session);

                                            //ClearMessageData();

                                            divSuccessMsg.Visible = true;
                                            LblSuccessMsgContent.Text = "Your message was successfully sent!";

                                            session.CommitTransaction();
                                        }
                                        catch (Exception ex)
                                        {
                                            session.RollBackTransaction();

                                            divWarningMsg.Visible = true;
                                            LblWarningMsgContent.Text = "Your message could not be sent to " + p2pUser.CompanyName + ". Please try again later or contact us!";

                                            Logger.DetailedError("An error occured during compose new email from company " + vSession.User.CompanyName + " to company " + p2pUser.CompanyName + " at " + DateTime.Now);
                                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                        }
                                    }
                                    else
                                    {
                                        divWarningMsg.Visible = true;
                                        LblWarningMsgContent.Text = "You have no messages left. They will be available again after the monthly subscription renewal!";
                                    }
                                }
                                else
                                {
                                    divWarningMsg.Visible = true;
                                    LblWarningMsgContent.Text = "Your message could not be sent. Please try again later or contact us!";
                                }
                            }
                            else
                            {
                                divWarningMsg.Visible = true;
                                LblWarningMsgContent.Text = "Your message could not be sent. Please try again later or contact us!";
                            }
                        }
                        else
                        {
                            divWarningMsg.Visible = true;
                            LblWarningMsgContent.Text = "Your message could not be sent. Please try again later or contact us!";
                        }
                    }
                    else
                    {
                        divWarningMsg.Visible = true;
                        LblWarningMsgContent.Text = "Your message could not be sent. Please try again later or contact us!";
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                ClearMessageData();

                divWarningMsg.Visible = true;
                LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later or contact us!";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnP2pSubmit_OnClick(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    divP2pSuccessMsg.Visible = false;
                    divP2pWarningMsg.Visible = false;

                    if (Request.QueryString["p2pViewID"] != null)
                    {
                        int p2pId = Convert.ToInt32(Session[Request.QueryString["p2pViewID"]]);
                        if (p2pId > 0)
                        {
                            ElioUsers p2pUser = Sql.GetP2pDealUserByDealId(p2pId, session);
                            if (p2pUser != null)
                            {
                                //if (TbxP2pCompanyName.Text.Trim() == "")
                                //{
                                //    LblP2pWarningMsgContent.Text = "Please enter partner company name!";
                                //    divP2pWarningMsg.Visible = true;
                                //    return;
                                //}

                                if (TbxP2pCompanyEmail.Text == "")
                                {
                                    LblP2pWarningMsgContent.Text = "Please enter partner company email!";
                                    divP2pWarningMsg.Visible = true;
                                    return;
                                }
                                else
                                {
                                    if (!Validations.IsEmail(TbxP2pCompanyEmail.Text))
                                    {
                                        LblP2pWarningMsgContent.Text = "Please enter a valid email!";
                                        divP2pWarningMsg.Visible = true;
                                        return;
                                    }
                                }


                                ElioUsers interestedUser = Sql.GetUserByEmail(TbxP2pCompanyEmail.Text, session);

                                if (interestedUser != null)
                                {
                                    if (interestedUser.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                    {
                                        ElioPartnerToPartnerDeals deal = Sql.GetPartnerToPartnerDealById(p2pId, session);
                                        if (deal != null)
                                        {
                                            deal.LastUpdated = DateTime.Now;
                                            deal.Status = (int)DealStatus.Closed;
                                            deal.PartnerUserId = interestedUser.Id;

                                            DataLoader<ElioPartnerToPartnerDeals> loader = new DataLoader<ElioPartnerToPartnerDeals>(session);
                                            loader.Update(deal);

                                            P2pMode = DealActionMode.VIEW;

                                            AllowEdit();
                                            UpdatePanelContent.Update();

                                            divP2pSuccessMsg.Visible = true;
                                            LblP2pSuccessMsgContent.Text = "Your deal was successfully closed to your partner!";

                                            try
                                            {
                                                ElioUsers reseller = Sql.GetUserById(deal.PartnerUserId, session);
                                                if (reseller != null)
                                                {
                                                    if (deal.Status == (int)DealStatus.Closed)
                                                        EmailSenderLib.SendPartner2PartnerChangeStatusDealRegistrationEmail(vSession.User.CompanyName, reseller.Email, deal.OpportunityName, DealStatus.Closed.ToString(), vSession.Lang, session);
                                                    else
                                                        EmailSenderLib.SendPartner2PartnerChangeStatusDealRegistrationEmail(vSession.User.CompanyName, reseller.Email, deal.OpportunityName, DealStatus.Open.ToString(), vSession.Lang, session);
                                                }
                                                else
                                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, added new lead distribution at {1}, but no partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardLeadDistributionAddEdit.aspx --> ERROR sending notification Email");
                                            }
                                            catch (Exception ex)
                                            {
                                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                                throw ex;
                                            }
                                        }
                                        else
                                        {
                                            LblP2pWarningMsgContent.Text = "Something went wrong. Please try again later!";
                                            divP2pWarningMsg.Visible = true;

                                            Logger.DetailedError(Request.Url.ToString(), "ERROR --> DashboardPartnerToPartnerAddEdit.aspx page", string.Format("User {0} tried to update deal at {1}, but could not find specific deal", vSession.User.Id.ToString(), DateTime.Now));
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        LblP2pWarningMsgContent.Text = "This email doesn't belong to a Channel Partner type of user!";
                                        divP2pWarningMsg.Visible = true;
                                        return;
                                    }
                                }
                                else
                                {
                                    LblP2pWarningMsgContent.Text = "This email doesn't belong to a Channel Partner!";
                                    divP2pWarningMsg.Visible = true;
                                    return;
                                }
                            }
                            else
                            {
                                LblP2pWarningMsgContent.Text = "Something went wrong. Please try again later!";
                                divP2pWarningMsg.Visible = true;

                                Logger.DetailedError(Request.Url.ToString(), "ERROR --> DashboardPartnerToPartnerAddEdit.aspx page", string.Format("User {0} tried to close deal at {1}, but reseller user could not find of deal {2}", vSession.User.Id.ToString(), DateTime.Now, p2pId.ToString()));
                                return;
                            }
                        }
                        else
                        {
                            LblP2pWarningMsgContent.Text = "Something went wrong. Please try again later!";
                            divP2pWarningMsg.Visible = true;

                            Logger.DetailedError(Request.Url.ToString(), "ERROR --> DashboardPartnerToPartnerAddEdit.aspx page", string.Format("User {0} tried to update(close) deal at {1}, but could not find deal from url viewID", vSession.User.Id.ToString(), DateTime.Now));
                            return;
                        }
                    }
                    else
                    {
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "partner-to-partner"), false);
                    }
                }
                else
                {
                    Response.Redirect(ControlLoader.Login, false);
                }
            }
            catch (Exception ex)
            {
                ClearMessageData();

                divWarningMsg.Visible = true;
                LblWarningMsgContent.Text = "We are sorry but your message could not be sent because an unkown error occured. Please try again later or contact us!";

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void BtnP2pCancelMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //TbxP2pCompanyName.Text = "";
                TbxP2pCompanyEmail.Text = "";
                divP2pSuccessMsg.Visible = false;
                divP2pWarningMsg.Visible = false;

                if (Request.QueryString["p2pViewID"] != null)
                {
                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "partner-to-partner-add-edit" + "?p2pViewID=" + Request.QueryString["p2pViewID"]), false);

                    //int p2pId = Convert.ToInt32(Session[Request.QueryString["p2pViewID"]]);
                    //if (p2pId > 0)
                    //{
                    //    ElioPartnerToPartnerDeals deal = Sql.GetPartnerToPartnerDealById(p2pId, session);
                    //    if (deal != null)
                    //    {
                    //        if (deal.Status == (int)DealStatus.Closed)
                    //        {
                    //            DdlP2pStatus.SelectedItem.Value = Convert.ToInt32(DealStatus.Closed).ToString();
                    //            DdlP2pStatus.SelectedText = DealStatus.Closed.ToString();
                    //        }
                    //        else
                    //        {
                    //            DdlP2pStatus.SelectedItem.Value = Convert.ToInt32(DealStatus.Open).ToString();
                    //            DdlP2pStatus.SelectedText = DealStatus.Open.ToString();
                    //        }
                    //    }
                    //}

                    //AllowEdit();
                    //UpdatePanelContent.Update();

                    //ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseP2pPartnerPopUp();", true);
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

        # endregion

        #region Dropdown Lists

        protected void cb1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //foreach (ListItem item in cb1.Items)
                //{
                //    if (item.Selected)
                //    {
                //        //ElioPartnerToPartnerSubIndustries subIndustry = new ElioPartnerToPartnerSubIndustries();

                //        //subIndustry.UserId = vSession.User.Id;
                //        //subIndustry.P2pId = 1;

                //        ElioSubIndustriesGroupItems subItem = new ElioSubIndustriesGroupItems();
                //        subItem.Id = Convert.ToInt32(item.Value.Split('~')[0]);
                //        subItem.SubIndustriesGroupId = Convert.ToInt32(item.Value.Split('~')[1]);
                //        subItem.Description = item.Text;

                //        if (UserSubIndustriesGroupItemsList == null)
                //            UserSubIndustriesGroupItemsList = new List<ElioSubIndustriesGroupItems>();

                //        UserSubIndustriesGroupItemsList.Add(subItem);
                //    }
                //    else
                //    {
                //        if (UserSubIndustriesGroupItemsList != null && UserSubIndustriesGroupItemsList.Count > 0)
                //        {
                //            foreach (ElioSubIndustriesGroupItems subItem in UserSubIndustriesGroupItemsList)
                //            {
                //                if (subItem.Id == subItem.Id)
                //                {
                //                    UserSubIndustriesGroupItemsList.Remove(subItem);
                //                    break;
                //                }
                //            }
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlVertical_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                //if (DdlVertical.SelectedItem.Value != "0")
                //{
                //    List<ElioSubcategoriesIJSubcategoriesGroups> userVerticals = Sql.GetUserSubIndustriesGroupItemsBySubIndustryGroupId(vSession.User.Id, Convert.ToInt32(DdlVertical.SelectedItem.Value), session);

                //    cb1.Items.Clear();
                //    cb2.Items.Clear();
                //    //cb3.Items.Clear();

                //    int listItemsCount = userVerticals.Count / 2;
                //    int modulo = userVerticals.Count / 2;
                //    int count = 1;

                //    foreach (ElioSubcategoriesIJSubcategoriesGroups groupItem in userVerticals)
                //    {
                //        ListItem listItem = new ListItem();

                //        listItem.Text = groupItem.SubCategoryDescription;
                //        listItem.Value = groupItem.SubIndustryGroupItemId.ToString() + "~" + Convert.ToInt32(DdlVertical.SelectedItem.Value).ToString();

                //        if (count <= listItemsCount)
                //            cb1.Items.Add(listItem);
                //        else
                //            cb2.Items.Add(listItem);

                //        if (UserSubIndustriesGroupItemsList != null && UserSubIndustriesGroupItemsList.Count > 0)
                //        {
                //            foreach (ElioSubIndustriesGroupItems subItem in UserSubIndustriesGroupItemsList)
                //            {
                //                if (subItem.Id == Convert.ToInt32(listItem.Value.Split('~')[0]))
                //                {
                //                    listItem.Selected = true;
                //                }
                //            }

                //            count++;
                //        }
                //    }
                //}
                //else
                //{
                //    cb1.Items.Clear();
                //    cb2.Items.Clear();
                //}
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

        protected void DdlP2pStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //TbxP2pCompanyName.Text = "";
                TbxP2pCompanyEmail.Text = "";

                if (DdlP2pStatus.SelectedItem.Value == Convert.ToInt32(DealStatus.Closed).ToString())
                    ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "OpenP2pPartnerPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}