using Microsoft.AspNetCore.Mvc.Rendering;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface ISocialService
    {
        Task<SelectList> GetAllSelectedAsync();
    }
}
