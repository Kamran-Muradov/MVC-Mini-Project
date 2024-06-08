using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class InstructorViewComponent : ViewComponent
    {
        private readonly IInstructorService _instructorService;

        public InstructorViewComponent(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var instructors = await _instructorService.GetAllWithSocialsAsync();

            var response = instructors.Select(m => new InstructorVMVC
            {
               FullName = m.FullName,
               Field = m.Field,
               Image = m.Image,
               Socials = m.InstructorSocials.Select(s => new InstructorSocialVMVC
               {
                   Icon = s.Social.Icon,
                   Link = s.Link
               })
            });

            return await Task.FromResult(View(response));
        }
    }

    public class InstructorVMVC
    {
        public string FullName { get; set; }
        public string Field { get; set; }
        public string Image { get; set; }
        public IEnumerable<InstructorSocialVMVC> Socials { get; set; }
    }

    public class InstructorSocialVMVC
    {
        public string Icon { get; set; }
        public string Link { get; set; }
    }
}
