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
        public Container Container = Container.GetContainer();
        public ActionResult Index()
        {
            return View(Container.GetUserModel(null));
        }

        // GET: /Search/
        public ActionResult Search(String searchKeywords)
        {
            if (searchKeywords != null && !searchKeywords.Equals(""))
            {
                return View(Container.GetUserModel(searchKeywords));
            }

            return View("Index");
        }

        public ActionResult Find(string term)
        {
            string[] products = Container.GetUserNames(term);
            return Json(products, JsonRequestBehavior.AllowGet);
        }
	}
}