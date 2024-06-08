using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Courses;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ICourseService
    {
        Task CreateAsync(CourseCreateVM data);
        Task DeleteAsync(Course course);
        Task EditAsync(Course course,CourseEditVM data);
        Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take);
        Task<IEnumerable<Course>> GetAllPopularAsync();
        Task<SelectList> GetAllSelectedActiveAsync();
        Task<SelectList> GetAllSelectedAvailableAsync(int studentId);
        IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> courses);
        Task<Course> GetByIdAsync(int id);
        Task<Course> GetByIdWithImagesAsync(int id);
        Task<Course> GetByIdWithAllDatasAsync(int id);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task DeleteCourseImageAsync(MainAndDeleteImageVM data);
        Task SetMainImageAsync(MainAndDeleteImageVM data);


    }
}
