using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Categories
{
    public class CategoryEditVM
    {
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        public string Image { get; set; }
        public IFormFile NewImage { get; set; }
    }
}
