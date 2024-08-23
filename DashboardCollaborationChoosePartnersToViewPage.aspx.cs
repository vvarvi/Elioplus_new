using System;
using System.Collections.Generic;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Controls.AlertControls;
using WdS.ElioPlus.Controls.Dashboard.AlertControls;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationChoosePartnersToViewPage : System.Web.UI.Page
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

                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    if (!IsPostBack)
                        FixPage();

                    //if (vSession.User.CompanyType == Types.Vendors.ToString() && divVendorGroupsArea.Visible)
                    //{
                    //    LblCreateGroup.Text = DdlLibraryGroups.SelectedValue == "0" ? "Create new Group" : "Update Group";
                    //    aBtnCancelCreateGroup.Visible = DdlLibraryGroups.SelectedValue == "0" ? false : true;
                    //}
                    //else
                    //{
                    //    LblCreateGroup.Text = "Create new Group";
                    //    aBtnCancelCreateGroup.Visible = false;
                    //}
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

        private void FixPage()
        {
            UpdateStrings();
            SetLinks();

            //if (vSession.User.CompanyType == Types.Vendors.ToString())
            //{
            //    //divVendorGroupsArea.Visible = true;
            //    FillGroups();
            //    TbxLibraryGroupName.Text = "";
            //    TbxLibraryGroupName.Text = DdlLibraryGroups.SelectedValue == "0" ? "" : DdlLibraryGroups.SelectedItem.Text;
            //    aBtnDeleteGroup.Visible = aBtnCancelCreateGroup.Visible = DdlLibraryGroups.SelectedValue == "0" ? false : true;
            //}

            UcRgd1.Visible = false;
            MessageControlUp.Visible = false;
            vSession.VendorsResellersList.Clear();
            vSession.VendorsResellersList = null;
        }

        private void FillGroups()
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            DdlLibraryGroups.Items.Clear();

            ListItem item = new ListItem();
            item.Value = "0";
            item.Text = "All partners";

            DdlLibraryGroups.Items.Add(item);

            List<ElioCollaborationUsersLibraryGroups> groups = SqlCollaboration.GetCollaborationUserLibraryGroups(vSession.User.Id, session);

            foreach (ElioCollaborationUsersLibraryGroups group in groups)
            {
                item = new ListItem();
                item.Value = group.Id.ToString();
                item.Text = group.GroupDescription;

                DdlLibraryGroups.Items.Add(item);
            }
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        private void GetPartnersList(Repeater rpt, DAMessageControl control, string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                DataTable table = new DataTable();

                table.Columns.Add("id1");
                table.Columns.Add("id2");
                table.Columns.Add("id3");
                table.Columns.Add("master_user_id1");
                table.Columns.Add("master_user_id2");
                table.Columns.Add("master_user_id3");
                table.Columns.Add("invitation_status1");
                table.Columns.Add("invitation_status2");
                table.Columns.Add("invitation_status3");
                table.Columns.Add("partner_user_id1");
                table.Columns.Add("partner_user_id2");
                table.Columns.Add("partner_user_id3");
                table.Columns.Add("company_name1");
                table.Columns.Add("company_name2");
                table.Columns.Add("company_name3");
                table.Columns.Add("email1");
                table.Columns.Add("email2");
                table.Columns.Add("email3");
                table.Columns.Add("company_logo1");
                table.Columns.Add("company_logo2");
                table.Columns.Add("company_logo3");

                int isPublic = 1;
                int isDeleted = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    rpt.Visible = true;
                    control.Visible = false;

                    int rows = partners.Count / 3;
                    int columns = partners.Count % 3;
                    int index = 0;

                    for (int i = 0; i < rows; i++)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = partners[index + 1].Id.ToString();
                        row["id3"] = partners[index + 2].Id.ToString();
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = partners[index + 1].MasterUserId.ToString();
                        row["master_user_id3"] = partners[index + 2].MasterUserId.ToString();
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = partners[index + 1].InvitationStatus;
                        row["invitation_status3"] = partners[index + 2].InvitationStatus;
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = partners[index + 1].PartnerUserId;
                        row["partner_user_id3"] = partners[index + 2].PartnerUserId;
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = partners[index + 1].CompanyName;
                        row["company_name3"] = partners[index + 2].CompanyName;
                        row["email1"] = partners[index].Email;
                        row["email2"] = partners[index + 1].Email;
                        row["email3"] = partners[index + 2].Email;
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = partners[index + 1].CompanyLogo;
                        row["company_logo3"] = partners[index + 2].CompanyLogo;
                        index = index + 3;

                        table.Rows.Add(row);
                    }

                    if (columns == 1)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = "";
                        row["id3"] = "";
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = "";
                        row["master_user_id3"] = "";
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = "";
                        row["invitation_status3"] = "";
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = "";
                        row["partner_user_id3"] = "";
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = "";
                        row["company_name3"] = "";
                        row["email1"] = partners[index].Email;
                        row["email2"] = "";
                        row["email3"] = "";
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = "";
                        row["company_logo3"] = "";

                        table.Rows.Add(row);
                    }
                    else if (columns == 2)
                    {
                        DataRow row = table.NewRow();
                        row["id1"] = partners[index].Id.ToString();
                        row["id2"] = partners[index + 1].Id.ToString();
                        row["id3"] = "";
                        row["master_user_id1"] = partners[index].MasterUserId.ToString();
                        row["master_user_id2"] = partners[index + 1].MasterUserId.ToString();
                        row["master_user_id3"] = "";
                        row["invitation_status1"] = partners[index].InvitationStatus;
                        row["invitation_status2"] = partners[index + 1].InvitationStatus;
                        row["invitation_status3"] = "";
                        row["partner_user_id1"] = partners[index].PartnerUserId;
                        row["partner_user_id2"] = partners[index + 1].PartnerUserId;
                        row["partner_user_id3"] = "";
                        row["company_name1"] = partners[index].CompanyName;
                        row["company_name2"] = partners[index + 1].CompanyName;
                        row["company_name3"] = "";
                        row["email1"] = partners[index].Email;
                        row["email2"] = partners[index + 1].Email;
                        row["email3"] = "";
                        row["company_logo1"] = partners[index].CompanyLogo;
                        row["company_logo2"] = partners[index + 1].CompanyLogo;
                        row["company_logo3"] = "";

                        table.Rows.Add(row);
                    }

                    //foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    //{
                    //    int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? partner.PartnerUserId : partner.MasterUserId;
                    //    ElioUsers company = Sql.GetUserById(partnerId, session);

                    //    if (company != null)
                    //    {
                    //        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, company.CompanyLogo);
                    //    }
                    //}

                    rpt.DataSource = table;
                    rpt.DataBind();
                }
                else
                {
                    rpt.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers yet" : "You have no Vendors yet";
                    GlobalMethods.ShowMessageControlDA(control, alert, MessageTypes.Info, true, true, false, true, false);
                }
            }
        }

        private void ResetPanelItems()
        {
            FillGroups();

            aBtnDeleteGroup.Visible = aBtnCancelCreateGroup.Visible = DdlLibraryGroups.SelectedValue == "0" ? false : true;
            TbxLibraryGroupName.Text = DdlLibraryGroups.SelectedValue == "0" ? "" : TbxLibraryGroupName.Text;
            LblCreateGroup.Text = DdlLibraryGroups.SelectedValue == "0" ? "Create new Group" : "Update Group";
        }

        #endregion

        #region Grids

        protected void Rdg1_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;
                        if (row != null)
                        {
                            HtmlGenericControl div1 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div1");
                            HtmlGenericControl div2 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div2");
                            HtmlGenericControl div3 = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "div3");

                            if (row["id1"].ToString() == "")
                                div1.Visible = false;
                            if (row["id2"].ToString() == "")
                                div2.Visible = false;
                            if (row["id3"].ToString() == "")
                                div3.Visible = false;

                            if (div1.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo1");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo1");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName1");

                                HiddenField hdnId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "id1");
                                HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id1");
                                HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status1");
                                HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id1");
                                HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email1");
                                HtmlAnchor aBtnChoose = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose1");
                                //CheckBox cbx1 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx1");

                                hdnId1.Value = row["id1"].ToString();

                                //if (vSession.User.CompanyType == Types.Vendors.ToString())
                                //{
                                    //cbx1.Visible = true;

                                    //if (DdlLibraryGroups.SelectedValue != "0")
                                    //{
                                    //    int groupId = Convert.ToInt32(DdlLibraryGroups.SelectedValue);
                                    //    int vendResId = Convert.ToInt32(hdnId1.Value);
                                    //    cbx1.Checked = SqlCollaboration.IsLibraryGroupMember(groupId, vendResId, session);
                                    //}
                                //}

                                hdnMasterUserId.Value = row["master_user_id1"].ToString();

                                hdnPartnerUserId.Value = row["partner_user_id1"].ToString();

                                hdnInvitationStatus.Value = row["invitation_status1"].ToString();
                                hdnEmail.Value = row["email1"].ToString();
                                ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                                if (company != null)
                                {
                                    aCompanyLogo.HRef = aCompanyName.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ToolTip = "View company's profile";
                                    imgCompanyLogo.AlternateText = "Company logo";
                                    imgCompanyLogo.ImageUrl = company.CompanyLogo;

                                    HtmlGenericControl spanNotificationNewReceivedFiles = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationNewReceivedFiles1");

                                    int sendNewFilesCount = SqlCollaboration.GetUserCollaborationLibraryFilesBySenderUserId(vSession.User.Id, company.Id, 1, 1, session);
                                    if (sendNewFilesCount > 0)
                                    {
                                        Label lblNotificationNewReceivedFiles = (Label)ControlFinder.FindControlRecursive(item, "LblNotificationNewReceivedFiles1");

                                        spanNotificationNewReceivedFiles.Visible = true;
                                        lblNotificationNewReceivedFiles.Text = sendNewFilesCount.ToString() + string.Format(" new {0}", (sendNewFilesCount > 1) ? "files" : "file");
                                    }
                                    else
                                        spanNotificationNewReceivedFiles.Visible = false;

                                    string url = "collaboration-library/" + company.GuId;

                                    aBtnChoose.HRef = ControlLoader.Dashboard(vSession.User, url);
                                }
                            }

                            if (div2.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo2");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo2");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName2");

                                HiddenField hdnId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "id2");
                                HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id2");
                                HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status2");
                                HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id2");
                                HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email2");
                                HtmlAnchor aBtnChoose = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose2");
                                //CheckBox cbx2 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx2");

                                hdnId2.Value = row["id2"].ToString();

                                //if (vSession.User.CompanyType == Types.Vendors.ToString())
                                //{
                                //    cbx2.Visible = true;

                                //    if (DdlLibraryGroups.SelectedValue != "0")
                                //    {
                                //        int groupId = Convert.ToInt32(DdlLibraryGroups.SelectedValue);
                                //        int vendResId = Convert.ToInt32(hdnId2.Value);
                                //        cbx2.Checked = SqlCollaboration.IsLibraryGroupMember(groupId, vendResId, session);
                                //    }
                                //}

                                hdnMasterUserId.Value = row["master_user_id2"].ToString();

                                hdnPartnerUserId.Value = row["partner_user_id2"].ToString();

                                hdnInvitationStatus.Value = row["invitation_status2"].ToString();
                                hdnEmail.Value = row["email2"].ToString();
                                ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                                if (company != null)
                                {
                                    aCompanyLogo.HRef = aCompanyName.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ToolTip = "View company's profile";
                                    imgCompanyLogo.AlternateText = "Company logo";
                                    imgCompanyLogo.ImageUrl = company.CompanyLogo;

                                    HtmlGenericControl spanNotificationNewReceivedFiles = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationNewReceivedFiles2");

                                    int sendNewFilesCount = SqlCollaboration.GetUserCollaborationLibraryFilesBySenderUserId(vSession.User.Id, company.Id, 1, 1, session);
                                    if (sendNewFilesCount > 0)
                                    {
                                        Label lblNotificationNewReceivedFiles = (Label)ControlFinder.FindControlRecursive(item, "LblNotificationNewReceivedFiles2");

                                        spanNotificationNewReceivedFiles.Visible = true;
                                        lblNotificationNewReceivedFiles.Text = sendNewFilesCount.ToString() + string.Format(" new {0}", (sendNewFilesCount > 1) ? "files" : "file");
                                    }
                                    else
                                        spanNotificationNewReceivedFiles.Visible = false;

                                    string url = "collaboration-library/" + company.GuId;

                                    aBtnChoose.HRef = ControlLoader.Dashboard(vSession.User, url);
                                }
                            }

                            if (div3.Visible)
                            {
                                Image imgCompanyLogo = (Image)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo3");

                                HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo3");
                                HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName3");

                                HiddenField hdnId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "id3");
                                HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id3");
                                HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status3");
                                HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id3");
                                HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email3");
                                HtmlAnchor aBtnChoose = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aBtnChoose3");
                                //CheckBox cbx3 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx3");

                                hdnId3.Value = row["id3"].ToString();

                                //if (vSession.User.CompanyType == Types.Vendors.ToString())
                                //{
                                //    cbx3.Visible = true;

                                //    if (DdlLibraryGroups.SelectedValue != "0")
                                //    {
                                //        int groupId = Convert.ToInt32(DdlLibraryGroups.SelectedValue);
                                //        int vendResId = Convert.ToInt32(hdnId3.Value);
                                //        cbx3.Checked = SqlCollaboration.IsLibraryGroupMember(groupId, vendResId, session);
                                //    }
                                //}

                                hdnMasterUserId.Value = row["master_user_id3"].ToString();

                                hdnPartnerUserId.Value = row["partner_user_id3"].ToString();

                                hdnInvitationStatus.Value = row["invitation_status3"].ToString();
                                hdnEmail.Value = row["email3"].ToString();
                                ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                                if (company != null)
                                {
                                    aCompanyLogo.HRef = aCompanyName.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                                    aCompanyLogo.Target = aCompanyName.Target = "_blank";
                                    imgCompanyLogo.ToolTip = "View company's profile";
                                    imgCompanyLogo.AlternateText = "Company logo";
                                    imgCompanyLogo.ImageUrl = company.CompanyLogo;

                                    HtmlGenericControl spanNotificationNewReceivedFiles = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanNotificationNewReceivedFiles3");

                                    int sendNewFilesCount = SqlCollaboration.GetUserCollaborationLibraryFilesBySenderUserId(vSession.User.Id, company.Id, 1, 1, session);
                                    if (sendNewFilesCount > 0)
                                    {
                                        Label lblNotificationNewReceivedFiles = (Label)ControlFinder.FindControlRecursive(item, "LblNotificationNewReceivedFiles3");

                                        spanNotificationNewReceivedFiles.Visible = true;
                                        lblNotificationNewReceivedFiles.Text = sendNewFilesCount.ToString() + string.Format(" new {0}", (sendNewFilesCount > 1) ? "files" : "file");
                                    }
                                    else
                                        spanNotificationNewReceivedFiles.Visible = false;

                                    string url = "collaboration-library/" + company.GuId;

                                    aBtnChoose.HRef = ControlLoader.Dashboard(vSession.User, url);
                                }
                            }
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

        protected void Rdg1_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (!IsPostBack)
                    GetPartnersList(Rdg1, UcRgd1, "id", null);
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

        protected void aBtnChoose1_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor btn = (HtmlAnchor)sender;

                var item = btn.Parent as RepeaterItem;
                if (item != null)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                        ElioUsers user = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);
                        if (user != null)
                        {
                            string url = "/collaboration-library/" + user.GuId;

                            Response.Redirect(ControlLoader.Dashboard(vSession.User, url), false);
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

        protected void BtnCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                MessageControlUp.Visible = false;

                #region Validations

                if (TbxLibraryGroupName.Text == "")
                {
                    //empty group name
                    GlobalMethods.ShowMessageControlDA(MessageControlUp, "Please, add group description", MessageTypes.Error, true, true, true, true, false);
                    return;
                }

                List<int> vendResIDs = new List<int>();

                foreach (RepeaterItem item in Rdg1.Items)
                {
                    CheckBox cbx1 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx1");
                    CheckBox cbx2 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx2");
                    CheckBox cbx3 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx3");

                    if (cbx1.Checked)
                    {
                        HiddenField hdnId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "id1");
                        vendResIDs.Add(Convert.ToInt32(hdnId1.Value));
                    }

                    if (cbx2.Checked)
                    {
                        HiddenField hdnId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "id2");
                        vendResIDs.Add(Convert.ToInt32(hdnId2.Value));
                    }

                    if (cbx3.Checked)
                    {
                        HiddenField hdnId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "id3");
                        vendResIDs.Add(Convert.ToInt32(hdnId3.Value));
                    }
                }

                if (vendResIDs.Count <= 1)
                {
                    //no selected
                    GlobalMethods.ShowMessageControlDA(MessageControlUp, "Please, you must select at least two in order to create a group", MessageTypes.Error, true, true, true, true, false);
                    return;
                }

                #endregion

                try
                {
                    if (DdlLibraryGroups.SelectedValue == "0")
                    {
                        #region Insert new Group

                        bool exist = SqlCollaboration.ExistLibraryGroupDescription(vSession.User.Id, TbxLibraryGroupName.Text.Trim(), session);
                        if (exist)
                        {
                            GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, this group name already exists by you!", MessageTypes.Error, true, true, true, true, false);
                            return;
                        }

                        session.BeginTransaction();

                        ElioCollaborationUsersLibraryGroups group = new ElioCollaborationUsersLibraryGroups();
                        group.UserId = vSession.User.Id;
                        group.GroupDescription = TbxLibraryGroupName.Text.Trim();
                        group.Sysdate = DateTime.Now;
                        group.LastUpdated = DateTime.Now;
                        group.IsActive = 1;
                        group.IsPublic = 1;

                        DataLoader<ElioCollaborationUsersLibraryGroups> loader = new DataLoader<ElioCollaborationUsersLibraryGroups>(session);
                        loader.Insert(group);

                        foreach (int vendResId in vendResIDs)
                        {
                            ElioCollaborationUsersLibraryGroupMembers member = new ElioCollaborationUsersLibraryGroupMembers();

                            member.UserId = vSession.User.Id;
                            member.VendorResellerId = vendResId;
                            member.LibraryGroupId = group.Id;
                            member.Sysdate = DateTime.Now;
                            member.LastUpdated = DateTime.Now;
                            member.IsPublic = 1;

                            DataLoader<ElioCollaborationUsersLibraryGroupMembers> loaderMem = new DataLoader<ElioCollaborationUsersLibraryGroupMembers>(session);
                            loaderMem.Insert(member);
                        }

                        session.CommitTransaction();

                        #endregion
                    }
                    else
                    {
                        #region Update Group

                        vendResIDs.Clear();

                        int groupId = Convert.ToInt32(DdlLibraryGroups.SelectedValue);

                        if (TbxLibraryGroupName.Text != DdlLibraryGroups.SelectedItem.Text)
                        {
                            bool exist = SqlCollaboration.ExistLibraryGroupDescriptionToOtherGroupId(vSession.User.Id, groupId, TbxLibraryGroupName.Text.Trim(), session);
                            if (exist)
                            {
                                GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, this name already exists to other group of yours!", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }
                            else
                            {
                                session.ExecuteQuery(@"UPDATE Elio_collaboration_users_library_groups
                                                    SET group_description = @group_description
                                                    WHERE id = @id
                                                    AND user_id = @user_id"
                                                    , DatabaseHelper.CreateStringParameter("@group_description", TbxLibraryGroupName.Text.Trim())
                                                    , DatabaseHelper.CreateIntParameter("@id", groupId)
                                                    , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));
                            }
                        }

                        session.BeginTransaction();

                        foreach (RepeaterItem item in Rdg1.Items)
                        {
                            HiddenField hdnId1 = (HiddenField)ControlFinder.FindControlRecursive(item, "id1");
                            HiddenField hdnId2 = (HiddenField)ControlFinder.FindControlRecursive(item, "id2");
                            HiddenField hdnId3 = (HiddenField)ControlFinder.FindControlRecursive(item, "id3");

                            CheckBox cbx1 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx1");
                            CheckBox cbx2 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx2");
                            CheckBox cbx3 = (CheckBox)ControlFinder.FindControlRecursive(item, "Cbx3");

                            if (hdnId1.Value != "")
                            {
                                bool exist = SqlCollaboration.IsLibraryGroupMember(groupId, Convert.ToInt32(hdnId1.Value), session);
                                if (cbx1.Checked)
                                {
                                    if (!exist)
                                        vendResIDs.Add(Convert.ToInt32(hdnId1.Value));
                                }
                                else
                                {
                                    if (exist)
                                        session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members 
                                                        WHERE user_id = @user_id 
                                                        AND vendor_reseller_id = @vendor_reseller_id 
                                                        AND library_group_id = @library_group_id"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", Convert.ToInt32(hdnId1.Value))
                                                            , DatabaseHelper.CreateIntParameter("@library_group_id", groupId));
                                }
                            }

                            if (hdnId2.Value != "")
                            {
                                bool exist = SqlCollaboration.IsLibraryGroupMember(groupId, Convert.ToInt32(hdnId2.Value), session);
                                if (cbx2.Checked)
                                {
                                    if (!exist)
                                        vendResIDs.Add(Convert.ToInt32(hdnId2.Value));
                                }
                                else
                                {
                                    if (exist)
                                        session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members 
                                                    WHERE user_id = @user_id 
                                                    AND vendor_reseller_id = @vendor_reseller_id 
                                                    AND library_group_id = @library_group_id"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", Convert.ToInt32(hdnId2.Value))
                                                            , DatabaseHelper.CreateIntParameter("@library_group_id", groupId));
                                }
                            }

                            if (hdnId3.Value != "")
                            {
                                bool exist = SqlCollaboration.IsLibraryGroupMember(groupId, Convert.ToInt32(hdnId3.Value), session);
                                if (cbx3.Checked)
                                {
                                    if (!exist)
                                        vendResIDs.Add(Convert.ToInt32(hdnId3.Value));
                                }
                                else
                                {
                                    if (exist)
                                        session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members 
                                                        WHERE user_id = @user_id 
                                                        AND vendor_reseller_id = @vendor_reseller_id 
                                                        AND library_group_id = @library_group_id"
                                                            , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                            , DatabaseHelper.CreateIntParameter("@vendor_reseller_id", Convert.ToInt32(hdnId3.Value))
                                                            , DatabaseHelper.CreateIntParameter("@library_group_id", groupId));
                                }
                            }
                        }

                        foreach (int vendResId in vendResIDs)
                        {
                            ElioCollaborationUsersLibraryGroupMembers member = new ElioCollaborationUsersLibraryGroupMembers();

                            member.UserId = vSession.User.Id;
                            member.VendorResellerId = vendResId;
                            member.LibraryGroupId = groupId;
                            member.Sysdate = DateTime.Now;
                            member.LastUpdated = DateTime.Now;
                            member.IsPublic = 1;

                            DataLoader<ElioCollaborationUsersLibraryGroupMembers> loaderMem = new DataLoader<ElioCollaborationUsersLibraryGroupMembers>(session);
                            loaderMem.Insert(member);
                        }

                        session.CommitTransaction();

                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    session.RollBackTransaction();
                    GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, something went wrong! Please try again later or contact us", MessageTypes.Error, true, true, true, true, false);
                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                }

                #region Reset Panel Items

                ResetPanelItems();

                GetPartnersList(Rdg1, UcRgd1, "id", null);
                Rdg1.DataBind();

                GlobalMethods.ShowMessageControlDA(MessageControlUp, (DdlLibraryGroups.SelectedValue == "0") ? "Group created successfully" : "Group updated successfully", MessageTypes.Success, true, true, true, true, false);

                #endregion
            }
            catch (Exception ex)
            {
                GlobalMethods.ShowMessageControlDA(MessageControlUp, "Sorry, something went wrong! Please try again later or contact us", MessageTypes.Error, true, true, true, true, false);
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        protected void aBtnCancelCreateGroup_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ResetPanelItems();
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

        protected void BtnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                divFailure.Visible = divSuccess.Visible = false;

                GlobalMethods.ShowMessageControlDA(UcConfirmationMessageControl, "Are you sure you want to delete this library group?", MessageTypes.Warning, true, true, true, false, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDeleteConfirm_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                MessageControlUp.Visible = false;

                if (vSession.User != null)
                {
                    if (DdlLibraryGroups.SelectedValue != "0")
                    {
                        try
                        {
                            session.BeginTransaction();

                            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_group_members WHERE library_group_id = @library_group_id AND user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@library_group_id", Convert.ToInt32(DdlLibraryGroups.SelectedValue))
                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            session.ExecuteQuery(@"DELETE FROM Elio_collaboration_users_library_groups WHERE id = @id AND user_id = @user_id"
                                                , DatabaseHelper.CreateIntParameter("@id", Convert.ToInt32(DdlLibraryGroups.SelectedValue))
                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id));

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();

                            string content = "Group could not be deleted! Please try again later or contact us.";
                            GlobalMethods.ShowMessageControlDA(MessageControlUp, content, MessageTypes.Error, true, true, true, true, false);
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                        ResetPanelItems();

                        GlobalMethods.ShowMessageControlDA(MessageControlUp, "Group was deleted successfully", MessageTypes.Success, true, true, true, true, false);

                        UpdatePanelContent.Update();
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                        GlobalMethods.ShowMessageControlDA(MessageControlUp, "Group could not be deleted! Please try again later or contact us.", MessageTypes.Error, true, true, true, true, false);
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                string content = "Group could not be deleted! Please try again later or contact us.";
                GlobalMethods.ShowMessageControlDA(MessageControlUp, content, MessageTypes.Error, true, true, true, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "CloseConfPopUp();", true);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        #endregion

        #region DropDownLists

        protected void DdlLibraryGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                GetPartnersList(Rdg1, UcRgd1, "id", null);
                Rdg1.DataBind();

                TbxLibraryGroupName.Text = DdlLibraryGroups.SelectedValue == "0" ? "" : DdlLibraryGroups.SelectedItem.Text;

                aBtnDeleteGroup.Visible = aBtnCancelCreateGroup.Visible = DdlLibraryGroups.SelectedValue == "0" ? false : true;
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