using Microsoft.AspNetCore.Mvc;

namespace MVC_Mini_Project.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
