using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VVS_System.Models;

namespace VVS_System.Controllers
{
    public class SearchChannelController : Controller
    {
        //
        // GET: /Search/
        public ActionResult Index()
        {
            Container vc = new Container();
            IEnumerable<UserModel> users = vc.getUserModel(null);

            return View(videos);
        }

        // GET: /Search/
        public ActionResult Search(String searchKeywords)
        {
            if (searchKeywords != null && !searchKeywords.Equals(""))
            {
                Container vs = new Container();
                return View(vs.getVideosModel(searchKeywords));
            }

            return View("Index");
        }

        public ActionResult Find(string term)
        {
            Container vs = new Container();
            string[] products = vs.getVideosNames(term);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
	}
}