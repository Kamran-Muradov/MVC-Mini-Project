using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Informations;

namespace MVC_Mini_Project.Services
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public InformationService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Information>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Informations
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Information>> GetAllAsync()
        {
            return await _context.Informations.ToListAsync();
        }

        public async Task<Information> GetByIdAsync(int id)
        {
            return await _context.Informations.FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Informations.CountAsync();
        }

        public async Task CreateAsync(InformationCreateVM data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.Image.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.Image.SaveFileToLocalAsync(path);

            await _context.Informations.AddAsync(new Information
            {
                Title = data.Title.Trim(),
                Description = data.Description.Trim(),
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Information information)
        {
            string imagePath = _env.GenerateFilePath("img", information.Image);
            imagePath.DeleteFileFromLocal();

            _context.Informations.Remove(information);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Information information, InformationEditVM data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", information.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";
                string newPath = _env.GenerateFilePath("img", fileName);
                await data.NewImage.SaveFileToLocalAsync(newPath);

                information.Image = fileName;
            }

            information.Title = data.Title.Trim();
            information.Description = data.Description.Trim();
            information.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }
    }
}
