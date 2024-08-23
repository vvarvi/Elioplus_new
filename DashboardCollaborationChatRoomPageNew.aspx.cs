using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.Enums;
using System.Data;
using System.IO;
using Telerik.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.Roles;
using WdS.ElioPlus.Lib.Roles.EnumsRoles;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationChatRoomPageNew : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        #region Page properties

        public HtmlInputFile FileToUpload
        {
            get
            {
                //HtmlInputFile result = null;

                if (ViewState["FileToUpload"] != null)
                    return (HtmlInputFile)ViewState["FileToUpload"];
                else
                    return null;
            }
            set
            {
                ViewState["FileToUpload"] = value;
            }
        }

        public bool IsEditMode
        {
            get
            {
                if (ViewState["IsEditMode"] != null)
                    return (bool)ViewState["IsEditMode"];
                else
                    return false;
            }
            set
            {
                ViewState["IsEditMode"] = value;
            }
        }

        public int GroupId
        {
            get
            {
                if (ViewState["GroupId"] != null)
                    return Convert.ToInt32(ViewState["GroupId"]);
                else
                    return -1;
            }
            set
            {
                ViewState["GroupId"] = value;
            }
        }

        public string Key
        {
            get
            {
                if (ViewState["Key"] != null)
                    return ViewState["Key"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["Key"] = value;
            }
        }

        public bool ShowPartner
        {
            get
            {
                if (ViewState["ShowPartner"] != null)
                    return (bool)ViewState["ShowPartner"];
                else
                    return false;
            }
            set
            {
                ViewState["ShowPartner"] = value;
            }
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ElioUsers user = null;
                    bool isError = false;
                    string errorPage = string.Empty;
                    string key = string.Empty;

                    RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                    Key = key;

                    if (isError)
                    {
                        Response.Redirect(errorPage, false);
                        return;
                    }

                    bool hasRight = ManagePermissions.ManagePermissionsRights(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, "DashboardCollaborationChatRoomPage", Actions.View, session);
                    if (!hasRight)
                    {
                        Response.Redirect(ControlLoader.PageDash405, false);
                        return;
                    }
                    FixPage();

                    //List<string> logedInUsers = Global.Sessions.ToList();
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

        #region AsyncUpload Files

        protected void RadAsyncUpload1_FileUploaded(object sender, Telerik.Web.UI.FileUploadedEventArgs e)
        {
            Bitmap bitmapImage = ImageResize.ResizeBitMapImage(RadAsyncUpload1.UploadedFiles[0].InputStream);
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            bitmapImage.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
        }

        #endregion

        #region Methods

        private bool FileExists(DataTable table, string fileName)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["fileName"].ToString() == fileName)
                {
                    return true;
                }
            }

            return false;
        }

        public void GetLibraryFiles(DBSession session)
        {
            List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

            if (defaultCategories.Count > 0)
            {
                //RptFilesLibrary.Visible = true;
                UcMessageAlertLibraryControl.Visible = false;

                RpLibraryFiles.Visible = true;
                DataTable table = new DataTable();

                table.Columns.Add("id");
                //table.Columns.Add("user_id");
                table.Columns.Add("category_description");
                table.Columns.Add("total_files_count");
                //table.Columns.Add("partner_total_files_count");
                table.Columns.Add("new_files_count");
                table.Columns.Add("sysdate");
                table.Columns.Add("is_default");
                table.Columns.Add("is_public");

                ElioUsers user = Sql.GetUserByGuId(Key, session);
                ShowPartner = (user == null) ? false : true;
                LblFileLibrary.Text = (ShowPartner) ? "Library Files from/to <b>" + user.CompanyName + "</b> partner" : "Library Files";

                foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
                {
                    int totalFilesCount = 0;

                    if (user == null)
                    {
                        totalFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, defaultCategory.Id, "0,1", false, session).Count;       //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(vSession.User.Id, defaultCategory.Id, "0,1", session);
                    }
                    else
                    {
                        if (user != null)  //see partner files
                            totalFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(user.Id, vSession.User.Id, defaultCategory.Id, "0,1", true, session).Count;            //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(user.Id, defaultCategory.Id, "0,1", session);
                    }

                    int newFilesCount = 0;

                    if (user == null)
                    {
                        newFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, defaultCategory.Id, "1", false, session).Count;               //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(vSession.User.Id, defaultCategory.Id, "1", session);
                    }
                    else
                    {
                        if (user != null)  //see partner new files
                            newFilesCount += SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(user.Id, vSession.User.Id, defaultCategory.Id, "1", true, session).Count;                      //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(user.Id, defaultCategory.Id, "1", session);
                    }

                    table.Rows.Add(defaultCategory.Id, defaultCategory.CategoryDescription, totalFilesCount, newFilesCount, defaultCategory.Sysdate, defaultCategory.IsDefault, defaultCategory.IsPublic);
                }

                RpLibraryFiles.DataSource = table;
                RpLibraryFiles.DataBind();

            }
            else
            {
                RpLibraryFiles.Visible = false;

                string alert = "You have no Library Categories Files";
                GlobalMethods.ShowMessageControlDA(UcMessageAlertLibraryControl, alert, MessageTypes.Info, true, true, false);
            }
        }

        protected void ImgBtnPreViewLogFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                SqlCollaboration.SetCollaborationLibraryFileAsViewedByFileId(Convert.ToInt32(item["id"].Text), session);

                imgBtn.Visible = false;

                GetLibraryFiles(session);

                //int simpleMsgCount = 0;
                //int groupMsgCount = 0;
                //int totalNewMsgCount = 0;
                //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

                UpdatePanel updatePanel1 = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanel1");
                if (updatePanel1 != null)
                    updatePanel1.Update();
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

        protected void ImgBtnDeleteLogFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtnDelete = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtnDelete.NamingContainer;

                int selectedCategoryId = Convert.ToInt32(item["selected_category"].Text);
                ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(selectedCategoryId, session);
                if (category != null)
                {
                    DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + category.CategoryDescription + "\\" + vSession.User.GuId + "\\"));
                    bool deleted = false;

                    foreach (FileInfo logFile in filesInDirectory.GetFiles())
                    {
                        if (logFile.Name == item["fileName"].Text)
                        {
                            logFile.Delete();
                            deleted = true;
                            break;
                        }
                    }

                    if (deleted)
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, Convert.ToInt32(item["id"].Text), false, false, session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        }

                        GetLibraryFiles(session);

                        RpLibraryFiles.Rebind();
                    }
                }
                else
                {
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;

                    GlobalMethods.ShowMessageControlDA(UcMessageAlertLibraryControl, alert, MessageTypes.Error, true, true, false);
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

        protected void RpLibraryFiles_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "Parent")
                {
                    GridDataItem item = (GridDataItem)e.Item;
                }
                else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "UserLibraryFiles")
                {
                    GridDataItem item = (GridDataItem)e.Item;

                    ImageButton imgBtnPreViewLogFiles = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnPreViewLogFiles");
                    ImageButton imgBtnDeleteLogFiles = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnDeleteLogFiles");
                    HyperLink hpLnkPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                    System.Web.UI.WebControls.Image imgFromTo = (System.Web.UI.WebControls.Image)ControlFinder.FindControlRecursive(item, "ImgFromTo");
                    ElioUsers partner = Sql.GetUserByGuId(Key, session);
                    if (partner != null)
                    {
                        if (vSession.User.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text) && partner.Id == Convert.ToInt32(item["user_id"].Text))
                        {
                            if (Convert.ToInt32(item["collaboration_group_id"].Text) > 0)
                            {
                                ElioCollaborationUsersGroups collGroup = SqlCollaboration.GetCollaborationUserGroupById(Convert.ToInt32(item["collaboration_group_id"].Text), session);
                                if (collGroup != null)
                                {
                                    imgFromTo.ToolTip = "Send to '" + collGroup.CollaborationGroupName + "' group";
                                }
                                else
                                    imgFromTo.ToolTip = "Send to group";
                            }
                            else
                                imgFromTo.ToolTip = "Send to " + partner.CompanyName;
                        }
                        else if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text) && partner.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text))
                        {
                            if (Convert.ToInt32(item["collaboration_group_id"].Text) > 0)
                            {
                                ElioCollaborationUsersGroups collGroup = SqlCollaboration.GetCollaborationUserGroupById(Convert.ToInt32(item["collaboration_group_id"].Text), session);
                                if (collGroup != null)
                                {
                                    imgFromTo.ToolTip = "Send by '" + collGroup.CollaborationGroupName + "' group";
                                }
                                else
                                    imgFromTo.ToolTip = "Send by group";
                            }
                            else
                                imgFromTo.ToolTip = "Send by " + partner.CompanyName;
                        }
                    }
                    else
                    {
                        if (vSession.User.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text) && vSession.User.Id == Convert.ToInt32(item["user_id"].Text))
                        {
                            imgFromTo.ToolTip = "Uploaded by me";
                        }
                        else if (vSession.User.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text) && vSession.User.Id != Convert.ToInt32(item["user_id"].Text))
                        {
                            partner = Sql.GetUserById(Convert.ToInt32(item["user_id"].Text), session);
                            if (partner != null)
                            {
                                if (Convert.ToInt32(item["collaboration_group_id"].Text) > 0)
                                {
                                    ElioCollaborationUsersGroups collGroup = SqlCollaboration.GetCollaborationUserGroupById(Convert.ToInt32(item["collaboration_group_id"].Text), session);
                                    if (collGroup != null)
                                    {
                                        imgFromTo.ToolTip = "Send to " + collGroup.CollaborationGroupName + " group";
                                    }
                                    else
                                        imgFromTo.ToolTip = "Send to group";
                                }
                                else
                                    imgFromTo.ToolTip = "Send to " + partner.CompanyName;
                            }
                            else
                                imgFromTo.Visible = false;
                        }
                        else if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text) && vSession.User.Id != Convert.ToInt32(item["uploaded_by_user_id"].Text))
                        {
                            partner = Sql.GetUserById(Convert.ToInt32(item["uploaded_by_user_id"].Text), session);
                            if (partner != null)
                                imgFromTo.ToolTip = "Send by " + partner.CompanyName;
                            else
                                imgFromTo.Visible = false;
                        }

                    }

                    //string filePath = item["selected_category"].Text + "/" + item["user_guid"].Text + "/" + item["fileName"].Text;
                    imgBtnPreViewLogFiles.Visible = (item["is_new"].Text == "1") ? true : false;       //(SqlCollaboration.GetCountCollaborationUsersLibraryFilesByFilePathAndNewStatus(filePath, 1, session) > 0 && (vSession.User.GuId == item["user_guid"].Text)) ? true : false;
                    imgBtnDeleteLogFiles.Visible = (vSession.User.GuId == item["user_guid"].Text) ? true : false;
                    hpLnkPreViewLogFiles.Text = item["fileName"].Text;
                    string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                    hpLnkPreViewLogFiles.NavigateUrl = url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + item["file_path"].Text;        //+ item["selected_category"].Text + "/" + item["user_guid"].Text + "/" + item["fileName"].Text;
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

        protected void RpLibraryFiles_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //UcMessageAlert.Visible = false;

                List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

                RpLibraryFiles.Visible = false;

                if (defaultCategories.Count > 0)
                {
                    RpLibraryFiles.Visible = true;
                    UcMessageAlertLibraryControl.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("category_description");
                    table.Columns.Add("total_files_count");
                    //table.Columns.Add("partner_total_files_count");
                    table.Columns.Add("new_files_count");
                    table.Columns.Add("sysdate");
                    table.Columns.Add("is_default");
                    table.Columns.Add("is_public");

                    ElioUsers user = Sql.GetUserByGuId(Key, session);
                    ShowPartner = (user == null) ? false : true;

                    foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
                    {
                        int totalPartnerFilesCount = 0;
                        int totalFilesCount = 0;

                        if (user == null)
                        {
                            totalFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, defaultCategory.Id, "0,1", false, session).Count;       //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(vSession.User.Id, defaultCategory.Id, "0,1", session);
                        }
                        else
                        {
                            if (user != null)  //see partner files
                                totalPartnerFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(user.Id, vSession.User.Id, defaultCategory.Id, "0,1", true, session).Count;            //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(user.Id, defaultCategory.Id, "0,1", session);
                        }

                        int newFilesCount = 0;

                        if (user == null)
                        {
                            newFilesCount = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, defaultCategory.Id, "1", false, session).Count;               //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(vSession.User.Id, defaultCategory.Id, "1", session);
                        }
                        else
                        {
                            if (user != null)  //see partner new files
                                newFilesCount += SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(user.Id, vSession.User.Id, defaultCategory.Id, "1", true, session).Count;                      //SqlCollaboration.GetCollaborationUserLibraryTotalPublicFilesByCategoryIdAndNewStatus(user.Id, defaultCategory.Id, "1", session);
                        }

                        table.Rows.Add(defaultCategory.Id, defaultCategory.CategoryDescription, totalFilesCount, newFilesCount, defaultCategory.Sysdate, defaultCategory.IsDefault, defaultCategory.IsPublic);
                    }

                    RpLibraryFiles.DataSource = table;
                }
                else
                {
                    RpLibraryFiles.Visible = false;
                    string alert = "You have no Library Categories Files";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlertLibraryControl, alert, MessageTypes.Info, true, true, false);
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

        protected void RpLibraryFiles_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void RpLibraryFiles_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "UserLibraryFiles":
                        {
                            int selectedCategoryID = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            DataTable table = new DataTable();

                            table.Columns.Add("id");
                            table.Columns.Add("user_id");
                            table.Columns.Add("uploaded_by_user_id");
                            table.Columns.Add("user_guid");
                            table.Columns.Add("file_path");
                            table.Columns.Add("selected_category");
                            table.Columns.Add("company_name");
                            table.Columns.Add("file_type");
                            table.Columns.Add("fileName");
                            table.Columns.Add("fileSize");
                            table.Columns.Add("date");
                            table.Columns.Add("is_new");
                            table.Columns.Add("collaboration_group_id");

                            ElioUsers partner = Sql.GetUserByGuId(Key, session);

                            if (partner == null)
                            {
                                List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                                if (userFiles.Count > 0)
                                {
                                    foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                                    {
                                        if (!FileExists(table, file.FileName))
                                            table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                                    }
                                }
                            }
                            else if (partner != null)        //(Key != "")
                            {
                                if (partner != null)
                                {
                                    List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                                    if (partnerUserFiles.Count > 0)
                                    {
                                        foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                                        {
                                            if (!FileExists(table, file.FileName))
                                                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                                        }
                                    }
                                }
                            }

                            if (table.Rows.Count > 0)
                            {
                                e.DetailTableView.DataSource = table;
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

        protected void RpLibraryFiles_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        private void FixPage()
        {
            if (!IsPostBack)
            {
                #region Clear selected chat partner

                vSession.VendorsResellersList.Clear();
                vSession.VendorsResellersList = null;

                #endregion

                ShowNotificationsMessages();
                ShowSelectedChatPartnersName(-1);
                //FillDropLists();
                GetPartnersList("id", null, 0);
                //GetLibraryFiles();

                UpdateStrings();
                SetLinks();

                UcMessageAlert.Visible = false;
                UcChatMessageAlert.Visible = false;
                //vSession.VendorsResellersList.Clear();
                //vSession.VendorsResellersList = null;
                //aInviteNew.HRef = ControlLoader.Dashboard(vSession.User, "collaboration-partners");
                divRetailorFailure.Visible = false;
                LblRetailorFailureMsg.Text = string.Empty;
                divRetailorSuccess.Visible = false;
                LblRetailorSuccessMsg.Text = string.Empty;

                divUploadFailure.Visible = false;
                divUploadSuccess.Visible = false;

                TbxGroupName.Text = string.Empty;
                //BtnDeleteConnections.ToolTip = (divTabConnections.Visible) ? "Delete connections" : "Delete groups";

                if (vSession.VendorsResellersList.Count > 0)
                {
                    RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                    if (rdgMailBox != null)
                    {
                        rdgMailBox.Rebind();
                    }
                }

                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                GetLibraryFiles(session);

                IsEditMode = false;
                GroupId = -1;
            }

            aCreateRetailor.Visible = (vSession.User.CompanyType == Types.Vendors.ToString()) ? true : false;            
        }

        private void ShowNotificationsMessages()
        {
            int simpleMsgCount = 0;
            int groupMsgCount = 0;
            int isNew = 1;
            int isViewed = 0;
            int isDeleted = 0;
            int isPublic = 1;

            int totalNewMessagesCount = SqlCollaboration.GetUserTotalSimpleNewUnreadMailBoxMessagesNotification(vSession.User.Id, vSession.User.CompanyType, isNew, isViewed, isDeleted, isPublic, out simpleMsgCount, out groupMsgCount, session);
            if (totalNewMessagesCount > 0)
            {
                if (simpleMsgCount > 0)
                {
                    spanConnectionsMsgCount.Visible = true;
                    LblConnectionsMsgCount.Text = simpleMsgCount.ToString();
                }
                else
                {
                    spanConnectionsMsgCount.Visible = false;
                    LblConnectionsMsgCount.Text = "";
                }

                if (groupMsgCount > 0)
                {
                    spanGroupMsgCount.Visible = true;
                    LblGroupMsgCount.Text = groupMsgCount.ToString();
                }
                else
                {
                    spanGroupMsgCount.Visible = false;
                    LblGroupMsgCount.Text = "";
                }
            }
            else
            {
                spanConnectionsMsgCount.Visible = false;
                spanGroupMsgCount.Visible = false;
                LblConnectionsMsgCount.Text = "";
                LblGroupMsgCount.Text = "";
            }

            //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMessagesCount);
        }

        private void SetLinks()
        {

        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        private void SetMailsAsViewed(out bool changeMailCountStatus)
        {
            changeMailCountStatus = false;
            int isDeleted = 0;
            int isPublic = 1;

            foreach (ElioCollaborationVendorsResellers coll in vSession.VendorsResellersList)
            {
                //List<ElioCollaborationUsersMailbox> collMails = SqlCollaboration.LoadCollaborationUsersMailBoxByVendResId(coll.Id, 0, "", "", session);
                List<ElioCollaborationUsersMailbox> collMails = SqlCollaboration.ChangeMailboxStatusByUserId(coll.Id, isDeleted, isPublic, vSession.User.Id, session);

                foreach (ElioCollaborationUsersMailbox collMail in collMails)
                {
                    if (collMail.IsNew == 1 && collMail.IsViewed == 0)
                    {
                        collMail.IsNew = 0;
                        collMail.IsViewed = 1;
                        collMail.DateViewed = DateTime.Now;

                        DataLoader<ElioCollaborationUsersMailbox> loader = new DataLoader<ElioCollaborationUsersMailbox>(session);
                        loader.Update(collMail);

                        changeMailCountStatus = true;
                    }

                    SqlCollaboration.SetCollaborationUsersLibraryFilesAsViewedByMailboxIdAndUserId(collMail.MailboxId, vSession.User.Id, session);
                }
            }
        }

        private bool ExistInList(ElioCollaborationVendorsResellers parnter)
        {
            bool exist = false;

            foreach (ElioCollaborationVendorsResellers item in vSession.VendorsResellersList)
            {
                if (parnter.Id == item.Id)
                    return true;
            }

            return exist;
        }

        private void AddRemoveItem1(ElioCollaborationVendorsResellers parnter, int mode)
        {
            if (mode == 0)
            {
                int i = 0;

                foreach (ElioCollaborationVendorsResellers item in vSession.VendorsResellersList)
                {
                    if (parnter.Id == item.Id)
                    {
                        vSession.VendorsResellersList.RemoveAt(i);
                        break;
                    }

                    i++;
                }
            }
            else if (mode == 1)
            {
                bool exist = false;

                foreach (ElioCollaborationVendorsResellers item in vSession.VendorsResellersList)
                {
                    if (parnter.Id == item.Id)
                    {
                        exist = true;
                    }
                }

                if (!exist)
                    vSession.VendorsResellersList.Add(parnter);
            }
        }

        private void AddRemoveItem(ElioCollaborationVendorsResellers parnter, int mode)
        {
            //int i = 0;
            //bool exist = false;
            if (vSession.VendorsResellersList.Count > 0)
            {
                foreach (ElioCollaborationVendorsResellers item in vSession.VendorsResellersList)
                {
                    if (parnter.Id == item.Id)
                    {
                        //exist = true;
                        vSession.VendorsResellersList.Clear();
                        break;
                        //mode = 0;
                        //break;
                    }
                    else
                    {
                        vSession.VendorsResellersList.Clear();
                        vSession.VendorsResellersList.Add(parnter);
                        break;
                        //exist = false;
                        //mode = 1;
                    }
                    //i++;                    
                }
            }
            else
                vSession.VendorsResellersList.Add(parnter);

            return;
            //if (mode == (int)Mode.Remove)
            //{
            //    if (exist)
            //        vSession.VendorsResellersList.RemoveAt(i);
            //}
            //else if (mode == (int)Mode.Add)
            //{
            //    if (!exist)
            //        vSession.VendorsResellersList.Add(parnter);
            //}
        }

        private void AddRemoveReceivers(ElioCollaborationVendorsResellers collaborationParnter, Mode partnerMode, Mode addRemoveMode)
        {
            if (partnerMode == Mode.SimpleMode)
            {
                if (vSession.VendorsResellersList.Count > 0)
                {
                    if (collaborationParnter.Id == vSession.VendorsResellersList[0].Id)
                    {
                        vSession.VendorsResellersList.Clear();
                    }
                    else
                    {
                        vSession.VendorsResellersList.Clear();
                        vSession.VendorsResellersList.Add(collaborationParnter);

                    }
                }
                else
                    vSession.VendorsResellersList.Add(collaborationParnter);
            }
            else if (partnerMode == Mode.GroupMode)
            {
                bool existInList = false;

                if (addRemoveMode == Mode.Add)
                {
                    if (vSession.VendorsResellersList.Count > 0)
                    {
                        foreach (ElioCollaborationVendorsResellers partner in vSession.VendorsResellersList)
                        {
                            if (partner.Id == collaborationParnter.Id)
                            {
                                existInList = true;
                                break;
                            }
                        }

                        if (!existInList)
                            vSession.VendorsResellersList.Add(collaborationParnter);
                    }
                    else
                        vSession.VendorsResellersList.Add(collaborationParnter);
                }
                else if (addRemoveMode == Mode.Remove)
                {
                    foreach (ElioCollaborationVendorsResellers partner in vSession.VendorsResellersList)
                    {
                        if (partner.Id == collaborationParnter.Id)
                        {
                            vSession.VendorsResellersList.Remove(partner);
                            break;
                        }
                    }
                }
            }
        }

        private void GetPartnersList(string orderByClause, GlobalMethods.SearchCriteria criteria, int libraryGroupId)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, libraryGroupId, session);

                if (partners.Count > 0)
                {
                    ImgBtnSendMsg.Visible = true;
                    RptConnectionList.Visible = true;
                    RptRetailorsList.Visible = true;
                    RptEditRetailorsList.Visible = true;
                    PnlCreateRetailorGroups.Enabled = true;

                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptConnectionList.DataSource = table;
                    RptConnectionList.DataBind();

                    RptRetailorsList.DataSource = table;
                    RptRetailorsList.DataBind();

                    //RptEditRetailorsList.DataSource = table;
                    //RptEditRetailorsList.DataBind();
                }
                else
                {
                    RptConnectionList.Visible = false;
                    RptRetailorsList.Visible = false;
                    //RptEditRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers yet" : "You have no Vendors yet";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlRetailors, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlEditRetailors, alert, MessageTypes.Info, true, true, false, false, false);
                    GlobalMethods.ShowMessageControlDA(MessageControlCreateRetailors, alert, MessageTypes.Info, true, true, false, false, false);

                    ImgBtnSendMsg.Visible = false;
                    PnlCreateRetailorGroups.Enabled = false;
                }
            }
        }

        private void GetPartnersListForSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    RptConnectionList.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptConnectionList.DataSource = table;
                    RptConnectionList.DataBind();
                }
                else
                {
                    RptConnectionList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers" : "You have no Vendors";
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
                    vSession.VendorsResellersList.Clear();
                    vSession.VendorsResellersList = null;
                }
            }
        }

        private void GetPartnersListForRetailorsSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    RptRetailorsList.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptRetailorsList.DataSource = table;
                    RptRetailorsList.DataBind();
                }
                else
                {
                    RptRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers results" : "You have no Vendors results";
                    GlobalMethods.ShowMessageControlDA(MessageControlRetailors, alert, MessageTypes.Info, true, true, false);
                }
            }
        }

        private void GetPartnersListForEditRetailorsSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                int isPublic = 1;
                int isDeleted = 0;
                int isNew = 1;
                int isViewed = 0;

                List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatus(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.IsAdminRole, CollaborateInvitationStatus.Confirmed.ToString(), isDeleted, isPublic, vSession.User.CompanyType, criteria, orderByClause, 0, session);

                if (partners.Count > 0)
                {
                    RptEditRetailorsList.Visible = true;
                    UcMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("master_user_id");
                    table.Columns.Add("invitation_status");
                    table.Columns.Add("partner_user_id");
                    table.Columns.Add("company_name");
                    table.Columns.Add("email");
                    table.Columns.Add("country");
                    table.Columns.Add("region");
                    table.Columns.Add("msgs_count");

                    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                    {
                        int totalNewNotViewedMsgs = SqlCollaboration.GetUserCountMailBoxMessagesByNewAndViewStatusId(partner.Id, partner.MasterUserId, partner.PartnerUserId, vSession.User.Id, isNew, isViewed, isDeleted, isPublic, session);

                        table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, partner.Country, partner.Region, totalNewNotViewedMsgs);
                    }

                    RptEditRetailorsList.DataSource = table;
                    RptEditRetailorsList.DataBind();
                }
                else
                {
                    RptEditRetailorsList.Visible = false;

                    string alert = (vSession.User.CompanyType == Types.Vendors.ToString()) ? "You have no Resellers results" : "You have no Vendors results";
                    GlobalMethods.ShowMessageControlDA(MessageControlEditRetailors, alert, MessageTypes.Info, true, true, false);
                }
            }
        }

        private void GetRetailorsGroupsList(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            if (vSession.User != null)
            {
                //if (criteria == null)
                //{
                List<ElioCollaborationUsersGroups> groups = SqlCollaboration.GetCollaborationUserGroups(vSession.User.Id, vSession.User.CompanyType, 1, 1, session);

                if (groups.Count > 0)
                {
                    RdgRetailorsGroups.Visible = true;
                    PnlCreateRetailorGroups.Enabled = true;
                    //UcSendMessageAlert.Visible = false;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("user_id");
                    table.Columns.Add("collaboration_group_name");
                    table.Columns.Add("group_msgs_count");
                    table.Columns.Add("date_created");
                    table.Columns.Add("last_update");
                    table.Columns.Add("is_active");
                    table.Columns.Add("is_public");

                    foreach (ElioCollaborationUsersGroups group in groups)
                    {
                        string strQuery = @"
                                            SELECT distinct count(mailbox_id) AS total_group_msgs_count
                                              FROM Elio_collaboration_users_group_mailbox
                                            INNER JOIN Elio_collaboration_mailbox
                                              ON Elio_collaboration_mailbox.id = Elio_collaboration_users_group_mailbox.mailbox_id 
                                              where 1 = 1
                                              AND receiver_user_id = @user_id
                                              AND sender_user_id != @user_id
                                              and is_new = @is_new
                                              and is_viewed = @is_viewed
                                              AND Elio_collaboration_users_group_mailbox.is_public = @is_public 
                                              AND Elio_collaboration_mailbox.is_public = @is_public 
                                              and group_id = @group_id";

                        int groupMsgsCount = 0;

                        DataTable tblGroupMsgs = session.GetDataTable(strQuery
                                                , DatabaseHelper.CreateIntParameter("@user_id", vSession.User.Id)
                                                , DatabaseHelper.CreateIntParameter("@is_new", 1)
                                                , DatabaseHelper.CreateIntParameter("@is_viewed", 0)
                                                , DatabaseHelper.CreateIntParameter("@is_public", 1)
                                                , DatabaseHelper.CreateIntParameter("@group_id", group.Id));

                        if (tblGroupMsgs.Rows.Count > 0)
                        {
                            groupMsgsCount = Convert.ToInt32(tblGroupMsgs.Rows[0]["total_group_msgs_count"]);
                        }

                        List<ElioCollaborationUsersGroupMembersIJUsers> groupMembers = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(group.Id, 1, 1, criteria, session);

                        if (groupMembers.Count > 0)
                        {
                            table.Rows.Add(group.Id, group.UserId, group.CollaborationGroupName, groupMsgsCount, group.DateCreated, group.LastUpdate, group.IsActive, group.IsPublic);
                        }

                        RdgRetailorsGroups.DataSource = table;
                        RdgRetailorsGroups.DataBind();
                    }

                    if (table.Rows.Count == 0)
                    {
                        RdgRetailorsGroups.Visible = false;
                        //PnlCreateRetailorGroups.Enabled = false;
                        GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "There are no Group members matching these criteria", MessageTypes.Info, true, true, false);
                    }
                    else
                    {
                        RdgRetailorsGroups.Visible = true;
                        MessageControlRetailors.Visible = false;
                    }
                }
                else
                {
                    RdgRetailorsGroups.Visible = false;
                    PnlCreateRetailorGroups.Enabled = false;
                    GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "You have no Group yet", MessageTypes.Info, true, true, false);
                }

                #region to delete

                //}
                //else
                //{
                //    List<ElioCollaborationUsersGroups> groups = SqlCollaboration.GetCollaborationUserGroups(vSession.User.Id, vSession.User.CompanyType, 1, 1, session);

                //    if (groups.Count > 0)
                //    {
                //        RdgRetailorsGroups.Visible = true;
                //        PnlCreateRetailorGroups.Enabled = true;

                //        DataTable table = new DataTable();

                //        table.Columns.Add("id");
                //        table.Columns.Add("user_id");
                //        table.Columns.Add("collaboration_group_name");
                //        table.Columns.Add("date_created");
                //        table.Columns.Add("last_update");
                //        table.Columns.Add("is_active");
                //        table.Columns.Add("is_public");

                //        foreach (ElioCollaborationUsersGroups group in groups)
                //        {
                //            List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCompanyName(2, group.Id, vSession.User.Id, criteria, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                //            if (partners!=null && partners.Count > 0)
                //            {
                //                table.Rows.Add(group.Id, group.UserId, group.CollaborationGroupName, group.DateCreated, group.LastUpdate, group.IsActive, group.IsPublic);
                //            }
                //        }

                //        if (table.Rows.Count > 0)
                //        {
                //            RdgRetailorsGroups.DataSource = table;
                //            RdgRetailorsGroups.Rebind();
                //        }
                //        else
                //        {
                //            RdgRetailorsGroups.Visible = false;
                //            PnlCreateRetailorGroups.Enabled = false;
                //            GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "You have no Group with these criteria", MessageTypes.Info, true, true, false);
                //        }
                //    }
                //    else
                //    {
                //        RdgRetailorsGroups.Visible = false;
                //        PnlCreateRetailorGroups.Enabled = false;
                //        GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "You have no Group yet", MessageTypes.Info, true, true, false);
                //    }
                //}

                #endregion
            }
            else
            {
                Response.Redirect(ControlLoader.Login, false);
            }
        }

        private void GetUserCollaborationGroupsForSearch(string orderByClause, GlobalMethods.SearchCriteria criteria)
        {
            List<ElioCollaborationUsersGroups> groups = SqlCollaboration.GetCollaborationUserGroups(vSession.User.Id, vSession.User.CompanyType, 1, 1, session);

            if (groups.Count > 0)
            {
                RdgRetailorsGroups.Visible = true;
                PnlCreateRetailorGroups.Enabled = true;

                DataTable table = new DataTable();

                table.Columns.Add("id");
                table.Columns.Add("user_id");
                table.Columns.Add("collaboration_group_name");
                table.Columns.Add("date_created");
                table.Columns.Add("last_update");
                table.Columns.Add("is_active");
                table.Columns.Add("is_public");

                foreach (ElioCollaborationUsersGroups group in groups)
                {
                    List<ElioCollaborationVendorsResellersIJUsers> partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCompanyName(2, group.Id, vSession.User.Id, criteria, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                    if (partners.Count > 0)
                    {
                        table.Rows.Add(group.Id, group.UserId, group.CollaborationGroupName, group.DateCreated, group.LastUpdate, group.IsActive, group.IsPublic);
                    }
                }

                RdgRetailorsGroups.DataSource = table;
            }
            else
            {
                RdgRetailorsGroups.Visible = false;
                PnlCreateRetailorGroups.Enabled = false;
                GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "You have no Group yet", MessageTypes.Info, true, true, false);
            }
        }

        private List<int> HasSelectedItem(Repeater rpt)
        {
            List<int> resellersIDs = new List<int>();

            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    HiddenField hdnPartnerId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                    resellersIDs.Add(Convert.ToInt32(hdnPartnerId.Value));
                }
            }

            return resellersIDs;
        }

        private bool HasSelectedItemOld(Repeater rpt)
        {
            int count = 0;
            foreach (RepeaterItem item in rpt.Items)
            {
                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                if (cbx.Checked)
                {
                    count++;
                }

                if (count > 1)
                    return true;
            }

            return false;
        }

        private void UploadInsertUserFile(HttpPostedFile file, int categoryId, int newMailBoxId, ElioUsers partner)
        {
            #region Add Receive File To Selected Category

            ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(categoryId, session);         //GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
            if (category != null)
            {
                #region Upload File to Path

                string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "/" + partner.GuId + "/";
                string fileName = "";

                UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, file, out fileName, session);
                //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                #endregion

                #region Add Library File

                ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                libraryFile.CategoryId = category.Id;
                libraryFile.FileTitle = "";
                libraryFile.FileName = fileName;
                libraryFile.FilePath = category.CategoryDescription + "/" + partner.GuId + "/" + fileName;     //serverMapPathTargetFolderToCopy;
                libraryFile.FileType = file.ContentType;
                libraryFile.IsPublic = 1;
                libraryFile.MailboxId = newMailBoxId;
                libraryFile.UserId = partner.Id;
                libraryFile.UploadedByUserId = vSession.User.Id;
                libraryFile.DateCreated = DateTime.Now;
                libraryFile.LastUpdate = DateTime.Now;

                DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                loader.Insert(libraryFile);

                #endregion

                #region Add Blob File

                ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                blobFile.FileName = fileName;
                //blobFile.CategoryDescription = category.CategoryDescription;
                //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                blobFile.FileSize = file.ContentLength;
                blobFile.FileType = file.ContentType;
                blobFile.IsPublic = 1;
                blobFile.DateCreated = DateTime.Now;
                blobFile.LastUpdate = DateTime.Now;
                blobFile.LibraryFilesId = libraryFile.Id;
                blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                blbLoader.Insert(blobFile);

                #endregion
            }

            #endregion
        }

        private void AddMailBoxToUsers(int receiversMode, List<ElioUsers> partners, List<ElioUsers> groupPartners, int groupId)
        {
            #region Add Mail Box

            ElioCollaborationMailbox newMailBox = new ElioCollaborationMailbox();

            newMailBox.MessageContent = RtbxNewMessage.Text;
            newMailBox.UserId = vSession.User.Id;
            newMailBox.DateCreated = DateTime.Now;
            newMailBox.LastUpdated = DateTime.Now;
            newMailBox.DateReceived = DateTime.Now;
            newMailBox.DateSend = DateTime.Now;
            newMailBox.TotalReplyComments = 0;
            newMailBox.IsPublic = 1;

            DataLoader<ElioCollaborationMailbox> mailBoxLoader = new DataLoader<ElioCollaborationMailbox>(session);
            mailBoxLoader.Insert(newMailBox);

            #endregion

            string fileName = "";

            if (receiversMode == 1)
            {
                if (partners.Count > 0)
                {
                    #region Simple Receivers

                    foreach (ElioUsers partner in partners)
                    {
                        #region Add User Mail Box

                        ElioCollaborationUsersMailbox collaborationUser = new ElioCollaborationUsersMailbox();
                        ElioCollaborationVendorsResellers vendorResellerCollaboration = null;

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            vendorResellerCollaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(vSession.User.Id, partner.Id, session);

                            if (vendorResellerCollaboration != null)
                            {
                                collaborationUser.VendorsResellersId = vendorResellerCollaboration.Id;
                            }

                            collaborationUser.MasterUserId = vSession.User.Id;
                            collaborationUser.PartnerUserId = partner.Id;
                        }
                        else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                        {
                            vendorResellerCollaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(partner.Id, vSession.User.Id, session);

                            if (vendorResellerCollaboration != null)
                            {
                                collaborationUser.VendorsResellersId = vendorResellerCollaboration.Id;
                            }

                            collaborationUser.MasterUserId = partner.Id;
                            collaborationUser.PartnerUserId = vSession.User.Id;
                        }

                        collaborationUser.MailboxId = newMailBox.Id;
                        collaborationUser.Sysdate = DateTime.Now;
                        collaborationUser.LastUpdated = DateTime.Now;
                        collaborationUser.IsNew = 1;
                        collaborationUser.IsViewed = 0;
                        collaborationUser.DateViewed = null;
                        collaborationUser.IsDeleted = 0;
                        collaborationUser.DateDeleted = null;
                        collaborationUser.IsPublic = 1;

                        if (!SqlCollaboration.ExistMessageAndCollaborationToUsersMailBox(collaborationUser.VendorsResellersId, newMailBox.Id, session))
                        {
                            DataLoader<ElioCollaborationUsersMailbox> usrMailBoxLoader = new DataLoader<ElioCollaborationUsersMailbox>(session);
                            usrMailBoxLoader.Insert(collaborationUser);
                        }

                        #endregion

                        #region Send File

                        var fileContentRes = inputFile.PostedFile;

                        if (divUpload.Visible)
                        {
                            if (fileContentRes != null && fileContentRes.ContentLength > 0)
                            {
                                #region Add Receive File To Selected Category

                                //ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(DdlCategory.SelectedItem.Value), session);         //GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
                                //if (category != null)
                                //{
                                #region Upload File to Path

                                string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + partner.GuId + "\\CollaborationSendFiles\\";
                                string extension = Path.GetExtension(fileContentRes.FileName);

                                bool successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                                //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                                #endregion

                                if (successUpload)
                                {
                                    #region Add Library File

                                    ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                    libraryFile.CategoryId = 0;
                                    libraryFile.FileName = fileName;
                                    libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");
                                    libraryFile.FilePath = partner.GuId + "/CollaborationSendFiles/" + fileName;     //serverMapPathTargetFolderToCopy;
                                    libraryFile.FileType = fileContentRes.ContentType;
                                    libraryFile.IsPublic = 1;
                                    libraryFile.IsNew = 1;
                                    libraryFile.MailboxId = newMailBox.Id;
                                    libraryFile.UserId = partner.Id;
                                    libraryFile.UploadedByUserId = vSession.User.Id;
                                    libraryFile.DateCreated = DateTime.Now;
                                    libraryFile.LastUpdate = DateTime.Now;
                                    libraryFile.CollaborationGroupId = 0;

                                    DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                    loader.Insert(libraryFile);

                                    #endregion

                                    #region Add Blob File

                                    ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                    blobFile.FileName = fileName;
                                    //blobFile.CategoryDescription = category.CategoryDescription;
                                    //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                    blobFile.FileSize = fileContentRes.ContentLength;
                                    blobFile.FileType = fileContentRes.ContentType;
                                    blobFile.IsPublic = 1;
                                    blobFile.DateCreated = DateTime.Now;
                                    blobFile.LastUpdate = DateTime.Now;
                                    blobFile.LibraryFilesId = libraryFile.Id;
                                    blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                    DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                    blbLoader.Insert(blobFile);

                                    #endregion
                                }
                                else
                                {
                                    throw new Exception("File size to big to be uploaded");
                                }
                                //}

                                #endregion
                            }
                            //else
                            //{
                                //GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "You have to select category for your file in order to send it", MessageTypes.Info, true, true, false);
                                //return;

                                #region Add Receive File To Received Files Category

                                //ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
                                //if (category != null)
                                //{
                                //    #region Upload File to Path

                                //    string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + partner.GuId + "\\";

                                //    UpLoadImage.UpLoadFile(vSession.User, serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);

                                //    #endregion

                                //    #region Add Library File

                                //    ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                //    libraryFile.CategoryId = category.Id;
                                //    libraryFile.FileTitle = "";
                                //    libraryFile.FileName = fileName;
                                //    libraryFile.FilePath = category.CategoryDescription + "\\" + partner.GuId + "\\" + fileName;     //serverMapPathTargetFolderToCopy;
                                //    libraryFile.FileType = fileContentRes.ContentType;
                                //    libraryFile.IsPublic = 1;
                                //    libraryFile.IsNew = 1;
                                //    libraryFile.MailboxId = newMailBox.Id;
                                //    libraryFile.UserId = partner.Id;
                                //    libraryFile.UploadedByUserId = vSession.User.Id;
                                //    libraryFile.DateCreated = DateTime.Now;
                                //    libraryFile.LastUpdate = DateTime.Now;

                                //    DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                //    loader.Insert(libraryFile);

                                //    #endregion

                                //    #region Add Blob File

                                //    ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                //    blobFile.FileName = fileName;
                                //    blobFile.CategoryDescription = category.CategoryDescription;
                                //    blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                //    blobFile.FileSize = fileContentRes.ContentLength;
                                //    blobFile.FileType = fileContentRes.ContentType;
                                //    blobFile.IsPublic = 1;
                                //    blobFile.DateCreated = DateTime.Now;
                                //    blobFile.LastUpdate = DateTime.Now;
                                //    blobFile.LibraryFilesId = libraryFile.Id;
                                //    blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "\\" + blobFile.FileName);

                                //    DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                //    blbLoader.Insert(blobFile);

                                //    #endregion
                                //}

                                #endregion
                            //}
                        }
                        else if (divLibrarySelectedFile.Visible && LblLibrarySelectedFile.Text != "" && HdnCategoryId.Value != "0" && HdnFileId.Value != "0")
                        {
                            #region Add File from Library to send - To Delete

                            //ElioCollaborationUsersLibraryFilesSend libraryFile = new ElioCollaborationUsersLibraryFilesSend();

                            //libraryFile.FileId = Convert.ToInt32(HdnFileId.Value);
                            //libraryFile.MailboxId = newMailBox.Id;
                            //libraryFile.UserIdFrom = newMailBox.UserId;
                            //libraryFile.UserIdTo = partner.Id;
                            //libraryFile.GroupId = 0;
                            //libraryFile.DateSend = DateTime.Now;
                            //libraryFile.IsPublic = 1;

                            //DataLoader<ElioCollaborationUsersLibraryFilesSend> fileLoader = new DataLoader<ElioCollaborationUsersLibraryFilesSend>(session);
                            //fileLoader.Insert(libraryFile);

                            #endregion

                            #region Copy Receive File To Partner Folder

                            ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(HdnCategoryId.Value), session);         //GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
                            if (category != null)
                            {
                                ElioCollaborationUsersLibraryFiles file = SqlCollaboration.GetCollaborationUserLibraryFileById(Convert.ToInt32(HdnFileId.Value), session);
                                if (file != null && file.FileName != "")
                                {
                                    #region Copy File to Path

                                    string serverMapPathSourceFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + vSession.User.GuId + "\\";

                                    string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + partner.GuId + "\\";

                                    bool successCopy = UpLoadImage.MoveFileToFolder(serverMapPathSourceFolder, serverMapPathTargetFolder, file.FileName);

                                    #endregion

                                    if (successCopy)
                                    {
                                        #region Add Library File

                                        ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                        libraryFile.CategoryId = category.Id;                                        
                                        libraryFile.FileName = file.FileName;
                                        libraryFile.FileTitle = file.FileTitle;
                                        libraryFile.FilePath = category.CategoryDescription + "/" + partner.GuId + "/" + file.FileName;     //serverMapPathTargetFolderToCopy;
                                        libraryFile.FileType = file.FileType;
                                        libraryFile.IsPublic = 1;
                                        libraryFile.IsNew = 1;
                                        libraryFile.MailboxId = newMailBox.Id;
                                        libraryFile.UserId = partner.Id;
                                        libraryFile.UploadedByUserId = vSession.User.Id;
                                        libraryFile.DateCreated = DateTime.Now;
                                        libraryFile.LastUpdate = DateTime.Now;
                                        libraryFile.CollaborationGroupId = 0;

                                        DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                        loader.Insert(libraryFile);

                                        #endregion

                                        #region Add Blob File

                                        ElioCollaborationBlobFiles blob = SqlCollaboration.GetCollaborationBlobFileByFileId(file.Id, session);
                                        if (blob != null)
                                        {
                                            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                            blobFile.FileName = libraryFile.FileName;
                                            //blobFile.CategoryDescription = category.CategoryDescription;
                                            //blobFile.FilePath = libraryFile.FilePath;
                                            blobFile.FileSize = blob.FileSize;
                                            blobFile.FileType = libraryFile.FileType;
                                            blobFile.IsPublic = 1;
                                            blobFile.DateCreated = DateTime.Now;
                                            blobFile.LastUpdate = DateTime.Now;
                                            blobFile.LibraryFilesId = libraryFile.Id;
                                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + libraryFile.FileName);

                                            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                            blbLoader.Insert(blobFile);
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        throw new Exception("File size to big to be uploaded");
                                    }
                                }
                            }

                            #endregion
                        }

                        #endregion
                    }

                    #endregion
                }
            }
            else if (receiversMode == 2)
            {
                if (groupPartners.Count > 0 && groupId > -1)
                {
                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        #region Group Receivers For Vendor's resellers

                        foreach (ElioUsers groupPartner in groupPartners)
                        {
                            #region Add User Mail Box

                            ElioCollaborationUsersMailbox collaborationUserMailbox = new ElioCollaborationUsersMailbox();
                            ElioCollaborationVendorsResellers vendorResellerCollaboration = null;

                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                vendorResellerCollaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(vSession.User.Id, groupPartner.Id, session);

                                if (vendorResellerCollaboration != null)
                                {
                                    collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                                }

                                collaborationUserMailbox.MasterUserId = vSession.User.Id;
                                collaborationUserMailbox.PartnerUserId = groupPartner.Id;
                            }

                            #region to delete

                            //to do
                            ////////else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            ////////{
                            ////////    if (groupPartner.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            ////////    {
                            ////////        vendorResellerCollaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(vSession.User.Id, groupPartner.Id, session);

                            ////////        if (vendorResellerCollaboration != null)
                            ////////        {
                            ////////            collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                            ////////        }

                            ////////        collaborationUserMailbox.MasterUserId = vSession.User.Id;
                            ////////        collaborationUserMailbox.PartnerUserId = groupPartner.Id;
                            ////////    }
                            ////////}

                            //else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            //{
                            //    if (vSession.User.Id != groupPartner.Id)
                            //    {
                            //        vendorResellerCollaboration = SqlCollaboration.GetCollaborationBetweenResellersId(vSession.User.Id, groupPartner.Id, session);

                            //        if (vendorResellerCollaboration != null)
                            //        {
                            //            collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                            //        }
                            //        else
                            //        {
                            //            vendorResellerCollaboration = new ElioCollaborationVendorsResellers();

                            //            vendorResellerCollaboration.MasterUserId = vSession.User.Id;
                            //            vendorResellerCollaboration.PartnerUserId = groupPartner.Id;
                            //            vendorResellerCollaboration.InvitationStatus = CollaborateInvitationStatus.Confirmed.ToString();
                            //            vendorResellerCollaboration.Sysdate = DateTime.Now;
                            //            vendorResellerCollaboration.LastUpdated = DateTime.Now;
                            //            vendorResellerCollaboration.IsActive = 1;

                            //            DataLoader<ElioCollaborationVendorsResellers> loader = new DataLoader<ElioCollaborationVendorsResellers>(session);
                            //            loader.Insert(vendorResellerCollaboration);

                            //            collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                            //        }

                            //        collaborationUserMailbox.MasterUserId = vendorResellerCollaboration.MasterUserId;       // groupPartner.Id;
                            //        collaborationUserMailbox.PartnerUserId = vendorResellerCollaboration.PartnerUserId;     // vSession.User.Id;
                            //    }
                            //}

                            #endregion

                            collaborationUserMailbox.MailboxId = newMailBox.Id;
                            collaborationUserMailbox.Sysdate = DateTime.Now;
                            collaborationUserMailbox.LastUpdated = DateTime.Now;
                            collaborationUserMailbox.IsNew = 1;
                            collaborationUserMailbox.IsViewed = 0;
                            collaborationUserMailbox.DateViewed = null;
                            collaborationUserMailbox.IsDeleted = 0;
                            collaborationUserMailbox.DateDeleted = null;
                            collaborationUserMailbox.IsPublic = 1;

                            if (!SqlCollaboration.ExistMessageAndCollaborationToUsersMailBox(collaborationUserMailbox.VendorsResellersId, newMailBox.Id, session))
                            {
                                DataLoader<ElioCollaborationUsersMailbox> usrMailBoxLoader = new DataLoader<ElioCollaborationUsersMailbox>(session);
                                usrMailBoxLoader.Insert(collaborationUserMailbox);

                                ElioCollaborationUsersGroupRetailorsMailbox groupRetailorMailbox = new ElioCollaborationUsersGroupRetailorsMailbox();

                                groupRetailorMailbox.UserId = vSession.User.Id;
                                groupRetailorMailbox.MasterUserId = collaborationUserMailbox.MasterUserId;
                                groupRetailorMailbox.PartnerUserId = collaborationUserMailbox.PartnerUserId;
                                groupRetailorMailbox.CollaborationGroupId = groupId;
                                groupRetailorMailbox.CollaborationVendorsResellersId = collaborationUserMailbox.VendorsResellersId;
                                groupRetailorMailbox.CollaborationUsersMailbox = collaborationUserMailbox.Id;
                                groupRetailorMailbox.MailboxId = collaborationUserMailbox.MailboxId;
                                groupRetailorMailbox.Sysdate = DateTime.Now;
                                groupRetailorMailbox.LastUpdate = DateTime.Now;

                                DataLoader<ElioCollaborationUsersGroupRetailorsMailbox> rMailboxLoader = new DataLoader<ElioCollaborationUsersGroupRetailorsMailbox>(session);
                                rMailboxLoader.Insert(groupRetailorMailbox);
                            }

                            #endregion

                            #region If exist file for send

                            var fileContentRes = inputFile.PostedFile;

                            if (divUpload.Visible)
                            {
                                if (fileContentRes != null && fileContentRes.ContentLength > 0)
                                {
                                    #region Add Receive File To Selected Category

                                    //ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(DdlCategory.SelectedItem.Value), session);
                                    //if (category != null)
                                    //{
                                    #region Upload File to Path

                                    string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + groupPartner.GuId + "\\CollaborationSendFiles\\";
                                    string extension = Path.GetExtension(fileContentRes.FileName);

                                    bool successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                                    //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                                    #endregion

                                    if (successUpload)
                                    {
                                        #region Add Library File

                                        ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                        libraryFile.CategoryId = 0;
                                        libraryFile.FileName = fileName;
                                        libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");
                                        libraryFile.FilePath = groupPartner.GuId + "/CollaborationSendFiles/" + fileName;      //serverMapPathTargetFolderToCopy;
                                        libraryFile.FileType = fileContentRes.ContentType;
                                        libraryFile.IsPublic = 1;
                                        libraryFile.IsNew = 1;
                                        libraryFile.MailboxId = newMailBox.Id;
                                        libraryFile.UserId = groupPartner.Id;
                                        libraryFile.UploadedByUserId = vSession.User.Id;
                                        libraryFile.DateCreated = DateTime.Now;
                                        libraryFile.LastUpdate = DateTime.Now;
                                        libraryFile.CollaborationGroupId = groupId;

                                        DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                        loader.Insert(libraryFile);

                                        #endregion

                                        #region Add Blob File

                                        ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                        blobFile.FileName = fileName;
                                        //blobFile.CategoryDescription = category.CategoryDescription;
                                        //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                        blobFile.FileSize = fileContentRes.ContentLength;
                                        blobFile.FileType = fileContentRes.ContentType;
                                        blobFile.IsPublic = 1;
                                        blobFile.DateCreated = DateTime.Now;
                                        blobFile.LastUpdate = DateTime.Now;
                                        blobFile.LibraryFilesId = libraryFile.Id;
                                        blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                        DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                        blbLoader.Insert(blobFile);

                                        #endregion
                                    }
                                    else
                                    {
                                        throw new Exception("File size to big to be uploaded");
                                    }
                                    //}

                                    #endregion
                                }
                                //else
                                //{
                                    #region Add Receive File To Received Files
                                    /*
                                    ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Receive Files", session);
                                    if (category != null)
                                    {
                                        #region Upload File to Path

                                        string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + groupPartner.GuId + "\\";
                                        string extension = Path.GetExtension(fileContentRes.FileName);

                                        UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                                        //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                                        #endregion

                                        #region Add Library File

                                        ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                        libraryFile.CategoryId = category.Id;
                                        libraryFile.FileName = fileName;
                                        libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");
                                        libraryFile.FilePath = category.CategoryDescription + "/" + groupPartner.GuId + "/" + fileName;         //serverMapPathTargetFolderToCopy;
                                        libraryFile.FileType = fileContentRes.ContentType;
                                        libraryFile.IsPublic = 1;
                                        libraryFile.IsNew = 1;
                                        libraryFile.MailboxId = newMailBox.Id;
                                        libraryFile.UserId = groupPartner.Id;
                                        libraryFile.UploadedByUserId = vSession.User.Id;
                                        libraryFile.DateCreated = DateTime.Now;
                                        libraryFile.LastUpdate = DateTime.Now;
                                        libraryFile.CollaborationGroupId = groupId;

                                        DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                        loader.Insert(libraryFile);

                                        #endregion

                                        #region Add Blob File

                                        ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                        blobFile.FileName = fileName;
                                        //blobFile.CategoryDescription = category.CategoryDescription;
                                        //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                        blobFile.FileSize = fileContentRes.ContentLength;
                                        blobFile.FileType = fileContentRes.ContentType;
                                        blobFile.IsPublic = 1;
                                        blobFile.DateCreated = DateTime.Now;
                                        blobFile.LastUpdate = DateTime.Now;
                                        blobFile.LibraryFilesId = libraryFile.Id;
                                        blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                        DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                        blbLoader.Insert(blobFile);

                                        #endregion
                                    }
                                    */
                                    #endregion
                                //}
                            }
                            else if (divLibrarySelectedFile.Visible && LblLibrarySelectedFile.Text != "" && HdnCategoryId.Value != "0" && HdnFileId.Value != "0")
                            {
                                #region Add File from Library to send - To Delete

                                //ElioCollaborationUsersLibraryFilesSend libraryFile = new ElioCollaborationUsersLibraryFilesSend();

                                //libraryFile.FileId = Convert.ToInt32(HdnFileId.Value);
                                //libraryFile.MailboxId = newMailBox.Id;
                                //libraryFile.UserIdFrom = newMailBox.UserId;
                                //libraryFile.UserIdTo = partner.Id;
                                //libraryFile.GroupId = 0;
                                //libraryFile.DateSend = DateTime.Now;
                                //libraryFile.IsPublic = 1;

                                //DataLoader<ElioCollaborationUsersLibraryFilesSend> fileLoader = new DataLoader<ElioCollaborationUsersLibraryFilesSend>(session);
                                //fileLoader.Insert(libraryFile);

                                #endregion

                                #region Copy Receive File To Partner Folder

                                ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(HdnCategoryId.Value), session);         //GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
                                if (category != null)
                                {
                                    ElioCollaborationUsersLibraryFiles file = SqlCollaboration.GetCollaborationUserLibraryFileById(Convert.ToInt32(HdnFileId.Value), session);
                                    if (file != null)
                                    {
                                        #region Copy File to Path

                                        string serverMapPathSourceFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + vSession.User.GuId + "\\";

                                        string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + groupPartner.GuId + "\\";

                                        bool successCopy = UpLoadImage.MoveFileToFolder(serverMapPathSourceFolder, serverMapPathTargetFolder, fileName);

                                        #endregion

                                        if (successCopy)
                                        {
                                            #region Add Library File

                                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                            libraryFile.CategoryId = category.Id;                                            
                                            libraryFile.FileName = file.FileName;
                                            libraryFile.FileTitle = file.FileTitle;
                                            libraryFile.FilePath = category.CategoryDescription + "/" + groupPartner.GuId + "/" + file.FileName;     //serverMapPathTargetFolderToCopy;
                                            libraryFile.FileType = file.FileType;
                                            libraryFile.IsPublic = 1;
                                            libraryFile.IsNew = 1;
                                            libraryFile.MailboxId = newMailBox.Id;
                                            libraryFile.UserId = groupPartner.Id;
                                            libraryFile.UploadedByUserId = vSession.User.Id;
                                            libraryFile.DateCreated = DateTime.Now;
                                            libraryFile.LastUpdate = DateTime.Now;
                                            libraryFile.CollaborationGroupId = groupId;

                                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                            loader.Insert(libraryFile);

                                            #endregion

                                            #region Add Blob File

                                            ElioCollaborationBlobFiles blob = SqlCollaboration.GetCollaborationBlobFileByFileId(file.Id, session);
                                            if (blob != null)
                                            {
                                                ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                                blobFile.FileName = libraryFile.FileName;
                                                //blobFile.CategoryDescription = category.CategoryDescription;
                                                //blobFile.FilePath = libraryFile.FilePath;
                                                blobFile.FileSize = blob.FileSize;
                                                blobFile.FileType = libraryFile.FileType;
                                                blobFile.IsPublic = 1;
                                                blobFile.DateCreated = DateTime.Now;
                                                blobFile.LastUpdate = DateTime.Now;
                                                blobFile.LibraryFilesId = libraryFile.Id;
                                                blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + libraryFile.FileName);

                                                DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                                blbLoader.Insert(blobFile);
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            throw new Exception("File size to big to be uploaded");
                                        }
                                    }
                                }

                                #endregion
                            }

                            #endregion
                        }

                        #endregion
                    }
                    else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                    {
                        #region Group Receivers For Resellers resellers or vendor

                        foreach (ElioUsers groupPartner in groupPartners)
                        {
                            #region Add User Mail Box

                            ElioCollaborationUsersMailbox collaborationUserMailbox = new ElioCollaborationUsersMailbox();
                            ElioCollaborationVendorsResellers vendorResellerCollaboration = null;

                            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                            {
                                if (groupPartner.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    vendorResellerCollaboration = SqlCollaboration.GetOrAllocateCollaborationByVendorAndResellerId(vSession.User.Id, groupPartner.Id, session);

                                    if (vendorResellerCollaboration != null)
                                    {
                                        collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                                    }

                                    collaborationUserMailbox.MasterUserId = vSession.User.Id;
                                    collaborationUserMailbox.PartnerUserId = groupPartner.Id;

                                    collaborationUserMailbox.MailboxId = newMailBox.Id;
                                    collaborationUserMailbox.Sysdate = DateTime.Now;
                                    collaborationUserMailbox.LastUpdated = DateTime.Now;
                                    collaborationUserMailbox.IsNew = 1;
                                    collaborationUserMailbox.IsViewed = 0;
                                    collaborationUserMailbox.DateViewed = null;
                                    collaborationUserMailbox.IsDeleted = 0;
                                    collaborationUserMailbox.DateDeleted = null;
                                    collaborationUserMailbox.IsPublic = 1;
                                }
                                if (groupPartner.CompanyType == Types.Vendors.ToString())
                                {
                                    vendorResellerCollaboration = SqlCollaboration.GetCollaborationByVendorAndResellerId(groupPartner.Id, vSession.User.Id, session);

                                    if (vendorResellerCollaboration != null)
                                    {
                                        collaborationUserMailbox.VendorsResellersId = vendorResellerCollaboration.Id;
                                    }

                                    collaborationUserMailbox.MasterUserId = groupPartner.Id;
                                    collaborationUserMailbox.PartnerUserId = vSession.User.Id;

                                    collaborationUserMailbox.MailboxId = newMailBox.Id;
                                    collaborationUserMailbox.Sysdate = DateTime.Now;
                                    collaborationUserMailbox.LastUpdated = DateTime.Now;
                                    collaborationUserMailbox.IsNew = 1;
                                    collaborationUserMailbox.IsViewed = 0;
                                    collaborationUserMailbox.DateViewed = null;
                                    collaborationUserMailbox.IsDeleted = 0;
                                    collaborationUserMailbox.DateDeleted = null;
                                    collaborationUserMailbox.IsPublic = 1;
                                }

                                if (!SqlCollaboration.ExistMessageAndCollaborationToUsersMailBox(collaborationUserMailbox.VendorsResellersId, newMailBox.Id, session))
                                {
                                    DataLoader<ElioCollaborationUsersMailbox> usrMailBoxLoader = new DataLoader<ElioCollaborationUsersMailbox>(session);
                                    usrMailBoxLoader.Insert(collaborationUserMailbox);

                                    ElioCollaborationUsersGroupRetailorsMailbox groupRetailorMailbox = new ElioCollaborationUsersGroupRetailorsMailbox();

                                    groupRetailorMailbox.UserId = vSession.User.Id;
                                    groupRetailorMailbox.MasterUserId = collaborationUserMailbox.MasterUserId;
                                    groupRetailorMailbox.PartnerUserId = collaborationUserMailbox.PartnerUserId;
                                    groupRetailorMailbox.CollaborationGroupId = groupId;
                                    groupRetailorMailbox.CollaborationVendorsResellersId = collaborationUserMailbox.VendorsResellersId;
                                    groupRetailorMailbox.CollaborationUsersMailbox = collaborationUserMailbox.Id;
                                    groupRetailorMailbox.MailboxId = collaborationUserMailbox.MailboxId;
                                    groupRetailorMailbox.Sysdate = DateTime.Now;
                                    groupRetailorMailbox.LastUpdate = DateTime.Now;

                                    DataLoader<ElioCollaborationUsersGroupRetailorsMailbox> rMailboxLoader = new DataLoader<ElioCollaborationUsersGroupRetailorsMailbox>(session);
                                    rMailboxLoader.Insert(groupRetailorMailbox);
                                }
                            }

                            #endregion

                            #region If exist file for send

                            var fileContentRes = inputFile.PostedFile;

                            if (divUpload.Visible)
                            {
                                if (fileContentRes != null && fileContentRes.ContentLength > 0)
                                {
                                    #region Add Receive File To Selected Category

                                    //ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(DdlCategory.SelectedItem.Value), session);
                                    //if (category != null)
                                    //{
                                    #region Upload File to Path

                                    string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + groupPartner.GuId + "\\CollaborationSendFiles\\";
                                    string extension = Path.GetExtension(fileContentRes.FileName);

                                    bool successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                                    //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                                    #endregion

                                    if (successUpload)
                                    {
                                        #region Add Library File

                                        ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                        libraryFile.CategoryId = 0;
                                        libraryFile.FileName = fileName;
                                        libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");
                                        libraryFile.FilePath = groupPartner.GuId + "/CollaborationSendFiles/" + fileName;      //serverMapPathTargetFolderToCopy;
                                        libraryFile.FileType = fileContentRes.ContentType;
                                        libraryFile.IsPublic = 1;
                                        libraryFile.IsNew = 1;
                                        libraryFile.MailboxId = newMailBox.Id;
                                        libraryFile.UserId = groupPartner.Id;
                                        libraryFile.UploadedByUserId = vSession.User.Id;
                                        libraryFile.DateCreated = DateTime.Now;
                                        libraryFile.LastUpdate = DateTime.Now;

                                        DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                        loader.Insert(libraryFile);

                                        #endregion

                                        #region Add Blob File

                                        ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                        blobFile.FileName = fileName;
                                        //blobFile.CategoryDescription = category.CategoryDescription;
                                        //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                        blobFile.FileSize = fileContentRes.ContentLength;
                                        blobFile.FileType = fileContentRes.ContentType;
                                        blobFile.IsPublic = 1;
                                        blobFile.DateCreated = DateTime.Now;
                                        blobFile.LastUpdate = DateTime.Now;
                                        blobFile.LibraryFilesId = libraryFile.Id;
                                        blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                        DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                        blbLoader.Insert(blobFile);

                                        #endregion
                                    }
                                    else
                                    {
                                        throw new Exception("File size to big to be uploaded");
                                    }
                                    //}

                                    #endregion
                                }
                                //else
                                //{
                                #region Add Receive File To Received Files
                                /*
                                ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Receive Files", session);
                                if (category != null)
                                {
                                    #region Upload File to Path

                                    string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + groupPartner.GuId + "\\";
                                    string extension = Path.GetExtension(fileContentRes.FileName);

                                    UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                                    //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                                    #endregion

                                    #region Add Library File

                                    ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                    libraryFile.CategoryId = category.Id;
                                    libraryFile.FileName = fileName;
                                    libraryFile.FileTitle = libraryFile.FileName.Replace(extension, "");
                                    libraryFile.FilePath = category.CategoryDescription + "/" + groupPartner.GuId + "/" + fileName;         //serverMapPathTargetFolderToCopy;
                                    libraryFile.FileType = fileContentRes.ContentType;
                                    libraryFile.IsPublic = 1;
                                    libraryFile.IsNew = 1;
                                    libraryFile.MailboxId = newMailBox.Id;
                                    libraryFile.UserId = groupPartner.Id;
                                    libraryFile.UploadedByUserId = vSession.User.Id;
                                    libraryFile.DateCreated = DateTime.Now;
                                    libraryFile.LastUpdate = DateTime.Now;

                                    DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                    loader.Insert(libraryFile);

                                    #endregion

                                    #region Add Blob File

                                    ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                    blobFile.FileName = fileName;
                                    //blobFile.CategoryDescription = category.CategoryDescription;
                                    //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                    blobFile.FileSize = fileContentRes.ContentLength;
                                    blobFile.FileType = fileContentRes.ContentType;
                                    blobFile.IsPublic = 1;
                                    blobFile.DateCreated = DateTime.Now;
                                    blobFile.LastUpdate = DateTime.Now;
                                    blobFile.LibraryFilesId = libraryFile.Id;
                                    blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                    DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                    blbLoader.Insert(blobFile);

                                    #endregion
                                }
                                */
                                #endregion
                                //}
                            }
                            else if (divLibrarySelectedFile.Visible && LblLibrarySelectedFile.Text != "" && HdnCategoryId.Value != "0" && HdnFileId.Value != "0")
                            {
                                #region Add File from Library to send - To Delete

                                //ElioCollaborationUsersLibraryFilesSend libraryFile = new ElioCollaborationUsersLibraryFilesSend();

                                //libraryFile.FileId = Convert.ToInt32(HdnFileId.Value);
                                //libraryFile.MailboxId = newMailBox.Id;
                                //libraryFile.UserIdFrom = newMailBox.UserId;
                                //libraryFile.UserIdTo = partner.Id;
                                //libraryFile.GroupId = 0;
                                //libraryFile.DateSend = DateTime.Now;
                                //libraryFile.IsPublic = 1;

                                //DataLoader<ElioCollaborationUsersLibraryFilesSend> fileLoader = new DataLoader<ElioCollaborationUsersLibraryFilesSend>(session);
                                //fileLoader.Insert(libraryFile);

                                #endregion

                                #region Copy Receive File To Partner Folder

                                ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(HdnCategoryId.Value), session);         //GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Received Files", session);
                                if (category != null)
                                {
                                    ElioCollaborationUsersLibraryFiles file = SqlCollaboration.GetCollaborationUserLibraryFileById(Convert.ToInt32(HdnFileId.Value), session);
                                    if (file != null)
                                    {
                                        #region Copy File to Path

                                        string serverMapPathSourceFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + vSession.User.GuId + "\\";

                                        string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + groupPartner.GuId + "\\";

                                        bool successCopy = UpLoadImage.MoveFileToFolder(serverMapPathSourceFolder, serverMapPathTargetFolder, fileName);

                                        #endregion

                                        if (successCopy)
                                        {
                                            #region Add Library File

                                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                                            libraryFile.CategoryId = category.Id;
                                            libraryFile.FileName = file.FileName;
                                            libraryFile.FileTitle = file.FileTitle;
                                            libraryFile.FilePath = category.CategoryDescription + "/" + groupPartner.GuId + "/" + file.FileName;     //serverMapPathTargetFolderToCopy;
                                            libraryFile.FileType = file.FileType;
                                            libraryFile.IsPublic = 1;
                                            libraryFile.IsNew = 1;
                                            libraryFile.MailboxId = newMailBox.Id;
                                            libraryFile.UserId = groupPartner.Id;
                                            libraryFile.UploadedByUserId = vSession.User.Id;
                                            libraryFile.DateCreated = DateTime.Now;
                                            libraryFile.LastUpdate = DateTime.Now;
                                            libraryFile.CollaborationGroupId = groupId;

                                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                                            loader.Insert(libraryFile);

                                            #endregion

                                            #region Add Blob File

                                            ElioCollaborationBlobFiles blob = SqlCollaboration.GetCollaborationBlobFileByFileId(file.Id, session);
                                            if (blob != null)
                                            {
                                                ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                                                blobFile.FileName = libraryFile.FileName;
                                                //blobFile.CategoryDescription = category.CategoryDescription;
                                                //blobFile.FilePath = libraryFile.FilePath;
                                                blobFile.FileSize = blob.FileSize;
                                                blobFile.FileType = libraryFile.FileType;
                                                blobFile.IsPublic = 1;
                                                blobFile.DateCreated = DateTime.Now;
                                                blobFile.LastUpdate = DateTime.Now;
                                                blobFile.LibraryFilesId = libraryFile.Id;
                                                blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + libraryFile.FileName);

                                                DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                                                blbLoader.Insert(blobFile);
                                            }

                                            #endregion
                                        }
                                        else
                                        {
                                            throw new Exception("File size to big to be uploaded");
                                        }
                                    }
                                }

                                #endregion
                            }

                            #endregion
                        }

                        #endregion
                    }
                }
            }

            # region Add Send File

            //string alert = string.Empty;

            //var fileContent = inputFile.PostedFile;

            //if (fileContent != null && fileContent.ContentLength > 0)
            //{
            //    if (DdlCategory.SelectedItem.Value != "0")
            //    {
            //        #region Upload File To Selected Categorys

            //        var fileSize = fileContent.ContentLength;
            //        var fileType = fileContent.ContentType;

            //        int pdfMaxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPdfSize"]);

            //        if (fileType != "application/octet-stream")
            //        {
            //            //if (fileType == "application/pdf" || (fileType == "application/csv" || fileType == "application/vnd.ms-excel"))
            //            //{
            //            if (fileSize > pdfMaxSize)
            //            {
            //                alert = "File size error!";

            //                GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, alert, MessageTypes.Error, true, true, false);
            //                //return;
            //            }
            //            //}
            //            //else
            //            //{
            //            //    alert = "Wrong file type!";

            //            //    return false;
            //            //}
            //        }
            //        else
            //        {
            //            alert = "File is application/octet-stream!";
            //            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, alert, MessageTypes.Error, true, true, false);
            //            return;
            //        }

            //        ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(DdlCategory.SelectedItem.Value), session);
            //        if (category != null)
            //        {
            //            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "\\" + vSession.User.GuId + "\\";    //

            //            UpLoadImage.UpLoadFile(vSession.User, serverMapPathTargetFolder, fileContent, out fileName, session);

            //            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

            //            libraryFile.CategoryId = category.Id;
            //            libraryFile.FileTitle = "";
            //            libraryFile.FileName = fileName;
            //            libraryFile.FilePath = category.CategoryDescription + "/" + vSession.User.GuId + "/" + fileName;         //serverMapPathTargetFolder;
            //            libraryFile.FileType = fileContent.ContentType;
            //            libraryFile.IsPublic = 1;
            //            libraryFile.IsNew = 1;
            //            libraryFile.MailboxId = newMailBox.Id;
            //            libraryFile.UserId = vSession.User.Id;
            //            libraryFile.UploadedByUserId = vSession.User.Id;
            //            libraryFile.DateCreated = DateTime.Now;
            //            libraryFile.LastUpdate = DateTime.Now;

            //            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
            //            loader.Insert(libraryFile);

            //            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

            //            blobFile.FileName = fileName;
            //            blobFile.CategoryDescription = category.CategoryDescription;
            //            blobFile.FilePath = serverMapPathTargetFolder;
            //            blobFile.FileSize = fileContent.ContentLength;
            //            blobFile.FileType = fileContent.ContentType;
            //            blobFile.IsPublic = 1;
            //            blobFile.DateCreated = DateTime.Now;
            //            blobFile.LastUpdate = DateTime.Now;
            //            blobFile.LibraryFilesId = libraryFile.Id;
            //            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobFile.FileName);

            //            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
            //            blbLoader.Insert(blobFile);
            //        }
            //        else
            //        {
            //            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "File did not uploaded because could not find category", MessageTypes.Error, true, true, false);
            //        }

            //        #endregion
            //    }
            //    else
            //    {
            //        #region Upload File To Send Files Category

            //        var fileSize = fileContent.ContentLength;
            //        var fileType = fileContent.ContentType;

            //        int pdfMaxSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPdfSize"]);

            //        if (fileType != "application/octet-stream")
            //        {
            //            //if (fileType == "application/pdf" || (fileType == "application/csv" || fileType == "application/vnd.ms-excel"))
            //            //{
            //            if (fileSize > pdfMaxSize)
            //            {
            //                alert = "File size error!";

            //                GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, alert, MessageTypes.Error, true, true, false);
            //                //return;
            //            }
            //            //}
            //            //else
            //            //{
            //            //    alert = "Wrong file type!";

            //            //    return false;
            //            //}
            //        }
            //        else
            //        {
            //            alert = "File is application/octet-stream!";
            //            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, alert, MessageTypes.Error, true, true, false);
            //            return;
            //        }

            //        ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesByCategory("Send Files", session);
            //        if (category != null)
            //        {
            //            string serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + category.CategoryDescription + "/" + vSession.User.GuId + "/";    //

            //            UpLoadImage.UpLoadFile(vSession.User, serverMapPathTargetFolder, fileContent, out fileName, session);

            //            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

            //            libraryFile.CategoryId = category.Id;
            //            libraryFile.FileTitle = "";
            //            libraryFile.FileName = fileName;
            //            libraryFile.FilePath = category.CategoryDescription + "/" + vSession.User.GuId + "/" + fileName;      //serverMapPathTargetFolder;
            //            libraryFile.FileType = fileContent.ContentType;
            //            libraryFile.IsPublic = 1;
            //            libraryFile.IsNew = 1;
            //            libraryFile.MailboxId = newMailBox.Id;
            //            libraryFile.UserId = vSession.User.Id;
            //            libraryFile.UploadedByUserId = vSession.User.Id;
            //            libraryFile.DateCreated = DateTime.Now;
            //            libraryFile.LastUpdate = DateTime.Now;

            //            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
            //            loader.Insert(libraryFile);

            //            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

            //            blobFile.FileName = fileName;
            //            blobFile.CategoryDescription = category.CategoryDescription;
            //            blobFile.FilePath = serverMapPathTargetFolder;
            //            blobFile.FileSize = fileContent.ContentLength;
            //            blobFile.FileType = fileContent.ContentType;
            //            blobFile.IsPublic = 1;
            //            blobFile.DateCreated = DateTime.Now;
            //            blobFile.LastUpdate = DateTime.Now;
            //            blobFile.LibraryFilesId = libraryFile.Id;
            //            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobFile.FileName);

            //            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
            //            blbLoader.Insert(blobFile);
            //        }
            //        else
            //        {
            //            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "File did not uploaded because could not find category", MessageTypes.Error, true, true, false);
            //        }

            //        #endregion
            //    }
            //}

            # endregion
        }

        private bool GetMsgReceivers(out List<ElioUsers> partners, out List<ElioUsers> groupPartners, out int groupId)
        {
            partners = new List<ElioUsers>();
            bool hasSelectedItem = false;
            ElioUsers partner = null;

            #region Simple Connections

            foreach (ElioCollaborationVendorsResellers collPartner in vSession.VendorsResellersList)
            {
                hasSelectedItem = true;
                var partnerUserId = collPartner.PartnerUserId;
                var masterUserId = collPartner.MasterUserId;

                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    partner = Sql.GetUserById(Convert.ToInt32(partnerUserId), session);
                }
                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    partner = Sql.GetUserById(Convert.ToInt32(masterUserId), session);
                }

                if (partner != null)
                {
                    partners.Add(partner);
                }
            }

            #region to delete

            //foreach (RepeaterItem item in RptConnectionList.Items)
            //{
            //    if (item != null)
            //    {
            //        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
            //        if (cbx != null)
            //        {
            //            if (cbx.Checked)
            //            {
            //                hasSelectedItem = true;
            //                var partnerUserId = cbx.InputAttributes["partner_user_id"];
            //                var masterUserId = cbx.InputAttributes["master_user_id"];

            //                if (vSession.User.CompanyType == Types.Vendors.ToString())
            //                {
            //                    partner = Sql.GetUserById(Convert.ToInt32(partnerUserId), session);
            //                }
            //                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            //                {
            //                    partner = Sql.GetUserById(Convert.ToInt32(masterUserId), session);
            //                }

            //                if (partner != null)
            //                {
            //                    partners.Add(partner);
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            #endregion

            #region Group Connections

            groupPartners = new List<ElioUsers>();
            //hasSelectedItem = false;
            ElioUsers groupPartner = null;
            groupId = -1;

            foreach (GridDataItem item in RdgRetailorsGroups.Items)
            {
                if (item != null)
                {
                    CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                    if (cbx != null)
                    {
                        if (cbx.Checked)
                        {
                            hasSelectedItem = true;
                            groupId = Convert.ToInt32(item["id"].Text);
                            string orderByClause = "Elio_collaboration_vendors_resellers.id";

                            List<ElioCollaborationVendorsResellersIJUsers> vendorResellers = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(2, groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                            foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorResellers)
                            {
                                if (vSession.User.Id != vendorReseller.PartnerUserId)
                                {
                                    //if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    //{
                                    groupPartner = Sql.GetUserById(vendorReseller.PartnerUserId, session);
                                    //}
                                    //else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                    //{
                                    //    groupPartner = Sql.GetUserById(vendorReseller.MasterUserId, session);
                                    //}

                                    if (groupPartner != null)
                                    {
                                        groupPartners.Add(groupPartner);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            #endregion

            return hasSelectedItem;
        }

        private bool GetPartnersToReceiveMessage(out List<ElioUsers> partners)
        {
            partners = new List<ElioUsers>();
            bool hasSelectedItem = false;

            if (vSession.VendorsResellersList.Count > 0)
            {
                #region Receivers

                foreach (ElioCollaborationVendorsResellers collPartner in vSession.VendorsResellersList)
                {
                    ElioUsers partner = null;

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                    {
                        partner = Sql.GetUserById(collPartner.PartnerUserId, session);
                    }
                    else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                    {
                        if (vSession.User.Id != collPartner.PartnerUserId)  //One of these partners is myself, so exclude him
                            partner = Sql.GetUserById(collPartner.PartnerUserId, session);
                    }

                    if (partner != null)
                    {
                        partners.Add(partner);
                        hasSelectedItem = true;
                    }
                }

                #region If session user is of type Reseller, then add master user of Group to include him in receivers list too

                if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                {
                    ElioUsers partner = Sql.GetUserById(Convert.ToInt32(vSession.VendorsResellersList[0].MasterUserId), session);

                    if (partner != null)
                    {
                        partners.Add(partner);
                        hasSelectedItem = true;
                    }
                }

                #endregion

                #endregion
            }

            return hasSelectedItem;
        }

        private List<ElioUsers> GetMsgGroupReceivers(out bool hasSelectedItem)
        {
            List<ElioUsers> partners = new List<ElioUsers>();
            hasSelectedItem = false;
            ElioUsers partner = null;

            #region To delete

            //foreach (RepeaterItem item in RptConnectionList.Items)
            //{
            //    if (item != null)
            //    {
            //        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
            //        if (cbx != null)
            //        {
            //            if (cbx.Checked)
            //            {
            //                hasSelectedItem = true;
            //                var partnerUserId = cbx.InputAttributes["partner_user_id"];
            //                var masterUserId = cbx.InputAttributes["master_user_id"];

            //                if (vSession.User.CompanyType == Types.Vendors.ToString())
            //                {
            //                    partner = Sql.GetUserById(Convert.ToInt32(partnerUserId), session);
            //                }
            //                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            //                {
            //                    partner = Sql.GetUserById(Convert.ToInt32(masterUserId), session);
            //                }

            //                if (partner != null)
            //                {
            //                    partners.Add(partner);
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

            foreach (GridDataItem item in RdgRetailorsGroups.Items)
            {
                if (item != null)
                {
                    CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                    if (cbx != null)
                    {
                        if (cbx.Checked)
                        {
                            hasSelectedItem = true;
                            var groupId = Convert.ToInt32(item["id"].Text);
                            string orderByClause = "Elio_collaboration_vendors_resellers.id";

                            List<ElioCollaborationVendorsResellersIJUsers> vendorResellers = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                            foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorResellers)
                            {
                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    partner = Sql.GetUserById(vendorReseller.PartnerUserId, session);
                                }
                                else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                {
                                    partner = Sql.GetUserById(vendorReseller.MasterUserId, session);
                                }

                                if (partner != null)
                                {
                                    partners.Add(partner);
                                }
                            }
                        }
                    }
                }
            }

            return partners;
        }

        private void FillDropLists()
        {
            DdlCategory.Items.Clear();

            List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

            ListItem item = new ListItem();

            item.Value = "0";
            item.Text = "Choose category";

            DdlCategory.Items.Add(item);

            foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
            {
                item = new ListItem();

                item.Value = defaultCategory.Id.ToString();
                item.Text = defaultCategory.CategoryDescription;

                DdlCategory.Items.Add(item);
            }
        }

        private void ResetChatPartnersData()
        {
            LblSelectedChatTitle.Text = "Select partner to chat with";
            LblSelectedConnectionToChat.Text = "";

            aChatCompanyLogo.HRef = "";
            aChatCompanyLogo.Visible = false;
            ImgChatCompanyLogo.Visible = false;
            ImgChatCompanyLogo.ImageUrl = "";
            ImgChatCompanyLogo.ToolTip = "";
            divGroupImages.Visible = false;
        }

        public void ShowGroupMembersImages(int groupId)
        {
            if (groupId > 0)
            {
                int isActive = 1;
                int isPublic = 1;
                List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(groupId, isActive, isPublic, null, session);

                if (members.Count > 0)
                {
                    divGroupImages.Visible = true;

                    if (members.Count <= 5)
                    {
                        if (members.Count >= 1)
                        {
                            divPic1.Visible = true;
                            Pic1.Src = members[0].CompanyLogo;
                            Pic1.Alt = members[0].CompanyName;
                        }

                        if (members.Count >= 2)
                        {
                            divPic2.Visible = true;
                            Pic2.Src = members[1].CompanyLogo;
                            Pic2.Alt = members[1].CompanyName;
                        }

                        if (members.Count >= 3)
                        {
                            divPic3.Visible = true;
                            Pic3.Src = members[2].CompanyLogo;
                            Pic3.Alt = members[2].CompanyName;
                        }

                        if (members.Count >= 4)
                        {
                            divPic4.Visible = true;
                            Pic4.Src = members[3].CompanyLogo;
                            Pic4.Alt = members[3].CompanyName;
                        }

                        if (members.Count >= 5)
                        {
                            divPic5.Visible = true;
                            Pic5.Src = members[4].CompanyLogo;
                            Pic5.Alt = members[4].CompanyName;
                        }

                        divMorePic.Visible = false;
                    }
                    else
                    {
                        divMorePic.Visible = true;
                    }
                }
                else
                    divGroupImages.Visible = false;
            }
            else
            {
                divGroupImages.Visible = false;
            }
        }

        private void ShowSelectedChatPartnersName(int groupId)
        {
            if (divTabConnections.Visible)
            {
                if (vSession.VendorsResellersList.Count > 0)
                {
                    ElioUsers partner = Sql.GetUserById((vSession.User.CompanyType == Types.Vendors.ToString()) ? vSession.VendorsResellersList[0].PartnerUserId : vSession.VendorsResellersList[0].MasterUserId, session);
                    if (partner != null)
                    {
                        LblSelectedChatTitle.Text = "You are chatting with";
                        LblSelectedConnectionToChat.Text = partner.CompanyName;
                        aChatCompanyLogo.HRef = ControlLoader.Profile(partner);
                        aChatCompanyLogo.Target = "_blank";
                        aChatCompanyLogo.Visible = true;
                        ImgChatCompanyLogo.ImageUrl = partner.CompanyLogo;
                        ImgChatCompanyLogo.Visible = true;
                        ImgChatCompanyLogo.ToolTip = "See " + partner.CompanyName + "'s profile";
                    }
                    else
                    {
                        ResetChatPartnersData();
                    }
                }
                else
                {
                    ResetChatPartnersData();
                }
            }
            else if (divTabGroups.Visible)
            {
                ResetChatPartnersData();

                if (groupId > 0)
                {
                    ElioCollaborationUsersGroups group = SqlCollaboration.GetCollaborationUserGroupById(groupId, session);
                    if (group != null)
                    {
                        LblSelectedChatTitle.Text = "You are chatting with your group";
                        LblSelectedConnectionToChat.Text = group.CollaborationGroupName;

                        ShowGroupMembersImages(group.Id);
                    }
                    else
                    {
                        LblSelectedChatTitle.Text = "Select group to chat with it's members";
                        LblSelectedConnectionToChat.Text = "";
                        divGroupImages.Visible = false;
                    }
                }
                else
                {
                    LblSelectedChatTitle.Text = "Select group to chat with it's members";
                    LblSelectedConnectionToChat.Text = "";
                    divGroupImages.Visible = false;
                }
            }
        }

        private int GetMembersGroupId()
        {
            foreach (GridDataItem item in RdgRetailorsGroups.Items)
            {
                if (item != null)
                {
                    CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                    if (cbx != null)
                    {
                        if (cbx.Checked)
                        {
                            return Convert.ToInt32(item["id"].Text);
                        }
                    }
                }
            }

            return -1;
        }

        private bool GetGroupMembersForMessages1(int groupId, out List<ElioUsers> groupPartners)
        {
            groupPartners = new List<ElioUsers>();
            ElioUsers user = new ElioUsers();
            int isActive = 1;
            int isPublic = 1;

            List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(groupId, isActive, isPublic, null, session);

            if (members.Count > 0)
            {
                if (vSession.User.CompanyType == Types.Vendors.ToString())
                {
                    foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                    {
                        user = Sql.GetUserById(member.GroupRetailorId, session);
                        if (user != null)
                            groupPartners.Add(user);
                    }
                }
                else
                {
                    bool masterInserted = false;
                    foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                    {
                        if (!masterInserted)
                        {
                            user = Sql.GetUserById(member.CreatorUserId, session);
                            if (user != null)
                                groupPartners.Add(user);

                            masterInserted = true;
                        }

                        if (vSession.User.Id != member.GroupRetailorId)
                        {
                            groupPartners.Add(user);
                        }
                    }
                }
            }

            return (groupPartners.Count > 0) ? true : false;
        }

        private void RestoreCheckBoxes(RadGrid rdg, GridDataItem rdgChechedItem, Repeater rpt, RepeaterItem rptCheckedItem)
        {
            try
            {
                if (rdg != null)
                {
                    #region Grid

                    foreach (GridDataItem item in rdg.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                        if (cbx != null && rdgChechedItem != null)
                        {
                            if (item.ClientID != rdgChechedItem.ClientID)
                                cbx.Checked = false;
                        }
                    }

                    #endregion
                }
                else if (rpt != null)
                {
                    #region Reset Repeater

                    foreach (RepeaterItem item in RptRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                        if (cbx != null && rptCheckedItem != null)
                        {
                            if (item.ClientID != rptCheckedItem.ClientID)
                                cbx.Checked = false;
                        }
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        private void ResetSelectedItems(RepeaterItem selectedItem)
        {
            foreach (RepeaterItem item in RptConnectionList.Items)
            {
                if (item != selectedItem)
                {
                    HtmlControl divMediaBody = (HtmlControl)ControlFinder.FindControlRecursive(item, "divMediaBody");
                    divMediaBody.Attributes["style"] = "padding-left:7px; padding-top:7px;";
                }
            }
        }

        public void StartDiscussionWithPartner(RepeaterItem item)
        {
            ResetSelectedItems(item);

            HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");

            var id = hdnId.Value;  //cbx.InputAttributes["id"];

            HtmlControl divMediaBody = (HtmlControl)ControlFinder.FindControlRecursive(item, "divMediaBody");

            RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
            if (rdgMailBox != null)
            {
                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(id), session);
                if (vendRes != null)
                {
                    vSession.VendorsResellersList = GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.SimpleMode, Mode.Any, vSession.VendorsResellersList, vendRes);

                    if (vSession.VendorsResellersList.Count > 0)
                    {
                        if (divMediaBody != null)
                        {
                            divMediaBody.Attributes["style"] = "background-color:#cccccc;padding-left:7px; padding-top:7px;";
                        }

                        bool changeMailCountStatus = false;
                        SetMailsAsViewed(out changeMailCountStatus);

                        rdgMailBox.Visible = true;
                        Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                        if (ucMailBoxListMessageAlert != null)
                            ucMailBoxListMessageAlert.Visible = false;

                        if (changeMailCountStatus)
                        {
                            Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                            HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                            spanMessagesCountNotification.Visible = false;
                            lblMessagesCount.Text = "";

                            ShowNotificationsMessages();
                        }

                        HtmlControl divSimpleMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divSimpleMailBox");
                        divSimpleMailBox.Visible = true;

                        HtmlControl divGroupMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divGroupMailBox");
                        divGroupMailBox.Visible = false;

                        rdgMailBox.Rebind();
                        UpdatePanel updatePanelChat = (UpdatePanel)ControlFinder.FindControlRecursive(this, "UpdatePanelChat");
                        updatePanelChat.Update();
                    }
                    else
                    {
                        if (divMediaBody != null)
                        {
                            divMediaBody.Attributes["style"] = "padding-left:7px; padding-top:7px;";
                        }

                        UcRdgMailBoxList.FixPage();
                    }
                }
                else
                {
                    UcRdgMailBoxList.FixPage();
                }

                ShowSelectedChatPartnersName(0);

                //int simpleMsgCount = 0;
                //int groupMsgCount = 0;
                //int totalNewMsgCount = 0;
                //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

                //UpdatePanel updatePanel1 = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanel1");
                //if (updatePanel1 != null)
                //    updatePanel1.Update();
            }
        }

        private void AddGroupUsersMailbox(List<ElioUsers> groupMembers, int groupId)
        {
            #region Add Mail Box

            ElioCollaborationMailbox newMailBox = new ElioCollaborationMailbox();

            newMailBox.MessageContent = RtbxNewMessage.Text;
            newMailBox.UserId = vSession.User.Id;
            newMailBox.DateCreated = DateTime.Now;
            newMailBox.LastUpdated = DateTime.Now;
            newMailBox.DateReceived = DateTime.Now;
            newMailBox.DateSend = DateTime.Now;
            newMailBox.TotalReplyComments = 0;
            newMailBox.IsPublic = 1;

            DataLoader<ElioCollaborationMailbox> mailBoxLoader = new DataLoader<ElioCollaborationMailbox>(session);
            mailBoxLoader.Insert(newMailBox);

            #endregion

            foreach (ElioUsers groupMember in groupMembers)
            {
                #region Add User Group Mail Box

                ElioCollaborationUsersGroupMaibox collaborationUserGroupMailbox = new ElioCollaborationUsersGroupMaibox();

                collaborationUserGroupMailbox.SenderUserId = vSession.User.Id;
                collaborationUserGroupMailbox.ReceiverUserId = groupMember.Id;
                collaborationUserGroupMailbox.MailboxId = newMailBox.Id;
                collaborationUserGroupMailbox.GroupId = groupId;
                collaborationUserGroupMailbox.Sysdate = DateTime.Now;
                collaborationUserGroupMailbox.LastUpdated = DateTime.Now;
                collaborationUserGroupMailbox.IsNew = 1;
                collaborationUserGroupMailbox.IsViewed = 0;
                collaborationUserGroupMailbox.DateViewed = null;
                collaborationUserGroupMailbox.IsDeleted = 0;
                collaborationUserGroupMailbox.DateDeleted = null;
                collaborationUserGroupMailbox.IsPublic = 1;

                DataLoader<ElioCollaborationUsersGroupMaibox> usrGroupMailBoxLoader = new DataLoader<ElioCollaborationUsersGroupMaibox>(session);
                usrGroupMailBoxLoader.Insert(collaborationUserGroupMailbox);

                #endregion

                #region If exist file for send

                if (divUpload.Visible)
                {
                    string fileName = "";
                    var fileContentRes = inputFile.PostedFile;

                    if (fileContentRes != null && fileContentRes.ContentLength > 0)
                    {
                        #region Add Receive File To Selected Category

                        //ElioCollaborationLibraryFilesDefaultCategories category = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(Convert.ToInt32(DdlCategory.SelectedItem.Value), session);
                        //if (category != null)
                        //{
                        #region Upload File to Path

                        string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + groupMember.GuId + "\\CollaborationSendFiles\\";

                        bool successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);
                        //File.Copy(serverMapPathTargetFolder + fileContent.FileName, serverMapPathTargetFolderToCopy + fileContent.FileName);

                        #endregion

                        if (successUpload)
                        {
                            #region Add Library File

                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                            libraryFile.CategoryId = 0;
                            libraryFile.FileTitle = "";
                            libraryFile.FileName = fileName;
                            libraryFile.FilePath = groupMember.GuId + "/CollaborationSendFiles/" + fileName;      //serverMapPathTargetFolderToCopy;
                            libraryFile.FileType = fileContentRes.ContentType;
                            libraryFile.IsPublic = 1;
                            libraryFile.IsNew = 1;
                            libraryFile.MailboxId = newMailBox.Id;
                            libraryFile.UserId = groupMember.Id;
                            libraryFile.UploadedByUserId = vSession.User.Id;
                            libraryFile.DateCreated = DateTime.Now;
                            libraryFile.LastUpdate = DateTime.Now;
                            libraryFile.CollaborationGroupId = groupId;

                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                            loader.Insert(libraryFile);

                            #endregion

                            #region Add Blob File

                            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                            blobFile.FileName = fileName;
                            //blobFile.CategoryDescription = category.CategoryDescription;
                            //blobFile.FilePath = serverMapPathTargetFolderToCopy;
                            blobFile.FileSize = fileContentRes.ContentLength;
                            blobFile.FileType = fileContentRes.ContentType;
                            blobFile.IsPublic = 1;
                            blobFile.DateCreated = DateTime.Now;
                            blobFile.LastUpdate = DateTime.Now;
                            blobFile.LibraryFilesId = libraryFile.Id;
                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                            blbLoader.Insert(blobFile);

                            #endregion
                        }
                        else
                        {
                            throw new Exception("File size to big to be uploaded");
                        }
                        //}

                        #endregion
                    }
                }

                #endregion
            }
        }

        # endregion

        #region Grids

        protected void RptEditRetailorsList_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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

                        //ElioCollaborationVendorsResellersIJUsers partner = (ElioCollaborationVendorsResellersIJUsers)args.Item.DataItem;

                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        //HiddenField hdnCollaborationGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "collaboration_group_id");
                        HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id");
                        HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status");
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                        HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email");

                        hdnId.Value = row["id"].ToString();
                        cbxSelectUser.InputAttributes.Add("id", hdnId.Value);

                        hdnMasterUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["master_user_id"].ToString() : row["partner_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        hdnPartnerUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["partner_user_id"].ToString() : row["master_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        hdnInvitationStatus.Value = row["invitation_status"].ToString();
                        hdnEmail.Value = row["email"].ToString();

                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                        Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                        if (GroupId != -1)
                            cbxSelectUser.Checked = SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);

                        lblCompanyName.Text = row["company_name"].ToString();
                        lblCountry.Text = row["country"].ToString();

                        HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                        if (row["msgs_count"].ToString() != "0")
                        {
                            spanMessagesCountNotification.Visible = true;
                            lblMessagesCount.Text = row["msgs_count"].ToString();
                        }
                        else
                        {
                            spanMessagesCountNotification.Visible = false;
                            lblMessagesCount.Text = "";
                        }

                        //int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(hdnPartnerUserId.Value) : Convert.ToInt32(hdnMasterUserId.Value);
                        ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                        if (company != null)
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                            aCompanyLogo.Target = aCompanyName.Target = aCompanyName.Target = "_blanck";
                            imgBtnCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgBtnCompanyLogo.ToolTip = "View company's profile";
                            imgBtnCompanyLogo.AlternateText = "Company logo";
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

        protected void RptEditRetailorsList_ItemCommand(object sender, RepeaterCommandEventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;
                RepeaterItem item = (RepeaterItem)cbx.NamingContainer;

                if (item != null)
                {
                    RepeaterItem repeaterItem = (RepeaterItem)args.Item;

                    if (repeaterItem != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                        if (rdgMailBox != null)
                        {
                            if (cbx.Checked)
                            {
                                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(row["id"].ToString()), session);
                                if (vendRes != null)
                                {
                                    UcRdgMailBoxList.VendorsResellersList = new List<ElioCollaborationVendorsResellers>();
                                    vSession.VendorsResellersList.Add(vendRes);

                                    rdgMailBox.Visible = true;
                                    Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                                    if (ucMailBoxListMessageAlert != null)
                                        ucMailBoxListMessageAlert.Visible = false;

                                    UpdatePanel updatePanel5 = (UpdatePanel)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UpdatePanel5");
                                    updatePanel5.Update();
                                }
                            }
                            else
                            {
                                vSession.VendorsResellersList.Clear();
                                vSession.VendorsResellersList = null;
                            }

                            rdgMailBox.Rebind();
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

        protected void RptRetailorsList_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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

                        //ElioCollaborationVendorsResellersIJUsers partner = (ElioCollaborationVendorsResellersIJUsers)args.Item.DataItem;

                        CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");

                        HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                        HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        //HiddenField hdnCollaborationGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "collaboration_group_id");
                        HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id");
                        HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status");
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                        HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email");

                        hdnId.Value = row["id"].ToString();
                        cbxSelectUser.InputAttributes.Add("id", hdnId.Value);

                        if (vSession.User.CompanyType == Types.Vendors.ToString())
                        {
                            if (GroupId > 0)
                            {
                                cbxSelectUser.Checked = SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);
                            }
                        }

                        //hdnCollaborationGroupId.Value = row["collaboration_group_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("collaboration_group_id", hdnCollaborationGroupId.Value);

                        hdnMasterUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["master_user_id"].ToString() : row["partner_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        hdnPartnerUserId.Value = (vSession.User.CompanyType == Types.Vendors.ToString()) ? row["partner_user_id"].ToString() : row["master_user_id"].ToString();
                        cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        //hdnMasterUserId.Value = row["master_user_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("master_user_id", hdnMasterUserId.Value);

                        //hdnPartnerUserId.Value = row["partner_user_id"].ToString();
                        //cbxSelectUser.InputAttributes.Add("partner_user_id", hdnPartnerUserId.Value);

                        hdnInvitationStatus.Value = row["invitation_status"].ToString();
                        hdnEmail.Value = row["email"].ToString();

                        Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                        Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                        Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                        lblCompanyName.Text = row["company_name"].ToString();
                        lblCountry.Text = row["country"].ToString();

                        HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                        if (row["msgs_count"].ToString() != "0")
                        {
                            spanMessagesCountNotification.Visible = true;
                            lblMessagesCount.Text = row["msgs_count"].ToString();
                        }
                        else
                        {
                            spanMessagesCountNotification.Visible = false;
                            lblMessagesCount.Text = "";
                        }

                        //int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(hdnPartnerUserId.Value) : Convert.ToInt32(hdnMasterUserId.Value);
                        ElioUsers company = Sql.GetUserById(Convert.ToInt32(hdnPartnerUserId.Value), session);

                        if (company != null)
                        {
                            aCompanyLogo.HRef = aCompanyName.HRef = ControlLoader.Profile(company);
                            aCompanyLogo.Target = aCompanyName.Target = "_blanck";
                            imgBtnCompanyLogo.ImageUrl = company.CompanyLogo;
                            imgBtnCompanyLogo.ToolTip = "View company's profile";
                            imgBtnCompanyLogo.AlternateText = "Company logo";
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

        protected void RptRetailorsList_ItemCommand(object sender, RepeaterCommandEventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;
                RepeaterItem item = (RepeaterItem)cbx.NamingContainer;

                if (item != null)
                {
                    RepeaterItem repeaterItem = (RepeaterItem)args.Item;

                    if (repeaterItem != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                        if (rdgMailBox != null)
                        {
                            if (cbx.Checked)
                            {
                                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(row["id"].ToString()), session);
                                if (vendRes != null)
                                {
                                    UcRdgMailBoxList.VendorsResellersList = new List<ElioCollaborationVendorsResellers>();
                                    vSession.VendorsResellersList.Add(vendRes);

                                    rdgMailBox.Visible = true;
                                    Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                                    if (ucMailBoxListMessageAlert != null)
                                        ucMailBoxListMessageAlert.Visible = false;

                                    UpdatePanel updatePanel5 = (UpdatePanel)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UpdatePanel5");
                                    updatePanel5.Update();
                                }
                            }
                            else
                            {
                                vSession.VendorsResellersList.Clear();
                                vSession.VendorsResellersList = null;
                            }

                            rdgMailBox.Rebind();
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

        protected void RptConnectionList_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        HtmlAnchor btnCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "BtnCompanyName");
                        if (btnCompanyName != null)
                        {
                            DataRowView row = (DataRowView)args.Item.DataItem;

                            ImageButton imgBtnCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgBtnCompanyLogo");
                            if (imgBtnCompanyLogo != null)
                            {
                                HtmlControl divMediaBody = (HtmlControl)ControlFinder.FindControlRecursive(item, "divMediaBody");
                                if (divMediaBody != null)
                                    divMediaBody.Attributes["style"] = "padding-left:7px; padding-top:7px;";

                                HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                                HiddenField hdnMasterUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "master_user_id");
                                HiddenField hdnInvitationStatus = (HiddenField)ControlFinder.FindControlRecursive(item, "invitation_status");
                                HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");
                                HiddenField hdnEmail = (HiddenField)ControlFinder.FindControlRecursive(item, "email");

                                hdnId.Value = row["id"].ToString();

                                hdnMasterUserId.Value = row["master_user_id"].ToString();

                                hdnPartnerUserId.Value = row["partner_user_id"].ToString();

                                hdnInvitationStatus.Value = row["invitation_status"].ToString();
                                hdnEmail.Value = row["email"].ToString();

                                Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                                Label lblRegion = (Label)ControlFinder.FindControlRecursive(item, "LblRegion");
                                Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");

                                lblCountry.Text = row["country"].ToString();
                                lblRegion.Text = "(" + row["region"].ToString() + ")";

                                HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                                if (row["msgs_count"].ToString() != "0")
                                {
                                    spanMessagesCountNotification.Visible = true;
                                    lblMessagesCount.Text = row["msgs_count"].ToString();
                                }
                                else
                                {
                                    spanMessagesCountNotification.Visible = false;
                                    lblMessagesCount.Text = "";
                                }

                                Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");

                                int partnerId = (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(hdnPartnerUserId.Value) : Convert.ToInt32(hdnMasterUserId.Value);
                                ElioUsers company = Sql.GetUserById(partnerId, session);    //Convert.ToInt32(hdnPartnerUserId.Value)

                                if (company != null)
                                {
                                    //////bool isLoggedInElio = Global.IsUserInSession(company.CurrentSessionId);

                                    imgBtnCompanyLogo.ImageUrl = company.CompanyLogo;
                                    //////imgBtnCompanyLogo.ToolTip = lblCompanyName.ToolTip = "View company's profile " + string.Format("({0})", ((isLoggedInElio) ? "online" : "away"));
                                    imgBtnCompanyLogo.AlternateText = "Company logo";

                                    lblCompanyName.Text = row["company_name"].ToString();

                                    HtmlControl spanPartnerStatus = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanPartnerStatus");

                                    //////if (isLoggedInElio)
                                    //////    spanPartnerStatus.Attributes["class"] = "status bg-success";
                                    //////else
                                    //////    spanPartnerStatus.Attributes["class"] = "status bg-default";
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

        protected void RptConnectionList_ItemCommand(object sender, RepeaterCommandEventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;
                RepeaterItem item = (RepeaterItem)cbx.NamingContainer;

                if (item != null)
                {
                    RepeaterItem repeaterItem = (RepeaterItem)args.Item;

                    if (repeaterItem != null)
                    {
                        DataRowView row = (DataRowView)args.Item.DataItem;

                        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                        if (rdgMailBox != null)
                        {
                            if (cbx.Checked)
                            {
                                ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(row["id"].ToString()), session);
                                if (vendRes != null)
                                {
                                    UcRdgMailBoxList.VendorsResellersList = new List<ElioCollaborationVendorsResellers>();
                                    vSession.VendorsResellersList.Add(vendRes);

                                    rdgMailBox.Visible = true;
                                    Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                                    if (ucMailBoxListMessageAlert != null)
                                        ucMailBoxListMessageAlert.Visible = false;

                                    UpdatePanel updatePanel5 = (UpdatePanel)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UpdatePanel5");
                                    updatePanel5.Update();
                                }
                            }
                            else
                            {
                                vSession.VendorsResellersList.Clear();
                                vSession.VendorsResellersList = null;
                            }

                            rdgMailBox.Rebind();
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
                
        protected void RdgRetailorsGroups_OnItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                session.OpenConnection();

                RepeaterItem item = (RepeaterItem)args.Item;
                if (item != null)
                {
                    DataRowView row = (DataRowView)item.DataItem;
                    if (row != null)
                    {
                        #region Parent

                        HtmlControl divMenuActions = (HtmlControl)ControlFinder.FindControlRecursive(item, "divMenuActions");
                        //HtmlAnchor aEditGroup = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aEditGroup");
                        //HtmlAnchor aDeleteGroup = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDeleteGroup");
                        divMenuActions.Visible = (vSession.User.CompanyType == Types.Vendors.ToString()) ? true : false;

                        Label lblGroupMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblGroupMessagesCount");
                        HtmlControl spanGroupMessagesCount = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanGroupMessagesCount");
                        if (row["group_msgs_count"].ToString() != "" && Convert.ToInt32(row["group_msgs_count"].ToString()) > 0)
                        {
                            lblGroupMessagesCount.Text = row["group_msgs_count"].ToString();
                            spanGroupMessagesCount.Visible = true;
                        }
                        else
                        {
                            spanGroupMessagesCount.Visible = false;
                            lblGroupMessagesCount.Text = "";
                        }

                        #endregion
                    }
                }
                //else if (e.Item is GridDataItem && e.Item.OwnerTableView.Name == "UsersGroupRetailors")
                //{
                    #region Vendors-Resellers

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aCompanyLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyLogo");
                //    //HtmlAnchor aCompanyName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCompanyName");

                //    ImageButton imgCompanyLogo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgCompanyLogo");

                //    Label lblCompanyName = (Label)ControlFinder.FindControlRecursive(item, "LblCompanyName");
                //    //Label lblCountry = (Label)ControlFinder.FindControlRecursive(item, "LblCountry");
                //    //CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                //    //lblCountry.Text = item["country"].Text;
                //    //string retailorEmail = item["email"].Text;      // (vSession.User.CompanyType == Types.Vendors.ToString()) ? Convert.ToInt32(item["partner_user_id"].Text) : Convert.ToInt32(item["master_user_id"].Text);
                //    ElioUsers company = Sql.GetUserByEmail(item["email"].Text, session);

                //    if (company != null)
                //    {
                //        lblCompanyName.Text = company.CompanyName;
                //        aCompanyLogo.HRef = ControlLoader.Profile(company);
                //        aCompanyLogo.Target = "_blank";
                //        imgCompanyLogo.ImageUrl = company.CompanyLogo;
                //        imgCompanyLogo.ToolTip = "View company's profile";
                //        imgCompanyLogo.AlternateText = "Company logo";
                //    }

                    #endregion
                //}
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

        protected void RdgRetailorsGroups_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (TbxSearch.Text == string.Empty)
                    {
                        List<ElioCollaborationUsersGroups> groups = SqlCollaboration.GetCollaborationUserGroups(vSession.User.Id, vSession.User.CompanyType, 1, 1, session);

                        if (groups.Count > 0)
                        {
                            RdgRetailorsGroups.Visible = true;
                            PnlCreateRetailorGroups.Enabled = true;
                            //UcSendMessageAlert.Visible = false;

                            DataTable table = new DataTable();

                            table.Columns.Add("id");
                            table.Columns.Add("user_id");
                            table.Columns.Add("collaboration_group_name");
                            table.Columns.Add("group_msgs_count");
                            table.Columns.Add("date_created");
                            table.Columns.Add("last_update");
                            table.Columns.Add("is_active");
                            table.Columns.Add("is_public");

                            foreach (ElioCollaborationUsersGroups group in groups)
                            {
                                table.Rows.Add(group.Id, group.UserId, group.CollaborationGroupName, 0, group.DateCreated, group.LastUpdate, group.IsActive, group.IsPublic);
                            }

                            RdgRetailorsGroups.DataSource = table;
                            RdgRetailorsGroups.DataBind();
                        }
                        else
                        {
                            RdgRetailorsGroups.Visible = false;
                            PnlCreateRetailorGroups.Enabled = false;
                            GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "You have no Group yet", MessageTypes.Info, true, true, false);
                        }
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
            finally
            {
                session.CloseConnection();
            }
        }

        protected void RdgRetailorsGroups_DetailTableDataBind(object sender, GridDetailTableDataBindEventArgs e)
        {
            try
            {
                session.OpenConnection();

                GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
                switch (e.DetailTableView.Name)
                {
                    case "UsersGroupRetailors":
                        {
                            int groupId = Convert.ToInt32(dataItem.GetDataKeyValue("id").ToString());

                            //string orderByClause = "Elio_collaboration_vendors_resellers.id";

                            //List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                            GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                            criteria.CompanyName = TbxSearch.Text;
                            if (divAdvancedSearch.Visible)
                            {
                                if (TbxAdvancedSearch.Text != "")
                                    criteria.CompanyName = TbxAdvancedSearch.Text;

                                if (DdlCountries.SelectedValue != "0")
                                {
                                    criteria.Country = DdlCountries.SelectedItem.Text;
                                }

                                if (DdlRegions.SelectedValue != "0")
                                {
                                    criteria.Region = DdlRegions.SelectedItem.Text;
                                }

                                if (Cbx1.Checked)
                                    criteria.PartnerPrograms.Add(Cbx1.Text);                     //criteria[3] = "'" + Cbx1.Text + "',";
                                if (Cbx2.Checked)
                                    criteria.PartnerPrograms.Add(Cbx2.Text);                     //criteria[3] += "'" + Cbx2.Text + "',";
                                if (Cbx3.Checked)
                                    criteria.PartnerPrograms.Add(Cbx3.Text);                     //criteria[3] += "'" + Cbx3.Text + "',";
                                if (Cbx4.Checked)
                                    criteria.PartnerPrograms.Add(Cbx4.Text);                     //criteria[3] += "'" + Cbx4.Text + "',";
                                if (Cbx5.Checked)
                                    criteria.PartnerPrograms.Add(Cbx5.Text);                     //criteria[3] += "'" + Cbx5.Text + "',";
                                if (Cbx6.Checked)
                                    criteria.PartnerPrograms.Add(Cbx6.Text);                     //criteria[3] += "'" + Cbx6.Text + "',";
                                if (Cbx7.Checked)
                                    criteria.PartnerPrograms.Add(Cbx7.Text);                     //criteria[3] += "'" + Cbx7.Text + "',";
                            }

                            //partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupIdAndCriteria(2, groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, criteria, orderByClause, session);

                            int isActive = 1;
                            int isPublic = 1;
                            List<ElioCollaborationUsersGroupMembersIJUsers> members = SqlCollaboration.GetUserGroupMembersIJUsersByGroupId(groupId, isActive, isPublic, criteria, session);

                            if (members.Count > 0)
                            {
                                RptConnectionList.Visible = true;
                                UcMessageAlert.Visible = false;

                                DataTable table = new DataTable();

                                table.Columns.Add("id");
                                table.Columns.Add("creator_user_id");
                                table.Columns.Add("group_retailor_id");
                                table.Columns.Add("collaboration_group_id");
                                table.Columns.Add("company_name");
                                table.Columns.Add("email");
                                table.Columns.Add("company_logo");
                                table.Columns.Add("country");
                                table.Columns.Add("region");
                                table.Columns.Add("date_created");
                                table.Columns.Add("last_update");

                                if (vSession.User.CompanyType == Types.Vendors.ToString())
                                {
                                    foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                                    {
                                        table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, member.CompanyName, member.Email
                                            , member.CompanyLogo, member.Country, member.Region, member.DateCreated, member.LastUpdate);
                                    }
                                }
                                else
                                {
                                    bool masterInserted = false;
                                    foreach (ElioCollaborationUsersGroupMembersIJUsers member in members)
                                    {
                                        if (!masterInserted)
                                        {
                                            ElioUsersIJCountries groupMaster = Sql.GetUserIJCountryById(member.CreatorUserId, session);
                                            if (groupMaster != null)
                                            {
                                                table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, groupMaster.CompanyName, groupMaster.Email
                                                , groupMaster.CompanyLogo, groupMaster.Country, groupMaster.Region, member.DateCreated, member.LastUpdate);

                                                masterInserted = true;
                                            }
                                        }

                                        if (vSession.User.Id != member.GroupRetailorId)
                                        {
                                            table.Rows.Add(member.Id, member.CreatorUserId, member.GroupRetailorId, member.CollaborationGroupId, member.CompanyName, member.Email
                                            , member.CompanyLogo, member.Country, member.Region, member.DateCreated, member.LastUpdate);
                                        }
                                    }
                                }

                                e.DetailTableView.DataSource = table;
                            }
                            else
                            {
                                e.DetailTableView.DataSource = null;
                                e.DetailTableView.Visible = false;

                                RdgRetailorsGroups.Visible = false;
                                //PnlCreateRetailorGroups.Enabled = false;
                                GlobalMethods.ShowMessageControlDA(MessageControlRetailors, "There are no Group members matching these criteria", MessageTypes.Info, true, true, false);
                            }

                            #region to delete

                            //if (partners.Count > 0)
                            //{
                            //    RptConnectionList.Visible = true;
                            //    UcMessageAlert.Visible = false;

                            //    DataTable table = new DataTable();

                            //    table.Columns.Add("id");
                            //    table.Columns.Add("master_user_id");
                            //    table.Columns.Add("invitation_status");
                            //    table.Columns.Add("partner_user_id");
                            //    table.Columns.Add("company_name");
                            //    table.Columns.Add("email");
                            //    table.Columns.Add("company_logo");
                            //    table.Columns.Add("country");

                            //    foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                            //    {
                            //        int partnerId = partner.PartnerUserId;      //(vSession.User.CompanyType == Types.Vendors.ToString()) ? partner.PartnerUserId : partner.PartnerUserId;
                            //        ElioUsers company = Sql.GetUserById(partnerId, session);

                            //        if (company != null)
                            //        {
                            //            table.Rows.Add(partner.Id, partner.MasterUserId, partner.InvitationStatus, partner.PartnerUserId, partner.CompanyName, partner.Email, company.CompanyLogo, company.Country);
                            //        }
                            //    }

                            //    e.DetailTableView.DataSource = table;
                            //}
                            //else
                            //{
                            //    e.DetailTableView.DataSource = null;
                            //    e.DetailTableView.Visible = false;
                            //}

                            #endregion
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

        protected void RdgRetailorsGroups_PreRender(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //RdgElioUsers.MasterTableView.Items[0].Expanded = true;
                //RdgElioUsers.MasterTableView.Items[0].ChildItem.NestedTableViews[0].Items[0].Expanded = true;
            }
        }

        #endregion

        # region Buttons

        protected void LnkBtnAdvancedSearch_OnClck(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (divAdvancedSearch.Visible)
                {
                    divAdvancedSearch.Visible = false;
                    TbxSearch.Visible = true;
                    TbxAdvancedSearch.Visible = false;
                    TbxSearch.Text = "";
                    LnkBtnAdvancedSearch.Text = "Advanced search";
                }
                else
                {
                    DdlCountries.Items.Clear();
                    ListItem item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlCountries.Items.Add(item);

                    List<ElioCountries> countries = Sql.GetPublicCountries(session);
                    foreach (ElioCountries country in countries)
                    {
                        item = new ListItem();

                        item.Text = country.CountryName;
                        item.Value = country.Id.ToString();

                        DdlCountries.Items.Add(item);
                    }

                    DdlRegions.Items.Clear();
                    item = new ListItem();
                    item.Text = "Select region";
                    item.Value = "0";
                    DdlRegions.Items.Add(item);

                    List<ElioCountries> regions = countries.GroupBy(x => x.Region).Select(y => y.First()).ToList();
                    foreach (ElioCountries region in regions)
                    {
                        item = new ListItem();

                        item.Text = region.Region;
                        item.Value = region.Id.ToString();

                        DdlRegions.Items.Add(item);
                    }

                    divAdvancedSearch.Visible = true;
                    TbxSearch.Visible = false;
                    TbxAdvancedSearch.Visible = true;
                    TbxAdvancedSearch.Text = "";
                    LnkBtnAdvancedSearch.Text = "Close Advanced search";

                    CbxSearchList.ClearSelection();

                    List<ElioPartners> allPartners = Sql.GetPartners(session);
                    int cbxCount = 1;
                    for (int i = 0; i < allPartners.Count; i++)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(this, "Cbx" + cbxCount.ToString());
                        cbx.Text = allPartners[i].PartnerDescription.Replace("(VAR)", "").Replace("(Developers)", "");
                        cbx.Checked = false;

                        cbxCount++;
                    }
                }

                if (divTabConnections.Visible)
                    GetPartnersListForSearch("id", null);
                else if (divTabGroups.Visible)
                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", null);
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

        protected void LnkBtnRetailorAdvancedSearch_OnClck(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = false;
                divRetailorSuccess.Visible = false;

                LblRetailorFailureMsg.Text = string.Empty;

                LblRetailorSuccessMsg.Text = string.Empty;

                if (divRetailorAdvancedSearch.Visible)
                {
                    divRetailorAdvancedSearch.Visible = false;
                    TbxRetailorSearch.Visible = true;
                    TbxRetailorAdvancedSearch.Visible = false;
                    TbxRetailorSearch.Text = "";
                }
                else
                {
                    DdlRetailorCountries.Items.Clear();
                    ListItem item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlRetailorCountries.Items.Add(item);

                    List<ElioCountries> countries = Sql.GetPublicCountries(session);
                    foreach (ElioCountries country in countries)
                    {
                        item = new ListItem();

                        item.Text = country.CountryName;
                        item.Value = country.Id.ToString();

                        DdlRetailorCountries.Items.Add(item);
                    }

                    DdlRetailorRegions.Items.Clear();
                    item = new ListItem();
                    item.Text = "Select region";
                    item.Value = "0";
                    DdlRetailorRegions.Items.Add(item);

                    List<ElioCountries> regions = countries.GroupBy(x => x.Region).Select(y => y.First()).ToList();
                    foreach (ElioCountries region in regions)
                    {
                        item = new ListItem();

                        item.Text = region.Region;
                        item.Value = region.Id.ToString();

                        DdlRetailorRegions.Items.Add(item);
                    }

                    divRetailorAdvancedSearch.Visible = true;
                    TbxRetailorSearch.Visible = false;
                    TbxRetailorAdvancedSearch.Visible = true;
                    TbxRetailorAdvancedSearch.Text = "";

                    CbxRetailorSearchList.ClearSelection();

                    List<ElioPartners> allPartners = Sql.GetPartners(session);
                    int cbxCount = 1;
                    for (int i = 0; i < allPartners.Count; i++)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(this, "Rcbx" + cbxCount.ToString());
                        cbx.Text = allPartners[i].PartnerDescription.Replace("(VAR)", "").Replace("(Developers)", "");
                        cbx.Checked = false;

                        cbxCount++;
                    }
                }

                GetPartnersListForRetailorsSearch("id", null);
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

        protected void LnkBtnEditRetailorAdvancedSearch_OnClck(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divEditRetailorFailure.Visible = false;
                divEditRetailorSuccess.Visible = false;

                LblEditRetailorFailureMsg.Text = string.Empty;
                LblEditRetailorSuccessMsg.Text = string.Empty;

                if (divEditRetailorAdvancedSearch.Visible)
                {
                    divEditRetailorAdvancedSearch.Visible = false;
                    TbxEditRetailorsSearch.Visible = true;
                    TbxEditRetailorsAdvancedSearch.Visible = false;
                    TbxEditRetailorsSearch.Text = "";
                }
                else
                {
                    DdlEditRetailorCountries.Items.Clear();
                    ListItem item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlEditRetailorCountries.Items.Add(item);

                    List<ElioCountries> countries = Sql.GetPublicCountries(session);
                    foreach (ElioCountries country in countries)
                    {
                        item = new ListItem();

                        item.Text = country.CountryName;
                        item.Value = country.Id.ToString();

                        DdlEditRetailorCountries.Items.Add(item);
                    }

                    DdlEditRetailorRegions.Items.Clear();
                    item = new ListItem();
                    item.Text = "Select region";
                    item.Value = "0";
                    DdlEditRetailorRegions.Items.Add(item);

                    List<ElioCountries> regions = countries.GroupBy(x => x.Region).Select(y => y.First()).ToList();
                    foreach (ElioCountries region in regions)
                    {
                        item = new ListItem();

                        item.Text = region.Region;
                        item.Value = region.Id.ToString();

                        DdlEditRetailorRegions.Items.Add(item);
                    }

                    divEditRetailorAdvancedSearch.Visible = true;
                    TbxEditRetailorsSearch.Visible = false;
                    TbxEditRetailorsAdvancedSearch.Visible = true;
                    TbxEditRetailorsAdvancedSearch.Text = "";

                    CbxEditRetailorAdvancedSearchList.ClearSelection();

                    List<ElioPartners> allPartners = Sql.GetPartners(session);
                    int cbxCount = 1;
                    for (int i = 0; i < allPartners.Count; i++)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(this, "EdRcbx" + cbxCount.ToString());
                        cbx.Text = allPartners[i].PartnerDescription.Replace("(VAR)", "").Replace("(Developers)", "");
                        cbx.Checked = false;

                        cbxCount++;
                    }
                }

                GetPartnersListForEditRetailorsSearch("id", null);
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

        protected void ImgBtnCompanyLogo_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                var item = ((ImageButton)sender).Parent.Parent as RepeaterItem;
                if (item != null)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        StartDiscussionWithPartner(item);
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

        protected void BtnCompanyName_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                var item = ((HtmlAnchor)sender).Parent.Parent as RepeaterItem;
                if (item != null)
                {
                    StartDiscussionWithPartner(item);
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

        protected void TbxSearch_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                vSession.VendorsResellersList.Clear();

                criteria.CompanyName = TbxSearch.Text;

                if (divAdvancedSearch.Visible)
                {
                    if (TbxAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxAdvancedSearch.Text;

                    if (DdlCountries.SelectedItem.Value != "0")
                        criteria.Country = DdlCountries.SelectedItem.Text;

                    if (DdlRegions.SelectedItem.Value != "0")
                        criteria.Region = DdlRegions.SelectedItem.Text;

                    if (Cbx1.Checked)
                        criteria.PartnerPrograms.Add(Cbx1.Text);
                    if (Cbx2.Checked)
                        criteria.PartnerPrograms.Add(Cbx2.Text);
                    if (Cbx3.Checked)
                        criteria.PartnerPrograms.Add(Cbx3.Text);
                    if (Cbx4.Checked)
                        criteria.PartnerPrograms.Add(Cbx4.Text);
                    if (Cbx5.Checked)
                        criteria.PartnerPrograms.Add(Cbx5.Text);
                    if (Cbx6.Checked)
                        criteria.PartnerPrograms.Add(Cbx6.Text);
                    if (Cbx7.Checked)
                        criteria.PartnerPrograms.Add(Cbx7.Text);
                }

                if (divTabConnections.Visible)
                {
                    GetPartnersListForSearch("id", criteria);
                }
                else if (divTabGroups.Visible)
                {
                    //GetUserCollaborationGroupsForSearch("Elio_collaboration_vendors_resellers.id", whereLikeMode, likeClause);
                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);
                }

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }

                ShowSelectedChatPartnersName(0);
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

        protected void TbxRetailorSearch_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                criteria.CompanyName = TbxRetailorSearch.Text;

                if (divRetailorAdvancedSearch.Visible)
                {
                    if (TbxRetailorAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                    if (DdlRetailorCountries.SelectedItem.Value != "0")
                        criteria.Country = DdlRetailorCountries.SelectedItem.Text;

                    if (DdlRetailorRegions.SelectedItem.Value != "0")
                        criteria.Region = DdlRetailorRegions.SelectedItem.Text;

                    if (Rcbx1.Checked)
                        criteria.PartnerPrograms.Add(Rcbx1.Text);
                    if (Rcbx2.Checked)
                        criteria.PartnerPrograms.Add(Rcbx2.Text);
                    if (Rcbx3.Checked)
                        criteria.PartnerPrograms.Add(Rcbx3.Text);
                    if (Rcbx4.Checked)
                        criteria.PartnerPrograms.Add(Rcbx4.Text);
                    if (Rcbx5.Checked)
                        criteria.PartnerPrograms.Add(Rcbx5.Text);
                    if (Rcbx6.Checked)
                        criteria.PartnerPrograms.Add(Rcbx6.Text);
                    if (Rcbx7.Checked)
                        criteria.PartnerPrograms.Add(Rcbx7.Text);
                }

                GetPartnersListForRetailorsSearch("id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
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

        protected void TbxEditRetailorsSearch_OnTextChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                criteria.CompanyName = TbxEditRetailorsSearch.Text.Trim();

                if (divEditRetailorAdvancedSearch.Visible)
                {
                    if (TbxEditRetailorsAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                    if (DdlEditRetailorCountries.SelectedItem.Value != "0")
                        criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;

                    if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                        criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;

                    if (EdRcbx1.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx1.Text);
                    if (EdRcbx2.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx2.Text);
                    if (EdRcbx3.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx3.Text);
                    if (EdRcbx4.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx4.Text);
                    if (EdRcbx5.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx5.Text);
                    if (EdRcbx6.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx6.Text);
                    if (EdRcbx7.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx7.Text);
                }

                GetPartnersListForEditRetailorsSearch("id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
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

        protected void ImgBtnRetailorSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //string whereLikeMode = "";
                //string searchLikeClause = "";
                //string programsLikeClause = "";
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                //whereLikeMode = "Name";
                criteria.CompanyName = TbxRetailorSearch.Text.Trim();

                GetPartnersListForRetailorsSearch("id", criteria);
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

        protected void ImgBtnEditRetailorSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //string whereLikeMode = "";
                //string searchLikeClause = "";
                //string programsLikeClause = "";
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                //whereLikeMode = "Name";
                criteria.CompanyName = TbxEditRetailorsSearch.Text.Trim();

                GetPartnersListForEditRetailorsSearch("id", criteria);
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

        protected void ImgBtnSearch_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //string whereLikeMode = "";
                //string searchLikeClause = "";
                //string programsLikeClause = "";

                //whereLikeMode = "Name";
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                criteria.CompanyName = TbxSearch.Text.Trim();

                GetPartnersListForSearch("id", criteria);
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

        protected void BtnViewConnections_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                BtnViewConnections.Attributes["style"] = "border-style:none !important; background-color:#ccc;border-radius:5px;min-height:0px;";
                BtnCreateGroups.Attributes["style"] = "border-style:none !important; background-color:transparent;border-radius:5px;min-height:0px;";

                divTabConnections.Visible = true;
                divTabGroups.Visible = false;
                TbxSearch.Text = string.Empty;

                //BtnDeleteConnections.ToolTip = (divTabConnections.Visible) ? "Delete connections" : "Delete groups";

                vSession.VendorsResellersList.Clear();
                UcRdgMailBoxList.CollaborationGroupId = -1;

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                RadGrid rdgGroupMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgGroupMailBox");
                if (rdgMailBox != null && rdgGroupMailBox != null)
                {
                    rdgGroupMailBox.Visible = false;
                    //rdgMailBox.Visible = true;

                    //rdgMailBox.Rebind();
                    UcRdgMailBoxList.FixPage();
                }

                GetPartnersList("id", null, 0);

                ShowSelectedChatPartnersName(0);
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

        protected void BtnCreateGroups_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                BtnCreateGroups.Attributes["style"] = "border-style:none !important; background-color:#ccc;border-radius:5px;min-height:0px;";
                BtnViewConnections.Attributes["style"] = "border-style:none !important; background-color:transparent;border-radius:5px;min-height:0px;";

                divTabConnections.Visible = false;
                divTabGroups.Visible = true;
                TbxSearch.Text = string.Empty;

                //BtnDeleteConnections.ToolTip = (divTabConnections.Visible) ? "Delete connections" : "Delete groups";

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();
                if (TbxSearch.Text != "")
                    criteria.CompanyName = TbxSearch.Text;

                GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                RadGrid rdgGroupMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgGroupMailBox");
                if (rdgMailBox != null && rdgGroupMailBox != null)
                {
                    //rdgGroupMailBox.Visible = true;
                    rdgMailBox.Visible = false;

                    //rdgGroupMailBox.Rebind();
                    UcRdgMailBoxList.FixPage();
                }

                ShowSelectedChatPartnersName(0);
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

        protected void BtnDeleteConnections_OnClick(object sender, EventArgs args)
        {
            try
            {
                bool hasSelectedItem = false;
                List<ElioUsers> partners = new List<ElioUsers>();
                ElioUsers partner = null;

                if (divTabConnections.Visible)
                {
                    foreach (RepeaterItem item in RptConnectionList.Items)
                    {
                        if (item != null)
                        {
                            CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");
                            if (cbx != null)
                            {
                                if (cbx.Checked)
                                {
                                    hasSelectedItem = true;
                                    var partnerUserId = cbx.InputAttributes["partner_user_id"];
                                    var masterUserId = cbx.InputAttributes["master_user_id"];

                                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                                    {
                                        partner = Sql.GetUserById(Convert.ToInt32(partnerUserId), session);
                                    }
                                    else if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
                                    {
                                        partner = Sql.GetUserById(Convert.ToInt32(masterUserId), session);
                                    }

                                    if (partner != null)
                                    {
                                        partners.Add(partner);
                                    }
                                }
                            }
                        }
                    }

                    if (!hasSelectedItem)
                    {
                        //error
                        return;
                    }

                    foreach (ElioUsers connectionPartner in partners)
                    {
                        //delete connection
                    }
                }
                else if (divTabGroups.Visible)
                {
                    hasSelectedItem = false;

                    #region Group Connections

                    List<ElioUsers> groupPartners = new List<ElioUsers>();
                    ElioUsers groupPartner = null;
                    int groupId = -1;

                    foreach (GridDataItem item in RdgRetailorsGroups.Items)
                    {
                        if (item != null)
                        {
                            CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectGroupUsers");
                            if (cbx != null)
                            {
                                if (cbx.Checked)
                                {
                                    hasSelectedItem = true;
                                    groupId = Convert.ToInt32(item["id"].Text);
                                    string orderByClause = "Elio_collaboration_vendors_resellers.id";

                                    List<ElioCollaborationVendorsResellersIJUsers> vendorResellers = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(2, groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                                    foreach (ElioCollaborationVendorsResellersIJUsers vendorReseller in vendorResellers)
                                    {
                                        if (vSession.User.Id != vendorReseller.PartnerUserId)
                                        {
                                            groupPartner = Sql.GetUserById(vendorReseller.PartnerUserId, session);

                                            if (groupPartner != null)
                                            {
                                                groupPartners.Add(groupPartner);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!hasSelectedItem)
                    {
                        //error
                        return;
                    }


                    foreach (ElioUsers groupConnectionPartners in groupPartners)
                    {
                        //delete partnres in group
                    }

                    if (groupId != -1)
                    {
                        //delete group
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxSelectAll_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                CheckBox cbxAll = (CheckBox)sender;

                var item = ((CheckBox)sender).Parent as RepeaterItem;
                if (item != null)
                {
                    foreach (RepeaterItem rptItem in RptRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(rptItem, "CbxSelectUser");
                        if (cbx != null)
                        {
                            cbx.Checked = cbxAll.Checked;
                        }
                        else
                        {
                            Logger.DetailedError(string.Format("User with ID {0} tried to select All retailors at {1}, but checkBox could not be found", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxEditSelectAll_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                CheckBox cbxAll = (CheckBox)sender;

                var item = ((CheckBox)sender).Parent as RepeaterItem;
                if (item != null)
                {
                    foreach (RepeaterItem rptItem in RptEditRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(rptItem, "CbxSelectUser");
                        if (cbx != null)
                        {
                            cbx.Checked = cbxAll.Checked;
                        }
                        else
                        {
                            Logger.DetailedError(string.Format("User with ID {0} tried to select All retailors in edit mode at {1}, but checkBox could not be found", vSession.User.Id.ToString(), DateTime.Now.ToString()));
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void CbxSelectGroupUsers1_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;

                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                if (item != null)
                {
                    //ResetOtherCheckBoxes(item);

                    var groupId = Convert.ToInt32(item["id"].Text);
                    string orderByClause = "Elio_collaboration_vendors_resellers.id";

                    List<ElioCollaborationVendorsResellersIJUsers> partners = new List<ElioCollaborationVendorsResellersIJUsers>();

                    if (vSession.User.CompanyType == Types.Vendors.ToString())
                        partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);
                    else
                        partners = SqlCollaboration.GetUserCollaborationConnectionsPartnersByUserTypeAndInvitationStatusByGroupId(2, groupId, vSession.User.Id, CollaborateInvitationStatus.Confirmed.ToString(), vSession.User.CompanyType, orderByClause, session);

                    RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                    if (rdgMailBox != null)
                    {
                        foreach (ElioCollaborationVendorsResellersIJUsers partner in partners)
                        {
                            ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(partner.Id, session);
                            if (vendRes != null)
                            {
                                if (cbx.Checked)
                                {
                                    //AddRemoveReceivers(vendRes, Mode.GroupMode, Mode.Add);
                                    GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.GroupMode, Mode.Add, vSession.VendorsResellersList, vendRes);
                                }
                                else
                                {
                                    //AddRemoveReceivers(vendRes, Mode.GroupMode, Mode.Remove);
                                    GlobalDBMethods.AddRemoveChatReceiversMessages(Mode.GroupMode, Mode.Remove, vSession.VendorsResellersList, vendRes);
                                }
                            }
                        }

                        bool changeMailCountStatus = false;
                        SetMailsAsViewed(out changeMailCountStatus);

                        if (changeMailCountStatus)
                        {
                            ShowNotificationsMessages();
                        }

                        if (vSession.VendorsResellersList.Count > 0)
                            ShowNotificationsMessages();

                        rdgMailBox.Visible = true;
                        Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                        if (ucMailBoxListMessageAlert != null)
                            ucMailBoxListMessageAlert.Visible = false;

                        UcRdgMailBoxList.QueryJoinClause = @"INNER JOIN Elio_collaboration_users_group_retailors_mailbox 
	                            ON Elio_collaboration_users_group_retailors_mailbox.mailbox_id = Elio_collaboration_users_mailbox.mailbox_id ";

                        if (UcRdgMailBoxList.CollaborationGroupId == -1)
                            UcRdgMailBoxList.CollaborationGroupId = Convert.ToInt32(groupId);

                        rdgMailBox.Rebind();

                        if (session.Connection.State == ConnectionState.Closed)
                            session.OpenConnection();

                        ShowSelectedChatPartnersName(groupId);
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

        protected void aGroupDescription_ServerClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aBtn = (HtmlAnchor)sender;

                RepeaterItem item = (RepeaterItem)aBtn.NamingContainer;

                if (item != null)
                {
                    //RestoreCheckBoxes(null, null, RdgRetailorsGroups, item);

                    HtmlControl divSimpleMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divSimpleMailBox");
                    divSimpleMailBox.Visible = false;

                    HtmlControl divGroupMailBox = (HtmlControl)ControlFinder.FindControlRecursive(this, "divGroupMailBox");
                    divGroupMailBox.Visible = true;

                    HiddenField hdnGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnGroupId");

                    var groupId = Convert.ToInt32(hdnGroupId.Value);
                    List<ElioUsers> groupUsers = new List<ElioUsers>();
                    int isActive = 1;
                    int isPublic = 1;

                    bool hasMembers = GlobalDBMethods.GetGroupMembersForMessages((int)groupId, isActive, isPublic, vSession.User.CompanyType, vSession.User.Id, out groupUsers, session);

                    if (hasMembers)
                    {
                        RadGrid rdgGroupMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgGroupMailBox");
                        if (rdgGroupMailBox != null)
                        {
                            UcRdgMailBoxList.CollaborationGroupId = groupId;

                            Label lblGroupMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblGroupMessagesCount");
                            HtmlControl spanGroupMessagesCount = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanGroupMessagesCount");
                            lblGroupMessagesCount.Text = "";
                            spanGroupMessagesCount.Visible = false;

                            //bool changeMailCountStatus = false;
                            //SetMailsAsViewed(out changeMailCountStatus);

                            //if (changeMailCountStatus)
                            //{
                            //    ShowNotificationsMessages();
                            //}

                            rdgGroupMailBox.Visible = true;
                            Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                            if (ucMailBoxListMessageAlert != null)
                                ucMailBoxListMessageAlert.Visible = false;

                            rdgGroupMailBox.Rebind();

                            //int simpleMsgCount = 0;
                            //int groupMsgCount = 0;
                            //int totalNewMsgCount = 0;
                            //this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

                            //UpdatePanel updatePanel1 = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanel1");
                            //if (updatePanel1 != null)
                            //    updatePanel1.Update();

                            if (hasMembers)
                                ShowNotificationsMessages();

                            if (session.Connection.State == ConnectionState.Closed)
                                session.OpenConnection();

                            ShowSelectedChatPartnersName(groupId);

                            UpdatePanel1.Update();
                        }
                    }
                    else
                        divGroupImages.Visible = false;
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

        protected void CbxSelectUser_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                CheckBox cbx = (CheckBox)sender;

                var item = ((CheckBox)sender).Parent as RepeaterItem;
                if (item != null)
                {
                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        var id = ((CheckBox)sender).InputAttributes["id"];

                        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                        if (rdgMailBox != null)
                        {
                            ElioCollaborationVendorsResellers vendRes = SqlCollaboration.GetCollaborationVendorResellerById(Convert.ToInt32(id), session);
                            if (vendRes != null)
                            {
                                if (cbx.Checked)
                                {
                                    AddRemoveItem(vendRes, (int)Mode.Add);

                                    bool changeMailCountStatus = false;
                                    SetMailsAsViewed(out changeMailCountStatus);

                                    rdgMailBox.Visible = true;
                                    Control ucMailBoxListMessageAlert = (Control)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "UcMailBoxListMessageAlert");
                                    if (ucMailBoxListMessageAlert != null)
                                        ucMailBoxListMessageAlert.Visible = false;

                                    if (changeMailCountStatus)
                                    {
                                        Label lblMessagesCount = (Label)ControlFinder.FindControlRecursive(item, "LblMessagesCount");
                                        HtmlControl spanMessagesCountNotification = (HtmlControl)ControlFinder.FindControlRecursive(item, "spanMessagesCountNotification");

                                        spanMessagesCountNotification.Visible = false;
                                        lblMessagesCount.Text = "";
                                    }
                                }
                                else
                                {
                                    AddRemoveItem(vendRes, (int)Mode.Remove);
                                }
                            }

                            UcRdgMailBoxList.QueryJoinClause = "";
                            UcRdgMailBoxList.CollaborationGroupId = -1;

                            rdgMailBox.Rebind();
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

        protected void ImgBtnDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                ImageButton imgBtn = (ImageButton)sender;
                RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;

                if (item != null)
                {
                    DataRowView row = (DataRowView)item.DataItem;

                    if (row != null)
                    {
                        SqlCollaboration.DeleteCollaborationById(Convert.ToInt32(row["id"].ToString()), session);

                        GetPartnersList("id", null, 0);
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

        protected void CbxSelectFile_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                CheckBox cbx = (CheckBox)sender;
                GridDataItem item = (GridDataItem)cbx.NamingContainer;

                int id = Convert.ToInt32(item[""].Text);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnUpload_OnClick(object sender, EventArgs args)
        {
            try
            {
                ////////UcChatMessageAlert.Visible = false;
                //////////HttpPostedFile myFile = Request.Files["UploadedFile"];


                //////////if (fileContentRes != null && fileContentRes.ContentLength > 0 && DdlCategory.SelectedItem.Value == "0")
                //////////{
                //////////    divUploadFailure.Visible = true;
                //////////    LblUploadFailureMsg.Text = "You must select a category for your file in order to send it.";
                //////////    return;
                //////////}

                ////////foreach (GridDataItem item in RpLibraryFiles.MasterTableView.Items)
                ////////{
                ////////    if (item is GridDataItem && item.OwnerTableView.Name == "UserLibraryFiles")
                ////////    {

                //////////switch (e.DetailTableView.Name)
                //////////{
                //////////    case "UserLibraryFiles":
                //////////        {

                ////////        GridDataItem itm = (GridDataItem)item.Parent;
                ////////        //GridDataItem dataItem = (GridDataItem)itm.DetailTableView.ParentItem;

                ////////        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectFile");

                ////////        if (cbx.Checked)
                ////////        {
                ////////            //count++;
                ////////        }
                ////////    }
                ////////}


                ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseUploadPopUp();", true);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void ImgBtnSendMsg_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcChatMessageAlert.Visible = false;

                if (vSession.User != null)
                {
                    #region Check for valid Data

                    if (RtbxNewMessage.Text.Trim() == string.Empty)
                    {
                        //GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "There is nothing to share with your partners", MessageTypes.Error, true, true, false);
                        LblSendTitle.Text = "Send Massage Warning";
                        LblSendfMsg.Text = "Please type a message in order to share it with your partner.";
                        //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                        if (inputFile.PostedFile != null || (divLibrarySelectedFile.Visible && LblLibrarySelectedFile.Text != "" && HdnCategoryId.Value != "0" && HdnFileId.Value != "0"))
                            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Please type your message before attaching the file of your choice", MessageTypes.Warning, true, true, true, true, false);
                        else
                            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Please type your message before send it to your partner", MessageTypes.Warning, true, true, true, true, false);

                        return;
                    }

                    if (inputFile.PostedFile != null && inputFile.PostedFile.ContentLength > 0)
                    {
                        //if (DdlCategory.SelectedValue == "0")
                        //{
                        //    //GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "You must select file category in order to send it", MessageTypes.Info, true, true, false);
                        //    LblSendTitle.Text = "Send File Warning";
                        //    LblSendfMsg.Text = "You must select file category in order to send it.";
                        //    //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                        //    GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "You must select file category in order to send it.", MessageTypes.Warning, true, true, true, true, false);

                        //    return;
                        //}

                        if (inputFile.PostedFile.ContentLength > int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]))
                        {
                            LblSendTitle.Text = "Send File Warning";
                            LblSendfMsg.Text = "Your file size is outside the bounds. Please try smaller file size to send or contact with us.";
                            //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Your file size is outside the bounds. Please try smaller file size to send or contact with us.", MessageTypes.Warning, true, true, true, true, false);

                            return;
                        }
                    }

                    #endregion

                    List<ElioUsers> partners = new List<ElioUsers>();
                    List<ElioUsers> groupPartners = new List<ElioUsers>();
                    int groupId = -1;
                    bool hasSelectedItem = false;
                    int memberGroupId = -1;

                    if (divTabConnections.Visible)
                    {
                        UcRdgMailBoxList.CollaborationGroupId = -1;

                        #region Simple Connections Receivers

                        groupPartners.Clear();
                        groupPartners = null;

                        hasSelectedItem = GetPartnersToReceiveMessage(out partners);

                        if (hasSelectedItem && partners.Count > 0)
                        {
                            try
                            {
                                session.BeginTransaction();

                                AddMailBoxToUsers(1, partners, groupPartners, groupId);

                                session.CommitTransaction();
                            }
                            catch (Exception ex)
                            {
                                session.RollBackTransaction();
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }
                        }
                        else
                        {
                            //GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "There is no receiver selected to send your message", MessageTypes.Error, true, true, false);
                            LblSendTitle.Text = "Send Message Warning";
                            LblSendfMsg.Text = "Please select a partner in order to share your message";
                            //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                            GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Please select a partner in order to share your message", MessageTypes.Warning, true, true, true, true, false);

                            return;
                        }

                        #endregion

                        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                        if (rdgMailBox != null)
                            rdgMailBox.Rebind();

                        partners.Clear();
                    }
                    else if (divTabGroups.Visible)
                    {
                        #region Group Connections Receivers

                        partners.Clear();
                        partners = null;

                        memberGroupId = GetMembersGroupId();

                        if (memberGroupId > -1)
                        {
                            UcRdgMailBoxList.CollaborationGroupId = memberGroupId;
                            int isActive = 1;
                            int isPublic = 1;

                            hasSelectedItem = GlobalDBMethods.GetGroupMembersForMessages(memberGroupId, isActive, isPublic, vSession.User.CompanyType, vSession.User.Id, out groupPartners, session);
                            if (hasSelectedItem && groupPartners.Count > 0)
                            {
                                try
                                {
                                    session.BeginTransaction();
                                    UcRdgMailBoxList.CollaborationGroupId = memberGroupId;

                                    AddGroupUsersMailbox(groupPartners, memberGroupId);
                                    //AddMailBoxToUsers(2, partners, groupPartners, selectedGroupId);

                                    session.CommitTransaction();
                                }
                                catch (Exception ex)
                                {
                                    session.RollBackTransaction();
                                    Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                                }

                                RadGrid rdgGroupMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgGroupMailBox");
                                if (rdgGroupMailBox != null)
                                    rdgGroupMailBox.Rebind();

                                groupPartners.Clear();
                                //memberGroupId = -1;
                                //UcRdgMailBoxList.CollaborationGroupId = -1;
                            }
                            else
                            {
                                LblSendTitle.Text = "Send Message Warning";
                                LblSendfMsg.Text = "Please select a partner in order to share your message";
                                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                                return;
                            }
                        }
                        else
                        {
                            //GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "There is no receiver selected to send your message", MessageTypes.Error, true, true, false);
                            LblSendTitle.Text = "Send Message Warning";
                            LblSendfMsg.Text = "Please select a partner in order to share your message";
                            System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                            return;
                        }

                        #endregion
                    }

                    GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Your message sent successfully", MessageTypes.Success, true, true, true, true, false);

                    RtbxNewMessage.Text = string.Empty;
                    inputFile = null;
                    LblLibrarySelectedFile.Text = "";
                    divLibrarySelectedFile.Visible = false;
                    //DdlCategory.SelectedIndex = -1;
                    //DdlCategory.ClearSelection();
                    //DdlCategory.SelectedValue = "0";
                    hasSelectedItem = false;

                    GetPartnersList("", null, 0);
                    UpdatePanel1.Update();

                    #region To Delete

                    //////bool hasSelectedItem = GetMsgReceivers(out partners, out groupPartners, out groupId);

                    //////if (hasSelectedItem && (partners.Count > 0 || groupPartners.Count > 0))
                    //////{
                    //////    try
                    //////    {
                    //////        session.BeginTransaction();

                    //////        AddMailBoxToUsers(2, partners, groupPartners, groupId);

                    //////        session.CommitTransaction();

                    //////        GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "Your message sent successfully", MessageTypes.Success, true, true, false);

                    //////        RtbxNewMessage.Text = string.Empty;
                    //////        partners.Clear();
                    //////        partners = null;

                    //////        RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                    //////        if (rdgMailBox != null)
                    //////            rdgMailBox.Rebind();
                    //////    }
                    //////    catch (Exception ex)
                    //////    {
                    //////        session.RollBackTransaction();
                    //////        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                    //////    }

                    //////}
                    //////else
                    //////{
                    //////    GlobalMethods.ShowMessageControlDA(UcChatMessageAlert, "There is no receiver selected to send your message", MessageTypes.Error, true, true, false);
                    //////    return;
                    //////}

                    #endregion
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

        protected void BtnAttachFile_OnClick(object sender, EventArgs args)
        {
            try
            {
                divUpload.Visible = !divUpload.Visible;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void LnkBtnOrderByPartners_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                LinkButton btn = (LinkButton)(sender);

                string orderByValue = btn.CommandArgument;

                GetPartnersList(orderByValue, null, 0);
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

        protected void BtnAddCategory_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                //if (Tbxcategory.Text == string.Empty)
                //{

                //    return;
                //}

                DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);

                ElioCollaborationUsersLibraryFilesCategories category = new ElioCollaborationUsersLibraryFilesCategories();

                category.UserId = vSession.User.Id;
                //category.CategoryDescription = Tbxcategory.Text;
                category.DateCreated = DateTime.Now;
                category.LastUpdate = DateTime.Now;
                category.IsPublic = 1;

                loader.Insert(category);

                //GetLibraryFiles();

                //Tbxcategory.Text = string.Empty;
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

        protected void ImgBtnDeleteCategory_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton btn = (ImageButton)sender;

                    var item = (btn).Parent as RepeaterItem;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");

                        if (hdnId != null)
                            SqlCollaboration.UpdatePublicStatusCollaborationLibraryFileById(Convert.ToInt32(hdnId.Value), vSession.User.Id, 0, session);
                    }

                    //GetLibraryFiles();
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

        protected void BtnCreate_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = divRetailorSuccess.Visible = false;
                LblRetailorFailureMsg.Text = LblRetailorSuccessMsg.Text = "";

                if (vSession.User != null)
                {
                    #region Validations

                    if (TbxGroupName.Text == string.Empty)
                    {
                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Please, add group name";
                        return;
                    }
                    else
                    {
                        if (!IsEditMode)
                        {
                            bool exist = SqlCollaboration.ExistCollaborationGroupDescription(vSession.User.Id, TbxGroupName.Text.Trim(), session);
                            if (exist)
                            {
                                divRetailorFailure.Visible = true;
                                LblRetailorFailureMsg.Text = "Sorry, this group name already exists by you.";
                                return;
                            }
                        }
                    }

                    List<int> resellersIDs = HasSelectedItem(RptRetailorsList);

                    if (resellersIDs.Count <= 1)
                    {
                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Please, you must select at least two in order to create a group.";
                        return;
                    }

                    #endregion

                    try
                    {
                        if (!IsEditMode)
                        {
                            #region Insert new Group

                            session.BeginTransaction();

                            ElioCollaborationUsersGroups group = new ElioCollaborationUsersGroups();

                            group.UserId = vSession.User.Id;
                            group.CollaborationGroupName = TbxGroupName.Text;
                            group.DateCreated = DateTime.Now;
                            group.LastUpdate = DateTime.Now;
                            group.IsActive = 1;
                            group.IsPublic = 1;

                            DataLoader<ElioCollaborationUsersGroups> loader = new DataLoader<ElioCollaborationUsersGroups>(session);
                            loader.Insert(group);

                            foreach (int resellerID in resellersIDs)
                            {
                                ElioCollaborationUsersGroupMembers member = new ElioCollaborationUsersGroupMembers();

                                member.CreatorUserId = vSession.User.Id;
                                member.GroupRetailorId = resellerID;
                                member.CollaborationGroupId = group.Id;
                                member.DateCreated = DateTime.Now;
                                member.LastUpdate = DateTime.Now;
                                member.IsActive = 1;
                                member.IsPublic = 1;

                                DataLoader<ElioCollaborationUsersGroupMembers> mLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);
                                mLoader.Insert(member);
                            }

                            session.CommitTransaction();

                            GroupId = group.Id;

                            #endregion
                        }
                        else
                        {
                            #region Update Group

                            if (GroupId > 0)
                            {
                                resellersIDs.Clear();

                                session.BeginTransaction();

                                bool exist = SqlCollaboration.ExistCollaborationGroupDescriptionToOtherGroupId(vSession.User.Id, GroupId, TbxGroupName.Text.Trim(), session);
                                if (exist)
                                {
                                    divRetailorFailure.Visible = true;
                                    LblRetailorFailureMsg.Text = "Sorry, this name already exists to other group of yours.";
                                    return;
                                }
                                else
                                {
                                    SqlCollaboration.UpdateCollaborationUserGroupByGroupId(GroupId, vSession.User.Id, TbxGroupName.Text, 1, 1, session);
                                }

                                foreach (RepeaterItem item in RptRetailorsList.Items)
                                {
                                    HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                                    CheckBox cbxSelectUser = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                                    if (hdnPartnerUserId.Value != "")
                                    {
                                        bool existMember = SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session);
                                        if (cbxSelectUser.Checked)
                                        {
                                            if (!existMember)
                                                resellersIDs.Add(Convert.ToInt32(hdnPartnerUserId.Value));
                                        }
                                        else
                                        {
                                            if (existMember)
                                                SqlCollaboration.DeleteCollaborationUserGroupRetailorByGroupId(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), session);
                                        }
                                    }
                                }

                                foreach (int resellerID in resellersIDs)
                                {
                                    ElioCollaborationUsersGroupMembers member = new ElioCollaborationUsersGroupMembers();

                                    member.CreatorUserId = vSession.User.Id;
                                    member.GroupRetailorId = resellerID;
                                    member.CollaborationGroupId = GroupId;
                                    member.DateCreated = DateTime.Now;
                                    member.LastUpdate = DateTime.Now;
                                    member.IsActive = 1;
                                    member.IsPublic = 1;

                                    DataLoader<ElioCollaborationUsersGroupMembers> mLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);
                                    mLoader.Insert(member);
                                }

                                session.CommitTransaction();
                            }
                            else
                            {
                                divRetailorFailure.Visible = true;
                                LblRetailorFailureMsg.Text = "Sorry, something went wrong! Please try again later or contact us";

                                Logger.DetailedError(Request.Url.ToString(), string.Format("No Group Id while update from user {0}", vSession.User.Id.ToString()));
                                return;
                            }

                            #region To Delete

                            /* other way 
                            SqlCollaboration.UpdateCollaborationUserGroupByGroupId(GroupId, vSession.User.Id, TbxGroupName.Text, 1, 1, session);

                            foreach (RepeaterItem item in RptRetailorsList.Items)
                            {
                                CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                                var vendorResellerId = cbx.InputAttributes["id"];
                                HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                                if (vendorResellerId != null)
                                {
                                    if (cbx.Checked)
                                    {
                                        if (!SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                        {
                                            ElioCollaborationUsersGroupMembers member = new ElioCollaborationUsersGroupMembers();

                                            member.CreatorUserId = vSession.User.Id;
                                            member.GroupRetailorId = Convert.ToInt32(hdnPartnerUserId.Value);
                                            member.CollaborationGroupId = GroupId;
                                            member.DateCreated = DateTime.Now;
                                            member.LastUpdate = DateTime.Now;
                                            member.IsActive = 1;
                                            member.IsPublic = 1;

                                            DataLoader<ElioCollaborationUsersGroupMembers> mLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);
                                            mLoader.Insert(member);
                                        }
                                    }
                                    else
                                    {
                                        if (SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                        {
                                            SqlCollaboration.DeleteCollaborationUserGroupRetailorByGroupId(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), session);
                                        }
                                    }
                                }
                                else
                                {
                                    GlobalMethods.ShowMessageControlDA(MessageControlUp, "Something went wrong. Retailor's group could not be updated.", MessageTypes.Error, true, true, true, true, false);
                                    return;
                                }
                            }
                            */

                            #endregion

                            #endregion
                        }
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();

                        divRetailorFailure.Visible = true;
                        LblRetailorFailureMsg.Text = "Sorry, something went wrong! Please try again later or contact us";

                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        throw ex;
                    }

                    divRetailorSuccess.Visible = true;
                    LblRetailorSuccessMsg.Text = (!IsEditMode) ? "Group created successfully" : "Group updated successfully";

                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", null);

                    IsEditMode = true;

                    UpdatePanel1.Update();
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

        protected void BtnEdit_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor aEditBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)aEditBtn.NamingContainer;

                HdnGroupId.Value = item["id"].Text;
                GroupId = Convert.ToInt32(item["id"].Text);
                TbxEditRetailorName.Text = item["collaboration_group_name"].Text;

                IsEditMode = true;

                GetPartnersList("id", null, Convert.ToInt32(HdnGroupId.Value));

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);
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

        protected void BtnEditRetailor_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divEditRetailorFailure.Visible = false;
                divEditRetailorSuccess.Visible = false;

                LblEditRetailorFailureMsg.Text = string.Empty;
                LblEditRetailorSuccessMsg.Text = string.Empty;

                if (vSession.User != null)
                {
                    if (TbxEditRetailorName.Text == string.Empty)
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Add your retailor's group name";
                        return;
                    }

                    if (!HasSelectedItemOld(RptEditRetailorsList))
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Select at least two for your retailor's group";
                        return;
                    }

                    if (GroupId == -1)
                    {
                        divEditRetailorFailure.Visible = true;
                        LblEditRetailorFailureMsg.Text = "Retailor's group could not be updated.";
                        return;
                    }

                    SqlCollaboration.UpdateCollaborationUserGroupByGroupId(GroupId, vSession.User.Id, TbxEditRetailorName.Text, 1, 1, session);

                    foreach (RepeaterItem item in RptEditRetailorsList.Items)
                    {
                        CheckBox cbx = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelectUser");

                        var vendorResellerId = cbx.InputAttributes["id"];
                        HiddenField hdnPartnerUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_user_id");

                        if (vendorResellerId != null)
                        {
                            if (cbx.Checked)
                            {
                                if (!SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                {
                                    ElioCollaborationUsersGroupMembers member = new ElioCollaborationUsersGroupMembers();

                                    member.CreatorUserId = vSession.User.Id;
                                    member.GroupRetailorId = Convert.ToInt32(hdnPartnerUserId.Value);
                                    member.CollaborationGroupId = GroupId;
                                    member.DateCreated = DateTime.Now;
                                    member.LastUpdate = DateTime.Now;
                                    member.IsActive = 1;
                                    member.IsPublic = 1;

                                    DataLoader<ElioCollaborationUsersGroupMembers> mLoader = new DataLoader<ElioCollaborationUsersGroupMembers>(session);
                                    mLoader.Insert(member);
                                }
                            }
                            else
                            {
                                if (SqlCollaboration.ExistCollaborationUserGroupRetailorInGroup(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), 1, 1, session))
                                {
                                    SqlCollaboration.DeleteCollaborationUserGroupRetailorByGroupId(GroupId, Convert.ToInt32(hdnPartnerUserId.Value), session);
                                }
                            }
                        }
                        else
                        {
                            divEditRetailorFailure.Visible = true;
                            LblEditRetailorFailureMsg.Text = "Something went wrong. Retailor's group could not be updated.";
                            return;
                        }
                    }

                    divEditRetailorSuccess.Visible = true;
                    LblEditRetailorSuccessMsg.Text = "You updated your group successfully.";

                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", null);

                    UpdatePanel1.Update();
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

        protected void ImgBtnDeleteGroup_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();
                session.BeginTransaction();

                ImageButton btn = (ImageButton)sender;
                GridDataItem item = (GridDataItem)btn.NamingContainer;

                HdnGroupId.Value = item["id"].Text;
                GroupId = Convert.ToInt32(item["id"].Text);

                if (GroupId > 0)
                {
                    SqlCollaboration.DeleteGroupById(GroupId, session);
                }

                session.CommitTransaction();

                GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", null);
                //System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenEditPopUp();", true);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                    rdgMailBox.Rebind();
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

        protected void aLibraryFile_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divLibrarySelectedFile.Visible = false;
                LblLibrarySelectedFile.Text = "";
                HdnCategoryId.Value = "0";
                HdnFileId.Value = "0";

                UcCollaborationLibrary.FillDropLists(session);
                UcCollaborationLibrary.FixTabsContent(1);

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenLibraryPopUp();", true);
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

        protected void aEditGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    GroupId = -1;
                    IsEditMode = false;

                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnGroupId");
                        Label lblGroupDescription = (Label)ControlFinder.FindControlRecursive(item, "LblGroupDescription");

                        divRetailorFailure.Visible = divRetailorSuccess.Visible = false;

                        //ResetGroupItems();

                        GroupId = Convert.ToInt32(hdnGroupId.Value);
                        TbxGroupName.Text = lblGroupDescription.Text;
                        IsEditMode = true;

                        GetPartnersList("id", null, 0);
                        //RptRetailorsList.DataBind();
                        BtnCreate.Text = (!IsEditMode) ? "Create" : "Update";

                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenCreateRetailorPopUp();", true);
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

        protected void aDeleteGroup_ServerClick(object sender, EventArgs e)
        {
            try
            {
                divSuccess.Visible = divFailure.Visible = false;

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnGroupId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnGroupId");

                        if (hdnGroupId != null)
                        {
                            GroupId = Convert.ToInt32(hdnGroupId.Value);
                            DeleteGroup(Convert.ToInt32(hdnGroupId.Value));
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

        protected void BtnDeleteGroup_Click(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = MessageControlRetailors.Visible = false;
                divSuccess.Visible = divFailure.Visible = false;

                if (vSession.User != null)
                {
                    if (GroupId > 0)
                    {
                        try
                        {
                            session.BeginTransaction();

                            SqlCollaboration.DeleteGroupById(GroupId, session);

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();

                            string content = "Group could not be deleted! Please try again later or contact us.";

                            divFailure.Visible = true;
                            LblFailureMsg.Text = content;

                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            return;
                        }

                        GroupId = -1;
                        IsEditMode = false;

                        GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", null);

                        divSuccess.Visible = true;
                        LblSuccessMsg.Text = "Group was deleted successfully";
                    }
                    else
                    {
                        divFailure.Visible = true;
                        LblFailureMsg.Text = "Group could not be deleted! Please try again later or contact us.";
                        return;
                    }
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                divFailure.Visible = true;
                LblFailureMsg.Text = "Group could not be deleted! Please try again later or contact us.";
                
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        private void DeleteGroup(int groupId)
        {
            if (groupId > -1)
            {
                BtnDeleteGroup.Visible = true;
                BtnDeleteFile.Visible = false;
                BtnDeletePreviewFile.Visible = false;
                BtnDeleteCategory.Visible = false;

                LblConfirmationMessage.Text = "Deleting this group, all the messages and files you have send or received from it will be deleted too. Are you sure you want to delete group?";

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
            }
            else
            {
                LblConfirmationMessage.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "20")).Text;
                //GlobalMethods.ShowMessageAlertControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false, false, false);
            }
        }

        #endregion

        #region Combo

        protected void DdlCategory_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                if (DdlCategory.SelectedIndex > 0)
                {
                    divLibraryFiles.Visible = true;

                    UcSelectLibraryFiles.SelectedCategory = DdlCategory.Text;

                    RadGrid rdgViewLogs = (RadGrid)ControlFinder.FindControlRecursive(UcSelectLibraryFiles, "RdgViewLogs");
                    if (rdgViewLogs != null)
                        rdgViewLogs.Rebind();
                }
                else
                    divLibraryFiles.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlCountries_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                if (divAdvancedSearch.Visible)
                {
                    if (TbxAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxAdvancedSearch.Text;

                    if (DdlCountries.SelectedValue != "0")
                    {
                        criteria.Country = DdlCountries.SelectedItem.Text;
                    }

                    if (DdlRegions.SelectedValue != "0")
                    {
                        criteria.Region = DdlRegions.SelectedItem.Text;
                    }

                    if (Cbx1.Checked)
                        criteria.PartnerPrograms.Add(Cbx1.Text);                     //criteria[3] = "'" + Cbx1.Text + "',";
                    if (Cbx2.Checked)
                        criteria.PartnerPrograms.Add(Cbx2.Text);                     //criteria[3] += "'" + Cbx2.Text + "',";
                    if (Cbx3.Checked)
                        criteria.PartnerPrograms.Add(Cbx3.Text);                     //criteria[3] += "'" + Cbx3.Text + "',";
                    if (Cbx4.Checked)
                        criteria.PartnerPrograms.Add(Cbx4.Text);                     //criteria[3] += "'" + Cbx4.Text + "',";
                    if (Cbx5.Checked)
                        criteria.PartnerPrograms.Add(Cbx5.Text);                     //criteria[3] += "'" + Cbx5.Text + "',";
                    if (Cbx6.Checked)
                        criteria.PartnerPrograms.Add(Cbx6.Text);                     //criteria[3] += "'" + Cbx6.Text + "',";
                    if (Cbx7.Checked)
                        criteria.PartnerPrograms.Add(Cbx7.Text);                     //criteria[3] += "'" + Cbx7.Text + "',";
                }

                if (divTabConnections.Visible)
                {
                    GetPartnersListForSearch("id", criteria);
                }
                else if (divTabGroups.Visible)
                {
                    //GetUserCollaborationGroupsForSearch("Elio_collaboration_vendors_resellers.id", whereLikeMode, likeClause);
                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);
                }

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }

                ShowSelectedChatPartnersName(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlRegions_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCountries> countries = null;
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                DdlCountries.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Select country";
                item.Value = "0";
                DdlCountries.Items.Add(item);

                if (TbxAdvancedSearch.Text != "")
                    criteria.CompanyName = TbxAdvancedSearch.Text;

                if (DdlRegions.SelectedItem.Value != "0")
                {
                    countries = Sql.GetCountriesByRegion(DdlRegions.SelectedItem.Text, session);
                }
                else
                {
                    countries = Sql.GetPublicCountries(session);

                    item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlCountries.Items.Add(item);
                }

                foreach (ElioCountries country in countries)
                {
                    item = new ListItem();

                    item.Text = country.CountryName;
                    item.Value = country.Id.ToString();

                    DdlCountries.Items.Add(item);
                }

                DdlCountries.DataBind();

                if (DdlCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlCountries.SelectedItem.Text;

                if (DdlRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlRegions.SelectedItem.Text;

                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                ShowSelectedChatPartnersName(0);

                if (divTabConnections.Visible)
                {
                    GetPartnersListForSearch("id", criteria);
                }
                else if (divTabGroups.Visible)
                {
                    //GetUserCollaborationGroupsForSearch("Elio_collaboration_vendors_resellers.id", whereLikeMode, likeClause);
                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlRetailorCountries_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                if (divRetailorAdvancedSearch.Visible)
                {
                    if (TbxRetailorAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                    if (DdlRetailorCountries.SelectedValue != "0")
                    {
                        criteria.Country = DdlRetailorCountries.SelectedItem.Text;
                    }

                    if (DdlRetailorRegions.SelectedValue != "0")
                    {
                        criteria.Region = DdlRetailorRegions.SelectedItem.Text;
                    }

                    if (Rcbx1.Checked)
                        criteria.PartnerPrograms.Add(Rcbx1.Text);
                    if (Rcbx2.Checked)
                        criteria.PartnerPrograms.Add(Rcbx2.Text);
                    if (Rcbx3.Checked)
                        criteria.PartnerPrograms.Add(Rcbx3.Text);
                    if (Rcbx4.Checked)
                        criteria.PartnerPrograms.Add(Rcbx4.Text);
                    if (Rcbx5.Checked)
                        criteria.PartnerPrograms.Add(Rcbx5.Text);
                    if (Rcbx6.Checked)
                        criteria.PartnerPrograms.Add(Rcbx6.Text);
                    if (Rcbx7.Checked)
                        criteria.PartnerPrograms.Add(Rcbx7.Text);
                }

                GetPartnersListForRetailorsSearch("id", criteria);
                //GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }

                //ShowSelectedChatPartnersName(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlRetailorRegions_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCountries> countries = null;
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                DdlRetailorCountries.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Select country";
                item.Value = "0";
                DdlRetailorCountries.Items.Add(item);

                if (TbxRetailorAdvancedSearch.Text != "")
                    criteria.CompanyName = TbxRetailorAdvancedSearch.Text;

                if (DdlRetailorRegions.SelectedItem.Value != "0")
                {
                    countries = Sql.GetCountriesByRegion(DdlRetailorRegions.SelectedItem.Text, session);
                }
                else
                {
                    countries = Sql.GetPublicCountries(session);

                    item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlCountries.Items.Add(item);
                }

                foreach (ElioCountries country in countries)
                {
                    item = new ListItem();

                    item.Text = country.CountryName;
                    item.Value = country.Id.ToString();

                    DdlRetailorCountries.Items.Add(item);
                }

                DdlRetailorCountries.DataBind();

                if (DdlRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlRetailorCountries.SelectedItem.Text;

                if (DdlRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlRetailorRegions.SelectedItem.Text;

                GetPartnersListForRetailorsSearch("id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlEditRetailorCountries_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                vSession.VendorsResellersList.Clear();
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                if (divEditRetailorAdvancedSearch.Visible)
                {
                    if (TbxEditRetailorsAdvancedSearch.Text != "")
                        criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                    if (DdlEditRetailorCountries.SelectedValue != "0")
                    {
                        criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;
                    }

                    if (DdlEditRetailorRegions.SelectedValue != "0")
                    {
                        criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;
                    }

                    if (EdRcbx1.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx1.Text);
                    if (EdRcbx2.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx2.Text);
                    if (EdRcbx3.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx3.Text);
                    if (EdRcbx4.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx4.Text);
                    if (EdRcbx5.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx5.Text);
                    if (EdRcbx6.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx6.Text);
                    if (EdRcbx7.Checked)
                        criteria.PartnerPrograms.Add(EdRcbx7.Text);
                }

                GetPartnersListForEditRetailorsSearch("id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }

                //ShowSelectedChatPartnersName(0);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void DdlEditRetailorRegions_OnSelectedIndexChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                List<ElioCountries> countries = null;
                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                DdlEditRetailorCountries.Items.Clear();
                ListItem item = new ListItem();
                item.Text = "Select country";
                item.Value = "0";
                DdlEditRetailorCountries.Items.Add(item);

                if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                {
                    countries = Sql.GetCountriesByRegion(DdlEditRetailorRegions.SelectedItem.Text, session);
                }
                else
                {
                    countries = Sql.GetPublicCountries(session);

                    item = new ListItem();
                    item.Text = "Select country";
                    item.Value = "0";
                    DdlEditRetailorCountries.Items.Add(item);
                }

                foreach (ElioCountries country in countries)
                {
                    item = new ListItem();

                    item.Text = country.CountryName;
                    item.Value = country.Id.ToString();

                    DdlEditRetailorCountries.Items.Add(item);
                }

                DdlEditRetailorCountries.DataBind();

                if (TbxEditRetailorsAdvancedSearch.Text != "")
                    criteria.CompanyName = TbxEditRetailorsAdvancedSearch.Text;

                if (DdlEditRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;

                if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;

                GetPartnersListForEditRetailorsSearch("id", criteria);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region CheckBoxes

        protected void Cbx1_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                criteria.CompanyName = (divAdvancedSearch.Visible) ? TbxAdvancedSearch.Text : TbxSearch.Text;

                if (DdlCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlCountries.SelectedItem.Text;

                if (DdlRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlRegions.SelectedItem.Text;

                if (Cbx1.Checked)
                    criteria.PartnerPrograms.Add(Cbx1.Text);
                if (Cbx2.Checked)
                    criteria.PartnerPrograms.Add(Cbx2.Text);
                if (Cbx3.Checked)
                    criteria.PartnerPrograms.Add(Cbx3.Text);
                if (Cbx4.Checked)
                    criteria.PartnerPrograms.Add(Cbx4.Text);
                if (Cbx5.Checked)
                    criteria.PartnerPrograms.Add(Cbx5.Text);
                if (Cbx6.Checked)
                    criteria.PartnerPrograms.Add(Cbx6.Text);
                if (Cbx7.Checked)
                    criteria.PartnerPrograms.Add(Cbx7.Text);

                if (divTabConnections.Visible)
                {
                    GetPartnersListForSearch("id", criteria);
                }
                else if (divTabGroups.Visible)
                {
                    //GetUserCollaborationGroupsForSearch("Elio_collaboration_vendors_resellers.id", whereLikeMode, likeClause);
                    GetRetailorsGroupsList("Elio_collaboration_vendors_resellers.id", criteria);
                }

                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                ShowSelectedChatPartnersName(0);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
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

        protected void Rcbx1_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = false;
                LblRetailorFailureMsg.Text = string.Empty;
                divRetailorSuccess.Visible = false;
                LblRetailorSuccessMsg.Text = string.Empty;

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                criteria.CompanyName = (divRetailorAdvancedSearch.Visible) ? TbxRetailorAdvancedSearch.Text : TbxRetailorSearch.Text;

                if (DdlRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlRetailorCountries.SelectedItem.Text;

                if (DdlRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlRetailorRegions.SelectedItem.Text;

                if (Rcbx1.Checked)
                    criteria.PartnerPrograms.Add(Rcbx1.Text);
                if (Rcbx2.Checked)
                    criteria.PartnerPrograms.Add(Rcbx2.Text);
                if (Rcbx3.Checked)
                    criteria.PartnerPrograms.Add(Rcbx3.Text);
                if (Rcbx4.Checked)
                    criteria.PartnerPrograms.Add(Rcbx4.Text);
                if (Rcbx5.Checked)
                    criteria.PartnerPrograms.Add(Rcbx5.Text);
                if (Rcbx6.Checked)
                    criteria.PartnerPrograms.Add(Rcbx6.Text);
                if (Rcbx7.Checked)
                    criteria.PartnerPrograms.Add(Rcbx7.Text);

                GetPartnersListForRetailorsSearch("id", criteria);

                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                ShowSelectedChatPartnersName(0);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
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

        protected void EdRcbx1_OnCheckedChanged(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = false;
                LblRetailorFailureMsg.Text = string.Empty;
                divRetailorSuccess.Visible = false;
                LblRetailorSuccessMsg.Text = string.Empty;

                GlobalMethods.SearchCriteria criteria = new GlobalMethods.SearchCriteria();

                criteria.CompanyName = (divEditRetailorAdvancedSearch.Visible) ? TbxEditRetailorsAdvancedSearch.Text : TbxRetailorAdvancedSearch.Text;

                if (DdlEditRetailorCountries.SelectedItem.Value != "0")
                    criteria.Country = DdlEditRetailorCountries.SelectedItem.Text;

                if (DdlEditRetailorRegions.SelectedItem.Value != "0")
                    criteria.Region = DdlEditRetailorRegions.SelectedItem.Text;

                if (EdRcbx1.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx1.Text);
                if (EdRcbx2.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx2.Text);
                if (EdRcbx3.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx3.Text);
                if (EdRcbx4.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx4.Text);
                if (EdRcbx5.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx5.Text);
                if (EdRcbx6.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx6.Text);
                if (EdRcbx7.Checked)
                    criteria.PartnerPrograms.Add(EdRcbx7.Text);

                GetPartnersListForEditRetailorsSearch("id", criteria);

                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                ShowSelectedChatPartnersName(0);

                RadGrid rdgMailBox = (RadGrid)ControlFinder.FindControlRecursive(UcRdgMailBoxList, "RdgMailBox");
                if (rdgMailBox != null)
                {
                    rdgMailBox.Rebind();
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

        protected void ImgBtnDeleteLibraryFile_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                LblLibrarySelectedFile.Text = "";
                HdnCategoryId.Value = "0";
                HdnFileId.Value = "0";
                divLibrarySelectedFile.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aCreateRetailor_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                divRetailorFailure.Visible = divRetailorSuccess.Visible = false;

                GroupId = -1;
                TbxGroupName.Text = "";
                IsEditMode = false;

                GetPartnersList("id", null, 0);
                BtnCreate.Text = (!IsEditMode) ? "Create" : "Update";

                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenCreateRetailorPopUp();", true);
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
    }
}