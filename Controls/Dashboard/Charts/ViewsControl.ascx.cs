using System;
using System.Linq;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.DBQueries;
using Libero.FusionCharts;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls.Dashboard.Charts
{
    public partial class ViewsControl : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        UpdateStrings();
                    }

                    if (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted))
                    {
                        string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "viewcontrol", "label", "2")).Text;
                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Warning, true, true, true);
                        ViewsChart.Visible = false;
                    }
                    else
                    {
                        ShowChart();
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

        private void UpdateStrings()
        {
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "viewcontrol", "label", "1")).Text;
        }

        private void ShowChart()
        {
            DataTable dt = Sql.GetCompanyViewsByCompanyIdForChart(vSession.User.Id, session);

            dt.Columns.Add("datetime");

            foreach (DataRow row in dt.Rows)
            {
                row["datetime"] = string.Format("{0:dd/MM}", row["date"]);
            }

            LineChart lchart = new LineChart();
            lchart.Background.BgColor = "ffffff";
            lchart.Background.BgAlpha = 50;
            lchart.ChartTitles.Caption = "";
            lchart.Template = new Libero.FusionCharts.Template.OceanTemplate();
            lchart.DataSource = dt;
            lchart.DataTextField = "datetime";
            lchart.DataValueField = "views";
            lchart.Canvas2D.CanvasBgColor = "f25a23";
            lchart.NumberFormat.DecimalPrecision = 0;

            ViewsChart.ShowChart(lchart);
            ViewsChart.Visible = true;
        }

        #endregion
    }
}