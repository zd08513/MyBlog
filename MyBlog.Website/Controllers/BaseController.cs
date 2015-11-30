using MyBlog.Website.MyFilter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Website.Controllers
{
    [UserFilter]
    public class BaseController : Controller
    {
    }
}