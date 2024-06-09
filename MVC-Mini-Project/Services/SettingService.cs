using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Settings;

namespace MVC_Mini_Project.Services
{
    public class SettingService : ISettingService
    {
        private readonly AppDbContext _context;

        public SettingService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, string>> GetAllAsync()
        {
            return await _context.Settings.ToDictionaryAsync(m => m.Key, m => m.Value);
        }

        public async Task<IEnumerable<Setting>> GetAllListedAsync()
        {
            return await _context.Settings.ToListAsync();
        }

        public async Task<Setting> GetByIdAsync(int id)
        {
            return await _context.Settings.FindAsync(id);
        }

        public async Task EditAsync(Setting setting, SettingEditVM data)
        {
            setting.Value = data.Value;
            setting.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
        }
    }
}
