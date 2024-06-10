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

        [HttpGet]
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SocialCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (await _socialService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "Social with this name already exists");
                return View();
            }

            await _socialService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var social = await _socialService.GetByIdAsync((int)id);

            if (social is null) return NotFound();

            return View(new SocialEditVM { Name = social.Name, Icon = social.Icon });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, SocialEditVM request)
        {
            if (id is null) return BadRequest();

            var social = await _socialService.GetByIdAsync((int)id);

            if (social is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (request.Name.Trim().ToLower() != social.Name.Trim().ToLower() && await _socialService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "Social with this name already exists");
                return View();
            }

            await _socialService.EditAsync(social, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _socialService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _socialService.DeleteAsync(category);

            return Ok();
        }
    }
}
