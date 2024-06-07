using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        public IFormFile Image { get; set; }
    }
}
