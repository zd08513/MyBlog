using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlogs.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ServerError()
        {
            return View();
        }
        public ActionResult NotHtml()
        {
            return View();
        }
    }
}
