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
using WdS.ElioPlus.Lib.LoadControls;
using System.Web.UI.HtmlControls;

namespace WdS.ElioPlus.Controls.Dashboard.Admin
{
    public partial class ViewAnonymousLogFiles : System.Web.UI.UserControl
    {
        private ElioSession vSession = new ElioSession();
        DBSession session = new DBSession();

        private static string logFilePath = System.Configuration.ConfigurationManager.AppSettings["LogAnonymousPath"];
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (vSession.User != null)
                {
                    UcMessageAlert.Visible = false;

                    UpdateStrings();
                }
                else
                    Response.Redirect(ControlLoader.Login, false);
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
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

                    HtmlAnchor aViewLogFilesText = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewLogFilesText");
                    HtmlAnchor aViewLogFiles = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aViewLogFiles");
                    aViewLogFiles.HRef = aViewLogFilesText.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewAnonymousLogFilesPath"] + "/" + item["fileName"].Text;
                    aViewLogFiles.Target = aViewLogFilesText.Target = "_blank";

                    Label lblViewLogFiles = (Label)ControlFinder.FindControlRecursive(item, "LblViewLogFiles");
                    lblViewLogFiles.Text = item["fileName"].Text;

                    HyperLink hpLnkBtnPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkBtnPreViewLogFiles");

                    string url = (HttpContext.Current.Request.IsLocal) ? "http://" : "https://";
                    url = url + HttpContext.Current.Request.Url.Authority + "/AnonymousLogs/" + item["fileName"].Text;

                    HyperLink hpLnkPreViewLogFiles = (HyperLink)ControlFinder.FindControlRecursive(item, "HpLnkPreViewLogFiles");
                    hpLnkPreViewLogFiles.Text = item["fileName"].Text;
                    hpLnkBtnPreViewLogFiles.NavigateUrl = hpLnkPreViewLogFiles.NavigateUrl = url;      //hpLnkPreViewLogFiles.NavigateUrl = System.Configuration.ConfigurationManager.AppSettings["ViewLogFilesPath"] + "/" + item["fileName"].Text;
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

                if (vSession.User != null)
                {
                    UcMessageAlert.Visible = false;

                    RdgViewLogs.Visible = true;

                    DataTable table = new DataTable();

                    table.Columns.Add("id");
                    table.Columns.Add("directory");
                    table.Columns.Add("fileName");
                    table.Columns.Add("fileSize");
                    table.Columns.Add("date");

                    DirectoryInfo logFilesDirectory = new DirectoryInfo(FileHelper.AddRootToPath(logFilePath));
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
                        RdgViewLogs.Visible = false;
                        string alert = Localizer.GetTranslationResults(vSession.Lang, new XElementInfo("content", "dashboard", "message", "12")).Text;

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Info, true, true, false);
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

        #endregion

        #region Buttons

        protected void ImgBtnDeleteLogFiles_OnClick(object sender, EventArgs args)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    ImageButton imgBtnDelete = (ImageButton)sender;
                    GridDataItem item = (GridDataItem)imgBtnDelete.NamingContainer;

                    DirectoryInfo filesInDirectory = new DirectoryInfo(FileHelper.AddRootToPath(logFilePath));
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

                        GlobalMethods.ShowMessageControlDA(UcMessageAlert, alert, MessageTypes.Error, true, true, false);
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

        #endregion
    }
}