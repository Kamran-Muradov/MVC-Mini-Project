using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Sliders;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ISliderService
    {
        Task<IEnumerable<Slider>> GetAllPaginateAsync(int page, int take);
        Task<IEnumerable<Slider>> GetAllAsync();
        Task<Slider> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task CreateAsync(SliderCreateVM data);
        Task DeleteAsync(Slider slider);
        Task EditAsync(Slider slider, SliderEditVM data);
    }
}
