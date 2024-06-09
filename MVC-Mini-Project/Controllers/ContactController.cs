using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Contacts;

namespace MVC_Mini_Project.Controllers
{
    public class ContactController : Controller
    {
        private readonly ISettingService _settingService;
        private readonly IContactService _contactService;

        public ContactController(
            ISettingService settingService,
            IContactService contactService)
        {
            _settingService = settingService;
            _contactService = contactService;
        }

        public async Task<IActionResult> Index()
        {
           
            return View(new ContactVM
            {
                Settings = await _settingService.GetAllAsync()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContactVM request)
        {
            if (!ModelState.IsValid)
            {
                return View("Index",new ContactVM
                {
                    Settings = await _settingService.GetAllAsync()
                });
            }

            await _contactService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }
    }
}
