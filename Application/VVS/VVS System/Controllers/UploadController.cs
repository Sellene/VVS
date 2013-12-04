using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace VVS_System.Controllers
{
    public class UploadController : Controller
    {
        private const string TempPath = @"C:\Temp";

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult UploadFiles(HttpPostedFileBase file)
        {
            //foreach (HttpPostedFileBase file in files)
            //{
                string path  = Server.MapPath("..\\Content\\Videos") + "\\" + file.FileName;
                System.IO.File.WriteAllBytes(path, ReadData(file.InputStream));
            //}

            return Json("All files have been successfully stored.");
        }

        private byte[] ReadData(Stream stream)
        {
            byte[] buffer = new byte[16 * 1024];

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