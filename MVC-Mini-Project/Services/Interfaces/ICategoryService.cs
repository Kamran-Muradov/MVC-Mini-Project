using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Categories;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllWithCoursesAsync();
        Task<Category> GetByIdAsync(int id);
        Task<Category> GetByIdWithCoursesAsync(int id);
        Task<SelectList> GetAllSelectedAsync();
        IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> categories);
        Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(CategoryCreateVM request);
        Task EditAsync(Category category, CategoryEditVM request);
        Task DeleteAsync(Category category);
    }
}
