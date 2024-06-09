using MVC_Mini_Project.Data;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Contacts;

namespace MVC_Mini_Project.Services
{
    public class ContactService : IContactService
    {
        private readonly AppDbContext _context;

        public ContactService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ContactVM data)
        {
            await _context.Contacts.AddAsync(new Contact
            {
                FullName = data.FullName,
                Email = data.Email,
                Subject = data.Subject,
                Message = data.Message
            });

            await _context.SaveChangesAsync();
        }
    }
}
