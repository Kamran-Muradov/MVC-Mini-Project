using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Informations;

namespace MVC_Mini_Project.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;

        public InformationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Information>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Informations
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .Include(m => m.InformationIcon)
                .ToListAsync();
        }

        public async Task<IEnumerable<Information>> GetAllAsync()
        {
            return await _context.Informations.ToListAsync();
        }

        public async Task<IEnumerable<Information>> GetAllWithIconAsync()
        {
            return await _context.Informations
                .Include(m => m.InformationIcon)
                .ToListAsync();
        }

        public async Task<Information> GetByIdAsync(int id)
        {
            return await _context.Informations.FindAsync(id);
        }

        public async Task<Information> GetByIdWithIconAsync(int id)
        {
            return await _context.Informations
                .Where(m => m.Id == id)
                .Include(m => m.InformationIcon)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Informations.CountAsync();
        }

        public async Task<bool> ExistAsync(string title)
        {
            return await _context.Informations.AnyAsync(m => m.Title.Trim().ToLower() == title.Trim().ToLower());
        }

        public async Task CreateAsync(InformationCreateVM data)
        {
            await _context.Informations.AddAsync(new Information
            {
                Title = data.Title.Trim(),
                Description = data.Description.Trim(),
                InformationIconId = data.InformationIconId
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information information)
        {
            _context.Informations.Remove(information);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Information information, InformationEditVM data)
        {
            information.Title = data.Title.Trim();
            information.Description = data.Description.Trim();
            information.InformationIconId = data.InformationIconId;
            information.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
