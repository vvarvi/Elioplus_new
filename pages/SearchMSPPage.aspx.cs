using System;
using System.Web.UI.HtmlControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Objects;
using System.Data;

namespace WdS.ElioPlus.pages
{
    public partial class SearchMSPPage : System.Web.UI.Page
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    FillRepeaters();

                SetBreadCrumb();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void SetBreadCrumb()
        {
            aBCrHome.HRef = ControlLoader.SearchForVendors;
            Label lbl = new Label();
            lbl.Text = "Saas Partner Programs" + " / ";
            aBCrHome.Controls.Add(lbl);

            aBCrCategory.HRef = ControlLoader.ManagedServiceProvidersPartnerPrograms;

            Label lblCategory = new Label();
            lblCategory.Text = "MSPs Partner Programs";
            aBCrCategory.Controls.Add(lblCategory);
        }

        private void LoadUrls()
        {
            FillRepeater(RptSalesMarketing, 1); // Sales & Marketing
            FillRepeater(RptCustomerManagement, 2); // Customer Management
            FillRepeater(RptProjectManagement, 3); // Project Management
            FillRepeater(RptOperationsWorkflow, 4); // Operations & Workflow
            FillRepeater(RptTrackingMeasurement, 5); // Tracking & Measurement
            FillRepeater(RptAccountingFinancials, 6); // Accounting & Financials
            FillRepeater(RptHR, 7); // HR
            FillRepeater(RptWebMobileSoftwareDevelopment, 8); // Web Mobile Software Development
            FillRepeater(RptItInfrastructure, 9); // IT & Infrastructure
            FillRepeater(RptBusinessUtilities, 10); // Business Utilities
            FillRepeater(RptDataSecurityGRC, 11); // Data Security & GRC
            FillRepeater(RptDesignMultimedia, 12); // Design & Multimedia
            FillRepeater(RptMiscellanious, 13); // Miscellaneous
            FillRepeater(RptUnifiedCommunications, 14); // Unified Communications
            FillRepeater(RptCADPLM, 15); // CAD & PLM
        }

        private void FillRepeaters()
        {
            try
            {
                session.OpenConnection();

                DataSet ds = Sql.GetSubIndustriesGroupItemssForAllIndustryGroupsByLinkPrefix("/msps/vendors/", session);
                foreach (DataTable tbl in ds.Tables)
                {
                    switch (Convert.ToInt32(tbl.Rows[0]["id"]))
                    {
                        case 1:
                            RptSalesMarketing.DataSource = tbl;
                            RptSalesMarketing.DataBind();
                            break;

                        case 2:
                            RptCustomerManagement.DataSource = tbl;
                            RptCustomerManagement.DataBind();
                            break;

                        case 3:
                            RptProjectManagement.DataSource = tbl;
                            RptProjectManagement.DataBind();
                            break;

                        case 4:
                            RptOperationsWorkflow.DataSource = tbl;
                            RptOperationsWorkflow.DataBind();
                            break;

                        case 5:
                            RptTrackingMeasurement.DataSource = tbl;
                            RptTrackingMeasurement.DataBind();
                            break;

                        case 6:
                            RptAccountingFinancials.DataSource = tbl;
                            RptAccountingFinancials.DataBind();
                            break;

                        case 7:
                            RptHR.DataSource = tbl;
                            RptHR.DataBind();
                            break;

                        case 8:
                            RptWebMobileSoftwareDevelopment.DataSource = tbl;
                            RptWebMobileSoftwareDevelopment.DataBind();
                            break;

                        case 9:
                            RptItInfrastructure.DataSource = tbl;
                            RptItInfrastructure.DataBind();
                            break;

                        case 10:
                            RptBusinessUtilities.DataSource = tbl;
                            RptBusinessUtilities.DataBind();
                            break;

                        case 11:
                            RptDataSecurityGRC.DataSource = tbl;
                            RptDataSecurityGRC.DataBind();
                            break;

                        case 12:
                            RptDesignMultimedia.DataSource = tbl;
                            RptDesignMultimedia.DataBind();
                            break;

                        case 13:
                            RptMiscellanious.DataSource = tbl;
                            RptMiscellanious.DataBind();
                            break;

                        case 14:
                            RptUnifiedCommunications.DataSource = tbl;
                            RptUnifiedCommunications.DataBind();
                            break;

                        case 15:
                            RptCADPLM.DataSource = tbl;
                            RptCADPLM.DataBind();
                            break;

                        //case 16:
                        //    RptHardware.DataSource = tbl;
                        //    RptHardware.DataBind();
                        //    break;
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

        private void FillRepeater(Repeater rpt, int id)
        {
            Repeater repeat = rpt;
            DataTable tab = GetSubIndustriesById(id);
            repeat.DataSource = tab;
            repeat.DataBind();
        }

        private DataTable GetSubIndustriesById(int id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("sub_industry_group_id");
            dt.Columns.Add("description");
            dt.Columns.Add("link");

            session.OpenConnection();
            List<ElioSubIndustriesGroupItems> IndustryGroupItem = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(id, session);
            session.CloseConnection();

            foreach (var item in IndustryGroupItem)
            {
                string link = "/msps/vendors/" + item.Description.Replace("&", "and").Replace(" ", "_").ToLower();
                dt.Rows.Add(item.Id, item.SubIndustriesGroupId, item.Description, link.ToLower());
            }

            return dt;
        }

        private void UpdateStrings()
        {

        }
    }
}