using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly IAboutService _aboutService;
        private readonly IInformationService _informationService;

        public AboutViewComponent(
            IAboutService aboutService,
            IInformationService informationService)
        {
            _aboutService = aboutService;
            _informationService = informationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var about = await _aboutService.GetFirstAsync();

            var informations = await _informationService.GetAllAsync();



            return await Task.FromResult(View(new AboutVMVC
            {
                Title = about.Title,
                Description = about.Description,
                Image = about.Image,
                Informations = informations.Select(m => m.Title).ToList()
            }));
        }
    }

    public class AboutVMVC
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public IEnumerable<string> Informations { get; set; }
    }
}
