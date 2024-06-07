using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_Mini_Project.Models;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IInformationIconService
    {
        Task<IEnumerable<InformationIcon>> GetAllAsync();
        Task<SelectList> GetAllSelectedAsync();
    }
}
