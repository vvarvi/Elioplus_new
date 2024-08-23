using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.Design;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Dashboard.Components.Grid
{
    public partial class MyMatchesGridControl : System.Web.UI.UserControl
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public int CollaborationId
        {
            get
            {
                return (ViewState["CollaborationId"] != null) ? Convert.ToInt32(ViewState["CollaborationId"].ToString()) : -1;
            }
            set
            {
                ViewState["CollaborationId"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    session.OpenConnection();

                    if (!IsPostBack)
                    {
                        //LoadDealResultStatusOpen();
                        UcConnectionsMessageAlert.Visible = false;
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

        #region methods

        private void FillDataForExport(bool hasSearchCriteria)
        {
            //string strQueryExport = @"select 
            //                    u.company_name as 'COMPANY NAME',
            //                    ISNULL(u.first_name, up.given_name) as 'FIRST NAME',
            //                    ISNULL(u.last_name, up.family_name) as 'LAST NAME',
            //                    ISNULL(u.position, up.title) as 'POSITION-TITLE',
            //                    ISNULL(u.phone, up.phone) as 'PHONE',
            //                    up.time_zone AS 'TIMEZONE',
            //                    u.address AS 'ADDRESS',
            //                    u.email AS 'EMAIL', 
            //                    u.official_email AS 'OFFICIAL EMAIL', 
            //                    u.website AS 'WEBSITE',
            //                    u.vendor_product_demo_link AS 'PRODUCT DEMO LINK',
            //                    upc.sector AS 'SECTOR',
            //                    upc.industry AS 'INDUSTRY',
            //                    upc.industry_group AS 'INDUSTRY GROUP',
            //                    upc.sub_industry AS 'SUB INDUSTRY',
            //                    upc.founded_year AS 'FOUNDED YEAR',
            //                    upc.fund_amount AS 'FUND AMOUNT',
            //                    upc.employees_number AS 'EMPLOYEES NUMBER',
            //                    upc.employees_range AS 'EMPLOYEES RANGE NUMBER',
            //                    u.country AS 'COUNTRY'
            //                    from Elio_users_connections uc
            //                    inner join Elio_users u
            //        	            on u.id = uc.connection_id
            //                    left join Elio_users_person up
            //        	            on u.id = up.user_id
            //                    left join dbo.Elio_users_person_companies upc
            //        	            on u.id = upc.user_id and up.id = upc.elio_person_id
            //                    where uc.user_id=" + vSession.User.Id + " " +
            //"and can_be_viewed=1";

            //if (hasSearchCriteria)
            //{
            //    if (RdpConnectionsFrom.SelectedDate != null)
            //    {
            //        strQueryExport += " and uc.sysdate>='" + Convert.ToDateTime(RdpConnectionsFrom.SelectedDate).ToString("yyyy/MM/dd") + "'";
            //    }

            //    if (RdpConnectionsTo.SelectedDate != null)
            //    {
            //        strQueryExport += " and uc.sysdate<='" + Convert.ToDateTime(RdpConnectionsTo.SelectedDate).ToString("yyyy/MM/dd") + "'";
            //    }

            //    if (RtbxCompanyName.Text != "")
            //    {
            //        strQueryExport += " and company_name like '" + RtbxCompanyName.Text.Trim() + "%'";
            //    }
            //}

            //strQueryExport += " order by uc.sysdate desc";

            //DataTable exportTable = session.GetDataTable(strQueryExport);
            //if (exportTable != null && exportTable.Rows.Count > 0)
            //{
            //    vSession.ViewStateDataStore = new DataTable();
            //    vSession.ViewStateDataStore = exportTable;
            //    ImgBtnExport.Visible = true;
            //}
            //else
            //{
            //    vSession.ViewStateDataStore = null;
            //    ImgBtnExport.Visible = false;
            //}
        }

        public void Bind()
        {
            try
            {
                UcConnectionsMessageAlert.Visible = false;
                //divConnections.Visible = divConnectionsTableHolder.Visible = true;

                if (vSession.User != null)
                {
                    DataTable table = null;
                    if (ViewState["StrQuery"] == null)
                    {
                        table = Sql.GetUserConnectionsDetailsIJUsersIJPersonsIJCompaniesTable(vSession.User.Id, 1, session);
                        FillDataForExport(false);
                    }
                    else
                    {
                        table = session.GetDataTable(ViewState["StrQuery"].ToString());
                    }

                    if (table.Rows.Count > 0)
                    {
                        //divConnections.Visible = divConnectionsTableHolder.Visible = true;
                        //RdgConnections.Visible = true;

                        Rdg.Visible = true;
                        Rdg.DataSource = table;
                        Rdg.DataBind();

                        //LblConLogo.Text = "Logo";
                        //LblConName.Text = "Name";
                        //LblConEmail.Text = "E-mail";
                        //LblLinkedin.Text = "Contact Person";
                        //LblConDateStarted.Text = "Date";
                        //LblStatus.Text = "Status";
                        //LblConAdd.Text = "Actions";
                    }
                    else
                    {
                        //divConnections.Visible = false;
                        //RdgConnections.Visible = false;
                        //ImgBtnExport.Visible = false;
                        Rdg.Visible = false;
                        GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    //divConnections.Visible = divConnectionsTableHolder.Visible = false;
                    //RdgConnections.Visible = false;

                    GlobalMethods.ShowMessageControlDA(UcConnectionsMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "18")).Text, MessageTypes.Info, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        # endregion

        #region Grids

        protected void RdgDealsOpen_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        if (vSession.User != null)
                        {

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgDealsOpen_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    Bind();
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