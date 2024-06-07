using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Instructors
{
    public class InstructorAddSocialVM
    {
        public int SocialId { get; set; }
        [Required]
        public string SocialLink { get; set; }
    }
}
