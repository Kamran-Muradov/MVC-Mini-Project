using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Instructors
{
    public class InstructorEditVM
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
        public string Image { get; set; }
        public List<InstructorSocialVM> InstructorSocials { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
