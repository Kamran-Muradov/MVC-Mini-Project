using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Settings
{
    public class SettingEditVM
    {
        [Required]
        public string Value { get; set; }
    }
}
