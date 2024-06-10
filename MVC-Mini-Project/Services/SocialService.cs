using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Socials;

namespace MVC_Mini_Project.Services
{
    public class SocialService : ISocialService
    {
        private readonly AppDbContext _context;

        public SocialService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(SocialCreateVM data)
        {
            await _context.AddAsync(new Social
            {
                Name = data.Name,
                Icon = data.Icon
            });

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Social social, SocialEditVM data)
        {
            social.Name = data.Name;
            social.Icon = data.Icon;
            social.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Social social)
        {
            _context.Remove(social);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Social>> GetAllAsync()
        {
            return await _context.Socials
                .OrderByDescending(m => m.Id)
                .ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var socials = await _context.Socials.ToListAsync();

            return new SelectList(socials, "Id", "Name");
        }

        public async Task<SelectList> GetAllSelectedAvailableAsync(int instructorId)
        {
            var instructorSocialIds = await _context.InstructorSocials
                .Where(m => m.InstructorId == instructorId)
                .Select(m => m.SocialId)
                .ToListAsync();

            var courses = await _context.Socials
                .Where(m => instructorSocialIds.All(c => c != m.Id))
                .ToListAsync();

            return new SelectList(courses, "Id", "Name");
        }

        public async Task<Social> GetByIdAsync(int id)
        {
            return await _context.Socials.FindAsync(id);
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Socials.AnyAsync(m => m.Name.Trim().ToLower() == name.Trim().ToLower());
        }
    }
}
