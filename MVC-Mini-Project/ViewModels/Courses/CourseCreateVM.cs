using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Courses
{
    public class CourseCreateVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        public int Rating { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public List<IFormFile> Images { get; set; }
    }
}
