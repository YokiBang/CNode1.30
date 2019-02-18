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
            //创建一个真正的AutoFac的工作容器
            var container = buider.Build();

            //注册api容器所需要使用的HttpConfiguration对象
            HttpConfiguration config = GlobalConfiguration.Configuration;
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            //将当前容器中的控制器工厂替换掉api默认的控制器工厂
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        /// <summary>
        /// 将实现类与接口注入到IOC容器中
        /// </summary>
        /// <param name="builder"></param>
        public static void SetupResolveRules(ContainerBuilder container)
        {
            container.RegisterType<ActiveRepository>().As<IActiveRepository>();
            container.RegisterType<AuthorRepository>().As<IAuthorRepository>();
            container.RegisterType<CommentRepository>().As<ICommentRepository>();
            container.RegisterType<CollectRepository>().As<ICollectRepository>();
            container.RegisterType<JobRepository>().As<IJobRepository>();
            container.RegisterType<ReadableRepository>().As<IReadableRepository>();
        }
    }
}