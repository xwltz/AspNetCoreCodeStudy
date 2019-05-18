using System.Reflection;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace Autofac
{
    public class AutofacConfig
    {
        /// <summary>
        ///     初始化
        /// </summary>
        public static void Initialise()
        {
            //var builder = new ContainerBuilder();
            //var service = Assembly.Load("Repository");
            //builder.RegisterControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterAssemblyTypes(service).AsImplementedInterfaces();
            //DependencyResolver.SetResolver(new AutofacDependencyResolver(builder.Build()));

            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly);//注册所有的Controller

            //发布环境
            //builder.RegisterAssemblyTypes(typeof (MvcApplication).Assembly).Where(t => t.Name.EndsWith("Repository") && t.Name.StartsWith("Stub")).AsImplementedInterfaces();
            
            //发布环境下，使用真实的数据访问层
            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly).Where(t => t.Name.EndsWith("Repository")).AsImplementedInterfaces();

            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}