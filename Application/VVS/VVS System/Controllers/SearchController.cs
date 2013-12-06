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
        public Container Container = Container.GetContainer();
        public ActionResult Index()
        {
            return View(Container.GetVideosModel(null));
        }

        // GET: /Search/
        public ActionResult Search(String searchKeywords)
        {
            if (searchKeywords != null && !searchKeywords.Equals(""))
            {
                return View(Container.GetVideosModel(searchKeywords));
            }

            return View("Index");
        }

        public ActionResult Find(string term)
        {
            string[] products = Container.GetVideosNames(term);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
	}
}