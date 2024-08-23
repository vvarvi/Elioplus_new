using System;
using System.Linq;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Collections.Generic;
using System.Data;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Libero.FusionCharts;
using WdS.ElioPlus.Lib.Enums;
using System.Web;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.StripePayment;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus
{
    public partial class AdminStatisticsPage : Telerik.Web.UI.RadAjaxPage
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page ViewState properties

        public string SearchQueryString
        {
            get
            {
                return (this.ViewState["SearchQueryString"] != null) ? ViewState["SearchQueryString"].ToString() : string.Empty;
            }

            set { this.ViewState["SearchQueryString"] = value; }
        }

        #endregion

        private string CookieName
        {
            get
            {
                return "lgn";
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null && Sql.IsUserAdministrator(vSession.User.Id, session))
                {
                    UcMessageAlert.Visible = false;
                    UcStripeMessageAlert.Visible = false;
                    UcMessageControlCriteria.Visible = false;

                    UpdateStrings();

                    ShowChart();

                    if (!IsPostBack)
                    {
                        #region Load Data

                        LoadCompanyTypes();
                        LoadStatus();
                        LoadPublicStatus();
                        LoadBillingType();
                        LoadApplicationType();
                        LoadCompanies();
                        LoadDashboardData();

                        LoadCountries();
                        LoadVerticals();

                        #endregion
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

        private void LoadVerticals()
        {
            #region Find Controls

            CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
            CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
            CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
            CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
            CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
            CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
            CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
            CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
            CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
            CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
            CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
            CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
            CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
            CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
            CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
            CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
            CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
            CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
            CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
            CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
            CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
            CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
            CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
            CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
            CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
            CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
            CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
            CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
            CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
            CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
            CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
            CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
            CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
            CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
            CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
            CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");
            CheckBoxList cbSub37 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub37");
            CheckBoxList cbSub38 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub38");
            CheckBoxList cbSub39 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub39");
            CheckBoxList cbSub40 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub40");
            CheckBoxList cbSub41 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub41");
            CheckBoxList cbSub42 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub42");
            CheckBoxList cbSub43 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub43");
            CheckBoxList cbSub44 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub44");

            #endregion

            #region Sub Industries

            cbSub1.Items.Clear();
            cbSub2.Items.Clear();
            cbSub3.Items.Clear();
            cbSub4.Items.Clear();
            cbSub5.Items.Clear();
            cbSub6.Items.Clear();
            cbSub7.Items.Clear();
            cbSub8.Items.Clear();
            cbSub9.Items.Clear();
            cbSub10.Items.Clear();
            cbSub11.Items.Clear();
            cbSub12.Items.Clear();
            cbSub13.Items.Clear();
            cbSub14.Items.Clear();
            cbSub15.Items.Clear();
            cbSub16.Items.Clear();
            cbSub17.Items.Clear();
            cbSub18.Items.Clear();
            cbSub19.Items.Clear();
            cbSub20.Items.Clear();
            cbSub21.Items.Clear();
            cbSub22.Items.Clear();
            cbSub23.Items.Clear();
            cbSub24.Items.Clear();
            cbSub25.Items.Clear();
            cbSub26.Items.Clear();
            cbSub27.Items.Clear();
            cbSub28.Items.Clear();
            cbSub29.Items.Clear();
            cbSub30.Items.Clear();
            cbSub31.Items.Clear();
            cbSub32.Items.Clear();
            cbSub33.Items.Clear();
            cbSub34.Items.Clear();
            cbSub35.Items.Clear();
            cbSub36.Items.Clear();
            cbSub37.Items.Clear();
            cbSub38.Items.Clear();
            cbSub39.Items.Clear();
            cbSub40.Items.Clear();
            cbSub41.Items.Clear();
            cbSub42.Items.Clear();
            cbSub43.Items.Clear();
            cbSub44.Items.Clear();

            int count = 0;

            #region Group_1

            List<ElioSubIndustriesGroupItems> salesMarketingGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.SalesMarketing), session);
            foreach (ElioSubIndustriesGroupItems group in salesMarketingGroup)
            {
                ListItem item = new ListItem();

                if (count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                    //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                    cbSub1.Items.Add(item);
                    count++;
                }
                else if (count > 5 && count <= 11)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                    //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                    cbSub2.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();
                    //item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                    cbSub3.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_2

            count = 0;
            List<ElioSubIndustriesGroupItems> customerManagementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.CustomerManagement), session);
            foreach (ElioSubIndustriesGroupItems group in customerManagementGroup)
            {
                ListItem item = new ListItem();

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub4.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 3)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub5.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub6.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_3

            count = 0;
            List<ElioSubIndustriesGroupItems> projectManagementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.ProjectManagement), session);
            foreach (ElioSubIndustriesGroupItems group in projectManagementGroup)
            {
                ListItem item = new ListItem();

                if (count <= 0)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub7.Items.Add(item);
                    count++;
                }
                else if (count > 0 && count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub8.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub9.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_4

            count = 0;
            List<ElioSubIndustriesGroupItems> operationsWorkflowGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.OperationsWorkflow), session);
            foreach (ElioSubIndustriesGroupItems group in operationsWorkflowGroup)
            {
                ListItem item = new ListItem();

                if (count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub10.Items.Add(item);
                    count++;
                }
                else if (count > 5 && count <= 11)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub11.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub12.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_5

            count = 0;
            List<ElioSubIndustriesGroupItems> trackingMeasurementGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.TrackingMeasurement), session);
            foreach (ElioSubIndustriesGroupItems group in trackingMeasurementGroup)
            {
                ListItem item = new ListItem();

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub13.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 3)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub14.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub15.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_6

            count = 0;
            List<ElioSubIndustriesGroupItems> accountingFinancialsGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.AccountingFinancials), session);
            foreach (ElioSubIndustriesGroupItems group in accountingFinancialsGroup)
            {
                ListItem item = new ListItem();

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub16.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 3)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub17.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub18.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_7

            count = 0;
            List<ElioSubIndustriesGroupItems> hRGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.HR), session);
            foreach (ElioSubIndustriesGroupItems group in hRGroup)
            {
                ListItem item = new ListItem();

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub19.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 4)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub20.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub21.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_8

            count = 0;
            List<ElioSubIndustriesGroupItems> webMobileSoftwareDevelopmentGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.WebMobileSoftwareDevelopment), session);
            foreach (ElioSubIndustriesGroupItems group in webMobileSoftwareDevelopmentGroup)
            {
                ListItem item = new ListItem();

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub22.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub23.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub24.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_9

            count = 0;
            List<ElioSubIndustriesGroupItems> iTInfrastructureGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.ITInfrastructure), session);
            foreach (ElioSubIndustriesGroupItems group in iTInfrastructureGroup)
            {
                ListItem item = new ListItem();

                if (count <= 3)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub25.Items.Add(item);
                    count++;
                }
                else if (count > 3 && count <= 7)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub26.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub27.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_10

            count = 0;
            List<ElioSubIndustriesGroupItems> businessUtilitiesGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.BusinessUtilities), session);
            foreach (ElioSubIndustriesGroupItems group in businessUtilitiesGroup)
            {
                ListItem item = new ListItem();

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub28.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub29.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub30.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_11

            count = 0;
            List<ElioSubIndustriesGroupItems> securityBackupGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.DataSecurityGRC), session);
            foreach (ElioSubIndustriesGroupItems group in securityBackupGroup)
            {
                ListItem item = new ListItem();

                if (count <= 4)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub31.Items.Add(item);
                    count++;
                }
                else if (count > 4 && count <= 9)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub32.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub33.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_12

            count = 0;
            List<ElioSubIndustriesGroupItems> designMultimediaGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.DesignMultimedia), session);
            foreach (ElioSubIndustriesGroupItems group in designMultimediaGroup)
            {
                ListItem item = new ListItem();

                if (count <= 0)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub34.Items.Add(item);
                    count++;
                }
                else if (count > 0 && count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub35.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub36.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_13

            count = 0;
            List<ElioSubIndustriesGroupItems> miscellaneousGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.Miscellaneous), session);
            foreach (ElioSubIndustriesGroupItems group in miscellaneousGroup)
            {
                ListItem item = new ListItem();

                if (count <= 0)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub37.Items.Add(item);
                    count++;
                }
                else if (count > 0 && count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub38.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_14

            count = 0;
            List<ElioSubIndustriesGroupItems> unifiedCommunications = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.UnifiedCommunications), session);
            foreach (ElioSubIndustriesGroupItems group in unifiedCommunications)
            {
                ListItem item = new ListItem();

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub39.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub40.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub41.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #region Group_15

            count = 0;
            List<ElioSubIndustriesGroupItems> cadPlm = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.CADPLM), session);
            foreach (ElioSubIndustriesGroupItems group in cadPlm)
            {
                ListItem item = new ListItem();

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub42.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub43.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub44.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #endregion
        }

        private void LoadCountries()
        {
            RcbxCountries.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = "-- Select Country--";

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

            RcbxCountries.FindItemByValue("0").Selected = true;
        }

        private void LoadTypes(GridDataItem item)
        {
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            rcbxCategory.Items.Clear();

            List<ElioUserTypes> types = Sql.GetUserAllTypes(session);
            foreach (ElioUserTypes type in types)
            {
                RadComboBoxItem rcbxitem = new RadComboBoxItem();

                rcbxitem.Value = type.Id.ToString();
                rcbxitem.Text = type.Description;

                rcbxCategory.Items.Add(rcbxitem);
            }
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

            //ViewsChart.ShowChart(lchart);
            //ViewsChart.Visible = true;           
        }

        private void LoadDashboardData()
        {
            //LblTotalVendors.Text = Sql.GetTotalRegisteredCompaniesByType(Types.Vendors.ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();

            //LblTotalResellers.Text = Sql.GetTotalRegisteredCompaniesByType(EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            //LblTotalThirdPartyResellers.Text = Sql.GetTotalRegisteredCompaniesByType(EnumHelper.GetDescription(Types.Resellers).ToString(), Convert.ToInt32(UserApplicationType.ThirdParty), session).ToString();
            //LblTotalDevelopers.Text = Sql.GetTotalRegisteredCompaniesByType(Types.Developers.ToString(), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            //LblTotalRegistered.Text = Sql.GetTotalCompaniesByAccountStatus(Convert.ToInt32(AccountStatus.Completed), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
            //LblTotalNotRegistered.Text = Sql.GetTotalCompaniesByAccountStatus(Convert.ToInt32(AccountStatus.NotCompleted), Convert.ToInt32(UserApplicationType.Elioplus), session).ToString();
        }

        private void UpdateStrings()
        {
            //Label1.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "2")).Text;
            //LblVendors.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "38")).Text;
            //LblDevelopers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "36")).Text;
            //LblTResellers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "60")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "37")).Text;
            //LblTThirdPartyResellers.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "61")).Text + Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "37")).Text;
            //LblRegistered.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "3")).Text;
            //LblNotRegistered.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "4")).Text;

            Label5.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "17")).Text;
            Label6.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "2")).Text;
            LblStatus.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "5")).Text;
            LblIsPublic.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "6")).Text;
            Label8.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "7")).Text;
            Label2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "9")).Text;
            Label3.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "10")).Text;
            Label4.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "11")).Text;            
            Label7.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "12")).Text;
            Label9.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "15")).Text;
            Label10.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "label", "16")).Text;

            RdgElioUsers.MasterTableView.GetColumn("company_name").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "1")).Text;
            RdgElioUsers.MasterTableView.GetColumn("billing_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "36")).Text;
            RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "38")).Text;
            RdgElioUsers.MasterTableView.GetColumn("company_type").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "2")).Text;
            RdgElioUsers.MasterTableView.GetColumn("email").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "3")).Text;
            RdgElioUsers.MasterTableView.GetColumn("is_public").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "4")).Text;
            RdgElioUsers.MasterTableView.GetColumn("account_status").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "5")).Text;
            RdgElioUsers.MasterTableView.GetColumn("country").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "37")).Text;
            RdgElioUsers.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "1", "column", "12")).Text;

            Label lblUpSearchText = (Label)ControlFinder.FindControlRecursive(RbtnUpSearch, "LblUpSearchText");
            Label lblSearchText = (Label)ControlFinder.FindControlRecursive(RbtnSearch, "LblSearchText");
            lblSearchText.Text = lblUpSearchText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "11")).Text;
            Label lblUpResetText = (Label)ControlFinder.FindControlRecursive(RbtnUpReset, "LblUpResetText");
            Label lblResetText = (Label)ControlFinder.FindControlRecursive(RbtnReset, "LblResetText");
            lblResetText.Text = lblUpResetText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "10")).Text;
        }

        private bool FixUserBillingTypeStatus(RadTextBox rtbxCustomerId, ElioUsers user)
        {
            bool allow = false;

            if (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed))
            {
                if (rtbxCustomerId.Text != string.Empty)
                {
                    allow = true;
                }
            }

            return allow;
        }

        private void FixUsersGrid(GridDataItem item, bool isSaveMode, bool isCancelClicked)
        {
            string alert = string.Empty;

            UcMessageAlert.Visible = isSaveMode;
            UcStripeMessageAlert.Visible = isSaveMode;

            RdgElioUsers.MasterTableView.GetColumn("billing_type").Display = true;
            RdgElioUsers.MasterTableView.GetColumn("stripe_customer_id").Display = true;

            Label lblCategory = (Label)ControlFinder.FindControlRecursive(item, "LblCategory");
            Label lblBillingType = (Label)ControlFinder.FindControlRecursive(item, "LblBillingType");
            Label lblStripeCustomerId = (Label)ControlFinder.FindControlRecursive(item, "LblStripeCustomerId");
            Label lblStatus = (Label)ControlFinder.FindControlRecursive(item, "LblStatus");
            Label lblFeature = (Label)ControlFinder.FindControlRecursive(item, "LblFeature");
            RadComboBox rcbxStatus = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxStatus");
            Label lblPublic = (Label)ControlFinder.FindControlRecursive(item, "LblPublic");
            RadComboBox rcbxPublic = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxPublic");
            RadComboBox rcbxCategory = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail");
            RadTextBox rtbxFeature = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxFeature");
            RadComboBox rcbxBillingType = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxBillingType");
            RadTextBox rtbxStripeCustomerId = (RadTextBox)ControlFinder.FindControlRecursive(item, "RtbxStripeCustomerId");
            ImageButton imgBtnSaveChanges = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSaveChanges");
            ImageButton imgBtnEditCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEditCompany");
            ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");

            if (!isSaveMode)
            {
                LoadTypes(item);
            }

            ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
            if (user != null)
            {
                if (isSaveMode && !isCancelClicked)
                {
                    user.CompanyType = rcbxCategory.SelectedItem.Text;
                    user.MashapeUsername = (user.CompanyType != Types.Vendors.ToString()) ? "" : user.MashapeUsername;
                    user.AccountStatus = Convert.ToInt32(rcbxStatus.SelectedItem.Value);
                    user.FeaturesNo = (string.IsNullOrEmpty(rtbxFeature.Text)) ? 0 : Convert.ToInt32(rtbxFeature.Text);
                    user.LastUpdated = DateTime.Now;
                    user.IsPublic = Convert.ToInt32(rcbxPublic.SelectedValue);
                    user.CustomerStripeId = rtbxStripeCustomerId.Text;

                    if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text == string.Empty)
                    {
                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "6")).Text;
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                        return;
                    }
                    else
                    {
                        if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rcbxBillingType.SelectedItem.Value == Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString())
                        {
                            DateTime? canceledAt = null;
                            bool successUnsubscription = false;
                            string stripeUnsubscribeError = string.Empty;
                            string defaultCreditCard = string.Empty;

                            ElioUsersCreditCards userCard = Sql.GetUserDefaultCreditCard(vSession.User.Id, vSession.User.CustomerStripeId, session);
                            if (userCard != null)
                                defaultCreditCard = userCard.CardStripeId;

                            try
                            {
                                successUnsubscription = StripeLib.UnSubscribeCustomer(ref canceledAt, user.CustomerStripeId, defaultCreditCard, ref stripeUnsubscribeError);
                            }
                            catch (Exception ex)
                            {
                                GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            if (successUnsubscription)
                            {
                                try
                                {
                                    session.BeginTransaction();

                                    PaymentLib.MakeUserFreemium(user, canceledAt, userCard, session);

                                    session.CommitTransaction();
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    return;
                                }
                            }
                            else
                            {
                                GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, stripeUnsubscribeError, MessageTypes.Error, true, true, false);
                                return;
                            }
                        }
                        else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text != string.Empty && rcbxBillingType.SelectedItem.Value != Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString())
                        {
                            DateTime? startDate = null;
                            DateTime? currentPeriodStart = null;
                            DateTime? currentPeriodEnd = null;
                            DateTime? trialPeriodStart = null;
                            DateTime? trialPeriodEnd = null;
                            DateTime? canceledAt = null;
                            string orderMode = string.Empty;
                            Xamarin.Payments.Stripe.StripeCard cardInfo = null;

                            Xamarin.Payments.Stripe.StripeSubscription subscription = StripeLib.GetCustomerSubscriptionInfo(ref startDate, ref currentPeriodStart, ref currentPeriodEnd, ref trialPeriodStart, ref trialPeriodEnd, ref canceledAt, ref orderMode, rtbxStripeCustomerId.Text);

                            if (startDate != null && ((currentPeriodStart != null && currentPeriodEnd != null) || (trialPeriodStart != null && trialPeriodEnd != null)))
                            {
                                try
                                {
                                    session.BeginTransaction();

                                    user = PaymentLib.MakeUserPremium(user, Convert.ToInt32(Packets.Premium), null, rtbxStripeCustomerId.Text, "", lblEmail.Text, subscription.Start, subscription.CurrentPeriodStart, subscription.CurrentPeriodEnd, subscription.TrialStart, subscription.TrialEnd, orderMode, cardInfo, session);

                                    session.CommitTransaction();
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                    return;
                                }
                            }
                        }
                        else if (user.BillingType == Convert.ToInt32(BillingTypePacket.FreemiumPacketType) && rtbxStripeCustomerId.Text == string.Empty && rcbxBillingType.SelectedItem.Value != Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString())
                        {
                            try
                            {
                                session.BeginTransaction();

                                user = PaymentLib.MakeUserVirtualPremium(user, Convert.ToInt32(rcbxBillingType.SelectedItem.Value), rtbxStripeCustomerId.Text, lblEmail.Text, DateTime.Now, DateTime.Now, DateTime.Now.AddDays(14), DateTime.Now, DateTime.Now.AddDays(14), OrderMode.Trialing.ToString(), session);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                GlobalMethods.ShowMessageControlDA(UcStripeMessageAlert, ex.Message, MessageTypes.Error, true, true, false);
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                return;
                            }
                        }

                        user.BillingType = Convert.ToInt32(rcbxBillingType.SelectedItem.Value);

                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                        loader.Update(user);

                        RdgElioUsers.Rebind();

                        alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "1")).Text;

                        if (Sql.IsUserAdministrator(user.Id, session))
                        {
                            vSession.User = user;
                            alert += Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "2")).Text;
                        }

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
                    }
                }

                lblCategory.Visible = isSaveMode;
                lblStatus.Visible = isSaveMode;
                lblPublic.Visible = isSaveMode;
                rcbxPublic.Visible = !isSaveMode;
                lblFeature.Visible = isSaveMode;
                lblBillingType.Visible = isSaveMode;
                lblStripeCustomerId.Visible = isSaveMode;
                rcbxStatus.Visible = !isSaveMode;
                rcbxCategory.Visible = !isSaveMode;
                rtbxFeature.Visible = !isSaveMode;
                rcbxBillingType.Visible = !isSaveMode;
                rtbxStripeCustomerId.Visible = !isSaveMode;
                imgBtnSaveChanges.Visible = !isSaveMode;
                imgBtnCancel.Visible = !isSaveMode;
                imgBtnEditCompany.Visible = isSaveMode;

                rtbxFeature.Text = user.FeaturesNo.ToString();
                rcbxBillingType.FindItemByValue(user.BillingType.ToString()).Selected = true;
                rtbxStripeCustomerId.Text = user.CustomerStripeId;
                rcbxStatus.FindItemByValue(user.AccountStatus.ToString()).Selected = true;
                rcbxPublic.FindItemByValue(user.IsPublic.ToString()).Selected = true;
                rcbxCategory.FindItemByText(user.CompanyType).Selected = true;
            }
            else
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "3")).Text;

                RdgElioUsers.Rebind();

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);
            }
        }

        private void LoadCompanyTypes()
        {
            RcbxCategory.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "39")).Text;
            RcbxCategory.Items.Add(item);

            List<ElioUserTypes> types = Sql.GetUserAllTypes(session);
            foreach (ElioUserTypes type in types)
            {
                item = new RadComboBoxItem();

                item.Value = type.Id.ToString();
                item.Text = type.Description;

                RcbxCategory.Items.Add(item);
            }
        }

        private void LoadStatus()
        {
            RcbxStatus.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "-1";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "40")).Text;
            RcbxStatus.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();

            item1.Value = "0";
            item1.Text = AccountStatus.NotCompleted.ToString();
            RcbxStatus.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();

            item2.Value = "1";
            item2.Text = AccountStatus.Completed.ToString();
            RcbxStatus.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();

            item3.Value = "2";
            item3.Text = AccountStatus.Deleted.ToString();
            RcbxStatus.Items.Add(item3);

            RadComboBoxItem item4 = new RadComboBoxItem();

            item4.Value = "3";
            item4.Text = AccountStatus.Blocked.ToString();
            RcbxStatus.Items.Add(item4);
        }

        private void LoadPublicStatus()
        {
            RcbxIsPublic.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();

            item.Value = "-1";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "41")).Text;
            RcbxIsPublic.Items.Add(item);

            RadComboBoxItem item1 = new RadComboBoxItem();

            item1.Value = "1";
            item1.Text =Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "43")).Text;
            RcbxIsPublic.Items.Add(item1);

            RadComboBoxItem item2 = new RadComboBoxItem();

            item2.Value = "0";
            item2.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "44")).Text;
            RcbxIsPublic.Items.Add(item2);           
        }

        private void LoadBillingType()
        {
            RcbxBillingType.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "58")).Text;
            RcbxBillingType.Items.Add(item);

            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Value = Convert.ToInt32(BillingTypePacket.FreemiumPacketType).ToString();
            item2.Text = BillingTypePacket.FreemiumPacketType.ToString();
            RcbxBillingType.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();
            item3.Value = Convert.ToInt32(BillingTypePacket.PremiumPacketType).ToString();
            item3.Text = BillingTypePacket.PremiumPacketType.ToString();
            RcbxBillingType.Items.Add(item3);

            RadComboBoxItem item4 = new RadComboBoxItem();
            item4.Value = Convert.ToInt32(BillingTypePacket.PremiumStartupPacketType).ToString();
            item4.Text = BillingTypePacket.PremiumStartupPacketType.ToString();
            RcbxBillingType.Items.Add(item4);

            RadComboBoxItem item5 = new RadComboBoxItem();
            item5.Value = Convert.ToInt32(BillingTypePacket.PremiumGrowthPacketType).ToString();
            item5.Text = BillingTypePacket.PremiumGrowthPacketType.ToString();
            RcbxBillingType.Items.Add(item5);

            RadComboBoxItem item6 = new RadComboBoxItem();
            item6.Value = Convert.ToInt32(BillingTypePacket.PremiumEnterprisePacketType).ToString();
            item6.Text = BillingTypePacket.PremiumEnterprisePacketType.ToString();
            RcbxBillingType.Items.Add(item6);

            RadComboBoxItem item7 = new RadComboBoxItem();
            item7.Value = Convert.ToInt32(BillingTypePacket.PremiumSelfServicePacketType).ToString();
            item7.Text = BillingTypePacket.PremiumSelfServicePacketType.ToString();
            RcbxBillingType.Items.Add(item7);
        }

        private void LoadApplicationType()
        {
            RcbxApplicationType.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "59")).Text;
            RcbxApplicationType.Items.Add(item);

            RadComboBoxItem item2 = new RadComboBoxItem();
            item2.Value = Convert.ToInt32(UserApplicationType.Elioplus).ToString();
            item2.Text = UserApplicationType.Elioplus.ToString();
            RcbxApplicationType.Items.Add(item2);

            RadComboBoxItem item3 = new RadComboBoxItem();
            item3.Value = Convert.ToInt32(UserApplicationType.ThirdParty).ToString();
            item3.Text = UserApplicationType.ThirdParty.ToString();
            RcbxApplicationType.Items.Add(item3);
        }

        private void LoadCompanies()
        {
            List<ElioUsers> companies = Sql.GetAllFullRegisteredUsers(session);

            RcbxName.Items.Clear();

            RadComboBoxItem item = new RadComboBoxItem();
            item.Value = "0";
            item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "42")).Text;
            RcbxName.Items.Add(item);

            foreach (ElioUsers company in companies)
            {
                item = new RadComboBoxItem();
                item.Value = company.Id.ToString();
                item.Text = company.CompanyName;

                RcbxName.Items.Add(item);
            }
        }

        private DataTable RetrieveSpecificTypeOfUsers(int userId, DBSession session)
        {
            DataTable table = new DataTable();

            List<ElioUsers> users = Sql.GetVendorsConnectionsByUserId(userId, session);
            if (users.Count > 0)
            {
                table.Columns.Add("id");
                table.Columns.Add("connection_id");
                table.Columns.Add("company_name");
                table.Columns.Add("country");
                table.Columns.Add("email");
                table.Columns.Add("sysdate");
                table.Columns.Add("last_updated");
                table.Columns.Add("current_period_start");
                table.Columns.Add("current_period_end");
                table.Columns.Add("status");

                foreach (ElioUsers user in users)
                {
                    table.Rows.Add(userId, user.Id, user.CompanyName, user.Country, user.Email, "", "", "", "", "");
                }
            }
            return table;
        }

        #endregion

        #region Grids
        
        protected void RdgElioUsers_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    #region Parent Items

                    GridDataItem item = (GridDataItem)e.Item;

                    ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (user != null)
                    {
                        RdgElioUsers.MasterTableView.GetColumn("company_name").Display = (RcbxName.SelectedValue != "0" || user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("account_status").Display = (RcbxStatus.SelectedValue != "-1") ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("company_type").Display = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("is_public").Display = (RcbxIsPublic.SelectedValue != "-1" || RcbxStatus.SelectedValue == "0") ? false : true;
                        RdgElioUsers.MasterTableView.GetColumn("billing_type").Display = (RcbxBillingType.SelectedValue != "0") ? false : true;

                        Label lblEmail = (Label)ControlFinder.FindControlRecursive(item, "LblEmail");
                        RadComboBox rcbxEmail = (RadComboBox)ControlFinder.FindControlRecursive(item, "RcbxEmail");

                        ImageButton imgBtnLoginAsCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnLoginAsCompany");
                        imgBtnLoginAsCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "4")).Text.Replace("{comapnyname}", (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed) ? user.CompanyName : user.Username));

                        ImageButton imgBtnPreviewCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreviewCompany");
                        imgBtnPreviewCompany.ToolTip = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "tooltip", "5")).Text;

                        imgBtnPreviewCompany.Visible = (user.AccountStatus == Convert.ToInt32(AccountStatus.NotCompleted)) ? false : true;

                        Image imgInfo = (Image)ControlFinder.FindControlRecursive(item, "ImgInfo");
                        RadToolTip rttImgInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttImgInfo");
                        rttImgInfo.Text = user.Description + "</br></br>" + "<h3>Company Overview</h3>" + "</br>" + user.Overview;
                        rttImgInfo.Title = "Company Description/Overview";

                        HtmlAnchor aShowVerticals = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aShowVerticals");
                        aShowVerticals.Visible = Sql.HasUserVerticals(user.Id, session);

                        if (!string.IsNullOrEmpty(user.OfficialEmail) && user.Email != user.OfficialEmail)
                        {
                            rcbxEmail.Items.Clear();
                            RadComboBoxItem rcbxitem = new RadComboBoxItem();

                            rcbxitem.Value = "0";
                            rcbxitem.Text = user.Email;

                            rcbxEmail.Items.Add(rcbxitem);

                            RadComboBoxItem rcbxitem1 = new RadComboBoxItem();
                            rcbxitem1.Value = "1";
                            rcbxitem1.Text = user.OfficialEmail;

                            rcbxEmail.Items.Add(rcbxitem1);
                        }
                        else
                        {
                            lblEmail.Text = user.Email;
                        }

                        rcbxEmail.Visible = (!string.IsNullOrEmpty(user.OfficialEmail) && user.Email != user.OfficialEmail) ? true : false;
                        lblEmail.Visible = (rcbxEmail.Visible) ? false : true;
                    }

                    #endregion
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "CompanyItems")
                {
                    #region Resellers

                    GridDataItem item = (GridDataItem)e.Item;
                    
                    int userId = Convert.ToInt32(item["id"].Text);

                    ElioUsers user = Sql.FindUserType(userId, session);

                    if (item["connection_id"].Text != "&nbsp;")
                    {
                        int connectionId = Convert.ToInt32(item["connection_id"].Text);

                        Image imgStatus = (Image)ControlFinder.FindControlRecursive(item, "ImgStatus");

                        int isUserId = (user.CompanyType == Types.Vendors.ToString()) ? userId : connectionId;
                        int isConnectionId = (user.CompanyType == Types.Vendors.ToString()) ? connectionId : userId;

                        ElioUsersConnections connection = Sql.GetConnection(isUserId, isConnectionId, session);

                        if (connection != null)
                        {
                            item["current_period_start"].Text = (string.IsNullOrEmpty(item["current_period_start"].Text)) ? connection.CurrentPeriodStart.ToString("MM/dd/yyyy") : "-";
                            item["current_period_end"].Text = (string.IsNullOrEmpty(item["current_period_end"].Text)) ? connection.CurrentPeriodEnd.ToString("MM/dd/yyyy") : "-";
                            
                            imgStatus.Visible = true;
                            imgStatus.ImageUrl = (connection.Status) ? "~/images/icons/small/success.png" : "~/images/icons/small/error.png";

                            HtmlAnchor aNestedShowVerticals = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aNestedShowVerticals");
                            aNestedShowVerticals.Visible = Sql.HasUserVerticals(connection.ConnectionId, session);
                        }
                        else
                        {
                            item["current_period_start"].Text = "-";
                            item["current_period_end"].Text = "-";
                            imgStatus.Visible = false;
                        }
                    }

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

        protected void RdgElioUsers_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (SearchQueryString != string.Empty)
                {
                    //SearchQueryString = "Select * From Elio_users with (nolock) order by id";

                    DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                    List<ElioUsers> users = loader.Load(SearchQueryString);

                    if (users.Count > 0)
                    {
                        RdgElioUsers.Visible = true;
                        UcMessageAlert.Visible = false;

                        DataTable table = new DataTable();

                        table.Columns.Add("id");
                        table.Columns.Add("company_name");
                        table.Columns.Add("billing_type");
                        table.Columns.Add("stripe_customer_id");
                        table.Columns.Add("company_type");
                        table.Columns.Add("email");
                        table.Columns.Add("is_public");
                        table.Columns.Add("account_status");
                        table.Columns.Add("country");

                        foreach (ElioUsers user in users)
                        {
                            string packetType = "Freemium type user";
                            if (user.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
                            {
                                ElioPackets packet = Sql.GetPacketByUserBillingTypePacketId(user.BillingType, session);
                                if (packet != null)
                                    packetType = packet.PackDescription + "type user";
                            }

                            table.Rows.Add(user.Id, user.CompanyName, packetType, user.CustomerStripeId, user.CompanyType, user.Email, (user.IsPublic == Convert.ToInt32(AccountPublicStatus.IsPublic)) ? "Public" : "Not Public", (user.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? "Completed" : "Not Completed", user.Country);
                        }

                        RdgElioUsers.DataSource = table;
                    }
                    else
                    {
                        RdgElioUsers.Visible = false;

                        string alert = "You have no company profiles to cover your seach criteria";
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                    }
                }
                else
                {
                    RdgElioUsers.Visible = false;

                    string alert = "Search company profiles by above criteria";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
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

        protected void RdgElioUsers_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "CompanyItems":
                        {
                            int userId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            ElioUsers user = Sql.FindUserType(Convert.ToInt32(userId), session);
                            if (user != null)
                            {
                                if (user.CompanyType == Types.Vendors.ToString())
                                {
                                    e.DetailTableView.Visible = true;
                                    e.DetailTableView.DataSource = RetrieveSpecificTypeOfUsers(userId, session);
                                }
                                else if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    e.DetailTableView.DataSource = null;    //RetrieveSpecificTypeOfUsers(userId, Types.Vendors.ToString(), session);
                                    e.DetailTableView.Visible = false;
                                }
                                else if (user.CompanyType == Types.Developers.ToString())
                                {
                                    e.DetailTableView.DataSource = null;
                                    e.DetailTableView.Visible = false;
                                }
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;
                            }
                        }

                        break;
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

        protected void RdgElioUsers_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        #region Buttons

        protected void ImgShowVerticals_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                TbxVerticals.Text = string.Empty;

                List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> verticals = Sql.GetUserSubcategoriesById(Convert.ToInt32(item["Id"].Text), session);
                foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers vertical in verticals)
                {
                    TbxVerticals.Text += vertical.DescriptionSubcategory + ",";
                }

                TbxVerticals.Text = (verticals.Count > 0) ? TbxVerticals.Text.Substring(0, TbxVerticals.Text.Length - 1) : "-";
                
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPopUp();", true);
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

        protected void ImgNestedShowVerticals_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aBtn.NamingContainer;

                TbxVerticals.Text = string.Empty;

                List<ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers> verticals = Sql.GetUserSubcategoriesById(Convert.ToInt32(item["connection_id"].Text), session);
                foreach (ElioUsersSubIndustriesGroupItemsIJElioSubIndustriesGroupItemsIJUsers vertical in verticals)
                {
                    TbxVerticals.Text += vertical.DescriptionSubcategory + ",";
                }

                TbxVerticals.Text = (verticals.Count > 0) ? TbxVerticals.Text.Substring(0, TbxVerticals.Text.Length - 1) : "-";
                
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenPopUp();", true);
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

        protected void RbtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                bool hasSelectedCriteria = false;
                SearchQueryString = "Select * From Elio_users with (nolock) ";

                #region Find Controls

                CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
                CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
                CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
                CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
                CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
                CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
                CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
                CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
                CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
                CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
                CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
                CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
                CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
                CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
                CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
                CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
                CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
                CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
                CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
                CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
                CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
                CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
                CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
                CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
                CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
                CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
                CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
                CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
                CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
                CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
                CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
                CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
                CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
                CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
                CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
                CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");
                CheckBoxList cbSub37 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub37");
                CheckBoxList cbSub38 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub38");
                CheckBoxList cbSub39 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub39");
                CheckBoxList cbSub40 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub40");
                CheckBoxList cbSub41 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub41");
                CheckBoxList cbSub42 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub42");
                CheckBoxList cbSub43 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub43");
                CheckBoxList cbSub44 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub44");

                #endregion

                #region Check CheckBox Lists

                List<CheckBoxList> cbxSubIndustryList = new List<CheckBoxList>();

                cbxSubIndustryList.Add(cbSub1);
                cbxSubIndustryList.Add(cbSub2);
                cbxSubIndustryList.Add(cbSub3);
                cbxSubIndustryList.Add(cbSub4);
                cbxSubIndustryList.Add(cbSub5);
                cbxSubIndustryList.Add(cbSub6);
                cbxSubIndustryList.Add(cbSub7);
                cbxSubIndustryList.Add(cbSub8);
                cbxSubIndustryList.Add(cbSub9);
                cbxSubIndustryList.Add(cbSub10);
                cbxSubIndustryList.Add(cbSub11);
                cbxSubIndustryList.Add(cbSub12);
                cbxSubIndustryList.Add(cbSub13);
                cbxSubIndustryList.Add(cbSub14);
                cbxSubIndustryList.Add(cbSub15);
                cbxSubIndustryList.Add(cbSub16);
                cbxSubIndustryList.Add(cbSub17);
                cbxSubIndustryList.Add(cbSub18);
                cbxSubIndustryList.Add(cbSub19);
                cbxSubIndustryList.Add(cbSub20);
                cbxSubIndustryList.Add(cbSub21);
                cbxSubIndustryList.Add(cbSub22);
                cbxSubIndustryList.Add(cbSub23);
                cbxSubIndustryList.Add(cbSub24);
                cbxSubIndustryList.Add(cbSub25);
                cbxSubIndustryList.Add(cbSub26);
                cbxSubIndustryList.Add(cbSub27);
                cbxSubIndustryList.Add(cbSub28);
                cbxSubIndustryList.Add(cbSub29);
                cbxSubIndustryList.Add(cbSub30);
                cbxSubIndustryList.Add(cbSub31);
                cbxSubIndustryList.Add(cbSub32);
                cbxSubIndustryList.Add(cbSub33);
                cbxSubIndustryList.Add(cbSub34);
                cbxSubIndustryList.Add(cbSub35);
                cbxSubIndustryList.Add(cbSub36);
                cbxSubIndustryList.Add(cbSub37);
                cbxSubIndustryList.Add(cbSub38);
                cbxSubIndustryList.Add(cbSub39);
                cbxSubIndustryList.Add(cbSub40);
                cbxSubIndustryList.Add(cbSub41);
                cbxSubIndustryList.Add(cbSub42);
                cbxSubIndustryList.Add(cbSub43);
                cbxSubIndustryList.Add(cbSub44);

                string selectedVerticals = string.Empty;
                List<string> verticals = GlobalMethods.GetSelectedCategories(cbxSubIndustryList, out selectedVerticals);

                if (verticals.Count > 0)
                {
                    hasSelectedCriteria = true;

                    SearchQueryString += @"inner join Elio_users_sub_industries_group_items
	                                        on Elio_users_sub_industries_group_items.user_id = Elio_users.id
                                        inner join Elio_sub_industries_group_items
	                                        on Elio_sub_industries_group_items.id = Elio_users_sub_industries_group_items.sub_industry_group_item_id
                                        inner join Elio_sub_industries_group
	                                        on Elio_sub_industries_group.id = Elio_sub_industries_group_items.sub_industies_group_id";

                    SearchQueryString += " where (1=1) ";

                    SearchQueryString += " and Elio_sub_industries_group_items.description in (" + selectedVerticals + ")";
                }
                else
                {
                    SearchQueryString += " where (1=1) ";
                }

                #endregion
                
                if (RcbxCategory.SelectedValue != "0")
                {
                    SearchQueryString += " and company_type='" + RcbxCategory.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxStatus.SelectedValue != "-1")
                {
                    SearchQueryString += " and account_status=" + RcbxStatus.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxIsPublic.SelectedValue != "-1")
                {
                    SearchQueryString += " and Elio_users.is_public=" + RcbxIsPublic.SelectedValue + " ";
                    hasSelectedCriteria = true;
                }
                if (RcbxName.SelectedValue != "0")
                {
                    SearchQueryString += " and company_name='" + RcbxName.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }
                if (RcbxBillingType.SelectedValue != "0")
                {
                    SearchQueryString += " and billing_type='" + RcbxBillingType.SelectedValue + "'";
                    hasSelectedCriteria = true;
                }
                if (RcbxApplicationType.SelectedValue != "0")
                {
                    SearchQueryString += " and user_application_type='" + RcbxApplicationType.SelectedValue + "'";
                    hasSelectedCriteria = true;
                }
                if (RtbxUserId.Text != string.Empty)
                {
                    SearchQueryString += " and Elio_users.id='" + RtbxUserId.Text + "'";
                    hasSelectedCriteria = true;
                }
                if (RtbxStripeCustomerId.Text != string.Empty)
                {
                    SearchQueryString += " and customer_stripe_id='" + RtbxStripeCustomerId.Text + "'";
                    hasSelectedCriteria = true;
                }
                if (RtbxEmail.Text != string.Empty)
                {
                    SearchQueryString += " and email='" + RtbxEmail.Text + "'";
                    hasSelectedCriteria = true;
                }
                if (RcbxCountries.SelectedValue != "0")
                {
                    SearchQueryString += " and country='" + RcbxCountries.SelectedItem.Text + "' ";
                    hasSelectedCriteria = true;
                }

                SearchQueryString += " order by Elio_users.id";

                if (hasSelectedCriteria)
                {
                    RdgElioUsers.Rebind();
                }
                else
                {
                    SearchQueryString = string.Empty;
                    RdgElioUsers.Rebind();
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
                RcbxCategory.SelectedValue = "0";
                RcbxStatus.SelectedValue = "-1";
                RcbxIsPublic.SelectedValue = "-1";
                RcbxName.SelectedValue = "0";
                RcbxBillingType.SelectedValue = "0";
                RtbxUserId.Text = string.Empty;
                RtbxStripeCustomerId.Text = string.Empty;
                RcbxApplicationType.SelectedValue = "0";
                RtbxEmail.Text = string.Empty;
                RcbxCountries.SelectedIndex = -1;

                #region Find Controls

                CheckBoxList cbSub1 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub1");
                CheckBoxList cbSub2 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub2");
                CheckBoxList cbSub3 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub3");
                CheckBoxList cbSub4 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub4");
                CheckBoxList cbSub5 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub5");
                CheckBoxList cbSub6 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub6");
                CheckBoxList cbSub7 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub7");
                CheckBoxList cbSub8 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub8");
                CheckBoxList cbSub9 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub9");
                CheckBoxList cbSub10 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub10");
                CheckBoxList cbSub11 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub11");
                CheckBoxList cbSub12 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub12");
                CheckBoxList cbSub13 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub13");
                CheckBoxList cbSub14 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub14");
                CheckBoxList cbSub15 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub15");
                CheckBoxList cbSub16 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub16");
                CheckBoxList cbSub17 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub17");
                CheckBoxList cbSub18 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub18");
                CheckBoxList cbSub19 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub19");
                CheckBoxList cbSub20 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub20");
                CheckBoxList cbSub21 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub21");
                CheckBoxList cbSub22 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub22");
                CheckBoxList cbSub23 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub23");
                CheckBoxList cbSub24 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub24");
                CheckBoxList cbSub25 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub25");
                CheckBoxList cbSub26 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub26");
                CheckBoxList cbSub27 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub27");
                CheckBoxList cbSub28 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub28");
                CheckBoxList cbSub29 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub29");
                CheckBoxList cbSub30 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub30");
                CheckBoxList cbSub31 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub31");
                CheckBoxList cbSub32 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub32");
                CheckBoxList cbSub33 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub33");
                CheckBoxList cbSub34 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub34");
                CheckBoxList cbSub35 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub35");
                CheckBoxList cbSub36 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub36");
                CheckBoxList cbSub37 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub37");
                CheckBoxList cbSub38 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub38");
                CheckBoxList cbSub39 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub39");
                CheckBoxList cbSub40 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub40");
                CheckBoxList cbSub41 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub41");
                CheckBoxList cbSub42 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub42");
                CheckBoxList cbSub43 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub43");
                CheckBoxList cbSub44 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub44");

                #endregion

                #region Check CheckBox Lists

                List<CheckBoxList> cbxSubIndustryList = new List<CheckBoxList>();

                cbxSubIndustryList.Add(cbSub1);
                cbxSubIndustryList.Add(cbSub2);
                cbxSubIndustryList.Add(cbSub3);
                cbxSubIndustryList.Add(cbSub4);
                cbxSubIndustryList.Add(cbSub5);
                cbxSubIndustryList.Add(cbSub6);
                cbxSubIndustryList.Add(cbSub7);
                cbxSubIndustryList.Add(cbSub8);
                cbxSubIndustryList.Add(cbSub9);
                cbxSubIndustryList.Add(cbSub10);
                cbxSubIndustryList.Add(cbSub11);
                cbxSubIndustryList.Add(cbSub12);
                cbxSubIndustryList.Add(cbSub13);
                cbxSubIndustryList.Add(cbSub14);
                cbxSubIndustryList.Add(cbSub15);
                cbxSubIndustryList.Add(cbSub16);
                cbxSubIndustryList.Add(cbSub17);
                cbxSubIndustryList.Add(cbSub18);
                cbxSubIndustryList.Add(cbSub19);
                cbxSubIndustryList.Add(cbSub20);
                cbxSubIndustryList.Add(cbSub21);
                cbxSubIndustryList.Add(cbSub22);
                cbxSubIndustryList.Add(cbSub23);
                cbxSubIndustryList.Add(cbSub24);
                cbxSubIndustryList.Add(cbSub25);
                cbxSubIndustryList.Add(cbSub26);
                cbxSubIndustryList.Add(cbSub27);
                cbxSubIndustryList.Add(cbSub28);
                cbxSubIndustryList.Add(cbSub29);
                cbxSubIndustryList.Add(cbSub30);
                cbxSubIndustryList.Add(cbSub31);
                cbxSubIndustryList.Add(cbSub32);
                cbxSubIndustryList.Add(cbSub33);
                cbxSubIndustryList.Add(cbSub34);
                cbxSubIndustryList.Add(cbSub35);
                cbxSubIndustryList.Add(cbSub36);
                cbxSubIndustryList.Add(cbSub37);
                cbxSubIndustryList.Add(cbSub38);
                cbxSubIndustryList.Add(cbSub39);
                cbxSubIndustryList.Add(cbSub40);
                cbxSubIndustryList.Add(cbSub41);
                cbxSubIndustryList.Add(cbSub42);
                cbxSubIndustryList.Add(cbSub43);
                cbxSubIndustryList.Add(cbSub44);

                GlobalMethods.ResetSelectedCategoriesList(cbxSubIndustryList);

                #endregion

                SearchQueryString = string.Empty;

                RdgElioUsers.Rebind();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnLoginAsCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
                    Session.Clear();

                    if (Request.Browser.Cookies)
                    {
                        HttpCookie loginCookie = Request.Cookies[CookieName];
                        if (loginCookie != null)
                        {
                            loginCookie.Expires = DateTime.Now.AddYears(-30);
                            Response.Cookies.Add(loginCookie);
                        }
                    }

                    vSession.User = user;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        protected void ImgBtnPreviewCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                vSession.ElioCompanyDetailsView =null;

                if (vSession.User != null)
                {
                    vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        if (vSession.Page != string.Empty)
                        {
                            Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
                        }
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

        protected void ImgBtnNestedLoginAsCompany(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["connection_id"].Text), session);
                if (user != null)
                {
                    Session.Clear();

                    if (Request.Browser.Cookies)
                    {
                        HttpCookie loginCookie = Request.Cookies[CookieName];
                        if (loginCookie != null)
                        {
                            loginCookie.Expires = DateTime.Now.AddYears(-30);
                            Response.Cookies.Add(loginCookie);
                        }
                    }

                    vSession.User = user;

                    Response.Redirect(ControlLoader.Dashboard(vSession.User, "home"), false);
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

        protected void ImgBtnNestedPreviewCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                vSession.ElioCompanyDetailsView = null;

                if (vSession.User != null)
                {
                    vSession.ElioCompanyDetailsView = Sql.GetUserById(Convert.ToInt32(item["connection_id"].Text), session);
                    if (vSession.ElioCompanyDetailsView != null)
                    {
                        if (vSession.Page != string.Empty)
                        {
                            Response.Redirect(ControlLoader.Profile(vSession.ElioCompanyDetailsView), false);
                        }
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
    }
}