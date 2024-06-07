using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Abouts;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IAboutService
    {
        Task<IEnumerable<About>> GetAllAsync();
        Task<About> GetByIdAsync(int id);
        Task<About> GetFirstAsync();
        Task CreateAsync(AboutCreateVM data);
        Task DeleteAsync(About about);
        Task EditAsync(About about, AboutEditVM data);
    }
}
