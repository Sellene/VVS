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
            int id = -1;

            if (User.Identity.IsAuthenticated)
            {
                id = Container.GetUser(User.Identity.Name).ID;
            }

            VideoModel vm = Container.GetVideoModel(video, id);
            vm.Video.Visualizations++;
            return View(vm);
        }


        public ActionResult UpdateLikes(int video, bool isLike)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new{url = Url.Action("Login", "Account")}, JsonRequestBehavior.AllowGet);
            }

            return Json(Container.UpdateLikes(video, Container.GetUser(User.Identity.Name).ID, isLike), JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddComment(int video, String comment)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }
            
            Comment c = Container.AddComment(video, Container.GetUser(User.Identity.Name).ID, comment);
            return Json(new { UserName = c.User.Name, Text = c.Text, Date = c.Date.ToString("dd/MM/yyyy HH:mm:ss") }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Subscribe(int user)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }
            
            Container.Subscribe(user, Container.GetUser(User.Identity.Name).ID);
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public ActionResult Favourite(int video)
        {
            if (!User.Identity.IsAuthenticated)
            {
                return Json(new { url = Url.Action("Login", "Account") }, JsonRequestBehavior.AllowGet);
            }
            
            Container.AddVideoToFavourites(video, Container.GetUser(User.Identity.Name).ID);
            return Json("hi", JsonRequestBehavior.AllowGet);
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