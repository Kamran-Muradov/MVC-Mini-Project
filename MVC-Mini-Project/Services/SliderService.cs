using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Sliders;

namespace MVC_Mini_Project.Services
{
    public class SliderService : ISliderService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public SliderService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IEnumerable<Slider>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Sliders
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Slider>> GetAllAsync()
        {
            return await _context.Sliders.ToListAsync();
        }

        public async Task<Slider> GetByIdAsync(int id)
        {
            return await _context.Sliders.FindAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Sliders.CountAsync();
        }

        public async Task CreateAsync(SliderCreateVM data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.Image.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.Image.SaveFileToLocalAsync(path);

            await _context.Sliders.AddAsync(new Slider
            {
                Title = data.Title.Trim(),
                Description = data.Description.Trim(),
                Image = fileName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Slider slider)
        {
            string imagePath = _env.GenerateFilePath("img", slider.Image);
            imagePath.DeleteFileFromLocal();

            _context.Sliders.Remove(slider);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Slider slider, SliderEditVM data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", slider.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";
                string newPath = _env.GenerateFilePath("img", fileName);
                await data.NewImage.SaveFileToLocalAsync(newPath);

                slider.Image = fileName;
            }

            slider.Title = data.Title.Trim();
            slider.Description = data.Description.Trim();

            await _context.SaveChangesAsync();
        }
    }
}
