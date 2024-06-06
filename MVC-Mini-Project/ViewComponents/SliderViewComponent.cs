using Microsoft.AspNetCore.Mvc;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly ISliderService _sliderService;

        public SliderViewComponent(ISliderService sliderService)
        {
            _sliderService = sliderService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var sliders = await _sliderService.GetAllAsync();

            var response = sliders.Select(m => new SliderVMVC
            {
                Title = m.Title,
                Description = m.Description,
                Image = m.Image
            });

            return await Task.FromResult(View(response));
        }
    }

    public class SliderVMVC
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }

}
