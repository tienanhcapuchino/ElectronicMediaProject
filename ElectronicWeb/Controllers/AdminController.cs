using Microsoft.AspNetCore.Mvc;

namespace ElectronicWeb.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
