using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Socials;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ISocialService
    {
        Task CreateAsync(SocialCreateVM data);
        Task EditAsync(Social social, SocialEditVM data);
        Task DeleteAsync(Social social);
        Task<IEnumerable<Social>> GetAllAsync();
        Task<SelectList> GetAllSelectedAsync();
        Task<Social> GetByIdAsync(int id);
        Task<bool> ExistAsync(string name);

    }
}
