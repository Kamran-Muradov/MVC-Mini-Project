using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class CourseViewComponent : ViewComponent
    {
        private readonly ICourseService _courseService;

        public CourseViewComponent(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _courseService.GetAllPopularAsync();
            var response = courses.Select(m => new CourseVMVC
            {
                Name = m.Name,
                MainImage = m.CourseImages.FirstOrDefault(i => i.IsMain)?.Name,
                Price = m.Price,
                Rating = m.Rating,
                Instructor = m.Instructor.FullName,
                Duration = Math.Ceiling((decimal)(m.EndDate - m.StartDate).Days / 30),
                StudentCount = m.CourseStudents.Count
            });

            return await Task.FromResult(View(response));
        }
    }

    public class CourseVMVC
    {
        public string Name { get; set; }
        public string MainImage { get; set; }
        public decimal Price { get; set; }
        public decimal Rating { get; set; }
        public string Instructor { get; set; }
        public decimal Duration { get; set; }
        public int StudentCount { get; set; }
    }
}
