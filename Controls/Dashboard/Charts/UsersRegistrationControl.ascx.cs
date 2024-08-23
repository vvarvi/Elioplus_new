using System;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.DBQueries;
using Libero.FusionCharts;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using Telerik.Web.UI;
using System.Web.UI.WebControls;

namespace WdS.ElioPlus.Controls.Dashboard.Charts
{
    public partial class UsersRegistrationControl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Sql.IsUserAdministrator(vSession.User.Id, session))
                    {
                        ShowUserRegistrationChart(vSession.SelectedValueForUserRegistrationChart, false);

                        if (!IsPostBack)
                        {
                            UpdateStrings();

                            LoadComboValues();
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

        #region Methods

        private void LoadComboValues()
        {
            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "usersregistrationcontrol", "label", "2")).Text;

            RcbxUserReg.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();
            item1.Value = "1";
            item1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "usersregistrationcontrol", "label", "3")).Text;

            RcbxUserReg.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Value = "2";
            item2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "usersregistrationcontrol", "label", "4")).Text;

            RcbxUserReg.Items.Add(item2);

        }

        private void UpdateStrings()
        {
            LblUserPer.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "usersregistrationcontrol", "label", "1")).Text;

            Label lblBtnSubmitText = (Label)ControlFinder.FindControlRecursive(BtnSubmit, "LblBtnSubmitText");
            lblBtnSubmitText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "usersregistrationcontrol", "button", "1")).Text;
        }

        private void ShowUserRegistrationChart(int selectedItem, bool selectItem)
        {
            DataTable dt = new DataTable();            

            switch (selectedItem)
            {
                case 0:
                    dt = Sql.GetCompanyRegistrationForChartPerYear(session);
                    dt.Columns.Add("datetime");

                    foreach (DataRow row in dt.Rows)
                    {
                        row["datetime"] = row["year"];
                    }

                    break;

                case 1:
                    dt = Sql.GetCompanyRegistrationForChartPerMonth(session);
                    dt.Columns.Add("datetime");

                    foreach (DataRow row in dt.Rows)
                    {
                        row["datetime"] = row["month"] + "/" + row["year"];
                    }

                    break;

                case 2:
                    dt = Sql.GetCompanyRegistrationForChartPerDay(session);
                    dt.Columns.Add("datetime");

                    foreach (DataRow row in dt.Rows)
                    {
                        row["datetime"] = row["day"] + "/" + row["month"];
                    }

                    break;
            }

            LineChart lchart = new LineChart();
            lchart.Background.BgColor = "ffffff";
            lchart.Background.BgAlpha = 50;
            lchart.ChartTitles.Caption = "";
            lchart.Template = new Libero.FusionCharts.Template.OceanTemplate();
            lchart.DataSource = dt;
            lchart.DataTextField = "datetime";
            lchart.DataValueField = "count";
            lchart.Canvas2D.CanvasBgColor = "f25a23";
            lchart.NumberFormat.DecimalPrecision = 0;

            FUserRegChart.ShowChart(lchart);
            FUserRegChart.Visible = true;

            if (selectItem)
            {
                RcbxUserReg.FindItemByValue(selectedItem.ToString()).Selected = true;

                vSession.SelectedValueForUserRegistrationChart = selectedItem;
            }
        }

        #endregion

        #region Buttons

        protected void BtnSubmit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.SelectedValueForUserRegistrationChart = Convert.ToInt32(RcbxUserReg.SelectedValue);
                ShowUserRegistrationChart(vSession.SelectedValueForUserRegistrationChart,true);

                Response.Redirect(vSession.Page, false);                
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