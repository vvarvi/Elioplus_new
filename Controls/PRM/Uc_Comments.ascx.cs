using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using WdS.ElioPlus.Controls.AlertControls;
using WdS.ElioPlus.Lib;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.DBQueries;
using WdS.ElioPlus.Lib.EmailNotificationSender;
using WdS.ElioPlus.Lib.Enums;
using WdS.ElioPlus.Lib.ImagesHelper;
using WdS.ElioPlus.Lib.LoadControls;
using WdS.ElioPlus.Lib.Localization;
using WdS.ElioPlus.Lib.Utils;
using WdS.ElioPlus.Objects;

namespace WdS.ElioPlus.Controls.PRM
{
    public partial class Uc_Comments : System.Web.UI.UserControl
    {
        DBSession session = new DBSession();
        ElioSession vSession = new ElioSession();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    //scriptManager.RegisterPostBackControl(RBtnBackVendor);                    

                    if (Request.QueryString["dealViewID"] == null)
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);

                    if (!IsPostBack)
                    {
                        if (Request.QueryString["dealViewID"] != null)
                        {
                            int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                            if (dealId > 0)
                            {
                                FixPage();
                            }
                            else
                                Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
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

        #region Methods

        private void FixPage()
        {
            TbxMessage.Text = "";
            UcRgd1.Visible = MessageCommentControl.Visible = false;

            if (vSession.User.CompanyType == EnumHelper.GetDescription(Types.Resellers).ToString())
            {
                
            }else
            {

            }

            UpdateStrings();
            SetLinks();

            if (vSession.User.BillingType != Convert.ToInt32(BillingTypePacket.FreemiumPacketType))
            {

            }
            else
            {

            }
        }

        private void SetLinks()
        {
            //aBtnGoFull.HRef = vSession.User.UserRegisterType == (int)UserRegisterType.ElioPlusRegisterType ? ControlLoader.FullRegistrationPage : ControlLoader.FullRegistrationPrmPage;
        }

        private void UpdateStrings()
        {
            
        }

        private void BuildRepeaterItem(RepeaterItemEventArgs args)
        {
            if (session.Connection.State == ConnectionState.Closed)
                session.OpenConnection();

            if (vSession.User == null)
            {
                Response.Redirect(ControlLoader.Login, false);
                return;
            }

            RepeaterItem item = (RepeaterItem)args.Item;
            if (item != null)
            {
                DataRowView row = (DataRowView)item.DataItem;
                if (row != null)
                {
                    HtmlGenericControl spanIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanIsNew");
                    HtmlGenericControl iIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "iIsNew");
                    HtmlAnchor aIsRead = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aIsRead");
                    HtmlGenericControl divDaysPast = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divDaysPast");
                    HtmlAnchor aPersonLogo = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPersonLogo");
                    HtmlAnchor aPersonImage = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aPersonImage");
                    HtmlAnchor aDealOwnerName = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aDealOwnerName");

                    Label lblDaysPast = (Label)ControlFinder.FindControlRecursive(item, "LblDaysPast");

                    string companyName = Regex.Replace(row["company_name"].ToString(), @"[^A-Za-z0-9]+", "-").Trim().ToLower();
                    
                    if (companyName.EndsWith("-"))
                        companyName = companyName.Substring(0, companyName.Length - 1);

                    if (row["company_type"].ToString() == Types.Vendors.ToString())
                        aPersonImage.HRef = aPersonLogo.HRef = "/profiles/vendors/" + row["user_id"].ToString() + "/" + companyName;
                    else
                        aPersonImage.HRef = aPersonLogo.HRef = "/profiles/channel-partners/" + row["user_id"].ToString() + "/" + companyName;

                    aPersonImage.Target = aPersonLogo.Target = "_blank";

                    string ownerName = Regex.Replace(row["owner_company_name"].ToString(), @"[^A-Za-z0-9]+", "-").Trim().ToLower();

                    if (ownerName.EndsWith("-"))
                        ownerName = ownerName.Substring(0, ownerName.Length - 1);

                    if (row["company_type"].ToString() == Types.Vendors.ToString())
                        aDealOwnerName.HRef = "/profiles/vendors/" + row["vendor_id"].ToString() + "/" + ownerName;
                    else
                        aDealOwnerName.HRef = "/profiles/channel-partners/" + row["vendor_id"].ToString() + "/" + ownerName;

                    aDealOwnerName.Target = "_blank";

                    if (!string.IsNullOrEmpty(row["is_new"].ToString()))
                    {
                        aIsRead.Visible = row["is_new"].ToString() == "1";
                        iIsNew.Attributes["class"] = (row["is_new"].ToString() == "1") ? "flaticon-star icon-1x text-warning" : "flaticon-star icon-1x text-light-warning";
                        spanIsNew.Attributes["title"] = (row["is_new"].ToString() == "1") ? "is new comment" : "is not new comment";
                    }

                    if (!string.IsNullOrEmpty(row["days_past"].ToString()))
                    {
                        if (Convert.ToInt32(row["days_past"].ToString()) > 0)
                        {
                            divDaysPast.Visible = true;
                            lblDaysPast.Text = Convert.ToInt32(row["days_past"].ToString()) == 1 ? row["days_past"].ToString() + " day ago" : row["days_past"].ToString() + " days ago";
                        }
                    }

                    HtmlGenericControl divUploadFileArea = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "divUploadFileArea");
                    HtmlAnchor aCommentFile = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aCommentFile");
                    Label lblPreViewCommentFile = (Label)ControlFinder.FindControlRecursive(item, "LblPreViewCommentFile");

                    if (!string.IsNullOrEmpty(row["file_path"].ToString()))
                    {
                        divUploadFileArea.Visible = true;
                        aCommentFile.HRef = System.Configuration.ConfigurationManager.AppSettings["ViewCommentsLibraryFilesPath"] + "/" + row["file_path"].ToString();

                        lblPreViewCommentFile.Text = (!string.IsNullOrEmpty(row["file_name"].ToString())) ? row["file_name"].ToString() : "comment file";

                        HtmlGenericControl spanCommentFileIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCommentFileIsNew");

                        spanCommentFileIsNew.Attributes["class"] = Convert.ToInt32(row["is_file_new"].ToString()) == 1 ? "flaticon2-clip-symbol text-warning icon-1x mr-2" : "flaticon2-clip-symbol text-light-warning icon-1x mr-2";
                    }
                }
            }
        }

