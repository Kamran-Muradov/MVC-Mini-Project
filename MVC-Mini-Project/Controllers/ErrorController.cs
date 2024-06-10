using Microsoft.AspNetCore.Mvc;

namespace MVC_Mini_Project.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult AppNotFound()
        {
            return View();
        }

        public IActionResult AppBadRequest()
        {
            return View();
        }

        public IActionResult AppInternalServer()
        {
            return View();
        }

        [Route("Error/{statusCode}")]
        public IActionResult Error(int? statusCode)
        {
            if (statusCode.HasValue)
            {
                switch (statusCode)
                {
                    case 404:
                        return View("AppNotFound");
                    case 400:
                        return View("AppBadRequest");
                }
            }

            return View("AppInternalServer");
        }
    }
}
