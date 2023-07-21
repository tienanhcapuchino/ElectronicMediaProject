using Microsoft.AspNetCore.Mvc;

namespace ElectronicWeb.Controllers
{
    public class ReadingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
