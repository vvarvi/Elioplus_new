using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus
{
    public partial class AdminElioFinancialIncomeFlowPage : System.Web.UI.Page
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

                    FixPage();

                    FixTotalAmountsFields();
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

        private void FixTotalAmountsFields()
        {
            if (RdgResults.DataSource == null && (TbxTotalIncome.Text == "0.00" || TbxTotalIncome.Text == string.Empty) && (TbxTotalIncomeVat.Text == "0.00" || TbxTotalIncomeVat.Text == string.Empty) && (TbxTotalIncomeWithNoVat.Text == "0.00" || TbxTotalIncomeWithNoVat.Text == string.Empty))
            {
                TbxTotalIncome.Text = "0.00";
                TbxTotalIncomeVat.Text = "0.00";
                TbxTotalIncomeWithNoVat.Text = "0.00";
            }

            FixCurrencyIconText();            
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {                
                UpdateStrings();
                SetLinks();

                UcMessage.Visible = false;                
            }

            UcMessageAlert.Visible = (RdgResults.DataSource == null) ? true : false;

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                LblRenewalHead.Visible = LblRenewal.Visible = true;
                LblRenewalHead.Text = "Renewal date: ";

                try
                {
                    LblRenewal.Text = Sql.GetSubscriptionPlanRenewalDate(vSession.User.CustomerStripeId, session).ToString("MM/dd/yyyy");
                }
                catch (Exception)
                {
                    LblRenewalHead.Visible = LblRenewal.Visible = false;

                    Logger.Debug(string.Format("User {0} seems to be premium but he has no order in his account", vSession.User.Id.ToString()));
                }
            }
            else
            {
                LblRenewalHead.Visible = LblRenewal.Visible = false;
            }

            //aBtnGoPremium.Visible = ((vSession.User.BillingType == Convert.ToInt32(BillingType.Freemium) && vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed))) ? true : false;
            aBtnGoFull.Visible = vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted) ? true : false;            
        }

        private void SetLinks()
        {
            aBtnGoFull.HRef = ControlLoader.FullRegistrationPage;
        }

        private void UpdateStrings()
        {
            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {
                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(vSession.User.BillingType, session);
                if (packet != null)
                {
                    LblPricingPlan.Text = "You are currently on a " + packet.PackDescription + " plan";
                }
            }
            else
            {
                LblPricingPlan.Text = "You are currently on a free plan";
            }

            LblElioplusDashboard.Text = "";

            LblDashboard.Text = "Elioplus Dashboard";
            LblGoFull.Text = "Complete your registration";
            LblDashPage.Text = "Financial Flow - Incomes";
            LblDashSubTitle.Text = "";
            LblAdminNameValue.Text = vSession.User.CompanyName;
            LblAdminIdValue.Text = vSession.User.Id.ToString();

            RdpDateIn.SelectedDate = DateTime.Now;
            RdpLastUpdated.SelectedDate = DateTime.Now;

            LblId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "72")).Text;
            LblAdminName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "70")).Text;
            LblAdminId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "71")).Text;
            LblCurrency.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "79")).Text;
            LblIncomeAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "63")).Text;
            LblIncomeSource.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "64")).Text;
            LblOrganization.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "78")).Text;
            LblVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "65")).Text;
            LblIncomeVatAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "77")).Text;
            LblIncomeWithNoVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "66")).Text;
            LblDateIn.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "67")).Text;
            LbllastUpdated.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "68")).Text;
            LblComments.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "69")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "73")).Text;
            LblTotalIncomes.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "74")).Text;
            LblTotalIncomeVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "75")).Text;
            LblTotalIncomeWithNoVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "76")).Text;

            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;

            RdgResults.MasterTableView.GetColumn("id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "1")).Text;
            RdgResults.MasterTableView.GetColumn("admin_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "2")).Text;
            RdgResults.MasterTableView.GetColumn("admin_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "3")).Text;
            RdgResults.MasterTableView.GetColumn("income_source").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "5")).Text;
            RdgResults.MasterTableView.GetColumn("organization").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "14")).Text;
            RdgResults.MasterTableView.GetColumn("datein").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "6")).Text;
            RdgResults.MasterTableView.GetColumn("last_updated").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "7")).Text;
            RdgResults.MasterTableView.GetColumn("last_edit_user_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "12")).Text;
            RdgResults.MasterTableView.GetColumn("vat").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "8")).Text;
            RdgResults.MasterTableView.GetColumn("income_amount").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "4")).Text;
            RdgResults.MasterTableView.GetColumn("vat_amount").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "13")).Text;
            RdgResults.MasterTableView.GetColumn("income_amount_with_no_vat").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "9")).Text;
            RdgResults.MasterTableView.GetColumn("comments").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "10")).Text;
            RdgResults.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "5", "column", "11")).Text;
        }

        private void LoadItemData(GridDataItem item)
        {
            LblIdValue.Text = item["id"].Text;
            LblAdminIdValue.Text = item["admin_id"].Text;
            LblAdminNameValue.Text = item["admin_name"].Text;
            TbxIncomeAmount.Text = item["income_amount"].Text.Trim().Replace("$", string.Empty); ;
            TbxIncomeSource.Text = item["income_source"].Text;
            TbxOrganization.Text = (item["organization"].Text != "&nbsp;" && item["organization"].Text != string.Empty) ? item["organization"].Text : string.Empty;
            TbxVat.Text = item["vat"].Text.Trim().Replace("%", string.Empty);
            TbxIncomeVatAmount.Text = item["vat_amount"].Text.Trim().Replace("$", string.Empty); ;
            TbxIncomeWithNoVat.Text = item["income_amount_with_no_vat"].Text.Trim().Replace("$", string.Empty); ;
            RdpDateIn.SelectedDate = Convert.ToDateTime(item["datein"].Text);
            RdpLastUpdated.SelectedDate = DateTime.Now; //Convert.ToDateTime(item["last_updated"].Text);
            TbxComments.Text = item["comments"].Text;
            RcbxIsPublic.FindItemByValue(item["is_public"].Text).Selected = true;
        }

        private void ClearFields()
        {
            LblAdminIdValue.Text = vSession.User.Id.ToString();
            LblAdminNameValue.Text = vSession.User.CompanyName;
            LblIdValue.Text = string.Empty;

            TbxIncomeAmount.Text = string.Empty;
            TbxIncomeSource.Text = string.Empty;
            TbxOrganization.Text = string.Empty;
            TbxVat.Text = string.Empty;
            TbxIncomeVatAmount.Text = string.Empty;
            TbxIncomeWithNoVat.Text = string.Empty;
            TbxComments.Text = string.Empty;

            RdpDateIn.SelectedDate = DateTime.Now;
            RdpLastUpdated.SelectedDate = DateTime.Now;

            RcbxIsPublic.FindItemByValue("1").Selected = true;
            RcbxCurrency.FindItemByValue("0").Selected = true;

            LblTotalIncomeCurrency.Text = LblTotalIncomeVatCurrency.Text = LblTotalIncomeWithNoVatCurrency.Text = RcbxCurrency.SelectedItem.Text;

            UcMessage.Visible = false;
        }

        private bool HasValidData()
        {
            bool isValid = true;

            if (TbxIncomeAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxIncomeAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxIncomeSource.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Source", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            //if (TbxOrganization.Text == string.Empty)
            //{
            //    GlobalMethods.ShowMessageControl(UcMessage, "Insert Organization", MessageTypes.Error, true, true, false);
            //    isValid = false;
            //    return isValid;
            //}

            if (TbxVat.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Vat", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxIncomeVatAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Vat Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxIncomeWithNoVat.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Income With No Vat Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (RdpDateIn.SelectedDate == null)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Register Amount Date", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (RdpLastUpdated.SelectedDate == null)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Register Amount Last Update Date", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            return isValid;
        }

        private void CalculateVatAndClearAmount(decimal originalAmount, decimal vat, ref decimal vatAmount, ref decimal clearAmount)
        {
            vatAmount = originalAmount * vat / 100;
            clearAmount = originalAmount - vatAmount;
        }

        private double ConvertAmount(double originalAmount)
        {
            double currency = 1.06;

            return (RcbxCurrency.SelectedItem.Value == "0") ? originalAmount * currency : originalAmount / currency;
        }

        private double ConvertAmountForGrid(double originalAmount)
        {
            double currency = 1.06;

            return (RcbxCurrency.SelectedItem.Value == "0") ? originalAmount : originalAmount / currency;
        }

        private double ConvertAmountToUSDollars(double originalAmount)
        {
            double currency = 1.06;

            return (RcbxCurrency.SelectedItem.Value == "0") ? originalAmount : originalAmount * currency;
        }

        private string FixCurrencyIconTextForGrid()
        {
            return (RcbxCurrency.SelectedItem.Value == "0") ? " $" : " €";
        }

        private void FixCurrencyIconText()
        {
            LblTotalIncomeCurrency.Text =
                LblTotalIncomeVatCurrency.Text =
                LblTotalIncomeWithNoVatCurrency.Text =
                LblIncomeAmountCurrency.Text =
                LblIncomeVatAmountCurrency.Text =
                LblIncomeWithNoVatCurrency.Text =
                (RcbxCurrency.SelectedItem.Value == "0") ? " $" : " €";
        }

        # endregion

        #region Grids

        protected void RdgResults_PageIndexChanged(object sender, Telerik.Web.UI.GridPageChangedEventArgs e)
        {
            vSession.CurrentGridPageIndex = e.NewPageIndex;
        }

        protected void RdgResults_ItemCreated(object sender, GridItemEventArgs e)
        {
            try
            {
                if (e.Item is GridNoRecordsItem)
                {
                    GridNoRecordsItem item = (GridNoRecordsItem)e.Item;
                    Literal ltlNoDataFound = (Literal)ControlFinder.FindControlRecursive(item, "LtlNoDataFound");
                    ltlNoDataFound.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "search", "grid", "1", "literal", "1")).Text;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgResults_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnEdit = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEdit");
                    //imgBtnEdit.Visible = (item["is_confirmed"].Text == "1") ? false : true;
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

        protected void RdgResults_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    List<ElioFinancialIncome> financialIncome = Sql.GetAllPublicElioFinancialIncome(session);

                    if (financialIncome.Count > 0)
                    {
                        RdgResults.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("admin_id");
                        table.Columns.Add("admin_name");
                        table.Columns.Add("income_source");
                        table.Columns.Add("organization");
                        table.Columns.Add("datein");
                        table.Columns.Add("last_updated");
                        table.Columns.Add("last_edit_user_id");
                        table.Columns.Add("vat");
                        table.Columns.Add("income_amount");
                        table.Columns.Add("income_vat_amount");
                        table.Columns.Add("income_amount_with_no_vat");
                        table.Columns.Add("comments");
                        table.Columns.Add("is_public");

                        foreach (ElioFinancialIncome income in financialIncome)
                        {
                            table.Rows.Add(income.Id, income.AdminId, income.AdminName, income.IncomeSource, income.Organization, GlobalMethods.FixDate(income.Datein.ToString("MM/dd/yyyy")), GlobalMethods.FixDate(income.LastUpdated.ToString("MM/dd/yyyy")), (income.LastEditUserId > 0) ? income.LastEditUserId.ToString() : "Not edited", income.Vat + " %", ConvertAmountForGrid(Convert.ToDouble(income.IncomeAmount)).ToString("0.00") + FixCurrencyIconTextForGrid(), ConvertAmountForGrid(Convert.ToDouble(income.VatAmount)).ToString("0.00") + FixCurrencyIconTextForGrid(), ConvertAmountForGrid(Convert.ToDouble(income.IncomeAmountWithNoVat)).ToString("0.00") + FixCurrencyIconTextForGrid(), income.Comments, income.IsPublic);
                        }

                        RdgResults.DataSource = table;

                        double totalIncome = 0.00;
                        double totalIncomeVatAmount = 0.00;
                        double totalClearIncomeAmount = 0.00;

                        Sql.GetFinancialIncomePublicAmounts(ref totalIncome, ref totalIncomeVatAmount, ref totalClearIncomeAmount, session);

                        TbxTotalIncome.Text = totalIncome.ToString("0.00");
                        TbxTotalIncomeVat.Text = totalIncomeVatAmount.ToString("0.00");
                        TbxTotalIncomeWithNoVat.Text = totalClearIncomeAmount.ToString("0.00");
                    }
                    else
                    {
                        RdgResults.Visible = false;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, "There are no financial data registered", MessageTypes.Info, true, true, false);
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

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    UcMessage.Visible = false;
                    
                    if (!HasValidData()) return;

                    DataLoader<ElioFinancialIncome> loader = new DataLoader<ElioFinancialIncome>(session);
                    ElioFinancialIncome income = null;

                    if (LblIdValue.Text == string.Empty)
                    {
                        #region Insert Financial

                        income = new ElioFinancialIncome();

                        income.AdminId = Convert.ToInt32(LblAdminIdValue.Text);
                        income.AdminName = LblAdminNameValue.Text;
                        income.IncomeAmount = Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeAmount.Text)));
                        income.IncomeSource = TbxIncomeSource.Text;
                        income.Organization = TbxOrganization.Text;
                        income.Vat = Convert.ToDecimal(TbxVat.Text);
                        income.VatAmount = Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeVatAmount.Text)));
                        income.IncomeAmountWithNoVat = Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeWithNoVat.Text)));
                        //income.IncomeAmountWithNoVat = income.IncomeAmount - income.VatAmount;
                        income.Datein = Convert.ToDateTime(RdpDateIn.SelectedDate);
                        income.LastUpdated = Convert.ToDateTime(RdpLastUpdated.SelectedDate);
                        income.Comments = TbxComments.Text;
                        income.IsPublic = Convert.ToInt32(RcbxIsPublic.SelectedValue);
                        income.LastEditUserId = 0;

                        loader.Insert(income);

                        #endregion
                    }
                    else
                    {
                        #region Update Financial

                        income = Sql.UpdateElioFinancialFlowById(Convert.ToInt32(LblIdValue.Text),
                                                                 Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeAmount.Text))),
                                                                 LblAdminNameValue.Text,
                                                                 Convert.ToInt32(LblAdminIdValue.Text),
                                                                 TbxIncomeSource.Text,
                                                                 TbxOrganization.Text,
                                                                 TbxComments.Text,
                                                                 Convert.ToDateTime(RdpDateIn.SelectedDate),
                                                                 Convert.ToDateTime(RdpLastUpdated.SelectedDate),
                                                                 Convert.ToDecimal(TbxVat.Text),
                                                                 Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeVatAmount.Text))),
                                                                 Convert.ToDecimal(ConvertAmountToUSDollars(Convert.ToDouble(TbxIncomeWithNoVat.Text))),
                                                                 Convert.ToInt32(RcbxIsPublic.SelectedValue),
                                                                 vSession.User.Id,
                                                                 session);

                        #endregion

                        #region Other Way to Update Data

                        //income = Sql.GetElioFinancialFlowById(Convert.ToInt32(LblIdvalue.Text), session);
                        //if (income != null)
                        //{
                        //    income.AdminId = vSession.User.Id;
                        //    income.AdminName = vSession.User.CompanyName;
                        //    income.IncomeAmount = Convert.ToDecimal(TbxIncomeAmount.Text);
                        //    income.IncomeSource = TbxIncomeSource.Text;
                        //    income.Vat = Convert.ToDecimal(TbxIncomeVat.Text);
                        //    income.IncomeAmountWithNoVat = Convert.ToDecimal(TbxIncomeWithNoVat.Text);
                        //    income.Datein = Convert.ToDateTime(RdpDateIn.SelectedDate);
                        //    income.LastUpdated = Convert.ToDateTime(RdpLastUpdated.SelectedDate);
                        //    income.Comments = TbxComments.Text;

                        //    loader.Update(income);
                        //}

                        #endregion
                    }

                    RdgResults.Rebind();

                    GlobalMethods.ShowMessageControl(UcMessage, "Your new financial income has been saved successfully", MessageTypes.Success, true, true, false);

                    ClearFields();
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

        protected void RbtnReset_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ClearFields();

                    RdgResults.Rebind();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    LoadItemData(item);
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
        }

        protected void ImgBtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    ImageButton imgBtn = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                    Sql.SetElioFinancialIncomesNotPublic(Convert.ToInt32(item["id"].Text), vSession.User.Id, session);

                    RdgResults.Rebind();
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

        protected void ImgBtnCalculate_OnClick(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (TbxIncomeAmount.Text.Trim() != string.Empty)
                    {
                        if (TbxVat.Text.Trim() != string.Empty)
                        {
                            decimal vatAmount = 0;
                            decimal clearAmount = 0;

                            CalculateVatAndClearAmount(Convert.ToDecimal(TbxIncomeAmount.Text), Convert.ToDecimal(TbxVat.Text), ref vatAmount, ref clearAmount);
                            TbxIncomeVatAmount.Text = vatAmount.ToString("0.00");
                            TbxIncomeWithNoVat.Text = clearAmount.ToString("0.00");
                        }
                        else
                        {
                            GlobalMethods.ShowMessageControl(UcMessage, "Insert Vat", MessageTypes.Error, true, true, false);
                            return;
                        }
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Amount", MessageTypes.Error, true, true, false);
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
        }

        # endregion

        #region Combo

        protected void RcbxCurrency_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                if (vSession.User != null)
                {
                    if (TbxTotalIncome.Text.Trim() != string.Empty && TbxTotalIncomeVat.Text.Trim() != string.Empty && TbxTotalIncomeWithNoVat.Text.Trim() != string.Empty)
                    {
                        //double totalIncomes = 0;
                        //double totalIncomesVat = 0;
                        //double totalIncomesWithNoVat = 0;

                        //totalIncomes = ConvertAmount(Convert.ToDouble(TbxTotalIncome.Text));
                        //totalIncomesVat = ConvertAmount(Convert.ToDouble(TbxTotalIncomeVat.Text));
                        //totalIncomesWithNoVat = ConvertAmount(Convert.ToDouble(TbxTotalIncomeWithNoVat.Text));

                        TbxTotalIncome.Text = ConvertAmount(Convert.ToDouble(TbxTotalIncome.Text)).ToString("0.00");
                        TbxTotalIncomeVat.Text = ConvertAmount(Convert.ToDouble(TbxTotalIncomeVat.Text)).ToString("0.00");
                        TbxTotalIncomeWithNoVat.Text = ConvertAmount(Convert.ToDouble(TbxTotalIncomeWithNoVat.Text)).ToString("0.00");

                        if (TbxIncomeAmount.Text.Trim() != string.Empty)
                        {
                            TbxIncomeAmount.Text = ConvertAmount(Convert.ToDouble(TbxIncomeAmount.Text)).ToString("0.00");
                        }

                        if (TbxVat.Text.Trim() != string.Empty)
                        {
                            decimal vatAmount = 0;
                            decimal clearAmount = 0;

                            CalculateVatAndClearAmount(Convert.ToDecimal(TbxIncomeAmount.Text), Convert.ToDecimal(TbxVat.Text), ref vatAmount, ref clearAmount);
                            TbxIncomeVatAmount.Text = vatAmount.ToString("0.00");
                            TbxIncomeWithNoVat.Text = clearAmount.ToString("0.00");
                        }

                        FixCurrencyIconText();

                        RdgResults.Rebind();
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Amount", MessageTypes.Error, true, true, false);
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
        }

        #endregion
    }
}