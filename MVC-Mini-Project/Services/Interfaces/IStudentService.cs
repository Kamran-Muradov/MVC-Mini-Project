using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Students;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateAsync(StudentCreateVM data);
        Task EditAsync(Student student, StudentEditVM data);
        Task DeleteAsync(Student student);
        Task DeleteCourseStudentAsync( StudentCourseDeleteVM data);
        Task<IEnumerable<Student>> GetAllPaginateAsync(int page, int take);
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<Student> GetByIdWithCoursesAsync(int id);
        Task<int> GetCountAsync();
        IEnumerable<StudentVM> GetMappedDatas(IEnumerable<Student> students);
        Task<bool> ExistEmailAsync(string email);
        Task<bool> ExistPhoneAsync(string phone);
    }
}
