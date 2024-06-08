using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Data;
using MVC_Mini_Project.Helpers.Extensions;
using MVC_Mini_Project.Models;
using MVC_Mini_Project.Services.Interfaces;
using MVC_Mini_Project.ViewModels.Students;

namespace MVC_Mini_Project.Services
{
    public class StudentService : IStudentService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public StudentService(
            AppDbContext context,
            IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task CreateAsync(StudentCreateVM data)
        {
            string fileName = $"{Guid.NewGuid()}-{data.Image.FileName}";

            string path = _env.GenerateFilePath("img", fileName);

            await data.Image.SaveFileToLocalAsync(path);

            await _context.Students.AddAsync(new Student
            {
                FullName = data.FullName.Trim(),
                Description = data.Description.Trim(),
                Email = data.Email.Trim(),
                Address = data.Address.Trim(),
                Phone = data.Phone.Trim(),
                Profession = data.Profession.Trim(),
                Image = fileName

            });

            await _context.SaveChangesAsync();

            var addedStudent = await _context.Students.OrderByDescending(m => m.Id).FirstOrDefaultAsync();

            await _context.CourseStudents.AddAsync(new CourseStudent
            {
                CourseId = data.CourseId,
                StudentId = addedStudent.Id
            });

            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Student student, StudentEditVM data)
        {
            if (data.NewImage is not null)
            {
                string oldPath = _env.GenerateFilePath("img", student.Image);
                oldPath.DeleteFileFromLocal();

                string fileName = $"{Guid.NewGuid()}-{data.NewImage.FileName}";
                string newPath = _env.GenerateFilePath("img", fileName);
                await data.NewImage.SaveFileToLocalAsync(newPath);

                student.Image = fileName;
            }

            if (data.CourseId is not null)
            {
                await _context.CourseStudents.AddAsync(new CourseStudent
                {
                    CourseId = (int)data.CourseId,
                    StudentId = student.Id
                });
            }

            student.FullName = data.FullName;
            student.Description = data.Description;
            student.Address = data.Address;
            student.Phone = data.Phone;
            student.Email = data.Email;
            student.Profession = data.Profession;
            student.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            string imagePath = _env.GenerateFilePath("img", student.Image);
            imagePath.DeleteFileFromLocal();

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCourseStudentAsync(StudentCourseDeleteVM data)
        {
            var courseStudent = await _context.CourseStudents
                .FirstOrDefaultAsync(m => m.CourseId == data.CourseId && m.StudentId == data.StudentId);

            _context.CourseStudents.Remove(courseStudent);

            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Student>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Students
                .OrderByDescending(m => m.Id)
                .Skip((page - 1) * take)
                .Take(take)
                .ToListAsync();
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetByIdWithCoursesAsync(int id)
        {
            return await _context.Students
                .Where(m => m.Id == id)
                .Include(m => m.CourseStudents)
                .ThenInclude(m => m.Course)
                .FirstOrDefaultAsync();
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Students.CountAsync();
        }

        public IEnumerable<StudentVM> GetMappedDatas(IEnumerable<Student> students)
        {
            return students.Select(m => new StudentVM
            {
                Id = m.Id,
                FullName = m.FullName,
                Profession = m.Profession,
                Image = m.Image
            });
        }

        public async Task<bool> ExistEmailAsync(string email)
        {
            return await _context.Students.AnyAsync(m => m.Email.Trim().ToLower() == email.Trim().ToLower());

        }

        public async Task<bool> ExistPhoneAsync(string phone)
        {
            return await _context.Students.AnyAsync(m => m.Phone.Trim().ToLower() == phone.Trim().ToLower());

        }
    }
}
