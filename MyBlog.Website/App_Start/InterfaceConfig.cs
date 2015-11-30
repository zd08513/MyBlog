using MyBlog.Biz;
using MyBlog.IBiz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyBlog.Website.App_Start
{
    /// <summary>
    /// 接口配置
    /// </summary>
    public static class InterfaceConfig
    {
        /// <summary>
        /// 接口注入
        /// </summary>
        public static void Register()
        {
            NinjectDependencyResolver Resolver = new NinjectDependencyResolver();
            
            //工资接口
            Resolver.Register<ISalaryInfoBiz, SalaryInfoBiz>();
            //应聘公司接口
            Resolver.Register<ICompanieBiz, CompanieBiz>();
            //用户接口
            Resolver.Register<IUserInfoBiz, UserInfoBiz>();

            DependencyResolver.SetResolver(Resolver);
        }
    }
}
