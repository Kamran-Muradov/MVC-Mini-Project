using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Users;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserRoleVM>> GetAllWithRolesMappedAsync(List<AppUser> users);
    }
}
