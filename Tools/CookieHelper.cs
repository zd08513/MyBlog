/// <summary>
/// ��˵����CookieHelper
/// ��ϵ��ʽ��361983679  
/// ������վ��[url=http://www.sufeinet.com/thread-655-1-1.html]http://www.sufeinet.com/thread-655-1-1.html[/url]
/// </summary>
using System;
using System.Web;
 
namespace Tools
{
    public class CookieHelper
    {
        /// <summary>
        /// ����cookie
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="httpOnly">����ͨ���ͻ��˽ű�����Ϊ��true</param>
        public static void SetCookie(string key, string value, bool httpOnly = true)
        {
            //����cookie����
            HttpCookie cookie = new HttpCookie(key);
            //����ֵ
            cookie.Value = value;
            cookie.HttpOnly = httpOnly;
            //��ӵ���Ӧ����
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// ����cookie
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="value">ֵ</param>
        /// <param name="saveTime">��Чʱ��</param>
        /// <param name="httpOnly">����ͨ���ͻ��˽ű�����Ϊ��true</param>
        public static void SetCookie(string key, string value, DateTime saveTime, bool httpOnly = true)
        {
            //����cookie����
            HttpCookie cookie = new HttpCookie(key)
            {
                //����ֵ
                Value = value,
                //���ù���ʱ��
                Expires = saveTime,
                HttpOnly = httpOnly
            };
            //��ӵ���Ӧ����
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        /// <summary>
        /// ��ȡcookie
        /// </summary>
        /// <param name="key">��</param>
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
        /// ���ָ��Cookie
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
        /// ��ȡָ��Cookieֵ
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
        /// ���һ��Cookie��24Сʱ���ڣ�
        /// </summary>
        /// <param name="cookiename"></param>
        /// <param name="cookievalue"></param>
        public static void SetCookie(string cookiename, string cookievalue)
        {
            SetCookie(cookiename, cookievalue, DateTime.Now.AddDays(1.0));
        }
        /// <summary>
        /// ���һ��Cookie
        /// </summary>
        /// <param name="cookiename">cookie��</param>
        /// <param name="cookievalue">cookieֵ</param>
        /// <param name="expires">����ʱ�� DateTime</param>
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