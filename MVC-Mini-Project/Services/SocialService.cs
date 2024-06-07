using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _context;

        public SocialService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<SelectList> GetAllSelectedAsync()
        {
            var socials = await _context.Socials.ToListAsync();

            return new SelectList(socials, "Id", "Name");
        }
    }
}
