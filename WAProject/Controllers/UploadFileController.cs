using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WAProject.Controllers
{
    public class UploadFileController : Controller
    {
		[HttpGet]
		public ActionResult Index() {
			return View ();
		}
		
		[HttpPost]
		public ActionResult Index(HttpPostedFileBase file)
		{
			string fileName = String.Empty;
			if (file.ContentLength > 0) {
				fileName = Path.GetFileName(file.FileName);
				var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
				file.SaveAs(path);
			}

			return Redirect ("/WebHook/Index/" + fileName);
		}    
    }
}