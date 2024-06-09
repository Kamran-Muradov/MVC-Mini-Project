using MVC_Mini_Project.ViewModels.Contacts;

namespace MVC_Mini_Project.Services.Interfaces
{
    public interface IContactService
    {
        Task CreateAsync(ContactVM data);
    }
}
