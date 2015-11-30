using MyBlog.Entity.Entity;
using MyBlog.Entity.Models;
using MyBlog.IBiz;
using MyBlog.Website.Extensions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools;

namespace MyBlog.Website.Controllers
{
    public class LoginController : Controller
    {
        [Inject]
        public IUserInfoBiz Biz { get; set; }
        /// <summary>
        /// 登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
#if DEBUG
            UserInfoExtensions.SetUserInfo(new UserInfo { UserName="admin",UserId="1",CreateTime=DateTime.Now});
            return RedirectToAction("index", "home");
#endif
            if (model.ImageCode == Functions.ToConvert<string>(Session["code"]))
            {
                ModelState.AddModelError("LoginErrorMessage", "验证码有误！");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                UserInfo info = Biz.Login(model.UserName, model.PassWord);
                if (info != null)
                {
                    UserInfoExtensions.SetUserInfo(info);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ModelState.AddModelError("LoginErrorMessage", "用户名或密码有误!");
                }
            }
            return View(model);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginOut()
        {
            UserInfoExtensions.ClearUserInfo();
            return RedirectToAction("index", "login");
        }
    }
}