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
using System.Configuration;

namespace WdS.ElioPlus.Controls.Collaboration.UserControls
{
    public partial class SelectLibraryFiles : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        //private static string logFilePath = System.Configuration.ConfigurationManager.AppSettings["LogPath"];

        //private string _filePath;
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
            RdgViewLogs.MasterTableView.GetColumn("directory").HeaderText = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("global", "", "grid", "3", "column", "2")).Text;
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
                table.Columns.Add("directory");
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

                    string filePath = item["selected_category"].Text + "\\" + item["user_guid"].Text + "\\" + item["fileName"].Text;
                    imgBtnPreViewLogFiles.Visible = (SqlCollaboration.GetCountCollaborationUsersLibraryFilesByFilePathAndNewStatus(filePath, 1, session) > 0 && (vSession.User.GuId == item["user_guid"].Text)) ? true : false;
                    imgBtnDeleteLogFiles.Visible = (vSession.User.GuId == item["user_guid"].Text) ? true : false;
                    hpLnkPreViewLogFiles.Text = item["fileName"].Text;
                    string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                    hpLnkPreViewLogFiles.NavigateUrl = url + HttpContext.Current.Request.Url.Authority + System.Configuration.ConfigurationManager.AppSettings["ViewCollaborationLibraryFilesPath"] + "/" + item["selected_category"].Text + "/" + item["user_guid"].Text + "/" + item["fileName"].Text;
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

        private bool FileExists(string fileName)
        {
            foreach (GridDataItem item in RdgViewLogs.Items)
            {
                if (item["fileName"].Text == fileName)
                {
                    return true;
                }
            }

            return false;
        }

        protected void ViewLogs_OnNeedDataSource(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                UcMessageAlert.Visible = false;

                RdgViewLogs.Visible = true;

                if (!string.IsNullOrEmpty(SelectedCategory))
                {
                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("user_guid");
                    table.Columns.Add("directory");
                    table.Columns.Add("selected_category");
                    table.Columns.Add("company_name");
                    table.Columns.Add("fileName");
                    table.Columns.Add("fileSize");
                    table.Columns.Add("date");

                    string logedUserFilesPathTargetFolder = ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + SelectedCategory + "\\" + vSession.User.GuId + "\\";

                    if (Directory.Exists(FileHelper.AddRootToPath(logedUserFilesPathTargetFolder)))
                    {
                        DirectoryInfo filesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(logedUserFilesPathTargetFolder));

                        List<FileInfo> loggedUserFiles = filesDirectory.GetFiles().ToList();

                        if (loggedUserFiles.Count > 0)
                        {
                            for (int i = loggedUserFiles.Count - 1; i >= 0; i--)
                            {
                                if (!FileExists(loggedUserFiles[i].Name))
                                    table.Rows.Add(i, vSession.User.GuId, loggedUserFiles[i].DirectoryName, SelectedCategory, "(" + vSession.User.CompanyName + " folder)", loggedUserFiles[i].Name, loggedUserFiles[i].Length, loggedUserFiles[i].LastWriteTime.ToString("dd/MM/yyyy"));
                            }
                        }
                    }

                    if (Key != "")
                    {
                        string partnerUserFilesPathTargetFolder = System.Configuration.ConfigurationManager.AppSettings["CollaborationLibraryFullTargetFolder"].ToString() + SelectedCategory + "\\" + Key + "\\";

                        if (Directory.Exists(FileHelper.AddRootToPath(partnerUserFilesPathTargetFolder)))
                        {
                            DirectoryInfo filesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(partnerUserFilesPathTargetFolder));

                            List<FileInfo> partnerUserFiles = filesDirectory.GetFiles().ToList();

                            if (partnerUserFiles.Count > 0)
                            {
                                string partnerCompanyName = "(other folder)";

                                ElioUsers partner = Sql.GetUserByGuId(Key, session);
                                if (partner != null)
                                    partnerCompanyName = partner.CompanyName;

                                for (int i = partnerUserFiles.Count - 1; i >= 0; i--)
                                {
                                    if (!FileExists(partnerUserFiles[i].Name))
                                        table.Rows.Add(i, Key, partnerUserFiles[i].DirectoryName, SelectedCategory, "(" + partnerCompanyName + " folrder)", partnerUserFiles[i].Name, partnerUserFiles[i].Length, partnerUserFiles[i].LastWriteTime.ToString("dd/MM/yyyy"));
                                }
                            }
                        }
                    }

                    if (table.Rows.Count > 0)
                    {
                        RdgViewLogs.DataSource = table;
                    }
                    else
                    {
                        RdgViewLogs.Visible = false;
                        string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "12")).Text;

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
                session.OpenConnection();

                ImageButton imgBtnDelete = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtnDelete.NamingContainer;

                DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(FilePath));
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

                ImageButton imgBtnDelete = (ImageButton)sender;
                GridDataItem item = (GridDataItem)imgBtnDelete.NamingContainer;

                string filePath = item["selected_category"].Text + "\\" + item["user_guid"].Text + "\\" + item["fileName"].Text;

                List<ElioCollaborationUsersLibraryFiles> userFiles = SqlCollaboration.GetCollaborationUsersLibraryFilesByFilePath(filePath, session);

                foreach (ElioCollaborationUsersLibraryFiles file in userFiles)
                {
                    file.IsNew = 0;
                    file.LastUpdate = DateTime.Now;

                    DataLoader<ElioCollaborationUsersLibraryFiles> loader = new DataLoader<ElioCollaborationUsersLibraryFiles>(session);
                    loader.Update(file);
                }
                if (userFiles.Count > 0)
                {
                    //Repeater rptFilesLibrary = (Repeater)ControlFinder.FindControlBackWards(this, "RptFilesLibrary");
                    //if (rptFilesLibrary != null)
                    //    rptFilesLibrary.DataBind();
                    //RdgViewLogs.Rebind();
                    imgBtnDelete.Visible = false;
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