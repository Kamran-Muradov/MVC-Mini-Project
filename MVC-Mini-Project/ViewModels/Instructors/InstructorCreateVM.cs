using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?i)[a-z]+[\ -].*[a-z]$", ErrorMessage = "Full name format is wrong")]
        public string FullName { get; set; }
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
        public string Field { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
