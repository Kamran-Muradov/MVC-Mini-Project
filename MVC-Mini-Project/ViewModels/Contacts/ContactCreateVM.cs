using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Contacts
{
    public class ContactCreateVM
    {
        [Required]
        [StringLength(100)]
        public string FullName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required] 
        public string Subject { get; set; }
        [Required] 
        public string Message { get; set; }
        public Dictionary<string,string> Settings { get; set; }
    }
}
