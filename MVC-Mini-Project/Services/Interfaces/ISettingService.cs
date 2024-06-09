using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Settings;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ISettingService
    {
        Task<Dictionary<string, string>> GetAllAsync();
        Task<IEnumerable<Setting>> GetAllListedAsync();
        Task<Setting> GetByIdAsync(int id);
        Task EditAsync(Setting setting, SettingEditVM data);
    }
}
