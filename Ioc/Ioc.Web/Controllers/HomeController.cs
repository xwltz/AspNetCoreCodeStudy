using System.Web.Mvc;
using Ico.IDal;
using Ioc.IBll;

namespace Ioc.Web.Controllers
{
    public class HomeController : Controller
    {
        //public IDataAccess Ida { get; set; }

        private readonly IDataAccess _ida;

        public HomeController(IDataAccess ida)
        {
            _ida = ida;
        }

        public ActionResult Index()
        {
            var result = _ida.Get();
            return Content(result);
        }
    }
}