using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Categories;

namespace MVC_Mini_Project.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CategoryService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Category>> GetAllWithCoursesAsync()
        {
            return await _context.Categories
                //.Include(m => m.Courses)
                .ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> GetByIdWithCoursesAsync(int id)
        {
            return await _context.Categories
                .Where(m => m.Id == id)
                //.Include(m => m.Courses)
                .FirstOrDefaultAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var categories = await _context.Categories.ToListAsync();

            return new SelectList(categories, "Id", "Name");
        }

        public IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> categories)
        {
            return categories.Select(m => new CategoryCourseVM
            {
                Id = m.Id,
                Name = m.Name,
                CreatedDate = m.CreatedDate.ToString("MM.dd.yyyy"),
                //CourseCount = m.Courses.Count
            });
        }

        public async Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Categories
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                //.Include(m => m.Courses)
                .ToListAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task CreateAsync(CategoryCreateVM request)
        {
            string fileName = $"{Guid.NewGuid()}-{request.Image.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await request.Image.SaveFileToLocalAsync(path);

            await _context.Categories.AddAsync(new Category
            {
                Name = request.Name.Trim(),
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Category category, CategoryEditVM request)
        {
            if (request.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", category.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{request.NewImage.FileName}";
                string newPath = _env.GenerateFilePath("img", fileName);
                await request.NewImage.SaveFileToLocalAsync(newPath);

                category.Image = fileName;
            }

            category.Name = request.Name.Trim();
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            string imagePath = _env.GenerateFilePath("img", category.Image);
            imagePath.DeleteFileFromLocal();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
