using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Helpers;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Instructors;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;
        private readonly ISocialService _socialService;

        public InstructorController(
            IInstructorService instructorService,
            ISocialService socialService)
        {
            _instructorService = instructorService;
            _socialService = socialService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var instructors = await _instructorService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _instructorService.GetMappedDatas(instructors);

            int totalPage = await GetPageCountAsync(4);

            Paginate<InstructorVM> response = new(mappedDatas, totalPage, page);

            return View(response);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InstructorCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (await _instructorService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "Instructor with this email already exists");
                return View();
            }

            if (await _instructorService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "Instructor with this phone already exists");
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                return View();
            }

            if (!request.Image.CheckFileSize(300))
            {
                ModelState.AddModelError("Image", "Image size must be max 300 KB");
                return View();
            }

            await _instructorService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdWithSocialsAsync((int)id);

            if (instructor is null) return NotFound();

            return View(new InstructorEditVM
            {
                FullName = instructor.FullName,
                Address = instructor.Address,
                Email = instructor.Email,
                Field = instructor.Field,
                Image = instructor.Image,
                Phone = instructor.Phone,
                InstructorSocials = instructor.InstructorSocials.Select(m => new InstructorSocialVM
                {
                    InstructorId = instructor.Id,
                    SocialId = m.SocialId,
                    Icon = m.Social.Icon,
                    Link = m.Link
                })
                    .ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, InstructorEditVM request)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdWithSocialsAsync((int)id);

            if (instructor is null) return NotFound();

            request.InstructorSocials = instructor.InstructorSocials.Select(m => new InstructorSocialVM
                {
                    InstructorId = instructor.Id,
                    SocialId = m.SocialId,
                    Icon = m.Social.Icon,
                    Link = m.Link
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                request.Image = instructor.Image;
                return View(request);
            }

            if (request.Email.Trim().ToLower() != instructor.Email.Trim().ToLower() && await _instructorService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "Instructor with this email already exists");
                request.Image = instructor.Image;
                return View(request);
            }

            if (request.Phone.Trim().ToLower() != instructor.Phone.Trim().ToLower() && await _instructorService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "Instructor with this phone already exists");
                request.Image = instructor.Image;
                return View(request);
            }

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Input can accept only image format");
                    request.Image = instructor.Image;
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(300))
                {
                    ModelState.AddModelError("Image", "Image size must be max 300 KB");
                    request.Image = instructor.Image;
                    return View(request);
                }
            }

            await _instructorService.EditAsync(instructor, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdAsync((int)id);

            if (instructor is null) return NotFound();

            await _instructorService.DeleteAsync(instructor);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdWithSocialsAsync((int)id);

            if (instructor is null) return NotFound();

            return View(new InstructorDetailVM
            {
                FullName = instructor.FullName,
                Address = instructor.Address,
                Email = instructor.Email,
                Field = instructor.Field,
                Image = instructor.Image,
                Phone = instructor.Phone,
                CreatedDate = instructor.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = instructor.UpdatedDate != null ? instructor.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A",
                InstructorSocials = instructor.InstructorSocials.Select(m => new InstructorSocialVM
                    {
                        Icon = m.Social.Icon,
                        Link = m.Link
                    })
                    .ToList()
            });
        }

        [HttpGet]
        public async Task<IActionResult> AddSocial(int? id)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdAsync((int)id);

            if (instructor is null) return NotFound();

            ViewBag.socials = await _socialService.GetAllSelectedAsync();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSocial(int? id, InstructorAddSocialVM request)
        {
            if (id is null) return BadRequest();

            var instructor = await _instructorService.GetByIdAsync((int)id);

            if (instructor is null) return NotFound();

            await _instructorService.AddSocialAsync((int)id, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteInstructorSocial(InstructorSocialDeleteVM request)
        {
            await _instructorService.DeleteInstructorSocialAsync(request);
            return Ok();
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int instructorCount = await _instructorService.GetCountAsync();

            return (int)Math.Ceiling((decimal)instructorCount / take);
        }
    }
}
