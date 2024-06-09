using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Socials;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SocialController : Controller
    {
        private readonly ISocialService _socialService;

        public SocialController(ISocialService socialService)
        {
            _socialService = socialService;
        }

        public async Task<IActionResult> Index()
        {
            var socials = await _socialService.GetAllAsync();

            var response = socials.Select(m => new SocialVM
            {
                Id = m.Id,
                Name = m.Name,
                Icon = m.Icon
            });

            return View(response);
        }
    }
}
