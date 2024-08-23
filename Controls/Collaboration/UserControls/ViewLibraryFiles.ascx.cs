using System;
using System.Linq;
using WdS.ElioPlus.Lib.Utils;
using System.Data;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.Localization;
using Telerik.Web.UI;
using System.IO;
using System.Collections.Generic;
using WdS.ElioPlus.Lib.Enums;
using System.Web.UI.WebControls;
using System.Web;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI;

namespace WdS.ElioPlus.Controls.Collaboration.UserControls
{
    public partial class ViewLibraryFiles : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        public string FilePath
        {
            get
            {
                string result = string.Empty;

                if (ViewState["FilePath"] != null)
                {
                    result = (string)ViewState["FilePath"];
                }

                return result;
            }
            set
            {
                ViewState["FilePath"] = value;
            }
        }

        public string SelectedCategory
        {
            get
            {
                string result = string.Empty;

                if (ViewState["SelectedCategory"] != null)
                {
                    result = (string)ViewState["SelectedCategory"];
                }

                return result;
            }
            set
            {
                ViewState["SelectedCategory"] = value;
            }
        }

        public int SelectedCategoryID
        {
            get
            {
                int result = -1;

                if (ViewState["SelectedCategoryID"] != null)
                {
                    result = (int)ViewState["SelectedCategoryID"];
                }

                return result;
            }
            set
            {
                ViewState["SelectedCategoryID"] = value;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                ElioUsers user = null;
                bool isError = false;
                string errorPage = string.Empty;
                string key = string.Empty;

                RequestPaths attr = new RequestPaths(HttpContext.Current.Request.Url.AbsolutePath, ref user, ref isError, ref errorPage, out key, session);

                Key = key;

                UcMessageAlert.Visible = false;

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

        #region Methods

        private void UpdateStrings()
        {
            //LblViewLogFiles.Text = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "label", "14")).Text;

            RdgViewLogs.MasterTableView.GetColumn("id").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "1")).Text;
            RdgViewLogs.MasterTableView.GetColumn("file_path").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "2")).Text;
            RdgViewLogs.MasterTableView.GetColumn("fileName").HeaderText = RdgViewLogs.MasterTableView.GetColumn("fileName_Temp").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "3")).Text;
            RdgViewLogs.MasterTableView.GetColumn("fileSize").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "4")).Text;
            RdgViewLogs.MasterTableView.GetColumn("date").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "5")).Text;
            RdgViewLogs.MasterTableView.GetColumn("actions").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "6")).Text;
        }

        public DataTable FillGrid()
        {
            DataTable table = new DataTable();

            if (!string.IsNullOrEmpty(FilePath))
            {
                table.Columns.Add("id");
                table.Columns.Add("file_path");
                table.Columns.Add("fileName");
                table.Columns.Add("fileSize");
                table.Columns.Add("date");

                DirectoryInfo logFilesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(FilePath));
                List<FileInfo> files = logFilesDirectory.GetFiles().ToList();

                if (files.Count > 0)
                {
                    for (int i = files.Count - 1; i >= 0; i--)
                    {
                        table.Rows.Add(i, files[i].DirectoryName, files[i].Name, files[i].Length, files[i].LastWriteTime.ToString("dd/MM/yyyy"));
                    }

                    RdgViewLogs.DataSource = table;
                }
                else
                {
                    table = null;
                    RdgViewLogs.Visible = false;
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "12")).Text;

                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
                }
            }

            return table;
        }

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

        #endregion

        #region Grids

        protected void ViewLogs_ItemCreated(object sender, GridItemEventArgs e)
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

        protected void ViewLogs_OnItemDataBound(object sender, GridItemEventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (e.Item is GridDataItem)
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

        protected void ViewLogs_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                RdgViewLogs.Visible = true;

                if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategoryID > -1)
                {
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
                        List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(vSession.User.Id, null, SelectedCategoryID, "0,1", false, session);      //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(vSession.User.Id, SelectedCategoryID, 1, false, session);

                        if (userFiles.Count > 0)
                        {
                            foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                            {
                                if (!FileExists(table, file.FileName))
                                    table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, SelectedCategory, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
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
                            List<ElioCollaborationUsersLibraryFiles> partnerUserFiles = SqlCollaboration.GetFilesSendByUserSendToUserAndUsers(partner.Id, vSession.User.Id, SelectedCategoryID, "0,1", true, session);         //SqlCollaboration.GetUserCollaborationLibraryFilesUploadedByCategoryId(partner.Id, SelectedCategoryID, 1, true, session);

                            if (partnerUserFiles.Count > 0)
                            {
                                foreach (ElioCollaborationUsersLibraryFiles file in partnerUserFiles)
                                {
                                    if (!FileExists(table, file.FileName))
                                        table.Rows.Add(file.Id, file.UserId, file.UploadedByUserId, vSession.User.GuId, file.FilePath, SelectedCategory, vSession.User.CompanyName, (file.FileType.Length >= 15) ? file.FileType.Substring(0, 15) : file.FileType, file.FileName, "file-size", file.DateCreated.ToString("dd/MM/yyyy"), file.IsNew, file.CollaborationGroupId);
                                }
                            }
                        }
                    }

                    if (table.Rows.Count > 0)
                    {
                        RdgViewLogs.Visible = true;
                        RdgViewLogs.DataSource = table;

                        LblViewSelectedCategoryFiles.Text = SelectedCategory;
                    }
                    else
                    {
                        RdgViewLogs.Visible = false;
                        LblViewSelectedCategoryFiles.Text = "";
                        string alert = "There are no files uploaded in this library";  //Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "12")).Text;

                        GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
                    }
                }
                else
                {
                    RdgViewLogs.Visible = false;
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "12")).Text;

                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
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

        protected void ImgBtnDeleteLogFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                ImageButton imgBtnDelete = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtnDelete.NamingContainer;

                DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + SelectedCategory + "\\" + vSession.User.GuId + "\\"));
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
                        session.OpenConnection();
                        session.BeginTransaction();

                        SqlCollaboration.DeleteOrUpdateUserCollaborationLibraryFileAndBlobById(vSession.User, Convert.ToInt32(item["id"].Text), false, false, session);

                        session.CommitTransaction();
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

                    ((DashboardCollaborationLibrary)this.Page).GetLibraryFiles(session);

                    RdgViewLogs.Rebind();
                }
                else
                {
                    string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "11")).Text;

                    GlobalMethods.ShowMessageControl(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

                ((DashboardCollaborationLibrary)this.Page).GetLibraryFiles(session);
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