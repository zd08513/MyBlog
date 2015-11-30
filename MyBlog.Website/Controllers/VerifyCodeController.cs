using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tools;

namespace MyBlog.Website.Controllers
{
    /// <summary>
    /// 控制器生成验证码
    /// </summary>
    public class VerifyCodeController : Controller
    {
        /// <summary>
        /// 输出验证码图片
        /// </summary>
        /// <returns></returns>
        public FileContentResult Index()
        {
            VerifyCode vc = new VerifyCode();
            string code = vc.CreateVerifyCode();
            Session["code"] = code;
            byte[] bytes = vc.CreateImage(code);
            return File(bytes, @"image/jpeg");
        }
    }
}