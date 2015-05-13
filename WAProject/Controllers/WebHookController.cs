using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WAProject.Controllers
{
    public class WebHookController : Controller
    {
		public ActionResult Index(string hookurl)
        {
            return View ();
        }
    }
}
