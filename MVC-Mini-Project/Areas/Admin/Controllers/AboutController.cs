using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Abouts;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly IAboutService _aboutService;

        public AboutController(IAboutService aboutService)
        {
            _aboutService = aboutService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var abouts = await _aboutService.GetAllAsync();

            return View(abouts.Select(m => new AboutVM
            {
                Id = m.Id,
                Title = m.Title,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                Image = m.Image
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AboutCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(500))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            await _aboutService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _aboutService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _aboutService.DeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _aboutService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new AboutEditVM
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, AboutEditVM request)
        {
            if (id is null) return BadRequest();

            var slider = await _aboutService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            if (!ModelState.IsValid)
            {
                request.Image = slider.Image;
                return View(request);
            }

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Input can accept only image format");
                    request.Image = slider.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(500))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    request.Image = slider.Image;
                    return View(request);
                }
            }

            await _aboutService.EditAsync(slider, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _aboutService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new AboutDetailVM
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image,
                CreatedDate = slider.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = slider.UpdatedDate != null ? slider.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A"
            });
        }
    }
}
