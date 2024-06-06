using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Helpers;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Informations;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;

        public InformationController(IInformationService informationService)
        {
            _informationService = informationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var sliders = await _informationService.GetAllPaginateAsync(page, 3);

            var mappedDatas = sliders.Select(m => new InformationVM
            {
                Id = m.Id,
                Title = m.Title,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                Image = m.Image
            });

            int totalPageCount = await GetPageCountAsync(3);

            Paginate<InformationVM> response = new(mappedDatas, totalPageCount, page);

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
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

            if (!request.Image.CheckFileSize(200))
            {
                ModelState.AddModelError("Image", "Image size must be max 200 KB");
                return View();
            }

            await _informationService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var category = await _informationService.GetByIdAsync((int)id);

            if (category is null) return NotFound();

            await _informationService.DeleteAsync(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _informationService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new InformationEditVM
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image
            });
        }

        public async Task<IActionResult> Edit(int? id, InformationEditVM request)
        {
            if (id is null) return BadRequest();

            var slider = await _informationService.GetByIdAsync((int)id);

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

                if (!request.NewImage.CheckFileSize(200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    request.Image = slider.Image;
                    return View(request);
                }
            }

            await _informationService.EditAsync(slider, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var slider = await _informationService.GetByIdAsync((int)id);

            if (slider is null) return NotFound();

            return View(new InformationDetailVM
            {
                Title = slider.Title,
                Description = slider.Description,
                Image = slider.Image,
                CreatedDate = slider.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = slider.UpdatedDate != null ? slider.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A"
            });
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _informationService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }
    }
}
