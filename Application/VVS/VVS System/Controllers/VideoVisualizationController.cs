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

        public Container Container = Container.GetContainer();


        public ActionResult Index(int video)
        {
            int dummy = 5;
            VideoModel vm = Container.GetVideoModel(video, dummy);
            vm.Video.Visualizations++;
            return View(vm);
        }


        public ActionResult UpdateLikes(int video, bool isLike)
        {
            int dummy = 5;

            return Json(Container.UpdateLikes(video, dummy, isLike), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddComment(int video, String comment)
        {
            int dummy = 5;

            Comment c = Container.AddComment(video, dummy, comment);
            return Json(new { UserName = c.User.Name, Text = c.Text, Date = c.Date.ToString("dd/MM/yyyy HH:mm:ss") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Subscribe(int user)
        {
            int dummy = 5;

            Container.Subscribe(user, dummy);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Advertisement(int video)
        {
            return View("Index",Container.GetAdvertisement(video));
        }

        public ActionResult SkipAds(int videoID)
        {
            return RedirectToAction("Index", new {video = videoID});
        }
	}
}