using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Contacts;

namespace MVC_Mini_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IContactService _contactService;
        private readonly UserManager<AppUser> _userManager;

        public ContactController(
            ISettingService settingService,
            IContactService contactService,
            UserManager<AppUser> userManager)
        {
            _settingService = settingService;
            _contactService = contactService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
           

            var response = new ContactCreateVM
            {
                Settings = await _settingService.GetAllAsync()
            };

            if (User.Identity.IsAuthenticated)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                response.Email = user.Email;
            }

            return View(response);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new ContactCreateVM
                {
                    Settings = await _settingService.GetAllAsync()
                });
            }

            await _contactService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
