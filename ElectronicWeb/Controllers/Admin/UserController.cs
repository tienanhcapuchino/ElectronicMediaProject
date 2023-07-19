using Microsoft.AspNetCore.Mvc;

namespace ElectronicWeb.Controllers.Admin
{
    public class UserController : Controller
    {
        public IActionResult UserManager()
        {
            return View();
        }
    }
}
