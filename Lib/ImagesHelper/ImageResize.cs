using System;
using System.Linq;
using WdS.ElioPlus.Lib.DB;
using WdS.ElioPlus.Lib.Utils;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace WdS.ElioPlus.Lib.ImagesHelper
{
    public class ImageResize
    {
        private static DBSession session = new DBSession();
        private static string Url = String.Empty;

        private static Image ResizeImage(Image image, int newWidth, int newHeight)
        {
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
            }
            return newImage;
        }

        // the one and only public method
        public static bool ResizeAndReplaceImage(string fullPathName, string extension, string newFilePath, int newWidth, int newHeight)
        {
            try
            {
                bool isError = false;

                Image original = Image.FromFile(fullPathName.ToLower());

                Image resized = ResizeImage(original, newWidth, newHeight);
                Image.FromFile(fullPathName).Dispose();

                ImageFormat imageIFormat = MapFormats(extension);

                if (imageIFormat != null)
                {
                    resized.Save(newFilePath, MapFormats(extension));
                    resized.Dispose();
                }
                else
                {
                    return true;
                }

                return isError;
            }
            catch (Exception ex)
            {
                Logger.DetailedError(newFilePath, ex.Message.ToString(), ex.StackTrace.ToString());
                return true;
            }
        }

        private static ImageFormat MapFormats(string extension)
        {
            switch (extension.ToLower())
            {
                case ".gif":
                    return ImageFormat.Gif;

                case ".jpg":
                case ".jpeg":
                    return ImageFormat.Jpeg;

                case ".png":
                    return ImageFormat.Png;

                default:
                    return null;
            }
        }

        private static void UpdateFileSizeValue(string fullPath, int uID)
        {
            string fileName = Path.GetFileName(fullPath);
            if (string.IsNullOrEmpty(fileName))
                return;

            try
            {
                session.OpenConnection();
                //DataLoader<UploadedFiles> loader = new DataLoader<UploadedFiles>(session);
                //UploadedFiles file = loader.LoadSingle("SELECT * FROM UploadedFiles WHERE userId=" + uID + " AND fileName='" + fileName + "' AND isdeleted=0");
                //if (file != null)
                //{
                //    FileInfo fi = new FileInfo(fullPath);
                //    string sql = "UPDATE UploadedFiles SET fileLength=" + (int)fi.Length + " WHERE Id=" + file.Id;
                //    session.ExecuteQuery(sql);
                //    session.CloseConnection();
                //}
            }
            catch (Exception ex)
            {
                Logger.DetailedError(Url, ex.Message.ToString(), ex.StackTrace.ToString());
            }
        }

        public static bool IsImageFile(string extension)
        {
            if (extension.ToLower().Contains("gif") || extension.ToLower().Contains("jpeg") || extension.ToLower().Contains("jpg") || extension.ToLower().Contains("png"))
            {
                return true;
            }
            return false;

        }

        public static string ChangeFileName(string filename, string ext)
        {
            DateTime date = DateTime.Now;
            string expireDateFixed = string.Format("{0:d_M_yyyy}", date);
            string random = "12345";
            string newfile = "" + filename.Replace(ext, "") + "_" + expireDateFixed + "_" + random + "" + ext + "";
            return newfile;
        }

        public static Bitmap ResizeBitMapImage(Stream stream)
        {
            System.Drawing.Image originalImage = Bitmap.FromStream(stream);

            int height = 331;
            int width = 495;

            Bitmap scaledImage = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(scaledImage))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(originalImage, 0, 0, width, height);
                return scaledImage;
            }

        }
    }
}