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
        private readonly IInformationIconService _informationIconService;

        public InformationController(
            IInformationService informationService,
            IInformationIconService informationIconService)
        {
            _informationService = informationService;
            _informationIconService = informationIconService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var informations = await _informationService.GetAllPaginateAsync(page, 3);

            var mappedDatas = informations.Select(m => new InformationVM
            {
                Id = m.Id,
                Title = m.Title,
                Icon = m.InformationIcon.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
            });

            int totalPageCount = await GetPageCountAsync(3);

            Paginate<InformationVM> response = new(mappedDatas, totalPageCount, page);

            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var icons = await _informationIconService.GetAllAsync();

            ViewBag.icons = icons.Select(m => new InformationIconVM
            {
                Id = m.Id,
                Class = m.Name
            });

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InformationCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                var icons = await _informationIconService.GetAllAsync();

                ViewBag.icons = icons.Select(m => new InformationIconVM
                {
                    Id = m.Id,
                    Class = m.Name
                });

                return View();
            }

            if (await _informationService.ExistAsync(request.Title))
            {
                ModelState.AddModelError("Title", "Information with this title already exists");

                var icons = await _informationIconService.GetAllAsync();

                ViewBag.icons = icons.Select(m => new InformationIconVM
                {
                    Id = m.Id,
                    Class = m.Name
                });

                return View();
            }

            await _informationService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var information = await _informationService.GetByIdAsync((int)id);

            if (information is null) return NotFound();

            await _informationService.DeleteAsync(information);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var information = await _informationService.GetByIdWithIconAsync((int)id);

            if (information is null) return NotFound();

            var icons = await _informationIconService.GetAllAsync();

            ViewBag.icons = icons.Select(m => new InformationIconVM
            {
                Id = m.Id,
                Class = m.Name
            });

            return View(new InformationEditVM
            {
                Title = information.Title,
                Description = information.Description,
                InformationIconId = information.InformationIconId
            });
        }

        public async Task<IActionResult> Edit(int? id, InformationEditVM request)
        {
            if (id is null) return BadRequest();

            var information = await _informationService.GetByIdWithIconAsync((int)id);

            if (information is null) return NotFound();

            if (!ModelState.IsValid)
            {
                var icons = await _informationIconService.GetAllAsync();

                ViewBag.icons = icons.Select(m => new InformationIconVM
                {
                    Id = m.Id,
                    Class = m.Name
                });

                request.InformationIconId = information.InformationIconId;

                return View(request);
            }

            if (request.Title.Trim().ToLower() != information.Title.Trim().ToLower() && await _informationService.ExistAsync(request.Title))
            {
                var icons = await _informationIconService.GetAllAsync();

                ViewBag.icons = icons.Select(m => new InformationIconVM
                {
                    Id = m.Id,
                    Class = m.Name
                });

                ModelState.AddModelError("Title", "Information with this title already exists");

                request.InformationIconId = information.InformationIconId;

                return View(request);
            }

            await _informationService.EditAsync(information, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var information = await _informationService.GetByIdWithIconAsync((int)id);

            if (information is null) return NotFound();

            return View(new InformationDetailVM
            {
                Title = information.Title,
                Description = information.Description,
                Icon = information.InformationIcon.Name,
                CreatedDate = information.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = information.UpdatedDate != null ? information.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A"
            });
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int informationCount = await _informationService.GetCountAsync();

            return (int)Math.Ceiling((decimal)informationCount / take);
        }
    }
}
