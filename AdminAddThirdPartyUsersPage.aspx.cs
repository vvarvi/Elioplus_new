using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using Telerik.Web.UI;
using System.Data;
using WdS.ElioPlus.Lib.Services.EnrichmentAPI;
using System.Web.UI;
using System.Xml.Linq;
using WdS.ElioPlus.Lib.Services.GoogleGeolocationAPI;
using EnvDTE;
using WdS.ElioPlus.Lib.LoadControls;

namespace WdS.ElioPlus
{
    public partial class AdminAddThirdPartyUsersPage : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (!IsPostBack)
                    {
                        FixPage();
                    }
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

        # region Methods

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
            CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
            CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
            CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

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
            cbSub45.Items.Clear();
            cbSub46.Items.Clear();
            cbSub47.Items.Clear();

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

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub7.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 2)
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

                if (count <= 6)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub10.Items.Add(item);
                    count++;
                }
                else if (count > 6 && count <= 13)
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
                else if (count > 1 && count <= 2)
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

                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub25.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
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

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub34.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 2)
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

                if (count <= 1)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub37.Items.Add(item);
                    count++;
                }
                else if (count > 1 && count <= 2)
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
            List<ElioSubIndustriesGroupItems> unifiedCommunicationsGroup = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.UnifiedCommunications), session);
            foreach (ElioSubIndustriesGroupItems group in unifiedCommunicationsGroup)
            {
                ListItem item = new ListItem();

                if (count <= 3)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub39.Items.Add(item);
                    count++;
                }
                else if (count > 3 && count <= 7)
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

            #region Group_16

            count = 0;
            List<ElioSubIndustriesGroupItems> hrdware = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.Hardware), session);
            foreach (ElioSubIndustriesGroupItems group in hrdware)
            {
                ListItem item = new ListItem();

                item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                if (count <= 2)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub45.Items.Add(item);
                    count++;
                }
                else if (count > 2 && count <= 5)
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub46.Items.Add(item);
                    count++;
                }
                else
                {
                    item.Text = group.Description;
                    item.Value = group.Id.ToString();
                    item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                    cbSub47.Items.Add(item);
                    count++;
                }
            }

            #endregion

            #endregion
        }

        private void FixPage()
        {            
            UpdateStrings();

            //LoadCompanyTypes();
            LoadCountries();
            LoadVerticals();

            LblWebSiteError.Text = string.Empty;
            //LblWebSiteError2.Text = string.Empty;
            LblWebSitePass.Text = string.Empty;

            UcMessageAlert.Visible = false;
            UcClearbitMessageAlert.Visible = false;
        }

        private void UpdateStrings()
        {
            
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

        private void LoadCompanyTypes()
        {
            //RcbxCategory.Items.Clear();

            //RadComboBoxItem item = new RadComboBoxItem();

            //item.Value = "0";
            //item.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "label", "39")).Text;
            //RcbxCategory.Items.Add(item);

            //List<ElioUserTypes> types = Sql.GetUserPublicTypes(session);
            //foreach (ElioUserTypes type in types)
            //{
            //    item = new RadComboBoxItem();

            //    item.Value = type.Id.ToString();
            //    item.Text = type.Description;

            //    RcbxCategory.Items.Add(item);
            //}
        }

        private void LoadTypes(GridDataItem item)
        {
            DropDownList rcbxCategory = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxCategory");
            rcbxCategory.Items.Clear();

            List<ElioUserTypes> types = Sql.GetUserPublicTypes(session);
            foreach (ElioUserTypes type in types)
            {
                ListItem rcbxitem = new ListItem();

                rcbxitem.Value = type.Id.ToString();
                rcbxitem.Text = type.Description;

                rcbxCategory.Items.Add(rcbxitem);
            }
        }

        private void FixUsersGrid(GridDataItem item, bool isSaveMode, bool isCancelClicked)
        {
            string alert = string.Empty;

            UcMessageAlert.Visible = isSaveMode;
            UcClearbitMessageAlert.Visible = false;

            Label lblCategory = (Label)ControlFinder.FindControlRecursive(item, "LblCategory");
            
            DropDownList rcbxCategory = (DropDownList)ControlFinder.FindControlRecursive(item, "RcbxCategory");           
            ImageButton imgBtnSaveChanges = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnSaveChanges");
            ImageButton imgBtnEditCompany = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnEditCompany");
            ImageButton imgBtnCancel = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCancel");

            if (!isSaveMode)
            {
                LoadTypes(item);
            }

            if (!isCancelClicked)
            {
                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);
                if (user != null)
                {
                    if (isSaveMode && !isCancelClicked)
                    {
                        user.CompanyType = rcbxCategory.SelectedItem.Text;
                        user.LastUpdated = DateTime.Now;
                        
                        DataLoader<ElioUsers> loader = new DataLoader<ElioUsers>(session);
                        loader.Update(user);

                        //RdgElioUsers.Rebind();

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
                rcbxCategory.Visible = !isSaveMode;                
                imgBtnSaveChanges.Visible = !isSaveMode;
                imgBtnCancel.Visible = !isSaveMode;
                imgBtnEditCompany.Visible = isSaveMode;
                //rcbxCategory.FindItemByText(user.CompanyType).Selected = true;
            }
            else
            {
                alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "adminpage", "message", "3")).Text;

                //RdgElioUsers.Rebind();

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Warning, true, true, false);
            }
        }

        private void ResetFields(bool errorsOnly)
        {
            if (!errorsOnly)
            {
                RcbxCategory.SelectedValue = "2";
                RcbxCountries.SelectedIndex = -1;
                RtbxUserId.Text = string.Empty;
                RtbxUsername.Text = string.Empty;
                RtbxPassword.Text = string.Empty;
                RtbxCompanyName.Text = string.Empty;
                RtbxEmail.Text = string.Empty;
                RtbxWebSite.Text = string.Empty;
                RtbxAddress.Text = string.Empty;
                RtbxCity.Text = string.Empty;
                RtbxState.Text = string.Empty;
                RtbxPhoneNumberPrefix.Text = string.Empty;
                RtbxPhone.Text = string.Empty;
                RtbxLinkedin.Text = string.Empty;
                CbxManagedServiceProvider.Checked = false;
                //RcbxProducts.Text = string.Empty;
                LblIntegrationProducts.Text = "";
                ImgBtnRemove.Visible = false;

                RbtnCancelEdit.Visible = false;

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
                CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
                CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
                CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

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
                cbxSubIndustryList.Add(cbSub45);
                cbxSubIndustryList.Add(cbSub46);
                cbxSubIndustryList.Add(cbSub47);

                GlobalDBMethods.ClearChekckBoxLists(cbxSubIndustryList);

                #endregion

                //RdgElioUsers.Rebind();
            }           

            LblTypeError.Text = string.Empty;
            LblUsernameError.Text = string.Empty;
            LblPasswordError.Text = string.Empty;
            LblCompanyNameError.Text = string.Empty;
            LblEmailError.Text = string.Empty;
            LblWebSiteError.Text = string.Empty;
            LblCountryError.Text = string.Empty;
            LblAddressError.Text = string.Empty;
            LblPhoneError.Text = string.Empty;
            LblLinkedinError.Text = string.Empty;
            LblManagedServiceProviderError.Text = string.Empty;
            LblSubIndustryError.Text = string.Empty;
        }

        private void InsertProducts(ElioUsers user)
        {
            if (user != null && user.Id > 0)
            {
                if (LblIntegrationProducts.Text != "")
                {
                    if (LblIntegrationProducts.Text.EndsWith(", "))
                        LblIntegrationProducts.Text = LblIntegrationProducts.Text.Substring(0, LblIntegrationProducts.Text.Length - 2);

                    string[] list = LblIntegrationProducts.Text.Split(',').ToArray();
                    if (list.Length > 0)
                    {
                        foreach (string item in list)
                        {
                            string itemDescription = item;
                            if (itemDescription != "")
                            {
                                if (itemDescription.StartsWith(" "))
                                    itemDescription = itemDescription.Substring(1);

                                if (itemDescription.EndsWith(" "))
                                    itemDescription = itemDescription.TrimEnd(' ');

                                if (user.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    ElioRegistrationProducts product = Sql.GetRegistrationProductsIDByDescription(itemDescription, session);
                                    if (product == null)
                                    {
                                        product = new ElioRegistrationProducts();
                                        product.Description = itemDescription;
                                        product.IsPublic = 1;
                                        product.Sysdate = DateTime.Now;

                                        DataLoader<ElioRegistrationProducts> insertLoader = new DataLoader<ElioRegistrationProducts>(session);
                                        insertLoader.Insert(product);
                                    }

                                    if (product != null)
                                    {
                                        bool exist = Sql.ExistUserRegistrationProduct(user.Id, product.Id, session);
                                        if (!exist)
                                        {
                                            ElioUsersRegistrationProducts userProducts = new ElioUsersRegistrationProducts();
                                            userProducts.UserId = user.Id;
                                            userProducts.RegProductsId = product.Id;

                                            DataLoader<ElioUsersRegistrationProducts> loader = new DataLoader<ElioUsersRegistrationProducts>(session);
                                            loader.Insert(userProducts);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        # endregion

        #region Grids

        #endregion

        #region Combo

        protected void RcbxCountries_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                RtbxPhoneNumberPrefix.Text = "+" + RcbxCountries.SelectedItem.ToolTip;

                if (RcbxCountries.SelectedIndex == 0)
                {
                    RtbxPhoneNumberPrefix.Text = string.Empty;
                    RtbxPhone.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void ImgBtnAdd_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (RcbxProducts.Text != "")
                {
                    if (RcbxProducts.Text.Length > 30)
                    {
                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, "Sorry, fill product with less than 30 characters for description!", MessageTypes.Error, true, true, false, true, false);
                        RcbxProducts.Entries.Clear();

                        return;
                    }
                }

                if (RcbxProducts.Text != "")
                    LblIntegrationProducts.Text += RcbxProducts.Text + ", ";

                RcbxProducts.Entries.Clear();

                ImgBtnRemove.Visible = LblIntegrationProducts.Text != "";
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

        protected void ImgBtnRemove_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LblIntegrationProducts.Text = "";
                ImgBtnRemove.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnGenerateUsername_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LblUsernameError.Text = string.Empty;
                LblPasswordError.Text = string.Empty;

                RtbxUsername.Text = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
                RtbxPassword.Text = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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

        protected void LnkBtnGeneratePassword_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LblPasswordError.Text = string.Empty;

                RtbxPassword.Text = GeneratePasswordLib.CreateRandomStringWithMax11Chars(15);
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

        protected void ImgBtnCheck_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LblWebSiteError.Text = string.Empty;
                //LblWebSiteError2.Text = string.Empty;
                LblWebSitePass.Text = string.Empty;

                if (RtbxWebSite.Text.Trim() != string.Empty)
                {
                    bool exist = Sql.IsDomainRegistered(RtbxWebSite.Text, session);

                    if (exist)
                    {
                        LblWebSiteError.Text = "This domain (website) is already registered.";
                        //LblWebSiteError2.Text = "You can continue anyway.";
                        return;
                    }
                    else
                    {
                        LblWebSitePass.Text = "This domain (website) is not registered";
                        return;
                    }
                }
                else
                {
                    LblWebSiteError.Text = "Please enter domain (website).";
                    return;
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

        protected void ImgBtnGetCity_Click(object sender, EventArgs args)
        {
            try
            {
                LblCityError.Text = string.Empty;

                if (RtbxAddress.Text.Trim() != string.Empty)
                {
                    String addressToCheck = RtbxAddress.Text;
                    String CountryCodeToCheck = RcbxCountries.SelectedValue != "0" ? RcbxCountries.SelectedItem.Text : "";
                    
                    IGeolocationProvider multipleProvider = new MultipleGeolocationProvider();

                    XDocument doc = new XDocument();

                    XDocument XmultipleProvider = multipleProvider.resolveAddress(addressToCheck, CountryCodeToCheck, RcbxCountries.SelectedValue == "0", false);

                    String status = "";

                    String provider = "";
                    
                    String VenueResolvedCity = "";
                    String VenueResolvedCountry = "";

                    try
                    {
                        status = XmultipleProvider.Element("NewDataSet").Element("QueryStatus").Value;

                        if (status == "connection_error")
                        {
                            LblCityError.Text = "API return: connection error";
                            Logger.DetailedError(Request.Url.ToString(), "Connection Error");
                        }
                    }
                    catch { }

                    if (status == "ok")
                    {
                        XElement NewDataSet = new XElement("NewDataSet");
                        if (XmultipleProvider.Element("NewDataSet") != null)
                        {
                            NewDataSet = XmultipleProvider.Element("NewDataSet");
                            XElement Table = new XElement("Table");
                            if (XmultipleProvider.Element("NewDataSet").Element("Table") != null)
                            {
                                Table = NewDataSet.Element("Table");

                                provider = Table.Element("PROVIDER").Value;
                                VenueResolvedCity = Table.Element("CITY").Value;
                                VenueResolvedCountry = Table.Element("COUNTRY_CODE").Value;
                            }
                        }

                        if (VenueResolvedCity != "")
                            RtbxCity.Text = VenueResolvedCity;
                        else
                            LblCityError.Text = "City could not be resolved from the address.";
                    }
                    else
                        LblCityError.Text = "City could not be resolved from the address.";
                }
                else
                {
                    LblCityError.Text = "Please enter address in order to get the city.";
                    return;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RbtnSave_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ResetFields(true);

                string alert = string.Empty;

                #region Check Fields

                if (RtbxUsername.Text == string.Empty)
                {
                    LblUsernameError.Text = "You must create username";
                    return;
                }

                if (RtbxPassword.Text == string.Empty)
                {
                    LblPasswordError.Text = "You must create password";
                    return;
                }

                if (RcbxCategory.SelectedItem.Value == "0")
                {
                    LblTypeError.Text = "You must select company type";
                    return;
                }

                if (RtbxUsername.Text == string.Empty && RtbxPassword.Text == string.Empty && RtbxCompanyName.Text == string.Empty && RtbxEmail.Text == string.Empty && RtbxWebSite.Text == string.Empty && RcbxCountries.SelectedIndex == -1 && RtbxAddress.Text == string.Empty && RtbxPhoneNumberPrefix.Text == string.Empty && RtbxPhone.Text == string.Empty && RtbxLinkedin.Text == string.Empty)
                {
                    return;
                }

                if (RtbxCompanyName.Text == string.Empty)
                {
                    LblCompanyNameError.Text = "You must enter company name";
                    return;
                }

                if (RtbxEmail.Text == string.Empty)
                {
                    LblEmailError.Text = "You must enter company email";
                    return;
                }
                else
                {
                    if (!Validations.IsEmail(RtbxEmail.Text))
                    {
                        LblEmailError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "3")).Text;
                        return;
                    }

                    if (RtbxUserId.Text == string.Empty)
                    {
                        if (Sql.ExistEmail(RtbxEmail.Text, session))
                        {
                            LblEmailError.Text = "The email is already registered";
                            return;
                        }
                    }
                    else
                    {
                        if (Sql.ExistEmailToOtherUser(RtbxEmail.Text, Convert.ToInt32(RtbxUserId.Text), session))
                        {
                            LblEmailError.Text = "The email is already registered to other user";
                            return;
                        }
                    }
                }

                #endregion

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
                CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
                CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
                CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

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
                cbxSubIndustryList.Add(cbSub45);
                cbxSubIndustryList.Add(cbSub46);
                cbxSubIndustryList.Add(cbSub47);

                if (!GlobalMethods.HasSelectedItemInCheckBoxList(cbxSubIndustryList))
                {
                    LblSubIndustryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "fullregistration", "message", "21")).Text;
                    return;
                }

                if (!GlobalMethods.IsValidChecked(cbxSubIndustryList))
                {
                    LblSubIndustryError.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "3")).Text;
                    return;
                }

                #endregion

                #region Personal Info

                ElioUsers user = new ElioUsers();

                user.CompanyType = RcbxCategory.SelectedItem.Text;
                user.CompanyName = RtbxCompanyName.Text;

                user.Email = RtbxEmail.Text.Trim();
                user.WebSite = (RtbxWebSite.Text != string.Empty) ? (RtbxWebSite.Text.StartsWith("http://") || RtbxWebSite.Text.StartsWith("https://")) ? RtbxWebSite.Text : "http://" + RtbxWebSite.Text : string.Empty;

                user.Address = RtbxAddress.Text;

                if (RcbxCountries.SelectedItem.Value != "0")
                {
                    user.Country = RcbxCountries.SelectedItem.Text;
                    user.CompanyRegion = Sql.GetRegionByCountryId(Convert.ToInt32(RcbxCountries.SelectedValue), session);
                    user.Phone = RcbxCountries.SelectedItem.ToolTip + RtbxPhone.Text.Replace("-", string.Empty).Replace(" ", string.Empty).Trim();
                }
                else
                {
                    user.Country = string.Empty;
                    user.CompanyRegion = string.Empty;
                    user.Phone = string.Empty;
                }

                user.City = RtbxCity.Text;
                user.State = RtbxState.Text;
                user.LinkedInUrl = RtbxLinkedin.Text;
                user.TwitterUrl = "";

                user.Ip = HttpContext.Current.Request.ServerVariables["remote_addr"];
                user.GuId = Guid.NewGuid().ToString();
                user.SysDate = DateTime.Now;
                user.LastUpdated = DateTime.Now;
                user.UserApplicationType = Convert.ToInt32(UserApplicationType.ThirdParty);

                user.Overview = string.Empty;
                user.Description = string.Empty;
                user.MashapeUsername = string.Empty;
                user.AccountStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                user.IsPublic = (int)AccountPublicStatus.IsNotPublic;
                user.CommunityStatus = Convert.ToInt32(AccountStatus.NotCompleted);
                user.CommunityProfileCreated = DateTime.Now;
                user.CommunityProfileLastUpdated = DateTime.Now;
                user.HasBillingDetails = 0;
                user.BillingType = Convert.ToInt32(BillingTypePacket.FreemiumPacketType);

                user.Username = RtbxUsername.Text;
                user.UsernameEncrypted = MD5.Encrypt(user.Username);
                user.Password = RtbxPassword.Text;
                user.PasswordEncrypted = MD5.Encrypt(user.Password);

                #endregion

                string actionMode = "";
                if (RtbxUserId.Text == string.Empty)
                {
                    user = GlobalDBMethods.InsertNewUser(user, session);

                    alert = "You have successfully insert third party user";
                    actionMode = "INSERT";
                }
                else
                {
                    user.Id = Convert.ToInt32(RtbxUserId.Text);
                    user = GlobalDBMethods.UpDateUser(user, session);

                    alert = "You have successfully updated third party user";
                    actionMode = "UPDATE";
                }

                #region Registration Products

                try
                {
                    bool inserted = true;

                    if (!string.IsNullOrEmpty(LblIntegrationProducts.Text.Trim()))
                        inserted = GlobalDBMethods.InsertProductsToUser(user, LblIntegrationProducts.Text, session);

                    if (!inserted)
                        throw new Exception("products did not insert to user " + user.Id);
                }
                catch (Exception ex)
                {
                    Logger.DetailedError(Request.Url.ToString(), "ERROR --> AdminAddThirdPartyUsersPage --> RbtnSave_OnClick --> InsertProducts(user) --> products did not insert to user " + ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #endregion

                #region Subindustries

                GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxSubIndustryList, Category.SubIndustry, user.Id, false, session);

                #endregion

                #region User Features

                ElioUsersFeatures freeFeatures = Sql.GetFeaturesbyUserType(Convert.ToInt32(BillingTypePacket.FreemiumPacketType), session);
                if (freeFeatures != null)
                {
                    ElioUserPacketStatus userPackStatus = Sql.GetUserPacketStatusFeatures(user.Id, session);
                    if (userPackStatus == null)
                    {
                        userPackStatus = new ElioUserPacketStatus();

                        userPackStatus.UserId = user.Id;
                        userPackStatus.PackId = freeFeatures.PackId;
                        userPackStatus.UserBillingType = freeFeatures.UserBillingType;
                        userPackStatus.AvailableLeadsCount = freeFeatures.TotalLeads;
                        userPackStatus.AvailableMessagesCount = freeFeatures.TotalMessages;
                        userPackStatus.AvailableConnectionsCount = freeFeatures.TotalConnections;
                        userPackStatus.AvailableManagePartnersCount = freeFeatures.TotalManagePartners;
                        userPackStatus.AvailableLibraryStorageCount = Convert.ToDecimal(freeFeatures.TotalLibraryStorage);
                        userPackStatus.Sysdate = DateTime.Now;
                        userPackStatus.LastUpdate = DateTime.Now;
                        userPackStatus.StartingDate = DateTime.Now;
                        userPackStatus.ExpirationDate = DateTime.Now.AddMonths(1);

                        DataLoader<ElioUserPacketStatus> loader = new DataLoader<ElioUserPacketStatus>(session);
                        loader.Insert(userPackStatus);
                    }
                }

                #endregion

                #region Partner Program

                DataLoader<ElioUsersPartners> loaderProgram = new DataLoader<ElioUsersPartners>(session);

                if (CbxManagedServiceProvider.Checked)
                {
                    ElioUsersPartners userMSPProgram = Sql.GetUsersPartnerProgramsById(user.Id, 7, session);    //Managed Service Provider
                    if (userMSPProgram == null)
                    {
                        userMSPProgram = new ElioUsersPartners();

                        userMSPProgram.UserId = user.Id;
                        userMSPProgram.PartnerId = 7;
                        loaderProgram.Insert(userMSPProgram);
                    }
                }
                else
                {
                    //Sql.DeleteUserPartnerProgramByPartnerID(vSession.User.Id, 7, session);
                }

                #endregion

                session.CommitTransaction();

                ResetFields(false);

                GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Success, true, true, false);

                if (user != null && actionMode == "INSERT")
                {
                    //ElioUsersPerson elioPerson = new ElioUsersPerson();
                    //ElioUsersPersonCompanies eliocompany = new ElioUsersPersonCompanies();
                    //List<ElioUsersPersonCompanyPhoneNumbers> phones = new List<ElioUsersPersonCompanyPhoneNumbers>();
                    //List<ElioUsersPersonCompanyTags> tags = new List<ElioUsersPersonCompanyTags>();
                    //ElioUsers newUser = user;
                    string emailAddress = "";

                    try
                    {
                        emailAddress = (!string.IsNullOrEmpty(user.Email)) ? user.Email : (!string.IsNullOrEmpty(user.OfficialEmail)) ? user.OfficialEmail : "";
                        bool success = false;

                        if (emailAddress != "")
                            //success = ClearBit.FindCombinedPersonCompanyByEmail(emailAddress, session, out elioPerson, out eliocompany, out phones, out tags, out newUser);
                            success = ClearBit.FindCombinedPersonCompanyByEmail_v2(user, emailAddress, session);
                        else
                        {
                            GlobalMethods.ShowMessageControlDA(UcClearbitMessageAlert, "Specific user has no email addresses available", MessageTypes.Error, true, true, false);
                        }

                        GlobalMethods.ShowMessageControlDA(UcClearbitMessageAlert, (success) ? "Data saved successfully from clearbit" : "Data could not be saved successfully from clearbit. Try again later!", (success) ? MessageTypes.Success : MessageTypes.Error, true, true, false);

                        if (success)
                        {
                            user.Overview = "View the solutions, services and product portfolio of " + user.CompanyName;

                            user = GlobalDBMethods.UpDateUser(user, session);
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.DetailedClearBitError(emailAddress);
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                session.RollBackTransaction();
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RbtnClear_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields(false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnEditCompany_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                ResetFields(true);

                RbtnCancelEdit.Visible = true;
                ImageButton imgBtnCancelEdit = (ImageButton)ControlFinder.FindControlRecursive(item,"ImgBtnCancelEdit");
                imgBtnCancelEdit.Visible = true;

                imgBtn.Visible = false;

                ElioUsers user = Sql.GetUserById(Convert.ToInt32(item["id"].Text), session);

                if (user != null)
                {
                    //RcbxCategory.FindItemByText(user.CompanyType).Selected = true;

                    if (user.Country != string.Empty)
                        RcbxCountries.FindItemByText(user.Country).Selected = true;
                    else
                        RcbxCountries.SelectedIndex = 0;

                    RtbxUserId.Text = user.Id.ToString();
                    RtbxUsername.Text = user.Username;
                    RtbxPassword.Text = user.Password;
                    RtbxCompanyName.Text = user.CompanyName;
                    RtbxEmail.Text = user.Email;
                    RtbxWebSite.Text = user.WebSite;
                    RtbxAddress.Text = user.Address;
                    RtbxCity.Text = user.City;
                    RtbxState.Text = user.State;
                    RtbxPhoneNumberPrefix.Text = "+ " + RcbxCountries.SelectedItem.ToolTip;
                    int prefixLength = RcbxCountries.SelectedItem.ToolTip.Length;
                    RtbxPhone.Text = (!string.IsNullOrEmpty(user.Phone)) ? user.Phone.Substring(prefixLength) : "";
                    RtbxLinkedin.Text = user.LinkedInUrl;

                    CbxManagedServiceProvider.Checked = Sql.HasUsersPartnerProgramByID(user.Id, 7, session);
                    
                    #region Subindustries

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
                    CheckBoxList cbSub45 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub45");
                    CheckBoxList cbSub46 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub46");
                    CheckBoxList cbSub47 = (CheckBoxList)ControlFinder.FindControlRecursive(RpbRegistrationSteps, "cbSub47");

                    #endregion

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
                    cbxSubIndustryList.Add(cbSub45);
                    cbxSubIndustryList.Add(cbSub46);
                    cbxSubIndustryList.Add(cbSub47);

                    GlobalDBMethods.LoadUserSubCategories(cbxSubIndustryList, Category.SubIndustry, user.Id, session);

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

        protected void RbtnCancelEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                ResetFields(false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion
    }
}