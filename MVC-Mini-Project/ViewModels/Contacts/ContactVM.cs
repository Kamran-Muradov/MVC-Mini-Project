using System.ComponentModel.DataAnnotations;

namespace MVC_Mini_Project.ViewModels.Contacts
{
    public class ContactVM
    {
        public Dictionary<string,string> Settings { get; set; }
        [Required]
        [StringLength(100)]
        [RegularExpression(@"^(?i)[a-z]+[\ -].*[a-z]$", ErrorMessage = "Full name format is wrong")]
        public string FullName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }
        [Required] 
        public string Subject { get; set; }
        [Required] 
        public string Message { get; set; }
    }
}
