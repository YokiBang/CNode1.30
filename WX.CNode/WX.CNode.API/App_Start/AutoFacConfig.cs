using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WX.CNode.API
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using Autofac.Integration.WebApi;
    using WX.CNode.IRepository;
    using WX.CNode.Repository;
    using System.Reflection;
    using System.Web.Http;
    using System.Web.Mvc;

    public static class AutoFacConfig
    {
        public static void Register()
        {
            //定义容器
            var buider = new ContainerBuilder();
            //实现类和接口注入容器
            SetupResolveRules(buider);
            //注册所有的apicontrollers
            buider.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            var container = buider.Build();

            //注册api容器所需要使用的HttpConfiguration对象
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        /// <summary>
        /// 将实现类与接口注入到IOC容器中
        /// </summary>
        /// <param name="builder"></param>
        public static void SetupResolveRules(ContainerBuilder container)
        {
            container.RegisterType<ActiveRepository>().As<IActiveRepository>();
        }
    }
}