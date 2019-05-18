using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Ico.IDal;
using Ioc.Dal;

namespace Ioc.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 这里一定要注意，首先依赖关系接口一定要设置为静态的，并且是public。
        /// 不然用到的时候无法调用。
        /// </summary>
        public static IContainer Container { get; set; }

        protected void Application_Start()
        {
            AutoDependency();

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        #region 手动注入
        /// <summary>
        /// 依赖注入
        /// </summary>
        public void Dependency()
        {
            //实例化控制器
            var builder = new ContainerBuilder();
            //注册类型（映射实现类）
            builder.RegisterType<OracleDal>().As<IDataAccess>();
            //注册到控制器
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();
            Container = builder.Build();
            //下面就是使用MVC的扩展 更改了MVC中的注入方式.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }
        #endregion

        #region 自动注入

        public void AutoDependency()
        {
            //实例化控制器
            var builder = new ContainerBuilder();
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.RelativeSearchPath, "*.dll").Select(Assembly.LoadFrom).ToArray();

            //注册所有实现了 IDependency 接口的类型
            var baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsSelf().AsImplementedInterfaces()
                .PropertiesAutowired().InstancePerLifetimeScope();

            //注册MVC类型
            builder.RegisterControllers(assemblies);
            builder.RegisterControllers(assemblies).PropertiesAutowired();
            builder.RegisterFilterProvider();

            builder.RegisterType<OracleDal>().As<IDataAccess>();

            Container = builder.Build();
            //下面就是使用MVC的扩展 更改了MVC中的注入方式.
            DependencyResolver.SetResolver(new AutofacDependencyResolver(Container));
        }

        #endregion
    }
}
