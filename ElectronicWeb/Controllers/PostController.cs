using Microsoft.AspNetCore.Mvc;

namespace ElectronicWeb.Controllers
{
    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