        private void LoadGridData()
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    if (Request.QueryString["dealViewID"] != null)
                    {
                        int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                        if (dealId > 0)
                        {
                            DataTable table = Sql.GetDealCommentsTbl(dealId, session);

                            if (table.Rows.Count > 0)
                            {
                                Rdg1.Visible = true;
                                UcRgd1.Visible = false;

                                Rdg1.Visible = true;
                                Rdg1.DataSource = table;
                                Rdg1.DataBind();
                            }
                            else
                            {
                                Rdg1.Visible = false;
                                UcRgd1.Visible = true;
                                GlobalMethods.ShowMessageControl(UcRgd1, "There are no comments", MessageTypes.Info, true, true, true, true, false);
                            }
                        }
                        else
                        {
                            Rdg1.Visible = false;
                            GlobalMethods.ShowMessageControl(UcRgd1, "There are no comments", MessageTypes.Info, true, true, true, true, false);
                        }
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

        #region Grids

        protected void Rdg1_ItemDataBound(object sender, RepeaterItemEventArgs args)
        {
            try
            {
                if (args.Item.ItemType == ListItemType.Item || args.Item.ItemType == ListItemType.AlternatingItem)
                {
                    RepeaterItem item = (RepeaterItem)args.Item;
                    if (item != null)
                    {
                        BuildRepeaterItem(args);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void Rdg1_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    LoadGridData();
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        #endregion

        #region Buttons

        protected void aSaveComment_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                if (vSession.User != null)
                {
                    MessageCommentControl.Visible = false;
                    string uploadFileMessage = "";

                    if (Request.QueryString["dealViewID"] != null)
                    {
                        int dealId = Convert.ToInt32(Session[Request.QueryString["dealViewID"]]);
                        if (dealId > 0)
                        {
                            #region Check Data

                            if (TbxMessage.Text == "")
                            {
                                GlobalMethods.ShowMessageControl(MessageCommentControl, "Please type a comment first", MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            var fileContentRes = inputFile.PostedFile;
                            int maxFileSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CommentsMaxFileLenght"]);

                            if (fileContentRes != null && fileContentRes.ContentLength > maxFileSize)
                            {
                                GlobalMethods.ShowMessageControl(MessageCommentControl, string.Format("Your file size is outside the bounds. Please try smaller file size than {0} KB to send or contact with us.", maxFileSize), MessageTypes.Error, true, true, true, true, false);
                                return;
                            }

                            #endregion

                            #region Add New Comment

                            ElioRegistrationComments comment = new ElioRegistrationComments();

                            comment.UserId = vSession.User.Id;
                            comment.Description = TbxMessage.Text;
                            comment.SysDate = DateTime.Now;
                            comment.LastUpdate = DateTime.Now;
                            comment.IsPublic = 1;
                            comment.IsNew = 1;

                            DataLoader<ElioRegistrationComments> loader = new DataLoader<ElioRegistrationComments>(session);
                            loader.Insert(comment);

                            ElioRegistrationDealsComments dealComment = new ElioRegistrationDealsComments();

                            dealComment.DealId = dealId;
                            dealComment.CommentId = comment.Id;

                            DataLoader<ElioRegistrationDealsComments> loaderDealComm = new DataLoader<ElioRegistrationDealsComments>(session);
                            loaderDealComm.Insert(dealComment);

                            #endregion

                            if (fileContentRes != null && fileContentRes.ContentLength > 0)
                            {
                                #region Add File To Comment

                                try
                                {
                                    string fileName = fileContentRes.FileName;

                                    #region Upload File to Path

                                    string serverMapPathTargetFolderToCopy = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings["CommentsLibraryTargetFolder"].ToString()) + "\\" + vSession.User.GuId + "\\";
                                    string extension = Path.GetExtension(fileContentRes.FileName);

                                    bool successUpload = UpLoadImage.UpLoadFile(serverMapPathTargetFolderToCopy, fileContentRes, out fileName, session);

                                    #endregion

                                    if (successUpload)
                                    {
                                        #region Add Comment File

                                        ElioRegistrationCommentsUsersFiles file = new ElioRegistrationCommentsUsersFiles();

                                        file.UserId = vSession.User.Id;
                                        file.CommentId = comment.Id;
                                        file.FileName = fileName;
                                        file.FileTitle = "Comment File to deal: " + dealId;
                                        file.FilePath = vSession.User.GuId + "/" + fileName;
                                        file.FileType = fileContentRes.ContentType;
                                        file.IsPublic = 1;
                                        file.IsNew = 1;
                                        file.DateCreated = DateTime.Now;
                                        file.LastUpdate = DateTime.Now;

                                        DataLoader<ElioRegistrationCommentsUsersFiles> loaderFile = new DataLoader<ElioRegistrationCommentsUsersFiles>(session);
                                        loaderFile.Insert(file);

                                        #endregion

                                        #region Add Comment Blob File

                                        ElioRegistrationCommentsBlobFiles blobFile = new ElioRegistrationCommentsBlobFiles();

                                        blobFile.CommentFilesId = file.Id;
                                        blobFile.FileName = file.FileName;
                                        blobFile.FilePath = serverMapPathTargetFolderToCopy;
                                        blobFile.FileSize = fileContentRes.ContentLength;
                                        blobFile.FileType = fileContentRes.ContentType;
                                        blobFile.IsPublic = 1;
                                        blobFile.DateCreated = DateTime.Now;
                                        blobFile.LastUpdate = DateTime.Now;
                                        blobFile.BlobFile = GlobalMethods.ConvertFileToBinary(serverMapPathTargetFolderToCopy + "/" + blobFile.FileName);

                                        DataLoader<ElioRegistrationCommentsBlobFiles> blbLoader = new DataLoader<ElioRegistrationCommentsBlobFiles>(session);
                                        blbLoader.Insert(blobFile);

                                        #endregion
                                    }
                                    else
                                    {
                                        throw new Exception("File size to big to be uploaded");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("File in comment with ID {0} could not be saved at {1} by user ID {2}.", comment.Id, DateTime.Now, vSession.User.Id), ex.StackTrace.ToString());
                                    uploadFileMessage = " Unfortunately, file could not be saved. Please try again or contact with us.";
                                }

                                #endregion
                            }

                            try
                            {
                                DataTable table = Sql.GetDealResellerEmailAndDealCompanyNameByDealId(dealComment.DealId, vSession.User.CompanyType, session);
                                if (table != null)
                                    EmailSenderLib.SendNewDealRegistrationCommentEmail(table.Rows[0]["email"].ToString(), table.Rows[0]["company_name"].ToString(), vSession.Lang, session);
                                else
                                    Logger.DetailedError(Request.Url.ToString(), string.Format("User with ID {0}, uploaded a new comment for deal {1} at {2}, but no receiver email or deal company name was found to send notification email to", vSession.User.Id.ToString(), dealComment.DealId, DateTime.Now.ToString()), "Uc_Comments.ascx --> ERROR sending deal comment notification Email");
                            }
                            catch (Exception ex)
                            {
                                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
                            }

                            GlobalMethods.ShowMessageControl(MessageCommentControl, "Your comment saved successfully!" + uploadFileMessage, MessageTypes.Success, true, true, true, true, false);

                            TbxMessage.Text = "";
                            inputFile = null;

                            LoadGridData();

                            UpdatePanel4.Update();
                        }
                        else
                            Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
                    }
                    else
                        Response.Redirect(ControlLoader.Dashboard(vSession.User, "deal-registration"), false);
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

        protected void aCancelComment_ServerClick(object sender, EventArgs e)
        {
            try
            {
                TbxMessage.Text = "";
                inputFile = null;

                MessageCommentControl.Visible = false;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Request.Url.ToString(), ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        protected void aIsRead_ServerClick(object sender, EventArgs e)
        {
            try
            {
                session.OpenConnection();

                HtmlAnchor imgBtn = (HtmlAnchor)sender;
                if (imgBtn != null)
                {
                    RepeaterItem item = (RepeaterItem)imgBtn.NamingContainer;
                    if (item != null)
                    {
                        HiddenField hdnId = (HiddenField)ControlFinder.FindControlRecursive(item, "HdnCommentId");
                        if (hdnId != null)
                        {
                            int commentId = Convert.ToInt32(hdnId.Value);
                            if (commentId > 0)
                            {
                                session.ExecuteQuery("Update Elio_registration_comments SET is_new = 0, last_update = getdate() WHERE id = @id"
                                        , DatabaseHelper.CreateIntParameter("@id", commentId));

                                session.ExecuteQuery("Update Elio_registration_comments_users_files SET is_new = 0, last_update = getdate() WHERE comment_id = @comment_id"
                                        , DatabaseHelper.CreateIntParameter("@comment_id", commentId));

                                HtmlAnchor aIsRead = (HtmlAnchor)ControlFinder.FindControlRecursive(item, "aIsRead");
                                HtmlGenericControl iIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "iIsNew");
                                HtmlGenericControl spanIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanIsNew");
                                HtmlGenericControl spanCommentFileIsNew = (HtmlGenericControl)ControlFinder.FindControlRecursive(item, "spanCommentFileIsNew");

                                if (aIsRead != null)
                                {
                                    aIsRead.Visible = false;
                                    iIsNew.Attributes["class"] = "flaticon-star icon-1x text-light-warning";
                                    spanCommentFileIsNew.Attributes["class"] = "flaticon2-clip-symbol text-light-warning icon-1x mr-2";
                                    spanIsNew.Attributes["title"] = "is not new comment";

                                    UpdatePanel4.Update();
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

        #endregion
    }
}