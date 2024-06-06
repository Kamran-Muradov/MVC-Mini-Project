using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.Models
{
    public class Information : BaseEntity
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
