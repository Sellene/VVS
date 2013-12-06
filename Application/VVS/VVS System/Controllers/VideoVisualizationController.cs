using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using VVS_System.Models;

namespace VVS_System.Controllers
{
    public class VideoVisualizationController : Controller
    {
       
        // GET: /VideoVisualition/
        public ActionResult Index(int video)
        {
            Container vs = new Container();
            return View(vs.getVideoModel(video));
        }

        public ActionResult Index2(int video)
        {
            Container vs = new Container();

            //MANDAR PUBLICIDADE AKI
            VideoModel videoModel = vs.getVideoModel(video);
            videoModel.IsAdvertisement = true;
            return View(videoModel);
        }

        public ActionResult SkipAds(Object sender, EventArgs e)
        {
            return RedirectToAction("Index", new {video = 1});
        }
	}
}