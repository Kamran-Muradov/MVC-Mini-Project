using MVC_Mini_Project.Models;
using MVC_Mini_Project.ViewModels.Contacts;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactCreateVM data);
        Task DeleteAsync(Contact contact);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task <Contact> GetByIdAsync(int id);

    }
}
