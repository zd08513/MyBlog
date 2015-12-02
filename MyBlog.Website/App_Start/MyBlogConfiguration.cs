using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlog.Website.App_Start
{
    public static class MyBlogConfiguration
    {
        public static int PagerSize = int.Parse(ConfigurationManager.AppSettings["PagerSize"].ToString());
        //缓存连接字符串
        private static string dbConnectionString = ConfigurationManager.ConnectionStrings["MyBlogContext"].ConnectionString;
        //缓存数据提供器名称
        private static string dbProviderName = ConfigurationManager.ConnectionStrings["MyBlogContext"].ProviderName;

        /// <summary>
        /// 返回针对 BalloonShop 数据库的连接字符串
        /// </summary>
        public static string DbConnectionString
        {
            get
            {
                return dbConnectionString;
            }
        }
        /// <summary>
        /// 返回数据提供器名称
        /// </summary>
        public static string DbProviderName
        {
            get
            {
                return dbProviderName;
            }
        }
        /// <summary>
        /// 返回邮件服务器的地址
        /// </summary>
        public static string MailServer
        {
            get
            {
                return ConfigurationManager.AppSettings["MailServer"];
            }
        }
        /// <summary>
        /// 返回电子邮件用户名
        /// </summary>
        public static string MailUsername
        {
            get
            {
                return ConfigurationManager.AppSettings["MailUsername"];
            }
        }
        /// <summary>
        /// 返回电子邮件密码
        /// </summary>
        public static string MailPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["MailPassword"];
            }
        }
        /// <summary>
        /// 返回电子邮件发件人
        /// </summary>
        public static string MailFrom
        {
            get
            {
                return ConfigurationManager.AppSettings["MailFrom"];
            }
        }
        /// <summary>
        /// 发送错误日志邮件？
        /// </summary>
        public static bool EnableErrorLogEmail
        {
            get
            {
                return bool.Parse(ConfigurationManager.AppSettings["EnableErrorLogEmail"]);
            }
        }
        /// <summary>
        /// 返回发送错误报告的电子邮件地址
        /// </summary>
        public static string ErrorLogEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["ErrorLogEmail"];
            }
        }
    }
}
