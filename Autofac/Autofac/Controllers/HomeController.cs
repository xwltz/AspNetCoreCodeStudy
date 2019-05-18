using System.Web.Mvc;
using Services;

namespace Autofac.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPersonRepository _personRepository;
        public HomeController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public ActionResult Index()
        {
            var students = _personRepository.GetAll();

            return View(students);
        }
    }
}