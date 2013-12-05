using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        
	}
}