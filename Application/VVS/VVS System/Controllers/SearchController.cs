using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VVS_System.Models;

namespace VVS_System.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            VideoContainer vc = new VideoContainer();
            IEnumerable<Video> videos = vc.getRecommended(6);

            return View(videos);
        }

        // GET: /Search/
        public ActionResult Search(String searchKeywords)
        {
            if (searchKeywords != null && !searchKeywords.Equals(""))
            {
                VideoContainer vs = new VideoContainer();
                return View(vs.getVideos(searchKeywords));
            }

            return View("Index");
        }

        public ActionResult Find(string term)
        {
            string[] products = { "hi", "bye" };
            return Json(products, JsonRequestBehavior.AllowGet);
        }
	}
}