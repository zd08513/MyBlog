using MyBlog.Website.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Website.MyFilter
{
    public class UserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            object model = UserInfoExtensions.GetUserInfo();
            if (model == null) filterContext.Result = new RedirectResult("/login/login");
        }
    }
}