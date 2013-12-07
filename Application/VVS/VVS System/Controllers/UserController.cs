using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Container = VVS_System.Models.Container;

namespace VVS_System.Controllers
{
    public class UserController : Controller
    {
        public Container Container = Container.GetContainer();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Profile(int user)
        {
            return View(Container.GetUserModel(user));
        }
	}
}