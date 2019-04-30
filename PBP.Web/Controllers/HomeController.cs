using Microsoft.AspNetCore.Mvc;

namespace PBP.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
