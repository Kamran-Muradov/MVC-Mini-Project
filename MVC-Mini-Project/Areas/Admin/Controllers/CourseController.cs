using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Helpers;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Courses;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public CourseController(
            ICourseService courseService,
            ICategoryService categoryService,
            IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _courseService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _courseService.GetMappedDatas(courses);

            int totalPage = await GetPageCountAsync(4);

            Paginate<CourseVM> response = new(mappedDatas, totalPage, page);

            return View(response);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
            ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (await _courseService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "Course with this name already exists");
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (request.StartDate >= request.EndDate)
            {
                ModelState.AddModelError("EndDate", "End date must be bigger than start date");
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                    ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");
                    ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                    return View();
                }
            }

            await _courseService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithImagesAsync((int)id);

            if (course is null) return NotFound();

            await _courseService.DeleteAsync(course);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
            ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);

            return View(new CourseEditVM
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price.ToString(),
                CategoryId = course.CategoryId,
                InstructorId = course.InstructorId,
                Rating = course.Rating,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Images = course.CourseImages.Select(m => new CourseImageEditVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsMain = m.IsMain,
                    CourseId = m.CourseId

                }).ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            List<CourseImageEditVM> images = course.CourseImages
                .Select(m => new CourseImageEditVM
                {
                    Id = m.Id,
                    Name = m.Name,
                    IsMain = m.IsMain,
                })
                .ToList();

            request.Images = images;

            if (!ModelState.IsValid)
            {
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (course.Name.Trim().ToLower() != request.Name.Trim().ToLower() && await _courseService.ExistAsync(request.Name))
            {
                ModelState.AddModelError("Name", "Course with this name already exists");
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.StartDate >= request.EndDate)
            {
                ModelState.AddModelError("EndDate", "End date must be bigger than start date");
                ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.NewImages is not null)
            {
                foreach (var item in request.NewImages)
                {
                    if (!item.CheckFileSize(500))
                    {
                        ModelState.AddModelError("Images", "Image size can be max 500 Kb");
                        ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        return View(request);
                    }

                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("Images", "File type must be only image");
                        ViewBag.categories = _categoryService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        ViewBag.instructors = _instructorService.GetAllSelectedAsync().Result.OrderBy(m => m.Text);
                        return View(request);
                    }
                }
            }

            await _courseService.EditAsync(course, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourseImage(MainAndDeleteImageVM request)
        {
            await _courseService.DeleteCourseImageAsync(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SetMainImage(MainAndDeleteImageVM request)
        {
            await _courseService.SetMainImageAsync(request);

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var course = await _courseService.GetByIdWithAllDatasAsync((int)id);

            if (course is null) return NotFound();

            return View(new CourseDetailVM
            {
                Name = course.Name,
                Description = course.Description,
                Price = course.Price,
                Category = course.Category.Name,
                Instructor = course.Instructor.FullName,
                Rating = course.Rating,
                CreatedDate = course.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = course.UpdatedDate != null ? course.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A",
                StartDate = course.StartDate.ToString("MM.dd.yyyy"),
                EndDate = course.EndDate.ToString("MM.dd.yyyy"),
                Images = course.CourseImages.Select(i => new CourseImageVM
                {
                    Name = i.Name,
                    IsMain = i.IsMain
                })
                .ToList()
            });
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int courseCount = await _courseService.GetCountAsync();

            return (int)Math.Ceiling((decimal)courseCount / take);
        }
    }
}
