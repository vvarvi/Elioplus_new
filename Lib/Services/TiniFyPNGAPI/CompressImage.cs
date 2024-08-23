using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using TinyPng;

namespace WdS.ElioPlus.Lib.Services.TiniFyPNGAPI
{
    public class CompressImage
    {
        public const string API_KEY = "";

        //public static async Task Compress()
        //{
            //var source = Tinify.FromFile("unoptimized.jpg");
            //await source.ToFile("optimized.jpg");

            //Tinify.Key = API_KEY; //TinyPNG Developer API KEY
            //string sFilePath = @"d:\ButterFly.jpg";
            //string sOptimizedFile = @"d:\ButterFlyOptimized.jpg";
            //var source = Tinify.FromFile(sFilePath);
            //await source.ToFile(sOptimizedFile);

        //}

        //public async void CompressImage()
        //{
            //Tinify.Key = API_KEY; //TinyPNG Developer API KEY
            //string sFilePath = @"d:\ButterFly.jpg";
            //string sOptimizedFile = @"d:\ButterFlyOptimized.jpg";
            //var source = Tinify.FromFile(sFilePath);
            //await source.ToFile(sOptimizedFile);
        //}

        //public async void ResizeImage()
        //{
            //Tinify.Key = API_KEY; //TinyPNG Developer API KEY
            //string sFilePath = @"d:\ButterFly.jpg";
            //string sOptimizedFile = @"d:\ButterFlyResized.jpg";
            //var source = Tinify.FromFile(sFilePath);
            //var resized = source.Resize(new
            //{
            //    method = "fit",
            //    width = 250,
            //    height = 250
            //});
            //await resized.ToFile(sOptimizedFile);
        //}
    }
}