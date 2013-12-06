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
       
        public ActionResult Index(int video)
        {
            int dummy = 5;

            Container vs = new Container();
            return View(vs.getVideoModel(video, dummy));
        }


        public ActionResult UpdateLikes(int video, bool isLike)
        {
            int dummy = 5;

            Container vs = new Container();
            return Json(vs.UpdateLikes(video, dummy, isLike), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddComment(int video, String comment)
        {
            int dummy = 5;

            Container vs = new Container();
            Comment c = vs.AddComment(video, dummy, comment);
            return Json(new { UserName = c.User.Name, Text = c.Text, Date = c.Date.ToString("dd/MM/yyyy HH:mm:ss") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Subscribe(int user)
        {
            int dummy = 5;

            Container vs = new Container();
            vs.Subscribe(user, dummy);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Advertisement(int video)
        {
            Container vs = new Container();

            int dummy = 5;

            //MANDAR PUBLICIDADE AKI
            VideoModel videoModel = vs.getVideoModel(video,dummy);
            videoModel.IsAdvertisement = true;
            return View("Index",videoModel);
        }

        public ActionResult SkipAds(int videoID)
        {
            return RedirectToAction("Index", new {video = videoID});
        }
	}
}