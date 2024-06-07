using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Informations
{
    public class InformationEditVM
    {
        [Required]
        [StringLength(200)]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        public int InformationIconId { get; set; }
    }
}
