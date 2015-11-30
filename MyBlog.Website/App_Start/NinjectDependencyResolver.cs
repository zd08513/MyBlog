using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Website.App_Start
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        public IKernel Kernel { get; set; }

        public NinjectDependencyResolver()
        {
            this.Kernel = new StandardKernel();
            this.Kernel.Settings.InjectNonPublic = true;
        }

        public void Register<IFrom, TTo>() where TTo : IFrom
        {
            this.Kernel.Bind<IFrom>().To<TTo>();
        }

        public object GetService(Type serviceType)
        {
            return this.Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.Kernel.GetAll(serviceType);
        }
    }
}