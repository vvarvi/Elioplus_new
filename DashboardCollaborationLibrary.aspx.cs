using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using System.Data;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.Localization;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.UI;
using WdS.ElioPlus.Controls.Dashboard.LibraryStorage;
using System.Configuration;
using WdS.ElioPlus.Lib.EmailNotificationSender;

namespace WdS.ElioPlus
{
    public partial class DashboardCollaborationLibrary : System.Web.UI.Page
    {
        ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public string FileNameToDelete
        {
            get
            {
                if (ViewState["FileNameToDelete"] != null)
                    return ViewState["FileNameToDelete"].ToString();
                else
                    return "";
            }
            set
            {
                ViewState["FileNameToDelete"] = value;
            }
        }

        public int FileIdToDelete
        {
            get
            {
                if (ViewState["FileIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileIdToDelete"] = value;
            }
        }

        public int FileCategoryIdToDelete
        {
            get
            {
                if (ViewState["FileCategoryIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileCategoryIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileCategoryIdToDelete"] = value;
            }
        }

        public int FileTypeIdToDelete
        {
            get
            {
                if (ViewState["FileTypeIdToDelete"] != null)
                    return Convert.ToInt32(ViewState["FileTypeIdToDelete"].ToString());
                else
                    return -1;
            }
            set
            {
                ViewState["FileTypeIdToDelete"] = value;
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

                    Key = key;

                    if (isError)
                    {
                        Response.Redirect(vSession.Page = errorPage, false);
                        return;
                    }

                    FixPage();
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

        # region Methods

        private void FixPage()
        {
            if (!IsPostBack)
            {
                FillDropLists();

                UpdateStrings();
                SetLinks();

                UploadMessageAlert.Visible = false;
                vSession.VendorsResellersList.Clear();
                vSession.VendorsResellersList = null;
            }
                       
            LblElioplusDashboard.Text = "";
            LblDashSubTitle.Text = "";
        }

        private void SetLinks()
        {
            
        }

        private void UpdateStrings()
        {
            //LblCollaborationTitle.Text = "";            
        }

        private void FillDropLists()
        {
            List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

            if (defaultCategories.Count > 0)
            {
                Ddlcategory.Items.Clear();

                DropDownListItem item = new DropDownListItem();

                item.Value = "0";
                item.Text = "Choose category";

                Ddlcategory.Items.Add(item);

                foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
                {
                    item = new DropDownListItem();

                    item.Value = defaultCategory.Id.ToString();
                    item.Text = defaultCategory.CategoryDescription;

                    Ddlcategory.Items.Add(item);
                }
            }
        }

        public void GetLibraryFiles(DBSession session)
        {
            List<ElioCollaborationLibraryFilesDefaultCategories> defaultCategories = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategories(session);

            //foreach (ElioCollaborationLibraryFilesDefaultCategories defaultCategory in defaultCategories)
            //{
            //    if (!SqlCollaboration.ExistFileCategory(vSession.User.Id, defaultCategory.CategoryDescription, session))
            //    {
            //        ElioCollaborationUsersLibraryFilesCategories category = new ElioCollaborationUsersLibraryFilesCategories();

            //        category.UserId = vSession.User.Id;
            //        category.CategoryDescription = defaultCategory.CategoryDescription;
            //        category.DateCreated = DateTime.Now;
            //        category.LastUpdate = DateTime.Now;
            //        category.IsPublic = 1;

            //        DataLoader<ElioCollaborationUsersLibraryFilesCategories> loader = new DataLoader<ElioCollaborationUsersLibraryFilesCategories>(session);
            //        loader.Insert(category);
            //    }
            //}

            //List<ElioCollaborationUsersLibraryFilesCategories> files = SqlCollaboration.GetCollaborationUserLibraryPublicFilesCategories(vSession.User.Id, session);

            if (defaultCategories.Count > 0)
            {
                //RptFilesLibrary.Visible = true;
                UcMessageAlertLibraryControl.Visible = false;

                //RpLibraryFiles.Visible = true;
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

                //RptFilesLibrary.DataSource = table;
                //RptFilesLibrary.DataBind();
                //RpLibraryFiles.DataSource = table;
                //RpLibraryFiles.DataBind();

                FillDropLists();

                UpdatePanel7.Update();
            }
            else
            {
                //RptFilesLibrary.Visible = false;
                //RpLibraryFiles.Visible = false;

                string alert = "You have no Library Categories Files";
                GlobalMethods.ShowMessageControlDA(UcMessageAlertLibraryControl, alert, MessageTypes.Info, true, true, false);
            }
        }

        private bool Validate(out string alert)
        {
            alert = "";

            if (TbxFileTitle.Text == string.Empty)
            {
                alert = "Fill file title";

                return false;
            }

            //RadDropDownList ddlcategory = (RadDropDownList)ControlFinder.FindControlRecursive(UcLibraryCategories, "Ddlcategory");

            if (Ddlcategory.SelectedItem.Value == "0")
            {
                alert = "Select category";

                return false;
            }

            # region check file

            var fileContent = inputFile.PostedFile;

            if (fileContent != null && fileContent.ContentLength > 0)
            {
                var fileSize = fileContent.ContentLength;
                var fileType = fileContent.ContentType;

                int maxCollaborationFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]);

                //if (fileType != "application/octet-stream")
                //{
                //if (fileType == "application/pdf" || (fileType == "application/csv" || fileType == "application/vnd.ms-excel"))
                //{
                if (fileSize > maxCollaborationFileLenght)
                {
                    alert = "Your file size is outside the bounds. Please try smaller file size to send or contact with us.";

                    return false;
                }
                //}
                //else
                //{
                //    alert = "Wrong file type!";

                //    return false;
                //}
                //}
                //else
                //{
                //    alert = "File is application/octet-stream!";

                //    return false;
                //}
            }
            else
            {
                alert = "Select file to upload!";

                return false;
            }

            # endregion

            #region User Available File Storage Space

            if (!GlobalDBMethods.UserHasAvailableStorage(vSession.User.Id, fileContent.ContentLength, session))
            {
                alert = " Your available storage space is not enough for this file to be uploaded";
                return false;
            }

            #endregion

            return true;
        }

        private bool FileExistsOld(DataTable table, string fileName)
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

        private bool FileExistsNew(DataTable table, string fileName)
        {
            foreach (DataRow row in table.Rows)
            {
                if (row["file_name"].ToString() == fileName)
                {
                    return true;
                }
            }

            return false;
        }

        # endregion

        #region Grids

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
                    Image imgFromTo = (Image)ControlFinder.FindControlRecursive(item, "ImgFromTo");
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
                                    imgFromTo.AlternateText = "Send to '" + collGroup.CollaborationGroupName + "' group";
                                }
                                else
                                {
                                    imgFromTo.ToolTip = "Send to group";
                                    imgFromTo.AlternateText = "Send to group";
                                }
                            }
                            else
                            {
                                imgFromTo.ToolTip = "Send to " + partner.CompanyName;
                                imgFromTo.AlternateText = "Send to " + partner.CompanyName;
                            }
                        }
                        else if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text) && partner.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text))
                        {
                            if (Convert.ToInt32(item["collaboration_group_id"].Text) > 0)
                            {
                                ElioCollaborationUsersGroups collGroup = SqlCollaboration.GetCollaborationUserGroupById(Convert.ToInt32(item["collaboration_group_id"].Text), session);
                                if (collGroup != null)
                                {
                                    imgFromTo.ToolTip = "Send by '" + collGroup.CollaborationGroupName + "' group";
                                    imgFromTo.AlternateText = "Send by '" + collGroup.CollaborationGroupName + "' group";
                                }
                                else
                                {
                                    imgFromTo.ToolTip = "Send by group";
                                    imgFromTo.AlternateText = "Send by group";
                                }
                            }
                            else
                            {
                                imgFromTo.ToolTip = "Send by " + partner.CompanyName;
                                imgFromTo.AlternateText = "Send by " + partner.CompanyName;
                            }
                        }
                    }
                    else
                    {
                        if (vSession.User.Id == Convert.ToInt32(item["uploaded_by_user_id"].Text) && vSession.User.Id == Convert.ToInt32(item["user_id"].Text))
                        {
                            imgFromTo.ToolTip = "Uploaded by me";
                            imgFromTo.AlternateText = "Uploaded by me";
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
                                        imgFromTo.AlternateText = "Send to " + collGroup.CollaborationGroupName + " group";
                                    }
                                    else
                                    {
                                        imgFromTo.ToolTip = "Send to group";
                                        imgFromTo.AlternateText = "Send to group";
                                    }
                                }
                                else
                                {
                                    imgFromTo.ToolTip = "Send to " + partner.CompanyName;
                                    imgFromTo.AlternateText = "Send to " + partner.CompanyName;
                                }
                            }
                            else
                                imgFromTo.Visible = false;
                        }
                        else if (vSession.User.Id == Convert.ToInt32(item["user_id"].Text) && vSession.User.Id != Convert.ToInt32(item["uploaded_by_user_id"].Text))
                        {
                            partner = Sql.GetUserById(Convert.ToInt32(item["uploaded_by_user_id"].Text), session);
                            if (partner != null)
                            {
                                imgFromTo.ToolTip = "Send by " + partner.CompanyName;
                                imgFromTo.AlternateText = "Send by " + partner.CompanyName;
                            }
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

                //RpLibraryFiles.Visible = false;

                if (defaultCategories.Count > 0)
                {
                    //RpLibraryFiles.Visible = true;
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

                    //RpLibraryFiles.DataSource = table;
                }
                else
                {
                    //RpLibraryFiles.Visible = false;
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

                            #region to delete

                            //string logedUserFilesPathTargetFolder = System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + SelectedCategory + "\\" + vSession.User.GuId + "\\";

                            //if (Directory.Exists(FileHelper.AddRootToPath(logedUserFilesPathTargetFolder)))
                            //{
                            //    DirectoryInfo filesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(logedUserFilesPathTargetFolder));

                            //    List<FileInfo> loggedUserFiles = filesDirectory.GetFiles().ToList();

                            //    if (loggedUserFiles.Count > 0)
                            //    {
                            //        for (int i = loggedUserFiles.Count - 1; i >= 0; i--)
                            //        {
                            //            if (!FileExists(table, loggedUserFiles[i].Name))
                            //                table.Rows.Add(i, vSession.User.GuId, loggedUserFiles[i].DirectoryName, SelectedCategory, "(" + vSession.User.CompanyName + " folder)", "file-type", loggedUserFiles[i].Name, loggedUserFiles[i].Length, loggedUserFiles[i].LastWriteTime.ToString("dd/MM/yyyy"));
                            //        }
                            //    }
                            //}

                            #endregion

                            ElioUsers partner = Sql.GetUserByGuId(Key, session);

                            if (partner == null)
                            {
                                List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                                if (userFiles.Count > 0)
                                {
                                    foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                                    {
                                        if (!FileExistsOld(table, file.FileName))
                                            table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                                    }
                                }                                
                            }
                            else if (partner != null)        //(Key != "")
                            {
                                #region to delete

                                //string partnerUserFilesPathTargetFolder = System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + SelectedCategory + "\\" + Key + "\\";

                                //if (Directory.Exists(FileHelper.AddRootToPath(partnerUserFilesPathTargetFolder)))
                                //{
                                //    DirectoryInfo filesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(partnerUserFilesPathTargetFolder));

                                //    List<FileInfo> partnerUserFiles = filesDirectory.GetFiles().ToList();

                                //    if (partnerUserFiles.Count > 0)
                                //    {
                                //        string partnerCompanyName = "(other folder)";

                                //        ElioUsers partner = Sql.GetUserByGuId(Key, session);
                                //        if (partner != null)
                                //            partnerCompanyName = partner.CompanyName;

                                //        for (int i = partnerUserFiles.Count - 1; i >= 0; i--)
                                //        {
                                //            if (!FileExists(table, partnerUserFiles[i].Name))
                                //                table.Rows.Add(i, Key, partnerUserFiles[i].DirectoryName, SelectedCategory, "(" + partnerCompanyName + " folrder)", "file-type", partnerUserFiles[i].Name, partnerUserFiles[i].Length, partnerUserFiles[i].LastWriteTime.ToString("dd/MM/yyyy"));
                                //        }
                                //    }
                                //}

                                #endregion

                                if (partner != null)
                                {
                                    List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                                    if (partnerUserFiles.Count > 0)
                                    {
                                        foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                                        {
                                            if (!FileExistsOld(table, file.FileName))
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
        
        protected void RptFilesLibrary_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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

                        CheckBox cbxSelect = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelect");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        //HiddenField hdnUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "user_id");
                        HiddenField hdnCategory = (HiddenField)ControlFinder.FindControlRecursive(item, "category_description");
                        HiddenField hdnTotalMineFilesCount = (HiddenField)ControlFinder.FindControlRecursive(item, "mine_total_files_count");
                        HiddenField hdnTotalPartnerFilesCount = (HiddenField)ControlFinder.FindControlRecursive(item, "partner_total_files_count");
                        HiddenField hdnNewFilesCount = (HiddenField)ControlFinder.FindControlRecursive(item, "new_files_count");
                        HiddenField hdnDate = (HiddenField)ControlFinder.FindControlRecursive(item, "sysdate");
                        HiddenField hdnLastUpdate = (HiddenField)ControlFinder.FindControlRecursive(item, "is_default");
                        HiddenField hdnIsPublic = (HiddenField)ControlFinder.FindControlRecursive(item, "is_public");

                        hdnId.Value = row["id"].ToString();
                        cbxSelect.InputAttributes.Add("id", hdnId.Value);

                        //hdnUserId.Value = row["user_id"].ToString();
                        //cbxSelect.InputAttributes.Add("user_id", hdnUserId.Value);

                        hdnCategory.Value = row["category_description"].ToString();
                        cbxSelect.InputAttributes.Add("category_description", hdnCategory.Value);

                        hdnTotalMineFilesCount.Value = row["mine_total_files_count"].ToString();
                        cbxSelect.InputAttributes.Add("mine_total_files_count", hdnCategory.Value);

                        hdnTotalPartnerFilesCount.Value = row["partner_total_files_count"].ToString();
                        cbxSelect.InputAttributes.Add("partner_total_files_count", hdnCategory.Value);

                        hdnNewFilesCount.Value = row["new_files_count"].ToString();
                        cbxSelect.InputAttributes.Add("new_files_count", hdnCategory.Value);

                        hdnDate.Value = row["sysdate"].ToString();
                        hdnLastUpdate.Value = row["is_default"].ToString();
                        hdnIsPublic.Value = row["is_public"].ToString();

                        LinkButton lnkBtnCategory = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnCategory");
                        lnkBtnCategory.Text = hdnCategory.Value;

                        LinkButton lnkBtnMineTotalFilesCount = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnMineTotalFilesCount");
                        lnkBtnMineTotalFilesCount.Text = hdnTotalMineFilesCount.Value + " (mine)";

                        LinkButton lnkBtnPartnerTotalFilesCount = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnPartnerTotalFilesCount");
                        lnkBtnPartnerTotalFilesCount.Text = hdnTotalPartnerFilesCount.Value + " (partner's)";

                        lnkBtnPartnerTotalFilesCount.Visible = ShowPartner;

                        LinkButton lnkBtnNewFilesCount = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnNewFilesCount");
                        lnkBtnNewFilesCount.Text = hdnNewFilesCount.Value + " (new)";
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

        protected void RptFilesLibrary_ItemCommand(object sender, RepeaterCommandEventArgs args)
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

        protected void RptFilesInCategory_OnItemDataBound(object sender, RepeaterItemEventArgs args)
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

                        CheckBox cbxSelect = (CheckBox)ControlFinder.FindControlRecursive(item, "CbxSelect");

                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "id");
                        HiddenField hdnUserId = (HiddenField)ControlFinder.FindControlRecursive(item, "user_id");
                        HiddenField hdnCategory = (HiddenField)ControlFinder.FindControlRecursive(item, "category_description");
                        HiddenField hdnDate = (HiddenField)ControlFinder.FindControlRecursive(item, "date_created");
                        HiddenField hdnLastUpdate = (HiddenField)ControlFinder.FindControlRecursive(item, "last_update");
                        HiddenField hdnIsPublic = (HiddenField)ControlFinder.FindControlRecursive(item, "is_public");

                        hdnId.Value = row["id"].ToString();
                        cbxSelect.InputAttributes.Add("id", hdnId.Value);

                        hdnUserId.Value = row["user_id"].ToString();
                        cbxSelect.InputAttributes.Add("user_id", hdnUserId.Value);

                        hdnCategory.Value = row["category_description"].ToString();
                        cbxSelect.InputAttributes.Add("category_description", hdnCategory.Value);

                        hdnDate.Value = hdnId.Value = row["date_created"].ToString();
                        hdnLastUpdate.Value = hdnId.Value = row["last_update"].ToString();
                        hdnIsPublic.Value = hdnId.Value = row["is_public"].ToString();

                        LinkButton lnkBtnCategory = (LinkButton)ControlFinder.FindControlRecursive(item, "LnkBtnCategory");
                        lnkBtnCategory.Text = hdnCategory.Value;
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

        private void BuildGridItem(GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                if (session.Connection.State == ConnectionState.Closed)
                    session.OpenConnection();

                GridDataItem item = (GridDataItem)e.Item;

                HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                ElioCollaborationUsersLibraryFiles file = SqlCollaboration.GetCollaborationUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                if (file != null)
                {
                    ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                    RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                    Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                    Label lblUploadDate = (Label)ControlFinder.FindControlRecursive(item, "LblUploadDate");
                    if (file.FileTitle.Length >= 45)
                    {
                        imgFileNameInfo.Visible = true;
                        rttFileNameInfo.Text = file.FileTitle;
                        lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                        lblUploadDate.Text = file.DateCreated.ToString();
                    }
                    else
                    {
                        imgFileNameInfo.Visible = false;
                        lblFileName.Text = file.FileTitle;
                        lblUploadDate.Text = file.DateCreated.ToString();
                    }

                    string[] filePath = file.FilePath.Split('/').ToArray();
                    if (filePath.Length > 0)
                    {
                        string fileNameWithExtension = filePath[filePath.Length - 1];
                        string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                        if (fileInArray.Length > 0)
                        {
                            string extension = fileInArray[fileInArray.Length - 1].ToLower();

                            Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                            if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                            {
                                imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                                imgFile.AlternateText = extension;
                            }
                            else
                            {
                                imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";
                                imgFile.AlternateText = "file";
                            }
                        }
                    }

                    HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                    hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                }
            }
        }

        private DataTable LoadGridData(RadGrid grd, int selectedCategoryID, DBSession session)
        {
            DataTable table = new DataTable();

            table.Columns.Add("id");
            table.Columns.Add("user_id");
            table.Columns.Add("file_type_id");
            table.Columns.Add("category_id");
            table.Columns.Add("file_title");
            table.Columns.Add("file_name");
            table.Columns.Add("file_path");
            table.Columns.Add("file_type");
            table.Columns.Add("file_format_extensions");
            table.Columns.Add("date_created");
            table.Columns.Add("is_public");
            table.Columns.Add("is_new");

            ElioUsers partner = Sql.GetUserByGuId(Key, session);

            if (partner == null)
            {
                List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                if (userFiles.Count > 0)
                {
                    grd.Visible = true;
                    foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                    {
                        if (!FileExistsNew(table, file.FileName))
                        {
                            //table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                            table.Rows.Add(file.Id, file.UserId, "file.FileTypeId", file.CategoryId, file.FileTitle, file.FileName, file.FilePath, file.FileType, "file.FileFormatExtensions", file.DateCreated, file.IsPublic, file.IsNew);
                        }
                    }
                }
            }
            else if (partner != null)        //(Key != "")
            {
                List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                if (partnerUserFiles.Count > 0)
                {
                    grd.Visible = true;
                    foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                    {
                        if (!FileExistsNew(table, file.FileName))
                            //table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                            table.Rows.Add(file.Id, file.UserId, "file.FileTypeId", file.CategoryId, file.FileTitle, file.FileName, file.FilePath, file.FileType, "file.FileFormatExtensions", file.DateCreated, file.IsPublic, file.IsNew);
                    }
                }
            }

            return table;           
        }

        protected void RdgProductUpdates_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                #region to delete

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioCollaborationUsersLibraryFiles file = SqlCollaboration.GetCollaborationUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}

                #endregion
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgProductUpdates_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 1;

                DataTable table = LoadGridData(RdgProductUpdates, selectedCategoryID, session);

                #region to delete

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                #endregion

                if (table.Rows.Count > 0)
                {
                    RdgProductUpdates.DataSource = table;
                }
                else
                {
                    RdgProductUpdates.DataSource = null;
                    RdgProductUpdates.Visible = false;
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

        protected void RdgMarketingMaterial_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioOnboardingUsersLibraryFiles file = Sql.GetOnboardingUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgMarketingMaterial_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 2;

                DataTable table = LoadGridData(RdgMarketingMaterial, selectedCategoryID, session);

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    RdgMarketingMaterial.DataSource = table;
                }
                else
                {
                    RdgMarketingMaterial.DataSource = null;
                    RdgMarketingMaterial.Visible = false;
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

        protected void RdgNewCampaign_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioOnboardingUsersLibraryFiles file = Sql.GetOnboardingUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgNewCampaign_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 3;

                DataTable table = LoadGridData(RdgNewCampaign, selectedCategoryID, session);

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    RdgNewCampaign.DataSource = table;
                }
                else
                {
                    RdgNewCampaign.DataSource = null;
                    RdgNewCampaign.Visible = false;
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

        protected void RdgNewsLetter_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioOnboardingUsersLibraryFiles file = Sql.GetOnboardingUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgNewsLetter_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 4;

                DataTable table = LoadGridData(RdgNewsLetter, selectedCategoryID, session);

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    RdgNewsLetter.DataSource = table;
                }
                else
                {
                    RdgNewsLetter.DataSource = null;
                    RdgNewsLetter.Visible = false;
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

        protected void RdgBanner_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioOnboardingUsersLibraryFiles file = Sql.GetOnboardingUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgBanner_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 5;

                DataTable table = LoadGridData(RdgBanner, selectedCategoryID, session);

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    RdgBanner.DataSource = table;
                }
                else
                {
                    RdgBanner.DataSource = null;
                    RdgBanner.Visible = false;
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

        protected void RdgDocumentationPdf_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                BuildGridItem(e);

                //if (e.Item is GridDataItem)
                //{
                //    if (session.Connection.State == ConnectionState.Closed)
                //        session.OpenConnection();

                //    GridDataItem item = (GridDataItem)e.Item;

                //    HtmlAnchor aDelete = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDelete");
                //    aDelete.Visible = vSession.User.CompanyType == Types.Vendors.ToString();

                //    ElioOnboardingUsersLibraryFiles file = Sql.GetOnboardingUserLibraryFileById(Convert.ToInt32(item["id"].Text), session);
                //    if (file != null)
                //    {
                //        ImageButton imgFileNameInfo = (ImageButton)ControlFinder.FindControlRecursive(item, "ImgFileNameInfo");
                //        RadToolTip rttFileNameInfo = (RadToolTip)ControlFinder.FindControlRecursive(item, "RttFileNameInfo");
                //        Label lblFileName = (Label)ControlFinder.FindControlRecursive(item, "LblFileName");
                //        if (file.FileTitle.Length >= 45)
                //        {
                //            imgFileNameInfo.Visible = true;
                //            rttFileNameInfo.Text = file.FileTitle;
                //            lblFileName.Text = file.FileTitle.Substring(0, 45) + "...";
                //        }
                //        else
                //        {
                //            imgFileNameInfo.Visible = false;
                //            lblFileName.Text = file.FileTitle;
                //        }

                //        string[] filePath = file.FilePath.Split('/').ToArray();
                //        if (filePath.Length > 0)
                //        {
                //            string fileNameWithExtension = filePath[filePath.Length - 1];
                //            string[] fileInArray = fileNameWithExtension.Split('.').ToArray();
                //            if (fileInArray.Length > 0)
                //            {
                //                string extension = fileInArray[fileInArray.Length - 1].ToLower();

                //                Image imgFile = (Image)ControlFinder.FindControlRecursive(item, "ImgFile");

                //                if (File.Exists(Server.MapPath("~/images/OnboardingFileTypes/" + extension + ".png")))
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/" + extension + ".png";
                //                else
                //                    imgFile.ImageUrl = "~/images/OnboardingFileTypes/file.png";

                //                HtmlGenericControl divTile = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divTile");

                //                if (divTile != null)
                //                {
                //                    switch (extension)
                //                    {
                //                        case "ppt":
                //                        case "pptx":
                //                            divTile.Attributes["class"] = "tile selected bg-yellow-saffron";
                //                            break;
                //                        case "html":
                //                            divTile.Attributes["class"] = "tile bg-green";
                //                            break;
                //                        case "pdf":
                //                            divTile.Attributes["class"] = "tile bg-red-sunglo";
                //                            break;
                //                        case "doc":
                //                        case "docx":
                //                        case "txt":
                //                            divTile.Attributes["class"] = "tile bg-blue-madison";
                //                            break;
                //                        case "xls":
                //                        case "xlsx":
                //                        case "csv":
                //                            divTile.Attributes["class"] = "tile bg-green-meadow";
                //                            break;
                //                        case "bmp":
                //                        case "gif":
                //                        case "jpg":
                //                        case "jpeg":
                //                        case "png":
                //                            divTile.Attributes["class"] = "tile bg-purple-studio";
                //                            break;
                //                        case "wav":
                //                        case "wma":
                //                        case "avi":
                //                            divTile.Attributes["class"] = "tile bg-grey-cascade";
                //                            break;

                //                        default:
                //                            divTile.Attributes["class"] = "tile bg-blue-hoki";
                //                            break;
                //                    }
                //                }
                //            }
                //        }

                //        HyperLink hpLnkFile = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkFile");
                //        hpLnkFile.NavigateUrl = (HttpContext.Current.Request.IsLocal) ? "http://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text : "https://" + HttpContext.Current.Request.Url.Authority + ConfigurationManager.AppSettings["ViewOnboardingLibraryFilesPath"].ToString() + "/" + item["file_path"].Text;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void RdgDocumentationPdf_OnNeedDataSource(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                int selectedCategoryID = 6;

                DataTable table = LoadGridData(RdgDocumentationPdf, selectedCategoryID, session);

                //table.Columns.Add("id");
                //table.Columns.Add("user_id");
                //table.Columns.Add("uploaded_by_user_id");
                //table.Columns.Add("user_guid");
                //table.Columns.Add("file_path");
                //table.Columns.Add("selected_category");
                //table.Columns.Add("company_name");
                //table.Columns.Add("file_type");
                //table.Columns.Add("fileName");
                //table.Columns.Add("fileSize");
                //table.Columns.Add("date");
                //table.Columns.Add("is_new");
                //table.Columns.Add("collaboration_group_id");

                //ElioUsers partner = Sql.GetUserByGuId(Key, session);

                //if (partner == null)
                //{
                //    List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, selectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                //    if (userFiles.Count > 0)
                //    {
                //        foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                //        {
                //            if (!FileExists(table, file.FileName))
                //                table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //        }
                //    }
                //}
                //else if (partner != null)        //(Key != "")
                //{
                //    if (partner != null)
                //    {
                //        List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, selectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                //        if (partnerUserFiles.Count > 0)
                //        {
                //            foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                //            {
                //                if (!FileExists(table, file.FileName))
                //                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, selectedCategoryID, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                //            }
                //        }
                //    }
                //}

                if (table.Rows.Count > 0)
                {
                    RdgDocumentationPdf.DataSource = table;
                }
                else
                {
                    RdgDocumentationPdf.DataSource = null;
                    RdgDocumentationPdf.Visible = false;
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

        # region Buttons
                
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

                    GetLibraryFiles(session);
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

        protected void BtnUploadFile_OnCick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                string serverMapPathTargetFolder = "";
                string fileName = "";
                bool successUpload = false;
                UploadMessageAlert.Visible = false;
                string alert = "";

                if (!Validate(out alert))
                {
                    //GlobalMethods.ShowMessageControl(UploadMessageAlert, alert, MessageTypes.Error, false, true, false);
                    LblFileUploadTitle.Text = "File Uploading Warning";
                    LblFileUploadfMsg.Text = alert;
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                    return;
                }

                try
                {
                    # region updoad file

                    var fileContent = inputFile.PostedFile;

                    //RadDropDownList ddlcategory = (RadDropDownList)ControlFinder.FindControlRecursive(UcLibraryCategories, "Ddlcategory");

                    serverMapPathTargetFolder = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryTargetFolder"].ToString()) + Ddlcategory.SelectedText + "/" + vSession.User.GuId + "/";

                    fileName = fileContent.FileName;

                    successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolder, fileContent, out fileName, session);

                    # endregion

                    if (successUpload)
                    {
                        try
                        {
                            session.BeginTransaction();

                            #region User File

                            ElioCollaborationUsersLibraryFiles libraryFile = new ElioCollaborationUsersLibraryFiles();

                            libraryFile.CategoryId = Convert.ToInt32(Ddlcategory.SelectedValue);
                            libraryFile.FileTitle = "Library_" + TbxFileTitle.Text.Replace(" ", "_").Trim();
                            libraryFile.FileName = fileName;
                            libraryFile.FilePath = Ddlcategory.SelectedText + "/" + vSession.User.GuId + "/" + fileName;       //serverMapPathTargetFolder;
                            libraryFile.FileType = fileContent.ContentType;
                            libraryFile.IsPublic = 1;
                            libraryFile.MailboxId = -1;
                            libraryFile.IsNew = 1;
                            libraryFile.UserId = vSession.User.Id;
                            libraryFile.UploadedByUserId = vSession.User.Id;
                            libraryFile.DateCreated = DateTime.Now;
                            libraryFile.LastUpdate = DateTime.Now;

                            DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                            loader.Insert(libraryFile);

                            #endregion

                            #region Blob File

                            ElioCollaborationBlobFiles blobFile = new ElioCollaborationBlobFiles();

                            blobFile.FileName = fileName;
                            //blobFile.CategoryDescription = Ddlcategory.SelectedItem.Text;
                            //blobFile.FilePath = serverMapPathTargetFolder;
                            blobFile.FileSize = fileContent.ContentLength;
                            blobFile.FileType = fileContent.ContentType;
                            blobFile.IsPublic = 1;
                            blobFile.DateCreated = DateTime.Now;
                            blobFile.LastUpdate = DateTime.Now;
                            blobFile.LibraryFilesId = libraryFile.Id;
                            blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolder + "/" + blobFile.FileName);

                            DataLoader<ElioCollaborationBlobFiles> blbLoader = new DataLoader<ElioCollaborationBlobFiles>(session);
                            blbLoader.Insert(blobFile);

                            #endregion

                            #region  Update User PacketStatus Available File Storage Count

                            bool success = GlobalDBMethods.ReduceUserFileStorage(vSession.User.Id, Convert.ToDouble(fileContent.ContentLength), session);
                            if (!success)
                                throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));

                            #endregion

                            #region to delete

                            ////double fileSizeGB = GlobalMethods.ConvertSize(Convert.ToDouble(fileContent.ContentLength), "GB");

                            ////ElioUserPacketStatus userPacketStatus = Sql.GetUserPacketStatusFeatures(vSession.User.Id, session);
                            ////if (userPacketStatus != null)
                            ////{
                            ////    if (userPacketStatus.AvailableLibraryStorageCount > 0 && userPacketStatus.AvailableLibraryStorageCount >= Convert.ToDecimal(fileSizeGB))
                            ////    {
                            ////        userPacketStatus.AvailableLibraryStorageCount = userPacketStatus.AvailableLibraryStorageCount - Convert.ToDecimal(fileSizeGB);
                            ////        userPacketStatus.LastUpdate = DateTime.Now;

                            ////        DataLoader<ElioUserPacketStatus> packetStatusLoader = new DataLoader<ElioUserPacketStatus>(session);
                            ////        packetStatusLoader.Update(userPacketStatus);
                            ////    }
                            ////    else
                            ////    {
                            ////        //not enought storage space
                            ////        throw new Exception(string.Format("User {0} tried to upload a file at {1} but he has not enough storage space from his packet (user billing packet type id = {2}), so it could not be saved or uploaded!", vSession.User.Id, DateTime.Now.ToString(), vSession.User.BillingType.ToString()));
                            ////    }
                            ////}

                            #endregion

                            session.CommitTransaction();
                        }
                        catch (Exception ex)
                        {
                            session.RollBackTransaction();
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            throw ex;
                        }

                        try
                        {
                            if (vSession.User.CompanyType == Types.Vendors.ToString())
                            {
                                List<string> emails = SqlCollaboration.GetResellersEmailByVendorUserId(vSession.User.Id, vSession.LoggedInSubAccountRoleID, vSession.SubAccountEmailLogin, CollaborateInvitationStatus.Confirmed.ToString(), session);

                                if (emails.Count > 0)
                                    EmailSenderLib.SendNewUploadedFileEmail(vSession.User.CompanyName, "", Ddlcategory.SelectedText, emails, false, vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded new onboarding file at {1}, but no collaboration partner email was found to send notification email to", vSession.User.Id.ToString(), DateTime.Now.ToString()), "DashboardPartnerOnboarding.aspx --> ERROR sending notification Email");
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            throw ex;
                        }

                        //RadGrid rdgViewLogs = (RadGrid)ControlFinder.FindControlRecursive(UcViewLibraryFiles, "RdgViewLogs");
                        //if (rdgViewLogs != null)
                        //    rdgViewLogs.Rebind();
                          
                        LblFileUploadTitle.Text = "File Uploading";
                        LblFileUploadfMsg.Text = "File was successfully uploaded to your library.";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                        //GetLibraryFiles(session);

                        UcStorageVolume ucStorageVolume = (UcStorageVolume)ControlFinder.FindControlRecursive(this.Master, "UcStorageVolume");
                        if (ucStorageVolume != null)
                            ucStorageVolume.GetUserStorageInfo();

                        switch(Ddlcategory.SelectedValue)
                        {
                            case "1":
                                RdgProductUpdates.Rebind();
                                break;

                            case "2":
                                RdgMarketingMaterial.Rebind();
                                break;

                            case "3":
                                RdgNewCampaign.Rebind();
                                break;

                            case "4":
                                RdgNewsLetter.Rebind();
                                break;

                            case "5":
                                RdgBanner.Rebind();
                                break;
                                
                            case "6":
                                RdgDocumentationPdf.Rebind();
                                break;
                        }

                        TbxFileTitle.Text = string.Empty;
                        Ddlcategory.SelectedItem.Value = "0";
                    }
                    else
                    {
                        LblFileUploadTitle.Text = "File Uploading Warning";
                        LblFileUploadfMsg.Text = "File could not be uploaded to your library (maybe it's size was outside the bounds). Please try again or contact with us.";
                        System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    //GlobalMethods.ShowMessageControl(UploadMessageAlert, "File could not be uploaded and saved.", MessageTypes.Error, false, true, false);
                    LblFileUploadTitle.Text = "File Uploading Warning";
                    LblFileUploadfMsg.Text = "File could not be uploaded to your library";
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);
                    bool deleted = false;

                    if (successUpload)
                        deleted = UpLoadImage.DeleteFileFromDirectory(serverMapPathTargetFolder, fileName);

                    Logger.DetailedError(Request.Url.ToString(), (deleted) ? string.Format("File not deleted successfully int path {0}", serverMapPathTargetFolder + "\\" + fileName) : "", ex.Message.ToString(), ex.StackTrace.ToString());
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
                
        protected void LnkBtnCategory_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LinkButton btn = (LinkButton)sender;

                    var item = (btn).Parent as RepeaterItem;
                    if (item != null)
                    {
                        //LoadSelectedFilesToPreview(item, false);
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

        protected void LnkBtnNewFilesCount_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    LinkButton btn = (LinkButton)sender;

                    var item = (btn).Parent as RepeaterItem;
                    if (item != null)
                    {
                        //LoadSelectedFilesToPreview(item, true);
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

                        UcStorageVolume ucStorageVolume = (UcStorageVolume)ControlFinder.FindControlRecursive(this.Master, "UcStorageVolume");
                        if (ucStorageVolume != null)
                            ucStorageVolume.GetUserStorageInfo();

                        //RpLibraryFiles.Rebind();

                        UpdatePanel updatePanel1 = (UpdatePanel)ControlFinder.FindControlRecursive(this.Master, "UpdatePanel1");
                        if (updatePanel1 != null)
                            updatePanel1.Update();
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

                int simpleMsgCount = 0;
                int groupMsgCount = 0;
                int totalNewMsgCount = 0;
                this.Master.ShowMasterNotifications(out simpleMsgCount, out groupMsgCount, out totalNewMsgCount);

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

        protected void aDelete_OnClick(object sender, EventArgs args)
        {
            try
            {
                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                GridDataItem item = (GridDataItem)imgBtn.NamingContainer;

                //FileTypeIdToDelete = Convert.ToInt32(item["file_type_id"].Text);
                FileIdToDelete = Convert.ToInt32(item["id"].Text);
                FileNameToDelete = item["file_name"].Text;
                FileCategoryIdToDelete = Convert.ToInt32(item["category_id"].Text);

                if (FileIdToDelete > 0 && FileNameToDelete != "" && FileCategoryIdToDelete > -1)
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfPopUp();", true);
                else
                {
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;
                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void BtnDeleteFile_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;
                bool deleted = false;
                if (FileIdToDelete > 0 && !string.IsNullOrEmpty(FileNameToDelete))
                {
                    try
                    {
                        session.BeginTransaction();

                        SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, FileIdToDelete, false, false, session);

                        deleted = true;
                        session.CommitTransaction();
                    }
                    catch (Exception ex)
                    {
                        session.RollBackTransaction();
                        Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                        deleted = false;
                    }

                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    if (deleted)
                    {
                        ElioCollaborationLibraryFilesDefaultCategories fCategory = SqlCollaboration.GetCollaborationUserLibraryPublicDefaultFilesCategoriesById(FileCategoryIdToDelete, session);
                        if (fCategory != null)
                        {
                            DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + fCategory.CategoryDescription + "\\" + vSession.User.GuId + "\\"));

                            foreach (FileInfo logFile in filesInDirectory.GetFiles())
                            {
                                if (logFile.Name == FileNameToDelete)
                                {
                                    logFile.Delete();

                                    FileTypeIdToDelete = -1;
                                    FileIdToDelete = -1;
                                    FileNameToDelete = "";

                                    break;
                                }
                            }


                            switch (fCategory.Id.ToString())
                            {
                                case "1":
                                    RdgProductUpdates.Rebind();

                                    tab_1_1.Attributes["class"] = "tab-pane fade active in";
                                    liProductUpdates.Attributes["class"] = "active";
                                    tab_1_1.Visible = true;

                                    tab_1_2.Attributes["class"] = "tab-pane fade";
                                    liMarketingMaterial.Attributes["class"] = "";
                                    tab_1_2.Visible = false;
                                    tab_1_3.Attributes["class"] = "tab-pane fade";
                                    liNewCampaign.Attributes["class"] = "";
                                    tab_1_3.Visible = false;
                                    tab_1_4.Attributes["class"] = "tab-pane fade";
                                    liNewsLetter.Attributes["class"] = "";
                                    tab_1_4.Visible = false;
                                    tab_1_5.Attributes["class"] = "tab-pane fade";
                                    liBanner.Attributes["class"] = "";
                                    tab_1_5.Visible = false;
                                    tab_1_6.Attributes["class"] = "tab-pane fade";
                                    liDocumentationPdf.Attributes["class"] = "";
                                    tab_1_6.Visible = false;

                                    LblProductUpdates.ForeColor = System.Drawing.Color.Gray;
                                    LblProductUpdates.Font.Bold = true;

                                    LblMarketingMaterial.Font.Bold = false;
                                    LblNewCampaign.Font.Bold = false;
                                    LblbNewsLetter.Font.Bold = false;
                                    LblBanner.Font.Bold = false;
                                    LblDocumentationPdf.Font.Bold = false;

                                    break;

                                case "2":
                                    RdgMarketingMaterial.Rebind();
                                    RdgMarketingMaterial.DataBind();

                                    tab_1_2.Attributes["class"] = "tab-pane fade active in";
                                    liMarketingMaterial.Attributes["class"] = "active";
                                    tab_1_2.Visible = true;

                                    tab_1_1.Attributes["class"] = "tab-pane fade";
                                    liProductUpdates.Attributes["class"] = "";
                                    tab_1_1.Visible = false;

                                    tab_1_3.Attributes["class"] = "tab-pane fade";
                                    liNewCampaign.Attributes["class"] = "";
                                    tab_1_3.Visible = false;
                                    tab_1_4.Attributes["class"] = "tab-pane fade";
                                    liNewsLetter.Attributes["class"] = "";
                                    tab_1_4.Visible = false;
                                    tab_1_5.Attributes["class"] = "tab-pane fade";
                                    liBanner.Attributes["class"] = "";
                                    tab_1_5.Visible = false;
                                    tab_1_6.Attributes["class"] = "tab-pane fade";
                                    liDocumentationPdf.Attributes["class"] = "";
                                    tab_1_6.Visible = false;

                                    LblMarketingMaterial.ForeColor = System.Drawing.Color.Gray;
                                    LblMarketingMaterial.Font.Bold = true;

                                    LblProductUpdates.Font.Bold = false;
                                    LblNewCampaign.Font.Bold = false;
                                    LblbNewsLetter.Font.Bold = false;
                                    LblBanner.Font.Bold = false;
                                    LblDocumentationPdf.Font.Bold = false;

                                    break;

                                case "3":
                                    RdgNewCampaign.Rebind();
                                    RdgNewCampaign.DataBind();

                                    tab_1_3.Attributes["class"] = "tab-pane fade active in";
                                    liNewCampaign.Attributes["class"] = "active";
                                    tab_1_3.Visible = true;

                                    tab_1_1.Attributes["class"] = "tab-pane fade";
                                    liProductUpdates.Attributes["class"] = "";
                                    tab_1_1.Visible = false;
                                    tab_1_2.Attributes["class"] = "tab-pane fade";
                                    liMarketingMaterial.Attributes["class"] = "";
                                    tab_1_2.Visible = false;
                                    tab_1_4.Attributes["class"] = "tab-pane fade";
                                    liNewsLetter.Attributes["class"] = "";
                                    tab_1_4.Visible = false;
                                    tab_1_5.Attributes["class"] = "tab-pane fade";
                                    liBanner.Attributes["class"] = "";
                                    tab_1_5.Visible = false;
                                    tab_1_6.Attributes["class"] = "tab-pane fade";
                                    liDocumentationPdf.Attributes["class"] = "";
                                    tab_1_6.Visible = false;

                                    LblNewCampaign.ForeColor = System.Drawing.Color.Gray;
                                    LblNewCampaign.Font.Bold = true;

                                    LblMarketingMaterial.Font.Bold = false;
                                    LblProductUpdates.Font.Bold = false;
                                    LblbNewsLetter.Font.Bold = false;
                                    LblBanner.Font.Bold = false;
                                    LblDocumentationPdf.Font.Bold = false;

                                    break;

                                case "4":
                                    RdgNewsLetter.Rebind();
                                    RdgNewsLetter.DataBind();

                                    tab_1_4.Attributes["class"] = "tab-pane fade active in";
                                    liNewsLetter.Attributes["class"] = "active";
                                    tab_1_4.Visible = true;

                                    tab_1_1.Attributes["class"] = "tab-pane fade";
                                    liProductUpdates.Attributes["class"] = "";
                                    tab_1_1.Visible = false;
                                    tab_1_2.Attributes["class"] = "tab-pane fade";
                                    liMarketingMaterial.Attributes["class"] = "";
                                    tab_1_2.Visible = false;
                                    tab_1_3.Attributes["class"] = "tab-pane fade";
                                    liNewCampaign.Attributes["class"] = "";
                                    tab_1_3.Visible = false;
                                    tab_1_5.Attributes["class"] = "tab-pane fade";
                                    liBanner.Attributes["class"] = "";
                                    tab_1_5.Visible = false;
                                    tab_1_6.Attributes["class"] = "tab-pane fade";
                                    liDocumentationPdf.Attributes["class"] = "";
                                    tab_1_6.Visible = false;

                                    LblbNewsLetter.ForeColor = System.Drawing.Color.Gray;
                                    LblbNewsLetter.Font.Bold = true;

                                    LblMarketingMaterial.Font.Bold = false;
                                    LblNewCampaign.Font.Bold = false;
                                    LblProductUpdates.Font.Bold = false;
                                    LblBanner.Font.Bold = false;
                                    LblDocumentationPdf.Font.Bold = false;

                                    break;

                                case "5":
                                    RdgBanner.Rebind();
                                    RdgBanner.DataBind();

                                    tab_1_5.Attributes["class"] = "tab-pane fade active in";
                                    liBanner.Attributes["class"] = "active";
                                    tab_1_5.Visible = true;

                                    tab_1_3.Attributes["class"] = "tab-pane fade";
                                    liNewCampaign.Attributes["class"] = "";
                                    tab_1_3.Visible = false;
                                    tab_1_1.Attributes["class"] = "tab-pane fade";
                                    liProductUpdates.Attributes["class"] = "";
                                    tab_1_1.Visible = false;
                                    tab_1_2.Attributes["class"] = "tab-pane fade";
                                    liMarketingMaterial.Attributes["class"] = "";
                                    tab_1_2.Visible = false;
                                    tab_1_4.Attributes["class"] = "tab-pane fade";
                                    liNewsLetter.Attributes["class"] = "";
                                    tab_1_4.Visible = false;
                                    tab_1_6.Attributes["class"] = "tab-pane fade";
                                    liDocumentationPdf.Attributes["class"] = "";
                                    tab_1_6.Visible = false;

                                    LblBanner.ForeColor = System.Drawing.Color.Gray;
                                    LblBanner.Font.Bold = true;

                                    LblMarketingMaterial.Font.Bold = false;
                                    LblProductUpdates.Font.Bold = false;
                                    LblbNewsLetter.Font.Bold = false;
                                    LblNewCampaign.Font.Bold = false;
                                    LblDocumentationPdf.Font.Bold = false;

                                    break;

                                case "6":
                                    RdgDocumentationPdf.Rebind();
                                    RdgDocumentationPdf.DataBind();

                                    tab_1_6.Attributes["class"] = "tab-pane fade active in";
                                    liDocumentationPdf.Attributes["class"] = "active";
                                    tab_1_6.Visible = true;

                                    tab_1_3.Attributes["class"] = "tab-pane fade";
                                    liNewCampaign.Attributes["class"] = "";
                                    tab_1_3.Visible = false;
                                    tab_1_1.Attributes["class"] = "tab-pane fade";
                                    liProductUpdates.Attributes["class"] = "";
                                    tab_1_1.Visible = false;
                                    tab_1_2.Attributes["class"] = "tab-pane fade";
                                    liMarketingMaterial.Attributes["class"] = "";
                                    tab_1_2.Visible = false;
                                    tab_1_4.Attributes["class"] = "tab-pane fade";
                                    liNewsLetter.Attributes["class"] = "";
                                    tab_1_4.Visible = false;
                                    tab_1_5.Attributes["class"] = "tab-pane fade";
                                    liBanner.Attributes["class"] = "";
                                    tab_1_5.Visible = false;

                                    LblDocumentationPdf.ForeColor = System.Drawing.Color.Gray;
                                    LblDocumentationPdf.Font.Bold = true;

                                    LblMarketingMaterial.Font.Bold = false;
                                    LblProductUpdates.Font.Bold = false;
                                    LblbNewsLetter.Font.Bold = false;
                                    LblNewCampaign.Font.Bold = false;
                                    LblBanner.Font.Bold = false;

                                    break;
                            }

                            UpdatePanelContent.Update();
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else
                {
                    System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Close Modal Popup", "CloseConfPopUp();", true);

                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;

                    GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                }
            }
            catch (Exception ex)
            {
                LblFileUploadTitle.Text = "Delete File Warning";
                LblFileUploadfMsg.Text = "File could not be deleted. Please try again later or contact with us.";
                GlobalMethods.ShowMessageControlDA(UploadMessageAlert, LblFileUploadfMsg.Text, MessageTypes.Error, false, true, false);
                System.Web.UI.ScriptManager.RegisterStartupScript(this, GetType(), "Open Modal Popup", "OpenConfirmationPopUp();", true);

                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
            finally
            {
                session.CloseConnection();
            }
        }

        # endregion

        #region Tabs

        protected void aProductUpdates_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_1.Attributes["class"] = "tab-pane fade active in";
                    liProductUpdates.Attributes["class"] = "active";
                    tab_1_1.Visible = true;

                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    liMarketingMaterial.Attributes["class"] = "";
                    tab_1_2.Visible = false;
                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    liNewCampaign.Attributes["class"] = "";
                    tab_1_3.Visible = false;
                    tab_1_4.Attributes["class"] = "tab-pane fade";
                    liNewsLetter.Attributes["class"] = "";
                    tab_1_4.Visible = false;
                    tab_1_5.Attributes["class"] = "tab-pane fade";
                    liBanner.Attributes["class"] = "";
                    tab_1_5.Visible = false;

                    tab_1_6.Attributes["class"] = "tab-pane fade";
                    liDocumentationPdf.Attributes["class"] = "";
                    tab_1_6.Visible = false;

                    LblProductUpdates.ForeColor = System.Drawing.Color.Gray;
                    LblProductUpdates.Font.Bold = true;

                    LblMarketingMaterial.Font.Bold = false;
                    LblNewCampaign.Font.Bold = false;
                    LblbNewsLetter.Font.Bold = false;
                    LblBanner.Font.Bold = false;
                    LblDocumentationPdf.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        protected void aTabMarketingMaterial_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_2.Attributes["class"] = "tab-pane fade active in";
                    liMarketingMaterial.Attributes["class"] = "active";
                    tab_1_2.Visible = true;

                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    liProductUpdates.Attributes["class"] = "";
                    tab_1_1.Visible = false;

                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    liNewCampaign.Attributes["class"] = "";
                    tab_1_3.Visible = false;
                    tab_1_4.Attributes["class"] = "tab-pane fade";
                    liNewsLetter.Attributes["class"] = "";
                    tab_1_4.Visible = false;
                    tab_1_5.Attributes["class"] = "tab-pane fade";
                    liBanner.Attributes["class"] = "";
                    tab_1_5.Visible = false;

                    tab_1_6.Attributes["class"] = "tab-pane fade";
                    liDocumentationPdf.Attributes["class"] = "";
                    tab_1_6.Visible = false;

                    LblMarketingMaterial.ForeColor = System.Drawing.Color.Gray;
                    LblMarketingMaterial.Font.Bold = true;

                    LblProductUpdates.Font.Bold = false;
                    LblNewCampaign.Font.Bold = false;
                    LblbNewsLetter.Font.Bold = false;
                    LblBanner.Font.Bold = false;
                    LblDocumentationPdf.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        protected void aTabNewCampaign_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_3.Attributes["class"] = "tab-pane fade active in";
                    liNewCampaign.Attributes["class"] = "active";
                    tab_1_3.Visible = true;

                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    liProductUpdates.Attributes["class"] = "";
                    tab_1_1.Visible = false;

                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    liMarketingMaterial.Attributes["class"] = "";
                    tab_1_2.Visible = false;

                    tab_1_4.Attributes["class"] = "tab-pane fade";
                    liNewsLetter.Attributes["class"] = "";
                    tab_1_4.Visible = false;

                    tab_1_5.Attributes["class"] = "tab-pane fade";
                    liBanner.Attributes["class"] = "";
                    tab_1_5.Visible = false;

                    tab_1_6.Attributes["class"] = "tab-pane fade";
                    liDocumentationPdf.Attributes["class"] = "";
                    tab_1_6.Visible = false;

                    LblNewCampaign.ForeColor = System.Drawing.Color.Gray;
                    LblNewCampaign.Font.Bold = true;

                    LblMarketingMaterial.Font.Bold = false;
                    LblProductUpdates.Font.Bold = false;
                    LblbNewsLetter.Font.Bold = false;
                    LblBanner.Font.Bold = false;
                    LblDocumentationPdf.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        protected void aTabNewsLetter_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_4.Attributes["class"] = "tab-pane fade active in";
                    liNewsLetter.Attributes["class"] = "active";
                    tab_1_4.Visible = true;

                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    liProductUpdates.Attributes["class"] = "";
                    tab_1_1.Visible = false;

                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    liMarketingMaterial.Attributes["class"] = "";
                    tab_1_2.Visible = false;
                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    liNewCampaign.Attributes["class"] = "";
                    tab_1_3.Visible = false;

                    tab_1_5.Attributes["class"] = "tab-pane fade";
                    liBanner.Attributes["class"] = "";
                    tab_1_5.Visible = false;

                    tab_1_6.Attributes["class"] = "tab-pane fade";
                    liDocumentationPdf.Attributes["class"] = "";
                    tab_1_6.Visible = false;

                    LblbNewsLetter.ForeColor = System.Drawing.Color.Gray;
                    LblbNewsLetter.Font.Bold = true;

                    LblMarketingMaterial.Font.Bold = false;
                    LblNewCampaign.Font.Bold = false;
                    LblProductUpdates.Font.Bold = false;
                    LblBanner.Font.Bold = false;
                    LblDocumentationPdf.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        protected void aTabBanner_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_5.Attributes["class"] = "tab-pane fade active in";
                    liBanner.Attributes["class"] = "active";
                    tab_1_5.Visible = true;

                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    liNewCampaign.Attributes["class"] = "";
                    tab_1_3.Visible = false;

                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    liProductUpdates.Attributes["class"] = "";
                    tab_1_1.Visible = false;

                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    liMarketingMaterial.Attributes["class"] = "";
                    tab_1_2.Visible = false;

                    tab_1_4.Attributes["class"] = "tab-pane fade";
                    liNewsLetter.Attributes["class"] = "";
                    tab_1_4.Visible = false;

                    tab_1_6.Attributes["class"] = "tab-pane fade";
                    liDocumentationPdf.Attributes["class"] = "";
                    tab_1_6.Visible = false;

                    LblBanner.ForeColor = System.Drawing.Color.Gray;
                    LblBanner.Font.Bold = true;

                    LblMarketingMaterial.Font.Bold = false;
                    LblProductUpdates.Font.Bold = false;
                    LblbNewsLetter.Font.Bold = false;
                    LblNewCampaign.Font.Bold = false;
                    LblDocumentationPdf.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        protected void aTabDocumentationPdf_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    tab_1_6.Attributes["class"] = "tab-pane fade active in";
                    liDocumentationPdf.Attributes["class"] = "active";
                    tab_1_6.Visible = true;

                    tab_1_3.Attributes["class"] = "tab-pane fade";
                    liNewCampaign.Attributes["class"] = "";
                    tab_1_3.Visible = false;

                    tab_1_1.Attributes["class"] = "tab-pane fade";
                    liProductUpdates.Attributes["class"] = "";
                    tab_1_1.Visible = false;

                    tab_1_2.Attributes["class"] = "tab-pane fade";
                    liMarketingMaterial.Attributes["class"] = "";
                    tab_1_2.Visible = false;

                    tab_1_4.Attributes["class"] = "tab-pane fade";
                    liNewsLetter.Attributes["class"] = "";
                    tab_1_4.Visible = false;

                    tab_1_5.Attributes["class"] = "tab-pane fade";
                    liBanner.Attributes["class"] = "";
                    tab_1_5.Visible = false;

                    LblDocumentationPdf.ForeColor = System.Drawing.Color.Gray;
                    LblDocumentationPdf.Font.Bold = true;

                    LblMarketingMaterial.Font.Bold = false;
                    LblProductUpdates.Font.Bold = false;
                    LblbNewsLetter.Font.Bold = false;
                    LblNewCampaign.Font.Bold = false;
                    LblBanner.Font.Bold = false;

                    //GetVendorsLibraryFiles();
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

        #endregion
    }
}