using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class InformationViewComponent : ViewComponent
    {
        private readonly IInformationService _informationService;

        public InformationViewComponent(IInformationService informationService)
        {
            _informationService = informationService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var sliders = await _informationService.GetAllAsync();

            var response = sliders.Select(m => new InformationVMVC
            {
                Title = m.Title,
                Description = m.Description,
                Image = m.Image
            });

            return await Task.FromResult(View(response));
        }
    }

    public class InformationVMVC
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
