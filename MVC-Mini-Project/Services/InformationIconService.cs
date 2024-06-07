using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;

namespace MVC_Mini_Project.Services
{
    public class InformationIconService : IInformationIconService
    {
        private readonly AppDbContext _context;

        public InformationIconService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InformationIcon>> GetAllAsync()
        {
            return await _context.InformationIcons.ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var icons = await _context.InformationIcons.ToListAsync();

            return new SelectList(icons, "Id", "Name");
        }
    }
}
