using System;
using System.Linq;
using WdS.ElioPlus.Objects;
using WdS.ElioPlus.Lib.DB;
using Telerik.Web.UI;
using System.IO;
using WdS.ElioPlus.Lib.DBQueries;
using System.Web;
using WdS.ElioPlus.Lib.Utils;
using System.Drawing.Imaging;

namespace WdS.ElioPlus.Lib.ImagesHelper
{
    public class UpLoadImage
    {
        private static int maxFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxFileLenght"]);

        private static int maxCollaborationFileLenght = int.Parse(System.Configuration.ConfigurationManager.AppSettings["CollaborationMaxFileLenght"]);

        private static string logoTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetFolder"]).ToString();
        private static string logoTargetMultiTempFolder = (System.Configuration.ConfigurationManager.AppSettings["LogoTargetMultiTempFolder"]).ToString();
        private static string personalImageTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["PersonalImageTargetFolder"]).ToString();
        private static string subAccountPersonalImageTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["SubAccountPersonalImageTargetFolder"]).ToString();

        private static string pdfTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["PdfTargetFolder"]).ToString();
        private static int maxPdfSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxPdfSize"]);

        private static string csvTargetFolder = (System.Configuration.ConfigurationManager.AppSettings["CsvTargetFolder"]).ToString();
        private static int maxCsvSize = int.Parse(System.Configuration.ConfigurationManager.AppSettings["MaxCsvSize"]);

        public static bool UpLoadCompanyLogo(ElioUsers user, string serverMapPathTargetFolder, FileUploadedEventArgs e, DBSession session)
        {
            return UpLoadCompanyLogo(user, serverMapPathTargetFolder, e, false, session);
        }

        public static bool UpLoadCompanyLogo(ElioUsers user, string serverMapPathTargetFolder, FileUploadedEventArgs e, bool isMultiAccount, DBSession session)
        {
            bool successfullFileUpload = false;

            if (e.File.ContentLength < maxFileLenght)
            {
                if (e.File.GetExtension().ToLower() == ".jpg" || e.File.GetExtension().ToLower() == ".jpeg" || e.File.GetExtension().ToLower() == ".png" || e.File.GetExtension().ToLower() == ".gif")
                {
                    string newFileName = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                    serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                    #region Create Logo Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    if (!isMultiAccount)
                    {
                        #region Delete old files in directory if exist

                        DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                        foreach (FileInfo fi in originaldir.GetFiles())
                        {
                            fi.Delete();
                        }

                        #endregion
                    }

                    e.File.SaveAs(serverMapPathTargetFolder + newFileName);

                    if (!isMultiAccount)
                    {
                        #region Update User

                        user.CompanyLogo = logoTargetFolder + user.GuId + "/" + newFileName;

                        user = GlobalDBMethods.UpDateUser(user, session);

                        #endregion
                    }

                    successfullFileUpload = true;
                }
            }

            return successfullFileUpload;
        }

        public static bool UpLoadPersonalImage(ElioUsers user, string serverMapPathTargetFolder, FileUploadedEventArgs e, DBSession session)
        {
            bool successfullFileUpload = false;

            if (e.File.ContentLength < maxFileLenght)
            {
                if (e.File.GetExtension().ToLower() == ".jpg" || e.File.GetExtension().ToLower() == ".jpeg" || e.File.GetExtension().ToLower() == ".png")
                {
                    string newname = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                    serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                    #region Create Logo Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    e.File.SaveAs(serverMapPathTargetFolder + newname);

                    #region Update User

                    user.PersonalImage = personalImageTargetFolder + user.GuId + "/" + newname;

                    user = GlobalDBMethods.UpDateUser(user, session);

                    #endregion

                    successfullFileUpload = true;
                }
            }

            return successfullFileUpload;
        }

        public static bool UpLoadSubAccountPersonalImage(ElioUsersSubAccounts subUser, string serverMapPathTargetFolder, FileUploadedEventArgs e, DBSession session)
        {
            bool successfullFileUpload = false;

            if (e.File.ContentLength < maxFileLenght)
            {
                if (e.File.GetExtension().ToLower() == ".jpg" || e.File.GetExtension().ToLower() == ".jpeg" || e.File.GetExtension().ToLower() == ".png" || e.File.GetExtension().ToLower() == ".gif")
                {
                    string newname = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                    serverMapPathTargetFolder = serverMapPathTargetFolder + subUser.Guid + "\\";

                    #region Create Logo Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    e.File.SaveAs(serverMapPathTargetFolder + newname);

                    #region Update User

                    subUser.PersonalImage = subAccountPersonalImageTargetFolder + subUser.Guid + "/" + newname;

                    GlobalDBMethods.UpDateSubUser(subUser, session);

                    #endregion

                    successfullFileUpload = true;
                }
            }

            return successfullFileUpload;
        }

        public static bool UpLoadPdfFile(ElioUsers user, string serverMapPathTargetFolder, FileUploadedEventArgs e, DBSession session)
        {
            bool successfullFileUpload = false;

            if (e.File.ContentLength < maxPdfSize)
            {
                if (e.File.GetExtension().ToLower() == ".pdf")
                {
                    string newname = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                    serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                    #region Create File Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    e.File.SaveAs(serverMapPathTargetFolder + newname);

                    #region Update User

                    ElioUsersFiles userFiles = new ElioUsersFiles();
                    DataLoader<ElioUsersFiles> userFilesLoader = new DataLoader<ElioUsersFiles>(session);

                    ElioUsersFiles userPdfFile = Sql.GetUserPdfFile(user.Id, session);

                    if (userPdfFile == null)
                    {
                        userFiles.UserId = user.Id;
                        userFiles.FilePath = pdfTargetFolder + user.GuId + "/" + newname;
                        userFiles.SysDate = DateTime.Now;
                        userFiles.LastUpdated = DateTime.Now;
                        userFiles.IsPublic = 1;

                        userFilesLoader.Insert(userFiles);
                    }
                    else
                    {
                        userPdfFile.FilePath = pdfTargetFolder + user.GuId + "/" + newname;
                        userPdfFile.LastUpdated = DateTime.Now;

                        userFilesLoader.Update(userPdfFile);
                    }

                    #endregion

                    successfullFileUpload = true;
                }
            }

            return successfullFileUpload;
        }

        public static bool UpLoadCsvFile(ElioUsers user, string serverMapPathTargetFolder, FileUploadedEventArgs e, DBSession session)
        {
            bool successfullFileUpload = false;

            if (e.File.ContentLength < maxCsvSize)
            {
                if (e.File.GetExtension().ToLower() == ".csv")
                {
                    string newname = ImageResize.ChangeFileName(e.File.GetName(), e.File.GetExtension());

                    serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                    #region Create File Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    #region Delete old files in directory if exist

                    DirectoryInfo originaldir = new DirectoryInfo(serverMapPathTargetFolder);

                    foreach (FileInfo fi in originaldir.GetFiles())
                    {
                        fi.Delete();
                    }

                    #endregion

                    e.File.SaveAs(serverMapPathTargetFolder + newname);

                    #region Update User

                    ElioUsersFiles userFiles = new ElioUsersFiles();
                    DataLoader<ElioUsersFiles> userFilesLoader = new DataLoader<ElioUsersFiles>(session);

                    ElioUsersFiles userCsvFile = Sql.GetUserCsvFile(user.Id, session);

                    if (userCsvFile == null)
                    {
                        userFiles.UserId = user.Id;
                        userFiles.FilePath = csvTargetFolder + user.GuId + "/" + newname;
                        userFiles.SysDate = DateTime.Now;
                        userFiles.LastUpdated = DateTime.Now;
                        userFiles.IsPublic = 1;

                        userFilesLoader.Insert(userFiles);
                    }
                    else
                    {
                        userCsvFile.FilePath = csvTargetFolder + user.GuId + "/" + newname;
                        userCsvFile.LastUpdated = DateTime.Now;

                        userFilesLoader.Update(userCsvFile);
                    }

                    #endregion

                    successfullFileUpload = true;
                }
            }

            return successfullFileUpload;
        }

        public static bool UpLoadLibraryFile(string serverMapPathTargetFolder, FileUploadedEventArgs file, out string fileName, DBSession session)
        {
            fileName = file.File.FileName;

            try
            {
                //string extension = file.File.GetExtension();

                if (file.File.ContentLength < maxCollaborationFileLenght)
                {
                    fileName = Validations.ConvertLettersToEnglish(fileName);

                    #region Conflict with IE maybe and it's path

                    if (fileName.Contains('\\'))
                    {
                        string[] fileNameParts = fileName.Split('\\').ToArray();
                        fileName = fileNameParts[fileNameParts.Length - 1];
                    }

                    #endregion

                    //serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                    #region Create File Directory

                    if (!Directory.Exists(serverMapPathTargetFolder))
                        Directory.CreateDirectory(serverMapPathTargetFolder);

                    #endregion

                    file.File.InputStream.Close();
                    file.File.SaveAs(serverMapPathTargetFolder + fileName);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logger.DetailedError("UploadImage.UpLoadLibraryFile.cs", ex.Message.ToString(), ex.StackTrace.ToString());
                throw ex;
            }

            return false;
        }

        public static bool UpLoadFile(string serverMapPathTargetFolder, HttpPostedFile file, out string fileName, DBSession session)
        {
            return UpLoadFile(serverMapPathTargetFolder, file, false, out fileName, session);
        }

        public static bool UpLoadFile(string serverMapPathTargetFolder, HttpPostedFile file, bool isPreviewFile, out string fileName, DBSession session)
        {            
            string extension = Path.GetExtension(file.FileName);
            fileName = file.FileName.Replace(extension, "");

            if (file.ContentLength < maxCollaborationFileLenght)
            {
                //if (file.ContentType.ToLower() == ".pdf" || file.ContentType.ToLower() == ".csv")
                //{
                fileName = (!isPreviewFile) ? Validations.ConvertLettersToEnglish(fileName) + extension : Validations.ConvertLettersToEnglish(fileName) + "_" + Guid.NewGuid().ToString().Substring(0, 5) + extension;

                #region Conflict with IE maybe and it's path

                if (fileName.Contains('\\'))
                {
                    string[] fileNameParts = fileName.Split('\\').ToArray();
                    fileName = fileNameParts[fileNameParts.Length - 1];
                }

                #endregion

                //serverMapPathTargetFolder = serverMapPathTargetFolder + user.GuId + "\\";

                #region Create File Directory

                if (!Directory.Exists(serverMapPathTargetFolder))
                    Directory.CreateDirectory(serverMapPathTargetFolder);

                #endregion

                file.SaveAs(serverMapPathTargetFolder + fileName);

                return true;
                //}
            }

            return false;
        }

        public static bool MoveFileToFolder(string serverMapPathSourceFolder, string serverMapPathTargetFolder, string fileName)
        {
            string sourceFile = System.IO.Path.Combine(serverMapPathSourceFolder, fileName);
            string destFile = System.IO.Path.Combine(serverMapPathTargetFolder, fileName);

            if (sourceFile != "" && destFile != "")
            {
                #region Create File Directory

                if (!Directory.Exists(serverMapPathTargetFolder))
                    Directory.CreateDirectory(serverMapPathTargetFolder);

                #endregion

                File.Copy(sourceFile, destFile, true);

                return true;
            }
            else
                return false;
        }

        public static bool DeleteFileFromDirectory(string filePath, string fileName)
        {
            #region Delete file in directory if exist

            if (Directory.Exists(filePath))
            {
                File.Delete(filePath + "\\" + fileName);
                
                return true;
            }

            #endregion

            return false;
        }

        public static string GetFilenameExtensionByImageFormat(ImageFormat format)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(x => x.FormatID == format.Guid).FilenameExtension;
        }

        public static string GetFilenameExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }
    }
}