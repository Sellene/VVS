using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using VVS_System.Models;
namespace VVS_System.Controllers
{
    public class UploadController : Controller
    {
        private const string TempPath = @"C:\Temp";
        private static Video videoGlobal;

        public Container Container = Container.GetContainer();

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
                videoGlobal.VideoPath = "..//Content//Videos//" + file.FileName;
                System.IO.File.WriteAllBytes(videoPath, ReadData(file.InputStream));

                //Gerar Thumbnail
                MediaPlayer player = new MediaPlayer { Volume = 0, ScrubbingEnabled = true };
                
                while (player.NaturalVideoHeight == 0)
                {
                    player.Open(new Uri(videoPath));
                    player.Pause(); 
                }
                
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

                imagePath = Path.ChangeExtension(Server.MapPath("..\\Content\\Videos\\Posters") + "\\" + file.FileName,"png");
                videoGlobal.PosterPath = Path.ChangeExtension("..//Content//Videos//Posters//" + file.FileName, "png");

                using (FileStream myFile = new FileStream(imagePath, FileMode.Create, FileAccess.Write))
                {
                    ms.WriteTo(myFile);
                }

                player.Close();
            }

            return Json("All files have been successfully stored.");
        }

        public ActionResult SendInfo(Video videoInfo)
        {
            int dummy = 5;
            
            videoGlobal.Name = videoInfo.Name;
            videoGlobal.IsPrivate = videoInfo.IsPrivate;
            videoGlobal.AllowComments = videoInfo.AllowComments;
            videoGlobal.Owner = Container.GetUser(dummy);

            Container.AddVideo(videoGlobal);

            return RedirectToAction("Index", "VideoVisualization", new {video = videoGlobal.ID});
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