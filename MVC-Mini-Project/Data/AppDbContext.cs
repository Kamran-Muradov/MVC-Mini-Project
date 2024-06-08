using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Mini_Project.Models;

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

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Slider>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Information>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<About>().HasQueryFilter(m => !m.SoftDeleted);
            builder.Entity<Category>().HasQueryFilter(m => !m.SoftDeleted);

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

            base.OnModelCreating(builder);
        }
    }
}
