using MyBlog.Factory;
using MyBlog.Website.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Configuration;
using Tools;

namespace MyBlog.Website
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BaseFactory.DbConfig = ConfigurationManager.AppSettings["FactoryConfig"];
            string access_path = Server.MapPath(@"/App_Data/MyBlogs.mdb");
            AccessHelper.connectionString = ConfigurationManager.ConnectionStrings["MyBlogAccessContext"].ToString() + access_path;

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
