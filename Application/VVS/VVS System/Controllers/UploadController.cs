using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DexterLib;
using VVS_System.Models;

namespace VVS_System.Controllers
{
    public class UploadController : Controller
    {
        private const string TempPath = @"C:\Temp";
        private static Video videoGlobal;

        public ActionResult Index()
        {
            videoGlobal = new Video();
            return View();
        }
        
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            string videoPath = "";
            string imagePath = "";

            if (file.ContentType.Contains("video"))
            {
                videoPath = Server.MapPath("..\\Content\\Videos") + "\\" + file.FileName;
                videoGlobal.VideoPath = videoPath;
                System.IO.File.WriteAllBytes(videoPath, ReadData(file.InputStream));

                //Gerar Thumbnail
                MediaPlayer player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };
                player.Open(new Uri(videoPath));
                player.Pause();
                int videoHeight = player.NaturalVideoHeight;
                int videoWidth = player.NaturalVideoWidth;
                Duration videoDuration = player.NaturalDuration;
                
                int videoLength = 30;

                if (videoDuration.HasTimeSpan)
                {
                    videoLength = (int)videoDuration.TimeSpan.TotalSeconds;
                }

                Random rnd = new Random();
                int frameSecond = rnd.Next(1, videoLength);

                player.Position = TimeSpan.FromSeconds(frameSecond);
                //We need to give MediaPlayer some time to load. 
                //The efficiency of the MediaPlayer depends                 
                //upon the capabilities of the machine it is running on and 
                //would be different from time to time
                System.Threading.Thread.Sleep(1 * 1000);

                RenderTargetBitmap rtb = new RenderTargetBitmap(videoWidth, videoHeight, 96, 96, PixelFormats.Pbgra32);
                DrawingVisual dv = new DrawingVisual();
                using (DrawingContext dc = dv.RenderOpen())
                {
                    dc.DrawVideo(player, new Rect(0, 0, videoWidth, videoHeight));
                }
                rtb.Render(dv);

                BitmapFrame frame = BitmapFrame.Create(rtb).GetCurrentValueAsFrozen() as BitmapFrame;
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(frame as BitmapFrame);
                MemoryStream ms = new MemoryStream();
                encoder.Save(ms);

                imagePath = Path.ChangeExtension(Server.MapPath("..\\Content\\Videos\\Posters") + "\\" + file.FileName,
                    "png");//Server.MapPath("..\\Content\\Videos\\Posters") + "\\";
                using (FileStream myFile = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(myFile);
                }

                player.Close();
            }

            if (file.ContentType.Contains("image"))
            {
                imagePath = Path.ChangeExtension(Server.MapPath("..\\Content\\Videos\\Posters") + "\\" + file.FileName, "png");
                videoGlobal.PosterPath = imagePath;
                System.IO.File.WriteAllBytes(imagePath, ReadData(file.InputStream));
            }

            
            

            //try
            //{
            //    MediaDetClass loMD = new MediaDetClass();
            //    loMD.Filename = Server.MapPath("..\\Content\\Videos") + "\\" + "IG1.mp4";
            //    loMD.CurrentStream = 0; 
            //    loMD.WriteBitmapBits(0, 150, 150, imagePath);

            //    System.Drawing.Image loImg = System.Drawing.Image.FromFile(imagePath);
            //    loImg.Save(imagePath, ImageFormat.Png);
            //    loImg.Dispose();
            //    System.IO.File.Delete(imagePath);
            //}
            //catch (Exception loEx)
            //{
            //    // Means media not supported
            //}



            //// create instance of video reader
            //VideoFileReader reader = new VideoFileReader();
            //// open video file
            //reader.Open(videoPath);
            ////// check some of its attributes
            ////Console.WriteLine("width:  " + reader.Width);
            ////Console.WriteLine("height: " + reader.Height);
            ////Console.WriteLine("fps:    " + reader.FrameRate);
            ////Console.WriteLine("codec:  " + reader.CodecName);
            //// read 100 video frames out of it
            //for (int i = 0; i < 100; i++)
            //{
            //    Bitmap videoFrame = reader.ReadVideoFrame();
            //    videoFrame.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

            //    // dispose the frame when it is no longer required
            //    videoFrame.Dispose();
            //}
            //reader.Close();


            //// Getting Frame From Video only not storing.
            //    Bitmap bmp = FrameGrabber.GetFrameFromVideo(videoPath, 0.2d);
            //    FrameGrabber.SaveFrameFromVideo(videoPath, 0.2d, imagePath);
            //    bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Gif);
                

           
            
            ////Storing return bmp from FrameGrabber Class.
            //bmp.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

            //// You get the frame and can save also this frame to specified location like which require the path of video and the image path.
            //// Path of the video and frame storing path
            //string videopath = Server.MapPath("~/Video/search.AVI");
            //string _imagepath = Server.MapPath("~/Frames/CropedImage.jpg");
            //FrameGrabber.SaveFrameFromVideo(_videopath, 0.2d, _imagepath);
            //bmp.Save(_imagepath, System.Drawing.Imaging.ImageFormat.Gif);

            return Json("All files have been successfully stored.");
        }

        public ActionResult SendInfo(Video videoInfo)
        {
            videoGlobal.Name = videoInfo.Name;
            videoGlobal.IsPrivate = videoInfo.IsPrivate;
            videoGlobal.AllowComments = videoInfo.AllowComments;

            //TO REGISTER ON CONTAINER

            return View("Index");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16*1024];

            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }

                return ms.ToArray();
            }
        }
    }
}