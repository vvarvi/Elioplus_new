using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WdS.ElioPlus.Lib.Utils;

namespace WdS.ElioPlus
{
    public partial class JpegImage : System.Web.UI.Page
    {
        CaptchaGenerator _captchaGenerator;

        protected void Page_Init(object sender, System.EventArgs e)
        {
            _captchaGenerator = new CaptchaGenerator();
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            string code = string.Empty;
            string urlKey = Request.QueryString["s"];
            if (!string.IsNullOrEmpty(urlKey))
            {
                code = _captchaGenerator.DencryptImageKey(urlKey);
                System.Diagnostics.Debug.WriteLine(code);
            }
            else
                code = this.Session["ImageText"].ToString();


            ImageCoding ImageObj = new ImageCoding(code, 120, 32, "Century Schoolbook");
            // Change the response headers to output a JPEG image.
            this.Response.Clear();
            this.Response.ContentType = "image/jpeg";
            this.Response.AppendHeader("Expires", "Tue, 1 Jan 1980 00:00:00 GMT");
            this.Response.AppendHeader("Cache-Control", "no-store, no-cache, must-revalidate");
            this.Response.AppendHeader("Cache-Control", " post-check=0, pre-check=0");
            this.Response.AppendHeader("Pragma", "no-cache");
            this.Response.AppendHeader("Copyright", "Peoplecert");
            this.Response.AppendHeader("Created-by", "Peoplecert");

            // Write the image to the response stream in JPEG format.
            ImageObj.Image.Save(this.Response.OutputStream, ImageFormat.Jpeg);
            this.Response.End();

            // Dispose the image object.
            ImageObj.Dispose();
        }
    }
}