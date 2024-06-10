using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public CategoryViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var categories = await _categoryService.GetAllWithCoursesAsync();

            

            return await Task.FromResult(View(categories.Select(m => new CategoryVMVC
            {
                Name = m.Name,
                Image = m.Image,
                CourseCount = m.Courses.Count
            })
                .OrderByDescending(m => m.CourseCount)
            ));
        }
    }

    public class CategoryVMVC
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public int CourseCount { get; set; }
    }
}
