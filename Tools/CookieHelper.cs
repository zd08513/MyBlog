/// <summary>
/// 类说明：CookieHelper
/// 联系方式：361983679  
/// 更新网站：[url=http://www.sufeinet.com/thread-655-1-1.html]http://www.sufeinet.com/thread-655-1-1.html[/url]
/// </summary>
using System;
using System.Web;
 
namespace Tools
{
    public class CookieHelper
    {
        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="httpOnly">不能通过客户端脚本访问为：true</param>
        public static void SetCookie(string key, string value, bool httpOnly = true)
        {
            //定义cookie对象
            HttpCookie cookie = new HttpCookie(key);
            //设置值
            cookie.Value = value;
            cookie.HttpOnly = httpOnly;
            //添加到响应体中
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 设置cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="saveTime">有效时间</param>
        /// <param name="httpOnly">不能通过客户端脚本访问为：true</param>
        public static void SetCookie(string key, string value, DateTime saveTime, bool httpOnly = true)
        {
            //定义cookie对象
            HttpCookie cookie = new HttpCookie(key)
            {
                //设置值
                Value = value,
                //设置过期时间
                Expires = saveTime,
                HttpOnly = httpOnly
            };
            //添加到响应体中
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// 读取cookie
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {
            string result = string.Empty;
            HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(key);
            if (cookie != null)
            {
                result = cookie.Value;
            }
            return result;
        }
        /// <summary>
        /// 清除指定Cookie
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        public static void ClearCookie(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddYears(-3);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
        }
        /// <summary>
        /// 获取指定Cookie值
        /// </summary>
        /// <param name="cookiename">cookiename</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookiename)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookiename];
            string str = string.Empty;
            if (cookie != null)
            {
                str = cookie.Value;
            }
            return str;
        }
        /// <summary>
        /// 添加一个Cookie（24小时过期）
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }
        /// <summary>
        /// 添加一个Cookie
        /// </summary>
        /// <param name="cookiename">cookie名</param>
        /// <param name="cookievalue">cookie值</param>
        /// <param name="expires">过期时间 DateTime</param>
        public static void SetCookie(string cookiename, string cookievalue, DateTime expires)
        {
            HttpCookie cookie = new HttpCookie(cookiename)
            {
                Value = cookievalue,
                Expires = expires
            };
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}