using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Localization;

namespace WdS.ElioPlus.Controls.Dashboard
{
    public partial class SubIndustries : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        private DBSession session = new DBSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                LoadData();

                UpdateStrings();
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
        
        protected void RtbSubInfo_OnTabClick(object sender, EventArgs args)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #region Methods

        private void LoadData()
        {
            PnlContent.Visible = (vSession.User.AccountStatus == Convert.ToInt32(AccountStatus.Completed)) ? true : false;
            if (PnlContent.Visible)
            {
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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 5)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub1.Items.Add(item);
                        count++;
                    }
                    else if (count > 5 && count <= 11)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub2.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub4.Items.Add(item);
                        count++;
                    }
                    else if (count > 1 && count <= 3)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub5.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub7.Items.Add(item);
                        count++;
                    }
                    else if (count > 1 && count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub8.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub10.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 5)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub11.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub13.Items.Add(item);
                        count++;
                    }
                    else if (count > 1 && count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub14.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub16.Items.Add(item);
                        count++;
                    }
                    else if (count > 1 && count <= 3)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub17.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub19.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 4)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub20.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub22.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 5)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub23.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub25.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 5)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub26.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub28.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 5)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub29.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 4)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub31.Items.Add(item);
                        count++;
                    }
                    else if (count > 4 && count <= 9)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub32.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 0)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub34.Items.Add(item);
                        count++;
                    }
                    else if (count > 0 && count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub35.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 0)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub37.Items.Add(item);
                        count++;
                    }
                    else if (count > 0 && count <= 1)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Attributes["group_id"] = group.SubIndustriesGroupId.ToString();

                        cbSub38.Items.Add(item);
                        count++;
                    }
                    else
                    {
                        
                    }
                }

                #endregion

                #region Group_14

                count = 0;
                List<ElioSubIndustriesGroupItems> unifiedCommunications = Sql.GetSubIndustriesGroupItemsByIndustryGroupId(Convert.ToInt32(SubIndustryGroup.UnifiedCommunications), session);
                foreach (ElioSubIndustriesGroupItems group in unifiedCommunications)
                {
                    ListItem item = new ListItem();

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
                    if (count <= 2)
                    {
                        item.Text = group.Description;
                        item.Value = group.Id.ToString();
                        item.Value = group.Id.ToString() + "~" + group.SubIndustriesGroupId.ToString();

                        cbSub39.Items.Add(item);
                        count++;
                    }
                    else if (count > 2 && count <= 4)
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

                    item.Selected = Sql.ExistUserSubIndustryGroupItem(vSession.User.Id, group.Id, session);
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
            }
        }

        private void UpdateStrings()
        {
            Label lblUpdateText = (Label)ControlFinder.FindControlRecursive(RbtnUpdate, "LblUpdateText");
            lblUpdateText.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "button", "4")).Text;
        }

        private void Clear()
        {
            UcMessageAlert.Visible = false;
        }

        #endregion

        #region Buttons

        protected void RbtnUpdate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                Clear();
                bool hasSeslectedItem = false;
                string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "1")).Text;

                ElioUsers user = Sql.GetUserById(vSession.User.Id, session);
                if (user != null)
                {
                    string category = string.Empty;
                    try
                    {
                        session.BeginTransaction();

                        #region Fix Sub Industry List

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

                        //hasSeslectedItem = Methods.FixUserIndustriesByCheckBoxList(cbxIndustryList, vSession.User.Id, session);
                        if (!GlobalMethods.IsValidChecked(cbxSubIndustryList))
                        {
                            GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "3")).Text, MessageTypes.Error, true, true, false);
                            return;
                        }

                        hasSeslectedItem = GlobalDBMethods.FixUserCategoriesByCheckBoxList(cbxSubIndustryList, Category.SubIndustry, vSession.User.Id, false, session);
                        if (!hasSeslectedItem)
                        {
                            //error      
                            category = Category.SubIndustry.ToString();
                            throw new Exception();
                        }

                        #endregion

                        session.CommitTransaction();
                    }
                    catch (Exception)
                    {
                        session.RollBackTransaction();
                        GlobalMethods.ShowMessageControl(UcMessageAlert, Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "1")).Text.Replace("{notselectedcategory}", category), MessageTypes.Error, true, true, false);
                        return;
                    }

                    hasSeslectedItem = true;

                    alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("controls", "companycategoriesdata", "label", "2")).Text;
                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Success, true, true, false);
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