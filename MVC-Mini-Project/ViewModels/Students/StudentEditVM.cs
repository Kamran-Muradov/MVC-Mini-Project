using MVC_Mini_Project.ViewModels.Instructors;
using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Students
{
    public class StudentEditVM
    {
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?i)[a-z]+[\ -].*[a-z]$", ErrorMessage = "Full name format is wrong")]
        public string FullName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Profession { get; set; }
        public int? CourseId { get; set; }
        public string Image { get; set; }
        public List<CourseStudentVM> CourseStudents { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
