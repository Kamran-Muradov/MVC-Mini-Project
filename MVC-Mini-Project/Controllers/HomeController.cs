﻿using Microsoft.AspNetCore.Mvc;

namespace MVC_Mini_Project.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
