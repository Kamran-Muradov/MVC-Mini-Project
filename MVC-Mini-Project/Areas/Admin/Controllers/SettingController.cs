using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Settings;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class SettingController : Controller
    {
        private readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var settings = await _settingService.GetAllListedAsync();

            var response = settings.Select(m => new SettingVM
            {
                Id = m.Id,
                Key = m.Key,
                Value = m.Value,
            });

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            return View(new SettingEditVM { Value = setting.Value });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SettingEditVM request)
        {
            if (id is null) return BadRequest();

            var setting = await _settingService.GetByIdAsync((int)id);

            if (setting is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _settingService.EditAsync(setting, request);

            return RedirectToAction(nameof(Index));
        }
    }
}
