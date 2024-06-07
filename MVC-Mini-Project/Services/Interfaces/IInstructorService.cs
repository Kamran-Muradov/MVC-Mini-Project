using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Instructors;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IInstructorService
    {
        Task CreateAsync(InstructorCreateVM data);
        Task EditAsync(Instructor instructor, InstructorEditVM data);
        Task DeleteAsync(Instructor instructor);
        Task AddSocialAsync(int id, InstructorAddSocialVM data);
        Task<IEnumerable<Instructor>> GetAllPaginateAsync(int page, int take);
        Task<Instructor> GetByIdAsync(int id);
        Task<Instructor> GetByIdWithSocialsAsync(int id);
        Task DeleteSocialAsync(InstructorSocialDeleteVM data);
        IEnumerable<InstructorVM> GetMappedDatas(IEnumerable<Instructor> instructors);
        Task<int> GetCountAsync();
        Task<bool> ExistEmailAsync(string email);
        Task<bool> ExistPhoneAsync(string phone);
    }
}
