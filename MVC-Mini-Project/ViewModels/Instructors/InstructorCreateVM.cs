using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Instructors
{
    public class InstructorCreateVM
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string Address { get; set; }
        [Required]
        [StringLength(100)]
        public string Phone { get; set; }
        [Required]
        [StringLength(100)]
        public string Field { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
