using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class StudentViewComponent : ViewComponent
    {
        private readonly IStudentService _studentService;

        public StudentViewComponent(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var students = await _studentService.GetAllAsync();

            var response = students.Select(m => new StudentVMVC()
            {
                FullName = m.FullName,
                Description = m.Description,
                Image = m.Image,
                Profession = m.Profession
            });

            return await Task.FromResult(View(response));
        }
    }

    public class StudentVMVC
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string Profession { get; set; }
    }
}
