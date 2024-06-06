using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Informations;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IInformationService
    {
        Task<IEnumerable<Information>> GetAllPaginateAsync(int page, int take);
        Task<IEnumerable<Information>> GetAllAsync();
        Task<Information> GetByIdAsync(int id);
        Task<int> GetCountAsync();
        Task CreateAsync(InformationCreateVM data);
        Task DeleteAsync(Information information);
        Task EditAsync(Information information, InformationEditVM data);
    }
}
