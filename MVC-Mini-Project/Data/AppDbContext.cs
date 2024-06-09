using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Models;
using System.IO;

namespace MVC_Mini_Project.Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<Information> Informations { get; set; }
        public DbSet<InformationIcon> InformationIcons { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Social> Socials { get; set; }
        public DbSet<InstructorSocial> InstructorSocials { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseImage> CourseImages { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<CourseStudent> CourseStudents { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Information>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<About>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Instructor>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Social>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Course>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Student>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Setting>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Contact>().HasQueryFilter(m => !m.SoftDeleted);

            builder.Entity<Slider>().HasData(
                new Slider
                {
                    Id = 1,
                    Title = "The Best Online Learning Platform",
                    Description = "Vero elitr justo clita lorem. Ipsum dolor at sed stet sit diam no. Kasd rebum ipsum et diam justo clita et kasd rebum sea sanctus eirmod elitr.",
                    Image = "carousel-1.jpg",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Slider
                {
                    Id = 2,
                    Title = "Get Educated Online From Your Home",
                    Description = "Vero elitr justo clita lorem. Ipsum dolor at sed stet sit diam no. Kasd rebum ipsum et diam justo clita et kasd rebum sea sanctus eirmod elitr.",
                    Image = "carousel-2.jpg",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                }
                );

            builder.Entity<Setting>().HasData(
                new Setting
                {
                    Id = 1,
                    Key = "Location",
                    Value = "123 Street, New York, USA",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 2,
                    Key = "Phone",
                    Value = "+012 345 67890",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 3,
                    Key = "Email",
                    Value = "info@example.com",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 4,
                    Key = "Logo",
                    Value = "fa fa-book me-3",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 5,
                    Key = "Twitter",
                    Value = "twitter.com",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 6,
                    Key = "Facebook",
                    Value = "facebook.com",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 7,
                    Key = "Youtube",
                    Value = "youtube.com",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                },
                new Setting
                {
                    Id = 8,
                    Key = "Linkedin",
                    Value = "linkedin.com",
                    CreatedDate = DateTime.Now,
                    SoftDeleted = false,
                }
            );

            base.OnModelCreating(builder);
        }
    }
}
