using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Courses
{
    public class CourseEditVM
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Price { get; set; }
        public int CategoryId { get; set; }
        public int InstructorId { get; set; }
        public int Rating { get; set; }
        [Required(ErrorMessage = "Start date is required")]
        public DateTime? StartDate { get; set; }
        [Required(ErrorMessage = "End date is required")]
        public DateTime? EndDate { get; set; }
        public List<CourseImageEditVM> Images { get; set; }
        public List<IFormFile> NewImages { get; set; }
    }
}
