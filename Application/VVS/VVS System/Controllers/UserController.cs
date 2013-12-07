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


        public ActionResult Profile(int user)
        {
            int id = -1;

            if (User.Identity.IsAuthenticated)
            {
                id = Container.GetUser(User.Identity.Name).ID;
            }

            return View(Container.GetUserModel(user, id));
        }
	}
}