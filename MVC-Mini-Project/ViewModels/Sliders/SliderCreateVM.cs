
using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Sliders
{
    public class SliderCreateVM
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
