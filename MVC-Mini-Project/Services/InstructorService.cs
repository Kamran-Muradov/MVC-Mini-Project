using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Instructors;

namespace MVC_Mini_Project.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public InstructorService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(InstructorCreateVM data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.Image.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.Image.SaveFileToLocalAsync(path);

            await _context.Instructors.AddAsync(new Instructor
            {
                FullName = data.FullName.Trim(),
                Email = data.Email.Trim(),
                Address = data.Address.Trim(),
                Phone = data.Phone.Trim(),
                Field = data.Field.Trim(),
                Image = fileName

            });

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Instructor instructor, InstructorEditVM data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", instructor.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";
                string newPath = _env.GenerateFilePath("img", fileName);
                await data.NewImage.SaveFileToLocalAsync(newPath);

                instructor.Image = fileName;
            }

            instructor.FullName = data.FullName;
            instructor.Address = data.Address;
            instructor.Phone = data.Phone;
            instructor.Email = data.Email;
            instructor.Field = data.Field;
            instructor.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

         public async Task DeleteAsync(Instructor instructor)
        {
            string imagePath = _env.GenerateFilePath("img", instructor.Image);
            imagePath.DeleteFileFromLocal();

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync();
        }

        public async Task AddSocialAsync(int id, InstructorAddSocialVM data)
        {
            await _context.InstructorSocials.AddAsync(new InstructorSocial
            {
                InstructorId = id,
                SocialId = data.SocialId,
                Link = data.SocialLink
            });

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Instructors
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Instructor>> GetAllWithSocialsAsync()
        {
            return await _context.Instructors
                .Include(m => m.InstructorSocials)
                .ThenInclude(m => m.Social)
                .ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var instructors = await _context.Instructors.ToListAsync();

            return new SelectList(instructors, "Id", "FullName");
        }

        public async Task<Instructor> GetByIdAsync(int id)
        {
            return await _context.Instructors.FindAsync(id);
        }

        public async Task<Instructor> GetByIdWithSocialsAsync(int id)
        {
            return await _context.Instructors
                .Where(m => m.Id == id)
                .Include(m => m.InstructorSocials)
                .ThenInclude(m => m.Social)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteSocialAsync(InstructorSocialDeleteVM data)
        {
            var social = await _context.InstructorSocials
                .FirstOrDefaultAsync(m => m.SocialId == data.SocialId &&
                                     m.InstructorId == data.InstructorId &&
                                     m.Link == data.Link);

            _context.InstructorSocials.Remove(social);

            await _context.SaveChangesAsync();
        }

        public IEnumerable<InstructorVM> GetMappedDatas(IEnumerable<Instructor> instructors)
        {
            return instructors.Select(m => new InstructorVM
            {
                Id = m.Id,
                FullName = m.FullName,
                Field = m.Field,
                Image = m.Image
            });
        }


        public async Task<int> GetCountAsync()
        {
            return await _context.Instructors.CountAsync();
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            return await _context.Instructors.AnyAsync(m => m.Email.Trim().ToLower() == email.Trim().ToLower());

        }

        public async Task<bool> ExistPhoneAsync(string phone)
        {
            return await _context.Instructors.AnyAsync(m => m.Phone.Trim().ToLower() == phone.Trim().ToLower());

        }
    }
}
