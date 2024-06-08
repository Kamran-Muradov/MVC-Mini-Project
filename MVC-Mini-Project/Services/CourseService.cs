using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Courses;

namespace MVC_Mini_Project.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CourseService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(CourseCreateVM data)
        {
            List<CourseImage> images = new();

            foreach (var item in data.Images)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                string path = _env.GenerateFilePath("img", fileName);

                await item.SaveFileToLocalAsync(path);

                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;

            Course course = new()
            {
                Name = data.Name,
                Description = data.Description,
                CategoryId = data.CategoryId,
                InstructorId = data.InstructorId,
                Rating = data.Rating,
                Price = decimal.Parse(data.Price),
                CourseImages = images,
                StartDate = data.StartDate,
                EndDate = data.EndDate
            };

            await _context.Courses.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            foreach (var item in course.CourseImages)
            {
                string path = _env.GenerateFilePath("img", item.Name);
                path.DeleteFileFromLocal();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Course course, CourseEditVM data)
        {
            if (data.NewImages is not null)
            {
                foreach (var item in data.NewImages)
                {
                    string fileName = $"{Guid.NewGuid()}-{item.FileName}";

                    string path = _env.GenerateFilePath("img", fileName);

                    await item.SaveFileToLocalAsync(path);

                    course.CourseImages.Add(new CourseImage { Name = fileName });
                }
            }

            course.Name = data.Name;
            course.Description = data.Description;
            course.Price = decimal.Parse(data.Price);
            course.CategoryId = data.CategoryId;
            course.InstructorId = data.InstructorId;
            course.Rating = data.Rating;
            course.StartDate = data.StartDate;
            course.EndDate = data.EndDate;
            course.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Courses
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .Include(m => m.CourseImages)
                .Include(m => m.Category)
                .Include(m => m.Instructor)
                .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPopularAsync()
        {
            return await _context.Courses
                .OrderByDescending(m => m.Rating)
                .Include(m => m.CourseImages)
                .Include(m => m.Instructor)
                .ToListAsync();
        }

        public IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> courses)
        {
            return courses.Select(m => new CourseVM
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price,
                CategoryName = m.Category.Name,
                Instructor = m.Instructor.FullName,
                MainImage = m.CourseImages.FirstOrDefault(i => i.IsMain)?.Name
            });
        }

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FindAsync(id);
        }

        public async Task<Course> GetByIdWithImagesAsync(int id)
        {
            return await _context.Courses
                .Where(m => m.Id == id)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync();
        }

        public async Task<Course> GetByIdWithAllDatasAsync(int id)
        {
            return await _context.Courses
                .Where(m => m.Id == id)
                .Include(m => m.Category)
                .Include(m => m.Instructor)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task DeleteCourseImageAsync(MainAndDeleteImageVM data)
        {
            var course = await _context.Courses
                .Where(m => m.Id == data.CourseId)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync();

            var courseImage = course.CourseImages.FirstOrDefault(m => m.Id == data.ImageId);

            _context.CourseImages.Remove(courseImage);
            await _context.SaveChangesAsync();

            string path = _env.GenerateFilePath("img", courseImage.Name);
            path.DeleteFileFromLocal();
        }

        public async Task SetMainImageAsync(MainAndDeleteImageVM data)
        {
            var course = await _context.Courses
                .Where(m => m.Id == data.CourseId)
                .Include(m => m.CourseImages)
                .FirstOrDefaultAsync();

            var courseImage = course.CourseImages.FirstOrDefault(m => m.Id == data.ImageId);

            course.CourseImages.FirstOrDefault(m => m.IsMain).IsMain = false;
            courseImage.IsMain = true;
            await _context.SaveChangesAsync();
        }
    }
}
