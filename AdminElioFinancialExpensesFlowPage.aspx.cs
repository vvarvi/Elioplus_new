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
    public partial class AdminElioFinancialExpensesFlowPage : System.Web.UI.Page
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
            double totalIncome = 0.00;
            double totalIncomeVatAmount = 0.00;
            double totalClearIncomeAmount = 0.00;

            double totalExpenses = 0.00;
            double totalExpensesVatAmount = 0.00;
            double totalClearExpensesAmount = 0.00;

            if ((TbxTotalIncome.Text == "0.00" || TbxTotalIncome.Text == string.Empty) && (TbxTotalIncomeVat.Text == "0.00" || TbxTotalIncomeVat.Text == string.Empty) && (TbxTotalIncomeWithNoVat.Text == "0.00" || TbxTotalIncomeWithNoVat.Text == string.Empty))
            {
                Sql.GetFinancialIncomePublicAmounts(ref totalIncome, ref totalIncomeVatAmount, ref totalClearIncomeAmount, session);

                TbxTotalIncome.Text = totalIncome.ToString("0.00");
                TbxTotalIncomeVat.Text = totalIncomeVatAmount.ToString("0.00");
                TbxTotalIncomeWithNoVat.Text = totalClearIncomeAmount.ToString("0.00");
            }

            if (RdgResults.DataSource == null && (TbxTotalExpenses.Text == "0.00" || TbxTotalExpenses.Text == string.Empty) && (TbxTotalExpensesVat.Text == "0.00" || TbxTotalExpensesVat.Text == string.Empty) && (TbxTotalExpensesWithNoVat.Text == "0.00" || TbxTotalExpensesWithNoVat.Text == string.Empty))
            {
                TbxTotalExpenses.Text = "0.00";
                TbxTotalExpensesVat.Text = "0.00";
                TbxTotalExpensesWithNoVat.Text = "0.00";
            }
            else
            {
                Sql.GetFinancialExpensesPublicAmounts(ref totalExpenses, ref totalExpensesVatAmount, ref totalClearExpensesAmount, session);

                TbxTotalExpenses.Text = totalExpenses.ToString("0.00");
                TbxTotalExpensesVat.Text = totalExpensesVatAmount.ToString("0.00");
                TbxTotalExpensesWithNoVat.Text = totalClearExpensesAmount.ToString("0.00");
            }

            TbxFinalAmount.Text = (totalIncome - totalExpenses).ToString("0.00");
            TbxFinalVat.Text = (totalIncomeVatAmount - totalExpensesVatAmount).ToString("0.00");
            TbxFinalClearAmount.Text = (totalClearIncomeAmount - totalClearExpensesAmount).ToString("0.00");

            LblTotalExpensesCurrency.Text = 
                LblTotalExpensesVatCurrency.Text = 
                LblTotalExpensesWithNoVatCurrency.Text = 
                LblTotalIncomeCurrency.Text = 
                LblTotalIncomeVatCurrency.Text = 
                LblTotalIncomeWithNoVatCurrency.Text = 
                LblFinalAmountCurrency.Text = 
                LblFinalVatCurrency.Text = 
                LblFinalClearCurrency.Text = 
                RcbxCurrency.SelectedItem.Text;
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {                
                UpdateStrings();
                SetLinks();

                UcMessage.Visible = false;

                FixTotalAmountsFields();
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
            LblDashPage.Text = "Financial Flow - Expenses";
            LblDashSubTitle.Text = "";
            LblAdminNameValue.Text = vSession.User.CompanyName;
            LblAdminIdValue.Text = vSession.User.Id.ToString();

            RdpDateIn.SelectedDate = DateTime.Now;
            RdpLastUpdated.SelectedDate = DateTime.Now;

            LblId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "72")).Text;
            LblAdminName.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "70")).Text;
            LblAdminId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "71")).Text;
            LblCurrency.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "79")).Text;
            LblExpensesAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "80")).Text;
            LblExpensesReason.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "81")).Text;
            LblUserId.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "94")).Text;
            LblOrganization.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "78")).Text;
            LblExpensesVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "82")).Text;
            LblExpensesVatAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "84")).Text;
            LblExpensesWithNoVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "83")).Text;
            LblDateIn.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "67")).Text;
            LbllastUpdated.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "68")).Text;
            LblComments.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "69")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "73")).Text;
            LblTotalExpenses.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "91")).Text;
            LblTotalExpensesVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "92")).Text;
            LblTotalExpensesWithNoVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "93")).Text;
            LblFinalAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "95")).Text;
            LblFinalVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "96")).Text;
            LblFinalClearAmount.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "97")).Text;

            LblTotalIncomes.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "74")).Text;
            LblTotalIncomeVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "75")).Text;
            LblTotalIncomeWithNoVat.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "76")).Text;

            Label lblSaveText = (Label)ControlFinder.FindControlRecursive(RbtnSave, "LblSaveText");
            lblSaveText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "6")).Text;

            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;

            RdgResults.MasterTableView.GetColumn("id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "1")).Text;
            RdgResults.MasterTableView.GetColumn("admin_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "2")).Text;
            RdgResults.MasterTableView.GetColumn("admin_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "3")).Text;
            RdgResults.MasterTableView.GetColumn("expenses_reason").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "5")).Text;
            RdgResults.MasterTableView.GetColumn("user_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "15")).Text;
            RdgResults.MasterTableView.GetColumn("organization").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "14")).Text;
            RdgResults.MasterTableView.GetColumn("datein").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "6")).Text;
            RdgResults.MasterTableView.GetColumn("last_updated").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "7")).Text;
            RdgResults.MasterTableView.GetColumn("last_edit_user_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "12")).Text;
            RdgResults.MasterTableView.GetColumn("vat").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "8")).Text;
            RdgResults.MasterTableView.GetColumn("expenses_amount").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "4")).Text;
            RdgResults.MasterTableView.GetColumn("vat_amount").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "13")).Text;
            RdgResults.MasterTableView.GetColumn("expenses_amount_with_no_vat").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "9")).Text;
            RdgResults.MasterTableView.GetColumn("comments").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "10")).Text;
            RdgResults.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "6", "column", "11")).Text;
        }

        private void LoadItemData(GridDataItem item)
        {
            LblIdValue.Text = item["id"].Text;
            LblAdminIdValue.Text = item["admin_id"].Text;
            LblAdminNameValue.Text = item["admin_name"].Text;
            TbxExpensesAmount.Text = item["expenses_amount"].Text.Trim().Replace("$", string.Empty); ;
            TbxExpensesReason.Text = item["expenses_reason"].Text;
            TbxUserId.Text = item["user_id"].Text;
            TbxOrganization.Text = (item["organization"].Text != "&nbsp;" && item["organization"].Text != string.Empty) ? item["organization"].Text : string.Empty;
            TbxExpensesVat.Text = item["vat"].Text.Trim().Replace("%", string.Empty);
            TbxExpensesVatAmount.Text = item["vat_amount"].Text.Trim().Replace("$", string.Empty); ;
            TbxExpensesWithNoVat.Text = item["expenses_amount_with_no_vat"].Text.Trim().Replace("$", string.Empty); ;
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

            TbxExpensesAmount.Text = string.Empty;
            TbxExpensesReason.Text = string.Empty;
            TbxUserId.Text = string.Empty;
            TbxOrganization.Text = string.Empty;
            TbxExpensesVat.Text = string.Empty;
            TbxExpensesVatAmount.Text = string.Empty;
            TbxExpensesWithNoVat.Text = string.Empty;
            TbxComments.Text = string.Empty;

            RdpDateIn.SelectedDate = DateTime.Now;
            RdpLastUpdated.SelectedDate = DateTime.Now;

            RcbxIsPublic.FindItemByValue("1").Selected = true;
            RcbxCurrency.FindItemByValue("0").Selected = true;

            LblTotalExpensesCurrency.Text = LblTotalExpensesVatCurrency.Text = LblTotalExpensesWithNoVatCurrency.Text = RcbxCurrency.SelectedItem.Text;

            UcMessage.Visible = false;
        }

        private bool HasValidData()
        {
            bool isValid = true;

            if (TbxExpensesAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxExpensesAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxExpensesReason.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses Reason", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxUserId.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert User Id", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            //if (TbxOrganization.Text == string.Empty)
            //{
            //    GlobalMethods.ShowMessageControl(UcMessage, "Insert Organization", MessageTypes.Error, true, true, false);
            //    isValid = false;
            //    return isValid;
            //}

            if (TbxExpensesVat.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Vat", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxExpensesVatAmount.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses Vat Amount", MessageTypes.Error, true, true, false);
                isValid = false;
                return isValid;
            }

            if (TbxExpensesWithNoVat.Text == string.Empty)
            {
                GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses WIth No Vat Amount", MessageTypes.Error, true, true, false);
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
                    List<ElioFinancialExpenses> financialExpenses = Sql.GetAllPublicElioFinancialExpenses(session);

                    if (financialExpenses.Count > 0)
                    {
                        RdgResults.Visible = true;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("admin_id");
                        table.Columns.Add("admin_name");
                        table.Columns.Add("expenses_reason");
                        table.Columns.Add("user_id");
                        table.Columns.Add("organization");
                        table.Columns.Add("datein");
                        table.Columns.Add("last_updated");
                        table.Columns.Add("last_edit_user_id");
                        table.Columns.Add("vat");
                        table.Columns.Add("expenses_amount");
                        table.Columns.Add("vat_amount");
                        table.Columns.Add("expenses_amount_with_no_vat");
                        table.Columns.Add("comments");
                        table.Columns.Add("is_public");

                        foreach (ElioFinancialExpenses expenses in financialExpenses)
                        {
                            table.Rows.Add(expenses.Id, expenses.AdminId, expenses.AdminName, expenses.ExpensesReason, expenses.UserId, expenses.Organization, GlobalMethods.FixDate(expenses.Datein.ToString("MM/dd/yyyy")), GlobalMethods.FixDate(expenses.LastUpdated.ToString("MM/dd/yyyy")), (expenses.LastEditUserId > 0) ? expenses.LastEditUserId.ToString() : "Not edited", expenses.Vat + " %", expenses.ExpensesAmount + " $", expenses.VatAmount + " $", expenses.ExpensesAmountWithNoVat + " $", expenses.Comments, expenses.IsPublic);
                        }

                        RdgResults.DataSource = table;

                        double totalExpenses = 0.00;
                        double totalExpensesVatAmount = 0.00;
                        double totalClearExpensesAmount = 0.00;

                        Sql.GetFinancialExpensesPublicAmounts(ref totalExpenses, ref totalExpensesVatAmount, ref totalClearExpensesAmount, session);

                        TbxTotalExpenses.Text = totalExpenses.ToString("0.00");
                        TbxTotalExpensesVat.Text = totalExpensesVatAmount.ToString("0.00");
                        TbxTotalExpensesWithNoVat.Text = totalClearExpensesAmount.ToString("0.00");

                        double totalIncome = 0;
                        double totalIncomeVatAmount = 0;
                        double totalClearIncomeAmount = 0;

                        Sql.GetFinancialIncomePublicAmounts(ref totalIncome, ref totalIncomeVatAmount, ref totalClearIncomeAmount, session);

                        TbxTotalIncome.Text = totalIncome.ToString();
                        TbxTotalIncomeVat.Text = totalIncomeVatAmount.ToString();
                        TbxTotalIncomeWithNoVat.Text = totalClearIncomeAmount.ToString();

                        TbxFinalAmount.Text = (totalIncome - totalExpenses).ToString("0.00");
                        TbxFinalVat.Text = (totalIncomeVatAmount - totalExpensesVatAmount).ToString("0.00");
                        TbxFinalClearAmount.Text = (totalClearIncomeAmount - totalClearExpensesAmount).ToString("0.00");
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

                    DataLoader<ElioFinancialExpenses> loader = new DataLoader<ElioFinancialExpenses>(session);
                    ElioFinancialExpenses expenses = null;

                    if (LblIdValue.Text == string.Empty)
                    {
                        #region Insert Financial

                        expenses = new ElioFinancialExpenses();

                        expenses.AdminId = Convert.ToInt32(LblAdminIdValue.Text);
                        expenses.AdminName = LblAdminNameValue.Text;
                        expenses.ExpensesAmount = Convert.ToDecimal(TbxExpensesAmount.Text);
                        expenses.ExpensesReason = TbxExpensesReason.Text;
                        expenses.UserId = Convert.ToInt32(TbxUserId.Text);
                        expenses.Organization = TbxOrganization.Text;
                        expenses.Vat = Convert.ToDecimal(TbxExpensesVat.Text);
                        expenses.VatAmount = Convert.ToDecimal(TbxExpensesVatAmount.Text);
                        expenses.ExpensesAmountWithNoVat = Convert.ToDecimal(TbxExpensesWithNoVat.Text);
                        expenses.Datein = Convert.ToDateTime(RdpDateIn.SelectedDate);
                        expenses.LastUpdated = Convert.ToDateTime(RdpLastUpdated.SelectedDate);
                        expenses.Comments = TbxComments.Text;
                        expenses.IsPublic = Convert.ToInt32(RcbxIsPublic.SelectedValue);
                        expenses.LastEditUserId = 0;

                        loader.Insert(expenses);

                        #endregion
                    }
                    else
                    {
                        #region Update Financial

                        expenses = Sql.UpdateElioFinancialExpensesFlowById(Convert.ToInt32(LblIdValue.Text),
                                                                 Convert.ToDecimal(TbxExpensesAmount.Text),
                                                                 LblAdminNameValue.Text,
                                                                 Convert.ToInt32(LblAdminIdValue.Text),
                                                                 TbxExpensesReason.Text,
                                                                 Convert.ToInt32(TbxUserId.Text),
                                                                 TbxOrganization.Text,
                                                                 TbxComments.Text,
                                                                 Convert.ToDateTime(RdpDateIn.SelectedDate),
                                                                 Convert.ToDateTime(RdpLastUpdated.SelectedDate),
                                                                 Convert.ToDecimal(TbxExpensesVat.Text),
                                                                 Convert.ToDecimal(TbxExpensesVatAmount.Text),
                                                                 Convert.ToDecimal(TbxExpensesWithNoVat.Text),
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

                    GlobalMethods.ShowMessageControl(UcMessage, "Your new financial expenses has been saved successfully", MessageTypes.Success, true, true, false);

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

                    Sql.SetElioFinancialExpensesNotPublic(Convert.ToInt32(item["id"].Text), vSession.User.Id, session);

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
                    if (TbxExpensesAmount.Text.Trim() != string.Empty)
                    {
                        if (TbxExpensesVat.Text.Trim() != string.Empty)
                        {
                            decimal vatAmount = Convert.ToDecimal(TbxExpensesAmount.Text) * (Convert.ToDecimal(TbxExpensesVat.Text) / 100);
                            TbxExpensesVatAmount.Text = vatAmount.ToString();
                            decimal clearAmount = Convert.ToDecimal(TbxExpensesAmount.Text) - vatAmount;
                            TbxExpensesWithNoVat.Text = clearAmount.ToString();
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
                    double totalIncomes = 0;
                    double totalIncomesVat = 0;
                    double totalIncomesWithNoVat = 0;

                    if (TbxTotalIncome.Text.Trim() != string.Empty && TbxTotalIncomeVat.Text.Trim() != string.Empty && TbxTotalIncomeWithNoVat.Text.Trim() != string.Empty)
                    {
                        if (RcbxCurrency.SelectedItem.Value == "0")
                        {
                            totalIncomes = Convert.ToDouble(TbxTotalIncome.Text) * 1.06;
                            totalIncomesVat = Convert.ToDouble(TbxTotalIncomeVat.Text) * 1.06;
                            totalIncomesWithNoVat = Convert.ToDouble(TbxTotalIncomeWithNoVat.Text) * 1.06;
                        }
                        else
                        {
                            totalIncomes = Convert.ToDouble(TbxTotalIncome.Text) / 1.06;
                            totalIncomesVat = Convert.ToDouble(TbxTotalIncomeVat.Text) / 1.06;
                            totalIncomesWithNoVat = Convert.ToDouble(TbxTotalIncomeWithNoVat.Text) / 1.06;
                        }

                        TbxTotalIncome.Text = totalIncomes.ToString("0.00");
                        TbxTotalIncomeVat.Text = totalIncomesVat.ToString("0.00");
                        TbxTotalIncomeWithNoVat.Text = totalIncomesWithNoVat.ToString("0.0");
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessage, "Insert Income Amount", MessageTypes.Error, true, true, false);
                        return;
                    }

                    double totalExpenses = 0.00;
                    double totalExpensesVat = 0.00;
                    double totalExpensesWithNoVat = 0.00;

                    if (TbxTotalExpenses.Text.Trim() != string.Empty && TbxTotalExpensesVat.Text.Trim() != string.Empty && TbxTotalExpensesWithNoVat.Text.Trim() != string.Empty)
                    {
                        if (RcbxCurrency.SelectedItem.Value == "0")
                        {
                            totalExpenses = Convert.ToDouble(TbxTotalExpenses.Text) * 1.06;
                            totalExpensesVat = Convert.ToDouble(TbxTotalExpensesVat.Text) * 1.06;
                            totalExpensesWithNoVat = Convert.ToDouble(TbxTotalExpensesWithNoVat.Text) * 1.06;
                        }
                        else
                        {
                            totalExpenses = Convert.ToDouble(TbxTotalExpenses.Text) / 1.06;
                            totalExpensesVat = Convert.ToDouble(TbxTotalExpensesVat.Text) / 1.06;
                            totalExpensesWithNoVat = Convert.ToDouble(TbxTotalExpensesWithNoVat.Text) / 1.06;
                        }

                        TbxTotalExpenses.Text = totalExpenses.ToString("0.00");
                        TbxTotalExpensesVat.Text = totalExpensesVat.ToString("0.00");
                        TbxTotalExpensesWithNoVat.Text = totalExpensesWithNoVat.ToString("0.0");
                    }
                    else
                    {
                        GlobalMethods.ShowMessageControl(UcMessage, "Insert Expenses Amount", MessageTypes.Error, true, true, false);
                        return;
                    }

                    TbxFinalAmount.Text = (totalIncomes - totalExpenses).ToString("0.00");
                    TbxFinalVat.Text = (totalIncomesVat - totalExpensesVat).ToString("0.00");
                    TbxFinalClearAmount.Text = (totalIncomesWithNoVat - totalExpensesWithNoVat).ToString("0.00");

                    LblTotalExpensesCurrency.Text =
                        LblTotalExpensesVatCurrency.Text =
                        LblTotalExpensesWithNoVatCurrency.Text =
                        LblFinalAmountCurrency.Text =
                        LblFinalVatCurrency.Text =
                        LblFinalClearCurrency.Text =
                        RcbxCurrency.SelectedItem.Text;
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