using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Enums;

namespace WdS.ElioPlus.Controls.Dashboard.Charts
{
    public partial class Statistics : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {               
                UpdateStrings();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void UpdateStrings()
        {
            RdgRegistrations.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgRegistrations.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgRegistrations.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            RdgRegistrations.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;

            RdgThirdRegistrations.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            RdgThirdRegistrations.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;

            RdgRegisteredUsers.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            RdgRegisteredUsers.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;

            RdgNotRegisteredUsers.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            RdgNotRegisteredUsers.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;

            RdgRegVsAll.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgRegVsAll.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            //RdgRegVsAll.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;

            RdgNotRegVsAll.MasterTableView.GetColumn("year").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "1")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_1").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "2")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_2").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "3")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_3").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "4")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_4").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "5")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_5").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "6")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_6").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "7")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_7").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "8")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_8").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "9")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_9").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "10")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_10").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "11")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_11").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "12")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("month_12").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "13")).Text;
            RdgNotRegVsAll.MasterTableView.GetColumn("sum").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "14")).Text;
            //RdgNotRegVsAll.MasterTableView.GetColumn("avg").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "2", "column", "15")).Text;
            
            //RdgAllTypes.MasterTableView.GetColumn("").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "23")).Text;
            //RdgAllTypes.MasterTableView.GetColumn("").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "12")).Text;
            //RdgAllTypes.MasterTableView.GetColumn("").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "23")).Text;
            //RdgAllTypes.MasterTableView.GetColumn("").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "12")).Text;
        }

        #endregion

        #region Grids

        protected void RdgRegistrations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgRegistrations.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    #region All

                    int[] monthsAll = new int[12];
                    int[] months = new int[12];

                    int[] monthsAllThird = new int[12];
                    int[] monthsThird = new int[12];

                    int totalΜonths = 0;
                    int totalMonthsThird = 0;
                    int totalΜonthsAll = 0;
                    int totalΜonthsAllThird = 0;
                    string allAccountStatus = "0,1";
                    string companyType = Types.Vendors.ToString() + "','" + EnumHelper.GetDescription(Types.Resellers).ToString();
                    decimal avgΜonthsAll = 0;
                    decimal avgΜonthsAllThird = 0;

                    #endregion

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        #region All Users Table

                        DataTable tblAll = StatisticsSql.GetCountOfUsersAndthirdPartyRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), allAccountStatus, Convert.ToInt32(UserApplicationType.Elioplus), session);
                        DataTable tblAllThirdParty = StatisticsSql.GetCountOfUsersAndthirdPartyRegistrationMonthsByYearByType((table0.Rows[i][0]).ToString(), Convert.ToInt32(AccountStatus.NotCompleted).ToString(), EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.ThirdParty), session);

                        months = GlobalMethods.FillMonthsAndThirdPartyTableWithTableValues(tblAll, tblAllThirdParty, out monthsThird);
                        monthsAll = GlobalMethods.FillUsersMonthsAndThirdPartyTableWithTableValues(months, monthsAll, monthsThird, monthsAllThird, out monthsAllThird);

                        totalΜonths = GlobalMethods.GetTotalUserAndThirdPartyYearRegistrations(months, monthsThird, out totalMonthsThird);
                        totalΜonthsAll += totalΜonths;
                        totalΜonthsAllThird += totalMonthsThird;

                        decimal avgΜonthsThird = 0;
                        decimal avgΜonths = GlobalMethods.GetAverageUserAndThirdPartyYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalΜonths, totalMonthsThird, out avgΜonthsThird);
                        avgΜonthsAll += avgΜonths;
                        avgΜonthsAllThird += avgΜonthsThird;

                        #endregion

                        table.Rows.Add((table0.Rows[i][0]).ToString(), months[0] + "(" + monthsThird[0] + ")", months[1] + "(" + monthsThird[1] + ")", months[2] + "(" + monthsThird[2] + ")", months[3] + "(" + monthsThird[3] + ")", months[4] + "(" + monthsThird[4] + ")", months[5] + "(" + monthsThird[5] + ")", months[6] + "(" + monthsThird[6] + ")", months[7] + "(" + monthsThird[7] + ")", months[8] + "(" + monthsThird[8] + ")", months[9] + "(" + monthsThird[9] + ")", months[10] + "(" + monthsThird[10] + ")", months[11] + "(" + monthsThird[11] + ")", totalΜonths + "(" + totalMonthsThird + ")", avgΜonths + "(" + Math.Round(avgΜonthsThird, 2) + ")");
                    }

                    table.Rows.Add(Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "statistics", "label", "1")).Text, monthsAll[0] + "(" + monthsAllThird[0] + ")", monthsAll[1] + "(" + monthsAllThird[1] + ")", monthsAll[2] + "(" + monthsAllThird[2] + ")", monthsAll[3] + "(" + monthsAllThird[3] + ")", monthsAll[4] + "(" + monthsAllThird[4] + ")", monthsAll[5] + "(" + monthsAllThird[5] + ")", monthsAll[6] + "(" + monthsAllThird[6] + ")", monthsAll[7] + "(" + monthsAllThird[7] + ")", monthsAll[8] + "(" + monthsAllThird[8] + ")", monthsAll[9] + "(" + monthsAllThird[9] + ")", monthsAll[10] + "(" + monthsAllThird[10] + ")", monthsAll[11] + "(" + monthsAllThird[11] + ")", totalΜonthsAll + "(" + totalΜonthsAllThird + ")", (table0.Rows.Count > 0) ? Math.Round(Convert.ToDecimal(avgΜonthsAll) / table0.Rows.Count, 2) + "(" + Math.Round(Convert.ToDecimal(avgΜonthsAllThird) / table0.Rows.Count, 2) + ")" : "0(0)");

                    RdgRegistrations.DataSource = table;
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

        protected void RdgThirdRegistrations_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgThirdRegistrations.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    #region All

                    int[] monthsAll = new int[12];
                    int[] months = new int[12];

                    int[] monthsAllThird = new int[12];
                    int[] monthsThird = new int[12];

                    int totalΜonths = 0;
                    int totalMonthsThird = 0;
                    int totalΜonthsAll = 0;
                    int totalΜonthsAllThird = 0;
                    string allAccountStatus = "0,1";
                    string companyType = Types.Vendors.ToString() + "','" + EnumHelper.GetDescription(Types.Resellers).ToString();
                    decimal avgΜonthsAll = 0;
                    decimal avgΜonthsAllThird = 0;

                    #endregion

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        #region All Users Table

                        DataTable tblAll = StatisticsSql.GetCountOfUsersAndthirdPartyRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), allAccountStatus, Convert.ToInt32(UserApplicationType.ThirdParty), session);
                        DataTable tblAllThirdParty = StatisticsSql.GetCountOfUsersAndthirdPartyRegistrationMonthsByYearByType((table0.Rows[i][0]).ToString(), Convert.ToInt32(AccountStatus.Completed).ToString(), EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.ThirdParty), session);

                        months = GlobalMethods.FillMonthsAndThirdPartyTableWithTableValues(tblAll, tblAllThirdParty, out monthsThird);
                        monthsAll = GlobalMethods.FillUsersMonthsAndThirdPartyTableWithTableValues(months, monthsAll, monthsThird, monthsAllThird, out monthsAllThird);

                        totalΜonths = GlobalMethods.GetTotalUserAndThirdPartyYearRegistrations(months, monthsThird, out totalMonthsThird);
                        totalΜonthsAll += totalΜonths;
                        totalΜonthsAllThird += totalMonthsThird;

                        decimal avgΜonthsThird = 0;
                        decimal avgΜonths = GlobalMethods.GetAverageUserAndThirdPartyYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalΜonths, totalMonthsThird, out avgΜonthsThird);
                        avgΜonthsAll += avgΜonths;
                        avgΜonthsAllThird += avgΜonthsThird;

                        #endregion

                        table.Rows.Add((table0.Rows[i][0]).ToString(), months[0] + "(" + monthsThird[0] + ")", months[1] + "(" + monthsThird[1] + ")", months[2] + "(" + monthsThird[2] + ")", months[3] + "(" + monthsThird[3] + ")", months[4] + "(" + monthsThird[4] + ")", months[5] + "(" + monthsThird[5] + ")", months[6] + "(" + monthsThird[6] + ")", months[7] + "(" + monthsThird[7] + ")", months[8] + "(" + monthsThird[8] + ")", months[9] + "(" + monthsThird[9] + ")", months[10] + "(" + monthsThird[10] + ")", months[11] + "(" + monthsThird[11] + ")", totalΜonths + "(" + totalMonthsThird + ")", avgΜonths + "(" + Math.Round(avgΜonthsThird, 2) + ")");
                    }

                    table.Rows.Add(Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "statistics", "label", "1")).Text, monthsAll[0] + "(" + monthsAllThird[0] + ")", monthsAll[1] + "(" + monthsAllThird[1] + ")", monthsAll[2] + "(" + monthsAllThird[2] + ")", monthsAll[3] + "(" + monthsAllThird[3] + ")", monthsAll[4] + "(" + monthsAllThird[4] + ")", monthsAll[5] + "(" + monthsAllThird[5] + ")", monthsAll[6] + "(" + monthsAllThird[6] + ")", monthsAll[7] + "(" + monthsAllThird[7] + ")", monthsAll[8] + "(" + monthsAllThird[8] + ")", monthsAll[9] + "(" + monthsAllThird[9] + ")", monthsAll[10] + "(" + monthsAllThird[10] + ")", monthsAll[11] + "(" + monthsAllThird[11] + ")", totalΜonthsAll + "(" + totalΜonthsAllThird + ")", (table0.Rows.Count > 0) ? Math.Round(Convert.ToDecimal(avgΜonthsAll) / table0.Rows.Count, 2) + "(" + Math.Round(Convert.ToDecimal(avgΜonthsAllThird) / table0.Rows.Count, 2) + ")" : "0(0)");

                    RdgThirdRegistrations.DataSource = table;
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

        protected void RdgRegisteredUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgRegisteredUsers.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    int[] totalUsers = new int[12];
                    int totalUsrs = 0;
                    decimal avgUsrs = 0;
                    int total = 0;
                    string accountStatus = Convert.ToInt32(AccountStatus.Completed).ToString();

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        DataTable tbl = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), accountStatus, session);

                        int[] months = GlobalMethods.FillMonthsTableWithTableValues(tbl);
                        totalUsers = GlobalMethods.FillUsersMonthsTableWithTableValues(months, totalUsers);
                        total = GlobalMethods.GetTotalUserYearRegistrations(months);
                        totalUsrs += total;

                        decimal avg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), total);
                        avgUsrs += avg;

                        table.Rows.Add((table0.Rows[i][0]).ToString(), months[0], months[1], months[2], months[3], months[4], months[5], months[6], months[7], months[8], months[9], months[10], months[11], total, avg);
                    }

                    table.Rows.Add(Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "statistics", "label", "1")).Text, totalUsers[0], totalUsers[1], totalUsers[2], totalUsers[3], totalUsers[4], totalUsers[5], totalUsers[6], totalUsers[7], totalUsers[8], totalUsers[9], totalUsers[10], totalUsers[11], totalUsrs, (table0.Rows.Count > 0) ? Math.Round(Convert.ToDecimal(avgUsrs) / table0.Rows.Count, 2) : 0);

                    RdgRegisteredUsers.DataSource = table;
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

        protected void RdgNotRegisteredUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgNotRegisteredUsers.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    int[] totalUsers = new int[12];
                    int totalUsrs = 0;
                    decimal avgUsrs = 0;
                    int total = 0;
                    string accountStatus = Convert.ToInt32(AccountStatus.NotCompleted).ToString();

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        DataTable tbl = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), accountStatus, session);

                        int[] months = GlobalMethods.FillMonthsTableWithTableValues(tbl);
                        totalUsers = GlobalMethods.FillUsersMonthsTableWithTableValues(months, totalUsers);
                        total = GlobalMethods.GetTotalUserYearRegistrations(months);
                        totalUsrs += total;

                        decimal avg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), total);
                        avgUsrs += avg;

                        table.Rows.Add((table0.Rows[i][0]).ToString(), months[0], months[1], months[2], months[3], months[4], months[5], months[6], months[7], months[8], months[9], months[10], months[11], total, avg);
                    }

                    table.Rows.Add(Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "statistics", "label", "1")).Text, totalUsers[0], totalUsers[1], totalUsers[2], totalUsers[3], totalUsers[4], totalUsers[5], totalUsers[6], totalUsers[7], totalUsers[8], totalUsers[9], totalUsers[10], totalUsers[11], totalUsrs, (table0.Rows.Count > 0) ? Math.Round(Convert.ToDecimal(avgUsrs) / table0.Rows.Count, 2) : 0);

                    RdgNotRegisteredUsers.DataSource = table;
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

        protected void RdgRegVsAll_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgRegVsAll.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                //table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    #region Registered

                    int[] totalUsersReg = new int[12];
                    int totalUsrsReg = 0;
                    decimal avgUsrsReg = 0;
                    int totalReg = 0;

                    #endregion

                    #region Not Registered

                    int[] totalUsersNotReg = new int[12];
                    int totalUsrsNotReg = 0;
                    decimal avgUsrsNotReg = 0;
                    int totalNotReg = 0;

                    #endregion

                    #region All

                    int[] totalUsersAll = new int[12];
                    int totalUsrsAll = 0;
                    decimal avgUsrsAll = 0;
                    int totalAll = 0;

                    #endregion

                    string regAccountStatus = Convert.ToInt32(AccountStatus.Completed).ToString();
                    string notRegAccountStatus = Convert.ToInt32(AccountStatus.NotCompleted).ToString();
                    string allAccountStatus = Convert.ToInt32(AccountStatus.Completed).ToString() + "," + Convert.ToInt32(AccountStatus.NotCompleted).ToString();

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        #region Registered Users Table

                        DataTable tblReg = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), regAccountStatus, session);

                        int[] monthsReg = GlobalMethods.FillMonthsTableWithTableValues(tblReg);
                        totalUsersReg = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsReg, totalUsersReg);
                        totalReg = GlobalMethods.GetTotalUserYearRegistrations(monthsReg);
                        totalUsrsReg += totalReg;

                        decimal avgReg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalReg);
                        avgUsrsReg += avgReg;

                        #endregion

                        #region Not Registered Table

                        DataTable tblNotReg = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), notRegAccountStatus, session);

                        int[] monthsNotReg = GlobalMethods.FillMonthsTableWithTableValues(tblNotReg);
                        totalUsersNotReg = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsNotReg, totalUsersNotReg);
                        totalNotReg = GlobalMethods.GetTotalUserYearRegistrations(monthsNotReg);
                        totalUsrsNotReg += totalNotReg;

                        decimal avgNotReg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalNotReg);
                        avgUsrsNotReg += avgNotReg;

                        #endregion

                        #region All Users Table

                        DataTable tblAll = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear(table0.Rows[i][0].ToString(), allAccountStatus, session);

                        int[] monthsAll = GlobalMethods.FillMonthsTableWithTableValues(tblAll);
                        totalUsersAll = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsAll, totalUsersAll);
                        totalAll = GlobalMethods.GetTotalUserYearRegistrations(monthsAll);
                        totalUsrsAll += totalAll;

                        decimal avgAll = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalAll);
                        avgUsrsAll += avgAll;

                        #endregion

                        decimal jan1 = ((Convert.ToDecimal(monthsAll[0]) > 0)) ? Convert.ToDecimal(monthsReg[0]) / (Convert.ToDecimal(monthsAll[0])) * 100 : 0;
                        decimal jan2 = ((Convert.ToDecimal(monthsAll[1]) > 0)) ? Convert.ToDecimal(monthsReg[1]) / (Convert.ToDecimal(monthsAll[1])) * 100 : 0;
                        decimal jan3 = ((Convert.ToDecimal(monthsAll[2]) > 0)) ? Convert.ToDecimal(monthsReg[2]) / (Convert.ToDecimal(monthsAll[2])) * 100 : 0;
                        decimal jan4 = ((Convert.ToDecimal(monthsAll[3]) > 0)) ? Convert.ToDecimal(monthsReg[3]) / (Convert.ToDecimal(monthsAll[3])) * 100 : 0;
                        decimal jan5 = ((Convert.ToDecimal(monthsAll[4]) > 0)) ? Convert.ToDecimal(monthsReg[4]) / (Convert.ToDecimal(monthsAll[4])) * 100 : 0;
                        decimal jan6 = ((Convert.ToDecimal(monthsAll[5]) > 0)) ? Convert.ToDecimal(monthsReg[5]) / (Convert.ToDecimal(monthsAll[5])) * 100 : 0;
                        decimal jan7 = ((Convert.ToDecimal(monthsAll[6]) > 0)) ? Convert.ToDecimal(monthsReg[6]) / (Convert.ToDecimal(monthsAll[6])) * 100 : 0;
                        decimal jan8 = ((Convert.ToDecimal(monthsAll[7]) > 0)) ? Convert.ToDecimal(monthsReg[7]) / (Convert.ToDecimal(monthsAll[7])) * 100 : 0;
                        decimal jan9 = ((Convert.ToDecimal(monthsAll[8]) > 0)) ? Convert.ToDecimal(monthsReg[8]) / (Convert.ToDecimal(monthsAll[8])) * 100 : 0;
                        decimal jan10 = ((Convert.ToDecimal(monthsAll[9]) > 0)) ? Convert.ToDecimal(monthsReg[9]) / (Convert.ToDecimal(monthsAll[9])) * 100 : 0;
                        decimal jan11 = ((Convert.ToDecimal(monthsAll[10]) > 0)) ? Convert.ToDecimal(monthsReg[10]) / (Convert.ToDecimal(monthsAll[10])) * 100 : 0;
                        decimal jan12 = ((Convert.ToDecimal(monthsAll[11]) > 0)) ? Convert.ToDecimal(monthsReg[11]) / (Convert.ToDecimal(monthsAll[11])) * 100 : 0;

                        table.Rows.Add((table0.Rows[i][0]).ToString(), Math.Round(jan1, 2) + " %", Math.Round(jan2, 2) + " %", Math.Round(jan3, 2) + " %", Math.Round(jan4, 2) + " %", Math.Round(jan5, 2) + " %", Math.Round(jan6, 2) + " %", Math.Round(jan7, 2) + " %", Math.Round(jan8, 2) + " %", Math.Round(jan9, 2) + " %", Math.Round(jan10, 2) + " %", Math.Round(jan11, 2) + " %", Math.Round(jan12, 2) + " %", (totalAll > 0) ? Math.Round(Convert.ToDecimal(totalReg) * 100 / totalAll, 2) + " % " : "0 %");
                    }

                    //table.Rows.Add("Total", totalUsers[0], totalUsers[1], totalUsers[2], totalUsers[3], totalUsers[4], totalUsers[5], totalUsers[6], totalUsers[7], totalUsers[8], totalUsers[9], totalUsers[10], totalUsers[11], totalUsrs, Math.Round(Convert.ToDecimal(total / DateTime.Now.Month), 2));

                    RdgRegVsAll.DataSource = table;
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

        protected void RdgNotRegVsAll_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgNotRegVsAll.Visible = true;

                DataTable table0 = StatisticsSql.GetUsersRegistrationYears(session);

                DataTable table = new DataTable();

                table.Columns.Add("year");
                table.Columns.Add("month_1");
                table.Columns.Add("month_2");
                table.Columns.Add("month_3");
                table.Columns.Add("month_4");
                table.Columns.Add("month_5");
                table.Columns.Add("month_6");
                table.Columns.Add("month_7");
                table.Columns.Add("month_8");
                table.Columns.Add("month_9");
                table.Columns.Add("month_10");
                table.Columns.Add("month_11");
                table.Columns.Add("month_12");
                table.Columns.Add("sum");
                //table.Columns.Add("avg");

                if (table0.Rows.Count > 0)
                {
                    #region Registered

                    int[] totalUsersReg = new int[12];
                    int totalUsrsReg = 0;
                    decimal avgUsrsReg = 0;
                    int totalReg = 0;

                    #endregion

                    #region Not Registered

                    int[] totalUsersNotReg = new int[12];
                    int totalUsrsNotReg = 0;
                    decimal avgUsrsNotReg = 0;
                    int totalNotReg = 0;

                    #endregion

                    #region All

                    int[] totalUsersAll = new int[12];
                    int totalUsrsAll = 0;
                    decimal avgUsrsAll = 0;
                    int totalAll = 0;

                    #endregion

                    string regAccountStatus = "1";
                    string notRegAccountStatus = "0";
                    string allAccountStatus = "0,1";

                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        #region Registered Users Table

                        DataTable tblReg = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), regAccountStatus, session);

                        int[] monthsReg = GlobalMethods.FillMonthsTableWithTableValues(tblReg);
                        totalUsersReg = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsReg, totalUsersReg);
                        totalReg = GlobalMethods.GetTotalUserYearRegistrations(monthsReg);
                        totalUsrsReg += totalReg;

                        decimal avgReg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalReg);
                        avgUsrsReg += avgReg;

                        #endregion

                        #region Not Registered Table

                        DataTable tblNotReg = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), notRegAccountStatus, session);

                        int[] monthsNotReg = GlobalMethods.FillMonthsTableWithTableValues(tblNotReg);
                        totalUsersNotReg = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsNotReg, totalUsersNotReg);
                        totalNotReg = GlobalMethods.GetTotalUserYearRegistrations(monthsNotReg);
                        totalUsrsNotReg += totalNotReg;

                        decimal avgNotReg = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalNotReg);
                        avgUsrsNotReg += avgNotReg;

                        #endregion

                        #region All Users Table

                        DataTable tblAll = StatisticsSql.GetCountOfUsersRegistrationMonthsByYear((table0.Rows[i][0]).ToString(), allAccountStatus, session);

                        int[] monthsAll = GlobalMethods.FillMonthsTableWithTableValues(tblAll);
                        totalUsersAll = GlobalMethods.FillUsersMonthsTableWithTableValues(monthsAll, totalUsersAll);
                        totalAll = GlobalMethods.GetTotalUserYearRegistrations(monthsAll);
                        totalUsrsAll += totalAll;

                        decimal avgAll = GlobalMethods.GetAverageUserYearRegistrations(Convert.ToInt32(table0.Rows[i][0]), totalAll);
                        avgUsrsAll += avgAll;

                        #endregion

                        decimal jan1 = ((Convert.ToDecimal(monthsAll[0]) > 0)) ? Convert.ToDecimal(monthsNotReg[0]) / (Convert.ToDecimal(monthsAll[0])) * 100 : 0;
                        decimal jan2 = ((Convert.ToDecimal(monthsAll[1]) > 0)) ? Convert.ToDecimal(monthsNotReg[1]) / (Convert.ToDecimal(monthsAll[1])) * 100 : 0;
                        decimal jan3 = ((Convert.ToDecimal(monthsAll[2]) > 0)) ? Convert.ToDecimal(monthsNotReg[2]) / (Convert.ToDecimal(monthsAll[2])) * 100 : 0;
                        decimal jan4 = ((Convert.ToDecimal(monthsAll[3]) > 0)) ? Convert.ToDecimal(monthsNotReg[3]) / (Convert.ToDecimal(monthsAll[3])) * 100 : 0;
                        decimal jan5 = ((Convert.ToDecimal(monthsAll[4]) > 0)) ? Convert.ToDecimal(monthsNotReg[4]) / (Convert.ToDecimal(monthsAll[4])) * 100 : 0;
                        decimal jan6 = ((Convert.ToDecimal(monthsAll[5]) > 0)) ? Convert.ToDecimal(monthsNotReg[5]) / (Convert.ToDecimal(monthsAll[5])) * 100 : 0;
                        decimal jan7 = ((Convert.ToDecimal(monthsAll[6]) > 0)) ? Convert.ToDecimal(monthsNotReg[6]) / (Convert.ToDecimal(monthsAll[6])) * 100 : 0;
                        decimal jan8 = ((Convert.ToDecimal(monthsAll[7]) > 0)) ? Convert.ToDecimal(monthsNotReg[7]) / (Convert.ToDecimal(monthsAll[7])) * 100 : 0;
                        decimal jan9 = ((Convert.ToDecimal(monthsAll[8]) > 0)) ? Convert.ToDecimal(monthsNotReg[8]) / (Convert.ToDecimal(monthsAll[8])) * 100 : 0;
                        decimal jan10 = ((Convert.ToDecimal(monthsAll[9]) > 0)) ? Convert.ToDecimal(monthsNotReg[9]) / (Convert.ToDecimal(monthsAll[9])) * 100 : 0;
                        decimal jan11 = ((Convert.ToDecimal(monthsAll[10]) > 0)) ? Convert.ToDecimal(monthsNotReg[10]) / (Convert.ToDecimal(monthsAll[10])) * 100 : 0;
                        decimal jan12 = ((Convert.ToDecimal(monthsAll[11]) > 0)) ? Convert.ToDecimal(monthsNotReg[11]) / (Convert.ToDecimal(monthsAll[11])) * 100 : 0;

                        table.Rows.Add((table0.Rows[i][0]).ToString(), Math.Round(jan1, 2) + " %", Math.Round(jan2, 2) + " %", Math.Round(jan3, 2) + " %", Math.Round(jan4, 2) + " %", Math.Round(jan5, 2) + " %", Math.Round(jan6, 2) + " %", Math.Round(jan7, 2) + " %", Math.Round(jan8, 2) + " %", Math.Round(jan9, 2) + " %", Math.Round(jan10, 2) + " %", Math.Round(jan11, 2) + " %", Math.Round(jan12, 2) + " %", (totalAll > 0) ? Math.Round(Convert.ToDecimal(totalNotReg) * 100 / totalAll, 2) + " % " : "0 %");
                    }

                    //table.Rows.Add("Total", totalUsers[0], totalUsers[1], totalUsers[2], totalUsers[3], totalUsers[4], totalUsers[5], totalUsers[6], totalUsers[7], totalUsers[8], totalUsers[9], totalUsers[10], totalUsers[11], totalUsrs, Math.Round(Convert.ToDecimal(total / DateTime.Now.Month), 2));

                    RdgNotRegVsAll.DataSource = table;
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

        protected void RdgAllTypes_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                RdgAllTypes.Visible = true;

                DataTable table0 = Sql.GetCompanyRegistrationForChartPerYear(session);

                DataTable table = new DataTable();

                table.Columns.Add("type");
                table.Columns.Add("year2014");
                table.Columns.Add("year2015");
                table.Columns.Add("vs");

                if (table0.Rows.Count > 0)
                {
                    for (int i = 0; i < table0.Rows.Count; i++)
                    {
                        table.Rows.Add("", "", "", "");
                    }

                    RdgAllTypes.DataSource = table;
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

        #endregion
    }
}