using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Helpers;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Students;

namespace MVC_Mini_Project.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ICourseService _courseService;

        public StudentController(
            IStudentService studentService,
            ICourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var students = await _studentService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _studentService.GetMappedDatas(students);

            int totalPage = await GetPageCountAsync(4);

            Paginate<StudentVM> response = new(mappedDatas, totalPage, page);

            return View(response);
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(StudentCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (await _studentService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "Student with this email already exists");
                ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (await _studentService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "Student with this phone already exists");
                ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (!request.Image.CheckFileType("image/"))
            {
                ModelState.AddModelError("Image", "Input can accept only image format");
                ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            if (!request.Image.CheckFileSize(300))
            {
                ModelState.AddModelError("Image", "Image size must be max 300 KB");
                ViewBag.courses = _courseService.GetAllSelectedActiveAsync().Result.OrderBy(m => m.Text);
                return View();
            }

            await _studentService.CreateAsync(request);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _studentService.GetByIdWithCoursesAsync((int)id);

            if (student is null) return NotFound();

            ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);

            return View(new StudentEditVM
            {
                FullName = student.FullName,
                Description = student.Description,
                Address = student.Address,
                Email = student.Email,
                Profession = student.Profession,
                Image = student.Image,
                Phone = student.Phone,
                CourseStudents = student.CourseStudents.Select(m => new CourseStudentVM
                {
                    StudentId = student.Id,
                    CourseId = m.CourseId,
                    CourseName = m.Course.Name
                })
                    .ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, StudentEditVM request)
        {
            if (id is null) return BadRequest();

            var student = await _studentService.GetByIdWithCoursesAsync((int)id);

            if (student is null) return NotFound();

            request.CourseStudents = student.CourseStudents.Select(m => new CourseStudentVM
            {
                StudentId = student.Id,
                CourseId = m.CourseId,
                CourseName = m.Course.Name
            })
                .ToList();

            if (!ModelState.IsValid)
            {
                request.Image = student.Image;
                ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.Email.Trim().ToLower() != student.Email.Trim().ToLower() && await _studentService.ExistEmailAsync(request.Email))
            {
                ModelState.AddModelError("Email", "Student with this email already exists");
                request.Image = student.Image;
                ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.Phone.Trim().ToLower() != student.Phone.Trim().ToLower() && await _studentService.ExistPhoneAsync(request.Phone))
            {
                ModelState.AddModelError("Phone", "Student with this phone already exists");
                request.Image = student.Image;
                ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);
                return View(request);
            }

            if (request.NewImage is not null)
            {
                if (!request.NewImage.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Image", "Input can accept only image format");
                    request.Image = student.Image;
                    ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);
                    return View(request);
                }

                if (!request.NewImage.CheckFileSize(300))
                {
                    ModelState.AddModelError("Image", "Image size must be max 300 KB");
                    request.Image = student.Image;
                    ViewBag.courses = _courseService.GetAllSelectedAvailableAsync((int)id).Result.OrderBy(m => m.Text);
                    return View(request);
                }
            }

            await _studentService.EditAsync(student, request);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCourseStudent(StudentCourseDeleteVM request)
        {
            await _studentService.DeleteCourseStudentAsync(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _studentService.GetByIdAsync((int)id);

            if (student is null) return NotFound();

            await _studentService.DeleteAsync(student);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();

            var student = await _studentService.GetByIdWithCoursesAsync((int)id);

            if (student is null) return NotFound();

            return View(new StudentDetailVM
            {
                FullName = student.FullName,
                Address = student.Address,
                Email = student.Email,
                Profession = student.Profession,
                Image = student.Image,
                Phone = student.Phone,
                CreatedDate = student.CreatedDate.ToString("MM.dd.yyyy"),
                UpdatedDate = student.UpdatedDate != null ? student.UpdatedDate.Value.ToString("MM.dd.yyyy") : "N/A",
                Courses = student.CourseStudents.Select(m => m.Course.Name).ToList()
            });
        }
        private async Task<int> GetPageCountAsync(int take)
        {
            int studentCount = await _studentService.GetCountAsync();

            return (int)Math.Ceiling((decimal)studentCount / take);
        }
    }
}
