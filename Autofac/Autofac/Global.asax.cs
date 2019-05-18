using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Autofac
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.Initialise();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}